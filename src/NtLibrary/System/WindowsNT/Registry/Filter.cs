using System.WindowsNT.PrivateImplementationDetails;
using System.WindowsNT.Registry.PrivateImplementationDetails;
namespace System.WindowsNT.Registry
{
	public class Filter : System.IDisposable
	{
		protected long Cookie { get; private set; }

		protected Filter(long cookie) { this.Cookie = cookie; }

		public Filter()
		{
			unsafe
			{
				byte* pContext = stackalloc byte[128];
				this.Cookie = Registry.PrivateImplementationDetails.Wrapper.CmRegistryCallback(this.Callback, new System.IntPtr(pContext));
			}
		}

		private NtStatus Callback(System.IntPtr context, RegNotifyClass arg1, System.IntPtr arg2)
		{
			System.Diagnostics.Debug.WriteLine("Callback works!");
			this.OnCall(new System.EventArgs());
			return NtStatus.SUCCESS;
		}

		protected virtual void OnCall(EventArgs e)
		{
			if (this.Call != null)
				this.Call(this, e);
		}

		public event System.EventHandler Call;

		~Filter() { this.Dispose(false); }

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public bool IsDisposed { get; private set; }

		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing)
				{
				}
				Registry.PrivateImplementationDetails.Wrapper.CmUnRegistryCallback(this.Cookie);
				this.IsDisposed = true;
			}
		}
	}
}