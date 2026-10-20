// PCL

namespace PCL
{
	public class ValidateFolderPath : ValidateBase
	{
		public bool UseMinecraftCharCheck { get; set; }
		public ValidateFolderPath()
		{
			UseMinecraftCharCheck = true;
		}
		public ValidateFolderPath(bool UseMinecraftCharCheck)
		{
			this.UseMinecraftCharCheck = true;
			this.UseMinecraftCharCheck = UseMinecraftCharCheck;
		}
		public override string Validate(string Str)
		{
			Str = Str.Replace("/", "\\");
			if (!Str.TrimEnd('\\').EndsWithF(":"))
			{
				Str = Str.TrimEnd('\\');
			}
			string text = new ValidateNullOrWhiteSpace().Validate(Str);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				return text;
			}
			text = new ValidateLength(1, 254).Validate(Str);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				return text;
			}
			string text2 = Str.RegexSeek(".{2,}~\\d");
			if (text2 != null)
			{
				return $"文件夹名不能包含 {text2} 这一特殊格式！";
			}
			if (!Str.StartsWithF("\\\\Mac\\"))
			{
				foreach (string item in from p in Environment.GetLogicalDrives()
					where DirectoryUtils.Exists(p)
					select p)
				{
					string text3 = Conversions.ToString(item.Upper().First()) + ":\\";
					if (Operators.CompareString(Str.Upper(), text3, TextCompare: false) == 0)
					{
						return "";
					}
					if (!Str.StartsWithF(text3, ignoreCase: true))
					{
						continue;
					}
					goto IL_015c;
				}
				return "文件夹路径头存在错误！";
			}
			goto IL_015c;
			IL_015c:
			int num = ((!Str.StartsWithF("\\\\Mac\\")) ? 1 : 2);
			checked
			{
				int num2 = Str.Split("\\").Count() - 1;
				for (int num3 = num; num3 <= num2; num3++)
				{
					string text4 = Str.Split("\\")[num3];
					if (Operators.CompareString(new ValidateNullOrWhiteSpace().Validate(text4), "", TextCompare: false) != 0)
					{
						return "文件夹路径存在错误！";
					}
					string text5 = new ValidateExcept(new string(System.IO.Path.GetInvalidFileNameChars()) + (UseMinecraftCharCheck ? "!;" : ""), "路径中存在无效字符！").Validate(text4);
					if (Operators.CompareString(text5, "", TextCompare: false) != 0)
					{
						return text5;
					}
					if (text4.StartsWithF(" "))
					{
						return "文件夹名不能以空格开头！";
					}
					if (text4.EndsWithF(" "))
					{
						return "文件夹名不能以空格结尾！";
					}
					if (text4.EndsWithF("."))
					{
						return "文件夹名不能以小数点结尾！";
					}
					string text6 = new ValidateExceptSame(new string[25]
					{
						"CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
						"COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
						"LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
					}, "文件夹名不可为 %！").Validate(text4);
					if (Operators.CompareString(text6, "", TextCompare: false) != 0)
					{
						return text6;
					}
				}
				return "";
			}
		}
	}
}
