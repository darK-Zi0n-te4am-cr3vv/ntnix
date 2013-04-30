using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Processes
{
	internal struct ProcessParameters
	{
		public uint AllocationSize;
		public uint Size;
		public uint Flags;
		public uint DebugFlags;
		public IntPtr hConsole;
		public uint ProcessGroup;
		public IntPtr hStdInput;
		public IntPtr hStdOutput;
		public IntPtr hStdError;
		public UnicodeString CurrentDirectoryName;
		public IntPtr CurrentDirectoryHandle;
		public UnicodeString DllPath;
		public UnicodeString ImagePathName;
		public UnicodeString CommandLine;
		public unsafe char* Environment;
		public uint dwX;
		public uint dwY;
		public uint dwXSize;
		public uint dwYSize;
		public uint dwXCountChars;
		public uint dwYCountChars;
		public uint dwFillAttribute;
		public StartFlags dwFlags;
		public ShowWindow wShowWindow;
		public UnicodeString WindowTitle;
		public UnicodeString DesktopInfo;
		public UnicodeString ShellInfo;
		public UnicodeString RuntimeInfo;
	}

	[System.Flags]
	public enum StartFlags
	{
		USESHOWWINDOW = 0x00000001,
		USESIZE = 0x00000002,
		USEPOSITION = 0x00000004,
		USECOUNTCHARS = 0x00000008,
		USEFILLATTRIBUTE = 0x00000010,
		RUNFULLSCREEN = 0x00000020,  // ignored for non-x86 platforms
		FORCEONFEEDBACK = 0x00000040,
		FORCEOFFFEEDBACK = 0x00000080,
		USESTDHANDLES = 0x00000100,
	}

	public enum ShowWindow
	{
		SW_HIDE = 0,
		SW_SHOWNORMAL = 1,
		SW_NORMAL = 1,
		SW_SHOWMINIMIZED = 2,
		SW_SHOWMAXIMIZED = 3,
		SW_MAXIMIZE = 3,
		SW_SHOWNOACTIVATE = 4,
		SW_SHOW = 5,
		SW_MINIMIZE = 6,
		SW_SHOWMINNOACTIVE = 7,
		SW_SHOWNA = 8,
		SW_RESTORE = 9,
		SW_SHOWDEFAULT = 10,
		SW_FORCEMINIMIZE = 11,
		SW_MAX = 11,
	}

	[System.Flags()]
	public enum ProcessAccessMask : uint
	{
		MaximumAllowed = (0x02000000),
		GenericRead = (0x80000000),
		GenericWrite = (0x40000000),
		GenericExecute = (0x20000000),
		GenericAll = (0x10000000),
		AllAccess = (AccessMask.StandardRightsRequired | AccessMask.Synchronize | 0xFFF)
	}

	public struct UserStack
	{
		public System.IntPtr FixedStackBase;
		public System.IntPtr FixedStackLimit;
		public System.IntPtr ExpandableStackBase;
		public System.IntPtr ExpandableStackLimit;
		public System.IntPtr ExpandableStackBottom;
	}

	public enum ProcessCreateFlags : uint
	{
		DEBUG_PROCESS = 0x00000001,
		DEBUG_ONLY_THIS_PROCESS = 0x00000002,
		CREATE_SUSPENDED = 0x00000004,
		DETACHED_PROCESS = 0x00000008,
		CREATE_NEW_CONSOLE = 0x00000010,
		NORMAL_PRIORITY_CLASS = 0x00000020,
		IDLE_PRIORITY_CLASS = 0x00000040,
		HIGH_PRIORITY_CLASS = 0x00000080,
		REALTIME_PRIORITY_CLASS = 0x00000100,
		CREATE_NEW_PROCESS_GROUP = 0x00000200,
		CREATE_UNICODE_ENVIRONMENT = 0x00000400,
		CREATE_SEPARATE_WOW_VDM = 0x00000800,
		CREATE_SHARED_WOW_VDM = 0x00001000,
		CREATE_FORCEDOS = 0x00002000,
		BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
		ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,
		STACK_SIZE_PARAM_IS_A_RESERVATION = 0x00010000,
		INHERIT_CALLER_PRIORITY = 0x00020000,
		CREATE_PROTECTED_PROCESS = 0x00040000,
		EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
		PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000,
		PROCESS_MODE_BACKGROUND_END = 0x00200000,
		CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
		CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
		CREATE_DEFAULT_ERROR_MODE = 0x04000000,
		CREATE_NO_WINDOW = 0x08000000,
		PROFILE_USER = 0x10000000,
		PROFILE_KERNEL = 0x20000000,
		PROFILE_SERVER = 0x40000000,
		CREATE_IGNORE_SYSTEM_DEFAULT = 0x80000000
	}

	public struct ClientID
	{
		public System.IntPtr UniqueProcess;
		public System.IntPtr UniqueThread;
	}

	internal enum ProcessInfoClass
	{
		ProcessBasicInformation,
		ProcessQuotaLimits,
		ProcessIoCounters,
		ProcessVmCounters,
		ProcessTimes,
		ProcessBasePriority,
		ProcessRaisePriority,
		ProcessDebugPort,
		ProcessExceptionPort,
		ProcessAccessToken,
		ProcessLdtInformation,
		ProcessLdtSize,
		ProcessDefaultHardErrorMode,
		ProcessIoPortHandlers,          // Note: this is kernel mode only
		ProcessPooledUsageAndLimits,
		ProcessWorkingSetWatch,
		ProcessUserModeIOPL,
		ProcessEnableAlignmentFaultFixup,
		ProcessPriorityClass,
		ProcessWx86Information,
		ProcessHandleCount,
		ProcessAffinityMask,
		ProcessPriorityBoost,
		ProcessDeviceMap,
		ProcessSessionInformation,
		ProcessForegroundInformation,
		ProcessWow64Information,
		ProcessImageFileName,
		ProcessLUIDDeviceMapsEnabled,
		ProcessBreakOnTermination,
		ProcessDebugObjectHandle,
		ProcessDebugFlags,
		ProcessHandleTracing,
		ProcessUnusedSpare1,
		ProcessExecuteFlags,
		ProcessResourceManagement,
		ProcessCookie,
		ProcessImageInformation,
		MaxProcessInfoClass             // MaxProcessInfoClass should always be the last enum
	}

	internal struct ProcessBasicInformation
	{
		public NtStatus ExitStatus;
		public unsafe PEB* PebBaseAddress;
		public System.IntPtr AffinityMask;
		public uint BasePriority;
		public System.IntPtr UniqueProcessId;
		public System.IntPtr InheritedFromUniqueProcessId;
	}

	internal struct KernelUserTimes
	{
		public long CreateTime;
		public long ExitTime;
		public long KernelTime;
		public long UserTime;
	}

	[StructLayout(LayoutKind.Explicit)]
	internal struct ProcessDeviceMapInformation
	{
		[FieldOffset(0)]
		public System.IntPtr DirectoryHandleSet;
		public struct AnonymousStruct
		{
			public uint DriveMap;
			public unsafe fixed byte DriveType[32];
		}
		[FieldOffset(0)]
		public AnonymousStruct Query;
	}

	internal struct ProcessDeviceMapInformationEx
	{
		public ProcessDeviceMapInformation Information;
		public uint Flags;    // specifies that the query type
	}

	internal struct ProcessSessionInformation
	{
		public uint SessionId;
	}

	internal struct ProcessHandleTracingEnable
	{
		public uint Flags;
	}

	internal struct ProcessHandleTracingEntry
	{
		public System.IntPtr Handle;
		public ClientID ClientId;
		public uint Type;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public System.IntPtr[] Stacks;
	}

	internal struct ProcessHandleTracingQuery
	{
		public System.IntPtr Handle;
		public uint TotalTraces;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public ProcessHandleTracingEntry[] HandleTrace;
	}

	internal struct ProcessQuotaLimits
	{
		public System.IntPtr PagedPoolLimit;
		public System.IntPtr NonPagedPoolLimit;
		public System.IntPtr MinimumWorkingSetSize;
		public System.IntPtr MaximumWorkingSetSize;
		public System.IntPtr PagefileLimit;
		public long TimeLimit;
	}

	public struct ProcessPooledUsageAndLimits
	{
		public System.IntPtr PeakPagedPoolUsage;
		public System.IntPtr PagedPoolUsage;
		public System.IntPtr PagedPoolLimit;
		public System.IntPtr PeakNonPagedPoolUsage;
		public System.IntPtr NonPagedPoolUsage;
		public System.IntPtr NonPagedPoolLimit;
		public System.IntPtr PeakPagefileUsage;
		public System.IntPtr PagefileUsage;
		public System.IntPtr PagefileLimit;
	}

	public struct ProcessAccessToken
	{

		//
		// Handle to Primary token to assign to the process.
		// TOKEN_ASSIGN_PRIMARY access to this token is needed.
		//

		public System.IntPtr Token;

		//
		// Handle to the initial thread of the process.
		// A process's access token can only be changed if the process has
		// no threads or one thread.  If the process has no threads, this
		// field must be set to NULL.  Otherwise, it must contain a handle
		// open to the process's only thread.  THREAD_QUERY_INFORMATION access
		// is needed via this handle.

		public System.IntPtr Thread;

	}

	internal struct PEB
	{
		public bool InheritedAddressSpace;
		public bool ReadImageFileExecOptions;
		public bool BeingDebugged;
		public bool Spare;
		public System.IntPtr Mutant;
		public System.IntPtr ImageBaseAddress;
		public unsafe PEB_LDR_DATA* LoaderData;
		public unsafe RTL_USER_PROCESS_PARAMETERS* ProcessParameters;
		public System.IntPtr SubSystemData;
		public System.IntPtr ProcessHeap;
		public System.IntPtr FastPebLock;
		public System.IntPtr FastPebLockRoutine;
		public System.IntPtr FastPebUnlockRoutine;
		public uint EnvironmentUpdateCount;
		public unsafe System.IntPtr* KernelCallbackTable;
		public System.IntPtr EventLogSection;
		public System.IntPtr EventLog;
		public unsafe PEB_FREE_BLOCK* FreeList;
		public uint TlsExpansionCounter;
		public System.IntPtr TlsBitmap;
		public unsafe fixed uint TlsBitmapBits[0x2];
		public System.IntPtr ReadOnlySharedMemoryBase;
		public System.IntPtr ReadOnlySharedMemoryHeap;
		public unsafe System.IntPtr* ReadOnlyStaticServerData;
		public System.IntPtr AnsiCodePageData;
		public System.IntPtr OemCodePageData;
		public System.IntPtr UnicodeCaseTableData;
		public uint NumberOfProcessors;
		public uint NtGlobalFlag;
		public unsafe fixed byte Spare2[0x4];
		public long CriticalSectionTimeout;
		public uint HeapSegmentReserve;
		public uint HeapSegmentCommit;
		public uint HeapDeCommitTotalFreeThreshold;
		public uint HeapDeCommitFreeBlockThreshold;
		public uint NumberOfHeaps;
		public uint MaximumNumberOfHeaps;
		public unsafe System.IntPtr** ProcessHeaps;
		public System.IntPtr GdiSharedHandleTable;
		public System.IntPtr ProcessStarterHelper;
		public System.IntPtr GdiDCAttributeList;
		public System.IntPtr LoaderLock;
		public uint OSMajorVersion;
		public uint OSMinorVersion;
		public uint OSBuildNumber;
		public uint OSPlatformId;
		public uint ImageSubSystem;
		public uint ImageSubSystemMajorVersion;
		public uint ImageSubSystemMinorVersion;
		public unsafe fixed uint GdiHandleBuffer[0x22];
		public uint PostProcessInitRoutine;
		public uint TlsExpansionBitmap;
		public unsafe fixed byte TlsExpansionBitmapBits[0x80];
		public uint SessionId;

	}

	internal struct RTL_USER_PROCESS_PARAMETERS
	{
		public uint MaximumLength;
		public uint Length;
		public uint Flags;
		public uint DebugFlags;
		public System.IntPtr ConsoleHandle;
		public uint ConsoleFlags;
		public System.IntPtr StdInputHandle;
		public System.IntPtr StdOutputHandle;
		public System.IntPtr StdErrorHandle;
		public UnicodeString CurrentDirectoryPath;
		public System.IntPtr CurrentDirectoryHandle;
		public UnicodeString DllPath;
		public UnicodeString ImagePathName;
		public UnicodeString CommandLine;
		public System.IntPtr Environment;
		public uint StartingPositionLeft;
		public uint StartingPositionTop;
		public uint Width;
		public uint Height;
		public uint CharWidth;
		public uint CharHeight;
		public uint ConsoleTextAttributes;
		public uint WindowFlags;
		public uint ShowWindowFlags;
		public UnicodeString WindowTitle;
		public UnicodeString DesktopName;
		public UnicodeString ShellInfo;
		public UnicodeString RuntimeData;
		public RTL_DRIVE_LETTER_CURDIR FirstDLCurrentDirectory;
	}

	internal struct RTL_DRIVE_LETTER_CURDIR
	{
		public ushort Flags;
		public ushort Length;
		public uint TimeStamp;
		public UnicodeString DosPath;
	}

	internal struct PEB_LDR_DATA
	{
		public uint Length;
		public bool Initialized;
		public System.IntPtr SsHandle;
		public unsafe LDR_MODULE* InLoadOrderModuleList;
		public unsafe LDR_MODULE* InMemoryOrderModuleList;
		public unsafe LDR_MODULE* InInitializationOrderModuleList;
	}

	internal struct LDR_MODULE
	{

		public unsafe LDR_MODULE* InLoadOrderModuleList;
		public unsafe LDR_MODULE* InMemoryOrderModuleList;
		public unsafe LDR_MODULE* InInitializationOrderModuleList;
		public System.IntPtr BaseAddress;
		public System.IntPtr EntryPoint;
		public uint SizeOfImage;
		public UnicodeString FullDllName;
		public UnicodeString BaseDllName;
		public uint Flags;
		public short LoadCount;
		public short TlsIndex;
		public System.IntPtr /*LIST_ENTRY*/ HashTableEntry;
		public uint TimeDateStamp;

	}

	internal struct PEB_FREE_BLOCK
	{
		public unsafe PEB_FREE_BLOCK* Next;
		public uint Size;
	}

	internal delegate void PEBLOCKROUTINE(System.IntPtr PebLock);

	[System.Diagnostics.DebuggerDisplay("Name = {Name}")]
	public class ProcessInformationSnapshot
	{
		internal ProcessInformationSnapshot(SystemProcessesInformation processInfo)
		{
			this.BasePriority = processInfo.BasePriority;
			this.CreateTime = System.DateTime.FromFileTime(processInfo.CreateTime);
			this.HandleCount = (int)processInfo.HandleCount;
			this.InheritedFromProcessId = unchecked((int)processInfo.InheritedFromProcessId);
			this.IoCounters = processInfo.IoCounters;
			this.KernelTime = System.DateTime.FromFileTime(processInfo.KernelTime);
			this.ProcessId = unchecked((int)processInfo.ProcessId);
			this.Name = processInfo.ProcessName.ToString();
			this.SessionId = unchecked((int)processInfo.SessionId);
			this.UserTime = System.DateTime.FromFileTime(processInfo.UserTime);
			this.VmCounters = processInfo.VmCounters;
			this.Threads = System.Array.ConvertAll(processInfo.Threads, ti => new Threading.ThreadInformationSnapshot(ti));
		}

		public System.DateTime CreateTime { get; private set; }
		public System.DateTime UserTime { get; private set; }
		public System.DateTime KernelTime { get; private set; }
		public string Name { get; private set; }
		public int BasePriority { get; private set; }
		public int ProcessId { get; private set; }
		public int InheritedFromProcessId { get; private set; }
		public int HandleCount { get; private set; }
		public int SessionId { get; private set; }
		public VMCounters VmCounters { get; private set; }
		public IOCounters IoCounters { get; private set; }
		public Threading.ThreadInformationSnapshot[] Threads { get; private set; }

		public override string ToString()
		{
			return string.Format("{{Name = \"{0}\", ID = {1}}}", this.Name, this.ProcessId);
		}
	}
}