using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.WindowsNT.Devices.IO;
using System.WindowsNT.Devices.IO.FileTables;
namespace System.WindowsNT.Devices.IO.FileTables1
{
	public struct FileRecordSegment
	{
		public int Signature { get; set; }
		public ulong LSN { get; set; }
		public ushort SequenceNumber { get; set; }
		public ushort LinkCount { get; set; }             /* Hard link count */
		public SegmentHeaderFlags Flags { get; set; }
		public uint BytesInUse { get; set; }             /* Real size of the FILE record */
		public uint BytesAllocated { get; set; }         /* Allocated size of the FILE record */
		public MFTSegmentReference BaseFileRecordSegment { get; set; }
		public short[] USN { get; set; }

		internal static FileRecordSegment FromBinary(byte[] binary) { int unused; return FromBinary(binary, out unused); }

		internal static FileRecordSegment FromBinary(byte[] binary, out int firstAttributeOffset)
		{
			IntPtr pFirstAttribute;
			unsafe
			{
				fixed (byte* ptr = binary)
				{
					FileRecordSegment result = FromPtr(new IntPtr(ptr), out pFirstAttribute);
					firstAttributeOffset = (int)pFirstAttribute - (int)ptr;
					return result;
				}
			}
		}

		internal static FileRecordSegment FromPtr(System.IntPtr ptr, out System.IntPtr pFirstAttribute)
		{
			unsafe
			{
				FileRecordSegmentHeader* pHeader = (FileRecordSegmentHeader*)ptr;
				pFirstAttribute = new IntPtr((byte*)ptr + pHeader->FirstAttributeOffset);
				byte* pUSN = (byte*)&pHeader->MultiSectorHeader + pHeader->MultiSectorHeader.UpdateSequenceArrayOffset;
				short[] usn = new short[pHeader->MultiSectorHeader.UpdateSequenceArraySize / sizeof(short)];
				Marshal.Copy(new IntPtr(pUSN), usn, 0, usn.Length);
				return new FileRecordSegment()
				{
					BaseFileRecordSegment = pHeader->BaseFileRecordSegment,
					BytesAllocated = pHeader->BytesAllocated,
					BytesInUse = pHeader->BytesInUse,
					Flags = pHeader->Flags,
					LinkCount = pHeader->LinkCount,
					LSN = pHeader->LSN,
					SequenceNumber = pHeader->SequenceNumber,
					Signature = pHeader->MultiSectorHeader.Signature,
					USN = usn
				};
			}
		}

		internal static FileRecordSegment FromStream(System.IO.MemoryStream memoryStream)
		{
			using (HGlobal ptr = new HGlobal(memoryStream.Length))
			{
				unsafe
				{
					System.IO.UnmanagedMemoryStream stream = new System.IO.UnmanagedMemoryStream((byte*)ptr.Address, ptr.AllocatedSize64, ptr.AllocatedSize64, System.IO.FileAccess.ReadWrite);
					memoryStream.WriteTo(stream);
					FileRecordSegmentHeader* pHeader = (FileRecordSegmentHeader*)ptr.Address;
					memoryStream.Seek(pHeader->FirstAttributeOffset, System.IO.SeekOrigin.Begin);
					byte* pUSN = (byte*)&pHeader->MultiSectorHeader + pHeader->MultiSectorHeader.UpdateSequenceArrayOffset;
					short[] usn = new short[pHeader->MultiSectorHeader.UpdateSequenceArraySize / sizeof(short)];
					Marshal.Copy(new IntPtr(pUSN), usn, 0, usn.Length);
					return new FileRecordSegment()
					{
						BaseFileRecordSegment = pHeader->BaseFileRecordSegment,
						BytesAllocated = pHeader->BytesAllocated,
						BytesInUse = pHeader->BytesInUse,
						Flags = pHeader->Flags,
						LinkCount = pHeader->LinkCount,
						LSN = pHeader->LSN,
						SequenceNumber = pHeader->SequenceNumber,
						Signature = pHeader->MultiSectorHeader.Signature,
						USN = usn
					};
				}
			}
		}
	}

	public sealed class FileTable
		: Collections.ObjectModel.KeyedCollection<long, FileRecord>
	{
		public FileTable() : base() { }
#if false
		public FileTable(FileRecord[] array, bool copy)
		{
			this.records = copy ? (FileRecord[])array.Clone() : array;
		}

		public FileTable(Collections.Generic.ICollection<FileRecord> collection)
		{
			this.records = new FileRecord[collection != null ? collection.Count : 0];
			if (collection != null)
			{
				foreach (FileRecord item in collection)
				{
					this.Add(item);
				}
			}
		}
#endif

		public long HighestReferenceNumber
		{
			get
			{
				return this[this.Count - 1].FileReferenceNumber;
			}
		}

		public FileRecord TryGetParent(FileRecord record)
		{
			for (int i = 0; i < record.Count; i++)
			{
				FileNameAttribute info = record[i] as FileNameAttribute;
				if (info != null)
				{
					if (this.Contains(info.ParentDirectorySegmentNumber))
					{
						return this[info.ParentDirectorySegmentNumber];
					}
				}
			}
			return null;
		}

		public string GetPath(FileRecord record)
		{
			Stack<FileRecord> records = new Stack<FileRecord>();
			FileRecord r = record;
			long lastFRN = r.FileReferenceNumber;
			while (r != null && (records.Count <= 0 || r != records.Peek()))
			{
				lastFRN = r.FileReferenceNumber;
				records.Push(r);
				r = this.TryGetParent(r);
			}
			if (records.Count > 0 && records.Peek().FileReferenceNumber == 5)
			{
				records.Pop();
			}
			string streamName = null;
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			for (int i = 0; records.Count > 0; ++i)
			{
				r = records.Pop();
				FileNameAttribute fna = r.Find(a => { FileNameAttribute fn = a as FileNameAttribute; return fn != null && fn.Flags == FileNameFlags.NTFS; }) as FileNameAttribute;
				if (fna == null)
				{
					fna = r.Find(a => a is FileNameAttribute) as FileNameAttribute;
				}
				if (fna == null)
				{
					NonResidentAttribute nra = (NonResidentAttribute)r.Find(a => a is NonResidentAttribute);
					if (nra != null)
					{
						records.Push(this[r.Segment.BaseFileRecordSegment.SegmentNumber]);
						streamName = nra.AttributeName;
						continue;
					}
					else
					{
						break;
					}
				}
				result.AppendFormat(@"{0}{1}", WindowsNT.IO.Path.PATH_SEPARATOR, fna.FileName);
				if (streamName != null)
				{
					result.Append("::");
					result.Append(streamName);
				}
			}
			return result.ToString();
		}

		protected override long GetKeyForItem(FileRecord item)
		{
			return item.FileReferenceNumber;
		}

		public void AddRange(IEnumerable<FileRecord> list)
		{
			foreach (FileRecord item in list)
			{
				this.Add(item);
			}
		}

#if false
		public void Add(FileRecord item)
		{ this[item.FileReferenceNumber] = item; }

		public void Clear()
		{
			for (long i = 0; i < this.records.Length; i++)
			{
				this.records[i] = null;
			}
		}

		public bool Contains(FileRecord item)
		{ return this.Contains(item.FileReferenceNumber); ; }

		public bool Contains(long frn)
		{ return this.records[frn] != null; }

		public void CopyTo(FileRecord[] array, int arrayIndex)
		{
		}

		public long Count
		{
			get
			{
				long count = 0;
				foreach (FileRecord r in this)
				{ count++; }
				return count;
			}
		}

		public bool IsReadOnly
		{ get { return false; } }

		public bool Remove(FileRecord item)
		{
			bool result = this.records[item.FileReferenceNumber] != null;
			this.records[item.FileReferenceNumber] = null;
			return result;
		}

		public IEnumerator<FileRecord> GetEnumerator()
		{
			foreach (FileRecord r in this.records)
			{ if (r != null) { yield return r; } }
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

		public FileRecord TryGetParent(FileRecord record)
		{
			FileNameAttribute fna = record.Find(a => a is FileNameAttribute) as FileNameAttribute;
			return fna == null ? null : this.records[fna.ParentDirectorySegmentNumber];
		}

		public FileRecord this[long index]
		{
			get { return index < this.records.LongLength ? this.records[index] : null; }
			set
			{
				if (index >= this.records.LongLength)
				{
					FileRecord[] newRecords = new FileRecord[2 * (index + 1)];
					Array.Copy(this.records, newRecords, this.records.LongLength);
					this.records = newRecords;
				}
				this.records[index] = value;
			}
		}

		public void TrimToSize()
		{
			long i;
			for (i = this.records.Length - 1; i >= 0; i--)
			{ if (this.records[i] != null) { break; } }
			FileRecord[] newRecords = new FileRecord[i + 1];
			Array.Copy(this.records, newRecords, newRecords.LongLength);
			this.records = newRecords;
		}

		int ICollection<FileRecord>.Count
		{ get { return (int)this.Count; } }
#endif
	}

	public class FileRecord : List<MFTAttribute>, IEquatable<FileRecord>, IComparable<FileRecord>
	{
		public FileRecord(long refNumber, FileRecordSegment segment)
			: base()
		{
			this.FileReferenceNumber = refNumber;
			this.Segment = segment;
		}

		public FileRecord(long refNumber, FileRecordSegment segment, IEnumerable<MFTAttribute> attributes)
			: base(attributes)
		{
			this.FileReferenceNumber = refNumber;
			this.Segment = segment;
		}

		public FileRecordSegment Segment { get; private set; }

		public long FileReferenceNumber { get; private set; }

		public override bool Equals(object obj)
		{ return this.Equals(obj as FileRecord); }

		public override int GetHashCode()
		{ return this.FileReferenceNumber.GetHashCode(); }

		public bool Equals(FileRecord other)
		{ return other != null && this.CompareTo(other) == 0; }

		public int CompareTo(FileRecord other)
		{ return this.FileReferenceNumber.CompareTo(other.FileReferenceNumber); }
	}

	[System.Diagnostics.DebuggerDisplay("{System.WindowsNT.PrivateImplementationDetails.Extensions.GetDisplayString(this, this.GetType(), new System.Collections.Generic.List<string>(new string[] {\"Name\"}))}")]
	public abstract class MFTAttribute
	{
		protected MFTAttribute(FormCode formCode)
		{
			this.FormCode = formCode;
		}

		public abstract AttributeTypeCode TypeCode { get; }
		public FormCode FormCode { get; internal set; }
		public AttributeMask AttributeFlags { get; set; }
		public ushort Instance { get; set; }
		public string AttributeName { get; set; }

		internal static MFTAttribute FromPtr(System.IntPtr ptr, out uint recordLength)
		{
			MFTAttribute result;
			System.IntPtr pNext;
			unsafe
			{
				byte* pHeader = (byte*)ptr;
				AttributeRecordHeader nullableHeader = AttributeRecordHeader.FromPtr(new System.IntPtr(pHeader), out pNext);
				if (nullableHeader != null)
				{
					AttributeRecordHeader header = nullableHeader;
					header.Name = Marshal.PtrToStringUni(new IntPtr(pHeader + header.NameOffset), header.NameLength);
					recordLength = header.RecordLength;
					if (header.FormCode == FormCode.ResidentForm)
					{
						IO.FileTables.ResidentAttribute* pAttribute = (IO.FileTables.ResidentAttribute*)(pHeader + (int)Marshal.OffsetOf(typeof(AttributeRecordHeader), "Form"));
						result = ResidentAttribute.FromPtr(header.TypeCode, new IntPtr((byte*)pHeader + pAttribute->ValueOffset), pAttribute->ValueLength);
					}
					else if (header.FormCode == FormCode.NonResidentForm)
					{
						IO.FileTables.NonResidentAttribute* pAttribute = (IO.FileTables.NonResidentAttribute*)(pHeader + (int)Marshal.OffsetOf(typeof(AttributeRecordHeader), "Form"));
						NonResidentAttribute nra = new NonResidentAttribute(header.TypeCode, MappingPair.Decompress(pAttribute->LowestVcn, new System.IO.UnmanagedMemoryStream(pHeader + pAttribute->MappingPairsOffset, header.RecordLength)))
						{
							AllocatedLength = pAttribute->AllocatedLength,
							CompressionUnit = pAttribute->CompressionUnit,
							FileSize = pAttribute->FileSize,
							HighestVcn = pAttribute->HighestVcn,
							IndexedFlag = pAttribute->IndexedFlag,
							LowestVcn = pAttribute->LowestVcn,
							TotalAllocated = pAttribute->TotalAllocated,
							ValidDataLength = pAttribute->ValidDataLength,
						};
						result = nra;
					}
					else
					{
						throw new System.ArgumentException(string.Format("The specified form code is not supported: {0}", header.FormCode));
					}
					if (result != null)
					{
						result.AttributeName = header.Name;
						result.Instance = header.Instance;
						result.AttributeFlags = header.AttributeFlags;
					}
				}
				else
				{
					recordLength = 0;
					result = null;
				}
			}
			return result;
		}

		internal static Collections.Generic.List<MFTAttribute> AllFromPtr(System.IntPtr ptr)
		{
			Collections.Generic.List<MFTAttribute> result = new System.Collections.Generic.List<MFTAttribute>();
			do
			{
				uint recordLength;
				MFTAttribute current = FromPtr(ptr, out recordLength);
				if (current == null)
				{
					break;
				}
				unsafe
				{
					ptr = new IntPtr((byte*)ptr + recordLength);
				}
				result.Add(current);
			} while (true);
			return result;
		}
	}

	public abstract class ResidentAttribute : MFTAttribute
	{
		public ResidentAttribute() : base(FormCode.ResidentForm) { }

		public byte ResidentFlags { get; set; }

		public static ResidentAttribute FromPtr(AttributeTypeCode typeCode, IntPtr pBinary, uint length)
		{
			switch (typeCode)
			{
				case AttributeTypeCode.StandardInformation:
					unsafe
					{
						StandardInformationAttribute result = new StandardInformationAttribute(*(StandardInformation*)pBinary);
						return result;
					}
				case AttributeTypeCode.FileName:
					unsafe
					{
						System.IntPtr pNext;
						FileNameAttribute result = new FileNameAttribute(FileNameInformation.FromPtr(pBinary, out pNext));
						return result;
					}
				case AttributeTypeCode.ObjectID:
					unsafe
					{
						return new ObjectIDAttribute(*(Guid*)pBinary);
					}
				case AttributeTypeCode.Data:
					byte[] binary = new byte[length];
					Marshal.Copy(pBinary, binary, 0, (int)length);
					return new DataAttribute(binary);
				case AttributeTypeCode.IndexRoot:
					unsafe
					{
						//DIRECTORY_INDEX result = DIRECTORY_INDEX.FromPtr(new IntPtr(pBinary));
						//return new DirectoryIndex(result); ;
					}
					goto ReturnNull;
				case AttributeTypeCode.AttributeList:
					unsafe
					{
						//System.IntPtr pNext;
						var result = AttributeListEntry.AllFromPtr(pBinary);
						return new AttributeListAttribute(result);
					}
				case AttributeTypeCode.SecurityDescriptor:
					binary = new byte[length];
					Marshal.Copy(pBinary, binary, 0, (int)length);
					System.Security.AccessControl.RawSecurityDescriptor rsd = new System.Security.AccessControl.RawSecurityDescriptor(binary, 0);
					return new SecurityAttribute() { Value = rsd };
				case AttributeTypeCode.VolumeName:
					unsafe
					{
						char* pName = (char*)pBinary;
						return new VolumeNameAttribute(Marshal.PtrToStringUni(new IntPtr(pName)));
					}
				case AttributeTypeCode.VolumeInformation:
					unsafe
					{
						VolumeInformation* pVI = (VolumeInformation*)pBinary;
						return new VolumeInformationAttribute(*pVI);
					}
				case AttributeTypeCode.Bitmap:
					goto ReturnNull;
				ReturnNull:
					return null;
				default:
					if (System.Enum.IsDefined(typeof(AttributeTypeCode), typeCode))
					{
						goto ReturnNull;
					}
					else
					{
						throw new System.NotSupportedException(string.Format("The specified type code is not supported: {0}", typeCode));
					}
			}
		}
	}

	public class NonResidentAttribute : MFTAttribute
	{
		private AttributeTypeCode _typeCode;

		public NonResidentAttribute(AttributeTypeCode typeCode) : this(typeCode, new List<MappingPair>()) { }

		public NonResidentAttribute(AttributeTypeCode typeCode, List<MappingPair> mappingPairs)
			: base(FormCode.NonResidentForm)
		{
			this._typeCode = typeCode;
			this.MappingPairs = mappingPairs;
		}

		public long LowestVcn { get; set; }
		public long HighestVcn { get; set; }
		public ushort CompressionUnit { get; set; }
		public byte IndexedFlag { get; set; }
		public long AllocatedLength { get; set; }
		public long FileSize { get; set; }
		public long ValidDataLength { get; set; }
		public long TotalAllocated { get; set; }
		public List<MappingPair> MappingPairs { get; private set; }

		public override AttributeTypeCode TypeCode
		{ get { return this._typeCode; } }
	}

	public class StandardInformationAttribute : ResidentAttribute
	{
		public StandardInformationAttribute() : base() { }

		internal StandardInformationAttribute(StandardInformation info)
			: this()
		{
			this.ChangeTime = DateTime.FromFileTime(info.ChangeTime);
			this.CreationTime = DateTime.FromFileTime(info.CreationTime);
			this.LastAccessTime = DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = DateTime.FromFileTime(info.LastWriteTime);
			this.OwnerID = info.OwnerId;
			this.SecurityID = info.SecurityId;
			this.FileAttributes = info.FileAttribute;
		}

		public DateTime CreationTime { get; set; }
		public DateTime ChangeTime { get; set; }
		public DateTime LastWriteTime { get; set; }
		public DateTime LastAccessTime { get; set; }
		public WindowsNT.IO.FileAttributes FileAttributes { get; set; }
		public uint OwnerID { get; set; }
		public uint SecurityID { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.StandardInformation; } }

		public override string ToString()
		{
			return string.Format("{0} {{Attributes = {1}}}", typeof(StandardInformationAttribute).Name, this.FileAttributes);
		}
	}

	public class FileNameAttribute : ResidentAttribute
	{
		public FileNameAttribute() : base() { }

		internal FileNameAttribute(FileNameInformation info)
			: this()
		{
			this.ParentDirectorySegmentNumber = info.ParentDirectory.SegmentNumber;
			this.ChangeTime = DateTime.FromFileTime(info.ChangeTime);
			this.CreationTime = DateTime.FromFileTime(info.CreationTime);
			this.LastAccessTime = DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = DateTime.FromFileTime(info.LastWriteTime);
			this.AllocatedSize = info.AllocatedSize;
			this.DataSize = info.DataSize;
			this.Flags = info.Flags;
			this.FileName = info.FileName;
		}

		public long ParentDirectorySegmentNumber { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime ChangeTime { get; set; }
		public DateTime LastWriteTime { get; set; }
		public DateTime LastAccessTime { get; set; }
		public long AllocatedSize { get; set; }
		public long DataSize { get; set; }
		public FileNameFlags Flags { get; set; }
		public string FileName { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.FileName; } }

		public override string ToString()
		{
			return string.Format(@"{0} {{Name = {1}}}", typeof(FileNameAttribute).Name, this.FileName);
		}
	}

	public class ObjectIDAttribute : ResidentAttribute
	{
		public ObjectIDAttribute(System.Guid id) : base() { this.ObjectID = id; }

		public System.Guid ObjectID { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.ObjectID; } }

		public override string ToString()
		{ return this.ObjectID.ToString(); }
	}

	public class SecurityAttribute : ResidentAttribute
	{
		public SecurityAttribute() : base() { }

		public System.Security.AccessControl.RawSecurityDescriptor Value { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.SecurityDescriptor; } }
	}

	public class DirectoryIndexAttribute : ResidentAttribute
	{
		internal DirectoryIndexAttribute(DirectoryIndex info)
		{
			this.AllocationSize = info.AllocationSize;
			this.Flags = info.Flags;
			this.IndexBlockLength = info.IndexBlockLength;
			this.Entries = new List<DirectoryEntry>(Array.ConvertAll(info.Entries, e => new DirectoryEntry(e)));
		}

		public uint AllocationSize { get; set; }
		public uint IndexBlockLength { get; set; }
		public DirectoryFlags Flags { get; set; }
		public List<DirectoryEntry> Entries { get; private set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.IndexRoot; } }
	}

	public class DataAttribute : ResidentAttribute
	{
		public DataAttribute() : this(new byte[0]) { }

		public DataAttribute(byte[] data)
			: base()
		{
			this.Data = data;
		}

		public byte[] Data { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.Data; } }
	}

	public class VolumeNameAttribute : ResidentAttribute
	{
		public VolumeNameAttribute() : this(string.Empty) { }

		public VolumeNameAttribute(string name)
			: base()
		{ this.VolumeName = name; }

		public string VolumeName { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.VolumeName; } }
	}

	public class VolumeInformationAttribute : ResidentAttribute
	{
		public VolumeInformationAttribute() : base() { }

		internal VolumeInformationAttribute(VolumeInformation info)
			: base() 
		{
			this.Flags = info.Flags;
			this.MajorVersion = info.MajorVersion;
			this.MinorVersion = info.MinorVersion;
		}

		public byte MajorVersion { get; set; }
		public byte MinorVersion { get; set; }
		public ushort Flags { get; set; }

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.VolumeInformation; } }
	}

	public class AttributeListAttribute : ResidentAttribute
	{
		private System.Collections.Generic.List<AttributeListEntry> attributes;

		public AttributeListAttribute(System.Collections.Generic.IEnumerable<AttributeListEntry> attributes)
		{
			this.attributes = attributes != null ? new List<AttributeListEntry>(attributes) : null;
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<AttributeListEntry> GetAttributes()
		{
			return new System.Collections.ObjectModel.ReadOnlyCollection<AttributeListEntry>(this.attributes);
		}

		public override AttributeTypeCode TypeCode
		{ get { return AttributeTypeCode.AttributeList; } }
	}

	public struct DirectoryEntry
	{
		internal DirectoryEntry(FileTables.DirectoryEntry info)
			: this()
		{
			this.FileReferenceNumber = info.FileReferenceNumber;
			this.Flags = info.Flags;
			this.AttributeLength = info.AttributeLength;
			unsafe
			{
				fixed (byte* pBinary = info.Value)
				{
					uint recordLength;
					this.Value = MFTAttribute.FromPtr(new IntPtr(pBinary), out recordLength);
				}
			}
		}

		public long FileReferenceNumber { get; set; }
		///<summary>The size, in bytes, of the attribute that is indexed.</summary>
		public ushort AttributeLength { get; set; }
		///<summary>A bit array of flags specifying properties of the entry.</summary>
		public DirectoryEntryFlags Flags { get; set; }
		public MFTAttribute Value { get; set; }
	}
}