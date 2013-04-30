using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;

namespace System.WindowsNT.Security.PrivateImplementationDetails
{
	internal static partial class Wrapper
	{
		public static SafeLSAPolicyHandle LsaOpenPolicy(AccessMask accessMask)
		{
			LSAObjectAttributes oa = new LSAObjectAttributes(0);
			System.IntPtr handle;
			NtStatus result = Native.LsaOpenPolicy(System.IntPtr.Zero, ref oa, accessMask, out handle);
			NtException.CheckAndThrowException(result);
			return new SafeLSAPolicyHandle(handle);
		}

		public static SafeLSAPolicyHandle LsaOpenPolicy(string systemName, AccessMask accessMask)
		{
			LSAUnicodeString wSystemName = (LSAUnicodeString)systemName;
			try
			{
				LSAObjectAttributes oa = new LSAObjectAttributes(0);
				System.IntPtr handle;
				NtStatus result = Native.LsaOpenPolicy(ref wSystemName, ref oa, accessMask, out handle);
				NtException.CheckAndThrowException(result);
				return new SafeLSAPolicyHandle(handle);
			}
			finally
			{
				wSystemName.Dispose();
			}
		}

		public static string LsaRetrievePrivateData(SafeLSAPolicyHandle policyHandle, string keyName)
		{
			LSAUnicodeString wKeyName = (LSAUnicodeString)keyName;
			try
			{
				unsafe
				{
					System.IntPtr pData;
					NtStatus result = Native.LsaRetrievePrivateData(policyHandle.DangerousGetHandle(), ref wKeyName, out pData);
					NtException.CheckAndThrowException(result);
					try
					{
						return ((LSAUnicodeString*)pData)->ToString();
					}
					finally
					{
						LSAUnicodeString data = *(LSAUnicodeString*)pData;
						Native.LsaFreeUnicodeString(ref data);
					}
				}
			}
			finally
			{
				wKeyName.Dispose();
			}
		}
	}
}