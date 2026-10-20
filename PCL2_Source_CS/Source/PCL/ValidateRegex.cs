// PCL

namespace PCL
{
	public class ValidateRegex : ValidateBase
	{
		public string Regex { get; set; }
		public string ErrorDescription { get; set; }
		public ValidateRegex()
		{
			ErrorDescription = "正则检查失败！";
		}
		public ValidateRegex(string Regex, string ErrorDescription = "正则检查失败！")
		{
			this.ErrorDescription = "正则检查失败！";
			this.Regex = Regex;
			this.ErrorDescription = ErrorDescription;
		}
		public override string Validate(string Str)
		{
			if (!Str.RegexCheck(Regex))
			{
				return ErrorDescription;
			}
			return "";
		}
	}
}
