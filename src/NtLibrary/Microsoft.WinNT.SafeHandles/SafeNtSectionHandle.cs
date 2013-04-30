namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtSectionHandle : SafeNtHandle
	{
		internal SafeNtSectionHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtSectionHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}