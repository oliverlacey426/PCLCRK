// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginNideSkin : Grid
	{






		public PageLoginNideSkin()
		{
			Loaded += PageLoginLegacy_Loaded;
			InitializeComponent();
			InitConnectEvents();
			Skin.Loader = PageLaunchLeft.SkinNide;
		}
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			Skin.Loader.Start();
		}
		public void Reload(bool KeepInput)
		{
			TextName.Text = Settings.Get<string>("CacheNideName");
			TextEmail.Text = Settings.Get<string>("CacheNideUsername");
			TextEmail.Visibility = ModBase.ToVisibility(!Settings.Get<bool>("UiLauncherEmail"));
		}
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string text = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected));
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
			{
				Token = "Nide",
				UserName = Settings.Get<string>("CacheNideUsername"),
				Password = Settings.Get<string>("CacheNidePass"),
				Description = "统一通行证",
				Type = ModLaunch.McLoginType.Nide,
				BaseUrl = "https://auth.mc-user.com:233/" + text + "/authserver"
			};
		}
		private void PageLoginNideSkin_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[6]
			{
				ModAnimation.AaOpacity(BtnEdit, 1.0 - BtnEdit.Opacity, 80),
				ModAnimation.AaHeight(BtnEdit, 25.5 - BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(BtnExit, 1.0 - BtnExit.Opacity, 80),
				ModAnimation.AaHeight(BtnExit, 25.5 - BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent())
			}, "PageLoginNideSkin Button");
		}
		private void PageLoginNideSkin_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[4]
			{
				ModAnimation.AaOpacity(BtnEdit, 0.0 - BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnEdit, 14.0 - BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(BtnExit, 0.0 - BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnExit, 14.0 - BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent())
			}, "PageLoginNideSkin Button");
		}
		public static void ExitLogin()
		{
			Settings.Set("CacheNideAccess", "");
			ModLaunch.McLoginNideLoader.Input = null;
			ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
		}
		private void Skin_Click(object sender, MouseButtonEventArgs e)
		{
			ModBase.OpenWebsite("https://login.mc-user.com:233/" + (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected)) + "/skin");
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.MouseEventHandler value2 = PageLoginNideSkin_MouseEnter;
						System.Windows.Input.MouseEventHandler value3 = PageLoginNideSkin_MouseLeave;
						Grid grid = PanData;
						if (grid != null)
						{
							grid.MouseEnter -= value2;
							grid.MouseLeave -= value3;
						}
						grid = PanData;
						if (grid != null)
						{
							grid.MouseEnter += value2;
							grid.MouseLeave += value3;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							ExitLogin();
						};
						MyIconButton myIconButton = BtnExit;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnExit;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MySkin.ClickEventHandler obj = Skin_Click;
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
		}

	}
}
