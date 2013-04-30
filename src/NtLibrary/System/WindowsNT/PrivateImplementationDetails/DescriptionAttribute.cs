namespace System.WindowsNT.PrivateImplementationDetails
{
	[System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	internal class DescriptionAttribute : System.Attribute
	{
		public DescriptionAttribute(string description)
			: base()
		{ this.Description = description; }

		public string Description { get; private set; }
	}
}