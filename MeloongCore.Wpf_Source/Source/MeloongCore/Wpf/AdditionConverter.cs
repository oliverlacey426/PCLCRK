// MeloongCore.Wpf

namespace MeloongCore.Wpf
{
	public class AdditionConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return 0;
			}
			if (!double.TryParse(value.ToString(), out var result))
			{
				return 0;
			}
			double result2 = 1.0;
			if (parameter != null)
			{
				double.TryParse(parameter.ToString(), out result2);
			}
			return result + result2;
		}
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Binding.DoNothing;
			}
			if (!double.TryParse(value.ToString(), out var result))
			{
				return Binding.DoNothing;
			}
			double result2 = 1.0;
			if (parameter != null)
			{
				double.TryParse(parameter.ToString(), out result2);
			}
			return result - result2;
		}
	}
}
