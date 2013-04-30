using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.Directories.PrivateImplementationDetails;
namespace System.WindowsNT.Directories
{
	public class NtDirectoryObject : NtObject
	{
		public NtDirectoryObject(SafeNtDirectoryHandle handle) : base() { this.Handle = handle; }

		public DirectoryAccessMask AccessMask { get { return (DirectoryAccessMask)this.ObjectAccessMask; } }

		public override SafeNtHandle GenericHandle { get { return this.Handle; } }

		public SafeNtDirectoryHandle Handle { get; private set; }

		public void EnumerateObjects(System.Predicate<DirectoryInformation> action)
		{
			Wrapper.NtEnumerateDirectoryObject(this.Handle, odi => action(new DirectoryInformation(odi)));
		}

		public TList GetChildren<TList>()
			where TList : System.Collections.Generic.ICollection<DirectoryInformation>, new()
		{
			TList result = new TList();
			this.EnumerateObjects(di => { result.Add(di); return true; });
			return result;
		}


		public static NtDirectoryObject CreateDirectory(string name, DirectoryAccessMask access, AllowedObjectAttributes attributes)
		{ return new NtDirectoryObject(Wrapper.NtCreateDirectoryObject(name, access, attributes)); }

		public static NtDirectoryObject OpenDirectory(string name, DirectoryAccessMask access, AllowedObjectAttributes attributes)
		{ return new NtDirectoryObject(Wrapper.NtOpenDirectoryObject(name, access, attributes)); }
	}
}