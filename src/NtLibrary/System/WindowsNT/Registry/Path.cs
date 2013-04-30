using System.Text.RegularExpressions;
using StringBuilder = System.Text.StringBuilder;
namespace System.WindowsNT.Registry
{
	public static class Path
	{
		private const char PATH_SEPARATOR = '\\';
		private static readonly string[] __hives = { "HKLM", "HKEY_LOCAL_MACHINE", "HKU", "HKEY_USERS", "HKCU", "HKEY_CURRENT_USER" };
		private static readonly string __pattern = string.Format("(?<HIVE>{0})", string.Join("|", __hives));
		private static readonly Regex __pathRegEx = new Regex(__pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public static string Normalize(string path)
		{
			return path.TrimEnd(PATH_SEPARATOR);
		}

		public static string Combine(params string[] paths)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string path in paths)
			{
				if (!path.StartsWith(PATH_SEPARATOR.ToString()))
				{
					sb.Append(PATH_SEPARATOR);
				}
				sb.Append(Path.Normalize(path));
			}
			return sb.ToString();
		}

		public static string GetKeyName(string path)
		{
			path = Path.Normalize(path);
			StringBuilder sb = new StringBuilder(path.Length);
			sb.Append(path.Substring(path.LastIndexOf(PATH_SEPARATOR) + 1));
			return sb.ToString();
		}

		public static string GetParentPath(string path)
		{
			path = Path.Normalize(path);
			StringBuilder sb = new StringBuilder(path.Length);
			sb.Append(path.Substring(0, path.LastIndexOf(PATH_SEPARATOR)));
			return sb.ToString();
		}

		public static string[] GetAncestory(string path)
		{
			return (path.StartsWith(PATH_SEPARATOR.ToString()) ? path.Substring(1) : path).Split(PATH_SEPARATOR);
		}

		public static string GetCommonParentPath(string path1, string path2, StringComparison comparison)
		{
			StringBuilder result = new StringBuilder();
			string[] p1 = GetAncestory(path1);
			string[] p2 = GetAncestory(path2);
			for (int i = 0; i < p1.Length & i < p2.Length; ++i)
			{
				if (p1[i].Equals(p2[i], comparison))
				{
					result.Append(PATH_SEPARATOR);
					result.Append(p1[i]);
				}
				else
				{
					break;
				}
			}
			return result.ToString();
		}
	}
}