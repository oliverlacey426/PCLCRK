// PCL

namespace PCL
{
	public partial class Application : System.Windows.Application
	{
		private static Mutex PclMutex;
		[SpecialName]
		private string[] _STATIC_ctor_2001_Prefixes;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_ctor_2001_Prefixes_Init;
		[SpecialName]
		private ConcurrentDictionary<string, Lazy<Assembly>> _STATIC_ctor_2001_LoadedAssemblies;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_ctor_2001_LoadedAssemblies_Init;
		internal AssemblyInfo Info
		{
			[DebuggerHidden]
			get
			{
				return new AssemblyInfo(Assembly.GetExecutingAssembly());
			}
		}
		private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
		{
			checked
			{
				try
				{
					MeloongCore.Main.Init("PCL");
					MeloongCore.Wpf.Main.Init();
					Logger.Instance = new PclLogger
					{
						logFolder = Paths.BaseThenName,
						MinLevel = ((!ModBase.ModeDebug) ? LogLevel.Info : LogLevel.Trace)
					};
					Thread.CurrentThread.Priority = ThreadPriority.Highest;
					try
					{
						if (Conversions.ToInteger(RegistryUtils.TryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full", "Release", 999999)) < 528040)
						{
							OldEnvironmentAssert();
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "检查 .NET Framework 版本失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
						ProjectData.ClearProjectError();
					}
					try
					{
						new FormattedText("", CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Fonts.SystemTypefaces.First(), 96.0, new ModBase.MyColor(), ModBase.DPI);
					}
					catch (UriFormatException ex3)
					{
						ProjectData.SetProjectError(ex3);
						UriFormatException ex4 = ex3;
						Environment.SetEnvironmentVariable("windir", Environment.GetEnvironmentVariable("SystemRoot"), EnvironmentVariableTarget.User);
						new FormattedText("", CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Fonts.SystemTypefaces.First(), 96.0, new ModBase.MyColor(), ModBase.DPI);
						ProjectData.ClearProjectError();
					}
					if (e.Args.Length > 0)
					{
						if (Operators.CompareString(e.Args[0], "--update", TextCompare: false) == 0)
						{
							ModSecret.UpdateReplace(Conversions.ToInteger(e.Args[1]), e.Args[2].Trim('"'), e.Args[3].Trim('"'), Conversions.ToBoolean(e.Args[4]));
							Environment.Exit(5);
						}
						else if (Operators.CompareString(e.Args[0], "--gpu", TextCompare: false) == 0)
						{
							try
							{
								ModMain.SetGPUPreference(e.Args[1].Trim('"'));
								Environment.Exit(5);
							}
							catch (Exception ex5)
							{
								ProjectData.SetProjectError(ex5);
								Exception ex6 = ex5;
								Environment.Exit(1);
								ProjectData.ClearProjectError();
							}
						}
						else if (e.Args[0].StartsWithF("--memory"))
						{
							ulong availablePhysicalMemory = MyWpfExtension.Computer.Info.AvailablePhysicalMemory;
							try
							{
								PageOtherTest.MemoryOptimizeInternal(ShowHint: false);
							}
							catch (Exception ex7)
							{
								ProjectData.SetProjectError(ex7);
								Exception ex8 = ex7;
								Interaction.MsgBox(ex8.Message, MsgBoxStyle.Critical, "内存优化失败");
								Environment.Exit(-1);
								ProjectData.ClearProjectError();
							}
							if (MyWpfExtension.Computer.Info.AvailablePhysicalMemory < availablePhysicalMemory)
							{
								Environment.Exit(0);
							}
							else
							{
								Environment.Exit((int)Math.Round((double)(MyWpfExtension.Computer.Info.AvailablePhysicalMemory - availablePhysicalMemory) / 1024.0));
							}
						}
					}
					try
					{
						DirectoryUtils.Create(Paths.Base + "PCL\\Pictures\\");
						DirectoryUtils.Create(Paths.Base + "PCL\\Musics\\");
						ModBase.CheckPermissionWithException(Paths.Base + "PCL\\");
					}
					catch (Exception ex9)
					{
						ProjectData.SetProjectError(ex9);
						Exception ex10 = ex9;
						Interaction.MsgBox($"PCL 没有对当前文件夹的权限（{Paths.Base}PCL\\），请尝试：" + "\r\n1. 将 PCL 移动到其他文件夹" + (Paths.Base.StartsWithF("C:", ignoreCase: true) ? "，例如 C 盘和桌面以外的其他位置。" : "。") + "\r\n2. 删除当前目录中的 PCL 文件夹，然后再试。\r\n3. 右键 PCL 选择属性，打开 兼容性 中的 以管理员身份运行此程序。", MsgBoxStyle.Critical, "运行环境错误");
						Environment.Exit(4);
						ProjectData.ClearProjectError();
					}
					while (true)
					{
						try
						{
							DirectoryUtils.Create(ModBase.PathTemp);
							ModBase.CheckPermissionWithException(ModBase.PathTemp);
						}
						catch (Exception ex11)
						{
							ProjectData.SetProjectError(ex11);
							Exception ex12 = ex11;
							if (Operators.CompareString(ModBase.PathTemp, System.IO.Path.GetTempPath() + "PCL\\", TextCompare: false) == 0)
							{
								ModMain.MyMsgBox("PCL 无法访问缓存文件夹，可能导致程序出错或无法正常使用！\r\n\r\n错误原因：" + ex12.GetDisplay(multiline: true), "缓存文件夹不可用");
								ProjectData.ClearProjectError();
								break;
							}
							ModMain.MyMsgBox("手动设置的缓存文件夹不可用，PCL 将使用默认缓存文件夹。\r\n\r\n错误原因：" + ex12.GetDisplay(multiline: true), "缓存文件夹不可用");
							Settings.Set("SystemSystemCache", "");
							ModBase.PathTemp = System.IO.Path.GetTempPath() + "PCL\\";
							ProjectData.ClearProjectError();
							continue;
						}
						break;
					}
					DirectoryUtils.Create(ModBase.PathTemp + "Cache\\");
					DirectoryUtils.Create(Paths.AppDataThenName);
					WaitingMutex(e);
					ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(100));
					ServicePointManager.Expect100Continue = false;
					ServicePointManager.DefaultConnectionLimit = 10000;
					ServicePointManager.UseNagleAlgorithm = false;
					ServicePointManager.EnableDnsRoundRobin = true;
					ServicePointManager.ReusePort = true;
					if (Environment.OSVersion.Version < new Version(6, 2))
					{
						ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
					}
					if (Settings.Get<bool>("UiLauncherLogo"))
					{
						ModMain.FrmStart = new SplashScreen("Images\\icon.ico");
						ModMain.FrmStart.Show(autoClose: false, topMost: true);
					}
					Logger.Info(string.Format("程序版本：{0} ({1}{2})", "快照版 2.13.1.1", 410, ""), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					Logger.Info(string.Format("识别码：{0}{1}", ModBase.Identify, ModSecret.ThemeCheckOne(9) ? "，已解锁反馈主题" : "，未解锁反馈主题"), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					Logger.Info($"程序路径：{ModBase.PathExe}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					Logger.Info($"系统编码：{Encoding.Default.HeaderName} ({Encoding.Default.CodePage}, GBK={ModBase.IsGBKEncoding})", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					Logger.Info($"管理员权限：{WindowsUtils.HasAdminRole()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					if (Paths.Base.Contains(System.IO.Path.GetTempPath()) || Paths.Base.Contains("AppData\\Local\\Temp\\"))
					{
						ModMain.MyMsgBox("请将 PCL 从压缩包中解压后再使用！\r\n如果不会解压，可以在网上寻找教程。", "需要解压！", "我知道了", "", "", IsWarn: true, HighLight: true, ForceWait: true);
						FormMain.EndProgramForce(ModBase.ProcessReturnValues.Cancel);
					}
					if (!Environment.Is64BitOperatingSystem)
					{
						ModMain.MyMsgBox("PCL 和新版 Minecraft 均不再支持 32 位系统，请重装为 64 位系统后再进行游戏！", "环境警告", "我知道了", "", "", IsWarn: true, HighLight: true, ForceWait: true);
						FormMain.EndProgramForce(ModBase.ProcessReturnValues.Cancel);
					}
					Logger.Info($"第一阶段加载用时：{ModBase.GetTimeMs() - ModBase.ApplicationStartTick} ms", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					ModBase.ApplicationStartTick = ModBase.GetTimeMs();
					ModAnimation.AniControlEnabled++;
				}
				catch (Exception ex13)
				{
					ProjectData.SetProjectError(ex13);
					Exception ex14 = ex13;
					string text = null;
					try
					{
						text = ModBase.PathExe;
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					Interaction.MsgBox(ex14.GetDisplay(multiline: true) + "\r\nPCL 所在路径：" + (string.IsNullOrEmpty(text) ? "获取失败" : text), MsgBoxStyle.Critical, "PCL 初始化错误");
					FormMain.EndProgramForce(ModBase.ProcessReturnValues.Exception);
					ProjectData.ClearProjectError();
				}
			}
		}
		private static void WaitingMutex(System.Windows.StartupEventArgs e)
		{
			try
			{
				PclMutex = new Mutex(initiallyOwned: true, "PCL_SingletonMutex", out var createdNew);
				if (createdNew)
				{
					return;
				}
				Logger.Warn("已有一个 PCL 实例正在运行", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
				if (e.Args.Length > 0 && Operators.CompareString(e.Args[0], "--wait", TextCompare: false) == 0)
				{
					try
					{
						if (PclMutex.WaitOne(10000))
						{
							return;
						}
					}
					catch (AbandonedMutexException ex)
					{
						ProjectData.SetProjectError(ex);
						AbandonedMutexException ex2 = ex;
						ProjectData.ClearProjectError();
						return;
					}
				}
				string ClassName = null;
				string WindowName = "Plain Craft Launcher\u3000";
				IntPtr intPtr = ModMain.FindWindow(ref ClassName, ref WindowName);
				if (intPtr == IntPtr.Zero)
				{
					WindowName = null;
					ClassName = "Plain Craft Launcher 2\u3000";
					intPtr = ModMain.FindWindow(ref WindowName, ref ClassName);
				}
				if (intPtr != IntPtr.Zero)
				{
					ModMain.ShowWindowToTop(intPtr);
				}
				Interaction.Beep();
				Environment.Exit(4);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "单例检查失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
		{
			ModMain.FrmMain?.EndProgram(SendWarning: false);
		}
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			int try0000_dispatch = -1;
			int num3 = default;
			int num = default;
			int num2 = default;
			string display = default;
			while (true)
			{
				try
				{
					/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
					switch (try0000_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 1;
						goto IL_0007;
					case 404:
						{
							num = num2;
							switch (num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0000;
							}
							int num4 = num + 1;
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_0007;
							case 3:
								goto IL_002d;
							case 4:
								goto IL_004a;
							case 6:
								goto IL_0057;
							case 7:
								goto IL_007d;
							case 8:
								goto IL_009a;
							case 10:
								goto IL_00a7;
							case 11:
								goto IL_00b1;
							case 13:
								goto IL_00bb;
							case 14:
								goto IL_00c3;
							case 15:
								goto IL_00d3;
							case 16:
								goto IL_010a;
							case 18:
								goto end_IL_0000_2;
							default:
								goto end_IL_0000;
							case 5:
							case 9:
							case 12:
							case 17:
							case 19:
								goto end_IL_0000_3;
							}
							goto default;
						}
						IL_010a:
						num2 = 16;
						OldEnvironmentAssert(e.Exception);
						goto end_IL_0000_3;
						IL_0007:
						num2 = 2;
						if (ModMain.FrmMain?.PageLeft != null && ModMain.FrmMain.PageLeft is IDispatcherUnhandledException)
						{
							goto IL_002d;
						}
						goto IL_0057;
						IL_009a:
						num2 = 8;
						if (e.Handled)
						{
							goto end_IL_0000_3;
						}
						goto IL_00a7;
						IL_00a7:
						num2 = 10;
						e.Handled = true;
						goto IL_00b1;
						IL_007d:
						num2 = 7;
						((IDispatcherUnhandledException)ModMain.FrmMain.PageRight).DispatcherUnhandledException(RuntimeHelpers.GetObjectValue(sender), e);
						goto IL_009a;
						IL_00b1:
						num2 = 11;
						if (ModBase.IsProgramEnding)
						{
							goto end_IL_0000_3;
						}
						goto IL_00bb;
						IL_002d:
						num2 = 3;
						((IDispatcherUnhandledException)ModMain.FrmMain.PageLeft).DispatcherUnhandledException(RuntimeHelpers.GetObjectValue(sender), e);
						goto IL_004a;
						IL_004a:
						num2 = 4;
						if (e.Handled)
						{
							goto end_IL_0000_3;
						}
						goto IL_0057;
						IL_0057:
						num2 = 6;
						if (ModMain.FrmMain?.PageRight != null && ModMain.FrmMain.PageRight is IDispatcherUnhandledException)
						{
							goto IL_007d;
						}
						goto IL_00a7;
						IL_00c3:
						num2 = 14;
						display = e.Exception.GetDisplay(multiline: true);
						goto IL_00d3;
						IL_00d3:
						num2 = 15;
						if (!display.Contains("System.Windows.Threading.Dispatcher.Invoke") && !display.Contains("MS.Internal.AppModel.ITaskbarList.HrInit") && !display.Contains("未能加载文件或程序集") && !display.Contains(".NET Framework"))
						{
							break;
						}
						goto IL_010a;
						IL_00bb:
						num2 = 13;
						ModMain.FeedbackInfo();
						goto IL_00c3;
						end_IL_0000_2:
						break;
					}
					num2 = 18;
					Logger.Error(e.Exception, "程序出现未知错误", LogBehavior.AlertThenCrash, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
					break;
					end_IL_0000:;
				}
				catch (Exception ex) when ((num3 != 0) & (num == 0))
				{
					ProjectData.SetProjectError(ex);
					try0000_dispatch = 404;
					continue;
				}
				throw ProjectData.CreateProjectError(-2146828237);
				continue;
				end_IL_0000_3:
				break;
			}
			if (num != 0)
			{
				ProjectData.ClearProjectError();
			}
		}
		private static void OldEnvironmentAssert(Exception ex = null)
		{
			ModBase.OpenWebsite("https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net48-offline-installer");
			if (ex == null)
			{
				Logger.Error(string.Format("你的 .NET Framework 版本过低或损坏，请下载并重新安装 .NET Framework 4.8！{0}若安装失败，可以先卸载高版本的 .NET Framework 然后再试。", "\r\n"), LogBehavior.AlertThenCrash, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
			}
			else
			{
				Logger.Error(ex, string.Format("你的 .NET Framework 版本过低或损坏，请下载并重新安装 .NET Framework 4.8！{0}若安装失败，可以先卸载高版本的 .NET Framework 然后再试。", "\r\n"), LogBehavior.AlertThenCrash, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
			}
		}
		private Application()
		{
			Startup += Application_Startup;
			SessionEnding += Application_SessionEnding;
			DispatcherUnhandledException += Application_DispatcherUnhandledException;
			if (_STATIC_ctor_2001_Prefixes_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_ctor_2001_Prefixes_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_ctor_2001_Prefixes_Init, ref lockTaken);
				if (_STATIC_ctor_2001_Prefixes_Init.State == 0)
				{
					_STATIC_ctor_2001_Prefixes_Init.State = 2;
					_STATIC_ctor_2001_Prefixes = new string[13]
					{
						"NAudio", "Newtonsoft.Json", "Ookii.Dialogs.Wpf", "Imazen.WebP", "CacheCow.Common", "CacheCow.Client.FileStore", "CacheCow.Client", "ThrottleDebounce", "System.Net.Http.Formatting", "Microsoft.Win32.Registry",
						"PCLCS", "MeloongCore.Wpf", "MeloongCore"
					};
				}
				else if (_STATIC_ctor_2001_Prefixes_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_ctor_2001_Prefixes_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_ctor_2001_Prefixes_Init);
				}
			}
			if (_STATIC_ctor_2001_LoadedAssemblies_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_ctor_2001_LoadedAssemblies_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken2 = false;
			try
			{
				Monitor.Enter(_STATIC_ctor_2001_LoadedAssemblies_Init, ref lockTaken2);
				if (_STATIC_ctor_2001_LoadedAssemblies_Init.State == 0)
				{
					_STATIC_ctor_2001_LoadedAssemblies_Init.State = 2;
					_STATIC_ctor_2001_LoadedAssemblies = new ConcurrentDictionary<string, Lazy<Assembly>>(StringComparer.Ordinal);
				}
				else if (_STATIC_ctor_2001_LoadedAssemblies_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_ctor_2001_LoadedAssemblies_Init.State = 1;
				if (lockTaken2)
				{
					Monitor.Exit(_STATIC_ctor_2001_LoadedAssemblies_Init);
				}
			}
			AppDomain.CurrentDomain.AssemblyResolve += [SpecialName] (object sender, ResolveEventArgs Args) =>
			{
				string text = _STATIC_ctor_2001_Prefixes.FirstOrDefault([SpecialName] (string p) => Args.Name.StartsWith(p, StringComparison.Ordinal));
				return (text == null) ? null : _STATIC_ctor_2001_LoadedAssemblies.GetOrAdd(text, [SpecialName] (string p) => new Lazy<Assembly>([SpecialName] () =>
				{
					Assembly result = Assembly.Load((byte[])PCL.My.Resources.Resources.ResourceManager.GetObject(p));
					if (Operators.CompareString(p, "Imazen.WebP", TextCompare: false) == 0)
					{
						ExtractLibwebp();
					}
					return result;
				}, LazyThreadSafetyMode.ExecutionAndPublication)).Value;
			};
		}
		private static void ExtractLibwebp()
		{
			string lpPathName = ModBase.PathPure.Value.TrimEnd('\\');
			SetDllDirectory(ref lpPathName);
			try
			{
				ModBase.ExtractResources(ModBase.PathPure.Value + "libwebp.dll", "libwebp64");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "写入 libwebp.dll 失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Application.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "SetDllDirectoryA", ExactSpelling = true, SetLastError = true)]
		private static extern bool SetDllDirectory([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpPathName);
		private void MyIconButton_Click(object sender, EventArgs e)
		{
			switch (Settings.Get<ModLaunch.McLoginType>("LoginType"))
			{
			case ModLaunch.McLoginType.Ms:
			{
				JObject jObject = (JObject)Settings.Get<string>("LoginMsJson").DeserializeJson();
				jObject.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				Settings.Set("LoginMsJson", jObject.ToString(Formatting.None));
				if (ModMain.FrmLoginMs.ComboAccounts.SelectedItem == NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null))
				{
					ModMain.FrmLoginMs.ComboAccounts.SelectedIndex = 0;
				}
				ModMain.FrmLoginMs.ComboAccounts.Items.Remove(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)));
				return;
			}
			case ModLaunch.McLoginType.Legacy:
			{
				List<string> list = new List<string>();
				list.AddRange(Settings.Get<string>("LoginLegacyName").ToString().Split("\u00a8"));
				list.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				Settings.Set("LoginLegacyName", list.Join('\u00a8'));
				ModMain.FrmLoginLegacy.ComboName.ItemsSource = list;
				ModMain.FrmLoginLegacy.ComboName.Text = (list.Any() ? list[0] : "");
				return;
			}
			}
			string text = Settings.Get<ModLaunch.McLoginType>("LoginType").ToString();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			if (Operators.CompareString(Settings.Get<string>("Login" + text + "Email"), "", TextCompare: false) != 0)
			{
				list2.AddRange(Settings.Get<string>("Login" + text + "Email").Split("\u00a8"));
			}
			if (Operators.CompareString(Settings.Get<string>("Login" + text + "Pass"), "", TextCompare: false) != 0)
			{
				list3.AddRange(Settings.Get<string>("Login" + text + "Pass").Split("\u00a8"));
			}
			checked
			{
				int num = list2.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					dictionary.Add(list2[i], list3[i]);
				}
				dictionary.Remove(Conversions.ToString(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
				Settings.Set("Login" + text + "Email", dictionary.Keys.Join('\u00a8'));
				Settings.Set("Login" + text + "Pass", dictionary.Values.Join('\u00a8'));
				if (Operators.CompareString(text, "Nide", TextCompare: false) != 0)
				{
					if (Operators.CompareString(text, "Auth", TextCompare: false) == 0)
					{
						ModMain.FrmLoginAuth.ComboName.ItemsSource = dictionary.Keys;
						ModMain.FrmLoginAuth.ComboName.Text = (dictionary.Keys.Any() ? dictionary.Keys.ElementAtOrDefault(0) : "");
						ModMain.FrmLoginAuth.TextPass.Password = (dictionary.Values.Any() ? dictionary.Values.ElementAtOrDefault(0) : "");
					}
				}
				else
				{
					ModMain.FrmLoginNide.ComboName.ItemsSource = dictionary.Keys;
					ModMain.FrmLoginNide.ComboName.Text = (dictionary.Keys.Any() ? dictionary.Keys.ElementAtOrDefault(0) : "");
					ModMain.FrmLoginNide.TextPass.Password = (dictionary.Values.Any() ? dictionary.Values.ElementAtOrDefault(0) : "");
				}
			}
		}
	}
}
