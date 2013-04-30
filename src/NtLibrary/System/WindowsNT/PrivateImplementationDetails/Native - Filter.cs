using System.Runtime.InteropServices;
using System.WindowsNT.Registry;

namespace System.WindowsNT.PrivateImplementationDetails
{
	internal partial class Native
	{
		[DllImport("ntoskrnl.exe")]
		public static extern NtStatus CmRegisterCallback([In, MarshalAs(UnmanagedType.FunctionPtr)] RegistryCallback Function, [In] System.IntPtr Context, [Out] out long Cookie);


		[DllImport("ntoskrnl.exe")]
		public static extern NtStatus CmUnRegisterCallback([In] long Cookie);
	}
}