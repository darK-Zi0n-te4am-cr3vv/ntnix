using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Errors
{
	public class NameNotFoundException : NtException
	{
		public NameNotFoundException() : this("The specified object name was not found.") { }
		public NameNotFoundException(string message) : base(NtStatus.OBJECT_NAME_NOT_FOUND, message) { }
	}

	public class InvalidNameException : NtException
	{
		public InvalidNameException() : this("The specified object name was invalid.") { }
		public InvalidNameException(string message) : base(NtStatus.OBJECT_NAME_INVALID, message) { }
	}

	public class PathNotFoundException : NtException
	{
		public PathNotFoundException() : this("The specified object path was not found.") { }
		public PathNotFoundException(string message) : base(NtStatus.OBJECT_PATH_NOT_FOUND, message) { }
	}

	public class InvalidPathException : NtException
	{
		public InvalidPathException() : this("The specified object path was invalid.") { }
		public InvalidPathException(string message) : base(NtStatus.OBJECT_PATH_INVALID, message) { }
	}

	public class BadPathSyntaxException : NtException
	{
		public BadPathSyntaxException() : this("The syntax of the object path was not valid.") { }
		public BadPathSyntaxException(string message) : base(NtStatus.OBJECT_PATH_SYNTAX_BAD, message) { }
	}
}