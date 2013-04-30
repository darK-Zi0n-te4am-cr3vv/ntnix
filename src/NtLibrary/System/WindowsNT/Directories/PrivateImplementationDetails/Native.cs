using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Directories.PrivateImplementationDetails
{
	internal static class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenDirectoryObject([Out] out System.IntPtr DirectoryHandle, [In] DirectoryAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateDirectoryObject([Out] out System.IntPtr DirectoryHandle, [In] DirectoryAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryDirectoryObject([In] HandleRef DirectoryObjectHandle, [Out] System.IntPtr pDirObjInformation, [In] uint BufferLength, [In] bool GetNextIndex, [In] bool IgnoreInputIndex, [In, Out] ref uint ObjectIndex, [Out, Optional] out uint DataWritten);
		
	}
}