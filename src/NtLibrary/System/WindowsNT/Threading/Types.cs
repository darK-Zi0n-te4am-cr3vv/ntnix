namespace System.WindowsNT.Threading
{
	internal struct ThreadBasicInformation
	{
		public System.WindowsNT.PrivateImplementationDetails.NtStatus ExitStatus;
		public IntPtr TebBaseAddress;
		public Processes.ClientID ClientId;
		public UIntPtr AffinityMask;
		public int Priority;
		public int BasePriority;
	}

	internal struct CSRSS_MESSAGE
	{
		public uint Unknown1;
		public uint Opcode;
		public uint Status;
		public uint Unknown2;
	}

	internal struct CSRSS_MESSAGE2
	{
		public short PortMessage;
		public CSRSS_MESSAGE CsrssMessage;
		public ProcessInformation ProcessInformation;
		public Processes.ClientID Debugger;
		public uint CreationFlags;
		public ulong VdmInfo;
	}

	public struct ProcessInformation
	{
		public IntPtr hProcess;
		public IntPtr hThread;
		public uint PID;
		public uint TID;
	}

	public enum ContextFlags
	{
		CONTEXT_CONTROL = (Context.CONTEXT_i386 | 0x00000001), // SS:SP, CS:IP, FLAGS, BP
		CONTEXT_INTEGER = (Context.CONTEXT_i386 | 0x00000002), // AX, BX, CX, DX, SI, DI
		CONTEXT_SEGMENTS = (Context.CONTEXT_i386 | 0x00000004), // DS, ES, FS, GS
		CONTEXT_FLOATING_POINT = (Context.CONTEXT_i386 | 0x00000008), // 387 state
		CONTEXT_DEBUG_REGISTERS = (Context.CONTEXT_i386 | 0x00000010), // DB 0-3,6,7
		CONTEXT_EXTENDED_REGISTERS = (Context.CONTEXT_i386 | 0x00000020), // cpu specific extensions
		CONTEXT_FULL = (CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS)
	}

	public struct Context
	{
		public const int CONTEXT_i386 = 0x00010000;

		//
		// The flags values within this flag control the contents of
		// a CONTEXT record.
		//
		// If the context record is used as an input parameter, then
		// for each portion of the context record controlled by a flag
		// whose value is set, it is assumed that that portion of the
		// context record contains valid context. If the context record
		// is being used to modify a threads context, then only that
		// portion of the threads context will be modified.
		//
		// If the context record is used as an IN OUT parameter to capture
		// the context of a thread, then only those portions of the thread's
		// context corresponding to set flags will be returned.
		//
		// The context record is never used as an OUT only parameter.
		//

		public ContextFlags ContextFlags;

		//
		// This section is specified/returned if CONTEXT_DEBUG_REGISTERS is
		// set in ContextFlags.  Note that CONTEXT_DEBUG_REGISTERS is NOT
		// included in CONTEXT_FULL.
		//

		public uint Dr0;
		public uint Dr1;
		public uint Dr2;
		public uint Dr3;
		public uint Dr6;
		public uint Dr7;

		//
		// This section is specified/returned if the
		// ContextFlags word contians the flag CONTEXT_FLOATING_POINT.
		//

		public FloatingSaveArea FloatSave;

		//
		// This section is specified/returned if the
		// ContextFlags word contians the flag CONTEXT_SEGMENTS.
		//

		public uint SegGs;
		public uint SegFs;
		public uint SegEs;
		public uint SegDs;

		//
		// This section is specified/returned if the
		// ContextFlags word contians the flag CONTEXT_INTEGER.
		//

		public uint Edi;
		public uint Esi;
		public uint Ebx;
		public uint Edx;
		public uint Ecx;
		public uint Eax;

		//
		// This section is specified/returned if the
		// ContextFlags word contians the flag CONTEXT_CONTROL.
		//

		public uint Ebp;
		public uint Eip;
		public uint SegCs;              // MUST BE SANITIZED
		public uint EFlags;             // MUST BE SANITIZED
		public uint Esp;
		public uint SegSs;

		//
		// This section is specified/returned if the ContextFlags word
		// contains the flag CONTEXT_EXTENDED_REGISTERS.
		// The format and contexts are processor specific
		//

		public unsafe fixed byte ExtendedRegisters[MAXIMUM_SUPPORTED_EXTENSION];


		public const int MAXIMUM_SUPPORTED_EXTENSION = 512;
	}

	public struct FloatingSaveArea
	{
		public uint ControlWord;
		public uint StatusWord;
		public uint TagWord;
		public uint ErrorOffset;
		public uint ErrorSelector;
		public uint DataOffset;
		public uint DataSelector;
		public unsafe fixed byte RegisterArea[SIZE_OF_80387_REGISTERS];
		public uint Cr0NpxState;

		public const int SIZE_OF_80387_REGISTERS = 80;
	}

	internal enum ThreadInformationClass
	{
		ThreadBasicInformation,
		ThreadTimes,
		ThreadPriority,
		ThreadBasePriority,
		ThreadAffinityMask,
		ThreadImpersonationToken,
		ThreadDescriptorTableEntry,
		ThreadEnableAlignmentFaultFixup,
		ThreadEventPair_Reusable,
		ThreadQuerySetWin32StartAddress,
		ThreadZeroTlsCell,
		ThreadPerformanceCount,
		ThreadAmILastThread,
		ThreadIdealProcessor,
		ThreadPriorityBoost,
		ThreadSetTlsArrayAddress,
		ThreadIsIoPending,
		ThreadHideFromDebugger,
		ThreadBreakOnTermination,
		MaxThreadInfoClass
	}

	public class ThreadInformationSnapshot
	{
		internal ThreadInformationSnapshot(SystemThreadsInformation threadInfo)
		{
			this.BasePriority = threadInfo.BasePriority;
			this.ClientId = threadInfo.ClientId;
			this.ContextSwitchCount = threadInfo.ContextSwitchCount;
			this.CreateTime = System.DateTime.FromFileTime(threadInfo.CreateTime);
			this.KernelTime = System.DateTime.FromFileTime(threadInfo.KernelTime);
			this.Priority = threadInfo.Priority;
			this.StartAddress = threadInfo.StartAddress;
			this.State = threadInfo.State;
			this.UserTime = System.DateTime.FromFileTime(threadInfo.UserTime);
			this.WaitReason = threadInfo.WaitReason;
			this.WaitTime = threadInfo.WaitTime;
		}

		public System.DateTime KernelTime { get; private set; }
		public System.DateTime UserTime { get; private set; }
		public System.DateTime CreateTime { get; private set; }
		public uint WaitTime { get; private set; }
		public System.IntPtr StartAddress { get; private set; }
		public System.WindowsNT.Processes.ClientID ClientId { get; private set; }
		public int Priority { get; private set; }
		public int BasePriority { get; private set; }
		public uint ContextSwitchCount { get; private set; }
		public ThreadState State { get; private set; }
		public KWaitReason WaitReason { get; private set; }
	}
}