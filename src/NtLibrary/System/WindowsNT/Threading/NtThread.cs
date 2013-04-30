using System.WindowsNT.Processes;
using System.WindowsNT.Threading.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Threading
{
	public class NtThread : NtObject
	{
		private static NtThread __current;

		protected NtThread(SafeNtThreadHandle handle) : base() { this.Handle = handle; }
		
		public SafeNtThreadHandle Handle { get; private set; }

		public override SafeNtHandle GenericHandle
		{ get { return this.Handle; } }

		public void Alert()
		{ Wrapper.NtAlertThread(this.Handle); }

		public void AlertResume()
		{ Wrapper.NtAlertResumeThread(this.Handle); }

		public ClientID ClientID { get { return Wrapper.NtQueryInformationThreadBasic(this.Handle).ClientId; } }

		public Context Context { get { return Wrapper.NtGetContextThread(this.Handle); } }

		internal WindowsNT.PrivateImplementationDetails.NtStatus ExitStatus 
		{ get { return Wrapper.NtQueryInformationThreadBasic(this.Handle).ExitStatus; } }

		public void Resume()
		{ Wrapper.NtResumeThread(this.Handle); }

		public void Suspend()
		{ Wrapper.NtSuspendThread(this.Handle); }

		public IntPtr TebBaseAddress { get { return Wrapper.NtQueryInformationThreadBasic(this.Handle).TebBaseAddress; } }

		public void Terminate() { this.Terminate(0); }

		public void Terminate(int exitCode)
		{ Wrapper.NtTerminateThread(this.Handle, unchecked((WindowsNT.PrivateImplementationDetails.NtStatus)exitCode)); }

		//=====================================

		public static void Continue(Context context, bool raiseAlert)
		{ Wrapper.NtContinue(context, raiseAlert); }

		public static void YieldCurrentThread()
		{
			Wrapper.NtYieldExecution();
		}

		public static NtThread Current
		{
			get
			{
				if (__current == null)
				{ __current = new NtThread(PrivateImplementationDetails.Wrapper.OpenCurrentThread()); }
				return __current;
			}
		}

		public static NtThread OpenThread(AccessMask accessMask, System.IntPtr threadID, AllowedObjectAttributes attributes)
		{
			return new NtThread(Wrapper.NtOpenThread(accessMask, new System.WindowsNT.Processes.ClientID() { UniqueThread = threadID }, attributes));
		}

		public static NtThread CreateThread(NtProcess parent, AccessMask accessMask, bool createSuspended, ref Context context, ref Processes.UserStack teb, out ClientID clientID, AllowedObjectAttributes attributes)
		{
			return new NtThread(Wrapper.NtCreateThread(accessMask, parent.Handle, ref context, createSuspended, attributes, ref teb, out clientID));
		}
	}
}