namespace Microsoft.WinNT.SafeHandles
{
	[System.Diagnostics.DebuggerDisplay("{handle}")]
	public abstract class SafeNtHandle : SafeHandleZeroIsInvalid
	{
		// Methods
		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		private SafeNtHandle() : base(true) { }

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeNtHandle(System.IntPtr preexistingHandle)
			: this(preexistingHandle, true)
		{ base.SetHandle(preexistingHandle); }

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeNtHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{ base.SetHandle(preexistingHandle); }

		protected sealed override bool ReleaseHandle()
		{
			System.WindowsNT.PrivateImplementationDetails.NtStatus result = System.WindowsNT.PrivateImplementationDetails.Native.NtClose(base.handle);
			bool success = System.WindowsNT.Errors.NtException.IsSuccess(result);
			return success;
		}

		public sealed override int GetHashCode()
		{ return base.handle.ToInt32().GetHashCode(); }

		public sealed override string ToString()
		{ return this.handle.ToString(); }

		public sealed override bool Equals(object obj)
		{ return obj != null && this.GetType() == obj.GetType() && this.handle == ((SafeNtHandle)obj).handle; }
	}
}