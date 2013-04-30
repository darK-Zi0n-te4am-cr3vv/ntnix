//#define Win32Error

using System.WindowsNT.PrivateImplementationDetails;

namespace System.WindowsNT.Errors
{
	public class NtException : System.Exception
	{
		internal NtException(string message) : this((NtStatus)0x80000000, message) { }

		internal NtException(NtStatus exceptionCode) : this(exceptionCode, GetMessage(exceptionCode)) { }

		internal NtException(NtStatus exceptionCode, string message)
			: this(exceptionCode, message, null)
		{
		}

		internal NtException(NtStatus exceptionCode, string message, Exception innerException)
			: base(message, innerException)
		{
			this.ExceptionCode = exceptionCode;
		}

		internal NtStatus ExceptionCode { get; private set; }

		internal static string GetMessage(NtStatus errorCode)
		{
#if Win32Error
			System.Text.StringBuilder lpBuffer = new System.Text.StringBuilder(0x200);
			if (Native.FormatMessage(0x3200, System.IntPtr.Zero, Errors.PrivateImplementationDetails.Native.RtlNtStatusToDosError(errorCode), 0, lpBuffer, lpBuffer.Capacity, System.IntPtr.Zero) != 0)
			{
				return lpBuffer.ToString();
			}
			return "Unknown Error";
#else
			return Extensions.GetDescription(errorCode);
#endif
		}


		[System.Diagnostics.DebuggerStepThrough()]
		internal static void CheckAndThrowException(NtStatus errorCode)
		{
			if (!IsSuccess(errorCode))
			{
				Exception ex;
				switch (errorCode)
				{
					case NtStatus.OBJECT_NAME_NOT_FOUND:
						ex = new NameNotFoundException();
						break;
					case NtStatus.OBJECT_NAME_INVALID:
						ex = new InvalidNameException();
						break;
					case NtStatus.OBJECT_PATH_NOT_FOUND:
						ex = new PathNotFoundException();
						break;
					case NtStatus.OBJECT_PATH_INVALID:
						ex = new InvalidPathException();
						break;
					case NtStatus.OBJECT_PATH_SYNTAX_BAD:
						ex = new BadPathSyntaxException();
						break;
					case NtStatus.ACCESS_DENIED:
						ex = new System.UnauthorizedAccessException();
						break;
					case NtStatus.ACCESS_VIOLATION:
						ex = new System.AccessViolationException();
						break;
					case NtStatus.INVALID_PARAMETER:
						ex = new System.ArgumentException(GetMessage(NtStatus.INVALID_PARAMETER));
						break;
					default:
						ex = new NtException(errorCode);
						break;
				}
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}

		internal static bool IsSuccess(NtStatus ntStatus)
		{
			return unchecked((int)ntStatus) >= 0;
		}
	}
}