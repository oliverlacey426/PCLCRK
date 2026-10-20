// PCL

namespace PCL
{
	[ContentProperty("Inlines")]
	[DesignerGenerated]
	public partial class MyExtraTextButton : Grid
	{
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
		public int Uuid;
		private string _Logo;
		private double _LogoScale;
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyExtraTextButton), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (sender != null)
			{
				((MyExtraTextButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));
		private bool _Show;
		private bool IsLeftMouseHeld;
		private const int AnimationColorIn = 120;
		private const int AnimationColorOut = 150;
		public string Logo
		{
			get
			{
				return _Logo;
			}
			set
			{
				if (Operators.CompareString(value, _Logo, TextCompare: false) != 0)
				{
					_Logo = value;
					Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
				}
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
				if (Path != null)
				{
					Path.RenderTransform = new ScaleTransform
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
		public bool Show
		{
			get
			{
				return _Show;
			}
			set
			{
				if (_Show == value)
				{
					return;
				}
				_Show = value;
				ModBase.RunInUi([SpecialName] () =>
				{
					if (value)
					{
						Opacity = 0.0;
						ModAnimation.AniStart(new ModAnimation.AniData[3]
						{
							ModAnimation.AaOpacity(this, 1.0 - Opacity, 80, 50),
							ModAnimation.AaScaleTransform(this, 0.15 - ((ScaleTransform)RenderTransform).ScaleX, 400, 50, new ModAnimation.AniEaseOutBack()),
							ModAnimation.AaScaleTransform(this, 0.85, 160, 50, new ModAnimation.AniEaseOutFluent())
						}, "MyExtraTextButton MainScale " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaOpacity(this, 0.0 - Opacity, 50, 50),
							ModAnimation.AaScaleTransform(this, 0.0 - ((ScaleTransform)RenderTransform).ScaleX, 100, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak))
						}, "MyExtraTextButton MainScale " + Conversions.ToString(Uuid));
					}
					IsHitTestVisible = value;
				});
			}
		}






		public event ClickEventHandler Click;
		public MyExtraTextButton()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshColor();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			Uuid = ModBase.GetUuid();
			_Logo = "";
			_LogoScale = 1.0;
			_Show = false;
			IsLeftMouseHeld = false;
			InitializeComponent();
			InitConnectEvents();
		}
		private void Button_LeftMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsLeftMouseHeld)
			{
				Logger.Info($"按下附加图标按钮：{Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraTextButton.xaml.vb");
				Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				e.Handled = true;
				ModMain.RaiseCustomEvent(this);
				Button_LeftMouseUp();
			}
		}
		private void Button_LeftMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsLeftMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
					ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
				}, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
			}
			IsLeftMouseHeld = true;
			Focus();
		}
		private void Button_LeftMouseUp()
		{
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
			IsLeftMouseHeld = false;
			RefreshColor();
		}
		private void Button_RightMouseUp()
		{
			if (!IsLeftMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
			}
			RefreshColor();
		}
		private void Button_MouseLeave()
		{
			IsLeftMouseHeld = false;
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
			RefreshColor();
		}
		public void RefreshColor()
		{
			try
			{
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					if (!IsEnabled)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrushGray4", 120), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
					}
					else if (IsMouseOver)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush4", 120), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush3", 150), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
					}
					return;
				}
				ModAnimation.AniStop("MyExtraTextButton Color " + Conversions.ToString(Uuid));
				if (!IsEnabled)
				{
					PanColor.SetResourceReference(System.Windows.Controls.Panel.BackgroundProperty, "ColorBrushGray4");
				}
				else if (IsMouseOver)
				{
					PanColor.SetResourceReference(System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush4");
				}
				else
				{
					PanColor.SetResourceReference(System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush3");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "刷新附加图标按钮颜色出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraTextButton.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MouseButtonEventHandler value2 = Button_LeftMouseUp;
						MouseButtonEventHandler value3 = Button_LeftMouseDown;
						MouseButtonEventHandler value4 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Button_LeftMouseUp();
						};
						MouseButtonEventHandler value5 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Button_RightMouseUp();
						};
						System.Windows.Input.MouseEventHandler value6 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							Button_MouseLeave();
						};
						System.Windows.Input.MouseEventHandler value7 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							RefreshColor();
						};
						System.Windows.Input.MouseEventHandler value8 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							RefreshColor();
						};
						Border border = PanClick;
						if (border != null)
						{
							border.MouseLeftButtonUp -= value2;
							border.MouseLeftButtonDown -= value3;
							border.MouseLeftButtonUp -= value4;
							border.MouseRightButtonUp -= value5;
							border.MouseLeave -= value6;
							border.MouseEnter -= value7;
							border.MouseLeave -= value8;
						}
						border = PanClick;
						if (border != null)
						{
							border.MouseLeftButtonUp += value2;
							border.MouseLeftButtonDown += value3;
							border.MouseLeftButtonUp += value4;
							border.MouseRightButtonUp += value5;
							border.MouseLeave += value6;
							border.MouseEnter += value7;
							border.MouseLeave += value8;
						}
			
			}
		}

	}
}
