// PCL

namespace PCL
{
	public class MyTextButton : System.Windows.Controls.Label
	{
		public delegate void ClickEventHandler(object sender, EventArgs e);
		public int Uuid;
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyTextButton), new PropertyMetadata("", [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
		{
			((VB_AnonymousDelegate_8<MyTextButton, DependencyPropertyChangedEventArgs>)([SpecialName] (MyTextButton sender, DependencyPropertyChangedEventArgs e) =>
			{
				if (!Operators.ConditionalCompareObjectEqual(e.OldValue, e.NewValue, TextCompare: false))
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(sender, 0.0 - sender.Opacity, 50),
						ModAnimation.AaCode([SpecialName] () =>
						{
							sender.Content = RuntimeHelpers.GetObjectValue(e.NewValue);
						}, 0, After: true),
						ModAnimation.AaOpacity(sender, 1.0, 170)
					}, "MyTextButton Text " + Conversions.ToString(sender.Uuid));
				}
			}))((MyTextButton)a0, a1);
		}));
		public bool IsMouseDown;
		private const int AnimationTimeIn = 100;
		private const int AnimationTimeOut = 200;
		private string ColorName;
		public string Text
		{
			get
			{
				return Conversions.ToString(GetValue(TextProperty));
			}
			set
			{
				SetValue(TextProperty, value);
			}
		}
		public event ClickEventHandler Click;
		public MyTextButton()
		{
			PreviewMouseLeftButtonDown += MyTextButton_MouseLeftButtonDown;
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				MyTextButton_MouseLeave();
			};
			PreviewMouseLeftButtonUp += MyTextButton_MouseLeftButtonUp;
			MouseEnter += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				RefreshColor();
			};
			Uuid = ModBase.GetUuid();
			IsMouseDown = false;
			SetResourceReference(System.Windows.Controls.Control.ForegroundProperty, "ColorBrush1");
			Background = ModSecret.ColorSemiTransparent;
		}
		private void MyTextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsMouseDown = true;
			e.Handled = true;
		}
		private void MyTextButton_MouseLeave()
		{
			IsMouseDown = false;
		}
		private void MyTextButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseDown)
			{
				IsMouseDown = false;
				Logger.Info($"按下文本按钮：{Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyTextButton.vb");
				Click?.Invoke(this, null);
				ModMain.RaiseCustomEvent(this);
				e.Handled = true;
			}
		}
		private void RefreshColor()
		{
			string text;
			int time;
			if (IsMouseDown)
			{
				text = "ColorBrush4";
				time = 30;
			}
			else if (IsMouseOver)
			{
				text = "ColorBrush3";
				time = 100;
			}
			else
			{
				text = "ColorBrush1";
				time = 200;
			}
			if (Operators.CompareString(ColorName, text, TextCompare: false) != 0)
			{
				ColorName = text;
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(this, System.Windows.Controls.Control.ForegroundProperty, text, time), "MyTextButton Color " + Conversions.ToString(Uuid));
					return;
				}
				ModAnimation.AniStop("MyTextButton Color " + Conversions.ToString(Uuid));
				SetResourceReference(System.Windows.Controls.Control.ForegroundProperty, text);
			}
		}
	}
}
