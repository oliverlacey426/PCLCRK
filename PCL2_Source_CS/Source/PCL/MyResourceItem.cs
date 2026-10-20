// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class MyResourceItem : Grid
	{
		public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);
		public int Uuid;
		private bool IsMouseDown;
		private Border _RectBack;
		private string StateLast;
		public string Title
		{
			get
			{
				return LabTitle.Text;
			}
			set
			{
				if (Operators.CompareString(LabTitle.Text, value, TextCompare: false) != 0)
				{
					LabTitle.Text = value;
				}
			}
		}
		public string SubTitle
		{
			get
			{
				return LabTitleRaw?.Text ?? "";
			}
			set
			{
				if (Operators.CompareString(LabTitleRaw.Text, value, TextCompare: false) != 0)
				{
					LabTitleRaw.Text = value;
					LabTitleRaw.Visibility = ((Operators.CompareString(value, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				}
			}
		}
		public string Description
		{
			get
			{
				return LabInfo.Text;
			}
			set
			{
				if (Operators.CompareString(LabInfo.Text, value, TextCompare: false) != 0)
				{
					LabInfo.Text = value;
				}
			}
		}
		public List<string> Tags
		{
			set
			{
				PanTags.Children.Clear();
				PanTags.Visibility = ((!value.Any()) ? Visibility.Collapsed : Visibility.Visible);
				foreach (string item in value)
				{
					Border border = new Border
					{
						Background = new ModBase.MyColor("#11000000"),
						Padding = new Thickness(3.0, 1.0, 3.0, 1.0),
						CornerRadius = new CornerRadius(3.0),
						Margin = new Thickness(0.0, 0.0, 3.0, 0.0),
						SnapsToDevicePixels = true,
						UseLayoutRounding = false
					};
					TextBlock child = new TextBlock
					{
						Text = item,
						Foreground = new ModBase.MyColor("#868686"),
						FontSize = 11.0
					};
					border.Child = child;
					PanTags.Children.Add(border);
				}
			}
		}
		public Border RectBack
		{
			get
			{
				if (_RectBack == null)
				{
					Border border = new Border
					{
						Name = "RectBack",
						CornerRadius = new CornerRadius(3.0),
						RenderTransform = new ScaleTransform(0.8, 0.8),
						RenderTransformOrigin = new System.Windows.Point(0.5, 0.5),
						BorderThickness = new Thickness(ModBase.GetWPFSize(1.0)),
						SnapsToDevicePixels = true,
						IsHitTestVisible = false,
						Opacity = 0.0
					};
					border.SetResourceReference(Border.BackgroundProperty, "ColorBrush7");
					border.SetResourceReference(Border.BorderBrushProperty, "ColorBrush6");
					Grid.SetColumnSpan(border, 999);
					Grid.SetRowSpan(border, 999);
					Children.Insert(0, border);
					_RectBack = border;
				}
				return _RectBack;
			}
		}
		public bool CanInteraction { get; set; }





















		public event ClickEventHandler Click;
		public MyResourceItem()
		{
			PreviewMouseLeftButtonUp += Button_MouseUp;
			Click += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				MyResourceItem_Click((MyResourceItem)a0, a1);
			};
			PreviewMouseLeftButtonDown += Button_MouseDown;
			MouseLeave += Button_MouseLeave;
			PreviewMouseLeftButtonUp += Button_MouseLeave;
			MouseEnter += RefreshColor;
			MouseLeave += RefreshColor;
			MouseLeftButtonDown += RefreshColor;
			MouseLeftButtonUp += RefreshColor;
			Uuid = ModBase.GetUuid();
			IsMouseDown = false;
			_RectBack = null;
			CanInteraction = true;
			InitializeComponent();
			InitConnectEvents();
		}
		private void LabInfo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ToolTipInfo.Content = LabInfo.Text;
			LabInfo.ToolTip = (ModBase.IsTextTrimmed(LabInfo) ? ToolTipInfo : null);
		}
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseDown)
			{
				Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
				if (!e.Handled)
				{
					Logger.Info($"按下资源工程列表项：{LabTitle.Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\MyResourceItem.xaml.vb");
				}
			}
		}
		private void MyResourceItem_Click(MyResourceItem sender, EventArgs e)
		{
			List<string> list = new List<string>();
			if (ModMain.FrmMain.PageCurrent.Page == FormMain.PageType.ResourceDetail)
			{
				foreach (MyCard child in ModMain.FrmDownloadResourceDetail.PanResults.Children)
				{
					if (Operators.CompareString(child.Title, "", TextCompare: false) != 0 && !child.IsSwapped)
					{
						list.Add(child.Title);
					}
				}
				Logger.Info(string.Format("记录当前已展开的卡片：{0}", string.Join("、", list)), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\MyResourceItem.xaml.vb");
			}
			string text = null;
			ModLoaders modLoaders = ModLoaders.None;
			ResourceTypes resourceTypes = default;
			if (ModMain.FrmMain.PageCurrent.Page == FormMain.PageType.Download)
			{
				switch (ModMain.FrmMain.PageCurrentSub)
				{
				case FormMain.PageSubType.DownloadMod:
					resourceTypes = ResourceTypes.Mod;
					text = ModMain.FrmDownloadMod.Content.Loader.Input.GameVersion;
					modLoaders = ModMain.FrmDownloadMod.Content.Loader.Input.ModLoaders;
					break;
				case FormMain.PageSubType.DownloadPack:
					resourceTypes = ResourceTypes.ModPack;
					text = ModMain.FrmDownloadPack.Content.Loader.Input.GameVersion;
					break;
				case FormMain.PageSubType.DownloadDataPack:
					resourceTypes = ResourceTypes.DataPack;
					text = ModMain.FrmDownloadDataPack.Content.Loader.Input.GameVersion;
					break;
				case FormMain.PageSubType.DownloadResourcePack:
					resourceTypes = ResourceTypes.ResourcePack;
					text = ModMain.FrmDownloadResourcePack.Content.Loader.Input.GameVersion;
					break;
				case FormMain.PageSubType.DownloadShader:
					resourceTypes = ResourceTypes.Shader;
					text = ModMain.FrmDownloadShader.Content.Loader.Input.GameVersion;
					break;
				}
			}
			else
			{
				resourceTypes = ResourceTypes.Any;
				text = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 2 }, null));
				modLoaders = (ModLoaders)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 3 }, null));
			}
			ModMain.FrmMain.PageChange(new FormMain.PageStackData
			{
				Page = FormMain.PageType.ResourceDetail,
				Additional = new object[5] { sender.Tag, list, text, modLoaders, resourceTypes }
			});
		}
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseOver && CanInteraction)
			{
				IsMouseDown = true;
			}
		}
		private void Button_MouseLeave(object sender, object e)
		{
			IsMouseDown = false;
		}
		public void RefreshColor(object sender, EventArgs e)
		{
			if (!CanInteraction)
			{
				return;
			}
			string text;
			int num;
			if (IsMouseOver)
			{
				if (IsMouseDown)
				{
					text = "MouseDown";
					num = 120;
				}
				else
				{
					text = "MouseOver";
					num = 120;
				}
			}
			else
			{
				text = "Idle";
				num = 180;
			}
			if (Operators.CompareString(StateLast, text, TextCompare: false) == 0)
			{
				return;
			}
			StateLast = text;
			checked
			{
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
					if (IsMouseOver)
					{
						list.AddRange(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrushBg1", num),
							ModAnimation.AaOpacity(RectBack, 1.0 - RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent())
						});
						if (IsMouseDown)
						{
							list.Add(ModAnimation.AaScaleTransform(RectBack, 0.996 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.2), 0, new ModAnimation.AniEaseOutFluent()));
						}
						else
						{
							list.Add(ModAnimation.AaScaleTransform(RectBack, 1.0 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.2), 0, new ModAnimation.AniEaseOutFluent()));
						}
					}
					else
					{
						list.AddRange(new ModAnimation.AniData[4]
						{
							ModAnimation.AaOpacity(RectBack, 0.0 - RectBack.Opacity, num),
							ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrush7", num),
							ModAnimation.AaScaleTransform(RectBack, 0.996 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent()),
							ModAnimation.AaScaleTransform(RectBack, -0.196, 1, 0, null, After: true)
						});
					}
					ModAnimation.AniStart(list, "MyResourceItem Color " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStop("MyResourceItem Color " + Conversions.ToString(Uuid));
					if (_RectBack != null)
					{
						RectBack.Opacity = 0.0;
					}
				}
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.MouseEventHandler value2 = LabInfo_MouseEnter;
						TextBlock textBlock = LabInfo;
						if (textBlock != null)
						{
							textBlock.MouseEnter -= value2;
						}
						textBlock = LabInfo;
						if (textBlock != null)
						{
							textBlock.MouseEnter += value2;
						}
			
			}
		}

	}
}
