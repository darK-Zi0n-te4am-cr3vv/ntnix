using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Loader.PrivateImplementationDetails
{
	internal static class Native
	{
		/// <param name="ModuleHandle">Address of MZ header in virtual memory of caller's process.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LdrLoadDll([In, Optional, MarshalAs(UnmanagedType.LPWStr)] string PathToFile, [In, Optional] LibraryLoadFlags Flags, [In] ref UnicodeString ModuleFileName, [Out] out IntPtr ModuleHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LdrUnloadDll([In] HandleRef ModuleHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LdrGetProcedureAddress([In] HandleRef ModuleHandle, [In, Optional] ref ANSIString FunctionName, [In, Optional] ushort Oridinal, [Out] out IntPtr FunctionAddress);
	}
}