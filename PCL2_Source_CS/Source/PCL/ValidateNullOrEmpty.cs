// PCL

namespace PCL
{
	public class ValidateNullOrEmpty : ValidateBase
	{
		public override string Validate(string Str)
		{
			if (Information.IsNothing(Str) || string.IsNullOrEmpty(Str))
			{
				return "输入内容不能为空！";
			}
			return "";
		}
	}
}
