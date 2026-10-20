// PCL

namespace PCL
{
	[ContentProperty("Inlines")]
	[DesignerGenerated]
	public partial class MyIconTextButton : Border
	{
		public delegate void CheckEventHandler(object sender, bool raiseByMouse);
		public delegate void ChangeEventHandler(object sender, bool raiseByMouse);
		public enum ColorState
		{
			Black,
			Highlight
		}
		public delegate void ClickEventHandler(object sender, ModBase.RouteEventArgs e);
		public int Uuid;
		private double _LogoScale;
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyIconTextButton), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (!Information.IsNothing(sender))
			{
				((MyIconTextButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));
		public static readonly DependencyProperty ColorTypeProperty = DependencyProperty.Register("ColorType", typeof(ColorState), typeof(MyIconTextButton), new PropertyMetadata(ColorState.Black));
		private bool IsMouseDown;
		private const int AnimationTimeOfMouseIn = 100;
		private const int AnimationTimeOfMouseOut = 150;
		public string Logo
		{
			get
			{
				return ShapeLogo.Data.ToString();
			}
			set
			{
				ShapeLogo.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
			}
		}
		public double LogoScale
		{
			get
			{
				return _LogoScale;
			}
			set
			{
				_LogoScale = value;
				if (!Information.IsNothing(ShapeLogo))
				{
					ShapeLogo.RenderTransform = new ScaleTransform
					{
						ScaleX = LogoScale,
						ScaleY = LogoScale
					};
				}
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
		public ColorState ColorType
		{
			get
			{
				return (ColorState)Conversions.ToInteger(GetValue(ColorTypeProperty));
			}
			set
			{
				if (ColorType != value)
				{
					SetValue(ColorTypeProperty, value);
					RefreshColor();
				}
			}
		}



		public event CheckEventHandler Check;
		public event ChangeEventHandler Change;
		public event ClickEventHandler Click;
		public MyIconTextButton()
		{
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				MyIconTextButton_MouseUp();
			};
			MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				MyIconTextButton_MouseDown();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				MyIconTextButton_MouseLeave();
			};
			MouseEnter += RefreshColor;
			Loaded += RefreshColor;
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor(RuntimeHelpers.GetObjectValue(a0), a1);
			};
			Uuid = ModBase.GetUuid();
			_LogoScale = 1.0;
			IsMouseDown = false;
			InitializeComponent();
		}
		private void MyIconTextButton_MouseUp()
		{
			if (IsMouseDown)
			{
				Logger.Info($"按下带图标按钮：{Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconTextButton.xaml.vb");
				IsMouseDown = false;
				Click?.Invoke(this, new ModBase.RouteEventArgs(RaiseByMouse: true));
				ModMain.RaiseCustomEvent(this);
				RefreshColor();
			}
		}
		private void MyIconTextButton_MouseDown()
		{
			IsMouseDown = true;
			RefreshColor();
		}
		private void MyIconTextButton_MouseLeave()
		{
			IsMouseDown = false;
			RefreshColor();
		}
		private void RefreshColor(object obj = null, object e = null)
		{
			try
			{
				if (IsLoaded && ModAnimation.AniControlEnabled == 0 && !false.Equals(RuntimeHelpers.GetObjectValue(e)))
				{
					switch (ColorType)
					{
					case ColorState.Black:
						if (IsMouseDown)
						{
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 70), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else if (IsMouseOver)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 100),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrushBg1", 100), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else if (IsEnabled)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush1", 150),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush1", 150)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - Background, 150), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrushGray5", 100),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrushGray5", 100)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - Background, 150), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						break;
					case ColorState.Highlight:
						if (IsMouseDown)
						{
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 70), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else if (IsMouseOver)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 100),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrushBg1", 100), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else if (IsEnabled)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 150),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 150)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - Background, 150), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[2]
							{
								ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrushGray5", 100),
								ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrushGray5", 100)
							}, "MyIconTextButton Checked " + Conversions.ToString(Uuid));
							ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - Background, 150), "MyIconTextButton Color " + Conversions.ToString(Uuid));
						}
						break;
					}
				}
				else
				{
					ModAnimation.AniStop("MyIconTextButton Checked " + Conversions.ToString(Uuid));
					ModAnimation.AniStop("MyIconTextButton Color " + Conversions.ToString(Uuid));
					switch (ColorType)
					{
					case ColorState.Black:
						Background = ModSecret.ColorSemiTransparent;
						ShapeLogo.SetResourceReference(Shape.FillProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
						LabText.SetResourceReference(TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
						break;
					case ColorState.Highlight:
						Background = ModSecret.ColorSemiTransparent;
						ShapeLogo.SetResourceReference(Shape.FillProperty, IsEnabled ? "ColorBrush3" : "ColorBrushGray5");
						LabText.SetResourceReference(TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush3" : "ColorBrushGray5");
						break;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "刷新带图标按钮颜色出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconTextButton.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}
}
