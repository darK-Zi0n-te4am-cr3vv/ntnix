using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.PrivateImplementationDetails;
using System.WindowsNT.Errors;
using System.Runtime.InteropServices;
namespace System.WindowsNT.Directories.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public static SafeNtDirectoryHandle NtCreateDirectoryObject(string name, DirectoryAccessMask access, AllowedObjectAttributes attributes)
		{
			UnicodeString wName = (UnicodeString)name;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtCreateDirectoryObject(out handle, access, ref oa);
					NtException.CheckAndThrowException(result);
					return new SafeNtDirectoryHandle(handle);
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		public static SafeNtDirectoryHandle NtOpenDirectoryObject(string name, DirectoryAccessMask access, AllowedObjectAttributes attributes)
		{
			UnicodeString wName = (UnicodeString)name;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtOpenDirectoryObject(out handle, access, ref oa);
					NtException.CheckAndThrowException(result);
					return new SafeNtDirectoryHandle(handle);
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		public static void NtEnumerateDirectoryObject(SafeNtDirectoryHandle directoryHandle, System.Predicate<ObjectDirectoryInformation> action)
		{
			unsafe
			{
				using (HGlobal pInfo = new HGlobal(Marshal.SizeOf(typeof(ObjectDirectoryInformation)) + 0x1000))
				{
					bool getNextIndex = true, ignoreInputIndex = false;
					uint index = 0;
					uint outputSize;
					bool @continue = true;
					NtStatus result;
					do
					{
						result = Native.NtQueryDirectoryObject(NTInternal.CreateHandleRef(directoryHandle), pInfo.Address, pInfo.AllocatedSize32, getNextIndex, ignoreInputIndex, ref index, out outputSize);
						if (result == NtStatus.NO_MORE_ENTRIES)
						{
							break;
						}
						NtException.CheckAndThrowException(result);
						ObjectDirectoryInformation info = ObjectDirectoryInformation.FromPtr(pInfo.Address, outputSize);
						@continue = action(info);
					} while (@continue);
				}
			}
		}
	}
}