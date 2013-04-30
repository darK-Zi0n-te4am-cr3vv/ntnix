using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.IO.PrivateImplementationDetails
{
	internal static class Native
	{
		///	<param name="IoStatusBlock">
		/// <para>Pointer to an <see cref="IoStatusBlock"/> structure that receives the final completion status and other information about the requested operation.</para>
		/// In particular, the Information member receives one of the following values:
		/// <para><list type="bullet">
		/// <term>FILE_CREATED</term>
		/// <term>FILE_OPENED</term>
		/// <term>FILE_OVERWRITTEN</term>
		/// <term>FILE_SUPERSEDED</term>
		/// <term>FILE_EXISTS</term>
		/// <term>FILE_DOES_NOT_EXIST</term>
		/// </list></para>
		/// </param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtCreateFile([Out] out System.IntPtr FileHandle, [In] FileAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes, [Out] out IoStatusBlock IoStatusBlock, [In, Optional] ulong* AllocationSize, [In] FileAttributes FileAttributes, [In] FileShare ShareAccess, [In] FileCreationDisposition CreateDisposition, [In] FileCreateOptions CreateOptions, [In, Optional] System.IntPtr EaBuffer, [In] uint EaLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtDeleteFile([In] ref ObjectAttributes ObjectAttributes);


		/// <param name="FileHandle">Handle returned by <see cref="NtCreateFile"/> or <see cref="NtOpenFile"/> for the file whose buffers will be flushed. This parameter is required and cannot be null. </param>
		/// <param name="IoStatusBlock">Address of the caller's I/O status block. This parameter is required and cannot be null.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtFlushBuffersFile([In] HandleRef FileHandle, [In] ref IoStatusBlock IoStatusBlock);


		///<param name="ExclusiveLock">If set, all read and write operation are denied for other processes. If not, only write operation is denied.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtLockFile([In] HandleRef FileHandle, [In, Optional] HandleRef LockGrantedEvent, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] ref long ByteOffset, [In] ref long Length, [Out] out uint Key, [In] bool ReturnImmediately, [In] bool ExclusiveLock);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtNotifyChangeDirectoryFile([In] HandleRef FileHandle, [In, Optional] HandleRef Event, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr Buffer, [In] uint BufferSize, [In] FileNotifyFilter CompletionFilter, [In] bool WatchTree);


		/// <summary>Opens an existing file, directory, device, or volume.</summary>
		/// <param name="FileHandle">Pointer to a HANDLE variable that receives a handle to the file.</param>
		/// <param name="DesiredAccess">Specifies an <see cref="AccessMask"/> value that determines the requested access to the object. For more information, see the <paramref name="DesiredAccess"/> parameter of <see cref="NtCreateFile"/>.</param>
		/// <param name="ObjectAttributes">Pointer to an <see cref="ObjectAttributes"/> structure that specifies the object name and other attributes. Use InitializeObjectAttributes to initialize this structure. If the caller is not running in a system thread context, it must set the <see cref="AllowedObjectAttributes.KernelHandle"/> attribute when it calls InitializeObjectAttributes. </param>
		/// <param name="IoStatusBlock">Pointer to an <see cref="IoStatusBlock"/> structure that receives the final completion status and information about the requested operation.</param>
		/// <param name="ShareAccess">Specifies the type of share access for the file. For more information, see the ShareAccess parameter of <see cref="NtCreateFile"/>.</param>
		/// <param name="OpenOptions">Specifies the options to apply when opening the file. For more information, see the CreateOptions parameter of <see cref="NtCreateFile"/>.</param>
		/// <returns>Returns <see cref="NtStatus.SUCCESS"/> or the appropriate <see cref="NtStatus"/> error code. In the latter case, the caller can find more information about the cause of the failure by checking the <paramref name="IoStatusBlock"/> parameter.</returns>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenFile([Out] out System.IntPtr FileHandle, [In] FileAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes, [Out] out IoStatusBlock IoStatusBlock, [In] FileShare ShareAccess, [In] FileCreateOptions OpenOptions);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryAttributesFile([In] ref ObjectAttributes ObjectAttributes, [Out] out FileBasicInformation FileAttributes);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryFullAttributesFile([In] ref ObjectAttributes ObjectAttributes, [Out] out FileNetworkOpenInformation FileAttributes);


		/// <summary>Returns various kinds of information about files in the directory specified by a given file handle.</summary>
		/// <param name="FileHandle">Handle returned by <see cref="NtCreateFile"/> or <see cref="NtOpenFile"/> for the file object that represents the directory for which information is being requested. The file object must have been opened for asynchronous I/O if the caller specifies a non-<see cref="System.IntPtr.Zero"/> value for <paramref name="Event"/> or <paramref name="ApcRoutine"/>. </param>
		/// <param name="Event">Handle for a caller-created event. If this parameter is supplied, the caller will be put into a wait state until the requested operation is completed and the given event is set to the Signaled state. This parameter is optional and can be <see cref="System.IntPtr.Zero"/>. It must be <see cref="System.IntPtr.Zero"/> if the caller will wait for the <paramref name="FileHandle"/> to be set to the Signaled state.</param>
		/// <param name="ApcRoutine">Address of an optional, caller-supplied APC routine to be called when the requested operation completes. This parameter is optional and can be <c>null</c>. If there is an I/O completion object associated with the file object, this parameter must be <see cref="System.IntPtr.Zero"/>.</param>
		/// <param name="ApcContext">
		/// <para>Pointer to a caller-determined context area if the caller supplies an APC or if an I/O completion object is associated with the file object. When the operation completes, this context is passed to the APC, if one was specified, or is included as part of the completion message that the I/O Manager posts to the associated I/O completion object.</para>
		/// <para>This parameter is optional and can be <see cref="System.IntPtr.Zero"/>. If ApcRoutine is <see cref="System.IntPtr.Zero"/> and there is no I/O completion object associated with the file object, this parameter must also be <see cref="System.IntPtr.Zero"/>.</para>
		/// </param>
		/// <param name="Length">Size, in bytes, of the buffer pointed to by FileInformation. The caller should set this parameter according to the given <see cref="FileInformationClass"/>.</param>
		/// <param name="ReturnSingleEntry">Set to <c>true</c> if only a single entry should be returned, <c>false</c> otherwise. If this parameter is <c>true</c>, <see cref="NtQueryDirectoryFile"/> returns only the first entry that is found. </param>
		/// <param name="FileName">
		/// <para>Pointer to a caller-allocated Unicode string containing the name of a file (or multiple files, if wildcards are used) within the directory specified by FileHandle. This parameter is optional and can be <c>null</c>.</para>
		/// <para>If <paramref name="FileName"/> is not <c>null</c>, only files whose names match the <paramref name="FileName"/> string are included in the directory scan. If FileName is <c>null</c>, all files are included. If <paramref name="RestartScan"/> is <c>false</c>, the value of <paramref name="FileName"/> is ignored.</para>
		/// </param>
		/// <param name="RestartScan">Set to <c>true</c> if the scan is to start at the first entry in the directory. Set to <c>false</c> if resuming the scan from a previous call. The caller must set this parameter to <c>true</c> when calling <see cref="NtQueryDirectoryFile"/> for the first time. </param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryDirectoryFile([In] HandleRef FileHandle, [In, Optional] HandleRef Event, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass, [In] bool ReturnSingleEntry, [In, Optional] System.IntPtr FileName, [In] bool RestartScan);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryEaFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr Buffer, [In] uint Length, [In] bool ReturnSingleEntry, [In, Optional] System.IntPtr EaLis, [In] uint EaListLength, [In, Optional] uint* EaIndex, [In] bool RestartScan);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryEaFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr Buffer, [In] uint Length, [In] bool ReturnSingleEntry, [In, Optional] System.IntPtr EaLis, [In] uint EaListLength, [In, Optional] ref uint EaIndex, [In] bool RestartScan);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryEaFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr Buffer, [In] uint Length, [In] bool ReturnSingleEntry, [In, Optional] System.IntPtr EaLis, [In] uint EaListLength, [In, Optional] System.IntPtr EaIndex, [In] bool RestartScan);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryVolumeInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr FsInformation, [In] uint Length, [In] FSInformationClass FsInformationClass);


		///<param name="EaBufferSize">Size of <paramref name="EaBuffer"/>, in bytes.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetEaFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] FileFullEaInformation[] EaBuffer, [In] uint EaBufferSize);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] System.IntPtr FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileAlignmentInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileAttributeTagInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileBasicInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileDispositionInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileEndOfFileInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileFullEaInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileNameInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileNetworkOpenInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FilePositionInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileRenameInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileStandardInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileValidDataLengthInformation FileInformation, [In] uint Length, [In] FileInformationClass FileInformationClass);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetVolumeInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileFsControlInformation FsInformation, [In] uint Length, [In] FSInformationClass FsInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetVolumeInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileFsLabelInformation FsInformation, [In] uint Length, [In] FSInformationClass FsInformationClass);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetVolumeInformationFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref FileFsObjectIdInformation FsInformation, [In] uint Length, [In] FSInformationClass FsInformationClass);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtReadFile([In] HandleRef FileHandle, [In, Optional] HandleRef Event, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [Out] System.IntPtr Buffer, [In] uint Length, long* ByteOffset, uint* Key);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtUnlockFile([In] HandleRef FileHandle, [Out] out IoStatusBlock IoStatusBlock, [In] ref long ByteOffset, [In] ref long Length, [In] ref uint Key);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtWriteFile([In] HandleRef FileHandle, [In, Optional] HandleRef Event, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] System.IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] byte[] Buffer, [In] uint Length, [In, Optional] long* ByteOffset, [In, Optional] uint* Key);


		// RTL ===============

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern bool RtlDosPathNameToNtPathName_U([In, MarshalAs(UnmanagedType.LPWStr)] string /*PCWSTR*/ DosPathName, [Out] out UnicodeString NtPathName, [Out, MarshalAs(UnmanagedType.LPWStr)] out string /* PCWSTR* */ NtFileNamePart,[Out] out IntPtr /* CURDIR* */ DirectoryInfo);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern bool RtlIsDosDeviceName_U([In, MarshalAs(UnmanagedType.LPWStr)] string /*PCWSTR*/ DeviceName);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern uint RtlDetermineDosPathNameType_U([In, MarshalAs(UnmanagedType.LPWStr)] string /*PCWSTR*/ DeviceName);
	}
}
