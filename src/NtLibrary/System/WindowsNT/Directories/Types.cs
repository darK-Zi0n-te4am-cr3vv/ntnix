using System.Runtime.InteropServices;
namespace System.WindowsNT.Directories
{
	[System.Flags()]
	public enum DirectoryAccessMask : uint
	{
		QUERY = (0x0001),
		TRAVERSE = (0x0002),
		CREATE_OBJECT = (0x0004),
		CREATE_SUBDIRECTORY = (0x0008),
		ALL_ACCESS = (AccessMask.StandardRightsRequired | 0xF),
		MAXIMUM_ALLOWED = AccessMask.MaximumAllowed
	}

	internal struct ObjectDirectoryInformation
	{
		public UnicodeString ObjectName;
		public UnicodeString ObjectTypeName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		public string ObjectTypeNameData;

		internal static ObjectDirectoryInformation FromPtr(IntPtr pInfo, uint dataLength)
		{
			ObjectDirectoryInformation result = (ObjectDirectoryInformation)Marshal.PtrToStructure(pInfo, typeof(ObjectDirectoryInformation));
			unsafe
			{
				System.IntPtr pTypeName = new IntPtr((byte*)pInfo + (uint)Marshal.SizeOf(typeof(UnicodeString)) + (uint)result.ObjectName.ByteLength + (uint)Marshal.OffsetOf(typeof(ObjectDirectoryInformation), "ObjectTypeNameData") - sizeof(char));
				result.ObjectTypeNameData = Marshal.PtrToStringUni(pTypeName);
			}
			return result;
		}
	}

	public struct DirectoryInformation
	{
		internal DirectoryInformation(ObjectDirectoryInformation information)
			: this()
		{
			this.ObjectName = information.ObjectName.ToString();
			this.ObjectTypeName = information.ObjectTypeNameData;
		}

		public string ObjectName { get; set; }
		public string ObjectTypeName { get; set; }

		public override string ToString()
		{
			return string.Format("{{Name = {0}, TypeName = {1}}}", this.ObjectName, this.ObjectTypeName);
		}
	}
}