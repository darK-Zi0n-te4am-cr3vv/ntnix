using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.WindowsNT.IO
{
	[System.Flags()]
	public enum ErrorsNotToThrow
	{
		ThrowAnyError = 0,
		NotFound = 1 << 1,
		FileTypeMismatch = 1 << 2,
		ReturnNullOnAnyError = ~0
	}

	[System.Flags()]
	public enum FileNotifyFilter : uint
	{
		CHANGE_FILE_NAME = 0x00000001,
		CHANGE_DIR_NAME = 0x00000002,
		CHANGE_NAME = 0x00000003,
		CHANGE_ATTRIBUTES = 0x00000004,
		CHANGE_SIZE = 0x00000008,
		CHANGE_LAST_WRITE = 0x00000010,
		CHANGE_LAST_ACCESS = 0x00000020,
		CHANGE_CREATION = 0x00000040,
		CHANGE_EA = 0x00000080,
		CHANGE_SECURITY = 0x00000100,
		CHANGE_STREAM_NAME = 0x00000200,
		CHANGE_STREAM_SIZE = 0x00000400,
		CHANGE_STREAM_WRITE = 0x00000800,
		VALID_MASK = 0x00000fff
	}

	public enum FileAction : uint
	{
		Added = 0x00000001,
		Removed = 0x00000002,
		Modified = 0x00000003,
		RenamedOldName = 0x00000004,
		RenamedNewName = 0x00000005,
		AddedStream = 0x00000006,
		RemovedStream = 0x00000007,
		ModifiedStream = 0x00000008,
		RemovedByDelete = 0x00000009,
		IDNotTunneled = 0x0000000A,
		TunneledIDCollision = 0x0000000B
	}

	public enum FileOpenInformation
	{
		Superceded = 0x00000000,
		Opened = 0x00000001,
		Created = 0x00000002,
		Overwritten = 0x00000003,
		Exists = 0x00000004,
		DoesNotExist = 0x00000005
	}

	[System.Flags()]
	public enum FileShare : uint
	{
		Exclusive = 0,
		Read = 0x00000001,
		Write = 0x00000002,
		ReadWrite = Read | Write,
		Delete = 0x00000004,
		ReadWriteDelete = Read | Write | Delete
	}

	[System.Flags()]
	public enum FileAccessMask : uint
	{
		AccessSystemSecurity = AccessMask.AccessSystemSecurity,
		AddFile = (0x0002),    // directory
		AddSubdirectory = (0x0004),    // directory
		/// <summary>CAUTION: Do NOT use for directories.</summary>
		AllAccess = (AccessMask.StandardRightsRequired | AccessMask.Synchronize | 0x1FF),
		Any = 0,
		/// <summary>
		/// <para>CAUTION: Do NOT use for directories.</para>
		/// <para>Append data to the file.</para>
		/// </summary>
		AppendData = (0x0004),    // file
		CreatePipeInstance = (0x0004),    // named pipe
		Delete = AccessMask.Delete,
		DeleteChild = (0x0040),    // directory
		/// <summary>Use system paging I/O to read data from the file into memory. This flag is irrelevant for device and intermediate drivers.</summary>
		Execute = (0x0020),    // file
		GenericAll = AccessMask.GenericAll,
		GenericExecute = (AccessMask.StandardRightsExecute | ReadAttributes | Execute | AccessMask.Synchronize),
		GenericRead = (AccessMask.StandardRightsRead | ReadData | ReadAttributes | ReadExtendedAttributes | AccessMask.Synchronize),
		GenericWrite = (AccessMask.StandardRightsWrite | WriteData | WriteAttributes | WriteExtendedAttributes | AppendData | AccessMask.Synchronize),
		/// <summary>CAUTION: Do NOT use for directories.</summary>
		ListDirectory = (0x0001),    // directory
		MaximumAllowed = AccessMask.MaximumAllowed,
		Read = (0x0001),    // file & pipe
		/// <summary>Read the file's attributes.</summary>
		ReadAttributes = (0x0080),    // all
		/// <summary>Read data from the file.</summary>
		ReadData = (0x0001),    // file & pipe
		/// <summary>Read the file's extended attributes (EAs). This flag is irrelevant for device and intermediate drivers.</summary>
		ReadExtendedAttributes = (0x0008),    // file & directory
		Special = (Any),
		Synchronize = AccessMask.Synchronize,
		Traverse = (0x0020),    // directory
		Write = (0x0002),    // file & pipe
		/// <summary>Write the file's attributes.</summary>
		WriteAttributes = (0x0100),    // all
		WriteDAC = AccessMask.WriteDAC,
		/// <summary>
		/// <para>Write data to the file.</para>
		/// <para>CAUTION: Do NOT use for directories.</para>
		/// </summary>
		WriteData = (0x0002),    // file & pipe
		/// <summary>Change the file's extended attributes (EAs). This flag is irrelevant for device and intermediate drivers.</summary>
		WriteExtendedAttributes = (0x0010),    // file & directory
		WriteOwner = AccessMask.WriteOwner,
	}

	internal enum FSInformationClass
	{
		FileFsVolumeInformation = 1,
		FileFsLabelInformation,      // 2
		FileFsSizeInformation,       // 3
		FileFsDeviceInformation,     // 4
		FileFsAttributeInformation,  // 5
		FileFsControlInformation,    // 6
		FileFsFullSizeInformation,   // 7
		FileFsObjectIdInformation,   // 8
		FileFsDriverPathInformation, // 9
		FileFsMaximumInformation
	}

	internal enum FileOffsetOptions : long
	{
		WriteToEndOfFile = -1,
		UserFilePointerPosition = -2
	}

	[System.Flags()]
	public enum FileAttributes : uint
	{
		None = 0x00000000,
		/// <summary>
		/// <para>A file or directory that is an archive file or directory.</para>
		/// <para>Applications use this attribute to mark files for backup or removal.</para>
		/// </summary>
		Archive = 0x00000020,
		/// <summary>
		/// <para>A file or directory that is compressed.</para>
		/// <para>For a file, all of the data in the file is compressed.</para>
		/// <para>For a directory, compression is the default for newly created files and subdirectories.</para>
		/// </summary>
		Compressed = 0x00000800,
		/// <summary>Reserved; do not use.</summary>
		Device = 0x00000040,
		/// <summary>The handle that identifies a directory.</summary>
		Directory = 0x00000010,
		/// <summary>
		/// <para>A file or directory that is encrypted.</para>
		/// <para>For a file, all data streams in the file are encrypted.</para>
		/// <para>For a directory, encryption is the default for newly created files and subdirectories.</para>
		/// </summary>
		Encrypted = 0x00004000,
		/// <summary>The file or directory is hidden. It is not included in an ordinary directory listing.</summary>
		Hidden = 0x00000002,
		/// <summary>
		/// <para>A file or directory that does not have other attributes set. </para>
		/// <para>This attribute is valid only when used alone.</para>
		/// </summary>
		Normal = 0x00000080,
		/// <summary>The file is not to be indexed by the content indexing service.</summary>
		NotContentIndexed = 0x00002000,
		/// <summary>
		/// <para>The data of a file is not available immediately.</para>
		/// <para>This attribute indicates that the file data is physically moved to offline storage. This attribute is used by Remote Storage, which is the hierarchical storage management software. Applications should not arbitrarily change this attribute.</para>
		/// </summary>
		Offline = 0x00001000,
		/// <summary>
		/// <para>A file or directory that is read-only.</para>
		/// <para>For a file, applications can read the file, but cannot write to it or delete it.</para>
		/// <para>For a directory, applications cannot delete it.</para>
		/// </summary>
		ReadOnly = 0x00000001,
		/// <summary>A file or directory that has an associated reparse point, or a file that is a symbolic link.</summary>
		ReparsePoint = 0x00000400,
		/// <summary>A file that is a sparse file.</summary>
		SparseFile = 0x00000200,
		/// <summary>A file or directory that the operating system uses a part of, or uses exclusively.</summary>
		System = 0x00000004,
		/// <summary>
		/// <para>A file that is being used for temporary storage.</para>
		/// <para>File systems avoid writing data back to mass storage if sufficient cache memory is available, because typically, an application deletes a temporary file after the handle is closed. In that scenario, the system can entirely avoid writing the data. Otherwise, the data is written after the handle is closed.</para>
		/// </summary>
		Temporary = 0x00000100,
		ValidFlags = 0x00007fb7,
		ValidSetFlags = 0x000031a7,
		/// <summary>A file is a virtual file.</summary>
		Virtual = 0x00010000
	}

	/// <summary>Specifies the action to perform if the file does or does not exist.</summary>
	public enum FileCreationDisposition : uint
	{
		/// <summary>If file exists, then replace the file. If file does not exist, then create the file.</summary>
		CreateAlways = 0x00000000,
		/// <summary>If file exists, then open the file. If file does not exist, then return an error.</summary>
		OpenOnly = 0x00000001,
		/// <summary>If file exists, then return an error. If file does not exist, then create the file.</summary>
		CreateOnly = 0x00000002,
		/// <summary>If file exists, then open the file. If file does not exist, then create the file.</summary>
		OpenOrCreate = 0x00000003,
		/// <summary>If file exists, then open the file, and overwrite it. If file does not exist, then return an error.</summary>
		OverwriteOnly = 0x00000004,
		/// <summary>If file exists, then open the file, and overwrite it. If file does not exist, then create the file.</summary>
		OverwriteOrCreate = 0x00000005,
	}

	[System.Flags()]
	public enum FileCreateOptions : uint
	{
		None = 0x00000000,
		Default = SynchronousIoNonAlert,
		/// <summary>The file is a directory. Compatible <see cref="FileCreationDisposition"/> flags are <see cref="SynchronousIoAlert"/>, <see cref="SynchronousIoNonAlert"/>, <see cref="WriteThrough"/>, <see cref="OpenForBackupIntent"/>, and <see cref="OpenByFileID"/>. The CreateDisposition parameter must be set to <see cref="FileCreationDisposition.CreateOnly"/>, <see cref="FileCreationDisposition.OpenOnly"/>, or <see cref="FileCreationDisposition.OpenOrCreate"/>.</summary>
		DirectoryFile = 0x00000001,
		/// <summary>System services, file-system drivers, and drivers that write data to the file must actually transfer the data to the file before any requested write operation is considered complete.</summary>
		WriteThrough = 0x00000002,
		/// <summary>All access to the file will be sequential.</summary>
		SequentialOnly = 0x00000004,
		/// <summary>The file cannot be cached or buffered in a driver's internal buffers. This flag is incompatible with the DesiredAccess parameter's <see cref="FileAccessMask.AppendData"/> flag.</summary>
		NoIntermediateBuffering = 0x00000008,
		/// <summary>All operations on the file are performed synchronously. Any wait on behalf of the caller is subject to premature termination from alerts. This flag also causes the I/O system to maintain the file-position pointer. If this flag is set, the <see cref="FileAccessMask.Synchronize"/> flag must be set in the DesiredAccess parameter.</summary>
		SynchronousIoAlert = 0x00000010,
		/// <summary>All operations on the file are performed synchronously. Waits in the system that synchronize I/O queuing and completion are not subject to alerts. This flag also causes the I/O system to maintain the file-position context. If this flag is set, the <see cref="FileAccessMask.Synchronize"/> flag must be set in the DesiredAccess parameter.</summary>
		SynchronousIoNonAlert = 0x00000020,
		/// <summary>The file is not a directory. The file object to open can represent a data file; a logical, virtual, or physical device; or a volume.</summary>
		NonDirectoryFile = 0x00000040,
		/// <summary>Create a tree connection for this file in order to open it over the network. This flag is not used by device and intermediate drivers.</summary>
		CreateTreeConnection = 0x00000080,
		/// <summary>Complete this operation immediately with an alternate success code if the target file is oplocked, rather than blocking the caller's thread. If the file is oplocked, another caller already has access to the file over the network. This flag is not used by device and intermediate drivers.</summary>
		CompleteOfOpLocked = 0x00000100,
		/// <summary>If the extended attributes (EAs) for an existing file being opened indicate that the caller must understand EAs to properly interpret the file, <see cref="PrivateImplementationDetails.Native.NtCreateFile"/> should return an error. This flag is irrelevant for device and intermediate drivers.</summary>
		NoEaKnowledge = 0x00000200,
		OpenForRecovery = 0x00000400,
		/// <summary>Access to the file can be random, so no sequential read-ahead operations should be performed by file-system drivers or by the system.</summary>
		RandomAccess = 0x00000800,
		/// <summary>The system deletes the file when the last handle to the file is passed to <see cref="WindowsNT.PrivateImplementationDetails.Native.NtClose"/>. If this flag is set, the <see cref="FileAccessMask.Delete"/> flag must be set in the DesiredAccess parameter.</summary>
		DeleteOnClose = 0x00001000,
		/// <summary>The file name that is specified by the <see cref="ObjectAttributes"/>parameter includes the 8-byte file reference number for the file. This number is assigned by and specific to the particular file system. If the file is a reparse point, the file name will also include the name of a device. Note that the FAT file system does not support this flag.</summary>
		OpenByFileID = 0x00002000,
		/// <summary>The file is being opened for backup intent. Therefore, the system should check for certain access rights and grant the caller the appropriate access to the file—before checking the DesiredAccess parameter against the file's security descriptor. This flag not used by device and intermediate drivers.</summary>
		OpenForBackupIntent = 0x00004000,
		NoCompression = 0x00008000,
		ReserveOpFilter = 0x00100000,
		/// <summary>Open a file with a reparse point and bypass normal reparse point processing for the file.</summary>
		OpenReparsePoint = 0x00200000,
		OpenNoRecall = 0x00400000,
		OpenForFreeSpaceQuery = 0x00800000,
		CopyStructuredStorage = 0x00000041,
		StructuredStorage = 0x00000441,
		ValidOptionFlags = 0x00ffffff,
		ValidPipeOptionFlags = 0x00000032,
		ValidMailslotOptionFlags = 0x00000032,
		ValidSetFlags = 0x00000036
	}

	public enum FileAlignment : uint
	{
		Byte = 0x00000000,
		Word = 0x00000001,
		Long = 0x00000003,
		Quad = 0x00000007,
		Octa = 0x0000000f,
		Byte32 = 0x0000001f,
		Byte64 = 0x0000003f,
		Byte128 = 0x0000007f,
		Byte256 = 0x000000ff,
		Byte512 = 0x000001ff
	}

	internal enum FileInformationClass
	{
		FileDirectoryInformation = 1,
		FileFullDirectoryInformation,   // 2
		FileBothDirectoryInformation,   // 3
		FileBasicInformation,           // 4  wdm
		FileStandardInformation,        // 5  wdm
		FileInternalInformation,        // 6
		FileEaInformation,              // 7
		FileAccessInformation,          // 8
		FileNameInformation,            // 9
		FileRenameInformation,          // 10
		FileLinkInformation,            // 11
		FileNamesInformation,           // 12
		FileDispositionInformation,     // 13
		FilePositionInformation,        // 14 wdm
		FileFullEaInformation,          // 15
		FileModeInformation,            // 16
		FileAlignmentInformation,       // 17
		FileAllInformation,             // 18
		FileAllocationInformation,      // 19
		FileEndOfFileInformation,       // 20 wdm
		FileAlternateNameInformation,   // 21
		FileStreamInformation,          // 22
		FilePipeInformation,            // 23
		FilePipeLocalInformation,       // 24
		FilePipeRemoteInformation,      // 25
		FileMailslotQueryInformation,   // 26
		FileMailslotSetInformation,     // 27
		FileCompressionInformation,     // 28
		FileObjectIdInformation,        // 29
		FileCompletionInformation,      // 30
		FileMoveClusterInformation,     // 31
		FileQuotaInformation,           // 32
		FileReparsePointInformation,    // 33
		FileNetworkOpenInformation,     // 34
		FileAttributeTagInformation,    // 35
		FileTrackingInformation,        // 36
		FileIdBothDirectoryInformation, // 37
		FileIdFullDirectoryInformation, // 38
		FileValidDataLengthInformation, // 39
		FileShortNameInformation,       // 40
		FileMaximumInformation
		// begin_wdm
	}

	internal enum FileCreateInformation
	{
		Superceded = 0x00000000,
		Opened = 0x00000001,
		Created = 0x00000002,
		Overwritten = 0x00000003,
		Exists = 0x00000004,
		DoesNotExist = 0x00000005
	}

	internal enum FileStorageType
	{
		StorageTypeDefault = 1,
		StorageTypeDirectory,
		StorageTypeFile,
		StorageTypeJunctionPoint,
		StorageTypeCatalog,
		StorageTypeStructuredStorage,
		StorageTypeEmbedding,
		StorageTypeStream
	}

	internal struct FileAlignmentInformation
	{
		public FileAlignment AlignmentRequirement;

		internal static FileAlignmentInformation FromPtr(System.IntPtr ptr)
		{ return (FileAlignmentInformation)Marshal.PtrToStructure(ptr, typeof(FileAlignmentInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FileAttributeTagInformation
	{
		public FileAttributes FileAttributes;
		public uint ReparseTag;

		internal static FileAttributeTagInformation FromPtr(System.IntPtr ptr)
		{ return (FileAttributeTagInformation)Marshal.PtrToStructure(ptr, typeof(FileAttributeTagInformation)); }
	}

	/// <summary>The caller can set one, all, or any other combination of these three members to –1. Only the members that are set to –1 will be unaffected by I/O operations on the file handle; the other members will be updated as appropriate.</summary>
	internal struct FileBasicInformation
	{
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public FileAttributes FileAttributes;

		internal static FileBasicInformation FromPtr(System.IntPtr ptr)
		{ return (FileBasicInformation)Marshal.PtrToStructure(ptr, typeof(FileBasicInformation)); }
	}

	public struct FileBasicAttributeInformation
	{
		internal FileBasicAttributeInformation (FileBasicInformation info)
			: this()
		{
			this.CreationTime = System.DateTime.FromFileTime(info.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(info.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(info.ChangeTime);
			this.FileAttributes = info.FileAttributes;
		}

		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public FileAttributes FileAttributes { get; set; }
	}

	internal struct FileDispositionInformation
	{
		public bool DeleteFile;

		internal static FileDispositionInformation FromPtr(System.IntPtr ptr)
		{ return (FileDispositionInformation)Marshal.PtrToStructure(ptr, typeof(FileDispositionInformation)); }
	}

	/// <remarks>EndOfFile specifies the byte offset to the end of the file. Because this value is zero-based, it actually refers to the first free byte in the file: that is, it is the offset to the byte immediately following the last valid byte in the file.</remarks>
	internal struct FileEndOfFileInformation
	{
		public long EndOfFile;

		internal static FileEndOfFileInformation FromPtr(System.IntPtr ptr)
		{ return (FileEndOfFileInformation)Marshal.PtrToStructure(ptr, typeof(FileEndOfFileInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
	public struct FileFullEaInformation
	{
		public uint NextEntryOffset;
		public byte Flags;
		public byte EaNameLength;
		public ushort EaValueLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string EaName;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] EaValue;


		internal static FileFullEaInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileFullEaInformation> result = new List<FileFullEaInformation>();
			FileFullEaInformation current;
			current = (FileFullEaInformation)Marshal.PtrToStructure(ptr, typeof(FileFullEaInformation));
			while (current.NextEntryOffset != 0)
			{
				unsafe
				{
					current.EaValue = new byte[(int)current.EaValueLength];
					byte* pEa = (byte*)ptr + (int)Marshal.OffsetOf(typeof(FileFullEaInformation), "EaValue");
					Marshal.Copy(new System.IntPtr(pEa), current.EaValue, 0, current.EaValueLength);
					result.Add(current);
					current = (FileFullEaInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileFullEaInformation));
				}
			}
			return result.ToArray();
		}
	}

	internal struct FileNameInformation
	{
		public uint FileNameLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileNameInformation FromPtr(System.IntPtr ptr)
		{
			FileNameInformation fileInformation = (FileNameInformation)Marshal.PtrToStructure(ptr, typeof(FileNameInformation));
			unsafe
			{
				fileInformation.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNameInformation), "FileName")), (int)fileInformation.FileNameLength / sizeof(char));
			}
			return fileInformation;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileNetworkOpenInformation
	{
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long AllocationSize;
		public long EndOfFile;
		public FileAttributes FileAttributes;

		internal static FileNetworkOpenInformation FromPtr(System.IntPtr ptr)
		{ return (FileNetworkOpenInformation)Marshal.PtrToStructure(ptr, typeof(FileNetworkOpenInformation)); }
	}

	public struct FileFullAttributeInformation
	{
		internal FileFullAttributeInformation(FileNetworkOpenInformation info)
			: this()
		{
			this.CreationTime = System.DateTime.FromFileTime(info.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(info.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(info.ChangeTime);
			this.AllocationSize = info.AllocationSize;
			this.EndOfFile = info.EndOfFile;
			this.FileAttributes = info.FileAttributes;
		}

		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long AllocationSize { get; set; }
		public long EndOfFile { get; set; }
		public FileAttributes FileAttributes { get; set; }
	}

	internal struct FilePositionInformation
	{
		/// <summary>If the file was opened or created with the <see cref="FileCreateOptions.NoIntermediateBuffering"/> option, the value of <see cref="CurrentByteOffset"/> must be an integral multiple of the sector size of the underlying device.</summary>
		public long CurrentByteOffset;

		internal static FilePositionInformation FromPtr(System.IntPtr ptr)
		{ return (FilePositionInformation)Marshal.PtrToStructure(ptr, typeof(FilePositionInformation)); }
	}

	internal struct FileStandardInformation
	{
		/// <summary>The file allocation size in bytes. Usually, this value is a multiple of the sector or cluster size of the underlying physical device.</summary>
		public long AllocationSize;
		/// <summary>Specifies the byte offset to the end of the file. Because this value is zero-based, it actually refers to the first free byte in the file; that is, it is the offset to the byte immediately following the last valid byte in the file.</summary>
		public long EndOfFile;
		public uint NumberOfHardLinks;
		public bool DeletePending;
		public bool Directory;

		internal static FileStandardInformation FromPtr(System.IntPtr ptr)
		{ return (FileStandardInformation)Marshal.PtrToStructure(ptr, typeof(FileStandardInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileValidDataLengthInformation
	{
		public long ValidDataLength;

		internal static FileValidDataLengthInformation FromPtr(System.IntPtr ptr)
		{ return (FileValidDataLengthInformation)Marshal.PtrToStructure(ptr, typeof(FileValidDataLengthInformation)); }
	}

	internal struct FileAllInformation
	{
		public FileBasicInformation BasicInformation;
		public FileStandardInformation StandardInformation;
		public FileInternalInformation InternalInformation;
		public FileEaInformation EaInformation;
		public FileAccessInformation AccessInformation;
		public FilePositionInformation PositionInformation;
		public FileModeInformation ModeInformation;
		public FileAlignmentInformation AlignmentInformation;
		public FileNameInformation NameInformation;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
	internal struct FileNamesInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public uint FileNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileNamesInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileNamesInformation> result = new List<FileNamesInformation>();
			FileNamesInformation current;
			do
			{
				unsafe
				{
					current = (FileNamesInformation)Marshal.PtrToStructure(ptr, typeof(FileNamesInformation));
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNamesInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					ptr = new IntPtr((byte*)ptr + current.NextEntryOffset);
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}

		internal static FileNamesInformation FromPtr1(System.IntPtr ptr)
		{
			FileNamesInformation result = (FileNamesInformation)Marshal.PtrToStructure(ptr, typeof(FileNamesInformation));
			unsafe { result.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNamesInformation), "FileName")), (int)result.FileNameLength / sizeof(char)); }
			return result;
		}
	}

	public struct FileNotifyInformation
	{
		internal uint NextEntryOffset;
		public FileAction Action;
		internal uint FileNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileNotifyInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileNotifyInformation> result = new List<FileNotifyInformation>();
			FileNotifyInformation current;
			do
			{
				unsafe
				{
					current = (FileNotifyInformation)Marshal.PtrToStructure(ptr, typeof(FileNotifyInformation));
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNotifyInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					ptr = new IntPtr((byte*)ptr + current.NextEntryOffset);
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}
	}

	internal struct FileObjectIdInformation
	{
		public long FileReference;
		public Guid ObjectId;
		public Guid BirthVolumeId;
		public Guid BirthObjectId;
		public Guid DomainId;
	}

	internal struct FileAccessInformation
	{
		public FileAccessMask AccessFlags;

		internal static FileAccessInformation FromPtr(System.IntPtr ptr)
		{ return (FileAccessInformation)Marshal.PtrToStructure(ptr, typeof(FileAccessInformation)); }
	}

	internal struct FileAllocationInformation
	{
		public long AllocationSize;

		internal static FileAllocationInformation FromPtr(System.IntPtr ptr)
		{ return (FileAllocationInformation)Marshal.PtrToStructure(ptr, typeof(FileAllocationInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
	internal struct FileBothDirectoryInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long EndOfFile;
		public long AllocationSize;
		public FileAttributes FileAttributes;
		public uint FileNameLength;
		public uint EaSize;
		public byte ShortNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
		public string ShortName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileBothDirectoryInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileBothDirectoryInformation> result = new List<FileBothDirectoryInformation>();
			FileBothDirectoryInformation current;
			current = (FileBothDirectoryInformation)Marshal.PtrToStructure(ptr, typeof(FileBothDirectoryInformation));
			do
			{
				unsafe
				{
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileBothDirectoryInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					current = (FileBothDirectoryInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileBothDirectoryInformation));
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}
	}

	public struct DirectoryFileBothInformation
	{
		internal DirectoryFileBothInformation(FileBothDirectoryInformation information)
			: this()
		{
			this.FileIndex = information.FileIndex;
			this.CreationTime = System.DateTime.FromFileTime(information.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(information.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(information.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(information.ChangeTime);
			this.EndOfFile = information.EndOfFile;
			this.AllocationSize = information.AllocationSize;
			this.FileAttributes = information.FileAttributes;
			this.ExtendedAttributesSize = information.EaSize;
			this.ShortName = information.ShortName.Substring(0, information.ShortNameLength / sizeof(char));
			this.FileName = information.FileName;
		}

		public uint FileIndex { get; set; }
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long EndOfFile { get; set; }
		public long AllocationSize { get; set; }
		public FileAttributes FileAttributes { get; set; }
		public uint ExtendedAttributesSize { get; set; }
		public string ShortName { get; set; }
		public string FileName { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}, Short Name = \"{1}\", Attributes = {2}", this.FileName, this.ShortName, this.FileAttributes);
			//return string.Format("{{Name = {8}, Index = {0}, Creation Time = {1}, Last Access Time = {2}, Last Write Time = {3}, Change Time = {4}, End Of File = {5}, Allocation Size = {6}, Attributes = {7}}}", this.FileIndex, this.CreationTime, this.LastAccessTime, this.LastWriteTime, this.ChangeTime, this.EndOfFile, this.AllocationSize, this.FileAttributes, this.FileName);
		}
	}

	internal struct FileCompletionInformation
	{
		public System.IntPtr Port;
		public uint Key;

		internal static FileCompletionInformation FromPtr(System.IntPtr ptr)
		{ return (FileCompletionInformation)Marshal.PtrToStructure(ptr, typeof(FileCompletionInformation)); }
	}

	internal struct FileCompressionInformation
	{
		public long CompressedFileSize;
		public ushort CompressionFormat;
		public byte CompressionUnitShift;
		public byte ChunkShift;
		public byte ClusterShift;
		public unsafe fixed byte Reserved[3];

		internal static FileCompressionInformation FromPtr(System.IntPtr ptr)
		{ return (FileCompressionInformation)Marshal.PtrToStructure(ptr, typeof(FileCompressionInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileCopyOnWriteInformation
	{
		public bool ReplaceIfExists;
		public System.IntPtr RootDirectory;
		public uint FileNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileCopyOnWriteInformation FromPtr(System.IntPtr ptr)
		{
			FileCopyOnWriteInformation fileInformation = (FileCopyOnWriteInformation)Marshal.PtrToStructure(ptr, typeof(FileCopyOnWriteInformation));
			unsafe
			{
				fileInformation.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileCopyOnWriteInformation), "FileName")), (int)fileInformation.FileNameLength / sizeof(char));
			}
			return fileInformation;
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileDirectoryInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long EndOfFile;
		public long AllocationSize;
		public FileAttributes FileAttributes;
		public uint FileNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileDirectoryInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileDirectoryInformation> result = new List<FileDirectoryInformation>();
			FileDirectoryInformation current;
			current = (FileDirectoryInformation)Marshal.PtrToStructure(ptr, typeof(FileDirectoryInformation));
			unsafe
			{
				current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileDirectoryInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
				result.Add(current);
				while (current.NextEntryOffset != 0)
				{
					current = (FileDirectoryInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileDirectoryInformation));
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileDirectoryInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
				}
			}
			return result.ToArray();
		}
	}

	public struct DirectoryFileInformation
	{
		internal DirectoryFileInformation(FileDirectoryInformation information)
			: this()
		{
			this.FileIndex = information.FileIndex;
			this.CreationTime = System.DateTime.FromFileTime(information.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(information.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(information.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(information.ChangeTime);
			this.EndOfFile = information.EndOfFile;
			this.AllocationSize = information.AllocationSize;
			this.FileAttributes = information.FileAttributes;
			this.FileName = information.FileName;
		}

		public uint FileIndex { get; set; }
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long EndOfFile { get; set; }
		public long AllocationSize { get; set; }
		public FileAttributes FileAttributes { get; set; }
		public string FileName { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}, Attributes = {1}", this.FileName, this.FileAttributes);
			//return string.Format("{{Name = {8}, Index = {0}, Creation Time = {1}, Last Access Time = {2}, Last Write Time = {3}, Change Time = {4}, End Of File = {5}, Allocation Size = {6}, Attributes = {7}}}", this.FileIndex, this.CreationTime, this.LastAccessTime, this.LastWriteTime, this.ChangeTime, this.EndOfFile, this.AllocationSize, this.FileAttributes, this.FileName);
		}
	}

	internal struct FileEaInformation
	{
		public uint EaSize;

		internal static FileEaInformation FromPtr(System.IntPtr ptr)
		{ return (FileEaInformation)Marshal.PtrToStructure(ptr, typeof(FileEaInformation)); }
	}

	public struct FileFsAttributeInformation
	{
		public uint FileSystemAttributes;
		public uint MaximumComponentNameLength;
		public uint FileSystemNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileSystemName;

		internal static FileFsAttributeInformation FromPtr(System.IntPtr ptr)
		{
			FileFsAttributeInformation fileInformation = (FileFsAttributeInformation)Marshal.PtrToStructure(ptr, typeof(FileFsAttributeInformation));
			unsafe
			{
				fileInformation.FileSystemName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileFsAttributeInformation), "FileSystemName")), (int)fileInformation.FileSystemNameLength / sizeof(char));
			}
			return fileInformation;
		}
	}

	public struct FileFsControlInformation
	{
		public long FreeSpaceStartFiltering;
		public long FreeSpaceThreshold;
		public long FreeSpaceStopFiltering;
		public long DefaultQuotaThreshold;
		public long DefaultQuotaLimit;
		public uint FileSystemControlFlags;

		internal static FileFsControlInformation FromPtr(System.IntPtr ptr)
		{ return (FileFsControlInformation)Marshal.PtrToStructure(ptr, typeof(FileFsControlInformation)); }

		public override string ToString()
		{
			return string.Format("{{FreeSpaceStartFiltering = {0}, FreeSpaceThreshold = {1}, FreeSpaceStopFiltering = {2}, DefaultQuotaThreshold = {3}, DefaultQuotaLimit = {4}, FileSystemControlFlags = {5}}}",
				this.FreeSpaceStartFiltering, this.FreeSpaceThreshold, this.FreeSpaceStopFiltering, this.DefaultQuotaThreshold, this.DefaultQuotaLimit, this.FileSystemControlFlags);
		}
	}

	public struct FileFsFullSizeInformation
	{
		public long TotalAllocationUnits;
		public long CallerAvailableAllocationUnits;
		public long ActualAvailableAllocationUnits;
		public uint SectorsPerAllocationUnit;
		public uint BytesPerSector;

		internal static FileFsFullSizeInformation FromPtr(System.IntPtr ptr)
		{ return (FileFsFullSizeInformation)Marshal.PtrToStructure(ptr, typeof(FileFsFullSizeInformation)); }
	}

	public struct FileFsLabelInformation
	{
		public uint VolumeLabelLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string VolumeLabel;

		internal uint GetMarshaledSize()
		{
			return (uint)Marshal.SizeOf(this) + (uint)(this.VolumeLabel.Length - 1) * sizeof(char);
		}

		internal static FileFsLabelInformation FromPtr(System.IntPtr ptr)
		{
			FileFsLabelInformation fileInformation = (FileFsLabelInformation)Marshal.PtrToStructure(ptr, typeof(FileFsLabelInformation));
			unsafe
			{
				fileInformation.VolumeLabel = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileFsLabelInformation), "VolumeLabel")), (int)fileInformation.VolumeLabelLength / sizeof(char));
			}
			return fileInformation;
		}

		public override string ToString()
		{
			return this.VolumeLabel;
		}
	}

	public struct FileFsObjectIdInformation
	{
		public Guid ObjectId;
		public Guid BirthVolumeId;
		public Guid BirthObjectId;
		public Guid DomainId;

		internal static FileFsObjectIdInformation FromPtr(System.IntPtr ptr)
		{ return (FileFsObjectIdInformation)Marshal.PtrToStructure(ptr, typeof(FileFsObjectIdInformation)); }
	}

	public struct FileFsSizeInformation
	{
		public long TotalAllocationUnits;
		public long AvailableAllocationUnits;
		public uint SectorsPerAllocationUnit;
		public uint BytesPerSector;

		internal static FileFsSizeInformation FromPtr(System.IntPtr ptr)
		{ return (FileFsSizeInformation)Marshal.PtrToStructure(ptr, typeof(FileFsSizeInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
	public struct FileFsVolumeInformation
	{
		public long VolumeCreationTime;
		public uint VolumeSerialNumber;
		public uint VolumeLabelLength;
		public bool SupportsObjects;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string VolumeLabel;

		internal static FileFsVolumeInformation FromPtr(System.IntPtr ptr)
		{
			FileFsVolumeInformation fileInformation = (FileFsVolumeInformation)Marshal.PtrToStructure(ptr, typeof(FileFsVolumeInformation));
			unsafe
			{
				fileInformation.VolumeLabel = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr - sizeof(char) + (int)Marshal.OffsetOf(typeof(FileFsVolumeInformation), "VolumeLabel")), (int)fileInformation.VolumeLabelLength / sizeof(char));
			}
			return fileInformation;
		}

		public override string ToString()
		{
			return string.Format("{{CreationTime = {0}, SerialNumber = {1}, SupportsObjects = {2}, Label = {3}}}", System.DateTime.FromFileTime(this.VolumeCreationTime), this.VolumeSerialNumber, this.SupportsObjects, this.VolumeLabel);
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileFullDirectoryInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long EndOfFile;
		public long AllocationSize;
		public FileAttributes FileAttributes;
		public uint FileNameLength;
		public uint EaSize;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileFullDirectoryInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileFullDirectoryInformation> result = new List<FileFullDirectoryInformation>();
			FileFullDirectoryInformation current;
			current = (FileFullDirectoryInformation)Marshal.PtrToStructure(ptr, typeof(FileFullDirectoryInformation));
			do
			{
				unsafe
				{
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileFullDirectoryInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					current = (FileFullDirectoryInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileFullDirectoryInformation));
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}
	}

	public struct DirectoryFileFullInformation
	{
		internal DirectoryFileFullInformation(FileFullDirectoryInformation information)
			: this()
		{
			this.FileIndex = information.FileIndex;
			this.CreationTime = System.DateTime.FromFileTime(information.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(information.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(information.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(information.ChangeTime);
			this.EndOfFile = information.EndOfFile;
			this.AllocationSize = information.AllocationSize;
			this.FileAttributes = information.FileAttributes;
			this.ExtendedAttributesSize = information.EaSize;
			this.FileName = information.FileName;
		}

		public uint FileIndex { get; set; }
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long EndOfFile { get; set; }
		public long AllocationSize { get; set; }
		public FileAttributes FileAttributes { get; set; }
		public uint ExtendedAttributesSize { get; set; }
		public string FileName { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}, Attributes = {1}", this.FileName, this.FileAttributes);
			//return string.Format("{{Name = {8}, Index = {0}, Creation Time = {1}, Last Access Time = {2}, Last Write Time = {3}, Change Time = {4}, End Of File = {5}, Allocation Size = {6}, Attributes = {7}}}", this.FileIndex, this.CreationTime, this.LastAccessTime, this.LastWriteTime, this.ChangeTime, this.EndOfFile, this.AllocationSize, this.FileAttributes, this.FileName);
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileIdBothDirInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long EndOfFile;
		public long AllocationSize;
		public FileAttributes FileAttributes;
		public uint FileNameLength;
		public uint EaSize;
		public byte ShortNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
		public string ShortName;
		public long FileId;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;


		internal static FileIdBothDirInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileIdBothDirInformation> result = new List<FileIdBothDirInformation>();
			FileIdBothDirInformation current;
			current = (FileIdBothDirInformation)Marshal.PtrToStructure(ptr, typeof(FileIdBothDirInformation));
			while (current.NextEntryOffset != 0)
			{
				unsafe
				{
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileIdBothDirInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					current = (FileIdBothDirInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileIdBothDirInformation));
				}
			}
			return result.ToArray();
		}

		public uint CalculateMarshaledSize()
		{ return (uint)Marshal.SizeOf(this) + this.FileNameLength - 1 * sizeof(char); }
	}

	public struct DirectoryFileIDBothInformation
	{
		internal DirectoryFileIDBothInformation(FileIdBothDirInformation information)
			: this()
		{
			this.FileIndex = information.FileIndex;
			this.CreationTime = System.DateTime.FromFileTime(information.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(information.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(information.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(information.ChangeTime);
			this.EndOfFile = information.EndOfFile;
			this.AllocationSize = information.AllocationSize;
			this.FileAttributes = information.FileAttributes;
			this.ExtendedAttributesSize = information.EaSize;
			this.ShortName = information.ShortName.Substring(0, information.ShortNameLength / sizeof(char));
			this.FileID = information.FileId;
			this.FileName = information.FileName;
		}

		public uint FileIndex { get; set; }
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long EndOfFile { get; set; }
		public long AllocationSize { get; set; }
		public FileAttributes FileAttributes { get; set; }
		public uint ExtendedAttributesSize { get; set; }
		public string ShortName { get; set; }
		public long FileID { get; set; }
		public string FileName { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}, Short Name = \"{1}\", ID = {2}, Attributes = {3}", this.FileName, this.ShortName, this.FileID, this.FileAttributes);
			//return string.Format("{{Name = {8}, Index = {0}, Creation Time = {1}, Last Access Time = {2}, Last Write Time = {3}, Change Time = {4}, End Of File = {5}, Allocation Size = {6}, Attributes = {7}}}", this.FileIndex, this.CreationTime, this.LastAccessTime, this.LastWriteTime, this.ChangeTime, this.EndOfFile, this.AllocationSize, this.FileAttributes, this.FileName);
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileIdFullDirInformation
	{
		public uint NextEntryOffset;
		public uint FileIndex;
		public long CreationTime;
		public long LastAccessTime;
		public long LastWriteTime;
		public long ChangeTime;
		public long EndOfFile;
		public long AllocationSize;
		public FileAttributes FileAttributes;
		public uint FileNameLength;
		public uint EaSize;
		public long FileId;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileIdFullDirInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileIdFullDirInformation> result = new List<FileIdFullDirInformation>();
			FileIdFullDirInformation current;
			current = (FileIdFullDirInformation)Marshal.PtrToStructure(ptr, typeof(FileIdFullDirInformation));
			while (current.NextEntryOffset != 0)
			{
				unsafe
				{
					current.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileIdFullDirInformation), "FileName")), (int)current.FileNameLength / sizeof(char));
					result.Add(current);
					current = (FileIdFullDirInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryOffset), typeof(FileIdFullDirInformation));
				}
			}
			return result.ToArray();
		}

		public uint CalculateMarshaledSize()
		{ return (uint)Marshal.SizeOf(this) + this.FileNameLength - 1 * sizeof(char); }
	}

	public struct DirectoryFileIDFullInformation
	{
		internal DirectoryFileIDFullInformation(FileIdFullDirInformation information)
			: this()
		{
			this.FileIndex = information.FileIndex;
			this.CreationTime = System.DateTime.FromFileTime(information.CreationTime);
			this.LastAccessTime = System.DateTime.FromFileTime(information.LastAccessTime);
			this.LastWriteTime = System.DateTime.FromFileTime(information.LastWriteTime);
			this.ChangeTime = System.DateTime.FromFileTime(information.ChangeTime);
			this.EndOfFile = information.EndOfFile;
			this.AllocationSize = information.AllocationSize;
			this.FileAttributes = information.FileAttributes;
			this.ExtendedAttributesSize = information.EaSize;
			this.FileID = information.FileId;
			this.FileName = information.FileName;
		}

		public uint FileIndex { get; set; }
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
		public System.DateTime LastWriteTime { get; set; }
		public System.DateTime ChangeTime { get; set; }
		public long EndOfFile { get; set; }
		public long AllocationSize { get; set; }
		public FileAttributes FileAttributes { get; set; }
		public uint ExtendedAttributesSize { get; set; }
		public long FileID { get; set; }
		public string FileName { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}, ID = {1}, Attributes = {2}", this.FileName, this.FileID, this.FileAttributes);
			//return string.Format("{{Name = {8}, Index = {0}, Creation Time = {1}, Last Access Time = {2}, Last Write Time = {3}, Change Time = {4}, End Of File = {5}, Allocation Size = {6}, Attributes = {7}}}", this.FileIndex, this.CreationTime, this.LastAccessTime, this.LastWriteTime, this.ChangeTime, this.EndOfFile, this.AllocationSize, this.FileAttributes, this.FileName);
		}
	}

	internal struct FileInternalInformation
	{
		public long IndexNumber;

		internal static FileInternalInformation FromPtr(System.IntPtr ptr)
		{ return (FileInternalInformation)Marshal.PtrToStructure(ptr, typeof(FileInternalInformation)); }
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FileLinkInformation
	{
		public bool ReplaceIfExists;
		public System.IntPtr RootDirectory;
		public uint FileNameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileLinkInformation FromPtr(System.IntPtr ptr)
		{
			FileLinkInformation fileInformation = (FileLinkInformation)Marshal.PtrToStructure(ptr, typeof(FileLinkInformation));
			unsafe
			{
				fileInformation.FileName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileLinkInformation), "FileName")), (int)fileInformation.FileNameLength / sizeof(char));
			}
			return fileInformation;
		}

		public uint CalculateMarshaledSize()
		{ return (uint)Marshal.SizeOf(this) + this.FileNameLength - 1 * sizeof(char); }
	}

	internal struct FileModeInformation
	{
		public uint Mode;

		internal static FileModeInformation FromPtr(System.IntPtr ptr)
		{ return (FileModeInformation)Marshal.PtrToStructure(ptr, typeof(FileModeInformation)); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct FileRenameInformation
	{
		public bool ReplaceIfExists;
		public System.IntPtr RootDirectory;
		public uint FileNameLength;
		public unsafe fixed char FileName[1];

		internal uint GetMarshaledSize()
		{
			return (uint)Marshal.SizeOf(this) + this.FileNameLength - sizeof(char);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FileStreamInformation
	{
		internal uint NextEntryOffset;
		internal uint StreamNameLength;
		public long StreamSize;
		public long StreamAllocationSize;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string StreamName;

		internal static FileStreamInformation[] FromPtr(System.IntPtr ptr)
		{
			List<FileStreamInformation> result = new List<FileStreamInformation>();
			FileStreamInformation current = new FileStreamInformation();
			do
			{
				unsafe
				{
					current = (FileStreamInformation)Marshal.PtrToStructure(ptr, typeof(FileStreamInformation));
					current.StreamName = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileStreamInformation), "StreamName")), (int)current.StreamNameLength / sizeof(char));
					result.Add(current);
					ptr = new IntPtr((byte*)ptr + current.NextEntryOffset);
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}

		internal uint CalculateMarshaledSize()
		{ return (uint)Marshal.SizeOf(this) + this.StreamNameLength - 1 * sizeof(char); }

		public override string ToString()
		{ return string.Format("{{Name = {0}, Size = {1}, Allocation Size = {2}}}", this.StreamName, this.StreamSize, this.StreamAllocationSize); }
	}

	internal struct FileTrackingInformation
	{
		public System.IntPtr DestinationFile;
		public uint ObjectInformationLength;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] ObjectInformation;

		internal static FileTrackingInformation FromPtr(System.IntPtr ptr)
		{
			FileTrackingInformation fileInformation = (FileTrackingInformation)Marshal.PtrToStructure(ptr, typeof(FileTrackingInformation));
			unsafe
			{
				fileInformation.ObjectInformation = new byte[fileInformation.ObjectInformationLength];
				Marshal.Copy(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileTrackingInformation), "ObjectInformation") + fileInformation.ObjectInformationLength + 1), fileInformation.ObjectInformation, 0, (int)fileInformation.ObjectInformationLength);
			}
			return fileInformation;
		}
	}
}