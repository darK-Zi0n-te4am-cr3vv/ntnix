using System;
using System.Runtime.InteropServices;

namespace NTNIX.NativeApi
{
    [StructLayout(LayoutKind.Sequential)]
    struct NtUnicodeString : IDisposable
    {
        [MarshalAs(UnmanagedType.U2)]
        private ushort Length;
        [MarshalAs(UnmanagedType.U2)]
        private ushort MaximumLength;
        [MarshalAs(UnmanagedType.LPWStr)]
        private IntPtr Buffer;

        public NtUnicodeString(string str)
        {
            Length = (ushort)(str.Length * 2);
            MaximumLength = (ushort)(Length + 2);
            Buffer = Marshal.StringToHGlobalUni(str);
        }

        public override string ToString()
        {
            return Marshal.PtrToStringUni(Buffer);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(Buffer);
            Buffer = IntPtr.Zero;
        }
    }
}
