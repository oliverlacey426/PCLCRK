// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageOtherAbout : MyPageRight
	{
		private new bool IsLoaded;








		public PageOtherAbout()
		{
			Loaded += PageOtherAbout_Loaded;
			IsLoaded = false;
			InitializeComponent();
			InitConnectEvents();
		}
		private void PageOtherAbout_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToHome();
			if (!IsLoaded)
			{
				IsLoaded = true;
				ItemAboutPcl.Info = ItemAboutPcl.Info.Replace("%VERSION%", "快照版 2.13.1.1").Replace("%VERSIONCODE%", Conversions.ToString(410)).Replace("%BRANCH%", Conversions.ToString(0));
			}
		}
		public static void CopyIdentify()
		{
			ModBase.ClipboardSet(ModBase.Identify);
		}
		private void BtnDonateCodeInput_Click()
		{
			ModSecret.InputPotatoCode(IsUpdating: false);
		}
		private void BtnDonateOutput_Click(object sender, EventArgs e)
		{
			ModSecret.GeneratePotatoCode();
		}
		private void BtnAboutUpdate_Click(object sender, MouseButtonEventArgs e)
		{
			ModSecret.UpdateCheckByButton();
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyButton.ClickEventHandler obj = BtnAboutUpdate_Click;
						MyButton myButton = BtnAboutUpdate;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnAboutUpdate;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnDonateOutput_Click;
						MyButton myButton = BtnDonateOutput;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDonateOutput;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							CopyIdentify();
						};
						MyButton myButton = BtnDonateCopy;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDonateCopy;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnDonateCodeInput_Click();
						};
						MyButton myButton = BtnDonateInput;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDonateInput;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
