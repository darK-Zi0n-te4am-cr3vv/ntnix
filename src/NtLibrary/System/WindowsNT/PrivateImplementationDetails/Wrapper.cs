using Microsoft.WinNT.SafeHandles;
using Marshal = System.Runtime.InteropServices.Marshal;
using System.WindowsNT.Errors;

namespace System.WindowsNT.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public const uint ExtraPadding = 4;

		public const uint SuggestedBufferSize = 8;

		public const System.StringComparison DefaultStringComparison = StringComparison.CurrentCultureIgnoreCase;

		public static System.IntPtr NtDuplicateObject(SafeNtProcessHandle sourceProcessHandle, SafeNtHandle objectHandle, SafeNtProcessHandle targetProcessHandle, AccessMask accessMask, bool inheritable, DuplicationOptions options)
		{
			System.IntPtr objHandle = objectHandle.DangerousGetHandle();
			System.IntPtr handle;
			NtStatus result = Native.NtDuplicateObject(NTInternal.CreateHandleRef(sourceProcessHandle), ref objHandle, NTInternal.CreateHandleRef(targetProcessHandle), out handle, accessMask, inheritable, options);
			System.GC.KeepAlive(objectHandle);
			NtException.CheckAndThrowException(result);
			return handle;
		}

		private static T NtQueryObject<T>(SafeNtHandle handle, ObjectInformationClass objectInformationClass)
			where T : struct
		{
			uint inputLength = (uint)Marshal.SizeOf(typeof(T));
			System.IntPtr pInfo = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				uint resultLength;
				NtStatus result = Native.NtQueryObject(NTInternal.CreateHandleRef(handle), objectInformationClass, pInfo, inputLength, out resultLength);
				while (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL | result == NtStatus.INFO_LENGTH_MISMATCH)
				{
					inputLength = (inputLength << 1) + 1;
					pInfo = Marshal.ReAllocHGlobal(pInfo, new System.IntPtr(inputLength));
					result = Native.NtQueryObject(NTInternal.CreateHandleRef(handle), objectInformationClass, pInfo, inputLength, out resultLength);
				}
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL | result == NtStatus.INFO_LENGTH_MISMATCH)
				{ System.Diagnostics.Debugger.Break(); }
				Errors.NtException.CheckAndThrowException(result);
				return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(pInfo, typeof(T));
			}
			finally
			{
				Marshal.FreeHGlobal(pInfo);
			}
		}

		public static string NtQueryObjectName(SafeNtHandle handle)
		{
			return NtQueryObject<ObjectNameInformation>(handle, ObjectInformationClass.ObjectNameInformation).ObjectName.ToString();
		}

		public static string NtQuerySymbolicLinkObject(SafeNtHandle handle)
		{
			unsafe
			{
				UnicodeString* pTarget = (UnicodeString*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(UnicodeString)));
				pTarget->String = (char*)&pTarget->String;
				try
				{
					uint returnedLength;
					NtStatus result = Native.NtQuerySymbolicLinkObject(NTInternal.CreateHandleRef(handle), new IntPtr(pTarget), out returnedLength);
					pTarget = (UnicodeString*)Marshal.ReAllocHGlobal(new System.IntPtr(pTarget), new IntPtr(returnedLength));
					result = Native.NtQuerySymbolicLinkObject(NTInternal.CreateHandleRef(handle), new System.IntPtr(pTarget), out returnedLength);
					NtException.CheckAndThrowException(result);
					return Marshal.PtrToStringUni(new System.IntPtr(pTarget->String), pTarget->ByteLength / sizeof(char));
				}
				finally
				{
					Marshal.FreeHGlobal(new System.IntPtr(pTarget));
				}
			}
		}

		public static ObjectBasicInformation NtQueryObjectBasic(SafeNtHandle handle)
		{
			return NtQueryObject<ObjectBasicInformation>(handle, ObjectInformationClass.ObjectBasicInformation);
		}

		public static ObjectDataInformation NtQueryObjectData(SafeNtHandle handle)
		{
			return NtQueryObject<ObjectDataInformation>(handle, ObjectInformationClass.ObjectDataInformation);
		}

		public static ObjectTypeInformation NtQueryObjectType(SafeNtHandle handle)
		{
			return NtQueryObject<ObjectTypeInformation>(handle, ObjectInformationClass.ObjectTypeInformation);
		}

		public static ObjectTypeInformation[] NtQueryObjectAll(SafeNtHandle handle)
		{
			uint inputLength = (uint)Marshal.SizeOf(typeof(ObjectAllInformation));
			System.IntPtr pInfo = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				uint resultLength;
				NtStatus result = Native.NtQueryObject(NTInternal.CreateHandleRef(handle), ObjectInformationClass.ObjectAllInformation, pInfo, inputLength, out resultLength);
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL | result == NtStatus.INFO_LENGTH_MISMATCH)
				{
					inputLength = resultLength;
					pInfo = Marshal.ReAllocHGlobal(pInfo, new System.IntPtr(inputLength));
					result = Native.NtQueryObject(NTInternal.CreateHandleRef(handle), ObjectInformationClass.ObjectAllInformation, pInfo, inputLength, out resultLength);
				}
				Errors.NtException.CheckAndThrowException(result);
				return ObjectAllInformation.FromPtr(pInfo); ;
			}
			finally
			{
				Marshal.FreeHGlobal(pInfo);
			}
		}

		public static void NtWaitForSingleObject(SafeNtHandle handle, bool alertable)
		{ unsafe { NtWaitForSingleObject(handle, alertable, null); } }

		public static bool NtWaitForSingleObject(SafeNtHandle handle, bool alertable, long timeout)
		{ unsafe { return NtWaitForSingleObject(handle, alertable, &timeout); } }

		private static unsafe bool NtWaitForSingleObject(SafeNtHandle handle, bool alertable, long* timeout)
		{
			NtStatus result = Native.NtWaitForSingleObject(NTInternal.CreateHandleRef(handle), alertable, timeout);
			NtException.CheckAndThrowException(result);
			return result != NtStatus.TIMEOUT;
		}

		// RTL

		public static AccessMask RtlMapGenericMask(GenericMapping mapping, AccessMask accessMask)
		{
			Native.RtlMapGenericMask(ref accessMask, ref mapping);
			return accessMask;
		}
	}
}