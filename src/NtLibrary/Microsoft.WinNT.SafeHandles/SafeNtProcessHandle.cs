namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtProcessHandle : SafeNtHandle
	{
		internal SafeNtProcessHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtProcessHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}