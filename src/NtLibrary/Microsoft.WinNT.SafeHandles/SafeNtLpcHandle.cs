namespace Microsoft.WinNT.SafeHandles
{
    public sealed class SafeNtLpcHandle : SafeNtHandle
    {
        internal SafeNtLpcHandle(System.IntPtr preexistingHandle)
            : base(preexistingHandle) { }

        internal SafeNtLpcHandle(System.IntPtr preexistingHandle, bool ownsHandle)
            : base(preexistingHandle, ownsHandle) { }
    }
}
