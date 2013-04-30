namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeLSAPolicyHandle : SafeLSAHandle
	{
		internal SafeLSAPolicyHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeLSAPolicyHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}