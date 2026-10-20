// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class MyIconButton : Border
	{
		public delegate void ClickEventHandler(object sender, EventArgs e);
		public enum Themes
		{
			Color,
			White,
			Black,
			Red,
			Custom
		}
		public int Uuid;
		private double _LogoScale;
		private SolidColorBrush _Foreground;
		private bool IsMouseDown;
		private const int AnimationColorIn = 120;
		private const int AnimationColorOut = 150;
		public string Logo
		{
			get
			{
				return Path.Data.ToString();
			}
			set
			{
				Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
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
				if (!Information.IsNothing(Path))
				{
					Path.RenderTransform = new ScaleTransform
					{
						ScaleX = LogoScale,
						ScaleY = LogoScale
					};
				}
			}
		}
		public Themes Theme { get; set; }
		public SolidColorBrush Foreground
		{
			get
			{
				return _Foreground;
			}
			set
			{
				_Foreground = value;
				checked
				{
					ModAnimation.AniControlEnabled++;
					RefreshAnim();
					ModAnimation.AniControlEnabled--;
				}
			}
		}
		public bool CanClick { get; set; }


		public event ClickEventHandler Click;
		public MyIconButton()
		{
			MouseLeftButtonUp += Button_MouseUp;
			MouseLeftButtonDown += Button_MouseDown;
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Button_MouseUp();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Button_MouseLeave();
			};
			MouseEnter += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshAnim();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshAnim();
			};
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshAnim();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshAnim();
			};
			Uuid = ModBase.GetUuid();
			_LogoScale = 1.0;
			Theme = Themes.Color;
			_Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 128, 128));
			IsMouseDown = false;
			CanClick = true;
			InitializeComponent();
		}
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (CanClick && IsMouseDown)
			{
				Logger.Info(string.Format("按下图标按钮{0}", string.IsNullOrEmpty(Name) ? "" : ("：" + Name)), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconButton.xaml.vb");
				Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				e.Handled = true;
				Button_MouseUp();
				ModMain.RaiseCustomEvent(this);
			}
		}
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (CanClick)
			{
				IsMouseDown = true;
				Focus();
				ModAnimation.AniStart(ModAnimation.AaScaleTransform(PanBack, 0.8 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)), "MyIconButton Scale " + Conversions.ToString(Uuid));
			}
		}
		private void Button_MouseUp()
		{
			if (CanClick)
			{
				if (IsMouseDown)
				{
					IsMouseDown = false;
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScaleTransform(PanBack, 1.05 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaScaleTransform(PanBack, -0.05, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
					}, "MyIconButton Scale " + Conversions.ToString(Uuid));
				}
				RefreshAnim();
			}
		}
		private void Button_MouseLeave()
		{
			if (CanClick)
			{
				IsMouseDown = false;
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanBack, 1.0 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutFluent()) }, "MyIconButton Scale " + Conversions.ToString(Uuid));
				RefreshAnim();
			}
		}
		public void RefreshAnim()
		{
			try
			{
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					if (PanBack.Background == null)
					{
						PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
					}
					if (Path.Fill == null)
					{
						switch (Theme)
						{
						case Themes.Red:
							Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
							break;
						case Themes.Black:
							Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
							break;
						case Themes.Custom:
							Path.Fill = new ModBase.MyColor(160.0, Foreground);
							break;
						}
					}
					List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
					if (!IsEnabled)
					{
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrushGray5", 120));
					}
					else if (IsMouseOver)
					{
						switch (Theme)
						{
						case Themes.Color:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush2", 120));
							break;
						case Themes.White:
							list.Add(ModAnimation.AaColor(PanBack, Border.BackgroundProperty, new ModBase.MyColor(50.0, 255.0, 255.0, 255.0) - PanBack.Background, 120));
							break;
						case Themes.Red:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(255.0, 76.0, 76.0) - Path.Fill, 120));
							break;
						case Themes.Black:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(230.0, 0.0, 0.0, 0.0) - Path.Fill, 120));
							break;
						case Themes.Custom:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(255.0, Foreground) - Path.Fill, 120));
							break;
						}
					}
					else
					{
						switch (Theme)
						{
						case Themes.Color:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush4", 150));
							PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case Themes.White:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush8", 150));
							list.Add(ModAnimation.AaColor(PanBack, Border.BackgroundProperty, new ModBase.MyColor(0.0, 255.0, 255.0, 255.0) - PanBack.Background, 150));
							break;
						case Themes.Red:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, 255.0, 76.0, 76.0) - Path.Fill, 150));
							PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case Themes.Black:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, 0.0, 0.0, 0.0) - Path.Fill, 150));
							PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						case Themes.Custom:
							list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, Foreground) - Path.Fill, 150));
							PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
							break;
						}
					}
					ModAnimation.AniStart(list, "MyIconButton Color " + Conversions.ToString(Uuid));
					return;
				}
				ModAnimation.AniStop("MyIconButton Color " + Conversions.ToString(Uuid));
				if (IsEnabled)
				{
					switch (Theme)
					{
					case Themes.Color:
						Path.SetResourceReference(Shape.FillProperty, "ColorBrush5");
						break;
					case Themes.White:
						Path.SetResourceReference(Shape.FillProperty, "ColorBrush8");
						break;
					case Themes.Red:
						Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
						break;
					case Themes.Black:
						Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
						break;
					case Themes.Custom:
						Path.Fill = new ModBase.MyColor(160.0, Foreground);
						break;
					}
				}
				else
				{
					Path.SetResourceReference(Shape.FillProperty, "ColorBrushGray5");
				}
				PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "刷新图标按钮动画状态出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconButton.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}
}
