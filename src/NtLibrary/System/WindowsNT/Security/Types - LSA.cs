using System.Runtime.InteropServices;
namespace System.WindowsNT.Security
{
	internal struct LSAUnicodeString : System.IDisposable
	{
		/// <summary>Length of the <see cref="System.String"/>, in <see cref="byte"/>s.</summary>
		private short Length;
		/// <summary>Maximum length of the <see cref="System.String"/>, in <see cref="byte"/>s.</summary>
		private short MaximumLength;
		private unsafe char* String;

		private readonly bool shouldDispose;

		private unsafe LSAUnicodeString(short length, short maxLength, char* @string, bool shouldDispose)
		{
			if (@string == null)
				throw new System.ArgumentNullException("string");
			this.Length = length;
			this.MaximumLength = maxLength;
			this.String = @string;
			this.shouldDispose = shouldDispose;
		}

		public override string ToString() { unsafe { return Marshal.PtrToStringUni(new System.IntPtr(this.String), this.Length / 2); } }

		public static implicit operator LSAUnicodeString(string @string)
		{
			unsafe
			{
				return new LSAUnicodeString(
					(short)(@string.Length * sizeof(char)),
					(short)(@string.Length * sizeof(char)),
					(char*)System.Runtime.InteropServices.Marshal.StringToHGlobalUni(@string),
					true);
			}
		}

		public static implicit operator string(LSAUnicodeString wString) { return wString.ToString(); }

		public void Dispose()
		{
			if (this.shouldDispose)
			{
				unsafe
				{
					if (this.String != null)
					{
						System.Runtime.InteropServices.Marshal.FreeHGlobal(new System.IntPtr(this.String));
						this.String = null;
						this.Length = 0;
						this.MaximumLength = 0;
					}
				}
			}
		}
	}

	internal struct LSAObjectAttributes
	{
		public unsafe LSAObjectAttributes(byte dummy) : this(System.IntPtr.Zero, null, AllowedObjectAttributes.None, null, null) { }

		public unsafe LSAObjectAttributes(System.IntPtr rootDirectory, LSAUnicodeString* objectName, AllowedObjectAttributes attributes, System.Security.AccessControl.RawSecurityDescriptor securityDescriptor, SecurityQualityOfService* securityQualityOfService)
			: this()
		{
			this.Length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(this);
			this.RootDirectory = rootDirectory;
			this.ObjectName = objectName;
			this.Attributes = attributes;
			this.SecurityDescriptor = securityDescriptor;
			this.SecurityQualityOfService = securityQualityOfService;
		}

		public uint Length;
		public System.IntPtr RootDirectory;
		public unsafe LSAUnicodeString* ObjectName;
		public AllowedObjectAttributes Attributes;
		public System.Security.AccessControl.RawSecurityDescriptor SecurityDescriptor; // Points to type SECURITY_DESCRIPTOR
		public unsafe SecurityQualityOfService* SecurityQualityOfService; // Points to type SECURITY_QUALITY_OF_SERVICE
	}
}