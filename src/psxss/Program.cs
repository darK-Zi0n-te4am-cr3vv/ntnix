using System;
using System.Runtime.InteropServices;
using NTNIX.NativeApi;
namespace NTNIX.SubsystemServer
{
    unsafe class Program
    {
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern NtStatus NtCreatePort([Out] out IntPtr PortHandle,
                                                    [In] ref NtObjectAttributes ObjectAttributes,
                                                    [In] uint MaxConnectInfoLength,
                                                    [In] uint MaxDataLength,
                                                    [In, Out] uint* Reserved = null);

        private static void Main(string[] args)
        {
            IntPtr handle;
            var attrs = new NtObjectAttributes
                {
                    ObjectName = new NtUnicodeString("ApiPort"),
                    Length = 
                };

            var status = NtCreatePort(out handle, ref attrs, 100, 100);
        }
    }
}
