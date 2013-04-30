namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtThreadHandle : SafeNtHandle
	{
		internal SafeNtThreadHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtThreadHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}