using System.WindowsNT.Processes.PrivateImplementationDetails;
using System.WindowsNT.Security;
using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.Memory;
using System.WindowsNT.IO;
using System.WindowsNT.Threading;
namespace System.WindowsNT.Processes
{
	public class NtProcess : NtObject
	{
		internal static MemCpyImplDelegate __memcopyimpl = (NtProcess.MemCpyImplDelegate)System.Delegate.CreateDelegate(typeof(NtProcess.MemCpyImplDelegate), typeof(Buffer), "memcpyimpl");

		private static NtProcess __current;

		protected NtProcess(SafeNtProcessHandle handle) : base() { this.Handle = handle; }

		public ProcessAccessMask AccessMask { get { return (ProcessAccessMask)this.ObjectAccessMask; } }

		public void AdjustPrivilege(Privilege privilege, bool enabled)
		{
			using (SecurityToken token = System.WindowsNT.Processes.NtProcess.Current.OpenSecurityToken(System.WindowsNT.Security.TokenAccessMask.AllAccess))
			{
				token.AdjustPrivilege(privilege, enabled);
			}
		}

		public NtProcess CreateChildProcess(string name, NtSection section, ProcessAccessMask accessMask, AllowedObjectAttributes attributes, bool inherit)
		{
			if (name != null)
			{
				return new NtProcess(Wrapper.NtCreateProcess(this.Handle, section != null ? section.Handle : null, name, accessMask, attributes, inherit));
			}
			else
			{
				return new NtProcess(Wrapper.NtCreateProcess(this.Handle, section != null ? section.Handle : null, accessMask, attributes, inherit));
			}
		}

		public SecurityToken OpenSecurityToken(TokenAccessMask accessMask)
		{ return SecurityToken.OpenProcessToken(this, accessMask); }

		public uint ExitStatus 
		{ get { return (uint)Wrapper.NtQueryInformationProcessBasic(this.Handle).ExitStatus; } }
		
		public System.IntPtr AffinityMask
		{ get { return Wrapper.NtQueryInformationProcessBasic(this.Handle).AffinityMask; } }
		
		public uint BasePriority
		{ get { return Wrapper.NtQueryInformationProcessBasic(this.Handle).BasePriority; } }

		public SafeNtProcessHandle Handle { get; private set; }

		public override SafeNtHandle GenericHandle { get { return this.Handle; } }

		public System.IntPtr UniqueProcessID
		{ get { return Wrapper.NtQueryInformationProcessBasic(this.Handle).UniqueProcessId; } }
		
		public System.IntPtr InheritedFromUniqueProcessID
		{ get { return Wrapper.NtQueryInformationProcessBasic(this.Handle).InheritedFromUniqueProcessId; } }

		public ProcessPooledUsageAndLimits PooledUsageAndLimits
		{ get { return Wrapper.NtQueryInformationProcessPooledUsageAndLimits(this.Handle); } }

		internal unsafe delegate void MemCpyImplDelegate(byte* src, byte* dest, int len);

		public NtProcess RunChild1(NtNonDirectoryFile executable, bool inherit)
		{
			//InheritAll();

			using (NtSection section = NtSection.CreateSection(null, AllowedObjectAttributes.None, PageProtection.EXECUTE_READWRITE, SectionAllocationAttributes.IMAGE, executable))
			{
				using (NtProcess process = NtProcess.CreateProcess(NtProcess.Current, null, section, ProcessAccessMask.AllAccess, AllowedObjectAttributes.None, true))
				{
					//var v = NtThread.Current.ClientID;
					SafeNtThreadHandle hThread = new SafeNtThreadHandle(System.WindowsNT.PrivateImplementationDetails.Wrapper.NtDuplicateObject(NtProcess.Current.Handle, NtThread.Current.Handle, NtProcess.Current.Handle, (WindowsNT.AccessMask)0, true, DuplicationOptions.SAME_ATTRIBUTES));
					Context context = Threading.PrivateImplementationDetails.Wrapper.NtGetContextThread(hThread);
					context.Eip = 0; //(uint)child
					MemoryBasicInformation mbi = Memory.PrivateImplementationDetails.Wrapper.NtQueryVirtualMemoryBasic(NtProcess.Current.Handle, (IntPtr)context.Esp);
					unsafe
					{
						UserStack stack = new UserStack() { ExpandableStackBase = (IntPtr)((byte*)mbi.BaseAddress + (long)mbi.RegionSize), ExpandableStackLimit = mbi.BaseAddress, ExpandableStackBottom = mbi.AllocationBase };
						ClientID cid;
						using (NtThread thread = NtThread.CreateThread(process, WindowsNT.AccessMask.MaximumAllowed, true, ref context, ref stack, out cid, AllowedObjectAttributes.None))
						{
							IntPtr tib = NtThread.Current.TebBaseAddress;
							//Memory.PrivateImplementationDetails.Wrapper.NtWriteVirtualMemory(process.Handle.DangerousGetHandle(), thread.TebBaseAddress, tib.ExceptionList, sizeof(tib.ExceptionList), 0);
							InformCsrss(process.Handle.DangerousGetHandle(), thread.Handle.DangerousGetHandle(), (uint)cid.UniqueProcess, (uint)cid.UniqueThread);

							thread.Resume();

							return process;
						}
					}
				}
			}
		}

		private void InformCsrss(IntPtr hProcess, IntPtr hThread, uint pid, uint tid)
		{
			unsafe
			{
				CSRSS_MESSAGE2 csrmsg = new CSRSS_MESSAGE2() { ProcessInformation = new ProcessInformation() { hProcess = hProcess, hThread = hThread, PID = pid, TID = tid } };
				Threading.PrivateImplementationDetails.Native.CsrClientCallServer(new IntPtr(&csrmsg), IntPtr.Zero, 0x10000, 0x24);
			}
		}

		public NtProcess RunChild2(NtNonDirectoryFile executable, string pipeName, bool inherit)
		{
			const uint PAGE_SIZE = 0x2000;

			using (NtSection section = NtSection.CreateSection(null, AllowedObjectAttributes.None, PageProtection.EXECUTE_READWRITE, SectionAllocationAttributes.IMAGE, executable))
			{
				NtProcess process = this.CreateChildProcess(null, section, ProcessAccessMask.AllAccess, AllowedObjectAttributes.None, inherit);

				UserStack stack = new UserStack();
				UIntPtr n = (UIntPtr)section.ImageStackReserve;
				NtVirtualMemoryAddress vma1 = new NtVirtualMemoryAddress(process, stack.ExpandableStackBottom, (byte)section.ImageStackZeroBits, ref n, MemoryAllocationType.RESERVE, PageProtection.EXECUTE_READWRITE, false);
				stack.ExpandableStackBottom = vma1.BaseAddress;
				unsafe
				{
					stack.ExpandableStackBase = (IntPtr)((byte*)stack.ExpandableStackBottom + section.ImageStackReserve);
					stack.ExpandableStackLimit = (IntPtr)((byte*)stack.ExpandableStackBase - section.ImageStackCommit);
				}
				n = (UIntPtr)(section.ImageStackCommit + PAGE_SIZE);
				unsafe
				{
					IntPtr p = (IntPtr)((byte*)stack.ExpandableStackBase - (long)n);
					NtVirtualMemoryAddress vma2 = new NtVirtualMemoryAddress(process, p, (byte)section.ImageStackZeroBits, ref n, MemoryAllocationType.COMMIT, PageProtection.EXECUTE_READWRITE, false);
					p = vma2.BaseAddress;

					n = (UIntPtr)PAGE_SIZE;
					uint n2 = (uint)n;
					PageProtection x = vma2.Protect(ref p, ref n2, PageProtection.READWRITE | PageProtection.GUARD);
					n = (UIntPtr)n2;

					Context context = new Context() { ContextFlags = ContextFlags.CONTEXT_FULL };
					context.SegGs = 0;
					context.SegFs = 0x38;
					context.SegEs = 0x20;
					context.SegDs = 0x20;
					context.SegSs = 0x20;
					context.SegCs = 0x18;
					context.EFlags = 0x3000;
					context.Esp = (uint)stack.ExpandableStackBase - 4;
					context.Eip = (uint)section.ImageEntryPoint;

					ClientID cid;
					using (NtThread thread = NtThread.CreateThread(process, WindowsNT.AccessMask.GenericAll, true, ref context, ref stack, out cid, AllowedObjectAttributes.CaseInsensitive))
					{
						//PROCESS_BASIC_INFORMATION pbi; // = ...get info for hProcess
						FileOpenInformation foi;
						using (NtNonDirectoryFile pipe = NtNonDirectoryFile.Open(pipeName, FileAccessMask.GenericRead | FileAccessMask.GenericWrite | FileAccessMask.Synchronize, AllowedObjectAttributes.Inherit, FileShare.Read, FileCreateOptions.SynchronousIoNonAlert, out foi, ErrorsNotToThrow.ThrowAnyError))
						{
							IntPtr hPipe = System.WindowsNT.PrivateImplementationDetails.Wrapper.NtDuplicateObject(NtProcess.Current.Handle, pipe.Handle, process.Handle, (AccessMask)0, false, DuplicationOptions.SAME_ACCESS);

							Wrapper.CreateProcessParameters(process, Wrapper.NtQueryInformationProcessBasic(process.Handle).PebBaseAddress, executable.Name, hPipe);

							//InformCsrss(hProcess, hThread, ULONG(cid.UniqueProcess), ULONG(cid.UniqueThread));

							 thread.Resume();

							return new NtProcess(new SafeNtProcessHandle(cid.UniqueProcess));
						}
					}
				}
				/*
				long? sectionOffset = null;
				IntPtr viewSize = (IntPtr)section.Size;
				section.MapView(process, @base, 0, (IntPtr)section.ImageStackCommit, viewSize, ref sectionOffset, SectionInherit.ViewShare, (MemoryAllocationType)0, PageProtection.EXECUTE_READWRITE, out @base, out viewSize);
				Context context = new Context() { Eax = (uint)section.ImageEntryPoint };
				UserStack teb = new UserStack()
				{
					ExpandableStackBase = (IntPtr)((long)section.BaseAddress + section.Size),
					ExpandableStackLimit = section.BaseAddress,
					ExpandableStackBottom = vma.Address,
				};
				unsafe
				{
					byte* imageAddress = (byte*)section.BaseAddress;
					__memcopyimpl((byte*)@base, imageAddress, (int)section.Size);
				}
				Threading.NtThread t = Threading.NtThread.CreateThread(process, WindowsNT.AccessMask.MaximumAllowed, true, context, teb, AllowedObjectAttributes.None);
				t.Resume();
				return process;
				*/
			}
		}

		public uint SessionID
		{ get { return Wrapper.NtQueryInformationProcessSession(this.Handle).SessionId; } }
		
		public string ImageName
		{ get { return Wrapper.NtQueryInformationProcessImageName(this.Handle).ToString(); } }

		public void Terminate() { this.Terminate(0); }

		public void Terminate(int exitCode)
		{ Wrapper.NtTerminateProcess(this.Handle, unchecked((WindowsNT.PrivateImplementationDetails.NtStatus)exitCode)); }

		//====================================

		public static NtProcess Current
		{
			get
			{
				if (__current == null)
					__current = new NtProcess(PrivateImplementationDetails.Wrapper.OpenCurrentProcess());
				return __current;
			}
		}

		public static ProcessInformationSnapshot[] GetAllProcessesInformation()
		{
			return System.Array.ConvertAll(Wrapper.NtQuerySystemInformationProcess(),
				delegate(SystemProcessesInformation pi)
				{
					ProcessInformationSnapshot result = new ProcessInformationSnapshot(pi);
					//UnicodeString.RtlFreeUnicodeString(ref pi.ProcessName);
					return result;
				});
		}

		public static NtProcess CreateProcess(NtProcess parent, string name, NtSection section, ProcessAccessMask accessMask, AllowedObjectAttributes attributes, bool inherit)
		{
			if (name != null)
			{
				return new NtProcess(Wrapper.NtCreateProcess(parent != null ? parent.Handle : null, section != null ? section.Handle : null, name, accessMask, attributes, inherit));
			}
			else
			{
				return new NtProcess(Wrapper.NtCreateProcess(parent != null ? parent.Handle : null, section != null ? section.Handle : null, accessMask, attributes, inherit));
			}
		}

		public static NtProcess OpenProcess(string processName, ProcessAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			return new NtProcess(Wrapper.NtOpenProcess(processName, accessMask, attributes));
		}

		public static NtProcess OpenProcess(ClientID processID, ProcessAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			return new NtProcess(Wrapper.NtOpenProcess(processID, accessMask, attributes));
		}
	}
}