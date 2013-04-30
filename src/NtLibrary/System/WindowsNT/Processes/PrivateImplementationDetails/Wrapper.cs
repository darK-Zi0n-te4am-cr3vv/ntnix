using System.Runtime.InteropServices;
using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Processes.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public static SafeNtProcessHandle NtCreateProcess(SafeNtProcessHandle parent, SafeNtSectionHandle section, string name, ProcessAccessMask access, AllowedObjectAttributes attributes, bool inheritObjectTable)
		{
			UnicodeString wName = (UnicodeString)name;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtCreateProcess(out handle, access, ref oa, NTInternal.CreateHandleRef(parent), inheritObjectTable, NTInternal.CreateHandleRef(section), System.IntPtr.Zero, System.IntPtr.Zero);
					NtException.CheckAndThrowException(result);
					return new SafeNtProcessHandle(handle);
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		public static SafeNtProcessHandle NtCreateProcess(SafeNtProcessHandle parent, SafeNtSectionHandle section, ProcessAccessMask access, AllowedObjectAttributes attributes, bool inheritObjectTable)
		{
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, null, attributes, null, null);
				System.IntPtr handle;
				NtStatus result = Native.NtCreateProcess(out handle, access, ref oa, NTInternal.CreateHandleRef(parent), inheritObjectTable, NTInternal.CreateHandleRef(section), System.IntPtr.Zero, System.IntPtr.Zero);
				NtException.CheckAndThrowException(result);
				return new SafeNtProcessHandle(handle);
			}
		}

		public static SafeNtProcessHandle OpenCurrentProcess()
		{ return new SafeNtProcessHandle(new System.IntPtr(-1), false); }

		public static unsafe ProcessParameters* RtlCreateProcessParameters(string imageFile, string dllPath, string currentDirectory, string commandLine, uint creationFlag, string windowTitle, string desktop, string shellInfo, string runtimeInfo, out int size)
		{
			unsafe
			{
				size = sizeof(ProcessParameters);
				UnicodeString wImageFile = default(UnicodeString),
					wDllPath = default(UnicodeString),
					wCurrentDirectory = default(UnicodeString),
					wCommandLine = default(UnicodeString),
					wWindowTitle = default(UnicodeString),
					wDesktop = default(UnicodeString),
					wShellInfo = default(UnicodeString),
					wRuntimeInfo = default(UnicodeString);
				UnicodeString* pwImageFile = null,
					pwDllPath = null,
					pwCurrentDirectory = null,
					pwCommandLine = null,
					pwWindowTitle = null,
					pwDesktop = null,
					pwShellInfo = null,
					pwRuntimeInfo = null;
				if (imageFile != null)
				{
					wImageFile = (UnicodeString)imageFile;
					size += wImageFile.MaximumLength;
					pwImageFile = &wImageFile;
				}
				if (dllPath != null)
				{
					wDllPath = (UnicodeString)dllPath;
					size += wDllPath.MaximumLength;
					pwDllPath = &wDllPath;
				}
				if (currentDirectory != null)
				{
					wCurrentDirectory = (UnicodeString)currentDirectory;
					size += wCurrentDirectory.MaximumLength;
					pwCurrentDirectory = &wCurrentDirectory;
				}
				if (commandLine != null)
				{
					wCommandLine = (UnicodeString)commandLine;
					size += wCommandLine.MaximumLength;
					pwCommandLine = &wCommandLine;
				}
				if (windowTitle != null)
				{
					wWindowTitle = (UnicodeString)windowTitle;
					size += wWindowTitle.MaximumLength;
					pwWindowTitle = &wWindowTitle;
				}
				if (desktop != null)
				{
					wDesktop = (UnicodeString)desktop;
					size += wDesktop.MaximumLength;
					pwDesktop = &wDesktop;
				}
				if (shellInfo != null)
				{
					wShellInfo = (UnicodeString)shellInfo;
					size += wShellInfo.MaximumLength;
					pwShellInfo = &wShellInfo;
				}
				if (runtimeInfo != null)
				{
					wRuntimeInfo = (UnicodeString)runtimeInfo;
					size += wRuntimeInfo.MaximumLength;
					pwRuntimeInfo = &wRuntimeInfo;
				}
				IntPtr result;
				int success = Native.RtlCreateProcessParameters(out result, (IntPtr)pwImageFile, (IntPtr)pwDllPath, (IntPtr)pwCurrentDirectory, (IntPtr)pwCommandLine, creationFlag, (IntPtr)pwWindowTitle, (IntPtr)pwDesktop, (IntPtr)pwShellInfo, (IntPtr)pwRuntimeInfo);
				return (ProcessParameters*)result;
			}
		}

		private static unsafe char* InitEnvironment(NtProcess process)
		{
			char* @base = null;
			uint dummy = 0;
			uint dummySize = sizeof(uint);
			UIntPtr regionSize = (UIntPtr)dummySize;
			//System.WindowsNT.Memory.PrivateImplementationDetails.Native.NtAllocateVirtualMemory(hProcess, &@base, 0, &m, MEM_COMMIT, PAGE_READWRITE);
			Memory.NtVirtualMemoryAddress vma = new Memory.NtVirtualMemoryAddress(process, (IntPtr)@base, 0, ref regionSize, Memory.MemoryAllocationType.COMMIT, Memory.PageProtection.READWRITE, false);
			//System.WindowsNT.Memory.PrivateImplementationDetails.Native.NtWriteVirtualMemory(hProcess, @base, &dummy, dummySize, 0);
			vma.Write(new System.IO.UnmanagedMemoryStream((byte*)&dummy, 0, dummySize, System.IO.FileAccess.Read), 0);
			return @base;
		}

		public static unsafe void CreateProcessParameters(NtProcess process, PEB* Peb, string imageFile, IntPtr hPipe)
		{
			int size;
			ProcessParameters* pp = RtlCreateProcessParameters(imageFile, "C:\\;C:\\Windows\\;C:\\Windows\\SYSTEM32\\", "C:\\Windows\\SYSTEM32\\", imageFile, 0, null, null, null, null, out size);
			pp->hStdInput = hPipe;
			pp->hStdOutput = hPipe; //hStdOutPipe;
			pp->hStdError = hPipe; //hStdOutPipe;
			pp->dwFlags = StartFlags.USESTDHANDLES | StartFlags.USESHOWWINDOW;
			pp->wShowWindow = ShowWindow.SW_HIDE;
			pp->Environment = InitEnvironment(process);
			UIntPtr n = (UIntPtr)pp->Size;
			IntPtr p = IntPtr.Zero;
			//NtAllocateVirtualMemory(hProcess, ref p, 0, ref n, MEM_COMMIT, PAGE_READWRITE);
			Memory.NtVirtualMemoryAddress vma = new System.WindowsNT.Memory.NtVirtualMemoryAddress(process, p, 0, ref n, System.WindowsNT.Memory.MemoryAllocationType.COMMIT, System.WindowsNT.Memory.PageProtection.READWRITE, false);
			//NtWriteVirtualMemory(hProcess, p, pp, pp->Size, 0);
			Memory.PrivateImplementationDetails.Wrapper.NtWriteVirtualMemory(process.Handle, (IntPtr)((char*)Peb + 0x10), new System.IO.UnmanagedMemoryStream((byte*)&p, sizeof(IntPtr), sizeof(IntPtr), System.IO.FileAccess.Read), (uint)sizeof(IntPtr));
			
			Native.RtlDestroyProcessParameters((IntPtr)pp);
		}
#if false
		public static unsafe int exec_piped(PUNICODE_STRING name, PUNICODE_STRING PipeName)
		{
			HANDLE hProcess, hThread, hSection, hFile;

			ObjectAttributes oa = new ObjectAttributes(IntPtr.Zero, name, AllowedObjectAttributes.CaseInsensitive, null, null);
			IO_STATUS_BLOCK iosb;
			ZwOpenFile(&hFile, FILE_EXECUTE | SYNCHRONIZE, &oa, &iosb,
				FILE_SHARE_READ, FILE_SYNCHRONOUS_IO_NONALERT);

			oa.ObjectName = 0;

			ZwCreateSection(&hSection, SECTION_ALL_ACCESS, &oa, 0,
				PAGE_EXECUTE, SEC_IMAGE, hFile);

			ZwClose(hFile);

			ZwCreateProcess(&hProcess, PROCESS_ALL_ACCESS, &oa,
				NtCurrentProcess(), TRUE, hSection, 0, 0);

			ZwClose(hSection);

			USER_STACK stack = { 0 };

			ULONG n = section.ImageStackReserve;
			ZwAllocateVirtualMemory(hProcess, &stack.ExpandableStackBottom, 0, &n,
				MEM_RESERVE, PAGE_READWRITE);

			stack.ExpandableStackBase = PCHAR(stack.ExpandableStackBottom)
				+ section.ImageStackReserve;
			stack.ExpandableStackLimit = PCHAR(stack.ExpandableStackBase)
				- section.ImageStackCommit;

			/* PAGE_EXECUTE_READWRITE is needed if initialisation code will be executed on stack*/
			n = section.ImageStackCommit + PAGE_SIZE;
			PVOID p = PCHAR(stack.ExpandableStackBase) - n;
			ZwAllocateVirtualMemory(hProcess, &p, 0, &n,
				MEM_COMMIT, PAGE_EXECUTE_READWRITE);

			ULONG x; n = PAGE_SIZE;
			ZwProtectVirtualMemory(hProcess, &p, &n,
				PAGE_READWRITE | PAGE_GUARD, &x);

			CONTEXT context = { CONTEXT_FULL };
			context.SegGs = 0;
			context.SegFs = 0x38;
			context.SegEs = 0x20;
			context.SegDs = 0x20;
			context.SegSs = 0x20;
			context.SegCs = 0x18;
			context.EFlags = 0x3000;
			context.Esp = ULONG(stack.ExpandableStackBase) - 4;
			context.Eip = ULONG(section.ImageEntryPoint);

			CLIENT_ID cid;

			ZwCreateThread(&hThread, THREAD_ALL_ACCESS, &oa,
				hProcess, &cid, &context, &stack, TRUE);

			PROCESS_BASIC_INFORMATION pbi; // = ...get info for hProcess
			
			HANDLE hPipe, hPipe1;
			oa.ObjectName = PipeName;
			oa.Attributes = OBJ_INHERIT;
			if (ZwOpenFile(&hPipe1, GENERIC_READ | GENERIC_WRITE | SYNCHRONIZE, &oa, &iosb, FILE_SHARE_READ | FILE_SHARE_WRITE, FILE_SYNCHRONOUS_IO_NONALERT | FILE_NON_DIRECTORY_FILE)) return 0;
			ZwDuplicateObject(NtCurrentProcess(), hPipe1, hProcess, &hPipe,
				0, 0, DUPLICATE_SAME_ACCESS | DUPLICATE_CLOSE_SOURCE);

			CreateProcessParameters(hProcess, pbi.PebBaseAddress, name, hPipe);

			InformCsrss(hProcess, hThread,
				ULONG(cid.UniqueProcess), ULONG(cid.UniqueThread));

			ZwResumeThread(hThread, 0);

			ZwClose(hProcess);
			ZwClose(hThread);

			return (int)cid.UniqueProcess;
		}
#endif
		public static SafeNtProcessHandle NtOpenProcess(ClientID id, ProcessAccessMask access, AllowedObjectAttributes attributes)
		{
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, null, attributes, null, null);
				System.IntPtr handle;
				NtStatus result = Native.NtOpenProcess(out handle, access, ref oa, &id);
				NtException.CheckAndThrowException(result);
				return new SafeNtProcessHandle(handle);
			}
		}

		public static SafeNtProcessHandle NtOpenProcess(string name, ProcessAccessMask access, AllowedObjectAttributes attributes)
		{
			UnicodeString wName = (UnicodeString)name;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtOpenProcess(out handle, access, ref oa, null);
					NtException.CheckAndThrowException(result);
					return new SafeNtProcessHandle(handle);
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		private static T NtQueryInformationProcess<T>(SafeNtProcessHandle processHandle, ProcessInfoClass processInfoClass)
			where T : struct
		{
			uint inputLength = (uint)Marshal.SizeOf(typeof(T));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				uint returnLength;
				NtStatus result = Native.NtQueryInformationProcess(NTInternal.CreateHandleRef(processHandle), processInfoClass, pBuffer, inputLength, out returnLength);
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL | result == NtStatus.INFO_LENGTH_MISMATCH)
				{
					inputLength = returnLength;
					pBuffer = Marshal.ReAllocHGlobal(pBuffer, new IntPtr(inputLength));
					result = Native.NtQueryInformationProcess(NTInternal.CreateHandleRef(processHandle), processInfoClass, pBuffer, inputLength, out returnLength);
				}
				NtException.CheckAndThrowException(result);
				return (T)Marshal.PtrToStructure(pBuffer, typeof(T));
			}
			finally
			{
				Marshal.FreeHGlobal(pBuffer);
			}
		}

		public static ProcessBasicInformation NtQueryInformationProcessBasic(SafeNtProcessHandle processHandle)
		{
			ProcessBasicInformation output = NtQueryInformationProcess<ProcessBasicInformation>(processHandle, ProcessInfoClass.ProcessBasicInformation);
			return output;
		}

		public static ProcessDeviceMapInformation NtQueryInformationProcessDeviceMap(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<ProcessDeviceMapInformation>(processHandle, ProcessInfoClass.ProcessDeviceMap); }

		public static ProcessSessionInformation NtQueryInformationProcessSession(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<ProcessSessionInformation>(processHandle, ProcessInfoClass.ProcessSessionInformation); }

		public static ProcessPooledUsageAndLimits NtQueryInformationProcessPooledUsageAndLimits(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<ProcessPooledUsageAndLimits>(processHandle, ProcessInfoClass.ProcessPooledUsageAndLimits); }

		//The rest may be incorrect...

		public static int NtQueryInformationProcessPriorityClass(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<int>(processHandle, ProcessInfoClass.ProcessPriorityClass); }

		public static int NtQueryInformationProcessPriorityBoost(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<int>(processHandle, ProcessInfoClass.ProcessPriorityBoost); }

		public static UnicodeString NtQueryInformationProcessImageName(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<UnicodeString>(processHandle, ProcessInfoClass.ProcessImageFileName); }

		/* TEMPLATE:
		public static  NtQueryInformationProcess(SafeNtProcessHandle processHandle)
		{ return NtQueryInformationProcess<>(processHandle, ProcessInfoClass); }
		*/

		public static SystemProcessesInformation[] NtQuerySystemInformationProcess()
		{
			uint returnLength;
			WindowsNT.PrivateImplementationDetails.Native.NtQuerySystemInformation(SystemInformationClass.SystemProcessesAndThreadsInformation, System.IntPtr.Zero, 0, out returnLength);
			uint inputLength = returnLength;
			System.IntPtr pInformation = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				NtStatus result = WindowsNT.PrivateImplementationDetails.Native.NtQuerySystemInformation(SystemInformationClass.SystemProcessesAndThreadsInformation, pInformation, inputLength, out returnLength);
				NtException.CheckAndThrowException(result);
				return SystemProcessesInformation.FromPtr(pInformation);
			}
			finally
			{
				Marshal.FreeHGlobal(pInformation);
			}
		}

		public static void NtTerminateProcess(SafeNtProcessHandle processHandle, NtStatus exitCode)
		{
			NtStatus result = Native.NtTerminateProcess(NTInternal.CreateHandleRef(processHandle), exitCode);
			NtException.CheckAndThrowException(result);
		}
	}
}