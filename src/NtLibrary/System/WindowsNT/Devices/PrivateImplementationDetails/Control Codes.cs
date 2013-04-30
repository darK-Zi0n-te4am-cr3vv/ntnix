namespace System.WindowsNT.Devices.PrivateImplementationDetails
{
	internal enum FileDevice : uint
	{
		BEEP = 0x00000001,
		CD_ROM = 0x00000002,
		CD_ROM_FILE_SYSTEM = 0x00000003,
		CONTROLLER = 0x00000004,
		DATALINK = 0x00000005,
		DFS = 0x00000006,
		DISK = 0x00000007,
		DISK_FILE_SYSTEM = 0x00000008,
		FILE_SYSTEM = 0x00000009,
		INPORT_PORT = 0x0000000a,
		KEYBOARD = 0x0000000b,
		MAILSLOT = 0x0000000c,
		MIDI_IN = 0x0000000d,
		MIDI_OUT = 0x0000000e,
		MOUSE = 0x0000000f,
		MULTI_UNC_PROVIDER = 0x00000010,
		NAMED_PIPE = 0x00000011,
		NETWORK = 0x00000012,
		NETWORK_BROWSER = 0x00000013,
		NETWORK_FILE_SYSTEM = 0x00000014,
		NULL = 0x00000015,
		PARALLEL_PORT = 0x00000016,
		PHYSICAL_NETCARD = 0x00000017,
		PRINTER = 0x00000018,
		SCANNER = 0x00000019,
		SERIAL_MOUSE_PORT = 0x0000001a,
		SERIAL_PORT = 0x0000001b,
		SCREEN = 0x0000001c,
		SOUND = 0x0000001d,
		STREAMS = 0x0000001e,
		TAPE = 0x0000001f,
		TAPE_FILE_SYSTEM = 0x00000020,
		TRANSPORT = 0x00000021,
		UNKNOWN = 0x00000022,
		VIDEO = 0x00000023,
		VIRTUAL_DISK = 0x00000024,
		WAVE_IN = 0x00000025,
		WAVE_OUT = 0x00000026,
		PORT_8042 = 0x00000027,
		NETWORK_REDIRECTOR = 0x00000028,
		BATTERY = 0x00000029,
		BUS_EXTENDER = 0x0000002a,
		MODEM = 0x0000002b,
		VDM = 0x0000002c,
		MASS_STORAGE = 0x0000002d,
		SMB = 0x0000002e,
		KS = 0x0000002f,
		CHANGER = 0x00000030,
		SMARTCARD = 0x00000031,
		ACPI = 0x00000032,
		DVD = 0x00000033,
		FULLSCREEN_VIDEO = 0x00000034,
		DFS_FILE_SYSTEM = 0x00000035,
		DFS_VOLUME = 0x00000036,
		SERENUM = 0x00000037,
		TERMSRV = 0x00000038,
		KSEC = 0x00000039,
		FIPS = 0x0000003A
	}

	internal enum Method : uint
	{
		BUFFERED = 0,
		IN_DIRECT = 1,
		OUT_DIRECT = 2,
		NEITHER = 3
	}

	internal enum FileAccess : uint
	{
		FILE_ANY_ACCESS = 0,
		FILE_SPECIAL_ACCESS = FileAccess.FILE_ANY_ACCESS,
		FILE_READ_ACCESS = 0x0001,    // file & pipe
		FILE_WRITE_ACCESS = 0x0002,    // file & pipe
	}

	namespace IOCTL
	{
		internal enum Storage : uint
		{
			IOCTL_STORAGE_BASE = FileDevice.MASS_STORAGE,
			CHECK_VERIFY = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0200 << 2) | (uint)Method.BUFFERED,
			CHECK_VERIFY2 = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0200 << 2) | (uint)Method.BUFFERED,
			MEDIA_REMOVAL = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0201 << 2) | (uint)Method.BUFFERED,
			EJECT_MEDIA = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0202 << 2) | (uint)Method.BUFFERED,
			LOAD_MEDIA = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0203 << 2) | (uint)Method.BUFFERED,
			LOAD_MEDIA2 = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0203 << 2) | (uint)Method.BUFFERED,
			RESERVE = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0204 << 2) | (uint)Method.BUFFERED,
			RELEASE = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0205 << 2) | (uint)Method.BUFFERED,
			FIND_NEW_DEVICES = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0206 << 2) | (uint)Method.BUFFERED,
			EJECTION_CONTROL = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0250 << 2) | (uint)Method.BUFFERED,
			MCN_CONTROL = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0251 << 2) | (uint)Method.BUFFERED,
			GET_MEDIA_TYPES = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0300 << 2) | (uint)Method.BUFFERED,
			GET_MEDIA_TYPES_EX = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0301 << 2) | (uint)Method.BUFFERED,
			GET_MEDIA_SERIAL_NUMBER = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0304 << 2) | (uint)Method.BUFFERED,
			GET_HOTPLUG_INFO = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0305 << 2) | (uint)Method.BUFFERED,
			SET_HOTPLUG_INFO = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0306 << 2) | (uint)Method.BUFFERED,
			RESET_BUS = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0400 << 2) | (uint)Method.BUFFERED,
			RESET_DEVICE = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0401 << 2) | (uint)Method.BUFFERED,
			BREAK_RESERVATION = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0405 << 2) | (uint)Method.BUFFERED,
			GET_DEVICE_NUMBER = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0420 << 2) | (uint)Method.BUFFERED,
			PREDICT_FAILURE = ((uint)IOCTL_STORAGE_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0440 << 2) | (uint)Method.BUFFERED,

		}

		internal enum Disk : uint
		{
			IOCTL_DISK_BASE = FileDevice.DISK,
			GET_DRIVE_GEOMETRY = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0000 << 2) | (uint)Method.BUFFERED,
			GET_PARTITION_INFO = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0001 << 2) | (uint)Method.BUFFERED,
			SET_PARTITION_INFO = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0002 << 2) | (uint)Method.BUFFERED,
			GET_DRIVE_LAYOUT = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0003 << 2) | (uint)Method.BUFFERED,
			SET_DRIVE_LAYOUT = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0004 << 2) | (uint)Method.BUFFERED,
			VERIFY = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0005 << 2) | (uint)Method.BUFFERED,
			FORMAT_TRACKS = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0006 << 2) | (uint)Method.BUFFERED,
			REASSIGN_BLOCKS = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0007 << 2) | (uint)Method.BUFFERED,
			PERFORMANCE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0008 << 2) | (uint)Method.BUFFERED,
			IS_WRITABLE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0009 << 2) | (uint)Method.BUFFERED,
			LOGGING = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x000a << 2) | (uint)Method.BUFFERED,
			FORMAT_TRACKS_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x000b << 2) | (uint)Method.BUFFERED,
			HISTOGRAM_STRUCTURE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x000c << 2) | (uint)Method.BUFFERED,
			HISTOGRAM_DATA = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x000d << 2) | (uint)Method.BUFFERED,
			HISTOGRAM_RESET = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x000e << 2) | (uint)Method.BUFFERED,
			REQUEST_STRUCTURE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x000f << 2) | (uint)Method.BUFFERED,
			REQUEST_DATA = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0010 << 2) | (uint)Method.BUFFERED,
			PERFORMANCE_OFF = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0018 << 2) | (uint)Method.BUFFERED,
			CONTROLLER_NUMBER = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0011 << 2) | (uint)Method.BUFFERED,
			GET_PARTITION_INFO_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0012 << 2) | (uint)Method.BUFFERED,
			SET_PARTITION_INFO_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0013 << 2) | (uint)Method.BUFFERED,
			GET_DRIVE_LAYOUT_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0014 << 2) | (uint)Method.BUFFERED,
			SET_DRIVE_LAYOUT_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0015 << 2) | (uint)Method.BUFFERED,
			CREATE_DISK = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0016 << 2) | (uint)Method.BUFFERED,
			GET_LENGTH_INFO = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0017 << 2) | (uint)Method.BUFFERED,
			GET_DRIVE_GEOMETRY_EX = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0028 << 2) | (uint)Method.BUFFERED,
			UPDATE_DRIVE_SIZE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0032 << 2) | (uint)Method.BUFFERED,
			GROW_PARTITION = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0034 << 2) | (uint)Method.BUFFERED,
			GET_CACHE_INFORMATION = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0035 << 2) | (uint)Method.BUFFERED,
			SET_CACHE_INFORMATION = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0036 << 2) | (uint)Method.BUFFERED,
			GET_WRITE_CACHE_STATE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0037 << 2) | (uint)Method.BUFFERED,
			DELETE_DRIVE_LAYOUT = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0040 << 2) | (uint)Method.BUFFERED,
			UPDATE_PROPERTIES = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0050 << 2) | (uint)Method.BUFFERED,
			FORMAT_DRIVE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x00f3 << 2) | (uint)Method.BUFFERED,
			SENSE_DEVICE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x00f8 << 2) | (uint)Method.BUFFERED,
			CHECK_VERIFY = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0200 << 2) | (uint)Method.BUFFERED,
			MEDIA_REMOVAL = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0201 << 2) | (uint)Method.BUFFERED,
			EJECT_MEDIA = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0202 << 2) | (uint)Method.BUFFERED,
			LOAD_MEDIA = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0203 << 2) | (uint)Method.BUFFERED,
			RESERVE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0204 << 2) | (uint)Method.BUFFERED,
			RELEASE = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0205 << 2) | (uint)Method.BUFFERED,
			FIND_NEW_DEVICES = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0206 << 2) | (uint)Method.BUFFERED,
			GET_MEDIA_TYPES = ((uint)IOCTL_DISK_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0x0300 << 2) | (uint)Method.BUFFERED,
		}

		internal enum Changer : uint
		{
			IOCTL_CHANGER_BASE = FileDevice.CHANGER,
			IOCTL_CHANGER_GET_PARAMETERS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0000 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_GET_STATUS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0001 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_GET_PRODUCT_DATA = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0002 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_SET_ACCESS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0004 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_GET_ELEMENT_STATUS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x0005 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_INITIALIZE_ELEMENT_STATUS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0006 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_SET_POSITION = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0007 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_EXCHANGE_MEDIUM = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0008 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_MOVE_MEDIUM = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x0009 << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_REINITIALIZE_TRANSPORT = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (0x000A << 2) | (uint)Method.BUFFERED,
			IOCTL_CHANGER_QUERY_VOLUME_TAGS = ((uint)IOCTL_CHANGER_BASE << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (0x000B << 2) | (uint)Method.BUFFERED,
		}

		internal enum VolumeBase : uint
		{
			IOCTL_VOLUME_BASE = 'V',
			IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS = ((uint)IOCTL_VOLUME_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0 << 2) | (uint)Method.BUFFERED,
			IOCTL_VOLUME_IS_CLUSTERED = ((uint)IOCTL_VOLUME_BASE << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (12 << 2) | (uint)Method.BUFFERED,
		}

		internal enum Video : uint
		{
			IOCTL_VIDEO_ENABLE_VDM = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x00 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_DISABLE_VDM = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x01 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_REGISTER_VDM = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x02 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_OUTPUT_DEVICE_POWER_STATE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x03 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_GET_OUTPUT_DEVICE_POWER_STATE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x04 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_MONITOR_DEVICE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x05 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_ENUM_MONITOR_PDO = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x06 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_INIT_WIN32K_CALLBACKS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x07 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_HANDLE_VIDEOPARAMETERS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x08 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_IS_VGA_DEVICE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x09 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_USE_DEVICE_IN_SESSION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x0a << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_PREPARE_FOR_EARECOVERY = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x0b << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SAVE_HARDWARE_STATE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x80 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_RESTORE_HARDWARE_STATE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x81 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_AVAIL_MODES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x100 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_NUM_AVAIL_MODES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x101 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_CURRENT_MODE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x102 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_CURRENT_MODE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x103 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_RESET_DEVICE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x104 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_LOAD_AND_SET_FONT = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x105 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_PALETTE_REGISTERS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x106 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_COLOR_REGISTERS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x107 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_ENABLE_CURSOR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x108 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_DISABLE_CURSOR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x109 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_CURSOR_ATTR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10a << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_CURSOR_ATTR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10b << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_CURSOR_POSITION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10c << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_CURSOR_POSITION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10d << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_ENABLE_POINTER = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10e << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_DISABLE_POINTER = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x10f << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_POINTER_ATTR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x110 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_POINTER_ATTR = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x111 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_POINTER_POSITION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x112 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_POINTER_POSITION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x113 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_POINTER_CAPABILITIES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x114 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_GET_BANK_SELECT_CODE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x115 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_MAP_VIDEO_MEMORY = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x116 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_UNMAP_VIDEO_MEMORY = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x117 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_PUBLIC_ACCESS_RANGES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x118 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_FREE_PUBLIC_ACCESS_RANGES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x119 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_COLOR_CAPABILITIES = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11a << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_POWER_MANAGEMENT = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11b << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_GET_POWER_MANAGEMENT = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11c << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SHARE_VIDEO_MEMORY = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11d << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_UNSHARE_VIDEO_MEMORY = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11e << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_COLOR_LUT_DATA = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x11f << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_GET_CHILD_STATE = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x120 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_VALIDATE_CHILD_STATE_CONFIGURATION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x121 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_CHILD_STATE_CONFIGURATION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x122 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SWITCH_DUALVIEW = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x123 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_BANK_POSITION = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x124 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_SUPPORTED_BRIGHTNESS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x125 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_QUERY_DISPLAY_BRIGHTNESS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x126 << 2) | (uint)Method.BUFFERED,
			IOCTL_VIDEO_SET_DISPLAY_BRIGHTNESS = ((uint)(FileDevice.VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x127 << 2) | (uint)Method.BUFFERED,
		}

		internal enum FSVideo : uint
		{
			IOCTL_FSVIDEO_COPY_FRAME_BUFFER = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x200 << 2) | (uint)Method.BUFFERED,
			IOCTL_FSVIDEO_WRITE_TO_FRAME_BUFFER = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x201 << 2) | (uint)Method.BUFFERED,
			IOCTL_FSVIDEO_REVERSE_MOUSE_POINTER = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x202 << 2) | (uint)Method.BUFFERED,
			IOCTL_FSVIDEO_SET_CURRENT_MODE = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x203 << 2) | (uint)Method.BUFFERED,
			IOCTL_FSVIDEO_SET_SCREEN_INFORMATION = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x204 << 2) | (uint)Method.BUFFERED,
			IOCTL_FSVIDEO_SET_CURSOR_POSITION = ((uint)(FileDevice.FULLSCREEN_VIDEO << 16) | ((uint)FileAccess.FILE_ANY_ACCESS) << 14) | (0x205 << 2) | (uint)Method.BUFFERED,
		}
	}

	public enum FSCTL : uint
	{
		REQUEST_OPLOCK_LEVEL_1 = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (0 << 2) | (uint)Method.BUFFERED,
		REQUEST_OPLOCK_LEVEL_2 = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (1 << 2) | (uint)Method.BUFFERED,
		REQUEST_BATCH_OPLOCK = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (2 << 2) | (uint)Method.BUFFERED,
		OPLOCK_BREAK_ACKNOWLEDGE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (3 << 2) | (uint)Method.BUFFERED,
		OPBATCH_ACK_CLOSE_PENDING = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (4 << 2) | (uint)Method.BUFFERED,
		OPLOCK_BREAK_NOTIFY = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (5 << 2) | (uint)Method.BUFFERED,
		LOCK_VOLUME = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (6 << 2) | (uint)Method.BUFFERED,
		UNLOCK_VOLUME = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (7 << 2) | (uint)Method.BUFFERED,
		DISMOUNT_VOLUME = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (8 << 2) | (uint)Method.BUFFERED,
		IS_VOLUME_MOUNTED = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (10 << 2) | (uint)Method.BUFFERED,
		IS_PATHNAME_VALID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (11 << 2) | (uint)Method.BUFFERED, // PATHNAME_BUFFER,
		MARK_VOLUME_DIRTY = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (12 << 2) | (uint)Method.BUFFERED,
		QUERY_RETRIEVAL_POINTERS = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (14 << 2) | (uint)Method.NEITHER,
		GET_COMPRESSION = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (15 << 2) | (uint)Method.BUFFERED,
		SET_COMPRESSION = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (16 << 2) | (uint)Method.BUFFERED,
		MARK_AS_SYSTEM_HIVE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (19 << 2) | (uint)Method.NEITHER,
		OPLOCK_BREAK_ACK_NO_2 = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (20 << 2) | (uint)Method.BUFFERED,
		INVALIDATE_VOLUMES = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (21 << 2) | (uint)Method.BUFFERED,
		QUERY_FAT_BPB = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (22 << 2) | (uint)Method.BUFFERED, // FSCTL_QUERY_FAT_BPB_BUFFER
		REQUEST_FILTER_OPLOCK = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (23 << 2) | (uint)Method.BUFFERED,
		FILESYSTEM_GET_STATISTICS = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (24 << 2) | (uint)Method.BUFFERED, // FILESYSTEM_STATISTICS
		GET_NTFS_VOLUME_DATA = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (25 << 2) | (uint)Method.BUFFERED, // NTFS_VOLUME_DATA_BUFFER
		GET_NTFS_FILE_RECORD = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (26 << 2) | (uint)Method.BUFFERED, // NTFS_FILE_RECORD_INPUT_BUFFER, NTFS_FILE_RECORD_OUTPUT_BUFFER
		GET_VOLUME_BITMAP = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (27 << 2) | (uint)Method.NEITHER, // STARTING_LCN_INPUT_BUFFER, VOLUME_BITMAP_BUFFER
		GET_RETRIEVAL_POINTERS = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (28 << 2) | (uint)Method.NEITHER, // STARTING_VCN_INPUT_BUFFER, RETRIEVAL_POINTERS_BUFFER
		MOVE_FILE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (29 << 2) | (uint)Method.BUFFERED, // MOVE_FILE_DATA,
		IS_VOLUME_DIRTY = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (30 << 2) | (uint)Method.BUFFERED,
		ALLOW_EXTENDED_DASD_IO = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (32 << 2) | (uint)Method.NEITHER,
		FIND_FILES_BY_SID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (35 << 2) | (uint)Method.NEITHER,
		SET_OBJECT_ID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (38 << 2) | (uint)Method.BUFFERED, // FileAccess.FILE_OBJECTID_BUFFER
		GET_OBJECT_ID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (39 << 2) | (uint)Method.BUFFERED, // FileAccess.FILE_OBJECTID_BUFFER
		DELETE_OBJECT_ID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (40 << 2) | (uint)Method.BUFFERED,
		SET_REPARSE_POINT = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (41 << 2) | (uint)Method.BUFFERED, // REPARSE_DATA_BUFFER,
		GET_REPARSE_POINT = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (42 << 2) | (uint)Method.BUFFERED, // REPARSE_DATA_BUFFER
		DELETE_REPARSE_POINT = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (43 << 2) | (uint)Method.BUFFERED, // REPARSE_DATA_BUFFER,
		ENUM_USN_DATA = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (44 << 2) | (uint)Method.NEITHER, // MFT_ENUM_DATA,
		SECURITY_ID_CHECK = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (45 << 2) | (uint)Method.NEITHER,  // BULK_SECURITY_TEST_DATA,
		READ_USN_JOURNAL = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (46 << 2) | (uint)Method.NEITHER, // READ_USN_JOURNAL_DATA, USN
		SET_OBJECT_ID_EXTENDED = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (47 << 2) | (uint)Method.BUFFERED,
		CREATE_OR_GET_OBJECT_ID = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (48 << 2) | (uint)Method.BUFFERED, // FileAccess.FILE_OBJECTID_BUFFER
		SET_SPARSE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (49 << 2) | (uint)Method.BUFFERED,
		SET_ZERO_DATA = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_WRITE_ACCESS << 14) | (50 << 2) | (uint)Method.BUFFERED, // FileAccess.FILE_ZERO_DATA_INFORMATION,
		QUERY_ALLOCATED_RANGES = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (51 << 2) | (uint)Method.NEITHER, // FileAccess.FILE_ALLOCATED_RANGE_BUFFER, FileAccess.FILE_ALLOCATED_RANGE_BUFFER
		SET_ENCRYPTION = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (53 << 2) | (uint)Method.NEITHER, // ENCRYPTION_BUFFER, DECRYPTION_STATUS_BUFFER
		ENCRYPTION_FSCTL_IO = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (54 << 2) | (uint)Method.NEITHER,
		WRITE_RAW_ENCRYPTED = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (55 << 2) | (uint)Method.NEITHER, // ENCRYPTED_DATA_INFO,
		READ_RAW_ENCRYPTED = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (56 << 2) | (uint)Method.NEITHER, // REQUEST_RAW_ENCRYPTED_DATA, ENCRYPTED_DATA_INFO
		CREATE_USN_JOURNAL = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (57 << 2) | (uint)Method.NEITHER, // CREATE_USN_JOURNAL_DATA,
		READ_FILE_USN_DATA = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (58 << 2) | (uint)Method.NEITHER, // Read the Usn Record for a file
		WRITE_USN_CLOSE_RECORD = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (59 << 2) | (uint)Method.NEITHER, // Generate Close Usn Record
		EXTEND_VOLUME = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (60 << 2) | (uint)Method.BUFFERED,
		QUERY_USN_JOURNAL = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (61 << 2) | (uint)Method.BUFFERED,
		DELETE_USN_JOURNAL = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (62 << 2) | (uint)Method.BUFFERED,
		MARK_HANDLE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (63 << 2) | (uint)Method.BUFFERED,
		SIS_COPYFILE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (64 << 2) | (uint)Method.BUFFERED,
		SIS_LINK_FILES = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (65 << 2) | (uint)Method.BUFFERED,
		HSM_MSG = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (66 << 2) | (uint)Method.BUFFERED,
		HSM_DATA = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS | (uint)FileAccess.FILE_WRITE_ACCESS << 14) | (68 << 2) | (uint)Method.NEITHER,
		RECALL_FILE = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_ANY_ACCESS << 14) | (69 << 2) | (uint)Method.NEITHER,
		READ_FROM_PLEX = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_READ_ACCESS << 14) | (71 << 2) | (uint)Method.OUT_DIRECT,
		FILE_PREFETCH = ((uint)FileDevice.FILE_SYSTEM << 16) | ((uint)FileAccess.FILE_SPECIAL_ACCESS << 14) | (72 << 2) | (uint)Method.BUFFERED,

	}
}