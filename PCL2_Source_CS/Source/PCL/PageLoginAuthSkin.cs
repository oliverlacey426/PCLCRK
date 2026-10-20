// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginAuthSkin : Grid
	{






		public PageLoginAuthSkin()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				PageLoginLegacy_Loaded();
			};
			InitializeComponent();
			InitConnectEvents();
			Skin.Loader = PageLaunchLeft.SkinAuth;
		}
		private void PageLoginLegacy_Loaded()
		{
			Skin.Loader.Start();
		}
		public void Reload(bool KeepInput)
		{
			TextName.Text = Settings.Get<string>("CacheAuthName");
			TextEmail.Text = Settings.Get<string>("CacheAuthUsername");
			TextEmail.Visibility = (Settings.Get<bool>("UiLauncherEmail") ? Visibility.Collapsed : Visibility.Visible);
			PageLoginLegacy_Loaded();
		}
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string baseUrl = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheAuthServerServer") : Settings.Get<string>("VersionServerAuthServer", ModMinecraft.McInstanceSelected)) + "/authserver";
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
			{
				Token = "Auth",
				BaseUrl = baseUrl,
				UserName = Settings.Get<string>("CacheAuthUsername"),
				Password = Settings.Get<string>("CacheAuthPass"),
				Description = "Authlib-Injector",
				Type = ModLaunch.McLoginType.Auth
			};
		}
		private void PageLoginAuthSkin_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[6]
			{
				ModAnimation.AaOpacity(BtnEdit, 1.0 - BtnEdit.Opacity, 80),
				ModAnimation.AaHeight(BtnEdit, 25.5 - BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(BtnExit, 1.0 - BtnExit.Opacity, 80),
				ModAnimation.AaHeight(BtnExit, 25.5 - BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent())
			}, "PageLoginAuthSkin Button");
		}
		private void PageLoginAuthSkin_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[4]
			{
				ModAnimation.AaOpacity(BtnEdit, 0.0 - BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnEdit, 14.0 - BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(BtnExit, 0.0 - BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaHeight(BtnExit, 14.0 - BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent())
			}, "PageLoginAuthSkin Button");
		}
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			if (ModLaunch.McLoginLoader.State == ModBase.LoadState.Loading)
			{
				Logger.Warn("要求更换角色，但登录加载器繁忙", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginAuthSkin.xaml.vb");
				if (ModLaunch.McLoginLoader.Input is ModLaunch.McLoginServer && ((ModLaunch.McLoginServer)ModLaunch.McLoginLoader.Input).ForceReselectProfile)
				{
					ModMain.Hint("正在尝试更换，请稍候！");
				}
				else
				{
					ModMain.Hint("正在登录中，请稍后再更换角色！", ModMain.HintType.Red);
				}
				return;
			}
			ModMain.Hint("正在尝试更换，请稍候！");
			Settings.Set("CacheAuthUuid", "");
			Settings.Set("CacheAuthName", "");
			ModBase.RunInThread([SpecialName] () =>
			{
				try
				{
					ModLaunch.McLoginServer loginData = GetLoginData();
					loginData.ForceReselectProfile = true;
					ModLaunch.McLoginLoader.WaitForExit(loginData, null, IsForceRestart: true);
					ModBase.RunInUi([SpecialName] () =>
					{
						Reload(KeepInput: true);
					});
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "更换角色失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginAuthSkin.xaml.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		public static void ExitLogin()
		{
			Settings.Set("CacheAuthAccess", "");
			Settings.Set("CacheAuthUuid", "");
			Settings.Set("CacheAuthName", "");
			ModLaunch.McLoginAuthLoader.Input = null;
			ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
		}
		private void Skin_Click(object sender, MouseButtonEventArgs e)
		{
			string text = (((object)ModMinecraft.McInstanceSelected != null) ? Settings.Get<string>("VersionServerAuthRegister", ModMinecraft.McInstanceSelected) : Settings.Get<string>("CacheAuthServerRegister"));
			if (string.IsNullOrEmpty(new ValidateHttp().Validate(text)))
			{
				ModBase.OpenWebsite(text.Replace("/auth/register", "/user/closet"));
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.MouseEventHandler value2 = PageLoginAuthSkin_MouseEnter;
						System.Windows.Input.MouseEventHandler value3 = PageLoginAuthSkin_MouseLeave;
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
						MyIconButton.ClickEventHandler obj = BtnEdit_Click;
						MyIconButton myIconButton = BtnEdit;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnEdit;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
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
