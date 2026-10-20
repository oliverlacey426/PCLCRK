// PCL

namespace PCL
{
	[StandardModule]
	public static class ModDownloadLib
	{
		public class McInstallRequest
		{
			public string NewInstanceName;
			public string VersionFolder;
			public string MinecraftName;
			public string MinecraftJson;
			public string OptiFineVersion;
			public ModDownload.DlOptiFineListEntry OptiFineEntry;
			public string ForgeVersion;
			public ModDownload.DlForgeVersionEntry ForgeEntry;
			public string NeoForgeVersion;
			public ModDownload.DlNeoForgeListEntry NeoForgeEntry;
			public string FabricVersion;
			public ResourceVersion FabricApi;
			public ResourceVersion OptiFabric;
			public ModDownload.DlLiteLoaderListEntry LiteLoaderEntry;
			public McInstallRequest()
			{
				MinecraftName = null;
				MinecraftJson = null;
				OptiFineVersion = null;
				OptiFineEntry = null;
				ForgeVersion = null;
				ForgeEntry = null;
				NeoForgeVersion = null;
				NeoForgeEntry = null;
				FabricVersion = null;
				FabricApi = null;
				OptiFabric = null;
				LiteLoaderEntry = null;
			}
		}
		[Serializable]
		[CompilerGenerated]
		internal sealed class _Closure___
		{
			public static readonly _Closure___ _I;
			public static MyIconButton.ClickEventHandler _IR5_1;
			public static MyIconButton.ClickEventHandler _IR5_2;
			public static MyIconButton.ClickEventHandler _IR6_3;
			public static MyIconButton.ClickEventHandler _IR6_4;
			public static MyIconButton.ClickEventHandler _IR6_5;
			public static Func<ModLoader.LoaderBase, bool> _I13_2;
			public static Func<ModLoader.LoaderBase, bool> _I13_3;
			public static Func<ModLoader.LoaderBase, bool> _I13_4;
			public static MyIconButton.ClickEventHandler _IR16_8;
			public static MyIconButton.ClickEventHandler _IR17_9;
			public static MyIconButton.ClickEventHandler _IR17_10;
			public static MyIconButton.ClickEventHandler _IR23_11;
			public static MyIconButton.ClickEventHandler _IR24_12;
			public static MyIconButton.ClickEventHandler _IR24_13;
			public static Func<string, bool> _I28_2;
			public static Func<ModLoader.LoaderBase, bool> _I30_4;
			public static Func<ModLoader.LoaderBase, bool> _I30_5;
			public static Func<ModLoader.LoaderBase, bool> _I30_6;
			public static Func<string, DirectoryInfo> _I30_9;
			public static Func<DirectoryInfo, bool> _I30_10;
			public static Func<DirectoryInfo, string> _I30_11;
			public static Func<string, bool> _I32_1;
			public static MyIconButton.ClickEventHandler _IR33_16;
			public static MyIconButton.ClickEventHandler _IR33_17;
			public static MyIconButton.ClickEventHandler _IR34_18;
			public static Action _I37_0;
			public static MyIconButton.ClickEventHandler _IR42_19;
			public static MyIconButton.ClickEventHandler _IR42_20;
			public static MyIconButton.ClickEventHandler _IR43_21;
			public static Func<string, string> _I57_0;
			static _Closure___()
			{
				_I = new _Closure___();
			}
			[SpecialName]
			internal void _Lambda___R5_1(object a0, EventArgs a1)
			{
				McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R5_2(object a0, EventArgs a1)
			{
				McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R6_3(object a0, EventArgs a1)
			{
				McDownloadMenuSave(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R6_4(object a0, EventArgs a1)
			{
				McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R6_5(object a0, EventArgs a1)
			{
				McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal bool _Lambda___13_2(ModLoader.LoaderBase l)
			{
				if (Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) != 0)
				{
					return Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0;
				}
				return true;
			}
			[SpecialName]
			internal bool _Lambda___13_3(ModLoader.LoaderBase l)
			{
				return l.State != ModBase.LoadState.Finished;
			}
			[SpecialName]
			internal bool _Lambda___13_4(ModLoader.LoaderBase l)
			{
				return l.State != ModBase.LoadState.Finished;
			}
			[SpecialName]
			internal void _Lambda___R16_8(object a0, EventArgs a1)
			{
				OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R17_9(object a0, EventArgs a1)
			{
				OptiFineSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R17_10(object a0, EventArgs a1)
			{
				OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R23_11(object a0, EventArgs a1)
			{
				LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R24_12(object a0, EventArgs a1)
			{
				LiteLoaderSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R24_13(object a0, EventArgs a1)
			{
				LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal bool _Lambda___28_2(string l)
			{
				return Operators.CompareString(l, "true", TextCompare: false) == 0;
			}
			[SpecialName]
			internal bool _Lambda___30_4(ModLoader.LoaderBase l)
			{
				if (Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) != 0)
				{
					return Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0;
				}
				return true;
			}
			[SpecialName]
			internal bool _Lambda___30_5(ModLoader.LoaderBase l)
			{
				return l.State != ModBase.LoadState.Finished;
			}
			[SpecialName]
			internal bool _Lambda___30_6(ModLoader.LoaderBase l)
			{
				return l.State != ModBase.LoadState.Finished;
			}
			[SpecialName]
			internal DirectoryInfo _Lambda___30_9(string f)
			{
				return DirectoryUtils.GetInfo(f);
			}
			[SpecialName]
			internal bool _Lambda___30_10(DirectoryInfo l)
			{
				if (l.Name.ContainsIgnoreCase("forge"))
				{
					return l.EnumerateFiles().Any();
				}
				return false;
			}
			[SpecialName]
			internal string _Lambda___30_11(DirectoryInfo d)
			{
				return d.Name;
			}
			[SpecialName]
			internal bool _Lambda___32_1(string d)
			{
				return Operators.CompareString(d, "", TextCompare: false) != 0;
			}
			[SpecialName]
			internal void _Lambda___R33_16(object a0, EventArgs a1)
			{
				ForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R33_17(object a0, EventArgs a1)
			{
				ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R34_18(object a0, EventArgs a1)
			{
				ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___37_0()
			{
				try
				{
					Logger.Info("刷新 Forge 推荐版本缓存开始", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/promos", null, null, null, null, "*/*", null, RequireJson: true);
					if (text.Length < 1000)
					{
						throw new Exception("获取的结果过短（" + text + "）");
					}
					JContainer jContainer = (JContainer)text.DeserializeJson();
					List<string> list = new List<string>();
					foreach (JObject item in (IEnumerable<JToken>)jContainer)
					{
						if (item["name"] != null && item["build"] != null)
						{
							string text2 = (string?)item["name"];
							if (text2.EndsWithF("-recommended"))
							{
								list.Add("\"" + text2.Replace("-recommended", "\":\"" + item["build"]["version"].ToString() + "\""));
							}
						}
					}
					if (list.Count < 5)
					{
						throw new Exception("获取的推荐版本数过少（" + text + "）");
					}
					string text3 = "{" + list.Join(",") + "}";
					FileUtils.Write(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json", text3);
					Logger.Info("刷新 Forge 推荐版本缓存成功", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "刷新 Forge 推荐版本缓存失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
			}
			[SpecialName]
			internal void _Lambda___R42_19(object a0, EventArgs a1)
			{
				NeoForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R42_20(object a0, EventArgs a1)
			{
				NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal void _Lambda___R43_21(object a0, EventArgs a1)
			{
				NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			}
			[SpecialName]
			internal string _Lambda___57_0(string l)
			{
				return l.Trim();
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___12_0
		{
			public AutoResetEvent _VB_Local_outputWaitHandle;
			public _Closure___12_1 _VB_NonLocal__VB_Closure_3;
			public _Closure___12_0(_Closure___12_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_outputWaitHandle = arg0._VB_Local_outputWaitHandle;
				}
			}
			[SpecialName]
			internal void _Lambda___R6(object a0, DataReceivedEventArgs a1)
			{
				_Lambda___0(RuntimeHelpers.GetObjectValue(a0), a1);
			}
			[SpecialName]
			internal object _Lambda___0(object sender, DataReceivedEventArgs e)
			{
				try
				{
					if (e.Data == null)
					{
						_VB_Local_outputWaitHandle.Set();
					}
					else
					{
						_VB_NonLocal__VB_Closure_3._VB_Local_LastResult = e.Data;
						Logger.Trace(_VB_NonLocal__VB_Closure_3._VB_Local_LastResult, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_3._VB_Local_TotalLength = checked(_VB_NonLocal__VB_Closure_3._VB_Local_TotalLength + 1);
						_VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.Progress += 9.999999999999999E-05;
					}
				}
				catch (ObjectDisposedException ex)
				{
					ProjectData.SetProjectError(ex);
					ObjectDisposedException ex2 = ex;
					ProjectData.ClearProjectError();
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "读取 OptiFine 安装器信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					if (_VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.State == ModBase.LoadState.Canceled && !_VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
					{
						Logger.Info("由于任务取消，已中止 OptiFine 安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_3._VB_Local_process.Kill();
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				return null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___12_1
		{
			public string _VB_Local_LastResult;
			public int _VB_Local_TotalLength;
			public Process _VB_Local_process;
			public _Closure___12_2 _VB_NonLocal__VB_Closure_2;
			public _Closure___12_1(_Closure___12_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_LastResult = arg0._VB_Local_LastResult;
					_VB_Local_TotalLength = arg0._VB_Local_TotalLength;
					_VB_Local_process = arg0._VB_Local_process;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___12_2
		{
			public ModLoader.LoaderTask<List<ModNet.NetFile>, bool> _VB_Local_Task;
			public _Closure___12_2(_Closure___12_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Task = arg0._VB_Local_Task;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___12_3
		{
			public AutoResetEvent _VB_Local_errorWaitHandle;
			public _Closure___12_0 _VB_NonLocal__VB_Closure_4;
			public _Closure___12_3(_Closure___12_3 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_errorWaitHandle = arg0._VB_Local_errorWaitHandle;
				}
			}
			[SpecialName]
			internal void _Lambda___R7(object a0, DataReceivedEventArgs a1)
			{
				_Lambda___1(RuntimeHelpers.GetObjectValue(a0), a1);
			}
			[SpecialName]
			internal object _Lambda___1(object sender, DataReceivedEventArgs e)
			{
				try
				{
					if (e.Data == null)
					{
						_VB_Local_errorWaitHandle.Set();
					}
					else
					{
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResult = e.Data;
						Logger.Trace(_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResult, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_TotalLength = checked(_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_TotalLength + 1);
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.Progress += 9.999999999999999E-05;
					}
				}
				catch (ObjectDisposedException ex)
				{
					ProjectData.SetProjectError(ex);
					ObjectDisposedException ex2 = ex;
					ProjectData.ClearProjectError();
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "读取 OptiFine 安装器错误信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					if (_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.State == ModBase.LoadState.Canceled && !_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
					{
						Logger.Info("由于任务取消，已中止 OptiFine 安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Kill();
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				return null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___13_0
		{
			public ModDownload.DlOptiFineListEntry _VB_Local_DownloadInfo;
			public string _VB_Local_Target;
			public ModLoader.LoaderCombo<string> _VB_Local_ClientDownloadLoader;
			public bool _VB_Local_IsCustomFolder;
			public string _VB_Local_ClientFolder;
			public string _VB_Local_McFolder;
			public string _VB_Local_VersionFolder;
			public string _VB_Local_Id;
			public _Closure___13_0(_Closure___13_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_DownloadInfo = arg0._VB_Local_DownloadInfo;
					_VB_Local_Target = arg0._VB_Local_Target;
					_VB_Local_ClientDownloadLoader = arg0._VB_Local_ClientDownloadLoader;
					_VB_Local_IsCustomFolder = arg0._VB_Local_IsCustomFolder;
					_VB_Local_ClientFolder = arg0._VB_Local_ClientFolder;
					_VB_Local_McFolder = arg0._VB_Local_McFolder;
					_VB_Local_VersionFolder = arg0._VB_Local_VersionFolder;
					_VB_Local_Id = arg0._VB_Local_Id;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				List<string> list = new List<string>();
				string text = _VB_Local_DownloadInfo.Inherit;
				if (Operators.CompareString(text, "1.8", TextCompare: false) == 0 || Operators.CompareString(text, "1.9", TextCompare: false) == 0)
				{
					text += ".0";
				}
				if (_VB_Local_DownloadInfo.IsPreview)
				{
					list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + _VB_Local_DownloadInfo.DisplayName.Replace(_VB_Local_DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
				}
				else
				{
					list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + _VB_Local_DownloadInfo.DisplayName.Replace(_VB_Local_DownloadInfo.Inherit + " ", ""));
				}
				try
				{
					string str = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + _VB_Local_DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
					Task.Progress = 0.8;
					list.Add("https://optifine.net/" + str.RegexSearch("downloadx\\?f=[^\"']+").First());
					Logger.Info($"OptiFine {_VB_Local_DownloadInfo.DisplayName} 官方下载地址：{list.Last()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"获取 OptiFine {_VB_Local_DownloadInfo.DisplayName} 官方下载地址失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list.ToArray(), _VB_Local_Target, new FileChecker
					{
						MinSize = 307200L
					})
				};
			}
			[SpecialName]
			internal void _Lambda___1(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				List<ModLoader.LoaderBase> source = (from l in _VB_Local_ClientDownloadLoader.GetLoaderList()
					where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
					where l.State != ModBase.LoadState.Finished
					select l).ToList();
				if (source.Any())
				{
					Logger.Info("OptiFine 安装正在等待原版文件下载完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				while (source.Any() && !Task.IsCanceled)
				{
					source = source.Where((_Closure___._I13_4 == null) ? (_Closure___._I13_4 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure___._I13_4).ToList();
					Thread.Sleep(50);
				}
				if (Task.IsCanceled || !_VB_Local_IsCustomFolder)
				{
					return;
				}
				object vanillaSyncLock = VanillaSyncLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(vanillaSyncLock, ref lockTaken);
					string lastPart = PathUtils.GetLastPart(_VB_Local_ClientFolder);
					DirectoryUtils.Create(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit);
					if (!FileUtils.Exists(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".json"))
					{
						FileUtils.Copy($"{_VB_Local_ClientFolder}{lastPart}.json", $"{_VB_Local_McFolder}versions\\{_VB_Local_DownloadInfo.Inherit}\\{_VB_Local_DownloadInfo.Inherit}.json");
					}
					if (!FileUtils.Exists(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".jar"))
					{
						FileUtils.Copy($"{_VB_Local_ClientFolder}{lastPart}.jar", $"{_VB_Local_McFolder}versions\\{_VB_Local_DownloadInfo.Inherit}\\{_VB_Local_DownloadInfo.Inherit}.jar");
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(vanillaSyncLock);
					}
				}
			}
			[SpecialName]
			internal void _Lambda___5(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				string text = ModMain.RequestTaskTempFolder();
				string text2 = text + ".minecraft\\";
				try
				{
					if (DirectoryUtils.Exists(text2 + "versions\\" + _VB_Local_DownloadInfo.Inherit))
					{
						DirectoryUtils.Delete(text2 + "versions\\" + _VB_Local_DownloadInfo.Inherit);
					}
					DirectoryUtils.Create(text2 + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\");
					ModMinecraft.McFolderLauncherProfilesJsonCreate(text2);
					FileUtils.Copy(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".json", text2 + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".json");
					FileUtils.Copy(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".jar", text2 + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".jar");
					Task.Progress = 0.06;
					bool flag = true;
					while (true)
					{
						try
						{
							McDownloadOptiFineInstall(text, _VB_Local_Target, Task, flag);
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							if (flag)
							{
								Logger.Warn(ex2, "使用 JavaWrapper 安装 OptiFine 失败，将不使用 JavaWrapper 并重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								flag = false;
								ProjectData.ClearProjectError();
								continue;
							}
							throw new Exception("运行 OptiFine 安装器失败", ex2);
						}
						break;
					}
					Task.Progress = 0.96;
					FileUtils.Delete(text2 + "launcher_profiles.json");
					DirectoryUtils.Copy(text2, _VB_Local_McFolder);
					Task.Progress = 0.98;
					FileUtils.Delete(_VB_Local_Target);
					DirectoryUtils.Delete(text);
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception innerException = ex3;
					throw new Exception("安装 OptiFine（方式 A）失败", innerException);
				}
			}
			[SpecialName]
			internal void _Lambda___6(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				try
				{
					FileUtils.Copy(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit + "\\" + _VB_Local_DownloadInfo.Inherit + ".jar", _VB_Local_VersionFolder + _VB_Local_Id + ".jar");
					Task.Progress = 0.7;
					ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(_VB_Local_McFolder + "versions\\" + _VB_Local_DownloadInfo.Inherit);
					string text = "{\r\n    \"id\": \"" + _VB_Local_Id + "\",\r\n    \"inheritsFrom\": \"" + _VB_Local_DownloadInfo.Inherit + "\",\r\n    \"time\": \"" + ((Operators.CompareString(_VB_Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : _VB_Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"releaseTime\": \"" + ((Operators.CompareString(_VB_Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : _VB_Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"type\": \"release\",\r\n    \"libraries\": [\r\n        {\"name\": \"optifine:OptiFine:" + _VB_Local_DownloadInfo.FileName.Replace("OptiFine_", "").Replace(".jar", "").Replace("preview_", "") + "\"},\r\n        {\"name\": \"net.minecraft:launchwrapper:1.12\"}\r\n    ],\r\n    \"mainClass\": \"net.minecraft.launchwrapper.Launch\",";
					Task.Progress = 0.8;
					FileUtils.Write(text: (!mcInstance.IsOldJson) ? (text + "\r\n    \"minimumLauncherVersion\": \"21\",\r\n    \"arguments\": {\r\n        \"game\": [\r\n            \"--tweakClass\",\r\n            \"optifine.OptiFineTweaker\"\r\n        ]\r\n    }\r\n}") : (text + "\r\n    \"minimumLauncherVersion\": 18,\r\n    \"minecraftArguments\": \"" + mcInstance.JsonObject["minecraftArguments"].ToString() + "  --tweakClass optifine.OptiFineTweaker\"\r\n}"), filePath: _VB_Local_VersionFolder + _VB_Local_Id + ".json");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("安装 OptiFine（方式 B）失败", innerException);
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___28_0
		{
			public AutoResetEvent _VB_Local_outputWaitHandle;
			public _Closure___28_1 _VB_NonLocal__VB_Closure_3;
			public _Closure___28_0(_Closure___28_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_outputWaitHandle = arg0._VB_Local_outputWaitHandle;
				}
			}
			[SpecialName]
			internal void _Lambda___R14(object a0, DataReceivedEventArgs a1)
			{
				_Lambda___0(RuntimeHelpers.GetObjectValue(a0), a1);
			}
			[SpecialName]
			internal object _Lambda___0(object sender, DataReceivedEventArgs e)
			{
				try
				{
					if (e.Data == null)
					{
						_VB_Local_outputWaitHandle.Set();
					}
					else
					{
						_VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Enqueue(e.Data);
						if (_VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Count > 100)
						{
							_VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Dequeue();
						}
						ForgelikeInjectorLine(e.Data, _VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task);
					}
				}
				catch (ObjectDisposedException ex)
				{
					ProjectData.SetProjectError(ex);
					ObjectDisposedException ex2 = ex;
					ProjectData.ClearProjectError();
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, $"读取 {_VB_NonLocal__VB_Closure_3._VB_Local_LoaderName} 安装器信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					if (_VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.State == ModBase.LoadState.Canceled && !_VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
					{
						Logger.Info($"由于任务取消，已中止 {_VB_NonLocal__VB_Closure_3._VB_Local_LoaderName} 安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_3._VB_Local_process.Kill();
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				return null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___28_1
		{
			public Queue<string> _VB_Local_LastResults;
			public string _VB_Local_LoaderName;
			public Process _VB_Local_process;
			public _Closure___28_2 _VB_NonLocal__VB_Closure_2;
			public _Closure___28_1(_Closure___28_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_LastResults = arg0._VB_Local_LastResults;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
					_VB_Local_process = arg0._VB_Local_process;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___28_2
		{
			public ModLoader.LoaderTask<bool, bool> _VB_Local_Task;
			public _Closure___28_2(_Closure___28_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Task = arg0._VB_Local_Task;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___28_3
		{
			public AutoResetEvent _VB_Local_errorWaitHandle;
			public _Closure___28_0 _VB_NonLocal__VB_Closure_4;
			public _Closure___28_3(_Closure___28_3 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_errorWaitHandle = arg0._VB_Local_errorWaitHandle;
				}
			}
			[SpecialName]
			internal void _Lambda___R15(object a0, DataReceivedEventArgs a1)
			{
				_Lambda___1(RuntimeHelpers.GetObjectValue(a0), a1);
			}
			[SpecialName]
			internal object _Lambda___1(object sender, DataReceivedEventArgs e)
			{
				try
				{
					if (e.Data == null)
					{
						_VB_Local_errorWaitHandle.Set();
					}
					else
					{
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Enqueue(e.Data);
						if (_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Count > 100)
						{
							_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Dequeue();
						}
						ForgelikeInjectorLine(e.Data, _VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task);
					}
				}
				catch (ObjectDisposedException ex)
				{
					ProjectData.SetProjectError(ex);
					ObjectDisposedException ex2 = ex;
					ProjectData.ClearProjectError();
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, $"读取 {_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LoaderName} 安装器错误信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					if (_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_NonLocal__VB_Closure_2._VB_Local_Task.State == ModBase.LoadState.Canceled && !_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
					{
						Logger.Info($"由于任务取消，已中止 {_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LoaderName} 安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						_VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Kill();
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				return null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___30_0
		{
			public string _VB_Local_Inherit;
			public string _VB_Local_LoaderVersion;
			public ModDownload.DlForgelikeEntry _VB_Local_Info;
			public string _VB_Local_LoaderName;
			public string _VB_Local_InstallerAddress;
			public bool _VB_Local_IsNeoForge;
			public bool _VB_Local_IsCustomFolder;
			public string _VB_Local_McFolder;
			public ModLoader.LoaderCombo<string> _VB_Local_ClientDownloadLoader;
			public string _VB_Local_ClientFolder;
			public string _VB_Local_VersionFolder;
			public string _VB_Local_NewInstanceName;
			public _Closure___30_0(_Closure___30_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Inherit = arg0._VB_Local_Inherit;
					_VB_Local_LoaderVersion = arg0._VB_Local_LoaderVersion;
					_VB_Local_Info = arg0._VB_Local_Info;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
					_VB_Local_InstallerAddress = arg0._VB_Local_InstallerAddress;
					_VB_Local_IsNeoForge = arg0._VB_Local_IsNeoForge;
					_VB_Local_IsCustomFolder = arg0._VB_Local_IsCustomFolder;
					_VB_Local_McFolder = arg0._VB_Local_McFolder;
					_VB_Local_ClientDownloadLoader = arg0._VB_Local_ClientDownloadLoader;
					_VB_Local_ClientFolder = arg0._VB_Local_ClientFolder;
					_VB_Local_VersionFolder = arg0._VB_Local_VersionFolder;
					_VB_Local_NewInstanceName = arg0._VB_Local_NewInstanceName;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, string> Task)
			{
				ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("McDownloadForgeLoader " + _VB_Local_Inherit, ModDownload.DlForgeVersionMain);
				loaderTask.WaitForExit(_VB_Local_Inherit);
				Task.Progress = 0.8;
				foreach (ModDownload.DlForgeVersionEntry item in loaderTask.Output)
				{
					if (ModMinecraft.CompareVersion(item.Version.ToString(), _VB_Local_LoaderVersion) == 0)
					{
						_VB_Local_Info = item;
						return;
					}
				}
				throw new Exception($"未能找到 {_VB_Local_LoaderName} " + _VB_Local_Inherit + "-" + _VB_Local_LoaderVersion + " 的详细信息！");
			}
			[SpecialName]
			internal void _Lambda___1(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				List<ModNet.NetFile> list = new List<ModNet.NetFile>();
				if (_VB_Local_Info.IsNeoForge)
				{
					string text = ((ModDownload.DlNeoForgeListEntry)_VB_Local_Info).UrlBase + "-installer.jar";
					list.Add(new ModNet.NetFile(new string[2]
					{
						text.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
						text
					}, _VB_Local_InstallerAddress, new FileChecker
					{
						MinSize = 65536L
					}));
				}
				else
				{
					ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)_VB_Local_Info;
					string arg = string.Format("{0}-{1}/forge-{2}-{3}-{4}.{5}", dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Category, dlForgeVersionEntry.FileExtension);
					list.Add(new ModNet.NetFile(new string[2]
					{
						$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{arg}",
						$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{arg}"
					}, _VB_Local_InstallerAddress, new FileChecker
					{
						MinSize = 65536L,
						Hash = dlForgeVersionEntry.Hash
					}));
				}
				Task.Output = list;
			}
			[SpecialName]
			internal void _Lambda___7(ModLoader.LoaderTask<bool, bool> Task)
			{
				ZipArchive zipArchive = null;
				try
				{
					_Closure___30_2 arg = default;
					_Closure___30_2 CS_8__locals2 = new _Closure___30_2(arg);
					Logger.Info($"开始进行 Forgelike 安装：{_VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					CS_8__locals2._VB_Local_OldList = DirectoryUtils.EnumerateDirectories(_VB_Local_McFolder + "versions\\").ToList();
					zipArchive = FileUtils.OpenZip(_VB_Local_InstallerAddress);
					_ = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
						.DeserializeJson();
					DirectoryUtils.Create(_VB_Local_VersionFolder);
					Task.Progress = 0.04;
					ModMinecraft.McFolderLauncherProfilesJsonCreate(_VB_Local_McFolder);
					Task.Progress = 0.05;
					bool flag = true;
					while (true)
					{
						try
						{
							ModBase.ExtractResources(ModBase.PathTemp + "Cache\\forge_installer.jar", "ForgeInstaller");
							Task.Progress = 0.06;
							ForgelikeInjector(_VB_Local_InstallerAddress, Task, _VB_Local_McFolder, flag, _VB_Local_IsNeoForge);
							Task.Progress = 0.97;
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							if (flag)
							{
								Logger.Warn(ex2, $"使用 JavaWrapper 安装 {_VB_Local_LoaderName} 失败，将不使用 JavaWrapper 并重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								flag = false;
								ProjectData.ClearProjectError();
								continue;
							}
							throw new Exception($"运行 {_VB_Local_LoaderName} 安装器失败", ex2);
						}
						break;
					}
					List<DirectoryInfo> list = (from f in DirectoryUtils.EnumerateDirectories(_VB_Local_McFolder + "versions\\").SkipWhile([SpecialName] (string i) => CS_8__locals2._VB_Local_OldList.Contains(i))
						select DirectoryUtils.GetInfo(f)).ToList();
					if (list.Count > 1)
					{
						list = list.Where([SpecialName] (DirectoryInfo l) => l.Name.ContainsIgnoreCase("forge") && l.EnumerateFiles().Any()).ToList();
					}
					if (list.Count == 1)
					{
						FileInfo fileInfo = list[0].EnumerateFiles().First();
						FileUtils.Copy(fileInfo.FullName, _VB_Local_VersionFolder + _VB_Local_NewInstanceName + ".json");
						Logger.Info($"已复制新增的版本 JSON 文件：{fileInfo.FullName} -> {_VB_Local_VersionFolder}{_VB_Local_NewInstanceName}.json", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					else if (list.Count > 1)
					{
						Logger.Info(string.Format("有多个疑似的新增版本，无法确定：{0}", list.Select([SpecialName] (DirectoryInfo d) => d.Name).Join(";")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					else
					{
						Logger.Info("未找到新增的版本文件夹", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception innerException = ex3;
					throw new Exception($"安装新 {_VB_Local_LoaderName} 版本失败", innerException);
				}
				finally
				{
					try
					{
						zipArchive?.Dispose();
						FileUtils.Delete(_VB_Local_InstallerAddress);
					}
					catch (Exception ex4)
					{
						ProjectData.SetProjectError(ex4);
						Exception ex5 = ex4;
						Logger.Warn(ex5, $"安装 {_VB_Local_LoaderName} 清理文件时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						ProjectData.ClearProjectError();
					}
				}
			}
			[SpecialName]
			internal void _Lambda___12(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				ZipArchive zipArchive = null;
				try
				{
					zipArchive = FileUtils.OpenZip(_VB_Local_InstallerAddress);
					Task.Progress = 0.2;
					JObject jObject = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
						.DeserializeJson();
					Task.Progress = 0.4;
					DirectoryUtils.Create(_VB_Local_VersionFolder);
					Task.Progress = 0.5;
					if (jObject["install"] == null)
					{
						Logger.Info($"开始进行 Forge 安装，Legacy 方式 1：{_VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						JObject jObject2 = (JObject)zipArchive.GetEntry(jObject["json"].ToString().TrimStart('/')).Open().ReadString()
							.DeserializeJson();
						jObject2["id"] = _VB_Local_NewInstanceName;
						FileUtils.Write(_VB_Local_VersionFolder + _VB_Local_NewInstanceName + ".json", jObject2.ToString());
						Task.Progress = 0.6;
						zipArchive.Dispose();
						FileUtils.ExtractToDirectory(_VB_Local_InstallerAddress, _VB_Local_InstallerAddress + "_unrar\\");
						if (DirectoryUtils.Exists(_VB_Local_InstallerAddress + "_unrar\\maven\\"))
						{
							DirectoryUtils.Copy(_VB_Local_InstallerAddress + "_unrar\\maven\\", _VB_Local_McFolder + "libraries\\");
						}
						DirectoryUtils.Delete(_VB_Local_InstallerAddress + "_unrar\\");
					}
					else
					{
						Logger.Info($"开始进行 Forge 安装，Legacy 方式 2：{_VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						FileUtils.Write(ModMinecraft.McLibGet((string?)jObject["install"]["path"], WithHead: true, IgnoreLiteLoader: false, _VB_Local_McFolder), zipArchive.GetEntry((string?)jObject["install"]["filePath"]).Open());
						Task.Progress = 0.9;
						jObject["versionInfo"]["id"] = _VB_Local_NewInstanceName;
						if (jObject["versionInfo"]["inheritsFrom"] == null)
						{
							((JObject)jObject["versionInfo"]).Add("inheritsFrom", _VB_Local_Inherit);
						}
						FileUtils.Write(_VB_Local_VersionFolder + _VB_Local_NewInstanceName + ".json", jObject["versionInfo"].ToString());
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("非新版方式安装 Forge 失败", innerException);
				}
				finally
				{
					try
					{
						zipArchive?.Dispose();
						FileUtils.Delete(_VB_Local_InstallerAddress);
						DirectoryUtils.Delete(_VB_Local_InstallerAddress + "_unrar\\");
					}
					catch (Exception ex2)
					{
						ProjectData.SetProjectError(ex2);
						Exception ex3 = ex2;
						Logger.Warn(ex3, "非新版方式安装 Forge 清理文件时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						ProjectData.ClearProjectError();
					}
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___30_1
		{
			public List<ModMinecraft.McLibEntry> _VB_Local_Libs;
			public _Closure___30_0 _VB_NonLocal__VB_Closure_2;
			public _Closure___30_1(_Closure___30_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Libs = arg0._VB_Local_Libs;
				}
			}
			[SpecialName]
			internal void _Lambda___2(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = new List<ModNet.NetFile>();
				ZipArchive zipArchive = null;
				checked
				{
					try
					{
						zipArchive = FileUtils.OpenZip(_VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress);
						Task.Progress = 0.2;
						JObject jObject = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
							.DeserializeJson();
						JObject content = (JObject)zipArchive.GetEntry("version.json").Open().ReadString()
							.DeserializeJson();
						jObject.Merge(content);
						_VB_Local_Libs = ModMinecraft.McLibListGetWithJson(jObject, KeepSameNameDifferentVersionResult: true);
						if (jObject["data"] != null && jObject["data"]["MOJMAPS"] != null)
						{
							Task.Progress = 0.4;
							JObject jObject2 = (JObject)ModNet.NetRequestByLoader(ModDownload.DlSourceLauncherOrMetaGet(Conversions.ToString(ModDownload.DlClientListGet(_VB_NonLocal__VB_Closure_2._VB_Local_Inherit))), 45000, IsJson: true).DeserializeJson();
							string text = jObject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).BeforeFirst("@");
							string localPath = ModMinecraft.McLibGet(text).Replace(".jar", "." + jObject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).Split("@")[1]);
							JToken jToken = jObject2["downloads"]["client_mappings"];
							_VB_Local_Libs.Add(new ModMinecraft.McLibEntry
							{
								IsNatives = false,
								LocalPath = localPath,
								OriginalName = text,
								Url = (string?)jToken["url"],
								Size = (long)jToken["size"],
								SHA1 = (string?)jToken["sha1"]
							});
							Logger.Info(string.Format("需要下载 Mappings：{0} (SHA1: {1})", jToken["url"], jToken["sha1"]), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
						Task.Progress = 0.8;
						int num = _VB_Local_Libs.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							if (_VB_Local_Libs[i].LocalPath.EndsWithF($"{_VB_NonLocal__VB_Closure_2._VB_Local_LoaderName.Lower()}-{_VB_NonLocal__VB_Closure_2._VB_Local_Inherit}-{_VB_NonLocal__VB_Closure_2._VB_Local_LoaderVersion}.jar") || _VB_Local_Libs[i].LocalPath.EndsWithF($"{_VB_NonLocal__VB_Closure_2._VB_Local_LoaderName.Lower()}-{_VB_NonLocal__VB_Closure_2._VB_Local_Inherit}-{_VB_NonLocal__VB_Closure_2._VB_Local_LoaderVersion}-client.jar"))
							{
								Logger.Warn($"已从待下载 {_VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库中移除：{_VB_Local_Libs[i].LocalPath}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								_VB_Local_Libs.RemoveAt(i);
								break;
							}
						}
						Task.Output = ModMinecraft.McLibNetFilesFromTokens(_VB_Local_Libs, ModMinecraft.McFolderSelected);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception(string.Format("获取{0}Forge 支持库列表失败", _VB_NonLocal__VB_Closure_2._VB_Local_IsNeoForge ? " Neo" : "新版 "), innerException);
					}
					finally
					{
						zipArchive?.Dispose();
					}
				}
			}
			[SpecialName]
			internal void _Lambda___3(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
			{
				if (_VB_NonLocal__VB_Closure_2._VB_Local_IsCustomFolder)
				{
					foreach (ModMinecraft.McLibEntry item in _VB_Local_Libs)
					{
						string text = item.LocalPath.Replace(ModMinecraft.McFolderSelected, _VB_NonLocal__VB_Closure_2._VB_Local_McFolder);
						if (!FileUtils.Exists(text))
						{
							FileUtils.Copy(item.LocalPath, text);
						}
						Logger.Trace($"复制的 {_VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库文件：{item.LocalPath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
				}
				List<ModLoader.LoaderBase> source = (from l in _VB_NonLocal__VB_Closure_2._VB_Local_ClientDownloadLoader.GetLoaderList()
					where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
					where l.State != ModBase.LoadState.Finished
					select l).ToList();
				if (source.Any())
				{
					Logger.Info($"{_VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 安装正在等待原版文件下载完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				while (source.Any() && !Task.IsCanceled)
				{
					source = source.Where((_Closure___._I30_6 == null) ? (_Closure___._I30_6 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure___._I30_6).ToList();
					Thread.Sleep(50);
				}
				if (Task.IsCanceled || !_VB_NonLocal__VB_Closure_2._VB_Local_IsCustomFolder)
				{
					return;
				}
				object vanillaSyncLock = VanillaSyncLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(vanillaSyncLock, ref lockTaken);
					string lastPart = PathUtils.GetLastPart(_VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder);
					DirectoryUtils.Create(_VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit);
					if (!FileUtils.Exists(_VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".json"))
					{
						FileUtils.Copy(_VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder + lastPart + ".json", _VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".json");
					}
					if (!FileUtils.Exists(_VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".jar"))
					{
						FileUtils.Copy(_VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder + lastPart + ".jar", _VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + _VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".jar");
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(vanillaSyncLock);
					}
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___30_2
		{
			public List<string> _VB_Local_OldList;
			public _Closure___30_2(_Closure___30_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_OldList = arg0._VB_Local_OldList;
				}
			}
			[SpecialName]
			internal bool _Lambda___8(string i)
			{
				return _VB_Local_OldList.Contains(i);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___56_0
		{
			public string _VB_Local_VersionFolder;
			public string _VB_Local_OptiFineFolder;
			public bool _VB_Local_OptiFineAsMod;
			public string _VB_Local_ForgeFolder;
			public McInstallRequest _VB_Local_Request;
			public string _VB_Local_NeoForgeFolder;
			public string _VB_Local_FabricFolder;
			public string _VB_Local_LiteLoaderFolder;
			public string _VB_Local_TempMcFolder;
			public string _VB_Local_ModsTempFolder;
			public bool _VB_Local_Modable;
			[SpecialName]
			internal void _Lambda___R22(ModLoader.LoaderTask<int, int> a0)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				FileUtils.Write(_VB_Local_VersionFolder + ".pclignore", "用于临时地在 PCL 的版本列表中屏蔽此版本。");
			}
			[SpecialName]
			internal void _Lambda___1(ModLoader.LoaderTask<string, string> Task)
			{
				MergeJson(_VB_Local_VersionFolder, _VB_Local_VersionFolder, _VB_Local_OptiFineFolder, _VB_Local_OptiFineAsMod, _VB_Local_ForgeFolder, _VB_Local_Request.ForgeVersion, _VB_Local_NeoForgeFolder, _VB_Local_Request.NeoForgeVersion, _VB_Local_FabricFolder, _VB_Local_LiteLoaderFolder);
				Task.Progress = 0.2;
				DirectoryUtils.Copy(_VB_Local_TempMcFolder + "libraries", ModMinecraft.McFolderSelected + "libraries");
				Task.Progress = 0.8;
				string text = new ModMinecraft.McInstance(_VB_Local_VersionFolder).PathIndie + "mods\\";
				if (DirectoryUtils.Exists(_VB_Local_ModsTempFolder))
				{
					DirectoryUtils.Copy(_VB_Local_ModsTempFolder, text);
				}
				else if (_VB_Local_Modable)
				{
					DirectoryUtils.Create(text);
					Logger.Info($"自动创建 Mod 文件夹：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				string text2 = new ModMinecraft.McInstance(_VB_Local_VersionFolder).PathIndie + "resourcepacks\\";
				DirectoryUtils.Create(text2);
				Logger.Info($"自动创建资源包文件夹：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			[SpecialName]
			internal void _Lambda___2(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = ModMinecraft.McLibNetFilesFromInstance(new ModMinecraft.McInstance(_VB_Local_VersionFolder));
			}
			[SpecialName]
			internal void _Lambda___R23(ModLoader.LoaderTask<int, int> a0)
			{
				_Lambda___3();
			}
			[SpecialName]
			internal void _Lambda___3()
			{
				FileUtils.Delete(_VB_Local_VersionFolder + ".pclignore");
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___8_0
		{
			public string _VB_Local_VersionFolder;
			public string _VB_Local_Id;
			public _Closure___8_0(_Closure___8_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_VersionFolder = arg0._VB_Local_VersionFolder;
					_VB_Local_Id = arg0._VB_Local_Id;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(_VB_Local_VersionFolder);
				if (mcInstance.JsonObject["downloads"] == null || mcInstance.JsonObject["downloads"]["server"] == null || mcInstance.JsonObject["downloads"]["server"]["url"] == null)
				{
					FileUtils.Delete(_VB_Local_VersionFolder + _VB_Local_Id + ".json");
					if (!DirectoryUtils.GetInfo(_VB_Local_VersionFolder).GetFileSystemInfos().Any())
					{
						DirectoryUtils.Delete(_VB_Local_VersionFolder);
					}
					Task.Output = new List<ModNet.NetFile>();
					ModMain.Hint($"Mojang 没有给 Minecraft {_VB_Local_Id} 提供官方服务端下载，没法下，撤退！", ModMain.HintType.Red);
					Thread.Sleep(2000);
					Task.Cancel();
				}
				else
				{
					string original = (string?)mcInstance.JsonObject["downloads"]["server"]["url"];
					FileChecker checker = new FileChecker
					{
						MinSize = 1024L,
						ActualSize = (long)(mcInstance.JsonObject["downloads"]["server"]["size"] ?? ((JToken)(-1))),
						Hash = (string?)mcInstance.JsonObject["downloads"]["server"]["sha1"]
					};
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), _VB_Local_VersionFolder + _VB_Local_Id + "-server.jar", checker)
					};
					string text = $"@echo off\r\ntitle {_VB_Local_Id} 原版服务端\r\necho 如果服务端立即停止，请右键编辑该脚本，将下一行开头的 java 替换为适合该 Minecraft 版本的完整 java.exe 的路径。\r\necho 你可以在 PCL 的 [设置 → 启动选项] 中查看已安装的 java，所需的 java.exe 一般在其中的 bin 文件夹下。\r\necho ------------------------------\r\necho 如果提示 \"You need to agree to the EULA in order to run the server\"，请打开 eula.txt，按说明阅读并同意 Minecraft EULA 后，将该文件最后一行中的 eula=false 改为 eula=true。\r\necho ------------------------------\r\n\"java\" -server -XX:+UseG1GC -Xmx6144M -Xms1024M -jar {_VB_Local_Id}-server.jar nogui\r\necho ----------------------\r\necho 服务端已停止。\r\npause";
					FileUtils.Write(_VB_Local_VersionFolder + "Launch Server.bat", text, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
					FileUtils.Delete(_VB_Local_VersionFolder + _VB_Local_Id + ".json");
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___9_0
		{
			public string _VB_Local_VersionFolder;
			public _Closure___9_0(_Closure___9_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_VersionFolder = arg0._VB_Local_VersionFolder;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
			{
				Task.Output = new List<ModNet.NetFile> { ModDownload.DlClientJarGet(new ModMinecraft.McInstance(_VB_Local_VersionFolder), ReturnNothingOnFileUseable: false) };
			}
		}
		private const string McDownloadClientLibName = "下载原版支持库文件";
		private const string McDownloadClientJsonName = "下载原版 json 文件";
		private static bool IsForgeRecommendedRefreshed = false;
		private static object InstallSyncLock = RuntimeHelpers.GetObjectValue(new object());
		private static object VanillaSyncLock = RuntimeHelpers.GetObjectValue(new object());
		private static List<ModLoader.LoaderBase> McDownloadClientLoader(string Id, string JsonUrl = null, string InstanceName = null)
		{
			InstanceName = InstanceName ?? Id;
			string text = ModMinecraft.McFolderSelected + "versions\\" + InstanceName + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			ModMinecraft.McInstance mcInstance = null;
			if (JsonUrl == null)
			{
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取原版 json 文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					string original = Conversions.ToString(ModDownload.DlClientListGet(Id));
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), text + InstanceName + ".json")
					};
				})
				{
					ProgressWeight = 2.0,
					Show = false
				});
			}
			list.Add(new ModNet.LoaderDownload("下载原版 json 文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(JsonUrl ?? ""), text + InstanceName + ".json", new FileChecker
				{
					IsJson = true
				})
			})
			{
				ProgressWeight = 3.0
			});
			list.Add(new ModLoader.LoaderTask<string, string>("读取 json 文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				mcInstance = new ModMinecraft.McInstance(text);
				mcInstance.Load();
			})
			{
				ProgressWeight = 0.0,
				Show = false
			});
			List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
			list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析原版支持库文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				Logger.Info($"开始分析原版支持库文件：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				Task.Output = ModMinecraft.McLibNetFilesFromInstance(mcInstance);
			})
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list2.Add(new ModNet.LoaderDownload("下载原版支持库文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 13.0,
				Show = false
			});
			list.Add(new ModLoader.LoaderCombo<string>("下载原版支持库文件", list2)
			{
				Block = false,
				ProgressWeight = 14.0
			});
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				try
				{
					ModNet.NetFile netFile = ModDownload.DlClientAssetIndexGet(mcInstance);
					Task.Output = ((netFile == null) ? new List<ModNet.NetFile>() : new List<ModNet.NetFile> { netFile });
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("分析资源文件索引地址失败", innerException);
				}
				try
				{
					JObject jObject = (JObject)FileUtils.ReadAsJson(text + InstanceName + ".json");
					jObject.Add("clientVersion", Id);
					FileUtils.Write($"{text}{InstanceName}.json", jObject.ToString());
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception innerException2 = ex2;
					throw new Exception("添加客户端版本失败", innerException2);
				}
			})
			{
				ProgressWeight = 1.0,
				Show = false
			});
			list3.Add(new ModNet.LoaderDownload("下载资源文件索引（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 3.0,
				Show = false
			});
			list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析所需资源文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
				ModMinecraft.McInstance instance = mcInstance;
				ModLoader.LoaderBase ProgressFeed = Task;
				List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList(instance, CheckHash: true, ref ProgressFeed);
				Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)ProgressFeed;
				loaderTask.Output = output;
			})
			{
				ProgressWeight = 0.01,
				Show = false
			});
			list3.Add(new ModNet.LoaderDownload("下载资源文件（副加载器）", new List<ModNet.NetFile>())
			{
				ProgressWeight = 14.0,
				Show = false
			});
			list.Add(new ModLoader.LoaderCombo<string>("下载原版资源文件", list3)
			{
				Block = false,
				ProgressWeight = 18.0
			});
			return list;
		}
		public static MyVirtualizingElement<MyListItem> McDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				JToken jToken = Entry["type"];
				string logo = (((string?)jToken == "release") ? (ModBase.PathImage + "Blocks/Grass.png") : (((string?)jToken == "snapshot") ? (ModBase.PathImage + "Blocks/CommandBlock.png") : ((!((string?)jToken == "special")) ? (ModBase.PathImage + "Blocks/CobbleStone.png") : (ModBase.PathImage + "Blocks/GoldBlock.png"))));
				MyListItem myListItem = new MyListItem
				{
					Logo = logo,
					SnapsToDevicePixels = true,
					Title = Entry["id"].ToString(),
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Entry
				};
				if (Entry["lore"] == null)
				{
					myListItem.Info = Entry["releaseTime"].Value<DateTime>().ToString("yyyy'/'MM'/'dd HH':'mm");
				}
				else
				{
					myListItem.Info = Entry["lore"].ToString();
				}
				if (Entry["url"].ToString().Contains("pcl"))
				{
					myListItem.Info = "[PCL 特供下载] " + myListItem.Info;
				}
				myListItem.Click += OnClick;
				if (IsSaveOnly)
				{
					myListItem.ContentHandler = McDownloadSaveMenuBuild;
				}
				else
				{
					myListItem.ContentHandler = McDownloadMenuBuild;
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		private static void McDownloadSaveMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
				ToolTip = "下载服务端"
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[2] { myIconButton2, myIconButton } }, null, null);
		}
		private static void McDownloadMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				McDownloadMenuSave(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton3 = new MyIconButton
			{
				LogoScale = 1.0,
				Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
				ToolTip = "下载服务端"
			};
			myIconButton3.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[3] { myIconButton, myIconButton2, myIconButton3 } }, null, null);
		}
		private static void McDownloadMenuLog(object sender, RoutedEventArgs e)
		{
			JToken instanceJson;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				instanceJson = (JToken)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				instanceJson = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			McUpdateLogShow(instanceJson);
		}
		private static void McDownloadMenuSaveServer(object sender, RoutedEventArgs e)
		{
			MyListItem myListItem;
			if (sender is MyListItem)
			{
				myListItem = (MyListItem)sender;
			}
			else
			{
				myListItem = ((!(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)) ? ((MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null)) : ((MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)));
			}
			try
			{
				_Closure___8_0 arg = default;
				_Closure___8_0 CS_8__locals24 = new _Closure___8_0(arg);
				CS_8__locals24._VB_Local_Id = myListItem.Title;
				string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[1] { "url" }, null).ToString();
				CS_8__locals24._VB_Local_VersionFolder = Dialogs.SelectFolder("选择目标文件夹", multiselect: false).FirstOrDefault();
				if (CS_8__locals24._VB_Local_VersionFolder == null)
				{
					return;
				}
				CS_8__locals24._VB_Local_VersionFolder = CS_8__locals24._VB_Local_VersionFolder + CS_8__locals24._VB_Local_Id + "\\";
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"Minecraft {CS_8__locals24._VB_Local_Id} 服务端下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该服务端正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
				list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
				{
					new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS_8__locals24._VB_Local_VersionFolder + CS_8__locals24._VB_Local_Id + ".json", new FileChecker
					{
						IsJson = true
					})
				})
				{
					ProgressWeight = 2.0
				});
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("构建服务端", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(CS_8__locals24._VB_Local_VersionFolder);
					if (mcInstance.JsonObject["downloads"] == null || mcInstance.JsonObject["downloads"]["server"] == null || mcInstance.JsonObject["downloads"]["server"]["url"] == null)
					{
						FileUtils.Delete(CS_8__locals24._VB_Local_VersionFolder + CS_8__locals24._VB_Local_Id + ".json");
						if (!DirectoryUtils.GetInfo(CS_8__locals24._VB_Local_VersionFolder).GetFileSystemInfos().Any())
						{
							DirectoryUtils.Delete(CS_8__locals24._VB_Local_VersionFolder);
						}
						Task.Output = new List<ModNet.NetFile>();
						ModMain.Hint($"Mojang 没有给 Minecraft {CS_8__locals24._VB_Local_Id} 提供官方服务端下载，没法下，撤退！", ModMain.HintType.Red);
						Thread.Sleep(2000);
						Task.Cancel();
					}
					else
					{
						string original2 = (string?)mcInstance.JsonObject["downloads"]["server"]["url"];
						FileChecker checker = new FileChecker
						{
							MinSize = 1024L,
							ActualSize = (long)(mcInstance.JsonObject["downloads"]["server"]["size"] ?? ((JToken)(-1))),
							Hash = (string?)mcInstance.JsonObject["downloads"]["server"]["sha1"]
						};
						Task.Output = new List<ModNet.NetFile>
						{
							new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original2), CS_8__locals24._VB_Local_VersionFolder + CS_8__locals24._VB_Local_Id + "-server.jar", checker)
						};
						string text = $"@echo off\r\ntitle {CS_8__locals24._VB_Local_Id} 原版服务端\r\necho 如果服务端立即停止，请右键编辑该脚本，将下一行开头的 java 替换为适合该 Minecraft 版本的完整 java.exe 的路径。\r\necho 你可以在 PCL 的 [设置 → 启动选项] 中查看已安装的 java，所需的 java.exe 一般在其中的 bin 文件夹下。\r\necho ------------------------------\r\necho 如果提示 \"You need to agree to the EULA in order to run the server\"，请打开 eula.txt，按说明阅读并同意 Minecraft EULA 后，将该文件最后一行中的 eula=false 改为 eula=true。\r\necho ------------------------------\r\n\"java\" -server -XX:+UseG1GC -Xmx6144M -Xms1024M -jar {CS_8__locals24._VB_Local_Id}-server.jar nogui\r\necho ----------------------\r\necho 服务端已停止。\r\npause";
						FileUtils.Write(CS_8__locals24._VB_Local_VersionFolder + "Launch Server.bat", text, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
						FileUtils.Delete(CS_8__locals24._VB_Local_VersionFolder + CS_8__locals24._VB_Local_Id + ".json");
					}
				})
				{
					ProgressWeight = 0.5,
					Show = false
				});
				list.Add(new ModNet.LoaderDownload("下载服务端文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 5.0
				});
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + CS_8__locals24._VB_Local_Id + " 服务端下载", list);
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(CS_8__locals24._VB_Local_Id);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始 Minecraft 服务端下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static void McDownloadMenuSave(object sender, RoutedEventArgs e)
		{
			MyListItem myListItem;
			if (sender is MyListItem)
			{
				myListItem = (MyListItem)sender;
			}
			else
			{
				myListItem = ((!(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)) ? ((MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null)) : ((MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null)));
			}
			try
			{
				_Closure___9_0 arg = default;
				_Closure___9_0 CS_8__locals6 = new _Closure___9_0(arg);
				string title = myListItem.Title;
				string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[1] { "url" }, null).ToString();
				CS_8__locals6._VB_Local_VersionFolder = Dialogs.SelectFolder("选择目标文件夹", multiselect: false).FirstOrDefault();
				if (CS_8__locals6._VB_Local_VersionFolder == null)
				{
					return;
				}
				CS_8__locals6._VB_Local_VersionFolder = CS_8__locals6._VB_Local_VersionFolder + title + "\\";
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"Minecraft {title} 下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
				list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
				{
					new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS_8__locals6._VB_Local_VersionFolder + title + ".json", new FileChecker
					{
						IsJson = true
					})
				})
				{
					ProgressWeight = 2.0
				});
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析核心 JAR 文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					Task.Output = new List<ModNet.NetFile> { ModDownload.DlClientJarGet(new ModMinecraft.McInstance(CS_8__locals6._VB_Local_VersionFolder), ReturnNothingOnFileUseable: false) };
				})
				{
					ProgressWeight = 0.5,
					Show = false
				});
				list.Add(new ModNet.LoaderDownload("下载核心 JAR 文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 5.0
				});
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + title + " 下载", list);
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(title);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始 Minecraft 下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static void McUpdateLogShow(JToken InstanceJson)
		{
			string text = InstanceJson["id"].ToString().Lower();
			string text2;
			switch (text)
			{
			case "3d shareware v1.34":
				text2 = "3D_Shareware_v1.34";
				break;
			case "2.0":
				text2 = "Java版2.0";
				break;
			case "1.rv-pre1":
				text2 = "Java版1.RV-Pre1";
				break;
			default:
				if (!text.Contains("combat-1") && !text.Contains("combat-212796"))
				{
					if (Operators.CompareString(text, "combat test 2", TextCompare: false) == 0 || text.Contains("combat-2") || text.Contains("combat-0"))
					{
						text2 = "Java版Combat_Test_2";
						break;
					}
					switch (text)
					{
					case "combat test 3":
					case "1.14_combat-3":
						text2 = "Java版Combat_Test_3";
						break;
					case "combat test 4":
					case "1.15_combat-1":
						text2 = "Java版Combat_Test_4";
						break;
					case "combat test 5":
					case "1.15_combat-6":
						text2 = "Java版Combat_Test_5";
						break;
					case "combat test 6":
					case "1.16_combat-0":
						text2 = "Java版Combat_Test_6";
						break;
					case "combat test 7c":
					case "1.16_combat-3":
						text2 = "Java版Combat_Test_7c";
						break;
					case "combat test 8b":
					case "1.16_combat-5":
						text2 = "Java版Combat_Test_8b";
						break;
					case "combat test 8c":
					case "1.16_combat-6":
						text2 = "Java版Combat_Test_8c";
						break;
					case "1.0.0-rc2-2":
						text2 = "Java版RC2";
						break;
					default:
						if (text.StartsWithF("1.19_deep_dark_experimental_snapshot-") || text.StartsWithF("1_19_deep_dark_experimental_snapshot-"))
						{
							text2 = text.Replace("1_19", "1.19").Replace("1.19_deep_dark_experimental_snapshot-", "Java版Deep_Dark_Experimental_Snapshot_");
							break;
						}
						if (Operators.CompareString(text, "b1.9-pre6", TextCompare: false) == 0)
						{
							text2 = "Java版Beta_1.9_Prerelease_6";
							break;
						}
						if (text.Contains("b1.9"))
						{
							text2 = "Java版Beta_1.9_Prerelease";
							break;
						}
						if ((string?)InstanceJson["type"] == "release" || (string?)InstanceJson["type"] == "snapshot" || (string?)InstanceJson["type"] == "special")
						{
							text2 = (text.Contains("w") ? "" : "Java版") + text.Replace(" Pre-Release ", "-pre");
							break;
						}
						if (text.StartsWithF("b"))
						{
							text2 = "Java版" + text.TrimEnd('a', 'b', 'c', 'd', 'e').Replace("b", "Beta_");
							break;
						}
						if (text.StartsWithF("a"))
						{
							text2 = "Java版" + text.TrimEnd('a', 'b', 'c', 'd', 'e').Replace("a", "Alpha_v");
							break;
						}
						switch (text)
						{
						case "inf-20100618":
							text2 = "Java版Infdev_20100618";
							break;
						default:
							if (!text.Contains("生存测试"))
							{
								if (text.StartsWithF("c0.31"))
								{
									text2 = "Java版Indev_0.31_20100130";
									break;
								}
								if (text.StartsWithF("c"))
								{
									text2 = "Java版" + text.Replace("c", "Classic_");
									break;
								}
								if (text.StartsWithF("rd-"))
								{
									text2 = "Java版Pre-classic_" + text;
									break;
								}
								Logger.Error($"未知的版本格式：{text}。", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								return;
							}
							goto case "c0.30_01c";
						case "c0.30_01c":
						case "c0.30_survival":
							text2 = "Java版Classic_0.30（生存模式）";
							break;
						}
						break;
					}
					break;
				}
				goto case "combat test 1";
			case "combat test 1":
				text2 = "Java版1.14.3_-_Combat_Test";
				break;
			}
			ModBase.OpenWebsite("https://zh.minecraft.wiki/w/" + text2.Replace("_experimental-snapshot-", "-exp"));
		}
		private static void McDownloadOptiFineSave(ModDownload.DlOptiFineListEntry DownloadInfo)
		{
			try
			{
				_ = DownloadInfo.InstanceName;
				string text = Dialogs.SaveFile("选择保存位置", DownloadInfo.FileName, null, new (string, string)[1] { ("jar", "OptiFine Jar") });
				if (text == null)
				{
					return;
				}
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"OptiFine {DownloadInfo.DisplayName} 下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry>("OptiFine " + DownloadInfo.DisplayName + " 下载", McDownloadOptiFineSaveLoader(DownloadInfo, text));
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(DownloadInfo);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始 OptiFine 下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static void McDownloadOptiFineInstall(string BaseMcFolderHome, string Target, ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task, bool UseJavaWrapper)
		{
			_Closure___12_2 obj = new _Closure___12_2(default(_Closure___12_2));
			obj._VB_Local_Task = Task;
			ValueRange<Version> valueRange = ValueRange<Version>.AtLeast(new Version(21, 0));
			checked
			{
				try
				{
					using ZipArchive zipArchive = FileUtils.OpenZip(Target);
					using Stream input = (zipArchive.GetEntry("optifine/Installer.class") ?? throw new FileNotFoundException("未找到 optifine/Installer.class")).Open();
					using BinaryReader binaryReader = new BinaryReader(input);
					byte[] array = binaryReader.ReadBytes(8);
					if (array.Length < 8)
					{
						throw new IOException("Installer.class 长度不足");
					}
					if (array[0] != 202 || array[1] != 254 || array[2] != 186 || array[3] != 190)
					{
						throw new InvalidDataException("Installer.class 文件头无效");
					}
					int num = array[6] * 256 + array[7];
					if (num < 49)
					{
						throw new InvalidDataException("Installer.class 版本过低");
					}
					if (num > 100)
					{
						throw new InvalidDataException("Installer.class 版本过高");
					}
					valueRange = ValueRange<Version>.AtLeast(new Version(num - 44, 0));
					Logger.Info($"OptiFine 安装器 class 版本：{num}，需要 Java {valueRange}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "读取 OptiFine 安装器 Java 版本失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				Java java = ModJava.SelectOrDownloadJava(valueRange, TryFixOrHint: true, obj._VB_Local_Task.CreateCancellationToken(), obj._VB_Local_Task.CreateSyncProgressProvider(0.0, 0.2));
				if (obj._VB_Local_Task.IsCanceled)
				{
					return;
				}
				if (java == null)
				{
					throw new OperationCanceledException();
				}
				string text = ((!UseJavaWrapper || Settings.Get<bool>("LaunchAdvanceDisableJLW")) ? $"-Duser.home=\"{BaseMcFolderHome.TrimEnd('\\')}\" -cp \"{Target}\" optifine.Installer" : string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -Duser.home=\"{1}\" -cp \"{2}\" -jar \"{3}\" optifine.Installer", ModBase.PathPure.Value.TrimEnd('\\'), BaseMcFolderHome.TrimEnd('\\'), Target, LaunchUtils.ExtractPatch("Launch\\JavaWrapper.jar", ModBase.PathPure.Value)));
				if (java.Version.Major >= 9)
				{
					text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
				}
				object installSyncLock = InstallSyncLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(installSyncLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(installSyncLock, ref lockTaken);
					_Closure___12_1 obj2 = new _Closure___12_1(default(_Closure___12_1));
					obj2._VB_NonLocal__VB_Closure_2 = obj;
					ProcessStartInfo processStartInfo = new ProcessStartInfo
					{
						FileName = java.JavaExePath,
						Arguments = text,
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardError = true,
						RedirectStandardOutput = true,
						WorkingDirectory = PathUtils.ToShortPath(BaseMcFolderHome)
					};
					if (processStartInfo.EnvironmentVariables.ContainsKey("appdata"))
					{
						processStartInfo.EnvironmentVariables["appdata"] = BaseMcFolderHome;
					}
					else
					{
						processStartInfo.EnvironmentVariables.Add("appdata", BaseMcFolderHome);
					}
					Logger.Info($"开始安装 OptiFine：{Target}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					obj2._VB_Local_TotalLength = 0;
					obj2._VB_Local_process = new Process
					{
						StartInfo = processStartInfo
					};
					obj2._VB_Local_LastResult = "";
					_Closure___12_0 obj3 = new _Closure___12_0(default(_Closure___12_0));
					obj3._VB_NonLocal__VB_Closure_3 = obj2;
					obj3._VB_Local_outputWaitHandle = new AutoResetEvent(initialState: false);
					try
					{
						_Closure___12_3 arg = default;
						_Closure___12_3 CS_8__locals18 = new _Closure___12_3(arg);
						CS_8__locals18._VB_NonLocal__VB_Closure_4 = obj3;
						CS_8__locals18._VB_Local_errorWaitHandle = new AutoResetEvent(initialState: false);
						try
						{
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.OutputDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
							{
								CS_8__locals18._VB_NonLocal__VB_Closure_4._Lambda___0(RuntimeHelpers.GetObjectValue(a0), a1);
							};
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.ErrorDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
							{
								CS_8__locals18._Lambda___1(RuntimeHelpers.GetObjectValue(a0), a1);
							};
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Start();
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.BeginOutputReadLine();
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.BeginErrorReadLine();
							while (!CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
							{
								Thread.Sleep(10);
							}
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_Local_outputWaitHandle.WaitOne(10000);
							CS_8__locals18._VB_Local_errorWaitHandle.WaitOne(10000);
							CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Dispose();
							if (CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_TotalLength < 1000 || CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResult.Contains("at "))
							{
								throw new Exception("安装器运行出错，末行为 " + CS_8__locals18._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResult);
							}
						}
						finally
						{
							if (CS_8__locals18._VB_Local_errorWaitHandle != null)
							{
								((IDisposable)CS_8__locals18._VB_Local_errorWaitHandle).Dispose();
							}
						}
					}
					finally
					{
						if (obj3._VB_Local_outputWaitHandle != null)
						{
							((IDisposable)obj3._VB_Local_outputWaitHandle).Dispose();
						}
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(installSyncLock);
					}
				}
			}
		}
		private static List<ModLoader.LoaderBase> McDownloadOptiFineLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string McFolder, ModLoader.LoaderCombo<string> ClientDownloadLoader, string ClientFolder)
		{
			_Closure___13_0 arg = default;
			_Closure___13_0 CS_8__locals80 = new _Closure___13_0(arg);
			CS_8__locals80._VB_Local_DownloadInfo = DownloadInfo;
			CS_8__locals80._VB_Local_McFolder = McFolder;
			CS_8__locals80._VB_Local_ClientDownloadLoader = ClientDownloadLoader;
			CS_8__locals80._VB_Local_ClientFolder = ClientFolder;
			CS_8__locals80._VB_Local_IsCustomFolder = Operators.CompareString(CS_8__locals80._VB_Local_McFolder, ModMinecraft.McFolderSelected, TextCompare: false) != 0;
			CS_8__locals80._VB_Local_Id = CS_8__locals80._VB_Local_DownloadInfo.InstanceName;
			CS_8__locals80._VB_Local_VersionFolder = CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_Id + "\\";
			bool flag = CS_8__locals80._VB_Local_DownloadInfo.Inherit.Contains("w") || ModMinecraft.McVersion.VersionToDrop(CS_8__locals80._VB_Local_DownloadInfo.Inherit) >= 140;
			CS_8__locals80._VB_Local_Target = (flag ? $"{ModMain.RequestTaskTempFolder()}OptiFine.jar" : string.Format("{0}libraries\\optifine\\OptiFine\\{1}\\{2}", CS_8__locals80._VB_Local_McFolder, CS_8__locals80._VB_Local_DownloadInfo.FileName.Replace("OptiFine_", "").Replace(".jar", "").Replace("preview_", ""), CS_8__locals80._VB_Local_DownloadInfo.FileName.Replace("OptiFine_", "OptiFine-").Replace("preview_", "")));
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 OptiFine 主文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				List<string> list2 = new List<string>();
				string text = CS_8__locals80._VB_Local_DownloadInfo.Inherit;
				if (Operators.CompareString(text, "1.8", TextCompare: false) == 0 || Operators.CompareString(text, "1.9", TextCompare: false) == 0)
				{
					text += ".0";
				}
				if (CS_8__locals80._VB_Local_DownloadInfo.IsPreview)
				{
					list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + CS_8__locals80._VB_Local_DownloadInfo.DisplayName.Replace(CS_8__locals80._VB_Local_DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
				}
				else
				{
					list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + CS_8__locals80._VB_Local_DownloadInfo.DisplayName.Replace(CS_8__locals80._VB_Local_DownloadInfo.Inherit + " ", ""));
				}
				try
				{
					string str = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + CS_8__locals80._VB_Local_DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
					Task.Progress = 0.8;
					list2.Add("https://optifine.net/" + str.RegexSearch("downloadx\\?f=[^\"']+").First());
					Logger.Info($"OptiFine {CS_8__locals80._VB_Local_DownloadInfo.DisplayName} 官方下载地址：{list2.Last()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"获取 OptiFine {CS_8__locals80._VB_Local_DownloadInfo.DisplayName} 官方下载地址失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list2.ToArray(), CS_8__locals80._VB_Local_Target, new FileChecker
					{
						MinSize = 307200L
					})
				};
			})
			{
				ProgressWeight = 8.0
			});
			list.Add(new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 8.0
			});
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("等待原版下载", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
			{
				List<ModLoader.LoaderBase> source = (from l in CS_8__locals80._VB_Local_ClientDownloadLoader.GetLoaderList()
					where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
					where l.State != ModBase.LoadState.Finished
					select l).ToList();
				if (source.Any())
				{
					Logger.Info("OptiFine 安装正在等待原版文件下载完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				while (source.Any() && !Task.IsCanceled)
				{
					source = source.Where((_Closure___._I13_4 == null) ? (_Closure___._I13_4 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure___._I13_4).ToList();
					Thread.Sleep(50);
				}
				if (!Task.IsCanceled && CS_8__locals80._VB_Local_IsCustomFolder)
				{
					object vanillaSyncLock = VanillaSyncLock;
					ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(vanillaSyncLock, ref lockTaken);
						string lastPart = PathUtils.GetLastPart(CS_8__locals80._VB_Local_ClientFolder);
						DirectoryUtils.Create(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit);
						if (!FileUtils.Exists(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".json"))
						{
							FileUtils.Copy($"{CS_8__locals80._VB_Local_ClientFolder}{lastPart}.json", $"{CS_8__locals80._VB_Local_McFolder}versions\\{CS_8__locals80._VB_Local_DownloadInfo.Inherit}\\{CS_8__locals80._VB_Local_DownloadInfo.Inherit}.json");
						}
						if (!FileUtils.Exists(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".jar"))
						{
							FileUtils.Copy($"{CS_8__locals80._VB_Local_ClientFolder}{lastPart}.jar", $"{CS_8__locals80._VB_Local_McFolder}versions\\{CS_8__locals80._VB_Local_DownloadInfo.Inherit}\\{CS_8__locals80._VB_Local_DownloadInfo.Inherit}.jar");
						}
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(vanillaSyncLock);
						}
					}
				}
			})
			{
				ProgressWeight = 0.1,
				Show = false
			});
			if (flag)
			{
				Logger.Info($"检测为新版 OptiFine：{CS_8__locals80._VB_Local_DownloadInfo.Inherit}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 A）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
				{
					string text = ModMain.RequestTaskTempFolder();
					string text2 = text + ".minecraft\\";
					try
					{
						if (DirectoryUtils.Exists(text2 + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit))
						{
							DirectoryUtils.Delete(text2 + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit);
						}
						DirectoryUtils.Create(text2 + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\");
						ModMinecraft.McFolderLauncherProfilesJsonCreate(text2);
						FileUtils.Copy(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".json", text2 + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".json");
						FileUtils.Copy(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".jar", text2 + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".jar");
						Task.Progress = 0.06;
						bool flag2 = true;
						while (true)
						{
							try
							{
								McDownloadOptiFineInstall(text, CS_8__locals80._VB_Local_Target, Task, flag2);
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								if (!flag2)
								{
									throw new Exception("运行 OptiFine 安装器失败", ex2);
								}
								Logger.Warn(ex2, "使用 JavaWrapper 安装 OptiFine 失败，将不使用 JavaWrapper 并重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								flag2 = false;
								ProjectData.ClearProjectError();
								continue;
							}
							break;
						}
						Task.Progress = 0.96;
						FileUtils.Delete(text2 + "launcher_profiles.json");
						DirectoryUtils.Copy(text2, CS_8__locals80._VB_Local_McFolder);
						Task.Progress = 0.98;
						FileUtils.Delete(CS_8__locals80._VB_Local_Target);
						DirectoryUtils.Delete(text);
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception innerException = ex3;
						throw new Exception("安装 OptiFine（方式 A）失败", innerException);
					}
				})
				{
					ProgressWeight = 8.0
				});
			}
			else
			{
				Logger.Info($"检测为旧版 OptiFine：{CS_8__locals80._VB_Local_DownloadInfo.Inherit}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 B）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
				{
					try
					{
						FileUtils.Copy(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + ".jar", CS_8__locals80._VB_Local_VersionFolder + CS_8__locals80._VB_Local_Id + ".jar");
						Task.Progress = 0.7;
						ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(CS_8__locals80._VB_Local_McFolder + "versions\\" + CS_8__locals80._VB_Local_DownloadInfo.Inherit);
						string text = "{\r\n    \"id\": \"" + CS_8__locals80._VB_Local_Id + "\",\r\n    \"inheritsFrom\": \"" + CS_8__locals80._VB_Local_DownloadInfo.Inherit + "\",\r\n    \"time\": \"" + ((Operators.CompareString(CS_8__locals80._VB_Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : CS_8__locals80._VB_Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"releaseTime\": \"" + ((Operators.CompareString(CS_8__locals80._VB_Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : CS_8__locals80._VB_Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"type\": \"release\",\r\n    \"libraries\": [\r\n        {\"name\": \"optifine:OptiFine:" + CS_8__locals80._VB_Local_DownloadInfo.FileName.Replace("OptiFine_", "").Replace(".jar", "").Replace("preview_", "") + "\"},\r\n        {\"name\": \"net.minecraft:launchwrapper:1.12\"}\r\n    ],\r\n    \"mainClass\": \"net.minecraft.launchwrapper.Launch\",";
						Task.Progress = 0.8;
						FileUtils.Write(text: (!mcInstance.IsOldJson) ? (text + "\r\n    \"minimumLauncherVersion\": \"21\",\r\n    \"arguments\": {\r\n        \"game\": [\r\n            \"--tweakClass\",\r\n            \"optifine.OptiFineTweaker\"\r\n        ]\r\n    }\r\n}") : (text + "\r\n    \"minimumLauncherVersion\": 18,\r\n    \"minecraftArguments\": \"" + mcInstance.JsonObject["minecraftArguments"].ToString() + "  --tweakClass optifine.OptiFineTweaker\"\r\n}"), filePath: CS_8__locals80._VB_Local_VersionFolder + CS_8__locals80._VB_Local_Id + ".json");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception("安装 OptiFine（方式 B）失败", innerException);
					}
				})
				{
					ProgressWeight = 1.0
				});
			}
			return list;
		}
		private static List<ModLoader.LoaderBase> McDownloadOptiFineSaveLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string TargetFolder)
		{
			return new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>>("获取 OptiFine 下载地址", [SpecialName] (ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>> Task) =>
				{
					List<string> list = new List<string>();
					string text = DownloadInfo.Inherit;
					if (Operators.CompareString(text, "1.8", TextCompare: false) == 0 || Operators.CompareString(text, "1.9", TextCompare: false) == 0)
					{
						text += ".0";
					}
					if (DownloadInfo.IsPreview)
					{
						list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + DownloadInfo.DisplayName.Replace(DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
					}
					else
					{
						list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + DownloadInfo.DisplayName.Replace(DownloadInfo.Inherit + " ", ""));
					}
					try
					{
						string str = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
						Task.Progress = 0.8;
						list.Add("https://optifine.net/" + str.RegexSearch("downloadx\\?f=[^\"']+").First());
						Logger.Info($"OptiFine {DownloadInfo.DisplayName} 官方下载地址：{list.Last()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"获取 OptiFine {DownloadInfo.DisplayName} 官方下载地址失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						ProjectData.ClearProjectError();
					}
					Task.Progress = 0.9;
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(list.ToArray(), TargetFolder, new FileChecker
						{
							MinSize = 65536L
						})
					};
				})
				{
					ProgressWeight = 6.0
				},
				new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 10.0,
					Block = true
				}
			};
		}
		public static MyVirtualizingElement<MyListItem> OptiFineDownloadListItem(ModDownload.DlOptiFineListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Title = Entry.DisplayName,
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Entry,
					Info = (Entry.IsPreview ? "测试版" : "正式版") + ((Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("，发布于 " + Entry.ReleaseTime)) + ((Entry.RequiredForgeVersion == null) ? "，不兼容 Forge" : ((Operators.CompareString(Entry.RequiredForgeVersion, "", TextCompare: false) == 0) ? "" : ("，兼容 Forge " + Entry.RequiredForgeVersion))),
					Logo = ModBase.PathImage + "Blocks/GrassPath.png"
				};
				myListItem.Click += OnClick;
				if (IsSaveOnly)
				{
					myListItem.ContentHandler = OptiFineSaveContMenuBuild;
				}
				else
				{
					myListItem.ContentHandler = OptiFineContMenuBuild;
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		private static void OptiFineSaveContMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[1] { myIconButton } }, null, null);
		}
		private static void OptiFineContMenuBuild(object sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				OptiFineSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[2] { myIconButton, myIconButton2 } }, null, null);
		}
		private static void OptiFineLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlOptiFineListEntry dlOptiFineListEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlOptiFineListEntry = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlOptiFineListEntry = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			ModBase.OpenWebsite("https://optifine.net/changelog?f=" + dlOptiFineListEntry.FileName);
		}
		public static void OptiFineSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlOptiFineListEntry downloadInfo;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				downloadInfo = (ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				downloadInfo = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			McDownloadOptiFineSave(downloadInfo);
		}
		private static void McDownloadLiteLoaderSave(ModDownload.DlLiteLoaderListEntry DownloadInfo)
		{
			try
			{
				string inherit = DownloadInfo.Inherit;
				string text = Dialogs.SaveFile("选择保存位置", DownloadInfo.FileName.Replace("-SNAPSHOT", ""), null, new (string, string)[1] { ("jar", "LiteLoader 安装器") });
				if (text == null)
				{
					return;
				}
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"LiteLoader {inherit} 下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
				List<string> list2 = new List<string>();
				if (DownloadInfo.IsLegacy)
				{
					switch (DownloadInfo.Inherit)
					{
					case "1.7.10":
						list2.Add("https://dl.liteloader.com/redist/1.7.10/liteloader-installer-1.7.10-04.jar");
						break;
					case "1.7.2":
						list2.Add("https://dl.liteloader.com/redist/1.7.2/liteloader-installer-1.7.2-04.jar");
						break;
					case "1.6.4":
						list2.Add("https://dl.liteloader.com/redist/1.6.4/liteloader-installer-1.6.4-01.jar");
						break;
					case "1.6.2":
						list2.Add("https://dl.liteloader.com/redist/1.6.2/liteloader-installer-1.6.2-04.jar");
						break;
					case "1.5.2":
						list2.Add("https://dl.liteloader.com/redist/1.5.2/liteloader-installer-1.5.2-01.jar");
						break;
					default:
						throw new NotSupportedException("未知的 Minecraft 版本（" + DownloadInfo.Inherit + "）");
					}
				}
				else
				{
					list2.Add("http://jenkins.liteloader.com/job/LiteLoaderInstaller%20" + DownloadInfo.Inherit + "/lastSuccessfulBuild/artifact/" + ((Operators.CompareString(DownloadInfo.Inherit, "1.8", TextCompare: false) == 0) ? "ant/dist/" : "build/libs/") + DownloadInfo.FileName);
				}
				list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list2.ToArray(), text, new FileChecker
					{
						MinSize = 1048576L
					})
				})
				{
					ProgressWeight = 15.0
				});
				ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry>("LiteLoader " + inherit + " 安装器下载", list);
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(DownloadInfo);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始 LiteLoader 安装器下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static List<ModLoader.LoaderBase> McDownloadLiteLoaderLoader(ModDownload.DlLiteLoaderListEntry DownloadInfo, string McFolder)
		{
			Operators.CompareString(McFolder, ModMinecraft.McFolderSelected, TextCompare: false);
			string inherit = DownloadInfo.Inherit;
			_ = ModBase.PathTemp + "Download\\" + inherit + "-Liteloader.jar";
			string text = DownloadInfo.Inherit + "-LiteLoader";
			string text2 = McFolder + "versions\\" + text + "\\";
			return new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<string, string>("安装 LiteLoader", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
				{
					try
					{
						JObject jObject = new JObject
						{
							{ "id", text },
							{
								"time",
								DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
							},
							{
								"releaseTime",
								DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
							},
							{ "type", "release" },
							{
								"arguments",
								("{\"game\":[\"--tweakClass\",\"" + DownloadInfo.JsonToken["tweakClass"].ToString() + "\"]}").DeserializeJson()
							},
							{
								"libraries",
								DownloadInfo.JsonToken["libraries"]
							}
						};
						((JContainer)jObject["libraries"]).Add(("{\"name\": \"com.mumfrey:liteloader:" + DownloadInfo.JsonToken["version"].ToString() + "\",\"url\": \"https://dl.liteloader.com/versions/\"}").DeserializeJson());
						jObject.Add("mainClass", "net.minecraft.launchwrapper.Launch");
						jObject.Add("minimumLauncherVersion", 18);
						jObject.Add("inheritsFrom", DownloadInfo.Inherit);
						jObject.Add("jar", DownloadInfo.Inherit);
						FileUtils.Write(text2 + text + ".json", jObject.ToString());
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception("安装新 LiteLoader 版本失败", innerException);
					}
				})
				{
					ProgressWeight = 1.0
				}
			};
		}
		public static MyVirtualizingElement<MyListItem> LiteLoaderDownloadListItem(ModDownload.DlLiteLoaderListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Title = Entry.Inherit,
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Entry,
					Info = (Entry.IsPreview ? "测试版" : "稳定版") + ((Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("，发布于 " + Entry.ReleaseTime)),
					Logo = ModBase.PathImage + "Blocks/Egg.png"
				};
				myListItem.Click += OnClick;
				if (IsSaveOnly)
				{
					myListItem.ContentHandler = LiteLoaderSaveContMenuBuild;
				}
				else
				{
					myListItem.ContentHandler = LiteLoaderContMenuBuild;
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		private static void LiteLoaderSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			if (Conversions.ToBoolean(NewLateBinding.LateGet(sender.Tag, null, "IsLegacy", new object[0], null, null, null)))
			{
				sender.Buttons = new MyIconButton[0];
				return;
			}
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
				ToolTip = "查看全部版本",
				Tag = sender
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[1] { myIconButton };
		}
		private static void LiteLoaderContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "保存安装器",
				Tag = sender
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				LiteLoaderSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			if (Conversions.ToBoolean(NewLateBinding.LateGet(sender.Tag, null, "IsLegacy", new object[0], null, null, null)))
			{
				sender.Buttons = new MyIconButton[1] { myIconButton };
				return;
			}
			MyIconButton myIconButton2 = new MyIconButton
			{
				Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
				ToolTip = "查看全部版本",
				Tag = sender
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
		}
		private static void LiteLoaderAll_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlLiteLoaderListEntry dlLiteLoaderListEntry = ((!(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)) ? ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
			ModBase.OpenWebsite("https://jenkins.liteloader.com/view/" + dlLiteLoaderListEntry.Inherit);
		}
		public static void LiteLoaderSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlLiteLoaderListEntry downloadInfo = ((!(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)) ? ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
			McDownloadLiteLoaderSave(downloadInfo);
		}
		public static void McDownloadForgelikeSave(ModDownload.DlForgelikeEntry Info)
		{
			try
			{
				string text = Dialogs.SaveFile("选择保存位置", $"{Info.LoaderName}-{Info.Inherit}-{Info.VersionName}.{Info.FileExtension}", null, new (string, string)[1] { (Info.FileExtension, $"{Info.LoaderName} 安装器") });
				if (text == null)
				{
					return;
				}
				string text2 = $"{Info.LoaderName} {Info.Inherit} - {Info.VersionName}";
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"{text2} 下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				List<ModNet.NetFile> list = new List<ModNet.NetFile>();
				if (Info.IsNeoForge)
				{
					string text3 = ((ModDownload.DlNeoForgeListEntry)Info).UrlBase + "-installer.jar";
					list.Add(new ModNet.NetFile(new string[2]
					{
						text3.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
						text3
					}, text, new FileChecker
					{
						MinSize = 65536L
					}));
				}
				else
				{
					ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)Info;
					list.Add(new ModNet.NetFile(new string[2]
					{
						$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}-{dlForgeVersionEntry.Category}.{dlForgeVersionEntry.FileExtension}",
						$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}-{dlForgeVersionEntry.Category}.{dlForgeVersionEntry.FileExtension}"
					}, text, new FileChecker
					{
						MinSize = 65536L,
						Hash = dlForgeVersionEntry.Hash
					}));
				}
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				list2.Add(new ModNet.LoaderDownload("下载主文件", list)
				{
					ProgressWeight = 6.0
				});
				ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry>(text2 + " 下载", list2);
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(Info);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"开始 {Info.LoaderName} 安装器下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static void ForgelikeInjector(string Target, ModLoader.LoaderTask<bool, bool> Task, string McFolder, bool UseJavaWrapper, bool IsNeoForge)
		{
			_Closure___28_2 obj = new _Closure___28_2(default(_Closure___28_2));
			obj._VB_Local_Task = Task;
			Java java = ModJava.SelectOrDownloadJava(ValueRange<Version>.AtLeast(new Version(8, 0, 60)), TryFixOrHint: true, obj._VB_Local_Task.CreateCancellationToken(), obj._VB_Local_Task.CreateSyncProgressProvider(0.0, 0.2));
			if (obj._VB_Local_Task.IsCanceled)
			{
				return;
			}
			if (java == null)
			{
				throw new OperationCanceledException();
			}
			string text = ((!UseJavaWrapper || Settings.Get<bool>("LaunchAdvanceDisableJLW")) ? $"-cp \"{ModBase.PathTemp}Cache\\forge_installer.jar;{Target}\" com.bangbang93.ForgeInstaller \"{McFolder}" : string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -cp \"{1}Cache\\forge_installer.jar;{2}\" -jar \"{3}\" com.bangbang93.ForgeInstaller \"{4}", ModBase.PathPure.Value.TrimEnd('\\'), ModBase.PathTemp, Target, LaunchUtils.ExtractPatch("Launch\\JavaWrapper.jar", ModBase.PathPure.Value), McFolder));
			if (java.Version.Major >= 9)
			{
				text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
			}
			object installSyncLock = InstallSyncLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(installSyncLock);
			bool lockTaken = false;
			checked
			{
				try
				{
					Monitor.Enter(installSyncLock, ref lockTaken);
					_Closure___28_1 obj2 = new _Closure___28_1(default(_Closure___28_1));
					obj2._VB_NonLocal__VB_Closure_2 = obj;
					ProcessStartInfo startInfo = new ProcessStartInfo
					{
						FileName = java.JavaExePath,
						Arguments = text,
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardError = true,
						RedirectStandardOutput = true
					};
					obj2._VB_Local_LoaderName = (IsNeoForge ? "NeoForge" : "Forge");
					Logger.Info($"开始安装 {obj2._VB_Local_LoaderName}：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					obj2._VB_Local_process = new Process
					{
						StartInfo = startInfo
					};
					obj2._VB_Local_LastResults = new Queue<string>();
					_Closure___28_0 obj3 = new _Closure___28_0(default(_Closure___28_0));
					obj3._VB_NonLocal__VB_Closure_3 = obj2;
					obj3._VB_Local_outputWaitHandle = new AutoResetEvent(initialState: false);
					try
					{
						_Closure___28_3 arg = default;
						_Closure___28_3 CS_8__locals21 = new _Closure___28_3(arg);
						CS_8__locals21._VB_NonLocal__VB_Closure_4 = obj3;
						CS_8__locals21._VB_Local_errorWaitHandle = new AutoResetEvent(initialState: false);
						try
						{
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.OutputDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
							{
								CS_8__locals21._VB_NonLocal__VB_Closure_4._Lambda___0(RuntimeHelpers.GetObjectValue(a0), a1);
							};
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.ErrorDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
							{
								CS_8__locals21._Lambda___1(RuntimeHelpers.GetObjectValue(a0), a1);
							};
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Start();
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.BeginOutputReadLine();
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.BeginErrorReadLine();
							while (!CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.HasExited)
							{
								Thread.Sleep(10);
							}
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_Local_outputWaitHandle.WaitOne(10000);
							CS_8__locals21._VB_Local_errorWaitHandle.WaitOne(10000);
							CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_process.Dispose();
							if (CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Reverse().Take(5).Any([SpecialName] (string l) => Operators.CompareString(l, "true", TextCompare: false) == 0))
							{
								return;
							}
							Logger.Warn(CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Join("\r\n"), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							string text2 = "";
							int num = Math.Max(0, CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Count - 5);
							int num2 = CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.Count - 1;
							for (int num3 = num; num3 <= num2; num3++)
							{
								text2 = text2 + "\r\n" + CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LastResults.ElementAtOrDefault(num3);
							}
							throw new Exception($"{CS_8__locals21._VB_NonLocal__VB_Closure_4._VB_NonLocal__VB_Closure_3._VB_Local_LoaderName} 安装器出错，日志结束部分为：" + text2);
						}
						finally
						{
							if (CS_8__locals21._VB_Local_errorWaitHandle != null)
							{
								((IDisposable)CS_8__locals21._VB_Local_errorWaitHandle).Dispose();
							}
						}
					}
					finally
					{
						if (obj3._VB_Local_outputWaitHandle != null)
						{
							((IDisposable)obj3._VB_Local_outputWaitHandle).Dispose();
						}
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(installSyncLock);
					}
				}
			}
		}
		private static void ForgelikeInjectorLine(string Content, ModLoader.LoaderTask<bool, bool> Task)
		{
			switch (Content)
			{
			case "Extracting json":
				Task.Progress = 0.27;
				break;
			case "Downloading libraries":
				Task.Progress = 0.28;
				break;
			case "  File exists: Checksum validated.":
				Task.Progress += 0.003;
				break;
			case "Building Processors":
				Task.Progress = 0.38;
				break;
			case "Task: DOWNLOAD_MOJMAPS":
				Task.Progress = 0.4;
				break;
			case "Task: MERGE_MAPPING":
				Task.Progress = 0.5;
				break;
			case "Splitting: ":
				Task.Progress = 0.55;
				break;
			case "Parameter Annotations":
				Task.Progress = 0.6;
				break;
			case "Processing Complete":
				Task.Progress = 0.67;
				break;
			case "log: null":
				Task.Progress = 0.67;
				break;
			case "Sorting":
				Task.Progress = 0.8;
				break;
			case "Remapping final jar":
				Task.Progress = 0.85;
				break;
			case "Remapping jar... 50%":
				Task.Progress = 0.9;
				break;
			case "Remapping jar... 100%":
				Task.Progress = 0.95;
				break;
			case "Injecting profile":
				Task.Progress = 0.98;
				break;
			default:
				Logger.Trace(Content, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				return;
			}
			Logger.Info(Content, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		private static List<ModLoader.LoaderBase> McDownloadForgelikeLoader(bool IsNeoForge, string LoaderVersion, string NewInstanceName, string Inherit, ModDownload.DlForgelikeEntry Info, string McFolder, ModLoader.LoaderCombo<string> ClientDownloadLoader, string ClientFolder)
		{
			_Closure___30_0 arg = default;
			_Closure___30_0 CS_8__locals149 = new _Closure___30_0(arg);
			CS_8__locals149._VB_Local_IsNeoForge = IsNeoForge;
			CS_8__locals149._VB_Local_LoaderVersion = LoaderVersion;
			CS_8__locals149._VB_Local_NewInstanceName = NewInstanceName;
			CS_8__locals149._VB_Local_Inherit = Inherit;
			CS_8__locals149._VB_Local_Info = Info;
			CS_8__locals149._VB_Local_McFolder = McFolder;
			CS_8__locals149._VB_Local_ClientDownloadLoader = ClientDownloadLoader;
			CS_8__locals149._VB_Local_ClientFolder = ClientFolder;
			CS_8__locals149._VB_Local_McFolder = CS_8__locals149._VB_Local_McFolder ?? ModMinecraft.McFolderSelected;
			if (CS_8__locals149._VB_Local_IsNeoForge && CS_8__locals149._VB_Local_Info == null)
			{
				if (Operators.CompareString(CS_8__locals149._VB_Local_Inherit, "1.20.1", TextCompare: false) == 0 && !CS_8__locals149._VB_Local_LoaderVersion.StartsWithF("1.20.1-"))
				{
					CS_8__locals149._VB_Local_Info = new ModDownload.DlNeoForgeListEntry("1.20.1-" + CS_8__locals149._VB_Local_LoaderVersion);
				}
				else
				{
					CS_8__locals149._VB_Local_Info = new ModDownload.DlNeoForgeListEntry(CS_8__locals149._VB_Local_LoaderVersion);
				}
			}
			if (!CS_8__locals149._VB_Local_IsNeoForge && CS_8__locals149._VB_Local_LoaderVersion.StartsWithF("1.") && CS_8__locals149._VB_Local_LoaderVersion.Contains("-"))
			{
				CS_8__locals149._VB_Local_Inherit = CS_8__locals149._VB_Local_LoaderVersion.BeforeFirst("-");
				CS_8__locals149._VB_Local_LoaderVersion = CS_8__locals149._VB_Local_LoaderVersion.AfterLast("-");
			}
			CS_8__locals149._VB_Local_LoaderName = (CS_8__locals149._VB_Local_IsNeoForge ? "NeoForge" : "Forge");
			CS_8__locals149._VB_Local_IsCustomFolder = Operators.CompareString(CS_8__locals149._VB_Local_McFolder, ModMinecraft.McFolderSelected, TextCompare: false) != 0;
			CS_8__locals149._VB_Local_InstallerAddress = ModMain.RequestTaskTempFolder() + "forge_installer.jar";
			CS_8__locals149._VB_Local_VersionFolder = $"{CS_8__locals149._VB_Local_McFolder}versions\\{CS_8__locals149._VB_Local_NewInstanceName}\\";
			_ = $"{CS_8__locals149._VB_Local_LoaderName} {CS_8__locals149._VB_Local_Inherit} - {CS_8__locals149._VB_Local_LoaderVersion}";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			_ = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals149._VB_Local_NewInstanceName}\\";
			if (CS_8__locals149._VB_Local_Info == null)
			{
				list.Add(new ModLoader.LoaderTask<string, string>($"获取 {CS_8__locals149._VB_Local_LoaderName} 详细信息", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
				{
					ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("McDownloadForgeLoader " + CS_8__locals149._VB_Local_Inherit, ModDownload.DlForgeVersionMain);
					loaderTask.WaitForExit(CS_8__locals149._VB_Local_Inherit);
					Task.Progress = 0.8;
					foreach (ModDownload.DlForgeVersionEntry item in loaderTask.Output)
					{
						if (ModMinecraft.CompareVersion(item.Version.ToString(), CS_8__locals149._VB_Local_LoaderVersion) == 0)
						{
							CS_8__locals149._VB_Local_Info = item;
							return;
						}
					}
					throw new Exception($"未能找到 {CS_8__locals149._VB_Local_LoaderName} " + CS_8__locals149._VB_Local_Inherit + "-" + CS_8__locals149._VB_Local_LoaderVersion + " 的详细信息！");
				})
				{
					ProgressWeight = 3.0
				});
			}
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>($"准备下载 {CS_8__locals149._VB_Local_LoaderName}", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
				if (CS_8__locals149._VB_Local_Info.IsNeoForge)
				{
					string text = ((ModDownload.DlNeoForgeListEntry)CS_8__locals149._VB_Local_Info).UrlBase + "-installer.jar";
					list2.Add(new ModNet.NetFile(new string[2]
					{
						text.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
						text
					}, CS_8__locals149._VB_Local_InstallerAddress, new FileChecker
					{
						MinSize = 65536L
					}));
				}
				else
				{
					ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)CS_8__locals149._VB_Local_Info;
					string arg3 = string.Format("{0}-{1}/forge-{2}-{3}-{4}.{5}", dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Category, dlForgeVersionEntry.FileExtension);
					list2.Add(new ModNet.NetFile(new string[2]
					{
						$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{arg3}",
						$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{arg3}"
					}, CS_8__locals149._VB_Local_InstallerAddress, new FileChecker
					{
						MinSize = 65536L,
						Hash = dlForgeVersionEntry.Hash
					}));
				}
				Task.Output = list2;
			})
			{
				ProgressWeight = 0.5,
				Show = false
			});
			list.Add(new ModNet.LoaderDownload($"下载 {CS_8__locals149._VB_Local_LoaderName} 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 9.0
			});
			if (CS_8__locals149._VB_Local_IsNeoForge || Conversions.ToDouble(CS_8__locals149._VB_Local_LoaderVersion.BeforeFirst(".")) >= 20.0)
			{
				_Closure___30_1 arg2 = default;
				_Closure___30_1 CS_8__locals129 = new _Closure___30_1(arg2);
				CS_8__locals129._VB_NonLocal__VB_Closure_2 = CS_8__locals149;
				Logger.Info(string.Format("检测为{0}Forge：{1}", CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsNeoForge ? " Neo" : "新版 ", CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderVersion), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				CS_8__locals129._VB_Local_Libs = null;
				list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>($"分析 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库文件", checked([SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					Task.Output = new List<ModNet.NetFile>();
					ZipArchive zipArchive = null;
					try
					{
						zipArchive = FileUtils.OpenZip(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress);
						Task.Progress = 0.2;
						JObject jObject = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
							.DeserializeJson();
						JObject content = (JObject)zipArchive.GetEntry("version.json").Open().ReadString()
							.DeserializeJson();
						jObject.Merge(content);
						CS_8__locals129._VB_Local_Libs = ModMinecraft.McLibListGetWithJson(jObject, KeepSameNameDifferentVersionResult: true);
						if (jObject["data"] != null && jObject["data"]["MOJMAPS"] != null)
						{
							Task.Progress = 0.4;
							JObject jObject2 = (JObject)ModNet.NetRequestByLoader(ModDownload.DlSourceLauncherOrMetaGet(Conversions.ToString(ModDownload.DlClientListGet(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit))), 45000, IsJson: true).DeserializeJson();
							string text = jObject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).BeforeFirst("@");
							string localPath = ModMinecraft.McLibGet(text).Replace(".jar", "." + jObject["data"]["MOJMAPS"]["client"].ToString().Trim("[]".ToCharArray()).Split("@")[1]);
							JToken jToken = jObject2["downloads"]["client_mappings"];
							CS_8__locals129._VB_Local_Libs.Add(new ModMinecraft.McLibEntry
							{
								IsNatives = false,
								LocalPath = localPath,
								OriginalName = text,
								Url = (string?)jToken["url"],
								Size = (long)jToken["size"],
								SHA1 = (string?)jToken["sha1"]
							});
							Logger.Info(string.Format("需要下载 Mappings：{0} (SHA1: {1})", jToken["url"], jToken["sha1"]), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
						Task.Progress = 0.8;
						int num = CS_8__locals129._VB_Local_Libs.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							if (CS_8__locals129._VB_Local_Libs[i].LocalPath.EndsWithF($"{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName.Lower()}-{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit}-{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderVersion}.jar") || CS_8__locals129._VB_Local_Libs[i].LocalPath.EndsWithF($"{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName.Lower()}-{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit}-{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderVersion}-client.jar"))
							{
								Logger.Warn($"已从待下载 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库中移除：{CS_8__locals129._VB_Local_Libs[i].LocalPath}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								CS_8__locals129._VB_Local_Libs.RemoveAt(i);
								break;
							}
						}
						Task.Output = ModMinecraft.McLibNetFilesFromTokens(CS_8__locals129._VB_Local_Libs, ModMinecraft.McFolderSelected);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception(string.Format("获取{0}Forge 支持库列表失败", CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsNeoForge ? " Neo" : "新版 "), innerException);
					}
					finally
					{
						zipArchive?.Dispose();
					}
				}))
				{
					ProgressWeight = 1.0
				});
				list.Add(new ModNet.LoaderDownload($"下载 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 12.0
				});
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>($"获取 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库文件", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
				{
					if (CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsCustomFolder)
					{
						foreach (ModMinecraft.McLibEntry item2 in CS_8__locals129._VB_Local_Libs)
						{
							string text = item2.LocalPath.Replace(ModMinecraft.McFolderSelected, CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder);
							if (!FileUtils.Exists(text))
							{
								FileUtils.Copy(item2.LocalPath, text);
							}
							Logger.Trace($"复制的 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 支持库文件：{item2.LocalPath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
					}
					List<ModLoader.LoaderBase> source = (from l in CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_ClientDownloadLoader.GetLoaderList()
						where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
						where l.State != ModBase.LoadState.Finished
						select l).ToList();
					if (source.Any())
					{
						Logger.Info($"{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 安装正在等待原版文件下载完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					while (source.Any() && !Task.IsCanceled)
					{
						source = source.Where((_Closure___._I30_6 == null) ? (_Closure___._I30_6 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure___._I30_6).ToList();
						Thread.Sleep(50);
					}
					if (!Task.IsCanceled && CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsCustomFolder)
					{
						object vanillaSyncLock = VanillaSyncLock;
						ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
						bool lockTaken = false;
						try
						{
							Monitor.Enter(vanillaSyncLock, ref lockTaken);
							string lastPart = PathUtils.GetLastPart(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder);
							DirectoryUtils.Create(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit);
							if (!FileUtils.Exists(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".json"))
							{
								FileUtils.Copy(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder + lastPart + ".json", CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".json");
							}
							if (!FileUtils.Exists(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".jar"))
							{
								FileUtils.Copy(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_ClientFolder + lastPart + ".jar", CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + "\\" + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_Inherit + ".jar");
							}
						}
						finally
						{
							if (lockTaken)
							{
								Monitor.Exit(vanillaSyncLock);
							}
						}
					}
				})
				{
					ProgressWeight = 0.1,
					Show = false
				});
				list.Add(new ModLoader.LoaderTask<bool, bool>(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsNeoForge ? "安装 NeoForge" : "安装 Forge（方式 A）", [SpecialName] (ModLoader.LoaderTask<bool, bool> Task) =>
				{
					ZipArchive zipArchive = null;
					try
					{
						_Closure___30_2 arg3 = default;
						_Closure___30_2 CS_8__locals150 = new _Closure___30_2(arg3);
						Logger.Info($"开始进行 Forgelike 安装：{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						CS_8__locals150._VB_Local_OldList = DirectoryUtils.EnumerateDirectories(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\").ToList();
						zipArchive = FileUtils.OpenZip(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress);
						_ = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
							.DeserializeJson();
						DirectoryUtils.Create(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_VersionFolder);
						Task.Progress = 0.04;
						ModMinecraft.McFolderLauncherProfilesJsonCreate(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder);
						Task.Progress = 0.05;
						bool flag = true;
						while (true)
						{
							try
							{
								ModBase.ExtractResources(ModBase.PathTemp + "Cache\\forge_installer.jar", "ForgeInstaller");
								Task.Progress = 0.06;
								ForgelikeInjector(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress, Task, CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder, flag, CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_IsNeoForge);
								Task.Progress = 0.97;
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								if (!flag)
								{
									throw new Exception($"运行 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 安装器失败", ex2);
								}
								Logger.Warn(ex2, $"使用 JavaWrapper 安装 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 失败，将不使用 JavaWrapper 并重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
								flag = false;
								ProjectData.ClearProjectError();
								continue;
							}
							break;
						}
						List<DirectoryInfo> list2 = (from f in DirectoryUtils.EnumerateDirectories(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_McFolder + "versions\\").SkipWhile([SpecialName] (string i) => CS_8__locals150._VB_Local_OldList.Contains(i))
							select DirectoryUtils.GetInfo(f)).ToList();
						if (list2.Count > 1)
						{
							list2 = list2.Where([SpecialName] (DirectoryInfo l) => l.Name.ContainsIgnoreCase("forge") && l.EnumerateFiles().Any()).ToList();
						}
						if (list2.Count == 1)
						{
							FileInfo fileInfo = list2[0].EnumerateFiles().First();
							FileUtils.Copy(fileInfo.FullName, CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_VersionFolder + CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_NewInstanceName + ".json");
							Logger.Info($"已复制新增的版本 JSON 文件：{fileInfo.FullName} -> {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_VersionFolder}{CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_NewInstanceName}.json", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
						else if (list2.Count > 1)
						{
							Logger.Info(string.Format("有多个疑似的新增版本，无法确定：{0}", list2.Select([SpecialName] (DirectoryInfo d) => d.Name).Join(";")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
						else
						{
							Logger.Info("未找到新增的版本文件夹", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception innerException = ex3;
						throw new Exception($"安装新 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 版本失败", innerException);
					}
					finally
					{
						try
						{
							zipArchive?.Dispose();
							FileUtils.Delete(CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_InstallerAddress);
						}
						catch (Exception ex4)
						{
							ProjectData.SetProjectError(ex4);
							Exception ex5 = ex4;
							Logger.Warn(ex5, $"安装 {CS_8__locals129._VB_NonLocal__VB_Closure_2._VB_Local_LoaderName} 清理文件时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							ProjectData.ClearProjectError();
						}
					}
				})
				{
					ProgressWeight = 10.0
				});
			}
			else
			{
				Logger.Info($"检测为非新版 Forge：{CS_8__locals149._VB_Local_LoaderVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>($"安装 {CS_8__locals149._VB_Local_LoaderName}（方式 B）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
				{
					ZipArchive zipArchive = null;
					try
					{
						zipArchive = FileUtils.OpenZip(CS_8__locals149._VB_Local_InstallerAddress);
						Task.Progress = 0.2;
						JObject jObject = (JObject)zipArchive.GetEntry("install_profile.json").Open().ReadString()
							.DeserializeJson();
						Task.Progress = 0.4;
						DirectoryUtils.Create(CS_8__locals149._VB_Local_VersionFolder);
						Task.Progress = 0.5;
						if (jObject["install"] == null)
						{
							Logger.Info($"开始进行 Forge 安装，Legacy 方式 1：{CS_8__locals149._VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							JObject jObject2 = (JObject)zipArchive.GetEntry(jObject["json"].ToString().TrimStart('/')).Open().ReadString()
								.DeserializeJson();
							jObject2["id"] = CS_8__locals149._VB_Local_NewInstanceName;
							FileUtils.Write(CS_8__locals149._VB_Local_VersionFolder + CS_8__locals149._VB_Local_NewInstanceName + ".json", jObject2.ToString());
							Task.Progress = 0.6;
							zipArchive.Dispose();
							FileUtils.ExtractToDirectory(CS_8__locals149._VB_Local_InstallerAddress, CS_8__locals149._VB_Local_InstallerAddress + "_unrar\\");
							if (DirectoryUtils.Exists(CS_8__locals149._VB_Local_InstallerAddress + "_unrar\\maven\\"))
							{
								DirectoryUtils.Copy(CS_8__locals149._VB_Local_InstallerAddress + "_unrar\\maven\\", CS_8__locals149._VB_Local_McFolder + "libraries\\");
							}
							DirectoryUtils.Delete(CS_8__locals149._VB_Local_InstallerAddress + "_unrar\\");
						}
						else
						{
							Logger.Info($"开始进行 Forge 安装，Legacy 方式 2：{CS_8__locals149._VB_Local_InstallerAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							FileUtils.Write(ModMinecraft.McLibGet((string?)jObject["install"]["path"], WithHead: true, IgnoreLiteLoader: false, CS_8__locals149._VB_Local_McFolder), zipArchive.GetEntry((string?)jObject["install"]["filePath"]).Open());
							Task.Progress = 0.9;
							jObject["versionInfo"]["id"] = CS_8__locals149._VB_Local_NewInstanceName;
							if (jObject["versionInfo"]["inheritsFrom"] == null)
							{
								((JObject)jObject["versionInfo"]).Add("inheritsFrom", CS_8__locals149._VB_Local_Inherit);
							}
							FileUtils.Write(CS_8__locals149._VB_Local_VersionFolder + CS_8__locals149._VB_Local_NewInstanceName + ".json", jObject["versionInfo"].ToString());
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception("非新版方式安装 Forge 失败", innerException);
					}
					finally
					{
						try
						{
							zipArchive?.Dispose();
							FileUtils.Delete(CS_8__locals149._VB_Local_InstallerAddress);
							DirectoryUtils.Delete(CS_8__locals149._VB_Local_InstallerAddress + "_unrar\\");
						}
						catch (Exception ex2)
						{
							ProjectData.SetProjectError(ex2);
							Exception ex3 = ex2;
							Logger.Warn(ex3, "非新版方式安装 Forge 清理文件时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							ProjectData.ClearProjectError();
						}
					}
				})
				{
					ProgressWeight = 1.0
				});
			}
			return list;
		}
		public static void ForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlForgeVersionEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			if (Entries.IsSingle())
			{
				return;
			}
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry = null;
			if (Entries.Any())
			{
				dlForgeVersionEntry = Entries[0];
			}
			else
			{
				Logger.Info("未找到可用的 Forge 版本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry2 = null;
			foreach (ModDownload.DlForgeVersionEntry Entry in Entries)
			{
				if (Entry.IsRecommended)
				{
					dlForgeVersionEntry2 = Entry;
				}
			}
			if (dlForgeVersionEntry != null && dlForgeVersionEntry == dlForgeVersionEntry2)
			{
				dlForgeVersionEntry = null;
			}
			if (dlForgeVersionEntry2 != null)
			{
				MyListItem myListItem = ForgeDownloadListItem(dlForgeVersionEntry2, OnClick, IsSaveOnly).Init();
				myListItem.Info = "推荐版" + ((Operators.CompareString(myListItem.Info, "", TextCompare: false) == 0) ? "" : ("，" + myListItem.Info));
				Stack.Children.Add(myListItem);
			}
			if (dlForgeVersionEntry != null)
			{
				MyListItem myListItem2 = ForgeDownloadListItem(dlForgeVersionEntry, OnClick, IsSaveOnly).Init();
				myListItem2.Info = "最新版" + ((Operators.CompareString(myListItem2.Info, "", TextCompare: false) == 0) ? "" : ("，" + myListItem2.Info));
				Stack.Children.Add(myListItem2);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
			});
		}
		public static MyVirtualizingElement<MyListItem> ForgeDownloadListItem(ModDownload.DlForgeVersionEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Title = Entry.VersionName,
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Entry,
					Info = new string[2]
					{
						(Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("发布于 " + Entry.ReleaseTime),
						ModBase.ModeDebug ? ("种类：" + Entry.Category) : ""
					}.Where([SpecialName] (string d) => Operators.CompareString(d, "", TextCompare: false) != 0).Join("，"),
					Logo = ModBase.PathImage + "Blocks/Anvil.png"
				};
				myListItem.Click += OnClick;
				if (IsSaveOnly)
				{
					myListItem.ContentHandler = ForgeSaveContMenuBuild;
				}
				else
				{
					myListItem.ContentHandler = ForgeContMenuBuild;
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		private static void ForgeContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				ForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
		}
		private static void ForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[1] { myIconButton };
		}
		private static void ForgeLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlForgeVersionEntry dlForgeVersionEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlForgeVersionEntry = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			ModBase.OpenWebsite($"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.VersionName}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.VersionName}-changelog.txt");
		}
		public static void ForgeSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlForgeVersionEntry info;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				info = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			McDownloadForgelikeSave(info);
		}
		public static void McDownloadForgeRecommendedRefresh()
		{
			if (IsForgeRecommendedRefreshed)
			{
				return;
			}
			IsForgeRecommendedRefreshed = true;
			ModBase.RunInNewThread([SpecialName] () =>
			{
				try
				{
					Logger.Info("刷新 Forge 推荐版本缓存开始", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/promos", null, null, null, null, "*/*", null, RequireJson: true);
					if (text.Length < 1000)
					{
						throw new Exception("获取的结果过短（" + text + "）");
					}
					JContainer jContainer = (JContainer)text.DeserializeJson();
					List<string> list = new List<string>();
					foreach (JObject item in (IEnumerable<JToken>)jContainer)
					{
						if (item["name"] != null && item["build"] != null)
						{
							string text2 = (string?)item["name"];
							if (text2.EndsWithF("-recommended"))
							{
								list.Add("\"" + text2.Replace("-recommended", "\":\"" + item["build"]["version"].ToString() + "\""));
							}
						}
					}
					if (list.Count < 5)
					{
						throw new Exception("获取的推荐版本数过少（" + text + "）");
					}
					string text3 = "{" + list.Join(",") + "}";
					FileUtils.Write(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json", text3);
					Logger.Info("刷新 Forge 推荐版本缓存成功", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "刷新 Forge 推荐版本缓存失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
			}, "ForgeRecommendedRefresh");
		}
		public static string McDownloadForgeRecommendedGet(string McVersion)
		{
			string result;
			try
			{
				if (McVersion == null)
				{
					result = null;
				}
				else
				{
					string text = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json");
					if (string.IsNullOrEmpty(text))
					{
						Logger.Info("没有 Forge 推荐版本缓存文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						result = null;
					}
					else
					{
						JObject jObject = (JObject)text.DeserializeJson();
						result = ((jObject != null && (McVersion ?? "").Contains(".") && jObject.ContainsKey(McVersion)) ? (jObject[McVersion] ?? ((JToken)"")).ToString() : null);
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, string.Format("获取 Forge 推荐版本失败（{0}）", McVersion ?? "null"), LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				result = null;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static void NeoForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlNeoForgeListEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			if (Entries.IsSingle())
			{
				return;
			}
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry = null;
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry2 = null;
			if (Entries.Any())
			{
				foreach (ModDownload.DlNeoForgeListEntry item in Entries.ToList())
				{
					if (item.IsBeta)
					{
						if (dlNeoForgeListEntry2 == null)
						{
							dlNeoForgeListEntry2 = item;
						}
						continue;
					}
					dlNeoForgeListEntry = item;
					break;
				}
			}
			else
			{
				Logger.Warn("未找到可用的 NeoForge 版本", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			if (dlNeoForgeListEntry != null)
			{
				MyListItem myListItem = NeoForgeDownloadListItem(dlNeoForgeListEntry, OnClick, IsSaveOnly).Init();
				myListItem.Info = ((Operators.CompareString(myListItem.Info, "", TextCompare: false) == 0) ? "最新稳定版" : ("最新" + myListItem.Info));
				Stack.Children.Add(myListItem);
			}
			if (dlNeoForgeListEntry2 != null)
			{
				MyListItem myListItem2 = NeoForgeDownloadListItem(dlNeoForgeListEntry2, OnClick, IsSaveOnly).Init();
				myListItem2.Info = ((Operators.CompareString(myListItem2.Info, "", TextCompare: false) == 0) ? "最新测试版" : ("最新" + myListItem2.Info));
				Stack.Children.Add(myListItem2);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
			});
		}
		public static MyVirtualizingElement<MyListItem> NeoForgeDownloadListItem(ModDownload.DlNeoForgeListEntry Info, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Title = Info.VersionName,
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Info,
					Info = (Info.IsBeta ? "测试版" : "稳定版"),
					Logo = ModBase.PathImage + "Blocks/NeoForge.png"
				};
				myListItem.Click += OnClick;
				if (IsSaveOnly)
				{
					myListItem.ContentHandler = NeoForgeSaveContMenuBuild;
				}
				else
				{
					myListItem.ContentHandler = NeoForgeContMenuBuild;
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		private static void NeoForgeContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
				ToolTip = "另存为"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				NeoForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton myIconButton2 = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
		}
		private static void NeoForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
		{
			MyIconButton myIconButton = new MyIconButton
			{
				LogoScale = 1.05,
				Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
				ToolTip = "更新日志"
			};
			myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			sender.Buttons = new MyIconButton[1] { myIconButton };
		}
		private static void NeoForgeLog_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				dlNeoForgeListEntry = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				dlNeoForgeListEntry = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			ModBase.OpenWebsite(dlNeoForgeListEntry.UrlBase + "-changelog.txt");
		}
		public static void NeoForgeSave_Click(object sender, RoutedEventArgs e)
		{
			ModDownload.DlNeoForgeListEntry info;
			if (NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null)
			{
				info = (ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null);
			}
			else
			{
				info = ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)));
			}
			McDownloadForgelikeSave(info);
		}
		public static void McDownloadFabricLoaderSave(JObject DownloadInfo)
		{
			try
			{
				string text = DownloadInfo["url"].ToString();
				string lastPart = PathUtils.GetLastPart(text);
				string lastPart2 = PathUtils.GetLastPart(DownloadInfo["version"].ToString());
				string text2 = Dialogs.SaveFile("选择保存位置", lastPart, null, new (string, string)[1] { ("jar", "Fabric 安装器") });
				if (text2 == null)
				{
					return;
				}
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
				{
					if (Operators.CompareString(item.Name, $"Fabric {lastPart2} 安装器下载", TextCompare: false) == 0)
					{
						ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
						return;
					}
				}
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
				List<string> list2 = new List<string>();
				list2.Add(text);
				list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list2.ToArray(), text2, new FileChecker
					{
						MinSize = 65536L
					})
				})
				{
					ProgressWeight = 15.0
				});
				ModLoader.LoaderCombo<JObject> loaderCombo = new ModLoader.LoaderCombo<JObject>("Fabric " + lastPart2 + " 安装器下载", list);
				loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
				loaderCombo.Start(DownloadInfo);
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始 Fabric 安装器下载失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static List<ModLoader.LoaderBase> McDownloadFabricLoader(string FabricVersion, string MinecraftName, string McFolder)
		{
			Operators.CompareString(McFolder, ModMinecraft.McFolderSelected, TextCompare: false);
			string text = "fabric-loader-" + FabricVersion + "-" + MinecraftName;
			string text2 = McFolder + "versions\\" + text + "\\";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			MinecraftName = MinecraftName.Replace("∞", "infinite");
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 Fabric 主文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(new string[2]
					{
						"https://bmclapi2.bangbang93.com/fabric-meta/v2/versions/loader/" + MinecraftName + "/" + FabricVersion + "/profile/json",
						"https://meta.fabricmc.net/v2/versions/loader/" + MinecraftName + "/" + FabricVersion + "/profile/json"
					}, text2 + text + ".json", new FileChecker
					{
						IsJson = true
					})
				};
			})
			{
				ProgressWeight = 0.5
			});
			list.Add(new ModNet.LoaderDownload("下载 Fabric 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 2.5
			});
			return list;
		}
		public static MyVirtualizingElement<MyListItem> FabricDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem();
				myListItem.Title = Entry["version"].ToString().Replace("+build", "");
				myListItem.SnapsToDevicePixels = true;
				myListItem.Height = 42.0;
				myListItem.Type = MyListItem.CheckType.Clickable;
				myListItem.Tag = Entry;
				myListItem.Info = (Entry["stable"].ToObject<bool>() ? "稳定版" : "测试版");
				myListItem.Logo = ModBase.PathImage + "Blocks/Fabric.png";
				myListItem.Click += OnClick;
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		public static MyVirtualizingElement<MyListItem> FabricApiDownloadListItem(ResourceVersion Entry, MyListItem.ClickEventHandler OnClick)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem();
				myListItem.Title = Entry.Display.Split("]")[1].Replace("Fabric API ", "").Replace(" build ", ".").BeforeFirst("+")
					.Trim();
				myListItem.SnapsToDevicePixels = true;
				myListItem.Height = 42.0;
				myListItem.Type = MyListItem.CheckType.Clickable;
				myListItem.Tag = Entry;
				myListItem.Info = Entry.ReleaseTypeDisplay + "，发布于 " + Entry.ReleaseDate.ToString("yyyy'/'MM'/'dd HH':'mm");
				myListItem.Logo = ModBase.PathImage + "Blocks/Fabric.png";
				myListItem.Click += OnClick;
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		public static MyVirtualizingElement<MyListItem> OptiFabricDownloadListItem(ResourceVersion Entry, MyListItem.ClickEventHandler OnClick)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Title = Entry.Display.Lower().Replace("optifabric-", "").Replace(".jar", "")
						.Trim()
						.TrimStart('v'),
					SnapsToDevicePixels = true,
					Height = 42.0,
					Type = MyListItem.CheckType.Clickable,
					Tag = Entry,
					Info = Entry.ReleaseTypeDisplay + "，发布于 " + Entry.ReleaseDate.ToString("yyyy'/'MM'/'dd HH':'mm"),
					Logo = ModBase.PathImage + "Blocks/OptiFabric.png"
				};
				myListItem.Click += OnClick;
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		public static void LoaderStateChangedHintOnly(ModLoader.LoaderBase Loader)
		{
			switch (Loader.State)
			{
			case ModBase.LoadState.Finished:
				ModMain.Hint(Loader.Name + "成功！", ModMain.HintType.Green);
				break;
			case ModBase.LoadState.Failed:
				ModMain.Hint(Loader.Name + "失败：" + Loader.Error.GetDisplay(multiline: false), ModMain.HintType.Red);
				break;
			case ModBase.LoadState.Canceled:
				ModMain.Hint(Loader.Name + "已取消！");
				break;
			}
		}
		public static void McInstallState(ModLoader.LoaderBase Loader)
		{
			switch (Loader.State)
			{
			case ModBase.LoadState.Finished:
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				ModMain.Hint(Loader.Name + "成功！", ModMain.HintType.Green);
				break;
			case ModBase.LoadState.Failed:
				ModMain.MyMsgBox(Loader.Error.GetDisplay(multiline: true), Loader.Name + "失败", "确定", "", "", IsWarn: true);
				break;
			case ModBase.LoadState.Canceled:
				ModMain.Hint(Loader.Name + "已取消！");
				break;
			case ModBase.LoadState.Loading:
				return;
			}
			McInstallFailedClearFolder(Loader);
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		public static void McInstallFailedClearFolder(object Loader)
		{
			try
			{
				Thread.Sleep(1000);
				if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Failed, TextCompare: false) || Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Canceled, TextCompare: false))
				{
					if (DirectoryUtils.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "saves\\"))) || DirectoryUtils.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "versions\\"))))
					{
						Logger.Warn(string.Format("由于版本已被独立启动，不清理版本文件夹：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						return;
					}
					Logger.Warn(string.Format("由于下载失败或取消，清理版本文件夹：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					DirectoryUtils.Delete(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null)));
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "下载失败或取消后清理版本文件夹失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static bool McInstall(McInstallRequest Request)
		{
			bool result;
			try
			{
				List<ModLoader.LoaderBase> list = McInstallLoader(Request);
				if (list == null)
				{
					result = false;
				}
				else
				{
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(Request.NewInstanceName + " 安装", list);
					loaderCombo.OnStateChanged = McInstallState;
					loaderCombo.Start(Request.VersionFolder);
					ModLoader.LoaderTaskbarAdd(loaderCombo);
					ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
					ModMain.FrmMain.BtnExtraDownload.Ribble();
					result = true;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "开始合并安装失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static List<ModLoader.LoaderBase> McInstallLoader(McInstallRequest Request)
		{
			_Closure___56_0 CS_8__locals180 = new _Closure___56_0();
			CS_8__locals180._VB_Local_Request = Request;
			CS_8__locals180._VB_Local_TempMcFolder = ModMain.RequestTaskTempFolder(CS_8__locals180._VB_Local_Request.OptiFineEntry != null || CS_8__locals180._VB_Local_Request.ForgeEntry != null || CS_8__locals180._VB_Local_Request.NeoForgeEntry != null);
			CS_8__locals180._VB_Local_VersionFolder = ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals180._VB_Local_Request.NewInstanceName + "\\";
			if (DirectoryUtils.Exists(CS_8__locals180._VB_Local_TempMcFolder))
			{
				DirectoryUtils.Delete(CS_8__locals180._VB_Local_TempMcFolder);
			}
			CS_8__locals180._VB_Local_OptiFineFolder = null;
			if (CS_8__locals180._VB_Local_Request.OptiFineVersion != null)
			{
				if (CS_8__locals180._VB_Local_Request.OptiFineVersion.Contains("_HD_U_"))
				{
					CS_8__locals180._VB_Local_Request.OptiFineVersion = "HD_U_" + CS_8__locals180._VB_Local_Request.OptiFineVersion.AfterLast("_HD_U_");
				}
				CS_8__locals180._VB_Local_Request.OptiFineEntry = new ModDownload.DlOptiFineListEntry
				{
					DisplayName = CS_8__locals180._VB_Local_Request.MinecraftName + " " + CS_8__locals180._VB_Local_Request.OptiFineVersion.Replace("HD_U_", "").Replace("_", "").Replace("pre", " pre"),
					Inherit = CS_8__locals180._VB_Local_Request.MinecraftName,
					IsPreview = CS_8__locals180._VB_Local_Request.OptiFineVersion.ContainsIgnoreCase("pre"),
					InstanceName = CS_8__locals180._VB_Local_Request.MinecraftName + "-OptiFine_" + CS_8__locals180._VB_Local_Request.OptiFineVersion,
					FileName = (CS_8__locals180._VB_Local_Request.OptiFineVersion.ContainsIgnoreCase("pre") ? "preview_" : "") + "OptiFine_" + CS_8__locals180._VB_Local_Request.MinecraftName + "_" + CS_8__locals180._VB_Local_Request.OptiFineVersion + ".jar"
				};
			}
			if (CS_8__locals180._VB_Local_Request.OptiFineEntry != null)
			{
				CS_8__locals180._VB_Local_OptiFineFolder = CS_8__locals180._VB_Local_TempMcFolder + "versions\\" + CS_8__locals180._VB_Local_Request.OptiFineEntry.InstanceName;
			}
			CS_8__locals180._VB_Local_ForgeFolder = null;
			if (CS_8__locals180._VB_Local_Request.ForgeEntry != null)
			{
				CS_8__locals180._VB_Local_Request.ForgeVersion = CS_8__locals180._VB_Local_Request.ForgeVersion ?? CS_8__locals180._VB_Local_Request.ForgeEntry.VersionName;
			}
			if (CS_8__locals180._VB_Local_Request.ForgeVersion != null)
			{
				CS_8__locals180._VB_Local_ForgeFolder = CS_8__locals180._VB_Local_TempMcFolder + "versions\\forge-" + CS_8__locals180._VB_Local_Request.ForgeVersion;
			}
			CS_8__locals180._VB_Local_NeoForgeFolder = null;
			if (CS_8__locals180._VB_Local_Request.NeoForgeEntry != null)
			{
				CS_8__locals180._VB_Local_Request.NeoForgeVersion = CS_8__locals180._VB_Local_Request.NeoForgeVersion ?? CS_8__locals180._VB_Local_Request.NeoForgeEntry.VersionName;
			}
			if (CS_8__locals180._VB_Local_Request.NeoForgeVersion != null)
			{
				CS_8__locals180._VB_Local_NeoForgeFolder = CS_8__locals180._VB_Local_TempMcFolder + "versions\\neoforge-" + CS_8__locals180._VB_Local_Request.NeoForgeVersion;
			}
			CS_8__locals180._VB_Local_FabricFolder = null;
			if (CS_8__locals180._VB_Local_Request.FabricVersion != null)
			{
				CS_8__locals180._VB_Local_FabricFolder = CS_8__locals180._VB_Local_TempMcFolder + "versions\\fabric-loader-" + CS_8__locals180._VB_Local_Request.FabricVersion + "-" + CS_8__locals180._VB_Local_Request.MinecraftName;
			}
			CS_8__locals180._VB_Local_LiteLoaderFolder = null;
			if (CS_8__locals180._VB_Local_Request.LiteLoaderEntry != null)
			{
				CS_8__locals180._VB_Local_LiteLoaderFolder = CS_8__locals180._VB_Local_TempMcFolder + "versions\\" + CS_8__locals180._VB_Local_Request.MinecraftName + "-LiteLoader";
			}
			CS_8__locals180._VB_Local_Modable = CS_8__locals180._VB_Local_Request.FabricVersion != null || CS_8__locals180._VB_Local_Request.ForgeEntry != null || CS_8__locals180._VB_Local_Request.NeoForgeEntry != null || CS_8__locals180._VB_Local_Request.LiteLoaderEntry != null;
			CS_8__locals180._VB_Local_ModsTempFolder = CS_8__locals180._VB_Local_TempMcFolder + "mods\\";
			CS_8__locals180._VB_Local_OptiFineAsMod = CS_8__locals180._VB_Local_Request.OptiFineEntry != null && CS_8__locals180._VB_Local_Modable;
			if (CS_8__locals180._VB_Local_OptiFineAsMod)
			{
				Logger.Info("OptiFine 将作为 Mod 进行下载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				if (CS_8__locals180._VB_Local_Request.LiteLoaderEntry != null)
				{
					CS_8__locals180._VB_Local_OptiFineFolder = CS_8__locals180._VB_Local_ModsTempFolder + CS_8__locals180._VB_Local_Request.MinecraftName + "\\";
				}
				else
				{
					CS_8__locals180._VB_Local_OptiFineFolder = CS_8__locals180._VB_Local_ModsTempFolder;
				}
			}
			if (CS_8__locals180._VB_Local_OptiFineFolder != null)
			{
				Logger.Info($"OptiFine 缓存：{CS_8__locals180._VB_Local_OptiFineFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			if (CS_8__locals180._VB_Local_ForgeFolder != null)
			{
				Logger.Info($"Forge 缓存：{CS_8__locals180._VB_Local_ForgeFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			if (CS_8__locals180._VB_Local_NeoForgeFolder != null)
			{
				Logger.Info($"NeoForge 缓存：{CS_8__locals180._VB_Local_NeoForgeFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			if (CS_8__locals180._VB_Local_FabricFolder != null)
			{
				Logger.Info($"Fabric 缓存：{CS_8__locals180._VB_Local_FabricFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			if (CS_8__locals180._VB_Local_LiteLoaderFolder != null)
			{
				Logger.Info($"LiteLoader 缓存：{CS_8__locals180._VB_Local_LiteLoaderFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			Logger.Info($"对应的原版版本：{CS_8__locals180._VB_Local_Request.MinecraftName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			if (FileUtils.Exists($"{CS_8__locals180._VB_Local_VersionFolder}{CS_8__locals180._VB_Local_Request.NewInstanceName}.json"))
			{
				ModMain.Hint("版本 " + CS_8__locals180._VB_Local_Request.NewInstanceName + " 已经存在！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<int, int>("添加忽略标识", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
			{
				CS_8__locals180._Lambda___0();
			})
			{
				Show = false,
				Block = false
			});
			if (CS_8__locals180._VB_Local_Request.FabricApi != null)
			{
				list.Add(new ModNet.LoaderDownload("下载 Fabric API", new List<ModNet.NetFile> { CS_8__locals180._VB_Local_Request.FabricApi.ToNetFile(CS_8__locals180._VB_Local_ModsTempFolder, ResourceVersion.DownloadReason.Dependency, CS_8__locals180._VB_Local_Request.MinecraftName, ModLoaders.Fabric) })
				{
					ProgressWeight = 3.0,
					Block = false
				});
			}
			if (CS_8__locals180._VB_Local_Request.OptiFabric != null)
			{
				list.Add(new ModNet.LoaderDownload("下载 OptiFabric", new List<ModNet.NetFile> { CS_8__locals180._VB_Local_Request.OptiFabric.ToNetFile(CS_8__locals180._VB_Local_ModsTempFolder, ResourceVersion.DownloadReason.Dependency, CS_8__locals180._VB_Local_Request.MinecraftName, ModLoaders.Fabric) })
				{
					ProgressWeight = 3.0,
					Block = false
				});
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("下载原版 " + CS_8__locals180._VB_Local_Request.MinecraftName, McDownloadClientLoader(CS_8__locals180._VB_Local_Request.MinecraftName, CS_8__locals180._VB_Local_Request.MinecraftJson, CS_8__locals180._VB_Local_Request.NewInstanceName))
			{
				Show = false,
				ProgressWeight = 39.0,
				Block = (CS_8__locals180._VB_Local_Request.ForgeVersion == null && CS_8__locals180._VB_Local_Request.NeoForgeVersion == null && CS_8__locals180._VB_Local_Request.OptiFineEntry == null && CS_8__locals180._VB_Local_Request.FabricVersion == null && CS_8__locals180._VB_Local_Request.LiteLoaderEntry == null)
			};
			list.Add(loaderCombo);
			if (CS_8__locals180._VB_Local_Request.OptiFineEntry != null)
			{
				if (CS_8__locals180._VB_Local_OptiFineAsMod)
				{
					list.Add(new ModLoader.LoaderCombo<string>("下载 OptiFine " + CS_8__locals180._VB_Local_Request.OptiFineEntry.DisplayName, McDownloadOptiFineSaveLoader(CS_8__locals180._VB_Local_Request.OptiFineEntry, CS_8__locals180._VB_Local_OptiFineFolder + CS_8__locals180._VB_Local_Request.OptiFineEntry.FileName))
					{
						Show = false,
						ProgressWeight = 16.0,
						Block = (CS_8__locals180._VB_Local_Request.ForgeVersion == null && CS_8__locals180._VB_Local_Request.NeoForgeVersion == null && CS_8__locals180._VB_Local_Request.FabricVersion == null && CS_8__locals180._VB_Local_Request.LiteLoaderEntry == null)
					});
				}
				else
				{
					list.Add(new ModLoader.LoaderCombo<string>("下载 OptiFine " + CS_8__locals180._VB_Local_Request.OptiFineEntry.DisplayName, McDownloadOptiFineLoader(CS_8__locals180._VB_Local_Request.OptiFineEntry, CS_8__locals180._VB_Local_TempMcFolder, loaderCombo, CS_8__locals180._VB_Local_Request.VersionFolder))
					{
						Show = false,
						ProgressWeight = 24.0,
						Block = (CS_8__locals180._VB_Local_Request.ForgeVersion == null && CS_8__locals180._VB_Local_Request.NeoForgeVersion == null && CS_8__locals180._VB_Local_Request.FabricVersion == null && CS_8__locals180._VB_Local_Request.LiteLoaderEntry == null)
					});
				}
			}
			if (CS_8__locals180._VB_Local_Request.ForgeVersion != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 Forge " + CS_8__locals180._VB_Local_Request.ForgeVersion, McDownloadForgelikeLoader(IsNeoForge: false, CS_8__locals180._VB_Local_Request.ForgeVersion, "forge-" + CS_8__locals180._VB_Local_Request.ForgeVersion, CS_8__locals180._VB_Local_Request.MinecraftName, CS_8__locals180._VB_Local_Request.ForgeEntry, CS_8__locals180._VB_Local_TempMcFolder, loaderCombo, CS_8__locals180._VB_Local_Request.VersionFolder))
				{
					Show = false,
					ProgressWeight = 25.0,
					Block = (CS_8__locals180._VB_Local_Request.FabricVersion == null && CS_8__locals180._VB_Local_Request.LiteLoaderEntry == null && CS_8__locals180._VB_Local_Request.NeoForgeEntry == null)
				});
			}
			if (CS_8__locals180._VB_Local_Request.NeoForgeVersion != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 NeoForge " + CS_8__locals180._VB_Local_Request.NeoForgeVersion, McDownloadForgelikeLoader(IsNeoForge: true, CS_8__locals180._VB_Local_Request.NeoForgeVersion, "neoforge-" + CS_8__locals180._VB_Local_Request.NeoForgeVersion, CS_8__locals180._VB_Local_Request.MinecraftName, CS_8__locals180._VB_Local_Request.NeoForgeEntry, CS_8__locals180._VB_Local_TempMcFolder, loaderCombo, CS_8__locals180._VB_Local_Request.VersionFolder))
				{
					Show = false,
					ProgressWeight = 25.0,
					Block = (CS_8__locals180._VB_Local_Request.ForgeEntry == null && CS_8__locals180._VB_Local_Request.FabricVersion == null && CS_8__locals180._VB_Local_Request.LiteLoaderEntry == null)
				});
			}
			if (CS_8__locals180._VB_Local_Request.LiteLoaderEntry != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 LiteLoader " + CS_8__locals180._VB_Local_Request.MinecraftName, McDownloadLiteLoaderLoader(CS_8__locals180._VB_Local_Request.LiteLoaderEntry, CS_8__locals180._VB_Local_TempMcFolder))
				{
					Show = false,
					ProgressWeight = 1.0,
					Block = (CS_8__locals180._VB_Local_Request.FabricVersion == null)
				});
			}
			if (CS_8__locals180._VB_Local_Request.FabricVersion != null)
			{
				list.Add(new ModLoader.LoaderCombo<string>("下载 Fabric " + CS_8__locals180._VB_Local_Request.FabricVersion, McDownloadFabricLoader(CS_8__locals180._VB_Local_Request.FabricVersion, CS_8__locals180._VB_Local_Request.MinecraftName, CS_8__locals180._VB_Local_TempMcFolder))
				{
					Show = false,
					ProgressWeight = 2.0,
					Block = true
				});
			}
			list.Add(new ModLoader.LoaderTask<string, string>("安装游戏", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				MergeJson(CS_8__locals180._VB_Local_VersionFolder, CS_8__locals180._VB_Local_VersionFolder, CS_8__locals180._VB_Local_OptiFineFolder, CS_8__locals180._VB_Local_OptiFineAsMod, CS_8__locals180._VB_Local_ForgeFolder, CS_8__locals180._VB_Local_Request.ForgeVersion, CS_8__locals180._VB_Local_NeoForgeFolder, CS_8__locals180._VB_Local_Request.NeoForgeVersion, CS_8__locals180._VB_Local_FabricFolder, CS_8__locals180._VB_Local_LiteLoaderFolder);
				Task.Progress = 0.2;
				DirectoryUtils.Copy(CS_8__locals180._VB_Local_TempMcFolder + "libraries", ModMinecraft.McFolderSelected + "libraries");
				Task.Progress = 0.8;
				string text = new ModMinecraft.McInstance(CS_8__locals180._VB_Local_VersionFolder).PathIndie + "mods\\";
				if (DirectoryUtils.Exists(CS_8__locals180._VB_Local_ModsTempFolder))
				{
					DirectoryUtils.Copy(CS_8__locals180._VB_Local_ModsTempFolder, text);
				}
				else if (CS_8__locals180._VB_Local_Modable)
				{
					DirectoryUtils.Create(text);
					Logger.Info($"自动创建 Mod 文件夹：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				string text2 = new ModMinecraft.McInstance(CS_8__locals180._VB_Local_VersionFolder).PathIndie + "resourcepacks\\";
				DirectoryUtils.Create(text2);
				Logger.Info($"自动创建资源包文件夹：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			})
			{
				ProgressWeight = 2.0,
				Block = true
			});
			if (CS_8__locals180._VB_Local_Request.OptiFineEntry != null || (CS_8__locals180._VB_Local_Request.ForgeVersion != null && Conversions.ToDouble(CS_8__locals180._VB_Local_Request.ForgeVersion.BeforeFirst(".")) >= 20.0) || CS_8__locals180._VB_Local_Request.NeoForgeVersion != null || CS_8__locals180._VB_Local_Request.FabricVersion != null || CS_8__locals180._VB_Local_Request.LiteLoaderEntry != null)
			{
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					Task.Output = ModMinecraft.McLibNetFilesFromInstance(new ModMinecraft.McInstance(CS_8__locals180._VB_Local_VersionFolder));
				})
				{
					ProgressWeight = 1.0,
					Show = false
				});
				list2.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
				{
					ProgressWeight = 7.0,
					Show = false
				});
				list.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list2)
				{
					ProgressWeight = 8.0
				});
			}
			list.Add(new ModLoader.LoaderTask<int, int>("删除忽略标识", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
			{
				CS_8__locals180._Lambda___3();
			})
			{
				Show = false
			});
			return list;
		}
		private static void MergeJson(string OutputFolder, string MinecraftFolder, string OptiFineFolder = null, bool OptiFineAsMod = false, string ForgeFolder = null, string ForgeVersion = null, string NeoForgeFolder = null, string NeoForgeVersion = null, string FabricFolder = null, string LiteLoaderFolder = null)
		{
			Logger.Info($"开始进行版本合并，输出：{OutputFolder}，Minecraft：{MinecraftFolder}" + ((OptiFineFolder != null) ? ("，OptiFine：" + OptiFineFolder) : "") + ((ForgeFolder != null) ? ("，Forge：" + ForgeFolder) : "") + ((NeoForgeFolder != null) ? ("，NeoForge：" + NeoForgeFolder) : "") + ((LiteLoaderFolder != null) ? ("，LiteLoader：" + LiteLoaderFolder) : "") + ((FabricFolder != null) ? ("，Fabric：" + FabricFolder) : ""), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			DirectoryUtils.Create(OutputFolder);
			bool flag = OptiFineFolder != null && !OptiFineAsMod;
			bool flag2 = ForgeFolder != null;
			bool flag3 = NeoForgeFolder != null;
			bool flag4 = LiteLoaderFolder != null;
			bool flag5 = FabricFolder != null;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			if (!OutputFolder.EndsWithF("\\"))
			{
				OutputFolder += "\\";
			}
			string lastPart = PathUtils.GetLastPart(OutputFolder);
			string filePath = OutputFolder + lastPart + ".json";
			string destFilePath = OutputFolder + lastPart + ".jar";
			if (!MinecraftFolder.EndsWithF("\\"))
			{
				MinecraftFolder += "\\";
			}
			string lastPart2 = PathUtils.GetLastPart(MinecraftFolder);
			string text6 = MinecraftFolder + lastPart2 + ".json";
			string sourceFilePath = MinecraftFolder + lastPart2 + ".jar";
			if (flag)
			{
				if (!OptiFineFolder.EndsWithF("\\"))
				{
					OptiFineFolder += "\\";
				}
				string lastPart3 = PathUtils.GetLastPart(OptiFineFolder);
				text = OptiFineFolder + lastPart3 + ".json";
			}
			if (flag2)
			{
				if (!ForgeFolder.EndsWithF("\\"))
				{
					ForgeFolder += "\\";
				}
				string lastPart4 = PathUtils.GetLastPart(ForgeFolder);
				text2 = ForgeFolder + lastPart4 + ".json";
			}
			if (flag3)
			{
				if (!NeoForgeFolder.EndsWithF("\\"))
				{
					NeoForgeFolder += "\\";
				}
				string lastPart5 = PathUtils.GetLastPart(NeoForgeFolder);
				text3 = NeoForgeFolder + lastPart5 + ".json";
			}
			if (flag4)
			{
				if (!LiteLoaderFolder.EndsWithF("\\"))
				{
					LiteLoaderFolder += "\\";
				}
				string lastPart6 = PathUtils.GetLastPart(LiteLoaderFolder);
				text4 = LiteLoaderFolder + lastPart6 + ".json";
			}
			if (flag5)
			{
				if (!FabricFolder.EndsWithF("\\"))
				{
					FabricFolder += "\\";
				}
				string lastPart7 = PathUtils.GetLastPart(FabricFolder);
				text5 = FabricFolder + lastPart7 + ".json";
			}
			JObject jObject = null;
			JObject jObject2 = null;
			JObject jObject3 = null;
			JObject jObject4 = null;
			JObject jObject5 = null;
			string text7 = FileUtils.TryReadAsString(text6) ?? "";
			if (!text7.StartsWithF("{"))
			{
				throw new Exception("Minecraft json 有误，地址：" + text6 + "，前段内容：" + text7.Substring(0, Math.Min(text7.Length, 1000)));
			}
			JObject jObject6 = (JObject)text7.DeserializeJson();
			if (flag)
			{
				string text8 = FileUtils.TryReadAsString(text) ?? "";
				if (!text8.StartsWithF("{"))
				{
					throw new Exception("OptiFine json 有误，地址：" + text + "，前段内容：" + text8.Substring(0, Math.Min(text8.Length, 1000)));
				}
				jObject = (JObject)text8.DeserializeJson();
			}
			if (flag2)
			{
				string text9 = FileUtils.TryReadAsString(text2) ?? "";
				if (!text9.StartsWithF("{"))
				{
					throw new Exception("Forge json 有误，地址：" + text2 + "，前段内容：" + text9.Substring(0, Math.Min(text9.Length, 1000)));
				}
				jObject2 = (JObject)text9.DeserializeJson();
			}
			if (flag3)
			{
				string text10 = FileUtils.TryReadAsString(text3) ?? "";
				if (!text10.StartsWithF("{"))
				{
					throw new Exception("NeoForge json 有误，地址：" + text3 + "，前段内容：" + text10.Substring(0, Math.Min(text10.Length, 1000)));
				}
				jObject3 = (JObject)text10.DeserializeJson();
			}
			if (flag4)
			{
				string text11 = FileUtils.TryReadAsString(text4) ?? "";
				if (!text11.StartsWithF("{"))
				{
					throw new Exception("LiteLoader json 有误，地址：" + text4 + "，前段内容：" + text11.Substring(0, Math.Min(text11.Length, 1000)));
				}
				jObject4 = (JObject)text11.DeserializeJson();
			}
			if (flag5)
			{
				string text12 = FileUtils.TryReadAsString(text5) ?? "";
				if (!text12.StartsWithF("{"))
				{
					throw new Exception("Fabric json 有误，地址：" + text5 + "，前段内容：" + text12.Substring(0, Math.Min(text12.Length, 1000)));
				}
				jObject5 = (JObject)text12.DeserializeJson();
			}
			List<string> list = (from l in ((jObject6["minecraftArguments"] ?? ((JToken)" ")).ToString() + " " + ((jObject != null) ? (jObject["minecraftArguments"] ?? ((JToken)" ")).ToString() : " ") + " " + ((jObject2 != null) ? (jObject2["minecraftArguments"] ?? ((JToken)" ")).ToString() : " ") + " " + ((jObject3 != null) ? (jObject3["minecraftArguments"] ?? ((JToken)" ")).ToString() : " ") + " " + ((jObject4 != null) ? (jObject4["minecraftArguments"] ?? ((JToken)" ")).ToString() : " ")).Split(" ", removeEmptyEntries: true)
				select l.Trim()).ToList();
			List<string> list2 = new List<string>();
			checked
			{
				int num = list.Count - 1;
				for (int num2 = 0; num2 <= num; num2++)
				{
					if (list[num2].StartsWithF("-"))
					{
						list2.Add(list[num2]);
					}
					else if (list2.Any() && list2.Last().StartsWithF("-") && !list2.Last().Contains(" "))
					{
						list2[list2.Count - 1] = list2.Last() + " " + list[num2];
					}
					else
					{
						list2.Add(list[num2]);
					}
				}
				string text13 = list2.Distinct().Join(" ");
				JObject jObject7 = jObject6;
				if (flag)
				{
					jObject.Remove("releaseTime");
					jObject.Remove("time");
					jObject7.Merge(jObject);
				}
				if (flag2)
				{
					jObject2.Remove("releaseTime");
					jObject2.Remove("time");
					jObject7.Merge(jObject2);
				}
				if (flag3)
				{
					jObject3.Remove("releaseTime");
					jObject3.Remove("time");
					jObject7.Merge(jObject3);
				}
				if (flag4)
				{
					jObject4.Remove("releaseTime");
					jObject4.Remove("time");
					jObject7.Merge(jObject4);
				}
				if (flag5)
				{
					jObject5.Remove("releaseTime");
					jObject5.Remove("time");
					jObject7.Merge(jObject5);
				}
				if (text13 != null && Operators.CompareString(text13.Replace(" ", ""), "", TextCompare: false) != 0)
				{
					jObject7["minecraftArguments"] = text13;
				}
				jObject7.Remove("_comment_");
				jObject7.Remove("inheritsFrom");
				jObject7.Remove("jar");
				jObject7["id"] = lastPart;
				FileUtils.Write(filePath, jObject7.ToString());
				FileUtils.Copy(sourceFilePath, destFilePath);
				Logger.Info($"版本合并 {lastPart} 完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
		}
	}
}
