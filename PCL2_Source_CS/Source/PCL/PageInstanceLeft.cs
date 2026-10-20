// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageInstanceLeft : MyPageLeft, IRefreshable
	{
		public static ModMinecraft.McInstance Instance = null;
		public FormMain.PageSubType PageID;
		public static RedoableWorker<Java> CurrentJavaWorker = new RedoableWorker<Java>([SpecialName] (CancellationToken? c, ProgressProvider p) => ModJava.SelectOrDownloadJava(Instance, TryFixOrHint: false, c.Value, p), "CurrentJavaWorker");






		public PageInstanceLeft()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshModDisabled();
			};
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ReloadCurrentJava();
			};
			PageID = FormMain.PageSubType.Default;
			InitializeComponent();
			InitConnectEvents();
		}
		public void RefreshModDisabled()
		{
			if ((object)Instance != null && Instance.Modable)
			{
				ItemMod.Visibility = Visibility.Visible;
				ItemModDisabled.Visibility = Visibility.Collapsed;
			}
			else
			{
				ItemMod.Visibility = Visibility.Collapsed;
				ItemModDisabled.Visibility = Visibility.Visible;
			}
		}
		private void PageCheck(FrameworkElement sender, ModBase.RouteEventArgs e)
		{
			if (sender.Tag != null)
			{
				PageChange((FormMain.PageSubType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}
		public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
		{
			if (ID == (FormMain.PageSubType)(-1))
			{
				ID = PageID;
			}
			switch (ID)
			{
			case FormMain.PageSubType.Default:
				if (ModMain.FrmInstanceOverall == null)
				{
					ModMain.FrmInstanceOverall = new PageInstanceOverall();
				}
				return ModMain.FrmInstanceOverall;
			case FormMain.PageSubType.DownloadMod:
				if (ModMain.FrmInstanceMod == null)
				{
					ModMain.FrmInstanceMod = new PageInstanceMod();
				}
				return ModMain.FrmInstanceMod;
			case FormMain.PageSubType.DownloadPack:
				if (ModMain.FrmInstanceModDisabled == null)
				{
					ModMain.FrmInstanceModDisabled = new PageInstanceModDisabled();
				}
				return ModMain.FrmInstanceModDisabled;
			case FormMain.PageSubType.SetupLink:
				if (Information.IsNothing(ModMain.FrmInstanceSetup))
				{
					ModMain.FrmInstanceSetup = new PageInstanceSetup();
				}
				return ModMain.FrmInstanceSetup;
			case FormMain.PageSubType.DownloadDataPack:
				if (ModMain.FrmInstanceExport == null)
				{
					ModMain.FrmInstanceExport = new PageInstanceExport();
				}
				return ModMain.FrmInstanceExport;
			default:
				throw new Exception("未知的版本设置子页面种类：" + Conversions.ToString((int)ID));
			}
		}
		public void PageChange(FormMain.PageSubType ID)
		{
			if (PageID == ID)
			{
				return;
			}
			checked
			{
				ModAnimation.AniControlEnabled++;
				try
				{
					PageChangeRun((MyPageRight)PageGet(ID));
					PageID = ID;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"切换分页面失败（ID {ID}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceLeft.xaml.vb");
					ProjectData.ClearProjectError();
				}
				finally
				{
					ModAnimation.AniControlEnabled--;
				}
			}
		}
		private static void PageChangeRun(MyPageRight Target)
		{
			ModAnimation.AniStop("FrmMain PageChangeRight");
			if (Target.Parent != null)
			{
				Target.SetValue(ContentPresenter.ContentProperty, null);
			}
			ModMain.FrmMain.PageRight = Target;
			((MyPageRight)ModMain.FrmMain.PanMainRight.Child).PageOnExit();
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaCode([SpecialName] () =>
				{
					((MyPageRight)ModMain.FrmMain.PanMainRight.Child).PageOnForceExit();
					ModMain.FrmMain.PanMainRight.Child = ModMain.FrmMain.PageRight;
					ModMain.FrmMain.PageRight.Opacity = 0.0;
				}, 130),
				ModAnimation.AaCode([SpecialName] () =>
				{
					ModMain.FrmMain.PageRight.Opacity = 1.0;
					ModMain.FrmMain.PageRight.PageOnEnter();
				}, 30, After: true)
			}, "PageLeft PageChange");
		}
		public void Refresh_Click(object sender, EventArgs e)
		{
			Refresh((FormMain.PageSubType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null))))));
		}
		public void Refresh()
		{
			Refresh(ModMain.FrmMain.PageCurrentSub);
		}
		void IRefreshable.Refresh()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Refresh
			this.Refresh();
		}
		public void Refresh(FormMain.PageSubType SubType)
		{
			switch (SubType)
			{
			case FormMain.PageSubType.DownloadMod:
				PageInstanceMod.Refresh();
				ItemMod.Checked = true;
				break;
			case FormMain.PageSubType.DownloadDataPack:
				if (ModMain.FrmInstanceExport != null)
				{
					ModMain.FrmInstanceExport.RefreshAll();
				}
				ItemExport.Checked = true;
				break;
			}
		}
		public void Reset(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("是否要初始化该版本的版本独立设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
			{
				if (Information.IsNothing(ModMain.FrmInstanceSetup))
				{
					ModMain.FrmInstanceSetup = new PageInstanceSetup();
				}
				ModMain.FrmInstanceSetup.Reset();
				ItemSetup.Checked = true;
			}
		}
		public static void ReloadCurrentJava()
		{
			if (ModAnimation.AniControlEnabled == 0 && (object)Instance != null)
			{
				CurrentJavaWorker.Start();
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((FrameworkElement)a0, a1);
						};
						MyListItem myListItem = ItemOverall;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemOverall;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((FrameworkElement)a0, a1);
						};
						MyListItem myListItem = ItemSetup;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemSetup;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((FrameworkElement)a0, a1);
						};
						MyListItem myListItem = ItemMod;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemMod;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((FrameworkElement)a0, a1);
						};
						MyListItem myListItem = ItemModDisabled;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemModDisabled;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((FrameworkElement)a0, a1);
						};
						MyListItem myListItem = ItemExport;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemExport;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
		}

	}
}
