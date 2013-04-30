using System.Runtime.InteropServices;
using System.WindowsNT.Security;
namespace System.WindowsNT
{
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Size = sizeof(int))]
	public struct BigEndianInt : System.IEquatable<BigEndianInt>, System.IComparable<BigEndianInt>
	{
		private unsafe fixed byte _bytes[sizeof(int)];
		public byte this[int index]
		{
			get
			{ unsafe { fixed (byte* bp = this._bytes) { return bp[index]; } } }
			set
			{ unsafe { fixed (byte* bp = this._bytes) { bp[index] = value; } } }
		}
		public static explicit operator int(BigEndianInt integer)
		{
			int result = new int();
			for (int i = 0; i < sizeof(int); ++i)
			{ unsafe { ((byte*)&result)[i] = integer._bytes[sizeof(int) - i]; } }
			return result;
		}
		public static explicit operator BigEndianInt(int integer)
		{
			BigEndianInt result = new BigEndianInt();
			unsafe
			{
				byte* ip = (byte*)&integer;
				for (int i = 0; i < sizeof(int); ++i)
				{ unsafe { result._bytes[i] = ip[sizeof(int) - i]; } }
			}
			return result;

		}
		public override bool Equals(object obj)
		{ return obj is BigEndianInt && this.Equals((BigEndianInt)obj); }
		public bool Equals(BigEndianInt other)
		{
			for (int i = 0; i < sizeof(int); ++i)
			{ unsafe { fixed (byte* bp = this._bytes) { if (bp[i] != other._bytes[i]) return false; } } }
			return true;
		}
		public override int GetHashCode()
		{ unsafe { fixed (byte* ip = this._bytes) { return (*(int*)ip).GetHashCode(); } } }
		public override string ToString() { return ((int)this).ToString(); }
		public static bool operator ==(BigEndianInt left, BigEndianInt right) { return left.Equals(right); }
		public static bool operator !=(BigEndianInt left, BigEndianInt right) { return !left.Equals(right); }
		public int CompareTo(BigEndianInt other) { return ((int)this).CompareTo((int)other); }
	}

	[System.Flags()]
	public enum AccessMask : uint
	{
		/// <summary>The caller can delete the object.</summary>
		Delete = (0x00010000),
		/// <summary>The caller can read the access control list (ACL) and ownership information for the file.</summary>
		ReadControl = (0x00020000),
		/// <summary>The caller can change the discretionary access control list (DACL) information for the object.</summary>
		WriteDAC = (0x00040000),
		/// <summary>The caller can change the ownership information for the file.</summary>
		WriteOwner = (0x00080000),
		/// <summary>The caller can perform a wait operation on the object.</summary>
		Synchronize = (0x00100000),
		/// <summary>Standard specific rights that correspond to <see cref="GenericAll"/>. This includes <see cref="Delete"/>, but not <see cref="Synchronize"/>.</summary>
		StandardRightsRequired = (0x000F0000),
		/// <summary>Standard specific rights that correspond to <see cref="GenericRead"/></summary>
		StandardRightsRead = (ReadControl),
		/// <summary>Standard specific rights that correspond to <see cref="GenericWrite"/></summary>
		StandardRightsWrite = (ReadControl),
		/// <summary>Standard specific rights that correspond to <see cref="GenericExecute"/></summary>
		StandardRightsExecute = (ReadControl),
		/// <summary>All standard access rights.</summary>
		StandardRightsAll = (0x001F0000),
		SpecificRightsAll = (0x0000FFFF),
		AccessSystemSecurity = (0x01000000),
		MaximumAllowed = (0x02000000),
		/// <summary>The caller can perform normal read operations on the object.</summary>
		GenericRead = (0x80000000),
		/// <summary>The caller can perform normal write operations on the object.</summary>
		GenericWrite = (0x40000000),
		/// <summary>The caller can execute the object. (Note this generally only makes sense for certain kinds of objects, such as file objects and section objects.)</summary>
		GenericExecute = (0x20000000),
		/// <summary>The caller can perform all normal operations on the object.</summary>
		GenericAll = (0x10000000)
	}

	[System.Flags()]
	public enum DuplicationOptions : uint
	{
		CLOSE_SOURCE = 0x00000001,
		SAME_ACCESS = 0x00000002,
		SAME_ATTRIBUTES = 0x00000004
	}

	internal struct UnicodeString : System.IDisposable
	{
		/// <summary>Length of the <see cref="System.String"/>, in <see cref="byte"/>s.</summary>
		public short ByteLength;
		/// <summary>Maximum length of the <see cref="System.String"/>, in <see cref="byte"/>s.</summary>
		public short MaximumLength;
		public unsafe char* String;

		private readonly bool shouldDispose;

		public unsafe UnicodeString(short length, short maxLength, char* @string, bool shouldDispose)
		{
			if (@string == null)
				throw new System.ArgumentNullException("string");
			this.ByteLength = length;
			this.MaximumLength = maxLength;
			this.String = @string;
			this.shouldDispose = shouldDispose;
		}

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		internal static extern void RtlInitUnicodeString([Out] out UnicodeString DestinationString, [In, MarshalAs(UnmanagedType.LPWStr)] string SourceString);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		internal static extern void RtlFreeUnicodeString([In] ref UnicodeString UnicodeString);

		public override string ToString()
		{
			unsafe
			{
				return this.String != null ? Marshal.PtrToStringUni(new System.IntPtr(this.String), this.ByteLength / 2) : null;
			}
		}

		public static implicit operator UnicodeString(string @string)
		{
			unsafe
			{
				return new UnicodeString(
					(short)(@string.Length * sizeof(char)),
					(short)(@string.Length * sizeof(char)),
					(char*)System.Runtime.InteropServices.Marshal.StringToHGlobalUni(@string),
					true);
			}
		}

		public static implicit operator string(UnicodeString wString) { return wString.ToString(); }

		public void Dispose()
		{
			if (this.shouldDispose)
			{
				unsafe
				{
					if (this.String != null)
					{
						System.Runtime.InteropServices.Marshal.FreeHGlobal(new System.IntPtr(this.String));
						this.String = null;
						this.ByteLength = 0;
						this.MaximumLength = 0;
					}
				}
			}
		}
	}

	internal struct ANSIString
	{
		public ANSIString(string str)
		{
			this.ByteLength = (ushort)(str.Length * sizeof(char));
			this.MaximumLength = this.ByteLength;
			this.String = str;
		}

		public readonly ushort ByteLength;
		public readonly ushort MaximumLength;
		[MarshalAs(UnmanagedType.LPStr)]
		public readonly string String;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal class PUnicodeString
	{
		public PUnicodeString(string str)
		{
			this.String = str;
		}

		public short ByteLength;
		public short MaximumLength;
		[MarshalAs(UnmanagedType.LPWStr)]
		private string _string;
		public string String
		{
			get { return this._string; }
			set
			{
				this._string = value;
				this.ByteLength = (short)(this._string.Length * sizeof(char));
				this.MaximumLength = this.ByteLength;
			}
		}

		public static explicit operator PUnicodeString(string str) { return new PUnicodeString(str); }
	}

	internal struct ObjectAttributes
	{
		public unsafe ObjectAttributes(System.IntPtr rootDirectory, UnicodeString* objectName, AllowedObjectAttributes attributes, System.Security.AccessControl.RawSecurityDescriptor securityDescriptor, SecurityQualityOfService* securityQualityOfService)
			: this()
		{
			this.Length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(this);
			this.RootDirectory = rootDirectory;
			this.ObjectName = objectName;
			this.Attributes = attributes;
			this.SecurityDescriptor = securityDescriptor;
			this.SecurityQualityOfService = securityQualityOfService;
		}

		public uint Length;
		public System.IntPtr RootDirectory;
		public unsafe UnicodeString* ObjectName;
		public AllowedObjectAttributes Attributes;
		public System.Security.AccessControl.RawSecurityDescriptor SecurityDescriptor; // Points to type SECURITY_DESCRIPTOR
		public unsafe SecurityQualityOfService* SecurityQualityOfService; // Points to type SECURITY_QUALITY_OF_SERVICE
	}

	[StructLayout(LayoutKind.Sequential)]
	internal class PObjectAttributes
	{
		public unsafe PObjectAttributes(System.IntPtr rootDirectory, PUnicodeString objectName, AllowedObjectAttributes attributes, System.Security.AccessControl.RawSecurityDescriptor securityDescriptor, SecurityQualityOfService* securityQualityOfService)
		{
			this.Length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(this);
			this.RootDirectory = rootDirectory;
			this.ObjectName = objectName;
			this.Attributes = attributes;
			this.SecurityDescriptor = securityDescriptor;
			this.SecurityQualityOfService = securityQualityOfService;
		}

		public uint Length;
		public System.IntPtr RootDirectory;
		public PUnicodeString ObjectName;
		public AllowedObjectAttributes Attributes;
		public System.Security.AccessControl.RawSecurityDescriptor SecurityDescriptor;
		public unsafe SecurityQualityOfService* SecurityQualityOfService;
	}

	[System.Flags()]
	public enum AllowedObjectAttributes : uint
	{
		None = 0x00000000,
		/// <summary>This handle can be inherited by child processes of the current process.</summary>
		Inherit = 0x00000002,
		/// <summary>This flag only applies to objects that are named within the object manager. By default, such objects are deleted when all open handles to them are closed. If this flag is specified, the object is not deleted when all open handles are closed. Drivers can use the ZwMakeTemporaryObject routine to make a permanent object non-permanent.</summary>
		Permanent = 0x00000010,
		/// <summary>
		/// <para>If this flag is set and the <see cref="ObjectAttributes"/> structure is passed to a routine that creates an object, the object can be accessed exclusively. That is, once a process opens such a handle to the object, no other processes can open handles to this object.</para>
		/// <para>If this flag is set and the <see cref="ObjectAttributes"/> structure is passed to a routine that creates an object handle, the caller is requesting exclusive access to the object for the process context that the handle was created in. This request can be granted only if the <see cref="Exclusive"/> flag was set when the object was created.</para>
		/// </summary>
		Exclusive = 0x00000020,
		/// <summary>If this flag is specified, a case-insensitive comparison is used when matching the name pointed to by the <see cref="ObjectAttributes.ObjectName"/> member against the names of existing objects. Otherwise, object names are compared using the default system settings.</summary>
		CaseInsensitive = 0x00000040,
		/// <summary>If this flag is specified, by using the object handle, to a routine that creates objects and if that object already exists, the routine should open that object. Otherwise, the routine creating the object returns an <see cref="PrivateImplementationDetails.NtStatus"/> code of <see cref="PrivateImplementationDetails.NtStatus.OBJECT_NAME_COLLISION"/>.</summary>
		OpenIf = 0x00000080,
		/// <summary>If an object handle, with this flag set, is passed to a routine that opens objects and if the object is a symbolic link object, the routine should open the symbolic link object itself, rather than the object that the symbolic link refers to (which is the default behavior).</summary>
		OpenLink = 0x00000100,
		/// <summary>The handle is created in system process context and can only be accessed from kernel mode.</summary>
		KernelHandle = 0x00000200,
		/// <summary>The routine that opens the handle should enforce all access checks for the object, even if the handle is being opened in kernel mode.</summary>
		ForceAccessCheck = 0x00000400,
		/// <summary>Reserved.</summary>
		[System.Obsolete("Reserved for internal use only.", true)]
		ValidAttributes = 0x000007F2
	}

	internal struct IoStatusBlock
	{
		public System.WindowsNT.PrivateImplementationDetails.NtStatus status;
		public InformationUnion Information;

		[StructLayout(LayoutKind.Explicit)]
		public struct InformationUnion
		{
			[FieldOffset(0)]
			public System.WindowsNT.IO.FileOpenInformation FileOpenInformation;
			[FieldOffset(0)]
			public uint BytesWritten;
			[FieldOffset(0)]
			public uint BytesRead;
		}
	}

	internal enum SystemInformationClass
	{
		SystemBasicInformation,
		SystemProcessorInformation,
		SystemPerformanceInformation,
		SystemTimeOfDayInformation,
		SystemNotImplemented1,
		SystemProcessesAndThreadsInformation,
		SystemCallCounts,
		SystemConfigurationInformation,
		SystemProcessorTimes,
		SystemGlobalFlag,
		SystemNotImplemented2,
		SystemModuleInformation,
		SystemLockInformation,
		SystemNotImplemented3,
		SystemNotImplemented4,
		SystemNotImplemented5,
		SystemHandleInformation,
		SystemObjectInformation,
		SystemPagefileInformation,
		SystemInstructionEmulationCounts,
		SystemInvalidInfoClass1,
		SystemCacheInformation,
		SystemPoolTagInformation,
		SystemProcessorStatistics,
		SystemDpcInformation,
		SystemNotImplemented6,
		SystemLoadImage,
		SystemUnloadImage,
		SystemTimeAdjustment,
		SystemNotImplemented7,
		SystemNotImplemented8,
		SystemNotImplemented9,
		SystemCrashDumpInformation,
		SystemExceptionInformation,
		SystemCrashDumpStateInformation,
		SystemKernelDebuggerInformation,
		SystemContextSwitchInformation,
		SystemRegistryQuotaInformation,
		SystemLoadAndCallImage,
		SystemPrioritySeparation,
		SystemNotImplemented10,
		SystemNotImplemented11,
		SystemInvalidInfoClass2,
		SystemInvalidInfoClass3,
		SystemTimeZoneInformation,
		SystemLookasideInformation,
		SystemSetTimeSlipEvent,
		SystemCreateSession,
		SystemDeleteSession,
		SystemInvalidInfoClass4,
		SystemRangeStartInformation,
		SystemVerifierInformation,
		SystemAddVerifier,
		SystemSessionProcessesInformation
	}

	internal struct SystemBasicInformation
	{
		public uint Unknown;
		public uint MaximumIncrement;
		public uint PhysicalPageSize;
		public uint NumberOfPhysicalPages;
		public uint LowestPhysicalPage;
		public uint HighestPhysicalPage;
		public uint AllocationGranularity;
		public uint LowestUserAddress;
		public uint HighestUserAddress;
		public uint ActiveProcessors;
		public byte NumberProcessors;
	}

	internal struct SystemProcessorInformation
	{
		public ushort ProcessorArchitecture;
		public ushort ProcessorLevel;
		public ushort ProcessorRevision;
		public ushort Unknown;
		public uint FeatureBits;
	}

	internal struct SystemPerformanceInformation
	{
		public long IdleTime;
		public long ReadTransferCount;
		public long WriteTransferCount;
		public long OtherTransferCount;
		public uint ReadOperationCount;
		public uint WriteOperationCount;
		public uint OtherOperationCount;
		public uint AvailablePages;
		public uint TotalCommittedPages;
		public uint TotalCommitLimit;
		public uint PeakCommitment;
		public uint PageFaults;
		public uint WriteCopyFaults;
		public uint TransistionFaults;
		public uint Reserved1;
		public uint DemandZeroFaults;
		public uint PagesRead;
		public uint PageReadIos;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public uint[] Reserved2;
		public uint PagefilePagesWritten;
		public uint PagefilePageWriteIos;
		public uint MappedFilePagesWritten;
		public uint MappedFilePageWriteIos;
		public uint PagedPoolUsage;
		public uint NonPagedPoolUsage;
		public uint PagedPoolAllocs;
		public uint PagedPoolFrees;
		public uint NonPagedPoolAllocs;
		public uint NonPagedPoolFrees;
		public uint TotalFreeSystemPtes;
		public uint SystemCodePage;
		public uint TotalSystemDriverPages;
		public uint TotalSystemCodePages;
		public uint SmallNonPagedLookasideListAllocateHits;
		public uint SmallPagedLookasideListAllocateHits;
		public uint Reserved3;
		public uint MmSystemCachePage;
		public uint PagedPoolPage;
		public uint SystemDriverPage;
		public uint FastReadNoWait;
		public uint FastReadWait;
		public uint FastReadResourceMiss;
		public uint FastReadNotPossible;
		public uint FastMdlReadNoWait;
		public uint FastMdlReadWait;
		public uint FastMdlReadResourceMiss;
		public uint FastMdlReadNotPossible;
		public uint MapDataNoWait;
		public uint MapDataWait;
		public uint MapDataNoWaitMiss;
		public uint MapDataWaitMiss;
		public uint PinMappedDataCount;
		public uint PinReadNoWait;
		public uint PinReadWait;
		public uint PinReadNoWaitMiss;
		public uint PinReadWaitMiss;
		public uint CopyReadNoWait;
		public uint CopyReadWait;
		public uint CopyReadNoWaitMiss;
		public uint CopyReadWaitMiss;
		public uint MdlReadNoWait;
		public uint MdlReadWait;
		public uint MdlReadNoWaitMiss;
		public uint MdlReadWaitMiss;
		public uint ReadAheadIos;
		public uint LazyWriteIos;
		public uint LazyWritePages;
		public uint DataFlushes;
		public uint DataPages;
		public uint ContextSwitches;
		public uint FirstLevelTbFills;
		public uint SecondLevelTbFills;
		public uint SystemCalls;
	}

	internal struct SystemTimeOfDayInformation
	{
		public long BootTime;
		public long CurrentTime;
		public long TimeZoneBias;
		public uint CurrentTimeZoneId;
	}

	internal struct SystemThreadsInformation
	{
		public long KernelTime;
		public long UserTime;
		public long CreateTime;
		public uint WaitTime;
		public System.IntPtr StartAddress;
		public System.WindowsNT.Processes.ClientID ClientId;
		public int Priority;
		public int BasePriority;
		public uint ContextSwitchCount;
		public ThreadState State;
		public KWaitReason WaitReason;
	}

	[System.Diagnostics.DebuggerDisplay("Name = {ProcessName}")]
	internal struct SystemProcessesInformation
	{
		public uint NextEntryDelta;
		public uint ThreadCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		internal uint[] Reserved1;
		public long CreateTime;
		public long UserTime;
		public long KernelTime;
		public UnicodeString ProcessName;
		public int BasePriority;
		public uint ProcessId;
		public uint InheritedFromProcessId;
		public uint HandleCount;
		public uint SessionId;
		internal uint Reserved2;
		public VMCounters VmCounters;
		public IOCounters IoCounters;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public SystemThreadsInformation[] Threads;

		internal static SystemProcessesInformation[] FromPtr(System.IntPtr ptr)
		{
			System.Collections.Generic.List<SystemProcessesInformation> result = new System.Collections.Generic.List<SystemProcessesInformation>();
			SystemProcessesInformation current;
			current = (SystemProcessesInformation)Marshal.PtrToStructure(ptr, typeof(SystemProcessesInformation));
			while (current.NextEntryDelta != 0)
			{
				unsafe
				{
					current.Threads = new SystemThreadsInformation[(int)current.ThreadCount];
					System.IntPtr pThreads = new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(SystemProcessesInformation), "Threads"));
					for (int i = 0; i < current.ThreadCount; ++i)
					{
						current.Threads[i] = (SystemThreadsInformation)Marshal.PtrToStructure(new System.IntPtr((byte*)pThreads + i * Marshal.SizeOf(typeof(SystemThreadsInformation))), typeof(SystemThreadsInformation));
					}
					result.Add(current);
					current = (SystemProcessesInformation)Marshal.PtrToStructure(ptr = new IntPtr((byte*)ptr + current.NextEntryDelta), typeof(SystemProcessesInformation));
				}
			}
			return result.ToArray();
		}
	}

	internal struct SystemCallCounts
	{
		public uint Size;
		public uint NumberOfDescriptorTables;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public uint[] NumberOfRoutinesInTable;
	}

	internal struct SystemConfigurationInformation
	{
		public uint DiskCount;
		public uint FloppyCount;
		public uint CdRomCount;
		public uint TapeCount;
		public uint SerialCount;
		public uint ParallelCount;
	}

	internal struct SystemProcessorTimes
	{
		public long IdleTime;
		public long KernelTime;
		public long UserTime;
		public long DpcTime;
		public long InterruptTime;
		public uint InterruptCount;
	}

	internal struct SystemGlobalFlag
	{
		public uint GlobalFlag;
	}

	internal struct SystemModuleInformation
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public uint[] Reserved;
		public System.IntPtr Base;
		public uint Size;
		public uint Flags;
		public ushort Index;
		public ushort Unknown;
		public ushort LoadCount;
		public ushort ModuleNameOffset;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] ImageName;
	}

	internal struct SystemLockInformation
	{
		public System.IntPtr Address;
		public ushort Type;
		public ushort Reserved1;
		public uint ExclusiveOwnerThreadId;
		public uint ActiveCount;
		public uint ContentionCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public uint[] Reserved2;
		public uint NumberOfSharedWaiters;
		public uint NumberOfExclusiveWaiters;
	}

	internal struct SystemHandleInformation
	{
		public uint ProcessId;
		public byte ObjectTypeNumber;
		public byte Flags;
		public ushort Handle;
		public System.IntPtr Object;
		public AccessMask GrantedAccess;
	}

	internal struct SystemObjectTypeInformation
	{
		public uint NextEntryOffset;
		public uint ObjectCount;
		public uint HandleCount;
		public uint TypeNumber;
		public uint InvalidAttributes;
		public GenericMapping GenericMapping;
		public AccessMask ValidAccessMask;
		public PoolType PoolType;
		public byte Unknown;
		public UnicodeString Name;
	}

	internal struct SystemObjectInformation
	{
		public uint NextEntryOffset;
		public System.IntPtr Object;
		public uint CreatorProcessId;
		public ushort Unknown;
		public ushort Flags;
		public uint PointerCount;
		public uint HandleCount;
		public uint PagedPoolUsage;
		public uint NonPagedPoolUsage;
		public uint ExclusiveProcessId;
		public System.Security.AccessControl.RawSecurityDescriptor SecurityDescriptor;
		public UnicodeString Name;
	}

	internal struct SystemPagefileInformation
	{
		public uint NextEntryOffset;
		public uint CurrentSize;
		public uint TotalUsed;
		public uint PeakUsed;
		public UnicodeString FileName;
	}

	internal struct SystemInstructionEmulationCounts
	{
		public uint GenericInvalidOpcode;
		public uint TwoByteOpcode;
		public uint ESprefix;
		public uint CSprefix;
		public uint SSprefix;
		public uint DSprefix;
		public uint FSPrefix;
		public uint GSprefix;
		public uint OPER32prefix;
		public uint ADDR32prefix;
		public uint INSB;
		public uint INSW;
		public uint OUTSB;
		public uint OUTSW;
		public uint PUSHFD;
		public uint POPFD;
		public uint INTnn;
		public uint INTO;
		public uint IRETD;
		public uint FloatingPointOpcode;
		public uint INBimm;
		public uint INWimm;
		public uint OUTBimm;
		public uint OUTWimm;
		public uint INB;
		public uint INW;
		public uint OUTB;
		public uint OUTW;
		public uint LOCKprefix;
		public uint REPNEprefix;
		public uint REPprefix;
		public uint CLI;
		public uint STI;
		public uint HLT;
	}

	internal struct SystemCacheInformation
	{
		public uint SystemCacheWsSize;
		public uint SystemCacheWsPeakSize;
		public uint SystemCacheWsFaults;
		public uint SystemCacheWsMinimum;
		public uint SystemCacheWsMaximum;
		public uint TransitionSharedPages;
		public uint TransitionSharedPagesPeak;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public uint[] Reserved;
	}

	internal struct SystemPoolTagInformation
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] Tag;
		public uint PagedPoolAllocs;
		public uint PagedPoolFrees;
		public uint PagedPoolUsage;
		public uint NonPagedPoolAllocs;
		public uint NonPagedPoolFrees;
		public uint NonPagedPoolUsage;
	}

	internal struct SystemProcessorStatistics
	{
		public uint ContextSwitches;
		public uint DpcCount;
		public uint DpcRequestRate;
		public uint TimeIncrement;
		public uint DpcBypassCount;
		public uint ApcBypassCount;
	}

	internal struct SystemDPCInformation
	{
		public uint Reserved;
		public uint MaximumDpcQueueDepth;
		public uint MinimumDpcRate;
		public uint AdjustDpcThreshold;
		public uint IdealDpcRate;
	}

	internal struct SystemLoadImage
	{
		public UnicodeString ModuleName;
		public System.IntPtr ModuleBase;
		public System.IntPtr Unknown;
		public System.IntPtr EntryPoint;
		public System.IntPtr ExportDirectory;
	}

	internal struct SystemUnloadImage
	{
		public System.IntPtr ModuleBase;
	}

	internal struct SystemQueryTimeAdjustment
	{
		public uint TimeAdjustment;
		public uint MaximumIncrement;
		public bool TimeSynchronization;
	}

	internal struct SystemSetTimeAdjustment
	{
		public uint TimeAdjustment;
		public bool TimeSynchronization;
	}

	internal struct SystemCrashDumpINformation
	{
		public System.IntPtr CrashDumpSectionHandle;
		public System.IntPtr Unknown;
	}

	internal struct SystemExceptionInformation
	{
		public uint AlignmentFixupCount;
		public uint ExceptionDispatchCount;
		public uint FloatingEmulationCount;
		public uint Reserved;
	}

	internal struct SystemCrashDumpStateInformation
	{
		public uint ValidCrashDump;
		public uint Unknown;
	}

	internal struct SystemKernelDebuggerInformation
	{
		public bool DebuggerEnabled;
		public bool DebuggerNotPresent;
	}

	internal struct SystemContextSwitchInformation
	{
		public uint ContextSwitches;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
		public uint[] ContextSwitchCounters;
	}

	internal struct SystemRegistryQuotaInformation
	{
		public uint RegistryQuota;
		public uint RegistryQuotaInUse;
		public uint PagedPoolSize;
	}

	internal struct SystemLoadAndCallImage
	{
		public UnicodeString ModuleName;
	}

	internal struct SystemPrioritySeparation
	{
		public uint PrioritySeparation;
	}

	internal struct SystemTimeZoneInformation
	{
		public int Bias;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string StandardName;
		public TimeFields StandardDate;
		public int StandardBias;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string DaylightName;
		public TimeFields DaylightDate;
		public int DaylightBias;
	}

	internal struct SystemLookAsideInformation
	{
		public ushort Depth;
		public ushort MaximumDepth;
		public uint TotalAllocates;
		public uint AllocateMisses;
		public uint TotalFrees;
		public uint FreeMisses;
		public PoolType Type;
		public uint Tag;
		public uint Size;
	}

	public enum ThreadState
	{
		StateInitialized,
		StateReady,
		StateRunning,
		StateStandby,
		StateTerminated,
		StateWait,
		StateTransition,
		StateUnknown
	}

	internal struct TimeFields
	{
		public short Year;        // range [1601...]
		public short Month;       // range [1..12]
		public short Day;         // range [1..31]
		public short Hour;        // range [0..23]
		public short Minute;      // range [0..59]
		public short Second;      // range [0..59]
		public short Milliseconds;// range [0..999]
		public short Weekday;     // range [0..6] == [Sunday..Saturday]
	}

	public enum PoolType
	{
		NonPagedPool,
		PagedPool,
		NonPagedPoolMustSucceed,
		DontUseThisType,
		NonPagedPoolCacheAligned,
		PagedPoolCacheAligned,
		NonPagedPoolCacheAlignedMustS,
		MaxPoolType
	}

	public struct GenericMapping
	{
		public AccessMask GenericRead;
		public AccessMask GenericWrite;
		public AccessMask GenericExecute;
		public AccessMask GenericAll;

		public override string ToString()
		{
			return string.Format("{{GenericRead = {0}, GenericWrite = {1}, GenericExecute = {2}, GenericAll = {3}}}", this.GenericRead, this.GenericWrite, this.GenericExecute, this.GenericAll);
		}
	}

	public enum KWaitReason
	{
		Executive,
		FreePage,
		PageIn,
		PoolAllocation,
		DelayExecution,
		Suspended,
		UserRequest,
		WrExecutive,
		WrFreePage,
		WrPageIn,
		WrPoolAllocation,
		WrDelayExecution,
		WrSuspended,
		WrUserRequest,
		WrEventPair,
		WrQueue,
		WrLpcReceive,
		WrLpcReply,
		WrVirtualMemory,
		WrPageOut,
		WrRendezvous,
		Spare2,
		Spare3,
		Spare4,
		Spare5,
		Spare6,
		WrKernel,
		MaximumWaitReason
	}

	public struct VMCounters
	{
		public System.IntPtr PeakVirtualSize;
		public System.IntPtr VirtualSize;
		public uint PageFaultCount;
		public System.IntPtr PeakWorkingSetSize;
		public System.IntPtr WorkingSetSize;
		public System.IntPtr QuotaPeakPagedPoolUsage;
		public System.IntPtr QuotaPagedPoolUsage;
		public System.IntPtr QuotaPeakNonPagedPoolUsage;
		public System.IntPtr QuotaNonPagedPoolUsage;
		public System.IntPtr PagefileUsage;
		public System.IntPtr PeakPagefileUsage;
	}

	public struct VMCountersEx
	{
		public VMCounters StandardCounters;
		public System.IntPtr PrivateUsage;
	}

	public struct IOCounters
	{
		public ulong ReadOperationCount;
		public ulong WriteOperationCount;
		public ulong OtherOperationCount;
		public ulong ReadTransferCount;
		public ulong WriteTransferCount;
		public ulong OtherTransferCount;
	}

	internal enum ObjectInformationClass
	{
		ObjectBasicInformation,
		ObjectNameInformation,
		ObjectTypeInformation,
		ObjectAllInformation,
		ObjectDataInformation
	}

	internal struct ObjectBasicInformation
	{
		public AllowedObjectAttributes Attributes;
		public AccessMask DesiredAccess;
		public uint HandleCount;
		public uint ReferenceCount;
		public uint PagedPoolQuota;
		public uint NonPagedPoolQuota;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] Unknown2;
	}

	internal struct ObjectDataInformation
	{
		public bool InheritHandle;
		public bool ProtectFromClose;
	}

	internal struct ObjectNameInformation
	{
		public UnicodeString ObjectName;
	}

	internal struct ObjectTypeInformation
	{
		public UnicodeString TypeName;
		public uint TotalNumberOfObjects;
		public uint TotalNumberOfHandles;
		public uint TotalPagedPoolUsage;
		public uint TotalNonPagedPoolUsage;
		public uint TotalNamePoolUsage;
		public uint TotalHandleTableUsage;
		public uint HighWaterNumberOfObjects;
		public uint HighWaterNumberOfHandles;
		public uint HighWaterPagedPoolUsage;
		public uint HighWaterNonPagedPoolUsage;
		public uint HighWaterNamePoolUsage;
		public uint HighWaterHandleTableUsage;
		public uint InvalidAttributes;
		public GenericMapping GenericMapping;
		public AccessMask ValidAccessMask;
		public bool SecurityRequired;
		public bool MaintainHandleCount;
		public PoolType PoolType;
		public uint DefaultPagedPoolCharge;
		public uint DefaultNonPagedPoolCharge;

		public override string ToString()
		{
			var fields = this.GetType().GetFields();
			string[] result = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				try
				{
					result[i] = string.Format("{0} = {1}", fields[i].Name, fields[i].GetValue(this));
				}
				catch (Exception)
				{
					throw;
				}
			}
			return string.Format("[{0}]", string.Join(", ", result));
		}
	}

	internal struct ObjectAllInformation
	{
		public uint NumberOfObjectsTypes;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public ObjectTypeInformation[] ObjectTypeInformation;

		internal static ObjectTypeInformation[] FromPtr(IntPtr pInfo)
		{
			ObjectAllInformation info = (ObjectAllInformation)Marshal.PtrToStructure(pInfo, typeof(ObjectAllInformation));
			ObjectTypeInformation[] result = new ObjectTypeInformation[info.NumberOfObjectsTypes];
			unsafe
			{
				byte* pArr = (byte*)pInfo + (int)Marshal.OffsetOf(typeof(ObjectAllInformation), "ObjectTypeInformation");
				for (int i = 0; i < result.Length; ++i)
				{
					result[i] = (ObjectTypeInformation)Marshal.PtrToStructure(new System.IntPtr(pArr + Marshal.SizeOf(typeof(ObjectTypeInformation)) * i), typeof(ObjectTypeInformation));
				}
			}
			return result;
		}
	}
}