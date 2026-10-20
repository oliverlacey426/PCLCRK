// PCL

namespace PCL
{
	public class MyCard : Grid
	{
		public delegate void PreviewSwapEventHandler(object sender, ModBase.RouteEventArgs e);
		public delegate void SwapEventHandler(object sender, ModBase.RouteEventArgs e);
		[CompilerGenerated]
		internal sealed class _Closure___R31_3
		{
			public PageDownloadResourceDetail _VB_NonLocal_2;
			public _Closure___R31_3(_Closure___R31_3 arg0)
			{
				if (arg0 != null)
				{
					_VB_NonLocal_2 = arg0._VB_NonLocal_2;
				}
			}
			[SpecialName]
			internal void _Lambda___R6(object a0, MouseButtonEventArgs a1)
			{
				_VB_NonLocal_2.Install_Click((MyListItem)a0, a1);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___R31_4
		{
			public PageDownloadInstall _VB_NonLocal_3;
			public _Closure___R31_4(_Closure___R31_4 arg0)
			{
				if (arg0 != null)
				{
					_VB_NonLocal_3 = arg0._VB_NonLocal_3;
				}
			}
			[SpecialName]
			internal void _Lambda___R7(object a0, MouseButtonEventArgs a1)
			{
				_VB_NonLocal_3.Fabric_Selected((MyListItem)a0, a1);
			}
		}
		private readonly Grid MainGrid;
		private readonly Border MainBorder;
		private TextBlock _MainTextBlock;
		private System.Windows.Shapes.Path _MainSwap;
		public int Uuid;
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MyCard), new PropertyMetadata(""));
		private bool IsLoad;
		private const double DropShadowIdleOpacity = 0.07;
		private const double DropShadowHoverOpacity = 0.4;
		private bool IsHeightAnimating;
		private double ActualUsedHeight;
		public FrameworkElement SwapControl;
		private bool _IsSwapped;
		public static readonly DependencyProperty IsSwapedProperty = DependencyProperty.Register("IsSwaped", typeof(bool), typeof(MyCard), new PropertyMetadata(false, [SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (sender != null && sender is MyCard)
			{
				((MyCard)sender).IsSwapped = Conversions.ToBoolean(e.NewValue);
			}
		}));
		private bool IsSwapMouseDown;
		private bool IsCustomMouseDown;
		public const int SwapedHeight = 40;
		public MyDropShadow MainChrome { get; }
		public UIElement BorderChild
		{
			get
			{
				return MainBorder.Child;
			}
			set
			{
				MainBorder.Child = value;
			}
		}
		public TextBlock MainTextBlock
		{
			get
			{
				Init();
				return _MainTextBlock;
			}
			set
			{
				_MainTextBlock = value;
			}
		}
		public System.Windows.Shapes.Path MainSwap
		{
			get
			{
				Init();
				return _MainSwap;
			}
			set
			{
				_MainSwap = value;
			}
		}
		public InlineCollection Inlines => MainTextBlock.Inlines;
		public CornerRadius CornerRadius
		{
			get
			{
				return MainChrome.CornerRadius;
			}
			set
			{
				MainChrome.CornerRadius = value;
				MainBorder.CornerRadius = value;
			}
		}
		public string Title
		{
			get
			{
				return Conversions.ToString(GetValue(TitleProperty));
			}
			set
			{
				SetValue(TitleProperty, value);
				if (_MainTextBlock != null)
				{
					MainTextBlock.Text = value;
				}
			}
		}
		public bool HasMouseAnimation { get; set; }
		public bool UseAnimation { get; set; }
		public bool CanSwap { get; set; }
		public int SwapType { get; set; }
		public bool IsSwapped
		{
			get
			{
				return _IsSwapped;
			}
			set
			{
				if (_IsSwapped == value)
				{
					return;
				}
				_IsSwapped = value;
				if (SwapControl != null)
				{
					if (!IsSwapped && SwapControl is StackPanel)
					{
						ref FrameworkElement swapControl = ref SwapControl;
						StackPanel Stack = (StackPanel)swapControl;
						StackInstall(ref Stack, SwapType, Title);
						swapControl = Stack;
					}
					if (IsLoaded)
					{
						SwapControl.Visibility = Visibility.Visible;
						TriggerForceResize();
						UpdateCursorStyle();
						ModAnimation.AniStart(ModAnimation.AaRotateTransform(MainSwap, (double)((!_IsSwapped) ? 180 : (SwapLogoRight ? 270 : 0)) - ((RotateTransform)MainSwap.RenderTransform).Angle, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)), "MyCard Swap " + Conversions.ToString(Uuid), RefreshTime: true);
					}
				}
			}
		}
		[Obsolete("IsSwaped 存在拼写错误，请换用 IsSwapped 属性。")]
		public bool IsSwaped
		{
			get
			{
				return Conversions.ToBoolean(GetValue(IsSwapedProperty));
			}
			set
			{
				SetValue(IsSwapedProperty, value);
			}
		}
		public bool SwapLogoRight { get; set; }
		public event PreviewSwapEventHandler PreviewSwap;
		public event SwapEventHandler Swap;
		public MyCard()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				Init();
			};
			MouseEnter += MyCard_MouseEnter;
			MouseLeave += MyCard_MouseLeave;
			SizeChanged += MySizeChanged;
			MouseLeftButtonDown += MyCard_MouseLeftButtonDown;
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				MyCard_MouseLeftButtonUp();
			};
			MouseLeave += MyCard_MouseLeave_Swap;
			MouseMove += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				UpdateCursorStyle();
			};
			Uuid = ModBase.GetUuid();
			IsLoad = false;
			HasMouseAnimation = true;
			UseAnimation = true;
			IsHeightAnimating = false;
			CanSwap = false;
			_IsSwapped = false;
			SwapLogoRight = false;
			IsSwapMouseDown = false;
			IsCustomMouseDown = false;
			MainChrome = new MyDropShadow
			{
				Margin = new Thickness(-3.0, -3.0, -3.0, -3.0 - ModBase.GetWPFSize(1.0)),
				ShadowRadius = 3.0,
				Opacity = 0.07,
				CornerRadius = new CornerRadius(5.0)
			};
			MainChrome.SetResourceReference(MyDropShadow.ColorProperty, "ColorObject1");
			Children.Insert(0, MainChrome);
			MainBorder = new Border
			{
				Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(245, byte.MaxValue, byte.MaxValue, byte.MaxValue)),
				CornerRadius = new CornerRadius(5.0),
				IsHitTestVisible = false
			};
			Children.Insert(1, MainBorder);
			MainGrid = new Grid();
			Children.Add(MainGrid);
		}
		private void Init()
		{
			if (IsLoad)
			{
				return;
			}
			IsLoad = true;
			if (MainTextBlock == null)
			{
				MainTextBlock = new TextBlock
				{
					HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness(15.0, 12.0, 0.0, 0.0),
					FontWeight = FontWeights.Bold,
					FontSize = 13.0,
					IsHitTestVisible = false
				};
				MainTextBlock.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush1");
				MainTextBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Title")
				{
					Source = this,
					Mode = BindingMode.OneWay
				});
				MainGrid.Children.Add(MainTextBlock);
			}
			if (CanSwap || SwapControl != null)
			{
				if (SwapControl == null && Children.Count > 3)
				{
					SwapControl = (FrameworkElement)Children[3];
				}
				MainSwap = new System.Windows.Shapes.Path
				{
					HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
					Stretch = Stretch.Uniform,
					Height = 6.0,
					Width = 10.0,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness(0.0, 17.0, 16.0, 0.0),
					Data = (Geometry)new GeometryConverter().ConvertFromString("M2,4 l-2,2 10,10 10,-10 -2,-2 -8,8 -8,-8 z"),
					RenderTransform = new RotateTransform(180.0),
					RenderTransformOrigin = new System.Windows.Point(0.5, 0.5)
				};
				MainSwap.SetResourceReference(Shape.FillProperty, "ColorBrush1");
				MainGrid.Children.Add(MainSwap);
			}
			if (IsSwapped && SwapControl != null)
			{
				MainSwap.RenderTransform = new RotateTransform(SwapLogoRight ? 270 : 0);
				SwapControl.Visibility = Visibility.Collapsed;
				bool useAnimation = UseAnimation;
				UseAnimation = false;
				Height = 40.0;
				ModAnimation.AniStop("MyCard Height " + Conversions.ToString(Uuid));
				IsHeightAnimating = false;
				ModBase.RunInUi([SpecialName] () =>
				{
					UseAnimation = useAnimation;
				}, ForceWaitUntilLoaded: true);
			}
		}
		public void StackInstall()
		{
			ref FrameworkElement swapControl = ref SwapControl;
			StackPanel Stack = (StackPanel)swapControl;
			StackInstall(ref Stack, SwapType, Title);
			swapControl = Stack;
			TriggerForceResize();
		}
		public static void StackInstall(ref StackPanel Stack, int Type, string CardTitle = "")
		{
			if (Stack.Tag == null)
			{
				return;
			}
			bool isBadDisplay = false;
			switch (Type)
			{
			case 3:
				Stack.Tag = ModBase.SortByComparison((List<ModDownload.DlOptiFineListEntry>)Stack.Tag, [SpecialName] (ModDownload.DlOptiFineListEntry a, ModDownload.DlOptiFineListEntry b) => ModMinecraft.CompareVersionGE(a.DisplayName, b.DisplayName));
				break;
			case 4:
			case 10:
				Stack.Tag = ModBase.SortByComparison((List<ModDownload.DlLiteLoaderListEntry>)Stack.Tag, [SpecialName] (ModDownload.DlLiteLoaderListEntry a, ModDownload.DlLiteLoaderListEntry b) => ModMinecraft.CompareVersionGE(a.Inherit, b.Inherit));
				break;
			case 6:
				Stack.Tag = ((List<ModDownload.DlForgeVersionEntry>)Stack.Tag).OrderByDescending([SpecialName] (ModDownload.DlForgeVersionEntry a) => a).ToList();
				break;
			case 8:
			case 9:
				Stack.Tag = ModBase.SortByComparison((List<ResourceVersion>)Stack.Tag, [SpecialName] (ResourceVersion a, ResourceVersion b) => DateTime.Compare(a.ReleaseDate, b.ReleaseDate) > 0);
				isBadDisplay = ((List<ResourceVersion>)Stack.Tag).DistinctBy([SpecialName] (ResourceVersion a) => a.Display).Count() != ((List<ResourceVersion>)Stack.Tag).Count;
				break;
			}
			switch (Type)
			{
			case 6:
				ModDownloadLib.ForgeDownloadListItemPreload(Stack, (List<ModDownload.DlForgeVersionEntry>)Stack.Tag, ModDownloadLib.ForgeSave_Click, IsSaveOnly: true);
				break;
			case 8:
				PageDownloadResourceDetail.ResourceFilesCardPreload(Stack, (List<ResourceVersion>)Stack.Tag);
				break;
			}
			_Closure___R31_3 obj2 = default;
			_Closure___R31_4 obj = default;
			foreach (object item in (IEnumerable)Stack.Tag)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				switch (Type)
				{
				case 0:
					Stack.Children.Add(((ModMinecraft.McInstance)objectValue).ToListItem(PageSelectRight.McInstanceListContent));
					break;
				case 2:
					Stack.Children.Add(ModDownloadLib.McDownloadListItem((JObject)objectValue, ModDownloadLib.McDownloadMenuSave, IsSaveOnly: true));
					break;
				case 3:
					Stack.Children.Add(ModDownloadLib.OptiFineDownloadListItem((ModDownload.DlOptiFineListEntry)objectValue, ModDownloadLib.OptiFineSave_Click, IsSaveOnly: true));
					break;
				case 6:
					Stack.Children.Add(ModDownloadLib.ForgeDownloadListItem((ModDownload.DlForgeVersionEntry)objectValue, ModDownloadLib.ForgeSave_Click, IsSaveOnly: true));
					break;
				case 7:
					Stack.Children.Add(ModDownloadLib.McDownloadListItem((JObject)objectValue, [SpecialName] (object sender, MouseButtonEventArgs e) =>
					{
						ModMain.FrmDownloadInstall.MinecraftSelected((MyListItem)sender, e);
					}, IsSaveOnly: false));
					break;
				case 8:
					Stack.Children.Add(((ResourceVersion)objectValue).ToListItem(ModMain.FrmDownloadResourceDetail.Save_Click, null, isBadDisplay));
					break;
				case 9:
				{
					UIElementCollection children2 = Stack.Children;
					ResourceVersion resourceVersion = (ResourceVersion)objectValue;
					obj2 = new _Closure___R31_3(obj2);
					obj2._VB_NonLocal_2 = ModMain.FrmDownloadResourceDetail;
					children2.Add(resourceVersion.ToListItem(obj2._Lambda___R6, ModMain.FrmDownloadResourceDetail.Save_Click, isBadDisplay));
					break;
				}
				case 10:
					Stack.Children.Add(ModDownloadLib.LiteLoaderDownloadListItem((ModDownload.DlLiteLoaderListEntry)objectValue, ModDownloadLib.LiteLoaderSave_Click, IsSaveOnly: true));
					break;
				case 11:
					Stack.Children.Add(((ModMain.HelpEntry)objectValue).ToListItem());
					break;
				case 12:
				{
					UIElementCollection children = Stack.Children;
					JObject entry = (JObject)objectValue;
					obj = new _Closure___R31_4(obj);
					obj._VB_NonLocal_3 = ModMain.FrmDownloadInstall;
					children.Add(ModDownloadLib.FabricDownloadListItem(entry, obj._Lambda___R7));
					break;
				}
				case 13:
					Stack.Children.Add(ModDownloadLib.NeoForgeDownloadListItem((ModDownload.DlNeoForgeListEntry)objectValue, ModDownloadLib.NeoForgeSave_Click, IsSaveOnly: true));
					break;
				default:
					Logger.Error($"未知的虚拟化种类：{Type}", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyCard.vb");
					break;
				}
			}
			Stack.Children.Add(new FrameworkElement
			{
				Height = 18.0
			});
			Stack.Tag = null;
		}
		private void MyCard_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (HasMouseAnimation)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (!Information.IsNothing(MainTextBlock))
				{
					list.Add(ModAnimation.AaColor(MainTextBlock, TextBlock.ForegroundProperty, "ColorBrush2", 90));
				}
				if (!Information.IsNothing(MainSwap))
				{
					list.Add(ModAnimation.AaColor(MainSwap, Shape.FillProperty, "ColorBrush2", 90));
				}
				list.AddRange(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(MainChrome, MyDropShadow.ColorProperty, "ColorObject4", 90),
					ModAnimation.AaOpacity(MainChrome, 0.4 - MainChrome.Opacity, 90)
				});
				ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(Uuid));
			}
		}
		private void MyCard_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (HasMouseAnimation)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (!Information.IsNothing(MainTextBlock))
				{
					list.Add(ModAnimation.AaColor(MainTextBlock, TextBlock.ForegroundProperty, "ColorBrush1", 90));
				}
				if (!Information.IsNothing(MainSwap))
				{
					list.Add(ModAnimation.AaColor(MainSwap, Shape.FillProperty, "ColorBrush1", 90));
				}
				list.AddRange(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(MainChrome, MyDropShadow.ColorProperty, "ColorObject1", 90),
					ModAnimation.AaOpacity(MainChrome, 0.07 - MainChrome.Opacity, 90)
				});
				ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(Uuid));
			}
		}
		private void MySizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (UseAnimation)
			{
				double num = (IsSwapped ? 40.0 : e.NewSize.Height) - e.PreviousSize.Height;
				if (e.PreviousSize.Height != 0.0 && !IsHeightAnimating && !(Math.Abs(num) < 1.0) && ActualHeight != 0.0)
				{
					StartHeightAnimation(num, e.PreviousSize.Height, IsLoadAnimation: false);
				}
			}
		}
		private void StartHeightAnimation(double Delta, double PreviousHeight, bool IsLoadAnimation)
		{
			if (IsHeightAnimating || ModMain.FrmMain == null)
			{
				return;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			double num = Math.Abs(Delta);
			checked
			{
				if (num <= 800.0)
				{
					list.Add(ModAnimation.AaHeight(this, Delta, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)));
				}
				else
				{
					int num2 = default;
					int num3;
					int num4;
					if (Delta < 0.0 && num - (double)num2 > 500.0)
					{
						num2 = 200;
						num3 = 150;
						num4 = (int)Math.Round((num - (double)num2) / 0.1);
					}
					else if (Delta > 0.0 && num - (double)num2 > 3000.0)
					{
						num4 = 5000;
						num2 = (int)Math.Round(num - (double)num4 * 0.3);
						num3 = 400;
					}
					else
					{
						num2 = 150;
						num3 = 200;
						num4 = 4000;
					}
					list.Add(ModAnimation.AaHeight(this, (num - (double)num2) * (double)Math.Sign(Delta), (int)Math.Round((num - (double)num2) / (double)num4 * 1000.0)));
					list.Add(ModAnimation.AaHeight(this, num2 * Math.Sign(Delta), num3, 0, new ModAnimation.AniEaseOutFluentWithInitial(num4, (double)num3 / 1000.0, num2), After: true));
				}
				list.Add(ModAnimation.AaCode([SpecialName] () =>
				{
					IsHeightAnimating = false;
					Height = ActualUsedHeight;
					if (IsSwapped && SwapControl != null)
					{
						SwapControl.Visibility = Visibility.Collapsed;
					}
				}, 0, After: true));
				ModAnimation.AniStart(list, "MyCard Height " + Conversions.ToString(Uuid));
				IsHeightAnimating = true;
				ActualUsedHeight = (IsSwapped ? 40.0 : Height);
				Height = PreviousHeight;
			}
		}
		public void TriggerForceResize()
		{
			Height = (IsSwapped ? 40.0 : double.NaN);
			ModAnimation.AniStop("MyCard Height " + Conversions.ToString(Uuid));
			IsHeightAnimating = false;
		}
		private void MyCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			double y = System.Windows.Input.Mouse.GetPosition(this).Y;
			if (IsSwapped || (!(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || IsMouseDirectlyOver)))
			{
				IsCustomMouseDown = true;
				if (IsSwapped || (SwapControl != null && !(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || IsMouseDirectlyOver)))
				{
					IsSwapMouseDown = true;
				}
			}
		}
		private void MyCard_MouseLeftButtonUp()
		{
			if (!IsCustomMouseDown)
			{
				return;
			}
			IsCustomMouseDown = false;
			ModMain.RaiseCustomEvent(this);
			if (!IsSwapMouseDown)
			{
				return;
			}
			IsSwapMouseDown = false;
			double y = System.Windows.Input.Mouse.GetPosition(this).Y;
			if (IsSwapped || (SwapControl != null && !(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || IsMouseDirectlyOver)))
			{
				ModBase.RouteEventArgs e = new ModBase.RouteEventArgs(RaiseByMouse: true);
				PreviewSwap?.Invoke(this, e);
				if (e.Handled)
				{
					IsSwapMouseDown = false;
					return;
				}
				IsSwapped = !IsSwapped;
				Logger.Info(string.Format("{0}{1}", IsSwapped ? "折叠卡片" : "展开卡片", (Title == null) ? "" : ("：" + Title)), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyCard.vb");
				Swap?.Invoke(this, e);
			}
		}
		private void MyCard_MouseLeave_Swap(object sender, System.Windows.Input.MouseEventArgs e)
		{
			IsSwapMouseDown = false;
		}
		private void UpdateCursorStyle()
		{
			if (_MainSwap == null)
			{
				Cursor = null;
				return;
			}
			double y = System.Windows.Input.Mouse.GetPosition(this).Y;
			Cursor = ((MainSwap.Visibility == Visibility.Visible && (IsSwapped || (y <= 34.0 && (y != 0.0 || IsMouseDirectlyOver)))) ? System.Windows.Input.Cursors.Hand : null);
		}
	}
}
