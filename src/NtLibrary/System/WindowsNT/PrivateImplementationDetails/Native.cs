using System.WindowsNT.IO;
using System.Runtime.InteropServices;

namespace System.WindowsNT.PrivateImplementationDetails
{
	internal static partial class Native
	{
		//===================================== All ============================================

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtClose([In] IntPtr handle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtDuplicateObject([In] HandleRef SourceProcessHandle, [In] ref System.IntPtr SourceHandle, [In] HandleRef TargetProcessHandle, [Out] out System.IntPtr TargetHandle, [In, Optional] AccessMask DesiredAccess, [In] bool InheritHandle, [In] DuplicationOptions Options);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryObject([In] HandleRef ObjectHandle, [In, Optional] ObjectInformationClass ObjectInformationClass, [Out] System.IntPtr ObjectInformation, [In] uint Length, [Out] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQuerySymbolicLinkObject([In] HandleRef LinkHandle, [In, Out] System.IntPtr LinkTarget, [Out, Optional] out uint ReturnedLength);


		/// <param name="Timeout">An optional pointer to a time-out value that specifies the absolute or relative time at which the wait is to be completed. A negative value specifies an interval relative to the current time. The value should be expressed in units of 100 nanoseconds. Absolute expiration times track any changes in the system time. Relative expiration times are not affected by system time changes. </param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtWaitForSingleObject([In] HandleRef Handle, [In] bool Alertable, [In, Optional] long* Timeout);


		//====================================== System ========================================
		
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQuerySystemInformation(SystemInformationClass SystemInformationClass, System.IntPtr SystemInformation, uint SystemInformationLength, out uint ReturnLength);

		//==================================== Non-NT APIs =====================================

		[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, System.Text.StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern void RtlMapGenericMask(ref AccessMask AccessMask, ref GenericMapping GenericMapping);

	}
}