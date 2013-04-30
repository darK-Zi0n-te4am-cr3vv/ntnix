using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Registry.PrivateImplementationDetails
{
	internal static partial class Wrapper
	{
		public static long CmRegistryCallback(RegistryCallback callback, System.IntPtr pContext)
		{
			long cookie;
			NtStatus result = WindowsNT.PrivateImplementationDetails.Native.CmRegisterCallback(callback, pContext, out cookie);
			NtException.CheckAndThrowException(result);
			return cookie;
		}

		public static void CmUnRegistryCallback(long cookie)
		{
			NtStatus result = WindowsNT.PrivateImplementationDetails.Native.CmUnRegisterCallback(cookie);
			NtException.CheckAndThrowException(result);
		}
	}
}
