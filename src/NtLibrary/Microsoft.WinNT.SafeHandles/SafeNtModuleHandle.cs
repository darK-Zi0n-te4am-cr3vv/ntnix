using System;
namespace Microsoft.WinNT.SafeHandles
{
	[System.Diagnostics.DebuggerDisplay("{handle}")]
	public class SafeNtModuleHandle : Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid
	{
		internal SafeNtModuleHandle(IntPtr handle) : this(true, handle) { }

		internal SafeNtModuleHandle(bool ownsHandle, IntPtr handle) : base(ownsHandle) { this.SetHandle(handle); }

		protected override bool ReleaseHandle()
		{
			return System.WindowsNT.Errors.NtException.IsSuccess(System.WindowsNT.Loader.PrivateImplementationDetails.Native.LdrUnloadDll(NTInternal.CreateHandleRef(this)));
		}
	}
}