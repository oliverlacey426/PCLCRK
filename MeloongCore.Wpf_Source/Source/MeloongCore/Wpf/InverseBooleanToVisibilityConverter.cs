// MeloongCore.Wpf

namespace MeloongCore.Wpf
{
	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Visibility.Visible;
			}
			bool result;
			return bool.TryParse(value.ToString(), out result) ? (result ? Visibility.Collapsed : Visibility.Visible) : Visibility.Visible;
		}
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			return value is Visibility visibility && visibility != Visibility.Visible;
		}
	}
}
