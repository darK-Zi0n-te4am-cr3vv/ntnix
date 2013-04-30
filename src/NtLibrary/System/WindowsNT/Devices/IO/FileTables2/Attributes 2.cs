#undef DEBUG
using System.Runtime.InteropServices;
using System.WindowsNT.Devices.IO;
using System.WindowsNT.Devices.IO.FileTables;
namespace System.WindowsNT.Devices.IO.FileTables2
{
	public class FileTable : System.Collections.ObjectModel.KeyedCollection<long, FileRecord>
	{
		public FileTable() : base() { }

		public FileRecord TryGetParent(FileRecord record)
		{
			FileNameAttribute fna;
			bool hasFna = record.TryGetFileName(FileNameFlags.NTFS, out fna);
			return hasFna && this.Contains(fna.ParentDirectory.SegmentNumber) ? this[fna.ParentDirectory.SegmentNumber] : null;
		}

		protected override long GetKeyForItem(FileRecord item) { return item.FileReferenceNumber; }

		public string GetPath(FileRecord fileRecord)
		{
			System.Collections.Generic.Stack<FileRecord> hierarchy = new System.Collections.Generic.Stack<FileRecord>();
			for (FileRecord r = fileRecord; r != null && (hierarchy.Count <= 0 || r != hierarchy.Peek()); r = this.TryGetParent(r))
			{
				hierarchy.Push(r);
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			while (hierarchy.Count > 0)
			{
				FileRecord currentRecord = hierarchy.Pop();
				FileNameAttribute fna;
				bool success = currentRecord.TryGetFileName(FileNameFlags.NTFS, out fna);
				sb.AppendFormat(@"\{0}", success ? fna.FileName : "?");
			}
			return sb.ToString();
		}
	}

	public class FileRecord : System.IDisposable, System.Collections.Generic.IEnumerable<AttributeRecordHeader>
	{
		private unsafe delegate void ArrayToPtrCopyDelegate(byte[] src, int srcIndex, byte* pDest, int destIndex, int len);
		private static ArrayToPtrCopyDelegate __arrayToPtrCopy;
		private unsafe delegate void PtrToPtrCopyDelegate(byte* src, byte* pDest, int len);
		private static PtrToPtrCopyDelegate __ptrToPtrCopy;

		static FileRecord()
		{
			__arrayToPtrCopy = (ArrayToPtrCopyDelegate)Delegate.CreateDelegate(typeof(ArrayToPtrCopyDelegate), typeof(Buffer).GetMethod("memcpy", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(byte[]), typeof(int), typeof(byte*), typeof(int), typeof(int) }, null));
			__ptrToPtrCopy = (PtrToPtrCopyDelegate)Delegate.CreateDelegate(typeof(PtrToPtrCopyDelegate), typeof(Buffer).GetMethod("memcpyimpl", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(byte*), typeof(byte*), typeof(int) }, null));
		}

		public struct Enumerator : System.Collections.Generic.IEnumerator<AttributeRecordHeader>
		{
			private FileRecord record;
			private IntPtr pCurrentHeader;
			private IntPtr pNextHeader;

			internal IntPtr AddressOfCurrentHeader
			{
				[System.Diagnostics.DebuggerStepThrough]
				get { return this.pCurrentHeader; }
			}

			internal IntPtr AddressOfCurrentForm
			{
				get
				{
					unsafe
					{
						return (IntPtr)((byte*)this.AddressOfCurrentHeader + (uint)Marshal.OffsetOf(typeof(AttributeRecordHeader), "Form"));
					}
				}
			}

			internal IntPtr AddressOfCurrentValue
			{
				get
				{
					AttributeRecordHeader header = AttributeRecordHeader.FromPtr(this.pCurrentHeader);
					if (header != null && header.FormCode == FormCode.ResidentForm)
					{
						unsafe
						{
							return (IntPtr)((byte*)this.pCurrentHeader + header.Form.Resident.ValueOffset);
						}
					}
					throw new InvalidOperationException();
				}
			}

			public Enumerator(FileRecord record)
				: this()
			{
				this.record = record;
				this.Reset();
			}

			public AttributeRecordHeader Current { get { return AttributeRecordHeader.FromPtr(this.pCurrentHeader); } }

			public NonResidentAttribute TryGetCurrentAsNonResident()
			{
				AttributeRecordHeader current = this.Current;
				if (current.FormCode == FormCode.NonResidentForm)
				{
					unsafe
					{
						byte* pMappingPairs = (byte*)this.AddressOfCurrentHeader + current.Form.Nonresident.MappingPairsOffset;
						System.IO.UnmanagedMemoryStream ums = new System.IO.UnmanagedMemoryStream(pMappingPairs, current.RecordLength);
						System.Collections.Generic.List<MappingPair> mappingPairs = MappingPair.Decompress(current.Form.Nonresident.LowestVcn, ums);
						if (mappingPairs.Count > 4)
						{
							ulong lcn, count;
							bool found = MappingPair.FindRun(pMappingPairs, 0, (ulong)current.Form.Nonresident.LowestVcn, (ulong)current.Form.Nonresident.HighestVcn, out lcn, out count);
						}
						return new NonResidentAttribute((FileTables.NonResidentAttribute*)this.AddressOfCurrentForm, mappingPairs);
					}
				}
				return null;
			}

			public void Dispose() { this.record = null; this.pCurrentHeader = System.IntPtr.Zero; this.pNextHeader = System.IntPtr.Zero; }

			object System.Collections.IEnumerator.Current { get { return this.Current; } }

			public bool MoveNext()
			{
				this.pCurrentHeader = this.pNextHeader;
				uint? recordLength = AttributeRecordHeader.TryReadRecordLength(this.pCurrentHeader);
				if (recordLength.HasValue)
				{
					unsafe
					{
						this.pNextHeader = (IntPtr)((byte*)this.pCurrentHeader + recordLength.Value);
						return true;
					}
				}
				else
				{
					this.Dispose();
					return false;
				}
			}

			public void Reset()
			{
				this.pCurrentHeader = record.pUnmanaged;
				unsafe
				{
					FileRecordSegmentHeader segmentHeader = *(FileRecordSegmentHeader*)this.pCurrentHeader;
					this.pNextHeader = (IntPtr)((byte*)this.pCurrentHeader + segmentHeader.FirstAttributeOffset);
				}
			}
		}

		private int allocSize;
		private IntPtr pUnmanaged;

		internal FileRecord(NTFS_FILE_RECORD_OUTPUT_BUFFER copy)
		{
			this.allocSize = (int)copy.FileRecordLength;
			this.pUnmanaged = Marshal.AllocHGlobal(allocSize);
			this.FileReferenceNumber = copy.FileReferenceNumber;
			unsafe
			{
				__arrayToPtrCopy(copy.FileRecordBuffer, 0, (byte*)this.pUnmanaged, 0, (int)copy.FileRecordLength);
			}
			//GC.AddMemoryPressure(this.allocSize);
		}

		internal FileRecord(IntPtr pRecordOutputBufferCopy)
		{
			this.allocSize = (int)NTFS_FILE_RECORD_OUTPUT_BUFFER.ReadRecordLength(pRecordOutputBufferCopy);
			this.pUnmanaged = Marshal.AllocHGlobal(allocSize);
			this.FileReferenceNumber = NTFS_FILE_RECORD_OUTPUT_BUFFER.ReadReferenceNumber(pRecordOutputBufferCopy);
			unsafe
			{
				__ptrToPtrCopy((byte*)NTFS_FILE_RECORD_OUTPUT_BUFFER.GetBufferPtr(pRecordOutputBufferCopy), (byte*)this.pUnmanaged, (int)NTFS_FILE_RECORD_OUTPUT_BUFFER.ReadRecordLength(pRecordOutputBufferCopy));
			}
			GC.AddMemoryPressure(this.allocSize);
		}

		~FileRecord() { this.Dispose(false); }

		public long FileReferenceNumber { get; private set; }

		public StandardAttribute? StandardInformation
		{
			get
			{
				Enumerator enumerator = new Enumerator(this);
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.FormCode == FormCode.ResidentForm
						&& enumerator.Current.TypeCode == AttributeTypeCode.StandardInformation)
					{
						unsafe
						{
							StandardInformation si = *(StandardInformation*)enumerator.AddressOfCurrentValue;
							return new StandardAttribute(si);
						}
					}
				}
				return null;
			}
		}

		private string VolumeName
		{
			get
			{
				Enumerator enumerator = new Enumerator(this);
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.FormCode == FormCode.ResidentForm
						&& enumerator.Current.TypeCode == AttributeTypeCode.VolumeName)
					{
						return Marshal.PtrToStringUni(enumerator.AddressOfCurrentValue);
					}
				}
				return null;
			}
		}

		public bool TryGetFileName(FileNameFlags preferred, out FileNameAttribute attribute)
		{
			bool found = false;
			FileNameAttribute result = default(FileNameAttribute);
			Enumerator enumerator = new Enumerator(this);
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.FormCode == FormCode.ResidentForm
					&& enumerator.Current.TypeCode == AttributeTypeCode.FileName)
				{
					unsafe
					{
						FileNameInformation fn = FileNameInformation.FromPtr(enumerator.AddressOfCurrentValue);
						FileNameAttribute fni = new FileNameAttribute(fn);
						if (fni.Flags == preferred)
						{
							found = true;
							result = fni;
							break;
						}
						else if (!found)
						{
							found = true;
							result = fni;
						}
					}
				}
			}
			attribute = result;
			return found;
		}

		public int IndexOf(FormCode form, AttributeTypeCode typeCode)
		{
			int i = 0;
			Enumerator enumerator = new Enumerator(this);
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.FormCode == form
					&& enumerator.Current.TypeCode == typeCode)
				{
					return i;
				}
				++i;
			}
			return -1;
		}

		public NonResidentAttribute TryGetNonResidentFileData()
		{
#if DEBUG
			bool stop = false;
			FileNameAttribute? fna = this.FileName;
			if (fna.HasValue && fna.Value.FileName.StartsWith(@"$Bad"))
			{
				stop = true;
			}
#endif
			Enumerator enumerator = new Enumerator(this);
			while (enumerator.MoveNext())
			{
				AttributeRecordHeader current = enumerator.Current;
				if (current.FormCode == FormCode.NonResidentForm
					&& current.TypeCode == AttributeTypeCode.Data)
				{
					unsafe
					{
#if DEBUG
						if (stop)
						{
							System.Diagnostics.Debugger.Break();
						}
#endif
						unsafe
						{
							byte* pMappingPairs = (byte*)enumerator.AddressOfCurrentHeader + current.Form.Nonresident.MappingPairsOffset;
							System.IO.UnmanagedMemoryStream ums = new System.IO.UnmanagedMemoryStream(pMappingPairs, current.RecordLength);
							System.Collections.Generic.List<MappingPair> mappingPairs = MappingPair.Decompress(current.Form.Nonresident.LowestVcn, ums);
							if (mappingPairs.Count > 4)
							{
								ulong lcn, count;
								bool found = MappingPair.FindRun(pMappingPairs, 0, (ulong)current.Form.Nonresident.LowestVcn, (ulong)current.Form.Nonresident.HighestVcn, out lcn, out count);
							}
							return new NonResidentAttribute((FileTables.NonResidentAttribute*)enumerator.AddressOfCurrentForm, mappingPairs);
						}
					}
				}
			}
			return null;
		}

		private FileNameAttribute? FindMatch(System.Predicate<FileNameAttribute> condition)
		{
			Enumerator enumerator = new Enumerator(this);
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.FormCode == FormCode.ResidentForm
					&& enumerator.Current.TypeCode == AttributeTypeCode.FileName)
				{
					//IntPtr dummy;
					unsafe
					{
						FileNameInformation fn = FileNameInformation.FromPtr(enumerator.AddressOfCurrentValue);
						FileNameAttribute fni = new FileNameAttribute(fn);
						if (condition(fni))
						{
							return fni;
						}
					}
				}
			}
			return null;
		}

		public FileNameAttribute? FileName
		{
			get
			{
				FileNameAttribute result;
				return this.TryGetFileName(FileNameFlags.NTFS, out result) ? (FileNameAttribute?)result : null;
			}
		}

		public AttributeRecordHeader this[int index]
		{ get { return AttributeRecordHeader.FromPtr(this.GetPointer(index)); } }

		private IntPtr GetPointer(int index)
		{
			unsafe
			{
				System.IntPtr pAttribute = (IntPtr)((byte*)this.pUnmanaged + ((FileRecordSegmentHeader*)this.pUnmanaged)->FirstAttributeOffset);
				for (int i = 0; i < index; i++)
				{
					uint? length = AttributeRecordHeader.TryReadRecordLength(pAttribute);
					if (!length.HasValue)
					{
						throw new System.IndexOutOfRangeException();
					}
					pAttribute = (IntPtr)((byte*)pAttribute + length.Value);
				}
				return pAttribute;
			}
		}

		public Enumerator GetEnumerator() { return new Enumerator(this); }

		System.Collections.Generic.IEnumerator<AttributeRecordHeader> System.Collections.Generic.IEnumerable<AttributeRecordHeader>.GetEnumerator() { return this.GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

		public FileRecordSegmentHeader FileRecordSegmentHeader { get { unsafe { return *(FileRecordSegmentHeader*)this.pUnmanaged; } } }

		public bool IsDisposed { get; private set; }

		public void Dispose() { this.Dispose(true); }

		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing)
				{
					Marshal.FreeHGlobal(this.pUnmanaged);
					GC.RemoveMemoryPressure(this.allocSize);
				}


				this.IsDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public System.Collections.Generic.List<NonResidentAttribute> GetNonResidentAttributes()
		{
			System.Collections.Generic.List<NonResidentAttribute> result = new System.Collections.Generic.List<NonResidentAttribute>();
			using (Enumerator enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AttributeRecordHeader current = enumerator.Current;
					NonResidentAttribute nra = enumerator.TryGetCurrentAsNonResident();
					if (nra != null)
					{
						result.Add(nra);
					}
				}
			}
			return result;
		}
	}

	[System.Diagnostics.DebuggerDisplay("{FileName}")]
	public struct FileNameAttribute
	{
		internal FileNameAttribute(FileNameInformation info)
		{
			this.ParentDirectory = info.ParentDirectory;
			this.CreationTime = DateTime.FromFileTime(info.CreationTime);
			this.ChangeTime = DateTime.FromFileTime(info.ChangeTime);
			this.LastAccessTime = DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = DateTime.FromFileTime(info.LastWriteTime);
			this.AllocatedSize = info.AllocatedSize;
			this.DataSize = info.DataSize;
			this.Attributes = info.Attributes;
			this.Flags = info.Flags;
			this.FileName = info.FileName;
		}

		public MFTSegmentReference ParentDirectory;
		public DateTime CreationTime;
		public DateTime ChangeTime;
		public DateTime LastWriteTime;
		public DateTime LastAccessTime;
		public long AllocatedSize;
		public long DataSize;
		public uint Attributes;
		public FileNameFlags Flags;
		public string FileName;
	}

	public struct StandardAttribute
	{
		internal StandardAttribute(StandardInformation info)
		{
			this.CreationTime = DateTime.FromFileTime(info.CreationTime);
			this.ChangeTime = DateTime.FromFileTime(info.ChangeTime);
			this.LastAccessTime = DateTime.FromFileTime(info.LastAccessTime);
			this.LastWriteTime = DateTime.FromFileTime(info.LastWriteTime);
			this.FileAttribute = info.FileAttribute;
			this.OwnerId = info.OwnerId;
			this.SecurityId = info.SecurityId;
		}

		public DateTime CreationTime;
		public DateTime ChangeTime;
		public DateTime LastWriteTime;
		public DateTime LastAccessTime;
		public WindowsNT.IO.FileAttributes FileAttribute;
		public uint OwnerId;
		public uint SecurityId;
	}

	public class NonResidentAttribute
	{
		internal unsafe NonResidentAttribute(IO.FileTables.NonResidentAttribute* addressOfNonResident, System.Collections.Generic.List<MappingPair> mappingPairs)
		{
			this.LowestVcn = addressOfNonResident->LowestVcn;
			this.HighestVcn = addressOfNonResident->HighestVcn;
			this.CompressionUnit = addressOfNonResident->CompressionUnit;
			this.IndexedFlag = addressOfNonResident->IndexedFlag;
			this.AllocatedLength = addressOfNonResident->AllocatedLength;
			this.FileSize = addressOfNonResident->FileSize;
			this.ValidDataLength = addressOfNonResident->ValidDataLength;
			this.TotalAllocated = addressOfNonResident->TotalAllocated;
			this.MappingPairs = mappingPairs;
		}

		public long LowestVcn;
		public long HighestVcn;
		public ushort CompressionUnit;
		public byte IndexedFlag;
		public long AllocatedLength;
		public long FileSize;
		public long ValidDataLength;
		public long TotalAllocated;
		public System.Collections.Generic.List<IO.MappingPair> MappingPairs;
	}
}