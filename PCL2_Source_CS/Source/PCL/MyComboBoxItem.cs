// PCL

namespace PCL
{
	public class MyComboBoxItem : ComboBoxItem
	{
		public int Uuid;
		private const int AnimationTimeIn = 100;
		private const int AnimationTimeOut = 300;
		private string BackColorName;
		private double FontOpacity;
		public MyComboBoxItem()
		{
			Unselected += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseMove += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			Selected += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshColor();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeftButtonUp += MyComboBoxItem_MouseLeftButtonUp;
			Uuid = ModBase.GetUuid();
			Style = (Style)FindResource("MyComboBoxItem");
		}
		private void RefreshColor()
		{
			string text;
			double num;
			int time;
			if (IsSelected)
			{
				text = "ColorBrush6";
				num = 1.0;
				time = 100;
			}
			else if (IsMouseOver)
			{
				text = "ColorBrush8";
				num = 1.0;
				time = 100;
			}
			else if (IsEnabled)
			{
				text = "ColorBrushTransparent";
				num = 1.0;
				time = 300;
			}
			else
			{
				text = "ColorBrushTransparent";
				num = 0.4;
				time = 300;
			}
			if (Operators.CompareString(BackColorName, text, TextCompare: false) != 0 || FontOpacity != num)
			{
				BackColorName = text;
				FontOpacity = num;
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaColor(this, System.Windows.Controls.Control.BackgroundProperty, BackColorName, time),
						ModAnimation.AaOpacity(this, FontOpacity - Opacity, time)
					}, "ComboBoxItem Color " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStop("ComboBoxItem Color " + Conversions.ToString(Uuid));
					SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, BackColorName);
					Opacity = FontOpacity;
				}
			}
		}
		public override string ToString()
		{
			return Content.ToString();
		}
		public static implicit operator string(MyComboBoxItem Value)
		{
			return Value.Content.ToString();
		}
		private void MyComboBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Logger.Info($"选择下拉列表项：{ToString()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBoxItem.vb");
		}
	}
}
