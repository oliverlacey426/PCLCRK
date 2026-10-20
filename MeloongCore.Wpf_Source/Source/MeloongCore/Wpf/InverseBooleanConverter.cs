// MeloongCore.Wpf

namespace MeloongCore.Wpf
{
	public class InverseBooleanConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			bool result;
			return bool.TryParse(value.ToString(), out result) && !result;
		}
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			bool result;
			return bool.TryParse(value.ToString(), out result) && !result;
		}
	}
}
