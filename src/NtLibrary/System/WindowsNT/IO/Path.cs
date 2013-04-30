using System.WindowsNT.IO.PrivateImplementationDetails;
namespace System.WindowsNT.IO
{
	public static class Path
	{
		internal const char PATH_SEPARATOR = '\\';

		public static string ToNTPath(string path)
		{
			return Wrapper.RtlDosPathNameToNtPathName(path);
		}

		public static string GetParentDirectory(string path)
		{
			return path.Substring(0, path.LastIndexOf(PATH_SEPARATOR));
		}

		public static string GetFileName(string path)
		{
			return path.Substring(path.LastIndexOf(PATH_SEPARATOR) + 1);
		}
	}
}