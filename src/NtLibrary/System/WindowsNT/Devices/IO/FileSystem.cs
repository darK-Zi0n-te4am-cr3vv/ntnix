using System.WindowsNT.IO;
using System.WindowsNT.Devices.PrivateImplementationDetails;
using System.WindowsNT.Devices.IO;
namespace System.WindowsNT.Devices.IO
{
	public static class FileSystem
	{
		public static System.Collections.BitArray GetVolumeBitmap(NtNonDirectoryFile volume)
		{
			long lcn = 0;
			return GetVolumeBitmap(volume, ref lcn, -1);
		}

		public static System.Collections.BitArray GetVolumeBitmap(NtNonDirectoryFile volume, ref long startingLcn, long bitcount)
		{ return Wrapper.GetVolumeBitmap(volume.Handle, ref startingLcn, bitcount); }

		internal static System.WindowsNT.Devices.IO.FileTables1.FileRecord ConvertToManagedFileRecord(long referenceNumber, byte[] binary)
		{
			unsafe
			{
				System.IntPtr pAttribute;
				using (System.Runtime.InteropServices.HGlobal ptr = new System.Runtime.InteropServices.HGlobal(binary.Length))
				{
					System.Runtime.InteropServices.Marshal.Copy(binary, 0, ptr.Address, binary.Length);
					System.WindowsNT.Devices.IO.FileTables1.FileRecordSegment segment = System.WindowsNT.Devices.IO.FileTables1.FileRecordSegment.FromPtr(ptr.Address, out pAttribute);
					return new System.WindowsNT.Devices.IO.FileTables1.FileRecord(referenceNumber, segment, System.WindowsNT.Devices.IO.FileTables1.MFTAttribute.AllFromPtr(pAttribute));
				}
			}
		}

		public static NTFS_VOLUME_EXTENDED_VOLUME_DATA GetVolumeData(NtNonDirectoryFile volume)
		{ return Wrapper.GetVolumeData(volume.Handle); }

		public static long GetHighestFileReferenceNumber(NtNonDirectoryFile volume)
		{
			return Wrapper.GetHighestFileReferenceNumber(volume.Handle);
		}

		public static System.WindowsNT.Devices.IO.FileTables1.FileRecord GetHighestFileRecord(NtNonDirectoryFile volume, out long fileReferenceNumber)
		{
			NTFS_FILE_RECORD_OUTPUT_BUFFER result = Wrapper.GetHighestFileRecord(volume.Handle);
			fileReferenceNumber = result.FileReferenceNumber;
			return result.GetManagedRecord();
		}

		public static string GetPath(NtNonDirectoryFile volume, long fileReferenceNumber, long? logicalClusterNumber)
		{
			System.Collections.Generic.Stack<string> paths = new System.Collections.Generic.Stack<string>();
			if (logicalClusterNumber != null)
			{
				FileTables2.FileRecord record = GetBinaryFileRecord(volume, fileReferenceNumber).GetManagedStruct();
				int foundAttIndex = -1;
				using (FileTables2.FileRecord.Enumerator enumerator = record.GetEnumerator())
				{
					int i = 0;
					while (foundAttIndex == -1 && enumerator.MoveNext())
					{
						FileTables2.NonResidentAttribute nra = enumerator.TryGetCurrentAsNonResident();
						if (nra != null)
						{
							long currentVCN = nra.LowestVcn;
							for (int j = 0; j < nra.MappingPairs.Count; ++j)
							{
								MappingPair mp = nra.MappingPairs[j];
								long length = mp.NextVCN - currentVCN;
								if (mp.CurrentLCN <= logicalClusterNumber & mp.CurrentLCN + length > logicalClusterNumber)
								{
									foundAttIndex = i;
									break;
								}
								currentVCN = mp.NextVCN;
							}
						}
						i++;
					}
				}
				if (foundAttIndex != -1)
				{
					using (FileTables2.FileRecord.Enumerator enumerator = record.GetEnumerator())
					{
						int i = 0;
						while (i <= foundAttIndex && enumerator.MoveNext())
						{
							i++;
						}
						FileTables.AttributeRecordHeader header = enumerator.Current;
						if (!string.IsNullOrEmpty(header.Name))
						{
							paths.Push(header.Name);
							paths.Push(":");
						}
					}
				}
			}
			long currentFRN = fileReferenceNumber;
			do
			{
				NTFS_FILE_RECORD_OUTPUT_BUFFER nativeRecord = GetBinaryFileRecord(volume, currentFRN);
				System.Diagnostics.Debug.Assert(currentFRN == nativeRecord.FileReferenceNumber, "Cluster changed while retrieving information.");
				FileTables2.FileRecord record = nativeRecord.GetManagedStruct();
				FileTables2.FileNameAttribute fileName;
				if (record.TryGetFileName(System.WindowsNT.Devices.IO.FileTables.FileNameFlags.NTFS, out fileName))
				{
					if (fileName.ParentDirectory.SegmentNumber > 0 && currentFRN != fileName.ParentDirectory.SegmentNumber)
					{
						currentFRN = fileName.ParentDirectory.SegmentNumber;
						paths.Push(fileName.FileName);
						paths.Push(@"\");
					}
					else
					{
						break;
					}
				}
				else if (record.FileRecordSegmentHeader.BaseFileRecordSegment.SegmentNumber > 0)
				{
					string name = record[record.IndexOf(System.WindowsNT.Devices.IO.FileTables.FormCode.NonResidentForm, System.WindowsNT.Devices.IO.FileTables.AttributeTypeCode.Data)].Name;
					if (name != null)
					{
						paths.Push(name);
						paths.Push(":");
					}
					currentFRN = record.FileRecordSegmentHeader.BaseFileRecordSegment.SegmentNumber;
				}
				else
				{
					break;
				}
			} while (true);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			while (paths.Count > 0)
			{
				sb.Append(paths.Pop());
			}

			return sb.ToString();
		}

		public static void LockVolume(NtNonDirectoryFile volume) { Wrapper.LockVolume(volume.Handle); }

		public static void UnlockVolume(NtNonDirectoryFile volume) { Wrapper.UnlockVolume(volume.Handle); }

		public static System.WindowsNT.Devices.IO.FileTables2.FileTable ReadFileTable(NtNonDirectoryFile volume)
		{
			return ReadFileTableFrom(volume, GetHighestFileReferenceNumber(volume));
		}

		public static System.WindowsNT.Devices.IO.FileTables2.FileTable ReadFileTableFrom(NtNonDirectoryFile volume, long highestFileReferenceNumber) 
		{ return Wrapper.ReadFileTableFrom(volume.Handle, highestFileReferenceNumber); }

		public static Collections.Generic.IEnumerable<NTFS_FILE_RECORD_OUTPUT_BUFFER> GetBinaryFileTableEnumerator(NtNonDirectoryFile volume)
		{ return GetBinaryFileTableEnumerator(volume, GetHighestFileReferenceNumber(volume)); }

		public static Collections.Generic.IEnumerable<NTFS_FILE_RECORD_OUTPUT_BUFFER> GetBinaryFileTableEnumerator(NtNonDirectoryFile volume, long highestFileReferenceNumber)
		{
			return Wrapper.GetFileRecordEnumerator(volume.Handle, highestFileReferenceNumber, GetVolumeData(volume).BytesPerFileRecordSegment);
		}

		public static Collections.Generic.IEnumerable<System.WindowsNT.Devices.IO.FileTables2.FileRecord> GetFileRecordStructEnumerator(NtNonDirectoryFile volume)
		{ return GetFileRecordStructEnumerator(volume, GetHighestFileReferenceNumber(volume)); }

		public static Collections.Generic.IEnumerable<System.WindowsNT.Devices.IO.FileTables2.FileRecord> GetFileRecordStructEnumerator(NtNonDirectoryFile volume, long highestFileReferenceNumber)
		{
			return Wrapper.GetFileRecordStructEnumerator(volume.Handle, highestFileReferenceNumber, GetVolumeData(volume).BytesPerFileRecordSegment);
		}

		public static System.WindowsNT.Devices.IO.FileTables1.FileRecord GetFileRecord(NtNonDirectoryFile volume, ref long fileReferenceNumber)
		{
			NTFS_FILE_RECORD_OUTPUT_BUFFER result = Wrapper.GetFileRecord(volume.Handle, fileReferenceNumber);
			fileReferenceNumber = result.FileReferenceNumber;
			return result.GetManagedRecord();
		}

		public static NTFS_FILE_RECORD_OUTPUT_BUFFER GetBinaryFileRecord(NtNonDirectoryFile volume, long fileReferenceNumber)
		{
			NTFS_FILE_RECORD_OUTPUT_BUFFER result = Wrapper.GetFileRecord(volume.Handle, fileReferenceNumber);
			fileReferenceNumber = result.FileReferenceNumber;
			return result;
		}

		public static RETRIEVAL_POINTERS_BUFFER? GetRetrievalPointers(NtFile fso, long startingVCN, ErrorsNotToThrow errorsNotToThrow)
		{ return Wrapper.GetRetrievalPointers(fso.Handle, startingVCN, errorsNotToThrow); }

		public static void SetZeroData(NtNonDirectoryFile file, long offset, long beyondFinalZero)
		{
			Wrapper.SetZeroData(file.Handle, offset, beyondFinalZero);
		}

		/// <summary>Relocates one or more virtual clusters of a file from one logical cluster to another within the same volume.</summary>
		/// <param name="volume">A handle to the volume that contains the file or directory whose clusters are to be moved.</param>
		/// <param name="file">A file to be moved. If the file is encrypted, the handle must have the <see cref="FileAccessMask.ReadData"/>, <see cref="FileAccessMask.WriteData"/>, <see cref="FileAccessMask.AppendData"/>, or <see cref="FileAccessMask.Execute"/> access right. Other files need only be opened with the <see cref="FileAccessMask.ReadAttributes"/> access right.</param>
		/// <param name="startingVCN">A VCN (cluster number relative to the beginning of a file) of the first cluster to be moved.</param>
		/// <param name="startingLCN">An LCN (cluster number on a volume) to which the VCN is to be moved.</param>
		/// <param name="clusterCount">The count of clusters to be moved.</param>
		public static void MoveFile(NtNonDirectoryFile volume, NtFile file, long startingVCN, long startingLCN, uint clusterCount)
		{ Wrapper.MoveFile(volume.Handle, file.Handle, startingVCN, startingLCN, clusterCount); }
	}
}