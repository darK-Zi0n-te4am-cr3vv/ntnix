using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Registry
{
	[System.Flags()]
	public enum KeyAccessMask : uint
	{
		AllAccess = (AccessMask.StandardRightsAll | QueryValue | SetValue | CreateSubKey | EnumerateSubKeys | Notify | CreateLink) & (~AccessMask.Synchronize),
		/// <summary>Create a symbolic link to the key. This flag is not used by device and intermediate drivers.</summary>
		CreateLink = (0x0020),
		/// <summary>Create subkeys for the key.</summary>
		CreateSubKey = (0x0004),
		Delete = AccessMask.Delete,
		/// <summary>Read the key's subkeys.</summary>
		EnumerateSubKeys = (0x0008),
		Execute = Read & ~AccessMask.Synchronize,
		MaximumAllowed = AccessMask.MaximumAllowed,
		/// <summary>Do not use.</summary>
		Notify = (0x0010),
		/// <summary>Read key values.</summary>
		QueryValue = (0x0001),
		Read = (AccessMask.StandardRightsRead | QueryValue | EnumerateSubKeys | Notify) & (~AccessMask.Synchronize),
		/// <summary>Write key values.</summary>
		SetValue = (0x0002),
		WOW64_32Key = (0x0200),
		WOW64_64Key = (0x0100),
		WOW64_64Res = (0x0300),
		Write = (AccessMask.StandardRightsWrite | SetValue | CreateSubKey) & (~AccessMask.Synchronize)
	}

	public enum RegNotifyFilter : uint
	{
		ChangeName = (0x00000001), // Create or delete (child)
		ChangeAttributes = (0x00000002),
		ChangeLastSet = (0x00000004), // time stamp
		ChangeSecurity = (0x00000008),
		LegalChangeFilter = (ChangeName | ChangeAttributes | ChangeLastSet | ChangeSecurity)
	}

	public enum RegCreationDisposition : uint
	{
		/// <summary>A new key was created.</summary>
		CreatedNewKey = (0x00000001),
		/// <summary>An existing key was opened.</summary>
		OpenedExistingKey = (0x00000002)
	}

	[System.Flags()]
	public enum RegCreateOptions : uint
	{
		Default = 0,
		/// <summary>Key is preserved when the system is rebooted.</summary>
		NonVolatile = (0x00000000),
		/// <summary>Key is not preserved when the system is rebooted.</summary>
		Volatile = (0x00000001),
		/// <summary>The newly created key is a symbolic link. This flag is not used by device and intermediate drivers.</summary>
		CreateLink = (0x00000002),
		/// <summary>Key should be created or opened with special privileges that allow backup and restore operations. This flag is not used by device and intermediate drivers.</summary>
		BackupRestore = (0x00000004),
		/// <summary>Open symbolic link.</summary>
		OpenLink = (0x00000008),
		Legal = NonVolatile | Volatile | CreateLink | BackupRestore | OpenLink
	}

	[System.Flags()]
	public enum RegHiveFormat
	{
		Standard = 1,
		Latest = 2,
		NoCompression = 4
	}

	public enum RegValueType : uint
	{
		/// <summary>No value type</summary>
		None = (0),
		/// <summary>Unicode nul terminated string</summary>
		SZ = (1),
		/// <summary>Unicode null terminated string (with environment variable references)</summary>
		ExpandSZ = (2),
		/// <summary>Free form binary</summary>
		Binary = (3),
		/// <summary>32-bit number</summary>
		DWord = (4),
		/// <summary>32-bit number (same as <see cref="DWord"/>)</summary>
		DWordLittleEndian = (4),
		/// <summary>32-bit, big endian number</summary>
		DWordBigEndian = (5),
		/// <summary>Symbolic Link (unicode)</summary>
		Link = (6),
		/// <summary>Multiple Unicode strings</summary>
		MultiSZ = (7),
		/// <summary>Resource list in the resource map</summary>
		ResourceList = (8),
		/// <summary>Resource list in the hardware description</summary>
		FullResourceDescriptor = (9),
		/// <summary></summary>
		ResourceRequirementList = (10),
		/// <summary>64-bit number</summary>
		QWord = (11),
		/// <summary>64-bit number (same as <see cref="QWord"/>)</summary>
		QWordLittleEndian = (11)
	}

	[System.Flags()]
	public enum KeyRestoreOptions : uint
	{
		Default = (0x00000000),
		/// <summary>Restore whole hive volatile</summary>
		WholeHiveVolatile = (0x00000001),
		/// <summary>Unwind changes to last flush</summary>
		RefreshHive = (0x00000002),
		/// <summary>Never lazy flush this hive</summary>
		NoLazyFlush = (0x00000004),
		/// <summary>Force the restore process even when we have open handles on subkeys</summary>
		ForceRestore = (0x00000008)
	}

	internal enum KeyInformationClass
	{
		KeyBasicInformation,
		KeyNodeInformation,
		KeyFullInformation,
		KeyNameInformation,
		KeyCachedInformation,
		KeyFlagsInformation
	}

	internal enum KeyValueInformationClass
	{
		KeyValueBasicInformation,
		KeyValueFullInformation,
		KeyValuePartialInformation,
		KeyValueFullInformationAlign64,
		KeyValuePartialInformationAlign64
	}

	internal enum KeySetInformationClass
	{
		KeyWriteTimeInformation,
		KeyUserFlagsInformation
	}

	public enum RegAction
	{
		KeyAdded,
		KeyRemoved,
		KeyModified
	}

	public struct RegKeyInformation
	{
		/// <summary>Not intended to be used by clients.</summary>
		internal RegKeyInformation(string name, string @class, int valueCount, int subKeyCount, int titleIndex, System.DateTime lastWriteTime)
			: this()
		{
			this.Name = name;
			this.Class = @class;
			this.TitleIndex = titleIndex;
			this.LastWriteTime = lastWriteTime;
			this.SubKeyCount = subKeyCount;
			this.ValueCount = valueCount;
		}
		public string Class { get; private set; }
		public string Name { get; private set; }
		public int TitleIndex { get; private set; }
		public int ValueCount { get; private set; }
		public int SubKeyCount { get; private set; }
		public System.DateTime LastWriteTime { get; private set; }
	}

	internal struct KeyValueEntry
	{
		public unsafe UnicodeString* ValueName;
		public uint DataLength;
		public uint DataOffset;
		public uint Type;
	}

	internal struct KeyBasicInformation
	{
		public long LastWriteTime;
		public uint TitleIndex;
		public uint NameLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name; //  Variable-length string

		internal static KeyBasicInformation FromPtr(System.IntPtr ptr)
		{
			KeyBasicInformation keyInformation = (KeyBasicInformation)Marshal.PtrToStructure(ptr, typeof(KeyBasicInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyBasicInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyNodeInformation
	{
		public long LastWriteTime;
		public uint TitleIndex;
		public uint ClassOffset;
		public uint ClassLength;
		public uint NameLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name; //  Variable-length string
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Class; //  Variable-length string

		internal static KeyNodeInformation FromPtr(System.IntPtr ptr)
		{
			KeyNodeInformation keyInformation = (KeyNodeInformation)Marshal.PtrToStructure(ptr, typeof(KeyNodeInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyNodeInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
				keyInformation.Class = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + keyInformation.ClassOffset), (int)keyInformation.ClassLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyFullInformation
	{
		public long LastWriteTime;
		public uint TitleIndex;
		public uint ClassOffset;
		public uint ClassLength;
		public uint SubKeys;
		public uint MaxNameLen;
		public uint MaxClassLen;
		public uint Values;
		public uint MaxValueNameLen;
		public uint MaxValueDataLen;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Class; //  Variable-length string

		internal static KeyFullInformation FromPtr(System.IntPtr ptr)
		{
			KeyFullInformation keyInformation = (KeyFullInformation)Marshal.PtrToStructure(ptr, typeof(KeyFullInformation));
			unsafe
			{
				keyInformation.Class = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + keyInformation.ClassOffset), (int)keyInformation.ClassLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyNameInformation
	{
		public uint NameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name;            // Variable length string

		internal static KeyNameInformation FromPtr(System.IntPtr ptr)
		{
			KeyNameInformation keyInformation = (KeyNameInformation)Marshal.PtrToStructure(ptr, typeof(KeyNameInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyNameInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyCachedInformation
	{
		public long LastWriteTime;
		public uint TitleIndex;
		public uint SubKeys;
		public uint MaxNameLen;
		public uint Values;
		public uint MaxValueNameLen;
		public uint MaxValueDataLen;
		public uint NameLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name;            // Variable length string

		internal static KeyCachedInformation FromPtr(System.IntPtr ptr)
		{
			KeyCachedInformation keyInformation = (KeyCachedInformation)Marshal.PtrToStructure(ptr, typeof(KeyCachedInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyCachedInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyFlagsInformation
	{
		public uint UserFlags;

		internal static KeyFlagsInformation FromPtr(System.IntPtr ptr)
		{ return (KeyFlagsInformation)Marshal.PtrToStructure(ptr, typeof(KeyFlagsInformation)); }
	}

	internal struct KeyValueBasicInformation
	{
		public uint TitleIndex;
		public RegValueType Type;
		public uint NameLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name; //  Variable-length string

		internal static KeyValueBasicInformation FromPtr(System.IntPtr ptr)
		{
			KeyValueBasicInformation keyInformation = (KeyValueBasicInformation)Marshal.PtrToStructure(ptr, typeof(KeyValueBasicInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyValueBasicInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
			}
			return keyInformation;
		}
	}

	internal struct KeyValuePartialInformation
	{
		public uint TitleIndex;
		public RegValueType Type;
		public uint DataLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] Data;

		internal static KeyValuePartialInformation FromPtr(System.IntPtr ptr)
		{
			KeyValuePartialInformation keyInformation = (KeyValuePartialInformation)Marshal.PtrToStructure(ptr, typeof(KeyValuePartialInformation));
			unsafe
			{
				keyInformation.Data = new byte[keyInformation.DataLength];
				Marshal.Copy(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyValuePartialInformation), "Data")), keyInformation.Data, 0, (int)keyInformation.DataLength);
			}
			return keyInformation;
		}

	}

	internal struct KeyValueFullInformation
	{
		public uint TitleIndex;
		public RegValueType Type;
		public uint DataOffset;
		public uint DataLength;
		public uint NameLength;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Name; //  Variable-length string
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] Data;

		internal static KeyValueFullInformation FromPtr(System.IntPtr ptr)
		{
			KeyValueFullInformation keyInformation = (KeyValueFullInformation)Marshal.PtrToStructure(ptr, typeof(KeyValueFullInformation));
			unsafe
			{
				keyInformation.Name = Marshal.PtrToStringUni(new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(KeyValueFullInformation), "Name")), (int)keyInformation.NameLength / sizeof(char));
				keyInformation.Data = new byte[keyInformation.DataLength];
				Marshal.Copy(new System.IntPtr((byte*)ptr + keyInformation.DataOffset), keyInformation.Data, 0, (int)keyInformation.DataLength);
			}
			return keyInformation;
		}
	}

	internal struct KeyWriteTimeInformation
	{
		public long LastWriteTime;

		internal static KeyWriteTimeInformation FromPtr(System.IntPtr ptr)
		{ return (KeyWriteTimeInformation)Marshal.PtrToStructure(ptr, typeof(KeyWriteTimeInformation)); }
	}

	internal struct KeyUserFlagsInformation
	{
		public uint UserFlags;

		internal static KeyUserFlagsInformation FromPtr(System.IntPtr ptr)
		{ return (KeyUserFlagsInformation)Marshal.PtrToStructure(ptr, typeof(KeyUserFlagsInformation)); }
	}

	internal struct KeyMultipleValueInformation
	{
		/// <summary>Pointer to <see cref="UnicodeString"/> structure containing value name. If specified value not exist, function fails.</summary>
		public unsafe UnicodeString* ValueName;
		/// <summary>Length of value's data, in bytes.</summary>
		public uint DataLength;
		/// <summary>Offset in output buffer (declared in <see cref="Registry.PrivateImplementationDetails.Native.NtQueryMultipleValueKey"/>) to value's data.</summary>
		public uint DataOffset;
		/// <summary>Type of queried value.</summary>
		public RegValueType Type;

		internal static KeyMultipleValueInformation FromPtr(System.IntPtr ptr)
		{ return (KeyMultipleValueInformation)Marshal.PtrToStructure(ptr, typeof(KeyMultipleValueInformation)); }
	}

	internal struct RegQueryMultipleValueKeyInformation
	{
		public System.IntPtr Object;
		public unsafe KeyValueEntry* ValueEntries;
		public uint EntryCount;
		public System.IntPtr ValueBuffer;
		public unsafe uint* BufferLength;
		public unsafe uint* RequiredBufferLength;
		public System.IntPtr CallContext;
		public System.IntPtr ObjectContext;
		public System.IntPtr Reserved;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct RegNotifyInformation
	{
		internal uint NextEntryOffset;
		public RegAction Action;
		internal uint KeyLength;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string Key;
		
		internal static RegNotifyInformation[] FromPtr(System.IntPtr ptr)
		{
			System.Collections.Generic.List<RegNotifyInformation> result = new System.Collections.Generic.List<RegNotifyInformation>();
			RegNotifyInformation current;
			do
			{
				unsafe
				{
					current = (RegNotifyInformation)Marshal.PtrToStructure(ptr, typeof(RegNotifyInformation));
					System.IntPtr pName = new System.IntPtr((byte*)ptr + (int)Marshal.OffsetOf(typeof(RegNotifyInformation), "Key"));
					current.Key = Marshal.PtrToStringUni(pName, (int)current.KeyLength / sizeof(char));
					result.Add(current);
					ptr = new IntPtr((byte*)ptr + current.NextEntryOffset);
				}
			} while (current.NextEntryOffset != 0);
			return result.ToArray();
		}
	}

	internal enum RegNotifyClass
	{
		RegNtDeleteKey,
		RegNtPreDeleteKey = RegNtDeleteKey,
		RegNtSetValueKey,
		RegNtPreSetValueKey = RegNtSetValueKey,
		RegNtDeleteValueKey,
		RegNtPreDeleteValueKey = RegNtDeleteValueKey,
		RegNtSetInformationKey,
		RegNtPreSetInformationKey = RegNtSetInformationKey,
		RegNtRenameKey,
		RegNtPreRenameKey = RegNtRenameKey,
		RegNtEnumerateKey,
		RegNtPreEnumerateKey = RegNtEnumerateKey,
		RegNtEnumerateValueKey,
		RegNtPreEnumerateValueKey = RegNtEnumerateValueKey,
		RegNtQueryKey,
		RegNtPreQueryKey = RegNtQueryKey,
		RegNtQueryValueKey,
		RegNtPreQueryValueKey = RegNtQueryValueKey,
		RegNtQueryMultipleValueKey,
		RegNtPreQueryMultipleValueKey = RegNtQueryMultipleValueKey,
		RegNtPreCreateKey,
		RegNtPostCreateKey,
		RegNtPreOpenKey,
		RegNtPostOpenKey,
		RegNtKeyHandleClose,
		RegNtPreKeyHandleClose = RegNtKeyHandleClose,
		// The following values apply only to Microsoft Windows Server 2003 and later.
		RegNtPostDeleteKey,
		RegNtPostSetValueKey,
		RegNtPostDeleteValueKey,
		RegNtPostSetInformationKey,
		RegNtPostRenameKey,
		RegNtPostEnumerateKey,
		RegNtPostEnumerateValueKey,
		RegNtPostQueryKey,
		RegNtPostQueryValueKey,
		RegNtPostQueryMultipleValueKey,
		RegNtPostKeyHandleClose,
		RegNtPreCreateKeyEx,
		RegNtPostCreateKeyEx,
		RegNtPreOpenKeyEx,
		RegNtPostOpenKeyEx,
		// The following values apply only to Microsoft Windows Vista and later.
		RegNtPreFlushKey,
		RegNtPostFlushKey,
		RegNtPreLoadKey,
		RegNtPostLoadKey,
		RegNtPreUnLoadKey,
		RegNtPostUnLoadKey,
		RegNtPreQueryKeySecurity,
		RegNtPostQueryKeySecurity,
		RegNtPreSetKeySecurity,
		RegNtPostSetKeySecurity,
		RegNtCallbackContextCleanup,
		MaxRegNtNotifyClass
	}

	internal delegate NtStatus RegistryCallback([In] System.IntPtr CallbackContext, [In] RegNotifyClass Argument1, [In] System.IntPtr Argument2);
}