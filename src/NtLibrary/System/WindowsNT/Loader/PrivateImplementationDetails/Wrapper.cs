using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Loader.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public static SafeNtModuleHandle LdrLoadDll(string pathToFile, LibraryLoadFlags flags, string moduleFileName)
		{
			UnicodeString wModuleName = (UnicodeString)moduleFileName;
			try
			{
				IntPtr handle;
				NtStatus result = Native.LdrLoadDll(pathToFile, flags, ref wModuleName, out handle);
				NtException.CheckAndThrowException(result);
				return new SafeNtModuleHandle(handle);
			}
			finally
			{
				wModuleName.Dispose();
			}
		}
	}
}