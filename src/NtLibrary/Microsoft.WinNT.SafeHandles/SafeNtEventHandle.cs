namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtEventHandle : SafeNtHandle
	{
		internal SafeNtEventHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtEventHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}