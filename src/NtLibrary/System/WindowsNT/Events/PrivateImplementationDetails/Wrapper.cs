using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace System.WindowsNT.Events.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public static void NtClearEvent(SafeNtEventHandle eventHandle)
		{
			NtStatus result = Native.NtClearEvent(NTInternal.CreateHandleRef(eventHandle));
			NtException.CheckAndThrowException(result);
		}

		public static SafeNtEventHandle NtCreateEvent(EventAccessMask desiredAccess, string eventName, EventType eventType)
		{
			System.IntPtr handle;
			UnicodeString wName = default(UnicodeString);
			unsafe
			{
				UnicodeString* pWName = null;
				if (eventName != null)
				{
					wName = (UnicodeString)eventName;
					pWName = &wName;
				}
				try
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, pWName, AllowedObjectAttributes.None, null, null);
					NtStatus result = Native.NtCreateEvent(out handle, desiredAccess, ref oa, eventType, false);
					NtException.CheckAndThrowException(result);
					return new SafeNtEventHandle(handle);
				}
				finally
				{
					if (eventName != null)
					{
						wName.Dispose();
					}
				}
			}
		}

		public static SafeNtEventHandle NtOpenEvent(EventAccessMask desiredAccess, string eventName)
		{
			System.IntPtr handle;
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, null, AllowedObjectAttributes.None, null, null);
				NtStatus result = Native.NtOpenEvent(out handle, desiredAccess, ref oa);
				NtException.CheckAndThrowException(result);
				return new SafeNtEventHandle(handle);
			}
		}

		public static int NtPulseEvent(SafeNtEventHandle eventHandle)
		{
			int previousState;
			NtStatus result = Native.NtPulseEvent(NTInternal.CreateHandleRef(eventHandle), out previousState);
			NtException.CheckAndThrowException(result);
			return previousState;
		}

		public static int NtResetEvent(SafeNtEventHandle eventHandle)
		{
			int previousState;
			NtStatus result = Native.NtResetEvent(NTInternal.CreateHandleRef(eventHandle), out previousState);
			NtException.CheckAndThrowException(result);
			return previousState;
		}

		public static int NtSetEvent(SafeNtEventHandle eventHandle)
		{
			int previousState;
			NtStatus result = Native.NtSetEvent(NTInternal.CreateHandleRef(eventHandle), out previousState);
			NtException.CheckAndThrowException(result);
			return previousState;
		}

		public static EventBasicInformation NtQueryEventBasic(SafeNtEventHandle eventHandle)
		{
			EventBasicInformation eventInformation = new EventBasicInformation();
			uint resultLength;
			NtStatus result = Native.NtQueryEvent(NTInternal.CreateHandleRef(eventHandle), EventInformationClass.EventBasicInformation, out eventInformation, (uint)Marshal.SizeOf(eventInformation), out resultLength);
			NtException.CheckAndThrowException(result);
			return eventInformation;
		}

		public static void NtWaitForSingleObject(SafeNtEventHandle eventHandle, bool alertable)
		{
			unsafe
			{
				NtStatus result = Native.NtWaitForSingleObject(NTInternal.CreateHandleRef(eventHandle), alertable, null);
				NtException.CheckAndThrowException(result);
			}
		}

		public static void NtWaitForSingleObject(SafeNtEventHandle eventHandle, bool alertable, long timeout)
		{
			unsafe
			{
				NtStatus result = Native.NtWaitForSingleObject(NTInternal.CreateHandleRef(eventHandle), alertable, &timeout);
				NtException.CheckAndThrowException(result);
			}
		}
	}
}