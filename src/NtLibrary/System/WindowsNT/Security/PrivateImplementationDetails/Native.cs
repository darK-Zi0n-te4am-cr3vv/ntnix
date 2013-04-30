using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Security.PrivateImplementationDetails
{
	internal static partial class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtAdjustPrivilegesToken([In] HandleRef TokenHandle, [In] bool DisableAllPrivileges, [In, Optional] ref TokenPrivileges NewState, [In, Optional] uint BufferLength, [Out, Optional] out TokenPrivileges PreviousState, [Out] out uint ReturnLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtPrivilegeCheck([In] HandleRef TokenHandle, [In] ref PrivilegeSet RequiredPrivileges, [Out] out bool Result);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQuerySecurityObject([In] HandleRef ObjectHandle, [In] SecurityInformation SecurityInformationClass, [Out] System.IntPtr DescriptorBuffer, [In] uint DescriptorBufferLength, [Out] out uint RequiredLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetSecurityObject([In] HandleRef ObjectHandle, [In] SecurityInformation SecurityInformationClass, [In] byte[] DescriptorBuffer);
	}
}