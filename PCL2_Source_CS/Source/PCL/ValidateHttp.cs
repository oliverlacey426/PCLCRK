// PCL

namespace PCL
{
	public class ValidateHttp : ValidateBase
	{
		public override string Validate(string Str)
		{
			if (Str.EndsWithF("/"))
			{
				Str = Str.Substring(0, checked(Str.Length - 1));
			}
			if (!Str.RegexCheck("^(http[s]?)\\://"))
			{
				return "输入的网址无效！";
			}
			return "";
		}
	}
}
