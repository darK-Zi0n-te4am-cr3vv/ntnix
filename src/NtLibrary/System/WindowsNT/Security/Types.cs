using System.Runtime.InteropServices;
namespace System.WindowsNT.Security
{
	[System.Flags()]
	public enum LuidAttributes : uint
	{
		EnabledByDefault = (0x00000001),
		Enabled = (0x00000002),
		Removed = (0X00000004),
		UsedForAccess = (0x80000000)
	}

	public enum Privilege : uint
	{
		MinWellKnown = (2),
		CREATE_TOKEN = (2),
		ASSIGNPRIMARYTOKEN = (3),
		LOCK_MEMORY = (4),
		INCREASE_QUOTA = (5),
		MACHINE_ACCOUNT = (6),
		UNSOLICITED_INPUT = (6),
		TCB = (7),
		SECURITY = (8),
		TAKE_OWNERSHIP = (9),
		LOAD_DRIVER = (10),
		SYSTEM_PROFILE = (11),
		SYSTEMTIME = (12),
		PROF_SINGLE_PROCESS = (13),
		INC_BASE_PRIORITY = (14),
		CREATE_PAGEFILE = (15),
		CREATE_PERMANENT = (16),
		Backup = (17),
		Restore = (18),
		SHUTDOWN = (19),
		DEBUG = (20),
		AUDIT = (21),
		SYSTEM_ENVIRONMENT = (22),
		ChangeNotify = (23),
		REMOTE_SHUTDOWN = (24),
		UNDOCK = (25),
		SYNC_AGENT = (26),
		ENABLE_DELEGATION = (27),
		MANAGE_VOLUME = (28),
		IMPERSONATE = (29),
		CreateGlobal = (30),
		MaxWellKnown = (CreateGlobal)
	}

	[System.Flags()]
	public enum TokenAccessMask : uint
	{
		AssignPrimary = (0x0001),
		Duplicate = (0x0002),
		Impersonate = (0x0004),
		Query = (0x0008),
		QuerySource = (0x0010),
		AdjustPrivileges = (0x0020),
		AdjustGroups = (0x0040),
		AdjustDefault = (0x0080),
		AdjustSessionID = (0x0100),
		AllAccessP = (AccessMask.StandardRightsRequired | AssignPrimary | Duplicate | Impersonate | Query | QuerySource | AdjustPrivileges | AdjustGroups | AdjustDefault),
		AllAccess = AllAccessP | AdjustSessionID
	}

	internal struct TokenPrivileges
	{
		public uint PrivilegeCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public LuidAndAttributes[] Privileges;

		public uint GetMarshaledSize()
		{
			return (uint)Marshal.SizeOf(this) - (uint)Marshal.SizeOf(typeof(LuidAndAttributes)) * ((uint)this.Privileges.Length - 1);
		}
	}

	internal struct LuidAndAttributes
	{
		public LUID Luid;
		public LuidAttributes Attributes;
	}

	internal struct LUID
	{
		private uint LowPart;
		private int HighPart;

		public static explicit operator LUID(uint @ulong)
		{
			return new LUID() { LowPart = @ulong };
		}
	}

	internal struct PrivilegeSet
	{
		public uint PrivilegeCount;
		public uint Control;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public LuidAndAttributes[] Privilege;
	}

#if CUSTOM_SECURITY

	public class PSecurityDescriptor
	{
		private unsafe SecurityDescriptor* pSecurityDescriptor;

		internal unsafe PSecurityDescriptor(SecurityDescriptor* pSecurityDescriptor)
		{ this.pSecurityDescriptor = pSecurityDescriptor; }
		
		~PSecurityDescriptor()
		{ unsafe { Marshal.FreeHGlobal(new System.IntPtr(this.pSecurityDescriptor)); } }

		public byte Revision
		{ get { unsafe { return this.pSecurityDescriptor->Revision; } } }

		public ushort Control
		{ get { unsafe { return this.pSecurityDescriptor->Control; } } }

		public PSID Group
		{
			get
			{
				unsafe
				{
					int offset = (int)this.pSecurityDescriptor->Group;
					return offset != 0 ? new PSID((SID*)((byte*)this.pSecurityDescriptor + offset)) : null;
				}
			}
		}

		public PSID Owner
		{
			get
			{
				unsafe
				{
					int offset = (int)this.pSecurityDescriptor->Owner;
					return offset != 0 ? new PSID((SID*)((byte*)this.pSecurityDescriptor + offset)) : null;
				}
			}
		}

		public PACL Sacl
		{
			get
			{
				unsafe
				{
					int offset = (int)this.pSecurityDescriptor->Sacl;
					return offset != 0 ? new PACL((ACL*)((byte*)this.pSecurityDescriptor + offset)) : null;
				}
			}
		}

		public PACL Dacl
		{
			get
			{
				unsafe
				{
					int offset = (int)this.pSecurityDescriptor->Dacl;
					return offset != 0 ? new PACL((ACL*)((byte*)this.pSecurityDescriptor + offset)) : null;
				}
			}
		}

		public override string ToString()
		{
			unsafe
			{
				return string.Format("PSecurityDescriptor {{Revision = {0}, Control = {1}, Group = {2}, Owner = {3}, Sacl = {4}, Dacl = {5}}}", this.Revision, this.Control, this.Group, this.Owner, this.Sacl, this.Dacl);
			}
		}
	}

	internal struct SecurityDescriptor
	{
		public byte Revision;
		private byte _PADDING_8;
		public ushort Control;
		public unsafe SID* Owner;
		public unsafe SID* Group;
		public unsafe ACL* Sacl;
		public unsafe ACL* Dacl;
	}

	public class PSID
	{
		private unsafe SID* pSID;

		internal unsafe PSID(SID* pSID)
		{ this.pSID = pSID; }

		public byte Revision
		{ get { unsafe { return this.pSID->Revision; } } }

		public byte SubAuthorityCount
		{ get { unsafe { return this.pSID->SubAuthorityCount; } } }

		public SIDIdentifierAuthority IdentifierAuthority
		{ get { unsafe { return new SIDIdentifierAuthority(this.pSID->IdentifierAuthority); } } }

		public int[] SubAuthority
		{
			get
			{
				unsafe
				{
					int[] result = new int[(int)this.pSID->SubAuthorityCount];
					Marshal.Copy(new System.IntPtr(this.pSID->SubAuthority), result, 0, result.Length);
					return result;
				}
			}
		}

		public override string ToString()
		{
			return string.Format("S-{0}-{1}-{2}", this.Revision, this.IdentifierAuthority, string.Join("-", System.Array.ConvertAll<int, string>(this.SubAuthority, sa => sa.ToString())));
			/*
			unsafe
			{
				UnicodeString str;
				NtStatus result = Native.RtlConvertSidToUnicodeString(out str, this.pSID, true);
				try
				{
					NtException.CheckAndThrowException(result);
					return (string)str;
				}
				finally
				{ Native.RtlFreeUnicodeString(ref str); }
			}
			*/
		}

		public struct SIDIdentifierAuthority
		{
			internal SIDIdentifierAuthority(SID.SidIdentifierAuthority other)
			{
				this.Value = new byte[6];
				unsafe
				{ Marshal.Copy(new System.IntPtr(other.Value), this.Value, 0, this.Value.Length); }
			}
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] Value;

			public override string ToString()
			{
				byte[] value = new byte[sizeof(long)];
				for (int i = 0; i < this.Value.Length; ++i)
				{ value[this.Value.Length - 1 - i] = this.Value[i]; }
				return System.BitConverter.ToInt64(value, 0).ToString();
			}
		}
	}

	internal struct SID
	{
		public byte Revision;
		public byte SubAuthorityCount;
		public SidIdentifierAuthority IdentifierAuthority;
		public unsafe fixed uint SubAuthority[1];

		public struct SidIdentifierAuthority
		{
			public unsafe fixed byte Value[6];
		}
	}

	public class PACL
	{
		private unsafe ACL* pACL;

		internal unsafe PACL(ACL* pACL)
		{ this.pACL = pACL; }

		public byte AclRevision
		{ get { unsafe { return this.pACL->AclRevision; } } }

		public ushort AclSize
		{ get { unsafe { return this.pACL->AclSize; } } }

		public PACE[] ACEs
		{
			get
			{
				unsafe
				{
					PACE[] result = new PACE[this.pACL->AceCount];
					for (int i = 0; i < result.Length; ++i)
					{ result[i] = new PACE((ACE*)this.pACL->Aces); }
					return result;
				}
			}
		}

		public override string ToString()
		{ return string.Format("ACL {{ACEs = [{0}], Revision = {1}}}", string.Join(", ", System.Array.ConvertAll(this.ACEs, ace => ace.ToString())), this.AclRevision); }
	}

	internal struct ACL
	{
		public byte AclRevision;
		private byte _PADDING_16;
		public ushort AclSize;
		public ushort AceCount;
		private ushort _PADDING_32;
		public unsafe fixed byte Aces[1];
	}

	public class PACE
	{
		private unsafe ACE* pACE;

		internal unsafe PACE(ACE* pACE)
		{
			this.pACE = pACE;
		}

		public ACEHeader AceHeader
		{ get { unsafe { return this.pACE->AceHeader; } } }
		public AccessMask Mask
		{ get { unsafe { return this.pACE->Mask; } } }
		public PSID SID
		{ get { unsafe { return new PSID(&this.pACE->SID); } } }

		public override string ToString()
		{
			return string.Format("ACE {{Header = {0}, Mask = 0x{1:X}, SID = {2}}}", this.AceHeader, this.Mask, this.SID);
		}
	}

	public struct ACEHeader
	{
		public ACEType AceType;
		public Flags AceFlags;
		public ushort AceSize;

		public enum Flags : byte
		{
			OBJECT_INHERIT_ACE = (0x1),
			CONTAINER_INHERIT_ACE = (0x2),
			NO_PROPAGATE_INHERIT_ACE = (0x4),
			INHERIT_ONLY_ACE = (0x8),
			INHERITED_ACE = (0x10),
			VALID_INHERIT_FLAGS = (0x1F)
		}

		public override string ToString()
		{
			return string.Format("ACE {{Type = {0}, Flags = {1}}}", this.AceType, this.AceFlags, this.AceSize);
		}
	}

	public enum ACEType : byte
	{
		ACCESS_ALLOWED_ACE_TYPE = (0x0),
		ACCESS_MIN_MS_ACE_TYPE = (0x0),
		ACCESS_DENIED_ACE_TYPE = (0x1),
		SYSTEM_AUDIT_ACE_TYPE = (0x2),
		SYSTEM_ALARM_ACE_TYPE = (0x3),
		ACCESS_MAX_MS_V2_ACE_TYPE = (0x3),
		ACCESS_ALLOWED_COMPOUND_ACE_TYPE = (0x4),
		ACCESS_MAX_MS_V3_ACE_TYPE = (0x4),
		ACCESS_ALLOWED_OBJECT_ACE_TYPE = (0x5),
		ACCESS_MIN_MS_OBJECT_ACE_TYPE = (0x5),
		ACCESS_DENIED_OBJECT_ACE_TYPE = (0x6),
		SYSTEM_AUDIT_OBJECT_ACE_TYPE = (0x7),
		SYSTEM_ALARM_OBJECT_ACE_TYPE = (0x8),
		ACCESS_MAX_MS_OBJECT_ACE_TYPE = (0x8),
		ACCESS_MAX_MS_V4_ACE_TYPE = (0x8),
		ACCESS_MAX_MS_ACE_TYPE = (0x8),
		ACCESS_ALLOWED_CALLBACK_ACE_TYPE = (0x9),
		ACCESS_DENIED_CALLBACK_ACE_TYPE = (0xA),
		ACCESS_ALLOWED_CALLBACK_OBJECT_ACE_TYPE = (0xB),
		ACCESS_DENIED_CALLBACK_OBJECT_ACE_TYPE = (0xC),
		SYSTEM_AUDIT_CALLBACK_ACE_TYPE = (0xD),
		SYSTEM_ALARM_CALLBACK_ACE_TYPE = (0xE),
		SYSTEM_AUDIT_CALLBACK_OBJECT_ACE_TYPE = (0xF),
		SYSTEM_ALARM_CALLBACK_OBJECT_ACE_TYPE = (0x10),
		ACCESS_MAX_MS_V5_ACE_TYPE = (0x10)
	}
	
	internal struct ACE
	{
		public ACEHeader AceHeader;
		public AccessMask Mask;
		public SID SID;
	}

#endif

	internal struct SecurityQualityOfService
	{
		public SecurityQualityOfService(SecurityImpersonationLevel impersonationLevel, bool contextTrackingMode, bool effectiveOnly)
			: this()
		{
			this.Length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(this);
			this.ImpersonationLevel = impersonationLevel;
			this.EffectiveOnly = effectiveOnly;
			this.ContextTrackingMode = contextTrackingMode;
		}

		public uint Length;
		public SecurityImpersonationLevel ImpersonationLevel;
		public bool ContextTrackingMode;
		public bool EffectiveOnly;
	}

	internal enum SecurityImpersonationLevel
	{
		SecurityAnonymous,
		SecurityIdentification,
		SecurityImpersonation,
		SecurityDelegation
	}

	public enum SecurityInformation : uint
	{
		Owner = (0x00000001),
		Group = (0x00000002),
		DACL = (0x00000004),
		SACL = (0x00000008),
		ProtectedDACL = (0x80000000),
		ProtectedSACL = (0x40000000),
		UnprotectedDACL = (0x20000000),
		UnprotectedSACL = (0x10000000)
	}
}