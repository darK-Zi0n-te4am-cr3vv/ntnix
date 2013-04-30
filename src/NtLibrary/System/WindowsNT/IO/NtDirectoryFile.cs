using System.WindowsNT.IO.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using System.Collections.Generic;
namespace System.WindowsNT.IO
{
	public class NtDirectoryFile : NtFile, IEnumerable<string>, IEnumerable<DirectoryFileInformation>, IEnumerable<DirectoryFileFullInformation>, IEnumerable<DirectoryFileBothInformation>, IEnumerable<DirectoryFileIDFullInformation>, IEnumerable<DirectoryFileIDBothInformation>
	{
		//private System.Threading.Thread eventListener;

		protected NtDirectoryFile(SafeNtFileHandle handle)
			: base(handle)
		{
			/*
			this.eventListener = new System.Threading.Thread(delegate()
				{
					do
					{
						this.Wait();
					} while (true);
				})
				{
					IsBackground = true
				};
			this.eventListener.Start();
			*/
		}


		public event System.EventHandler<NotifyEventArgs> Changed
		{
			add { this.AddChangeHandler(Events.NtEvent.Create(null, System.WindowsNT.Events.EventAccessMask.AllAccess, System.WindowsNT.Events.EventType.NotificationEvent), false, value); }
			remove { }
		}


		public NtDirectoryFile CreateDirectory(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, FileAttributes fileAttributes, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			createOptions &= ~FileCreateOptions.NonDirectoryFile;
			createOptions |= FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtCreateFile(this.Handle, name, accessMask, attributes, 0, fileAttributes, fileShare, fcd, createOptions, errorMode);
			return handle != null ? new NtDirectoryFile(handle) : null;
		}

		public NtNonDirectoryFile CreateFile(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, ulong fileSize, FileAttributes fileAttributes, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtCreateFile(this.Handle, name, accessMask, attributes, fileSize, fileAttributes, fileShare, fcd, createOptions, errorMode);
			return handle != null ? new NtNonDirectoryFile(handle) : null;
		}

#if ENABLE_ENUMERATORS

		public void ForEachFileName(string pattern, System.Predicate<string> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, (FileNamesInformation name) => action(name.FileName)); }

		public void ForEachFileInformation(string pattern, System.Predicate<DirectoryFileInformation> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, fdi => action(new DirectoryFileInformation(fdi))); }

		public void ForEachFileInformation(string pattern, System.Predicate<DirectoryFileFullInformation> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, fdi => action(new DirectoryFileFullInformation(fdi))); }

		public void ForEachFileInformation(string pattern, System.Predicate<DirectoryFileBothInformation> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, fdi => action(new DirectoryFileBothInformation(fdi))); }

		public void ForEachFileIDInformation(string pattern, System.Predicate<DirectoryFileIDFullInformation> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, fdi => action(new DirectoryFileIDFullInformation(fdi))); }

		public void ForEachFileIDInformation(string pattern, System.Predicate<DirectoryFileIDBothInformation> action)
		{ Wrapper.NtEnumerateDirectory(this.Handle, pattern, fdi => action(new DirectoryFileIDBothInformation(fdi))); }

#endif

		public IEnumerable<string> GetFileNamesEnumerator(string pattern)
		{ return Wrapper.NtQueryDirectoryFileNames(this.Handle, pattern); }

		public IEnumerable<DirectoryFileInformation> GetFileInformationEnumerator(string pattern)
		{
			foreach (var info in Wrapper.NtQueryDirectoryInformation(this.Handle, pattern))
			{
				yield return new DirectoryFileInformation(info);
			}
		}

		public IEnumerable<DirectoryFileFullInformation> GetFileFullInformationEnumerator(string pattern)
		{
			foreach (var info in Wrapper.NtQueryDirectoryFullInformation(this.Handle, pattern))
			{
				yield return new DirectoryFileFullInformation(info);
			}
		}

		public IEnumerable<DirectoryFileBothInformation> GetFileBothInformationEnumerator(string pattern)
		{
			foreach (var info in Wrapper.NtQueryDirectoryBothInformation(this.Handle, pattern))
			{
				yield return new DirectoryFileBothInformation(info);
			}
		}

		public IEnumerable<DirectoryFileIDFullInformation> GetFileIDFullInformationEnumerator(string pattern)
		{
			foreach (var info in Wrapper.NtQueryDirectoryIDFullInformation(this.Handle, pattern))
			{
				yield return new DirectoryFileIDFullInformation(info);
			}
		}

		public IEnumerable<DirectoryFileIDBothInformation> GetFileIDBothInformationEnumerator(string pattern)
		{
			foreach (var info in Wrapper.NtQueryDirectoryIDBothInformation(this.Handle, pattern))
			{
				yield return new DirectoryFileIDBothInformation(info);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{ return ((IEnumerable<string>)this).GetEnumerator(); }

		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{ return this.GetFileNamesEnumerator(null).GetEnumerator(); }

		IEnumerator<DirectoryFileInformation> IEnumerable<DirectoryFileInformation>.GetEnumerator()
		{ return this.GetFileInformationEnumerator(null).GetEnumerator(); }

		IEnumerator<DirectoryFileFullInformation> IEnumerable<DirectoryFileFullInformation>.GetEnumerator()
		{ return this.GetFileFullInformationEnumerator(null).GetEnumerator(); }

		IEnumerator<DirectoryFileBothInformation> IEnumerable<DirectoryFileBothInformation>.GetEnumerator()
		{ return this.GetFileBothInformationEnumerator(null).GetEnumerator(); }

		IEnumerator<DirectoryFileIDFullInformation> IEnumerable<DirectoryFileIDFullInformation>.GetEnumerator()
		{ return this.GetFileIDFullInformationEnumerator(null).GetEnumerator(); }

		IEnumerator<DirectoryFileIDBothInformation> IEnumerable<DirectoryFileIDBothInformation>.GetEnumerator()
		{ return this.GetFileIDBothInformationEnumerator(null).GetEnumerator(); }

		public FileBasicAttributeInformation GetChildAttributes(string fileName, AllowedObjectAttributes attributes)
		{
			return Wrapper.NtQueryAttributesFile(this.Handle, fileName, attributes);
		}

		public string[] GetFileNames(string pattern)
		{
			Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
			foreach (string item in (IEnumerable<string>)this)
			{ result.Add(item); }
			return result.ToArray();
		}

		public DirectoryFileInformation[] GetFilesInformation(string pattern)
		{
			System.Collections.Generic.List<DirectoryFileInformation> result = new System.Collections.Generic.List<DirectoryFileInformation>();
			foreach (DirectoryFileInformation item in (IEnumerable<DirectoryFileInformation>)this)
			{ result.Add(item); }
			return result.ToArray();
		}

		public NtDirectoryFile OpenDirectory(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions &= ~FileCreateOptions.NonDirectoryFile;
			createOptions |= FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(this.Handle, name, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			return handle != null ? new NtDirectoryFile(handle) : null;
		}

		public NtNonDirectoryFile OpenFile(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions |= FileCreateOptions.NonDirectoryFile;
			createOptions &= ~FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(this.Handle, name, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			return handle != null ? new NtNonDirectoryFile(Handle) : null;
		}

		public void AddChangeHandler(Events.NtEvent @event, bool watchTree, System.EventHandler<NotifyEventArgs> eh)
		{
			Wrapper.NtNotifyChangeDirectoryFile(this.Handle, @event != null ? @event.Handle : null, FileNotifyFilter.VALID_MASK, watchTree, (fnis) =>
			{
				eh(this, new NotifyEventArgs(fnis));
			});
		 }


		public class NotifyEventArgs : EventArgs
		{
			public NotifyEventArgs()
			{ this.Information = Array.AsReadOnly(new FileNotifyInformation[0]); }

			public NotifyEventArgs(params FileNotifyInformation[] info)
			{ this.Information = Array.AsReadOnly(info); }

			public NotifyEventArgs(System.Collections.Generic.IList<FileNotifyInformation> list)
			{ this.Information = new System.Collections.ObjectModel.ReadOnlyCollection<FileNotifyInformation>(list); }

			public System.Collections.ObjectModel.ReadOnlyCollection<FileNotifyInformation> Information { get; private set; }

			public override string ToString()
			{
				string[] strs = new string[this.Information.Count];
				for (int i = 0; i < this.Information.Count; i++)
				{
					strs[i] = string.Format("Action {1} occured on \"{0}\"", this.Information[i].FileName, this.Information[i].Action);
				}
				return string.Join("\r\n", strs);
			}
		}


		public static NtDirectoryFile Create(string name, FileAccessMask accessMask, FileShare fileShare, FileCreateOptions createOptions, ErrorsNotToThrow errorMode)
		{
			return Create(name, accessMask, fileShare, FileCreateOptions.Default, AllowedObjectAttributes.CaseInsensitive, FileAttributes.Normal, FileCreationDisposition.OpenOrCreate, errorMode);
		}

		public static NtDirectoryFile Create(string name, FileAccessMask accessMask, FileShare fileShare, FileCreateOptions createOptions, AllowedObjectAttributes attributes, FileAttributes fileAttributes, FileCreationDisposition fcd, ErrorsNotToThrow errorMode)
		{
			createOptions &= ~FileCreateOptions.NonDirectoryFile;
			createOptions |= FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtCreateFile(null, name, accessMask, attributes, 0, fileAttributes, fileShare, fcd, createOptions, errorMode);
			return handle != null ? new NtDirectoryFile(handle) : null;
		}

		public static NtDirectoryFile Open(string name, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions &= ~FileCreateOptions.NonDirectoryFile;
			createOptions |= FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(null, name, accessMask, attributes, createOptions, fileShare, out openInformation, errorMode);
			return handle != null ? new NtDirectoryFile(handle) : null;
		}

		public static NtDirectoryFile Open(long fileReference, FileAccessMask accessMask, AllowedObjectAttributes attributes, FileShare fileShare, FileCreateOptions createOptions, out FileOpenInformation openInformation, ErrorsNotToThrow errorMode)
		{
			createOptions &= ~FileCreateOptions.NonDirectoryFile;
			createOptions |= FileCreateOptions.DirectoryFile;
			SafeNtFileHandle handle = Wrapper.NtOpenFile(fileReference, accessMask, attributes, createOptions, fileShare, out openInformation, ErrorsNotToThrow.ThrowAnyError);
			return handle != null ? new NtDirectoryFile(handle) : null;
		}
	}
}