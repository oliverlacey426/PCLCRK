// PCL

namespace PCL
{
	[StandardModule]
	public static class ModLaunch
	{
		public class McLaunchOptions
		{
			public string ServerIp;
			public string SaveBatch;
			public ModMinecraft.McInstance Instance;
			public List<string> ExtraGameArgs;
			public McLaunchOptions()
			{
				ServerIp = null;
				SaveBatch = null;
				Instance = null;
				ExtraGameArgs = new List<string>();
			}
		}
		public enum McLoginType
		{
			Legacy = 0,
			Nide = 2,
			Auth = 3,
			Ms = 5
		}
		public abstract class McLoginData
		{
			public McLoginType Type;
			public override bool Equals(object obj)
			{
				if (obj != null)
				{
					return obj.GetHashCode() == GetHashCode();
				}
				return false;
			}
		}
		public class McLoginServer : McLoginData
		{
			public string UserName;
			public string Password;
			public string BaseUrl;
			public string Token;
			public string Description;
			public bool ForceReselectProfile;
			public McLoginServer(McLoginType Type)
			{
				ForceReselectProfile = false;
				base.Type = Type;
			}
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal((UserName + Password + BaseUrl + Token + Conversions.ToString((int)Type)).GetStableHashCode()), 2147483647m));
			}
		}
		public class McLoginMs : McLoginData
		{
			public string OAuthRefreshToken;
			public string AccessToken;
			public string Uuid;
			public string UserName;
			public string ProfileJson;
			public McLoginMs()
			{
				OAuthRefreshToken = "";
				AccessToken = "";
				Uuid = "";
				UserName = "";
				ProfileJson = "";
				Type = McLoginType.Ms;
			}
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal((OAuthRefreshToken + UserName).GetStableHashCode()), 2147483647m));
			}
		}
		public class McLoginLegacy : McLoginData
		{
			public string UserName;
			public int SkinType;
			public string SkinName;
			public McLoginLegacy()
			{
				Type = McLoginType.Legacy;
			}
			public override int GetHashCode()
			{
				return Convert.ToInt32(decimal.Remainder(new decimal((UserName + Conversions.ToString(SkinType) + SkinName + Conversions.ToString((int)Type)).GetStableHashCode()), 2147483647m));
			}
		}
		public struct McLoginResult
		{
			public string Name;
			public string Uuid;
			public string AccessToken;
			public string Type;
			public string ClientToken;
			public string ProfileJson;
		}
		[CompilerGenerated]
		internal sealed class _Closure___12_0
		{
			public bool _VB_Local_Finished;
			public _Closure___12_0(_Closure___12_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Finished = arg0._VB_Local_Finished;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				PageOtherTest.MemoryOptimize(ShowHint: false);
				_VB_Local_Finished = true;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___35_0
		{
			public JObject _VB_Local_LoginJson;
			public string _VB_Local_SelectedName;
			public string _VB_Local_SelectedId;
			public _Closure___35_0(_Closure___35_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_LoginJson = arg0._VB_Local_LoginJson;
					_VB_Local_SelectedName = arg0._VB_Local_SelectedName;
					_VB_Local_SelectedId = arg0._VB_Local_SelectedId;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				List<IMyRadio> list = new List<IMyRadio>();
				List<JToken> list2 = new List<JToken>();
				foreach (JToken item in (IEnumerable<JToken>)_VB_Local_LoginJson["availableProfiles"])
				{
					list.Add(new MyRadioBox
					{
						Text = item["name"].ToString()
					});
					list2.Add(item);
				}
				int value = ModMain.MyMsgBoxSelect(list, "选择使用的角色").Value;
				_VB_Local_SelectedName = list2[value]["name"].ToString();
				_VB_Local_SelectedId = list2[value]["id"].ToString();
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___51_0
		{
			public string _VB_Local_StdoutArgName;
			public string _VB_Local_StderrArgName;
			public _Closure___51_0(_Closure___51_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_StdoutArgName = arg0._VB_Local_StdoutArgName;
					_VB_Local_StderrArgName = arg0._VB_Local_StderrArgName;
				}
			}
			[SpecialName]
			internal bool _Lambda___3(string a)
			{
				return a.StartsWithF($"-D{_VB_Local_StdoutArgName}=");
			}
			[SpecialName]
			internal bool _Lambda___4(string a)
			{
				return a.StartsWithF($"-D{_VB_Local_StderrArgName}=");
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___52_0
		{
			public string _VB_Local_Arg;
			public _Closure___52_0(_Closure___52_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Arg = arg0._VB_Local_Arg;
				}
			}
			[SpecialName]
			internal void _Lambda___0(string a)
			{
				_VB_Local_Arg = _VB_Local_Arg + " " + ((a.Contains(" ") && !a.StartsWithF("\"") && !a.EndsWithF("\"")) ? string.Format("\"{0}\"", a.Replace("\"", "\\\"")) : a);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___53_0
		{
			public System.Windows.Size _VB_Local_Result;
			public _Closure___53_0(_Closure___53_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Result = arg0._VB_Local_Result;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				_VB_Local_Result = new System.Windows.Size(ModBase.GetPixelSize(ModMain.FrmMain.PanForm.ActualWidth), ModBase.GetPixelSize(ModMain.FrmMain.PanForm.ActualHeight));
			}
		}
		public static McLaunchOptions CurrentLaunchOptions = null;
		public static ModLoader.LoaderTask<McLaunchOptions, object> McLaunchLoader = new ModLoader.LoaderTask<McLaunchOptions, object>("Loader Launch", McLaunchStart)
		{
			OnStateChanged = [SpecialName] (ModLoader.LoaderBase a0) =>
			{
				McLaunchState((ModLoader.LoaderTask<McLaunchOptions, object>)a0);
			}
		};
		public static ModLoader.LoaderCombo<object> McLaunchLoaderReal;
		public static Process McLaunchProcess;
		public static ModWatcher.Watcher McLaunchWatcher;
		private static string CanceledHint = null;
		public static ModLoader.LoaderTask<McLoginData, McLoginResult> McLoginLoader = new ModLoader.LoaderTask<McLoginData, McLoginResult>("登录", McLoginStart, McLoginInput, ThreadPriority.BelowNormal)
		{
			ReloadTimeout = 1,
			ProgressWeight = 15.0,
			Block = false
		};
		public static ModLoader.LoaderTask<McLoginMs, McLoginResult> McLoginMsLoader = new ModLoader.LoaderTask<McLoginMs, McLoginResult>("Loader Login Ms", McLoginMsStart)
		{
			ReloadTimeout = 1
		};
		public static ModLoader.LoaderTask<McLoginLegacy, McLoginResult> McLoginLegacyLoader = new ModLoader.LoaderTask<McLoginLegacy, McLoginResult>("Loader Login Legacy", McLoginLegacyStart);
		public static ModLoader.LoaderTask<McLoginServer, McLoginResult> McLoginNideLoader = new ModLoader.LoaderTask<McLoginServer, McLoginResult>("Loader Login Nide", McLoginServerStart)
		{
			ReloadTimeout = 600000
		};
		public static ModLoader.LoaderTask<McLoginServer, McLoginResult> McLoginAuthLoader = new ModLoader.LoaderTask<McLoginServer, McLoginResult>("Loader Login Auth", McLoginServerStart)
		{
			ReloadTimeout = 600000
		};
		public static Java McLaunchJavaSelected = null;
		private static string McLaunchArgument;
		private static Encoding McLaunchEncoding;
		public static bool McLaunchStart(McLaunchOptions Options = null)
		{
			CurrentLaunchOptions = Options ?? new McLaunchOptions();
			if (!ModBase.RunInUi())
			{
				throw new Exception("McLaunchStart 必须在 UI 线程调用！");
			}
			bool result;
			if (McLaunchLoader.State == ModBase.LoadState.Loading)
			{
				ModMain.Hint("已有游戏正在启动中！", ModMain.HintType.Red);
				result = false;
			}
			else
			{
				if ((object)CurrentLaunchOptions.Instance != null && ModMinecraft.McInstanceSelected != CurrentLaunchOptions.Instance)
				{
					McLaunchLog("在启动前切换到版本 " + CurrentLaunchOptions.Instance.Name);
					try
					{
						CurrentLaunchOptions.Instance.GetJsonPath();
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ModMain.Hint($"无法启动 {CurrentLaunchOptions.Instance.Name}：当前 Minecraft 文件夹中尚未安装该版本！", ModMain.HintType.Red);
						result = false;
						ProjectData.ClearProjectError();
						goto IL_019e;
					}
					CurrentLaunchOptions.Instance.Load();
					if (CurrentLaunchOptions.Instance.State == ModMinecraft.McInstanceState.Error)
					{
						ModMain.Hint($"无法启动 {CurrentLaunchOptions.Instance.Name}：{CurrentLaunchOptions.Instance.Info}", ModMain.HintType.Red);
						result = false;
						goto IL_019e;
					}
					ModMinecraft.McInstanceSelected = CurrentLaunchOptions.Instance;
					ModMain.FrmLaunchLeft.RefreshButtonsUI();
					ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: false);
				}
				ModMain.FrmMain.AprilGiveup();
				ModMain.FrmMain.PageStack = ModMain.FrmMain.PageStack.Where([SpecialName] (FormMain.PageStackData p) => p.Page != FormMain.PageType.InstanceSelect).ToList();
				McLaunchLoader.Start(Options, IsForceRestart: true);
				result = true;
			}
			goto IL_019e;
			IL_019e:
			return result;
		}
		public static void McLaunchLog(string Text, LogLevel Level = LogLevel.Info)
		{
			Text = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(Text, '*'), '*');
			if (ModBase.ModeDebug)
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmLaunchRight.LabLog.Text += string.Format("{0}[{1:HH':'mm':'ss'.'fff}] {2}", "\r\n", DateTime.Now, Text);
				});
			}
			Logger.Log(Text, Level, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
		}
		private static void McLaunchState(ModLoader.LoaderTask<McLaunchOptions, object> Loader)
		{
			switch (McLaunchLoader.State)
			{
			case ModBase.LoadState.Waiting:
			case ModBase.LoadState.Finished:
			case ModBase.LoadState.Failed:
			case ModBase.LoadState.Canceled:
				ModMain.FrmLaunchLeft.PageChangeToLogin();
				break;
			case ModBase.LoadState.Loading:
				ModMain.FrmLaunchRight.LabLog.Text = "";
				break;
			}
		}
		private static void McLaunchStart(ModLoader.LoaderTask<McLaunchOptions, object> Loader)
		{
			ModBase.RunInUiWait(ModMain.FrmLaunchLeft.PageChangeToLaunching);
			try
			{
				McLaunchPrecheck();
				McLaunchLog("预检测已通过");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (!ex2.IsCanceled())
				{
					ModMain.Hint(ex2.Message, ModMain.HintType.Red);
				}
				throw;
			}
			bool flag = CurrentLaunchOptions?.SaveBatch != null;
			try
			{
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>
				{
					new ModLoader.LoaderTask<int, int>("获取 Java", McLaunchJava)
					{
						ProgressWeight = 4.0,
						Block = false
					},
					McLoginLoader,
					new ModLoader.LoaderCombo<string>("补全文件", ModDownload.DlClientFix(ModMinecraft.McInstanceSelected, CheckAssetsHash: false, DownloadAssetIndexInBackground: true))
					{
						ProgressWeight = 15.0,
						Show = false
					},
					new ModLoader.LoaderTask<string, int>("获取启动参数", McLaunchArgumentMain)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<int, int>("解压文件", McLaunchNatives)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<int, int>("预启动处理", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
					{
						McLaunchPrerun();
					})
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, int>("执行自定义命令", McLaunchCustom)
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, Process>("启动进程", McLaunchRun)
					{
						ProgressWeight = 2.0
					},
					new ModLoader.LoaderTask<Process, int>("等待游戏窗口出现", McLaunchWait)
					{
						ProgressWeight = 1.0
					},
					new ModLoader.LoaderTask<int, int>("结束处理", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
					{
						McLaunchEnd();
					})
					{
						ProgressWeight = 1.0
					}
				};
				switch (Settings.Get<int>("VersionRamOptimize", ModMinecraft.McInstanceSelected))
				{
				case 0:
					if (Settings.Get<bool>("LaunchArgumentRam"))
					{
						((ModLoader.LoaderCombo<string>)list[2]).Block = false;
						list.Insert(3, new ModLoader.LoaderTask<int, int>("内存优化", McLaunchMemoryOptimize)
						{
							ProgressWeight = 30.0
						});
					}
					break;
				case 1:
					((ModLoader.LoaderCombo<string>)list[2]).Block = false;
					list.Insert(3, new ModLoader.LoaderTask<int, int>("内存优化", McLaunchMemoryOptimize)
					{
						ProgressWeight = 30.0
					});
					break;
				}
				ModLoader.LoaderCombo<object> loaderCombo = new ModLoader.LoaderCombo<object>("Minecraft 启动", list)
				{
					Show = false
				};
				if (McLoginLoader.State == ModBase.LoadState.Finished)
				{
					McLoginLoader.State = ModBase.LoadState.Waiting;
				}
				McLaunchLoaderReal = loaderCombo;
				CanceledHint = null;
				loaderCombo.Start();
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				while (loaderCombo.State == ModBase.LoadState.Loading)
				{
					ModMain.FrmLaunchLeft.Dispatcher.Invoke(ModMain.FrmLaunchLeft.LaunchingRefresh);
					Thread.Sleep(100);
				}
				ModMain.FrmLaunchLeft.Dispatcher.Invoke(ModMain.FrmLaunchLeft.LaunchingRefresh);
				switch (loaderCombo.State)
				{
				case ModBase.LoadState.Finished:
					ModMain.Hint(ModMinecraft.McInstanceSelected.Name + " 启动成功！", ModMain.HintType.Green);
					if (!flag)
					{
						ModMain.Telemetry("Minecraft 启动成功", "Version", ModMinecraft.McInstanceSelected.VersionDisplayName(), "LoginType", Settings.Get<McLoginType>("LoginType").ToString());
					}
					break;
				case ModBase.LoadState.Canceled:
					if (CanceledHint == null)
					{
						ModMain.Hint(flag ? "已取消导出启动脚本！" : "已取消启动！");
					}
					else
					{
						ModMain.Hint(CanceledHint, ModMain.HintType.Green);
					}
					break;
				case ModBase.LoadState.Failed:
					throw loaderCombo.Error;
				default:
					throw new Exception("错误的状态改变：" + loaderCombo.State);
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				ex4.ThrowIfCanceled();
				Exception ex5 = ex4;
				while (true)
				{
					if (ex5.Message.StartsWithF("$"))
					{
						ModMain.MyMsgBox(ex5.Message.TrimStart('$'), flag ? "导出启动脚本失败" : "启动失败");
						throw;
					}
					if (ex5.InnerException == null)
					{
						break;
					}
					ex5 = ex5.InnerException;
				}
				McLaunchLog("错误：" + ex4.GetDisplay(multiline: true));
				Logger.Error(ex4, flag ? "导出启动脚本失败" : "Minecraft 启动失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				if (!flag)
				{
					ModMain.Telemetry("Minecraft 启动失败", "Version", ModMinecraft.McInstanceSelected.VersionDisplayName(), "LoginType", Settings.Get<McLoginType>("LoginType").ToString(), "Exception", ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(ex4.GetDisplay(multiline: true), '*'), '*'));
				}
				throw;
			}
		}
		private static void McLaunchMemoryOptimize(ModLoader.LoaderTask<int, int> Loader)
		{
			_Closure___12_0 arg = default;
			_Closure___12_0 CS_8__locals3 = new _Closure___12_0(arg);
			McLaunchLog("内存优化开始");
			CS_8__locals3._VB_Local_Finished = false;
			ModBase.RunInNewThread([SpecialName] () =>
			{
				PageOtherTest.MemoryOptimize(ShowHint: false);
				CS_8__locals3._VB_Local_Finished = true;
			}, "Launch Memory Optimize");
			while (!CS_8__locals3._VB_Local_Finished && !Loader.IsCanceled)
			{
				if (Loader.Progress < 0.7)
				{
					Loader.Progress += 0.007;
				}
				else
				{
					Loader.Progress += (0.95 - Loader.Progress) * 0.02;
				}
				Thread.Sleep(100);
			}
		}
		private static void McLaunchPrecheck()
		{
			if (Settings.Get<bool>("SystemDebugDelay"))
			{
				Thread.Sleep(ModBase.RandomInteger(100, 2000));
			}
			if (ModMinecraft.McInstanceSelected.PathIndie.Contains("!") || ModMinecraft.McInstanceSelected.PathIndie.Contains(";"))
			{
				throw new Exception("游戏路径中不可包含 ! 或 ;（" + ModMinecraft.McInstanceSelected.PathIndie + "）");
			}
			if (ModMinecraft.McInstanceSelected.PathVersion.Contains("!") || ModMinecraft.McInstanceSelected.PathVersion.Contains(";"))
			{
				throw new Exception("游戏路径中不可包含 ! 或 ;（" + ModMinecraft.McInstanceSelected.PathVersion + "）");
			}
			if ((object)ModMinecraft.McInstanceSelected == null)
			{
				throw new Exception("未选择 Minecraft 版本！");
			}
			ModMinecraft.McInstanceSelected.Load();
			if (ModMinecraft.McInstanceSelected.State == ModMinecraft.McInstanceState.Error)
			{
				throw new Exception("Minecraft 存在问题：" + ModMinecraft.McInstanceSelected.Info);
			}
			string text = "";
			ModBase.RunInUiWait([SpecialName] () =>
			{
				text = McLoginAble(McLoginInput());
			});
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				throw new ArgumentException(text);
			}
			if (CurrentLaunchOptions?.SaveBatch != null || Settings.Get<bool>("HintBuy") || Settings.Get<McLoginType>("LoginType") == McLoginType.Ms)
			{
				return;
			}
			if (CultureInfo.CurrentCulture.Name.StartsWithF("zh") || CultureInfo.CurrentUICulture.Name.StartsWithF("zh"))
			{
				ModBase.RunInNewThread([SpecialName] () =>
				{
					switch (Settings.Get<int>("SystemLaunchCount"))
					{
					case 3:
					case 8:
					case 15:
					case 30:
					case 50:
					case 70:
					case 90:
					case 110:
					case 130:
					case 180:
					case 220:
					case 280:
					case 330:
					case 380:
					case 450:
					case 550:
					case 660:
					case 750:
					case 880:
					case 950:
					case 1100:
					case 1300:
					case 1500:
					case 1700:
					case 1900:
						if (ModMain.MyMsgBox("你已经启动了 " + Conversions.ToString(Settings.Get<int>("SystemLaunchCount")) + " 次 Minecraft 啦！\r\n如果觉得 Minecraft 还不错，可以购买正版支持一下，毕竟开发游戏也真的很不容易……不要一直白嫖啦。\r\n\r\n在登录一次正版账号后，就不会再出现这个提示了！", "考虑一下正版？", "支持正版游戏！", "下次一定") == 1)
						{
							ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
						}
						break;
					}
				}, "Buy Minecraft");
			}
			else
			{
				if (Settings.Get<McLoginType>("LoginType") != McLoginType.Legacy)
				{
					return;
				}
				switch (ModMain.MyMsgBox("你必须先登录正版账号，才能进行离线登录！", "正版验证", "购买正版", "试玩", "返回", IsWarn: false, HighLight: true, ForceWait: false, [SpecialName] () =>
				{
					ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
				}))
				{
				case 2:
					ModMain.Hint("游戏将以试玩模式启动！", ModMain.HintType.Red);
					CurrentLaunchOptions.ExtraGameArgs.Add("--demo");
					break;
				case 3:
					throw new OperationCanceledException();
				}
			}
		}
		public static string McLoginName()
		{
			switch (Settings.Get<McLoginType>("LoginType"))
			{
			case McLoginType.Ms:
				if (Operators.CompareString(Settings.Get<string>("CacheMsV2Name"), "", TextCompare: false) != 0)
				{
					return Settings.Get<string>("CacheMsV2Name");
				}
				break;
			case McLoginType.Legacy:
				if (Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) != 0)
				{
					return Settings.Get<string>("LoginLegacyName").ToString().BeforeFirst("\u00a8");
				}
				break;
			case McLoginType.Nide:
				if (Operators.CompareString(Settings.Get<string>("CacheNideName"), "", TextCompare: false) != 0)
				{
					return Settings.Get<string>("CacheNideName");
				}
				break;
			case McLoginType.Auth:
				if (Operators.CompareString(Settings.Get<string>("CacheAuthName"), "", TextCompare: false) != 0)
				{
					return Settings.Get<string>("CacheAuthName");
				}
				break;
			}
			if (Operators.CompareString(Settings.Get<string>("CacheMsV2Name"), "", TextCompare: false) != 0)
			{
				return Settings.Get<string>("CacheMsV2Name");
			}
			if (Operators.CompareString(Settings.Get<string>("CacheNideName"), "", TextCompare: false) != 0)
			{
				return Settings.Get<string>("CacheNideName");
			}
			if (Operators.CompareString(Settings.Get<string>("CacheAuthName"), "", TextCompare: false) != 0)
			{
				return Settings.Get<string>("CacheAuthName");
			}
			if (Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) != 0)
			{
				return Settings.Get<string>("LoginLegacyName").ToString().BeforeFirst("\u00a8");
			}
			return null;
		}
		public static string McLoginAble()
		{
			switch (Settings.Get<McLoginType>("LoginType"))
			{
			case McLoginType.Ms:
				if (Operators.CompareString(Settings.Get<string>("CacheMsV2OAuthRefresh"), "", TextCompare: false) == 0)
				{
					return ModMain.FrmLoginMs.IsVaild();
				}
				return "";
			case McLoginType.Legacy:
				return ModMain.FrmLoginLegacy.IsVaild();
			case McLoginType.Nide:
				if (Operators.CompareString(Settings.Get<string>("CacheNideAccess"), "", TextCompare: false) == 0)
				{
					return ModMain.FrmLoginNide.IsVaild();
				}
				return "";
			case McLoginType.Auth:
				if (Operators.CompareString(Settings.Get<string>("CacheAuthAccess"), "", TextCompare: false) == 0)
				{
					return ModMain.FrmLoginAuth.IsVaild();
				}
				return "";
			default:
				return "未知的登录方式";
			}
		}
		public static string McLoginAble(McLoginData LoginData)
		{
			return LoginData.Type switch
			{
				McLoginType.Ms => PageLoginMs.IsVaild((McLoginMs)LoginData), 
				McLoginType.Legacy => PageLoginLegacy.IsVaild((McLoginLegacy)LoginData), 
				McLoginType.Nide => PageLoginNide.IsVaild((McLoginServer)LoginData), 
				McLoginType.Auth => PageLoginAuth.IsVaild((McLoginServer)LoginData), 
				_ => "未知的登录方式", 
			};
		}
		public static McLoginData McLoginInput()
		{
			McLoginData result = null;
			McLoginType mcLoginType = Settings.Get<McLoginType>("LoginType");
			try
			{
				switch (mcLoginType)
				{
				case McLoginType.Legacy:
					result = PageLoginLegacy.GetLoginData();
					break;
				case McLoginType.Ms:
					result = ((Operators.CompareString(Settings.Get<string>("CacheMsV2OAuthRefresh"), "", TextCompare: false) != 0) ? PageLoginMsSkin.GetLoginData() : PageLoginMs.GetLoginData());
					break;
				case McLoginType.Nide:
					result = ((Operators.CompareString(Settings.Get<string>("CacheNideAccess"), "", TextCompare: false) != 0) ? PageLoginNideSkin.GetLoginData() : PageLoginNide.GetLoginData());
					break;
				case McLoginType.Auth:
					result = ((Operators.CompareString(Settings.Get<string>("CacheAuthAccess"), "", TextCompare: false) != 0) ? PageLoginAuthSkin.GetLoginData() : PageLoginAuth.GetLoginData());
					break;
				}
				return result;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception($"获取登录输入信息失败（{mcLoginType}）", innerException);
			}
		}
		private static void McLoginStart(ModLoader.LoaderTask<McLoginData, McLoginResult> Data)
		{
			McLaunchLog("登录加载已开始");
			string text = McLoginAble(Data.Input);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				throw new ArgumentException(text);
			}
			ModLoader.LoaderBase loaderBase = null;
			switch (Data.Input.Type)
			{
			case McLoginType.Ms:
				loaderBase = McLoginMsLoader;
				break;
			case McLoginType.Legacy:
				loaderBase = McLoginLegacyLoader;
				break;
			case McLoginType.Nide:
				loaderBase = McLoginNideLoader;
				break;
			case McLoginType.Auth:
				loaderBase = McLoginAuthLoader;
				break;
			}
			loaderBase.WaitForExit(Data.Input, McLoginLoader, Data.IsForceRestarting);
			object obj = NewLateBinding.LateGet(loaderBase, null, "Output", new object[0], null, null, null);
			Data.Output = ((obj != null) ? ((McLoginResult)obj) : default(McLoginResult));
			ModBase.RunInUi([SpecialName] () =>
			{
				ModMain.FrmLaunchLeft.RefreshPage(KeepInput: true, Anim: false);
			});
			McLaunchLog("登录加载已结束");
		}
		private static void McLoginMsStart(ModLoader.LoaderTask<McLoginMs, McLoginResult> Data)
		{
			McLoginMs input = Data.Input;
			string userName = input.UserName;
			McLaunchLog("登录方式：正版（" + ((Operators.CompareString(userName, "", TextCompare: false) == 0) ? "尚未登录" : userName) + "）");
			Data.Progress = 0.05;
			long num = Settings.Get<long>("CacheMsV2Expires");
			if (!Data.IsForceRestarting && num > 0 && num > ModBase.GetUnixTimestampUtc() && Operators.CompareString(input.UserName, Settings.Get<string>("CacheMsV2Name"), TextCompare: false) == 0)
			{
				Data.Output = new McLoginResult
				{
					Name = input.UserName,
					Type = "Microsoft",
					AccessToken = Settings.Get<string>("CacheMsV2Access"),
					Uuid = Settings.Get<string>("CacheMsV2Uuid"),
					ClientToken = Settings.Get<string>("CacheMsV2Uuid"),
					ProfileJson = Settings.Get<string>("CacheMsV2ProfileJson")
				};
				McLaunchLog("无需登录，AccessToken 尚未过期");
				goto IL_035b;
			}
			(string, string) tuple;
			if (Operators.CompareString(input.OAuthRefreshToken, "", TextCompare: false) != 0)
			{
				tuple = MsLoginStep1Refresh(input.OAuthRefreshToken);
				if (Operators.CompareString(tuple.Item1, "Relogin", TextCompare: false) != 0)
				{
					goto IL_014a;
				}
			}
			tuple = MsLoginStep1New(Data);
			goto IL_014a;
			IL_035b:
			McLaunchLog($"微软登录结束，AccessToken 将在 {DateTimeOffset.FromUnixTimeSeconds(num).LocalDateTime} 过期");
			Settings.Set("HintBuy", true);
			if (ModSecret.ThemeUnlock(10, ShowDoubleHint: false))
			{
				ModMain.MyMsgBox("感谢你对正版游戏的支持！\r\n隐藏主题 跳票红 已解锁！");
			}
			return;
			IL_014a:
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			Data.Progress = 0.25;
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			string item = tuple.Item1;
			string item2 = tuple.Item2;
			string xBLToken = MsLoginStep2(item);
			Data.Progress = 0.4;
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			(string XSTSToken, string UHS) tokens = MsLoginStep3(xBLToken);
			Data.Progress = 0.55;
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			(string, int) tuple2 = MsLoginStep4(tokens);
			num = tuple2.Item2;
			Data.Progress = 0.7;
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			MsLoginStep5(tuple2.Item1);
			Data.Progress = 0.85;
			if (Data.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			(string, string, string) tuple3 = MsLoginStep6(tuple2.Item1);
			Data.Progress = 0.98;
			Settings.Set("CacheMsV2OAuthRefresh", item2);
			Settings.Set("CacheMsV2Access", tuple2.Item1);
			Settings.Set("CacheMsV2Uuid", tuple3.Item1);
			Settings.Set("CacheMsV2Name", tuple3.Item2);
			Settings.Set("CacheMsV2ProfileJson", tuple3.Item3);
			Settings.Set("CacheMsV2Expires", tuple2.Item2);
			JObject jObject = (JObject)Settings.Get<string>("LoginMsJson").DeserializeJson();
			jObject.Remove(input.UserName);
			jObject[tuple3.Item2] = item2;
			Settings.Set("LoginMsJson", jObject.ToString(Formatting.None));
			Data.Output = new McLoginResult
			{
				AccessToken = tuple2.Item1,
				Name = tuple3.Item2,
				Uuid = tuple3.Item1,
				Type = "Microsoft",
				ClientToken = tuple3.Item1,
				ProfileJson = tuple3.Item3
			};
			goto IL_035b;
		}
		private static void McLoginServerStart(ModLoader.LoaderTask<McLoginServer, McLoginResult> Data)
		{
			McLoginServer input = Data.Input;
			bool flag = false;
			bool flag2 = false;
			McLaunchLog("登录方式：" + input.Description);
			Data.Progress = 0.05;
			if (Data.Input.ForceReselectProfile || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Username"), Data.Input.UserName, TextCompare: false) != 0 || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Pass"), Data.Input.Password, TextCompare: false) != 0 || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Access"), "", TextCompare: false) == 0 || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Client"), "", TextCompare: false) == 0 || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Uuid"), "", TextCompare: false) == 0 || Operators.CompareString(Settings.Get<string>("Cache" + input.Token + "Name"), "", TextCompare: false) == 0)
			{
				goto IL_021d;
			}
			try
			{
				if (Data.IsCanceled)
				{
					throw new OperationCanceledException();
				}
				McLoginRequestValidate(ref Data);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "验证登录失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				if (ex2.IsBadNetwork())
				{
					throw new Exception("$登录失败：你的网络环境不佳，导致难以连接到海外服务器。\r\n请稍后再试，或使用加速器、VPN 改善网络环境。");
				}
				ProjectData.ClearProjectError();
				goto IL_019d;
			}
			goto IL_027b;
			IL_021d:
			try
			{
				if (Data.IsCanceled)
				{
					throw new OperationCanceledException();
				}
				flag = McLoginRequestLogin(ref Data);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				McLaunchLog("登录失败：" + ex4.GetDisplay(multiline: true));
				throw;
			}
			if (flag)
			{
				McLaunchLog("重新进行刷新登录");
				flag2 = true;
				Data.Progress = 0.65;
				goto IL_01ad;
			}
			goto IL_027b;
			IL_027b:
			Data.Progress = 0.95;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			checked
			{
				try
				{
					if (Operators.CompareString(Settings.Get<string>("Login" + input.Token + "Email"), "", TextCompare: false) != 0)
					{
						list.AddRange(Settings.Get<string>("Login" + input.Token + "Email").Split("\u00a8"));
					}
					if (Operators.CompareString(Settings.Get<string>("Login" + input.Token + "Pass"), "", TextCompare: false) != 0)
					{
						list2.AddRange(Settings.Get<string>("Login" + input.Token + "Pass").Split("\u00a8"));
					}
					int num = list.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						dictionary.Add(list[i], list2[i]);
					}
					dictionary.Remove(input.UserName);
					list = new List<string>(dictionary.Keys);
					list.Insert(0, input.UserName);
					list2 = new List<string>(dictionary.Values);
					list2.Insert(0, input.Password);
					Settings.Set("Login" + input.Token + "Email", list.Join('\u00a8'));
					Settings.Set("Login" + input.Token + "Pass", list2.Join('\u00a8'));
					return;
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					Logger.Error(ex6, "保存启动记录失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					Settings.Set("Login" + input.Token + "Email", "");
					Settings.Set("Login" + input.Token + "Pass", "");
					ProjectData.ClearProjectError();
					return;
				}
			}
			IL_019d:
			Data.Progress = 0.25;
			goto IL_01ad;
			IL_01ad:
			try
			{
				if (Data.IsCanceled)
				{
					throw new OperationCanceledException();
				}
				McLoginRequestRefresh(ref Data, flag);
			}
			catch (Exception ex7)
			{
				ProjectData.SetProjectError(ex7);
				Exception ex8 = ex7;
				McLaunchLog("刷新登录失败：" + ex8.GetDisplay(multiline: true));
				if (flag2)
				{
					throw new Exception("二轮刷新登录失败", ex8);
				}
				ProjectData.ClearProjectError();
				goto IL_01ff;
			}
			goto IL_027b;
			IL_01ff:
			Data.Progress = (flag ? 0.85 : 0.45);
			goto IL_021d;
		}
		private static void McLoginLegacyStart(ModLoader.LoaderTask<McLoginLegacy, McLoginResult> Data)
		{
			McLoginLegacy input = Data.Input;
			McLaunchLog("登录方式：离线（" + input.UserName + "）");
			Data.Progress = 0.1;
			ref McLoginResult output = ref Data.Output;
			output.Name = input.UserName;
			output.Uuid = McLoginLegacyUuidWithCustomSkin(input.UserName, input.SkinType, input.SkinName);
			output.Type = "Legacy";
			Data.Output.AccessToken = Data.Output.Uuid;
			Data.Output.ClientToken = Data.Output.Uuid;
			List<string> list = new List<string>();
			if (Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) != 0)
			{
				list.AddRange(Settings.Get<string>("LoginLegacyName").ToString().Split("\u00a8"));
			}
			list.Remove(input.UserName);
			list.Insert(0, input.UserName);
			Settings.Set("LoginLegacyName", list.Join('\u00a8'));
		}
		private static void McLoginRequestValidate(ref ModLoader.LoaderTask<McLoginServer, McLoginResult> Data)
		{
			McLaunchLog("验证登录开始（Validate, " + Data.Input.Token + "）");
			string text = Settings.Get<string>("Cache" + Data.Input.Token + "Access");
			string text2 = Settings.Get<string>("Cache" + Data.Input.Token + "Client");
			string uuid = Settings.Get<string>("Cache" + Data.Input.Token + "Uuid");
			string name = Settings.Get<string>("Cache" + Data.Input.Token + "Name");
			JObject jObject = new JObject(new JProperty("accessToken", text), new JProperty("clientToken", text2), new JProperty("requestUser", true));
			ModNet.NetRequestByClientRetry(Data.Input.BaseUrl + "/validate", HttpMethod.Post, null, jObject.ToString(Formatting.None), "application/json; charset=utf-8", "*/*", new string[1, 2] { { "Accept-Language", "zh-CN" } });
			Data.Output.AccessToken = text;
			Data.Output.ClientToken = text2;
			Data.Output.Uuid = uuid;
			Data.Output.Name = name;
			Data.Output.Type = Data.Input.Token;
			McLaunchLog("验证登录成功（Validate, " + Data.Input.Token + "）");
		}
		private static void McLoginRequestRefresh(ref ModLoader.LoaderTask<McLoginServer, McLoginResult> Data, bool RequestUser)
		{
			McLaunchLog("刷新登录开始（Refresh, " + Data.Input.Token + "）");
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry(Data.Input.BaseUrl + "/refresh", HttpMethod.Post, null, new JObject(new JProperty("selectedProfile", new JObject(new JProperty("name", Settings.Get<string>($"Cache{Data.Input.Token}Name")), new JProperty("id", Settings.Get<string>($"Cache{Data.Input.Token}Uuid")))), new JProperty("accessToken", Settings.Get<string>($"Cache{Data.Input.Token}Access")), new JProperty("requestUser", true)).ToString(Formatting.None), "application/json; charset=utf-8", "*/*", new string[1, 2] { { "Accept-Language", "zh-CN" } }, RequireJson: true).DeserializeJson();
			if (jObject["selectedProfile"] == null)
			{
				throw new Exception("选择的角色 " + Settings.Get<string>("Cache" + Data.Input.Token + "Name") + " 无效！");
			}
			Data.Output.AccessToken = jObject["accessToken"].ToString();
			Data.Output.ClientToken = jObject["clientToken"].ToString();
			Data.Output.Uuid = jObject["selectedProfile"]["id"].ToString();
			Data.Output.Name = jObject["selectedProfile"]["name"].ToString();
			Data.Output.Type = Data.Input.Token;
			Settings.Set("Cache" + Data.Input.Token + "Access", Data.Output.AccessToken);
			Settings.Set("Cache" + Data.Input.Token + "Client", Data.Output.ClientToken);
			Settings.Set("Cache" + Data.Input.Token + "Uuid", Data.Output.Uuid);
			Settings.Set("Cache" + Data.Input.Token + "Name", Data.Output.Name);
			Settings.Set("Cache" + Data.Input.Token + "Username", Data.Input.UserName);
			Settings.Set("Cache" + Data.Input.Token + "Pass", Data.Input.Password);
			McLaunchLog("刷新登录成功（Refresh, " + Data.Input.Token + "）");
		}
		private static bool McLoginRequestLogin(ref ModLoader.LoaderTask<McLoginServer, McLoginResult> Data)
		{
			bool result;
			try
			{
				_Closure___35_0 arg = default;
				_Closure___35_0 CS_8__locals24 = new _Closure___35_0(arg);
				bool flag = false;
				McLaunchLog("登录开始（Login, " + Data.Input.Token + "）");
				JObject jObject = new JObject(new JProperty("agent", new JObject(new JProperty("name", "Minecraft"), new JProperty("version", 1))), new JProperty("username", Data.Input.UserName), new JProperty("password", Data.Input.Password), new JProperty("requestUser", true));
				CS_8__locals24._VB_Local_LoginJson = (JObject)ModNet.NetRequestByClientRetry(Data.Input.BaseUrl + "/authenticate", HttpMethod.Post, null, jObject.ToString(Formatting.None), "application/json; charset=utf-8", "*/*", new string[1, 2] { { "Accept-Language", "zh-CN" } }, RequireJson: true).DeserializeJson();
				if (CS_8__locals24._VB_Local_LoginJson["availableProfiles"].Count() == 0)
				{
					if (Data.Input.ForceReselectProfile)
					{
						ModMain.Hint("你还没有创建角色，无法更换！", ModMain.HintType.Red);
					}
					throw new Exception("$你还没有创建角色，请在创建角色后再试！");
				}
				if (Data.Input.ForceReselectProfile && CS_8__locals24._VB_Local_LoginJson["availableProfiles"].IsSingle())
				{
					ModMain.Hint("你的账户中只有一个角色，无法更换！", ModMain.HintType.Red);
				}
				CS_8__locals24._VB_Local_SelectedName = null;
				CS_8__locals24._VB_Local_SelectedId = null;
				if ((CS_8__locals24._VB_Local_LoginJson["selectedProfile"] == null || Data.Input.ForceReselectProfile) && CS_8__locals24._VB_Local_LoginJson["availableProfiles"].Count() > 1)
				{
					flag = true;
					string right = Settings.Get<string>("Cache" + Data.Input.Token + "Uuid");
					foreach (JToken item in (IEnumerable<JToken>)CS_8__locals24._VB_Local_LoginJson["availableProfiles"])
					{
						if (Operators.CompareString(item["id"].ToString(), right, TextCompare: false) == 0)
						{
							CS_8__locals24._VB_Local_SelectedName = item["name"].ToString();
							CS_8__locals24._VB_Local_SelectedId = item["id"].ToString();
							McLaunchLog("根据缓存选择的角色：" + CS_8__locals24._VB_Local_SelectedName);
						}
					}
					if (CS_8__locals24._VB_Local_SelectedName == null)
					{
						McLaunchLog("要求玩家选择角色");
						ModBase.RunInUiWait([SpecialName] () =>
						{
							List<IMyRadio> list = new List<IMyRadio>();
							List<JToken> list2 = new List<JToken>();
							foreach (JToken item2 in (IEnumerable<JToken>)CS_8__locals24._VB_Local_LoginJson["availableProfiles"])
							{
								list.Add(new MyRadioBox
								{
									Text = item2["name"].ToString()
								});
								list2.Add(item2);
							}
							int value = ModMain.MyMsgBoxSelect(list, "选择使用的角色").Value;
							CS_8__locals24._VB_Local_SelectedName = list2[value]["name"].ToString();
							CS_8__locals24._VB_Local_SelectedId = list2[value]["id"].ToString();
						});
						McLaunchLog("玩家选择的角色：" + CS_8__locals24._VB_Local_SelectedName);
					}
				}
				else
				{
					CS_8__locals24._VB_Local_SelectedName = CS_8__locals24._VB_Local_LoginJson["selectedProfile"]["name"].ToString();
					CS_8__locals24._VB_Local_SelectedId = CS_8__locals24._VB_Local_LoginJson["selectedProfile"]["id"].ToString();
				}
				Data.Output.AccessToken = CS_8__locals24._VB_Local_LoginJson["accessToken"].ToString();
				Data.Output.ClientToken = CS_8__locals24._VB_Local_LoginJson["clientToken"].ToString();
				Data.Output.Name = CS_8__locals24._VB_Local_SelectedName;
				Data.Output.Uuid = CS_8__locals24._VB_Local_SelectedId;
				Data.Output.Type = Data.Input.Token;
				Settings.Set("Cache" + Data.Input.Token + "Access", Data.Output.AccessToken);
				Settings.Set("Cache" + Data.Input.Token + "Client", Data.Output.ClientToken);
				Settings.Set("Cache" + Data.Input.Token + "Uuid", Data.Output.Uuid);
				Settings.Set("Cache" + Data.Input.Token + "Name", Data.Output.Name);
				Settings.Set("Cache" + Data.Input.Token + "Username", Data.Input.UserName);
				Settings.Set("Cache" + Data.Input.Token + "Pass", Data.Input.Password);
				McLaunchLog("登录成功（Login, " + Data.Input.Token + "）");
				result = flag;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				string display = ex2.GetDisplay(multiline: false);
				Logger.Info(ex2, "登录失败原始错误信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				if (ex2 is HttpRequestCodeException)
				{
					string text = null;
					try
					{
						string response = ((HttpRequestCodeException)ex2).Response;
						if (response.ContainsIgnoreCase("errorMessage"))
						{
							text = response.DeserializeJson()["errorMessage"].ToString();
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					if (!string.IsNullOrWhiteSpace(text))
					{
						if (text.Contains("密码错误") || text.ContainsIgnoreCase("Incorrect username or password"))
						{
							McLaunchLog("密码错误，退出登录");
							switch (Data.Input.Type)
							{
							case McLoginType.Auth:
								ModBase.RunInUi(PageLoginAuthSkin.ExitLogin);
								break;
							case McLoginType.Nide:
								ModBase.RunInUi(PageLoginNideSkin.ExitLogin);
								break;
							}
						}
						throw new Exception("$登录失败：" + text);
					}
				}
				if (!display.Contains("(403)"))
				{
					if (ex2.IsBadNetwork())
					{
						throw new Exception("$登录失败：你的网络环境不佳，导致难以连接到海外服务器。\r\n请稍后再试，或使用加速器、VPN 改善网络环境。");
					}
					if (ex2.Message.StartsWithF("$"))
					{
						throw;
					}
					throw new Exception("登录失败：" + ex2.Message, ex2);
				}
				switch (Data.Input.Type)
				{
				case McLoginType.Auth:
					throw new Exception("$登录失败，以下为可能的原因：\r\n - 输入的账号或密码错误。\r\n - 登录尝试过于频繁，导致被暂时屏蔽。请不要操作，等待 10 分钟后再试。\r\n - 只注册了账号，但没有在皮肤站新建角色。");
				case McLoginType.Nide:
					throw new Exception("$登录失败，以下为可能的原因：\r\n - 输入的账号或密码错误。\r\n - 密码错误次数过多，导致被暂时屏蔽。请不要操作，等待 10 分钟后再试。\r\n" + (Data.Input.UserName.Contains("@") ? "" : " - 登录账号应为邮箱或统一通行证账号，而非游戏角色 ID。\r\n") + " - 只注册了账号，但没有加入对应服务器。");
				default:
					result = false;
					ProjectData.ClearProjectError();
					break;
				}
			}
			return result;
		}
		private static (string OAuthAccessToken, string OAuthRefreshToken) MsLoginStep1New(ModLoader.LoaderTask<McLoginMs, McLoginResult> Data)
		{
			ModMain.MyMsgBoxConverter myMsgBoxConverter;
			while (true)
			{
				McLaunchLog("开始微软登录步骤 1/6（原始登录）");
				JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode", HttpMethod.Post, null, string.Format("client_id={0}&tenant=/consumers&scope=XboxLive.signin%20offline_access", "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1"), "application/x-www-form-urlencoded", "*/*", null, RequireJson: true).DeserializeJson();
				McLaunchLog("网页登录地址：" + jObject["verification_uri"].ToString());
				myMsgBoxConverter = new ModMain.MyMsgBoxConverter
				{
					Content = jObject,
					ForceWait = true,
					Type = ModMain.MyMsgBoxType.Login
				};
				ModMain.WaitingMyMsgBox.Enqueue(myMsgBoxConverter);
				while (myMsgBoxConverter.Result == null)
				{
					Thread.Sleep(100);
				}
				if (!(myMsgBoxConverter.Result is ModBase.RestartException))
				{
					break;
				}
				if (ModMain.MyMsgBox(string.Format("请在登录时选择 {0}其他登录方法{1}，然后选择 {2}使用我的密码{3}。{4}如果没有该选项，请选择 {5}设置密码{6}，设置完毕后再登录。", ModBase.vbLQ, ModBase.vbRQ, ModBase.vbLQ, ModBase.vbRQ, "\r\n", ModBase.vbLQ, ModBase.vbRQ), "需要使用密码登录", "重新登录", "设置密码", "取消", IsWarn: false, HighLight: true, ForceWait: false, null, [SpecialName] () =>
				{
					ModBase.OpenWebsite("https://account.live.com/password/Change");
				}) != 1)
				{
					throw new OperationCanceledException();
				}
			}
			if (myMsgBoxConverter.Result is Exception)
			{
				throw (Exception)myMsgBoxConverter.Result;
			}
			(string, string) result = (Conversions.ToString(NewLateBinding.LateIndexGet(myMsgBoxConverter.Result, new object[1] { 0 }, null)), Conversions.ToString(NewLateBinding.LateIndexGet(myMsgBoxConverter.Result, new object[1] { 1 }, null)));
			return result;
		}
		private static (string OAuthAccessToken, string OAuthRefreshToken) MsLoginStep1Refresh(string Code)
		{
			McLaunchLog("开始微软登录步骤 1/6（刷新登录）");
			string data;
			(string, string) result;
			try
			{
				data = ModNet.NetRequestByClientMultiple("https://login.live.com/oauth20_token.srf", HttpMethod.Post, 2, string.Format("client_id={0}&refresh_token={1}&grant_type=refresh_token&scope=XboxLive.signin%20offline_access", "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1", StringUtils.FormUrlEscape(Code)), "application/x-www-form-urlencoded", "*/*", new string[2, 2]
				{
					{ "Accept-Language", "en-US,en;q=0.5" },
					{ "X-Requested-With", "XMLHttpRequest" }
				});
			}
			catch (HttpRequestCodeException ex)
			{
				ProjectData.SetProjectError(ex);
				HttpRequestCodeException ex2 = ex;
				string response = ex2.Response;
				if (response.ContainsIgnoreCase("must sign in again") || response.ContainsIgnoreCase("password expired") || (response.Contains("refresh_token") && response.ContainsIgnoreCase("is not valid")) || response.ContainsIgnoreCase("expired"))
				{
					result = ("Relogin", "");
					ProjectData.ClearProjectError();
					goto IL_019e;
				}
				if (response.Contains("Account security interrupt"))
				{
					ModMain.MyMsgBox("该账号由于安全问题无法登陆，请前往微软账户页获取更多信息。", "登录失败", "我知道了", "", "", IsWarn: true);
					throw new OperationCanceledException();
				}
				if (response.Contains("service abuse"))
				{
					ModMain.MyMsgBox("非常抱歉，该账号已被微软封禁，无法登录。", "登录失败", "我知道了", "", "", IsWarn: true);
					throw new OperationCanceledException();
				}
				throw;
			}
			JObject jObject = (JObject)data.DeserializeJson();
			string item = jObject["access_token"].ToString();
			string item2 = jObject["refresh_token"].ToString();
			result = (item, item2);
			goto IL_019e;
			IL_019e:
			return result;
		}
		private static string MsLoginStep2(string AccessToken)
		{
			McLaunchLog("开始微软登录步骤 2/6");
			string content = "{\r\n           \"Properties\": {\r\n               \"AuthMethod\": \"RPS\",\r\n               \"SiteName\": \"user.auth.xboxlive.com\",\r\n               \"RpsTicket\": \"" + (AccessToken.StartsWithF("d=") ? "" : "d=") + AccessToken + "\"\r\n           },\r\n           \"RelyingParty\": \"http://auth.xboxlive.com\",\r\n           \"TokenType\": \"JWT\"\r\n        }";
			return ModNet.NetRequestByClientMultiple("https://user.auth.xboxlive.com/user/authenticate", HttpMethod.Post, 3, content, "application/json", "*/*", null, RequireJson: true).DeserializeJson()["Token"].ToString();
		}
		private static (string XSTSToken, string UHS) MsLoginStep3(string XBLToken)
		{
			McLaunchLog("开始微软登录步骤 3/6");
			string content = "{\r\n           \"Properties\": {\r\n               \"SandboxId\": \"RETAIL\",\r\n               \"UserTokens\": [\"" + XBLToken + "\"]\r\n           },\r\n           \"RelyingParty\": \"rp://api.minecraftservices.com/\",\r\n           \"TokenType\": \"JWT\"\r\n        }";
			string data;
			try
			{
				data = ModNet.NetRequestByClientMultiple("https://xsts.auth.xboxlive.com/xsts/authorize", HttpMethod.Post, 3, content, "application/json", "*/*", null, RequireJson: true);
			}
			catch (HttpRequestCodeException ex)
			{
				ProjectData.SetProjectError(ex);
				HttpRequestCodeException ex2 = ex;
				if (ex2.Response.Contains("2148916227"))
				{
					ModMain.MyMsgBox("该账号似乎已被微软封禁，无法登录。", "登录失败", "我知道了", "", "", IsWarn: true);
					throw new OperationCanceledException();
				}
				if (ex2.Response.Contains("2148916233"))
				{
					if (ModMain.MyMsgBox("你尚未注册 Xbox 账户，请在注册后再登录。", "登录提示", "注册", "取消") == 1)
					{
						ModBase.OpenWebsite("https://signup.live.com/signup");
					}
					throw new OperationCanceledException();
				}
				if (ex2.Response.Contains("2148916235"))
				{
					ModMain.MyMsgBox(string.Format("你的网络所在的国家或地区无法登录微软账号。{0}请使用加速器或 VPN，然后再试。", "\r\n"), "登录失败", "我知道了");
					throw new OperationCanceledException();
				}
				if (ex2.Response.Contains("2148916238"))
				{
					if (ModMain.MyMsgBox("该账号年龄不足，你需要先修改出生日期，然后才能登录。\r\n该账号目前填写的年龄是否在 13 岁以上？", "登录提示", "13 岁以上", "12 岁以下", "我不知道") == 1)
					{
						ModBase.OpenWebsite("https://account.live.com/editprof.aspx");
						ModMain.MyMsgBox("请在打开的网页中修改账号的出生日期（至少改为 18 岁以上）。\r\n在修改成功后等待一分钟，然后再回到 PCL，就可以正常登录了！", "登录提示");
					}
					else
					{
						ModBase.OpenWebsite("https://support.microsoft.com/zh-cn/account-billing/如何更改-microsoft-帐户上的出生日期-837badbc-999e-54d2-2617-d19206b9540a");
						ModMain.MyMsgBox("请根据打开的网页的说明，修改账号的出生日期（至少改为 18 岁以上）。\r\n在修改成功后等待一分钟，然后再回到 PCL，就可以正常登录了！", "登录提示");
					}
					throw new OperationCanceledException();
				}
				throw;
			}
			JObject jObject = (JObject)data.DeserializeJson();
			return (XSTSToken: jObject["Token"].ToString(), UHS: jObject["DisplayClaims"]["xui"][0]["uhs"].ToString());
		}
		private static (string AccessToken, int ExpiresAt) MsLoginStep4((string XSTSToken, string UHS) Tokens)
		{
			McLaunchLog("开始微软登录步骤 4/6");
			string content = new JObject(new JProperty("identityToken", $"XBL3.0 x={Tokens.UHS};{Tokens.XSTSToken}")).ToString(Formatting.None);
			string data;
			try
			{
				data = ModNet.NetRequestByClientRetry("https://api.minecraftservices.com/authentication/login_with_xbox", HttpMethod.Post, null, content, "application/json", "*/*", null, RequireJson: true);
			}
			catch (HttpRequestCodeException ex)
			{
				ProjectData.SetProjectError(ex);
				HttpRequestCodeException ex2 = ex;
				if (ex2.StatusCode == (HttpStatusCode)429)
				{
					Logger.Warn(ex2, "微软登录第 4 步汇报 429", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					throw new Exception("$登录尝试太过频繁，请等待几分钟后再试！");
				}
				if (ex2.StatusCode == HttpStatusCode.Forbidden)
				{
					Logger.Warn(ex2, "微软登录第 4 步汇报 403", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					throw new Exception("$当前 IP 的登录尝试异常。\r\n如果你使用了 VPN 或加速器，请把它们关掉或更换节点后再试！");
				}
				if (ex2.StatusCode == HttpStatusCode.ServiceUnavailable)
				{
					Logger.Warn(ex2, "微软登录第 4 步汇报 503", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					throw new Exception("$Mojang 服务器出现问题，请稍后再试。\r\n你的网络是正常的，PCL 也是正常的，是 Mojang 出问题了……");
				}
				string? response = ex2.Response;
				if (response != null && response.Contains("ACCOUNT_SUSPENDED"))
				{
					ModMain.MyMsgBox("该账号似乎已被微软封禁，无法登录。", "登录失败", "我知道了", "", "", IsWarn: true);
					throw new OperationCanceledException();
				}
				throw;
			}
			JObject jObject = (JObject)data.DeserializeJson();
			return (AccessToken: jObject["access_token"].ToString(), ExpiresAt: checked((int)(jObject["expires_in"].ToObject<int>() + ModBase.GetUnixTimestampUtc() - 1200)));
		}
		private static void MsLoginStep5(string AccessToken)
		{
			McLaunchLog("开始微软登录步骤 5/6");
			string text = ModNet.NetRequestByClientMultiple("https://api.minecraftservices.com/entitlements/mcstore", null, 2, null, "application/json", "*/*", new string[1, 2] { 
			{
				"Authorization",
				"Bearer " + AccessToken
			} }, RequireJson: true);
			try
			{
				JObject jObject = (JObject)text.DeserializeJson();
				if (!jObject.ContainsKey("items") || !jObject["items"].Any())
				{
					int num = ModMain.MyMsgBox("你尚未购买正版 Minecraft，或者 Xbox Game Pass 已到期。", "登录失败", "购买 Minecraft", "取消");
					if (num == 1)
					{
						ModBase.OpenWebsite("https://www.xbox.com/zh-cn/games/store/minecraft-java-bedrock-edition-for-pc/9nxp44l49shj");
					}
					throw new OperationCanceledException();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"微软登录第 5 步异常：{text}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				throw;
			}
		}
		private static (string UUID, string UserName, string ProfileJson) MsLoginStep6(string AccessToken)
		{
			McLaunchLog("开始微软登录步骤 6/6");
			string text;
			try
			{
				text = ModNet.NetRequestByClientMultiple("https://api.minecraftservices.com/minecraft/profile", null, 2, null, "application/json", "*/*", new string[1, 2] { 
				{
					"Authorization",
					"Bearer " + AccessToken
				} }, RequireJson: true);
			}
			catch (HttpRequestCodeException ex)
			{
				ProjectData.SetProjectError(ex);
				HttpRequestCodeException ex2 = ex;
				switch (ex2.StatusCode)
				{
				case (HttpStatusCode)429:
					Logger.Warn(ex2, "微软登录第 6 步汇报 429", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					throw new Exception("$登录尝试太过频繁，请等待几分钟后再试！");
				case HttpStatusCode.NotFound:
					Logger.Warn(ex2, "微软登录第 6 步汇报 404", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					ModBase.RunInNewThread([SpecialName] () =>
					{
						int num = ModMain.MyMsgBox("请先创建 Minecraft 玩家档案，然后再重新登录。", "登录失败", "创建档案", "取消");
						if (num == 1)
						{
							ModBase.OpenWebsite("https://www.minecraft.net/zh-hans/msaprofile/mygames/editprofile");
						}
					}, "Login Failed: Create Profile");
					throw new OperationCanceledException();
				default:
					throw;
				}
			}
			JObject jObject = (JObject)text.DeserializeJson();
			string item = jObject["id"].ToString();
			string item2 = jObject["name"].ToString();
			return (UUID: item, UserName: item2, ProfileJson: text);
		}
		private static string McLoginLegacyUuidWithCustomSkin(string UserName, int SkinType, string SkinName)
		{
			string text = Conversions.ToString(McLoginLegacyUuid(UserName));
			checked
			{
				switch (SkinType)
				{
				case 1:
					while (Operators.CompareString(ModMinecraft.McSkinSex(text), "Steve", TextCompare: false) != 0)
					{
						if (text.EndsWithF("FFFFF"))
						{
							text = text.Substring(0, 27) + "00000";
						}
						text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1).ToString("X").PadLeft(5, '0');
					}
					break;
				case 2:
					while (Operators.CompareString(ModMinecraft.McSkinSex(text), "Alex", TextCompare: false) != 0)
					{
						if (text.EndsWithF("FFFFF"))
						{
							text = text.Substring(0, 27) + "00000";
						}
						text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1).ToString("X").PadLeft(5, '0');
					}
					break;
				case 3:
					try
					{
						if (Operators.CompareString(SkinName, "", TextCompare: false) != 0 && (object)ModMinecraft.McInstanceSelected != null && ModMinecraft.McInstanceSelected.Version.Vanilla.Major < 20)
						{
							Logger.Info($"由于离线皮肤设置，使用正版 UUID：{SkinName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
							text = Conversions.ToString(McLoginMojangUuid(SkinName, ThrowOnNotFound: false));
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "皮肤信息获取失败，游戏将以无皮肤的方式启动", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
						ProjectData.ClearProjectError();
					}
					break;
				case 4:
					while (Operators.CompareString(ModMinecraft.McSkinSex(text), Settings.Get<bool>("LaunchSkinSlim") ? "Alex" : "Steve", TextCompare: false) != 0)
					{
						if (text.EndsWithF("FFFFF"))
						{
							text = text.Substring(0, 27) + "00000";
						}
						text = text.Substring(0, 27) + (long.Parse(text.Substring(27), NumberStyles.AllowHexSpecifier) + 1).ToString("X").PadLeft(5, '0');
					}
					break;
				}
				return text;
			}
		}
		public static object McLoginMojangUuid(string Name, bool ThrowOnNotFound)
		{
			if (Name.Trim().Length == 0)
			{
				return new string('0', 32);
			}
			string text = ModBase.ReadIni(ModBase.PathTemp + "Cache\\Uuid\\Mojang.ini", Name);
			if (Strings.Len(text) == 32)
			{
				return text;
			}
			try
			{
				text = (string?)((((JObject)ModNet.NetRequestByClientRetry("https://api.mojang.com/users/profiles/minecraft/" + Name, null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson()) ?? throw new FileNotFoundException("正版玩家档案不存在（" + Name + "）"))["id"] ?? ((JToken)""));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"从官网获取正版 Uuid 失败（{Name}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				if (ThrowOnNotFound || Operators.CompareString(ex2.GetType().Name, "FileNotFoundException", TextCompare: false) != 0)
				{
					throw new Exception("从官网获取正版 Uuid 失败", ex2);
				}
				text = Conversions.ToString(McLoginLegacyUuid(Name));
				ProjectData.ClearProjectError();
			}
			if (Strings.Len(text) != 32)
			{
				throw new Exception("获取的正版 Uuid 长度不足（" + text + "）");
			}
			ModBase.WriteIni(ModBase.PathTemp + "Cache\\Uuid\\Mojang.ini", Name, text);
			return text;
		}
		public static object McLoginLegacyUuid(string Name)
		{
			string text = Name.Length.ToString("X").EnsureLength('0', 16) + Name.GetStableHashCode().ToString("X").EnsureLength('0', 16);
			return text.Substring(0, 12) + "3" + text.Substring(13, 3) + "9" + text.Substring(17, 15);
		}
		private static void McLaunchJava(ModLoader.LoaderTask<int, int> Task)
		{
			McLaunchJavaSelected = ModJava.SelectOrDownloadJava(ModMinecraft.McInstanceSelected, TryFixOrHint: true, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider());
			if (!Task.IsCanceled)
			{
				if (McLaunchJavaSelected == null)
				{
					throw new OperationCanceledException();
				}
				if (ModBase.ModeDebug)
				{
					McLaunchLog($"预期 Java 版本范围：{ModJava.GetJavaRequirement(ModMinecraft.McInstanceSelected).Range}");
					McLaunchLog(string.Format("Java 版本设置类别：{0}", Settings.Get<int>("VersionArgumentJavaV2", ModMinecraft.McInstanceSelected)));
				}
				McLaunchLog("选择的 Java：" + McLaunchJavaSelected.ToString());
			}
		}
		private static void McLaunchArgumentMain(ModLoader.LoaderTask<string, int> Loader)
		{
			List<string> list = new List<string>();
			McLaunchLog("参数获取开始");
			list.AddRange(McLaunchArgumentsJVM());
			list.Add((string?)ModMinecraft.McInstanceSelected.JsonObject["mainClass"]);
			list.AddRange(McLaunchArgumentsGame());
			McLaunchLog("=================== 启动参数开始 ===================");
			List<(string, string)> list2 = McLaunchArgumentsReplace(list).ToList();
			string text = "";
			foreach (string item in list)
			{
				string text2 = item;
				foreach (var item2 in list2)
				{
					text2 = text2.Replace(item2.Item1, item2.Item2);
				}
				if (text2.IndexOfAny(new char[6] { '&', '|', '<', '>', '^', ' ' }) >= 0 && !text2.StartsWithF("\"") && !text2.EndsWithF("\""))
				{
					text2 = string.Format("\"{0}\"", text2.Replace("\"", "\\\""));
				}
				McLaunchLog(text2);
				text = text + text2 + " ";
			}
			McLaunchLog("=================== 启动参数结束 ===================");
			McLaunchArgument = text.TrimEnd();
		}
		private static List<string> McLaunchArgumentsJVM()
		{
			_Closure___51_0 arg = default;
			_Closure___51_0 CS_8__locals6 = new _Closure___51_0(arg);
			List<string> list = new List<string>();
			if (ModMinecraft.McInstanceSelected.JsonObject["arguments"] != null && ModMinecraft.McInstanceSelected.JsonObject["arguments"]["jvm"] != null)
			{
				ModMinecraft.McInstance mcInstance = ModMinecraft.McInstanceSelected;
				while (true)
				{
					McLaunchLog($"新版 JVM 参数：从版本 JSON 获取（{mcInstance.Name}）");
					if (mcInstance.JsonObject["arguments"] != null && mcInstance.JsonObject["arguments"]["jvm"] != null)
					{
						foreach (JToken item in (IEnumerable<JToken>)mcInstance.JsonObject["arguments"]["jvm"])
						{
							switch (item.Type)
							{
							case JTokenType.String:
								list.Add(item.ToString().Trim());
								break;
							case JTokenType.Object:
							{
								JObject jObject = (JObject)item;
								if ((jObject.ContainsKey("rules") && !ModMinecraft.McJsonRuleCheck(jObject["rules"])) || !jObject.ContainsKey("value"))
								{
									break;
								}
								if (jObject["value"].Type == JTokenType.String)
								{
									list.Add(jObject["value"].ToString().Trim());
									break;
								}
								foreach (JToken item2 in (IEnumerable<JToken>)jObject["value"])
								{
									list.Add(item2.ToString().Trim());
								}
								break;
							}
							}
						}
					}
					if (Operators.CompareString(mcInstance.InheritName, "", TextCompare: false) == 0)
					{
						break;
					}
					mcInstance = new ModMinecraft.McInstance(mcInstance.InheritName);
				}
				list = list.Select([SpecialName] (string a) => (!a.Contains(" ") || a.StartsWithF("\"") || a.EndsWithF("\"")) ? a : string.Format("\"{0}\"", a.Replace("\"", "\\\""))).ToList();
			}
			else
			{
				McLaunchLog("旧版 JVM 参数");
				list.Add("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
				list.Add("-Djava.library.path=${natives_directory}");
				list.Add("-cp");
				list.Add("${classpath}");
			}
			string text = Settings.Get<string>("VersionAdvanceJvm", ModMinecraft.McInstanceSelected);
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = Settings.Get<string>("LaunchAdvanceJvm");
			}
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				text = ModMain.ArgumentReplace(text);
			}
			list = DeduplicateJavaArguments(SplitJavaArguments(list.Join(' ').Replace("McEmu= ", "McEmu=") + " " + text).ToList(), IsJVMArgs: true);
			if (Operators.CompareString(McLoginLoader.Output.Type, "Nide", TextCompare: false) == 0)
			{
				list.Add("-javaagent:\"${pure_directory}\\nide8auth.jar\"=\"" + Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected) + "\"");
			}
			if (Operators.CompareString(McLoginLoader.Output.Type, "Auth", TextCompare: false) == 0)
			{
				if (McLaunchJavaSelected.Version.Major >= 6)
				{
					list.Add("-Djavax.net.ssl.trustStoreType=WINDOWS-ROOT");
				}
				string text2 = Settings.Get<string>("VersionServerAuthServer", ModMinecraft.McInstanceSelected);
				try
				{
					McLaunchLog("开始 Authlib Injector Prefetch");
					string s = ModNet.NetRequestByClientRetry(text2, null, null, null, null, "*/*", null, RequireJson: false, Encoding.UTF8);
					list.Add("-javaagent:\"${pure_directory}\\authlib-injector.jar\"=\"" + text2 + "\"");
					list.Add("-Dauthlibinjector.side=client");
					list.Add("-Dauthlibinjector.yggdrasil.prefetched=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(s)));
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("无法连接到第三方登录服务器（" + text2 + "）", innerException);
				}
			}
			bool flag = !Settings.Get<bool>("LaunchAdvanceDisableJLW") && !Settings.Get<bool>("VersionAdvanceDisableJLW", ModMinecraft.McInstanceSelected) && !ModBase.IsGBKEncoding;
			if (flag && text.Contains("-javaagent"))
			{
				flag = false;
				McLaunchLog("由于使用了自定义 javaagent，已禁用 JLW，这可能导致游戏崩溃！", LogLevel.Warn);
			}
			if (flag)
			{
				if (McLaunchJavaSelected.Version.Major >= 9)
				{
					list.Add("--add-exports");
					list.Add("cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED");
				}
				list.Add("-Doolloo.jlw.tmpdir=${pure_directory}");
			}
			McLaunchLog("使用 JLW：" + Conversions.ToString(flag));
			bool flag2 = !Settings.Get<bool>("LaunchAdvanceDisableLUA") && !Settings.Get<bool>("VersionAdvanceDisableLUA", ModMinecraft.McInstanceSelected) && (from e in ModMinecraft.McLibListGet(ModMinecraft.McInstanceSelected, IncludeMainJar: false)
				select e.OriginalName).Contains("org.lwjgl:lwjgl:3.4.1");
			if (flag2)
			{
				list.Add(string.Format("-javaagent:\"{0}\"", LaunchUtils.ExtractPatch("Launch\\LwjglUnsafeAgent.jar", ModBase.PathPure.Value)));
			}
			McLaunchLog("使用 LUA：" + Conversions.ToString(flag2));
			McLaunchLog("当前剩余内存：" + Conversions.ToString(Math.Round((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0) + "G");
			checked
			{
				int num = (int)Math.Floor(PageInstanceSetup.GetRam(ModMinecraft.McInstanceSelected) * 1024.0);
				list.Add("-Xmx" + Conversions.ToString(num) + "m");
				int num2 = Settings.Get<int>("LaunchAdvanceGC");
				if ((object)ModMinecraft.McInstanceSelected != null && Settings.Get<int>("VersionAdvanceGC", ModMinecraft.McInstanceSelected) > 0)
				{
					num2 = Settings.Get<int>("VersionAdvanceGC", ModMinecraft.McInstanceSelected) - 1;
				}
				if (num2 != 3)
				{
					bool flag3 = false;
					if ((num2 == 0 && McLaunchJavaSelected.Version.Major < 15) || (num2 == 1 && McLaunchJavaSelected.Version.Major < 21) || num2 == 2 || num2 == 4)
					{
						flag3 = true;
					}
					if (Environment.OSVersion.Version.Major < 10 || Environment.OSVersion.Version.Build < 17763)
					{
						flag3 = true;
					}
					McLaunchLog(string.Format("GC 设置：{0}，选取 {1}，Java 版本：{2}", num2, flag3 ? "G1GC" : "ZGC", McLaunchJavaSelected.Version.Major));
					list = list.Where([SpecialName] (string a) => !a.RegexCheck("( )*-XX:[+-]?(Use\\w+GC|ZGenerational|UseCompactObjectHeaders|G1\\w+Percent|G1\\w+Size|(Max|Min)(GCPauseMillis|HeapFreeRatio))")).ToList();
					list.Add("-XX:+UnlockExperimentalVMOptions");
					if (McLaunchJavaSelected.Version.Major >= 24)
					{
						list.Add("-XX:+UseCompactObjectHeaders");
					}
					if (flag3 && num2 == 4)
					{
						list.Add("-XX:+UseG1GC");
						list.Add("-XX:G1NewSizePercent=20");
						list.Add("-XX:G1ReservePercent=20");
						list.Add("-XX:G1HeapRegionSize=32M");
						list.Add("-XX:MaxGCPauseMillis=50");
						if (!ModBase.ModeDebug)
						{
							list.Add("-XX:+PerfDisableSharedMem");
						}
						if (McLaunchJavaSelected.Version.Major <= 7)
						{
							list.Add("-XX:MaxPermSize=512m");
						}
						if (McLaunchJavaSelected.Version.Major == 8)
						{
							list.Add("-XX:+ParallelRefProcEnabled");
						}
						if (McLaunchJavaSelected.Version.Major >= 12)
						{
							list.Add("-XX:MinHeapFreeRatio=25");
						}
						if (McLaunchJavaSelected.Version.Major >= 12)
						{
							list.Add("-XX:MaxHeapFreeRatio=40");
						}
					}
					else if (flag3)
					{
						list.Add("-XX:+UseG1GC");
						list.Add("-XX:G1NewSizePercent=20");
						list.Add("-XX:G1ReservePercent=20");
						list.Add("-XX:G1HeapRegionSize=32M");
						list.Add("-XX:MaxGCPauseMillis=50");
						if (McLaunchJavaSelected.Version.Major <= 7)
						{
							list.Add("-XX:MaxPermSize=512m");
						}
					}
					else
					{
						list.Add("-XX:+UseZGC");
						if (McLaunchJavaSelected.Version.Major == 21 || McLaunchJavaSelected.Version.Major == 22)
						{
							list.Add("-XX:+ZGenerational");
						}
					}
				}
				list.Remove("-XX:MaxDirectMemorySize=256M");
				list.Add("-Dlog4j2.formatMsgNoLookups=true");
				int major = McLaunchJavaSelected.Version.Major;
				string text3;
				if (major >= 21)
				{
					text3 = "UTF-8";
				}
				else
				{
					text3 = ((major < 18) ? McLaunchJavaSelected.FileEncodingName : McLaunchJavaSelected.NativeEncodingName);
				}
				try
				{
					McLaunchEncoding = Encoding.GetEncoding(text3.Equals("GBK", StringComparison.OrdinalIgnoreCase) ? "GB18030" : text3);
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception ex3 = ex2;
					Encoding encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage);
					McLaunchLog(string.Format("无法识别 Java 编码 {0}，已回退为 {1}", text3 ?? "Nothing", encoding.WebName), LogLevel.Warn);
					McLaunchEncoding = encoding;
					ProjectData.ClearProjectError();
				}
				McLaunchLog($"使用编码：{McLaunchEncoding.WebName}");
				string arg2 = ((McLaunchEncoding.CodePage == 54936) ? "GBK" : McLaunchEncoding.WebName);
				CS_8__locals6._VB_Local_StdoutArgName = ((McLaunchJavaSelected.Version.Major < 19) ? "sun.stdout.encoding" : "stdout.encoding");
				CS_8__locals6._VB_Local_StderrArgName = ((McLaunchJavaSelected.Version.Major < 19) ? "sun.stderr.encoding" : "stderr.encoding");
				if (!list.Any([SpecialName] (string a) => a.StartsWithF($"-D{CS_8__locals6._VB_Local_StdoutArgName}=")))
				{
					list.Add($"-D{CS_8__locals6._VB_Local_StdoutArgName}={arg2}");
				}
				if (!list.Any([SpecialName] (string a) => a.StartsWithF($"-D{CS_8__locals6._VB_Local_StderrArgName}=")))
				{
					list.Add($"-D{CS_8__locals6._VB_Local_StderrArgName}={arg2}");
				}
				if (McLaunchJavaSelected.Version.Major >= 18 && McLaunchJavaSelected.Version.Major < 21)
				{
					list.Add("-Dfile.encoding=COMPAT");
				}
				if (flag)
				{
					list.Add("-jar");
					list.Add(LaunchUtils.ExtractPatch("Launch\\JavaWrapper.jar", ModBase.PathPure.Value));
				}
				return DeduplicateJavaArguments(list, IsJVMArgs: true);
			}
		}
		private static List<string> McLaunchArgumentsGame()
		{
			_Closure___52_0 arg = default;
			_Closure___52_0 CS_8__locals16 = new _Closure___52_0(arg);
			CS_8__locals16._VB_Local_Arg = "";
			JToken? jToken = ModMinecraft.McInstanceSelected.JsonObject["minecraftArguments"];
			if (jToken != null && jToken.ToString().Any())
			{
				string text = ModMinecraft.McInstanceSelected.JsonObject["minecraftArguments"].ToString();
				McLaunchLog("旧版游戏参数：" + text);
				text += " --height ${resolution_height} --width ${resolution_width}";
				CS_8__locals16._VB_Local_Arg = CS_8__locals16._VB_Local_Arg + " " + text;
			}
			if (ModMinecraft.McInstanceSelected.JsonObject["arguments"] != null && ModMinecraft.McInstanceSelected.JsonObject["arguments"]["game"] != null)
			{
				ModMinecraft.McInstance mcInstance = ModMinecraft.McInstanceSelected;
				VB_AnonymousDelegate_18<string> obj = [SpecialName] (string a) =>
				{
					CS_8__locals16._VB_Local_Arg = CS_8__locals16._VB_Local_Arg + " " + ((a.Contains(" ") && !a.StartsWithF("\"") && !a.EndsWithF("\"")) ? string.Format("\"{0}\"", a.Replace("\"", "\\\"")) : a);
				};
				while (true)
				{
					McLaunchLog($"新版游戏参数：从版本 JSON 获取（{mcInstance.Name}）");
					if (mcInstance.JsonObject["arguments"] != null && mcInstance.JsonObject["arguments"]["game"] != null)
					{
						foreach (JToken item in (IEnumerable<JToken>)mcInstance.JsonObject["arguments"]["game"])
						{
							switch (item.Type)
							{
							case JTokenType.String:
								obj(item.ToString());
								break;
							case JTokenType.Object:
							{
								JObject jObject = (JObject)item;
								if ((jObject.ContainsKey("rules") && !ModMinecraft.McJsonRuleCheck(jObject["rules"])) || !jObject.ContainsKey("value"))
								{
									break;
								}
								if (jObject["value"].Type == JTokenType.String)
								{
									obj(jObject["value"].ToString());
									break;
								}
								foreach (JToken item2 in (IEnumerable<JToken>)jObject["value"])
								{
									obj(item2.ToString());
								}
								break;
							}
							}
						}
					}
					if (Operators.CompareString(mcInstance.InheritName, "", TextCompare: false) == 0)
					{
						break;
					}
					mcInstance = new ModMinecraft.McInstance(mcInstance.InheritName);
				}
			}
			string text2 = Settings.Get<string>("VersionAdvanceGame", ModMinecraft.McInstanceSelected);
			if (Operators.CompareString(text2, "", TextCompare: false) == 0)
			{
				text2 = Settings.Get<string>("LaunchAdvanceGame");
			}
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				text2 = ModMain.ArgumentReplace(text2);
			}
			CS_8__locals16._VB_Local_Arg = CS_8__locals16._VB_Local_Arg + " " + text2;
			if ((ModMinecraft.McInstanceSelected.Version.HasForge || ModMinecraft.McInstanceSelected.Version.HasLiteLoader) && ModMinecraft.McInstanceSelected.Version.HasOptiFine)
			{
				if (CS_8__locals16._VB_Local_Arg.Contains("--tweakClass optifine.OptiFineForgeTweaker"))
				{
					McLaunchLog("发现正确的 OptiFineForge TweakClass，目前的游戏参数：" + CS_8__locals16._VB_Local_Arg);
					CS_8__locals16._VB_Local_Arg = CS_8__locals16._VB_Local_Arg.Replace(" --tweakClass optifine.OptiFineForgeTweaker", "") + " --tweakClass optifine.OptiFineForgeTweaker";
				}
				if (CS_8__locals16._VB_Local_Arg.Contains("--tweakClass optifine.OptiFineTweaker"))
				{
					McLaunchLog("发现错误的 OptiFineForge TweakClass，目前的游戏参数：" + CS_8__locals16._VB_Local_Arg);
					CS_8__locals16._VB_Local_Arg = CS_8__locals16._VB_Local_Arg.Replace(" --tweakClass optifine.OptiFineTweaker", "") + " --tweakClass optifine.OptiFineForgeTweaker";
					try
					{
						FileUtils.Write(ModMinecraft.McInstanceSelected.GetJsonPath(), FileUtils.ReadAsString(ModMinecraft.McInstanceSelected.GetJsonPath()).Replace("optifine.OptiFineTweaker", "optifine.OptiFineForgeTweaker"));
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "替换 OptiFineForge TweakClass 失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
						ProjectData.ClearProjectError();
					}
				}
			}
			List<string> list = DeduplicateJavaArguments(SplitJavaArguments(CS_8__locals16._VB_Local_Arg).ToList(), IsJVMArgs: false);
			list.AddRange(CurrentLaunchOptions.ExtraGameArgs);
			if (Settings.Get<int>("LaunchArgumentWindowType") == 0)
			{
				list.Add("--fullscreen");
			}
			string text3 = (string.IsNullOrEmpty(CurrentLaunchOptions.ServerIp) ? Settings.Get<string>("VersionServerEnter", ModMinecraft.McInstanceSelected) : CurrentLaunchOptions.ServerIp);
			if (text3.Any())
			{
				if (DateTime.Compare(ModMinecraft.McInstanceSelected.ReleaseTime, new DateTime(2023, 4, 4)) > 0)
				{
					list.Add("--quickPlayMultiplayer");
					list.Add(text3);
				}
				else
				{
					if (text3.Contains(":"))
					{
						list.Add("--server");
						list.Add(text3.Split(":")[0]);
						list.Add("--port");
						list.Add(text3.Split(":")[1]);
					}
					else
					{
						list.Add("--server");
						list.Add(text3);
						list.Add("--port");
						list.Add("25565");
					}
					if (ModMinecraft.McInstanceSelected.Version.HasOptiFine)
					{
						ModMain.Hint("OptiFine 与自动进入服务器可能不兼容，有概率导致材质丢失甚至游戏崩溃！", ModMain.HintType.Red);
					}
				}
			}
			return DeduplicateJavaArguments(list, IsJVMArgs: false);
		}
		private static IEnumerable<(string Key, string Value)> McLaunchArgumentsReplace(List<string> Arguments)
		{
			yield return (Key: "${classpath_separator}", Value: ";");
			yield return (Key: "${natives_directory}", Value: GetNativesFolder().TrimEnd('\\'));
			yield return (Key: "${library_directory}", Value: PathUtils.ToShortPath(ModMinecraft.McFolderSelected + "libraries"));
			yield return (Key: "${libraries_directory}", Value: PathUtils.ToShortPath(ModMinecraft.McFolderSelected + "libraries"));
			yield return (Key: "${pure_directory}", Value: ModBase.PathPure.Value.TrimEnd('\\'));
			yield return (Key: "${launcher_name}", Value: "PCL");
			yield return (Key: "${launcher_version}", Value: Conversions.ToString(410));
			yield return (Key: "${version_name}", Value: ModMinecraft.McInstanceSelected.Name);
			checked
			{
				yield return (Key: "${game_directory}", Value: PathUtils.ToShortPath(Strings.Left(ModMinecraft.McInstanceSelected.PathIndie, ModMinecraft.McInstanceSelected.PathIndie.Count() - 1)));
				yield return (Key: "${assets_root}", Value: PathUtils.ToShortPath(ModMinecraft.McFolderSelected + "assets"));
				yield return (Key: "${user_properties}", Value: "{}");
				yield return (Key: "${auth_player_name}", Value: McLoginLoader.Output.Name);
				yield return (Key: "${auth_uuid}", Value: McLoginLoader.Output.Uuid);
				yield return (Key: "${auth_access_token}", Value: McLoginLoader.Output.AccessToken);
				yield return (Key: "${access_token}", Value: McLoginLoader.Output.AccessToken);
				yield return (Key: "${auth_session}", Value: McLoginLoader.Output.AccessToken);
				yield return (Key: "${user_type}", Value: "msa");
				yield return (Key: "${primary_jar}", Value: PathUtils.ToShortPath(ModMinecraft.McInstanceSelected.PathVersion + ModMinecraft.McInstanceSelected.Name + ".jar"));
				yield return (Key: "${game_assets}", Value: PathUtils.ToShortPath(ModMinecraft.McFolderSelected + "assets\\virtual\\legacy"));
				yield return (Key: "${assets_index_name}", Value: ModMinecraft.McAssetsGetIndexName(ModMinecraft.McInstanceSelected));
				string text = Settings.Get<string>("VersionArgumentInfo", ModMinecraft.McInstanceSelected);
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					text = Settings.Get<string>("LaunchArgumentInfo");
				}
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					if (Arguments.Contains("--versionType"))
					{
						int num = Arguments.IndexOf("--versionType");
						Arguments.RemoveAt(num);
						if (num < Arguments.Count)
						{
							Arguments.RemoveAt(num);
						}
					}
					yield return (Key: "${version_type}", Value: "\"\"");
				}
				else
				{
					yield return (Key: "${version_type}", Value: ModMain.ArgumentReplace(text));
				}
				System.Windows.Size size;
				switch (Settings.Get<int>("LaunchArgumentWindowType"))
				{
				case 2:
				{
					_Closure___53_0 arg = default;
					_Closure___53_0 CS_8__locals2 = new _Closure___53_0(arg);
					ModBase.RunInUiWait([SpecialName] () =>
					{
						CS_8__locals2._VB_Local_Result = new System.Windows.Size(ModBase.GetPixelSize(ModMain.FrmMain.PanForm.ActualWidth), ModBase.GetPixelSize(ModMain.FrmMain.PanForm.ActualHeight));
					});
					size = CS_8__locals2._VB_Local_Result;
					size.Height -= 29.5 * (double)ModBase.DPI / 96.0;
					break;
				}
				case 3:
					size = new System.Windows.Size(Math.Max(100, Settings.Get<int>("LaunchArgumentWindowWidth")), Math.Max(100, Settings.Get<int>("LaunchArgumentWindowHeight")));
					break;
				default:
					size = new System.Windows.Size(854.0, 480.0);
					break;
				}
				if (ModMinecraft.McInstanceSelected.Version.Vanilla.Major <= 12 && McLaunchJavaSelected.Version >= new Version(8, 0, 200) && McLaunchJavaSelected.Version <= new Version(8, 0, 321) && !ModMinecraft.McInstanceSelected.Version.HasOptiFine && !ModMinecraft.McInstanceSelected.Version.HasForge)
				{
					McLaunchLog($"已应用窗口大小过大修复（Java 版本：{McLaunchJavaSelected.Version}）");
					size.Width /= (double)ModBase.DPI / 96.0;
					size.Height /= (double)ModBase.DPI / 96.0;
				}
				yield return (Key: "${resolution_width}", Value: Conversions.ToString(Math.Round(size.Width)));
				yield return (Key: "${resolution_height}", Value: Conversions.ToString(Math.Round(size.Height)));
				int num2 = -1;
				List<string> list = new List<string>();
				string text2 = null;
				List<ModMinecraft.McLibEntry>.Enumerator enumerator = default;
				try
				{
					enumerator = ModMinecraft.McLibListGet(ModMinecraft.McInstanceSelected, IncludeMainJar: true).GetEnumerator();
					while (enumerator.MoveNext())
					{
						ModMinecraft.McLibEntry current = enumerator.Current;
						if (!current.IsNatives)
						{
							if (current.Name != null && Operators.CompareString(current.Name, "optifine:OptiFine", TextCompare: false) == 0)
							{
								text2 = current.LocalPath;
							}
							else
							{
								list.Add(current.LocalPath);
							}
						}
					}
				}
				finally
				{
					if (num2 < 0)
					{
						((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
					}
				}
				if (text2 != null)
				{
					list.Insert(list.Count - 2, text2);
				}
				yield return (Key: "${classpath}", Value: list.Select([SpecialName] (string c) => PathUtils.ToShortPath(c)).Join(';'));
			}
		}
		private static IEnumerable<string> SplitJavaArguments(string Arg)
		{
			Arg = Arg.ReplaceLineEndings(" ", mergeMultiple: true).Trim();
			if (string.IsNullOrWhiteSpace(Arg))
			{
				yield break;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			checked
			{
				for (int i = 0; i < Arg.Length; i++)
				{
					char c = Arg[i];
					if (c == '\\' && i + 1 < Arg.Length && Arg[i + 1] == '"')
					{
						stringBuilder.Append("\\\"");
						i++;
						continue;
					}
					switch (c)
					{
					case '"':
						flag = !flag;
						stringBuilder.Append(c);
						continue;
					case ' ':
						if (!flag)
						{
							if (stringBuilder.Length > 0)
							{
								yield return stringBuilder.ToString();
								stringBuilder.Clear();
							}
							continue;
						}
						break;
					}
					stringBuilder.Append(c);
				}
				if (stringBuilder.Length > 0)
				{
					yield return stringBuilder.ToString();
				}
			}
		}
		private static List<string> DeduplicateJavaArguments(List<string> Args, bool IsJVMArgs)
		{
			List<string> list = new List<string>();
			int num = 0;
			checked
			{
				while (num < Args.Count)
				{
					string text = Args[num];
					if (!text.StartsWithF("-") || num + 1 >= Args.Count || (Args[num + 1].StartsWithF("-") && ModBase.Val(Args[num + 1].AfterFirst("-")) == 0.0))
					{
						num++;
						if (list.Contains(text))
						{
							McLaunchLog(string.Format("已去重单个{0}参数：{1}", IsJVMArgs ? " JVM " : "游戏", text));
						}
						else
						{
							list.Add(text);
						}
						continue;
					}
					string text2 = Args[num + 1];
					num += 2;
					int num2 = list.Count - 2;
					int num3 = 0;
					while (true)
					{
						if (num3 <= num2)
						{
							if (Operators.CompareString(list[num3], text, TextCompare: false) == 0)
							{
								if (!IsJVMArgs && Operators.CompareString(text, "--tweakClass", TextCompare: false) != 0)
								{
									McLaunchLog(string.Format("已覆盖重复的{0}参数：{1} {2} → {3} {4}", IsJVMArgs ? " JVM " : "游戏", text, list[num3 + 1], text, text2));
									list[num3 + 1] = text2;
									break;
								}
								if (Operators.CompareString(list[num3 + 1], text2, TextCompare: false) == 0)
								{
									McLaunchLog(string.Format("已去重{0}参数对：{1} {2}", IsJVMArgs ? " JVM " : "游戏", text, text2));
									break;
								}
							}
							num3++;
							continue;
						}
						list.Add(text);
						list.Add(text2);
						break;
					}
				}
				return list;
			}
		}
		private static void McLaunchNatives(ModLoader.LoaderTask<int, int> Loader)
		{
			string nativesFolder = GetNativesFolder();
			DirectoryUtils.Create(nativesFolder);
			McLaunchLog("正在解压 Natives 文件");
			List<string> list = new List<string>();
			foreach (ModMinecraft.McLibEntry item in ModMinecraft.McLibListGet(ModMinecraft.McInstanceSelected, IncludeMainJar: true))
			{
				if (!item.IsNatives)
				{
					continue;
				}
				ZipArchive zipArchive;
				try
				{
					zipArchive = FileUtils.OpenZip(item.LocalPath);
				}
				catch (InvalidDataException ex)
				{
					ProjectData.SetProjectError(ex);
					InvalidDataException ex2 = ex;
					Logger.Warn(ex2, $"打开 Natives 文件失败（{item.LocalPath}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					FileUtils.Delete(item.LocalPath);
					throw new Exception("无法打开 Natives 文件（" + item.LocalPath + "），该文件可能已损坏，请重新尝试启动游戏");
				}
				foreach (ZipArchiveEntry entry in zipArchive.Entries)
				{
					string fullName = entry.FullName;
					if (!fullName.EndsWithF(".dll", ignoreCase: true))
					{
						continue;
					}
					string text = nativesFolder + fullName;
					list.Add(text);
					FileInfo info = FileUtils.GetInfo(text);
					if (info.Exists)
					{
						if (info.Length == entry.Length)
						{
							if (ModBase.ModeDebug)
							{
								McLaunchLog("无需解压：" + text);
							}
							continue;
						}
						try
						{
							FileUtils.Delete(text);
						}
						catch (UnauthorizedAccessException ex3)
						{
							ProjectData.SetProjectError(ex3);
							UnauthorizedAccessException ex4 = ex3;
							McLaunchLog("删除原 dll 访问被拒绝，这通常代表有一个 MC 正在运行，跳过解压：" + text);
							McLaunchLog("实际的错误信息：" + ex4.GetDisplay(multiline: true));
							ProjectData.ClearProjectError();
							break;
						}
					}
					FileUtils.Write(text, entry.Open());
					McLaunchLog("已解压：" + text);
				}
				zipArchive?.Dispose();
			}
			foreach (string item2 in DirectoryUtils.EnumerateFiles(nativesFolder))
			{
				if (!list.Contains(item2))
				{
					try
					{
						McLaunchLog("删除：" + item2);
						FileUtils.Delete(item2);
					}
					catch (UnauthorizedAccessException ex5)
					{
						ProjectData.SetProjectError(ex5);
						UnauthorizedAccessException ex6 = ex5;
						McLaunchLog("删除多余文件访问被拒绝，跳过删除步骤");
						McLaunchLog("实际的错误信息：" + ex6.GetDisplay(multiline: true));
						ProjectData.ClearProjectError();
						break;
					}
				}
			}
		}
		private static string GetNativesFolder()
		{
			string text = PathUtils.AddSlashSuffix(PathUtils.ToShortPath(ModMinecraft.McInstanceSelected.PathVersion)) + ModMinecraft.McInstanceSelected.Name + "-natives\\";
			if (!ModBase.IsGBKEncoding && !text.IsAsciiOnly())
			{
				text = Paths.AppData + ".minecraft\\bin\\natives\\";
				if (!text.IsAsciiOnly())
				{
					text = ModBase.OsDrive + "ProgramData\\PCL\\natives\\";
				}
			}
			DirectoryUtils.Create(text);
			return PathUtils.AddSlashSuffix(PathUtils.ToShortPath(text));
		}
		private static void McLaunchPrerun()
		{
			if (Settings.Get<bool>("LaunchAdvanceGraphicCard"))
			{
				try
				{
					ModMain.SetGPUPreference(McLaunchJavaSelected.JavaExePath);
					ModMain.SetGPUPreference(ModBase.PathExe);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (WindowsUtils.HasAdminRole())
					{
						Logger.Warn(ex2, "直接调整显卡设置失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					}
					else
					{
						Logger.Warn(ex2, "直接调整显卡设置失败，将以管理员权限重启 PCL 再次尝试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
						try
						{
							if (ModBase.RunAsAdmin($"--gpu \"{McLaunchJavaSelected.JavaExePath}\"") != 5)
							{
								throw new Exception("调整过程中出现异常");
							}
							McLaunchLog("以管理员权限重启 PCL 并调整显卡设置成功");
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Error(ex4, "调整显卡设置失败，Minecraft 可能会使用默认显卡运行", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
							ProjectData.ClearProjectError();
						}
					}
					ProjectData.ClearProjectError();
				}
			}
			try
			{
				if (Operators.CompareString(McLoginLoader.Output.Type, "Microsoft", TextCompare: false) == 0)
				{
					ModMinecraft.McFolderLauncherProfilesJsonCreate(ModMinecraft.McFolderSelected);
					JObject content = (JObject)("\r\n            {\r\n              \"authenticationDatabase\": {\r\n                \"00000111112222233333444445555566\": {\r\n                  \"username\": \"" + McLoginLoader.Output.Name.Replace("\"", "-") + "\",\r\n                  \"profiles\": {\r\n                    \"66666555554444433333222221111100\": {\r\n                        \"displayName\": \"" + McLoginLoader.Output.Name + "\"\r\n                    }\r\n                  }\r\n                }\r\n              },\r\n              \"clientToken\": \"" + McLoginLoader.Output.ClientToken + "\",\r\n              \"selectedUser\": {\r\n                \"account\": \"00000111112222233333444445555566\", \r\n                \"profile\": \"66666555554444433333222221111100\"\r\n              }\r\n            }").DeserializeJson();
					JObject jObject = (JObject)FileUtils.ReadAsJson(ModMinecraft.McFolderSelected + "launcher_profiles.json");
					jObject.Merge(content);
					FileUtils.Write(ModMinecraft.McFolderSelected + "launcher_profiles.json", jObject.ToString(), Encoding.GetEncoding("GB18030"));
					McLaunchLog("已更新 launcher_profiles.json");
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Warn(ex6, "更新 launcher_profiles.json 失败，将在删除文件后重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				try
				{
					FileUtils.Delete(ModMinecraft.McFolderSelected + "launcher_profiles.json");
					ModMinecraft.McFolderLauncherProfilesJsonCreate(ModMinecraft.McFolderSelected);
					JObject content2 = (JObject)("\r\n                    {\r\n                      \"authenticationDatabase\": {\r\n                        \"00000111112222233333444445555566\": {\r\n                          \"username\": \"" + McLoginLoader.Output.Name.Replace("\"", "-") + "\",\r\n                          \"profiles\": {\r\n                            \"66666555554444433333222221111100\": {\r\n                                \"displayName\": \"" + McLoginLoader.Output.Name + "\"\r\n                            }\r\n                          }\r\n                        }\r\n                      },\r\n                      \"clientToken\": \"" + McLoginLoader.Output.ClientToken + "\",\r\n                      \"selectedUser\": {\r\n                        \"account\": \"00000111112222233333444445555566\", \r\n                        \"profile\": \"66666555554444433333222221111100\"\r\n                      }\r\n                    }").DeserializeJson();
					JObject jObject2 = (JObject)FileUtils.ReadAsJson(ModMinecraft.McFolderSelected + "launcher_profiles.json");
					jObject2.Merge(content2);
					FileUtils.Write(ModMinecraft.McFolderSelected + "launcher_profiles.json", jObject2.ToString(), Encoding.GetEncoding("GB18030"));
					McLaunchLog("已在删除后更新 launcher_profiles.json");
				}
				catch (Exception ex7)
				{
					ProjectData.SetProjectError(ex7);
					Exception ex8 = ex7;
					Logger.Error(ex8, "更新 launcher_profiles.json 失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
			string text = ModMinecraft.McInstanceSelected.PathIndie + "options.txt";
			if (!FileUtils.Exists(text))
			{
				string text2 = ModMinecraft.McInstanceSelected.PathIndie + "config\\yosbr\\options.txt";
				if (FileUtils.Exists(text2))
				{
					McLaunchLog("将修改 Yosbr Mod 中的 options.txt");
					text = text2;
					ModBase.WriteIni(text, "lang", "none");
				}
			}
			try
			{
				string text3 = ModBase.ReadIni(text, "lang", "none");
				string text4;
				if (Operators.CompareString(text3, "none", TextCompare: false) != 0 && DirectoryUtils.Exists(ModMinecraft.McInstanceSelected.PathIndie + "saves"))
				{
					text4 = text3.Lower();
				}
				else
				{
					text4 = (Settings.Get<bool>("ToolHelpChinese") ? "zh_cn" : "en_us");
				}
				if (!ModMinecraft.McInstanceSelected.Version.Vaild || ModMinecraft.McInstanceSelected.Version.Vanilla.Major <= 10)
				{
					text4 = checked(text4.Substring(0, text4.Length - 2) + text4.Substring(text4.Length - 2).Upper());
				}
				if (Operators.CompareString(text3, text4, TextCompare: false) == 0)
				{
					McLaunchLog($"需要的语言为 {text4}，当前语言为 {text3}，无需修改");
				}
				else
				{
					ModBase.WriteIni(text, "lang", "-");
					ModBase.WriteIni(text, "lang", text4);
					McLaunchLog($"已将语言从 {text3} 修改为 {text4}");
				}
				switch (Settings.Get<int>("LaunchArgumentWindowType"))
				{
				case 0:
					ModBase.WriteIni(text, "fullscreen", "true");
					break;
				default:
					ModBase.WriteIni(text, "fullscreen", "false");
					break;
				case 1:
					break;
				}
			}
			catch (Exception ex9)
			{
				ProjectData.SetProjectError(ex9);
				Exception ex10 = ex9;
				Logger.Error(ex10, "更新 options.txt 失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				ProjectData.ClearProjectError();
			}
			if (ModMinecraft.McInstanceSelected.Version.Vanilla.Major <= 7 && ModMinecraft.McInstanceSelected.Version.Vanilla.Major >= 2 && McLoginLoader.Input.Type == McLoginType.Legacy && (Settings.Get<int>("LaunchSkinType") == 2 || (Settings.Get<int>("LaunchSkinType") == 4 && Settings.Get<bool>("LaunchSkinSlim"))))
			{
				ModMain.Hint("此 Minecraft 版本尚不支持 Alex 皮肤，你的皮肤可能会显示为 Steve！", ModMain.HintType.Red);
			}
			try
			{
				string text5 = ModMinecraft.McInstanceSelected.PathIndie + "resourcepacks\\PCL2 Skin.zip";
				bool flag = ModMinecraft.McInstanceSelected.Version.Vanilla.Major >= 13 || ModMinecraft.McInstanceSelected.Version.Vanilla.Major < 6;
				if (McLoginLoader.Input.Type == McLoginType.Legacy && Settings.Get<int>("LaunchSkinType") == 4 && FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
				{
					string text6 = ModBase.PathTemp + "pack.mcmeta";
					string text7 = ModBase.PathTemp + "pack.png";
					int num;
					switch (ModMinecraft.McInstanceSelected.Version.Vanilla.Major)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						McLaunchLog("Minecraft 版本过老，尚不支持自定义离线皮肤");
						return;
					case 6:
					case 7:
					case 8:
						num = 1;
						break;
					case 9:
					case 10:
						num = 2;
						break;
					case 11:
					case 12:
						num = 3;
						break;
					case 13:
					case 14:
						num = 4;
						break;
					case 15:
						num = 5;
						break;
					case 16:
						num = 6;
						break;
					case 17:
						num = 7;
						break;
					case 18:
						num = ((ModMinecraft.McInstanceSelected.Version.Vanilla.Minor > 2) ? 9 : 8);
						break;
					case 19:
						num = ((ModMinecraft.McInstanceSelected.Version.Vanilla.Minor > 3) ? 12 : 9);
						break;
					case 20:
						num = ((ModMinecraft.McInstanceSelected.Version.Vanilla.Minor > 1) ? 17 : 15);
						break;
					default:
						num = 17;
						break;
					}
					McLaunchLog("正在构建自定义皮肤资源包，格式为：" + Conversions.ToString(num));
					new MyBitmap(ModBase.PathImage + "Heads/Logo.png").Save(text7);
					FileUtils.Write(text6, "{\"pack\":{\"pack_format\":" + Conversions.ToString(num) + ",\"description\":\"PCL 自定义离线皮肤资源包\"}}");
					MyBitmap myBitmap = new MyBitmap(Paths.AppDataThenName + "CustomSkin.png");
					if ((ModMinecraft.McInstanceSelected.Version.Vanilla.Major == 6 || ModMinecraft.McInstanceSelected.Version.Vanilla.Major == 7) && myBitmap.Pic.Height == 64)
					{
						McLaunchLog("该 Minecraft 版本不支持双层皮肤，已进行裁剪");
						myBitmap = myBitmap.Clip(0, 0, 64, 32);
					}
					myBitmap.Save(Paths.Base + "PCL\\CustomSkin_Cliped.png");
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{ "pack.mcmeta", text6 },
						{ "pack.png", text7 }
					};
					if (ModMinecraft.McInstanceSelected.Version.Vanilla >= new Version(19, 0, 3))
					{
						string[] array = new string[9] { "alex", "ari", "efe", "kai", "makena", "noor", "steve", "sunny", "zuri" };
						foreach (string arg in array)
						{
							dictionary.Add(string.Format("assets/minecraft/textures/entity/player/{0}/{1}.png", Settings.Get<bool>("LaunchSkinSlim") ? "slim" : "wide", arg), Paths.Base + "PCL\\CustomSkin_Cliped.png");
						}
					}
					else
					{
						dictionary.Add(string.Format("assets/minecraft/textures/entity/{0}", Settings.Get<bool>("LaunchSkinSlim") ? "alex.png" : "steve.png"), Paths.Base + "PCL\\CustomSkin_Cliped.png");
					}
					FileUtils.CreateZipFromFiles(text5, dictionary);
					FileUtils.Delete(Paths.Base + "PCL\\CustomSkin_Cliped.png");
					ModBase.IniClearCache(text);
					string text8 = ModBase.ReadIni(text, "resourcePacks", "[]").TrimStart('[').TrimEnd(']');
					if (flag)
					{
						if (Operators.CompareString(text8, "", TextCompare: false) == 0)
						{
							text8 = "\"vanilla\"";
						}
						List<string> list = new List<string>(text8.Split(","));
						List<string> list2 = new List<string>();
						foreach (string item in list)
						{
							switch (item)
							{
							case "\"file/PCL2 Skin.zip\"":
							case null:
							case "":
								continue;
							}
							list2.Add(item);
						}
						list2.Add("\"file/PCL2 Skin.zip\"");
						string value = "[" + list2.Join(',') + "]";
						ModBase.WriteIni(text, "resourcePacks", value);
						return;
					}
					List<string> list3 = new List<string>(text8.Split(","));
					List<string> list4 = new List<string>();
					foreach (string item2 in list3)
					{
						switch (item2)
						{
						case "\"PCL2 Skin.zip\"":
						case null:
						case "":
							continue;
						}
						list4.Add(item2);
					}
					list4.Add("\"PCL2 Skin.zip\"");
					string value2 = "[" + list4.Join(',') + "]";
					ModBase.WriteIni(text, "resourcePacks", value2);
				}
				else
				{
					if (!FileUtils.Exists(text5))
					{
						return;
					}
					McLaunchLog("正在清空自定义皮肤资源包");
					FileUtils.Delete(text5);
					ModBase.IniClearCache(text);
					string text9 = ModBase.ReadIni(text, "resourcePacks", "[]").TrimStart('[').TrimEnd(']');
					if (flag)
					{
						if (Operators.CompareString(text9, "", TextCompare: false) == 0)
						{
							text9 = "\"vanilla\"";
						}
						List<string> list5 = new List<string>(text9.Split(","));
						list5.Remove("\"file/PCL2 Skin.zip\"");
						string value3 = "[" + list5.Join(',') + "]";
						ModBase.WriteIni(text, "resourcePacks", value3);
					}
					else
					{
						List<string> list6 = new List<string>(text9.Split(","));
						list6.Remove("\"PCL2 Skin.zip\"");
						string value4 = "[" + list6.Join(',') + "]";
						ModBase.WriteIni(text, "resourcePacks", value4);
					}
				}
			}
			catch (Exception ex11)
			{
				ProjectData.SetProjectError(ex11);
				Exception ex12 = ex11;
				Logger.Error(ex12, "离线皮肤资源包设置失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static void McLaunchCustom(ModLoader.LoaderTask<int, int> Loader)
		{
			string text = Settings.Get<string>("LaunchAdvanceRun");
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				text = ModMain.ArgumentReplace(text);
			}
			string text2 = Settings.Get<string>("VersionAdvanceRun", ModMinecraft.McInstanceSelected);
			if (Operators.CompareString(text2, "", TextCompare: false) != 0)
			{
				text2 = ModMain.ArgumentReplace(text2);
			}
			try
			{
				string raw = $"chcp {((McLaunchEncoding.CodePage == 54936 && ModBase.IsGBKEncoding) ? 936 : McLaunchEncoding.CodePage)}>nul" + "\r\n@echo off\r\n" + $"title 启动 - {ModMinecraft.McInstanceSelected.Name}" + "\r\necho 游戏正在启动，请稍候。\r\n" + $"cd /D \"{PathUtils.ToShortPath(ModMinecraft.McInstanceSelected.PathIndie)}\"" + "\r\n" + text + "\r\n" + text2 + "\r\n" + $"\"{McLaunchJavaSelected.JavaExePath}\" {McLaunchArgument}" + "\r\necho 游戏已退出。\r\npause";
				FileUtils.Write(CurrentLaunchOptions.SaveBatch ?? (Paths.Base + "PCL\\LatestLaunch.bat"), ModMinecraft.FilterAccessToken(raw, 'F').Replace("%", "%%"), McLaunchEncoding);
				if (CurrentLaunchOptions.SaveBatch != null)
				{
					McLaunchLog("导出启动脚本完成，强制结束启动过程");
					CanceledHint = "导出启动脚本成功！";
					ModBase.OpenExplorer(CurrentLaunchOptions.SaveBatch);
					Loader.Parent.Cancel();
					return;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "输出启动脚本失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				if (CurrentLaunchOptions.SaveBatch != null)
				{
					throw ex2;
				}
				ProjectData.ClearProjectError();
			}
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				McLaunchLog("正在执行全局自定义命令：" + text);
				Process process = null;
				try
				{
					process = ModBase.StartProcess(new ProcessStartInfo
					{
						FileName = "cmd.exe",
						Arguments = $"/c \"{text}\"",
						WorkingDirectory = ModMinecraft.McFolderSelected,
						UseShellExecute = false,
						CreateNoWindow = true
					});
					if (Settings.Get<bool>("LaunchAdvanceRunWait"))
					{
						while (!process.HasExited && !Loader.IsCanceled)
						{
							Thread.Sleep(10);
						}
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Error(ex4, "执行全局自定义命令失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					ProjectData.ClearProjectError();
				}
				finally
				{
					if (process != null && !process.HasExited && Loader.IsCanceled)
					{
						McLaunchLog("由于取消启动，已强制结束自定义命令 CMD 进程");
						process.Kill();
					}
				}
			}
			if (Operators.CompareString(text2, "", TextCompare: false) == 0)
			{
				return;
			}
			McLaunchLog("正在执行版本自定义命令：" + text2);
			Process process2 = null;
			try
			{
				process2 = ModBase.StartProcess(new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = $"/c \"{text2}\"",
					WorkingDirectory = ModMinecraft.McFolderSelected,
					UseShellExecute = false,
					CreateNoWindow = true
				});
				if (Settings.Get<bool>("VersionAdvanceRunWait", ModMinecraft.McInstanceSelected))
				{
					while (!process2.HasExited && !Loader.IsCanceled)
					{
						Thread.Sleep(10);
					}
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "执行版本自定义命令失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (process2 != null && !process2.HasExited && Loader.IsCanceled)
				{
					McLaunchLog("由于取消启动，已强制结束自定义命令 CMD 进程");
					process2.Kill();
				}
			}
		}
		private static void McLaunchRun(ModLoader.LoaderTask<int, Process> Loader)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo(McLaunchJavaSelected.JavaExePath);
			string text = processStartInfo.EnvironmentVariables["Path"];
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(text))
			{
				list.AddRange(text.Split(";"));
			}
			list.Add(PathUtils.ToShortPath(McLaunchJavaSelected.Folder));
			processStartInfo.EnvironmentVariables["Path"] = list.Distinct().Join(';');
			processStartInfo.EnvironmentVariables["appdata"] = PathUtils.ToShortPath(ModMinecraft.McFolderSelected);
			processStartInfo.WorkingDirectory = ModMinecraft.McInstanceSelected.PathIndie;
			processStartInfo.UseShellExecute = false;
			processStartInfo.StandardOutputEncoding = McLaunchEncoding;
			processStartInfo.StandardErrorEncoding = McLaunchEncoding;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.Arguments = McLaunchArgument;
			Process process = ModBase.StartProcess(processStartInfo);
			McLaunchLog("已启动游戏进程：" + McLaunchJavaSelected.JavaExePath);
			if (Loader.IsCanceled)
			{
				McLaunchLog("由于取消启动，已强制结束游戏进程");
				process.Kill();
				return;
			}
			Loader.Output = process;
			McLaunchProcess = process;
			try
			{
				process.PriorityBoostEnabled = true;
				switch (Settings.Get<int>("LaunchArgumentPriority"))
				{
				case 0:
					process.PriorityClass = ProcessPriorityClass.AboveNormal;
					break;
				case 2:
					process.PriorityClass = ProcessPriorityClass.BelowNormal;
					break;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "设置进程优先级失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static void McLaunchWait(ModLoader.LoaderTask<Process, int> Loader)
		{
			McLaunchLog("");
			McLaunchLog("~ 基础参数 ~");
			McLaunchLog("PCL 版本：快照版 2.13.1.1 (" + Conversions.ToString(410) + ")");
			McLaunchLog(string.Format("游戏版本：{0}（{1}，Drop {2}{3}）", ModMinecraft.McInstanceSelected.VersionDisplayName(), ModMinecraft.McInstanceSelected.Version.Vanilla, ModMinecraft.McInstanceSelected.Version.Drop, ModMinecraft.McInstanceSelected.Version.Reliable ? "" : "，无法完全确定"));
			McLaunchLog("资源版本：" + ModMinecraft.McAssetsGetIndexName(ModMinecraft.McInstanceSelected));
			McLaunchLog("版本继承：" + ((Operators.CompareString(ModMinecraft.McInstanceSelected.InheritName, "", TextCompare: false) == 0) ? "无" : ModMinecraft.McInstanceSelected.InheritName));
			McLaunchLog("分配的内存：" + Conversions.ToString(PageInstanceSetup.GetRam(ModMinecraft.McInstanceSelected)) + " GB（" + Conversions.ToString(Math.Round(PageInstanceSetup.GetRam(ModMinecraft.McInstanceSelected) * 1024.0)) + " MB）");
			McLaunchLog("MC 文件夹：" + ModMinecraft.McFolderSelected);
			McLaunchLog("版本文件夹：" + ModMinecraft.McInstanceSelected.PathVersion);
			McLaunchLog("版本隔离：" + Conversions.ToString(Operators.CompareString(ModMinecraft.McInstanceSelected.PathIndie, ModMinecraft.McInstanceSelected.PathVersion, TextCompare: false) == 0));
			McLaunchLog("HMCL 格式：" + Conversions.ToString(ModMinecraft.McInstanceSelected.IsHmclFormatJson));
			McLaunchLog("Java 信息：" + ((McLaunchJavaSelected != null) ? McLaunchJavaSelected.ToString() : "无可用 Java"));
			McLaunchLog("Natives 文件夹：" + GetNativesFolder());
			McLaunchLog("");
			McLaunchLog("~ 登录参数 ~");
			McLaunchLog("玩家用户名：" + McLoginLoader.Output.Name);
			McLaunchLog("AccessToken：" + McLoginLoader.Output.AccessToken);
			McLaunchLog("ClientToken：" + McLoginLoader.Output.ClientToken);
			McLaunchLog("UUID：" + McLoginLoader.Output.Uuid);
			McLaunchLog("登录方式：" + McLoginLoader.Output.Type);
			McLaunchLog("");
			string text = Settings.Get<string>("VersionArgumentTitle", ModMinecraft.McInstanceSelected);
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = Settings.Get<string>("LaunchArgumentTitle");
			}
			text = ModMain.ArgumentReplace(text, null, ReplaceTime: false);
			ModWatcher.Watcher watcher = (McLaunchWatcher = new ModWatcher.Watcher(Loader, ModMinecraft.McInstanceSelected, text));
			while (watcher.State == ModWatcher.Watcher.MinecraftState.Loading)
			{
				Thread.Sleep(100);
			}
			if (watcher.State == ModWatcher.Watcher.MinecraftState.Crashed)
			{
				throw new OperationCanceledException();
			}
		}
		private static void McLaunchEnd()
		{
			McLaunchLog("开始启动结束处理");
			if (Settings.Get<bool>("UiMusicStop"))
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					if (ModMusic.MusicPause())
					{
						Logger.Info("已根据设置，在启动后暂停音乐播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					}
				});
			}
			else if (Settings.Get<bool>("UiMusicStart"))
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					if (ModMusic.MusicResume())
					{
						Logger.Info("已根据设置，在启动后开始音乐播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModLaunch.vb");
					}
				});
			}
			McLaunchLog("启动器可见性：" + Conversions.ToString(Settings.Get<int>("LaunchArgumentVisible")));
			switch (Settings.Get<int>("LaunchArgumentVisible"))
			{
			case 0:
				McLaunchLog("已根据设置，在启动后关闭启动器");
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.EndProgram(SendWarning: false);
				});
				break;
			case 2:
			case 3:
				McLaunchLog("已根据设置，在启动后隐藏启动器");
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.Hidden = true;
				});
				break;
			case 4:
				McLaunchLog("已根据设置，在启动后最小化启动器");
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.WindowState = WindowState.Minimized;
				});
				break;
			}
			Settings.Set("SystemLaunchCount", checked(Settings.Get<int>("SystemLaunchCount") + 1));
		}
	}
}
