using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Processes.PrivateImplementationDetails
{
	internal static class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateProcess([Out] out System.IntPtr ProcessHandle, [In] ProcessAccessMask DesiredAccess, [In, Optional] ref ObjectAttributes ObjectAttributes, [In] HandleRef ParentProcess, [In] bool InheritObjectTable, [In, Optional] HandleRef SectionHandle, [In, Optional] System.IntPtr DebugPort, [In, Optional] System.IntPtr ExceptionPort);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateProcess([Out] out System.IntPtr ProcessHandle, [In] ProcessAccessMask DesiredAccess, [In, Optional] System.IntPtr ObjectAttributes, [In] HandleRef ParentProcess, [In] bool InheritObjectTable, [In, Optional] HandleRef SectionHandle, [In, Optional] System.IntPtr DebugPort, [In, Optional] System.IntPtr ExceptionPort);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateProcessEx([Out] out System.IntPtr ProcessHandle, [In] ProcessAccessMask DesiredAccess, [In, Optional] ref ObjectAttributes ObjectAttributes, [In] System.IntPtr ParentProcess, [In] bool InheritObjectTable, [In] ProcessCreateFlags CreateFlags, [In, Optional] System.IntPtr SectionHandle, [In, Optional] System.IntPtr DebugPort, [In, Optional] System.IntPtr ExceptionPort, uint JobMemberLevel);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtOpenProcess([Out] out System.IntPtr ProcessHandle, [In] ProcessAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes, [In, Optional] ClientID* ClientId);


		/// <param name="HandleAttributes">Attributes for the access token handle. Only <see cref="AllowedObjectAttributes.KernelHandle"/> is currently supported. If the caller is not running in the system process context, it must specify <see cref="AllowedObjectAttributes.KernelHandle"/> for this parameter.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenProcessTokenEx([In] HandleRef ProcessHandle, [In] Security.TokenAccessMask DesiredAccess, [In] AllowedObjectAttributes HandleAttributes, [Out] out System.IntPtr TokenHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenProcessToken([In] HandleRef ProcessHandle, [In] Security.TokenAccessMask DesiredAccess, [Out] out System.IntPtr TokenHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryInformationProcess([In] HandleRef ProcessHandle, ProcessInfoClass ProcessInformationClass, [In] System.IntPtr ProcessInformation, uint ProcessInformationLength, [Out] out uint ReturnLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtTerminateProcess([In] HandleRef ProcessHandle, [In] NtStatus ExitStatus);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern int RtlCreateProcessParameters(out IntPtr /* ProcessParameters* */ pp, IntPtr /* UnicodeString* */ ImageFile, IntPtr /* UnicodeString* */ DllPath, IntPtr /* UnicodeString* */ CurrentDirectory, IntPtr /* UnicodeString* */ CommandLine, uint CreationFlag, IntPtr /* UnicodeString* */ WindowTitle, IntPtr /* UnicodeString* */ Desktop, IntPtr /* UnicodeString* */ Reserved, IntPtr /* UnicodeString* */ Reserved2);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern void RtlDestroyProcessParameters(IntPtr /* ProcessParameters* */ pp);
	}
}