namespace System.Runtime.InteropServices
{
	public sealed class HGlobal : ConstrainedExecution.CriticalFinalizerObject, System.IDisposable
	{
		public HGlobal(int size) : this(new IntPtr(size)) { }

		public HGlobal(long size) : this(new IntPtr(size)) { }

		public HGlobal(IntPtr size)
			: base()
		{
			this.Address = Marshal.AllocHGlobal(size);
			this.AllocatedSize = size;
			GC.AddMemoryPressure(this.AllocatedSize64);
		}

		public IntPtr AllocatedSize { get; private set; }
		public uint AllocatedSize32 { get { return (uint)this.AllocatedSize; } }
		public long AllocatedSize64 { get { return (long)this.AllocatedSize; } }
		public IntPtr Address { get; private set; }
		public bool IsDisposed { get; private set; }

		[ConstrainedExecution.ReliabilityContract(ConstrainedExecution.Consistency.WillNotCorruptState, ConstrainedExecution.Cer.None), ConstrainedExecution.PrePrepareMethod()]
		~HGlobal() { this.Dispose(false); }

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing)
				{
					//Release managed
				}
				//Release unmanaged
				Marshal.FreeHGlobal(this.Address);
				GC.RemoveMemoryPressure(this.AllocatedSize64);
				this.AllocatedSize = IntPtr.Zero;
				this.IsDisposed = true;
				this.Address = IntPtr.Zero;
			}
		}

		public void ReAlloc(int cb) { this.ReAlloc(new IntPtr(cb)); }

		public void ReAlloc(uint cb) { this.ReAlloc(new IntPtr(cb)); }

		public void ReAlloc(IntPtr cb)
		{
			if (!this.IsDisposed)
			{
				this.Address = Marshal.ReAllocHGlobal(this.Address, cb);
				GC.RemoveMemoryPressure(this.AllocatedSize64);
				this.AllocatedSize = cb;
			}
			else
			{
				throw new System.ObjectDisposedException("Cannot re-allocate a freed buffer.");
			}
		}

		public override string ToString()
		{ return this.Address.ToString(); }

		public static explicit operator IntPtr(HGlobal ptr)
		{ return ptr.Address; }
	}
}