using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Memory.PrivateImplementationDetails
{
	internal static class Native
	{
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtCreateSection([Out] out System.IntPtr SectionHandle, [In] SectionAccessMask DesiredAccess, [In, Optional] System.IntPtr ObjectAttributes, [In, Optional] long* MaximumSize, [In] PageProtection SectionPageProtection, [In] SectionAllocationAttributes AllocationAttributes, [In, Optional] HandleRef FileHandle);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtCreateSection([Out] out System.IntPtr SectionHandle, [In] SectionAccessMask DesiredAccess, [In, Optional] ref ObjectAttributes ObjectAttributes, [In, Optional] long* MaximumSize, [In] PageProtection SectionPageProtection, [In] SectionAllocationAttributes AllocationAttributes, [In, Optional] HandleRef FileHandle);


		/// <summary>Maps a view of a section into the virtual address space of a subject process.</summary>
		/// <param name="BaseAddress">Pointer to a variable that receives the base address of the view. If the value of this parameter is not NULL, the view is allocated starting at the specified virtual address rounded down to the next 64-kilobyte address boundary.</param>
		/// <param name="ZeroBits">Specifies the number of high-order address bits that must be zero in the base address of the section view. The value of this parameter must be less than 21 and is used only if BaseAddress is NULL—in other words, when the caller allows the system to determine where to allocate the view.</param>
		/// <param name="CommitSize">Specifies the size, in bytes, of the initially committed region of the view. CommitSize is meaningful only for page-file backed sections and is rounded up to the nearest multiple of PAGE_SIZE. (For sections that map files, both the data and the image are committed at section-creation time.)</param>
		/// <param name="SectionOffset">Pointer to a variable that receives the offset, in bytes, from the beginning of the section to the view. If this pointer is not NULL, the offset is rounded down to the next allocation-granularity size boundary.</param>
		/// <param name="ViewSize">
		/// Pointer to a SIZE_T variable. If the initial value of this variable is zero, ZwMapViewOfSection maps a view of the section that starts at SectionOffset and continues to the end of the section. Otherwise, the initial value specifies the view's size, in bytes. ZwMapViewOfSection always rounds this value up to the nearest multiple of PAGE_SIZE before mapping the view.
		/// On return, the value receives the actual size, in bytes, of the view.
		/// </param>
		/// <param name="InheritDisposition">Specifies how the view is to be shared with child processes.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtMapViewOfSection([In] HandleRef SectionHandle, [In] HandleRef ProcessHandle, [In, Out] IntPtr* BaseAddress, [In] IntPtr ZeroBits, [In] IntPtr CommitSize, [In, Out, Optional] long* SectionOffset, [In, Out] IntPtr* ViewSize, [In] SectionInherit InheritDisposition, [In] MemoryAllocationType AllocationType, [In] PageProtection Win32Protect);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtAllocateVirtualMemory([In] HandleRef ProcessHandle, [In, Out] System.IntPtr* BaseAddress, [In] uint ZeroBits, [In, Out] ref System.UIntPtr RegionSize, [In] MemoryAllocationType AllocationType, [In] PageProtection Protect);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtFlushVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [In, Out] ref uint NumberOfBytesToFlush, [Out] out uint NumberOfBytesFlushed);


		/// <param name="RegionSize">If the <see cref="MemoryFreeType.RELEASE"/> flag is set in the <paramref name="FreeType"/> parameter, RegionSize must be zero. The function frees the entire region that was reserved in the initial allocation call to this function.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtFreeVirtualMemory([In] HandleRef ProcessHandle, [In, Out] ref System.IntPtr BaseAddress, [In, Out] uint* RegionSize, [In] MemoryFreeType FreeType);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtProtectVirtualMemory([In] HandleRef ProcessHandle, [In, Out] System.IntPtr* BaseAddress, [In, Out] uint* NumberOfBytesToProtect, [In] PageProtection NewAccessProtection, [Out] out PageProtection OldAccessProtection);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [In] MemoryInformationClass MemoryInformationClass, [Out] IntPtr Buffer, [In] uint Length, [Out, Optional] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [In] MemoryInformationClass MemoryInformationClass, out MemoryBasicInformation Buffer, [In] uint Length, [Out, Optional] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtReadVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [Out] IntPtr Buffer, [In] uint NumberOfBytesToRead, [Out, Optional] out uint NumberOfBytesReaded);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtReadVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [Out] ArrayWithOffset Buffer, [In] uint NumberOfBytesToRead, [Out, Optional] out uint NumberOfBytesReaded);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtWriteVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [In] IntPtr Buffer, [In] uint NumberOfBytesToWrite, [Out, Optional] out uint NumberOfBytesWritten);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtWriteVirtualMemory([In] HandleRef ProcessHandle, [In] IntPtr BaseAddress, [In] ArrayWithOffset Buffer, [In] uint NumberOfBytesToWrite, [Out, Optional] out uint NumberOfBytesWritten);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQuerySection([In] HandleRef SectionHandle, [In] SectionInformationClass InformationClass, [Out] out SectionBasicInformation InformationBuffer, [In] uint InformationBufferSize, [Out, Optional] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQuerySection([In] HandleRef SectionHandle, [In] SectionInformationClass InformationClass, [Out] out SectionImageInformation InformationBuffer, [In] uint InformationBufferSize, [Out, Optional] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenSection([Out] out System.IntPtr SectionHandle, [In] SectionAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes);
	}
}
