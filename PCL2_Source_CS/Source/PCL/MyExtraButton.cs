// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class MyExtraButton : Grid
	{
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
		public delegate void RightClickEventHandler(object sender, MouseButtonEventArgs e);
		public delegate bool ShowCheckDelegate();
		private double _Progress;
		public int Uuid;
		private string _Logo;
		private double _LogoScale;
		private bool _Show;
		public ShowCheckDelegate ShowCheck;
		private bool _CanRightClick;
		private bool IsLeftMouseHeld;
		private bool IsRightMouseHeld;
		private const int AnimationColorIn = 120;
		private const int AnimationColorOut = 150;
		public double Progress
		{
			get
			{
				return _Progress;
			}
			set
			{
				if (_Progress != value)
				{
					_Progress = value;
					if (value < 0.0001)
					{
						PanProgress.Visibility = Visibility.Collapsed;
						return;
					}
					PanProgress.Visibility = Visibility.Visible;
					RectProgress.Rect = new Rect(0.0, 40.0 * (1.0 - value), 40.0, 40.0 * value);
				}
			}
		}
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
						Visibility = Visibility.Visible;
						ModAnimation.AniStart(new ModAnimation.AniData[3]
						{
							ModAnimation.AaScaleTransform(this, 0.3 - ((ScaleTransform)RenderTransform).ScaleX, 500, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
							ModAnimation.AaScaleTransform(this, 0.7, 500, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
							ModAnimation.AaHeight(this, 50.0 - Height, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
						}, "MyExtraButton MainScale " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[3]
						{
							ModAnimation.AaScaleTransform(this, 0.0 - ((ScaleTransform)RenderTransform).ScaleX, 100, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
							ModAnimation.AaHeight(this, 0.0 - Height, 400, 100, new ModAnimation.AniEaseOutFluent()),
							ModAnimation.AaCode([SpecialName] () =>
							{
								Visibility = Visibility.Collapsed;
							}, 0, After: true)
						}, "MyExtraButton MainScale " + Conversions.ToString(Uuid));
					}
					IsHitTestVisible = value;
				});
			}
		}
		public bool CanRightClick
		{
			get
			{
				return _CanRightClick;
			}
			set
			{
				_CanRightClick = value;
			}
		}







		public event ClickEventHandler Click;
		public event RightClickEventHandler RightClick;
		public MyExtraButton()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshColor();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			_Progress = 0.0;
			Uuid = ModBase.GetUuid();
			_Logo = "";
			_LogoScale = 1.0;
			_Show = false;
			ShowCheck = null;
			_CanRightClick = false;
			IsLeftMouseHeld = false;
			IsRightMouseHeld = false;
			InitializeComponent();
			InitConnectEvents();
		}
		public void ShowRefresh()
		{
			if (ShowCheck != null)
			{
				Show = ShowCheck();
			}
		}
		private void Button_LeftMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsLeftMouseHeld)
			{
				Logger.Info(string.Format("按下附加按钮{0}", RuntimeHelpers.GetObjectValue(Operators.ConditionalCompareObjectEqual(ToolTip, "", TextCompare: false) ? "" : Operators.ConcatenateObject("：", ToolTip))), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
				Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				e.Handled = true;
				Button_LeftMouseUp();
			}
		}
		private void Button_RightMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsRightMouseHeld)
			{
				Logger.Info(string.Format("右键按下附加按钮{0}", RuntimeHelpers.GetObjectValue(Operators.ConditionalCompareObjectEqual(ToolTip, "", TextCompare: false) ? "" : Operators.ConcatenateObject("：", ToolTip))), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
				RightClick?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				e.Handled = true;
				Button_RightMouseUp();
			}
		}
		private void Button_LeftMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsLeftMouseHeld && !IsRightMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
					ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
				}, "MyExtraButton Scale " + Conversions.ToString(Uuid));
			}
			IsLeftMouseHeld = true;
			Focus();
		}
		private void Button_RightMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (CanRightClick)
			{
				if (!IsLeftMouseHeld && !IsRightMouseHeld)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
						ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
					}, "MyExtraButton Scale " + Conversions.ToString(Uuid));
				}
				IsRightMouseHeld = true;
				Focus();
			}
		}
		private void Button_LeftMouseUp()
		{
			if (!IsRightMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
			}
			if (IsLeftMouseHeld)
			{
				ModMain.RaiseCustomEvent(this);
			}
			IsLeftMouseHeld = false;
			RefreshColor();
		}
		private void Button_RightMouseUp()
		{
			if (CanRightClick)
			{
				if (!IsLeftMouseHeld)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
				}
				IsRightMouseHeld = false;
				RefreshColor();
			}
		}
		private void Button_MouseLeave()
		{
			IsLeftMouseHeld = false;
			IsRightMouseHeld = false;
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
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
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrushGray4", 120), "MyExtraButton Color " + Conversions.ToString(Uuid));
					}
					else if (IsMouseOver)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush4", 120), "MyExtraButton Color " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(ModAnimation.AaColor(PanColor, System.Windows.Controls.Panel.BackgroundProperty, "ColorBrush3", 150), "MyExtraButton Color " + Conversions.ToString(Uuid));
					}
					return;
				}
				ModAnimation.AniStop("MyExtraButton Color " + Conversions.ToString(Uuid));
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
				Logger.Warn(ex2, "刷新图标按钮颜色出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		public void Ribble()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				Border border = new Border
				{
					CornerRadius = new CornerRadius(1000.0),
					BorderThickness = new Thickness(0.001),
					Opacity = 0.5,
					RenderTransformOrigin = new System.Windows.Point(0.5, 0.5),
					RenderTransform = new ScaleTransform()
				};
				border.SetResourceReference(Border.BackgroundProperty, "ColorBrush5");
				PanScale.Children.Insert(0, border);
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaScaleTransform(border, 13.0, 1000, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Strong, 0.3)),
					ModAnimation.AaOpacity(border, 0.0 - border.Opacity, 1000),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PanScale.Children.Remove(border);
					}, 0, After: true)
				}, "ExtraButton Ribble " + Conversions.ToString(ModBase.GetUuid()));
			});
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MouseButtonEventHandler value2 = Button_LeftMouseUp;
						MouseButtonEventHandler value3 = Button_RightMouseUp;
						MouseButtonEventHandler value4 = Button_LeftMouseDown;
						MouseButtonEventHandler value5 = Button_RightMouseDown;
						MouseButtonEventHandler value6 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Button_LeftMouseUp();
						};
						MouseButtonEventHandler value7 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Button_RightMouseUp();
						};
						System.Windows.Input.MouseEventHandler value8 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							Button_MouseLeave();
						};
						System.Windows.Input.MouseEventHandler value9 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							RefreshColor();
						};
						System.Windows.Input.MouseEventHandler value10 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							RefreshColor();
						};
						Border border = PanClick;
						if (border != null)
						{
							border.MouseLeftButtonUp -= value2;
							border.MouseRightButtonUp -= value3;
							border.MouseLeftButtonDown -= value4;
							border.MouseRightButtonDown -= value5;
							border.MouseLeftButtonUp -= value6;
							border.MouseRightButtonUp -= value7;
							border.MouseLeave -= value8;
							border.MouseEnter -= value9;
							border.MouseLeave -= value10;
						}
						border = PanClick;
						if (border != null)
						{
							border.MouseLeftButtonUp += value2;
							border.MouseRightButtonUp += value3;
							border.MouseLeftButtonDown += value4;
							border.MouseRightButtonDown += value5;
							border.MouseLeftButtonUp += value6;
							border.MouseRightButtonUp += value7;
							border.MouseLeave += value8;
							border.MouseEnter += value9;
							border.MouseLeave += value10;
						}
			
			}
		}

	}
}
