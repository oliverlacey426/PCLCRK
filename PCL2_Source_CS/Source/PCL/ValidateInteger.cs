// PCL

namespace PCL
{
	public class ValidateInteger : ValidateBase
	{
		public int Min { get; set; }
		public int Max { get; set; }
		public ValidateInteger()
		{
			Max = int.MaxValue;
		}
		public ValidateInteger(int Min, int Max)
		{
			this.Max = int.MaxValue;
			this.Min = Min;
			this.Max = Max;
		}
		public override string Validate(string Str)
		{
			if (Str.Length > 9)
			{
				return "请输入一个大小合理的数字！";
			}
			if (!int.TryParse(Str, out var result))
			{
				return "请输入一个整数！";
			}
			if (result > Max)
			{
				return "不可超过 " + Conversions.ToString(Max) + "！";
			}
			if (result < Min)
			{
				return "不可低于 " + Conversions.ToString(Min) + "！";
			}
			return "";
		}
	}
}
