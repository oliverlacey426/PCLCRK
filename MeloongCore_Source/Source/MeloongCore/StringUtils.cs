// MeloongCore

namespace MeloongCore
{
	public static class StringUtils
	{
		public static string XmlEscape(string str)
		{
			if (str.StartsWithF("{"))
			{
				str = "{}" + str;
			}
			return StringExtensions.ReplaceLineEndings(str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
				.Replace("'", "&apos;")
				.Replace("\"", "&quot;"), "&#xa;");
		}
		public static string LikePatternEscape(string input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in input)
			{
				bool flag;
				switch (c)
				{
				case '#':
				case '*':
				case '?':
				case '[':
				case ']':
					flag = true;
					break;
				default:
					flag = false;
					break;
				}
				if (flag)
				{
					stringBuilder.Append('[').Append(c).Append(']');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string RegexEscape(string value)
		{
			return Regex.Escape(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string RegexUnescape(string value)
		{
			return Regex.Unescape(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string UrlEscape(string value)
		{
			return Uri.EscapeDataString(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string UrlUnescape(string value)
		{
			return Uri.UnescapeDataString(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string FormUrlEscape(string value)
		{
			return WebUtility.UrlEncode(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string FormUrlUnescape(string value)
		{
			return WebUtility.UrlDecode(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string HtmlEscape(string value)
		{
			return WebUtility.HtmlEncode(value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string HtmlUnescape(string value)
		{
			return WebUtility.HtmlDecode(value);
		}
		public static string FormatByteSize(long byteSize)
		{
			string arg = ((byteSize < 0) ? "-" : "");
			if (byteSize < 0)
			{
				byteSize *= -1;
			}
			if (byteSize < 1000)
			{
				return $"{arg}{byteSize} B";
			}
			if (byteSize < 1024000)
			{
				string text = Math.Round((double)byteSize / 1024.0).ToString(CultureInfo.InvariantCulture);
				return $"{arg}{Math.Round((double)byteSize / 1024.0, (3 - text.Length).Clamp(0, 2))} K";
			}
			if (byteSize < 1048576000)
			{
				string text2 = Math.Round((double)byteSize / 1024.0 / 1024.0).ToString(CultureInfo.InvariantCulture);
				return $"{arg}{Math.Round((double)byteSize / 1024.0 / 1024.0, (3 - text2.Length).Clamp(0, 2))} M";
			}
			string text3 = Math.Round((double)byteSize / 1024.0 / 1024.0 / 1024.0).ToString(CultureInfo.InvariantCulture);
			return $"{arg}{Math.Round((double)byteSize / 1024.0 / 1024.0 / 1024.0, (3 - text3.Length).Clamp(0, 2))} G";
		}
		public static string FormatTimeSpan(TimeSpan span, bool isShort)
		{
			string text = ((span.TotalMilliseconds > 0.0) ? "后" : "前");
			if (span.TotalMilliseconds < 0.0)
			{
				span = -span;
			}
			int num = (int)Math.Floor((double)span.Days / 30.0);
			if (isShort)
			{
				if (num >= 12)
				{
					return $"{Math.Floor((double)num / 12.0)} 年{text}";
				}
				if (num >= 2)
				{
					return $"{num} 个月{text}";
				}
				if (span.TotalDays >= 2.0)
				{
					return $"{span.Days} 天{text}";
				}
				if (span.TotalHours >= 1.0)
				{
					return $"{span.Hours} 小时{text}";
				}
				if (span.TotalMinutes >= 1.0)
				{
					return $"{span.Minutes} 分钟{text}";
				}
				if (span.TotalSeconds >= 1.0)
				{
					return $"{span.Seconds} 秒{text}";
				}
				return "1 秒" + text;
			}
			if (num >= 61)
			{
				return $"{Math.Floor((double)num / 12.0)} 年{text}";
			}
			if (num >= 12)
			{
				return string.Format("{0} 年{1}{2}", Math.Floor((double)num / 12.0), (num % 12 > 0) ? $" {num % 12} 个月" : "", text);
			}
			if (num >= 4)
			{
				return $"{num} 个月{text}";
			}
			if (num >= 1)
			{
				return string.Format("{0} 月{1}{2}", num, (span.Days % 30 > 0) ? $" {span.Days % 30} 天" : "", text);
			}
			if (span.TotalDays >= 4.0)
			{
				return $"{span.Days} 天{text}";
			}
			if (span.TotalDays >= 1.0)
			{
				return string.Format("{0} 天{1}{2}", span.Days, (span.Hours > 0) ? $" {span.Hours} 小时" : "", text);
			}
			if (span.TotalHours >= 10.0)
			{
				return $"{span.Hours} 小时{text}";
			}
			if (span.TotalHours >= 1.0)
			{
				return string.Format("{0} 小时{1}{2}", span.Hours, (span.Minutes > 0) ? $" {span.Minutes} 分钟" : "", text);
			}
			if (span.TotalMinutes >= 10.0)
			{
				return $"{span.Minutes} 分钟{text}";
			}
			if (span.TotalMinutes >= 1.0)
			{
				return string.Format("{0} 分{1}{2}", span.Minutes, (span.Seconds > 0) ? $" {span.Seconds} 秒" : "", text);
			}
			if (span.TotalSeconds >= 1.0)
			{
				return $"{span.Seconds} 秒{text}";
			}
			return "1 秒" + text;
		}
		public static Version ParseVersionWithDefaults(string value)
		{
			while (value.Count((char c) => c == '.') < 3)
			{
				value += ".0";
			}
			return Version.Parse(value);
		}
	}
}
