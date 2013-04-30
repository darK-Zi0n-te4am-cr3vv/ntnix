using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.Memory.PrivateImplementationDetails;
using System.WindowsNT.IO;
namespace System.WindowsNT.Memory
{
	[System.Diagnostics.DebuggerDisplay("Name = {Name}")]
	public class NtSection : NtObject
	{
		protected NtSection(SafeNtSectionHandle handle) : base() { this.Handle = handle; }

		public SectionAccessMask AccessMask { get { return (SectionAccessMask)this.ObjectAccessMask; } }

		public override SafeNtHandle GenericHandle { get { return this.Handle; } }

		public SafeNtSectionHandle Handle { get; private set; }

		public System.IntPtr BaseAddress
		{ get { return Wrapper.NtQuerySectionBasic(this.Handle).BaseAddress; } }

		public SectionAllocationAttributes Attributes
		{ get { return Wrapper.NtQuerySectionBasic(this.Handle).Attributes; } }

		public long Size
		{ get { return Wrapper.NtQuerySectionBasic(this.Handle).Size; } }

		public System.IntPtr ImageEntryPoint
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).EntryPoint; } }

		public bool ImageExecutable
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).Executable; } }

		public ushort ImageNumber
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).ImageNumber; } }

		public ushort ImageMajorSubsystemVersion
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).MajorSubsystemVersion; } }

		public ushort ImageMinorSubsystemVersion
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).MinorSubsystemVersion; } }

		public uint ImageStackCommit
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).StackCommit; } }

		public uint ImageStackReserve
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).StackReserve; } }

		public uint ImageSubsystem
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).Subsystem; } }

		public uint ImageCharacteristics
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).Characteristics; } }

		public uint ImageStackZeroBits
		{ get { return Wrapper.NtQuerySectionImage(this.Handle).StackZeroBits; } }

		public void MapView(Processes.NtProcess process, IntPtr? baseAddress, byte zeroBits, IntPtr commitSize, IntPtr? viewSize, ref long? sectionOffset, SectionInherit inherit, MemoryAllocationType allocationType, PageProtection protection, out System.IntPtr actualBaseAddress, out System.IntPtr actualViewSize)
		{
			Wrapper.NtMapViewOfSection(this.Handle, process.Handle, baseAddress, zeroBits, commitSize, ref sectionOffset, viewSize, inherit, allocationType, protection, out actualBaseAddress, out actualViewSize);
		}

		public static NtSection CreateSection(string name, AllowedObjectAttributes attributes, long maxSize, PageProtection protection, SectionAllocationAttributes allocAttributes, NtNonDirectoryFile file)
		{ return new NtSection(Wrapper.NtCreateSection(name, attributes, maxSize, protection, allocAttributes, file != null ? file.Handle : null)); }

		public static NtSection CreateSection(string name, AllowedObjectAttributes attributes, PageProtection protection, SectionAllocationAttributes allocAttributes, NtNonDirectoryFile file)
		{ return new NtSection(Wrapper.NtCreateSection(name, attributes, protection, allocAttributes, file != null ? file.Handle : null)); }
	}
}