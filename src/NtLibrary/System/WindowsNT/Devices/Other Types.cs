using System.Runtime.InteropServices;
using System.WindowsNT.Devices.PrivateImplementationDetails;
using BOOLEAN = System.Boolean;
using BYTE = System.Byte;
using CHAR = System.Byte;
using DEVICE_TYPE = System.UInt32;
using DWORD = System.UInt32;
using DWORD64 = System.UInt64;
using DWORDLONG = System.UInt64;
using GUID = System.Guid;
using HANDLE = System.IntPtr;
using LARGE_INTEGER = System.Int64;
using LONG = System.Int32;
using LONGLONG = System.Int64;
using PVOID = System.IntPtr;
using SHORT = System.Int16;
using UCHAR = System.Byte;
using UINT32 = System.UInt32;
using ULONG = System.UInt32;
using ULONG_PTR = System.UIntPtr;
using ULONGLONG = System.UInt64;
using USHORT = System.UInt16;
using USN = System.Int64;
using WCHAR = System.Char;
using WORD = System.Int16;
namespace System.WindowsNT.Devices
{
	internal struct STORAGE_HOTPLUG_INFO
	{
		public DWORD Size;
		public BOOLEAN MediaRemovable;
		public BOOLEAN MediaHotplug;
		public BOOLEAN DeviceHotplug;
		public BOOLEAN WriteCacheEnableOverride;
	}
	internal struct STORAGE_DEVICE_NUMBER
	{
		public DEVICE_TYPE DeviceType;
		public DWORD DeviceNumber;
		public DWORD PartitionNumber;
	}
	internal struct STORAGE_BUS_RESET_REQUEST
	{
		public BYTE PathId;
	}
	internal struct PREVENT_MEDIA_REMOVAL
	{
		public BOOLEAN PreventMediaRemoval;
	}
	internal struct TAPE_STATISTICS
	{
		public DWORD Version;
		public DWORD Flags;
		public LARGE_INTEGER RecoveredWrites;
		public LARGE_INTEGER UnrecoveredWrites;
		public LARGE_INTEGER RecoveredReads;
		public LARGE_INTEGER UnrecoveredReads;
		public BYTE CompressionRatioReads;
		public BYTE CompressionRatioWrites;
	}
	internal struct TAPE_GET_STATISTICS
	{
		public DWORD Operation;
	}
	internal enum STORAGE_MEDIA_TYPE
	{
		DDS_4mm = 0x20,
		MiniQic,
		Travan,
		QIC,
		MP_8mm,
		AME_8mm,
		AIT1_8mm,
		DLT,
		NCTP,
		IBM_3480,
		IBM_3490E,
		IBM_Magstar_3590,
		IBM_Magstar_MP,
		STK_DATA_D3,
		SONY_DTF,
		DV_6mm,
		DMI,
		SONY_D2,
		CLEANER_CARTRIDGE,
		CD_ROM,
		CD_R,
		CD_RW,
		DVD_ROM,
		DVD_R,
		DVD_RW,
		MO_3_RW,
		MO_5_WO,
		MO_5_RW,
		MO_5_LIMDOW,
		PC_5_WO,
		PC_5_RW,
		PD_5_RW,
		ABL_5_WO,
		PINNACLE_APEX_5_RW,
		SONY_12_WO,
		PHILIPS_12_WO,
		HITACHI_12_WO,
		CYGNET_12_WO,
		KODAK_14_WO,
		MO_NFR_525,
		NIKON_12_RW,
		IOMEGA_ZIP,
		IOMEGA_JAZ,
		SYQUEST_EZ135,
		SYQUEST_EZFLYER,
		SYQUEST_SYJET,
		AVATAR_F2,
		MP2_8mm,
		DST_S,
		DST_M,
		DST_L,
		VXATape_1,
		VXATape_2,
		STK_9840,
		LTO_Ultrium,
		LTO_Accelis,
		DVD_RAM,
		AIT_8mm,
		ADR_1,
		ADR_2,
		STK_9940,
		SAIT
	}
	internal enum STORAGE_BUS_TYPE
	{
		BusTypeUnknown = 0x00,
		BusTypeScsi,
		BusTypeAtapi,
		BusTypeAta,
		BusType1394,
		BusTypeSsa,
		BusTypeFibre,
		BusTypeUsb,
		BusTypeRAID,
		BusTypeMaxReserved = 0x7F
	}
	[StructLayout(LayoutKind.Explicit)]
	internal struct DEVICE_MEDIA_INFO
	{
		public struct DiskInfo
		{
			public LARGE_INTEGER Cylinders;
			public STORAGE_MEDIA_TYPE MediaType;
			public DWORD TracksPerCylinder;
			public DWORD SectorsPerTrack;
			public DWORD BytesPerSector;
			public DWORD NumberMediaSides;
			public DWORD MediaCharacteristics;
		}
		[FieldOffset(0)]
		public DiskInfo DiskInformation;
		public struct RemovableDiskInfo
		{
			public LARGE_INTEGER Cylinders;
			public STORAGE_MEDIA_TYPE MediaType;
			public DWORD TracksPerCylinder;
			public DWORD SectorsPerTrack;
			public DWORD BytesPerSector;
			public DWORD NumberMediaSides;
			public DWORD MediaCharacteristics;
		}
		[FieldOffset(0)]
		public RemovableDiskInfo RemovableDiskInformation;
		public struct TapeInfo
		{
			public STORAGE_MEDIA_TYPE MediaType;
			public DWORD MediaCharacteristics;
			public DWORD CurrentBlockSize;
			public STORAGE_BUS_TYPE BusType;
			public struct ScsiInformation
			{
				public BYTE MediumType;
				public BYTE DensityCode;
			}
			public ScsiInformation BusSpecificData;
		}
		[FieldOffset(0)]
		public TapeInfo TapeInformation;
	}
	internal struct GET_MEDIA_TYPES
	{
		public DWORD DeviceType;
		public DWORD MediaInfoCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public DEVICE_MEDIA_INFO[] MediaInfo;
	}
	internal struct STORAGE_PREDICT_FAILURE
	{
		public DWORD PredictFailure;
		public unsafe fixed BYTE VendorSpecific[512];
	}
	internal enum MEDIA_TYPE
	{
		Unknown,
		F5_1Pt2_512,
		F3_1Pt44_512,
		F3_2Pt88_512,
		F3_20Pt8_512,
		F3_720_512,
		F5_360_512,
		F5_320_512,
		F5_320_1024,
		F5_180_512,
		F5_160_512,
		RemovableMedia,
		FixedMedia,
		F3_120M_512,
		F3_640_512,
		F5_640_512,
		F5_720_512,
		F3_1Pt2_512,
		F3_1Pt23_1024,
		F5_1Pt23_1024,
		F3_128Mb_512,
		F3_230Mb_512,
		F8_256_128,
		F3_200Mb_512,
		F3_240M_512,
		F3_32M_512
	}
	internal struct FORMAT_PARAMETERS
	{
		public MEDIA_TYPE MediaType;
		public DWORD StartCylinderNumber;
		public DWORD EndCylinderNumber;
		public DWORD StartHeadNumber;
		public DWORD EndHeadNumber;
	}
	internal struct FORMAT_EX_PARAMETERS
	{
		public MEDIA_TYPE MediaType;
		public DWORD StartCylinderNumber;
		public DWORD EndCylinderNumber;
		public DWORD StartHeadNumber;
		public DWORD EndHeadNumber;
		public WORD FormatGapLength;
		public WORD SectorsPerTrack;
		public unsafe fixed WORD SectorNumber[1];
	}
	internal struct DISK_GEOMETRY
	{
		public LARGE_INTEGER Cylinders;
		public MEDIA_TYPE MediaType;
		public DWORD TracksPerCylinder;
		public DWORD SectorsPerTrack;
		public DWORD BytesPerSector;
	}
	internal struct PARTITION_INFORMATION
	{
		public LARGE_INTEGER StartingOffset;
		public LARGE_INTEGER PartitionLength;
		public DWORD HiddenSectors;
		public DWORD PartitionNumber;
		public BYTE PartitionType;
		public BOOLEAN BootIndicator;
		public BOOLEAN RecognizedPartition;
		public BOOLEAN RewritePartition;
	}
	internal struct SET_PARTITION_INFORMATION
	{
		public BYTE PartitionType;
	}
	internal struct DRIVE_LAYOUT_INFORMATION
	{
		public DWORD PartitionCount;
		public DWORD Signature;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public PARTITION_INFORMATION[] PartitionEntry;
	}
	internal struct VERIFY_INFORMATION
	{
		public LARGE_INTEGER StartingOffset;
		public DWORD Length;
	}
	internal struct REASSIGN_BLOCKS
	{
		public WORD Reserved;
		public WORD Count;
		public unsafe fixed DWORD BlockNumber[1];
	}
	internal enum PARTITION_STYLE
	{
		PARTITION_STYLE_MBR,
		PARTITION_STYLE_GPT,
		PARTITION_STYLE_RAW
	}
	internal struct PARTITION_INFORMATION_GPT
	{
		public GUID PartitionType;
		public GUID PartitionId;
		public DWORD64 Attributes;
		public unsafe fixed WCHAR Name[36];
	}
	internal struct PARTITION_INFORMATION_MBR
	{
		public BYTE PartitionType;
		public BOOLEAN BootIndicator;
		public BOOLEAN RecognizedPartition;
		public DWORD HiddenSectors;
	}
	internal struct SET_PARTITION_INFORMATION_EX
	{
		public PARTITION_STYLE PartitionStyle;
		[StructLayout(LayoutKind.Explicit)]
		public struct SET_PARTITION_INFORMATION_ANY
		{
			[FieldOffset(0)]
			public SET_PARTITION_INFORMATION Mbr;
			[FieldOffset(0)]
			public PARTITION_INFORMATION_GPT Gpt;
		}
		public SET_PARTITION_INFORMATION_ANY SetPartitionInformation;
	}
	internal struct CREATE_DISK_GPT
	{
		public GUID DiskId;
		public DWORD MaxPartitionCount;
	}
	internal struct CREATE_DISK_MBR
	{
		public DWORD Signature;
	}
	internal struct CREATE_DISK
	{
		public PARTITION_STYLE PartitionStyle;
		public struct PartitionInfo
		{
			public CREATE_DISK_MBR Mbr;
			public CREATE_DISK_GPT Gpt;
		}
		public PartitionInfo CreateDisk;
	}
	internal struct GET_LENGTH_INFORMATION
	{
		public LARGE_INTEGER Length;
	}
	internal struct PARTITION_INFORMATION_EX
	{
		public PARTITION_STYLE PartitionStyle;
		public LARGE_INTEGER StartingOffset;
		public LARGE_INTEGER PartitionLength;
		public DWORD PartitionNumber;
		public BOOLEAN RewritePartition;
		[StructLayout(LayoutKind.Explicit)]
		public struct PARTITION_INFORMATION
		{
			[FieldOffset(0)]
			public PARTITION_INFORMATION_MBR Mbr;
			[FieldOffset(0)]
			public PARTITION_INFORMATION_GPT Gpt;
		}
		public PARTITION_INFORMATION PartitionInformation;
	}
	internal struct DRIVE_LAYOUT_INFORMATION_GPT
	{
		public GUID DiskId;
		public LARGE_INTEGER StartingUsableOffset;
		public LARGE_INTEGER UsableLength;
		public DWORD MaxPartitionCount;
	}
	internal struct DRIVE_LAYOUT_INFORMATION_MBR
	{
		public DWORD Signature;
	}
	internal struct DRIVE_LAYOUT_INFORMATION_EX
	{
		public DWORD PartitionStyle;
		public DWORD PartitionCount;
		[StructLayout(LayoutKind.Explicit)]
		public struct DRIVE_LAYOUT_INFORMATION
		{
			[FieldOffset(0)]
			public DRIVE_LAYOUT_INFORMATION_MBR Mbr;
			[FieldOffset(0)]
			public DRIVE_LAYOUT_INFORMATION_GPT Gpt;
		}
		public DRIVE_LAYOUT_INFORMATION DriveLayoutInformation;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public PARTITION_INFORMATION_EX[] PartitionEntry;
	}
	internal enum DETECTION_TYPE
	{
		DetectNone,
		DetectInt13,
		DetectExInt13
	}
	internal struct DISK_INT13_INFO
	{
		public WORD DriveSelect;
		public DWORD MaxCylinders;
		public WORD SectorsPerTrack;
		public WORD MaxHeads;
		public WORD NumberDrives;
	}
	internal struct DISK_EX_INT13_INFO
	{
		public WORD ExBufferSize;
		public WORD ExFlags;
		public DWORD ExCylinders;
		public DWORD ExHeads;
		public DWORD ExSectorsPerTrack;
		public DWORD64 ExSectorsPerDrive;
		public WORD ExSectorSize;
		public WORD ExReserved;
	}
	internal struct DISK_DETECTION_INFO
	{
		public DWORD SizeOfDetectInfo;
		public DETECTION_TYPE DetectionType;
		public DISK_INT13_INFO Int13;
		public DISK_EX_INT13_INFO ExInt13;
	}
	internal struct DISK_PARTITION_INFO
	{
		public DWORD SizeOfPartitionInfo;
		public PARTITION_STYLE PartitionStyle;
		[StructLayout(LayoutKind.Explicit)]
		public struct PartitionInfo
		{
			public struct MBR
			{
				public DWORD Signature;
				public DWORD CheckSum;
			}
			[FieldOffset(0)]
			public MBR Mbr;
			public struct GPT
			{
				public GUID DiskId;
			}
			[FieldOffset(0)]
			public GPT Gpt;
		}
	}
	internal struct DISK_GEOMETRY_EX
	{
		public DISK_GEOMETRY Geometry;
		public LARGE_INTEGER DiskSize;
		public unsafe fixed BYTE Data[1];
	}
	internal struct DISK_CONTROLLER_NUMBER
	{
		public DWORD ControllerNumber;
		public DWORD DiskNumber;
	}
	internal enum DISK_CACHE_RETENTION_PRIORITY
	{
		EqualPriority,
		KeepPrefetchedData,
		KeepReadData
	}
	internal enum DISK_WRITE_CACHE_STATE
	{
		DiskWriteCacheNormal,
		DiskWriteCacheForceDisable,
		DiskWriteCacheDisableNotSupported
	}
	internal struct DISK_CACHE_INFORMATION
	{
		public BOOLEAN ParametersSavable;
		public BOOLEAN ReadCacheEnabled;
		public BOOLEAN WriteCacheEnabled;
		public DISK_CACHE_RETENTION_PRIORITY ReadRetentionPriority;
		public DISK_CACHE_RETENTION_PRIORITY WriteRetentionPriority;
		public WORD DisablePrefetchTransferLength;
		public BOOLEAN PrefetchScalar;
		[StructLayout(LayoutKind.Explicit)]
		public struct Prefetch
		{
			public struct SCALAR_PREFETCH
			{
				public WORD Minimum;
				public WORD Maximum;
				public WORD MaximumBlocks;
			}
			[FieldOffset(0)]
			public SCALAR_PREFETCH ScalarPrefetch;
			public struct BLOCK_PREFETCH
			{
				public WORD Minimum;
				public WORD Maximum;
			}
			[FieldOffset(0)]
			public BLOCK_PREFETCH BlockPrefetch;
		}
	}
	internal struct DISK_GROW_PARTITION
	{
		public DWORD PartitionNumber;
		public LARGE_INTEGER BytesToGrow;
	}
	internal struct HISTOGRAM_BUCKET
	{
		public DWORD Reads;
		public DWORD Writes;
	}
	internal struct DISK_HISTOGRAM
	{
		public LARGE_INTEGER DiskSize;
		public LARGE_INTEGER Start;
		public LARGE_INTEGER End;
		public LARGE_INTEGER Average;
		public LARGE_INTEGER AverageRead;
		public LARGE_INTEGER AverageWrite;
		public DWORD Granularity;
		public DWORD Size;
		public DWORD ReadCount;
		public DWORD WriteCount;
		public unsafe HISTOGRAM_BUCKET* Histogram;
	}
	internal struct DISK_PERFORMANCE
	{
		public LARGE_INTEGER BytesRead;
		public LARGE_INTEGER BytesWritten;
		public LARGE_INTEGER ReadTime;
		public LARGE_INTEGER WriteTime;
		public LARGE_INTEGER IdleTime;
		public DWORD ReadCount;
		public DWORD WriteCount;
		public DWORD QueueDepth;
		public DWORD SplitCount;
		public LARGE_INTEGER QueryTime;
		public DWORD StorageDeviceNumber;
		public unsafe fixed WCHAR StorageManagerName[8];
	}
	internal struct DISK_RECORD
	{
		public LARGE_INTEGER ByteOffset;
		public LARGE_INTEGER StartTime;
		public LARGE_INTEGER EndTime;
		public PVOID VirtualAddress;
		public DWORD NumberOfBytes;
		public BYTE DeviceNumber;
		public BOOLEAN ReadRequest;
	}
	internal struct DISK_LOGGING
	{
		public BYTE Function;
		public PVOID BufferAddress;
		public DWORD BufferSize;
	}
	internal enum BIN_TYPES
	{
		RequestSize,
		RequestLocation
	}
	internal struct BIN_RANGE
	{
		public LARGE_INTEGER StartValue;
		public LARGE_INTEGER Length;
	}
	internal struct PERF_BIN
	{
		public DWORD NumberOfBins;
		public DWORD TypeOfBin;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public BIN_RANGE[] BinsRanges;
	}
	internal struct BIN_COUNT
	{
		public BIN_RANGE BinRange;
		public DWORD BinCount;
	}
	internal struct BIN_RESULTS
	{
		public DWORD NumberOfBins;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public BIN_COUNT[] BinCounts;
	}
	internal enum ELEMENT_TYPE
	{
		AllElements,
		ChangerTransport,
		ChangerSlot,
		ChangerIEPort,
		ChangerDrive,
		ChangerDoor,
		ChangerKeypad,
		ChangerMaxElement
	}
	internal struct CHANGER_ELEMENT
	{
		public ELEMENT_TYPE ElementType;
		public DWORD ElementAddress;
	}
	internal struct CHANGER_ELEMENT_LIST
	{
		public CHANGER_ELEMENT Element;
		public DWORD NumberOfElements;
	}
	internal struct GET_CHANGER_PARAMETERS
	{
		public DWORD Size;
		public WORD NumberTransportElements;
		public WORD NumberStorageElements;
		public WORD NumberCleanerSlots;
		public WORD NumberIEElements;
		public WORD NumberDataTransferElements;
		public WORD NumberOfDoors;
		public WORD FirstSlotNumber;
		public WORD FirstDriveNumber;
		public WORD FirstTransportNumber;
		public WORD FirstIEPortNumber;
		public WORD FirstCleanerSlotAddress;
		public WORD MagazineSize;
		public DWORD DriveCleanTimeout;
		public DWORD Features0;
		public DWORD Features1;
		public BYTE MoveFromTransport;
		public BYTE MoveFromSlot;
		public BYTE MoveFromIePort;
		public BYTE MoveFromDrive;
		public BYTE ExchangeFromTransport;
		public BYTE ExchangeFromSlot;
		public BYTE ExchangeFromIePort;
		public BYTE ExchangeFromDrive;
		public BYTE LockUnlockCapabilities;
		public BYTE PositionCapabilities;
		public unsafe fixed BYTE Reserved1[2];
		public unsafe fixed DWORD Reserved2[2];
	}
	internal struct CHANGER_PRODUCT_DATA
	{
		public unsafe fixed BYTE VendorId[Wrapper.VENDOR_ID_LENGTH];
		public unsafe fixed BYTE ProductId[Wrapper.PRODUCT_ID_LENGTH];
		public unsafe fixed BYTE Revision[Wrapper.REVISION_LENGTH];
		public unsafe fixed BYTE SerialNumber[Wrapper.SERIAL_NUMBER_LENGTH];
		public BYTE DeviceType;
	}
	internal struct CHANGER_SET_ACCESS
	{
		public CHANGER_ELEMENT Element;
		public DWORD Control;
	}
	internal struct CHANGER_READ_ELEMENT_STATUS
	{
		public CHANGER_ELEMENT_LIST ElementList;
		public BOOLEAN VolumeTagInfo;
	}
	internal struct CHANGER_ELEMENT_STATUS
	{
		public CHANGER_ELEMENT Element;
		public CHANGER_ELEMENT SrcElementAddress;
		public DWORD Flags;
		public DWORD ExceptionCode;
		public BYTE TargetId;
		public BYTE Lun;
		public WORD Reserved;
		public unsafe fixed BYTE PrimaryVolumeID[Wrapper.MAX_VOLUME_ID_SIZE];
		public unsafe fixed BYTE AlternateVolumeID[Wrapper.MAX_VOLUME_ID_SIZE];
	}
	internal struct CHANGER_ELEMENT_STATUS_EX
	{
		public CHANGER_ELEMENT Element;
		public CHANGER_ELEMENT SrcElementAddress;
		public DWORD Flags;
		public DWORD ExceptionCode;
		public BYTE TargetId;
		public BYTE Lun;
		public WORD Reserved;
		public unsafe fixed BYTE PrimaryVolumeID[Wrapper.MAX_VOLUME_ID_SIZE];
		public unsafe fixed BYTE AlternateVolumeID[Wrapper.MAX_VOLUME_ID_SIZE];
		public unsafe fixed BYTE VendorIdentification[Wrapper.VENDOR_ID_LENGTH];
		public unsafe fixed BYTE ProductIdentification[Wrapper.PRODUCT_ID_LENGTH];
		public unsafe fixed BYTE SerialNumber[Wrapper.SERIAL_NUMBER_LENGTH];
	}
	internal struct CHANGER_INITIALIZE_ELEMENT_STATUS
	{
		public CHANGER_ELEMENT_LIST ElementList;
		public BOOLEAN BarCodeScan;
	}
	internal struct CHANGER_SET_POSITION
	{
		public CHANGER_ELEMENT Transport;
		public CHANGER_ELEMENT Destination;
		public BOOLEAN Flip;
	}
	internal struct CHANGER_EXCHANGE_MEDIUM
	{
		public CHANGER_ELEMENT Transport;
		public CHANGER_ELEMENT Source;
		public CHANGER_ELEMENT Destination1;
		public CHANGER_ELEMENT Destination2;
		public BOOLEAN Flip1;
		public BOOLEAN Flip2;
	}
	internal struct CHANGER_MOVE_MEDIUM
	{
		public CHANGER_ELEMENT Transport;
		public CHANGER_ELEMENT Source;
		public CHANGER_ELEMENT Destination;
		public BOOLEAN Flip;
	}
	internal struct CHANGER_SEND_VOLUME_TAG_INFORMATION
	{
		public CHANGER_ELEMENT StartingElement;
		public DWORD ActionCode;
		public unsafe fixed BYTE VolumeIDTemplate[Wrapper.MAX_VOLUME_TEMPLATE_SIZE];
	}
	internal struct READ_ELEMENT_ADDRESS_INFO
	{
		public DWORD NumberOfElements;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public CHANGER_ELEMENT_STATUS[] ElementStatus;
	}
	internal struct PATHNAME_BUFFER
	{
		public DWORD PathNameLength;
		public unsafe fixed WCHAR Name[1];
	}
	internal struct FSCTL_QUERY_FAT_BPB_BUFFER
	{
		public unsafe fixed BYTE First0x24BytesOfBootSector[0x24];
	}
	internal struct VIDEO_VDM
	{
		public HANDLE ProcessHandle;
	}
	internal struct VIDEO_REGISTER_VDM
	{
		public ULONG MinimumStateSize;
	}
	internal struct VIDEO_MONITOR_DEVICE
	{
		public ULONG flag;
		public HANDLE pdo;
		public ULONG HwID;
	}
	internal enum VIDEO_WIN32K_CALLBACKS_PARAMS_TYPE
	{
		VideoPowerNotifyCallout = 1,
		VideoDisplaySwitchCallout,
		VideoEnumChildPdoNotifyCallout,
		VideoFindAdapterCallout,
		VideoWakeupCallout,
		VideoChangeDisplaySettingsCallout
	}
	internal struct VIDEO_WIN32K_CALLBACKS_PARAMS
	{
		public VIDEO_WIN32K_CALLBACKS_PARAMS_TYPE CalloutType;
		public PVOID PhysDisp;
		public ULONG_PTR Param;
		public LONG Status;
	}
	internal delegate void VIDEO_WIN32K_CALLOUT([In] PVOID Params);
	internal struct VIDEO_WIN32K_CALLBACKS
	{
		public PVOID PhysDisp;
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public VIDEO_WIN32K_CALLOUT Callout;
		public ULONG bACPI;
		public HANDLE pPhysDeviceObject;
		public ULONG DualviewFlags;
	}
	internal struct VIDEO_DEVICE_SESSION_STATUS
	{
		public ULONG bEnable;
		public ULONG bSuccess;
	}
	internal struct VIDEO_HARDWARE_STATE_HEADER
	{
		public ULONG Length;
		public unsafe fixed UCHAR PortValue[0x30];
		public ULONG AttribIndexDataState;
		public ULONG BasicSequencerOffset;
		public ULONG BasicCrtContOffset;
		public ULONG BasicGraphContOffset;
		public ULONG BasicAttribContOffset;
		public ULONG BasicDacOffset;
		public ULONG BasicLatchesOffset;
		public ULONG ExtendedSequencerOffset;
		public ULONG ExtendedCrtContOffset;
		public ULONG ExtendedGraphContOffset;
		public ULONG ExtendedAttribContOffset;
		public ULONG ExtendedDacOffset;
		public ULONG ExtendedValidatorStateOffset;
		public ULONG ExtendedMiscDataOffset;
		public ULONG PlaneLength;
		public ULONG Plane1Offset;
		public ULONG Plane2Offset;
		public ULONG Plane3Offset;
		public ULONG Plane4Offset;
		public ULONG VGAStateFlags;
		public ULONG DIBOffset;
		public ULONG DIBBitsPerPixel;
		public ULONG DIBXResolution;
		public ULONG DIBYResolution;
		public ULONG DIBXlatOffset;
		public ULONG DIBXlatLength;
		public ULONG VesaInfoOffset;
		public PVOID FrameBufferData;
	}
	internal struct VIDEO_HARDWARE_STATE
	{
		public unsafe VIDEO_HARDWARE_STATE_HEADER* StateHeader;
		public ULONG StateLength;
	}
	internal struct VIDEO_NUM_MODES
	{
		public ULONG NumModes;
		public ULONG ModeInformationLength;
	}
	internal struct VIDEO_MODE
	{
		public ULONG RequestedMode;
	}
	internal struct VIDEO_MODE_INFORMATION
	{
		public ULONG Length;
		public ULONG ModeIndex;
		public ULONG VisScreenWidth;
		public ULONG VisScreenHeight;
		public ULONG ScreenStride;
		public ULONG NumberOfPlanes;
		public ULONG BitsPerPlane;
		public ULONG Frequency;
		public ULONG XMillimeter;
		public ULONG YMillimeter;
		public ULONG NumberRedBits;
		public ULONG NumberGreenBits;
		public ULONG NumberBlueBits;
		public ULONG RedMask;
		public ULONG GreenMask;
		public ULONG BlueMask;
		public ULONG AttributeFlags;
		public ULONG VideoMemoryBitmapWidth;
		public ULONG VideoMemoryBitmapHeight;
		public ULONG DriverSpecificAttributeFlags;
	}
	internal struct VIDEO_LOAD_FONT_INFORMATION
	{
		public USHORT WidthInPixels;
		public USHORT HeightInPixels;
		public ULONG FontSize;
		public unsafe fixed UCHAR Font[1];
	}
	internal struct VIDEO_PALETTE_DATA
	{
		public USHORT NumEntries;
		public USHORT FirstEntry;
		public unsafe fixed USHORT Colors[1];
	}
	internal struct VIDEO_CLUTDATA
	{
		public UCHAR Red;
		public UCHAR Green;
		public UCHAR Blue;
		public UCHAR Unused;
	}
	internal struct VIDEO_CLUT
	{
		public USHORT NumEntries;
		public USHORT FirstEntry;
		[StructLayout(LayoutKind.Explicit)]
		public struct LOOKUP_TABLE
		{
			[FieldOffset(0)]
			public VIDEO_CLUTDATA RgbArray;
			[FieldOffset(0)]
			public ULONG RgbLong;
		}
		public LOOKUP_TABLE LookupTable;
	}
	internal struct VIDEO_CURSOR_POSITION
	{
		public SHORT Column;
		public SHORT Row;
	}
	internal struct VIDEO_CURSOR_ATTRIBUTES
	{
		public USHORT Width;
		public USHORT Height;
		public SHORT Column;
		public SHORT Row;
		public UCHAR Rate;
		public UCHAR Enable;
	}
	internal struct VIDEO_POINTER_POSITION
	{
		public SHORT Column;
		public SHORT Row;
	}
	internal struct VIDEO_POINTER_ATTRIBUTES
	{
		public ULONG Flags;
		public ULONG Width;
		public ULONG Height;
		public ULONG WidthInBytes;
		public ULONG Enable;
		public SHORT Column;
		public SHORT Row;
		public unsafe fixed UCHAR Pixels[1];
	}
	internal struct VIDEO_POINTER_CAPABILITIES
	{
		public ULONG Flags;
		public ULONG MaxWidth;
		public ULONG MaxHeight;
		public ULONG HWPtrBitmapStart;
		public ULONG HWPtrBitmapEnd;
	}
	internal struct VIDEO_BANK_SELECT
	{
		public ULONG Length;
		public ULONG Size;
		public ULONG BankingFlags;
		public ULONG BankingType;
		public ULONG PlanarHCBankingType;
		public ULONG BitmapWidthInBytes;
		public ULONG BitmapSize;
		public ULONG Granularity;
		public ULONG PlanarHCGranularity;
		public ULONG CodeOffset;
		public ULONG PlanarHCBankCodeOffset;
		public ULONG PlanarHCEnableCodeOffset;
		public ULONG PlanarHCDisableCodeOffset;
	}
	internal enum VIDEO_BANK_TYPE
	{
		VideoNotBanked = 0,
		VideoBanked1RW,
		VideoBanked1R1W,
		VideoBanked2RW,
		NumVideoBankTypes
	}
	internal struct VIDEO_MEMORY
	{
		public PVOID RequestedVirtualAddress;
	}
	internal struct VIDEO_SHARE_MEMORY
	{
		public HANDLE ProcessHandle;
		public ULONG ViewOffset;
		public ULONG ViewSize;
		public PVOID RequestedVirtualAddress;
	}
	internal struct VIDEO_SHARE_MEMORY_INFORMATION
	{
		public ULONG SharedViewOffset;
		public ULONG SharedViewSize;
		public PVOID VirtualAddress;
	}
	internal struct VIDEO_MEMORY_INFORMATION
	{
		public PVOID VideoRamBase;
		public ULONG VideoRamLength;
		public PVOID FrameBufferBase;
		public ULONG FrameBufferLength;
	}
	internal struct VIDEO_PUBLIC_ACCESS_RANGES
	{
		public ULONG InIoSpace;
		public ULONG MappedInIoSpace;
		public PVOID VirtualAddress;
	}
	internal struct VIDEO_COLOR_CAPABILITIES
	{
		public ULONG Length;
		public ULONG AttributeFlags;
		public LONG RedPhosphoreDecay;
		public LONG GreenPhosphoreDecay;
		public LONG BluePhosphoreDecay;
		public LONG WhiteChromaticity_x;
		public LONG WhiteChromaticity_y;
		public LONG WhiteChromaticity_Y;
		public LONG RedChromaticity_x;
		public LONG RedChromaticity_y;
		public LONG GreenChromaticity_x;
		public LONG GreenChromaticity_y;
		public LONG BlueChromaticity_x;
		public LONG BlueChromaticity_y;
		public LONG WhiteGamma;
		public LONG RedGamma;
		public LONG GreenGamma;
		public LONG BlueGamma;
	}
	internal enum VIDEO_POWER_STATE
	{
		VideoPowerUnspecified = 0,
		VideoPowerOn = 1,
		VideoPowerStandBy,
		VideoPowerSuspend,
		VideoPowerOff,
		VideoPowerHibernate,
		VideoPowerShutdown,
		VideoPowerMaximum
	}
	internal struct VIDEO_POWER_MANAGEMENT
	{
		public ULONG Length;
		public ULONG DPMSVersion;
		public ULONG PowerState;
	}
	internal struct VIDEO_COLOR_LUT_DATA
	{
		public ULONG Length;
		public ULONG LutDataFormat;
		public unsafe fixed UCHAR LutData[1];
	}
	internal struct VIDEO_LUT_RGB256WORDS
	{
		public unsafe fixed USHORT Red[256];
		public unsafe fixed USHORT Green[256];
		public unsafe fixed USHORT Blue[256];
	}
	internal struct BANK_POSITION
	{
		public ULONG ReadBankPosition;
		public ULONG WriteBankPosition;
	}
	internal struct COORD
	{
		public SHORT X;
		public SHORT Y;
	}
	internal struct CHAR_INFO
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct CHAR_UNION
		{
			[FieldOffset(0)]
			public WCHAR UnicodeChar;
			[FieldOffset(0)]
			public CHAR AsciiChar;
		}
		public CHAR_UNION Char;
		public USHORT Attributes;
	}
	internal struct FSCNTL_SCREEN_INFO
	{
		public COORD Position;
		public COORD ScreenSize;
		public ULONG nNumberOfChars;
	}
	internal struct FONT_IMAGE_INFO
	{
		public COORD FontSize;
		public unsafe UCHAR* ImageBits;
	}
	internal struct CHAR_IMAGE_INFO
	{
		public CHAR_INFO CharInfo;
		public FONT_IMAGE_INFO FontImageInfo;
	}
	internal struct VGA_CHAR
	{
		public CHAR Char;
		public CHAR Attributes;
	}
	internal struct FSVIDEO_COPY_FRAME_BUFFER
	{
		public FSCNTL_SCREEN_INFO SrcScreen;
		public FSCNTL_SCREEN_INFO DestScreen;
	}
	internal struct FSVIDEO_WRITE_TO_FRAME_BUFFER
	{
		public unsafe CHAR_IMAGE_INFO* SrcBuffer;
		public FSCNTL_SCREEN_INFO DestScreen;
	}
	internal struct FSVIDEO_REVERSE_MOUSE_POINTER
	{
		public FSCNTL_SCREEN_INFO Screen;
		public ULONG dwType;
	}
	internal struct FSVIDEO_MODE_INFORMATION
	{
		public VIDEO_MODE_INFORMATION VideoMode;
		public VIDEO_MEMORY_INFORMATION VideoMemory;
	}
	internal struct FSVIDEO_SCREEN_INFORMATION
	{
		public COORD ScreenSize;
		public COORD FontSize;
	}
	internal struct FSVIDEO_CURSOR_POSITION
	{
		public VIDEO_CURSOR_POSITION Coord;
		public ULONG dwType;
	}
}