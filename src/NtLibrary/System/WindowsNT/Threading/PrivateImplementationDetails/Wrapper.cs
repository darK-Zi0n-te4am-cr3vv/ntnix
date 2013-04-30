using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using System.WindowsNT.Processes;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Threading.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public static SafeNtThreadHandle NtCreateThread(AccessMask desiredAccess, SafeNtProcessHandle parent, ref Context context, bool createSuspended, AllowedObjectAttributes attributes, ref Processes.UserStack teb, out ClientID clientID)
		{
			//UnicodeString name = (UnicodeString)processName;
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, null, attributes, null, null);
				System.IntPtr handle;
				NtStatus result = Native.NtCreateThread(out handle, desiredAccess, ref oa, NTInternal.CreateHandleRef(parent), out clientID, ref context, ref teb, createSuspended);
				//name.Dispose();
				NtException.CheckAndThrowException(result);
				return new SafeNtThreadHandle(handle);
			}
		}

		public static SafeNtThreadHandle NtOpenThread(AccessMask desiredAccess, ClientID clientID, AllowedObjectAttributes attributes)
		{
			System.IntPtr handle;
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, null, attributes, null, null);
				NtStatus result = Native.NtOpenThread(out handle, desiredAccess, ref oa, ref clientID);
				NtException.CheckAndThrowException(result);
				return new SafeNtThreadHandle(handle);
			}
		}

		public static SafeNtThreadHandle OpenCurrentThread()
		{ return new SafeNtThreadHandle(new System.IntPtr(-1), false); }

		public static void NtAlertThread(SafeNtThreadHandle threadHandle)
		{
			NtStatus result = Native.NtAlertThread(NTInternal.CreateHandleRef(threadHandle));
			NtException.CheckAndThrowException(result);
		}

		public static uint NtAlertResumeThread(SafeNtThreadHandle threadHandle)
		{
			uint suspendCount;
			NtStatus result = Native.NtAlertResumeThread(NTInternal.CreateHandleRef(threadHandle), out suspendCount);
			NtException.CheckAndThrowException(result);
			return suspendCount;
		}

		public static Context NtGetContextThread(SafeNtThreadHandle threadHandle)
		{
			Context context;
			NtStatus result = Native.NtGetContextThread(NTInternal.CreateHandleRef(threadHandle), out context);
			NtException.CheckAndThrowException(result);
			return context;
		}

		private static T NtQueryInformationThread<T>(SafeNtThreadHandle threadHandle, ThreadInformationClass infoClass)
		{
			using (Runtime.InteropServices.HGlobal pBuffer = new Runtime.InteropServices.HGlobal(Runtime.InteropServices.Marshal.SizeOf(typeof(T))))
			{
				uint returnLength;
				NtStatus result;
				result = Native.NtQueryInformationThread(NTInternal.CreateHandleRef(threadHandle), infoClass, pBuffer.Address, pBuffer.AllocatedSize32, out returnLength);
				NtException.CheckAndThrowException(result);
				return (T)Runtime.InteropServices.Marshal.PtrToStructure(pBuffer.Address, typeof(T));
			}
		}

		public static ThreadBasicInformation NtQueryInformationThreadBasic(SafeNtThreadHandle threadHandle)
		{
			return NtQueryInformationThread<ThreadBasicInformation>(threadHandle, ThreadInformationClass.ThreadBasicInformation);
		}

		public static void NtQueueApcThread(SafeNtThreadHandle threadHandle, Events.IoApcRoutine apcRoutine, System.IntPtr context, ref IoStatusBlock unknown1, uint unknown2)
		{
			NtStatus result = Native.NtQueueApcThread(NTInternal.CreateHandleRef(threadHandle), apcRoutine, context, ref unknown1, unknown2);
			NtException.CheckAndThrowException(result);
		}

		public static uint NtResumeThread(SafeNtThreadHandle threadHandle)
		{
			uint suspendCount;
			NtStatus result = Native.NtResumeThread(NTInternal.CreateHandleRef(threadHandle), out suspendCount);
			NtException.CheckAndThrowException(result);
			return suspendCount;
		}

		public static uint NtSuspendThread(SafeNtThreadHandle threadHandle)
		{
			uint previousSuspendCount;
			NtStatus result = Native.NtSuspendThread(NTInternal.CreateHandleRef(threadHandle), out previousSuspendCount);
			NtException.CheckAndThrowException(result);
			return previousSuspendCount;
		}

		public static void NtTerminateThread(SafeNtThreadHandle threadHandle, NtStatus status)
		{
			NtStatus result = Native.NtTerminateThread(NTInternal.CreateHandleRef(threadHandle), status);
			NtException.CheckAndThrowException(result);
		}

		public static void NtTestAlert()
		{
			NtStatus result = Native.NtTestAlert();
			NtException.CheckAndThrowException(result);
		}

		public static void NtYieldExecution()
		{
			NtStatus result = Native.NtYieldExecution();
			NtException.CheckAndThrowException(result);
		}

		internal static void NtContinue(Context context, bool raiseAlert)
		{
			NtStatus result = Native.NtContinue(ref context, raiseAlert);
			NtException.CheckAndThrowException(result);
		}
	}
}