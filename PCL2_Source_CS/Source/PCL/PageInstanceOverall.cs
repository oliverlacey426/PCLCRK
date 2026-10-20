// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageInstanceOverall : MyPageRight
	{
		[CompilerGenerated]
		internal sealed class _Closure___16_0
		{
			public ModLoader.LoaderCombo<string> _VB_Local_Loader;
			public _Closure___16_0(_Closure___16_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Loader = arg0._VB_Local_Loader;
				}
			}
			[SpecialName]
			internal void _Lambda___R1(ModLoader.LoaderBase a0)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				switch (_VB_Local_Loader.State)
				{
				case ModBase.LoadState.Finished:
					ModMain.Hint(_VB_Local_Loader.Name + "成功！", ModMain.HintType.Green);
					break;
				case ModBase.LoadState.Failed:
					ModMain.Hint(_VB_Local_Loader.Name + "失败：" + _VB_Local_Loader.Error.GetDisplay(multiline: false), ModMain.HintType.Red);
					break;
				case ModBase.LoadState.Canceled:
					ModMain.Hint(_VB_Local_Loader.Name + "已取消！");
					break;
				}
			}
		}
		private bool IsLoad;
		public MyListItem InstanceHeaderItem;




















		public PageInstanceOverall()
		{
			Loaded += PageSetupLaunch_Loaded;
			IsLoad = false;
			InitializeComponent();
			InitConnectEvents();
		}
		private void PageSetupLaunch_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToHome();
			Reload();
			if (!IsLoad)
			{
				IsLoad = true;
				PanDisplay.TriggerForceResize();
			}
		}
		private void Reload()
		{
			checked
			{
				ModAnimation.AniControlEnabled++;
				ComboDisplayType.SelectedIndex = Conversions.ToInteger(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
				BtnDisplayStar.Text = (PageInstanceLeft.Instance.IsStar ? "从收藏夹中移除" : "加入收藏夹");
				BtnFolderMods.Visibility = ((!PageInstanceLeft.Instance.Modable) ? Visibility.Collapsed : Visibility.Visible);
				PanDisplayItem.Children.Clear();
				InstanceHeaderItem = PageInstanceLeft.Instance.ToListItem().Init();
				InstanceHeaderItem.IsHitTestVisible = false;
				PanDisplayItem.Children.Add(InstanceHeaderItem);
				ModMain.FrmMain.PageNameRefresh();
				ComboDisplayLogo.SelectedIndex = 0;
				string text = ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "Logo");
				if (Conversions.ToBoolean(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "LogoCustom", "False")))
				{
					foreach (MyComboBoxItem item in (IEnumerable)ComboDisplayLogo.Items)
					{
						if (Operators.ConditionalCompareObjectEqual(item.Tag, text, TextCompare: false) || (Operators.ConditionalCompareObjectEqual(item.Tag, "PCL\\Logo.png", TextCompare: false) && text.EndsWithF("PCL\\Logo.png")))
						{
							ComboDisplayLogo.SelectedItem = item;
							break;
						}
					}
				}
				ModAnimation.AniControlEnabled--;
			}
		}
		private void ComboDisplayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!IsLoad || ModAnimation.AniControlEnabled != 0)
			{
				return;
			}
			if (ComboDisplayType.SelectedIndex != 1)
			{
				try
				{
					ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(ComboDisplayType.SelectedIndex));
					PageInstanceLeft.Instance.DisplayType = (ModMinecraft.McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
					ModMain.FrmInstanceLeft.RefreshModDisabled();
					ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
					ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"修改版本分类失败（{PageInstanceLeft.Instance.Name}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
					ProjectData.ClearProjectError();
				}
				Reload();
				return;
			}
			try
			{
				if (!Settings.Get<bool>("HintHide"))
				{
					if (ModMain.MyMsgBox("确认要从版本列表中隐藏该版本吗？隐藏该版本后，它将不再出现于 PCL 显示的版本列表中。\r\n此后，在版本列表页面按下 F11 才可以查看被隐藏的版本。", "隐藏版本提示", "确定", "取消") != 1)
					{
						ComboDisplayType.SelectedIndex = 0;
						return;
					}
					Settings.Set("HintHide", true);
				}
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(1));
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"隐藏版本 {PageInstanceLeft.Instance.Name} 失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnDisplayDesc_Click(object sender, EventArgs e)
		{
			try
			{
				string text = ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
				string text2 = ModMain.MyMsgBoxInput("更改描述", "修改版本的描述文本，留空则使用 PCL 的默认描述。", text, new Collection<ValidateBase>(), "默认描述");
				if (text2 != null && Operators.CompareString(text, text2, TextCompare: false) != 0)
				{
					ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo", text2);
				}
				PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
				Reload();
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"版本 {PageInstanceLeft.Instance.Name} 描述更改失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnDisplayRename_Click(object sender, EventArgs e)
		{
			try
			{
				string name = PageInstanceLeft.Instance.Name;
				string pathVersion = PageInstanceLeft.Instance.PathVersion;
				string text = ModMain.MyMsgBoxInput("重命名版本", "", name, new Collection<ValidateBase>
				{
					new ValidateFolderName(ModMinecraft.McFolderSelected + "versions", UseMinecraftCharCheck: true, new List<string> { name })
				});
				if (!string.IsNullOrWhiteSpace(text))
				{
					string text2 = ModMinecraft.McFolderSelected + "versions\\" + text + "\\";
					string jsonPath = PageInstanceLeft.Instance.GetJsonPath();
					JObject jObject;
					try
					{
						jObject = (JObject)FileUtils.ReadAsJson(jsonPath);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "在重命名读取 JSON 时失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
						jObject = PageInstanceLeft.Instance.JsonObject;
						ProjectData.ClearProjectError();
					}
					DirectoryUtils.Move(pathVersion, text2);
					PageInstanceLeft.Instance.ResetSettingsCache();
					if (DirectoryUtils.Exists($"{text2}{name}-natives"))
					{
						DirectoryUtils.Move($"{text2}{name}-natives", $"{text2}{text}-natives");
					}
					if (FileUtils.Exists($"{text2}{name}.jar"))
					{
						FileUtils.Move($"{text2}{name}.jar", $"{text2}{text}.jar");
					}
					if (FileUtils.Exists(text2 + "PCL\\Setup.ini"))
					{
						FileUtils.Write(text2 + "PCL\\Setup.ini", FileUtils.ReadAsString(text2 + "PCL\\Setup.ini").Replace(pathVersion, text2));
					}
					if (Operators.CompareString(ModBase.ReadIni(ModMinecraft.McFolderSelected + "PCL.ini", "Version"), name, TextCompare: false) == 0)
					{
						ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "Version", text);
					}
					try
					{
						jObject["id"] = text;
						FileUtils.Delete(text2 + PathUtils.GetLastPart(jsonPath));
						FileUtils.Write(text2 + text + ".json", jObject.ToString());
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Warn(ex4, "重命名版本 json 失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
						ProjectData.ClearProjectError();
					}
					ModMain.Hint("重命名成功！", ModMain.HintType.Green);
					PageInstanceLeft.Instance = new ModMinecraft.McInstance(text).Load();
					if (!Information.IsNothing(ModMinecraft.McInstanceSelected) && ModMinecraft.McInstanceSelected.Equals(PageInstanceLeft.Instance))
					{
						ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "Version", text);
					}
					Reload();
					ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "重命名版本失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void ComboDisplayLogo_SelectionChanged()
		{
			if (!IsLoad || ModAnimation.AniControlEnabled != 0)
			{
				return;
			}
			try
			{
				if (ComboDisplayLogo.SelectedItem == ItemDisplayLogoCustom)
				{
					string text = Dialogs.SelectFile("选择图片", multiselect: false, null, new (string[], string)[1] { (new string[5] { "png", "jpeg", "jpg", "gif", "webp" }, "常用图片文件") }).FirstOrDefault();
					if (string.IsNullOrEmpty(text))
					{
						Reload();
						return;
					}
					FileUtils.Copy(text, PageInstanceLeft.Instance.PathVersion + "PCL\\Logo.png");
				}
				else
				{
					FileUtils.Delete(PageInstanceLeft.Instance.PathVersion + "PCL\\Logo.png");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"更改自定义版本图标失败（{PageInstanceLeft.Instance.Name}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				string text2 = Conversions.ToString(NewLateBinding.LateGet(ComboDisplayLogo.SelectedItem, null, "Tag", new object[0], null, null, null));
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "Logo", text2);
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Operators.CompareString(text2, "", TextCompare: false) != 0));
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
				Reload();
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"更改版本图标失败（{PageInstanceLeft.Instance.Name}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnDisplayStar_Click(object sender, EventArgs e)
		{
			try
			{
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(!PageInstanceLeft.Instance.IsStar));
				PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
				Reload();
				ModMinecraft.McInstanceListForceRefresh = true;
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"版本 {PageInstanceLeft.Instance.Name} 收藏状态更改失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnFolderVersion_Click()
		{
			OpenInstanceFolder(PageInstanceLeft.Instance);
		}
		public static void OpenInstanceFolder(ModMinecraft.McInstance Instance)
		{
			ModBase.OpenExplorer(Instance.PathVersion);
		}
		private void BtnFolderSaves_Click()
		{
			string text = PageInstanceLeft.Instance.PathIndie + "saves\\";
			DirectoryUtils.Create(text);
			ModBase.OpenExplorer(text);
		}
		private void BtnFolderMods_Click()
		{
			string text = PageInstanceLeft.Instance.PathIndie + "mods\\";
			DirectoryUtils.Create(text);
			ModBase.OpenExplorer(text);
		}
		private void BtnFolderScreen_Click(object sender, MouseButtonEventArgs e)
		{
			string text = PageInstanceLeft.Instance.PathIndie + "screenshots\\";
			DirectoryUtils.Create(text);
			ModBase.OpenExplorer(text);
		}
		private void BtnManageScript_Click()
		{
			try
			{
				string text = Dialogs.SaveFile("选择脚本保存位置", "启动 " + PageInstanceLeft.Instance.Name + ".bat", null, new (string, string)[1] { ("bat", "批处理文件") });
				if (text == null)
				{
					return;
				}
				if (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
				{
					ModMain.Hint("请在当前启动任务结束后再试！", ModMain.HintType.Red);
				}
				else if (ModLaunch.McLaunchStart(new ModLaunch.McLaunchOptions
				{
					SaveBatch = text,
					Instance = PageInstanceLeft.Instance
				}))
				{
					if (Settings.Get<ModLaunch.McLoginType>("LoginType") == ModLaunch.McLoginType.Legacy)
					{
						ModMain.Hint("正在导出启动脚本……");
					}
					else
					{
						ModMain.Hint("正在导出启动脚本……（注意，使用脚本启动可能会导致登录失效！）");
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"导出启动脚本失败（{PageInstanceLeft.Instance.Name}）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnManageCheck_Click(object sender, EventArgs e)
		{
			try
			{
				_Closure___16_0 arg = default;
				_Closure___16_0 CS_8__locals5 = new _Closure___16_0(arg);
				if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(PageInstanceLeft.Instance)))
				{
					ModMain.Hint("请先关闭 [版本设置 → 设置 → 高级启动选项 → 关闭文件校验]，然后再尝试补全文件！");
					return;
				}
				foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar)
				{
					if (Operators.CompareString(item.Name, PageInstanceLeft.Instance.Name + " 文件补全", TextCompare: false) == 0)
					{
						ModMain.Hint("正在处理中，请稍候！", ModMain.HintType.Red);
						return;
					}
				}
				CS_8__locals5._VB_Local_Loader = new ModLoader.LoaderCombo<string>(PageInstanceLeft.Instance.Name + " 文件补全", ModDownload.DlClientFix(PageInstanceLeft.Instance, CheckAssetsHash: true, DownloadAssetIndexInBackground: false));
				CS_8__locals5._VB_Local_Loader.OnStateChanged = [SpecialName] (ModLoader.LoaderBase a0) =>
				{
					CS_8__locals5._Lambda___0();
				};
				CS_8__locals5._VB_Local_Loader.Start(PageInstanceLeft.Instance.Name);
				ModLoader.LoaderTaskbarAdd(CS_8__locals5._VB_Local_Loader);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"尝试补全文件失败（{PageInstanceLeft.Instance.Name}）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnManageDelete_Click(object sender, EventArgs e)
		{
			try
			{
				bool shiftKeyDown = MyWpfExtension.Computer.Keyboard.ShiftKeyDown;
				bool flag = PageInstanceLeft.Instance.State != ModMinecraft.McInstanceState.Error && Operators.CompareString(PageInstanceLeft.Instance.PathIndie, ModMinecraft.McFolderSelected, TextCompare: false) != 0;
				DirectoryInfo info = DirectoryUtils.GetInfo(PageInstanceLeft.Instance.PathIndie + "saves\\");
				List<string> list = ((flag && info.Exists) ? (from Save in info.EnumerateDirectories()
					orderby Save.LastWriteTime descending
					select $"{Save.Name}（上次修改：{StringUtils.FormatTimeSpan(Save.LastWriteTime - DateTime.Now, isShort: false)}）").ToList() : new List<string>());
				switch (ModMain.MyMsgBox(string.Format("你确定要{0}删除版本 {1} 吗？", shiftKeyDown ? "永久" : "", PageInstanceLeft.Instance.Name) + (flag ? ("\r\n该版本对应的存档、资源包、Mod 等文件也将被一并删除！" + (list.Any() ? ("\r\n\r\n这会删除以下存档：\r\n· " + list.Join("\r\n· ")) : "")) : ""), "版本删除确认", "确定", "取消", "", flag || shiftKeyDown))
				{
				case 1:
					PageInstanceLeft.Instance.ResetSettingsCache();
					DirectoryUtils.Delete(PageInstanceLeft.Instance.PathVersion, !shiftKeyDown);
					ModMain.Hint("已删除版本 " + PageInstanceLeft.Instance.Name + "！", ModMain.HintType.Green);
					break;
				case 2:
					return;
				}
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				ModMain.FrmMain.PageBack();
			}
			catch (OperationCanceledException ex)
			{
				ProjectData.SetProjectError(ex);
				OperationCanceledException ex2 = ex;
				Logger.Warn(ex2, $"删除版本 {PageInstanceLeft.Instance.Name} 被主动取消", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"删除版本 {PageInstanceLeft.Instance.Name} 失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
						{
							ComboDisplayLogo_SelectionChanged();
						};
						MyComboBox myComboBox = ComboDisplayLogo;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
						}
						myComboBox = ComboDisplayLogo;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							ComboDisplayLogo_SelectionChanged();
						};
						MyComboBoxItem myComboBoxItem = ItemDisplayLogoCustom;
						if (myComboBoxItem != null)
						{
							myComboBoxItem.PreviewMouseLeftButtonUp -= value2;
						}
						myComboBoxItem = ItemDisplayLogoCustom;
						if (myComboBoxItem != null)
						{
							myComboBoxItem.PreviewMouseLeftButtonUp += value2;
						}
			
			}
			{
						SelectionChangedEventHandler value2 = ComboDisplayType_SelectionChanged;
						MyComboBox myComboBox = ComboDisplayType;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
						}
						myComboBox = ComboDisplayType;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnDisplayRename_Click;
						MyButton myButton = BtnDisplayRename;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDisplayRename;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnDisplayDesc_Click;
						MyButton myButton = BtnDisplayDesc;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDisplayDesc;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnDisplayStar_Click;
						MyButton myButton = BtnDisplayStar;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnDisplayStar;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnFolderVersion_Click();
						};
						MyButton myButton = BtnFolderVersion;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFolderVersion;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnFolderSaves_Click();
						};
						MyButton myButton = BtnFolderSaves;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFolderSaves;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnFolderMods_Click();
						};
						MyButton myButton = BtnFolderMods;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFolderMods;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnFolderScreen_Click;
						MyButton myButton = BtnFolderScreen;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFolderScreen;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnManageScript_Click();
						};
						MyButton myButton = BtnManageScript;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnManageScript;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnManageCheck_Click;
						MyButton myButton = BtnManageCheck;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnManageCheck;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnManageDelete_Click;
						MyButton myButton = BtnManageDelete;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnManageDelete;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
