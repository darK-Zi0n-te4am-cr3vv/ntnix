using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WinNT.SafeHandles;

namespace System.WindowsNT.LPC
{
    public class NtLpcPortBase : NtObject
    {
        internal NtLpcPortBase(SafeNtLpcHandle handle)
        {
            Handle = handle;
        }

        public SafeNtLpcHandle Handle { get; private set; }

        public override SafeNtHandle GenericHandle
        {
            get { return Handle; }
        }
    }
}
