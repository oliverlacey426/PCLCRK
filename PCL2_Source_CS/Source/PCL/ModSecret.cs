// PCL

namespace PCL
{
	[StandardModule]
	internal sealed class ModSecret
	{
		[CompilerGenerated]
		internal sealed class _Closure___36_0
		{
			public bool _VB_Local_EffectSetup;
			public _Closure___36_0(_Closure___36_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_EffectSetup = arg0._VB_Local_EffectSetup;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				checked
				{
					try
					{
						int num = Settings.Get<int>("UiLauncherTheme");
						if (!ThemeCheckOne(num))
						{
							Logger.Warn($"检测到尚未解锁的主题：{num}，已重置为默认主题", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							Settings.Set("UiLauncherTheme", 0);
						}
						if (!_VB_Local_EffectSetup || ModMain.FrmSetupUI == null)
						{
							return;
						}
						List<int> list = new List<int>();
						int num2 = 0;
						do
						{
							if (ThemeCheckOne(num2))
							{
								list.Add(num2);
							}
							num2++;
						}
						while (num2 <= 14);
						list.Add(42);
						ModAnimation.AniControlEnabled++;
						if (list.Contains(5))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme5.Opacity = 1.0;
							ModMain.FrmSetupUI.LabLauncherTheme5Unlock.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme5Gray.Visibility = Visibility.Collapsed;
						}
						if (list.Contains(6))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme6.IsEnabled = true;
						}
						if (list.Contains(7))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme7.IsEnabled = true;
						}
						if (list.Contains(8))
						{
							ModMain.FrmSetupUI.LabLauncherTheme8Copy.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme8.IsEnabled = true;
						}
						if (list.Contains(9))
						{
							ModMain.FrmSetupUI.LabLauncherTheme9Copy.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme9.IsEnabled = true;
						}
						if (list.Contains(10))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme10.IsEnabled = true;
						}
						if (list.Contains(11))
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme11.IsEnabled = true;
						}
						else if (!Information.IsNothing(ModMain.FrmSetupUI) && !Information.IsNothing(ModMain.FrmSetupUI.RadioLauncherTheme11) && ModMain.FrmSetupUI.RadioLauncherTheme11.IsLoaded)
						{
							int num3 = 0;
							foreach (int item in list)
							{
								if (item >= 5 && item <= 14)
								{
									num3++;
								}
							}
							if (num3 < 3)
							{
								ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "需要解锁三个隐藏主题";
							}
							else
							{
								ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "点击打开解密游戏入口";
							}
						}
						if (list.Contains(12))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme12.IsEnabled = true;
						}
						if (list.Contains(13))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme13.IsEnabled = true;
						}
						if (list.Contains(14))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme14.IsEnabled = true;
						}
						ModAnimation.AniControlEnabled--;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if ((ex2.InnerException ?? ex2) is FormatException)
						{
							Logger.Error(ex2, "解锁的主题列表存档已损坏，主题解锁已被重置", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							Settings.Set("UiLauncherThemeHide2", "");
						}
						else
						{
							Logger.Error(ex2, "检查主题失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						}
						ProjectData.ClearProjectError();
					}
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___42_0
		{
			public IEnumerable<string> _VB_Local_UrlsOverride;
			public _Closure___42_0(_Closure___42_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_UrlsOverride = arg0._VB_Local_UrlsOverride;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___42_1
		{
			public List<string> _VB_Local_Urls;
			public string _VB_Local_Extension;
			public string _VB_Local_LocalPath;
			public ModLoader.LoaderCombo<string> _VB_Local_Loader;
			public _Closure___42_0 _VB_NonLocal__VB_Closure_2;
			public _Closure___42_1(_Closure___42_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Urls = arg0._VB_Local_Urls;
					_VB_Local_Extension = arg0._VB_Local_Extension;
					_VB_Local_LocalPath = arg0._VB_Local_LocalPath;
					_VB_Local_Loader = arg0._VB_Local_Loader;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				if (ServerLoader.State == ModBase.LoadState.Loading)
				{
					ServerLoader.WaitForExit();
				}
				if (_VB_NonLocal__VB_Closure_2._VB_Local_UrlsOverride != null)
				{
					_VB_Local_Urls = _VB_NonLocal__VB_Closure_2._VB_Local_UrlsOverride.ToList();
				}
				else if (ServerConfig?["Update"]?[ModBase.BuildTypes.Snapshot.ToString()] != null)
				{
					JObject jObject = (JObject)ServerConfig["Update"][ModBase.BuildTypes.Snapshot.ToString()];
					if (jObject.ContainsKey("PatchUrls") && UpdateMark.StartsWithF("Empty"))
					{
						_VB_Local_Urls = jObject["PatchUrls"].ToObject<List<string>>();
					}
					else if (jObject.ContainsKey("DownloadUrls"))
					{
						_VB_Local_Urls = jObject["DownloadUrls"].ToObject<List<string>>();
					}
				}
				if (_VB_Local_Urls == null || !_VB_Local_Urls.Any())
				{
					Logger.Error("更新失败：服务器没有提供有效的更新包下载链接，或无法连接到服务器！", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					IsUpdating = false;
					return;
				}
				_VB_Local_Urls = _VB_Local_Urls.Select([SpecialName] (string u) => ModMain.ArgumentReplace(u, WebUtility.HtmlEncode)).ToList();
				_VB_Local_Extension = "." + _VB_Local_Urls.First().BeforeLast("{CDN}").AfterLast(".");
				_VB_Local_LocalPath = string.Format("{0}{1}.{2}{3}", ModMain.RequestTaskTempFolder(), "2.13.1.1", 0, _VB_Local_Extension);
				Logger.Info(string.Format("更新开始，静默：{0}，扩展名：{1}，下载目标：{2}，URL：{3}", _STATIC_UpdateStart_0212151280E11E_IsSilentUpdate, _VB_Local_Extension, _VB_Local_LocalPath, _VB_Local_Urls.Join("，")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				Task.Output = new ModNet.NetFile[1]
				{
					new ModNet.NetFile(_VB_Local_Urls, _VB_Local_LocalPath)
				}.ToList();
			}
			[SpecialName]
			internal void _Lambda___R1(ModLoader.LoaderTask<string, int> a0)
			{
				_Lambda___2();
			}
			[SpecialName]
			internal void _Lambda___2()
			{
				string text = Paths.Base + "PCL\\Plain Craft Launcher 2.exe";
				if (FileUtils.Exists(text))
				{
					FileUtils.Delete(text);
					Logger.Info($"已清理已存在的更新文件：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
				checked
				{
					if (Operators.CompareString(_VB_Local_Extension, ".zip", TextCompare: false) == 0)
					{
						Logger.Info($"解压更新文件：{_VB_Local_LocalPath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						FileUtils.ExtractToDirectory(_VB_Local_LocalPath, Paths.Base + "PCL\\");
						byte[] array = FileUtils.ReadAsBytes(text);
						List<byte> list = new List<byte>();
						for (int i = array.Length - 1; i >= 0; i += -1)
						{
							byte b = (byte)(255 - array[i]);
							list.Add((byte)(b switch
							{
								byte.MaxValue => 0, 
								0 => 255, 
								_ => b, 
							}));
						}
						array = list.ToArray();
						FileUtils.Write(text, array);
					}
					else
					{
						Logger.Info($"应用补丁文件：{ModBase.PathExe} + {_VB_Local_LocalPath} → {Paths.Base}PCL\\Plain Craft Launcher 2.exe", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						MsDelta.Apply(ModBase.PathExe, _VB_Local_LocalPath, $"{Paths.Base}PCL\\Plain Craft Launcher 2.exe");
					}
					if (_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate)
					{
						IsUpdateWaitingRestart = true;
						return;
					}
					if (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
					{
						ModMain.Hint("更新已准备就绪，PCL 将在游戏启动完成后重启！", ModMain.HintType.Green);
						while (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
						{
							Thread.Sleep(10);
						}
					}
					UpdateRestart(TriggerRestartAndByEnd: true);
				}
			}
			[SpecialName]
			internal void _Lambda___R2(ModLoader.LoaderBase a0)
			{
				_Lambda___3();
			}
			[SpecialName]
			internal void _Lambda___3()
			{
				if (_VB_Local_Loader.State == ModBase.LoadState.Failed)
				{
					if (_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate)
					{
						Logger.Warn(_VB_Local_Loader.Error, "启动器静默更新失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
					else if (_VB_Local_Loader.Error.GetDisplay(multiline: false).Contains("(403)"))
					{
						ModMain.MyMsgBox("你的系统时间可能并不准确，导致下载验证未通过。\r\n请在校对、修改系统时间后再次尝试更新。", "启动器更新失败");
					}
					else if (_VB_Local_Loader.Error.GetDisplay(multiline: false).Contains("(404)"))
					{
						ModMain.MyMsgBox(string.Format("未找到更新包。可能的原因有：{0}- 你所使用的 PCL 不是官方版本{1}- 服务器异常", "\r\n", "\r\n"), "启动器更新失败");
					}
					else if (_VB_Local_Loader.Error is FileNotFoundException)
					{
						if (ModMain.MyMsgBox(string.Format("由于被 Windows 安全中心拦截，或者存在权限问题，导致 PCL 无法更新。{0}请将 PCL 所在文件夹加入白名单！", "\r\n"), "更新失败", "查看帮助", "确定", "", IsWarn: true) == 1)
						{
							CustomEvent.Raise(CustomEvent.EventType.打开帮助, "启动器/Microsoft Defender 添加排除项.json");
						}
					}
					else
					{
						Logger.Error(_VB_Local_Loader.Error, "启动器更新失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
				}
				if (_VB_Local_Loader.State == ModBase.LoadState.Finished || _VB_Local_Loader.State == ModBase.LoadState.Failed || _VB_Local_Loader.State == ModBase.LoadState.Canceled)
				{
					IsUpdating = false;
				}
			}
			[SpecialName]
			internal void _Lambda___4()
			{
				if (!_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate)
				{
					ModLoader.LoaderTaskbarAdd(_VB_Local_Loader);
					ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___45_0
		{
			public string _VB_Local_NewFilePath;
			public _Closure___45_0(_Closure___45_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_NewFilePath = arg0._VB_Local_NewFilePath;
				}
			}
			[SpecialName]
			internal string _Lambda___0(string Reason)
			{
				return string.Format("由于{0}，PCL 无法完成更新。{1}请依次尝试：{2}", Reason, "\r\n", "\r\n") + ((Paths.Base.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ignoreCase: true) || Paths.Base.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ignoreCase: true)) ? " - 将 PCL 文件移动到桌面、文档、C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "") + (Paths.Base.StartsWithF("C", ignoreCase: true) ? " - 将 PCL 文件移动到 C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "") + ((!WindowsUtils.HasAdminRole()) ? " - 右键 PCL，选择以管理员身份运行\r\n" : "") + string.Format(" - 手动剪切 PCL 文件夹中的新版本程序，覆盖原程序：{0}   将位于 {1}{2}   的程序{3}   剪切到 {4} 并覆盖即可。", "\r\n", ModBase.PathExe, "\r\n", "\r\n", _VB_Local_NewFilePath);
			}
		}
		public const string VersionBranchMain = "Official";
		public const string RegFolder = "PCL";
		public const string OAuthClientId = "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1";
		public const string ClsBaseUrl = "https://ap-shanghai.cls.tencentcs.com/track?topic_id=pcl-remote-1253424809";
		public static ModBase.MyColor Color1 = new ModBase.MyColor(52.0, 61.0, 74.0);
		public static ModBase.MyColor Color2 = new ModBase.MyColor(11.0, 91.0, 203.0);
		public static ModBase.MyColor Color3 = new ModBase.MyColor(19.0, 112.0, 243.0);
		public static ModBase.MyColor Color4 = new ModBase.MyColor(72.0, 144.0, 245.0);
		public static ModBase.MyColor Color5 = new ModBase.MyColor(150.0, 192.0, 249.0);
		public static ModBase.MyColor Color6 = new ModBase.MyColor(213.0, 230.0, 253.0);
		public static ModBase.MyColor Color7 = new ModBase.MyColor(222.0, 236.0, 253.0);
		public static ModBase.MyColor Color8 = new ModBase.MyColor(234.0, 242.0, 254.0);
		public static ModBase.MyColor ColorBg0 = new ModBase.MyColor(150.0, 192.0, 249.0);
		public static ModBase.MyColor ColorBg1 = new ModBase.MyColor(190.0, Color7);
		public static ModBase.MyColor ColorGray1 = new ModBase.MyColor(64.0, 64.0, 64.0);
		public static ModBase.MyColor ColorGray2 = new ModBase.MyColor(115.0, 115.0, 115.0);
		public static ModBase.MyColor ColorGray3 = new ModBase.MyColor(140.0, 140.0, 140.0);
		public static ModBase.MyColor ColorGray4 = new ModBase.MyColor(166.0, 166.0, 166.0);
		public static ModBase.MyColor ColorGray5 = new ModBase.MyColor(204.0, 204.0, 204.0);
		public static ModBase.MyColor ColorGray6 = new ModBase.MyColor(235.0, 235.0, 235.0);
		public static ModBase.MyColor ColorGray7 = new ModBase.MyColor(240.0, 240.0, 240.0);
		public static ModBase.MyColor ColorGray8 = new ModBase.MyColor(245.0, 245.0, 245.0);
		public static ModBase.MyColor ColorSemiTransparent = new ModBase.MyColor(1.0, Color8);
		public static int ThemeNow = -1;
		public static int ColorHue = 210;
		public static int ColorSat = 85;
		public static int ColorLightAdjust = 0;
		public static OneOf<int, int[]> ColorHueTopbarDelta = 0;
		public static int ThemeDontClick = 0;
		private static bool IsThemeColorfulLoaded = false;
		private static int ColorHueDelta = 1;
		private static bool IsUpdating = false;
		internal static bool IsUpdateWaitingRestart = false;
		private static bool IsUpdateRestartWaitingTasks = false;
		private static string UpdateMark = "EmptyMark********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";
		public static JObject ServerConfig;
		public static ModLoader.LoaderTask<int, int> ServerLoader = new ModLoader.LoaderTask<int, int>("PCL 配置更新", ServerSub, null, ThreadPriority.BelowNormal)
		{
			ReloadTimeout = 3600000
		};
		private static List<string> _PotatoFeatures = null;
		private const string EcdsaPublicKey = "RUNTMSAAAAC4QTUNAewh23Q4Q6koHkyIrDIIZUSbua23sf2DiZmIRwSzadISDRyTVTbuWniH3KR7rKj8XBsabms1be6i3c+S";
		[SpecialName]
		private static bool _STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init;
		[SpecialName]
		private static bool _STATIC_UpdateStart_0212151280E11E_IsSilentUpdate;
		public static DonationRank CurrentRank
		{
			get
			{
				DonationRank[] array = new DonationRank[5]
				{
					DonationRank.Rank98,
					DonationRank.Rank54,
					DonationRank.Rank23,
					DonationRank.Rank12,
					DonationRank.Rank6
				};
				for (int i = 0; i < array.Length; i = checked(i + 1))
				{
					DonationRank result = array[i];
					if (PotatoFeatures.Contains(result.ToString()))
					{
						return result;
					}
				}
				return DonationRank.None;
			}
		}
		private static bool HasUpdatePermission
		{
			get
			{
				if (CurrentRank < DonationRank.Rank12)
				{
					return PotatoFeatures.Contains("Update");
				}
				return true;
			}
		}
		private static List<string> PotatoFeatures
		{
			get
			{
				if (_PotatoFeatures == null)
				{
					_PotatoFeatures = new List<string>();
					string[] array = Settings.Get<string>("Potatoes").ToString().Split('|', removeEmptyEntries: true);
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						OneOf<List<string>, string> oneOf = CheckPotatoCode(array[i]);
						if (oneOf.Is<List<string>>())
						{
							_PotatoFeatures.AddRange(oneOf.As<List<string>>());
						}
					}
					_PotatoFeatures = _PotatoFeatures.Distinct().ToList();
				}
				return _PotatoFeatures;
			}
		}
		internal static string SecretCdnSign(string UrlWithMark)
		{
			if (ModBase.ApplicationStartTick < 1)
			{
				return UrlWithMark;
			}
			if (!UrlWithMark.EndsWithF("{CDN}"))
			{
				return UrlWithMark;
			}
			string text = UrlWithMark.Replace("{CDN}", "").Replace(" ", "%20");
			string text2 = ModBase.RandomInteger(0, 2147483645).ToString("x").EnsureLength('0', 8);
			string text3 = Conversions.ToString(ModBase.GetUnixTimestampUtc());
			string text4 = text.Substring(checked(text.IndexOfF("://") + 3));
			text4 = text4.Substring(text4.IndexOfF("/"));
			string text5 = CryptographyUtils.ComputeHash(new string[5] { text4, text3, text2, "0", "PCL2Server" }.Join("-"));
			return text + (text.Contains("?") ? "&" : "?") + "sign=" + new string[4] { text3, text2, "0", text5 }.Join("-");
		}
		internal static void SecretHeadersSign(string Url, ref HttpRequestMessage Req, bool SimulateBrowserHeaders = false)
		{
			if (ModBase.ApplicationStartTick < 1)
			{
				return;
			}
			if (!Req.Headers.UserAgent.Any())
			{
				if (Url.Contains("baidupcs.com") || Url.Contains("baidu.com"))
				{
					Req.Headers.Add("User-Agent", "LogStatistic");
				}
				else if (SimulateBrowserHeaders)
				{
					Req.Headers.Add("User-Agent", string.Format("PCL2/{0}.{1} Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36", "2.13.1.1", 0));
				}
				else
				{
					Req.Headers.Add("User-Agent", string.Format("PCL2/{0}.{1}", "2.13.1.1", 0));
				}
			}
			if (!SimulateBrowserHeaders)
			{
				Req.Headers.Referrer = new Uri($"http://{410}.pcl2.server/");
			}
			if (Url.Contains("api.curseforge.com") || Url.Contains("forgecdn.net"))
			{
				Req.Headers.Add("x-api-key", Encoding.Default.GetString(Convert.FromBase64String("JDJhJDEwJDdXV1YxV0VMY3dYOFhwN2Q2OE1Icy41Z3JUeVpWYTZTeW55ZVN5TWgxcnNFODluSWEwOXpP")));
			}
		}
		public static void ThemeRefresh(int NewTheme = -1)
		{
			checked
			{
				try
				{
					if ((ThemeNow == NewTheme && NewTheme >= 0) || !ThemeCheckOne((NewTheme >= 0) ? NewTheme : ThemeNow))
					{
						return;
					}
					if (NewTheme >= 0)
					{
						ThemeNow = NewTheme;
					}
					switch (ThemeNow)
					{
					case 10:
						ColorHue = 5;
						ColorSat = 70;
						ColorLightAdjust = -5;
						ColorHueTopbarDelta = 0;
						break;
					case 9:
						ColorHue = 25;
						ColorSat = 75;
						ColorLightAdjust = 5;
						ColorHueTopbarDelta = 0;
						break;
					case 4:
						ColorHue = 35;
						ColorSat = 30;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = 0;
						break;
					case 8:
						ColorHue = 50;
						ColorSat = 65;
						ColorLightAdjust = 10;
						ColorHueTopbarDelta = 0;
						break;
					case 3:
						ColorHue = 75;
						ColorSat = 50;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = 0;
						break;
					case 2:
						ColorHue = 130;
						ColorSat = 40;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = new int[3] { 5, -5, 5 };
						break;
					case 42:
						ColorHue = 225;
						ColorSat = 60;
						ColorLightAdjust = -15;
						ColorHueTopbarDelta = 0;
						break;
					case 1:
						ColorHue = 180;
						ColorSat = 50;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = 0;
						break;
					case 0:
						ColorHue = 210;
						ColorSat = 85;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = 0;
						break;
					case 5:
						ColorHue = 225;
						ColorSat = 10;
						ColorLightAdjust = -20;
						ColorHueTopbarDelta = 0;
						break;
					case 11:
						ColorHue = 230;
						ColorSat = 60;
						ColorLightAdjust = -5;
						ColorHueTopbarDelta = 0;
						break;
					case 7:
						ColorHue = 285;
						ColorSat = 50;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = 0;
						break;
					case 13:
						ColorHue = 290;
						ColorSat = 50;
						ColorLightAdjust = 0;
						ColorHueTopbarDelta = new int[3] { -30, 10, 50 };
						break;
					case 6:
						ColorHue = 345;
						ColorSat = 70;
						ColorLightAdjust = 15;
						ColorHueTopbarDelta = 0;
						break;
					case 12:
						if (!IsThemeColorfulLoaded)
						{
							IsThemeColorfulLoaded = true;
							ColorHue = ModBase.RandomInteger(0, 359);
							ColorHueDelta = (ModBase.RandomInteger(0, 1) * 2 - 1) * 4;
						}
						unchecked
						{
							ColorHue = checked(ColorHue + ColorHueDelta) % 360;
							ColorSat = 45;
							ColorLightAdjust = 0;
							break;
						}
					case 14:
						ColorHue = Settings.Get<int>("UiLauncherHue");
						ColorSat = Settings.Get<int>("UiLauncherSat");
						ColorLightAdjust = Settings.Get<int>("UiLauncherLight") - 20;
						ColorHueTopbarDelta = Settings.Get<int>("UiLauncherDelta") - 90;
						break;
					default:
						ColorHue = 210;
						ColorSat = 85;
						ColorLightAdjust = 0;
						break;
					}
					switch (ThemeDontClick)
					{
					case 1:
						ColorLightAdjust = 999;
						break;
					case 2:
						ColorHue = ModBase.RandomInteger(0, 359);
						ColorSat = ModBase.RandomInteger(40, 70);
						ColorLightAdjust = ModBase.RandomInteger(-20, 20);
						break;
					}
					Color1 = new ModBase.MyColor().FromHSL2(ColorHue, (double)ColorSat * 0.2, 25.0 + (double)ColorLightAdjust * 0.3);
					Color2 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 45 + ColorLightAdjust);
					Color3 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 55 + ColorLightAdjust);
					Color4 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 65 + ColorLightAdjust);
					Color5 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 80.0 + (double)ColorLightAdjust * 0.4);
					Color6 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 91.0 + (double)ColorLightAdjust * 0.1);
					Color7 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 95.0);
					Color8 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 97.0);
					ColorBg0 = Color4 * 0.4 + Color5 * 0.4 + ColorGray4 * 0.2;
					ColorBg1 = new ModBase.MyColor(190.0, Color7);
					ColorSemiTransparent = new ModBase.MyColor(1.0, Color8);
					System.Windows.Application.Current.Resources["ColorBrush1"] = new SolidColorBrush(Color1);
					System.Windows.Application.Current.Resources["ColorBrush2"] = new SolidColorBrush(Color2);
					System.Windows.Application.Current.Resources["ColorBrush3"] = new SolidColorBrush(Color3);
					System.Windows.Application.Current.Resources["ColorBrush4"] = new SolidColorBrush(Color4);
					System.Windows.Application.Current.Resources["ColorBrush5"] = new SolidColorBrush(Color5);
					System.Windows.Application.Current.Resources["ColorBrush6"] = new SolidColorBrush(Color6);
					System.Windows.Application.Current.Resources["ColorBrush7"] = new SolidColorBrush(Color7);
					System.Windows.Application.Current.Resources["ColorBrush8"] = new SolidColorBrush(Color8);
					System.Windows.Application.Current.Resources["ColorBrushBg0"] = new SolidColorBrush(ColorBg0);
					System.Windows.Application.Current.Resources["ColorBrushBg1"] = new SolidColorBrush(ColorBg1);
					System.Windows.Application.Current.Resources["ColorObject1"] = (System.Windows.Media.Color)Color1;
					System.Windows.Application.Current.Resources["ColorObject2"] = (System.Windows.Media.Color)Color2;
					System.Windows.Application.Current.Resources["ColorObject3"] = (System.Windows.Media.Color)Color3;
					System.Windows.Application.Current.Resources["ColorObject4"] = (System.Windows.Media.Color)Color4;
					System.Windows.Application.Current.Resources["ColorObject5"] = (System.Windows.Media.Color)Color5;
					System.Windows.Application.Current.Resources["ColorObject6"] = (System.Windows.Media.Color)Color6;
					System.Windows.Application.Current.Resources["ColorObject7"] = (System.Windows.Media.Color)Color7;
					System.Windows.Application.Current.Resources["ColorObject8"] = (System.Windows.Media.Color)Color8;
					System.Windows.Application.Current.Resources["ColorObjectBg0"] = (System.Windows.Media.Color)ColorBg0;
					System.Windows.Application.Current.Resources["ColorObjectBg1"] = (System.Windows.Media.Color)ColorBg1;
					ThemeRefreshMain();
					if (ThemeNow != 12 && ThemeNow != 14 && ThemeDontClick != 2)
					{
						Logger.Info($"刷新主题：{ThemeNow}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "刷新主题颜色失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		public static void ThemeRefreshMain()
		{
			ModBase.RunInUi(checked([SpecialName] () =>
			{
				if (ModMain.FrmMain.IsLoaded)
				{
					System.Windows.Media.LinearGradientBrush linearGradientBrush = new System.Windows.Media.LinearGradientBrush
					{
						EndPoint = new System.Windows.Point(1.0, 0.0),
						StartPoint = new System.Windows.Point(0.0, 0.0)
					};
					if (ThemeNow == 5)
					{
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 25.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.5,
							Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 15.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 1.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 25.0)
						});
						ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
						ModMain.FrmMain.PanTitle.Background.Freeze();
					}
					else if (ThemeNow != 12 && ThemeDontClick != 2)
					{
						int[] array = ColorHueTopbarDelta.Switch([SpecialName] (int d) => new int[3]
						{
							-d,
							0,
							d
						}, [SpecialName] (int[] d) => d);
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + array[0], ColorSat, 48 + ColorLightAdjust)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.5,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + array[1], ColorSat, 54 + ColorLightAdjust)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 1.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + array[2], ColorSat, 48 + ColorLightAdjust)
						});
						ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
						ModMain.FrmMain.PanTitle.Background.Freeze();
					}
					else
					{
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue - 21, ColorSat, 53 + ColorLightAdjust)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.33,
							Color = new ModBase.MyColor().FromHSL2(ColorHue - 7, ColorSat, 47 + ColorLightAdjust)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.67,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + 7, ColorSat, 47 + ColorLightAdjust)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 1.0,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + 21, ColorSat, 53 + ColorLightAdjust)
						});
						ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
					}
					if (ThemeNow >= 5 && ThemeNow != 14)
					{
						ModMain.FrmMain.ImgTitle.Source = $"{ModBase.PathImage}Themes/{ThemeNow}.png";
					}
					else
					{
						ModMain.FrmMain.ImgTitle.Source = null;
					}
					ModMain.FrmMain.ImgTitle.Opacity = ((ThemeNow == 13) ? 0.25 : 0.5);
					if (Settings.Get<bool>("UiBackgroundColorful"))
					{
						linearGradientBrush = new System.Windows.Media.LinearGradientBrush
						{
							EndPoint = new System.Windows.Point(0.1, 1.0),
							StartPoint = new System.Windows.Point(0.9, 0.0)
						};
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = -0.1,
							Color = new ModBase.MyColor().FromHSL2(ColorHue - 15, (double)ColorSat * 0.8, 91.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 0.4,
							Color = new ModBase.MyColor().FromHSL2(ColorHue, (double)ColorSat * 0.8, 91.0)
						});
						linearGradientBrush.GradientStops.Add(new GradientStop
						{
							Offset = 1.1,
							Color = new ModBase.MyColor().FromHSL2(ColorHue + 15, (double)ColorSat * 0.8, 91.0)
						});
						ModMain.FrmMain.PanForm.Background = linearGradientBrush;
					}
					else
					{
						ModMain.FrmMain.PanForm.Background = new ModBase.MyColor(245.0, 245.0, 245.0);
					}
					ModMain.FrmMain.PanForm.Background.Freeze();
				}
			}));
		}
		internal static void ThemeCheckAll(bool EffectSetup)
		{
			_Closure___36_0 arg = default;
			_Closure___36_0 CS_8__locals2 = new _Closure___36_0(arg);
			CS_8__locals2._VB_Local_EffectSetup = EffectSetup;
			ModBase.RunInUi(checked([SpecialName] () =>
			{
				try
				{
					int num = Settings.Get<int>("UiLauncherTheme");
					if (!ThemeCheckOne(num))
					{
						Logger.Warn($"检测到尚未解锁的主题：{num}，已重置为默认主题", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						Settings.Set("UiLauncherTheme", 0);
					}
					if (CS_8__locals2._VB_Local_EffectSetup && ModMain.FrmSetupUI != null)
					{
						List<int> list = new List<int>();
						int num2 = 0;
						do
						{
							if (ThemeCheckOne(num2))
							{
								list.Add(num2);
							}
							num2++;
						}
						while (num2 <= 14);
						list.Add(42);
						ModAnimation.AniControlEnabled++;
						if (list.Contains(5))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme5.Opacity = 1.0;
							ModMain.FrmSetupUI.LabLauncherTheme5Unlock.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme5Gray.Visibility = Visibility.Collapsed;
						}
						if (list.Contains(6))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme6.IsEnabled = true;
						}
						if (list.Contains(7))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme7.IsEnabled = true;
						}
						if (list.Contains(8))
						{
							ModMain.FrmSetupUI.LabLauncherTheme8Copy.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme8.IsEnabled = true;
						}
						if (list.Contains(9))
						{
							ModMain.FrmSetupUI.LabLauncherTheme9Copy.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme9.IsEnabled = true;
						}
						if (list.Contains(10))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme10.IsEnabled = true;
						}
						if (list.Contains(11))
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.Visibility = Visibility.Collapsed;
							ModMain.FrmSetupUI.RadioLauncherTheme11.IsEnabled = true;
						}
						else if (!Information.IsNothing(ModMain.FrmSetupUI) && !Information.IsNothing(ModMain.FrmSetupUI.RadioLauncherTheme11) && ModMain.FrmSetupUI.RadioLauncherTheme11.IsLoaded)
						{
							int num3 = 0;
							foreach (int item in list)
							{
								if (item >= 5 && item <= 14)
								{
									num3++;
								}
							}
							if (num3 < 3)
							{
								ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "需要解锁三个隐藏主题";
							}
							else
							{
								ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "点击打开解密游戏入口";
							}
						}
						if (list.Contains(12))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme12.IsEnabled = true;
						}
						if (list.Contains(13))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme13.IsEnabled = true;
						}
						if (list.Contains(14))
						{
							ModMain.FrmSetupUI.RadioLauncherTheme14.IsEnabled = true;
						}
						ModAnimation.AniControlEnabled--;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if ((ex2.InnerException ?? ex2) is FormatException)
					{
						Logger.Error(ex2, "解锁的主题列表存档已损坏，主题解锁已被重置", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						Settings.Set("UiLauncherThemeHide2", "");
					}
					else
					{
						Logger.Error(ex2, "检查主题失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
					ProjectData.ClearProjectError();
				}
			}));
		}
		internal static bool ThemeCheckOne(int Id)
		{
			if (new int[6] { 0, 1, 2, 3, 4, 42 }.Contains(Id))
			{
				return true;
			}
			if (PotatoFeatures.Contains("Theme" + Conversions.ToString(Id)))
			{
				return true;
			}
			switch (Id)
			{
			case 8:
				return CurrentRank >= DonationRank.Rank23;
			case 14:
				return Enumerable.Range(5, 9).Count(ThemeCheckOne) >= 5;
			default:
				if (Settings.Get<string>("UiLauncherThemeHide").Contains("7"))
				{
					ThemeUnlock(7, ShowDoubleHint: false);
				}
				return Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|")
					.Contains(Id.ToString());
			}
		}
		internal static bool ThemeUnlock(int Id, bool ShowDoubleHint = true, string UnlockHint = null)
		{
			if (ModMain.FrmMain == null)
			{
				return false;
			}
			if (Id == 8)
			{
				throw new Exception("不可使用此方式解锁秋仪金！");
			}
			List<string> list = new List<string>(Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|"));
			if (list.Contains(Id.ToString()))
			{
				if (ShowDoubleHint)
				{
					ModMain.Hint("该隐藏主题已被解锁！");
				}
				return false;
			}
			Logger.Info($"解锁隐藏主题：{Id}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (UnlockHint != null)
			{
				ModMain.Hint(UnlockHint, ModMain.HintType.Green, Log: false);
			}
			list.Add(Id.ToString());
			Settings.Set("UiLauncherThemeHide2", list.Join("|"));
			ThemeCheckAll(EffectSetup: true);
			return true;
		}
		internal static void UpdateCheckByButton()
		{
			if (_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init, ref lockTaken);
				if (_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init.State == 0)
				{
					_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init.State = 2;
					_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate = false;
				}
				else if (_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate_Init);
				}
			}
			if (_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate)
			{
				ModMain.Hint("正在检查更新，请稍候！");
				return;
			}
			_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate = true;
			ModBase.RunInThread([SpecialName] () =>
			{
				try
				{
					ModMain.Hint("正在检查更新，请稍候！");
					ServerLoader.WaitForExit(null, null, IsForceRestart: true);
					bool? flag = PageSetupSystem.IsLauncherNewest();
					if (!flag.HasValue)
					{
						ModMain.Hint("连接 PCL 服务器失败，请确认系统时间是否准确，尝试将 PCL 加入杀毒软件或防火墙白名单，然后重启 PCL！", ModMain.HintType.Red);
					}
					else if (flag == true)
					{
						ModMain.Hint(string.Format("已经是最新{0} {1}，不需要更新啦，可以直接使用！", "快照版", "2.13.1.1"), ModMain.HintType.Green);
					}
					else
					{
						if (!HasUpdatePermission)
						{
							InputPotatoCode(IsUpdating: true);
							if (!HasUpdatePermission)
							{
								return;
							}
						}
						else if (ModMain.MyMsgBox("发现启动器更新，是否立即下载？", "发现更新！", "更新！冲！", "取消") == 2)
						{
							return;
						}
						UpdateStart(Silent: false);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "尝试手动开始更新失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					ProjectData.ClearProjectError();
				}
				finally
				{
					_STATIC_UpdateCheckByButton_001_IsUiCheckingUpdate = false;
				}
			});
		}
		private static void UpdateStart(bool Silent, IEnumerable<string> UrlsOverride = null)
		{
			_Closure___42_0 obj = new _Closure___42_0(default(_Closure___42_0));
			obj._VB_Local_UrlsOverride = UrlsOverride;
			if (IsUpdating)
			{
				if (!Silent)
				{
					ModMain.Hint("PCL 正在下载更新，更新结束时将自动重启，请稍候！");
					_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate = false;
				}
				if (IsUpdateWaitingRestart)
				{
					UpdateRestart(TriggerRestartAndByEnd: true);
				}
				return;
			}
			IsUpdating = true;
			_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate = Silent;
			if (!_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate)
			{
				ModMain.Hint("PCL 正在下载更新，更新结束时将自动重启，请稍候！");
			}
			try
			{
				_Closure___42_1 arg = default;
				_Closure___42_1 CS_8__locals28 = new _Closure___42_1(arg);
				CS_8__locals28._VB_NonLocal__VB_Closure_2 = obj;
				CS_8__locals28._VB_Local_Urls = null;
				CS_8__locals28._VB_Local_Extension = null;
				CS_8__locals28._VB_Local_LocalPath = null;
				CS_8__locals28._VB_Local_Loader = new ModLoader.LoaderCombo<string>("启动器更新", new ModLoader.LoaderBase[3]
				{
					new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取更新信息", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
					{
						if (ServerLoader.State == ModBase.LoadState.Loading)
						{
							ServerLoader.WaitForExit();
						}
						if (CS_8__locals28._VB_NonLocal__VB_Closure_2._VB_Local_UrlsOverride != null)
						{
							CS_8__locals28._VB_Local_Urls = CS_8__locals28._VB_NonLocal__VB_Closure_2._VB_Local_UrlsOverride.ToList();
						}
						else if (ServerConfig?["Update"]?[ModBase.BuildTypes.Snapshot.ToString()] != null)
						{
							JObject jObject = (JObject)ServerConfig["Update"][ModBase.BuildTypes.Snapshot.ToString()];
							if (jObject.ContainsKey("PatchUrls") && UpdateMark.StartsWithF("Empty"))
							{
								CS_8__locals28._VB_Local_Urls = jObject["PatchUrls"].ToObject<List<string>>();
							}
							else if (jObject.ContainsKey("DownloadUrls"))
							{
								CS_8__locals28._VB_Local_Urls = jObject["DownloadUrls"].ToObject<List<string>>();
							}
						}
						if (CS_8__locals28._VB_Local_Urls == null || !CS_8__locals28._VB_Local_Urls.Any())
						{
							Logger.Error("更新失败：服务器没有提供有效的更新包下载链接，或无法连接到服务器！", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							IsUpdating = false;
						}
						else
						{
							CS_8__locals28._VB_Local_Urls = CS_8__locals28._VB_Local_Urls.Select([SpecialName] (string u) => ModMain.ArgumentReplace(u, WebUtility.HtmlEncode)).ToList();
							CS_8__locals28._VB_Local_Extension = "." + CS_8__locals28._VB_Local_Urls.First().BeforeLast("{CDN}").AfterLast(".");
							CS_8__locals28._VB_Local_LocalPath = string.Format("{0}{1}.{2}{3}", ModMain.RequestTaskTempFolder(), "2.13.1.1", 0, CS_8__locals28._VB_Local_Extension);
							Logger.Info(string.Format("更新开始，静默：{0}，扩展名：{1}，下载目标：{2}，URL：{3}", _STATIC_UpdateStart_0212151280E11E_IsSilentUpdate, CS_8__locals28._VB_Local_Extension, CS_8__locals28._VB_Local_LocalPath, CS_8__locals28._VB_Local_Urls.Join("，")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							Task.Output = new ModNet.NetFile[1]
							{
								new ModNet.NetFile(CS_8__locals28._VB_Local_Urls, CS_8__locals28._VB_Local_LocalPath)
							}.ToList();
						}
					})
					{
						ProgressWeight = 0.1
					},
					new ModNet.LoaderDownload("下载更新文件", new List<ModNet.NetFile>())
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<string, int>("安装更新", [SpecialName] (ModLoader.LoaderTask<string, int> a0) =>
					{
						CS_8__locals28._Lambda___2();
					})
					{
						ProgressWeight = 0.1
					}
				});
				CS_8__locals28._VB_Local_Loader.OnStateChanged = [SpecialName] (ModLoader.LoaderBase a0) =>
				{
					CS_8__locals28._Lambda___3();
				};
				CS_8__locals28._VB_Local_Loader.Start();
				ModBase.RunInUi([SpecialName] () =>
				{
					if (!_STATIC_UpdateStart_0212151280E11E_IsSilentUpdate)
					{
						ModLoader.LoaderTaskbarAdd(CS_8__locals28._VB_Local_Loader);
						ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
					}
				});
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始启动器更新失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				IsUpdating = false;
				ProjectData.ClearProjectError();
			}
		}
		public static void UpdateRestart(bool TriggerRestartAndByEnd)
		{
			try
			{
				IsUpdateWaitingRestart = false;
				string fileName = Paths.Base + "PCL\\Plain Craft Launcher 2.exe";
				string text = $"--update {Process.GetCurrentProcess().Id} \"{AppDomain.CurrentDomain.SetupInformation.ApplicationName}\" \"{AppDomain.CurrentDomain.SetupInformation.ApplicationName}\" {TriggerRestartAndByEnd}";
				Logger.Info($"更新程序启动，参数：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				ModBase.StartProcess(new ProcessStartInfo(fileName)
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					Arguments = text
				});
				if (TriggerRestartAndByEnd)
				{
					ModMain.FrmMain.EndProgram(SendWarning: false);
					Logger.Info("已由于更新强制结束程序", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
			}
			catch (Win32Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Win32Exception ex2 = ex;
				Logger.Warn(ex2, "自动更新时触发 Win32 错误，疑似被拦截", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				if (ModMain.MyMsgBox(string.Format("由于被 Windows 安全中心拦截，或者存在权限问题，导致 PCL 无法更新。{0}请将 PCL 所在文件夹加入白名单，或者手动用 {1}PCL\\Plain Craft Launcher 2.exe 替换当前文件！", "\r\n", Paths.Base), "更新失败", "查看帮助", "确定", "", IsWarn: true) == 1)
				{
					CustomEvent.Raise(CustomEvent.EventType.打开帮助, "启动器/Microsoft Defender 添加排除项.json");
				}
				ProjectData.ClearProjectError();
			}
		}
		public static void UpdateReplace(int ProcessId, string OldFileName, string NewFileName, bool TriggerRestart)
		{
			_Closure___45_0 arg = default;
			_Closure___45_0 CS_8__locals7 = new _Closure___45_0(arg);
			try
			{
				Process.GetProcessById(ProcessId).Kill();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			checked
			{
				string filePath = Paths.Base.Substring(0, Paths.Base.Length - 4) + PathUtils.GetLastPart(OldFileName);
				CS_8__locals7._VB_Local_NewFilePath = Paths.Base.Substring(0, Paths.Base.Length - 4) + PathUtils.GetLastPart(NewFileName);
				Exception ex = null;
				int num = 0;
				do
				{
					try
					{
						FileUtils.Delete(filePath);
						FileUtils.Delete(CS_8__locals7._VB_Local_NewFilePath);
						if (!FileUtils.Exists(filePath) && !FileUtils.Exists(CS_8__locals7._VB_Local_NewFilePath))
						{
							break;
						}
					}
					catch (Exception ex2)
					{
						ProjectData.SetProjectError(ex2);
						Exception ex3 = ex2;
						ex = ex3;
						ProjectData.ClearProjectError();
					}
					Thread.Sleep(num * 500 + 500);
					num++;
				}
				while (num <= 4);
				VB_AnonymousDelegate_11<string, string> obj = [SpecialName] (string Reason) => string.Format("由于{0}，PCL 无法完成更新。{1}请依次尝试：{2}", Reason, "\r\n", "\r\n") + ((Paths.Base.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ignoreCase: true) || Paths.Base.StartsWithF(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ignoreCase: true)) ? " - 将 PCL 文件移动到桌面、文档、C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "") + (Paths.Base.StartsWithF("C", ignoreCase: true) ? " - 将 PCL 文件移动到 C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "") + ((!WindowsUtils.HasAdminRole()) ? " - 右键 PCL，选择以管理员身份运行\r\n" : "") + string.Format(" - 手动剪切 PCL 文件夹中的新版本程序，覆盖原程序：{0}   将位于 {1}{2}   的程序{3}   剪切到 {4} 并覆盖即可。", "\r\n", ModBase.PathExe, "\r\n", "\r\n", CS_8__locals7._VB_Local_NewFilePath);
				if (FileUtils.Exists(filePath) || FileUtils.Exists(CS_8__locals7._VB_Local_NewFilePath))
				{
					if (ex is UnauthorizedAccessException)
					{
						Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", obj("删除老版本文件时权限不足"), "\r\n", "\r\n", "\r\n", ex.GetDisplay(multiline: true)), MsgBoxStyle.Critical, "更新失败");
					}
					else
					{
						Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", obj("删除老版本文件时出错"), "\r\n", "\r\n", "\r\n", ex.GetDisplay(multiline: true)), MsgBoxStyle.Critical, "更新失败");
					}
					return;
				}
				try
				{
					FileUtils.Copy(ModBase.PathExe, CS_8__locals7._VB_Local_NewFilePath);
				}
				catch (UnauthorizedAccessException ex4)
				{
					ProjectData.SetProjectError(ex4);
					UnauthorizedAccessException ex5 = ex4;
					Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", obj("复制新版本文件时权限不足"), "\r\n", "\r\n", "\r\n", ex5.GetDisplay(multiline: true)), MsgBoxStyle.Critical, "更新失败");
					ProjectData.ClearProjectError();
				}
				catch (Exception ex6)
				{
					ProjectData.SetProjectError(ex6);
					Exception ex7 = ex6;
					Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", obj("复制新版本文件时出错"), "\r\n", "\r\n", "\r\n", ex7.GetDisplay(multiline: true)), MsgBoxStyle.Critical, "更新失败");
					ProjectData.ClearProjectError();
					return;
				}
				if (!TriggerRestart)
				{
					return;
				}
				try
				{
					ModBase.StartProcess(CS_8__locals7._VB_Local_NewFilePath);
				}
				catch (Exception ex8)
				{
					ProjectData.SetProjectError(ex8);
					Exception ex9 = ex8;
					Interaction.MsgBox(string.Format("PCL 更新已完成，但在自动重启 PCL 时遇到了意外，请手动启动 PCL。{0}{1}详细错误信息：{2}{3}", "\r\n", "\r\n", "\r\n", ex9.GetDisplay(multiline: true)), MsgBoxStyle.Information, "更新完成");
					ProjectData.ClearProjectError();
				}
			}
		}
		internal static void DownloadLatestPCL(ModLoader.LoaderBase LoaderToSyncProgress = null)
		{
			int num = (FileUtils.Exists(ModBase.PathTemp + "Latest.exe") ? Settings.Get<int>("SystemHighestSavedBetaVersionReg") : 0);
			int num2 = 0;
			string fullStr = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\Notice.cfg") ?? "";
			if (fullStr.Split("|").Count() >= 3)
			{
				num2 = Conversions.ToInteger(fullStr.Split("|")[2]);
			}
			if (num < num2)
			{
				Logger.Info($"需要下载最新正式版：{num} -> {num2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				if (ServerConfig == null)
				{
					ServerLoader.WaitForExit(null, null, ServerLoader.State == ModBase.LoadState.Finished);
				}
				if (ServerConfig == null)
				{
					throw new Exception("无法从服务器获取正式版 PCL 下载链接！");
				}
				ModNet.NetDownloadByLoader(ServerConfig["Update"]["Release"]["DownloadUrls"].Select([SpecialName] (JToken t) => t.ToString()).ToList(), ModBase.PathTemp + "Latest.zip", LoaderToSyncProgress, new FileChecker
				{
					MinSize = 1048576L
				});
				FileUtils.ExtractToDirectory(ModBase.PathTemp + "Latest.zip", ModBase.PathTemp);
				FileUtils.Delete(ModBase.PathTemp + "Latest.zip");
				FileUtils.Move(ModBase.PathTemp + "Plain Craft Launcher 2.exe", ModBase.PathTemp + "Latest.exe");
				Settings.Set("SystemHighestSavedBetaVersionReg", num2);
			}
		}
		private static void ServerSub(ModLoader.LoaderTask<int, int> Loader)
		{
			try
			{
				if (FileUtils.Exists(Paths.Base + "PCL\\update.exe"))
				{
					FileUtils.Delete(Paths.Base + "PCL\\update.exe");
					Logger.Info("已清理更新缓存", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "清理更新缓存失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				ProjectData.ClearProjectError();
			}
			int num = 0;
			string text = ModBase.PathTemp + "Cache\\ServerConfig.json";
			checked
			{
				try
				{
					if (FileUtils.Exists(text))
					{
						ServerConfig = (JObject)FileUtils.ReadAsJson(text);
						num = (int)Math.Round(ModBase.Val(ServerConfig["Version"]));
						Logger.Info($"已读取本地缓存的配置：{num}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "读取配置信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					ServerConfig = null;
					FileUtils.Delete(text);
					ProjectData.ClearProjectError();
				}
				Loader.Progress = 0.2;
				int num2 = Settings.Get<int>("HintNotice");
				int num3 = Settings.Get<int>("HintDownload");
				string filePath = ModBase.PathTemp + "Cache\\Notice.cfg";
				string text2;
				int num4;
				int num5;
				try
				{
					text2 = ModNet.NetRequestByClientRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.cfg{CDN}");
					num4 = (int)Math.Round(ModBase.Val(text2.Split("|")[0]));
					num5 = (int)Math.Round(ModBase.Val(text2.Split("|")[3]));
					if (num4 == 0)
					{
						throw new Exception("获取到的内容有误！（" + text2 + "）");
					}
					if (num4 > num2)
					{
						Logger.Info($"本地公告编号：{num2}，服务器公告：{text2}，需更新", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
					else
					{
						Logger.Info($"本地公告编号：{num2}，服务器公告：{text2}，无需更新", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
					FileUtils.Write(filePath, text2);
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					if (ex6 is InvalidOperationException && ex6.Message.Contains("FIPS"))
					{
						if (ModMain.MyMsgBox("由于系统未启用 FIPS 兼容算法，PCL 可能无法正常运行。\r\n请按照教程启用该功能，然后重启 PCL。", "兼容性警告", "打开教程", "取消", "", IsWarn: true) == 1)
						{
							ModBase.OpenWebsite("https://blog.csdn.net/qq_37608398/article/details/81209922");
						}
					}
					else
					{
						Logger.Warn(ex6, "获取 PCL 服务器状态失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						FileUtils.Delete(filePath);
					}
					ProjectData.ClearProjectError();
					return;
				}
				Loader.Progress = 0.4;
				try
				{
					double num6 = ModBase.Val(text2.Split("|")[4]);
					if ((double)num < num6)
					{
						Logger.Info($"本地配置版本号：{num}，服务器配置版本号：{num6}，需要更新", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						ModNet.NetDownloadByClient("https://pcl2-server-1253424809.file.myqcloud.com/ServerConfig.json{CDN}", text);
						ServerConfig = (JObject)FileUtils.ReadAsJson(text);
					}
				}
				catch (Exception ex7)
				{
					ProjectData.SetProjectError(ex7);
					Exception ex8 = ex7;
					Logger.Warn(ex8, "下载配置信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					ProjectData.ClearProjectError();
				}
				Loader.Progress = 0.6;
				try
				{
					if (num5 > num3 || !FileUtils.Exists(ModBase.PathTemp + "Cache\\download.json"))
					{
						string text3 = ModNet.NetRequestByClientRetry("https://pcl2-server-1253424809.file.myqcloud.com/minecraft/download.json{CDN}", null, null, null, null, "*/*", null, RequireJson: true);
						FileUtils.Write(ModBase.PathTemp + "Cache\\download.json", text3);
					}
					Settings.Set("HintDownload", num5);
				}
				catch (Exception ex9)
				{
					ProjectData.SetProjectError(ex9);
					Exception ex10 = ex9;
					Logger.Warn(ex10, "下载 PCL 特供版信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					FileUtils.Delete(ModBase.PathTemp + "Cache\\download.json");
					ProjectData.ClearProjectError();
				}
				Loader.Progress = 0.8;
				string text4;
				try
				{
					if (num4 <= num2 && FileUtils.Exists(ModBase.PathTemp + "Cache\\Notice.json"))
					{
						text4 = FileUtils.ReadAsString(ModBase.PathTemp + "Cache\\Notice.json");
					}
					else
					{
						text4 = ModNet.NetRequestByClientRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.json{CDN}", null, null, null, null, "*/*", null, RequireJson: true);
						FileUtils.Write(ModBase.PathTemp + "Cache\\Notice.json", text4);
					}
					Settings.Set("HintNotice", num4);
				}
				catch (Exception ex11)
				{
					ProjectData.SetProjectError(ex11);
					Exception ex12 = ex11;
					Logger.Warn(ex12, "下载 PCL 服务器公告失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					FileUtils.Delete(ModBase.PathTemp + "Cache\\Notice.json");
					ProjectData.ClearProjectError();
					return;
				}
				Loader.Progress = 0.95;
				try
				{
					foreach (JObject item in (IEnumerable<JToken>)text4.Replace("{UNIQUE}", ModBase.Identify).DeserializeJson())
					{
						int num7 = (int)Math.Round(ModBase.Val(item["id"] ?? ((JToken)0)));
						if (num7 <= num2)
						{
							continue;
						}
						bool flag = true;
						foreach (JProperty item2 in (IEnumerable<JToken>)(item["requirements"] ?? ((JToken)new byte[0])))
						{
							if (!ServerRequirement(item2.Name, item2.Value))
							{
								flag = false;
							}
						}
						if (!flag)
						{
							continue;
						}
						int num8 = (int)(item["importantLevel"] ?? ((JToken)2));
						bool flag2 = (bool)(item["isUpdate"] ?? ((JToken)false));
						Logger.Info($"重要等级 {num8}，更新公告 {flag2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						if (Settings.Get<int>(flag2 ? "SystemSystemUpdate" : "SystemSystemActivity") > num8 || (flag2 && IsUpdating))
						{
							continue;
						}
						string title = (item["title"] ?? ((JToken)"公告")).ToString();
						string text5 = (item["description"] ?? ((JToken)"")).ToString().Replace("\\n", "\r\n");
						JArray jArray = (JArray)(item["buttons"] ?? new JArray());
						int count = jArray.Count;
						object obj = new object[0];
						if (count > 0)
						{
							Logger.Info($"显示公告 {num7}：{text5}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							int value;
							switch (count)
							{
							case 1:
								value = ModMain.MyMsgBox(text5, title, (jArray[0]["text"] ?? ((JToken)"确定")).ToString(), "", "", IsWarn: false, HighLight: true, ForceWait: true);
								break;
							case 2:
								value = ModMain.MyMsgBox(text5, title, (jArray[0]["text"] ?? ((JToken)"确定")).ToString(), (jArray[1]["text"] ?? ((JToken)"确定")).ToString());
								break;
							case 3:
								value = ModMain.MyMsgBox(text5, title, (jArray[0]["text"] ?? ((JToken)"确定")).ToString(), (jArray[1]["text"] ?? ((JToken)"确定")).ToString(), (jArray[2]["text"] ?? ((JToken)"确定")).ToString());
								break;
							default:
								Logger.Warn($"公告 {num7} 的弹窗有 {count} 个按钮，无法显示", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
								continue;
							}
							value = value.Clamp(1, count);
							obj = jArray[value - 1]["actions"] ?? ((JToken)new byte[0]);
						}
						else
						{
							obj = item["actions"] ?? ((JToken)new byte[0]);
						}
						foreach (JProperty item3 in (IEnumerable)obj)
						{
							try
							{
								switch (item3.Name)
								{
								case "copy":
									ModBase.ClipboardSet(item3.Value.ToString());
									break;
								case "website":
									ModBase.OpenWebsite(item3.Value.ToString());
									break;
								case "stop":
									ModMain.FrmMain.EndProgram(SendWarning: false);
									break;
								case "setup":
								{
									JObject jObject2 = (JObject)item3.Value;
									foreach (KeyValuePair<string, JToken> item4 in jObject2)
									{
										Settings.Set(item4.Key, item4.Value.ToString());
									}
									break;
								}
								case "update":
								case "slientupdate":
								{
									_ = (JObject)item3.Value;
									bool flag3 = Operators.CompareString(item3.Name, "slientupdate", TextCompare: false) == 0;
									if (!HasUpdatePermission)
									{
										if (!flag3)
										{
											InputPotatoCode(IsUpdating: true);
										}
										if (!HasUpdatePermission)
										{
											break;
										}
									}
									UpdateStart(flag3);
									break;
								}
								default:
									throw new Exception("未知的行动支：" + item3.Name + ", " + item3.Value.ToString());
								}
							}
							catch (Exception ex13)
							{
								ProjectData.SetProjectError(ex13);
								Exception ex14 = ex13;
								Logger.Error(ex14, $"执行 PCL 服务器公告动作失败（{item3.Name}, {item3.Value}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
								ProjectData.ClearProjectError();
							}
						}
					}
				}
				catch (Exception ex15)
				{
					ProjectData.SetProjectError(ex15);
					Exception ex16 = ex15;
					Logger.Warn(text4, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					Logger.Error(ex16, $"读取 PCL 服务器公告失败（{text4.Length}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					FileUtils.Delete(ModBase.PathTemp + "Cache\\Notice.json");
					ProjectData.ClearProjectError();
				}
			}
		}
		private static bool ServerRequirement(string Name, JToken Value)
		{
			checked
			{
				bool result;
				try
				{
					switch (Name)
					{
					case "d10000 <=":
						result = (double)ModBase.RandomInteger(1, 10000) <= ModBase.Val(Value);
						break;
					case "opencount <=":
						result = (double)Settings.Get<int>("SystemCount") <= ModBase.Val(Value);
						break;
					case "opencount >=":
						result = (double)Settings.Get<int>("SystemCount") >= ModBase.Val(Value);
						break;
					case "versioncode <=":
						result = 410.0 <= ModBase.Val(Value);
						break;
					case "versioncode >=":
						result = 410.0 >= ModBase.Val(Value);
						break;
					case "versionbranch <=":
						result = ModBase.Val(0) <= ModBase.Val(Value);
						break;
					case "versionbranch >=":
						result = ModBase.Val(0) >= ModBase.Val(Value);
						break;
					case "uniqueaddress =":
						result = Operators.CompareString(ModBase.Identify, Value.ToString(), TextCompare: false) == 0;
						break;
					case "unlockedtheme =":
						result = ThemeCheckOne((int)Math.Round(ModBase.Val(Value)));
						break;
					case "unlockedtheme !=":
						result = !ThemeCheckOne((int)Math.Round(ModBase.Val(Value)));
						break;
					case "setupinteger >=":
						result = (double)Settings.Get<int>(((JProperty)Value).Name) >= ModBase.Val(((JProperty)Value).Value);
						break;
					case "setupinteger <=":
						result = (double)Settings.Get<int>(((JProperty)Value).Name) <= ModBase.Val(((JProperty)Value).Value);
						break;
					case "setupboolean =":
						result = Settings.Get<bool>(((JProperty)Value).Name) == ((JProperty)Value).Value.ToObject<bool>();
						break;
					case "debug =":
						result = Operators.CompareString(ModBase.ModeDebug.ToString().Lower(), Value.ToString().Lower(), TextCompare: false) == 0;
						break;
					default:
						Logger.Warn($"未知的条件支：{Name}, {Value}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						result = false;
						break;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn($"判断分支条件失败：{Name}, {Value}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					result = false;
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}
		private static OneOf<List<string>, string> CheckPotatoCode(string Key)
		{
			OneOf<List<string>, string> result;
			if (string.IsNullOrEmpty(Key))
			{
				result = "输入的土豆码为空。";
			}
			else if (Key.Length < 30 && Key.ContainsIgnoreCase(ModBase.Identify))
			{
				result = string.Format("你输入的是识别码，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆 {1}】。", "\r\n", ModBase.Identify);
			}
			else if (Key.Length > 85 && Key.Length < 90 && Key.EndsWithF("="))
			{
				result = string.Format("你输入的是解锁码，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆】。", "\r\n");
			}
			else if (Key.Length > 150 && Key.Length < 165 && Key.EndsWithF("11"))
			{
				result = string.Format("你输入的是更新密钥，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆】。", "\r\n");
			}
			else if (!Key.StartsWithF("CD", ignoreCase: true) || !Key.Contains("#"))
			{
				result = string.Format("你输入的不是土豆码……有一说一，这是啥玩意儿？{0}要获取土豆码，请在爱发电私信发送【土豆 {1}】。", "\r\n", ModBase.Identify);
			}
			else if (Key[2] > '1')
			{
				result = string.Format("这个土豆码得在新版 PCL 上才能使用。{0}请更新 PCL，或是在爱发电私信发送【下载】，然后在新下载的 PCL 上输入土豆码。", "\r\n");
			}
			else if ('1' > Key[3])
			{
				result = string.Format("这个土豆码不太新鲜，已经过期了。{0}请在爱发电私信重新发送【土豆 {1}】，获取更新鲜的土豆码。", "\r\n", ModBase.Identify);
			}
			else if (!Key.StartsWithF("CD"))
			{
				result = string.Format("土豆码区分大小写，但你把一些大写的东西输成小写的了。{0}你可以选中文本，然后右键复制粘贴，不必手动输入。", "\r\n");
			}
			else
			{
				try
				{
					string[] array = Key.Substring(4).Replace("p", "=").Replace("%", "p")
						.Split('#');
					string text = Encoding.UTF8.GetString(Convert.FromBase64String(array[1]));
					CryptographyUtils.EcdsaVerify(ModBase.Identify + Conversions.ToString(Key[2]) + Conversions.ToString(Key[3]) + text, array[0], "RUNTMSAAAAC4QTUNAewh23Q4Q6koHkyIrDIIZUSbua23sf2DiZmIRwSzadISDRyTVTbuWniH3KR7rKj8XBsabms1be6i3c+S");
					result = text.Split(',', removeEmptyEntries: true).ToList();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"土豆码校验失败（{Key}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					result = string.Format("土豆码有误，请检查：{0}- 私信显示的识别码应当为 {1}，如果不是，请在私信发送【土豆 {2}】{3}- 你输入的以下土豆码是否与私信中的一致：{4}{5}{6}{7}错误原因：{8}", "\r\n", ModBase.Identify, ModBase.Identify, "\r\n", "\r\n", Key, "\r\n", "\r\n", ex2.Message);
					ProjectData.ClearProjectError();
				}
			}
			return result;
		}
		public static void InputPotatoCode(bool IsUpdating)
		{
			string text;
			if (IsUpdating)
			{
				if (ModMain.MyMsgBox(string.Format("请点击【复制命令】，然后将复制的命令粘贴到爱发电私信，获取土豆码。{0}输入土豆码后，就能继续更新啦！", "\r\n"), "验证爱发电赞助", "输入土豆码", "复制命令", "取消", IsWarn: false, HighLight: true, ForceWait: false, null, [SpecialName] () =>
				{
					ModBase.ClipboardSet("土豆 " + ModBase.Identify, ShowSuccessHint: false);
					ModBase.OpenWebsite("https://meloong.com/afd/message/df2dfad2960911e89c5252540025c377?potato");
				}) == 3)
				{
					return;
				}
				text = ModMain.MyMsgBoxInput("输入土豆码", string.Format("请在【爱发电】向龙腾猫跃发送【土豆 {0}】，然后输入自动回复的土豆码以继续更新。{1}自动回复可能比较慢，发送后等一会儿刷新一下网页应该就有了。", ModBase.Identify, "\r\n"), null, new Collection<ValidateBase>
				{
					new ValidateNullOrWhiteSpace()
				});
			}
			else
			{
				text = ModMain.MyMsgBoxInput("输入土豆码", string.Format("请输入你获得的土豆码。{0}你的识别码：{1}", "\r\n", ModBase.Identify), null, new Collection<ValidateBase>
				{
					new ValidateNullOrWhiteSpace()
				});
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			text = text.ReplaceLineEndings("").Between("：", "在").Trim(' ', '【', '】');
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			Logger.Info($"输入的土豆码：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (!IsUpdating && checked(Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 4), TextCompare: false) == 0 || Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 5), TextCompare: false) == 0 || Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 6), TextCompare: false) == 0))
			{
				ModMain.MyMsgBox(string.Format("隐藏主题 极客蓝 已解锁！{0}难道，这就是终点了？还是说……？", "\r\n"), "解锁新隐藏主题！");
				ThemeUnlock(11);
				return;
			}
			try
			{
				OneOf<List<string>, string> oneOf = CheckPotatoCode(text);
				if (oneOf.Is<string>())
				{
					throw new Exception(oneOf.As<string>());
				}
				List<string> list = oneOf.As<List<string>>();
				Logger.Info(string.Format("土豆码包含的特性：{0}", list.Join(", ")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				if (list.Any([SpecialName] (string f) => !PotatoFeatures.Contains(f)))
				{
					List<string> list2 = Settings.Get<string>("Potatoes").ToString().Split('|', removeEmptyEntries: true)
						.ToList();
					list2.Add(text);
					Settings.Set("Potatoes", list2.Join("|"));
					_PotatoFeatures = null;
				}
				if (IsUpdating)
				{
					if (!HasUpdatePermission)
					{
						throw new Exception("这个土豆码里不包含更新权限……");
					}
				}
				else
				{
					ModMain.Hint("已成功解锁！", ModMain.HintType.Green);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Info($"土豆码无效：{ex2.Message}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				if (ModMain.MyMsgBox(ex2.Message, "错误", "重新输入", "取消", "", IsWarn: true) == 1)
				{
					InputPotatoCode(IsUpdating);
				}
				ProjectData.ClearProjectError();
			}
		}
		internal static void GeneratePotatoCode()
		{
		}
		internal static string GetIdentify()
		{
			string result;
			try
			{
				if (ModBase.ApplicationStartTick < 1)
				{
					result = "0000-0000-0000-0000";
				}
				else
				{
					string text;
					try
					{
						text = RegistryUtils.TryRead(string.Format("HKEY_LOCAL_MACHINE\\SYSTEM\\Har{0}wareConfig", "D".Lower()), "LastConfig", "Unknown").ToString().Upper()
							.Trim('{', '}');
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "获取主板标识码失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						text = "Unknown";
						ProjectData.ClearProjectError();
					}
					string text2;
					try
					{
						text2 = Settings.Get<string>("Identify");
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						text2 = "";
						ProjectData.ClearProjectError();
					}
					if (text2.Length < 3)
					{
						text2 = Conversions.ToString(ModBase.GetTimeMs()) + Conversions.ToString(MyWpfExtension.Computer.Info.AvailablePhysicalMemory);
						Settings.Set("Identify", text2);
					}
					string text3 = (text + text2).GetStableHashCode().ToString("X").EnsureLength('7', 16);
					result = $"{text3.Substring(4, 4)}-{text3.Substring(12, 4)}-{text3.Substring(0, 4)}-{text3.Substring(8, 4)}";
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, string.Format("PCL 无法获取设备识别码，这可能会导致部分设置无法正常存储。{0}{1}详细的错误信息", "\r\n", "\r\n"), LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				result = "0000-0000-0000-0000";
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}
}
