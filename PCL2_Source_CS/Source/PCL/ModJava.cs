// PCL

namespace PCL
{
	[StandardModule]
	public static class ModJava
	{
		private static RedoableWorker _JavaListRefreshWorker;
		public static RedoableWorker JavaListRefreshWorker
		{
			get
			{
				return _JavaListRefreshWorker;
			}
			set
			{
				Action value2 = UpdateJavaLists;
				Action value3 = UpdateJavaLists;
				if (_JavaListRefreshWorker != null)
				{
					_JavaListRefreshWorker.Started -= value2;
					_JavaListRefreshWorker.Stopped -= value3;
				}
				_JavaListRefreshWorker = value;
				if (_JavaListRefreshWorker != null)
				{
					_JavaListRefreshWorker.Started += value2;
					_JavaListRefreshWorker.Stopped += value3;
				}
			}
		}
		internal class JavaDownloadInfo
		{
			public string ComponentName { get; set; }
			public JObject Java { get; set; }
			public Version JavaVersion { get; set; }
		}
		[CompilerGenerated]
		internal sealed class _Closure___11_0
		{
			public CancellationToken _VB_Local_c;
			public _Closure___11_0(_Closure___11_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_c = arg0._VB_Local_c;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___11_1
		{
			public ValueRange<Version> _VB_Local_Range;
			public string _VB_Local_RecommendedComponent;
			public _Closure___11_0 _VB_NonLocal__VB_Closure_2;
			public _Closure___11_1(_Closure___11_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Range = arg0._VB_Local_Range;
					_VB_Local_RecommendedComponent = arg0._VB_Local_RecommendedComponent;
				}
			}
			[SpecialName]
			internal Java _Lambda___0()
			{
				JavaListRefreshWorker.WaitIfRunning();
				foreach (Java item in Configs.JavaList.Get())
				{
					if (!item.CheckAsync(_VB_NonLocal__VB_Closure_2._VB_Local_c).Run())
					{
						Logger.Warn($"Java 检查失败，将尝试列表中的下一个 Java：{item}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					}
					else if ((object)item.Version != null && _VB_Local_Range.Contains(item.Version))
					{
						Logger.Info($"已选定 Java：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						return item;
					}
				}
				return null;
			}
			[SpecialName]
			internal bool _Lambda___2(JavaDownloadInfo Comp)
			{
				return Operators.CompareString(Comp.Java["version"]["name"].ToString(), _VB_Local_RecommendedComponent, TextCompare: false) == 0;
			}
			[SpecialName]
			internal bool _Lambda___3(JavaDownloadInfo Comp)
			{
				return _VB_Local_Range.Contains(Comp.JavaVersion);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___16_0
		{
			public string _VB_Local_BaseDir;
			public _Closure___16_0(_Closure___16_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_BaseDir = arg0._VB_Local_BaseDir;
				}
			}
			[SpecialName]
			internal void _Lambda___R1(ModLoader.LoaderBase a0)
			{
				_Lambda___0((ModLoader.LoaderCombo<int>)a0);
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderCombo<int> RawLoader)
			{
				if (RawLoader.State == ModBase.LoadState.Failed || RawLoader.State == ModBase.LoadState.Canceled)
				{
					Logger.Warn($"由于 Java 下载未完成，清理未下载完成的 Java 文件夹：{_VB_Local_BaseDir}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					DirectoryUtils.Delete(_VB_Local_BaseDir);
				}
			}
		}
		private static readonly object JavaLock;
		[SpecialName]
		private static RedoableWorker<List<JavaDownloadInfo>> _STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init;

		static ModJava()
		{
			JavaListRefreshWorker = new RedoableWorker([SpecialName] (CancellationToken? c, ProgressProvider p) =>
			{
				JavaUtils.RefreshListAsync(c.Value, p).Run();
			}, "JavaListRefreshWorker");
			JavaLock = RuntimeHelpers.GetObjectValue(new object());
		}
		public static void JavaInit()
		{
			try
			{
				if (Configs.JavaConfigVersion.Get() == 0)
				{
					try
					{
						Logger.Warn("正在从老版本迁移 Java 列表", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						List<Java> javas = (from JavaObject in Settings.Get<string>("LaunchArgumentJavaAll").DeserializeJson<JArray>()
							select new Java(JavaObject["Path"].ToString())).ToList();
						javas = JavaUtils.SortAsync(javas).Run();
						Logger.Info($"已从老版本设置中发现 {javas.Count} 个 Java", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						string text = Settings.Get<string>("LaunchArgumentJavaSelect");
						string value = (text.StartsWithF("{") ? text.DeserializeJson<JObject>()["Path"].ToString() : "");
						Java java = javas.FirstOrDefault([SpecialName] (Java j) => j.Folder.Equals(value, StringComparison.OrdinalIgnoreCase));
						if (java != null)
						{
							javas.Remove(java);
							javas.Insert(0, java);
							Logger.Info($"已将老版本设置中强制选择的 Java 置顶：{java}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						}
						Configs.JavaList.Set(new ConcurrentList<Java>(javas));
						Configs.JavaConfigVersion.Set(1);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "从老版本 PCL 迁移 Java 列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						ProjectData.ClearProjectError();
					}
				}
				ConcurrentList<Java> concurrentList = Configs.JavaList.Get();
				if (concurrentList.Any())
				{
					Logger.Info(string.Format("共有 {0} 个可用的 Java：{1}- {2}", concurrentList.Count, "\r\n", concurrentList.Select([SpecialName] (Java j) => j.ToString()).Join("\r\n- ")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
				}
				else
				{
					Logger.Warn("初始化未找到可用的 Java，将自动触发搜索", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					JavaListRefreshWorker.Start();
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "初始化 Java 列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
				Configs.JavaList.Reset();
				ProjectData.ClearProjectError();
			}
		}
		public static void UpdateJavaLists()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				ModMain.FrmSetupLaunch?.UpdateJavaList();
				ModMain.FrmInstanceSetup?.UpdateJavaList();
			});
		}
		public static void ManuallyImportJava()
		{
			string text = Dialogs.SelectFile("选择 java.exe（可能在 bin 文件夹中）", multiselect: false, null, "java.exe|java.exe").FirstOrDefault();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Java java = new Java(PathUtils.RemoveLastPart(text));
			if (!java.CheckAsync().Run())
			{
				ModMain.Hint("该 Java 存在异常，无法使用", ModMain.HintType.Red);
				return;
			}
			Configs.JavaRemovedList.Edit([SpecialName] (ref List<string> List) =>
			{
				List.RemoveIf([SpecialName] (string f) => f.Equals(java.Folder, StringComparison.OrdinalIgnoreCase));
			});
			Configs.JavaList.Edit([SpecialName] (ref ConcurrentList<Java> List) =>
			{
				List.Remove(java);
				List.Insert(0, java);
			});
			ModMain.Hint($"已将 Java 加入列表：{text}", ModMain.HintType.Green);
			UpdateJavaLists();
		}
		public static void ManuallyRemoveJava(Java JavaEntry)
		{
			Configs.JavaList.Edit([SpecialName] (ref ConcurrentList<Java> List) =>
			{
				List.Remove(JavaEntry);
			});
			Configs.JavaRemovedList.Edit([SpecialName] (List<string> List) => List.Append(JavaEntry.Folder).DistinctBy([SpecialName] (string s) => s.Lower()).ToList());
			ModMain.Hint($"已将 Java 移除出列表：{JavaEntry}", ModMain.HintType.Green);
			UpdateJavaLists();
			PageInstanceLeft.ReloadCurrentJava();
		}
		public static void ManuallySearchJava(CancellationToken CancellationToken = default(CancellationToken))
		{
			ModBase.RunInThread([SpecialName] () =>
			{
				Configs.JavaList.Get().ForAll([SpecialName] (Java java) =>
				{
					java.InvalidateChecked();
				});
				JavaListRefreshWorker.Start(CancellationToken);
				JavaListRefreshWorker.WaitIfRunningAsync(-1, CancellationToken).Run();
				if (Configs.JavaList.Get().Any())
				{
					ModMain.Hint("已找到 " + Conversions.ToString(Configs.JavaList.Get().Count) + " 个 Java！", ModMain.HintType.Green);
				}
				else
				{
					ModMain.Hint("未找到可用的 Java！", ModMain.HintType.Red);
				}
			});
		}
		public static (ValueRange<Version> Range, string RecommendedComponent) GetJavaRequirement(ModMinecraft.McInstance Instance)
		{
			if (Settings.Get<int>("VersionArgumentJavaV2", Instance) == 1)
			{
				try
				{
					ValueRange<Version> item = ValueRange<Version>.FromString(Settings.Get<string>("VersionArgumentJavaRange", Instance), [SpecialName] (string s) => StringUtils.ParseVersionWithDefaults(s));
					return (Range: item, RecommendedComponent: null);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(string.Format("版本设置中指定的 Java 版本范围有误（{0}），这可能是因为首次修改此设置，尚未初始化", Settings.Get<string>("VersionArgumentJavaRange", Instance)), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					ProjectData.ClearProjectError();
				}
			}
			ValueRange<Version> valueRange = ValueRange<Version>.All();
			VB_AnonymousDelegate_4<ValueRange<Version>> obj = [SpecialName] (ValueRange<Version> Constraint) =>
			{
				valueRange = valueRange?.Intersect(Constraint);
				if (valueRange.IsEmpty())
				{
					Logger.Warn($"Java 版本要求冲突，若启动失败请在版本设置手动指定 Java：当前追加的要求为 {Constraint}，游戏版本为 {Instance.VersionDisplayName()}", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					valueRange = Constraint;
				}
			};
			if ((!Instance.Version.Vaild && DateTime.Compare(Instance.ReleaseTime, new DateTime(2024, 4, 2)) >= 0) || (Instance.Version.Vaild && Instance.Version.Vanilla >= new Version(20, 0, 5)))
			{
				obj(ValueRange<Version>.AtLeast(new Version(21, 0)));
			}
			else if ((!Instance.Version.Vaild && DateTime.Compare(Instance.ReleaseTime, new DateTime(2021, 11, 16)) >= 0) || (Instance.Version.Vaild && Instance.Version.Vanilla.Major >= 18))
			{
				obj(ValueRange<Version>.AtLeast(new Version(17, 0)));
			}
			else if ((!Instance.Version.Vaild && DateTime.Compare(Instance.ReleaseTime, new DateTime(2021, 5, 11)) >= 0) || (Instance.Version.Vaild && Instance.Version.Vanilla.Major >= 17))
			{
				obj(ValueRange<Version>.AtLeast(new Version(16, 0)));
			}
			else if (Instance.ReleaseTime.Year >= 2017)
			{
				obj(ValueRange<Version>.AtLeast(new Version(8, 0)));
			}
			else if (DateTime.Compare(Instance.ReleaseTime, new DateTime(2013, 5, 1)) <= 0 && Instance.ReleaseTime.Year >= 2001)
			{
				obj(ValueRange<Version>.LessThan(new Version(9, 0)));
			}
			string text = null;
			int? num2;
			int? num = (num2 = Instance.JsonObject?["javaVersion"]?["majorVersion"]?.ToObject<int>());
			int num3 = (num.HasValue ? num2.GetValueOrDefault() : (Instance.JsonVersion?["java_version"]?.ToObject<int>()).GetValueOrDefault());
			if (num3 >= 22)
			{
				Logger.Info("Mojang 要求至少使用 Java " + Conversions.ToString(num3), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
				obj(ValueRange<Version>.AtLeast(new Version(num3, 0)));
				text = Instance.JsonObject?["javaVersion"]?["component"]?.ToString() ?? Instance.JsonVersion?["java_component"]?.ToString();
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					text = null;
				}
			}
			if (Instance.Version.HasOptiFine && Instance.Version.Vaild)
			{
				if (Instance.Version.Vanilla.Major < 7)
				{
					obj(ValueRange<Version>.LessThan(new Version(9, 0)));
				}
				else if (Instance.Version.Vanilla.Major >= 8 && Instance.Version.Vanilla.Major < 12)
				{
					obj(ValueRange<Version>.ClosedOpen(new Version(8, 0), new Version(9, 0)));
				}
				else if (Instance.Version.Vanilla.Major == 12)
				{
					obj(ValueRange<Version>.LessThan(new Version(9, 0)));
				}
			}
			if (Instance.Version.HasForge)
			{
				if (Instance.Version.Vanilla >= new Version(6, 0, 1) && Instance.Version.Vanilla <= new Version(7, 0, 2))
				{
					obj(ValueRange<Version>.ClosedOpen(new Version(7, 0), new Version(8, 0)));
				}
				else if (Instance.Version.Vanilla.Major <= 12 || !Instance.Version.Vaild)
				{
					obj(ValueRange<Version>.LessThan(new Version(9, 0)));
				}
				else if (Instance.Version.Vanilla.Major <= 14)
				{
					obj(ValueRange<Version>.ClosedOpen(new Version(8, 0), new Version(11, 0)));
				}
				else if (Instance.Version.Vanilla.Major == 15)
				{
					obj(ValueRange<Version>.ClosedOpen(new Version(8, 0), new Version(16, 0)));
				}
				else if (ModMinecraft.CompareVersionGE(Instance.Version.Forge, "34.0.0") && ModMinecraft.CompareVersionGE("36.2.25", Instance.Version.Forge))
				{
					obj(ValueRange<Version>.AtMost(new Version(8, 0, 320)));
				}
				else if (ModMinecraft.CompareVersionGE(Instance.Version.Forge, "36.2.26") && ModMinecraft.CompareVersionGE("36.999999.999999", Instance.Version.Forge))
				{
					obj(ValueRange<Version>.LessThan(new Version(24, 0)));
				}
				else if (ModMinecraft.CompareVersionGE(Instance.Version.Forge, "37.0.0") && ModMinecraft.CompareVersionGE("37.0.79", Instance.Version.Forge))
				{
					obj(ValueRange<Version>.LessThan(new Version(17, 0)));
				}
				else if (Instance.Version.Vanilla.Major == 18 && Instance.Version.HasOptiFine)
				{
					obj(ValueRange<Version>.LessThan(new Version(19, 0)));
				}
				else if (ModMinecraft.CompareVersionGE(Instance.Version.Forge, "45.0.21") && ModMinecraft.CompareVersionGE("45.0.65", Instance.Version.Forge))
				{
					obj(ValueRange<Version>.LessThan(new Version(20, 0)));
				}
				else if (ModMinecraft.CompareVersionGE(Instance.Version.Forge, "45.0.66") && ModMinecraft.CompareVersionGE("47.4.8", Instance.Version.Forge))
				{
					obj(ValueRange<Version>.LessThan(new Version(22, 0)));
				}
			}
			if (Instance.Version.HasNeoForge && (Instance.Version.Vanilla == new Version(20, 0, 1) || (ModMinecraft.CompareVersionGE("20.2.62-beta", Instance.Version.NeoForge) && !Instance.Version.NeoForge.Contains("25w14craftmine"))))
			{
				obj(ValueRange<Version>.LessThan(new Version(22, 0)));
			}
			if (Instance.Version.HasFabric && Instance.Version.Vaild)
			{
				if (Instance.Version.Vanilla.Major >= 15 && Instance.Version.Vanilla.Major <= 16)
				{
					obj(ValueRange<Version>.AtLeast(new Version(8, 0)));
				}
				else if (Instance.Version.Vanilla.Major >= 18)
				{
					obj(ValueRange<Version>.AtLeast(new Version(17, 0)));
				}
			}
			if (Instance.Version.HasLiteLoader && Instance.Version.Vaild)
			{
				obj(ValueRange<Version>.LessThan(new Version(9, 0)));
			}
			if (Settings.Get<ModLaunch.McLoginType>("LoginType") == ModLaunch.McLoginType.Nide)
			{
				obj(ValueRange<Version>.AtLeast(new Version(8, 0, 141)));
			}
			return (Range: valueRange, RecommendedComponent: text);
		}
		public static Java SelectOrDownloadJava(OneOf<ValueRange<Version>, ModMinecraft.McInstance> Target, bool TryFixOrHint, CancellationToken c = default(CancellationToken), ProgressProvider p = null)
		{
			_Closure___11_0 obj = new _Closure___11_0(default(_Closure___11_0));
			obj._VB_Local_c = c;
			checked
			{
				Java result;
				try
				{
					if (Target.Is<ModMinecraft.McInstance>())
					{
						MigrateInstanceJavaSettings(Target.As<ModMinecraft.McInstance>(), obj._VB_Local_c, p?.SplitTo(0.2));
					}
					ModMinecraft.McInstance mcInstance = (Target.Is<ModMinecraft.McInstance>() ? Target.As<ModMinecraft.McInstance>() : null);
					switch (((object)mcInstance != null) ? Settings.Get<int>("VersionArgumentJavaV2", mcInstance) : 0)
					{
					case 2:
					{
						Logger.Info($"版本设置要求使用版本文件夹中的 Java：{mcInstance.PathVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						Java java2 = JavaUtils.SearchFoldersAsync(includeSubDirectories: true, new string[1] { mcInstance.PathVersion }, obj._VB_Local_c, p?.SplitTo(1.0)).Run().FirstOrDefault();
						if (java2 != null)
						{
							Logger.Info($"已发现版本文件夹中的 Java：{java2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							result = java2;
							break;
						}
						if (TryFixOrHint)
						{
							SwitchToInstanceSetup(mcInstance);
							ModMain.MyMsgBox(string.Format("该版本的版本设置选择了 {0}使用版本文件夹中的 Java{1}，但版本文件夹里没能找到 Java。{2}请先修改启动选项中的 Java 设置，或在版本文件夹中放一个 Java，然后再试。", ModBase.vbLQ, ModBase.vbRQ, "\r\n"), "未找到 Java", "确定", "", "", IsWarn: true, HighLight: true, ForceWait: true);
						}
						result = null;
						break;
					}
					case 3:
					{
						Java java3 = Configs.JavaForced.Get(mcInstance.Config);
						Logger.Info($"版本设置中强制指定的 Java：{mcInstance.PathVersion} → {java3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						if (java3 == null)
						{
							if (TryFixOrHint)
							{
								SwitchToInstanceSetup(mcInstance);
								ModMain.MyMsgBox(string.Format("该版本的版本设置选择了 {0}使用指定的 Java{1}，但还没有指定任何 Java。{2}请先选择你想使用的 Java，然后再试。", ModBase.vbLQ, ModBase.vbRQ, "\r\n"), "未选择 Java", "确定", "", "", IsWarn: true, HighLight: true, ForceWait: true);
							}
							p?.Skip();
							result = null;
						}
						else if (!Configs.JavaList.Get().Contains(java3))
						{
							Configs.JavaForced.Reset(mcInstance.Config);
							UpdateJavaLists();
							if (TryFixOrHint)
							{
								SwitchToInstanceSetup(mcInstance);
								Logger.Error("版本设置中指定的 Java 不在 Java 列表中，请重新选择一个 Java！", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							}
							p?.Skip();
							result = null;
						}
						else if (!java3.CheckAsync(obj._VB_Local_c).Run())
						{
							Configs.JavaForced.Reset(mcInstance.Config);
							UpdateJavaLists();
							if (TryFixOrHint)
							{
								SwitchToInstanceSetup(mcInstance);
								Logger.Error("版本设置中指定的 Java 已无法使用，请重新选择一个 Java，然后再试。", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							}
							else
							{
								Logger.Error("版本设置中指定的 Java 已无法使用，请重新选择一个 Java！", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							}
							p?.Finish();
							result = null;
						}
						else
						{
							p?.Finish();
							result = java3;
						}
						break;
					}
					default:
					{
						_Closure___11_1 arg = default;
						_Closure___11_1 CS_8__locals41 = new _Closure___11_1(arg);
						CS_8__locals41._VB_NonLocal__VB_Closure_2 = obj;
						CS_8__locals41._VB_Local_Range = (Target.Is<ModMinecraft.McInstance>() ? GetJavaRequirement(mcInstance).Range : Target.As<ValueRange<Version>>());
						CS_8__locals41._VB_Local_RecommendedComponent = (Target.Is<ModMinecraft.McInstance>() ? GetJavaRequirement(mcInstance).RecommendedComponent : null);
						Logger.Info($"自动选择以下范围的 Java：{CS_8__locals41._VB_Local_Range} {CS_8__locals41._VB_Local_RecommendedComponent}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						if (CS_8__locals41._VB_Local_Range.IsEmpty())
						{
							throw new Exception("Java 版本需求存在冲突，导致没有任何可能适配的 Java，请查看 PCL 的日志了解详细信息！");
						}
						VB_AnonymousDelegate_5<Java> obj2 = [SpecialName] () =>
						{
							JavaListRefreshWorker.WaitIfRunning();
							foreach (Java item in Configs.JavaList.Get())
							{
								if (!item.CheckAsync(CS_8__locals41._VB_NonLocal__VB_Closure_2._VB_Local_c).Run())
								{
									Logger.Warn($"Java 检查失败，将尝试列表中的下一个 Java：{item}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
								}
								else if ((object)item.Version != null && CS_8__locals41._VB_Local_Range.Contains(item.Version))
								{
									Logger.Info($"已选定 Java：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
									return item;
								}
							}
							return (Java)null;
						};
						p?.Set(0.25);
						Java java = obj2();
						if (java != null)
						{
							p?.Skip();
							result = java;
							break;
						}
						object javaLock = JavaLock;
						ObjectFlowControl.CheckForSyncLockOnValueType(javaLock);
						bool lockTaken = false;
						try
						{
							Monitor.Enter(javaLock, ref lockTaken);
							Logger.Warn("既有列表中没有合适的 Java，重新触发搜索", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							JavaListRefreshWorker.Start(CS_8__locals41._VB_NonLocal__VB_Closure_2._VB_Local_c, p?.SplitTo(0.3));
							java = obj2();
							if (java != null)
							{
								p?.Skip();
								result = java;
								break;
							}
							if (!TryFixOrHint)
							{
								result = null;
								break;
							}
							Logger.Warn("电脑中没有合适的 Java，开始获取 Mojang 提供的 Java 下载列表", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
							if (_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init == null)
							{
								Interlocked.CompareExchange(ref _STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init, new StaticLocalInitFlag(), null);
							}
							bool lockTaken2 = false;
							try
							{
								Monitor.Enter(_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init, ref lockTaken2);
								if (_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init.State == 0)
								{
									_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init.State = 2;
									_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker = new RedoableWorker<List<JavaDownloadInfo>>([SpecialName] (CancellationToken? cancellationToken) =>
									{
										string data = ModNet.NetRequestByLoader(ModDownload.DlVersionListOrder(new string[1] { "https://piston-meta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json" }, new string[1] { "https://bmclapi2.bangbang93.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json" }), 45000, IsJson: true);
										cancellationToken?.ThrowIfCancellationRequested();
										List<JavaDownloadInfo> list = new List<JavaDownloadInfo>();
										foreach (JProperty item2 in (IEnumerable<JToken>)data.DeserializeJson<JObject>()["windows-x64"])
										{
											cancellationToken?.ThrowIfCancellationRequested();
											if (Operators.CompareString(item2.Name, "minecraft-java-exe", TextCompare: false) != 0)
											{
												try
												{
													JToken jToken = item2.Value.FirstOrDefault();
													if (jToken != null)
													{
														string text = jToken["version"]["name"].ToString();
														if (text.Contains("."))
														{
															string[] array = text.Split(".");
															list.Add(new JavaDownloadInfo
															{
																ComponentName = item2.Name,
																Java = (JObject)jToken,
																JavaVersion = new Version((int)Math.Round(ModBase.Val(array[0])), (int)Math.Round(ModBase.Val(array[1])), (int)Math.Round((array.Length >= 3) ? ModBase.Val(array[2]) : 0.0), (int)Math.Round((array.Length >= 4) ? ModBase.Val(array[3]) : 0.0))
															});
														}
														else if (text.StartsWithF("8u51-"))
														{
															list.Add(new JavaDownloadInfo
															{
																ComponentName = item2.Name,
																Java = (JObject)jToken,
																JavaVersion = new Version(8, 0, 51, 0)
															});
														}
														else
														{
															list.Add(new JavaDownloadInfo
															{
																ComponentName = item2.Name,
																Java = (JObject)jToken,
																JavaVersion = new Version((int)Math.Round(ModBase.Val(text.BeforeFirstOfAny(new string[2] { "u", "." }))), 0, 0, 0)
															});
														}
													}
												}
												catch (Exception ex3)
												{
													ProjectData.SetProjectError(ex3);
													Exception ex4 = ex3;
													Logger.Warn(ex4, $"解析 Mojang 提供的 Java 列表项失败，已跳过（{item2.Name}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
													ProjectData.ClearProjectError();
												}
											}
										}
										return list;
									}, "SelectOrDownloadJava");
								}
								else if (_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init.State == 2)
								{
									throw new IncompleteInitialization();
								}
							}
							finally
							{
								_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init.State = 1;
								if (lockTaken2)
								{
									Monitor.Exit(_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker_Init);
								}
							}
							if (!_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker.HasSucceeded)
							{
								_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker.Start(CS_8__locals41._VB_NonLocal__VB_Closure_2._VB_Local_c, p?.SplitTo(0.4));
							}
							_STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker.WaitIfRunning(-1, CS_8__locals41._VB_NonLocal__VB_Closure_2._VB_Local_c);
							p?.Set(0.4, skiped: true);
							List<JavaDownloadInfo> lastResult = _STATIC_SelectOrDownloadJava_041282351511824521512823D11282411287982118239128249_DownloadListWorker.LastResult;
							JavaDownloadInfo javaDownloadInfo = lastResult.FirstOrDefault([SpecialName] (JavaDownloadInfo Comp) => Operators.CompareString(Comp.Java["version"]["name"].ToString(), CS_8__locals41._VB_Local_RecommendedComponent, TextCompare: false) == 0) ?? lastResult.FirstOrDefault([SpecialName] (JavaDownloadInfo Comp) => CS_8__locals41._VB_Local_Range.Contains(Comp.JavaVersion));
							if (javaDownloadInfo == null)
							{
								Logger.Warn("Mojang 提供的 Java 列表中没有合适的 Java，无法自动下载", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
								bool flag = CS_8__locals41._VB_Local_Range.HasLower && CS_8__locals41._VB_Local_Range.IsLowerInclusive && CS_8__locals41._VB_Local_Range.Lower.Minor <= 0 && CS_8__locals41._VB_Local_Range.Lower.Build <= 0 && CS_8__locals41._VB_Local_Range.Lower.Revision <= 0;
								bool flag2 = CS_8__locals41._VB_Local_Range.HasUpper && !CS_8__locals41._VB_Local_Range.IsUpperInclusive && CS_8__locals41._VB_Local_Range.Upper.Minor <= 0 && CS_8__locals41._VB_Local_Range.Upper.Build <= 0 && CS_8__locals41._VB_Local_Range.Upper.Revision <= 0;
								string arg2;
								if (CS_8__locals41._VB_Local_Range.HasLower && CS_8__locals41._VB_Local_Range.HasUpper)
								{
									if (!flag || !flag2)
									{
										arg2 = $" Java {CS_8__locals41._VB_Local_Range}";
									}
									else
									{
										arg2 = ((CS_8__locals41._VB_Local_Range.Lower.Major != CS_8__locals41._VB_Local_Range.Upper.Major - 1) ? $" Java {CS_8__locals41._VB_Local_Range.Lower.Major} ~ {CS_8__locals41._VB_Local_Range.Upper.Major - 1}" : $" Java {CS_8__locals41._VB_Local_Range.Lower.Major}");
									}
								}
								else if (CS_8__locals41._VB_Local_Range.HasLower)
								{
									if (flag)
									{
										arg2 = $" Java {CS_8__locals41._VB_Local_Range.Lower.Major} 或更高版本的 Java";
									}
									else
									{
										arg2 = ((!CS_8__locals41._VB_Local_Range.IsLowerInclusive) ? $"版本高于 {CS_8__locals41._VB_Local_Range.Lower} 的 Java" : $" Java {CS_8__locals41._VB_Local_Range.Lower} 或更高版本的 Java");
									}
								}
								else if (flag2)
								{
									arg2 = $" Java {CS_8__locals41._VB_Local_Range.Upper.Major - 1} 或更低版本的 Java";
								}
								else
								{
									arg2 = ((!CS_8__locals41._VB_Local_Range.IsUpperInclusive) ? $"版本低于 {CS_8__locals41._VB_Local_Range.Upper} 的 Java" : $" Java {CS_8__locals41._VB_Local_Range.Upper} 或更低版本的 Java");
								}
								ModMain.MyMsgBox(string.Format("PCL 没能找到合适的 Java。{0}请在网上搜索并手动安装{1}， 然后在【设置 → 启动 → 高级选项 → Java 列表】中导入它。", "\r\n", arg2), "需要手动安装 Java");
								result = null;
								break;
							}
							ModLoader.LoaderCombo<int> loaderCombo = CreateJavaDownloadLoader(javaDownloadInfo);
							try
							{
								loaderCombo.WaitForExit(null, null, IsForceRestart: true, CS_8__locals41._VB_NonLocal__VB_Closure_2._VB_Local_c, p?.SplitTo(0.95));
							}
							finally
							{
								loaderCombo.Cancel();
							}
							java = obj2();
							if (java != null)
							{
								p?.Finish();
								result = java;
								break;
							}
							throw new Exception($"下载 Java {javaDownloadInfo.ComponentName} 后仍然无法找到合适的 Java");
						}
						finally
						{
							if (lockTaken)
							{
								Monitor.Exit(javaLock);
							}
						}
					}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ex2.ThrowIfCanceled();
					Logger.Error(ex2, "选择 Java 时出现异常", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					p?.Skip();
					result = null;
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}
		private static void SwitchToInstanceSetup(ModMinecraft.McInstance Instance)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				PageInstanceLeft.Instance = Instance;
				ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSetup, FormMain.PageSubType.SetupLink);
			});
		}
		private static void MigrateInstanceJavaSettings(ModMinecraft.McInstance Instance, CancellationToken c = default(CancellationToken), ProgressProvider p = null)
		{
			if (Configs.JavaMigrated.Get(Instance.Config))
			{
				p?.Skip();
				return;
			}
			if (ModMain.FrmInstanceSetup != null)
			{
				ModBase.RunInUiWait([SpecialName] () =>
				{
					ModMain.FrmInstanceSetup.ComboArgumentJava.IsEnabled = false;
				});
			}
			try
			{
				Logger.Info($"正在尝试迁移版本独立的 Java 设置（{Instance.Name}）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
				string text = Settings.Get<string>("VersionArgumentJavaSelect", Instance);
				if (!text.StartsWithF("{"))
				{
					p?.Set(0.4, skiped: true);
				}
				else
				{
					Java java = new Java(text.DeserializeJson<JObject>()["Path"].ToString());
					if (java.CheckAsync(c).Run())
					{
						Logger.Info($"将版本独立设置改为老版本中强制指定的 Java：{java}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						if (!Configs.JavaList.Get().Contains(java))
						{
							Configs.JavaList.Edit([SpecialName] (ref ConcurrentList<Java> List) =>
							{
								List.Add(java);
							});
							Logger.Info($"已将强制指定的 Java 加入 Java 列表底部：{java}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
						}
						Configs.JavaForced.Set(java, Instance.Config);
						Settings.Set("VersionArgumentJavaV2", 3, Instance);
						UpdateJavaLists();
						Configs.JavaMigrated.Set(value: true, Instance.Config);
						p?.Finish();
						return;
					}
					ModMain.Hint("该版本的设置中强制指定的 Java 存在异常，该设置已重置", ModMain.HintType.Red);
					p?.Set(0.4);
				}
				List<Java> list = JavaUtils.SearchFoldersAsync(includeSubDirectories: true, new string[1] { Instance.PathVersion }, c, p?.SplitTo(1.0)).Run();
				if (list.Any())
				{
					Logger.Info($"在版本文件夹中发现了 Java，将版本独立设置改为优先使用版本文件夹的 Java：{list}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					Settings.Set("VersionArgumentJavaV2", 2, Instance);
				}
				else
				{
					Logger.Info("没有需要迁移的内容，自动选择 Java", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
					Settings.Set("VersionArgumentJavaV2", 0, Instance);
				}
				Configs.JavaMigrated.Set(value: true, Instance.Config);
			}
			finally
			{
				if (ModMain.FrmInstanceSetup != null)
				{
					ModBase.RunInUiWait([SpecialName] () =>
					{
						SettingService.RefreshSettings(ModMain.FrmInstanceSetup.ComboArgumentJava);
						SettingService.RefreshSettings(ModMain.FrmInstanceSetup.ComboArgumentJavaSelect);
						ModMain.FrmInstanceSetup.ComboArgumentJava.IsEnabled = true;
					});
				}
			}
		}
		private static ModLoader.LoaderCombo<int> CreateJavaDownloadLoader(JavaDownloadInfo Target)
		{
			_Closure___16_0 arg = default;
			_Closure___16_0 CS_8__locals4 = new _Closure___16_0(arg);
			string text = (string?)Target.Java["manifest"]["url"];
			Logger.Info(string.Format("准备下载 Java {0}（{1}）：{2}", Target.Java["version"]["name"], Target.ComponentName, text), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
			string data = ModNet.NetRequestByLoader(ModDownload.DlSourceOrder(new string[1] { text }, new string[1] { text.Replace("piston-meta.mojang.com", "bmclapi2.bangbang93.com") }), 45000, IsJson: true);
			CS_8__locals4._VB_Local_BaseDir = $"{Paths.AppData}.minecraft\\runtime\\{Target.ComponentName}\\";
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			foreach (JProperty item in (IEnumerable<JToken>)((JObject)data.DeserializeJson())["files"])
			{
				if (((JObject)item.Value)["downloads"]?["raw"] != null)
				{
					JObject jObject = (JObject)((JObject)item.Value)["downloads"]["raw"];
					FileChecker fileChecker = new FileChecker
					{
						ActualSize = (long)jObject["size"],
						Hash = (string?)jObject["sha1"]
					};
					if (Operators.CompareString(fileChecker.Hash, "12976a6c2b227cbac58969c1455444596c894656", TextCompare: false) != 0 && Operators.CompareString(fileChecker.Hash, "c80e4bab46e34d02826eab226a4441d0970f2aba", TextCompare: false) != 0 && Operators.CompareString(fileChecker.Hash, "84d2102ad171863db04e7ee22a259d1f6c5de4a5", TextCompare: false) != 0)
					{
						string text2 = (string?)jObject["url"];
						list.Add(new ModNet.NetFile(ModDownload.DlSourceOrder(new string[1] { text2 }, new string[1] { text2.Replace("piston-data.mojang.com", "bmclapi2.bangbang93.com") }), CS_8__locals4._VB_Local_BaseDir + item.Name, fileChecker));
					}
				}
			}
			Logger.Info($"该 Java 需要下载 {list.Count} 个文件，目标文件夹：{CS_8__locals4._VB_Local_BaseDir}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModJava.vb");
			return new ModLoader.LoaderCombo<int>("下载 Java", new ModLoader.LoaderBase[2]
			{
				new ModNet.LoaderDownload("下载 Java 文件", list)
				{
					ProgressWeight = 10.0
				},
				new ModLoader.LoaderWorker("刷新 Java 列表", JavaListRefreshWorker)
			})
			{
				OnStateChanged = [SpecialName] (ModLoader.LoaderBase a0) =>
				{
					CS_8__locals4._Lambda___0((ModLoader.LoaderCombo<int>)a0);
				}
			};
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
	}
}
