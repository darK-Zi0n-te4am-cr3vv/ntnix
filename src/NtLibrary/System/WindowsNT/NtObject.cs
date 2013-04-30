#define EXTRA_INFO

using System.WindowsNT.PrivateImplementationDetails;
using System.WindowsNT.Security;
using Microsoft.WinNT.SafeHandles;

namespace System.WindowsNT
{
	public abstract class NtObject : System.IDisposable
	{
		protected NtObject() : base() { }

		~NtObject() { this.Dispose(false); }

		protected AccessMask ObjectAccessMask
		{ get { return Wrapper.NtQueryObjectBasic(this.GenericHandle).DesiredAccess; } }

		public AllowedObjectAttributes ObjectAttributes
		{ get { return Wrapper.NtQueryObjectBasic(this.GenericHandle).Attributes; } }

#if EXTRA_INFO
		public int DefaultNonPagedPoolCharge
		{ get { return unchecked((int)Wrapper.NtQueryObjectType(this.GenericHandle).DefaultNonPagedPoolCharge); } }

		public int DefaultPagedPoolCharge
		{ get { return unchecked((int)Wrapper.NtQueryObjectType(this.GenericHandle).DefaultPagedPoolCharge); } }
#endif

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Handle is managed
				if (!this.IsDisposed)
				{
					this.GenericHandle.Close();
				}
			}
		}

		public abstract SafeNtHandle GenericHandle { get; }

#if EXTRA_INFO
		internal GenericMapping GenericMapping
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).GenericMapping; } }

		protected bool HasAccess(AccessMask access)
		{
			return (this.ObjectAccessMask & access) != 0;
		}

		internal AccessMask NongenericAccessMask
		{ get { return Wrapper.RtlMapGenericMask(this.GenericMapping, this.ObjectAccessMask); } }
#endif

		public System.Security.AccessControl.RawSecurityDescriptor GetSecurityDescriptor(SecurityInformation information)
		{
			return Security.PrivateImplementationDetails.Wrapper.NtQuerySecurityObject(this.GenericHandle, information);
		}

#if EXTRA_INFO
		public int HandleCount
		{ get { return (int)Wrapper.NtQueryObjectBasic(this.GenericHandle).HandleCount; } }

		internal int HighWaterNumberOfHandles
		{ get { return (int)Wrapper.NtQueryObjectType(this.GenericHandle).HighWaterNumberOfHandles; } }

		internal int HighWaterNumberOfObjects
		{ get { return (int)Wrapper.NtQueryObjectType(this.GenericHandle).HighWaterNumberOfObjects; } }

		public bool InheritHandle
		{ get { return Wrapper.NtQueryObjectData(this.GenericHandle).InheritHandle; } }

		internal int InvalidAttributes
		{ get { return unchecked((int)Wrapper.NtQueryObjectType(this.GenericHandle).InvalidAttributes); } }
#endif

		public bool IsDisposed { get { return this.GenericHandle.IsClosed; } }
		
#if EXTRA_INFO
		internal bool MaintainHandleCount
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).MaintainHandleCount; } }

		internal int NonPagedPoolQuota
		{ get { return (int)Wrapper.NtQueryObjectBasic(this.GenericHandle).NonPagedPoolQuota; } }
#endif

		public string ObjectName
		{
			get
			{
				if (this.IsDisposed)
					throw new System.ObjectDisposedException(this.GetType().Name);
				return Wrapper.NtQueryObjectName(this.GenericHandle);
			}
		}

#if EXTRA_INFO
		internal int PagedPoolQuota
		{ get { return (int)Wrapper.NtQueryObjectBasic(this.GenericHandle).PagedPoolQuota; } }

		public PoolType PoolType
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).PoolType; } }

		public bool ProtectFromClose
		{ get { return Wrapper.NtQueryObjectData(this.GenericHandle).ProtectFromClose; } }

		public int ReferenceCount
		{ get { return (int)Wrapper.NtQueryObjectBasic(this.GenericHandle).ReferenceCount; } }

		internal bool SecurityRequired
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).SecurityRequired; } }
#endif

		public void SetSecurityDescriptor(SecurityInformation information, System.Security.AccessControl.RawSecurityDescriptor descriptor)
		{
			Security.PrivateImplementationDetails.Wrapper.NtSetSecurityObject(this.GenericHandle, information, descriptor);
		}

		internal string SymbolicLinkTargetName
		{ get { return Wrapper.NtQuerySymbolicLinkObject(this.GenericHandle); } }

#if EXTRA_INFO
		internal int TotalNumberOfHandles
		{ get { return (int)Wrapper.NtQueryObjectType(this.GenericHandle).TotalNumberOfHandles; } }

		internal int TotalNumberOfObjects
		{ get { return (int)Wrapper.NtQueryObjectType(this.GenericHandle).TotalNumberOfObjects; } }
#endif

		public string ObjectTypeName
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).TypeName; } }

#if EXTRA_INFO
		internal byte[] Unknown2
		{ get { return Wrapper.NtQueryObjectBasic(this.GenericHandle).Unknown2; } }

		internal AccessMask ValidAccessMask
		{ get { return Wrapper.NtQueryObjectType(this.GenericHandle).ValidAccessMask; } }

		internal ObjectTypeInformation[] Types
		{ get { return Wrapper.NtQueryObjectAll(this.GenericHandle); } }
#endif

		public virtual void Wait(bool alertable)
		{
			Wrapper.NtWaitForSingleObject(this.GenericHandle, alertable);
		}

		public virtual bool Wait(long timeout, bool alertable)
		{
			return Wrapper.NtWaitForSingleObject(this.GenericHandle, alertable, timeout);
		}
	}
}