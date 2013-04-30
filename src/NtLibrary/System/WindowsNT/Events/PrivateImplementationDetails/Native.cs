using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Events.PrivateImplementationDetails
{
	internal static class Native
	{

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateEvent([Out] out System.IntPtr EventHandle, [In] EventAccessMask DesiredAccess, [In, Optional] ref ObjectAttributes ObjectAttributes, [In] EventType EventType, [In] bool InitialState);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtClearEvent([In] HandleRef EventHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenEvent([Out] out System.IntPtr EventHandle, [In] EventAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryEvent([In] HandleRef EventHandle, [In] EventInformationClass EventInformationClass, [Out] System.IntPtr EventInformation, [In] uint EventInformationLength, [Out, Optional] out uint ReturnLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryEvent([In] HandleRef EventHandle, [In] EventInformationClass EventInformationClass, [Out] out EventBasicInformation EventInformation, [In] uint EventInformationLength, [Out, Optional] out uint ReturnLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueueApcThread([In] HandleRef ThreadHandle, [In, MarshalAs(UnmanagedType.FunctionPtr)] IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcRoutineContext, [In, Optional] IoStatusBlock ApcStatusBlock, [In, Optional] uint ApcReserved);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtPulseEvent([In] HandleRef EventHandle, [Out, Optional] out int PreviousState);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtResetEvent([In] HandleRef EventHandle, [Out, Optional] out int PreviousState);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetEvent([In] HandleRef EventHandle, [Out, Optional] out int PreviousState);


		/// <param name="Timeout">An optional pointer to a time-out value that specifies the absolute or relative time at which the wait is to be completed. A negative value specifies an interval relative to the current time. The value should be expressed in units of 100 nanoseconds. Absolute expiration times track any changes in the system time. Relative expiration times are not affected by system time changes. </param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtWaitForSingleObject([In] HandleRef Handle, [In] bool Alertable, [In] long* Timeout);

	}
}