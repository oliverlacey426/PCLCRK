// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLaunchLeft : MyPageLeft
	{
		private enum PageType
		{
			None,
			Legacy,
			Nide,
			NideSkin,
			Auth,
			AuthSkin,
			Ms,
			MsSkin
		}
		[CompilerGenerated]
		internal sealed class _Closure___33_0
		{
			public bool _VB_Local_HasLaunchDownloader;
			public PageLaunchLeft _VB_Me;
			public _Closure___33_0(_Closure___33_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_HasLaunchDownloader = arg0._VB_Local_HasLaunchDownloader;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				if (!_VB_Local_HasLaunchDownloader)
				{
					_VB_Me.LabLaunchingDownload.Visibility = Visibility.Collapsed;
					_VB_Me.LabLaunchingDownloadLeft.Visibility = Visibility.Collapsed;
				}
			}
		}
		private bool IsLoad;
		private bool IsLoadFinished;
		private PageType PageCurrent;
		public static ModLoader.LoaderTask<(string, string), string> SkinMs = new ModLoader.LoaderTask<(string, string), string>("Loader Skin Ms", SkinMsLoad, SkinMsInput, ThreadPriority.AboveNormal);
		public static ModLoader.LoaderTask<(string, string), string> SkinLegacy = new ModLoader.LoaderTask<(string, string), string>("Loader Skin Legacy", SkinLegacyLoad, SkinLegacyInput, ThreadPriority.AboveNormal);
		public static ModLoader.LoaderTask<(string, string), string> SkinNide = new ModLoader.LoaderTask<(string, string), string>("Loader Skin Nide", SkinNideLoad, SkinNideInput, ThreadPriority.AboveNormal);
		public static ModLoader.LoaderTask<(string, string), string> SkinAuth = new ModLoader.LoaderTask<(string, string), string>("Loader Skin Auth", SkinAuthLoad, SkinAuthInput, ThreadPriority.AboveNormal);
		public static List<ModLoader.LoaderTask<(string, string), string>> SkinLoaders = new List<ModLoader.LoaderTask<(string, string), string>> { SkinMs, SkinLegacy, SkinNide, SkinAuth };
		private int BtnLaunchState;
		private ModMinecraft.McInstance BtnLaunchInstance;
		private double ShowProgress;
		private bool IsWidthAnimating;
		private double ActualUsedWidth;
		private bool IsHeightAnimating;
		private double ActualUsedHeight;































		public PageLaunchLeft()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				PageLaunchLeft_Loaded();
			};
			IsLoad = false;
			IsLoadFinished = false;
			PageCurrent = PageType.None;
			BtnLaunchState = 0;
			BtnLaunchInstance = null;
			ShowProgress = 0.0;
			IsWidthAnimating = false;
			IsHeightAnimating = false;
			InitializeComponent();
			InitConnectEvents();
		}
		public void PageLaunchLeft_Loaded()
		{
			if (IsLoad)
			{
				RefreshPage(KeepInput: true, Anim: false);
			}
			AprilPosTrans.X = 0.0;
			AprilPosTrans.Y = 0.0;
			if (IsLoad)
			{
				return;
			}
			IsLoad = true;
			checked
			{
				ModAnimation.AniControlEnabled++;
				ModMinecraft.McInstanceListLoader.LoadingStateChanged += [SpecialName] (MyLoading.MyLoadingState a0, MyLoading.MyLoadingState a1) =>
				{
					RefreshButtonsUI();
				};
				ModMinecraft.McFolderListLoader.LoadingStateChanged += [SpecialName] (MyLoading.MyLoadingState a0, MyLoading.MyLoadingState a1) =>
				{
					RefreshButtonsUI();
				};
				RefreshButtonsUI();
				ModBase.RunInNewThread([SpecialName] () =>
				{
					string text = null;
					if (FileUtils.Exists(Paths.Base + "modpack.zip"))
					{
						text = Paths.Base + "modpack.zip";
					}
					if (FileUtils.Exists(Paths.Base + "modpack.mrpack"))
					{
						text = Paths.Base + "modpack.mrpack";
					}
					if (text != null)
					{
						Logger.Warn($"需自动安装整合包：{text}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						PageSelectLeft.CreateMcFolderInCurrentPath();
						ModMinecraft.McFolderListLoader.WaitForExit();
					}
					if (Operators.CompareString(ModMinecraft.McFolderSelected, "", TextCompare: false) == 0 || !DirectoryUtils.Exists(ModMinecraft.McFolderSelected))
					{
						if (Operators.CompareString(ModMinecraft.McFolderSelected, "", TextCompare: false) == 0)
						{
							Logger.Info("没有已储存的 Minecraft 文件夹", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						}
						else
						{
							Logger.Warn($"Minecraft 文件夹无效，该文件夹已不存在：{ModMinecraft.McFolderSelected}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						}
						ModMinecraft.McFolderListLoader.WaitForExit(null, null, IsForceRestart: true);
						ModMinecraft.McFolderSelected = ModMinecraft.McFolderList.First().Location;
					}
					if (Settings.Get<bool>("SystemDebugDelay"))
					{
						Thread.Sleep(ModBase.RandomInteger(500, 3000));
					}
					if (text != null)
					{
						try
						{
							ModLoader.LoaderCombo<string> loaderCombo = ModModpack.ModpackInstall(text);
							Logger.Info($"自动安装整合包已开始：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
							loaderCombo.WaitForExit();
							if (loaderCombo.State == ModBase.LoadState.Finished)
							{
								Logger.Info($"自动安装整合包成功，清理安装包：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
								FileUtils.Delete(text);
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							if (ex2.IsCanceled())
							{
								Logger.Info($"自动安装整合包已取消：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
							}
							else
							{
								Logger.Error(ex2, $"自动安装整合包失败：{text}", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
							}
							ProjectData.ClearProjectError();
						}
					}
					string text2 = ModBase.ReadIni(ModMinecraft.McFolderSelected + "PCL.ini", "Version");
					ModMinecraft.McInstance mcInstance = ((Operators.CompareString(text2, "", TextCompare: false) == 0) ? null : new ModMinecraft.McInstance(text2));
					if ((object)mcInstance == null || !mcInstance.PathVersion.StartsWithF(ModMinecraft.McFolderSelected) || !mcInstance.Check())
					{
						Logger.Info(string.Format("当前选择的 Minecraft 版本无效：{0}", ((object)mcInstance == null) ? "null" : mcInstance.PathVersion), (!Information.IsNothing(mcInstance)) ? LogBehavior.ToastIfDebug : LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						if (ModMinecraft.McInstanceListLoader.State != ModBase.LoadState.Finished)
						{
							ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", WaitForExit: true);
						}
						if (!ModMinecraft.McInstanceList.Any() || ModMinecraft.McInstanceList.First().Value[0].Logo.Contains("RedstoneBlock"))
						{
							mcInstance = null;
							Logger.Info("无可用 Minecraft 版本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						}
						else
						{
							mcInstance = ModMinecraft.McInstanceList.First().Value[0];
							Logger.Info($"自动选择 Minecraft 版本：{mcInstance.PathVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
						}
					}
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMinecraft.McInstanceSelected = mcInstance;
						IsLoadFinished = true;
						RefreshButtonsUI();
						RefreshPage(KeepInput: false, Anim: false);
						if (Operators.CompareString(ModLaunch.McLoginAble(), "", TextCompare: false) == 0)
						{
							ModLaunch.McLoginLoader.Start();
						}
						if (Environment.CommandLine.Contains("--test"))
						{
							ModBase.RunInThread([SpecialName] () =>
							{
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									ModMain.FrmMain.PageChange(FormMain.PageType.Download);
								});
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									ModMain.FrmMain.PageChange(FormMain.PageType.Download, FormMain.PageSubType.DownloadMod);
								});
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									ModMain.FrmMain.PageChange(FormMain.PageType.Setup);
								});
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									ModMain.FrmMain.PageChange(FormMain.PageType.Other);
								});
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									BtnVersion_Click();
								});
								Thread.Sleep(500);
								ModBase.RunInUi([SpecialName] () =>
								{
									BtnMore_Click();
								});
								Thread.Sleep(500);
								FormMain.EndProgramForce((ModBase.ProcessReturnValues)123);
							});
						}
					});
				}, "Version Check", ThreadPriority.AboveNormal);
				ModLaunch.McLoginType mcLoginType = Settings.Get<ModLaunch.McLoginType>("LoginType");
				if (mcLoginType == ModLaunch.McLoginType.Legacy || mcLoginType == ModLaunch.McLoginType.Ms)
				{
					((MyRadioButton)FindName("RadioLoginType" + Conversions.ToString(unchecked((int)mcLoginType)))).Checked = true;
				}
				RefreshPage(KeepInput: false, Anim: false);
				ModAnimation.AniControlEnabled--;
			}
		}
		public void PageChangeToLaunching()
		{
			switch (Settings.Get<ModLaunch.McLoginType>("LoginType"))
			{
			case ModLaunch.McLoginType.Legacy:
				LabLaunchingMethod.Text = "离线登录";
				break;
			case ModLaunch.McLoginType.Ms:
				LabLaunchingMethod.Text = "正版登录";
				break;
			case ModLaunch.McLoginType.Nide:
				LabLaunchingMethod.Text = "统一通行证";
				break;
			case ModLaunch.McLoginType.Auth:
				LabLaunchingMethod.Text = "Authlib-Injector";
				break;
			}
			LabLaunchingName.Text = ModMinecraft.McInstanceSelected.Name;
			LabLaunchingStage.Text = "初始化";
			LabLaunchingTitle.Text = ((ModLaunch.CurrentLaunchOptions?.SaveBatch == null) ? "正在启动游戏" : "正在导出启动脚本");
			LabLaunchingProgress.Text = "0.00 %";
			LabLaunchingProgress.Opacity = 1.0;
			LabLaunchingDownload.Visibility = Visibility.Visible;
			LabLaunchingProgressLeft.Opacity = 0.6;
			LabLaunchingDownload.Visibility = Visibility.Visible;
			LabLaunchingDownload.Text = "0 B/s";
			LabLaunchingDownload.Opacity = 0.0;
			LabLaunchingDownload.Visibility = Visibility.Collapsed;
			LabLaunchingDownloadLeft.Opacity = 0.0;
			LabLaunchingDownloadLeft.Visibility = Visibility.Collapsed;
			ProgressLaunchingFinished.Width = new GridLength(0.0, GridUnitType.Star);
			ProgressLaunchingUnfinished.Width = new GridLength(1.0, GridUnitType.Star);
			PanLaunchingHint.Opacity = 0.0;
			PanLaunchingHint.Visibility = Visibility.Collapsed;
			PanLaunchingInfo.Width = double.NaN;
			ModLaunch.McLaunchProcess = null;
			ModLaunch.McLaunchWatcher = null;
			LabLaunchingHint.Text = PageOtherTest.GetRandomHint();
			PanInput.IsHitTestVisible = false;
			PanLaunching.IsHitTestVisible = false;
			LoadLaunching.State.LoadingState = MyLoading.MyLoadingState.Run;
			PanLaunching.Visibility = Visibility.Visible;
			ModAnimation.AniStart(new ModAnimation.AniData[6]
			{
				ModAnimation.AaOpacity(PanInput, 0.0, 50),
				ModAnimation.AaOpacity(PanInput, 0.0 - PanInput.Opacity, 110, 0, new ModAnimation.AniEaseInFluent(), After: true),
				ModAnimation.AaScaleTransform(PanInput, 1.2 - ((ScaleTransform)PanInput.RenderTransform).ScaleX, 160),
				ModAnimation.AaOpacity(PanLaunching, 1.0 - PanLaunching.Opacity, 150, 100),
				ModAnimation.AaScaleTransform(PanLaunching, 1.0 - ((ScaleTransform)PanLaunching.RenderTransform).ScaleX, 500, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanLaunching.IsHitTestVisible = true;
				}, 150)
			}, "Launch State Page");
		}
		public void PageChangeToLogin()
		{
			NewLateBinding.LateCall(PageGet(PageCurrent), null, "Reload", new object[1] { false }, new string[1] { "KeepInput" }, null, null, IgnoreReturn: true);
			PanInput.IsHitTestVisible = false;
			PanLaunching.IsHitTestVisible = false;
			LoadLaunching.State.LoadingState = MyLoading.MyLoadingState.Stop;
			PanInput.Visibility = Visibility.Visible;
			ModAnimation.AniStart(new ModAnimation.AniData[5]
			{
				ModAnimation.AaOpacity(PanLaunching, 0.0 - PanLaunching.Opacity, 150),
				ModAnimation.AaScaleTransform(PanLaunching, 0.8 - ((ScaleTransform)PanLaunching.RenderTransform).ScaleX, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaOpacity(PanInput, 1.0 - PanInput.Opacity, 250, 50),
				ModAnimation.AaScaleTransform(PanInput, 1.0 - ((ScaleTransform)PanInput.RenderTransform).ScaleX, 300, 50, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanInput.IsHitTestVisible = true;
				}, 200)
			}, "Launch State Page", RefreshTime: true);
		}
		private object PageGet(PageType Type)
		{
			switch (Type)
			{
			case PageType.Legacy:
				if (Information.IsNothing(ModMain.FrmLoginLegacy))
				{
					ModMain.FrmLoginLegacy = new PageLoginLegacy();
				}
				return ModMain.FrmLoginLegacy;
			case PageType.Nide:
				if (Information.IsNothing(ModMain.FrmLoginNide))
				{
					ModMain.FrmLoginNide = new PageLoginNide();
				}
				return ModMain.FrmLoginNide;
			case PageType.NideSkin:
				if (Information.IsNothing(ModMain.FrmLoginNideSkin))
				{
					ModMain.FrmLoginNideSkin = new PageLoginNideSkin();
				}
				return ModMain.FrmLoginNideSkin;
			case PageType.Auth:
				if (Information.IsNothing(ModMain.FrmLoginAuth))
				{
					ModMain.FrmLoginAuth = new PageLoginAuth();
				}
				return ModMain.FrmLoginAuth;
			case PageType.AuthSkin:
				if (Information.IsNothing(ModMain.FrmLoginAuthSkin))
				{
					ModMain.FrmLoginAuthSkin = new PageLoginAuthSkin();
				}
				return ModMain.FrmLoginAuthSkin;
			case PageType.Ms:
				if (Information.IsNothing(ModMain.FrmLoginMs))
				{
					ModMain.FrmLoginMs = new PageLoginMs();
				}
				return ModMain.FrmLoginMs;
			case PageType.MsSkin:
				if (Information.IsNothing(ModMain.FrmLoginMsSkin))
				{
					ModMain.FrmLoginMsSkin = new PageLoginMsSkin();
				}
				return ModMain.FrmLoginMsSkin;
			default:
				throw new ArgumentOutOfRangeException("Type", "即将切换的登录分页编号越界");
			}
		}
		private object PageChange(PageType Type, bool Anim)
		{
			object obj = ModMain.FrmLoginMs;
			checked
			{
				object result;
				try
				{
					if (PageCurrent == Type)
					{
						result = obj;
					}
					else
					{
						obj = RuntimeHelpers.GetObjectValue(PageGet(Type));
						ModAnimation.AniStop("FrmLogin PageChange");
						if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(obj)) && !Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj, null, "Parent", new object[0], null, null, null))))
						{
							NewLateBinding.LateCall(obj, null, "SetValue", new object[2]
							{
								ContentPresenter.ContentProperty,
								null
							}, null, null, null, IgnoreReturn: true);
						}
						if (Anim)
						{
							Dispatcher.Invoke([SpecialName] () =>
							{
								ModAnimation.AniStart(new ModAnimation.AniData[3]
								{
									ModAnimation.AaOpacity(PanLogin, 0.0 - PanLogin.Opacity, 100, 0, new ModAnimation.AniEaseOutFluent()),
									ModAnimation.AaCode([SpecialName] () =>
									{
										ModAnimation.AniControlEnabled++;
										PanLogin.Children.Clear();
										PanLogin.Children.Add((UIElement)obj);
										ModAnimation.AniControlEnabled--;
									}, 100),
									ModAnimation.AaOpacity(PanLogin, 1.0, 100, 120, new ModAnimation.AniEaseInFluent())
								}, "FrmLogin PageChange");
							}, DispatcherPriority.Render);
						}
						else
						{
							ModAnimation.AniControlEnabled++;
							PanLogin.Children.Clear();
							PanLogin.Children.Add((UIElement)obj);
							ModAnimation.AniControlEnabled--;
						}
						PageCurrent = Type;
						result = obj;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"切换登录分页失败（{Type}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					result = obj;
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}
		public void RefreshPage(bool KeepInput, bool Anim)
		{
			int num;
			if ((object)ModMinecraft.McInstanceSelected != null)
			{
				num = Settings.Get<int>("VersionServerLogin", ModMinecraft.McInstanceSelected);
				Settings.Set("LoginPageType", num);
			}
			else
			{
				num = Settings.Get<int>("LoginPageType");
			}
			PageType pageType;
			switch (num)
			{
			case 0:
				if (RadioLoginType5.Checked)
				{
					pageType = ((Operators.CompareString(Settings.Get<string>("CacheMsV2Access"), "", TextCompare: false) != 0) ? PageType.MsSkin : PageType.Ms);
					Settings.Set("LoginType", ModLaunch.McLoginType.Ms);
				}
				else
				{
					pageType = PageType.Legacy;
					Settings.Set("LoginType", ModLaunch.McLoginType.Legacy);
				}
				PanType.Visibility = Visibility.Visible;
				PanTypeOne.Visibility = Visibility.Collapsed;
				RadioLoginType5.Visibility = Visibility.Visible;
				RadioLoginType0.Visibility = Visibility.Visible;
				break;
			case 1:
				pageType = ((Operators.CompareString(Settings.Get<string>("CacheMsV2Access"), "", TextCompare: false) != 0) ? PageType.MsSkin : PageType.Ms);
				Settings.Set("LoginType", ModLaunch.McLoginType.Ms);
				PanType.Visibility = Visibility.Collapsed;
				PanTypeOne.Visibility = Visibility.Visible;
				PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M511.488256 95.184408c35.310345 22.516742 95.184408 55.78011 167.34033 84.437781 75.738131 29.681159 148.405797 40.93953 191.392304 45.033483v353.615193c0 73.691154-50.662669 164.781609-136.123938 244.101949C649.65917 901.181409 558.568716 942.12094 512 942.12094c-46.568716 0-137.65917-40.93953-222.096952-119.748126C204.441779 742.54073 153.77911 651.450275 153.77911 577.247376v-353.103448c42.474763-4.093953 116.165917-15.352324 191.904048-45.545227 75.226387-30.192904 133.565217-63.456272 165.805098-83.414293M512 0c-4.093953 0-8.187906 1.535232-11.258371 3.582209l-14.84058 10.234882c-1.023488 0.511744-67.550225 47.592204-170.410794 88.531735-100.813593 39.916042-198.556722 41.963018-199.58021 41.963018l-25.075462 0.511744c-10.746627 0.511744-18.934533 8.187906-18.934533 18.422789v414.000999c0 216.97951 286.064968 446.24088 440.09995 446.24088s440.09995-229.261369 440.09995-445.729136V163.758121c0-10.234883-8.69965-18.422789-18.934533-18.422789l-24.563718-0.511744c-1.023488 0-98.766617-2.046977-199.58021-41.963018-103.372314-40.93953-170.410795-88.01999-170.922538-88.531734L523.258371 3.582209c-3.070465-2.558721-7.164418-3.582209-11.258371-3.582209z M743.308346 410.930535l-260.477761 260.477761c-15.864068 15.864068-41.963018 15.864068-57.827087 0l-144.823588-144.823588c-15.864068-15.864068-15.864068-41.963018 0-57.827087 8.187906-8.187906 18.422789-11.770115 29.169415-11.770115 10.234883 0 20.981509 4.093953 29.169416 11.770115l115.654173 115.654173L685.993003 352.591704c15.864068-15.864068 41.963018-15.864068 57.827087 0 15.352324 16.375812 15.352324 42.474763-0.511744 58.338831z");
				LabTypeOne.Text = "正版登录";
				RadioLoginType5.Visibility = Visibility.Visible;
				RadioLoginType0.Visibility = Visibility.Collapsed;
				break;
			case 2:
				pageType = PageType.Legacy;
				Settings.Set("LoginType", ModLaunch.McLoginType.Legacy);
				PanType.Visibility = Visibility.Collapsed;
				PanTypeOne.Visibility = Visibility.Visible;
				PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M533.293176 788.841412a60.235294 60.235294 0 1 1 85.202824 85.202823l-42.616471 42.586353c-129.355294 129.385412-339.124706 129.385412-468.510117 0-129.385412-129.385412-129.385412-339.124706 0-468.510117l42.586353-42.616471a60.235294 60.235294 0 1 1 85.202823 85.202824l-42.61647 42.586352a210.823529 210.823529 0 1 0 298.164706 298.164706l42.586352-42.61647z m255.548236-255.548236l42.61647-42.586352a210.823529 210.823529 0 1 0-298.164706-298.164706l-42.586352 42.61647a60.235294 60.235294 0 1 1-85.202824-85.202823l42.616471-42.586353c129.355294-129.385412 339.124706-129.385412 468.510117 0 129.385412 129.385412 129.385412 339.124706 0 468.510117l-42.586353 42.616471a60.235294 60.235294 0 1 1-85.202823-85.202824zM192.542118 192.542118a60.235294 60.235294 0 0 1 85.202823 0l553.712941 553.712941a60.235294 60.235294 0 0 1-85.202823 85.202823L192.542118 277.744941a60.235294 60.235294 0 0 1 0-85.202823z");
				LabTypeOne.Text = "离线登录";
				break;
			case 3:
				pageType = ((Operators.CompareString(Settings.Get<string>("CacheNideAccess"), "", TextCompare: false) != 0) ? PageType.NideSkin : PageType.Nide);
				Settings.Set("LoginType", ModLaunch.McLoginType.Nide);
				PanType.Visibility = Visibility.Collapsed;
				PanTypeOne.Visibility = Visibility.Visible;
				PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M834.5 684.1c-31.2-70.4-98.9-120.9-179.1-127.3 63.5-8.5 112.6-63 112.6-128.8 0-71.8-58.2-130-130-130s-130 58.2-130 130c0 65.9 49 120.3 112.6 128.8-80.2 6.4-148 57-179.1 127.3-8.7 19.7 6 42 27.6 42 12.1 0 22.7-7.5 27.7-18.5 24.3-53.9 78.5-91.5 141.3-91.5s117 37.6 141.3 91.5c5 11.1 15.6 18.5 27.7 18.5 21.4 0 36.1-22.3 27.4-42zM567.9 427.9c0-38.6 31.4-70 70-70s70 31.4 70 70-31.4 70-70 70-70-31.4-70-70zM460.3 347.9H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h243.3c16.6 0 30-13.4 30-30 0.1-16.5-13.4-30-29.9-30zM367.4 459.6H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h150.4c16.6 0 30-13.4 30-30 0.1-16.6-13.4-30-29.9-30zM297.4 571.2H217c-16.6 0-30 13.4-30 30s13.4 30 30 30h80.4c16.6 0 30-13.4 30-30 0-16.5-13.5-30-30-30zM900 236v552H124V236h776m0-60H124c-33.1 0-60 26.9-60 60v552c0 33.1 26.9 60 60 60h776c33.1 0 60-26.9 60-60V236c0-33.1-26.9-60-60-60z");
				LabTypeOne.Text = "统一通行证登录";
				break;
			case 4:
				pageType = ((Operators.CompareString(Settings.Get<string>("CacheAuthAccess"), "", TextCompare: false) != 0) ? PageType.AuthSkin : PageType.Auth);
				Settings.Set("LoginType", ModLaunch.McLoginType.Auth);
				PanType.Visibility = Visibility.Collapsed;
				PanTypeOne.Visibility = Visibility.Visible;
				PathTypeOne.Data = (Geometry)new GeometryConverter().ConvertFromString("M834.5 684.1c-31.2-70.4-98.9-120.9-179.1-127.3 63.5-8.5 112.6-63 112.6-128.8 0-71.8-58.2-130-130-130s-130 58.2-130 130c0 65.9 49 120.3 112.6 128.8-80.2 6.4-148 57-179.1 127.3-8.7 19.7 6 42 27.6 42 12.1 0 22.7-7.5 27.7-18.5 24.3-53.9 78.5-91.5 141.3-91.5s117 37.6 141.3 91.5c5 11.1 15.6 18.5 27.7 18.5 21.4 0 36.1-22.3 27.4-42zM567.9 427.9c0-38.6 31.4-70 70-70s70 31.4 70 70-31.4 70-70 70-70-31.4-70-70zM460.3 347.9H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h243.3c16.6 0 30-13.4 30-30 0.1-16.5-13.4-30-29.9-30zM367.4 459.6H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h150.4c16.6 0 30-13.4 30-30 0.1-16.6-13.4-30-29.9-30zM297.4 571.2H217c-16.6 0-30 13.4-30 30s13.4 30 30 30h80.4c16.6 0 30-13.4 30-30 0-16.5-13.5-30-30-30zM900 236v552H124V236h776m0-60H124c-33.1 0-60 26.9-60 60v552c0 33.1 26.9 60 60 60h776c33.1 0 60-26.9 60-60V236c0-33.1-26.9-60-60-60z");
				LabTypeOne.Text = (((object)ModMinecraft.McInstanceSelected == null) ? Settings.Get<string>("CacheAuthServerName") : Settings.Get<string>("VersionServerAuthName", ModMinecraft.McInstanceSelected));
				if (Operators.CompareString(LabTypeOne.Text, "", TextCompare: false) == 0)
				{
					LabTypeOne.Text = "第三方登录";
				}
				break;
			default:
				Logger.Error($"未知的登录页面：{num}", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
				goto case 0;
			}
			if (PageCurrent != pageType)
			{
				object instance = PageChange(pageType, Anim);
				object[] array = new object[1] { KeepInput };
				object[] array2 = array;
				bool[] array3 = new bool[1] { true };
				bool[] array4 = array3;
				NewLateBinding.LateCall(instance, null, "Reload", array, null, null, array3, IgnoreReturn: true);
				if (array4[0])
				{
					KeepInput = (bool)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(bool));
				}
				MyRadioButton myRadioButton = (MyRadioButton)FindName("RadioLoginType" + Conversions.ToString((int)Settings.Get<ModLaunch.McLoginType>("LoginType")));
				if (myRadioButton != null)
				{
					myRadioButton.Checked = true;
				}
			}
		}
		private void RadioLoginType_Change(object sender, bool raiseByMouse)
		{
			if (raiseByMouse)
			{
				RefreshPage(KeepInput: true, Anim: true);
			}
		}
		private static (string, string) SkinMsInput()
		{
			return (Settings.Get<string>("CacheMsV2Name"), Settings.Get<string>("CacheMsV2Uuid"));
		}
		private static void SkinMsLoad(ModLoader.LoaderTask<(string, string), string> Data)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				if (ModMain.FrmLoginMsSkin != null && ModMain.FrmLoginMsSkin.Skin != null)
				{
					ModMain.FrmLoginMsSkin.Skin.Clear();
				}
			});
			string item = Data.Input.Item1;
			string item2 = Data.Input.Item2;
			if (Operators.CompareString(item, "", TextCompare: false) == 0)
			{
				Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
				Logger.Info("获取微软正版皮肤失败，ID 为空", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
			}
			else
			{
				try
				{
					string address = ModMinecraft.McSkinGetAddress(item2, "Ms");
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					address = ModMinecraft.McSkinDownload(address);
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					Data.Output = address;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.IsCanceled())
					{
						Data.Output = "";
						ProjectData.ClearProjectError();
						return;
					}
					if (ex2.GetDisplay(multiline: false).Contains("(429)"))
					{
						Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
						Logger.Error($"获取正版皮肤失败（{item}）：获取皮肤太过频繁，请 5 分钟后再试！", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else if (ex2.GetDisplay(multiline: false).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
						Logger.Info("用户未设置自定义皮肤，跳过皮肤加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else
					{
						Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
						Logger.Error(ex2, $"获取微软正版皮肤失败（{item}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					ProjectData.ClearProjectError();
				}
			}
			if (ModMain.FrmLoginMsSkin != null)
			{
				ModBase.RunInUi(ModMain.FrmLoginMsSkin.Skin.Load);
			}
			else if (!Data.IsCanceled)
			{
				Data.Input = default;
			}
		}
		private static (string, string) SkinLegacyInput()
		{
			int num = Settings.Get<int>("LaunchSkinType");
			(string, string) result;
			switch (num)
			{
			case 0:
			{
				PageLoginLegacy frmLoginLegacy = ModMain.FrmLoginLegacy;
				if (frmLoginLegacy != null && frmLoginLegacy.IsReloaded)
				{
					result = ("0", ModMain.FrmLoginLegacy.ComboName.Text.Trim() ?? "");
				}
				else
				{
					result = ((Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) != 0) ? ("0", Settings.Get<string>("LoginLegacyName").ToString().BeforeFirst("\u00a8") ?? "") : ("0", ""));
				}
				break;
			}
			case 3:
				result = ("3", Settings.Get<string>("LaunchSkinID"));
				break;
			default:
				result = (num.ToString(), "");
				break;
			}
			return result;
		}
		private static void SkinLegacyLoad(ModLoader.LoaderTask<(string, string), string> Data)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				if (ModMain.FrmLoginLegacy != null && ModMain.FrmLoginLegacy.Skin != null)
				{
					ModMain.FrmLoginLegacy.Skin.Clear();
				}
			});
			switch (Conversions.ToInteger(Data.Input.Item1))
			{
			case 0:
				Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(Data.Input.Item2))) + ".png";
				break;
			case 1:
				Data.Output = ModBase.PathImage + "Skins/Steve.png";
				break;
			case 2:
				Data.Output = ModBase.PathImage + "Skins/Alex.png";
				break;
			case 3:
			{
				string item = Data.Input.Item2;
				try
				{
					if (item.Count() < 2)
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						break;
					}
					string uUID = Conversions.ToString(ModLaunch.McLoginMojangUuid(item, ThrowOnNotFound: true));
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					uUID = ModMinecraft.McSkinGetAddress(uUID, "Mojang");
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					uUID = ModMinecraft.McSkinDownload(uUID);
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					Data.Output = uUID;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.IsCanceled())
					{
						Data.Output = "";
						ProjectData.ClearProjectError();
						return;
					}
					if (ex2.GetDisplay(multiline: false).Contains("(429)"))
					{
						Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
						Logger.Info($"获取离线登录使用的正版皮肤失败（{item}）：获取皮肤太过频繁，请 5 分钟后再试！", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else
					{
						Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
						Logger.Warn(ex2, $"获取离线登录使用的正版皮肤失败（{item}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					ProjectData.ClearProjectError();
				}
				break;
			}
			case 4:
				if (!FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
				{
					ModMain.Hint("未找到离线皮肤自定义文件，可能它已被删除。PCL 将使用默认的 Steve 皮肤！");
					Settings.Set("LaunchSkinType", 1);
					goto case 1;
				}
				Data.Output = Paths.AppDataThenName + "CustomSkin.png";
				break;
			}
			if (ModMain.FrmLoginLegacy != null)
			{
				ModBase.RunInUi(ModMain.FrmLoginLegacy.Skin.Load);
			}
			else if (!Data.IsCanceled)
			{
				Data.Input = default;
			}
		}
		private static (string, string) SkinNideInput()
		{
			return (Settings.Get<string>("CacheNideName"), Settings.Get<string>("CacheNideUuid"));
		}
		private static void SkinNideLoad(ModLoader.LoaderTask<(string, string), string> Data)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				if (ModMain.FrmLoginNideSkin != null && ModMain.FrmLoginNideSkin.Skin != null)
				{
					ModMain.FrmLoginNideSkin.Skin.Clear();
				}
			});
			string item = Data.Input.Item1;
			string item2 = Data.Input.Item2;
			if (Operators.CompareString(item, "", TextCompare: false) == 0)
			{
				Data.Output = ModBase.PathImage + "Skins/" + ModMinecraft.McSkinSex(Conversions.ToString(ModLaunch.McLoginLegacyUuid(item))) + ".png";
				Logger.Info("获取统一通行证皮肤失败，ID 为空", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
			}
			else
			{
				try
				{
					string address = ModMinecraft.McSkinGetAddress(item2, "Nide");
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					address = ModMinecraft.McSkinDownload(address);
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					Data.Output = address;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.IsCanceled())
					{
						Data.Output = "";
						ProjectData.ClearProjectError();
						return;
					}
					if (ex2.GetDisplay(multiline: false).Contains("(429)"))
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Error($"获取统一通行证皮肤失败（{item}）：获取皮肤太过频繁，请 5 分钟后再试！", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else if (ex2.GetDisplay(multiline: false).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Info("用户未设置自定义皮肤，跳过皮肤加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Error(ex2, $"获取统一通行证皮肤失败（{item}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					ProjectData.ClearProjectError();
				}
			}
			if (ModMain.FrmLoginNideSkin != null)
			{
				ModBase.RunInUi(ModMain.FrmLoginNideSkin.Skin.Load);
			}
			else if (!Data.IsCanceled)
			{
				Data.Input = default;
			}
		}
		private static (string, string) SkinAuthInput()
		{
			return (Settings.Get<string>("CacheAuthName"), Settings.Get<string>("CacheAuthUuid"));
		}
		private static void SkinAuthLoad(ModLoader.LoaderTask<(string, string), string> Data)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				if (ModMain.FrmLoginAuthSkin != null && ModMain.FrmLoginAuthSkin.Skin != null)
				{
					ModMain.FrmLoginAuthSkin.Skin.Clear();
				}
			});
			string item = Data.Input.Item1;
			string item2 = Data.Input.Item2;
			if (Operators.CompareString(item, "", TextCompare: false) == 0)
			{
				Data.Output = ModBase.PathImage + "Skins/Steve.png";
				Logger.Info("获取 Authlib-Injector 皮肤失败，ID 为空", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
			}
			else
			{
				try
				{
					string address = ModMinecraft.McSkinGetAddress(item2, "Auth");
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					address = ModMinecraft.McSkinDownload(address);
					if (Data.IsCanceled)
					{
						throw new OperationCanceledException("当前任务已取消：" + item);
					}
					Data.Output = address;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.IsCanceled())
					{
						Data.Output = "";
						ProjectData.ClearProjectError();
						return;
					}
					if (ex2.GetDisplay(multiline: false).Contains("(429)"))
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Error($"获取 Authlib-Injector 皮肤失败（{item}）：获取皮肤太过频繁，请 5 分钟后再试！", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else if (ex2.GetDisplay(multiline: false).Contains("未设置自定义皮肤"))
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Info("用户未设置自定义皮肤，跳过皮肤加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					else
					{
						Data.Output = ModBase.PathImage + "Skins/Steve.png";
						Logger.Error(ex2, $"获取 Authlib-Injector 皮肤失败（{item}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					}
					ProjectData.ClearProjectError();
				}
			}
			if (ModMain.FrmLoginAuthSkin != null)
			{
				ModBase.RunInUi(ModMain.FrmLoginAuthSkin.Skin.Load);
			}
			else if (!Data.IsCanceled)
			{
				Data.Input = default;
			}
		}
		private void BtnVersion_Click()
		{
			if (ModLaunch.McLaunchLoader.State != ModBase.LoadState.Loading)
			{
				ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
			}
		}
		public void LaunchButtonClick()
		{
			if (ModLaunch.McLaunchLoader.State != ModBase.LoadState.Loading && BtnLaunch.IsEnabled && (ModMain.FrmMain.PageRight == null || ModMain.FrmMain.PageRight.PageState == MyPageRight.PageStates.ContentStay || ModMain.FrmMain.PageRight.PageState == MyPageRight.PageStates.ContentEnter))
			{
				if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
				{
					ModSecret.ThemeUnlock(12, ShowDoubleHint: false, "隐藏主题 滑稽彩 已解锁！");
					ModMain.IsAprilGiveup = true;
					Settings.Set("AprilYear", DateTime.Now.Year);
					ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleX = 1.0;
					ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleY = 1.0;
					ModMain.FrmLaunchLeft.AprilPosTrans.X = 0.0;
					ModMain.FrmLaunchLeft.AprilPosTrans.Y = 0.0;
					ModMain.FrmMain.BtnExtraApril.ShowRefresh();
				}
				if (Operators.CompareString(BtnLaunch.Text, "启动游戏", TextCompare: false) == 0)
				{
					ModLaunch.McLaunchStart();
				}
				else if (Operators.CompareString(BtnLaunch.Text, "下载游戏", TextCompare: false) == 0)
				{
					ModMain.FrmMain.PageChange(FormMain.PageType.Download);
				}
			}
		}
		public void RefreshButtonsUI()
		{
			if (!BtnLaunch.IsLoaded)
			{
				return;
			}
			int num = ((IsLoadFinished && ModMinecraft.McInstanceListLoader.State != ModBase.LoadState.Loading && ModMinecraft.McFolderListLoader.State != ModBase.LoadState.Loading) ? (((object)ModMinecraft.McInstanceSelected != null) ? 3 : ((Settings.Get<bool>("UiHiddenPageDownload") && !PageSetupUI.HiddenForceShow) ? 1 : 2)) : 0);
			if (num != BtnLaunchState || Operators.CompareString(((object)ModMinecraft.McInstanceSelected == null) ? "" : ModMinecraft.McInstanceSelected.PathVersion, ((object)BtnLaunchInstance == null) ? "" : BtnLaunchInstance.PathVersion, TextCompare: false) != 0)
			{
				BtnLaunchInstance = ModMinecraft.McInstanceSelected;
				BtnLaunchState = num;
				switch (num)
				{
				case 0:
					Logger.Info("启动按钮：正在加载 Minecraft 版本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					ModMain.FrmLaunchLeft.BtnLaunch.Text = "正在加载";
					ModMain.FrmLaunchLeft.BtnLaunch.IsEnabled = false;
					ModMain.FrmLaunchLeft.LabVersion.Text = "正在加载中，请稍候";
					ModMain.FrmLaunchLeft.BtnVersion.IsEnabled = false;
					ModMain.FrmLaunchLeft.BtnMore.Visibility = Visibility.Collapsed;
					break;
				case 1:
					Logger.Info("启动按钮：无 Minecraft 版本，下载已禁用", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					ModMain.FrmLaunchLeft.BtnLaunch.Text = "启动游戏";
					ModMain.FrmLaunchLeft.BtnLaunch.IsEnabled = false;
					ModMain.FrmLaunchLeft.LabVersion.Text = "未找到可用的游戏版本";
					ModMain.FrmLaunchLeft.BtnVersion.IsEnabled = true;
					ModMain.FrmLaunchLeft.BtnMore.Visibility = Visibility.Collapsed;
					break;
				case 2:
					Logger.Info("启动按钮：无 Minecraft 版本，要求下载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					ModMain.FrmLaunchLeft.BtnLaunch.Text = "下载游戏";
					ModMain.FrmLaunchLeft.BtnLaunch.IsEnabled = true;
					ModMain.FrmLaunchLeft.LabVersion.Text = "未找到可用的游戏版本";
					ModMain.FrmLaunchLeft.BtnVersion.IsEnabled = true;
					ModMain.FrmLaunchLeft.BtnMore.Visibility = Visibility.Collapsed;
					break;
				case 3:
					Logger.Info($"启动按钮：Minecraft 版本：{ModMinecraft.McInstanceSelected.PathVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					ModMain.FrmLaunchLeft.BtnLaunch.Text = "启动游戏";
					ModMain.FrmLaunchLeft.BtnVersion.IsEnabled = true;
					ModMain.FrmLaunchLeft.BtnLaunch.IsEnabled = true;
					ModMain.FrmLaunchLeft.LabVersion.Text = ModMinecraft.McInstanceSelected.Name;
					break;
				}
			}
			ModMain.FrmLaunchLeft.BtnVersion.Visibility = ((!PageSetupUI.HiddenForceShow && Settings.Get<bool>("UiHiddenFunctionSelect")) ? Visibility.Collapsed : Visibility.Visible);
			if (num == 3)
			{
				ModMain.FrmLaunchLeft.BtnMore.Visibility = ModMain.FrmLaunchLeft.BtnVersion.Visibility;
			}
		}
		private void BtnCancel_Click()
		{
			if (ModLaunch.McLaunchLoaderReal == null)
			{
				return;
			}
			ModLaunch.McLaunchLoaderReal.Cancel();
			ModLaunch.McLaunchLog("已取消启动");
			try
			{
				if (ModLaunch.McLaunchWatcher != null)
				{
					ModLaunch.McLaunchWatcher.Kill();
				}
				else if (ModLaunch.McLaunchProcess != null && !ModLaunch.McLaunchProcess.HasExited)
				{
					ModLaunch.McLaunchProcess.Kill();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "取消启动结束进程失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnMore_Click()
		{
			if (ModLaunch.McLaunchLoader.State != ModBase.LoadState.Loading)
			{
				ModMinecraft.McInstanceSelected.Load();
				PageInstanceLeft.Instance = ModMinecraft.McInstanceSelected;
				ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSetup);
			}
		}
		public void LaunchingRefresh()
		{
			try
			{
				_Closure___33_0 arg = default;
				_Closure___33_0 CS_8__locals11 = new _Closure___33_0(arg);
				CS_8__locals11._VB_Me = this;
				if (ModLaunch.McLaunchLoaderReal.State == ModBase.LoadState.Canceled)
				{
					return;
				}
				bool flag = false;
				try
				{
					foreach (ModLoader.LoaderBase loader in ModLaunch.McLaunchLoaderReal.GetLoaderList(RequireShow: false))
					{
						if (loader.State != ModBase.LoadState.Loading && loader.State != ModBase.LoadState.Waiting)
						{
							continue;
						}
						LabLaunchingStage.Text = loader.Name;
						flag = Operators.CompareString(loader.Name, "等待游戏窗口出现", TextCompare: false) == 0 || Operators.CompareString(loader.Name, "结束处理", TextCompare: false) == 0;
						goto end_IL_0022;
					}
					LabLaunchingStage.Text = "已完成";
					end_IL_0022:;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "获取是否启动完成失败，可能是由于启动状态改变导致集合已修改", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					ProjectData.ClearProjectError();
					return;
				}
				TextBlock labLaunchingTitle = LabLaunchingTitle;
				string text;
				if (flag || ModLaunch.McLaunchLoaderReal.State == ModBase.LoadState.Finished)
				{
					text = "已启动游戏";
				}
				else
				{
					text = ((ModLaunch.CurrentLaunchOptions.SaveBatch == null) ? "正在启动游戏" : "正在导出启动脚本");
				}
				labLaunchingTitle.Text = text;
				if (ModAnimation.AniIsRun("Launch State Page"))
				{
					flag = false;
				}
				double progress = ModLaunch.McLaunchLoaderReal.Progress;
				if (progress >= ShowProgress)
				{
					ShowProgress += (progress - ShowProgress) * 0.1 + 0.0025;
				}
				if (progress <= ShowProgress)
				{
					ShowProgress = progress;
				}
				if (flag)
				{
					ShowProgress = 1.0;
				}
				LabLaunchingProgress.Text = (ShowProgress * 100.0).ToString("0.00") + " %";
				CS_8__locals11._VB_Local_HasLaunchDownloader = false;
				try
				{
					if (ModNet.NetManager.Speed != 0L || LabLaunchingDownload.Visibility != Visibility.Collapsed)
					{
						foreach (ModNet.LoaderDownload task in ModNet.NetManager.Tasks)
						{
							if (task.RealParent != null && Operators.CompareString(task.RealParent.Name, "Minecraft 启动", TextCompare: false) == 0 && task.State == ModBase.LoadState.Loading)
							{
								CS_8__locals11._VB_Local_HasLaunchDownloader = true;
							}
						}
					}
					else
					{
						CS_8__locals11._VB_Local_HasLaunchDownloader = false;
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "获取 Minecraft 启动下载器失败，可能是因为启动被取消", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
					CS_8__locals11._VB_Local_HasLaunchDownloader = false;
					ProjectData.ClearProjectError();
				}
				LabLaunchingDownload.Text = StringUtils.FormatByteSize(ModNet.NetManager.Speed) + "/s";
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>
				{
					ModAnimation.AaGridLengthWidth(ProgressLaunchingFinished, ShowProgress - ProgressLaunchingFinished.Width.Value, 130, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaGridLengthWidth(ProgressLaunchingUnfinished, 1.0 - ShowProgress - ProgressLaunchingUnfinished.Width.Value, 130, 0, new ModAnimation.AniEaseOutFluent())
				};
				if (CS_8__locals11._VB_Local_HasLaunchDownloader == (LabLaunchingDownload.Visibility == Visibility.Collapsed))
				{
					LabLaunchingDownload.Visibility = Visibility.Visible;
					LabLaunchingDownloadLeft.Visibility = Visibility.Visible;
					list.AddRange(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabLaunchingDownload, (double)(CS_8__locals11._VB_Local_HasLaunchDownloader ? 1 : 0) - LabLaunchingDownload.Opacity, 90),
						ModAnimation.AaOpacity(LabLaunchingDownloadLeft, (CS_8__locals11._VB_Local_HasLaunchDownloader ? 0.5 : 0.0) - LabLaunchingDownloadLeft.Opacity, 90),
						ModAnimation.AaCode([SpecialName] () =>
						{
							if (!CS_8__locals11._VB_Local_HasLaunchDownloader)
							{
								CS_8__locals11._VB_Me.LabLaunchingDownload.Visibility = Visibility.Collapsed;
								CS_8__locals11._VB_Me.LabLaunchingDownloadLeft.Visibility = Visibility.Collapsed;
							}
						}, 110)
					});
				}
				if (!flag == (LabLaunchingProgress.Visibility == Visibility.Collapsed))
				{
					LabLaunchingProgress.Visibility = Visibility.Visible;
					LabLaunchingProgressLeft.Visibility = Visibility.Visible;
					if (flag)
					{
						PanLaunchingHint.Visibility = Visibility.Visible;
					}
					list.AddRange(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabLaunchingProgress, (double)((!flag) ? 1 : 0) - LabLaunchingProgress.Opacity, 90),
						ModAnimation.AaOpacity(LabLaunchingProgressLeft, ((!flag) ? 0.5 : 0.0) - LabLaunchingProgressLeft.Opacity, 90),
						ModAnimation.AaOpacity(PanLaunchingHint, (double)(flag ? 1 : 0) - PanLaunchingHint.Opacity, 90)
					});
				}
				ModAnimation.AniStart(list, "Launching Progress");
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "刷新启动信息失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchLeft.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void PanLaunchingInfo_SizeChangedW(object sender, SizeChangedEventArgs e)
		{
			double value = e.NewSize.Width - e.PreviousSize.Width;
			if (e.PreviousSize.Width != 0.0 && !IsWidthAnimating && !(Math.Abs(value) < 1.0) && PanLaunchingInfo.ActualWidth != 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaWidth(PanLaunchingInfo, value, 180, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaCode([SpecialName] () =>
					{
						IsWidthAnimating = false;
						PanLaunchingInfo.Width = ActualUsedWidth;
					}, 0, After: true)
				}, "Launching Info Width");
				IsWidthAnimating = true;
				ActualUsedWidth = PanLaunchingInfo.Width;
				PanLaunchingInfo.Width = e.PreviousSize.Width;
			}
		}
		private void PanLaunchingInfo_SizeChangedH(object sender, SizeChangedEventArgs e)
		{
			double value = e.NewSize.Height - e.PreviousSize.Height;
			if (e.PreviousSize.Height != 0.0 && !IsHeightAnimating && !(Math.Abs(value) < 1.0) && PanLaunchingInfo.ActualHeight != 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaHeight(PanLaunchingInfo, value, 180, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaCode([SpecialName] () =>
					{
						IsHeightAnimating = false;
						PanLaunchingInfo.Height = ActualUsedHeight;
					}, 0, After: true)
				}, "Launching Info Height");
				IsHeightAnimating = true;
				ActualUsedHeight = PanLaunchingInfo.Height;
				PanLaunchingInfo.Height = e.PreviousSize.Height;
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnVersion_Click();
						};
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
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnMore_Click();
						};
						MyButton myButton = BtnMore;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnMore;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = RadioLoginType_Change;
						MyRadioButton myRadioButton = RadioLoginType5;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = RadioLoginType5;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = RadioLoginType_Change;
						MyRadioButton myRadioButton = RadioLoginType0;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = RadioLoginType0;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LaunchButtonClick();
						};
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							RefreshButtonsUI();
						};
						MyButton myButton = BtnLaunch;
						if (myButton != null)
						{
							myButton.Click -= obj;
							myButton.Loaded -= value2;
						}
						myButton = BtnLaunch;
						if (myButton != null)
						{
							myButton.Click += obj;
							myButton.Loaded += value2;
						}
			
			}
			{
						SizeChangedEventHandler value2 = PanLaunchingInfo_SizeChangedW;
						SizeChangedEventHandler value3 = PanLaunchingInfo_SizeChangedH;
						Grid grid = PanLaunchingInfo;
						if (grid != null)
						{
							grid.SizeChanged -= value2;
							grid.SizeChanged -= value3;
						}
						grid = PanLaunchingInfo;
						if (grid != null)
						{
							grid.SizeChanged += value2;
							grid.SizeChanged += value3;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnCancel_Click();
						};
						MyButton myButton = BtnCancel;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnCancel;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
