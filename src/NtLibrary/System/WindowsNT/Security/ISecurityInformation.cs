using System.Runtime.InteropServices;
using System.WindowsNT.Registry;
namespace System.WindowsNT.Security
{
	[ComConversionLoss, ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("965FC360-16FF-11d0-91CB-00AA00BBB723")]
	public interface ISecurityInformation
	{
		uint GetObjectInformation(out SI_OBJECT_INFO pObjectInfo);
		unsafe uint GetSecurity(SecurityInformation RequestedInformation, ref byte[] ppSecurityDescriptor, [MarshalAs(UnmanagedType.Bool)] bool fDefault);
		uint SetSecurity(SecurityInformation SecurityInformation, byte[] pSecurityDescriptor);
		unsafe uint GetAccessRights(ref System.Guid pguidObjectType, uint dwFlags, ref SI_ACCESS* ppAccess, ref uint pcAccesses, ref uint piDefaultAccess);
		uint MapGeneric(ref System.Guid pguidObjectType, ref byte pAceFlags, ref AccessMask pMask);
		unsafe uint GetInheritTypes(ref SI_INHERIT_TYPE* ppInheritTypes, uint* pcInheritTypes);
		uint PropertySheetPageCallback(System.IntPtr hwnd, uint uMsg, SI_PAGE_TYPE uPage);
	}

	public class NtRegistryKeySecurityInformation : ISecurityInformation
	{
		private NtRegistryKey key;
		
		public NtRegistryKeySecurityInformation(NtRegistryKey key)
		{
			this.key = key;
		}

		public uint GetObjectInformation(out SI_OBJECT_INFO pObjectInfo)
		{
			pObjectInfo = new SI_OBJECT_INFO() 
			{
				dwFlags = SIObjectInfoFlags.SI_EDIT_PERMS | SIObjectInfoFlags.SI_EDIT_OWNER | SIObjectInfoFlags.SI_EDIT_AUDITS,
			};
			return 0;
		}

		public unsafe uint GetSecurity(SecurityInformation RequestedInformation, ref byte[] ppSecurityDescriptor, bool fDefault)
		{
			System.Security.AccessControl.RawSecurityDescriptor descriptor = this.key.GetSecurityDescriptor(RequestedInformation);
			byte[] result = new byte[descriptor.BinaryLength];
			descriptor.GetBinaryForm(result, 0);
			ppSecurityDescriptor = result;
			return 0;
		}

		public uint SetSecurity(SecurityInformation SecurityInformation, byte[] pSecurityDescriptor)
		{
			this.key.SetSecurityDescriptor(SecurityInformation, new System.Security.AccessControl.RawSecurityDescriptor(pSecurityDescriptor, 0));
			return 0;
		}

		public unsafe uint GetAccessRights(ref Guid pguidObjectType, uint dwFlags, ref SI_ACCESS* ppAccess, ref uint pcAccesses, ref uint piDefaultAccess)
		{
			return 0;
		}

		public uint MapGeneric(ref Guid pguidObjectType, ref byte pAceFlags, ref AccessMask pMask)
		{
			return 0;
		}

		public unsafe uint GetInheritTypes(ref SI_INHERIT_TYPE* ppInheritTypes, uint* pcInheritTypes)
		{
			return 0;
		}

		public uint PropertySheetPageCallback(IntPtr hwnd, uint uMsg, SI_PAGE_TYPE uPage)
		{
			return 0;
		}

		[DllImport("Aclui.dll", ThrowOnUnmappableChar = true)]
		public static extern bool EditSecurity(System.IntPtr hwndOwner, [MarshalAs(UnmanagedType.Interface)] ISecurityInformation psi);
	}

	public enum SIObjectInfoFlags : uint
	{
		SI_EDIT_PERMS = 0x00000000,
		SI_EDIT_OWNER = 0x00000001,
		SI_EDIT_AUDITS = 0x00000002,
		SI_CONTAINER = 0x00000004,
		SI_READONLY = 0x00000008,
		SI_ADVANCED = 0x00000010,
		SI_RESET = 0x00000020,
		SI_OWNER_READONLY = 0x00000040,
		SI_EDIT_PROPERTIES = 0x00000080,
		SI_OWNER_RECURSE = 0x00000100,
		SI_NO_ACL_PROTECT = 0x00000200,
		SI_NO_TREE_APPLY = 0x00000400,
		SI_PAGE_TITLE = 0x00000800,
		SI_SERVER_IS_DC = 0x00001000,
		SI_RESET_DACL_TREE = 0x00004000,
		SI_RESET_SACL_TREE = 0x00008000,
		SI_OBJECT_GUID = 0x00010000,
		SI_EDIT_EFFECTIVE = 0x00020000,
		SI_RESET_DACL = 0x00040000,
		SI_RESET_SACL = 0x00080000,
		SI_RESET_OWNER = 0x00100000,
		SI_NO_ADDITIONAL_PERMISSION = 0x00200000,
		SI_MAY_WRITE = 0x10000000
	}

	public struct SI_OBJECT_INFO
	{
		public SIObjectInfoFlags dwFlags;
		public System.IntPtr hInstance;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszServerName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszObjectName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszPageTitle;
		public System.Guid guidObjectType;
	}

	public struct SI_INHERIT_TYPE
	{
		public unsafe System.Guid* pguid;
		public uint dwFlags;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszName;
	}

	public enum SI_PAGE_TYPE
	{
		SI_PAGE_PERMSI_PAGE_PERM,
		SI_PAGE_ADVPERMSI_PAGE_ADVPERM,
		SI_PAGE_AUDITSI_PAGE_AUDIT,
		SI_PAGE_OWNERSI_PAGE_OWNER,
		SI_PAGE_EFFECTIVESI_PAGE_EFFECTIVE,
		SI_PAGE_TAKEOWNERSHIPSI_PAGE_TAKEOWNERSHIP
	}

	public struct SI_ACCESS 
	{
		public unsafe System.Guid* pguid;
		public AccessMask mask;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszName;
		public uint dwFlags;
	}
}