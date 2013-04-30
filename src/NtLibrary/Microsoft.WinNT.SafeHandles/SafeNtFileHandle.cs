namespace Microsoft.WinNT.SafeHandles
{
	public class SafeNtFileHandle : SafeNtHandle
	{
		internal SafeNtFileHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtFileHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}