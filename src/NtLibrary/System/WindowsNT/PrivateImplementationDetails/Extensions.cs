namespace System.WindowsNT.PrivateImplementationDetails
{
	internal static class Extensions
	{
		private static readonly System.Reflection.FieldInfo[] fields = typeof(NtStatus).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

		public static string GetDisplayString(object obj, params string[] propertyNames)
		{
			return GetDisplayString(obj, obj.GetType(), propertyNames == null ? null : new Collections.Generic.List<string>(propertyNames));
		}

		public static string GetDisplayString(object obj, Collections.Generic.ICollection<string> propertyNames)
		{
			return GetDisplayString(obj, obj.GetType(), propertyNames);
		}

		public static string GetDisplayStringFromType<T>(T obj)
		{
			return GetDisplayString(obj, typeof(T), null);
		}

		private static string GetDisplayString(object obj, System.Type type, Collections.Generic.ICollection<string> propertyNames)
		{
			Reflection.PropertyInfo[] pis = type.GetProperties();
			System.Collections.Generic.List<string> strs = new System.Collections.Generic.List<string>(pis.Length);
			foreach (Reflection.PropertyInfo pi in pis)
			{
				if (pi.CanRead && (propertyNames == null || propertyNames.Contains(pi.Name)))
				{
					object value = pi.GetValue(obj, null);
					string v = value as string;
					strs.Add(string.Format(@"{0} = {1}", pi.Name, value != null ? v != null ? v : value.ToString() : "(null)"));
				}
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder(type.Name);
			sb.Append(' ');
			sb.Append('{');
			if (strs.Count > 0)
			{
				sb.Append(strs[0].ToString());
				for (int i = 1; i < strs.Count; i++)
				{
					sb.Append(", ");
					sb.Append(strs[i].ToString());
				}
			}
			sb.Append('}');
			return sb.ToString();
		}

		public static string GetDescription(NtStatus variable)
		{
			System.Reflection.FieldInfo fieldInfo = System.Array.Find<System.Reflection.FieldInfo>(fields, delegate(System.Reflection.FieldInfo fi)
			{
				return System.Enum.GetName(variable.GetType(), variable).Equals(fi.Name);
			}
			);
			DescriptionAttribute[] descriptions = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			string concatenated = string.Concat(System.Array.ConvertAll<DescriptionAttribute, string>(descriptions, d => d.Description));
			string result = string.IsNullOrEmpty(concatenated) ? string.Format(variable.ToString(), "No description found for: {0}.") : concatenated;
			return result;
		}

		public static string GetDescription<T>(T variable)
		{
			System.Reflection.FieldInfo[] fields = variable.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			System.Reflection.FieldInfo fieldInfo = System.Array.Find<System.Reflection.FieldInfo>(fields, delegate(System.Reflection.FieldInfo fi)
			{
				return System.Enum.GetName(variable.GetType(), variable).Equals(fi.Name);
			}
			);
			DescriptionAttribute[] descriptions = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			string concatenated = string.Concat(System.Array.ConvertAll<DescriptionAttribute, string>(descriptions,	d => d.Description));
			string result = string.IsNullOrEmpty(concatenated) ? string.Format(variable.ToString(), "No description found for: {0}.") : concatenated;
			return result;
		}

		/*
		public static string GetNtStatusMessage(NtStatus errorCode)
		{
			const int ALLOCATE_BUFFER = 0x00000100,
				IGNORE_INSERTS = 0x00000200,
				FROM_STRING = 0x00000400,
				FROM_HMODULE = 0x00000800,
				FROM_SYSTEM = 0x00001000,
				ARGUMENt_ARRAY = 0x00002000;
			System.IntPtr hModule = Native.LoadLibrary(@"C:\Windows\System32\ntdll.dll");
			string result = string.Empty;
			unsafe
			{
				char* lpBuffer = null;
				try
				{
					if (Native.FormatMessage(ALLOCATE_BUFFER | FROM_HMODULE, hModule, (int)errorCode, 0, new System.IntPtr(&lpBuffer), 0) != 0)
					{
						result = new string(lpBuffer);
					}
					else
					{
						result = string.Format("Unknown error code: {0}", errorCode);
					}
				}
				finally
				{
					Native.FreeLibrary(hModule);
					if (lpBuffer != null)
					{
						Native.LocalFree(new System.IntPtr(lpBuffer));
					}
				}
			}
			return result;
		}
		*/
	}
}