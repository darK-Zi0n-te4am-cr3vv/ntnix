namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtTokenHandle : SafeNtHandle
	{
		internal SafeNtTokenHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtTokenHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}