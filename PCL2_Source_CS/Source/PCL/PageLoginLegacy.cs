// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginLegacy : Grid
	{
		public bool IsReloaded;




		public PageLoginLegacy()
		{
			Loaded += PageLoginLegacy_Loaded;
			IsReloaded = false;
			InitializeComponent();
			InitConnectEvents();
			Skin.Loader = PageLaunchLeft.SkinLegacy;
		}
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			Skin.Loader.Start();
		}
		public void Reload(bool KeepInput)
		{
			if (KeepInput && IsReloaded)
			{
				string text = ComboName.Text.Trim();
				ComboName.ItemsSource = ((Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) == 0) ? null : Settings.Get<string>("LoginLegacyName").Split("\u00a8"));
				ComboName.Text = text;
			}
			else if (Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) == 0)
			{
				ComboName.ItemsSource = null;
			}
			else
			{
				ComboName.ItemsSource = Settings.Get<string>("LoginLegacyName").ToString().Split("\u00a8");
				ComboName.Text = Settings.Get<string>("LoginLegacyName").ToString().BeforeFirst("\u00a8")
					.Trim();
			}
			IsReloaded = true;
		}
		public static ModLaunch.McLoginData GetLoginData()
		{
			string userName = ((ModMain.FrmLoginLegacy == null) ? "" : ModMain.FrmLoginLegacy.ComboName.Text.Replace("\u00a8", "").Trim());
			return new ModLaunch.McLoginLegacy
			{
				UserName = userName,
				SkinType = Settings.Get<int>("LaunchSkinType"),
				SkinName = Settings.Get<string>("LaunchSkinID")
			};
		}
		public static string IsVaild(ModLaunch.McLoginLegacy LoginData)
		{
			if (Operators.CompareString(LoginData.UserName.Trim(), "", TextCompare: false) == 0)
			{
				return "玩家名不能为空！";
			}
			if (LoginData.UserName.Contains("\""))
			{
				return "玩家名不能包含英文引号！";
			}
			if ((object)ModMinecraft.McInstanceSelected != null && ModMinecraft.McInstanceSelected.Version.Vaild && ModMinecraft.McInstanceSelected.Version.Vanilla >= new Version(20, 0, 3) && LoginData.UserName.Trim().Length > 16)
			{
				return "自 1.20.3 起，玩家名至多只能包含 16 个字符！";
			}
			return "";
		}
		public string IsVaild()
		{
			return IsVaild((ModLaunch.McLoginLegacy)GetLoginData());
		}
		private void ComboName_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				int caretIndex = ((MyTextBox)ComboName.Template.FindName("PART_EditableTextBox", ComboName)).CaretIndex;
				if (caretIndex == ComboName.Text.Length || caretIndex == 0)
				{
					e.Handled = true;
				}
			}
		}
		private void ComboLegacy_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Settings.Get<int>("LaunchSkinType") == 0)
			{
				PageLaunchLeft.SkinLegacy.Start(null, IsForceRestart: true);
			}
			HintChinese.Visibility = (ComboName.Text.RegexCheck("^[0-9A-Za-z_]*$") ? Visibility.Collapsed : Visibility.Visible);
			ModBase.RunInUi([SpecialName] () =>
			{
				PanMain.InvalidateMeasure();
			}, ForceWaitUntilLoaded: true);
		}
		private void Skin_Click()
		{
			if ((Settings.Get<bool>("UiHiddenPageSetup") || Settings.Get<bool>("UiHiddenSetupLaunch")) && !PageSetupUI.HiddenForceShow)
			{
				ModMain.Hint("启动设置已被禁用！", ModMain.HintType.Red);
			}
			else
			{
				ModMain.FrmMain.PageChange(FormMain.PageType.Setup);
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MySkin.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Skin_Click();
						};
						MySkin mySkin = Skin;
						if (mySkin != null)
						{
							mySkin.Click -= obj;
						}
						mySkin = Skin;
						if (mySkin != null)
						{
							mySkin.Click += obj;
						}
			
			}
			{
						System.Windows.Input.KeyEventHandler value2 = ComboName_PreviewKeyDown;
						MyComboBox.TextChangedEventHandler obj = ComboLegacy_TextChanged;
						MyComboBox myComboBox = ComboName;
						if (myComboBox != null)
						{
							myComboBox.PreviewKeyDown -= value2;
							myComboBox.TextChanged -= obj;
						}
						myComboBox = ComboName;
						if (myComboBox != null)
						{
							myComboBox.PreviewKeyDown += value2;
							myComboBox.TextChanged += obj;
						}
			
			}
		}

	}
}
