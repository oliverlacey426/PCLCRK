// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageInstanceModDisabled : MyPageRight
	{



		public PageInstanceModDisabled()
		{
			InitializeComponent();
			InitConnectEvents();
		}
		private void BtnDownload_Click(object sender, EventArgs e)
		{
			ModMain.FrmMain.PageChange(FormMain.PageType.Download);
		}
		private void BtnVersion_Click(object sender, EventArgs e)
		{
			ModMain.FrmMain.PageChange(FormMain.PageType.Launch);
			ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
		}
		public void BtnDownload_Loaded()
		{
			Visibility visibility = (((Settings.Get<bool>("UiHiddenPageDownload") && !PageSetupUI.HiddenForceShow) || (ModMain.FrmSelectRight != null && ModMain.FrmSelectRight.ShowHidden)) ? Visibility.Collapsed : Visibility.Visible);
			if (BtnDownload.Visibility != visibility)
			{
				BtnDownload.Visibility = visibility;
				PanMain.TriggerForceResize();
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyButton.ClickEventHandler obj = BtnDownload_Click;
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							BtnDownload_Loaded();
						};
						MyButton myButton = BtnDownload;
						if (myButton != null)
						{
							myButton.Click -= obj;
							myButton.Loaded -= value2;
						}
						myButton = BtnDownload;
						if (myButton != null)
						{
							myButton.Click += obj;
							myButton.Loaded += value2;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnVersion_Click;
						MyButton myButton = BtnVersion;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnVersion;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
