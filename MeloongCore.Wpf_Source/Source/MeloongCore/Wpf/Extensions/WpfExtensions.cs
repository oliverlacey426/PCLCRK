// MeloongCore.Wpf.Extensions

namespace MeloongCore.Wpf.Extensions
{
	public static class WpfExtensions
	{
		public static bool Any(this UIElementCollection? arr)
		{
			if (arr == null)
			{
				return false;
			}
			return arr.Count > 0;
		}
	}
}
