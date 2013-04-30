using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
using System.WindowsNT.Events;
namespace System.WindowsNT.Devices.PrivateImplementationDetails
{
	internal static class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false, ExactSpelling = true, PreserveSig = true)]
		public static extern NtStatus NtDeviceIoControlFile([In] HandleRef FileHandle, [In] HandleRef Event, [In] IoApcRoutine ApcRoutine, [In] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] uint IoControlCode, [In] System.IntPtr InputBuffer, [In] uint InputBufferLength, [Out] System.IntPtr OutputBuffer, [In] uint OutputBufferLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false, ExactSpelling = true, PreserveSig = true)]
		public static extern NtStatus NtFsControlFile([In] HandleRef FileHandle, [In, Optional] HandleRef Event, [In, Optional] IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] FSCTL FsControlCode, [In, Optional] System.IntPtr InputBuffer, [In] uint InputBufferLength, [Out, Optional] System.IntPtr OutputBuffer, [In] uint OutputBufferLength);
	}
}