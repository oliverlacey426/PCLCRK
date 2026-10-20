// PCL

namespace PCL
{
	public class ValidateNullOrWhiteSpace : ValidateBase
	{
		public override string Validate(string Str)
		{
			if (Information.IsNothing(Str) || string.IsNullOrWhiteSpace(Str))
			{
				return "输入内容不能为空！";
			}
			return "";
		}
	}
}
