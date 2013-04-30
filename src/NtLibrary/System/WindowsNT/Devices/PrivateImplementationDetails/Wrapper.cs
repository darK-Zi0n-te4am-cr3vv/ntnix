using System.Runtime.InteropServices;
using System.WindowsNT.Devices.IO;
using System.WindowsNT.Errors;
using System.WindowsNT.IO;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.Devices.Video;
namespace System.WindowsNT.Devices.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		public const int MAX_VOLUME_ID_SIZE = 36;
		public const int PRODUCT_ID_LENGTH = 16;
		public const int MAX_VOLUME_TEMPLATE_SIZE = 40;
		public const int VENDOR_ID_LENGTH = 8;
		public const int REVISION_LENGTH = 4;
		public const int SERIAL_NUMBER_LENGTH = 32;

		public static Collections.BitArray GetVolumeBitmap(SafeNtFileHandle volumeHandle, ref long startingLcn, long bitcount)
		{
			IoStatusBlock ioStatusBlock;
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = GetVolumeData(volumeHandle);
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(VOLUME_BITMAP_BUFFER)) + (bitcount < 0 ? (volumeData.TotalClusters + 7) / 8 : bitcount << 3)))
			{
				unsafe
				{
					VOLUME_BITMAP_BUFFER* pBitmapBuffer = (VOLUME_BITMAP_BUFFER*)pBuffer.Address;
					STARTING_LCN_INPUT_BUFFER startLCN = new STARTING_LCN_INPUT_BUFFER() { StartingLcn = 0 };

					NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_VOLUME_BITMAP, new System.IntPtr(&startLCN), (uint)Marshal.SizeOf(typeof(STARTING_LCN_INPUT_BUFFER)), new System.IntPtr(pBitmapBuffer), pBuffer.AllocatedSize32);
					if ((result != NtStatus.BUFFER_OVERFLOW & result != NtStatus.BUFFER_TOO_SMALL) || bitcount < 0)
					{
						NtException.CheckAndThrowException(result);
					}
					startingLcn = pBitmapBuffer->StartingLcn;
					byte[] bitmap = new byte[(pBitmapBuffer->BitmapSize + 7) / 8];
					Marshal.Copy(new System.IntPtr(pBitmapBuffer->Buffer), bitmap, 0, bitmap.Length);
					return new System.Collections.BitArray(bitmap) { Length = (int)pBitmapBuffer->BitmapSize };
				}
			}
		}

		public static NTFS_VOLUME_EXTENDED_VOLUME_DATA GetVolumeData(SafeNtFileHandle volumeHandle)
		{
			IoStatusBlock ioStatusBlock;
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = new NTFS_VOLUME_EXTENDED_VOLUME_DATA();
			unsafe
			{
				NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_NTFS_VOLUME_DATA, System.IntPtr.Zero, 0, new System.IntPtr(&volumeData), (uint)Marshal.SizeOf(volumeData));
				NtException.CheckAndThrowException(result);
			}
			return volumeData;
		}

		public static long GetHighestFileReferenceNumber(SafeNtFileHandle volumeHandle)
		{
			IoStatusBlock ioStatusBlock;
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = GetVolumeData(volumeHandle);
			NTFS_FILE_RECORD_INPUT_BUFFER input = new NTFS_FILE_RECORD_INPUT_BUFFER();
			unsafe
			{
				byte* pOutput = stackalloc byte[Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER))];
				long start = 0, end = long.MaxValue;
				do
				{
					input.FileReferenceNumber = (start + end) >> 1;
					NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_NTFS_FILE_RECORD, new System.IntPtr(&input), (uint)Marshal.SizeOf(input), new IntPtr(pOutput), (uint)Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)));
					if (result == NtStatus.INVALID_PARAMETER)
					{
						end = input.FileReferenceNumber;
					}
					else
					{
						start = input.FileReferenceNumber;
					}
				} while (start < end - 1);
				return start;
			}
		}

		public static NTFS_FILE_RECORD_OUTPUT_BUFFER GetHighestFileRecord(SafeNtFileHandle volumeHandle)
		{
			IoStatusBlock ioStatusBlock;
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = GetVolumeData(volumeHandle);
			using (HGlobal pFileRecord = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + volumeData.BytesPerFileRecordSegment - 1))
			{
				NTFS_FILE_RECORD_INPUT_BUFFER input = new NTFS_FILE_RECORD_INPUT_BUFFER();
				long start = 0, end = long.MaxValue;
				do
				{
					input.FileReferenceNumber = (start + end) >> 1;
					unsafe
					{
						NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_NTFS_FILE_RECORD, new System.IntPtr(&input), (uint)Marshal.SizeOf(input), pFileRecord.Address, pFileRecord.AllocatedSize32);
						if (result == NtStatus.INVALID_PARAMETER)
						{
							end = input.FileReferenceNumber;
						}
						else
						{
							start = input.FileReferenceNumber;
						}
					}
				} while (start < end - 1);
				return NTFS_FILE_RECORD_OUTPUT_BUFFER.FromPtr(pFileRecord.Address);
			}
		}

		public static NTFS_FILE_RECORD_OUTPUT_BUFFER GetFileRecord(SafeNtFileHandle volumeHandle, long referenceNumber)
		{
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = GetVolumeData(volumeHandle);
			using (HGlobal pFileRecord = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + volumeData.BytesPerFileRecordSegment - 1))
			{
				GetFileRecord(volumeHandle, referenceNumber, volumeData.BytesPerFileRecordSegment, pFileRecord);
				return NTFS_FILE_RECORD_OUTPUT_BUFFER.FromPtr(pFileRecord.Address);
			}
		}

		public static System.WindowsNT.Devices.IO.FileTables2.FileRecord GetFileRecordStruct(SafeNtFileHandle volumeHandle, long referenceNumber)
		{
			NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = GetVolumeData(volumeHandle);
			using (HGlobal pFileRecord = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + volumeData.BytesPerFileRecordSegment - 1))
			{
				GetFileRecord(volumeHandle, referenceNumber, volumeData.BytesPerFileRecordSegment, pFileRecord);
				return new System.WindowsNT.Devices.IO.FileTables2.FileRecord(pFileRecord.Address);
			}
		}

		public static System.Collections.Generic.IEnumerable<NTFS_FILE_RECORD_OUTPUT_BUFFER> GetFileRecordEnumerator(SafeNtFileHandle volumeHandle, long referenceNumber, uint bytesPerFileRecordSegment)
		{
			using (HGlobal pFileRecord = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + bytesPerFileRecordSegment - 1))
			{
				for (long recordNumber = referenceNumber; recordNumber >= 0; recordNumber--)
				{
					GetFileRecord(volumeHandle, recordNumber, bytesPerFileRecordSegment, pFileRecord);
					NTFS_FILE_RECORD_OUTPUT_BUFFER result = NTFS_FILE_RECORD_OUTPUT_BUFFER.FromPtr(pFileRecord.Address);
					recordNumber = result.FileReferenceNumber;
					yield return result;
				}
			}
		}

		public static System.Collections.Generic.IEnumerable<System.WindowsNT.Devices.IO.FileTables2.FileRecord> GetFileRecordStructEnumerator(SafeNtFileHandle volumeHandle, long referenceNumber, uint bytesPerFileRecordSegment)
		{
			using (HGlobal pFileRecord = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + bytesPerFileRecordSegment - 1))
			{
				for (long recordNumber = referenceNumber; recordNumber >= 0; recordNumber--)
				{
					GetFileRecord(volumeHandle, recordNumber, bytesPerFileRecordSegment, pFileRecord);
					System.WindowsNT.Devices.IO.FileTables2.FileRecord result = new System.WindowsNT.Devices.IO.FileTables2.FileRecord(pFileRecord.Address);
					recordNumber = result.FileReferenceNumber;
					yield return result;
				}
			}
		}

		public static void LockVolume(SafeNtFileHandle volumeHandle)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.LOCK_VOLUME, System.IntPtr.Zero, 0, IntPtr.Zero, 0);
			NtException.CheckAndThrowException(result);
		}

		public static void UnlockVolume(SafeNtFileHandle volumeHandle)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.UNLOCK_VOLUME, System.IntPtr.Zero, 0, IntPtr.Zero, 0);
			NtException.CheckAndThrowException(result);
		}

		public static void GetFileRecord(SafeNtFileHandle volumeHandle, long referenceNumber, uint bytesPerFileRecordSegment, HGlobal pFileRecord)
		{
			IoStatusBlock ioStatusBlock;
			NTFS_FILE_RECORD_INPUT_BUFFER input = new NTFS_FILE_RECORD_INPUT_BUFFER() { FileReferenceNumber = referenceNumber };
			unsafe
			{
				NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_NTFS_FILE_RECORD, new System.IntPtr(&input), (uint)Marshal.SizeOf(input), pFileRecord.Address, pFileRecord.AllocatedSize32);
				NtException.CheckAndThrowException(result);
			}
		}

		public static System.WindowsNT.Devices.IO.FileTables2.FileTable ReadFileTableFrom(SafeNtFileHandle volumeHandle, long highestFileReferenceNumber)
		{
			IoStatusBlock ioStatusBlock;
			uint sizePerStruct = (uint)Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + GetVolumeData(volumeHandle).BytesPerFileRecordSegment - 1;
			NTFS_FILE_RECORD_INPUT_BUFFER input = new NTFS_FILE_RECORD_INPUT_BUFFER() { FileReferenceNumber = highestFileReferenceNumber };
			using (HGlobal pFileRecord = new HGlobal(highestFileReferenceNumber * sizePerStruct))
			{
				for (int i = 0; input.FileReferenceNumber > 0 && i < pFileRecord.AllocatedSize64 / sizePerStruct; ++i)
				{
					unsafe
					{
						IntPtr ptr = (IntPtr)((byte*)pFileRecord.Address + i * sizePerStruct);
						NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_NTFS_FILE_RECORD, new System.IntPtr(&input), (uint)Marshal.SizeOf(input), ptr, sizePerStruct);
						NtException.CheckAndThrowException(result);
						input.FileReferenceNumber = NTFS_FILE_RECORD_OUTPUT_BUFFER.ReadReferenceNumber(ptr) - 1;
					}
				}

				System.WindowsNT.Devices.IO.FileTables2.FileTable fileTable = new System.WindowsNT.Devices.IO.FileTables2.FileTable();
				for (int i = 0; i < pFileRecord.AllocatedSize64 / sizePerStruct; i++)
				{
					unsafe
					{
						IntPtr ptr = (IntPtr)((byte*)pFileRecord.Address + i * sizePerStruct);
						if (NTFS_FILE_RECORD_OUTPUT_BUFFER.ReadReferenceNumber(ptr) == 0)
						{
							break;
						}
						System.WindowsNT.Devices.IO.FileTables2.FileRecord fileRecord = new System.WindowsNT.Devices.IO.FileTables2.FileRecord(ptr);
						fileTable.Add(fileRecord);
					}
				}
				return fileTable;
			}
		}

		public static RETRIEVAL_POINTERS_BUFFER? GetRetrievalPointers(SafeNtFileHandle fileSystemObjectHandle, long startingVCN, ErrorsNotToThrow errorsNotToThrow)
		{
			IoStatusBlock ioStatusBlock;
			STARTING_VCN_INPUT_BUFFER input = new STARTING_VCN_INPUT_BUFFER() { StartingVcn = startingVCN };
			using (HGlobal pRetrievalPointers = new HGlobal(Marshal.SizeOf(typeof(NTFS_FILE_RECORD_OUTPUT_BUFFER)) + 0x100))
			{
				unsafe
				{
					NtStatus result;
					do
					{
						pRetrievalPointers.ReAlloc(pRetrievalPointers.AllocatedSize32 << 1 + 1);
						result = Native.NtFsControlFile(NTInternal.CreateHandleRef(fileSystemObjectHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.GET_RETRIEVAL_POINTERS, new System.IntPtr(&input), (uint)Marshal.SizeOf(input), pRetrievalPointers.Address, pRetrievalPointers.AllocatedSize32);
					} while (result == NtStatus.BUFFER_TOO_SMALL | result == NtStatus.BUFFER_OVERFLOW);
					
					if (result != NtStatus.END_OF_FILE)
					{
						if (!WindowsNT.IO.PrivateImplementationDetails.Wrapper.CheckAndThrowFileException(result, errorsNotToThrow))
						{
							return null;
						}
					}
					RETRIEVAL_POINTERS_BUFFER output = RETRIEVAL_POINTERS_BUFFER.FromPtr(pRetrievalPointers.Address);
					return output;
				}
			}
		}

		public static void MoveFile(SafeNtFileHandle volumeHandle, SafeNtFileHandle fileHandle, long startingVCN, long startingLCN, uint clusterCount)
		{
			IoStatusBlock ioStatusBlock;
			MOVE_FILE_DATA data = new MOVE_FILE_DATA() { FileHandle = fileHandle.DangerousGetHandle(), StartingVcn = startingVCN, StartingLcn = startingLCN, ClusterCount = clusterCount };
			unsafe
			{
				NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(volumeHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.MOVE_FILE, new System.IntPtr(&data), (uint)Marshal.SizeOf(data), System.IntPtr.Zero, 0);
				NtException.CheckAndThrowException(result);
			}
		}

		public static void SetZeroData(SafeNtFileHandle fileHandle, long offset, long beyondFinalZero)
		{
			IoStatusBlock ioStatusBlock;
			FILE_ZERO_DATA_INFORMATION data = new FILE_ZERO_DATA_INFORMATION() { FileOffset = offset, BeyondFinalZero = beyondFinalZero };
			unsafe
			{
				NtStatus result = Native.NtFsControlFile(NTInternal.CreateHandleRef(fileHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, FSCTL.SET_ZERO_DATA, new System.IntPtr(&data), (uint)Marshal.SizeOf(data), System.IntPtr.Zero, 0);
				NtException.CheckAndThrowException(result);
			}
		}


		public static DISPLAY_BRIGHTNESS GetBrightness(SafeNtFileHandle displayHandle)
		{
			IoStatusBlock ioStatusBlock;
			DISPLAY_BRIGHTNESS brightness = new DISPLAY_BRIGHTNESS();
			unsafe
			{
				NtStatus result = Native.NtDeviceIoControlFile(NTInternal.CreateHandleRef(displayHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, (uint)IOCTL.Video.IOCTL_VIDEO_QUERY_DISPLAY_BRIGHTNESS, System.IntPtr.Zero, 0, new System.IntPtr(&brightness), (uint)Marshal.SizeOf(brightness));
				bool success = result != 0;
				if (!success)
				{
					throw new NtException("Could not retrieve the display brightness.");
				}
			}
			return brightness;
		}

		public static byte[] GetSupportedBrightness(SafeNtFileHandle displayHandle)
		{
			IoStatusBlock ioStatusBlock;
			byte[] output = new byte[256];
			unsafe
			{
				fixed (byte* pOutput = output)
				{
					NtStatus result = Native.NtDeviceIoControlFile(NTInternal.CreateHandleRef(displayHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, (uint)IOCTL.Video.IOCTL_VIDEO_QUERY_SUPPORTED_BRIGHTNESS, System.IntPtr.Zero, 0, new System.IntPtr(pOutput), (uint)output.Length);
					NtException.CheckAndThrowException(result);
				}
			}
			return output;
		}
	}
}