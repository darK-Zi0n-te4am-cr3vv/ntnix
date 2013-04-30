using System.WindowsNT.IO.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using System.Collections.Generic;

namespace System.WindowsNT.IO
{
	[System.Diagnostics.DebuggerDisplay("Name = {Name}")]
	public class NtFile : NtObject, IEnumerable<FileFullEaInformation>
	{
		public NtFile(SafeNtFileHandle handle) : base() { this.Handle = handle; }

		public FileAccessMask AccessMask { get { return (FileAccessMask)this.ObjectAccessMask; } }

		public FileAlignment Alignment
		{ get { return Wrapper.NtQueryAlignmentInformationFile(this.Handle).AlignmentRequirement; } }

		public long AllocationSize
		{ get { return Wrapper.NtQueryStandardInformationFile(this.Handle).AllocationSize; } }

		public FileAttributes Attributes
		{
			get { return Wrapper.NtQueryBasicInformationFile(this.Handle).FileAttributes; }
			set { Wrapper.NtSetBasicInformationFile(this.Handle, new FileBasicInformation() { FileAttributes = value }); }
		}

		public System.DateTime ChangeTime
		{
			get { return System.DateTime.FromFileTime(Wrapper.NtQueryBasicInformationFile(this.Handle).ChangeTime); }
			set
			{
				FileBasicInformation fileInformation = Wrapper.NtQueryBasicInformationFile(this.Handle);
				fileInformation.ChangeTime = value.ToFileTime();
				Wrapper.NtSetBasicInformationFile(this.Handle, fileInformation);
			}
		}

		public System.DateTime CreationTime
		{
			get { return System.DateTime.FromFileTime(Wrapper.NtQueryBasicInformationFile(this.Handle).CreationTime); }
			set
			{
				FileBasicInformation fileInformation = Wrapper.NtQueryBasicInformationFile(this.Handle);
				fileInformation.CreationTime = value.ToFileTime();
				Wrapper.NtSetBasicInformationFile(this.Handle, fileInformation);
			}
		}

		public bool DeleteOnClose
		{
			get { return Wrapper.NtQueryStandardInformationFile(this.Handle).DeletePending; }
			set { Wrapper.NtSetDispositionInformationFile(this.Handle, new FileDispositionInformation() { DeleteFile = value }); }
		}

		/// <summary>The byte offset to the byte AFTER the last byte of the file; i.e., the byte offset to the next file.</summary>
		public long EndOfFile
		{
			get { return Wrapper.NtQueryStandardInformationFile(this.Handle).EndOfFile; }
			set { Wrapper.NtSetEndOfFileInformationFile(this.Handle, new FileEndOfFileInformation() { EndOfFile = value }); }
		}

		public FileFullEaInformation[] ExtendedAttributes
		{
			get
			{
				List<FileFullEaInformation> result = new List<FileFullEaInformation>();
				foreach (FileFullEaInformation eai in this)
				{
					result.Add(eai);
				}
				return result.ToArray();
			}
		}

		public bool Exists(string path, AllowedObjectAttributes attributes)
		{
			return Wrapper.NtFileExists(path, attributes);
		}

		public IEnumerator<FileFullEaInformation> GetEnumerator()
		{ return Wrapper.NtQueryEaFile(this.Handle); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{ return this.GetEnumerator(); }
		public override SafeNtHandle GenericHandle
		{ get { return this.Handle; } }

		public SafeNtFileHandle Handle { get; private set; }

		public long HardLinks
		{
			get { return Wrapper.NtQueryStandardInformationFile(this.Handle).NumberOfHardLinks; }
		}

		public long IndexNumber
		{ get { return Wrapper.NtQueryInternalInformationFile(this.Handle).IndexNumber; } }

		public bool IsDirectoryObject
		{ get { return Wrapper.NtQueryStandardInformationFile(this.Handle).Directory; } }

		public System.DateTime LastAccessTime
		{
			get { return System.DateTime.FromFileTime(Wrapper.NtQueryBasicInformationFile(this.Handle).LastAccessTime); }
			set
			{
				FileBasicInformation fileInformation = Wrapper.NtQueryBasicInformationFile(this.Handle);
				fileInformation.LastAccessTime = value.ToFileTime();
				Wrapper.NtSetBasicInformationFile(this.Handle, fileInformation);
			}
		}

		public System.DateTime LastWriteTime
		{
			get { return System.DateTime.FromFileTime(Wrapper.NtQueryBasicInformationFile(this.Handle).LastWriteTime); }
			set
			{
				FileBasicInformation fileInformation = Wrapper.NtQueryBasicInformationFile(this.Handle);
				fileInformation.LastWriteTime = value.ToFileTime();
				Wrapper.NtSetBasicInformationFile(this.Handle, fileInformation);
			}
		}

		public string Name
		{
			get
			{
				return Wrapper.NtQueryNameInformationFile(this.Handle).FileName;
			}
			set
			{
				Wrapper.NtSetRenameInformationFile(this.Handle, value, false, System.IntPtr.Zero);
			}
		}

		public NtDirectoryFile OpenParentDirectory(FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare share, FileCreateOptions createOptions)
		{
			FileOpenInformation openInformation;
			return NtDirectoryFile.Open(Path.GetParentDirectory(this.Name), accessMask, attributes, share, createOptions, out openInformation, ErrorsNotToThrow.ThrowAnyError);
		}

		public uint ReparseTag
		{
			get { return Wrapper.NtQueryAttributeTagInformationFile(this.Handle).ReparseTag; }
			set
			{
				FileAttributeTagInformation fileInformation = Wrapper.NtQueryAttributeTagInformationFile(this.Handle);
				fileInformation.ReparseTag = value;
				Wrapper.NtSetAttributeTagInformationFile(this.Handle, fileInformation);
			}
		}

		// Static

		public static NtFile Create(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, ulong? fileSize, FileAttributes fileAttributes, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			SafeNtFileHandle handle = Wrapper.NtCreateFile(null, name, accessMask, attributes, fileSize, fileAttributes, fileShare, fcd, createOptions, errorMode);
			return handle != null ? new NtFile(handle) : null;
		}

		public static FileBasicAttributeInformation GetAttributes(string path, AllowedObjectAttributes attributes)
		{
			return Wrapper.NtQueryAttributesFile(path, attributes);
		}

		public static FileFullAttributeInformation GetFullAttributes(string path, AllowedObjectAttributes attributes)
		{
			return Wrapper.NtQueryFullAttributesFile(path, attributes);
		}

		public static NtFile CreateByID(NtFile parent, long id, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			SafeNtFileHandle handle = Wrapper.NtCreateFile(parent != null ? parent.Handle : null, id, accessMask, attributes, null, FileAttributes.Normal, fileShare, fcd, createOptions, errorMode);
			return new NtFile(handle);
		}

		public static NtFile OpenByID(long id, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			SafeNtFileHandle handle = Wrapper.NtOpenFile(id, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			return new NtFile(handle);
		}
	}
}