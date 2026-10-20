// PCL

namespace PCL
{
	public class CrashAnalyzer
	{
		private enum AnalyzeFileType
		{
			HsErr,
			MinecraftLog,
			ExtraLogFile,
			ExtraReportFile,
			CrashReport
		}
		private enum CrashReason
		{
			Java虚拟机参数有误,
			Mod文件被解压,
			MixinBootstrap缺失,
			内存不足,
			使用JDK,
			显卡不支持OpenGL,
			使用OpenJ9,
			Java版本过高,
			Java版本不兼容,
			Mod名称包含特殊字符,
			显卡驱动不支持导致无法设置像素格式,
			极短的程序输出,
			Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION,
			玩家手动触发调试崩溃,
			光影或资源包导致OpenGL1282错误,
			文件或内容校验失败,
			确定Mod导致游戏崩溃,
			怀疑Mod导致游戏崩溃,
			Mod配置文件导致游戏崩溃,
			ModMixin失败,
			Mod加载器报错,
			Mod初始化失败,
			堆栈分析发现关键字,
			堆栈分析发现Mod名称,
			OptiFine导致无法加载世界,
			特定方块导致崩溃,
			特定实体导致崩溃,
			材质过大或显卡配置不足,
			没有可用的分析文件,
			使用32位Java导致JVM无法分配足够多的内存,
			Mod重复安装,
			Mod互不兼容,
			OptiFine与Forge不兼容,
			Fabric报错,
			Fabric报错并给出解决方案,
			Forge报错,
			低版本Forge与高版本Java不兼容,
			版本Json中存在多个Forge,
			Mod过多导致超出ID限制,
			NightConfig的Bug,
			ShadersMod与OptiFine同时安装,
			Forge安装不完整,
			Mod需要Java11,
			Mod缺少前置或MC版本错误
		}
		[CompilerGenerated]
		internal sealed class _Closure___28_0
		{
			public string _VB_Local_FileAddress;
			public _Closure___28_0(_Closure___28_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				_VB_Local_FileAddress = Dialogs.SaveFile("选择保存位置", "错误报告-" + DateTime.Now.ToString("G").Replace("/", "-").Replace(":", ".")
					.Replace(" ", "_") + ".zip", null, new (string, string)[1] { ("zip", "Minecraft 错误报告") });
			}
		}
		private string TempFolder;
		private ModMinecraft.McInstance TargetInstance;
		private List<KeyValuePair<string, string[]>> AnalyzeRawFiles;
		private KeyValuePair<string, string[]>? DirectFile;
		private string LogMc;
		private string LogMcDebug;
		private string LogHs;
		private string LogCrash;
		private string LogAll;
		private Dictionary<CrashReason, List<string>> CrashReasons;
		private List<string> OutputFiles;
		public CrashAnalyzer(ModMinecraft.McInstance TargetInstance = null)
		{
			AnalyzeRawFiles = new List<KeyValuePair<string, string[]>>();
			DirectFile = null;
			LogMc = null;
			LogMcDebug = null;
			LogHs = null;
			LogCrash = null;
			CrashReasons = new Dictionary<CrashReason, List<string>>();
			OutputFiles = new List<string>();
			this.TargetInstance = TargetInstance;
			TempFolder = ModMain.RequestTaskTempFolder();
			DirectoryUtils.Create(TempFolder + "Temp\\");
			DirectoryUtils.Create(TempFolder + "Report\\");
			Logger.Info($"崩溃分析暂存文件夹：{TempFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
		}
		public void Collect(string VersionPathIndie, IList<string> LatestLog = null)
		{
			Logger.Info("步骤 1：收集日志文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			List<string> list = new List<string>();
			try
			{
				list.AddRange(DirectoryUtils.EnumerateFiles(VersionPathIndie + "crash-reports\\", includeSubDirectories: true));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "收集 Minecraft 崩溃日志文件夹下的日志失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				foreach (FileInfo item in DirectoryUtils.GetInfo(VersionPathIndie).Parent.Parent.EnumerateFiles())
				{
					if (Operators.CompareString(item.Extension ?? "", ".log", TextCompare: false) == 0)
					{
						list.Add(PathUtils.RemoveExtendedPrefix(item.FullName));
					}
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "收集 Minecraft 主文件夹下的日志失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				foreach (FileInfo item2 in DirectoryUtils.GetInfo(VersionPathIndie).EnumerateFiles())
				{
					if (Operators.CompareString(item2.Extension ?? "", ".log", TextCompare: false) == 0)
					{
						list.Add(PathUtils.RemoveExtendedPrefix(item2.FullName));
					}
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Warn(ex6, "收集 Minecraft 隔离文件夹下的日志失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				ProjectData.ClearProjectError();
			}
			list.Add(VersionPathIndie + "logs\\latest.log");
			list.Add(VersionPathIndie + "logs\\debug.log");
			list = list.Distinct().ToList();
			List<string> list2 = new List<string>();
			foreach (string item3 in list)
			{
				try
				{
					FileInfo info = FileUtils.GetInfo(item3);
					if (info.Exists)
					{
						double num = Math.Abs((info.LastWriteTime - DateTime.Now).TotalMinutes);
						if (num < 3.0 && info.Length > 0)
						{
							list2.Add(item3);
							Logger.Info($"可能可用的日志文件：{item3}（{Math.Round(num, 1)} 分钟）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
					}
				}
				catch (Exception ex7)
				{
					ProjectData.SetProjectError(ex7);
					Exception ex8 = ex7;
					Logger.Warn(ex8, $"确认崩溃日志时间失败（{item3}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					ProjectData.ClearProjectError();
				}
			}
			if (!list2.Any())
			{
				Logger.Info("未发现可能可用的日志文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			}
			foreach (string item4 in list2)
			{
				try
				{
					AnalyzeRawFiles.Add(new KeyValuePair<string, string[]>(item4, FileUtils.ReadAsLines(item4)));
				}
				catch (Exception ex9)
				{
					ProjectData.SetProjectError(ex9);
					Exception ex10 = ex9;
					Logger.Warn(ex10, $"读取可能的崩溃日志文件失败（{item4}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					ProjectData.ClearProjectError();
				}
			}
			if (LatestLog != null && LatestLog.Any())
			{
				string text = LatestLog.Join("\r\n");
				Logger.Info(string.Format("以下为游戏输出的最后一段内容：{0}{1}", "\r\n", text), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				FileUtils.Write(TempFolder + "RawOutput.log", text);
				AnalyzeRawFiles.Add(new KeyValuePair<string, string[]>(TempFolder + "RawOutput.log", LatestLog.ToArray()));
				LatestLog.Clear();
			}
			Logger.Info($"步骤 1：收集日志文件完成，收集到 {AnalyzeRawFiles.Count} 个文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
		}
		public void Import(string FilePath)
		{
			Logger.Info("步骤 1：自主导入日志文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			try
			{
				FileInfo info = FileUtils.GetInfo(FilePath);
				if (info.Exists && info.Length > 0 && !FilePath.EndsWithF(".jar", ignoreCase: true))
				{
					FileUtils.ExtractToDirectory(FilePath, TempFolder + "Temp\\");
					Logger.Info($"已解压导入的日志文件：{FilePath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					goto IL_00b0;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			FileUtils.Copy(FilePath, TempFolder + "Temp\\" + PathUtils.GetLastPart(FilePath));
			Logger.Info($"已复制导入的日志文件：{FilePath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			goto IL_00b0;
			IL_00b0:
			foreach (FileInfo item in DirectoryUtils.GetInfo(TempFolder + "Temp\\").EnumerateFiles())
			{
				try
				{
					if (item.Exists && item.Length != 0L)
					{
						string left = item.Extension.Lower();
						if (Operators.CompareString(left, ".log", TextCompare: false) == 0 || Operators.CompareString(left, ".txt", TextCompare: false) == 0)
						{
							AnalyzeRawFiles.Add(new KeyValuePair<string, string[]>(PathUtils.RemoveExtendedPrefix(item.FullName), FileUtils.ReadAsLines(item.FullName)));
						}
						else
						{
							FileUtils.Delete(item.FullName);
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "导入单个日志文件失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					ProjectData.ClearProjectError();
				}
			}
			Logger.Info($"步骤 1：自主导入日志文件，收集到 {AnalyzeRawFiles.Count} 个文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
		}
		public bool Prepare()
		{
			Logger.Info("步骤 2：准备日志文本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			DirectFile = null;
			List<KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>>> list = new List<KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>>>();
			foreach (KeyValuePair<string, string[]> analyzeRawFile in AnalyzeRawFiles)
			{
				string text = PathUtils.GetLastPart(analyzeRawFile.Key).Lower();
				AnalyzeFileType analyzeFileType;
				if (text.StartsWithF("hs_err"))
				{
					analyzeFileType = AnalyzeFileType.HsErr;
					if (!DirectFile.HasValue)
					{
						DirectFile = analyzeRawFile;
					}
				}
				else if (text.StartsWithF("crash-"))
				{
					analyzeFileType = AnalyzeFileType.CrashReport;
					if (!DirectFile.HasValue)
					{
						DirectFile = analyzeRawFile;
					}
				}
				else
				{
					switch (text)
					{
					case "latest.log":
					case "latest log.txt":
					case "debug.log":
					case "debug log.txt":
					case "游戏崩溃前的输出.txt":
					case "rawoutput.log":
						analyzeFileType = AnalyzeFileType.MinecraftLog;
						DirectFile = analyzeRawFile;
						break;
					case "启动器日志.txt":
					case "PCL2 启动器日志.txt":
					case "PCL 启动器日志.txt":
					case "log1.txt":
						if (analyzeRawFile.Value.Any([SpecialName] (string s) => s.Contains("以下为游戏输出的最后一段内容")))
						{
							analyzeFileType = AnalyzeFileType.MinecraftLog;
							if (!DirectFile.HasValue)
							{
								DirectFile = analyzeRawFile;
							}
						}
						else
						{
							analyzeFileType = AnalyzeFileType.ExtraLogFile;
						}
						break;
					default:
						if (text.EndsWithF(".log", ignoreCase: true))
						{
							analyzeFileType = AnalyzeFileType.ExtraLogFile;
							break;
						}
						if (text.EndsWithF(".txt", ignoreCase: true))
						{
							analyzeFileType = AnalyzeFileType.ExtraReportFile;
							break;
						}
						Logger.Info($"{text} 分类为 Ignore", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						continue;
					}
				}
				if (analyzeRawFile.Value.Any())
				{
					list.Add(new KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>>(analyzeFileType, analyzeRawFile));
					Logger.Info($"{text} 分类为 {analyzeFileType}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				else
				{
					Logger.Info($"{text} 由于内容为空跳过", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
			}
			if (list.Any() && list.All([SpecialName] (KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>> p) => p.Key == AnalyzeFileType.ExtraLogFile))
			{
				Logger.Info("由于仅发现了额外日志，将它们视作 Minecraft 日志进行分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				list = list.Select([SpecialName] (KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>> p) => new KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>>(AnalyzeFileType.MinecraftLog, p.Value)).ToList();
			}
			AnalyzeFileType[] array = new AnalyzeFileType[4]
			{
				AnalyzeFileType.MinecraftLog,
				AnalyzeFileType.HsErr,
				AnalyzeFileType.ExtraLogFile,
				AnalyzeFileType.CrashReport
			};
			foreach (AnalyzeFileType analyzeFileType2 in array)
			{
				List<KeyValuePair<string, string[]>> list2 = new List<KeyValuePair<string, string[]>>();
				foreach (KeyValuePair<AnalyzeFileType, KeyValuePair<string, string[]>> item in list)
				{
					if (analyzeFileType2 == item.Key)
					{
						list2.Add(item.Value);
					}
				}
				if (!list2.Any())
				{
					continue;
				}
				try
				{
					switch (analyzeFileType2)
					{
					case AnalyzeFileType.HsErr:
					case AnalyzeFileType.CrashReport:
					{
						SortedList<DateTime, KeyValuePair<string, string[]>> sortedList = new SortedList<DateTime, KeyValuePair<string, string[]>>();
						foreach (KeyValuePair<string, string[]> item2 in list2)
						{
							try
							{
								sortedList.Add(FileUtils.GetInfo(item2.Key).LastWriteTime, item2);
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								Logger.Warn(ex2, "获取日志文件修改时间失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
								sortedList.Add(new DateTime(1900, 1, 1), item2);
								ProjectData.ClearProjectError();
							}
						}
						KeyValuePair<string, string[]> value3 = sortedList.Last().Value;
						OutputFiles.Add(value3.Key);
						if (analyzeFileType2 == AnalyzeFileType.HsErr)
						{
							LogHs = GetHeadTailLines(value3.Value, 200, 100);
							Logger.Info($"输出报告：{value3.Key}，作为虚拟机错误信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							Logger.Info($"导入分析：{value3.Key}，作为虚拟机错误信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
						else
						{
							LogCrash = GetHeadTailLines(value3.Value, 300, 700);
							Logger.Info($"输出报告：{value3.Key}，作为 Minecraft 崩溃报告", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							Logger.Info($"导入分析：{value3.Key}，作为 Minecraft 崩溃报告", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
						break;
					}
					case AnalyzeFileType.MinecraftLog:
					{
						LogMc = "";
						LogMcDebug = "";
						Dictionary<string, KeyValuePair<string, string[]>> dictionary = new Dictionary<string, KeyValuePair<string, string[]>>();
						foreach (KeyValuePair<string, string[]> item3 in list2)
						{
							dictionary[PathUtils.GetLastPart(item3.Key).Lower()] = item3;
							OutputFiles.Add(item3.Key);
							Logger.Info($"输出报告：{item3.Key}，作为 Minecraft 或启动器日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
						string[] array2 = new string[6] { "rawoutput.log", "启动器日志.txt", "log1.txt", "游戏崩溃前的输出.txt", "PCL2 启动器日志.txt", "PCL 启动器日志.txt" };
						foreach (string key in array2)
						{
							if (!dictionary.ContainsKey(key))
							{
								continue;
							}
							KeyValuePair<string, string[]> keyValuePair = dictionary[key];
							bool flag = false;
							string[] value = keyValuePair.Value;
							foreach (string text2 in value)
							{
								if (flag)
								{
									ref string logMc = ref LogMc;
									logMc = logMc + text2 + "\n";
								}
								else if (text2.Contains("以下为游戏输出的最后一段内容"))
								{
									flag = true;
									Logger.Info("找到 PCL 输出的游戏实时日志头", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
								}
							}
							if (!flag)
							{
								LogMc += GetHeadTailLines(keyValuePair.Value, 0, 500);
							}
							LogMc = LogMc.TrimEnd("\r\n".ToCharArray());
							Logger.Info($"导入分析：{keyValuePair.Key}，作为启动器日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							break;
						}
						string[] array3 = new string[4] { "latest.log", "latest log.txt", "debug.log", "debug log.txt" };
						foreach (string key2 in array3)
						{
							if (dictionary.ContainsKey(key2))
							{
								KeyValuePair<string, string[]> keyValuePair2 = dictionary[key2];
								LogMc += GetHeadTailLines(keyValuePair2.Value, 1500, 500);
								Logger.Info($"导入分析：{keyValuePair2.Key}，作为 Minecraft 日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
								break;
							}
						}
						string[] array4 = new string[2] { "debug.log", "debug log.txt" };
						foreach (string key3 in array4)
						{
							if (dictionary.ContainsKey(key3))
							{
								KeyValuePair<string, string[]> keyValuePair3 = dictionary[key3];
								LogMcDebug += GetHeadTailLines(keyValuePair3.Value, 1000, 0);
								Logger.Info($"导入分析：{keyValuePair3.Key}，作为 Minecraft Debug 日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
								break;
							}
						}
						if (Operators.CompareString(LogMc, "", TextCompare: false) == 0)
						{
							if (Operators.CompareString(LogMcDebug, "", TextCompare: false) != 0)
							{
								LogMc = LogMcDebug;
							}
							else
							{
								if (!dictionary.Any())
								{
									LogMc = null;
									throw new Exception("无法找到匹配的 Minecraft Log");
								}
								KeyValuePair<string, string[]> value2 = dictionary.First().Value;
								LogMc += GetHeadTailLines(value2.Value, 1500, 500);
								Logger.Info($"导入分析：{value2.Key}，作为兜底日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							}
						}
						if (Operators.CompareString(LogMcDebug, "", TextCompare: false) == 0)
						{
							LogMcDebug = null;
						}
						break;
					}
					case AnalyzeFileType.ExtraLogFile:
					case AnalyzeFileType.ExtraReportFile:
						foreach (KeyValuePair<string, string[]> item4 in list2)
						{
							OutputFiles.Add(item4.Key);
							Logger.Info($"输出报告：{item4.Key}，不用作分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
						break;
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "分类处理日志文件时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					ProjectData.ClearProjectError();
				}
			}
			bool flag2 = LogMc != null || LogHs != null || LogCrash != null;
			if (flag2)
			{
				Logger.Info(string.Format("{0}用作分析", ("[Crash] 步骤 2：准备日志文本完成，找到" + ((LogMc == null) ? "" : "游戏日志、") + ((LogMcDebug == null) ? "" : "游戏 Debug 日志、") + ((LogHs == null) ? "" : "虚拟机日志、") + ((LogCrash == null) ? "" : "崩溃日志、")).TrimEnd('、')), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			}
			else
			{
				Logger.Info("步骤 2：准备日志文本完成，没有任何可供分析的日志", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			}
			return flag2;
		}
		private string GetHeadTailLines(string[] Raw, int HeadLines, int TailLines)
		{
			checked
			{
				if (Raw.Length <= HeadLines + TailLines)
				{
					return Raw.Distinct().Join("\n");
				}
				List<string> list = new List<string>();
				int num = 0;
				int num2 = Raw.Length - 1;
				int i;
				for (i = 0; i <= num2; i++)
				{
					if (!list.Contains(Raw[i]))
					{
						num++;
						list.Add(Raw[i]);
						if (num >= HeadLines)
						{
							break;
						}
					}
				}
				int num3 = 0;
				int num4 = Raw.Length - 1;
				int num5 = i;
				for (int j = num4; j >= num5; j += -1)
				{
					if (!list.Contains(Raw[j]))
					{
						num3++;
						list.Insert(num, Raw[j]);
						if (num3 >= TailLines)
						{
							break;
						}
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string item in list)
				{
					if (Operators.CompareString(item, "", TextCompare: false) != 0)
					{
						stringBuilder.Append(item);
						stringBuilder.Append("\n");
					}
				}
				return stringBuilder.ToString();
			}
		}
		public void Analyze()
		{
			Logger.Info("步骤 3：分析崩溃原因", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			LogAll = (LogMc ?? LogMcDebug ?? "") + LogHs + LogCrash;
			AnalyzeCrit1();
			if (!CrashReasons.Any())
			{
				AnalyzeCrit2();
				if (!CrashReasons.Any())
				{
					if (LogAll.Contains("orge") || LogAll.Contains("abric") || LogAll.Contains("uilt") || LogAll.Contains("iteloader"))
					{
						List<string> list = new List<string>();
						if (LogCrash != null)
						{
							Logger.Info("开始进行崩溃日志堆栈分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							list.AddRange(AnalyzeStackKeyword(LogCrash.BeforeFirst("System Details")));
						}
						if (LogMc != null)
						{
							List<string> list2 = LogMc.RegexSearch("/FATAL] .+?(?=[\\n]+\\[)").ToList();
							if (LogMc.Contains("Unreported exception thrown!"))
							{
								list2.Add(LogMc.Between("Unreported exception thrown!", "at oolloo.jlw.Wrapper"));
							}
							Logger.Info($"开始进行 Minecraft 日志堆栈分析，发现 {list2.Count} 个报错项", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							foreach (string item in list2)
							{
								list.AddRange(AnalyzeStackKeyword(item));
							}
						}
						if (LogHs != null)
						{
							Logger.Info("开始进行虚拟机堆栈分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
							string errorStack = LogHs.Between("T H R E A D", "Registers:");
							list.AddRange(AnalyzeStackKeyword(errorStack));
						}
						if (list.Any())
						{
							List<string> list3 = AnalyzeModName(list);
							if (list3 == null)
							{
								AppendReason(CrashReason.堆栈分析发现关键字, list);
							}
							else
							{
								AppendReason(CrashReason.堆栈分析发现Mod名称, list3);
							}
							goto IL_0219;
						}
					}
					else
					{
						Logger.Info("可能并未安装 Mod，不进行堆栈分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					}
					AnalyzeCrit3();
				}
			}
			goto IL_0219;
			IL_0219:
			if (!CrashReasons.Any())
			{
				Logger.Info("步骤 3：分析崩溃原因完成，未找到可能的原因", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				return;
			}
			Logger.Info($"步骤 3：分析崩溃原因完成，找到 {CrashReasons.Count} 条可能的原因", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			foreach (KeyValuePair<CrashReason, List<string>> crashReason in CrashReasons)
			{
				Logger.Info(string.Format("- {0}{1}", crashReason.Key, crashReason.Value.Any() ? ("（" + crashReason.Value.Join('；') + "）") : ""), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			}
		}
		private void AppendReason(CrashReason Reason, IEnumerable<string> Additional = null)
		{
			if (CrashReasons.ContainsKey(Reason))
			{
				if (Additional != null)
				{
					CrashReasons[Reason].AddRange(Additional);
					CrashReasons[Reason] = CrashReasons[Reason].Distinct().ToList();
				}
			}
			else
			{
				CrashReasons.Add(Reason, new List<string>((Additional ?? new string[0]).ToList()));
			}
			Logger.Info(string.Format("可能的崩溃原因：{0}{1}", Reason, (Additional != null && Additional.Any()) ? ("（" + Additional.Join('；') + "）") : ""), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
		}
		private void AppendReason(CrashReason Reason, string Additional)
		{
			AppendReason(Reason, string.IsNullOrEmpty(Additional) ? null : new List<string> { Additional });
		}
		private void AnalyzeCrit1()
		{
			if (LogMc == null && LogHs == null && LogCrash == null)
			{
				AppendReason(CrashReason.没有可用的分析文件);
				return;
			}
			if (LogCrash != null)
			{
				if (LogCrash.Contains("Unable to make protected final java.lang.Class java.lang.ClassLoader.defineClass"))
				{
					AppendReason(CrashReason.Java版本过高);
				}
				if (LogCrash.Contains("Failed loading config file "))
				{
					AppendReason(CrashReason.Mod配置文件导致游戏崩溃, new string[2]
					{
						TryAnalyzeModName((LogCrash.RegexSeek("(?<=Failed loading config file .+ for modid )[^\\n]+") ?? "").TrimEnd('\r')).First(),
						(LogCrash.RegexSeek("(?<=Failed loading config file ).+(?= of type)") ?? "").TrimEnd('\r')
					});
				}
			}
			if (LogMc != null)
			{
				if (LogMc.Contains("Unrecognized option:"))
				{
					AppendReason(CrashReason.Java虚拟机参数有误);
				}
				if (LogMc.Contains("Found multiple arguments for option fml.forgeVersion, but you asked for only one"))
				{
					AppendReason(CrashReason.版本Json中存在多个Forge);
				}
				if (LogMc.Contains("The driver does not appear to support OpenGL"))
				{
					AppendReason(CrashReason.显卡不支持OpenGL);
				}
				if (LogMc.Contains("java.lang.ClassCastException: java.base/jdk"))
				{
					AppendReason(CrashReason.使用JDK);
				}
				if (LogMc.Contains("java.lang.ClassCastException: class jdk."))
				{
					AppendReason(CrashReason.使用JDK);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.client.renderer.texture.SpriteContents.<init>"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'java.lang.String com.mojang.blaze3d.systems.RenderSystem.getBackendDescription"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.client.renderer.block.model.BakedQuad.<init>"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'void net.minecraftforge.client.gui.overlay.ForgeGui.renderSelectedItemName"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'void net.minecraft.server.level.DistanceManager"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: 'net.minecraft.network.chat.FormattedText net.minecraft.client.gui.Font.ellipsize"))
				{
					AppendReason(CrashReason.OptiFine与Forge不兼容);
				}
				if (LogMc.Contains("Open J9 is not supported") || LogMc.Contains("OpenJ9 is incompatible") || LogMc.Contains(".J9VMInternals."))
				{
					AppendReason(CrashReason.使用OpenJ9);
				}
				if (LogMc.Contains("java.lang.NoSuchFieldException: ucp"))
				{
					AppendReason(CrashReason.Java版本过高);
				}
				if (LogMc.Contains("because module java.base does not export"))
				{
					AppendReason(CrashReason.Java版本过高);
				}
				if (LogMc.Contains("java.lang.ClassNotFoundException: jdk.nashorn.api.scripting.NashornScriptEngineFactory"))
				{
					AppendReason(CrashReason.Java版本过高);
				}
				if (LogMc.Contains("java.lang.ClassNotFoundException: java.lang.invoke.LambdaMetafactory"))
				{
					AppendReason(CrashReason.Java版本过高);
				}
				if (LogMc.Contains("The directories below appear to be extracted jar files. Fix this before you continue."))
				{
					AppendReason(CrashReason.Mod文件被解压);
				}
				if (LogMc.Contains("Extracted mod jars found, loading will NOT continue"))
				{
					AppendReason(CrashReason.Mod文件被解压);
				}
				if (LogMc.Contains("java.lang.ClassNotFoundException: org.spongepowered.asm.launch.MixinTweaker"))
				{
					AppendReason(CrashReason.MixinBootstrap缺失);
				}
				if (LogMc.Contains("Couldn't set pixel format"))
				{
					AppendReason(CrashReason.显卡驱动不支持导致无法设置像素格式);
				}
				if (LogMc.Contains("java.lang.OutOfMemoryError") || LogMc.Contains("an out of memory error"))
				{
					AppendReason(CrashReason.内存不足);
				}
				if (LogMc.Contains("java.lang.RuntimeException: Shaders Mod detected. Please remove it, OptiFine has built-in support for shaders."))
				{
					AppendReason(CrashReason.ShadersMod与OptiFine同时安装);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: sun.security.util.ManifestEntryVerifier") || LogMc.Contains("java.lang.NoSuchMethodError: 'void sun.security.util.ManifestEntryVerifier"))
				{
					AppendReason(CrashReason.低版本Forge与高版本Java不兼容);
				}
				if (LogMc.Contains("1282: Invalid operation"))
				{
					AppendReason(CrashReason.光影或资源包导致OpenGL1282错误);
				}
				if (LogMc.Contains("signer information does not match signer information of other classes in the same package"))
				{
					AppendReason(CrashReason.文件或内容校验失败, (LogMc.RegexSeek("(?<=class \")[^']+(?=\"'s signer information)") ?? "").TrimEnd('\r'));
				}
				if (LogMc.Contains("Maybe try a lower resolution resourcepack?"))
				{
					AppendReason(CrashReason.材质过大或显卡配置不足);
				}
				if (LogMc.Contains("java.lang.NoSuchMethodError: net.minecraft.world.server.ChunkManager$ProxyTicketManager.shouldForceTicks(J)Z") && LogMc.Contains("OptiFine"))
				{
					AppendReason(CrashReason.OptiFine导致无法加载世界);
				}
				if (LogMc.Contains("com.electronwill.nightconfig.core.io.ParsingException: Not enough data available") && !CrashReasons.ContainsKey(CrashReason.Mod配置文件导致游戏崩溃))
				{
					AppendReason(CrashReason.NightConfig的Bug);
				}
				if (LogMc.Contains("Cannot find launch target fmlclient, unable to launch"))
				{
					AppendReason(CrashReason.Forge安装不完整);
				}
				if (LogMc.Contains("Invalid paths argument, contained no existing paths") && LogMc.Contains("libraries\\net\\minecraftforge\\fmlcore"))
				{
					AppendReason(CrashReason.Forge安装不完整);
				}
				if (LogMc.Contains("Invalid module name: '' is not a Java identifier"))
				{
					AppendReason(CrashReason.Mod名称包含特殊字符);
				}
				if (LogMc.Contains("has been compiled by a more recent version of the Java Runtime (class file version 55.0), this version of the Java Runtime only recognizes class file versions up to"))
				{
					AppendReason(CrashReason.Mod需要Java11);
				}
				if (LogMc.Contains("java.lang.RuntimeException: java.lang.NoSuchMethodException: no such method: sun.misc.Unsafe.defineAnonymousClass(Class,byte[],Object[])Class/invokeVirtual"))
				{
					AppendReason(CrashReason.Mod需要Java11);
				}
				if (LogMc.Contains("java.lang.IllegalArgumentException: The requested compatibility level JAVA_11 could not be set. Level is not supported by the active JRE or ASM version"))
				{
					AppendReason(CrashReason.Mod需要Java11);
				}
				if (LogMc.Contains("Unsupported class file major version"))
				{
					AppendReason(CrashReason.Java版本不兼容);
				}
				if (LogMc.Contains("Unsupported major.minor version"))
				{
					AppendReason(CrashReason.Java版本不兼容);
				}
				if (LogMc.Contains("Level is not supported by the active JRE or ASM version"))
				{
					AppendReason(CrashReason.Java版本不兼容);
				}
				if (LogMc.Contains("Invalid maximum heap size"))
				{
					AppendReason(CrashReason.使用32位Java导致JVM无法分配足够多的内存);
				}
				if (LogMc.Contains("Could not reserve enough space"))
				{
					if (LogMc.Contains("for 1048576KB object heap"))
					{
						AppendReason(CrashReason.使用32位Java导致JVM无法分配足够多的内存);
					}
					else
					{
						AppendReason(CrashReason.内存不足);
					}
				}
				if (LogMc.Contains("Caught exception from "))
				{
					AppendReason(CrashReason.确定Mod导致游戏崩溃, TryAnalyzeModName(LogMc.RegexSeek("(?<=Caught exception from )[^\\n]+?")?.TrimEnd("\r\n ".ToCharArray())));
				}
				if (LogMc.Contains("DuplicateModsFoundException"))
				{
					AppendReason(CrashReason.Mod重复安装, LogMc.RegexSearch("(?<=\\n\\t[\\w]+ : [A-Z]:[^\\n]+(/|\\\\))[^/\\\\\\n]+?.jar", RegexOptions.IgnoreCase));
				}
				if (LogMc.Contains("Found a duplicate mod"))
				{
					AppendReason(CrashReason.Mod重复安装, (LogMc.RegexSeek("Found a duplicate mod[^\\n]+") ?? "").RegexSearch("[^\\\\/]+.jar", RegexOptions.IgnoreCase));
				}
				if (LogMc.Contains("Found duplicate mods"))
				{
					AppendReason(CrashReason.Mod重复安装, LogMc.RegexSearch("(?<=Mod ID: ')\\w+?(?=' from mod files:)").Distinct().ToList());
				}
				if (LogMc.Contains("ModResolutionException: Duplicate"))
				{
					AppendReason(CrashReason.Mod重复安装, (LogMc.RegexSeek("ModResolutionException: Duplicate[^\\n]+") ?? "").RegexSearch("[^\\\\/]+.jar", RegexOptions.IgnoreCase));
				}
				if (LogMc.Contains("Incompatible mods found!"))
				{
					AppendReason(CrashReason.Mod互不兼容, (LogMc.RegexSeek("(?<=Incompatible mods found![\\s\\S]+: )[\\s\\S]+?(?=\\tat )") ?? "").BeforeFirst("更多信息：").Replace("Some of your mods are incompatible with the game or each other!", "").Trim("\r\n ".ToCharArray()));
				}
				if (LogMc.Contains("Missing or unsupported mandatory dependencies:"))
				{
					AppendReason(CrashReason.Mod缺少前置或MC版本错误, (from s in LogMc.RegexSearch("(?<=Missing or unsupported mandatory dependencies:)([\\n\\r]+\\t(.*))+", RegexOptions.IgnoreCase)
						select s.Trim("\r\n\t ".ToCharArray())).Distinct().ToList());
				}
			}
			if (LogHs != null)
			{
				if (LogHs.Contains("The system is out of physical RAM or swap space"))
				{
					AppendReason(CrashReason.内存不足);
				}
				if (LogHs.Contains("Out of Memory Error"))
				{
					AppendReason(CrashReason.内存不足);
				}
				if (LogHs.Contains("EXCEPTION_ACCESS_VIOLATION"))
				{
					if (LogHs.Contains("# C  [ig"))
					{
						AppendReason(CrashReason.Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION);
					}
					if (LogHs.Contains("# C  [atio"))
					{
						AppendReason(CrashReason.AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION);
					}
					if (LogHs.Contains("# C  [nvoglv"))
					{
						AppendReason(CrashReason.Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION);
					}
				}
			}
			if (LogCrash == null)
			{
				return;
			}
			if (LogCrash.Contains("maximum id range exceeded"))
			{
				AppendReason(CrashReason.Mod过多导致超出ID限制);
			}
			if (LogCrash.Contains("java.lang.OutOfMemoryError"))
			{
				AppendReason(CrashReason.内存不足);
			}
			if (LogCrash.Contains("Pixel format not accelerated"))
			{
				AppendReason(CrashReason.显卡驱动不支持导致无法设置像素格式);
			}
			if (LogCrash.Contains("Manually triggered debug crash"))
			{
				AppendReason(CrashReason.玩家手动触发调试崩溃);
			}
			if (LogCrash.Contains("has mods that were not found") && LogCrash.RegexCheck("The Mod File [^\\n]+optifine\\\\OptiFine[^\\n]+ has mods that were not found"))
			{
				AppendReason(CrashReason.OptiFine与Forge不兼容);
			}
			if (LogCrash.Contains("-- MOD "))
			{
				string text = LogCrash.Between("-- MOD ", "Failure message:");
				if (text.ContainsIgnoreCase(".jar"))
				{
					AppendReason(CrashReason.确定Mod导致游戏崩溃, (text.RegexSeek("(?<=Mod File: ).+") ?? "").TrimEnd("\r\n ".ToCharArray()));
				}
				else
				{
					AppendReason(CrashReason.Mod加载器报错, (LogCrash.RegexSeek("(?<=Failure message: )[\\w\\W]+?(?=\\tMod)") ?? "").Replace("\t", " ").TrimEnd("\r\n ".ToCharArray()));
				}
			}
			if (LogCrash.Contains("Multiple entries with same key: "))
			{
				AppendReason(CrashReason.确定Mod导致游戏崩溃, TryAnalyzeModName((LogCrash.RegexSeek("(?<=Multiple entries with same key: )[^=]+") ?? "").TrimEnd("\r\n ".ToCharArray())));
			}
			if (LogCrash.Contains("LoaderExceptionModCrash: Caught exception from "))
			{
				AppendReason(CrashReason.确定Mod导致游戏崩溃, TryAnalyzeModName((LogCrash.RegexSeek("(?<=LoaderExceptionModCrash: Caught exception from )[^\\n]+") ?? "").TrimEnd("\r\n ".ToCharArray())));
			}
			if (LogCrash.Contains("Failed loading config file "))
			{
				AppendReason(CrashReason.Mod配置文件导致游戏崩溃, new string[2]
				{
					TryAnalyzeModName((LogCrash.RegexSeek("(?<=Failed loading config file .+ for modid )[^\\n]+") ?? "").TrimEnd('\r')).First(),
					(LogCrash.RegexSeek("(?<=Failed loading config file ).+(?= of type)") ?? "").TrimEnd('\r')
				});
			}
		}
		private void AnalyzeCrit2()
		{
			VB_AnonymousDelegate_9<string, bool> obj = [SpecialName] (string LogText) =>
			{
				if (!LogText.Contains("Mixin prepare failed ") && !LogText.Contains("Mixin apply failed ") && !LogText.Contains("MixinApplyError") && !LogText.Contains("MixinTransformerError") && !LogText.Contains("mixin.injection.throwables.") && !LogText.Contains(".json] FAILED during )"))
				{
					return false;
				}
				string text2 = LogText.RegexSeek("(?<=from mod )[^.\\/ ]+(?=\\] from)");
				if (text2 == null)
				{
					text2 = LogText.RegexSeek("(?<=for mod )[^.\\/ ]+(?= failed)");
				}
				if (text2 != null)
				{
					AppendReason(CrashReason.ModMixin失败, TryAnalyzeModName(text2.TrimEnd("\r\n ".ToCharArray())));
					return true;
				}
				using (IEnumerator<string> enumerator = LogText.RegexSearch("(?<=^[^\\t]+[ \\[{(])[^ \\[{(]+\\.[^ ]+(?=\\.json)", RegexOptions.Multiline).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						AppendReason(CrashReason.ModMixin失败, TryAnalyzeModName(current.Replace("mixins", "mixin").Replace(".mixin", "").Replace("mixin.", "")));
						return true;
					}
				}
				AppendReason(CrashReason.ModMixin失败);
				return true;
			};
			if (LogMc != null)
			{
				bool flag = obj(LogMc);
				if (LogMc.Contains("An exception was thrown, the game will display an error screen and halt."))
				{
					AppendReason(CrashReason.Forge报错, LogMc.RegexSeek("(?<=the game will display an error screen and halt.[\\n\\r]+[^\\n]+?Exception: )[\\s\\S]+?(?=\\n\\tat)")?.Trim('\r'));
				}
				if (LogMc.Contains("A potential solution has been determined:"))
				{
					AppendReason(CrashReason.Fabric报错并给出解决方案, (LogMc.RegexSeek("(?<=A potential solution has been determined:\\n)(\\s+ - [^\\n]+\\n)+") ?? "").RegexSearch("(?<=\\s+)[^\\n]+").Join("\n"));
				}
				if (LogMc.Contains("A potential solution has been determined, this may resolve your problem:"))
				{
					AppendReason(CrashReason.Fabric报错并给出解决方案, (LogMc.RegexSeek("(?<=A potential solution has been determined, this may resolve your problem:\\n)(\\s+ - [^\\n]+\\n)+") ?? "").RegexSearch("(?<=\\s+)[^\\n]+").Join("\n"));
				}
				if (LogMc.Contains("确定了一种可能的解决方法，这样做可能会解决你的问题："))
				{
					AppendReason(CrashReason.Fabric报错并给出解决方案, (LogMc.RegexSeek("(?<=确定了一种可能的解决方法，这样做可能会解决你的问题：\\n)(\\s+ - [^\\n]+\\n)+") ?? "").RegexSearch("(?<=\\s+)[^\\n]+").Join("\n"));
				}
				if (!flag && LogMc.Contains("due to errors, provided by "))
				{
					AppendReason(CrashReason.确定Mod导致游戏崩溃, TryAnalyzeModName((LogMc.RegexSeek("(?<=due to errors, provided by ')[^']+") ?? "").TrimEnd("\r\n ".ToCharArray())));
				}
			}
			if (LogCrash == null)
			{
				return;
			}
			obj(LogCrash);
			if (!LogCrash.Contains("Suspected Mod"))
			{
				return;
			}
			string text = LogCrash.Between("Suspected Mod", "Stacktrace");
			if (!text.StartsWithF("s: None"))
			{
				IEnumerable<string> enumerable = text.RegexSearch("(?<=\\n\\t[^(\\t]+\\()[^)\\n]+");
				if (enumerable.Any())
				{
					AppendReason(CrashReason.怀疑Mod导致游戏崩溃, TryAnalyzeModName(enumerable));
				}
			}
		}
		private void AnalyzeCrit3()
		{
			if (LogMc != null)
			{
				if (!LogMc.Contains("at net.") && !LogMc.Contains("INFO]") && LogHs == null && LogCrash == null && LogMc.Length < 100)
				{
					AppendReason(CrashReason.极短的程序输出, LogMc);
				}
				if (LogMc.Contains("Mod resolution failed"))
				{
					AppendReason(CrashReason.Mod加载器报错);
				}
				if (LogMc.Contains("Failed to create mod instance."))
				{
					AppendReason(CrashReason.Mod初始化失败, TryAnalyzeModName((LogMc.RegexSeek("(?<=Failed to create mod instance. ModID: )[^,]+") ?? LogMc.RegexSeek("(?<=Failed to create mod instance. ModId )[^\\n]+(?= for )") ?? "").TrimEnd('\r')));
				}
			}
			if (LogCrash != null)
			{
				if (LogCrash.Contains("\tBlock location: World: "))
				{
					AppendReason(CrashReason.特定方块导致崩溃, LogCrash.RegexSeek("(?<=\\tBlock: Block\\{)[^\\}]+") + " " + LogCrash.RegexSeek("(?<=\\tBlock location: World: )\\([^\\)]+\\)"));
				}
				if (LogCrash.Contains("\tEntity's Exact location: "))
				{
					AppendReason(CrashReason.特定实体导致崩溃, LogCrash.RegexSeek("(?<=\\tEntity Type: )[^\\n]+(?= \\()") + " (" + (LogCrash.RegexSeek("(?<=\\tEntity's Exact location: )[^\\n]+") ?? "").TrimEnd("\r\n".ToCharArray()) + ")");
				}
			}
		}
		private List<string> AnalyzeStackKeyword(string ErrorStack)
		{
			ErrorStack = "\n" + ErrorStack + "\n";
			List<string> list = new List<string>();
			list.AddRange(ErrorStack.RegexSearch("(?<=\\n[^{]+)[a-zA-Z_]+\\w+\\.[a-zA-Z_]+[\\w\\.]+(?=\\.[\\w\\.$]+\\.)"));
			list.AddRange(from s in ErrorStack.RegexSearch("(?<=at [^(]+?\\.\\w+\\$\\w+\\$)[\\w\\$]+?(?=\\$\\w+\\()")
				select s.Replace("$", "."));
			list = list.Distinct().ToList();
			List<string> list2 = new List<string>();
			checked
			{
				foreach (string item in list)
				{
					string[] array = new string[20]
					{
						"java", "sun", "javax", "jdk", "oolloo", "org.lwjgl", "com.sun", "net.minecraftforge", "paulscode.sound", "com.mojang",
						"net.minecraft", "cpw.mods", "com.google", "org.apache", "org.spongepowered", "net.fabricmc", "com.mumfrey", "com.electronwill.nightconfig", "it.unimi.dsi", "MojangTricksIntelDriversForPerformance_javaw"
					};
					int num = 0;
					while (true)
					{
						if (num < array.Length)
						{
							string prefix = array[num];
							if (item.StartsWithF(prefix))
							{
								break;
							}
							num++;
							continue;
						}
						list2.Add(item.Trim());
						break;
					}
				}
				list2 = list2.Distinct().ToList();
				Logger.Info($"找到 {list2.Count} 条可能的堆栈信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				if (!list2.Any())
				{
					return new List<string>();
				}
				foreach (string item2 in list2)
				{
					Logger.Info($"- {item2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				List<string> list3 = new List<string>();
				foreach (string item3 in list2)
				{
					string[] array2 = item3.Split(".");
					int num2 = Math.Min(3, array2.Count() - 1);
					for (int num3 = 0; num3 <= num2; num3++)
					{
						string text = array2[num3];
						if (text.Length > 2 && !text.StartsWithF("func_") && !new string[106]
						{
							"com", "org", "net", "asm", "fml", "mod", "jar", "sun", "lib", "map",
							"gui", "dev", "nio", "api", "dsi", "top", "mcp", "core", "init", "mods",
							"main", "file", "game", "load", "read", "done", "util", "tile", "item", "base",
							"fake", "oshi", "impl", "data", "pool", "task", "forge", "setup", "block", "model",
							"mixin", "event", "unimi", "netty", "world", "lwjgl", "fakes", "fabric", "gitlab", "common",
							"server", "config", "mixins", "compat", "loader", "launch", "script", "entity", "assist", "client",
							"plugin", "modapi", "mojang", "shader", "events", "github", "recipe", "render", "packet", "events",
							"preinit", "preload", "machine", "reflect", "channel", "general", "handler", "content", "systems", "modules",
							"service", "scripts", "network", "fastutil", "optifine", "internal", "platform", "override", "fabricmc", "neoforge",
							"external", "injection", "listeners", "scheduler", "minecraft", "universal", "multipart", "neoforged", "microsoft", "transformer",
							"transformers", "minecraftforge", "blockentity", "spongepowered", "electronwill", "concurrent"
						}.Contains(text.Lower()))
						{
							list3.Add(text.Trim());
						}
					}
				}
				list3 = list3.Distinct().ToList();
				Logger.Info($"从堆栈信息中找到 {list3.Count} 个可能的 Mod ID 关键词", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				if (list3.Any())
				{
					Logger.Info(string.Format("- {0}", list3.Join(", ")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				if (list3.Count > 10)
				{
					Logger.Info("关键词过多，考虑匹配出错，不纳入考虑", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					return new List<string>();
				}
				return list3;
			}
		}
		private List<string> AnalyzeModName(IEnumerable<string> Keywords)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			foreach (string Keyword in Keywords)
			{
				string[] array = Keyword.Split("(");
				foreach (string text in array)
				{
					list2.Add(text.Trim(" )".ToCharArray()));
				}
			}
			Keywords = list2;
			if (LogCrash != null && LogCrash.Contains("A detailed walkthrough of the error"))
			{
				string text2 = LogCrash.Replace("A detailed walkthrough of the error", "\u00a8");
				bool flag = text2.Contains("Fabric Mods");
				if (flag)
				{
					text2 = text2.Replace("Fabric Mods", "\u00a8");
					Logger.Info("崩溃报告中检测到 Fabric Mod 信息格式", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				text2 = text2.AfterLast("\u00a8");
				List<string> list3 = new List<string>();
				string[] array2 = text2.Split("\n");
				foreach (string text3 in array2)
				{
					if ((text3.ContainsIgnoreCase(".jar") && checked(text3.Length - text3.Replace(".jar", "").Length) == 4) || (flag && text3.StartsWithF("\t\t") && !text3.RegexCheck("\\t\\tfabric[\\w-]*: Fabric")))
					{
						list3.Add(text3);
					}
				}
				Logger.Info($"崩溃报告中找到 {list3.Count} 个可能的 Mod 项目行", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				List<string> list4 = new List<string>();
				foreach (string Keyword2 in Keywords)
				{
					foreach (string item in list3)
					{
						string text4 = item.Lower().Replace("_", "");
						if (text4.Contains(Keyword2.Lower().Replace("_", "")) && !text4.Contains("minecraft.jar") && !text4.Contains(" forge-") && !text4.Contains(" mixin-"))
						{
							list4.Add(item.Trim("\r\n".ToCharArray()));
							break;
						}
					}
				}
				list4 = list4.Distinct().ToList();
				Logger.Info($"崩溃报告中找到 {list4.Count} 个可能的崩溃 Mod 匹配行", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				foreach (string item2 in list4)
				{
					Logger.Info($"- {item2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				foreach (string item3 in list4)
				{
					string text5 = ((!flag) ? item3.RegexSeek("(?<=\\()[^\\t]+.jar(?=\\))|(?<=(\\t\\t)|(\\| ))[^\\t\\|]+.jar", RegexOptions.IgnoreCase) : item3.RegexSeek("(?<=: )[^\\n]+(?= [^\\n]+)"));
					if (text5 != null)
					{
						list.Add(text5);
					}
				}
			}
			if (LogMcDebug != null)
			{
				IEnumerable<string> enumerable = LogMcDebug.RegexSearch("(?<=valid mod file ).*", RegexOptions.Multiline);
				Logger.Info($"Debug 信息中找到 {enumerable.Count()} 个可能的 Mod 项目行", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				List<string> list5 = new List<string>();
				foreach (string Keyword3 in Keywords)
				{
					foreach (string item4 in enumerable)
					{
						if (item4.Contains($"{{{Keyword3}}}"))
						{
							list5.Add(item4);
						}
					}
				}
				list5 = list5.Distinct().ToList();
				Logger.Info($"Debug 信息中找到 {list5.Count} 个可能的崩溃 Mod 匹配行", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				foreach (string item5 in list5)
				{
					Logger.Info($"- {item5}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
				}
				foreach (string item6 in list5)
				{
					string text6 = item6.RegexSeek(".*(?= with)");
					if (text6 != null)
					{
						list.Add(text6);
					}
				}
			}
			list = list.Distinct().ToList();
			if (!list.Any())
			{
				return null;
			}
			Logger.Info($"找到 {list.Count} 个可能的崩溃 Mod 文件名", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			foreach (string item7 in list)
			{
				Logger.Info($"- {item7}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
			}
			return list;
		}
		private List<string> TryAnalyzeModName(string Keyword)
		{
			List<string> list = new List<string> { Keyword ?? "" };
			if (string.IsNullOrEmpty(Keyword))
			{
				return list;
			}
			return AnalyzeModName(list) ?? list;
		}
		private List<string> TryAnalyzeModName(IEnumerable<string> Keywords)
		{
			if (!Keywords.Any())
			{
				return Keywords.ToList();
			}
			return AnalyzeModName(Keywords) ?? Keywords.ToList();
		}
		public void Output(bool IsManualAnalyze, List<string> ExtraFiles = null)
		{
			ModMain.FrmMain.ShowWindowToTop();
			string analyzeResult = GetAnalyzeResult(IsManualAnalyze);
			string title = (IsManualAnalyze ? "错误报告分析结果" : "Minecraft 出现错误");
			string button = ((IsManualAnalyze || !DirectFile.HasValue) ? "" : "查看输出");
			string button2 = (IsManualAnalyze ? "" : "导出错误报告");
			VB_AnonymousDelegate_10 obj = ((IsManualAnalyze || !DirectFile.HasValue) ? null : ((VB_AnonymousDelegate_10)([SpecialName] () =>
			{
				if (FileUtils.Exists(DirectFile.Value.Key))
				{
					ModBase.StartProcess(DirectFile.Value.Key);
				}
				else
				{
					string text2 = ModBase.PathTemp + "Crash.txt";
					FileUtils.Write(text2, DirectFile.Value.Value.Join("\r\n"));
					ModBase.StartProcess(text2);
				}
			})));
			int num = ModMain.MyMsgBox(analyzeResult, title, "确定", button, button2, IsWarn: false, HighLight: true, ForceWait: false, null, (obj == null) ? null : new Action(obj.Invoke));
			if (num == 3)
			{
				_Closure___28_0 arg = default;
				_Closure___28_0 CS_8__locals6 = new _Closure___28_0(arg);
				CS_8__locals6._VB_Local_FileAddress = null;
				try
				{
					ModBase.RunInUiWait([SpecialName] () =>
					{
						CS_8__locals6._VB_Local_FileAddress = Dialogs.SaveFile("选择保存位置", "错误报告-" + DateTime.Now.ToString("G").Replace("/", "-").Replace(":", ".")
							.Replace(" ", "_") + ".zip", null, new (string, string)[1] { ("zip", "Minecraft 错误报告") });
					});
					if (CS_8__locals6._VB_Local_FileAddress == null)
					{
						return;
					}
					FileUtils.Delete(CS_8__locals6._VB_Local_FileAddress);
					ModMain.FeedbackInfo();
					Logger.Instance.Flush();
					if (ExtraFiles != null)
					{
						OutputFiles.AddRange(ExtraFiles);
					}
					foreach (string outputFile in OutputFiles)
					{
						string text = PathUtils.GetLastPart(outputFile);
						Encoding encoding = null;
						switch (text)
						{
						case "LatestLaunch.bat":
							text = "启动脚本.bat";
							break;
						case "Log1.txt":
							text = "PCL 启动器日志.txt";
							encoding = Encoding.UTF8;
							break;
						case "RawOutput.log":
							text = "游戏崩溃前的输出.txt";
							encoding = Encoding.UTF8;
							break;
						}
						if (FileUtils.Exists(outputFile))
						{
							string raw = FileUtils.ReadAsString(outputFile, encoding);
							raw = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(raw, Conversions.ToChar((Operators.CompareString(text, "启动脚本.bat", TextCompare: false) == 0) ? "F" : "*")), '*');
							FileUtils.Write(TempFolder + "Report\\" + text, raw, encoding);
							Logger.Info($"导出错误报告文件：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
						}
					}
					FileUtils.CreateZipFromDirectory(CS_8__locals6._VB_Local_FileAddress, TempFolder + "Report\\");
					DirectoryUtils.Delete(TempFolder + "Report\\");
					ModMain.Hint("错误报告已导出！", ModMain.HintType.Green);
					ModBase.OpenExplorer(CS_8__locals6._VB_Local_FileAddress);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "导出错误报告失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModCrash.vb");
					ProjectData.ClearProjectError();
					return;
				}
			}
			if (!IsManualAnalyze)
			{
				ModMain.Telemetry("Minecraft 崩溃", "Version", ((object)TargetInstance == null) ? "null" : TargetInstance.VersionDisplayName(), "Detail", ModMinecraft.FilterUserName(analyzeResult, '*'));
			}
		}
		private string GetAnalyzeResult(bool IsHandAnalyze)
		{
			if (!CrashReasons.Any())
			{
				if (IsHandAnalyze)
				{
					return "很抱歉，PCL 无法确定错误原因。";
				}
				return string.Format("很抱歉，你的游戏出现了一些问题……{0}如果要寻求帮助，请把错误报告文件发给对方，而不是发送这个窗口的照片或者截图。", "\r\n");
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<CrashReason, List<string>> crashReason in CrashReasons)
			{
				List<string> value = crashReason.Value;
				switch (crashReason.Key)
				{
				case CrashReason.Java虚拟机参数有误:
					list.Add("由于 Java 参数有误，导致游戏无法继续运行。\\n请检查高级选项中的 Java 虚拟机参数和游戏参数设置是否有误。");
					break;
				case CrashReason.Mod文件被解压:
					list.Add("由于 Mod 文件被解压了，导致游戏无法继续运行。\\n直接把整个 Mod 文件放进 Mod 文件夹中即可，若解压就会导致游戏出错。\\n\\n请删除 Mod 文件夹中已被解压的 Mod，然后再启动游戏。");
					break;
				case CrashReason.内存不足:
					list.Add("Minecraft 内存不足，导致其无法继续运行。\\n这很可能是因为电脑内存不足、游戏分配的内存不足，或是配置要求过高。\\n\\n你可以尝试在 更多 → 百宝箱 中选择 内存优化，然后再启动游戏。\\n如果还是不行，请在启动设置中增加为游戏分配的内存，并删除配置要求较高的材质、Mod、光影。\\n如果依然不奏效，请在开始游戏前尽量关闭其他软件，或者……换台电脑？\\h");
					break;
				case CrashReason.使用OpenJ9:
					list.Add("游戏因为使用 OpenJ9 而崩溃了。\\n请在版本设置的 Java 选项中改用非 OpenJ9 的 Java，然后再启动游戏。");
					break;
				case CrashReason.使用JDK:
					list.Add("游戏似乎因为使用 JDK，或 Java 版本过高而崩溃了。\\n请在版本设置的 Java 选项中改用 Java 8，然后再启动游戏。\\n如果你没有安装 Java 8，可以从网络中下载、安装一个。");
					break;
				case CrashReason.Java版本过高:
					list.Add("游戏似乎因为你所使用的 Java 版本过高而崩溃了。\\n请在版本设置的 Java 选项中改用较低版本的 Java，然后再启动游戏。\\n如果没有，可以从网络中下载、安装一个。");
					break;
				case CrashReason.Java版本不兼容:
					list.Add("游戏不兼容你当前使用的 Java。\\n请在版本设置的 Java 选项中改用合适版本的 Java，然后再启动游戏。\\n如果你没有安装任何 Mod 并且因此崩溃，请向 PCL 提交反馈。");
					break;
				case CrashReason.Mod名称包含特殊字符:
					list.Add("由于有 Mod 的名称包含特殊字符，导致游戏崩溃。\\n请尝试修改 Mod 文件名，让它只包含英文字母、数字、减号（-）、下划线（_）和小数点，然后再启动游戏。");
					break;
				case CrashReason.MixinBootstrap缺失:
					list.Add("由于缺失 MixinBootstrap，导致游戏崩溃。\\n请尝试安装 MixinBootstrap。若安装后依然崩溃，可以尝试在文件名前添加英文感叹号。");
					break;
				case CrashReason.使用32位Java导致JVM无法分配足够多的内存:
					if (Environment.Is64BitOperatingSystem)
					{
						list.Add("你似乎正在使用 32 位 Java，这会导致 Minecraft 无法使用所需的内存，进而造成崩溃。\\n\\n请在启动设置的 Java 选择一项中改用 64 位的 Java 再启动游戏，然后再启动游戏。\\n如果你没有安装 64 位的 Java，你可以从网络中下载、安装一个。");
					}
					else
					{
						list.Add("你正在使用 32 位的操作系统，这会导致 Minecraft 无法使用所需的内存，进而造成崩溃。\\n\\n你或许只能重装 64 位的操作系统来解决此问题。\\n如果你的电脑内存在 2GB 以内，那或许只能换台电脑了……\\h");
					}
					break;
				case CrashReason.Mod缺少前置或MC版本错误:
					if (value.Any())
					{
						list.Add("由于未安装正确的前置 Mod，导致游戏退出。\\n缺失的依赖项：\\n - " + value.Join("\\n - ") + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("由于未安装正确的前置 Mod，导致游戏退出。\\n请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.堆栈分析发现关键字:
					if (value.IsSingle())
					{
						list.Add("你的游戏遇到了一些问题，PCL 为此找到了一个可疑的关键词：" + value.First() + "。\\n\\n如果你知道某个关键词对应的 Mod，那么有可能就是它引起的错误，你也可以查看错误报告获取详情。\\h");
					}
					else
					{
						list.Add("你的游戏遇到了一些问题，PCL 为此找到了以下可疑的关键词：\\n - " + value.Join(", ") + "\\n\\n如果你知道某个关键词对应的 Mod，那么有可能就是它引起的错误，你也可以查看错误报告获取详情。\\h");
					}
					break;
				case CrashReason.怀疑Mod导致游戏崩溃:
				case CrashReason.堆栈分析发现Mod名称:
					if (value.IsSingle())
					{
						list.Add("PCL 怀疑名为 " + value.First() + " 的 Mod 导致了游戏出错，但不能完全确定。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					else
					{
						list.Add("PCL 怀疑以下 Mod 导致了游戏出错，但不能完全确定：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					break;
				case CrashReason.确定Mod导致游戏崩溃:
					if (value.IsSingle())
					{
						list.Add("名为 " + value.First() + " 的 Mod 导致了游戏出错。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					else
					{
						list.Add("以下 Mod 导致了游戏出错：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					break;
				case CrashReason.ModMixin失败:
					if (value.Count == 0)
					{
						list.Add("部分 Mod 注入失败，导致游戏出错。\\n这一般代表着部分 Mod 与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试逐步禁用 Mod，然后观察游戏是否还会崩溃，以此定位导致崩溃的 Mod。\\n\\e\\h");
					}
					else if (value.IsSingle())
					{
						list.Add("名为 " + value.First() + " 的 Mod 注入失败，导致游戏出错。\\n这一般代表着它与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					else
					{
						list.Add("以下 Mod 导致了游戏出错：\\n - " + value.Join("\\n - ") + "\\n这一般代表着它们与其他 Mod 或当前环境不兼容，或是它存在 Bug。\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					break;
				case CrashReason.Mod配置文件导致游戏崩溃:
					if (value[1] == null)
					{
						list.Add("名为 " + value.First() + " 的 Mod 导致了游戏出错。\\n\\e\\h");
						break;
					}
					list.Add("名为 " + value.First() + " 的 Mod 导致了游戏出错：\\n其配置文件 " + value[1] + " 存在异常，无法读取。");
					break;
				case CrashReason.Mod初始化失败:
					if (value.IsSingle())
					{
						list.Add("名为 " + value.First() + " 的 Mod 初始化失败，导致游戏无法继续加载。\\n你可以尝试禁用此 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					else
					{
						list.Add("以下 Mod 初始化失败，导致游戏出错：\\n - " + value.Join("\\n - ") + "\\n\\n你可以尝试依次禁用上述 Mod，然后观察游戏是否还会崩溃。\\n\\e\\h");
					}
					break;
				case CrashReason.特定方块导致崩溃:
					if (value.IsSingle())
					{
						list.Add("游戏似乎因为方块 " + value.First() + " 出现了问题。\\n\\n你可以创建一个新世界，并观察游戏的运行情况：\\n - 若正常运行，则是该方块导致出错，你或许需要使用一些方式删除此方块。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
					}
					else
					{
						list.Add("游戏似乎因为世界中的某些方块出现了问题。\\n\\n你可以创建一个新世界，并观察游戏的运行情况：\\n - 若正常运行，则是某些方块导致出错，你或许需要删除该世界。\\n - 若仍然出错，问题就可能来自其他原因……\\h");
					}
					break;
				case CrashReason.Mod重复安装:
					if (value.Count >= 2)
					{
						list.Add("你重复安装了多个相同的 Mod：\\n - " + value.Join("\\n - ") + "\\n\\n每个 Mod 只能出现一次，请删除重复的 Mod，然后再启动游戏。");
					}
					else
					{
						list.Add("你可能重复安装了多个相同的 Mod，导致游戏出错。\\n\\n每个 Mod 只能出现一次，请删除重复的 Mod，然后再启动游戏。\\e\\h");
					}
					break;
				case CrashReason.特定实体导致崩溃:
					if (value.IsSingle())
					{
						if (value.First().Contains("minecraft:player"))
						{
							if (value.First().Contains(" "))
							{
								list.Add(string.Format("游戏因为位于 {0} 的玩家实体导致了崩溃。\\h", value.First().AfterFirst(" ")));
							}
							else
							{
								list.Add("游戏因为玩家实体导致了崩溃。\\h");
							}
						}
						else
						{
							list.Add($"游戏因为实体 {value.First()} 导致了崩溃。\\h");
						}
					}
					else
					{
						list.Add($"游戏因为下列实体导致了崩溃：{value.Join('、')}\\h");
					}
					break;
				case CrashReason.OptiFine与Forge不兼容:
					list.Add("由于 OptiFine 与当前版本的 Forge 不兼容，导致了游戏崩溃。\\n\\n请前往 OptiFine 官网（https://optifine.net/downloads）查看 OptiFine 所兼容的 Forge 版本，并严格按照对应版本重新安装游戏。");
					break;
				case CrashReason.ShadersMod与OptiFine同时安装:
					list.Add("无需同时安装 OptiFine 和 Shaders Mod，OptiFine 已经集成了 Shaders Mod 的功能。\\n在删除 Shaders Mod 后，游戏即可正常运行。");
					break;
				case CrashReason.低版本Forge与高版本Java不兼容:
					list.Add("由于低版本 Forge 与当前 Java 不兼容，导致了游戏崩溃。\\n\\n请尝试以下解决方案：\\n - 更新 Forge 到 36.2.26 或更高版本\\n - 换用版本低于 1.8.0.320 的 Java");
					break;
				case CrashReason.版本Json中存在多个Forge:
					list.Add("可能由于其他启动器修改了 Forge 版本，当前版本的文件存在异常，导致了游戏崩溃。\\n请尝试重新全新安装 Forge，而非使用其他启动器修改 Forge 版本。");
					break;
				case CrashReason.玩家手动触发调试崩溃:
					list.Add("* 事实上，你的游戏没有任何问题，这是你自己触发的崩溃。\\n* 你难道没有更重要的事要做吗？");
					break;
				case CrashReason.Mod需要Java11:
					list.Add("你所安装的部分 Mod 似乎需要使用 Java 11 启动。\\n请在启动设置的 Java 选择一项中改用 Java 11，然后再启动游戏。\\n如果你没有安装 Java 11，你可以从网络中下载、安装一个。");
					break;
				case CrashReason.极短的程序输出:
					list.Add($"程序返回了以下信息：\\n{value.First()}\\n\\h");
					break;
				case CrashReason.OptiFine导致无法加载世界:
					list.Add("你所使用的 OptiFine 可能导致了你的游戏出现问题。\\n\\n该问题只在特定 OptiFine 版本中出现，你可以尝试更换 OptiFine 的版本。\\h");
					break;
				case CrashReason.显卡不支持OpenGL:
				case CrashReason.显卡驱动不支持导致无法设置像素格式:
				case CrashReason.Intel驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
				case CrashReason.AMD驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
				case CrashReason.Nvidia驱动不兼容导致EXCEPTION_ACCESS_VIOLATION:
					if (LogAll.Contains("hd graphics "))
					{
						list.Add("你的显卡驱动存在问题，或未使用独立显卡，导致游戏无法正常运行。\\n\\n如果你的电脑存在独立显卡，请使用独立显卡而非 Intel 核显启动 PCL 与 Minecraft。\\n如果问题依然存在，请尝试升级你的显卡驱动到最新版本，或回退到出厂版本。\\n如果还是不行，还可以尝试使用 8.0.51 或更低版本的 Java。\\h");
					}
					else
					{
						list.Add("你的显卡驱动存在问题，导致游戏无法正常运行。\\n\\n请尝试升级你的显卡驱动到最新版本，或回退到出厂版本，然后再启动游戏。\\n如果还是不行，可以尝试使用 8.0.51 或更低版本的 Java。\\n如果问题依然存在，那么你可能需要换个更好的显卡……\\h");
					}
					break;
				case CrashReason.材质过大或显卡配置不足:
					list.Add("你所使用的材质分辨率过高，或显卡配置不足，导致游戏无法继续运行。\\n\\n如果你正在使用高清材质，请将它移除。\\n如果你没有使用材质，那么你可能需要更新显卡驱动，或者换个更好的显卡……\\h");
					break;
				case CrashReason.NightConfig的Bug:
					list.Add("由于 Night Config 存在问题，导致了游戏崩溃。\\n你可以尝试安装 Night Config Fixes 模组，这或许能解决此问题。\\h");
					break;
				case CrashReason.光影或资源包导致OpenGL1282错误:
					list.Add("你所使用的光影或材质导致游戏出现了一些问题……\\n\\n请尝试删除你所添加的这些额外资源。\\h");
					break;
				case CrashReason.Mod过多导致超出ID限制:
					list.Add("你所安装的 Mod 过多，超出了游戏的 ID 限制，导致了游戏崩溃。\\n请尝试安装 JEID 等修复 Mod，或删除部分大型 Mod。");
					break;
				case CrashReason.文件或内容校验失败:
					list.Add("部分文件或内容校验失败，导致游戏出现了问题。\\n\\n请尝试删除游戏（包括 Mod）并重新下载，或尝试在重新下载时使用 VPN。\\h");
					break;
				case CrashReason.Forge安装不完整:
					list.Add("由于安装的 Forge 文件丢失，导致游戏无法正常运行。\\n请重新安装一次相同版本的 Forge，然后再启动游戏。\\n在打包游戏时删除 libraries 文件夹可能导致此错误。\\h");
					break;
				case CrashReason.Fabric报错:
					if (value.IsSingle())
					{
						list.Add("Fabric 提供了以下错误信息：\\n" + value.First() + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("Fabric 可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.Mod互不兼容:
					if (value.IsSingle())
					{
						list.Add("你所安装的 Mod 不兼容：\\n" + value.First() + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("你所安装的 Mod 不兼容，Mod 加载器可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.Mod加载器报错:
					if (value.IsSingle())
					{
						list.Add("Mod 加载器提供了以下错误信息：\\n" + value.First() + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("Mod 加载器可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.Fabric报错并给出解决方案:
					if (value.IsSingle())
					{
						list.Add("Fabric 提供了以下解决方案：\\n" + value.First() + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("Fabric 可能已经提供了解决方案，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.Forge报错:
					if (value.IsSingle())
					{
						list.Add("Forge 提供了以下错误信息：\\n" + value.First() + "\\n\\n请根据上述信息进行对应处理，如果看不懂英文可以使用翻译软件。");
					}
					else
					{
						list.Add("Forge 可能已经提供了错误信息，请根据错误报告中的日志信息进行对应处理，如果看不懂英文可以使用翻译软件。\\h");
					}
					break;
				case CrashReason.没有可用的分析文件:
					list.Add("你的游戏出现了一些问题，但 PCL 未能找到相关记录文件，因此无法进行分析。\\h");
					break;
				default:
					list.Add("PCL 获取到了没有详细信息的错误原因（" + Conversions.ToString((int)CrashReasons.First().Key) + "），请向 PCL 作者提交反馈以获取详情。\\h");
					break;
				}
			}
			return list.Join("\\n\\n此外，").Replace("\\n", "\r\n").Replace("\\h", "")
				.Replace("\\e", IsHandAnalyze ? "" : "\r\n你可以查看错误报告了解错误具体是如何发生的。")
				.ReplaceLineEndings("\r\n")
				.Trim("\r\n".ToCharArray()) + ((!list.Any([SpecialName] (string r) => r.EndsWithF("\\h")) || IsHandAnalyze) ? "" : ("\r\n如果要寻求帮助，请把错误报告文件发给对方，而不是发送这个窗口的照片或者截图。" + ((PageSetupSystem.IsLauncherNewest() ?? true) ? "" : "\r\n\r\n此外，你正在使用老版本 PCL，更新 PCL 或许也能解决这个问题。\r\n你可以点击 [设置 → 其他 → 启动器 → 检查更新] 更新 PCL。")));
		}
	}
}
