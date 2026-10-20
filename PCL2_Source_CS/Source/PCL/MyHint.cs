// PCL

namespace PCL
{
	[ContentProperty("Inlines")]
	[DesignerGenerated]
	public partial class MyHint : Border
	{
		public enum Themes
		{
			Blue,
			Red,
			Yellow
		}
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
		public int Uuid;
		private Themes _ColorType;
		public static readonly DependencyProperty IsWarnProperty = DependencyProperty.Register("IsWarn", typeof(bool), typeof(MyHint), new PropertyMetadata(true, [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
		{
			((VB_AnonymousDelegate_6<MyHint, DependencyPropertyChangedEventArgs>)([SpecialName] (MyHint d, DependencyPropertyChangedEventArgs e) =>
			{
				d.Theme = (Conversions.ToBoolean(e.NewValue) ? Themes.Red : Themes.Blue);
			}))((MyHint)a0, a1);
		}));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyHint), new PropertyMetadata("", [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
		{
			((VB_AnonymousDelegate_6<MyHint, DependencyPropertyChangedEventArgs>)([SpecialName] (MyHint d, DependencyPropertyChangedEventArgs e) =>
			{
				d.LabText.Text = Conversions.ToString(e.NewValue);
			}))((MyHint)a0, a1);
		}));
		private bool IsMouseDown;
		public bool HasBorder
		{
			get
			{
				return BorderThickness.Top > 0.0;
			}
			set
			{
				if (value)
				{
					BorderThickness = new Thickness(3.0, ModBase.GetWPFSize(1.0), ModBase.GetWPFSize(1.0), ModBase.GetWPFSize(1.0));
				}
				else
				{
					BorderThickness = new Thickness(3.0, 0.0, 0.0, 0.0);
				}
			}
		}
		public Themes Theme
		{
			get
			{
				return _ColorType;
			}
			set
			{
				_ColorType = value;
				UpdateUI();
			}
		}
		[Obsolete("IsWarn 已过时。请换用 Theme 属性。")]
		public bool IsWarn
		{
			get
			{
				return Theme == Themes.Red;
			}
			set
			{
				Theme = (value ? Themes.Red : Themes.Blue);
			}
		}
		public InlineCollection Inlines => LabText.Inlines;
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
		public bool CanClose
		{
			get
			{
				return BtnClose.Visibility == Visibility.Visible;
			}
			set
			{
				BtnClose.Visibility = ((!value) ? Visibility.Collapsed : Visibility.Visible);
			}
		}
		public string RelativeSetup { get; set; }



		public event ClickEventHandler Click;
		public MyHint()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				UpdateUI();
			};
			Loaded += MyHint_Loaded;
			MouseLeftButtonUp += MyHint_MouseUp;
			MouseLeftButtonDown += MyHint_MouseDown;
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				MyHint_MouseLeave();
			};
			Uuid = ModBase.GetUuid();
			_ColorType = Themes.Red;
			RelativeSetup = "";
			IsMouseDown = false;
			InitializeComponent();
			InitConnectEvents();
		}
		private void UpdateUI()
		{
			switch (Theme)
			{
			case Themes.Blue:
				Background = new ModBase.MyColor("#d9ecff");
				BorderBrush = new ModBase.MyColor("#1172D4");
				LabText.Foreground = new ModBase.MyColor("#0F64B8");
				BtnClose.Foreground = new ModBase.MyColor("#0F64B8");
				break;
			case Themes.Red:
				Background = new ModBase.MyColor("#ffdddf");
				BorderBrush = new ModBase.MyColor("#d82929");
				LabText.Foreground = new ModBase.MyColor("#bf0b0b");
				BtnClose.Foreground = new ModBase.MyColor("#bf0b0b");
				break;
			case Themes.Yellow:
				Background = new ModBase.MyColor("#ffebd7");
				BorderBrush = new ModBase.MyColor("#f57a00");
				LabText.Foreground = new ModBase.MyColor("#d86c00");
				BtnClose.Foreground = new ModBase.MyColor("#d86c00");
				break;
			}
		}
		private void MyHint_Loaded(object sender, RoutedEventArgs e)
		{
			if (CanClose && Settings.Get<bool>(RelativeSetup))
			{
				Visibility = Visibility.Collapsed;
			}
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
			Settings.SetSafe(RelativeSetup, true);
			ModAnimation.AniDispose(this, RemoveFromChildren: false);
		}
		private void MyHint_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseDown)
			{
				IsMouseDown = false;
				Logger.Info(string.Format("按下提示条{0}", string.IsNullOrEmpty(Name) ? "" : ("：" + Name)), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyHint.xaml.vb");
				Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				if (!e.Handled)
				{
					e.Handled = true;
					ModMain.RaiseCustomEvent(this);
				}
			}
		}
		private void MyHint_MouseDown(object sender, MouseButtonEventArgs e)
		{
			IsMouseDown = true;
		}
		private void MyHint_MouseLeave()
		{
			IsMouseDown = false;
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyIconButton.ClickEventHandler obj = BtnClose_Click;
						MyIconButton myIconButton = BtnClose;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnClose;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
		}

	}
}
