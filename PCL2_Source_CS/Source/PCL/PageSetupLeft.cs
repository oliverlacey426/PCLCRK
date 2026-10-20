// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageSetupLeft : MyPageLeft
	{
		private bool IsLoad;
		private bool IsPageSwitched;
		public FormMain.PageSubType PageID;





		private void PageSetupLeft_Loaded(object sender, RoutedEventArgs e)
		{
			bool flag = false;
			if (ItemLaunch.Checked && Settings.Get<bool>("UiHiddenSetupLaunch"))
			{
				flag = true;
			}
			if (ItemUI.Checked && Settings.Get<bool>("UiHiddenSetupUi"))
			{
				flag = true;
			}
			if (ItemSystem.Checked && Settings.Get<bool>("UiHiddenSetupSystem"))
			{
				flag = true;
			}
			if (ItemLink.Checked && Settings.Get<bool>("UiHiddenSetupLink"))
			{
				flag = true;
			}
			if (PageSetupUI.HiddenForceShow)
			{
				flag = false;
			}
			if (IsLoad && !flag)
			{
				return;
			}
			IsLoad = true;
			PageSetupUI.HiddenRefresh();
			if (!IsPageSwitched)
			{
				if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
				{
					ItemLaunch.SetChecked(value: true, user: false, anime: false);
				}
				else if (!Settings.Get<bool>("UiHiddenSetupUi"))
				{
					ItemUI.SetChecked(value: true, user: false, anime: false);
				}
				else if (!Settings.Get<bool>("UiHiddenSetupSystem"))
				{
					ItemSystem.SetChecked(value: true, user: false, anime: false);
				}
				else if (!Settings.Get<bool>("UiHiddenSetupLink"))
				{
					ItemLink.SetChecked(value: true, user: false, anime: false);
				}
				else
				{
					ItemLaunch.SetChecked(value: true, user: false, anime: false);
				}
			}
		}
		private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
		{
			IsPageSwitched = false;
		}
		public PageSetupLeft()
		{
			Loaded += PageSetupLeft_Loaded;
			Unloaded += PageOtherLeft_Unloaded;
			IsLoad = false;
			IsPageSwitched = false;
			InitializeComponent();
			InitConnectEvents();
			if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
			{
				PageID = FormMain.PageSubType.Default;
			}
			else if (!Settings.Get<bool>("UiHiddenSetupUi"))
			{
				PageID = FormMain.PageSubType.DownloadMod;
			}
			else if (!Settings.Get<bool>("UiHiddenSetupSystem"))
			{
				PageID = FormMain.PageSubType.DownloadPack;
			}
			else if (!Settings.Get<bool>("UiHiddenSetupLink"))
			{
				PageID = FormMain.PageSubType.SetupLink;
			}
			else
			{
				PageID = FormMain.PageSubType.Default;
			}
		}
		private void PageCheck(MyListItem sender, EventArgs e)
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
				if (ModMain.FrmSetupLaunch == null)
				{
					ModMain.FrmSetupLaunch = new PageSetupLaunch();
				}
				return ModMain.FrmSetupLaunch;
			case FormMain.PageSubType.DownloadMod:
				if (ModMain.FrmSetupUI == null)
				{
					ModMain.FrmSetupUI = new PageSetupUI();
				}
				return ModMain.FrmSetupUI;
			case FormMain.PageSubType.SetupLink:
				if (ModMain.FrmSetupLink == null)
				{
					ModMain.FrmSetupLink = new PageSetupLink();
				}
				return ModMain.FrmSetupLink;
			case FormMain.PageSubType.DownloadPack:
				if (ModMain.FrmSetupSystem == null)
				{
					ModMain.FrmSetupSystem = new PageSetupSystem();
				}
				return ModMain.FrmSetupSystem;
			default:
				throw new Exception("未知的设置子页面种类：" + Conversions.ToString((int)ID));
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
				IsPageSwitched = true;
				try
				{
					switch (ID)
					{
					case FormMain.PageSubType.Default:
						if (Information.IsNothing(ModMain.FrmSetupLaunch))
						{
							ModMain.FrmSetupLaunch = new PageSetupLaunch();
						}
						PageChangeRun(ModMain.FrmSetupLaunch);
						break;
					case FormMain.PageSubType.DownloadMod:
						if (Information.IsNothing(ModMain.FrmSetupUI))
						{
							ModMain.FrmSetupUI = new PageSetupUI();
						}
						PageChangeRun(ModMain.FrmSetupUI);
						break;
					case FormMain.PageSubType.SetupLink:
						if (Information.IsNothing(ModMain.FrmSetupLink))
						{
							ModMain.FrmSetupLink = new PageSetupLink();
						}
						PageChangeRun(ModMain.FrmSetupLink);
						break;
					case FormMain.PageSubType.DownloadPack:
						if (Information.IsNothing(ModMain.FrmSetupSystem))
						{
							ModMain.FrmSetupSystem = new PageSetupSystem();
						}
						PageChangeRun(ModMain.FrmSetupSystem);
						break;
					default:
						throw new Exception("未知的设置子页面种类：" + Conversions.ToString(unchecked((int)ID)));
					}
					PageID = ID;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"切换分页面失败（ID {ID}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLeft.xaml.vb");
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
		public void Reset(object sender, EventArgs e)
		{
			double num = ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
			if (num == 0.0)
			{
				if (ModMain.MyMsgBox("是否要初始化 启动 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
				{
					if (Information.IsNothing(ModMain.FrmSetupLaunch))
					{
						ModMain.FrmSetupLaunch = new PageSetupLaunch();
					}
					ModMain.FrmSetupLaunch.Reset();
					ItemLaunch.Checked = true;
				}
			}
			else if (num == 2.0)
			{
				if (ModMain.MyMsgBox("是否要初始化 个性化 页面的所有设置？该操作不可撤销。\r\n（背景图片与音乐、主页等外部文件不会被删除）", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
				{
					if (Information.IsNothing(ModMain.FrmSetupUI))
					{
						ModMain.FrmSetupUI = new PageSetupUI();
					}
					ModMain.FrmSetupUI.Reset();
					ItemUI.Checked = true;
				}
			}
			else if (num == 3.0)
			{
				if (ModMain.MyMsgBox("是否要初始化 其他 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
				{
					if (Information.IsNothing(ModMain.FrmSetupSystem))
					{
						ModMain.FrmSetupSystem = new PageSetupSystem();
					}
					ModMain.FrmSetupSystem.Reset();
					ItemSystem.Checked = true;
				}
			}
			else if (num == 1.0 && ModMain.MyMsgBox("是否要初始化 联机 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
			{
				if (Information.IsNothing(ModMain.FrmSetupLink))
				{
					ModMain.FrmSetupLink = new PageSetupLink();
				}
				ModMain.FrmSetupLink.Reset();
				ItemLink.Checked = true;
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((MyListItem)a0, a1);
						};
						MyListItem myListItem = ItemLaunch;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemLaunch;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((MyListItem)a0, a1);
						};
						MyListItem myListItem = ItemLink;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemLink;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((MyListItem)a0, a1);
						};
						MyListItem myListItem = ItemUI;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemUI;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							PageCheck((MyListItem)a0, a1);
						};
						MyListItem myListItem = ItemSystem;
						if (myListItem != null)
						{
							myListItem.Check -= obj;
						}
						myListItem = ItemSystem;
						if (myListItem != null)
						{
							myListItem.Check += obj;
						}
			
			}
		}

	}
}
