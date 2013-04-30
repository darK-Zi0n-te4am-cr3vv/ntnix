using System.Runtime.InteropServices;
using CHAR = System.Byte;
using LONGLONG = System.Int64;
using UCHAR = System.Byte;
using ULONG = System.UInt32;
using ULONGLONG = System.UInt64;
using USHORT = System.UInt16;
namespace System.WindowsNT.Devices.IO.FileTables
{
	[StructLayout(LayoutKind.Sequential)]
	public class AttributeRecordHeader
	{
		public AttributeRecordHeader() { }

		public AttributeTypeCode TypeCode;
		///<summary>The size of the attribute record, in bytes. This value reflects the required size for the record variant and is always rounded to the nearest quadword boundary.</summary>
		public ULONG RecordLength;
		public FormCode FormCode;
		///<summary>The size of the optional attribute name, in characters, or 0 if there is no attribute name. The maximum attribute name length is 255 characters.</summary>
		internal UCHAR NameLength;
		///<summary>The offset of the attribute name from the start of the attribute record, in bytes. If the <see cref="NameLength"/> member is 0, this member is undefined.</summary>
		internal USHORT NameOffset;
		public AttributeMask AttributeFlags;
		public USHORT Instance;
		[StructLayout(LayoutKind.Explicit)]
		public struct FormUnion
		{
			[FieldOffset(0)]
			public ResidentAttribute Resident;
			[FieldOffset(0)]
			public NonResidentAttribute Nonresident;
		}
		public FormUnion Form;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name;

		[System.Diagnostics.DebuggerHidden]
		internal static AttributeRecordHeader FromPtr(IntPtr ptr) { IntPtr next; return FromPtr(ptr, out next); }

		internal static AttributeRecordHeader FromPtr(IntPtr ptr, out IntPtr pNext)
		{
			AttributeRecordHeader output;
			AttributeTypeCode tc = ReadTypeCode(ptr);
			if (!System.Enum.IsDefined(typeof(AttributeTypeCode), tc))
			{
				System.Diagnostics.Debugger.Break();
			}
			if (tc != AttributeTypeCode.EndOfAttributes)
			{
				AttributeRecordHeader result = (AttributeRecordHeader)Marshal.PtrToStructure(ptr, typeof(AttributeRecordHeader));
				unsafe
				{
					pNext = new IntPtr((byte*)ptr + result.RecordLength);
					if (result.NameLength != 0)
					{
						result.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + result.NameOffset), result.NameLength);
					}
					else
					{
						result.Name = null;
					}
					output = result;
				}
			}
			else
			{
				pNext = IntPtr.Zero;
				output = null;
			}
			return output;
		}

		internal static ULONG? TryReadRecordLength(IntPtr ptr)
		{
			unsafe
			{
				AttributeTypeCode typeCode = *(AttributeTypeCode*)ptr;
				if (typeCode != AttributeTypeCode.EndOfAttributes)
					return (ULONG?)*(ULONG*)((byte*)ptr + sizeof(int));
				else
					return null;
			}
		}

		private static AttributeTypeCode ReadTypeCode(IntPtr ptr) { unsafe { return *(AttributeTypeCode*)ptr; } }
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct BootSector
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public UCHAR[] Magic;                           // 0x00
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
		public string OemName;                         // 0x03
		public USHORT BytesPerSector;                     // 0x0B
		public UCHAR SectorsPerCluster;                  // 0x0D
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public UCHAR[] Unused0;                         // 0x0E
		public UCHAR MediaId;                            // 0x15
		public short Unused1;                         // 0x16
		public USHORT SectorsPerTrack;
		public USHORT Heads;
		public long Unused2;
		public int Unknown0; /* always 80 00 80 00 */
		public ULONGLONG SectorCount;
		public ULONGLONG MftLocation;
		public ULONGLONG MftMirrLocation;
		public CHAR ClustersPerMftRecord;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public UCHAR[] Unused3;
		public CHAR ClustersPerIndexRecord;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public UCHAR[] Unused4;
		public ULONGLONG SerialNumber;                       // 0x48
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 432)]
		public UCHAR[] BootCode;                      // 0x50

		internal static BootSector FromPtr(IntPtr ptr)
		{ return (BootSector)Marshal.PtrToStructure(ptr, typeof(BootSector)); }
	}
	public enum FormCode : byte
	{
		ResidentForm = 0x00,
		NonResidentForm = 0x01
	}
	[System.Flags()]
	public enum AttributeMask : ushort
	{
		CompressionMask = 0x00FF,
		Encrypted = 0x4000,
		Sparse = 0x8000
	}
	[System.Diagnostics.DebuggerDisplay("Segment Number = {SegmentNumber}, Sequence Number = {SequenceNumber}")]
	public struct MFTSegmentReference
	{
		public ULONG SegmentNumberLowPart;
		public USHORT SegmentNumberHighPart;
		public long SegmentNumber
		{ get { return (long)this.SegmentNumberHighPart << (sizeof(long) << 2) | unchecked((long)(ulong)this.SegmentNumberLowPart); } }
		public USHORT SequenceNumber;
	}
	public struct ResidentAttribute
	{
		public ULONG ValueLength;
		internal USHORT ValueOffset;
		public UCHAR Flags;
		internal UCHAR Reserved;
		public unsafe fixed byte Value[1];
	}
	public struct NonResidentAttribute
	{
		public LONGLONG LowestVcn;
		public LONGLONG HighestVcn;
		///<summary>The offset to the mapping pairs array from the start of the attribute record, in bytes.</summary>
		public USHORT MappingPairsOffset;
		public USHORT CompressionUnit;
		internal unsafe fixed UCHAR Reserved[3];
		public UCHAR IndexedFlag;
		public LONGLONG AllocatedLength;
		public LONGLONG FileSize;
		public LONGLONG ValidDataLength;
		public LONGLONG TotalAllocated;
		public unsafe fixed byte MappingPairs[1];
	}
	///<remarks>
	///<para>The multisector header and update sequence array provide detection of incomplete multisector transfers for devices that either have a physical sector size greater than or equal to the sequence number stride (512) or that do not transfer sectors out of order. If a device exists that has a sector size smaller than the sequence number stride and it sometimes transfers sectors out of order, then the update sequence array will not provide absolute detection of incomplete transfers. The sequence number stride is set to a small enough number to provide absolute protection for all known hard disks. It is not set any smaller or there might be excessive run time or space overhead.</para>
	///<para>The update sequence array consists of an array of <c>n</c> <see cref="USHORT"/> values, where <c>n</c> is the size of the structure being protected divided by the sequence number stride. The first word contains the update sequence number, which is a cyclical counter of the number of times the containing structure has been written to disk. Next are the <c>n</c> saved <see cref="USHORT"/> values that were overwritten by the update sequence number the last time the containing structure was written to disk.</para>
	///<para>Each time the protected structure is about to be written to disk, the last word in each sequence number stride is saved to its respective position in the sequence number array, then it is overwritten with the next update sequence number. After the write, or whenever the structure is read, the saved word from the sequence number array is restored to its actual position in the structure. Before restoring the saved words on reads, all the sequence numbers at the end of each stride are compared with the actual sequence number at the start of the array. If any of these comparisons are not equal, then a failed multisector transfer has been detected.</para>
	///<para>The size of the array is determined by the size of the containing structure. The update sequence array should be included at the end of the header of the structure it is protecting because of its variable size. The user must ensure that the correct space is reserved for the containing structure: <c>(size of structure / 512 + 1) * sizeof(<see cref="USHORT"/>)</c>.</para>
	///</remarks>
	public struct MultiSectorHeader
	{
		public int Signature;
		///<summary>The offset to the update sequence array, from the start of this structure. The update sequence array must end before the last <see cref="USHORT"/> value in the first sector.</summary>
		public USHORT UpdateSequenceArrayOffset;
		///<summary>The size of the update sequence array, in bytes.</summary>
		public USHORT UpdateSequenceArraySize;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AttributeListEntry
	{
		public AttributeTypeCode AttributeTypeCode;
		public USHORT RecordLength;
		///<summary>The size of the optional attribute name, in characters. If a name exists, this value is nonzero and the structure is followed immediately by a Unicode string of the specified number of characters.</summary>
		internal UCHAR AttributeNameLength;
		internal UCHAR AttributeNameOffset;
		public long LowestVcn;
		public MFTSegmentReference SegmentReference;
		public USHORT AttributeID;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string AttributeName;

		internal static Collections.Generic.List<AttributeListEntry> AllFromPtr(System.IntPtr ptr)
		{
			Collections.Generic.List<AttributeListEntry> result = new System.Collections.Generic.List<AttributeListEntry>();
			AttributeListEntry current;
			do
			{
				unsafe
				{
					current = FromPtr(ptr, out ptr);
					if (System.Enum.IsDefined(typeof(AttributeTypeCode), current.AttributeTypeCode))
					{
						result.Add(current);
					}
					else
					{
						break;
					}
				}
			} while (true);
			return result;
		}

		internal static AttributeListEntry FromPtr(System.IntPtr ptr, out System.IntPtr pNext)
		{
			AttributeListEntry result = (AttributeListEntry)Marshal.PtrToStructure(ptr, typeof(AttributeListEntry));
			unsafe
			{
				result.AttributeName = Marshal.PtrToStringUni(new IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(AttributeListEntry), "AttributeName")));
				pNext = new IntPtr((byte*)ptr + Marshal.SizeOf(typeof(AttributeListEntry)));
			}
			return result;
		}
	}
	internal struct StandardInformation
	{
		public LONGLONG CreationTime;
		public LONGLONG ChangeTime;
		public LONGLONG LastWriteTime;
		public LONGLONG LastAccessTime;
		public WindowsNT.IO.FileAttributes FileAttribute;
		public unsafe fixed ULONG AlignmentOrReserved[3];
		public ULONG OwnerId;
		public ULONG SecurityId;
		//public ULONGLONG QuotaCharge;
		//public USN Usn;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal class FileNameInformation
	{
		public FileNameInformation() { }

		public MFTSegmentReference ParentDirectory;
		public LONGLONG CreationTime;
		public LONGLONG ChangeTime;
		public LONGLONG LastWriteTime;
		public LONGLONG LastAccessTime;
		public LONGLONG AllocatedSize;
		public LONGLONG DataSize;
		public ULONG Attributes;
		public ULONG AlignmentOrReserved;
		///<summary>The length of the file name, in Unicode characters.</summary>
		public UCHAR FileNameLength;
		public FileNameFlags Flags;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string FileName;

		internal static FileNameInformation FromPtr(System.IntPtr ptr, out System.IntPtr pNext)
		{
			FileNameInformation result = (FileNameInformation)Marshal.PtrToStructure(ptr, typeof(FileNameInformation));
			unsafe
			{
				result.FileName = Marshal.PtrToStringUni(new IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNameInformation), "FileName")), result.FileNameLength);
				pNext = new IntPtr((byte*)ptr + Marshal.SizeOf(typeof(FileNameInformation)) + result.FileNameLength * sizeof(char));
			}
			return result;
		}

		internal static FileNameInformation FromPtr(System.IntPtr ptr)
		{
			FileNameInformation result = (FileNameInformation)Marshal.PtrToStructure(ptr, typeof(FileNameInformation));
			unsafe
			{
				result.FileName = Marshal.PtrToStringUni(new IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(FileNameInformation), "FileName")), result.FileNameLength);
				//pNext = new IntPtr((byte*)ptr + Marshal.SizeOf(typeof(FILE_NAME)) + result.FileNameLength * sizeof(char));
			}
			return result;
		}
	}
	[System.Flags()]
	public enum FileNameFlags : byte
	{
		NTFS = 0x01,
		DOS = 0x02
	}
	internal struct VolumeInformation
	{
		public ULONGLONG Unknown1;
		public UCHAR MajorVersion;
		public UCHAR MinorVersion;
		public USHORT Flags;
		public ULONG Unknown2;
	}
	public struct FileRecordSegmentHeader
	{
		public MultiSectorHeader MultiSectorHeader;
		public ULONGLONG LSN;
		public USHORT SequenceNumber;
		public USHORT LinkCount;             /* Hard link count */
		public USHORT FirstAttributeOffset;
		public SegmentHeaderFlags Flags;
		public ULONG BytesInUse;             /* Real size of the FILE record */
		public ULONG BytesAllocated;         /* Allocated size of the FILE record */
		public MFTSegmentReference BaseFileRecordSegment;
		internal USHORT Reserved4;
		internal unsafe fixed USHORT USN[1];
	}
	[System.Flags()]
	public enum SegmentHeaderFlags : ushort
	{
		FileRecordSegmentInUse = 0x0001,
		FileNameIndexPresent = 0x0002
	}
	public enum AttributeTypeCode : byte
	{
		TxF = 0x00,
		StandardInformation = 0x10,
		AttributeList = 0x20,
		FileName = 0x30,
		ObjectID = 0x40,
		SecurityDescriptor = 0x50,
		VolumeName = 0x60,
		VolumeInformation = 0x70,
		Data = 0x80,
		IndexRoot = 0x90,
		IndexAllocation = 0xA0,
		Bitmap = 0xB0,
		ReparsePoint = 0xC0,
		EAInformation = 0xD0,
		EA = 0xE0,
		PropertySet = 0xF0,
		/// <summary>Used internally. This is NOT a valid type code.</summary>
		EndOfAttributes = 0xFF
	}
	internal struct IndexBlockHeader
	{
		public MultiSectorHeader MultiSectorHeader;
		public long IndexBlockVCN;
		public DirectoryIndex DirectoryIndex;
	}
	[System.Flags()]
	public enum DirectoryFlags : uint
	{
		SmallDirectory = 0x00000000,
		LargeDirectory = 0x00000001
	}
	internal struct DirectoryIndex
	{
		///<summary>The offset, in bytes, from the start of the structure to the first <see cref="DirectoryEntry"/> structure.</summary>
		public uint EntriesOffset;
		///<summary>The size, in bytes, of the portion of the index block that is in use.</summary>
		public uint IndexBlockLength;
		///<summary>The size, in bytes, of disk space allocated for the index block.</summary>
		public uint AllocationSize;
		///<summary>A bit array of flags specifying properties of the index.</summary>
		public DirectoryFlags Flags;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public DirectoryEntry[] Entries;

		internal static DirectoryIndex FromPtr(System.IntPtr ptr)
		{
			DirectoryIndex result = (DirectoryIndex)Marshal.PtrToStructure(ptr, typeof(DirectoryIndex));
			Collections.Generic.List<DirectoryEntry> entries = new Collections.Generic.List<DirectoryEntry>();
			System.IntPtr pNext = ptr;
			DirectoryEntry current;
			do
			{
				current = DirectoryEntry.FromPtr(pNext, out pNext);
				entries.Add(current);
			} while ((current.Flags & DirectoryEntryFlags.LastEntry) == 0);
			result.Entries = entries.ToArray();
			return result;
		}
	}
	[System.Flags()]
	public enum DirectoryEntryFlags : uint
	{
		HasTrailingVCN = 0x00000001,
		LastEntry = 0x00000002
	}
	/// <remarks>If the <see cref="DirectoryEntryFlags.HasTrailingVCN"/> flag of a <see cref="DirectoryEntry"/> is set, the last eight bytes of the directory entry contain the VCN of the index block that holds the entries immediately preceding the current entry.</remarks>
	public struct DirectoryEntry
	{
		public long FileReferenceNumber;
		///<summary>The size, in bytes, of the directory entry.</summary>
		public ushort Length;
		///<summary>The size, in bytes, of the attribute that is indexed.</summary>
		public ushort AttributeLength;
		///<summary>A bit array of flags specifying properties of the entry.</summary>
		public DirectoryEntryFlags Flags;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] Value;

		public static DirectoryEntry FromPtr(System.IntPtr ptr, out System.IntPtr pNext)
		{
			DirectoryEntry result = (DirectoryEntry)Marshal.PtrToStructure(ptr, typeof(DirectoryEntry));
			result.Value = new byte[result.Length]; //Is this correct?
			unsafe
			{
				Marshal.Copy(new IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(DirectoryEntry), "Value")), result.Value, 0, result.Value.Length);
				pNext = new IntPtr((byte*)ptr + result.Length); //Is this correct?
			}
			return result;
		}
	}
	// Where can I find: public struct ATTRIBUTE_BITMAP { }
	public struct AttributeList
	{
		public AttributeTypeCode AttributeType;
		public USHORT Length;
		public UCHAR NameLength;
		public UCHAR NameOffset;
		public LONGLONG StartVcn; // LowVcn
		public LONGLONG FileReferenceNumber;
		public USHORT AttributeNumber;
		public unsafe fixed USHORT AlignmentOrReserved[3];
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] Value;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name;

		internal static AttributeList FromPtr(System.IntPtr ptr, out System.IntPtr pNext)
		{
			AttributeList result = (AttributeList)Marshal.PtrToStructure(ptr, typeof(AttributeList));
			result.Value = new byte[result.Length];
			unsafe
			{
				Marshal.Copy(new IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(AttributeList), "Value")), result.Value, 0, result.Value.Length);
				result.Name = Marshal.PtrToStringUni(new IntPtr((byte*)ptr + result.NameOffset), result.NameLength);
				pNext = new IntPtr((byte*)ptr + result.Length);


			}
			return result;
		}
	}
	// public struct VolumeName contains the volume label as a Unicode string
}