using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.IO.PrivateImplementationDetails;
namespace System.WindowsNT.IO
{
	public class NtSynchronizedFile : NtNonDirectoryFile
	{
		protected internal NtSynchronizedFile(SafeNtFileHandle handle) : base(handle) { }

		public long CurrentByteOffset
		{
			get { return Wrapper.NtQueryPositionInformationFile(this.Handle).CurrentByteOffset; }
			set
			{
				FilePositionInformation fileInformation = Wrapper.NtQueryPositionInformationFile(this.Handle);
				fileInformation.CurrentByteOffset = value;
				Wrapper.NtSetPositionInformationFile(this.Handle, fileInformation);
			}
		}

		public byte[] ReadBytes(int bytes)
		{
			return Wrapper.NtReadFile(this.Handle, (uint)bytes);
		}

		public void WriteBytes(byte[] bytes)
		{
			Wrapper.NtWriteFile(this.Handle, bytes);
		}

	}
}