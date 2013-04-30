using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using Marshal = System.Runtime.InteropServices.Marshal;
using Other = System.WindowsNT.PrivateImplementationDetails.Wrapper;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.WindowsNT.IO.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		/// <returns>Returns <c>true</c> if success, otherwise <c>false</c>, meaning <c>null</c> should be returned.</returns>
		[System.Diagnostics.DebuggerHidden()]
		internal static bool CheckAndThrowFileException(NtStatus result, ErrorsNotToThrow errorMode)
		{
			bool success = NtException.IsSuccess(result);
			if (!success)
			{
				if ((errorMode == ErrorsNotToThrow.ReturnNullOnAnyError) |
					(((errorMode & ErrorsNotToThrow.FileTypeMismatch) != 0) && (result == NtStatus.NOT_A_DIRECTORY | result == NtStatus.FILE_IS_A_DIRECTORY)) |
					(((errorMode & ErrorsNotToThrow.NotFound) != 0) && (result == NtStatus.OBJECT_NAME_NOT_FOUND | result == NtStatus.OBJECT_PATH_NOT_FOUND)))
				{
					return false;
				}
				else
				{
					NtException.CheckAndThrowException(result);
				}
			}
			return true;
		}

		// Files ========================================================================================================

		public static SafeNtFileHandle NtCreateFile(SafeNtFileHandle previousHandle, string fileName, FileAccessMask accessMask, AllowedObjectAttributes attributes, ulong? fileSize, FileAttributes fileAttributes, FileShare fileShare, FileCreationDisposition fcd, FileCreateOptions createOptions, ErrorsNotToThrow errorMode)
		{
			if (((createOptions & (FileCreateOptions.SynchronousIoAlert | FileCreateOptions.SynchronousIoNonAlert)) != 0) && ((accessMask & FileAccessMask.Synchronize) == 0))
			{ throw new System.ArgumentException("Synchronous create option requested without synchronous access."); }

			UnicodeString wFileName = (UnicodeString)fileName;
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wFileName, attributes, null, null);
				System.IntPtr fileHandle;
				IoStatusBlock ioStatusBlock;
				ulong* pFileSize = null;
				ulong tempFileSize = 0;
				if (fileSize != null)
				{
					tempFileSize = fileSize.Value;
					pFileSize = &tempFileSize;
				}
				NtStatus result = Native.NtCreateFile(out fileHandle, accessMask, ref oa, out ioStatusBlock, pFileSize, fileAttributes, fileShare, fcd, createOptions, System.IntPtr.Zero, 0);
				System.GC.KeepAlive(previousHandle);
				wFileName.Dispose();
				if (!CheckAndThrowFileException(result, errorMode))
					return null;
				return new SafeNtFileHandle(fileHandle);
			}
		}

		public static SafeNtFileHandle NtCreateFile(SafeNtFileHandle previousHandle, long id, FileAccessMask accessMask, AllowedObjectAttributes attributes, ulong? fileSize, FileAttributes fileAttributes, FileShare fileShare, FileCreationDisposition fcd, FileCreateOptions createOptions, ErrorsNotToThrow errorMode)
		{
			if (((accessMask & FileAccessMask.Synchronize) != 0) != ((createOptions & (FileCreateOptions.SynchronousIoAlert | FileCreateOptions.SynchronousIoNonAlert)) != 0))
			{ throw new System.ArgumentException("Synchronous create option requested without synchronous access."); }
			createOptions |= FileCreateOptions.OpenByFileID;
			unsafe
			{
				UnicodeString wFileName = new UnicodeString(sizeof(long), sizeof(long), (char*)&id, false);
				ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wFileName, attributes, null, null);
				System.IntPtr fileHandle;
				IoStatusBlock ioStatusBlock;
				ulong tempFileSize = 0;
				ulong* pFileSize = null;
				if (fileSize != null)
				{
					tempFileSize = fileSize.Value;
					pFileSize = &tempFileSize;
				}
				NtStatus result = Native.NtCreateFile(out fileHandle, accessMask, ref oa, out ioStatusBlock, pFileSize, fileAttributes, fileShare, fcd, createOptions, System.IntPtr.Zero, 0);
				System.GC.KeepAlive(previousHandle);
				if (!CheckAndThrowFileException(result, errorMode))
					return null;
				return new SafeNtFileHandle(fileHandle);
			}
		}

		public static void NtDeleteFile(SafeNtFileHandle fileHandle)
		{
			unsafe
			{
				ObjectAttributes oa = new ObjectAttributes(fileHandle.DangerousGetHandle(), null, AllowedObjectAttributes.None, null, null);
				NtStatus result = Native.NtDeleteFile(ref oa);
				System.GC.KeepAlive(fileHandle);
				NtException.CheckAndThrowException(result);
			}
		}

		public static void NtDeleteFile(string fileName)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wFileName, AllowedObjectAttributes.None, null, null);
					NtStatus result = Native.NtDeleteFile(ref oa);
					NtException.CheckAndThrowException(result);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		public static IEnumerator<FileFullEaInformation> NtQueryEaFile(SafeNtFileHandle fileHandle)
		{
			IoStatusBlock ioStatusBlock;
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(FileFullEaInformation))))
			{
				NtStatus result;
				bool restart = true;
				do
				{
					result = Native.NtQueryEaFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, false, System.IntPtr.Zero, 0, System.IntPtr.Zero, restart);
					if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						FileFullEaInformation information = (FileFullEaInformation)Marshal.PtrToStructure(pBuffer.Address, typeof(FileFullEaInformation));
						pBuffer.ReAlloc(pBuffer.AllocatedSize32 + (uint)information.EaNameLength + information.EaValueLength);
						result = Native.NtQueryEaFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, false, System.IntPtr.Zero, 0, System.IntPtr.Zero, restart);
					}
					if (result == NtStatus.NO_EAS_ON_FILE | result == NtStatus.NO_MORE_EAS)
					{
						break;
					}
					NtException.CheckAndThrowException(result);
					foreach (FileFullEaInformation fi in FileFullEaInformation.FromPtr(pBuffer.Address))
					{
						yield return fi;
					}
					restart = false;
				} while (true);
			}
		}

		public static void NtEnumerateEaFile(SafeNtFileHandle fileHandle, System.Predicate<FileFullEaInformation> action)
		{
			IoStatusBlock ioStatusBlock;
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(FileFullEaInformation))))
			{
				NtStatus result;
				bool @continue = true;
				bool restart = true;
				do
				{
					unsafe
					{
						result = Native.NtQueryEaFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, false, System.IntPtr.Zero, 0, null, restart);
					}
					if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						FileFullEaInformation information = (FileFullEaInformation)Marshal.PtrToStructure(pBuffer.Address, typeof(FileFullEaInformation));
						pBuffer.ReAlloc(pBuffer.AllocatedSize32 + (uint)information.EaNameLength + information.EaValueLength);
						unsafe
						{
							result = Native.NtQueryEaFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, false, System.IntPtr.Zero, 0, null, restart);
						}
					}
					if (result == NtStatus.NO_EAS_ON_FILE | result == NtStatus.NO_MORE_EAS)
					{
						break;
					}
					NtException.CheckAndThrowException(result);
					foreach (FileFullEaInformation fi in FileFullEaInformation.FromPtr(pBuffer.Address))
					{
						if (!action(fi))
						{
							@continue = false;
							break;
						}
					}
					restart = false;
				} while (@continue);
			}
		}

		private static IEnumerable<T> NtQueryDirectory<T>(SafeNtFileHandle directoryHandle, FileInformationClass fileInformationClass, string pattern, System.Converter<System.IntPtr, T[]> converter)
		{
			IoStatusBlock ioStatusBlock;
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(T))))
			{
				UnicodeString wPattern = default(UnicodeString);
				if (pattern != null)
				{
					wPattern = (UnicodeString)pattern;
				}
				GCHandle? hPattern = pattern != null ? (GCHandle?)GCHandle.Alloc(wPattern, GCHandleType.Pinned) : null;
				try
				{
					NtStatus result;
					bool restart = true;
					do
					{
						result = Native.NtQueryDirectoryFile(NTInternal.CreateHandleRef(directoryHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass, false, hPattern != null ? hPattern.Value.AddrOfPinnedObject() : System.IntPtr.Zero, restart);
						while (ioStatusBlock.status == NtStatus.BUFFER_OVERFLOW | ioStatusBlock.status == NtStatus.BUFFER_TOO_SMALL)
						{
							pBuffer.ReAlloc(pBuffer.AllocatedSize32 << 1);
							result = Native.NtQueryDirectoryFile(NTInternal.CreateHandleRef(directoryHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass, false, hPattern != null ? hPattern.Value.AddrOfPinnedObject() : System.IntPtr.Zero, restart);
						}
						if (ioStatusBlock.status == NtStatus.NO_MORE_FILES | ioStatusBlock.status == NtStatus.NO_SUCH_FILE)
						{
							break;
						}
						NtException.CheckAndThrowException(result);

						T[] arr = converter(pBuffer.Address);
						foreach (T v in arr)
						{
							yield return v;
						}

						restart = false;
					} while (true);
				}
				finally
				{
					if (hPattern != null)
						hPattern.Value.Free();
				}
			}
		}

		public static IEnumerable<string> NtQueryDirectoryFileNames(SafeNtFileHandle directoryHandle, string pattern)
		{
			foreach (FileNamesInformation fni in NtQueryDirectory<FileNamesInformation>(directoryHandle, FileInformationClass.FileNamesInformation, pattern, FileNamesInformation.FromPtr))
			{
				yield return fni.FileName;
			}
		}

		public static IEnumerable<FileDirectoryInformation> NtQueryDirectoryInformation(SafeNtFileHandle directoryHandle, string pattern)
		{
			return NtQueryDirectory<FileDirectoryInformation>(directoryHandle, FileInformationClass.FileDirectoryInformation, pattern, FileDirectoryInformation.FromPtr);
		}

		public static IEnumerable<FileFullDirectoryInformation> NtQueryDirectoryFullInformation(SafeNtFileHandle directoryHandle, string pattern)
		{
			return NtQueryDirectory<FileFullDirectoryInformation>(directoryHandle, FileInformationClass.FileFullDirectoryInformation, pattern, FileFullDirectoryInformation.FromPtr);
		}

		public static IEnumerable<FileBothDirectoryInformation> NtQueryDirectoryBothInformation(SafeNtFileHandle directoryHandle, string pattern)
		{
			return NtQueryDirectory<FileBothDirectoryInformation>(directoryHandle, FileInformationClass.FileBothDirectoryInformation, pattern, FileBothDirectoryInformation.FromPtr);
		}

		public static IEnumerable<FileIdFullDirInformation> NtQueryDirectoryIDFullInformation(SafeNtFileHandle directoryHandle, string pattern)
		{
			return NtQueryDirectory<FileIdFullDirInformation>(directoryHandle, FileInformationClass.FileIdFullDirectoryInformation, pattern, FileIdFullDirInformation.FromPtr);
		}

		public static IEnumerable<FileIdBothDirInformation> NtQueryDirectoryIDBothInformation(SafeNtFileHandle directoryHandle, string pattern)
		{
			return NtQueryDirectory<FileIdBothDirInformation>(directoryHandle, FileInformationClass.FileIdBothDirectoryInformation, pattern, FileIdBothDirInformation.FromPtr);
		}

#if ENABLE_ENUMERATORS

		private static void NtEnumerateDirectory<T>(SafeNtFileSystemObjectHandle directoryHandle, FileInformationClass fileInformationClass, string pattern, System.Predicate<System.IntPtr> action)
		{
			IoStatusBlock ioStatusBlock;
			using (UnmanagedPointer pBuffer = new UnmanagedPointer(Marshal.SizeOf(typeof(T))))
			{
				UnicodeString wPattern = default(UnicodeString);
				if (pattern != null)
				{
					wPattern = (UnicodeString)pattern;
				}
				try
				{
					NtStatus result;
					bool @continue = true;
					bool restart = true;
					do
					{
						unsafe
						{
							result = Native.NtQueryDirectoryFile(Internal.CreateHandleRef(directoryHandle), System.IntPtr.Zero, null, System.IntPtr.Zero, out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass, false, new System.IntPtr(&wPattern), restart);
							while (ioStatusBlock.status == NtStatus.BUFFER_OVERFLOW | ioStatusBlock.status == NtStatus.BUFFER_TOO_SMALL)
							{
								pBuffer.ReAlloc(pBuffer.AllocatedSize32 << 1);
								result = Native.NtQueryDirectoryFile(Internal.CreateHandleRef(directoryHandle), System.IntPtr.Zero, null, System.IntPtr.Zero, out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass, false, new System.IntPtr(&wPattern), restart);
							}
							if (ioStatusBlock.status == NtStatus.NO_MORE_FILES | ioStatusBlock.status == NtStatus.NO_SUCH_FILE)
							{
								break;
							}
							NtException.CheckAndThrowException(result);
							if (!action(pBuffer.Address))
							{
								@continue = false;
								break;
							}
							restart = false;
						}
					} while (@continue);
				}
				finally
				{
					if (pattern != null)
						wPattern.Dispose();
				}
			}
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileNamesInformation> action)
		{
			NtEnumerateDirectory<FileNamesInformation>(directoryHandle, FileInformationClass.FileNamesInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileNamesInformation info in FileNamesInformation.FromPtr(ptr))
					if (!action(info))
						return false;
				return true;
			});
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileDirectoryInformation> action)
		{
			NtEnumerateDirectory<FileDirectoryInformation>(directoryHandle, FileInformationClass.FileDirectoryInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileDirectoryInformation info in FileDirectoryInformation.FromPtr(ptr))
				{
					if (!action(info))
						return false;
				}
				return true;
			});
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileFullDirectoryInformation> action)
		{
			NtEnumerateDirectory<FileFullDirectoryInformation>(directoryHandle, FileInformationClass.FileFullDirectoryInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileFullDirectoryInformation info in FileFullDirectoryInformation.FromPtr(ptr))
					if (!action(info))
						return false;
				return true;
			});
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileBothDirectoryInformation> action)
		{
			NtEnumerateDirectory<FileBothDirectoryInformation>(directoryHandle, FileInformationClass.FileBothDirectoryInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileBothDirectoryInformation info in FileBothDirectoryInformation.FromPtr(ptr))
					if (!action(info))
						return false;
				return true;
			});
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileIdFullDirInformation> action)
		{
			NtEnumerateDirectory<FileIdFullDirInformation>(directoryHandle, FileInformationClass.FileIdFullDirectoryInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileIdFullDirInformation info in FileIdFullDirInformation.FromPtr(ptr))
					if (!action(info))
						return false;
				return true;
			});
		}

		public static void NtEnumerateDirectory(SafeNtFileSystemObjectHandle directoryHandle, string pattern, System.Predicate<FileIdBothDirInformation> action)
		{
			NtEnumerateDirectory<FileIdBothDirInformation>(directoryHandle, FileInformationClass.FileIdBothDirectoryInformation, pattern, delegate(System.IntPtr ptr)
			{
				foreach (FileIdBothDirInformation info in FileIdBothDirInformation.FromPtr(ptr))
					if (!action(info))
						return false;
				return true;
			});
		}
#endif
		public static bool NtFileExists(string fileName, AllowedObjectAttributes attributes)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wFileName, attributes, null, null);
					FileBasicInformation info;
					NtStatus result = Native.NtQueryAttributesFile(ref oa, out info);
					return NtException.IsSuccess(result);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		public static void NtFlushFile(SafeNtFileHandle fileHandle)
		{
			IoStatusBlock ioStatusBlock = new IoStatusBlock();
			NtStatus result = Native.NtFlushBuffersFile(NTInternal.CreateHandleRef(fileHandle), ref ioStatusBlock);
			NtException.CheckAndThrowException(result);
		}

		public static uint NtLockFile(SafeNtFileHandle fileHandle, long offset, long length, bool exclusive)
		{
			IoStatusBlock ioStatusBlock;
			uint key;
			NtStatus result = Native.NtLockFile(NTInternal.CreateHandleRef(fileHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, ref offset, ref length, out key, false, exclusive);
			NtException.CheckAndThrowException(result);
			return key;
		}

		public static void NtNotifyChangeDirectoryFile(SafeNtFileHandle directoryHandle, SafeNtEventHandle eventHandle, FileNotifyFilter notifyFilter, bool watchTree, System.Action<FileNotifyInformation[]> notify)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = 1024;
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)inputLength);
			NtStatus result = Native.NtNotifyChangeDirectoryFile(NTInternal.CreateHandleRef(directoryHandle), NTInternal.CreateHandleRef(eventHandle),
				delegate(System.IntPtr ptr, ref IoStatusBlock iosb, uint reserved)
				{
					try
					{
						if (System.Threading.Thread.CurrentThread.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
						{
							notify(FileNotifyInformation.FromPtr(ptr));
						}
					}
					finally
					{
						Marshal.FreeHGlobal(ptr);
					}
				}, pBuffer, out ioStatusBlock, pBuffer, inputLength, notifyFilter, watchTree);
			NtException.CheckAndThrowException(result);
			//return FileNotifyInformation.FromPtr(pBuffer.Address);
		}

		public static SafeNtFileHandle NtOpenFile(SafeNtFileHandle previousHandle, string fileName, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileCreateOptions createOptions, FileShare share, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wFileName, attributes, null, null);
					System.IntPtr fileHandle;
					IoStatusBlock ioStatusBlock;
					NtStatus result = Native.NtOpenFile(out fileHandle, accessMask, ref oa, out ioStatusBlock, share, createOptions);
					System.GC.KeepAlive(previousHandle);
					openInformation = ioStatusBlock.Information.FileOpenInformation;
					if (!CheckAndThrowFileException(result, errorMode))
						return null;
					return new SafeNtFileHandle(fileHandle);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		public static SafeNtFileHandle NtOpenFile(long id, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileCreateOptions createOptions, FileShare share, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.OpenByFileID;
			string idStr = id.ToString();
			UnicodeString wID = (UnicodeString)idStr;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wID, attributes, null, null);
					System.IntPtr fileHandle;
					IoStatusBlock ioStatusBlock;
					NtStatus result = Native.NtOpenFile(out fileHandle, accessMask, ref oa, out ioStatusBlock, share, createOptions);
					openInformation = ioStatusBlock.Information.FileOpenInformation;
					if (!CheckAndThrowFileException(result, errorMode))
						return null;
					return new SafeNtFileHandle(fileHandle);
				}
			}
			finally
			{
				wID.Dispose();
			}
		}
		
		public static FileBasicAttributeInformation NtQueryAttributesFile(SafeNtFileHandle parentDirectoryHandle, string fileName, AllowedObjectAttributes attributes)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(parentDirectoryHandle.DangerousGetHandle(), &wFileName, attributes, null, null);
					FileBasicInformation info;
					NtStatus result = Native.NtQueryAttributesFile(ref oa, out info);
					System.GC.KeepAlive(parentDirectoryHandle);
					NtException.CheckAndThrowException(result);
					return new FileBasicAttributeInformation(info);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		public static FileBasicAttributeInformation NtQueryAttributesFile(string fileName, AllowedObjectAttributes attributes)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wFileName, attributes, null, null);
					FileBasicInformation info;
					NtStatus result = Native.NtQueryAttributesFile(ref oa, out info);
					NtException.CheckAndThrowException(result);
					return new FileBasicAttributeInformation(info);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		public static FileFullAttributeInformation NtQueryFullAttributesFile(string fileName, AllowedObjectAttributes attributes)
		{
			UnicodeString wFileName = (UnicodeString)fileName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wFileName, attributes, null, null);
					FileNetworkOpenInformation info;
					NtStatus result = Native.NtQueryFullAttributesFile(ref oa, out info);
					NtException.CheckAndThrowException(result);
					return new FileFullAttributeInformation(info);
				}
			}
			finally
			{
				wFileName.Dispose();
			}
		}

		private static T NtQueryInformationFile<T>(SafeNtFileHandle fileHandle, FileInformationClass fileInformationClass, System.Converter<System.IntPtr, T> marshaler)
		{
			IoStatusBlock ioStatusBlock;
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(T))))
			{
				NtStatus result;
				unsafe
				{
					result = Native.NtQueryInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass);
					while (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						pBuffer.ReAlloc(((pBuffer.AllocatedSize32 << 1) + 1) & (unchecked((uint)~0) >> 1));
						result = Native.NtQueryInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, fileInformationClass);
					}
					NtException.CheckAndThrowException(result);
					T output = marshaler(pBuffer.Address);
					return output;
				}
			}
		}

		public static FileAlignmentInformation NtQueryAlignmentInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileAlignmentInformation>(fileHandle, FileInformationClass.FileAlignmentInformation, FileAlignmentInformation.FromPtr); }

		public static FileAttributeTagInformation NtQueryAttributeTagInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileAttributeTagInformation>(fileHandle, FileInformationClass.FileAttributeTagInformation, FileAttributeTagInformation.FromPtr); }

		public static FileBasicInformation NtQueryBasicInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileBasicInformation>(fileHandle, FileInformationClass.FileBasicInformation, FileBasicInformation.FromPtr); }

		public static FileInternalInformation NtQueryInternalInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileInternalInformation>(fileHandle, FileInformationClass.FileInternalInformation, FileInternalInformation.FromPtr); }

		public static FileNameInformation NtQueryNameInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileNameInformation>(fileHandle, FileInformationClass.FileNameInformation, FileNameInformation.FromPtr); }

		public static FileNetworkOpenInformation NtQueryNetworkOpenInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileNetworkOpenInformation>(fileHandle, FileInformationClass.FileNetworkOpenInformation, FileNetworkOpenInformation.FromPtr); }

		public static FilePositionInformation NtQueryPositionInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FilePositionInformation>(fileHandle, FileInformationClass.FilePositionInformation, FilePositionInformation.FromPtr); }

		public static FileStandardInformation NtQueryStandardInformationFile(SafeNtFileHandle fileHandle)
		{ return NtQueryInformationFile<FileStandardInformation>(fileHandle, FileInformationClass.FileStandardInformation, FileStandardInformation.FromPtr); }

		public static FileStreamInformation[] NtQueryStreamInformationFile(SafeNtFileHandle fileHandle, bool throwErrors)
		{
			IoStatusBlock ioStatusBlock;
			using (HGlobal pBuffer = new HGlobal(Marshal.SizeOf(typeof(FileStreamInformation))))
			{
				NtStatus result;
				unsafe
				{
					result = Native.NtQueryInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, FileInformationClass.FileStreamInformation);
					while (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						pBuffer.ReAlloc((pBuffer.AllocatedSize32 << 1) + 1);
						result = Native.NtQueryInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, FileInformationClass.FileStreamInformation);
					}
					if (!NtException.IsSuccess(result) & !throwErrors)
					{
						return null;
					}
					else
					{
						NtException.CheckAndThrowException(result);
						FileStreamInformation[] output;
						if (ioStatusBlock.Information.BytesWritten == 0)
						{ output = new FileStreamInformation[0]; }
						else
						{ output = FileStreamInformation.FromPtr(pBuffer.Address); }
						return output;
					}
				}
			}
		}

		private static T NtQueryVolumeInformationFile<T>(SafeNtFileHandle volumeHandle, FSInformationClass fsInformationClass, System.Converter<System.IntPtr, T> marshaler)
		{
			uint inputLength = (uint)Marshal.SizeOf(typeof(T));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				IoStatusBlock ioStatusBlock;
				NtStatus result;
				do
				{
					inputLength = (inputLength << 1) + 1;
					pBuffer = Marshal.ReAllocHGlobal(pBuffer, new IntPtr(inputLength));
					result = Native.NtQueryVolumeInformationFile(NTInternal.CreateHandleRef(volumeHandle), out ioStatusBlock, pBuffer, inputLength, fsInformationClass);
				} while (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL);
				NtException.CheckAndThrowException(result);
				return marshaler(pBuffer);
			}
			finally
			{
				Marshal.FreeHGlobal(pBuffer);
			}
		}

		public static FileFsAttributeInformation NtQueryVolumeInformationFileAttribute(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsAttributeInformation>(volumeHandle, FSInformationClass.FileFsAttributeInformation, FileFsAttributeInformation.FromPtr); }

		public static FileFsControlInformation NtQueryVolumeInformationFileControl(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsControlInformation>(volumeHandle, FSInformationClass.FileFsControlInformation, FileFsControlInformation.FromPtr); }

		public static FileFsFullSizeInformation NtQueryVolumeInformationFileFullSize(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsFullSizeInformation>(volumeHandle, FSInformationClass.FileFsFullSizeInformation, FileFsFullSizeInformation.FromPtr); }

		public static FileFsLabelInformation NtQueryVolumeInformationFileLabel(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsLabelInformation>(volumeHandle, FSInformationClass.FileFsLabelInformation, FileFsLabelInformation.FromPtr); }

		public static FileFsObjectIdInformation NtQueryVolumeInformationFileObjectID(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsObjectIdInformation>(volumeHandle, FSInformationClass.FileFsObjectIdInformation, FileFsObjectIdInformation.FromPtr); }

		public static FileFsSizeInformation NtQueryVolumeInformationFileSize(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsSizeInformation>(volumeHandle, FSInformationClass.FileFsSizeInformation, FileFsSizeInformation.FromPtr); }

		public static FileFsVolumeInformation NtQueryVolumeInformationFileVolume(SafeNtFileHandle volumeHandle)
		{ return NtQueryVolumeInformationFile<FileFsVolumeInformation>(volumeHandle, FSInformationClass.FileFsVolumeInformation, FileFsVolumeInformation.FromPtr); }

		public static void NtSetVolumeInformationFile(SafeNtFileHandle volumeHandle, FileFsLabelInformation info)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtSetVolumeInformationFile(NTInternal.CreateHandleRef(volumeHandle), out ioStatusBlock, ref info, info.GetMarshaledSize(), FSInformationClass.FileFsLabelInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetVolumeInformationFile(SafeNtFileHandle volumeHandle, FileFsControlInformation info)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtSetVolumeInformationFile(NTInternal.CreateHandleRef(volumeHandle), out ioStatusBlock, ref info, (uint)Marshal.SizeOf(info), FSInformationClass.FileFsLabelInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetVolumeInformationFile(SafeNtFileHandle volumeHandle, FileFsObjectIdInformation info)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtSetVolumeInformationFile(NTInternal.CreateHandleRef(volumeHandle), out ioStatusBlock, ref info, (uint)Marshal.SizeOf(info), FSInformationClass.FileFsLabelInformation);
			NtException.CheckAndThrowException(result);
		}

		public static byte[] NtReadFile(SafeNtFileHandle fileHandle, uint bytesToRead)
		{ unsafe { return NtReadFile(fileHandle, bytesToRead, (long)FileOffsetOptions.UserFilePointerPosition, null); } }

		/// <summary>Note: RESETS the current byte offset!</summary>
		public static byte[] NtReadFile(SafeNtFileHandle fileHandle, uint bytesToRead, long offset, uint? key)
		{
			IoStatusBlock ioStatusBlock;
			byte[] bytes;
			using (HGlobal pBuffer = new HGlobal(bytesToRead))
			{
				unsafe
				{
					uint k = key.GetValueOrDefault();
					uint* pKey = key.HasValue ? &k : null;
					NtStatus result = Native.NtReadFile(NTInternal.CreateHandleRef(fileHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, pBuffer.Address, pBuffer.AllocatedSize32, &offset, pKey);
					if (result != NtStatus.END_OF_FILE)
					{
						NtException.CheckAndThrowException(result);
					}
					bytes = new byte[ioStatusBlock.Information.BytesRead];
				}
				Marshal.Copy(pBuffer.Address, bytes, 0, bytes.Length);
				return bytes;
			}
		}

		public static Events.NtEvent NtBeginReadFile(NtNonDirectoryFile file, object state, byte[] buffer, long offset, uint? key)
		{
			IoStatusBlock ioStatusBlock;
			uint k = key.GetValueOrDefault();
			Events.NtEvent @event = Events.NtEvent.Create(null, Events.EventAccessMask.AllAccess, Events.EventType.SynchronizationEvent);
			unsafe
			{
				uint* pKey = key.HasValue ? &k : null;
				GCHandle hBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				try
				{
					NtStatus result = Native.NtReadFile(NTInternal.CreateHandleRef(file.Handle), NTInternal.CreateHandleRef(@event != null ? @event.Handle : null),
						delegate(System.IntPtr hBuffIntPtr, ref IoStatusBlock iosb, uint reserved)
						{
							GCHandle hBuff = GCHandle.FromIntPtr(hBuffIntPtr);
							try
							{
								NtException.CheckAndThrowException(iosb.status);
							}
							finally
							{
								hBuff.Free();
							}
						}, GCHandle.ToIntPtr(hBuffer), out ioStatusBlock, hBuffer.AddrOfPinnedObject(), (uint)buffer.Length, &offset, pKey);
					if (result != NtStatus.END_OF_FILE)
					{
						NtException.CheckAndThrowException(result);
					}
					return @event;
				}
				catch (Exception)
				{
					hBuffer.Free();
					throw;
				}
			}
		}

		public static void NtSetAlignmentInformationFile(SafeNtFileHandle fileHandle, FileAlignmentInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileAlignmentInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetAttributeTagInformationFile(SafeNtFileHandle fileHandle, FileAttributeTagInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileAttributeTagInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetBasicInformationFile(SafeNtFileHandle fileHandle, FileBasicInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileBasicInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetDispositionInformationFile(SafeNtFileHandle fileHandle, FileDispositionInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileDispositionInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetEndOfFileInformationFile(SafeNtFileHandle fileHandle, FileEndOfFileInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileEndOfFileInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetFullEaInformationFile(SafeNtFileHandle fileHandle, FileFullEaInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileFullEaInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetNetworkInformationFile(SafeNtFileHandle fileHandle, FileNetworkOpenInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileNetworkOpenInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetPositionInformationFile(SafeNtFileHandle fileHandle, FilePositionInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FilePositionInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtSetRenameInformationFile(SafeNtFileHandle fileHandle, string newName, bool replace, System.IntPtr rootDirectory)
		{
			unsafe
			{
				FileRenameInformation renameInfoTemp = new FileRenameInformation() { RootDirectory = rootDirectory, ReplaceIfExists = replace, FileNameLength = sizeof(char) * (uint)newName.Length };
				uint inputLength = (uint)Marshal.SizeOf(typeof(FileRenameInformation)) + renameInfoTemp.FileNameLength;
				FileRenameInformation* pRenameInfo = (FileRenameInformation*)Marshal.AllocHGlobal((int)inputLength);
				try
				{
					Marshal.StructureToPtr(renameInfoTemp, new System.IntPtr(pRenameInfo), false);
					fixed (char* pName = newName)
					{
						for (int i = 0; i < newName.Length; i++)
						{
							pRenameInfo->FileName[i] = pName[i];
						}
					}
					IoStatusBlock ioStatusBlock;
					NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, new System.IntPtr(pRenameInfo), inputLength, FileInformationClass.FileRenameInformation);
					NtException.CheckAndThrowException(result);
				}
				finally
				{
					Marshal.FreeHGlobal(new System.IntPtr(pRenameInfo));
				}
			}
		}

		public static void NtSetValidDataLengthInformationFile(SafeNtFileHandle fileHandle, FileValidDataLengthInformation fileInformation)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = (uint)Marshal.SizeOf(fileInformation);
			NtStatus result = Native.NtSetInformationFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref fileInformation, inputLength, FileInformationClass.FileValidDataLengthInformation);
			NtException.CheckAndThrowException(result);
		}

		public static void NtUnlockFile(SafeNtFileHandle fileHandle, long offset, long length, uint key)
		{
			IoStatusBlock ioStatusBlock;
			NtStatus result = Native.NtUnlockFile(NTInternal.CreateHandleRef(fileHandle), out ioStatusBlock, ref offset, ref length, ref key);
			NtException.CheckAndThrowException(result);
		}

		public static void NtWriteFile(SafeNtFileHandle fileHandle, byte[] bytes)
		{ unsafe { NtWriteFile(fileHandle, bytes, (long)FileOffsetOptions.UserFilePointerPosition, null); } }

		public static void NtWriteToEndOfFile(SafeNtFileHandle fileHandle, byte[] bytes)
		{ unsafe { NtWriteFile(fileHandle, bytes, (long)FileOffsetOptions.WriteToEndOfFile, null); } }

		/// <summary>Note: RESETS the current byte offset!</summary>
		public static void NtWriteFile(SafeNtFileHandle fileHandle, byte[] bytes, long offset, uint? key)
		{
			IoStatusBlock ioStatusBlock;
			uint k = key ?? 0;
			unsafe
			{
				NtStatus result = Native.NtWriteFile(NTInternal.CreateHandleRef(fileHandle), NTInternal.NullHandleRef, null, System.IntPtr.Zero, out ioStatusBlock, bytes, (uint)bytes.Length, &offset, key.HasValue ? &k : null);
				if (result != NtStatus.END_OF_FILE)
				{
					NtException.CheckAndThrowException(result);
				}
			}
		}



		// RTL ============================

		public static string RtlDosPathNameToNtPathName(string dosPath)
		{
			IntPtr pInfo;
			UnicodeString pathName;
			string part;
			bool result = IO.PrivateImplementationDetails.Native.RtlDosPathNameToNtPathName_U(dosPath, out pathName, out part, out pInfo);
			return result ? pathName.ToString() : null;
		}
	}
}