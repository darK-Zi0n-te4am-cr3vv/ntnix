using System.Runtime.InteropServices;
using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Security.PrivateImplementationDetails
{
	internal static partial class Wrapper
	{
		public static void NtAdjustPrivilegeToken(SafeNtTokenHandle tokenHandle, Privilege privilege, bool enable)
		{
			TokenPrivileges privSet = new TokenPrivileges()
				{
					PrivilegeCount = 1,
					Privileges = new LuidAndAttributes[1]
					{
						new LuidAndAttributes()
						{ 
							Luid = (LUID)unchecked((uint)privilege),
							Attributes = enable ? LuidAttributes.Enabled : 0 
						} 
					}
				};
			TokenPrivileges old;
			uint returnedLength;
			NtStatus status = Native.NtAdjustPrivilegesToken(NTInternal.CreateHandleRef(tokenHandle), false, /* don't disable all privileges */ref privSet, privSet.GetMarshaledSize(), out old, out returnedLength);
			NtException.CheckAndThrowException(status);
		}

		public static SafeNtTokenHandle NtOpenProcessToken(SafeNtProcessHandle processHandle, TokenAccessMask accessMask)
		{
			System.IntPtr handle;
			NtStatus result = Processes.PrivateImplementationDetails.Native.NtOpenProcessToken(NTInternal.CreateHandleRef(processHandle), accessMask, out handle);
			NtException.CheckAndThrowException(result);
			return new SafeNtTokenHandle(handle);
		}

		public static bool NtPrivilegeCheck(SafeNtTokenHandle tokenHandle, params Privilege[] requiredPrivileges)
		{
			PrivilegeSet privSet = new PrivilegeSet()
			{
				PrivilegeCount = 1,
				Privilege = System.Array.ConvertAll<Privilege, LuidAndAttributes>(requiredPrivileges, delegate(Privilege p)
					{
						return new LuidAndAttributes() { Luid = (LUID)(uint)p, Attributes = LuidAttributes.Enabled };
					}
				)
			};
			bool output;
			NtStatus result = Native.NtPrivilegeCheck(NTInternal.CreateHandleRef(tokenHandle), ref privSet, out output);
			NtException.CheckAndThrowException(result);
			return output;
		}

		public static System.Security.AccessControl.RawSecurityDescriptor NtQuerySecurityObject(SafeNtHandle objectHandle, SecurityInformation information)
		{
			uint requiredLength;
			NtStatus result = Native.NtQuerySecurityObject(NTInternal.CreateHandleRef(objectHandle), information, System.IntPtr.Zero, 0, out requiredLength);
			int inputLength = (int)requiredLength;
			unsafe
			{
				System.IntPtr pDescriptor = Marshal.AllocHGlobal(inputLength);
				try
				{
					result = Native.NtQuerySecurityObject(NTInternal.CreateHandleRef(objectHandle), information, pDescriptor, (uint)inputLength, out requiredLength);
					NtException.CheckAndThrowException(result);
					byte[] bytes = new byte[requiredLength];
					Marshal.Copy(pDescriptor, bytes, 0, bytes.Length);
					return new System.Security.AccessControl.RawSecurityDescriptor(bytes, 0);
				}
				finally
				{
					Marshal.FreeHGlobal(pDescriptor);
				}
			}
		}

		public static void NtSetSecurityObject(SafeNtHandle objectHandle, SecurityInformation information, System.Security.AccessControl.GenericSecurityDescriptor descriptor)
		{
			byte[] bytes = new byte[descriptor.BinaryLength];
			descriptor.GetBinaryForm(bytes, 0);
			NtStatus result = Native.NtSetSecurityObject(NTInternal.CreateHandleRef(objectHandle), information, bytes);
			NtException.CheckAndThrowException(result);
		}
	}
}