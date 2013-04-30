using System.WindowsNT.IO.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.IO
{
	public class NtNonDirectoryFile : NtFile
	{
		public NtNonDirectoryFile(string name, FileAccessMask accessMask, FileShare fileShare, FileCreateOptions createOptions)
			: this(name, accessMask, AllowedObjectAttributes.CaseInsensitive, fileShare, createOptions, 0, FileAttributes.Normal, FileCreationDisposition.OpenOrCreate)
		{ }

		public NtNonDirectoryFile(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, ulong? fileSize, FileAttributes fileAttributes, FileCreationDisposition fcd)
			: base(Wrapper.NtCreateFile(null, name, accessMask, attributes, fileSize, fileAttributes, fileShare, fcd, (createOptions | FileCreateOptions.NonDirectoryFile) & ~FileCreateOptions.DirectoryFile, ErrorsNotToThrow.ThrowAnyError))
		{
		}

		protected internal NtNonDirectoryFile(SafeNtFileHandle handle)
			: base(handle)
		{
		}

		public NtNonDirectoryFile CreateStream(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, ulong fileSize, FileAttributes fileAttributes, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtCreateFile(this.Handle, name, accessMask, attributes, fileSize, fileAttributes, fileShare, fcd, createOptions, errorMode);
			return handle != null ? new NtNonDirectoryFile(handle) : null;
		}

		public void Flush()
		{
			Wrapper.NtFlushFile(this.Handle);
		}

		public FileStreamInformation[] GetStreams()
		{ return Wrapper.NtQueryStreamInformationFile(this.Handle, true); }

		public bool IsDeletePending
		{ get { return Wrapper.NtQueryStandardInformationFile(this.Handle).DeletePending; } }

		public NtNonDirectoryFile OpenStream(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(this.Handle, name, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			return handle != null ? new NtNonDirectoryFile(Handle) : null;
		}

		public FileStreamInformation[] TryGetStreams()
		{
			return Wrapper.NtQueryStreamInformationFile(this.Handle, false);
		}

		public byte[] ReadBytes(int bytes, long offset)
		{
			return Wrapper.NtReadFile(this.Handle, (uint)bytes, offset, null);
		}

		public byte[] ReadBytes(int bytes, long offset, uint key)
		{
			return Wrapper.NtReadFile(this.Handle, (uint)bytes, offset, key);
		}

		public void WriteBytes(byte[] bytes, long offset)
		{
			Wrapper.NtWriteFile(this.Handle, bytes, offset, null);
		}

		public void WriteBytes(byte[] bytes, long offset, uint key)
		{
			Wrapper.NtWriteFile(this.Handle, bytes, offset, key);
		}

		public void WriteBytesToEndOfFile(byte[] bytes)
		{
			Wrapper.NtWriteToEndOfFile(this.Handle, bytes);
		}

		public long ValidDataLength
		{ set { Wrapper.NtSetValidDataLengthInformationFile(this.Handle, new FileValidDataLengthInformation() { ValidDataLength = value }); } }

		public FileFsVolumeInformation VolumeInformation
		{ get { return Wrapper.NtQueryVolumeInformationFileVolume(this.Handle); } }

		public FileFsSizeInformation VolumeSizeInformation
		{ get { return Wrapper.NtQueryVolumeInformationFileSize(this.Handle); } }

		public FileFsFullSizeInformation VolumeFullSizeInformation
		{ get { return Wrapper.NtQueryVolumeInformationFileFullSize(this.Handle); } }

		public FileFsControlInformation VolumeControlInformation
		{
			get { return Wrapper.NtQueryVolumeInformationFileControl(this.Handle); }
			set { Wrapper.NtSetVolumeInformationFile(this.Handle, value); }
		}

		public string VolumeLabel
		{
			get { return Wrapper.NtQueryVolumeInformationFileLabel(this.Handle).VolumeLabel; }
			set { Wrapper.NtSetVolumeInformationFile(this.Handle, new FileFsLabelInformation() { VolumeLabel = value, VolumeLabelLength = (uint)value.Length * sizeof(char) }); }
		}

		public FileFsObjectIdInformation VolumeObjectID
		{
			get { return Wrapper.NtQueryVolumeInformationFileObjectID(this.Handle); }
			set { Wrapper.NtSetVolumeInformationFile(this.Handle, value); }
		}

		// Static =======================================================

		public static NtNonDirectoryFile Open(long id, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(id, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			if (handle != null)
			{
				return (createOptions & (FileCreateOptions.SynchronousIoAlert | FileCreateOptions.SynchronousIoNonAlert)) != 0 ? new NtSynchronizedFile(handle) : new NtNonDirectoryFile(handle);
			}
			else
			{
				return null;
			}
		}

		public static NtNonDirectoryFile Open(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(null, name, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			if (handle != null)
			{
				return (createOptions & (FileCreateOptions.SynchronousIoAlert | FileCreateOptions.SynchronousIoNonAlert)) != 0 ? new NtSynchronizedFile(handle) : new NtNonDirectoryFile(handle);
			}
			else
			{
				return null;
			}
		}

		public static string[] GetVolumes()
		{ return GetVolumes<System.Collections.Generic.List<string>>().ToArray(); }

		public static TList GetVolumes<TList>()
			where TList : System.Collections.Generic.ICollection<string>, new()
		{
			TList result = new TList();
			using (Directories.NtDirectoryObject dir = Directories.NtDirectoryObject.OpenDirectory(@"\DosDevices\Global", Directories.DirectoryAccessMask.QUERY, AllowedObjectAttributes.CaseInsensitive))
			{
				dir.EnumerateObjects(delegate(Directories.DirectoryInformation di)
				{
					if (di.ObjectName != null && System.Text.RegularExpressions.Regex.IsMatch(di.ObjectName, @"\p{Lu}:", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
					{
						result.Add(di.ObjectName);
					}
					return true;
				});
			}
			return result;
		}
	}
}