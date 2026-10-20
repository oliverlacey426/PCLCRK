// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class FormMain : Window
	{
		public enum PageType
		{
			Launch,
			Download,
			Link,
			Setup,
			Other,
			InstanceSelect,
			TaskManager,
			InstanceSetup,
			ResourceDetail,
			HelpDetail
		}
		public enum PageSubType
		{
			Default = 0,
			DownloadInstall = Default,
			DownloadMod = 2,
			DownloadPack = 3,
			DownloadDataPack = 4,
			DownloadResourcePack = 5,
			DownloadShader = 6,
			SetupLaunch = Default,
			SetupLink = 1,
			SetupUI = DownloadMod,
			SetupSystem = DownloadPack,
			LinkMain = Default,
			OtherHelp = Default,
			OtherAbout = SetupLink,
			OtherTest = DownloadMod,
			InstanceOverall = Default,
			InstanceSetup = SetupLink,
			InstanceMod = DownloadMod,
			InstanceModDisabled = DownloadPack,
			InstanceExport = DownloadDataPack
		}
		public class PageStackData
		{
			public PageType Page;
			public object Additional;
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				if (other is PageStackData)
				{
					PageStackData pageStackData = (PageStackData)other;
					if (Page != pageStackData.Page)
					{
						return false;
					}
					if (Additional == null)
					{
						return pageStackData.Additional == null;
					}
					return pageStackData.Additional != null && Additional.Equals(RuntimeHelpers.GetObjectValue(pageStackData.Additional));
				}
				if (other is int)
				{
					if (Operators.ConditionalCompareObjectNotEqual(Page, other, TextCompare: false))
					{
						return false;
					}
					return Additional == null;
				}
				return false;
			}
			public static bool operator ==(PageStackData left, PageStackData right)
			{
				return EqualityComparer<PageStackData>.Default.Equals(left, right);
			}
			public static bool operator !=(PageStackData left, PageStackData right)
			{
				return !(left == right);
			}
			public static implicit operator PageStackData(PageType Value)
			{
				return new PageStackData
				{
					Page = Value
				};
			}
			public static implicit operator PageType(PageStackData Value)
			{
				return Value.Page;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___1_0
		{
			public string _VB_Local_Content;
			public _Closure___1_0(_Closure___1_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Content = arg0._VB_Local_Content;
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				if (ModMain.MyMsgBox(_VB_Local_Content, "PCL 已更新至快照版 2.13.1.1", "确定", "完整更新日志") == 2)
				{
					ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat?tab=feed");
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___22_0
		{
			public string _VB_Local_Code;
			public Action _I1;
			[SpecialName]
			internal void _Lambda___0()
			{
				if (ModMain.MyMsgBox("嘿，是否使用复制的邀请码加入房间？", "加入联机房间", "加入", "取消") != 2)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						PageLinkMain.Join(_VB_Local_Code);
						ModBase.ClipboardSet(null, ShowSuccessHint: false);
					});
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				PageLinkMain.Join(_VB_Local_Code);
				ModBase.ClipboardSet(null, ShowSuccessHint: false);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___25_0
		{
			public IEnumerable<string> _VB_Local_FilePathList;
			public _Closure___25_0(_Closure___25_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_FilePathList = arg0._VB_Local_FilePathList;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				_Closure___25_1 arg = default;
				_Closure___25_1 CS_8__locals4 = new _Closure___25_1(arg);
				string text = _VB_Local_FilePathList.First();
				Logger.Warn(string.Format("接受文件拖拽：{0}{1}", text, _VB_Local_FilePathList.Any() ? $" 等 {_VB_Local_FilePathList.Count()} 个文件" : ""), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				if (DirectoryUtils.Exists(_VB_Local_FilePathList.First()) && !FileUtils.Exists(_VB_Local_FilePathList.First()))
				{
					ModMain.Hint("请拖入一个文件，而非文件夹！", ModMain.HintType.Red);
					return;
				}
				if (!FileUtils.Exists(_VB_Local_FilePathList.First()))
				{
					ModMain.Hint("拖入的文件不存在：" + _VB_Local_FilePathList.First(), ModMain.HintType.Red);
					return;
				}
				List<string> list = _VB_Local_FilePathList.ToList();
				if (_VB_Local_FilePathList.Count() > 1)
				{
					foreach (string item in list)
					{
						if (!new string[4] { "jar", "litemod", "disabled", "old" }.Contains(item.AfterLast(".").Lower()))
						{
							ModMain.Hint("一次请只拖入一个文件！", ModMain.HintType.Red);
							return;
						}
					}
				}
				CS_8__locals4._VB_Local_Extension = text.AfterLast(".").Lower();
				if (Operators.CompareString(CS_8__locals4._VB_Local_Extension, "xaml", TextCompare: false) == 0)
				{
					Logger.Info("文件后缀为 XAML，作为主页加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("已存在一个主页文件，是否要将它覆盖？", "覆盖确认", "覆盖", "取消") != 2)
					{
						FileUtils.Copy(text, Paths.Base + "PCL\\Custom.xaml");
						ModBase.RunInUi([SpecialName] () =>
						{
							Settings.Set("UiCustomType", 1);
							ModMain.FrmLaunchRight.ForceRefresh();
							ModMain.Hint("已加载主页自定义文件！", ModMain.HintType.Green);
						});
					}
				}
				else
				{
					if (PageInstanceMod.InstallMods(list))
					{
						return;
					}
					if (new string[3] { "zip", "rar", "mrpack" }.Any([SpecialName] (string t) => Operators.CompareString(t, CS_8__locals4._VB_Local_Extension, TextCompare: false) == 0))
					{
						Logger.Info("文件为压缩包，尝试作为整合包安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						try
						{
							ModModpack.ModpackInstall(text);
							ModBase.RunInUi([SpecialName] () =>
							{
								ModMain.FrmMain.PageChange(PageType.TaskManager);
							});
							return;
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							if (ex2.IsCanceled())
							{
								ProjectData.ClearProjectError();
								return;
							}
							ProjectData.ClearProjectError();
						}
					}
					if (Operators.CompareString(CS_8__locals4._VB_Local_Extension, "rar", TextCompare: false) == 0)
					{
						ModMain.Hint("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试！");
						return;
					}
					try
					{
						Logger.Info("尝试进行错误报告分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						CrashAnalyzer crashAnalyzer = new CrashAnalyzer();
						crashAnalyzer.Import(text);
						if (crashAnalyzer.Prepare())
						{
							crashAnalyzer.Analyze();
							crashAnalyzer.Output(IsManualAnalyze: true, new List<string>());
							return;
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Error(ex4, "自主错误报告分析失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						ProjectData.ClearProjectError();
					}
					ModMain.Hint("PCL 无法确定应当执行的文件拖拽操作……");
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___25_1
		{
			public string _VB_Local_Extension;
			public _Closure___25_1(_Closure___25_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Extension = arg0._VB_Local_Extension;
				}
			}
			[SpecialName]
			internal bool _Lambda___2(string t)
			{
				return Operators.CompareString(t, _VB_Local_Extension, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___50_0
		{
			public string _VB_Local_PageName;
			public FormMain _VB_Me;
			public _Closure___50_0(_Closure___50_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_PageName = arg0._VB_Local_PageName;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				_VB_Me.LabTitleInner.Text = _VB_Local_PageName;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___9_0
		{
			public ScaleTransform _VB_Local_TransformScale;
			public TranslateTransform _VB_Local_TransformPos;
			public RotateTransform _VB_Local_TransformRotate;
			public _Closure___9_0(_Closure___9_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_TransformScale = arg0._VB_Local_TransformScale;
					_VB_Local_TransformPos = arg0._VB_Local_TransformPos;
					_VB_Local_TransformRotate = arg0._VB_Local_TransformRotate;
				}
			}
			[SpecialName]
			internal void _Lambda___2(object i)
			{
				ScaleTransform scaleTransform;
				(scaleTransform = _VB_Local_TransformScale).ScaleX = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleX, i));
				(scaleTransform = _VB_Local_TransformScale).ScaleY = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleY, i));
			}
			[SpecialName]
			internal void _Lambda___3(object i)
			{
				TranslateTransform translateTransform;
				(translateTransform = _VB_Local_TransformPos).Y = Conversions.ToDouble(Operators.AddObject(translateTransform.Y, i));
			}
			[SpecialName]
			internal void _Lambda___4(object i)
			{
				RotateTransform rotateTransform;
				(rotateTransform = _VB_Local_TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(rotateTransform.Angle, i));
			}
		}
		private bool IsWindowLoadFinished;
		private static bool IsLogShown = false;
		public bool IsSizeSaveable;
		public bool IsSystemTimeChanged;
		private bool _Hidden;
		public PageStackData PageCurrent;
		public PageStackData PageLast;
		public List<PageStackData> PageStack;
		public MyPageLeft PageLeft;
		public MyPageRight PageRight;
		private bool IsChangingPage;
		public System.Windows.Input.MouseEventArgs lastMouseArg;
		[SpecialName]
		private DragHelper _STATIC_ctor_2001_Helper;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_ctor_2001_Helper_Init;
		[SpecialName]
		private System.Windows.IDataObject _STATIC_HandleDrag_20211C1284AD_PrevData;
		[SpecialName]
		private System.Windows.DragDropEffects _STATIC_HandleDrag_20211C1284AD_PrevEffects;
		public bool Hidden
		{
			get
			{
				return _Hidden;
			}
			set
			{
				if (_Hidden == value)
				{
					return;
				}
				_Hidden = value;
				if (value)
				{
					Left -= 10000.0;
					ShowInTaskbar = false;
					Visibility = Visibility.Hidden;
					Logger.Info($"窗口已隐藏，位置：({Left},{Top})", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				}
				else
				{
					if (Left < -2000.0)
					{
						Left += 10000.0;
					}
					ShowWindowToTop();
				}
			}
		}
		public PageSubType PageCurrentSub
		{
			get
			{
				PageStackData pageCurrent = PageCurrent;
				if (pageCurrent == PageType.Download)
				{
					if (ModMain.FrmDownloadLeft == null)
					{
						ModMain.FrmDownloadLeft = new PageDownloadLeft();
					}
					return ModMain.FrmDownloadLeft.PageID;
				}
				if (pageCurrent == PageType.Setup)
				{
					if (ModMain.FrmSetupLeft == null)
					{
						ModMain.FrmSetupLeft = new PageSetupLeft();
					}
					return ModMain.FrmSetupLeft.PageID;
				}
				if (pageCurrent == PageType.Other)
				{
					if (ModMain.FrmOtherLeft == null)
					{
						ModMain.FrmOtherLeft = new PageOtherLeft();
					}
					return ModMain.FrmOtherLeft.PageID;
				}
				if (pageCurrent == PageType.InstanceSetup)
				{
					if (ModMain.FrmInstanceLeft == null)
					{
						ModMain.FrmInstanceLeft = new PageInstanceLeft();
					}
					return ModMain.FrmInstanceLeft.PageID;
				}
				return PageSubType.Default;
			}
		}














































		private void ShowUpdateLog(int LastVersion)
		{
			_Closure___1_0 arg = default;
			_Closure___1_0 CS_8__locals2 = new _Closure___1_0(arg);
			int num = 0;
			int num2 = 0;
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			checked
			{
				if (LastVersion < 410)
				{
					if (LastVersion == 409)
					{
						list.Add(new KeyValuePair<int, string>(1, "修复：更新 Mod 时可能更新成另一个 Mod 加载器的版本"));
					}
					num += 2;
					num2 += 6;
				}
				if (LastVersion < 409)
				{
					list.Add(new KeyValuePair<int, string>(2, "优化：版本设置中，添加打开截图文件夹的快捷方式"));
					list.Add(new KeyValuePair<int, string>(2, "优化：导出整合包时，可以勾选导出光影设置文件"));
					num += 21;
					num2 += 22;
				}
				if (LastVersion < 407)
				{
					if (LastVersion == 405)
					{
						list.Add(new KeyValuePair<int, string>(1, "修复：使用部分主页预设时崩溃"));
						list.Add(new KeyValuePair<int, string>(1, "修复：无法删除部分文件夹"));
						list.Add(new KeyValuePair<int, string>(1, "修复：版本设置中指定的 Java 无法被保存"));
						num++;
						num2 += 3;
					}
					num += 2;
				}
				if (LastVersion < 405)
				{
					list.Add(new KeyValuePair<int, string>(4, "新增：重做 Java 管理与相关设置，允许调整 Java 优先级、指定 Java 版本范围等"));
					list.Add(new KeyValuePair<int, string>(3, "优化：导出整合包时允许自动导出版本文件夹中的 Java"));
					list.Add(new KeyValuePair<int, string>(3, "优化：重做弹出提示的样式以及动画，以更符合现代 UI 审美"));
					list.Add(new KeyValuePair<int, string>(1, "修复：可能无法安装 OptiFine 26.1.2"));
					num += 35;
					num2 += 17;
				}
				if (LastVersion < 403)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：Windows 7 无法正常联网"));
					num++;
					num2 += 2;
				}
				if (LastVersion < 401)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：可能无法更换披风"));
					num += 8;
					num2 += 6;
				}
				if (LastVersion < 399)
				{
					list.Add(new KeyValuePair<int, string>(2, "优化：升级框架到 .NET Framework 4.8"));
					num += 28;
					num2 += 19;
				}
				if (LastVersion < 397 && LastVersion >= 393 && LastVersion < 397)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：无法使用自定义隐藏主题"));
				}
				if (LastVersion < 395 && LastVersion == 393)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：部分个性化功能失效"));
				}
				if (LastVersion < 393)
				{
					list.Add(new KeyValuePair<int, string>(2, "优化：允许永久关闭启动页面的快照版提示"));
					list.Add(new KeyValuePair<int, string>(2, "优化：下载游戏、整合包的稳定性"));
					num++;
					num2 += 5;
				}
				if (LastVersion < 390)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：会为部分 Forge / NeoForge 选择不支持的 Java"));
					num2 += 4;
				}
				if (LastVersion < 388)
				{
					if (LastVersion == 386)
					{
						list.Add(new KeyValuePair<int, string>(2, "修复：无法使用 Java 25+ 启动 Forge"));
					}
					num += 4;
					num2 += 3;
				}
				if (LastVersion < 386)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：Minecraft 使用 Java 25+ 时的一个性能问题"));
					if (LastVersion == 384 || LastVersion == 385)
					{
						list.Add(new KeyValuePair<int, string>(2, "修复：无法正常重命名游戏版本"));
					}
					if (LastVersion == 385)
					{
						list.Add(new KeyValuePair<int, string>(1, "修复：无法显示 Modrinth 整合包的版本列表"));
					}
					num2++;
				}
				if (LastVersion < 385)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：无法下载 NeoForge 26.1"));
					if (LastVersion == 384)
					{
						list.Add(new KeyValuePair<int, string>(2, "修复：下载可能失败，提示任务管理刷新线程出错"));
						list.Add(new KeyValuePair<int, string>(2, "修复：无法保存选择的 Minecraft 文件夹"));
						list.Add(new KeyValuePair<int, string>(1, "修复：使用中文搜索 Mod 时，部分结果会忽略筛选条件"));
					}
					num += 3;
					num2 += 6;
				}
				if (LastVersion < 384)
				{
					list.Add(new KeyValuePair<int, string>(3, "优化：降低 Minecraft 的内存占用"));
					list.Add(new KeyValuePair<int, string>(2, "修复：部分关键词搜不到资源，特别是中文 Mod 搜索经常没有结果"));
					list.Add(new KeyValuePair<int, string>(1, "修复：老版本 Windows 无法启动游戏"));
					list.Add(new KeyValuePair<int, string>(1, "修复：连接 Mojang 的服务可能失败，提示错误码 421"));
					list.Add(new KeyValuePair<int, string>(1, "修复：无法访问爱发电"));
					num += 21;
					num2 += 14;
				}
				if (LastVersion < 382)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：下载可能完全卡住，或是下载进度反复回退"));
					list.Add(new KeyValuePair<int, string>(1, "新增：内存管理设置，可以选择 G1GC、ZGC 或分代 ZGC"));
					list.Add(new KeyValuePair<int, string>(1, "优化：来自 Modrinth 的文件下载速度"));
					num += 15;
					num2 += 16;
				}
				if (LastVersion < 380)
				{
					num += 3;
					num2 += 13;
				}
				if (LastVersion < 378 && LastVersion >= 377)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：在版本列表中存在 OptiFine 时可能无法加载版本列表"));
					num2 += 2;
				}
				if (LastVersion < 377)
				{
					if (LastVersion >= 373)
					{
						list.Add(new KeyValuePair<int, string>(5, "删除：暂时隐藏联机入口……不过只是暂时关闭，它还会回来的！"));
					}
					list.Add(new KeyValuePair<int, string>(3, "新增：适配新的 Minecraft 版本号系统与 Unobfuscated 版本"));
					list.Add(new KeyValuePair<int, string>(2, "优化：巨幅优化各个下载页面和 Mod 管理页面的性能"));
					list.Add(new KeyValuePair<int, string>(1, "优化：更换披风时会显示当前使用的披风"));
					num += 33;
					num2 += 29;
				}
				if (LastVersion < 375)
				{
					num += 16;
					num2 += 4;
				}
				if (LastVersion < 374)
				{
					num += 9;
					num2 += 7;
				}
				if (LastVersion < 373)
				{
					num += 7;
					num2 += 10;
				}
				List<string> list2 = new List<string>();
				List<KeyValuePair<int, string>> list3 = list.OrderByDescending([SpecialName] (KeyValuePair<int, string> f) => f.Key).ToList();
				if (!list3.Any() && num == 0 && num2 == 0)
				{
					list2.Add("没有更新日志……");
				}
				int num3 = Math.Min(9, list3.Count - 1);
				for (int num4 = 0; num4 <= num3; num4++)
				{
					list2.Add(list3[num4].Value);
				}
				if (list3.Count > 10)
				{
					num += list3.Count - 10;
				}
				if (num > 0 || num2 > 0)
				{
					list2.Add(((num > 0) ? (Conversions.ToString(num) + " 项小调整与修改") : "") + ((num > 0 && num2 > 0) ? "，" : "") + ((num2 > 0) ? ("修复了 " + Conversions.ToString(num2) + " 个 Bug") : "") + "，详见完整更新日志");
				}
				CS_8__locals2._VB_Local_Content = "· " + list2.Join("\r\n· ");
				ModBase.RunInNewThread([SpecialName] () =>
				{
					if (ModMain.MyMsgBox(CS_8__locals2._VB_Local_Content, "PCL 已更新至快照版 2.13.1.1", "确定", "完整更新日志") == 2)
					{
						ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat?tab=feed");
					}
				}, "UpdateLog Output");
			}
		}
		public FormMain()
		{
			Loaded += FormMain_Loaded;
			Closing += FormMain_Closing;
			SizeChanged += [SpecialName] (object a0, SizeChangedEventArgs a1) =>
			{
				FormMain_SizeChanged();
			};
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				FormMain_SizeChanged();
			};
			KeyDown += FormMain_KeyDown;
			MouseDown += FormMain_MouseDown;
			Activated += [SpecialName] (object a0, EventArgs a1) =>
			{
				FormMain_Activated();
			};
			Drop += FrmMain_Drop;
			MouseMove += FormMain_MouseMove;
			IsWindowLoadFinished = false;
			IsSizeSaveable = false;
			IsSystemTimeChanged = false;
			_Hidden = false;
			PageCurrent = PageType.Launch;
			PageLast = PageType.Launch;
			PageStack = new List<PageStackData>();
			IsChangingPage = false;
			lastMouseArg = null;
			ModBase.ApplicationStartTick = ModBase.GetTimeMs();
			ModMain.FrmMain = this;
			ModMain.FrmLaunchLeft = new PageLaunchLeft();
			ModMain.FrmLaunchRight = new PageLaunchRight();
			int num = Settings.Get<int>("SystemLastVersionReg");
			if (num < 410)
			{
				UpgradeSub(num);
			}
			else if (num > 410)
			{
				DowngradeSub(num);
			}
			if (!Settings.HasSaved("LaunchArgumentIndieV2"))
			{
				if (Settings.HasSaved("LaunchArgumentIndie"))
				{
					Logger.Info("从老 PCL 迁移版本隔离", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					Settings.Set("LaunchArgumentIndieV2", Settings.Get<int>("LaunchArgumentIndie"));
				}
				else if (ModBase.HasIniKey("Setup", "LaunchVersionSelect"))
				{
					Logger.Info("从老 PCL 升级，但此前未调整版本隔离，使用老的版本隔离默认值", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					Settings.Set("LaunchArgumentIndieV2", Settings.GetDefault("LaunchArgumentIndie"));
				}
				else
				{
					Logger.Info("全新的 PCL，使用新的版本隔离默认值", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					Settings.Set("LaunchArgumentIndieV2", Settings.GetDefault("LaunchArgumentIndieV2"));
				}
			}
			ModSecret.ThemeCheckAll(EffectSetup: false);
			ModSecret.ThemeRefresh(Settings.Get<int>("UiLauncherTheme"));
			AddHandler(DragDrop.DragEnterEvent, new System.Windows.DragEventHandler(HandleDrag), handledEventsToo: true);
			AddHandler(DragDrop.DragOverEvent, new System.Windows.DragEventHandler(HandleDrag), handledEventsToo: true);
			InitializeComponent();
			InitConnectEvents();
			Opacity = 0.0;
			if (WindowsUtils.HasAdminRole())
			{
				if (_STATIC_ctor_2001_Helper_Init == null)
				{
					Interlocked.CompareExchange(ref _STATIC_ctor_2001_Helper_Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_STATIC_ctor_2001_Helper_Init, ref lockTaken);
					if (_STATIC_ctor_2001_Helper_Init.State == 0)
					{
						_STATIC_ctor_2001_Helper_Init.State = 2;
						_STATIC_ctor_2001_Helper = new DragHelper();
					}
					else if (_STATIC_ctor_2001_Helper_Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_STATIC_ctor_2001_Helper_Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_STATIC_ctor_2001_Helper_Init);
					}
				}
				SourceInitialized += [SpecialName] (object a0, EventArgs a1) =>
				{
					_Lambda___3_0();
				};
				Closing += [SpecialName] (object a0, CancelEventArgs a1) =>
				{
					_Lambda___3_1();
				};
				_STATIC_ctor_2001_Helper.DragDrop += [SpecialName] (object a0, EventArgs a1) =>
				{
					_Lambda___3_2();
				};
			}
			if (!Information.IsNothing(ModMain.FrmLaunchLeft.Parent))
			{
				ModMain.FrmLaunchLeft.SetValue(ContentPresenter.ContentProperty, null);
			}
			if (!Information.IsNothing(ModMain.FrmLaunchRight.Parent))
			{
				ModMain.FrmLaunchRight.SetValue(ContentPresenter.ContentProperty, null);
			}
			PanMainLeft.Child = ModMain.FrmLaunchLeft;
			PageLeft = ModMain.FrmLaunchLeft;
			PanMainRight.Child = ModMain.FrmLaunchRight;
			PageRight = ModMain.FrmLaunchRight;
			ModMain.FrmLaunchRight.PageState = MyPageRight.PageStates.ContentStay;
			if (ModBase.ModeDebug)
			{
				ModMain.Hint("[调试模式] PCL 正以调试模式运行，这可能会导致性能下降，若无必要请不要开启！");
			}
			ModMinecraft.McFolderListLoader.Start(0);
			Logger.Info($"第二阶段加载用时：{checked(ModBase.GetTimeMs() - ModBase.ApplicationStartTick)} ms", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		private void FormMain_Loaded(object sender, RoutedEventArgs e)
		{
			ModBase.ApplicationStartTick = ModBase.GetTimeMs();
			ModBase.Handle = new WindowInteropHelper(this).Handle;
			UpdateBackgroundAndTitleBar();
			PageSetupUI.HiddenRefresh();
			PageSetupUI.BackgroundRefresh(IsHint: false, Refresh: true);
			ModMusic.MusicRefreshPlay(ShowHint: false, IsFirstLoad: true);
			BtnExtraDownload.ShowCheck = BtnExtraDownload_ShowCheck;
			BtnExtraBack.ShowCheck = BtnExtraBack_ShowCheck;
			BtnExtraApril.ShowCheck = BtnExtraApril_ShowCheck;
			BtnExtraShutdown.ShowCheck = BtnExtraShutdown_ShowCheck;
			BtnExtraApril.ShowRefresh();
			MyResizer myResizer = new MyResizer(this);
			myResizer.addResizerDown(ResizerB);
			myResizer.addResizerLeft(ResizerL);
			myResizer.addResizerLeftDown(ResizerLB);
			myResizer.addResizerLeftUp(ResizerLT);
			myResizer.addResizerRight(ResizerR);
			myResizer.addResizerRightDown(ResizerRB);
			myResizer.addResizerRightUp(ResizerRT);
			myResizer.addResizerUp(ResizerT);
			if (ModBase.RandomInteger(1, 1000) == 233)
			{
				ShapeTitleLogo.Data = (Geometry)new GeometryConverter().ConvertFromString("M26,29 v-25 h6 a7,7 180 0 1 0,14 h-6 M83,6.5 a10,11.5 180 1 0 0,18 M48,2.5 v24.5 h13.5");
			}
			ModSecret.ThemeRefreshMain();
			try
			{
				Height = Settings.Get<int>("WindowHeight");
				Width = Settings.Get<int>("WindowWidth");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "读取窗口默认大小失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				Height = MinHeight + 100.0;
				Width = MinWidth + 100.0;
				ProjectData.ClearProjectError();
			}
			Topmost = false;
			checked
			{
				if (ModMain.FrmStart != null)
				{
					ModMain.FrmStart.Close(new TimeSpan(0, 0, 0, 0, (int)Math.Round(400.0 / ModAnimation.AniSpeed)));
				}
				Top = (ModBase.GetWPFSize(MyWpfExtension.Computer.Screen.WorkingArea.Height) - Height) / 2.0;
				Left = (ModBase.GetWPFSize(MyWpfExtension.Computer.Screen.WorkingArea.Width) - Width) / 2.0;
				IsSizeSaveable = true;
				ShowWindowToTop();
				((HwndSource)PresentationSource.FromVisual(this)).AddHook(WndProc);
				ModAnimation.AniStart(new ModAnimation.AniData[5]
				{
					ModAnimation.AaCode([SpecialName] () =>
					{
						ModAnimation.AniControlEnabled--;
					}, 50),
					ModAnimation.AaOpacity(this, (double)Settings.Get<int>("UiLauncherTransparent") / 1000.0 + 0.4, 250, 100),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						TranslateTransform transformPos;
						(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
					}, 0.0 - TransformPos.Y, 600, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						RotateTransform transformRotate;
						(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
					}, 0.0 - TransformRotate.Angle, 500, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PanBack.RenderTransform = null;
						IsWindowLoadFinished = true;
						Logger.Info($"DPI：{ModBase.DPI}，系统版本：{Environment.OSVersion.VersionString}，PCL 位置：{ModBase.PathExe}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					}, 0, After: true)
				}, "Form Show");
				ModAnimation.AniStart();
				ModMain.TimerMainStart();
				ModBase.RunInNewThread([SpecialName] () =>
				{
					if (Settings.Get<int>("SystemEulaVersion") < 2)
					{
						switch (ModMain.MyMsgBox((Settings.Get<int>("SystemEulaVersion") == 0) ? "在使用 PCL 前，请先阅读用户协议与免责声明。" : string.Format("PCL 的用户协议与免责声明已更新。{0}请阅读更新后的用户协议与免责声明。", "\r\n"), "协议授权", "同意", "拒绝", "查看用户协议与免责声明", IsWarn: false, HighLight: true, ForceWait: false, null, null, [SpecialName] () =>
						{
							ModBase.OpenWebsite("https://shimo.im/docs/rGrd8pY8xWkt6ryW");
						}))
						{
						case 1:
							Settings.Set("SystemEulaVersion", 2);
							break;
						case 2:
							EndProgram(SendWarning: false);
							break;
						}
					}
					try
					{
						ModJava.JavaInit();
						Thread.Sleep(100);
						ModDownload.DlClientListMojangLoader.Start(1);
						RunCountSub();
						ModSecret.ServerLoader.Start();
						ModBase.RunInNewThread(ModMain.TryClearTaskTemp, "TryClearTaskTemp", ThreadPriority.BelowNormal);
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Error(ex4, "初始化加载池运行失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						ProjectData.ClearProjectError();
					}
					try
					{
						FileUtils.Delete(Paths.Base + "PCL\\Plain Craft Launcher 2.exe");
					}
					catch (Exception ex5)
					{
						ProjectData.SetProjectError(ex5);
						Exception ex6 = ex5;
						Logger.Warn(ex6, "清理自动更新文件失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						ProjectData.ClearProjectError();
					}
					ModMain.Telemetry("启动");
				}, "初始化", ThreadPriority.Lowest);
				Logger.Info($"第三阶段加载用时：{ModBase.GetTimeMs() - ModBase.ApplicationStartTick} ms", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
		}
		private void RunCountSub()
		{
			Settings.Set("SystemCount", checked(Settings.Get<int>("SystemCount") + 1));
			int num = Settings.Get<int>("SystemCount");
			if (num == 1)
			{
				ModMain.MyMsgBox("欢迎使用 PCL 快照版！\r\n快照版包含尚未在正式版发布的测试性功能，仅用于赞助者本人尝鲜。所以请不要发给其他人或者用于制作整合包哦！\r\n如果你并非通过赞助或赞助者本人邀请进群获得的本程序，那么可能是有人在违规传播，记得提醒他一下啦。", "快照版使用说明");
			}
			if (Settings.Get<int>("SystemCount") >= 99 && ModSecret.ThemeUnlock(6, ShowDoubleHint: false))
			{
				ModMain.MyMsgBox("你已经使用了 99 次 PCL 啦，感谢你长期以来的支持！\r\n隐藏主题 铁杆粉 已解锁！");
			}
		}
		private void UpgradeSub(int LastVersionCode)
		{
			Logger.Info($"版本号从 {LastVersionCode} 升高到 {410}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			Settings.Set("SystemLastVersionReg", 410);
			int num = Settings.Get<int>("SystemHighestAlphaVersionReg");
			if (num < 410)
			{
				Settings.Set("SystemHighestAlphaVersionReg", 410);
				Logger.Info($"最高版本号从 {num} 升高到 {410}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (Settings.Get<int>("LaunchArgumentWindowType") == 5)
			{
				Settings.Set("LaunchArgumentWindowType", 1);
			}
			if (num <= 207)
			{
				List<string> list = new List<string> { "2" };
				list.AddRange(new List<string>(Settings.Get<string>("UiLauncherThemeHide").ToString().Split("|")));
				list.AddRange(new List<string>(Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|")));
				Settings.Set("UiLauncherThemeHide2", list.Distinct().ToList().Join('|'));
			}
			if (LastVersionCode <= 115 && Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|")
				.Contains("13"))
			{
				List<string> list2 = new List<string>(Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|"));
				list2.Remove("13");
				Settings.Set("UiLauncherThemeHide2", list2.Join('|'));
				ModMain.MyMsgBox("由于新版 PCL 修改了欧皇彩的解锁方式，你需要重新解锁欧皇彩。\r\n多谢各位的理解啦！", "重新解锁提醒");
			}
			if (LastVersionCode <= 152 && Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|")
				.Contains("12"))
			{
				List<string> list3 = new List<string>(Settings.Get<string>("UiLauncherThemeHide2").ToString().Split("|"));
				list3.Remove("12");
				Settings.Set("UiLauncherThemeHide2", list3.Join('|'));
				ModMain.MyMsgBox("由于新版 PCL 修改了滑稽彩的解锁方式，你需要重新解锁滑稽彩。\r\n多谢各位的理解啦！", "重新解锁提醒");
			}
			if (LastVersionCode <= 161 && FileUtils.Exists(Paths.Base + "PCL\\CustomSkin.png") && !FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
			{
				FileUtils.Copy(Paths.Base + "PCL\\CustomSkin.png", Paths.AppDataThenName + "CustomSkin.png");
				Logger.Info("已移动离线自定义皮肤 (162)", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (LastVersionCode <= 263 && FileUtils.Exists(ModBase.PathTemp + "CustomSkin.png") && !FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
			{
				FileUtils.Copy(ModBase.PathTemp + "CustomSkin.png", Paths.AppDataThenName + "CustomSkin.png");
				Logger.Info("已移动离线自定义皮肤 (264)", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (LastVersionCode <= 205)
			{
				Settings.Set("UiHiddenOtherHelp", false);
				Logger.Info("已解除帮助页面的隐藏", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (!Settings.Get<bool>("CacheMsV2Migrated"))
			{
				Settings.Set("CacheMsV2Migrated", true);
				Settings.Set("CacheMsV2OAuthRefresh", Settings.Get<string>("CacheMsOAuthRefresh"));
				Settings.Set("CacheMsV2Access", Settings.Get<string>("CacheMsAccess"));
				Settings.Set("CacheMsV2ProfileJson", Settings.Get<string>("CacheMsProfileJson"));
				Settings.Set("CacheMsV2Uuid", Settings.Get<string>("CacheMsUuid"));
				Settings.Set("CacheMsV2Name", Settings.Get<string>("CacheMsName"));
				Logger.Info("已从老版本迁移微软登录结果", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (Settings.HasSaved("ToolDownloadTranslate") && !Settings.HasSaved("ToolDownloadTranslateV2"))
			{
				Settings.Set("ToolDownloadTranslateV2", checked(Settings.Get<int>("ToolDownloadTranslate") + 1));
				Logger.Info("已从老版本迁移 Mod 命名设置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (LastVersionCode <= 381 && Settings.HasSaved("LaunchAdvanceJvm") && Operators.CompareString(Settings.Get<string>("LaunchAdvanceJvm").ToString().Replace("-XX:+UseG1GC ", "")
				.Replace("-XX:-UseAdaptiveSizePolicy ", "")
				.Trim(), Settings.GetDefault("LaunchAdvanceJvm"), TextCompare: false) == 0)
			{
				Settings.Reset("LaunchAdvanceJvm");
				Logger.Info("已重置 JVM 参数设置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			if (LastVersionCode > 0 && num < 410)
			{
				ShowUpdateLog(num);
			}
		}
		private void DowngradeSub(int LastVersionCode)
		{
			Logger.Info($"版本号从 {LastVersionCode} 降低到 {410}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			Settings.Set("SystemLastVersionReg", 410);
		}
		private void FormMain_Closing(object sender, CancelEventArgs e)
		{
			EndProgram(SendWarning: true);
			e.Cancel = true;
		}
		public void EndProgram(bool SendWarning)
		{
			if (ModNet.HasDownloadingTask())
			{
				if (SendWarning && ModMain.MyMsgBox("还有下载任务尚未完成，是否确定退出？", "提示", "确定", "取消") == 2)
				{
					return;
				}
				ModBase.RunInNewThread([SpecialName] () =>
				{
					Logger.Info("正在强行停止任务", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar.ToList())
					{
						item.Cancel();
					}
				}, "强行停止下载任务");
			}
			PageLinkMain frmLinkMain = ModMain.FrmLinkMain;
			if (frmLinkMain != null && frmLinkMain.TryExit(!SendWarning, Closing: true))
			{
				return;
			}
			ModBase.RunInUiWait([SpecialName] () =>
			{
				IsHitTestVisible = false;
				if (PanBack.RenderTransform == null)
				{
					_Closure___9_0 arg = default;
					_Closure___9_0 CS_8__locals13 = new _Closure___9_0(arg);
					CS_8__locals13._VB_Local_TransformPos = new TranslateTransform(0.0, 0.0);
					CS_8__locals13._VB_Local_TransformRotate = new RotateTransform(0.0);
					CS_8__locals13._VB_Local_TransformScale = new ScaleTransform(1.0, 1.0);
					PanBack.RenderTransform = new TransformGroup
					{
						Children = new TransformCollection(new Transform[3] { CS_8__locals13._VB_Local_TransformRotate, CS_8__locals13._VB_Local_TransformPos, CS_8__locals13._VB_Local_TransformScale })
					};
					ModAnimation.AniStart(new ModAnimation.AniData[6]
					{
						ModAnimation.AaOpacity(this, 0.0 - Opacity, 140, 40, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaDouble([SpecialName] (object i) =>
						{
							ScaleTransform scaleTransform;
							(scaleTransform = CS_8__locals13._VB_Local_TransformScale).ScaleX = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleX, i));
							(scaleTransform = CS_8__locals13._VB_Local_TransformScale).ScaleY = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleY, i));
						}, 0.88 - CS_8__locals13._VB_Local_TransformScale.ScaleX, 180),
						ModAnimation.AaDouble([SpecialName] (object i) =>
						{
							TranslateTransform translateTransform;
							(translateTransform = CS_8__locals13._VB_Local_TransformPos).Y = Conversions.ToDouble(Operators.AddObject(translateTransform.Y, i));
						}, 20.0 - CS_8__locals13._VB_Local_TransformPos.Y, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaDouble([SpecialName] (object i) =>
						{
							RotateTransform rotateTransform;
							(rotateTransform = CS_8__locals13._VB_Local_TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(rotateTransform.Angle, i));
						}, 0.6 - CS_8__locals13._VB_Local_TransformRotate.Angle, 180, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaCode([SpecialName] () =>
						{
							IsHitTestVisible = false;
							Top = -10000.0;
							ShowInTaskbar = false;
						}, 210),
						ModAnimation.AaCode([SpecialName] () =>
						{
							EndProgramForce();
						}, 230)
					}, "Form Close");
				}
				else
				{
					EndProgramForce();
				}
				Logger.Info("收到关闭指令", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			});
		}
		public static void EndProgramForce(ModBase.ProcessReturnValues ReturnCode = ModBase.ProcessReturnValues.Success)
		{
			int try0000_dispatch = -1;
			checked
			{
				int num3 = default;
				int num = default;
				int num2 = default;
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
						case 314:
							{
								num = num2;
								switch (num3)
								{
								case 1:
									break;
								default:
									goto end_IL_0000;
								}
								int num4 = unchecked(num + 1);
								num = 0;
								switch (num4)
								{
								case 1:
									break;
								case 2:
									goto IL_0007;
								case 3:
									goto IL_000f;
								case 4:
									goto IL_001d;
								case 5:
									goto IL_0026;
								case 6:
									goto IL_002e;
								case 7:
									goto IL_0034;
								case 8:
									goto IL_003d;
								case 9:
									goto IL_0044;
								case 10:
									goto IL_0057;
								case 11:
									goto IL_0060;
								case 12:
									goto IL_007d;
								case 13:
									goto IL_008a;
								case 14:
									goto IL_00a8;
								case 15:
									goto IL_00b0;
								case 16:
									goto IL_00bd;
								case 17:
									goto IL_00c3;
								case 18:
									goto end_IL_0000_2;
								default:
									goto end_IL_0000;
								case 19:
									goto end_IL_0000_3;
								}
								goto default;
							}
							IL_00c3:
							num2 = 17;
							Environment.Exit(unchecked((int)ReturnCode));
							break;
							IL_0007:
							num2 = 2;
							ModBase.IsProgramEnding = true;
							goto IL_000f;
							IL_000f:
							num2 = 3;
							ModAnimation.AniControlEnabled++;
							goto IL_001d;
							IL_001d:
							num2 = 4;
							if (ModSecret.IsUpdateWaitingRestart)
							{
								goto IL_0026;
							}
							goto IL_002e;
							IL_0026:
							num2 = 5;
							ModSecret.UpdateRestart(TriggerRestartAndByEnd: false);
							goto IL_002e;
							IL_002e:
							num2 = 6;
							if (ReturnCode == ModBase.ProcessReturnValues.Exception)
							{
								goto IL_0034;
							}
							goto IL_008a;
							IL_0034:
							num2 = 7;
							if (!IsLogShown)
							{
								goto IL_003d;
							}
							goto IL_007d;
							IL_003d:
							num2 = 8;
							ModMain.FeedbackInfo();
							goto IL_0044;
							IL_0044:
							num2 = 9;
							Logger.Info("请在 https://github.com/Meloong-Git/PCL/issues 提交错误报告，以便于作者解决此问题！", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							goto IL_0057;
							IL_0057:
							num2 = 10;
							IsLogShown = true;
							goto IL_0060;
							IL_0060:
							num2 = 11;
							ModBase.StartProcess(Paths.Base + "PCL\\Log1.txt");
							goto IL_007d;
							IL_007d:
							num2 = 12;
							Thread.Sleep(500);
							goto IL_008a;
							IL_008a:
							num2 = 13;
							Logger.Info($"程序已退出，返回值：{ReturnCode}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							goto IL_00a8;
							IL_00a8:
							num2 = 14;
							ConfigUtils.SaveAll();
							goto IL_00b0;
							IL_00b0:
							num2 = 15;
							Logger.Instance.Flush();
							goto IL_00bd;
							IL_00bd:
							num2 = 16;
							if (ReturnCode == ModBase.ProcessReturnValues.Success)
							{
								break;
							}
							goto IL_00c3;
							end_IL_0000_2:
							break;
						}
						num2 = 18;
						Process.GetCurrentProcess().Kill();
						break;
						end_IL_0000:;
					}
					catch (Exception ex) when ((num3 != 0) & (num == 0))
					{
						ProjectData.SetProjectError(ex);
						try0000_dispatch = 314;
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
		}
		private void BtnTitleClose_Click(object sender, RoutedEventArgs e)
		{
			EndProgram(SendWarning: true);
		}
		private void FormDragMove(object sender, MouseButtonEventArgs e)
		{
			int try0000_dispatch = -1;
			int num3 = default;
			int num = default;
			int num2 = default;
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
					case 79:
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
								goto end_IL_0000_2;
							default:
								goto end_IL_0000;
							case 4:
								goto end_IL_0000_3;
							}
							goto default;
						}
						IL_0007:
						num2 = 2;
						if (!Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "IsMouseDirectlyOver", new object[0], null, null, null)))
						{
							goto end_IL_0000_3;
						}
						break;
						end_IL_0000_2:
						break;
					}
					num2 = 3;
					DragMove();
					break;
					end_IL_0000:;
				}
				catch (Exception ex) when ((num3 != 0) & (num == 0))
				{
					ProjectData.SetProjectError(ex);
					try0000_dispatch = 79;
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
		private void FormMain_SizeChanged()
		{
			if (IsSizeSaveable)
			{
				Settings.Set("WindowHeight", Height);
				Settings.Set("WindowWidth", Width);
			}
			RectForm.Rect = new Rect(0.0, 0.0, BorderForm.ActualWidth, BorderForm.ActualHeight);
			PanForm.Width = BorderForm.ActualWidth + 0.001;
			PanForm.Height = BorderForm.ActualHeight + 0.001;
			PanMain.Width = PanForm.Width;
			PanMain.Height = Math.Max(0.0, PanForm.Height - PanTitle.ActualHeight);
			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
			}
		}
		private void BtnTitleMin_Click()
		{
			WindowState = WindowState.Minimized;
		}
		public static void UpdateBackgroundAndTitleBar(object Value)
		{
			if (ModMain.FrmMain != null && ModMain.FrmMain.IsLoaded)
			{
				ModMain.FrmMain.UpdateBackgroundAndTitleBar();
			}
		}
		public void UpdateBackgroundAndTitleBar()
		{
			Logger.Info("从设置更新背景图片与标题栏样式", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			ImgBack.Opacity = (double)Settings.Get<int>("UiBackgroundOpacity") / 1000.0;
			double num = checked(Settings.Get<int>("UiBackgroundBlur") + 1);
			if (num == 1.0)
			{
				ImgBack.Effect = null;
			}
			else
			{
				ImgBack.Effect = new BlurEffect
				{
					Radius = num
				};
			}
			ImgBack.Margin = new Thickness((0.0 - num) / 1.8);
			int num2 = Settings.Get<int>("UiBackgroundSuit");
			if (ImgBack.Background != null)
			{
				ImageBrush imageBrush = (ImageBrush)ImgBack.Background;
				if (num2 == 0)
				{
					num2 = ((!(imageBrush.ImageSource.Width < PanMain.ActualWidth / 2.0) || !(imageBrush.ImageSource.Height < PanMain.ActualHeight / 2.0)) ? 2 : 4);
				}
				imageBrush.Stretch = Stretch.UniformToFill;
				imageBrush.TileMode = TileMode.None;
				imageBrush.Viewport = new Rect(0.0, 0.0, 1.0, 1.0);
				imageBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
				imageBrush.AlignmentX = AlignmentX.Center;
				imageBrush.AlignmentY = AlignmentY.Center;
				switch (num2)
				{
				case 1:
					imageBrush.Stretch = Stretch.None;
					break;
				case 3:
					imageBrush.Stretch = Stretch.Fill;
					break;
				case 4:
					imageBrush.Stretch = Stretch.None;
					imageBrush.TileMode = TileMode.Tile;
					imageBrush.Viewport = new Rect(0.0, 0.0, imageBrush.ImageSource.Width, imageBrush.ImageSource.Height);
					imageBrush.ViewportUnits = BrushMappingMode.Absolute;
					break;
				case 5:
					imageBrush.AlignmentX = AlignmentX.Left;
					imageBrush.AlignmentY = AlignmentY.Top;
					break;
				case 6:
					imageBrush.AlignmentX = AlignmentX.Right;
					imageBrush.AlignmentY = AlignmentY.Top;
					break;
				case 7:
					imageBrush.AlignmentX = AlignmentX.Left;
					imageBrush.AlignmentY = AlignmentY.Bottom;
					break;
				case 8:
					imageBrush.AlignmentX = AlignmentX.Right;
					imageBrush.AlignmentY = AlignmentY.Bottom;
					break;
				case 9:
					imageBrush.AlignmentX = AlignmentX.Left;
					imageBrush.AlignmentY = AlignmentY.Center;
					break;
				case 10:
					imageBrush.AlignmentX = AlignmentX.Right;
					imageBrush.AlignmentY = AlignmentY.Center;
					break;
				case 11:
					imageBrush.AlignmentX = AlignmentX.Center;
					imageBrush.AlignmentY = AlignmentY.Top;
					break;
				case 12:
					imageBrush.AlignmentX = AlignmentX.Center;
					imageBrush.AlignmentY = AlignmentY.Bottom;
					break;
				}
			}
			switch (Settings.Get<int>("UiLogoType"))
			{
			case 0:
				ShapeTitleLogo.Visibility = Visibility.Collapsed;
				LabTitleLogo.Visibility = Visibility.Collapsed;
				ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				break;
			case 1:
				ShapeTitleLogo.Visibility = Visibility.Visible;
				LabTitleLogo.Visibility = Visibility.Collapsed;
				ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				break;
			case 2:
				ShapeTitleLogo.Visibility = Visibility.Collapsed;
				LabTitleLogo.Visibility = Visibility.Visible;
				ImageTitleLogo.Visibility = Visibility.Collapsed;
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
				}
				break;
			case 3:
				ShapeTitleLogo.Visibility = Visibility.Collapsed;
				LabTitleLogo.Visibility = Visibility.Collapsed;
				ImageTitleLogo.Visibility = Visibility.Visible;
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Visible;
				}
				try
				{
					ImageTitleLogo.Source = Paths.Base + "PCL\\Logo.png";
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ImageTitleLogo.Source = null;
					Logger.Error(ex2, "显示标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
				break;
			}
			ModMain.FrmSetupUI?.CardLogo.TriggerForceResize();
			LabTitleLogo.Text = Settings.Get<string>("UiLogoText");
			PanTitleMain.ColumnDefinitions[0].Width = new GridLength((!Settings.Get<bool>("UiLogoLeft") || Settings.Get<int>("UiLogoType") != 0) ? 1 : 0, GridUnitType.Star);
		}
		private void FormMain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.IsRepeat)
			{
				return;
			}
			if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
			{
				e.Handled = true;
			}
			if (PanMsg.Children.Any())
			{
				if (e.Key == Key.Return)
				{
					NewLateBinding.LateCall(PanMsg.Children[0], null, "Btn1_Click", new object[0], null, null, null, IgnoreReturn: true);
				}
				else if (e.Key == Key.Escape || (e.Key == Key.W && System.Windows.Input.Keyboard.Modifiers.HasFlag(ModifierKeys.Control)))
				{
					object obj = PanMsg.Children[0];
					if (!(obj is MyMsgInput) && !(obj is MyMsgSelect) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn3", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, TextCompare: false))
					{
						NewLateBinding.LateCall(obj, null, "Btn3_Click", new object[0], null, null, null, IgnoreReturn: true);
					}
					else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn2", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, TextCompare: false))
					{
						NewLateBinding.LateCall(obj, null, "Btn2_Click", new object[0], null, null, null, IgnoreReturn: true);
					}
					else
					{
						NewLateBinding.LateCall(obj, null, "Btn1_Click", new object[0], null, null, null, IgnoreReturn: true);
					}
				}
				return;
			}
			if (e.Key == Key.Escape)
			{
				TriggerPageBack();
			}
			if (e.Key == Key.F11 && PageCurrent == PageType.InstanceSelect)
			{
				ModMain.FrmSelectRight.ShowHidden = !ModMain.FrmSelectRight.ShowHidden;
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			else if (e.Key == Key.F12)
			{
				PageSetupUI.HiddenForceShow = !PageSetupUI.HiddenForceShow;
				if (PageSetupUI.HiddenForceShow)
				{
					ModMain.Hint("功能隐藏设置已暂时关闭！", ModMain.HintType.Green);
				}
				else
				{
					ModMain.Hint("功能隐藏设置已重新开启！", ModMain.HintType.Green);
				}
				PageSetupUI.HiddenRefresh();
			}
			else if (e.Key == Key.F5)
			{
				if (PageLeft is IRefreshable)
				{
					((IRefreshable)PageLeft).Refresh();
				}
				if (PageRight is IRefreshable)
				{
					((IRefreshable)PageRight).Refresh();
				}
			}
			else if (e.Key == Key.W && System.Windows.Input.Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
			{
				EndProgram(SendWarning: true);
			}
			else if (e.Key == Key.Return && PageCurrent == PageType.Launch)
			{
				if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
				{
					ModMain.Hint("木大！");
				}
				else
				{
					ModMain.FrmLaunchLeft.LaunchButtonClick();
				}
			}
		}
		private void FormMain_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (ModMain.FrmMain.PanMsg.Children.Count <= 0 && !ModMain.WaitingMyMsgBox.Any() && (e.ChangedButton == MouseButton.XButton1 || e.ChangedButton == MouseButton.XButton2))
			{
				TriggerPageBack();
			}
		}
		private void TriggerPageBack()
		{
			if (PageCurrent == PageType.Download && PageCurrentSub == PageSubType.Default && ModMain.FrmDownloadInstall.IsInSelectPage)
			{
				ModMain.FrmDownloadInstall.ExitSelectPage();
			}
			else
			{
				PageBack();
			}
		}
		private void FormMain_Activated()
		{
			_Closure___22_0 obj = new _Closure___22_0();
			try
			{
				if (PageCurrent == PageType.InstanceSetup && PageCurrentSub == PageSubType.DownloadMod)
				{
					ModMain.FrmInstanceMod.ReloadModList();
				}
				else if (PageCurrent == PageType.InstanceSetup && (PageCurrentSub == PageSubType.SetupLink || PageCurrentSub == PageSubType.DownloadDataPack))
				{
					PageInstanceLeft.ReloadCurrentJava();
				}
				else if (PageCurrent == PageType.InstanceSelect)
				{
					ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "切回窗口时出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void HandleDrag(object sender, System.Windows.DragEventArgs e)
		{
			try
			{
				if (e.Handled && e.Effects != System.Windows.DragDropEffects.None)
				{
					return;
				}
				e.Handled = true;
				if (e.Data == _STATIC_HandleDrag_20211C1284AD_PrevData)
				{
					e.Effects = _STATIC_HandleDrag_20211C1284AD_PrevEffects;
					return;
				}
				e.Effects = System.Windows.DragDropEffects.None;
				if (e.Data.GetDataPresent(System.Windows.DataFormats.Text))
				{
					string value = Conversions.ToString(e.Data.GetData(System.Windows.DataFormats.Text));
					if (value.StartsWithF("authlib-injector:yggdrasil-server:"))
					{
						e.Effects = System.Windows.DragDropEffects.Copy;
					}
					else if (value.StartsWithF("file:///"))
					{
						e.Effects = System.Windows.DragDropEffects.Copy;
					}
				}
				else if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
				{
					string[] array = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
					if (array != null && array.Length > 0)
					{
						e.Effects = System.Windows.DragDropEffects.Link;
					}
				}
				_STATIC_HandleDrag_20211C1284AD_PrevData = e.Data;
				_STATIC_HandleDrag_20211C1284AD_PrevEffects = e.Effects;
				Logger.Info($"设置拖放类型：{e.Effects}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "处理拖放时出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void FrmMain_Drop(object sender, System.Windows.DragEventArgs e)
		{
			try
			{
				ShowWindowToTop();
				if (e.Data.GetDataPresent(System.Windows.DataFormats.Text))
				{
					try
					{
						string text = Conversions.ToString(e.Data.GetData(System.Windows.DataFormats.Text));
						Logger.Info($"接受文本拖拽：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						if (text.StartsWithF("authlib-injector:yggdrasil-server:"))
						{
							e.Handled = true;
							e.Effects = System.Windows.DragDropEffects.Copy;
							string text2 = StringUtils.FormUrlUnescape(text.Substring("authlib-injector:yggdrasil-server:".Length));
							Logger.Info($"Authlib 拖拽：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							if (!string.IsNullOrEmpty(new ValidateHttp().Validate(text2)))
							{
								ModMain.Hint($"输入的 Authlib 验证服务器不符合网址格式（{text2}）！", ModMain.HintType.Red);
							}
							else
							{
								ModMinecraft.McInstance mcInstance = ((PageCurrent == PageType.InstanceSetup) ? PageInstanceLeft.Instance : ModMinecraft.McInstanceSelected);
								if ((object)mcInstance == null)
								{
									ModMain.Hint("请先下载游戏，再设置第三方登录！", ModMain.HintType.Red);
								}
								else
								{
									if (Operators.CompareString(text2, "https://littleskin.cn/api/yggdrasil", TextCompare: false) == 0)
									{
										if (ModMain.MyMsgBox($"是否要在版本 {mcInstance.Name} 中开启 LittleSkin 登录？" + "\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消") == 2)
										{
											return;
										}
										Settings.Set("VersionServerLogin", 4, mcInstance);
										Settings.Set("VersionServerAuthServer", "https://littleskin.cn/api/yggdrasil", mcInstance);
										Settings.Set("VersionServerAuthRegister", "https://littleskin.cn/auth/register", mcInstance);
										Settings.Set("VersionServerAuthName", "LittleSkin 登录", mcInstance);
									}
									else
									{
										if (ModMain.MyMsgBox($"是否要在版本 {mcInstance.Name} 中开启第三方登录？" + "\r\n" + $"登录服务器：{text2}" + "\r\n\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消") == 2)
										{
											return;
										}
										Settings.Set("VersionServerLogin", 4, mcInstance);
										Settings.Set("VersionServerAuthServer", text2, mcInstance);
										Settings.Set("VersionServerAuthRegister", text2.Replace("api/yggdrasil", "auth/register"), mcInstance);
										Settings.Set("VersionServerAuthName", "", mcInstance);
									}
									if (PageCurrent == PageType.InstanceSetup && PageCurrentSub == PageSubType.SetupLink)
									{
										ModMain.FrmInstanceSetup.Reload();
									}
									else if (PageCurrent == PageType.Launch)
									{
										ModMain.FrmLaunchLeft.RefreshPage(KeepInput: true, Anim: false);
									}
								}
							}
						}
						else if (text.StartsWithF("file:///"))
						{
							string item = WebUtility.UrlDecode(text).Substring("file:///".Length).Replace("/", "\\");
							e.Handled = true;
							e.Effects = System.Windows.DragDropEffects.Copy;
							FileDrag(new List<string> { item });
						}
						return;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "无法接取文本拖拽事件", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						ProjectData.ClearProjectError();
						return;
					}
				}
				if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(e.Data.GetData(System.Windows.DataFormats.FileDrop));
					if (objectValue == null)
					{
						ModMain.Hint("请将文件解压后再拖入！", ModMain.HintType.Red);
						return;
					}
					e.Handled = true;
					e.Effects = System.Windows.DragDropEffects.Link;
					FileDrag((IEnumerable<string>)objectValue);
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "接取拖拽事件失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void FileDrag(IEnumerable<string> FilePathList)
		{
			_Closure___25_0 arg = default;
			_Closure___25_0 CS_8__locals11 = new _Closure___25_0(arg);
			CS_8__locals11._VB_Local_FilePathList = FilePathList;
			ModBase.RunInNewThread([SpecialName] () =>
			{
				_Closure___25_1 arg2 = default;
				_Closure___25_1 CS_8__locals15 = new _Closure___25_1(arg2);
				string text = CS_8__locals11._VB_Local_FilePathList.First();
				Logger.Warn(string.Format("接受文件拖拽：{0}{1}", text, CS_8__locals11._VB_Local_FilePathList.Any() ? $" 等 {CS_8__locals11._VB_Local_FilePathList.Count()} 个文件" : ""), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				if (DirectoryUtils.Exists(CS_8__locals11._VB_Local_FilePathList.First()) && !FileUtils.Exists(CS_8__locals11._VB_Local_FilePathList.First()))
				{
					ModMain.Hint("请拖入一个文件，而非文件夹！", ModMain.HintType.Red);
				}
				else if (!FileUtils.Exists(CS_8__locals11._VB_Local_FilePathList.First()))
				{
					ModMain.Hint("拖入的文件不存在：" + CS_8__locals11._VB_Local_FilePathList.First(), ModMain.HintType.Red);
				}
				else
				{
					List<string> list = CS_8__locals11._VB_Local_FilePathList.ToList();
					if (CS_8__locals11._VB_Local_FilePathList.Count() > 1)
					{
						foreach (string item in list)
						{
							if (!new string[4] { "jar", "litemod", "disabled", "old" }.Contains(item.AfterLast(".").Lower()))
							{
								ModMain.Hint("一次请只拖入一个文件！", ModMain.HintType.Red);
								return;
							}
						}
					}
					CS_8__locals15._VB_Local_Extension = text.AfterLast(".").Lower();
					if (Operators.CompareString(CS_8__locals15._VB_Local_Extension, "xaml", TextCompare: false) == 0)
					{
						Logger.Info("文件后缀为 XAML，作为主页加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("已存在一个主页文件，是否要将它覆盖？", "覆盖确认", "覆盖", "取消") != 2)
						{
							FileUtils.Copy(text, Paths.Base + "PCL\\Custom.xaml");
							ModBase.RunInUi([SpecialName] () =>
							{
								Settings.Set("UiCustomType", 1);
								ModMain.FrmLaunchRight.ForceRefresh();
								ModMain.Hint("已加载主页自定义文件！", ModMain.HintType.Green);
							});
						}
					}
					else if (!PageInstanceMod.InstallMods(list))
					{
						if (new string[3] { "zip", "rar", "mrpack" }.Any([SpecialName] (string t) => Operators.CompareString(t, CS_8__locals15._VB_Local_Extension, TextCompare: false) == 0))
						{
							Logger.Info("文件为压缩包，尝试作为整合包安装", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							try
							{
								ModModpack.ModpackInstall(text);
								ModBase.RunInUi([SpecialName] () =>
								{
									ModMain.FrmMain.PageChange(PageType.TaskManager);
								});
								return;
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								if (ex2.IsCanceled())
								{
									ProjectData.ClearProjectError();
									return;
								}
								ProjectData.ClearProjectError();
							}
						}
						if (Operators.CompareString(CS_8__locals15._VB_Local_Extension, "rar", TextCompare: false) == 0)
						{
							ModMain.Hint("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试！");
						}
						else
						{
							try
							{
								Logger.Info("尝试进行错误报告分析", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
								CrashAnalyzer crashAnalyzer = new CrashAnalyzer();
								crashAnalyzer.Import(text);
								if (crashAnalyzer.Prepare())
								{
									crashAnalyzer.Analyze();
									crashAnalyzer.Output(IsManualAnalyze: true, new List<string>());
									return;
								}
							}
							catch (Exception ex3)
							{
								ProjectData.SetProjectError(ex3);
								Exception ex4 = ex3;
								Logger.Error(ex4, "自主错误报告分析失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
								ProjectData.ClearProjectError();
							}
							ModMain.Hint("PCL 无法确定应当执行的文件拖拽操作……");
						}
					}
				}
			}, "文件拖拽");
		}
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch (msg)
			{
			case 30:
			{
				DateTime now = DateTime.Now;
				if (DateTime.Compare(now.Date, ModBase.ApplicationOpenTime.Date) == 0)
				{
					Logger.Info($"系统时间微调为：{now.ToLongDateString()} {now.ToLongTimeString()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					IsSystemTimeChanged = false;
				}
				else
				{
					Logger.Info($"系统时间修改为：{now.ToLongDateString()} {now.ToLongTimeString()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					IsSystemTimeChanged = true;
				}
				break;
			}
			case 6402:
				Logger.Info($"收到置顶信息：{hwnd.ToInt64()}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				if (!IsWindowLoadFinished)
				{
					Logger.Info("窗口尚未加载完成，忽略置顶请求", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					return IntPtr.Zero;
				}
				ShowWindowToTop();
				handled = true;
				break;
			}
			return IntPtr.Zero;
		}
		public void ShowWindowToTop()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				Visibility = Visibility.Visible;
				ShowInTaskbar = true;
				WindowState = WindowState.Normal;
				Hidden = false;
				Topmost = true;
				Topmost = false;
				ModMain.SetForegroundWindow(ModBase.Handle);
				Focus();
				Logger.Info($"窗口已置顶，位置：({Left}, {Top}), {Width} x {Height}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			});
		}
		private string PageNameGet(PageStackData Stack)
		{
			return Stack.Page switch
			{
				PageType.InstanceSelect => "版本选择", 
				PageType.TaskManager => "任务管理", 
				PageType.InstanceSetup => "版本设置 - " + (((object)PageInstanceLeft.Instance == null) ? "未知版本" : PageInstanceLeft.Instance.Name), 
				PageType.ResourceDetail => ((ResourceProject)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 0 }, null)).TranslatedName, 
				PageType.HelpDetail => ((ModMain.HelpEntry)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 0 }, null)).Title, 
				_ => "", 
			};
		}
		public void PageNameRefresh(PageStackData Type)
		{
			LabTitleInner.Text = PageNameGet(Type);
		}
		public void PageNameRefresh()
		{
			PageNameRefresh(PageCurrent);
		}
		public void PageChange(PageStackData Stack, PageSubType SubType = PageSubType.Default)
		{
			if (Operators.CompareString(PageNameGet(Stack), "", TextCompare: false) == 0)
			{
				PageChangeExit();
				IsChangingPage = true;
				((MyRadioButton)PanTitleSelect.Children[(int)(PageType)Stack]).SetChecked(value: true, raiseByMouse: true, Operators.CompareString(PageNameGet(PageCurrent), "", TextCompare: false) == 0);
				IsChangingPage = false;
				switch (Stack.Page)
				{
				case PageType.Download:
					if (ModMain.FrmDownloadLeft == null)
					{
						ModMain.FrmDownloadLeft = new PageDownloadLeft();
					}
					((MyListItem)ModMain.FrmDownloadLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
					break;
				case PageType.Setup:
					if (ModMain.FrmSetupLeft == null)
					{
						ModMain.FrmSetupLeft = new PageSetupLeft();
					}
					((MyListItem)ModMain.FrmSetupLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
					break;
				case PageType.Other:
					if (ModMain.FrmOtherLeft == null)
					{
						ModMain.FrmOtherLeft = new PageOtherLeft();
					}
					((MyListItem)ModMain.FrmOtherLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
					break;
				}
				PageChangeActual(Stack, SubType);
				return;
			}
			PageType page = Stack.Page;
			if (page == PageType.InstanceSetup)
			{
				if (ModMain.FrmInstanceLeft == null)
				{
					ModMain.FrmInstanceLeft = new PageInstanceLeft();
				}
				((MyListItem)ModMain.FrmInstanceLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
			}
			PageChangeActual(Stack, SubType);
		}
		private void BtnTitleSelect_Click(MyRadioButton sender, bool raiseByMouse)
		{
			if (!IsChangingPage)
			{
				PageChangeActual((PageType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
			}
		}
		public void PageBack()
		{
			if (PageStack.Any())
			{
				PageChangeActual(PageStack[0]);
			}
			else
			{
				PageChange(PageType.Launch);
			}
		}
		private void PageChangeActual(PageStackData Stack, PageSubType SubType = (PageSubType)(-1))
		{
			if (PageCurrent == Stack && (PageCurrentSub == SubType || SubType == (PageSubType)(-1)))
			{
				return;
			}
			checked
			{
				ModAnimation.AniControlEnabled++;
				try
				{
					_Closure___50_0 arg = default;
					_Closure___50_0 CS_8__locals5 = new _Closure___50_0(arg);
					CS_8__locals5._VB_Me = this;
					CS_8__locals5._VB_Local_PageName = PageNameGet(Stack);
					if (Operators.CompareString(CS_8__locals5._VB_Local_PageName, "", TextCompare: false) == 0)
					{
						PageChangeExit();
					}
					else if (PageStack.Any())
					{
						ModAnimation.AniStart(new ModAnimation.AniData[3]
						{
							ModAnimation.AaOpacity(LabTitleInner, 0.0 - LabTitleInner.Opacity, 130),
							ModAnimation.AaCode([SpecialName] () =>
							{
								CS_8__locals5._VB_Me.LabTitleInner.Text = CS_8__locals5._VB_Local_PageName;
							}, 0, After: true),
							ModAnimation.AaOpacity(LabTitleInner, 1.0, 150, 30)
						}, "FrmMain Titlebar SubLayer");
						if (PageStack.Contains(Stack))
						{
							while (PageStack.Contains(Stack))
							{
								PageStack.RemoveAt(0);
							}
						}
						else
						{
							PageStack.Insert(0, PageCurrent);
						}
					}
					else
					{
						PanTitleInner.Visibility = Visibility.Visible;
						PanTitleMain.IsHitTestVisible = false;
						PanTitleInner.IsHitTestVisible = true;
						PageNameRefresh(Stack);
						ModAnimation.AniStart(new ModAnimation.AniData[5]
						{
							ModAnimation.AaOpacity(PanTitleMain, 0.0 - PanTitleMain.Opacity, 150),
							ModAnimation.AaX(PanTitleMain, 12.0 - PanTitleMain.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
							ModAnimation.AaOpacity(PanTitleInner, 1.0 - PanTitleInner.Opacity, 150, 200),
							ModAnimation.AaX(PanTitleInner, 0.0 - PanTitleInner.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack()),
							ModAnimation.AaCode([SpecialName] () =>
							{
								PanTitleMain.Visibility = Visibility.Collapsed;
							}, 0, After: true)
						}, "FrmMain Titlebar FirstLayer");
						PageStack.Insert(0, PageCurrent);
					}
					PageLast = PageCurrent;
					PageCurrent = Stack;
					switch (Stack.Page)
					{
					case PageType.Launch:
						PageChangeAnim(ModMain.FrmLaunchLeft, ModMain.FrmLaunchRight);
						break;
					case PageType.Download:
						if (ModMain.FrmDownloadLeft == null)
						{
							ModMain.FrmDownloadLeft = new PageDownloadLeft();
						}
						PageChangeAnim(ModMain.FrmDownloadLeft, (FrameworkElement)ModMain.FrmDownloadLeft.PageGet(SubType));
						break;
					case PageType.Link:
						if (ModMain.FrmLinkMain == null)
						{
							ModMain.FrmLinkMain = new PageLinkMain();
						}
						PageChangeAnim(new MyPageLeft(), ModMain.FrmLinkMain);
						break;
					case PageType.Setup:
						if (ModMain.FrmSetupLeft == null)
						{
							ModMain.FrmSetupLeft = new PageSetupLeft();
						}
						PageChangeAnim(ModMain.FrmSetupLeft, (FrameworkElement)ModMain.FrmSetupLeft.PageGet(SubType));
						break;
					case PageType.Other:
						if (ModMain.FrmOtherLeft == null)
						{
							ModMain.FrmOtherLeft = new PageOtherLeft();
						}
						PageChangeAnim(ModMain.FrmOtherLeft, (FrameworkElement)ModMain.FrmOtherLeft.PageGet(SubType));
						break;
					case PageType.InstanceSelect:
						if (ModMain.FrmSelectLeft == null)
						{
							ModMain.FrmSelectLeft = new PageSelectLeft();
						}
						if (ModMain.FrmSelectRight == null)
						{
							ModMain.FrmSelectRight = new PageSelectRight();
						}
						PageChangeAnim(ModMain.FrmSelectLeft, ModMain.FrmSelectRight);
						break;
					case PageType.TaskManager:
						if (ModMain.FrmSpeedLeft == null)
						{
							ModMain.FrmSpeedLeft = new PageSpeedLeft();
						}
						if (ModMain.FrmSpeedRight == null)
						{
							ModMain.FrmSpeedRight = new PageSpeedRight();
						}
						PageChangeAnim(ModMain.FrmSpeedLeft, ModMain.FrmSpeedRight);
						break;
					case PageType.InstanceSetup:
						if (ModMain.FrmInstanceLeft == null)
						{
							ModMain.FrmInstanceLeft = new PageInstanceLeft();
						}
						PageChangeAnim(ModMain.FrmInstanceLeft, (FrameworkElement)ModMain.FrmInstanceLeft.PageGet(SubType));
						break;
					case PageType.ResourceDetail:
						if (ModMain.FrmDownloadResourceDetail == null)
						{
							ModMain.FrmDownloadResourceDetail = new PageDownloadResourceDetail();
						}
						PageChangeAnim(new MyPageLeft(), ModMain.FrmDownloadResourceDetail);
						break;
					case PageType.HelpDetail:
						PageChangeAnim(new MyPageLeft(), (FrameworkElement)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 1 }, null));
						break;
					}
					BtnExtraDownload.ShowRefresh();
					BtnExtraApril.ShowRefresh();
					Logger.Info($"切换主要页面：{Stack.Page}, {SubType}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"切换主要页面失败（ID {PageCurrent.Page}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
				finally
				{
					ModAnimation.AniControlEnabled--;
				}
			}
		}
		private void PageChangeAnim(FrameworkElement TargetLeft, FrameworkElement TargetRight)
		{
			ModAnimation.AniStop("FrmMain LeftChange");
			ModAnimation.AniStop("PageLeft PageChange");
			checked
			{
				ModAnimation.AniControlEnabled++;
				if (TargetLeft.Parent != null)
				{
					TargetLeft.SetValue(ContentPresenter.ContentProperty, null);
				}
				if (TargetRight != null && TargetRight.Parent != null)
				{
					TargetRight.SetValue(ContentPresenter.ContentProperty, null);
				}
				PageLeft = (MyPageLeft)TargetLeft;
				PageRight = (MyPageRight)TargetRight;
				((MyPageLeft)PanMainLeft.Child).TriggerHideAnimation();
				((MyPageRight)PanMainRight.Child).PageOnExit();
				ModAnimation.AniControlEnabled--;
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaCode([SpecialName] () =>
					{
						ModAnimation.AniControlEnabled++;
						PanMainLeft.Child = PageLeft;
						PageLeft.Opacity = 0.0;
						PanMainLeft.Background = null;
						ModAnimation.AniControlEnabled--;
						ModBase.RunInUi([SpecialName] () =>
						{
							PanMainLeft_Resize(PanMainLeft.ActualWidth);
						}, ForceWaitUntilLoaded: true);
					}, 110),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PageLeft.Opacity = 1.0;
						PageLeft.TriggerShowAnimation();
					}, 30, After: true)
				}, "FrmMain PageChangeLeft");
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaCode([SpecialName] () =>
					{
						ModAnimation.AniControlEnabled++;
						((MyPageRight)PanMainRight.Child).PageOnForceExit();
						PanMainRight.Child = PageRight;
						PageRight.Opacity = 0.0;
						PanMainRight.Background = null;
						ModAnimation.AniControlEnabled--;
						ModBase.RunInUi([SpecialName] () =>
						{
							BtnExtraBack.ShowRefresh();
						}, ForceWaitUntilLoaded: true);
					}, 110),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PageRight.Opacity = 1.0;
						PageRight.PageOnEnter();
					}, 30, After: true)
				}, "FrmMain PageChangeRight");
			}
		}
		private void PageChangeExit()
		{
			if (PageStack.Any())
			{
				PanTitleMain.Visibility = Visibility.Visible;
				PanTitleMain.IsHitTestVisible = true;
				PanTitleInner.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[5]
				{
					ModAnimation.AaOpacity(PanTitleInner, 0.0 - PanTitleInner.Opacity, 150),
					ModAnimation.AaX(PanTitleInner, -18.0 - PanTitleInner.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent()),
					ModAnimation.AaOpacity(PanTitleMain, 1.0 - PanTitleMain.Opacity, 150, 200),
					ModAnimation.AaX(PanTitleMain, 0.0 - PanTitleMain.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PanTitleInner.Visibility = Visibility.Collapsed;
					}, 0, After: true)
				}, "FrmMain Titlebar FirstLayer");
				PageStack.Clear();
			}
		}
		private void PanMainLeft_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (e.WidthChanged)
			{
				PanMainLeft_Resize(e.NewSize.Width);
			}
		}
		private void PanMainLeft_Resize(double NewWidth)
		{
			if (Math.Abs(NewWidth - RectLeftBackground.Width) > 0.1 && ModAnimation.AniControlEnabled == 0)
			{
				if (PanMain.Opacity < 0.1)
				{
					PanMainLeft.IsHitTestVisible = false;
				}
				if (NewWidth > 0.0)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaWidth(RectLeftBackground, NewWidth - RectLeftBackground.Width, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)),
						ModAnimation.AaOpacity(RectLeftShadow, 1.0 - RectLeftShadow.Opacity, 180),
						ModAnimation.AaCode([SpecialName] () =>
						{
							PanMainLeft.IsHitTestVisible = true;
						}, 150)
					}, "FrmMain LeftChange", RefreshTime: true);
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaWidth(RectLeftBackground, 0.0 - RectLeftBackground.Width, 180, 0, new ModAnimation.AniEaseOutFluent()),
						ModAnimation.AaOpacity(RectLeftShadow, 0.0 - RectLeftShadow.Opacity, 180),
						ModAnimation.AaCode([SpecialName] () =>
						{
							PanMainLeft.IsHitTestVisible = true;
						}, 150)
					}, "FrmMain LeftChange", RefreshTime: true);
				}
			}
			else
			{
				RectLeftBackground.Width = NewWidth;
				PanMainLeft.IsHitTestVisible = true;
				ModAnimation.AniStop("FrmMain LeftChange");
			}
		}
		public void DragTick()
		{
			if (ModMain.DragControl != null && System.Windows.Input.Mouse.LeftButton != MouseButtonState.Pressed)
			{
				DragStop();
			}
		}
		public void DragDoing()
		{
			if (ModMain.DragControl != null)
			{
				if (System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed)
				{
					NewLateBinding.LateCall(ModMain.DragControl, null, "DragDoing", new object[0], null, null, null, IgnoreReturn: true);
				}
				else
				{
					DragStop();
				}
			}
		}
		public void DragStop()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				if (ModMain.DragControl != null)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(ModMain.DragControl);
					ModMain.DragControl = null;
					NewLateBinding.LateCall(objectValue, null, "DragStop", new object[0], null, null, null, IgnoreReturn: true);
				}
			});
		}
		private void BtnExtraMusic_Click(object sender, EventArgs e)
		{
			ModMusic.MusicControlPause();
		}
		private void BtnExtraMusic_RightClick(object sender, EventArgs e)
		{
			ModMusic.MusicControlNext();
		}
		private void BtnExtraDownload_Click(object sender, EventArgs e)
		{
			PageChange(PageType.TaskManager);
		}
		private bool BtnExtraDownload_ShowCheck()
		{
			if (ModNet.HasDownloadingTask())
			{
				return !(PageCurrent == PageType.TaskManager);
			}
			return false;
		}
		public void AprilGiveup()
		{
			if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
			{
				ModMain.Hint("=D", ModMain.HintType.Green);
				ModMain.IsAprilGiveup = true;
				ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleX = 1.0;
				ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleY = 1.0;
				BtnExtraApril.ShowRefresh();
			}
		}
		public bool BtnExtraApril_ShowCheck()
		{
			if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
			{
				return PageCurrent == PageType.Launch;
			}
			return false;
		}
		public void BtnExtraShutdown_Click()
		{
			try
			{
				if (ModLaunch.McLaunchLoaderReal != null)
				{
					ModLaunch.McLaunchLoaderReal.Cancel();
				}
				foreach (ModWatcher.Watcher mcWatcher in ModWatcher.McWatcherList)
				{
					mcWatcher.Kill();
				}
				ModMain.Hint("已关闭运行中的 Minecraft！", ModMain.HintType.Green);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "强制关闭所有 Minecraft 失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		public bool BtnExtraShutdown_ShowCheck()
		{
			return ModWatcher.HasRunningMinecraft;
		}
		public void BackToTop()
		{
			MyScrollViewer myScrollViewer = BtnExtraBack_GetRealChild();
			if (myScrollViewer != null)
			{
				myScrollViewer.PerformVerticalOffsetDelta(0.0 - myScrollViewer.VerticalOffset);
			}
			else
			{
				Logger.Error("无法返回顶部，未找到合适的 RealScroll", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
		}
		private bool BtnExtraBack_ShowCheck()
		{
			MyScrollViewer myScrollViewer = BtnExtraBack_GetRealChild();
			if (myScrollViewer != null && myScrollViewer.Visibility == Visibility.Visible)
			{
				return myScrollViewer.VerticalOffset > Height + (double)((!BtnExtraBack.Show) ? 700 : 0);
			}
			return false;
		}
		private MyScrollViewer BtnExtraBack_GetRealChild()
		{
			if (PanMainRight.Child == null || !(PanMainRight.Child is MyPageRight))
			{
				return null;
			}
			MyPageRight myPageRight = (MyPageRight)PanMainRight.Child;
			return (MyScrollViewer)myPageRight.FindName(myPageRight.PanScroll);
		}
		private void FormMain_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			lastMouseArg = e;
		}
		[SpecialName]
		[CompilerGenerated]
		private void _Lambda___3_0()
		{
			WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
			_STATIC_ctor_2001_Helper.HwndIntPtrSource = HwndSource.FromHwnd(windowInteropHelper.Handle);
			_STATIC_ctor_2001_Helper.AddHook();
		}
		[SpecialName]
		[CompilerGenerated]
		private void _Lambda___3_1()
		{
			_STATIC_ctor_2001_Helper.RemoveDragHook();
		}
		[SpecialName]
		[CompilerGenerated]
		private void _Lambda___3_2()
		{
			FileDrag(_STATIC_ctor_2001_Helper.DropFilePaths);
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.MouseEventHandler value2 = [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
						{
							DragDoing();
						};
						Grid grid = PanBack;
						if (grid != null)
						{
							grid.MouseMove -= value2;
						}
						grid = PanBack;
						if (grid != null)
						{
							grid.MouseMove += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = FormDragMove;
						Grid grid = PanTitle;
						if (grid != null)
						{
							grid.MouseLeftButtonDown -= value2;
						}
						grid = PanTitle;
						if (grid != null)
						{
							grid.MouseLeftButtonDown += value2;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnTitleClose_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
						};
						MyIconButton myIconButton = BtnTitleClose;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnTitleClose;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnTitleMin_Click();
						};
						MyIconButton myIconButton = BtnTitleMin;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnTitleMin;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							BtnTitleSelect_Click((MyRadioButton)a0, a1);
						};
						MyRadioButton myRadioButton = BtnTitleSelect0;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = BtnTitleSelect0;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							BtnTitleSelect_Click((MyRadioButton)a0, a1);
						};
						MyRadioButton myRadioButton = BtnTitleSelect1;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = BtnTitleSelect1;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							BtnTitleSelect_Click((MyRadioButton)a0, a1);
						};
						MyRadioButton myRadioButton = BtnTitleSelect2;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = BtnTitleSelect2;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							BtnTitleSelect_Click((MyRadioButton)a0, a1);
						};
						MyRadioButton myRadioButton = BtnTitleSelect3;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = BtnTitleSelect3;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							BtnTitleSelect_Click((MyRadioButton)a0, a1);
						};
						MyRadioButton myRadioButton = BtnTitleSelect4;
						if (myRadioButton != null)
						{
							myRadioButton.Check -= obj;
						}
						myRadioButton = BtnTitleSelect4;
						if (myRadioButton != null)
						{
							myRadioButton.Check += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							PageBack();
						};
						MyIconButton myIconButton = BtnTitleInner;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnTitleInner;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						SizeChangedEventHandler value2 = PanMainLeft_SizeChanged;
						Border border = PanMainLeft;
						if (border != null)
						{
							border.SizeChanged -= value2;
						}
						border = PanMainLeft;
						if (border != null)
						{
							border.SizeChanged += value2;
						}
			
			}
			{
						MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BackToTop();
						};
						MyExtraButton myExtraButton = BtnExtraBack;
						if (myExtraButton != null)
						{
							myExtraButton.Click -= obj;
						}
						myExtraButton = BtnExtraBack;
						if (myExtraButton != null)
						{
							myExtraButton.Click += obj;
						}
			
			}
			{
						MyExtraButton.ClickEventHandler obj = BtnExtraDownload_Click;
						MyExtraButton myExtraButton = BtnExtraDownload;
						if (myExtraButton != null)
						{
							myExtraButton.Click -= obj;
						}
						myExtraButton = BtnExtraDownload;
						if (myExtraButton != null)
						{
							myExtraButton.Click += obj;
						}
			
			}
			{
						MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							AprilGiveup();
						};
						MyExtraButton myExtraButton = BtnExtraApril;
						if (myExtraButton != null)
						{
							myExtraButton.Click -= obj;
						}
						myExtraButton = BtnExtraApril;
						if (myExtraButton != null)
						{
							myExtraButton.Click += obj;
						}
			
			}
			{
						MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnExtraShutdown_Click();
						};
						MyExtraButton myExtraButton = BtnExtraShutdown;
						if (myExtraButton != null)
						{
							myExtraButton.Click -= obj;
						}
						myExtraButton = BtnExtraShutdown;
						if (myExtraButton != null)
						{
							myExtraButton.Click += obj;
						}
			
			}
			{
						MyExtraButton.ClickEventHandler obj = BtnExtraMusic_Click;
						MyExtraButton.RightClickEventHandler obj2 = BtnExtraMusic_RightClick;
						MyExtraButton myExtraButton = BtnExtraMusic;
						if (myExtraButton != null)
						{
							myExtraButton.Click -= obj;
							myExtraButton.RightClick -= obj2;
						}
						myExtraButton = BtnExtraMusic;
						if (myExtraButton != null)
						{
							myExtraButton.Click += obj;
							myExtraButton.RightClick += obj2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = FormDragMove;
						Grid grid = PanMsg;
						if (grid != null)
						{
							grid.MouseLeftButtonDown -= value2;
						}
						grid = PanMsg;
						if (grid != null)
						{
							grid.MouseLeftButtonDown += value2;
						}
			
			}
		}

	}
}
