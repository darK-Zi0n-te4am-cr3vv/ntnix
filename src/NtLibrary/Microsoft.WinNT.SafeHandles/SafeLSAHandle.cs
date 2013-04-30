namespace Microsoft.WinNT.SafeHandles
{
	[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	public abstract class SafeLSAHandle : SafeHandleZeroIsInvalid
	{
		// Methods
		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		private SafeLSAHandle() : base(true) { }

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeLSAHandle(System.IntPtr preexistingHandle)
			: this(preexistingHandle, true)
		{ base.SetHandle(preexistingHandle); }

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeLSAHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{ base.SetHandle(preexistingHandle); }

		protected sealed override bool ReleaseHandle()
		{
			bool result = System.WindowsNT.Errors.NtException.IsSuccess(System.WindowsNT.Security.PrivateImplementationDetails.Native.LsaClose(base.handle));
			this.OnHandleReleased(new System.EventArgs());
			return result;
		}

		public sealed override int GetHashCode()
		{ return base.handle.ToInt32().GetHashCode(); }

		public sealed override string ToString()
		{ return this.handle.ToString(); }

		public sealed override bool Equals(object obj)
		{ return obj != null && this.GetType() == obj.GetType() && this.handle == ((SafeLSAHandle)obj).handle; }

		public event System.EventHandler HandleReleased;

		protected virtual void OnHandleReleased(System.EventArgs e)
		{
			if (this.HandleReleased != null)
				this.HandleReleased(this, e);
		}
	}
}