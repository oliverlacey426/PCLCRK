// PCL

namespace PCL
{
	public class SettingService
	{
		public static readonly DependencyProperty KeyProperty = DependencyProperty.RegisterAttached("Key", typeof(string), typeof(SettingService), new PropertyMetadata(null));
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(string), typeof(SettingService), new PropertyMetadata(null));
		public static string GetKey(DependencyObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return Conversions.ToString(obj.GetValue(KeyProperty));
		}
		public static void SetKey(DependencyObject obj, string value)
		{
			obj.SetValue(KeyProperty, value);
		}
		public static string GetValue(DependencyObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return Conversions.ToString(obj.GetValue(ValueProperty));
		}
		public static void SetValue(DependencyObject obj, string value)
		{
			obj.SetValue(ValueProperty, value);
		}
		public static void ResetSettings(DependencyObject Target)
		{
			LogicalTreeHelper.GetChildren(Target).OfType<DependencyObject>().ForAll([SpecialName] (DependencyObject c) =>
			{
				ResetSettings(c);
			});
			string key = GetKey(Target);
			if (key != null)
			{
				Settings.Reset(key, PageInstanceLeft.Instance);
			}
		}
		public static void RefreshSettings(DependencyObject Target)
		{
			LogicalTreeHelper.GetChildren(Target).OfType<DependencyObject>().ForAll([SpecialName] (DependencyObject c) =>
			{
				RefreshSettings(c);
			});
			string key = GetKey(Target);
			if (key != null)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Settings.Get(key, PageInstanceLeft.Instance));
				if (Target is ISettingControl)
				{
					((ISettingControl)Target).RefreshSetting(Conversions.ToString(objectValue));
				}
			}
		}
		public static void SaveSetting(object sender)
		{
			if (ModAnimation.AniControlEnabled != 0 || !(sender is ISettingControl))
			{
				return;
			}
			string key = GetKey((DependencyObject)sender);
			if (key != null)
			{
				string currentSetting = ((ISettingControl)sender).GetCurrentSetting();
				if (currentSetting != null)
				{
					Settings.Set(key, currentSetting, PageInstanceLeft.Instance);
				}
			}
		}
	}
}
