// PCL

namespace PCL
{
	public class ValidateLength : ValidateBase
	{
		public int Min { get; set; }
		public int Max { get; set; }
		public ValidateLength()
		{
			Min = 0;
			Max = int.MaxValue;
		}
		public ValidateLength(int Min, int Max = int.MaxValue)
		{
			this.Min = 0;
			this.Max = int.MaxValue;
			this.Min = Min;
			this.Max = Max;
		}
		public override string Validate(string Str)
		{
			if (Strings.Len(Str) != Max && Max == Min)
			{
				return $"长度必须为 {Max} 个字符！";
			}
			if (Strings.Len(Str) > Max)
			{
				return $"长度最长为 {Max} 个字符！";
			}
			if (Strings.Len(Str) < Min)
			{
				return $"长度至少需 {Min} 个字符！";
			}
			return "";
		}
	}
}
