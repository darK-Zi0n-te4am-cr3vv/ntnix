//#define IS_REGISTRY_VALUE_COLLECTION

using System.Collections;
using System.Collections.Generic;
using System.WindowsNT.Events;
using System.WindowsNT.Registry.PrivateImplementationDetails;
using System.WindowsNT.Security;
using Microsoft.WinNT.SafeHandles;

namespace System.WindowsNT.Registry
{
	public class NtRegistryKey : NtObject
	{
		public const short NoCountLimit = -1;

		static NtRegistryKey() { AutoAskForPrivilege = true; }

		public class ValueCollection : IEnumerable<RegValueInformation>
		{
			private struct RegistryValueCollectionEnumerator : IEnumerator<RegValueInformation>
			{
				public RegistryValueCollectionEnumerator(ValueCollection collection) : this() { this.Collection = collection; }

				private int CurrentIndex { get; set; }

				public ValueCollection Collection { get; private set; }

				public RegValueInformation Current
				{
					get
					{
						return new RegValueInformation(this.Collection.GetName(this.CurrentIndex), this.Collection[this.CurrentIndex].Data);
					}
				}

				void System.IDisposable.Dispose() { }

				object IEnumerator.Current
				{ get { return this.Current; } }

				public bool MoveNext()
				{
					return ++this.CurrentIndex < this.Collection.Count;
				}

				public void Reset()
				{ this.CurrentIndex = 0; }
			}

			internal ValueCollection(NtRegistryKey key) { this.Key = key; }

			public NtRegistryKey Key { get; private set; }

			public bool Contains(string name)
			{
				this.Key.CheckAndThrowIfDisposed();
				return Wrapper.NtValueExists(this.Key.Handle, name);
			}

			public int Count
			{
				get
				{
					this.Key.CheckAndThrowIfDisposed();
					return (int)Wrapper.NtQueryKeyFull(this.Key.Handle).Values;
				}
			}

			public IEnumerable<string> GetNameEnumerator()
			{ return this.GetNameEnumerator(0); }

			public IEnumerable<string> GetNameEnumerator(int startIndex)
			{ return this.GetNameEnumerator(startIndex, NoCountLimit); }

			public IEnumerable<string> GetNameEnumerator(int startIndex, int count)
			{
				this.Key.CheckAndThrowIfDisposed();
				foreach (KeyValueBasicInformation kvi in Wrapper.NtEnumerateValueKeyBasic(this.Key.Handle, (uint)startIndex, count))
				{
					yield return kvi.Name;
				}
			}

			public IEnumerable<RegValueInformation> GetEnumerator()
			{ return this.GetEnumerator(0); }

			public IEnumerable<RegValueInformation> GetEnumerator(int startIndex)
			{ return this.GetEnumerator(startIndex, NoCountLimit); }

			public IEnumerable<RegValueInformation> GetEnumerator(int startIndex, int count)
			{
				this.Key.CheckAndThrowIfDisposed();
				foreach (KeyValueFullInformation kvi in Wrapper.NtEnumerateValueKeyFull(this.Key.Handle, (uint)startIndex, count))
				{
					yield return new RegValueInformation(kvi.Name, (int)kvi.NameLength, RegistryValueData.ToActualType(kvi.Type, kvi.Data), (int)kvi.DataLength, (int)kvi.TitleIndex);
				}
			}

			public string GetName(int index)
			{
				this.Key.CheckAndThrowIfDisposed();
				return Wrapper.NtQueryValueKeyBasic(this.Key.Handle, (uint)index).Name;
			}

			public List<string> GetNames(bool sort)
			{
				this.Key.CheckAndThrowIfDisposed();
				List<string> result = new List<string>();
				foreach (string name in this.GetNameEnumerator())
				{
					result.Add(name);
				}
				return result;
			}

			public RegValueInformation GetExtendedInformation(string name)
			{
				this.Key.CheckAndThrowIfDisposed();
				KeyValueFullInformation valueInfo = Wrapper.NtQueryValueKeyFull(this.Key.Handle, name);
				RegValueInformation result = new RegValueInformation(valueInfo.Name, (int)valueInfo.NameLength, RegistryValueData.ToActualType(valueInfo.Type, valueInfo.Data), (int)valueInfo.DataLength, (int)valueInfo.TitleIndex);
				return result;
			}

			public bool Remove(string name)
			{
				if (this.Contains(name))
				{
					this.Key.CheckAndThrowIfDisposed();
					Wrapper.NtDeleteValueKey(this.Key.Handle, name);
					return true;
				}
				else
				{ return false; }
			}

			public RegistryValueData this[string name]
			{
				get
				{
					return this.GetExtendedInformation(name).Data;
				}
				set
				{
					this.Key.CheckAndThrowIfDisposed();
					Wrapper.NtSetValueKey(this.Key.Handle, name, value, (uint)this.Key.TitleIndex);
					this.OnValueChanged(new System.EventArgs());
				}
			}

			public RegValueInformation this[int index]
			{
				get
				{
					this.Key.CheckAndThrowIfDisposed();
					KeyValueFullInformation valueInfo = Wrapper.NtQueryValueKeyFull(this.Key.Handle, (uint)index);
					return new RegValueInformation(valueInfo.Name, (int)valueInfo.NameLength, RegistryValueData.ToActualType(valueInfo.Type, valueInfo.Data), (int)valueInfo.DataLength, (int)valueInfo.TitleIndex);
				}
			}

			public event System.EventHandler<EventArgs> ValueChanged;

			protected virtual void OnValueChanged(EventArgs e)
			{
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, e);
				}
			}

			public IEnumerable<MatchResult> GetMatches(bool searchValueNames, bool searchValueData, System.Text.RegularExpressions.Regex regex)
			{
				if (searchValueNames & !searchValueData)
				{
					foreach (string valueName in this.GetNameEnumerator())
					{
						System.Text.RegularExpressions.Match m;
						m = regex.Match(valueName);
						if (m.Success)
						{
							yield return new MatchResult(MatchType.ValueName, this.Key.FullPath, valueName, m);
						}
					}
				}
				else if (searchValueData)
				{
					foreach (RegValueInformation val in this.GetEnumerator())
					{
						System.Text.RegularExpressions.Match m;
						if (searchValueNames)
						{
							m = regex.Match(val.Name);
							if (m.Success)
							{
								yield return new MatchResult(MatchType.ValueName, this.Key.FullPath, val.Name, m);
							}
						}
						m = regex.Match(val.Data.ToString());
						if (m.Success)
						{
							yield return new MatchResult(MatchType.ValueData, this.Key.FullPath, val.Name, m);
						}
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{ return ((IEnumerable<RegValueInformation>)this).GetEnumerator(); }

			IEnumerator<RegValueInformation> IEnumerable<RegValueInformation>.GetEnumerator()
			{
				this.Key.CheckAndThrowIfDisposed();
				foreach (KeyValueFullInformation kvi in Wrapper.NtEnumerateValueKeyFull(this.Key.Handle, 0, NoCountLimit))
				{
					yield return new RegValueInformation(kvi.Name, (int)kvi.NameLength, RegistryValueData.ToActualType(kvi.Type, kvi.Data), (int)kvi.DataLength, (int)kvi.TitleIndex);
				}
			}
		}

		public class SubKeyCollection
		{
			internal SubKeyCollection(NtRegistryKey key) { this.Key = key; }

			public NtRegistryKey Key { get; private set; }

			public int Count
			{
				get
				{
					this.Key.CheckAndThrowIfDisposed();
					return (int)Wrapper.NtQuerySubKeyCount(this.Key.Handle);
				}
			}

			public NtRegistryKey Create(string name, KeyAccessMask accessMask, AllowedObjectAttributes attributes, RegCreateOptions createOptions, out RegCreationDisposition creationDisposition)
			{
				this.Key.CheckAndThrowIfDisposed();
				SafeNtRegistryHandle handle = Wrapper.NtCreateKey(this.Key.Handle, name, accessMask, createOptions, out creationDisposition, attributes);
				return handle == null ? null : new NtRegistryKey(handle);
			}

			public IEnumerable<string> GetNameEnumerator()
			{ return this.GetNameEnumerator(0); }

			public IEnumerable<string> GetNameEnumerator(int startIndex)
			{
				this.Key.CheckAndThrowIfDisposed();
				foreach (KeyBasicInformation ki in Wrapper.NtEnumerateKeyBasic(this.Key.Handle, (uint)startIndex))
				{
					yield return ki.Name;
				}
			}

			public bool Exists(string subKeyName)
			{
				this.Key.CheckAndThrowIfDisposed();
				return Wrapper.NtKeyExists(this.Key.Handle, subKeyName);
			}

			public string[] GetNames(bool continueOnError)
			{
				this.Key.CheckAndThrowIfDisposed();
				List<string> result = new List<string>();
				foreach (string name in this.GetNameEnumerator())
				{
					result.Add(name);
				}
				return result.ToArray();
			}

			/// <returns>The specified registry key, or <c>null</c> if it is not found.</returns>
			public NtRegistryKey Open(int index, KeyAccessMask desiredAccess, AllowedObjectAttributes attributes)
			{
				this.Key.CheckAndThrowIfDisposed();
				return this.Open(Wrapper.QuerySubKeyBasic(this.Key.Handle, (uint)index).Name, desiredAccess, attributes);
			}
			
			/// <returns>The specified registry key, or <c>null</c> if it is not found.</returns>
			public NtRegistryKey Open(string name, KeyAccessMask accessMask, AllowedObjectAttributes attributes)
			{
				this.Key.CheckAndThrowIfDisposed();
				SafeNtRegistryHandle handle = Wrapper.NtOpenKey(this.Key.Handle, name, accessMask, attributes);
				return handle == null ? null : new NtRegistryKey(handle);
			}

			public RegKeyInformation this[int index]
			{
				get
				{
					this.Key.CheckAndThrowIfDisposed();
					KeyNodeInformation subKeyInformation1 = Wrapper.QuerySubKeyNode(this.Key.Handle, (uint)index);
					KeyFullInformation subKeyInformation2 = Wrapper.QuerySubKeyFull(this.Key.Handle, (uint)index);
					return new RegKeyInformation(subKeyInformation1.Name, subKeyInformation1.Class, (int)subKeyInformation2.Values, (int)subKeyInformation2.SubKeys, (int)subKeyInformation1.TitleIndex, System.DateTime.FromFileTime(subKeyInformation1.LastWriteTime));
				}
			}

			public void Remove(string name, AllowedObjectAttributes subKeyAllowedObjectAttributes)
			{
				this.Key.CheckAndThrowIfDisposed();
				this.Open(name, KeyAccessMask.Delete, subKeyAllowedObjectAttributes).DeleteKey();
			}

			public IEnumerable<MatchResult> GetMatches(System.Text.RegularExpressions.Regex regex)
			{
				foreach (string subkeyName in this.GetNameEnumerator())
				{
					System.Text.RegularExpressions.Match m = regex.Match(subkeyName);
					if (m.Success)
					{
						yield return new MatchResult(Path.Combine(this.Key.FullPath, subkeyName), m);
					}
				};
			}
		}

		public struct MatchResult
		{
			internal MatchResult(string keyPath, System.Text.RegularExpressions.Match match) : this(MatchType.KeyName, keyPath, null, match) { }
			internal MatchResult(MatchType type, string keyPath, string valueName, System.Text.RegularExpressions.Match match)
				: this()
			{
				if (!System.Enum.IsDefined(typeof(MatchType), type))
					throw new System.ArgumentOutOfRangeException("type");
				if (keyPath == null)
					throw new System.ArgumentNullException("keyPath");
				if (match == null)
					throw new System.ArgumentNullException("match");
				this.Type = type;
				this.KeyPath = keyPath;
				this.ValueName = valueName;
				this.Match = match;
			}

			public MatchType Type { get; set; }
			public string KeyPath { get; set; }
			/// <summary>Returns <c>null</c> if the type is <see cref="MatchType.KeyName"/>.</summary>
			public string ValueName { get; set; }
			public System.Text.RegularExpressions.Match Match { get; private set; }
		}

		public enum MatchType { KeyName, ValueName, ValueData }
		
		public enum RecursionOptions
		{
			None,
			LevelByLevel,
			ChildrenFirst
		}

		protected NtRegistryKey(SafeNtRegistryHandle handle)
			: base()
		{
			this.Handle = handle;
			this.SubKeys = new SubKeyCollection(this);
			this.Values = new ValueCollection(this);
		}

		protected void CheckAndThrowIfDisposed()
		{
			if (this.IsDisposed)
			{ throw new ObjectDisposedException(base.ToString()); }
		}

		public void Compact()
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForAllPrivileges();
			}
			Wrapper.NtCompactKeys(this.Handle);
		}

		public void Compress()
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForAllPrivileges();
			}
			Wrapper.NtCompressKey(this.Handle);
		}

		private void AskForAllPrivileges()
		{
			Privilege[] allPrivileges = (Privilege[])System.Enum.GetValues(typeof(Privilege));
			foreach (Privilege p in allPrivileges)
			{
				AskForPrivilege(p);
			}
		}

		public void DeleteKey()
		{
			this.CheckAndThrowIfDisposed();
			Wrapper.NtDeleteKey(this.Handle);
			this.Handle.Close();
		}

		public IEnumerable<MatchResult> GetMatches(bool searchSubKeyNames, bool searchValueNames, bool searchValueData, System.Text.RegularExpressions.Regex regex, RecursionOptions recursion, AllowedObjectAttributes subKeyOpenAttributes)
		{
			if (recursion == RecursionOptions.ChildrenFirst)
			{
				IEnumerator<string> nameEnumerator = this.SubKeys.GetNameEnumerator().GetEnumerator();
				while (true)
				{
					try
					{
						if (!nameEnumerator.MoveNext())
						{
							break;
						}
					}
					catch (Exception)
					{
						break;
					}
					NtRegistryKey subkey = this.SubKeys.Open(nameEnumerator.Current, KeyAccessMask.MaximumAllowed, subKeyOpenAttributes);
					if (subkey != null)
					{
						foreach (MatchResult m in subkey.GetMatches(searchSubKeyNames, searchValueNames, searchValueData, regex, recursion, subKeyOpenAttributes))
						{
							yield return m;
						}
					}
				}
			}

			IEnumerator<MatchResult> matches = this.Values.GetMatches(searchValueNames, searchValueData, regex).GetEnumerator();
			while (true)
			{
				try
				{
					if (!matches.MoveNext())
					{
						break;
					}
				}
				catch (Exception)
				{
					break;
				}
				yield return matches.Current;
			}

			if (searchSubKeyNames)
			{
				matches = this.SubKeys.GetMatches(regex).GetEnumerator();
				while (true)
				{
					try
					{
						if (!matches.MoveNext())
						{
							break;
						}
					}
					catch (Exception)
					{
						break;
					}
					yield return matches.Current;
				}
			}

			if (recursion == RecursionOptions.LevelByLevel)
			{
				IEnumerator<string> nameEnumerator = this.SubKeys.GetNameEnumerator().GetEnumerator();
				while (true)
				{
					try
					{
						if (!nameEnumerator.MoveNext())
						{
							break;
						}
					}
					catch (Exception)
					{
						break;
					}
					NtRegistryKey subkey = this.SubKeys.Open(nameEnumerator.Current, KeyAccessMask.MaximumAllowed, subKeyOpenAttributes);
					if (subkey != null)
					{
						foreach (MatchResult mr in subkey.GetMatches(searchSubKeyNames, searchValueNames, searchValueData, regex, recursion, subKeyOpenAttributes))
						{
							yield return mr;
						}
					}
				}
			}
		}

		public void Flush()
		{
			this.CheckAndThrowIfDisposed();
			Wrapper.NtFlushKey(this.Handle);
		}

		public void Load(string hiveName, string fileName, AllowedObjectAttributes attributes)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Restore);
			}
			Wrapper.NtLoadKey(this.Handle, hiveName, fileName, attributes);
		}

		public void Load2(string hiveName, string fileName, KeyRestoreOptions loadOptions, AllowedObjectAttributes attributes)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Restore);
			}
			Wrapper.NtLoadKey2(this.Handle, hiveName, fileName, loadOptions, attributes);
		}

		public void Lock()
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForAllPrivileges();
			}
			Wrapper.NtLockRegistryKey(this.Handle);
		}

		protected virtual void OnNameChanged(NameChangedEventArgs e)
		{
			if (this.NameChanged != null)
			{
				this.NameChanged(this, e);
			}
		}

		public NtRegistryKey OpenParentKey(KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{ return OpenKey(Path.GetParentPath(this.FullPath), accessMask, attributes); }

		public void Restore(System.WindowsNT.IO.NtFile file, KeyRestoreOptions restoreOptions)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Restore);
			}
			Wrapper.NtRestoreKey(this.Handle, file.Handle, restoreOptions);
		}

		public void Save(IO.NtFile file)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Backup);
			}
			Wrapper.NtSaveKey(this.Handle, file.Handle);
		}

		public void SaveEx(System.WindowsNT.IO.NtFile file, RegHiveFormat hiveFormat)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Backup);
			}
			Wrapper.NtSaveKeyEx(this.Handle, file.Handle, hiveFormat);
		}

		public void Unload(string subKeyName)
		{
			this.CheckAndThrowIfDisposed();
			if (AutoAskForPrivilege)
			{
				AskForPrivilege(Privilege.Restore);
			}
			Wrapper.NtUnloadKey(Path.Combine(this.FullPath, subKeyName));
		}

		public RegNotifyInformation[] WaitForChange(RegNotifyFilter notifyFilter, bool watchSubTree)
		{
			this.CheckAndThrowIfDisposed();
			return Wrapper.NtNotifyChangeKey(this.Handle, notifyFilter, watchSubTree);
		}

		// Properties

		public KeyAccessMask AccessMask { get { return (KeyAccessMask)this.ObjectAccessMask; } }

		public string Class
		{ get { return Wrapper.NtQueryKeyFull(this.Handle).Class; } }

		public string FullPath
		{
			get
			{
				this.CheckAndThrowIfDisposed();
				return Wrapper.NtQueryKeyName(this.Handle).Name;
			}
		}
		
		public SafeNtRegistryHandle Handle { get; private set; }

		public override SafeNtHandle GenericHandle
		{ get { return this.Handle; } }

		public System.DateTime LastWriteTime
		{
			get
			{
				this.CheckAndThrowIfDisposed();
				return System.DateTime.FromFileTime(Wrapper.NtQueryKeyBasic(this.Handle).LastWriteTime);
			}
			set
			{
				this.CheckAndThrowIfDisposed();
				Wrapper.NtSetInformationKey(this.Handle, new KeyWriteTimeInformation() { LastWriteTime = value.ToFileTime() });
			}
		}

		public string Name
		{
			get
			{
				this.CheckAndThrowIfDisposed();
				return /*this.name != null ? this.name :*/ Path.GetKeyName(this.FullPath);
			}
			set
			{
				this.CheckAndThrowIfDisposed();
				Wrapper.NtRenameKey(this.Handle, value);
				//this.name = null;
				this.OnNameChanged(new NameChangedEventArgs(this.Name));
			}
		}

		public SubKeyCollection SubKeys { get; private set; }

		public int TitleIndex
		{
			get
			{
				this.CheckAndThrowIfDisposed();
				return (int)Wrapper.NtQueryKeyBasic(this.Handle).TitleIndex;
			}
		}

		public int UserFlags
		{
			get { return unchecked((int)Wrapper.NtQueryKeyFlags(this.Handle).UserFlags); }
			set { Wrapper.NtSetInformationKey(this.Handle, new KeyUserFlagsInformation() { UserFlags = unchecked((uint)value) }); }
		}

		public ValueCollection Values { get; private set; }

		// Events

		public event System.EventHandler<NameChangedEventArgs> NameChanged;

		// Static ===========================================

		public static NtRegistryKey OpenMachineHive() { return OpenMachineHive(KeyAccessMask.AllAccess, AllowedObjectAttributes.None); }
		public static NtRegistryKey OpenMachineHive(KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			const string MACHINE_HIVE_PATH = @"\REGISTRY\MACHINE";
			return NtRegistryKey.OpenKey(MACHINE_HIVE_PATH, accessMask, attributes);
		}

		public static NtRegistryKey OpenUsersHive() { return OpenUsersHive(KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.None); }
		public static NtRegistryKey OpenUsersHive(KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			const string USERS_HIVE_PATH = @"\REGISTRY\USER";
			return NtRegistryKey.OpenKey(USERS_HIVE_PATH, accessMask, attributes);
		}

		public static NtRegistryKey OpenCurrentUserKey() { return OpenCurrentUserKey(KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.None); }
		public static NtRegistryKey OpenCurrentUserKey(KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			string CURRENT_USER_HIVE_PATH = RtlFormatCurrentUserKeyPath();
			return NtRegistryKey.OpenKey(CURRENT_USER_HIVE_PATH, accessMask, attributes);
		}

		public static NtRegistryKey OpenRegistry() { return OpenRegistry(KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.None); }
		public static NtRegistryKey OpenRegistry(KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			const string ALL_REGISTRY = @"\REGISTRY";
			return NtRegistryKey.OpenKey(ALL_REGISTRY, accessMask, attributes);
		}

		/// <returns>The specified registry key, or <c>null</c> if it is not found.</returns>
		public static NtRegistryKey OpenKey(string path, KeyAccessMask accessMask, AllowedObjectAttributes attributes)
		{
			path = Path.Normalize(path);
			SafeNtRegistryHandle handle = Wrapper.NtOpenKey(null, path, accessMask, attributes);
			return handle == null ? null : new NtRegistryKey(handle);
		}

		public static NtRegistryKey CreateKey(string path, KeyAccessMask accessMask, AllowedObjectAttributes attributes, RegCreateOptions createOptions, RegCreationDisposition creationDisposition)
		{
			path = Path.Normalize(path);
			SafeNtRegistryHandle handle = Wrapper.NtCreateKey(null, path, accessMask, createOptions, out creationDisposition, attributes);
			return handle == null ? null : new NtRegistryKey(handle);
		}

		public static string RtlFormatCurrentUserKeyPath()
		{
			UnicodeString result;
			Native.RtlFormatCurrentUserKeyPath(out result);
			return result;
		}

		public static bool AutoAskForPrivilege { get; set; }

		public const short MaxValueNameLength = 32767;
		public const short MaxKeyNameLength = 256;

		public static bool KeyExists(string keyName)
		{
			return Wrapper.NtKeyExists(null, keyName);
		}

		private static void AskForPrivilege(Privilege privilege)
		{
			Processes.NtProcess.Current.AdjustPrivilege(privilege, true);
		}
	}
}