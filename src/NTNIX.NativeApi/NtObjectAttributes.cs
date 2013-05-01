using System;
using System.Runtime.InteropServices;

namespace NTNIX.NativeApi
{
    [StructLayout(LayoutKind.Sequential)]
    public class NtObjectAttributes : IDisposable
    {
        public int Length;
        public IntPtr RootDirectory;
        private IntPtr m_ObjectName;
        public uint Attributes;
        public IntPtr SecurityDescriptor;
        public IntPtr SecurityQualityOfService;

        public NtUnicodeString ObjectName 
        {
            get
            {
                return (NtUnicodeString) Marshal.PtrToStructure(
                    m_ObjectName, typeof (NtUnicodeString));
            }
            set
            {
                var deleteOld = m_ObjectName != IntPtr.Zero;
                if (!deleteOld)
                {
                    m_ObjectName = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                }

                Marshal.StructureToPtr(value, m_ObjectName, deleteOld);
            }
        }

        public void Dispose()
        {
            if (m_ObjectName != IntPtr.Zero)
            {
                Marshal.DestroyStructure(m_ObjectName, typeof (NtUnicodeString));
                Marshal.FreeHGlobal(m_ObjectName);

                m_ObjectName = IntPtr.Zero;
            }
        }
    }
}
