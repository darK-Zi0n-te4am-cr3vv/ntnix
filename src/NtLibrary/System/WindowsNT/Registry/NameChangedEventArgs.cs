namespace System.WindowsNT.Registry
{
	public class NameChangedEventArgs : System.EventArgs
	{
		public NameChangedEventArgs(string newName)
			: base()
		{
			this.NewName = newName;
		}

		public string NewName { get; private set; }
	}
}