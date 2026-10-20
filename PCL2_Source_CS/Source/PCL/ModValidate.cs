// PCL

namespace PCL
{
	[StandardModule]
	public static class ModValidate
	{
		public static string Validate(string Text, IEnumerable<ValidateBase> ValidateRules)
		{
			string text = "";
			foreach (ValidateBase ValidateRule in ValidateRules)
			{
				text = ValidateRule.Validate(Text);
				if (text == null)
				{
					return "";
				}
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					return text;
				}
			}
			return text;
		}
	}
}
