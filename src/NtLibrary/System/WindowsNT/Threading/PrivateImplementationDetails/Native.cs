using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Threading.PrivateImplementationDetails
{
	internal static class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus CsrClientCallServer([In] IntPtr Message, [In] IntPtr unknown, [In] uint Opcode, [In] uint Size);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtAlertThread([In] HandleRef ThreadHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtAlertResumeThread([In] HandleRef ThreadHandle, [Out, Optional] out uint SuspendCount);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtContinue(ref Context context, bool raiseAlert);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateThread([Out] out IntPtr ThreadHandle, [In] AccessMask DesiredAccess, [In, Optional] ref ObjectAttributes ObjectAttributes, [In] HandleRef ProcessHandle, [Out] out Processes.ClientID ClientId, [In] ref Context ThreadContext, [In] ref Processes.UserStack InitialTeb, [In] bool CreateSuspended);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtGetContextThread([In] HandleRef ThreadHandle, [Out] out Context pContext);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenThread([Out] out IntPtr ThreadHandle, [In] AccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes, [In] ref Processes.ClientID ClientId);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenThreadToken([In] HandleRef ThreadHandle, [In] AccessMask DesiredAccess, [In] bool OpenAsSelf, [Out] out IntPtr TokenHandle);


		/// <param name="OpenAsSelf">
		/// <para><see cref="bool"/> value specifying whether the access check is to be made against the security context of the thread calling ZwOpenThreadTokenEx or against the security context of the process for the calling thread. </para>
		/// <para>If this parameter is <c>false</c>, the access check is performed using the security context for the calling thread. If the thread is impersonating a client, this security context can be that of a client process. If this parameter is <c>true</c>, the access check is made using the security context of the process for the calling thread.</para>
		/// </param>
		/// <param name="HandleAttributes">Attributes for the access token handle. Only <see cref="AllowedObjectAttributes.KernelHandle"/> is currently supported. If the caller is not running in the system process context, it must specify <see cref="AllowedObjectAttributes.KernelHandle"/> for this parameter.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenThreadTokenEx([In] HandleRef ThreadHandle, [In] AccessMask DesiredAccess, [In] bool OpenAsSelf, [In] AllowedObjectAttributes HandleAttributes, [Out] out IntPtr TokenHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryInformationThread([In] HandleRef ThreadHandle, [In] ThreadInformationClass ThreadInformationClass, [Out] IntPtr ThreadInformation, [In] uint ThreadInformationLength, [Out, Optional] out uint ReturnLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueueApcThread([In] HandleRef ThreadHandle, [In] Events.IoApcRoutine ApcRoutine, [In, Optional] IntPtr ApcRoutineContext, [In, Optional] ref IoStatusBlock ApcStatusBlock, [In, Optional] uint ApcReserved);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtResumeThread([In] HandleRef ThreadHandle, [Out, Optional] out uint SuspendCount);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSuspendThread([In] HandleRef ThreadHandle, [Out, Optional] out uint PreviousSuspendCount);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtTerminateThread([In] HandleRef ThreadHandle, [In] NtStatus ExitStatus);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtTestAlert();


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtYieldExecution();
	}
}