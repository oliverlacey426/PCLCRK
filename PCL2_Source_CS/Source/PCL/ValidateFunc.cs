// PCL

namespace PCL
{
	public class ValidateFunc : ValidateBase
	{
		private readonly Func<string, string> _func;
		public ValidateFunc(Func<string, string> F)
		{
			_func = F;
		}
		public override string Validate(string Str)
		{
			return _func(Str);
		}
	}
}
