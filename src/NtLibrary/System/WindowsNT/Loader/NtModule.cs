using Microsoft.WinNT.SafeHandles;
using System.WindowsNT.Loader.PrivateImplementationDetails;
namespace System.WindowsNT.Loader
{
	public class NtModule
	{
		public NtModule(string pathToFile, LibraryLoadFlags flags, string moduleFileName) { this.Handle = Wrapper.LdrLoadDll(pathToFile, flags, moduleFileName); }

		~NtModule() { this.Dispose(false); }

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.IsDisposed)
				{
					// Handle is managed
					this.Handle.Close();
				}
			}
		}

		public bool IsDisposed { get { return this.Handle.IsClosed; } }

		public SafeNtModuleHandle Handle { get; private set; }
	}
}