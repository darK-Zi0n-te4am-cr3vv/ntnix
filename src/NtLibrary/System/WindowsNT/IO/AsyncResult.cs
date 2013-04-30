using System.WindowsNT.Events;
namespace System.WindowsNT.IO
{
	public class AsyncResult<TObject>
		where TObject : NtObject
	{
		internal AsyncResult() : base() { }

		public object AsyncState { get; internal set; }

		public TObject AsyncObject { get; internal set; }

		public NtEvent AsyncWaitEvent { get; internal set; }

		public bool CompletedSynchronously { get; internal set; }

		public bool IsCompleted
		{
			get
			{
				return this.AsyncWaitEvent.State;
			}
		}
	}
}