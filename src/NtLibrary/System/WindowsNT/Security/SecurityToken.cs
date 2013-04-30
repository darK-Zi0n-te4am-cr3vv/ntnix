using System.WindowsNT.Security.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Security
{
	public class SecurityToken : NtObject
	{
		protected SecurityToken(SafeNtTokenHandle handle) : base() { this.Handle = handle; }

		public TokenAccessMask AccessMask { get { return (TokenAccessMask)this.ObjectAccessMask; } }

		public SafeNtTokenHandle Handle { get; private set; }

		public override SafeNtHandle GenericHandle
		{ get { return this.Handle; } }

		public void AdjustPrivilege(Privilege privilege, bool enabled)
		{
			Wrapper.NtAdjustPrivilegeToken(this.Handle, privilege, enabled);
		}

		public static SecurityToken OpenProcessToken(Processes.NtProcess process, TokenAccessMask accessMask)
		{ return new SecurityToken(Wrapper.NtOpenProcessToken(process.Handle, accessMask)); }
	}
}