using System.WindowsNT.Memory.PrivateImplementationDetails;
using System.WindowsNT.Processes;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Memory
{
	public class NtVirtualMemoryAddress : System.IDisposable
	{
		private NtVirtualMemoryAddress(SafeNtProcessHandle processHandle, System.IntPtr address, bool autoRelease)
		{
			this.ProcessHandle = processHandle;
			this.Address = address;
			if (!autoRelease)
			{
				System.GC.SuppressFinalize(this);
			}
		}

		public NtVirtualMemoryAddress(NtProcess process, System.IntPtr? baseAddress, byte zeroBits, ref System.UIntPtr regionSize, MemoryAllocationType allocationType, PageProtection protection, bool autoRelease)
			: this(process.Handle, Wrapper.NtAllocateVirtualMemory(process.Handle, baseAddress, zeroBits, ref regionSize, allocationType, protection), autoRelease) { }

		~NtVirtualMemoryAddress() { this.Dispose(false); }

		public void Dispose() { this.Dispose(true); }

		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing)
				{
				}

				Wrapper.NtFreeVirtualMemory(this.ProcessHandle, this.Address);
			}
		}

		public readonly System.IntPtr Address;
		public IntPtr AllocationBase { get { return this.BasicInformation.AllocationBase; } }
		public PageProtection AllocationProtect { get { return this.BasicInformation.AllocationProtect; } }
		public IntPtr BaseAddress { get { return this.BasicInformation.BaseAddress; } }
		public PageProtection Protection { get { return this.BasicInformation.Protect; } }
		public IntPtr RegionSize { get { return this.BasicInformation.RegionSize; } }
		public MemoryAllocationType State { get { return this.BasicInformation.State; } }
		public SectionAllocationAttributes Type { get { return this.BasicInformation.Type; } }
		public bool IsDisposed { get; private set; }
		public SafeNtProcessHandle ProcessHandle { get; private set; }

		private MemoryBasicInformation BasicInformation { get { return Wrapper.NtQueryVirtualMemoryBasic(this.ProcessHandle, this.Address); } }

		public uint Read(System.IO.UnmanagedMemoryStream ums, uint bytesToRead)
		{ return Wrapper.NtReadVirtualMemory(this.ProcessHandle, this.Address, ums, bytesToRead); }

		public uint Read(byte[] bytes, int offset, uint bytesToRead)
		{ return Wrapper.NtReadVirtualMemory(this.ProcessHandle, this.Address, bytes, offset, bytesToRead); }

		public byte[] Read(uint bytesToRead)
		{ return Wrapper.NtReadVirtualMemory(this.ProcessHandle, this.Address, bytesToRead); }

		public uint Write(System.IO.UnmanagedMemoryStream ums, uint bytesToWrite)
		{ return Wrapper.NtWriteVirtualMemory(this.ProcessHandle, this.Address, ums, bytesToWrite); }

		public uint Write(byte[] bytes, int offset, uint bytesToWrite)
		{ return Wrapper.NtWriteVirtualMemory(this.ProcessHandle, this.Address, bytes, offset, bytesToWrite); }

		public uint Write(byte[] bytes, uint bytesToWrite) { return this.Write(bytes, 0, bytesToWrite); }

		public uint Write(byte[] bytes) { return this.Write(bytes, (uint)bytes.Length); }

		public PageProtection Protect(ref IntPtr baseAddress, ref uint bytesToProtect, PageProtection protection)
		{
			return Wrapper.NtProtectVirtualMemory(this.ProcessHandle, ref baseAddress, ref bytesToProtect, protection);
		}
	}
}