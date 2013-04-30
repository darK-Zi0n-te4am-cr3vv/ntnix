using System.Runtime.InteropServices;
namespace System.WindowsNT.Memory
{
	public enum MemoryInformationClass : uint
	{
		MemoryBasicInformation,
	}

	public struct MemoryBasicInformation
	{
		public IntPtr BaseAddress;
		public IntPtr AllocationBase;
		public PageProtection AllocationProtect;
		public IntPtr RegionSize;
		public MemoryAllocationType State;
		public PageProtection Protect;
		public SectionAllocationAttributes Type;
	}

	public enum SectionAccessMask : uint
	{
		MaximumAllowed = AccessMask.MaximumAllowed,
		SECTION_QUERY = 0x0001,
		SECTION_MAP_WRITE = 0x0002,
		SECTION_MAP_READ = 0x0004,
		SECTION_MAP_EXECUTE = 0x0008,
		SECTION_EXTEND_SIZE = 0x0010,
		SECTION_ALL_ACCESS = (AccessMask.StandardRightsRequired | SECTION_QUERY | SECTION_MAP_WRITE | SECTION_MAP_READ | SECTION_MAP_EXECUTE | SECTION_EXTEND_SIZE)
	}

	public enum PageProtection : uint
	{
		NOACCESS = 0x01,
		READONLY = 0x02,
		READWRITE = 0x04,
		WRITECOPY = 0x08,
		EXECUTE = 0x10,
		EXECUTE_READ = 0x20,
		EXECUTE_READWRITE = 0x40,
		EXECUTE_WRITECOPY = 0x80,
		GUARD = 0x100,
		NOCACHE = 0x200,
		WRITECOMBINE = 0x400
	}

	public enum SectionAllocationAttributes : uint
	{
		FILE = 0x800000,
		IMAGE = 0x1000000,
		RESERVE = 0x4000000,
		COMMIT = 0x8000000,
		NOCACHE = 0x10000000,
		PROTECTED_IMAGE = 0x2000000,
		WRITECOMBINE = 0x40000000,
		LARGE_PAGES = 0x80000000
	}

	internal enum SectionInformationClass
	{
		SectionBasicInformation,
		SectionImageInformation
	}

	public enum MemoryFreeType : uint
	{
		DECOMMIT = 0x4000,
		RELEASE = 0x8000
	}

	public enum MemoryAllocationType : uint
	{
		COMMIT = 0x1000,
		RESERVE = 0x2000,
		FREE = 0x10000,
		PRIVATE = 0x20000,
		MAPPED = 0x40000,
		RESET = 0x80000,
		TOP_DOWN = 0x100000,
		WRITE_WATCH = 0x200000,
		ROTATE = 0x800000,
		LARGE_PAGES = 0x20000000,
		PHYSICAL = 0x400000,
		FOUR_MB_PAGES = 0x80000000
	}

	internal struct SectionBasicInformation
	{
		public System.IntPtr BaseAddress;
		public SectionAllocationAttributes Attributes;
		public long Size;
	}

	public enum SectionInherit
	{
		/// <summary>The view will be mapped into any child processes that is created in the future. </summary>
		ViewShare = 1,
		/// <summary>The view will not be mapped into child processes.</summary>
		ViewUnmap = 2
	}

	internal struct SectionImageInformation
	{
		public System.IntPtr EntryPoint;
		public uint StackZeroBits;
		public uint StackReserve;
		public uint StackCommit;
		public uint Subsystem;
		public ushort MinorSubsystemVersion;
		public ushort MajorSubsystemVersion;
		public uint Unknown1;
		public uint Characteristics;
		public ushort ImageNumber;
		public bool Executable;
		public byte Unknown2;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public unsafe uint[] Unknown4;
	}
}