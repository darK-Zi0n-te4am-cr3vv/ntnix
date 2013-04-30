namespace Microsoft.WinNT.SafeHandles
{
	public sealed class SafeNtRegistryHandle : SafeNtHandle
	{
		internal SafeNtRegistryHandle(System.IntPtr preexistingHandle) : base(preexistingHandle) { }

		internal SafeNtRegistryHandle(System.IntPtr preexistingHandle, bool ownsHandle)
			: base(preexistingHandle, ownsHandle) { }
	}
}