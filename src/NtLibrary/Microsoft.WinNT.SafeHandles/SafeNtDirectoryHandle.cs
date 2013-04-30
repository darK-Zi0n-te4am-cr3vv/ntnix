namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtDirectoryHandle : SafeNtHandle
	{
		internal SafeNtDirectoryHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtDirectoryHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}