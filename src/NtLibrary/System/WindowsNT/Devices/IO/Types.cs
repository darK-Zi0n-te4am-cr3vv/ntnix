using System.Runtime.InteropServices;
using BOOLEAN = System.Boolean;
using BYTE = System.Byte;
using DWORD = System.UInt32;
using DWORDLONG = System.UInt64;
using HANDLE = System.IntPtr;
using LARGE_INTEGER = System.Int64;
using LONGLONG = System.Int64;
using UINT32 = System.UInt32;
using ULONG = System.UInt32;
using USHORT = System.UInt16;
using USN = System.Int64;
using WCHAR = System.Char;
using WORD = System.Int16;
namespace System.WindowsNT.Devices.IO
{
	internal struct NTFS_EXTENDED_VOLUME_DATA
	{
		public DWORD ByteCount;
		public WORD MajorVersion;
		public WORD MinorVersion;
	}
	internal struct STARTING_LCN_INPUT_BUFFER
	{
		public LARGE_INTEGER StartingLcn;
	}
	internal struct VOLUME_BITMAP_BUFFER
	{
		public LARGE_INTEGER StartingLcn;
		public LARGE_INTEGER BitmapSize;
		public unsafe fixed BYTE Buffer[1];
	}
	internal struct STARTING_VCN_INPUT_BUFFER
	{
		public LARGE_INTEGER StartingVcn;
	}
	public struct NTFS_VOLUME_EXTENDED_VOLUME_DATA
	{
		public LARGE_INTEGER VolumeSerialNumber;
		public LARGE_INTEGER NumberSectors;
		public LARGE_INTEGER TotalClusters;
		public LARGE_INTEGER FreeClusters;
		public LARGE_INTEGER TotalReserved;
		public DWORD BytesPerSector;
		public DWORD BytesPerCluster;
		public DWORD BytesPerFileRecordSegment;
		public DWORD ClustersPerFileRecordSegment;
		public LARGE_INTEGER MftValidDataLength;
		public LARGE_INTEGER MftStartLcn;
		public LARGE_INTEGER Mft2StartLcn;
		public LARGE_INTEGER MftZoneStart;
		public LARGE_INTEGER MftZoneEnd;
		public ULONG ByteCount;
		public USHORT MajorVersion;
		public USHORT MinorVersion;
	}
	[System.Diagnostics.DebuggerDisplay("StartingVCN = {StartingVCN}")]
	public struct RETRIEVAL_POINTERS_BUFFER
	{
		public DWORD ExtentCount;
		public LARGE_INTEGER StartingVCN;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public MappingPair[] Extents;
		[System.Runtime.CompilerServices.IndexerName("ExtentLengths")]
		public long this[int extentIndex]
		{ get { return this.Extents[extentIndex].NextVCN - (extentIndex == 0 ? this.StartingVCN : this.Extents[extentIndex - 1].NextVCN); } }
		internal static RETRIEVAL_POINTERS_BUFFER FromPtr(System.IntPtr ptr)
		{
			RETRIEVAL_POINTERS_BUFFER result = (RETRIEVAL_POINTERS_BUFFER)Marshal.PtrToStructure(ptr, typeof(RETRIEVAL_POINTERS_BUFFER));
			result.Extents = new MappingPair[result.ExtentCount];
			unsafe
			{
				MappingPair* pExtents = (MappingPair*)((byte*)ptr + (int)Marshal.OffsetOf(typeof(RETRIEVAL_POINTERS_BUFFER), "Extents"));
				for (int i = 0; i < result.Extents.Length; i++)
				{
					result.Extents[i] = pExtents[i];
				}
			}
			return result;
		}
		public override string ToString()
		{
			return string.Format("{{Starting VCN = {0}, Extents = [{1}]}}", this.StartingVCN, string.Join(", ", System.Array.ConvertAll(this.Extents, e => e.ToString())));
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct NTFS_FILE_RECORD_OUTPUT_BUFFER
	{
		public LARGE_INTEGER FileReferenceNumber;
		public DWORD FileRecordLength;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public BYTE[] FileRecordBuffer;

		public System.WindowsNT.Devices.IO.FileTables1.FileRecord GetManagedRecord()
		{
			return IO.FileSystem.ConvertToManagedFileRecord(this.FileReferenceNumber, this.FileRecordBuffer);
		}

		public Devices.IO.FileTables2.FileRecord GetManagedStruct()
		{
			return new System.WindowsNT.Devices.IO.FileTables2.FileRecord(this);
		}

		internal static LARGE_INTEGER ReadReferenceNumber(System.IntPtr ptr) { unsafe { return *(LARGE_INTEGER*)ptr; } }

		internal static DWORD ReadRecordLength(System.IntPtr ptr) { unsafe { return *(DWORD*)((byte*)ptr + sizeof(LARGE_INTEGER)); } }

		internal static IntPtr GetBufferPtr(System.IntPtr ptr) { unsafe { return (IntPtr)((byte*)ptr + sizeof(LARGE_INTEGER) + sizeof(DWORD)); } }

		internal static NTFS_FILE_RECORD_OUTPUT_BUFFER FromPtr(System.IntPtr ptr)
		{
			NTFS_FILE_RECORD_OUTPUT_BUFFER result = default(NTFS_FILE_RECORD_OUTPUT_BUFFER);
			result = (NTFS_FILE_RECORD_OUTPUT_BUFFER)Marshal.PtrToStructure(ptr, typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER));
			unsafe
			{
				byte* pFileRecord = (byte*)ptr + (int)Marshal.OffsetOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER), "FileRecordBuffer");
				result.FileRecordBuffer = new BYTE[result.FileRecordLength];
				Marshal.Copy(new System.IntPtr(pFileRecord), result.FileRecordBuffer, 0, (int)result.FileRecordLength);
			}
			return result;
		}
	}
	internal struct NTFS_FILE_RECORD_INPUT_BUFFER
	{
		public LARGE_INTEGER FileReferenceNumber;
	}
	internal struct MOVE_FILE_DATA
	{
		public HANDLE FileHandle;
		public LARGE_INTEGER StartingVcn;
		public LARGE_INTEGER StartingLcn;
		public DWORD ClusterCount;
	}
	internal struct MOVE_FILE_DATA32
	{
		public UINT32 FileHandle;
		public LARGE_INTEGER StartingVcn;
		public LARGE_INTEGER StartingLcn;
		public DWORD ClusterCount;
	}
	internal struct MFT_ENUM_DATA
	{
		public DWORDLONG StartFileReferenceNumber;
		public USN LowUsn;
		public USN HighUsn;
	}
	internal struct CREATE_USN_JOURNAL_DATA
	{
		public DWORDLONG MaximumSize;
		public DWORDLONG AllocationDelta;
	}
	internal struct READ_USN_JOURNAL_DATA
	{
		public USN StartUsn;
		public DWORD ReasonMask;
		public DWORD ReturnOnlyOnClose;
		public DWORDLONG Timeout;
		public DWORDLONG BytesToWaitFor;
		public DWORDLONG UsnJournalID;
	}
	internal struct USN_RECORD
	{
		public DWORD RecordLength;
		public WORD MajorVersion;
		public WORD MinorVersion;
		public DWORDLONG FileReferenceNumber;
		public DWORDLONG ParentFileReferenceNumber;
		public USN Usn;
		public LARGE_INTEGER TimeStamp;
		public DWORD Reason;
		public DWORD SourceInfo;
		public DWORD SecurityId;
		public DWORD FileAttributes;
		public WORD FileNameLength;
		public WORD FileNameOffset;
		public unsafe fixed WCHAR FileName[1];
	}
	internal struct USN_JOURNAL_DATA
	{
		public DWORDLONG UsnJournalID;
		public USN FirstUsn;
		public USN NextUsn;
		public USN LowestValidUsn;
		public USN MaxUsn;
		public DWORDLONG MaximumSize;
		public DWORDLONG AllocationDelta;
	}
	internal struct DELETE_USN_JOURNAL_DATA
	{
		public DWORDLONG UsnJournalID;
		public DWORD DeleteFlags;
	}
	internal struct MARK_HANDLE_INFO
	{
		public DWORD UsnSourceInfo;
		public HANDLE VolumeHandle;
		public DWORD HandleInfo;
	}
	internal struct FILE_PREFETCH
	{
		public DWORD Type;
		public DWORD Count;
		public unsafe fixed DWORDLONG Prefetch[1];
	}
	internal struct MARK_HANDLE_INFO32
	{
		public DWORD UsnSourceInfo;
		public UINT32 VolumeHandle;
		public DWORD HandleInfo;
	}
	internal struct FILESYSTEM_STATISTICS
	{
		public WORD FileSystemType;
		public WORD Version;
		public DWORD SizeOfCompleteStructure;
		public DWORD UserFileReads;
		public DWORD UserFileReadBytes;
		public DWORD UserDiskReads;
		public DWORD UserFileWrites;
		public DWORD UserFileWriteBytes;
		public DWORD UserDiskWrites;
		public DWORD MetaDataReads;
		public DWORD MetaDataReadBytes;
		public DWORD MetaDataDiskReads;
		public DWORD MetaDataWrites;
		public DWORD MetaDataWriteBytes;
		public DWORD MetaDataDiskWrites;
	}
	internal struct FAT_STATISTICS
	{
		public DWORD CreateHits;
		public DWORD SuccessfulCreates;
		public DWORD FailedCreates;
		public DWORD NonCachedReads;
		public DWORD NonCachedReadBytes;
		public DWORD NonCachedWrites;
		public DWORD NonCachedWriteBytes;
		public DWORD NonCachedDiskReads;
		public DWORD NonCachedDiskWrites;
	}
	internal struct BitmapWritesUserLevel
	{
		public WORD Write;
		public WORD Create;
		public WORD SetInfo;
	}
	internal struct MFTWritesUserLevel
	{
		public WORD Write;
		public WORD Create;
		public WORD SetInfo;
		public WORD Flush;
	}
	internal struct MftBitmapWritesUserLevel
	{
		public WORD Write;
		public WORD Create;
		public WORD SetInfo;
		public WORD Flush;
	}
	internal struct Allocate
	{
		public DWORD Calls;
		public DWORD Clusters;
		public DWORD Hints;
		public DWORD RunsReturned;
		public DWORD HintsHonored;
		public DWORD HintsClusters;
		public DWORD Cache;
		public DWORD CacheClusters;
		public DWORD CacheMiss;
		public DWORD CacheMissClusters;
	}
	internal struct NTFS_STATISTICS
	{
		public DWORD LogFileFullExceptions;
		public DWORD OtherExceptions;
		public DWORD MftReads;
		public DWORD MftReadBytes;
		public DWORD MftWrites;
		public DWORD MftWriteBytes;
		public MFTWritesUserLevel MftWritesUserLevel;
		public WORD MftWritesFlushForLogFileFull;
		public WORD MftWritesLazyWriter;
		public WORD MftWritesUserRequest;
		public DWORD Mft2Writes;
		public DWORD Mft2WriteBytes;
		public MFTWritesUserLevel Mft2WritesUserLevel;
		public WORD Mft2WritesFlushForLogFileFull;
		public WORD Mft2WritesLazyWriter;
		public WORD Mft2WritesUserRequest;
		public DWORD RootIndexReads;
		public DWORD RootIndexReadBytes;
		public DWORD RootIndexWrites;
		public DWORD RootIndexWriteBytes;
		public DWORD BitmapReads;
		public DWORD BitmapReadBytes;
		public DWORD BitmapWrites;
		public DWORD BitmapWriteBytes;
		public WORD BitmapWritesFlushForLogFileFull;
		public WORD BitmapWritesLazyWriter;
		public WORD BitmapWritesUserRequest;
		public BitmapWritesUserLevel BitmapWritesUserLevel;
		public DWORD MftBitmapReads;
		public DWORD MftBitmapReadBytes;
		public DWORD MftBitmapWrites;
		public DWORD MftBitmapWriteBytes;
		public WORD MftBitmapWritesFlushForLogFileFull;
		public WORD MftBitmapWritesLazyWriter;
		public WORD MftBitmapWritesUserRequest;
		public MftBitmapWritesUserLevel MftBitmapWritesUserLevel;
		public DWORD UserIndexReads;
		public DWORD UserIndexReadBytes;
		public DWORD UserIndexWrites;
		public DWORD UserIndexWriteBytes;
		public DWORD LogFileReads;
		public DWORD LogFileReadBytes;
		public DWORD LogFileWrites;
		public DWORD LogFileWriteBytes;
		public Allocate Allocate;
	}
	internal struct FILE_OBJECTID_BUFFER
	{
		public Guid ObjectId;
		public Guid BirthVolumeId;
		public Guid BirthObjectId;
		public Guid DomainId;
	}
	internal struct FILE_SET_SPARSE_BUFFER
	{
		public BOOLEAN SetSparse;
	}
	internal struct FILE_ZERO_DATA_INFORMATION
	{
		public LARGE_INTEGER FileOffset;
		public LARGE_INTEGER BeyondFinalZero;
	}
	internal struct FILE_ALLOCATED_RANGE_BUFFER
	{
		public LARGE_INTEGER FileOffset;
		public LARGE_INTEGER Length;
	}
	internal struct ENCRYPTION_BUFFER
	{
		public DWORD EncryptionOperation;
		public unsafe fixed BYTE Private[1];
	}
	internal struct DECRYPTION_STATUS_BUFFER
	{
		public BOOLEAN NoEncryptedStreams;
	}
	internal struct REQUEST_RAW_ENCRYPTED_DATA
	{
		public LONGLONG FileOffset;
		public DWORD Length;
	}
	internal struct ENCRYPTED_DATA_INFO
	{
		public DWORDLONG StartingFileOffset;
		public DWORD OutputBufferOffset;
		public DWORD BytesWithinFileSize;
		public DWORD BytesWithinValidDataLength;
		public WORD CompressionFormat;
		public BYTE DataUnitShift;
		public BYTE ChunkShift;
		public BYTE ClusterShift;
		public BYTE EncryptionFormat;
		public WORD NumberOfDataBlocks;
		public unsafe fixed DWORD DataBlockSize[1];
	}
	internal struct PLEX_READ_DATA_REQUEST
	{
		public LARGE_INTEGER ByteOffset;
		public DWORD ByteLength;
		public DWORD PlexNumber;
	}
	internal struct SI_COPYFILE
	{
		public DWORD SourceFileNameLength;
		public DWORD DestinationFileNameLength;
		public DWORD Flags;
		public unsafe fixed WCHAR FileNameBuffer[1];
	}
	internal struct DISK_EXTENT
	{
		public DWORD DiskNumber;
		public LARGE_INTEGER StartingOffset;
		public LARGE_INTEGER ExtentLength;
	}
	internal struct VOLUME_DISK_EXTENTS
	{
		public DWORD NumberOfDiskExtents;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public DISK_EXTENT[] Extents;
	}
}