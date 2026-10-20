// PCL

namespace PCL
{
	public class ValidateNullable : ValidateBase
	{
		public override string Validate(string Str)
		{
			if (Information.IsNothing(Str) || string.IsNullOrEmpty(Str))
			{
				return null;
			}
			return "";
		}
	}
}
