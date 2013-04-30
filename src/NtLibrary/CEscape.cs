namespace System.Text
{
	public static class CStrings
	{
		public const char EscapeChar = '\\';
		public const char EscapeEscapeSequence = EscapeChar;
		public const char NullEscapeSequence = '0';

		public static string CEscape(string str)
		{
			System.Text.StringBuilder sb = new StringBuilder(str.Length);
			for (int i = 0; i < str.Length; ++i)
			{
				switch (str[i])
				{
					case EscapeChar:
						sb.Append(EscapeChar.ToString() + EscapeEscapeSequence);
						break;
					case '\0':
						sb.Append(EscapeChar.ToString() + NullEscapeSequence);
						break;
					default:
						sb.Append(str[i]);
						break;
				}
			}
			return sb.ToString();
		}

		public static string CUnescape(string str)
		{
			System.Text.StringBuilder sb = new StringBuilder(str.Length);
			for (int i = 0; i < str.Length; ++i)
			{
				if (str[i] == EscapeChar)
				{
					if (++i < str.Length)
					{
						switch (str[i])
						{
							case EscapeEscapeSequence:
								sb.Append(EscapeChar);
								break;
							case NullEscapeSequence:
								sb.Append('\0');
								break;
							default:
								throw new System.ArgumentException(string.Format("This escape sequence was not recognized: \"{0}{1}\"", EscapeChar, str[i]), "str");
						}
					}
					else
					{
						throw new System.Exception("Nothing followed the escape character.");
					}
				}
				else
				{
					sb.Append(str[i]);
				}
			}
			return sb.ToString();
		}
	}
}