using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Errors.PrivateImplementationDetails
{
	internal static class Native
	{

		///<param name="Response">Array of <see cref="int"/> parameters for use in error message string. </param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtRaiseHardError([In] NtStatus ErrorStatus, [In] uint NumberOfParameters, [In, Optional] System.IntPtr UnicodeStringParameterMask, [In] /* Was originally: PVOID* */ System.IntPtr Parameters, [In] HardErrorResponseOption ResponseOption, [Out] out HardErrorResponse Response);

		//RTL

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern int RtlNtStatusToDosError([In] NtStatus Status);
	}
}