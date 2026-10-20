// PCL

namespace PCL
{
	[StandardModule]
	public static class ModWatcher
	{
		public class Watcher
		{
			public enum MinecraftState
			{
				Loading,
				Running,
				Crashed,
				Ended,
				Canceled
			}
			private delegate void EnumWindowsSub(IntPtr hwnd, int lParam);
			[CompilerGenerated]
			internal sealed class _Closure___5_0
			{
				public string _VB_Local_WindowTitle;
				public ModLoader.LoaderTask<Process, int> _VB_Local_Loader;
				public Watcher _VB_Me;
				public _Closure___5_0(_Closure___5_0 arg0)
				{
					if (arg0 != null)
					{
						_VB_Local_WindowTitle = arg0._VB_Local_WindowTitle;
						_VB_Local_Loader = arg0._VB_Local_Loader;
					}
				}
				[SpecialName]
				internal void _Lambda___0()
				{
					try
					{
						while (_VB_Me.State != MinecraftState.Ended && _VB_Me.State != MinecraftState.Canceled && (_VB_Local_Loader.State != ModBase.LoadState.Canceled || _VB_Me.State == MinecraftState.Crashed) && (_VB_Me.State != MinecraftState.Crashed || !_VB_Me.GameProcess.HasExited))
						{
							_VB_Me.TimerWindow();
							_VB_Me.TimerLog();
							int num = 1;
							do
							{
								if (_VB_Me.IsWindowFinished && _VB_Me.IsWindowAppeared && Operators.CompareString(_VB_Local_WindowTitle, "", TextCompare: false) != 0 && _VB_Me.State == MinecraftState.Running && !_VB_Me.GameProcess.HasExited)
								{
									string text = _VB_Local_WindowTitle.Replace("{date}", DateTime.Now.ToString("yyyy'/'M'/'d")).Replace("{time}", DateTime.Now.ToString("HH':'mm':'ss"));
									int hWnd = (int)_VB_Me.WindowHandle;
									string str = new string(text.ToCharArray());
									SetWindowText(hWnd, ref str);
								}
								Thread.Sleep(64);
								num = checked(num + 1);
							}
							while (num <= 3);
						}
						if (_VB_Me.State == MinecraftState.Crashed)
						{
							WatcherStateChanged();
						}
						_VB_Me.WatcherLog("Minecraft 日志监控已退出");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "Minecraft 日志监控主循环出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
						_VB_Me.State = MinecraftState.Ended;
						ProjectData.ClearProjectError();
					}
				}
			}
			public Process GameProcess;
			public ModMinecraft.McInstance Instance;
			private string WindowTitle;
			private int PID;
			public ModLoader.LoaderTask<Process, int> Loader;
			private MinecraftState _State;
			public List<string> WaitingLog;
			private readonly object WaitingLogLock;
			public ConcurrentQueue<string> LatestLog;
			private int LogProgress;
			private bool IsWindowAppeared;
			private bool IsWindowFinished;
			private IntPtr WindowHandle;
			public MinecraftState State
			{
				get
				{
					return _State;
				}
				set
				{
					if (_State != value)
					{
						_State = value;
						WatcherStateChanged();
					}
				}
			}
			public Watcher(ModLoader.LoaderTask<Process, int> Loader, ModMinecraft.McInstance Instance, string WindowTitle)
			{
				_Closure___5_0 arg = default;
				_Closure___5_0 CS_8__locals26 = new _Closure___5_0(arg);
				CS_8__locals26._VB_Local_Loader = Loader;
				CS_8__locals26._VB_Local_WindowTitle = WindowTitle;
				CS_8__locals26._VB_Me = this;
				this.WindowTitle = "";
				_State = MinecraftState.Loading;
				WaitingLog = new List<string>(1000);
				WaitingLogLock = RuntimeHelpers.GetObjectValue(new object());
				LatestLog = new ConcurrentQueue<string>();
				LogProgress = 0;
				IsWindowAppeared = false;
				IsWindowFinished = false;
				this.Loader = CS_8__locals26._VB_Local_Loader;
				this.Instance = Instance;
				this.WindowTitle = CS_8__locals26._VB_Local_WindowTitle;
				PID = CS_8__locals26._VB_Local_Loader.Input.Id;
				WatcherLog("开始 Minecraft 日志监控");
				if (Operators.CompareString(this.WindowTitle, "", TextCompare: false) != 0)
				{
					WatcherLog("要求窗口标题：" + CS_8__locals26._VB_Local_WindowTitle);
				}
				List<Watcher> list = new List<Watcher>();
				foreach (Watcher mcWatcher in McWatcherList)
				{
					if (mcWatcher.State != MinecraftState.Crashed && mcWatcher.State != MinecraftState.Ended && mcWatcher.State != MinecraftState.Canceled)
					{
						list.Add(mcWatcher);
					}
				}
				list.Add(this);
				McWatcherList = list;
				WatcherStateChanged();
				GameProcess = CS_8__locals26._VB_Local_Loader.Input;
				GameProcess.BeginOutputReadLine();
				GameProcess.BeginErrorReadLine();
				GameProcess.OutputDataReceived += LogReceived;
				GameProcess.ErrorDataReceived += LogReceived;
				ModBase.RunInNewThread([SpecialName] () =>
				{
					try
					{
						while (CS_8__locals26._VB_Me.State != MinecraftState.Ended && CS_8__locals26._VB_Me.State != MinecraftState.Canceled && (CS_8__locals26._VB_Local_Loader.State != ModBase.LoadState.Canceled || CS_8__locals26._VB_Me.State == MinecraftState.Crashed) && (CS_8__locals26._VB_Me.State != MinecraftState.Crashed || !CS_8__locals26._VB_Me.GameProcess.HasExited))
						{
							CS_8__locals26._VB_Me.TimerWindow();
							CS_8__locals26._VB_Me.TimerLog();
							int num = 1;
							do
							{
								if (CS_8__locals26._VB_Me.IsWindowFinished && CS_8__locals26._VB_Me.IsWindowAppeared && Operators.CompareString(CS_8__locals26._VB_Local_WindowTitle, "", TextCompare: false) != 0 && CS_8__locals26._VB_Me.State == MinecraftState.Running && !CS_8__locals26._VB_Me.GameProcess.HasExited)
								{
									string text = CS_8__locals26._VB_Local_WindowTitle.Replace("{date}", DateTime.Now.ToString("yyyy'/'M'/'d")).Replace("{time}", DateTime.Now.ToString("HH':'mm':'ss"));
									int hWnd = (int)CS_8__locals26._VB_Me.WindowHandle;
									string str = new string(text.ToCharArray());
									SetWindowText(hWnd, ref str);
								}
								Thread.Sleep(64);
								num = checked(num + 1);
							}
							while (num <= 3);
						}
						if (CS_8__locals26._VB_Me.State == MinecraftState.Crashed)
						{
							WatcherStateChanged();
						}
						CS_8__locals26._VB_Me.WatcherLog("Minecraft 日志监控已退出");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "Minecraft 日志监控主循环出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
						CS_8__locals26._VB_Me.State = MinecraftState.Ended;
						ProjectData.ClearProjectError();
					}
				}, "Minecraft Watcher PID " + Conversions.ToString(PID));
			}
			private void LogReceived(object sender, DataReceivedEventArgs e)
			{
				object waitingLogLock = WaitingLogLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(waitingLogLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(waitingLogLock, ref lockTaken);
					WaitingLog.Add(e.Data);
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(waitingLogLock);
					}
				}
			}
			private void TimerLog()
			{
				try
				{
					List<string> list = new List<string>();
					object waitingLogLock = WaitingLogLock;
					ObjectFlowControl.CheckForSyncLockOnValueType(waitingLogLock);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(waitingLogLock, ref lockTaken);
						if (!WaitingLog.Any())
						{
							return;
						}
						list = WaitingLog;
						WaitingLog = new List<string>(1000);
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(waitingLogLock);
						}
					}
					foreach (string item in list)
					{
						GameLog(item);
					}
					if (State == MinecraftState.Loading)
					{
						ProgressUpdate();
					}
					if (GameProcess.HasExited)
					{
						WatcherLog("Minecraft 已退出，返回值：" + Conversions.ToString(GameProcess.ExitCode));
						if (State == MinecraftState.Loading)
						{
							WatcherLog("Minecraft 尚未加载完成，可能已崩溃", LogLevel.Error);
							Crashed();
						}
						else if (GameProcess.ExitCode != 0 && State == MinecraftState.Running && Instance.ReleaseTime.Year >= 2012)
						{
							WatcherLog("Minecraft 返回值异常，可能已崩溃", LogLevel.Error);
							Crashed();
						}
						else if (State != MinecraftState.Crashed)
						{
							State = MinecraftState.Ended;
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "输出 Minecraft 日志失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
					ProjectData.ClearProjectError();
				}
			}
			private void GameLog(string Text)
			{
				if (Text == null)
				{
					return;
				}
				Text = Text.ReplaceLineEndings("\r\n");
				LatestLog.Enqueue(Text);
				if (LatestLog.Count >= 501)
				{
					ConcurrentQueue<string> latestLog = LatestLog;
					string result = null;
					latestLog.TryDequeue(out result);
				}
				Logger.Trace(Text, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
				if (LogProgress < 1)
				{
					WatcherLog("日志 1/5：已出现日志输出");
					LogProgress = 1;
				}
				if (LogProgress < 2 && Text.Contains("Setting user:"))
				{
					WatcherLog("日志 2/5：游戏用户已设置");
					LogProgress = 2;
				}
				else if (LogProgress < 3 && Text.ContainsIgnoreCase("lwjgl version"))
				{
					WatcherLog("日志 3/5：LWJGL 版本已确认");
					LogProgress = 3;
				}
				else if (LogProgress < 4 && (Text.Contains("OpenAL initialized") || Text.Contains("Starting up SoundSystem")))
				{
					WatcherLog("日志 4/5：OpenAL 已加载");
					LogProgress = 4;
				}
				else if (LogProgress < 5 && ((Text.Contains("Created") && Text.Contains("textures") && Text.Contains("-atlas")) || Text.Contains("Found animation info")))
				{
					WatcherLog("日志 5/5：材质已加载");
					LogProgress = 5;
				}
				if (!Text.Contains("[CHAT]"))
				{
					if (Text.Contains("Someone is closing me!") || Text.Contains("Restarting Minecraft with command"))
					{
						WatcherLog("识别为关闭的 Log：" + Text);
						State = MinecraftState.Ended;
					}
					else if (Text.Contains("Crash report saved to") || Text.Contains("This crash report has been saved to:"))
					{
						WatcherLog("识别为崩溃的 Log：" + Text, LogLevel.Error);
						Crashed();
					}
					else if (Text.Contains("Could not save crash report to"))
					{
						WatcherLog("识别为崩溃的 Log：" + Text, LogLevel.Error);
						Crashed();
					}
					else if (Text.Contains("/ERROR]: Unable to launch") || Text.Contains("An exception was thrown, the game will display an error screen and halt."))
					{
						WatcherLog("识别为崩溃的 Log：" + Text, LogLevel.Error);
						Crashed();
					}
				}
			}
			private void WatcherLog(string Text, LogLevel Level = LogLevel.Info)
			{
				ModLaunch.McLaunchLog("[" + Conversions.ToString(PID) + "] " + Text, Level);
			}
			private void ProgressUpdate()
			{
				double progress;
				if (IsWindowAppeared || LogProgress == 5)
				{
					progress = 0.95;
					WatcherLog("Minecraft 加载已完成");
					State = MinecraftState.Running;
				}
				else
				{
					progress = (double)Math.Min(LogProgress, 3) / 3.0 * 0.9;
				}
				Loader.Progress = progress;
			}
			private void TimerWindow()
			{
				try
				{
					if (GameProcess.HasExited || IsWindowFinished)
					{
						return;
					}
					KeyValuePair<IntPtr, string>? keyValuePair = null;
					try
					{
						keyValuePair = TryGetMinecraftWindow();
					}
					catch (Win32Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Win32Exception ex2 = ex;
						Logger.Error(ex2, "由于反作弊或安全软件拦截，PCL 无法操作游戏窗口", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
						IsWindowFinished = true;
						ProjectData.ClearProjectError();
					}
					if (!keyValuePair.HasValue)
					{
						return;
					}
					string value = keyValuePair.Value.Value;
					IntPtr key = keyValuePair.Value.Key;
					if (!value.StartsWithF("FML") && !value.StartsWithF("Quilt Loader"))
					{
						WindowHandle = key;
						WatcherLog($"Minecraft 窗口已加载：{value}（{key.ToInt64()}）");
						IsWindowFinished = true;
						if (Settings.Get<int>("LaunchArgumentWindowType") == 4)
						{
							ModBase.RunInNewThread([SpecialName] () =>
							{
								try
								{
									Thread.Sleep(2000);
									ShowWindow(WindowHandle, 3u);
									WatcherLog($"已最大化 Minecraft 窗口：{key.ToInt64()}");
								}
								catch (Exception ex5)
								{
									ProjectData.SetProjectError(ex5);
									Exception ex6 = ex5;
									Logger.Warn(ex6, "最大化 Minecraft 窗口时出现错误", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
									ProjectData.ClearProjectError();
								}
							}, "MinecraftWindowMaximize");
						}
					}
					else if (!IsWindowAppeared)
					{
						WatcherLog("Mod Loader 窗口已加载：" + value + "（" + Conversions.ToString(key.ToInt64()) + "）");
					}
					IsWindowAppeared = true;
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Error(ex4, "检查 Minecraft 窗口失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
					ProjectData.ClearProjectError();
				}
			}
			private KeyValuePair<IntPtr, string>? TryGetMinecraftWindow()
			{
				object obj = null;
				EnumWindows([SpecialName] (IntPtr hwnd, int lParam) =>
				{
					if (obj == null)
					{
						StringBuilder stringBuilder = new StringBuilder(512);
						GetClassName((int)hwnd, stringBuilder, stringBuilder.Capacity);
						switch (stringBuilder.ToString())
						{
						case "GLFW30":
						case "SDL_app":
						case "LWJGL":
						case "SunAwtFrame":
						{
							stringBuilder = new StringBuilder(512);
							GetWindowText((int)hwnd, stringBuilder, stringBuilder.Capacity);
							string text = stringBuilder.ToString();
							if (text.StartsWithF("FML") || (Operators.CompareString(text, "PopupMessageWindow", TextCompare: false) != 0 && !text.StartsWithF("GLFW")))
							{
								int lpdwProcessId = default;
								GetWindowThreadProcessId(hwnd, ref lpdwProcessId);
								try
								{
									if (DateTime.Compare(Process.GetProcessById(lpdwProcessId).StartTime, GameProcess.StartTime) < 0)
									{
										break;
									}
								}
								catch (Exception ex)
								{
									ProjectData.SetProjectError(ex);
									Exception ex2 = ex;
									Logger.Warn(ex2, "枚举 Minecraft 窗口进程失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
									ProjectData.ClearProjectError();
									break;
								}
								obj = new KeyValuePair<IntPtr, string>(hwnd, text);
							}
							break;
						}
						}
					}
				}, 0);
				return (KeyValuePair<IntPtr, string>?)obj;
			}
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern bool EnumWindows(EnumWindowsSub hWnd, int lParam);
			[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetClassNameA", ExactSpelling = true, SetLastError = true)]
			private static extern int GetClassName(int hWnd, StringBuilder str, int maxCount);
			[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextA", ExactSpelling = true, SetLastError = true)]
			private static extern int GetWindowText(int hWnd, StringBuilder str, int maxCount);
			[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowTextA", ExactSpelling = true, SetLastError = true)]
			private static extern bool SetWindowText(int hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string str);
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern bool ShowWindow(IntPtr hWnd, uint cmdWindow);
			[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			private static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);
			private void Crashed()
			{
				if (State == MinecraftState.Crashed || State == MinecraftState.Ended)
				{
					return;
				}
				State = MinecraftState.Crashed;
				WatcherLog("Minecraft 已崩溃，将在 2 秒后开始崩溃分析", LogLevel.Error);
				ModMain.Hint("检测到 Minecraft 出现错误，错误分析已开始……", ModMain.HintType.Red);
				ModMain.FeedbackInfo();
				ModBase.RunInNewThread([SpecialName] () =>
				{
					try
					{
						Thread.Sleep(2000);
						WatcherLog("崩溃分析开始");
						CrashAnalyzer crashAnalyzer = new CrashAnalyzer(Instance);
						crashAnalyzer.Collect(Instance.PathIndie, LatestLog.ToList());
						crashAnalyzer.Prepare();
						crashAnalyzer.Analyze();
						crashAnalyzer.Output(IsManualAnalyze: false, new List<string>
						{
							Instance.PathVersion + Instance.Name + ".json",
							Paths.Base + "PCL\\Log1.txt",
							Paths.Base + "PCL\\LatestLaunch.bat"
						});
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "崩溃分析失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
						ProjectData.ClearProjectError();
					}
				}, "Crash Analyzer");
			}
			public void Kill()
			{
				State = MinecraftState.Canceled;
				WatcherLog("尝试强制结束 Minecraft 进程");
				try
				{
					if (!GameProcess.HasExited)
					{
						GameProcess.Kill();
					}
					WatcherLog("已强制结束 Minecraft 进程");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "强制结束 Minecraft 进程失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		public static List<Watcher> McWatcherList = new List<Watcher>();
		private static bool IsWatcherRunning = false;
		public static bool HasRunningMinecraft = false;
		private static void WatcherStateChanged()
		{
			bool flag = false;
			bool triggerLauncherShutdown = true;
			foreach (Watcher mcWatcher in McWatcherList)
			{
				if (mcWatcher.State == Watcher.MinecraftState.Loading || mcWatcher.State == Watcher.MinecraftState.Running || (mcWatcher.State == Watcher.MinecraftState.Crashed && !mcWatcher.GameProcess.HasExited))
				{
					flag = true;
					break;
				}
				if (mcWatcher.State == Watcher.MinecraftState.Crashed || mcWatcher.State == Watcher.MinecraftState.Canceled)
				{
					triggerLauncherShutdown = false;
				}
			}
			if (IsWatcherRunning != flag)
			{
				IsWatcherRunning = flag;
				if (IsWatcherRunning)
				{
					MinecraftStart();
				}
				else
				{
					MinecraftStop(triggerLauncherShutdown);
				}
			}
		}
		private static void MinecraftStart()
		{
			ModLaunch.McLaunchLog("[全局] 出现运行中的 Minecraft");
			HasRunningMinecraft = true;
			ModMain.FrmMain.BtnExtraShutdown.ShowRefresh();
		}
		private static void MinecraftStop(bool TriggerLauncherShutdown)
		{
			ModLaunch.McLaunchLog("[全局] 已无运行中的 Minecraft");
			HasRunningMinecraft = false;
			ModMain.FrmMain.BtnExtraShutdown.ShowRefresh();
			if (Settings.Get<bool>("UiMusicStop"))
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					if (ModMusic.MusicResume())
					{
						Logger.Info("已根据设置，在结束后开始音乐播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
					}
				});
			}
			else if (Settings.Get<bool>("UiMusicStart"))
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					if (ModMusic.MusicPause())
					{
						Logger.Info("已根据设置，在结束后暂停音乐播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModWatcher.vb");
					}
				});
			}
			switch (Settings.Get<int>("LaunchArgumentVisible"))
			{
			case 2:
				if (TriggerLauncherShutdown)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmMain.EndProgram(SendWarning: false);
					});
				}
				else
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmMain.Hidden = false;
					});
				}
				break;
			case 3:
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.Hidden = false;
				});
				break;
			}
		}
	}
}
