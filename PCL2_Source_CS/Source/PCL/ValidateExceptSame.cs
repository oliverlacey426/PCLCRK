// PCL

namespace PCL
{
	public class ValidateExceptSame : ValidateBase
	{
		public Collection<string> Excepts { get; set; }
		public string ErrorMessage { get; set; }
		public bool IgnoreCase { get; set; }
		public ValidateExceptSame()
		{
			Excepts = new Collection<string>();
			IgnoreCase = false;
		}
		public ValidateExceptSame(Collection<string> Excepts, string ErrorMessage = "输入内容不能为 %", bool IgnoreCase = false)
		{
			this.Excepts = new Collection<string>();
			this.IgnoreCase = false;
			this.Excepts = Excepts;
			this.ErrorMessage = ErrorMessage;
			this.IgnoreCase = IgnoreCase;
		}
		public ValidateExceptSame(IEnumerable<string> Excepts, string ErrorMessage = "输入内容不能为 %", bool IgnoreCase = false)
			: this(new Collection<string>(new List<string>(Excepts)), ErrorMessage, IgnoreCase)
		{
		}
		public ValidateExceptSame(string Except, string ErrorMessage = "输入内容不能为 %", bool IgnoreCase = false)
			: this(new Collection<string> { Except }, ErrorMessage, IgnoreCase)
		{
		}
		public override string Validate(string Str)
		{
			if (Str == null)
			{
				return ErrorMessage.Replace("%", "null");
			}
			foreach (string except in Excepts)
			{
				if (IgnoreCase)
				{
					if (Operators.CompareString(Str.Lower(), except.Lower(), TextCompare: false) == 0)
					{
						return ErrorMessage.Replace("%", except);
					}
				}
				else if (Str.Equals(except))
				{
					return ErrorMessage.Replace("%", except);
				}
			}
			return "";
		}
	}
}
