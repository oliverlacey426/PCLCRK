// PCL

namespace PCL
{
	public class ValidateExcept : ValidateBase
	{
		public Collection<string> Excepts { get; set; }
		public string ErrorMessage { get; set; }
		public ValidateExcept()
		{
			Excepts = new Collection<string>();
			ErrorMessage = "输入内容不能包含 %";
		}
		public ValidateExcept(Collection<string> Excepts, string ErrorMessage = "输入内容不能包含 %")
		{
			this.Excepts = new Collection<string>();
			this.Excepts = Excepts;
			this.ErrorMessage = ErrorMessage;
		}
		public ValidateExcept(IEnumerable<string> Excepts, string ErrorMessage = "输入内容不能包含 %")
			: this(new Collection<string>(new List<string>(Excepts)), ErrorMessage)
		{
		}
		public ValidateExcept(IEnumerable<char> Excepts, string ErrorMessage = "输入内容不能包含 %")
			: this(new Collection<string>(Excepts.Select([SpecialName] (char c) => c.ToString()).ToList()), ErrorMessage)
		{
		}
		public override string Validate(string Str)
		{
			foreach (string except in Excepts)
			{
				if (Str.IndexOfF(except, ignoreCase: true) >= 0)
				{
					if (Information.IsNothing(ErrorMessage))
					{
						ErrorMessage = "";
					}
					return ErrorMessage.Replace("%", except);
				}
			}
			return "";
		}
	}
}
