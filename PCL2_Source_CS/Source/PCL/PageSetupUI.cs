// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageSetupUI : MyPageRight
	{
		private static bool _HiddenForceShow = false;
		[SpecialName]
		private bool _STATIC_PageSetupUI_Loaded_20211C128135_Reloaded;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init;
		public static bool HiddenForceShow
		{
			get
			{
				return _HiddenForceShow;
			}
			set
			{
				_HiddenForceShow = value;
				HiddenRefresh();
			}
		}

































































































		public PageSetupUI()
		{
			Loaded += PageSetupUI_Loaded;
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				HiddenRefresh();
			};
			InitializeComponent();
			InitConnectEvents();
		}
		private void PageSetupUI_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToHome();
			ModSecret.ThemeCheckAll(EffectSetup: true);
			if (ModSecret.ThemeDontClick != 0)
			{
				string text = null;
				switch (ModSecret.ThemeDontClick)
				{
				case 1:
					text = "眼瞎白";
					break;
				case 2:
					text = "真·滑稽彩";
					break;
				}
				foreach (object child in PanLauncherTheme.Children)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(child);
					if (objectValue is MyRadioBox && ((MyRadioBox)objectValue).IsEnabled)
					{
						((MyRadioBox)objectValue).Text = text;
					}
				}
			}
			checked
			{
				ModAnimation.AniControlEnabled++;
				Refresh();
				ModAnimation.AniControlEnabled--;
				if (_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init == null)
				{
					Interlocked.CompareExchange(ref _STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init, ref lockTaken);
					if (_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init.State == 0)
					{
						_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init.State = 2;
						_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded = false;
					}
					else if (_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded_Init);
					}
				}
				if (!_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded)
				{
					_STATIC_PageSetupUI_Loaded_20211C128135_Reloaded = true;
					SliderLoad();
					if (!RadioLauncherTheme8.IsEnabled)
					{
						LabLauncherTheme8Copy.ToolTip = $"累积赞助达到 ¥23.33 后，在爱发电私信发送【土豆 {ModBase.Identify}】以解锁。" + "\r\n右键打开赞助页面，如果觉得 PCL 做得还不错就支持一下吧 =w=！";
					}
					RadioLauncherTheme8.ToolTip = $"累积赞助达到 ¥23.33 后，在爱发电私信发送【土豆 {ModBase.Identify}】以解锁";
					if (!RadioLauncherTheme9.IsEnabled)
					{
						LabLauncherTheme9Copy.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁（右键打开反馈页面）\r\n· 提交一个 Pull Request 或主页预设，在标记为 [完成] 后回复识别码要求解锁";
					}
					RadioLauncherTheme9.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁\r\n· 提交一个 Pull Request 或主页预设，在标记为 [完成] 后回复识别码要求解锁";
				}
			}
		}
		public void Refresh()
		{
			try
			{
				SettingService.RefreshSettings(this);
				BackgroundRefresh(IsHint: false, Refresh: false);
				CheckLogoLeft.Visibility = ((!RadioLogoType0.Checked) ? Visibility.Collapsed : Visibility.Visible);
				PanLogoText.Visibility = ((!RadioLogoType2.Checked) ? Visibility.Collapsed : Visibility.Visible);
				PanLogoChange.Visibility = ((!RadioLogoType3.Checked) ? Visibility.Collapsed : Visibility.Visible);
				MusicRefreshUI();
				OnMainPageTypeChanged();
			}
			catch (NullReferenceException ex)
			{
				ProjectData.SetProjectError(ex);
				NullReferenceException ex2 = ex;
				Logger.Error(ex2, "个性化设置项存在异常，已被自动重置", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				Reset();
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "重载个性化设置时出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		public void Reset()
		{
			try
			{
				SettingService.ResetSettings(this);
				Logger.Info("已初始化个性化设置！", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ModMain.Hint("已初始化个性化设置", ModMain.HintType.Green, Log: false);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "初始化个性化设置失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
			Refresh();
		}
		private void BtnUIBgOpen_Click(object sender, EventArgs e)
		{
			ModBase.OpenExplorer(Paths.Base + "PCL\\Pictures\\");
		}
		private void BtnBackgroundRefresh_Click(object sender, EventArgs e)
		{
			BackgroundRefresh(IsHint: true, Refresh: true);
		}
		public void BackgroundRefreshUI(bool Show, int Count)
		{
			if (!Information.IsNothing(PanBackgroundOpacity))
			{
				if (Show)
				{
					PanBackgroundOpacity.Visibility = Visibility.Visible;
					PanBackgroundBlur.Visibility = Visibility.Visible;
					PanBackgroundSuit.Visibility = Visibility.Visible;
					BtnBackgroundClear.Visibility = Visibility.Visible;
					CardBackground.Title = "背景图片（" + Conversions.ToString(Count) + " 张）";
				}
				else
				{
					PanBackgroundOpacity.Visibility = Visibility.Collapsed;
					PanBackgroundBlur.Visibility = Visibility.Collapsed;
					PanBackgroundSuit.Visibility = Visibility.Collapsed;
					BtnBackgroundClear.Visibility = Visibility.Collapsed;
					CardBackground.Title = "背景图片";
				}
				CardBackground.TriggerForceResize();
			}
		}
		private void BtnBackgroundClear_Click(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("即将删除背景图片文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", IsWarn: true) == 1)
			{
				DirectoryUtils.Delete(Paths.Base + "PCL\\Pictures");
				BackgroundRefresh(IsHint: false, Refresh: true);
				ModMain.Hint("背景图片已清空！", ModMain.HintType.Green);
			}
		}
		public static void BackgroundRefresh(bool IsHint, bool Refresh)
		{
			try
			{
				DirectoryUtils.Create(Paths.Base + "PCL\\Pictures\\");
				List<string> list = new List<string>();
				foreach (string item in DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Pictures\\", includeSubDirectories: true))
				{
					string extension = PathUtils.GetExtension(item);
					if (Operators.CompareString(extension, "ini", TextCompare: false) != 0 && Operators.CompareString(extension, "db", TextCompare: false) != 0)
					{
						list.Add(item);
					}
				}
				if (!list.Any())
				{
					if (Refresh)
					{
						if (ModMain.FrmMain.ImgBack.Visibility == Visibility.Collapsed)
						{
							if (IsHint)
							{
								ModMain.Hint("未检测到可用背景图片！", ModMain.HintType.Red);
							}
						}
						else
						{
							ModMain.FrmMain.ImgBack.Visibility = Visibility.Collapsed;
							if (IsHint)
							{
								ModMain.Hint("背景图片已清除！", ModMain.HintType.Green);
							}
						}
					}
					if (!Information.IsNothing(ModMain.FrmSetupUI))
					{
						ModMain.FrmSetupUI.BackgroundRefreshUI(Show: false, 0);
					}
					return;
				}
				if (Refresh)
				{
					string text = ModBase.RandomOne(list);
					try
					{
						Logger.Info($"加载背景图片：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						ModMain.FrmMain.ImgBack.Background = (ImageBrush)new MyBitmap(text);
						ModMain.FrmMain.ImgBack.Visibility = Visibility.Visible;
						ModMain.FrmMain.UpdateBackgroundAndTitleBar();
						if (IsHint)
						{
							ModMain.Hint("背景图片已刷新：" + PathUtils.GetLastPart(text), ModMain.HintType.Green, Log: false);
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if (ex2.Message.Contains("参数无效"))
						{
							Logger.Error(string.Format("刷新背景图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text), LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						}
						else
						{
							Logger.Error(ex2, $"刷新背景图片失败（{text}）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						}
						ProjectData.ClearProjectError();
					}
				}
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.BackgroundRefreshUI(Show: true, list.Count);
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "刷新背景图片时出现未知错误", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnLogoChange_Click(object sender, EventArgs e)
		{
			string text = Dialogs.SelectFile("选择图片", multiselect: false, null, new (string[], string)[1] { (new string[5] { "png", "jpg", "jpeg", "gif", "webp" }, "常用图片文件") }).FirstOrDefault();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string text2 = Paths.Base + "PCL\\Logo.png";
			try
			{
				FileUtils.Copy(text, text2);
				ModMain.FrmMain.ImageTitleLogo.Source = null;
				ModMain.FrmMain.ImageTitleLogo.Source = text2;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (ex2.Message.Contains("参数无效"))
				{
					Logger.Error(string.Format("改变标题栏图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text2), LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				}
				else
				{
					Logger.Error(ex2, "设置标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				}
				ModMain.FrmMain.ImageTitleLogo.Source = null;
				ProjectData.ClearProjectError();
			}
		}
		private void RadioLogoType3_Check(object sender, ModBase.RouteEventArgs e)
		{
			if (ModAnimation.AniControlEnabled != 0 || !e.RaiseByMouse)
			{
				return;
			}
			while (true)
			{
				string text = Paths.Base + "PCL\\Logo.png";
				if (FileUtils.Exists(text))
				{
					try
					{
						ModMain.FrmMain.ImageTitleLogo.Source = null;
						ModMain.FrmMain.ImageTitleLogo.Source = text;
						return;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if (ex2.Message.Contains("参数无效"))
						{
							Logger.Error(string.Format("改变标题栏图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text), LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						}
						else
						{
							Logger.Error(ex2, "调整标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						}
						ModMain.FrmMain.ImageTitleLogo.Source = null;
						e.Handled = true;
						try
						{
							FileUtils.Delete(text);
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Error(ex4, "清理错误的标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
							ProjectData.ClearProjectError();
						}
						ProjectData.ClearProjectError();
						return;
					}
				}
				string text2 = Dialogs.SelectFile("选择图片", multiselect: false, null, new (string[], string)[1] { (new string[5] { "png", "jpeg", "jpg", "gif", "webp" }, "常用图片文件") }).FirstOrDefault();
				if (string.IsNullOrEmpty(text2))
				{
					break;
				}
				try
				{
					FileUtils.Copy(text2, text);
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					Logger.Error(ex6, "复制标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					ProjectData.ClearProjectError();
					return;
				}
			}
			ModMain.FrmMain.ImageTitleLogo.Source = null;
			e.Handled = true;
		}
		private void BtnLogoDelete_Click(object sender, EventArgs e)
		{
			try
			{
				FileUtils.Delete(Paths.Base + "PCL\\Logo.png");
				RadioLogoType1.SetChecked(value: true, user: true);
				ModMain.Hint("标题栏图片已清空！", ModMain.HintType.Green);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "清空标题栏图片失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnMusicOpen_Click(object sender, EventArgs e)
		{
			ModBase.OpenExplorer(Paths.Base + "PCL\\Musics\\");
		}
		private void BtnMusicRefresh_Click(object sender, EventArgs e)
		{
			ModMusic.MusicRefreshPlay(ShowHint: true);
		}
		public void MusicRefreshUI()
		{
			if (PanBackgroundOpacity == null)
			{
				return;
			}
			if (ModMusic.MusicAllList.Any())
			{
				PanMusicVolume.Visibility = Visibility.Visible;
				PanMusicDetail.Visibility = Visibility.Visible;
				BtnMusicClear.Visibility = Visibility.Visible;
				CardMusic.Title = "背景音乐（" + Conversions.ToString(DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Musics\\", includeSubDirectories: true).Count([SpecialName] (string f) => !new string[7] { "ini", "jpg", "txt", "cfg", "lrc", "db", "png" }.Contains(PathUtils.GetExtension(f)))) + " 首）";
			}
			else
			{
				PanMusicVolume.Visibility = Visibility.Collapsed;
				PanMusicDetail.Visibility = Visibility.Collapsed;
				BtnMusicClear.Visibility = Visibility.Collapsed;
				CardMusic.Title = "背景音乐";
			}
			CardMusic.TriggerForceResize();
		}
		private void BtnMusicClear_Click(object sender, EventArgs e)
		{
			if (ModMain.MyMsgBox("即将删除背景音乐文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", IsWarn: true) != 1)
			{
				return;
			}
			ModBase.RunInThread([SpecialName] () =>
			{
				ModMain.Hint("正在删除背景音乐……");
				ModMusic.MusicNAudio = null;
				ModMusic.MusicWaitingList = new List<string>();
				ModMusic.MusicAllList = new List<string>();
				Thread.Sleep(200);
				try
				{
					DirectoryUtils.Delete(Paths.Base + "PCL\\Musics");
					ModMain.Hint("背景音乐已删除！", ModMain.HintType.Green);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "删除背景音乐失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					DirectoryUtils.Create(Paths.Base + "PCL\\Musics\\");
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMusic.MusicRefreshPlay(ShowHint: false);
					});
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Error(ex4, "重建背景音乐文件夹失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		private void CheckMusicStart_Change()
		{
			if (ModAnimation.AniControlEnabled == 0 && CheckMusicStart.Checked)
			{
				CheckMusicStop.Checked = false;
			}
		}
		private void CheckMusicStop_Change()
		{
			if (ModAnimation.AniControlEnabled == 0 && CheckMusicStop.Checked)
			{
				CheckMusicStart.Checked = false;
			}
		}
		private void BtnCustomFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("当前已存在布局文件，继续生成教学文件将会覆盖现有布局文件！", "覆盖确认", "继续", "取消", "", IsWarn: true) != 2)
				{
					ModBase.ExtractResources(Paths.Base + "PCL\\Custom.xaml", "Custom");
					ModMain.Hint("教学文件已生成！", ModMain.HintType.Green);
					ModBase.OpenExplorer(Paths.Base + "PCL\\Custom.xaml");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "生成教学文件失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnCustomRefresh_Click()
		{
			ModMain.FrmLaunchRight.ForceRefresh();
			ModMain.Hint("已刷新主页！", ModMain.HintType.Green);
		}
		private void BtnCustomTutorial_Click(object sender, EventArgs e)
		{
			ModMain.MyMsgBox("1. 点击 生成教学文件 按钮，这会在 PCL 文件夹下生成 Custom.xaml 布局文件。\r\n2. 使用记事本等工具打开这个文件并进行修改，修改完记得保存。\r\n3. 点击 刷新主页 按钮，查看主页现在长啥样了。\r\n\r\n你可以在生成教学文件后直接刷新主页，对照着进行修改，更有助于理解。\r\n直接将主页文件拖进 PCL 窗口也可以快捷加载。", "主页自定义教程");
		}
		public static void OnMainPageTypeChanged()
		{
			if (ModMain.FrmSetupUI != null)
			{
				switch (Settings.Get<int>("UiCustomType"))
				{
				case 0:
					ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.HintCustomWarn.Visibility = Visibility.Collapsed;
					break;
				case 1:
					ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.HintCustomWarn.Visibility = (Settings.Get<bool>("HintCustomWarn") ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmSetupUI.HintCustom.Text = string.Format("从 PCL 文件夹下的 Custom.xaml 读取主页内容。{0}你可以手动编辑该文件，向主页添加文本、图片、常用网站、快捷启动等功能。", "\r\n");
					CustomEventService.SetEventType(ModMain.FrmSetupUI.HintCustom, CustomEvent.EventType.None);
					break;
				case 2:
					ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.HintCustomWarn.Visibility = (Settings.Get<bool>("HintCustomWarn") ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmSetupUI.HintCustom.Text = string.Format("从指定网址联网获取主页内容。服主也可以用于动态更新服务器公告。{0}如果你制作了稳定运行的联网主页，可以点击这条提示投稿，若合格即可加入预设！", "\r\n");
					CustomEventService.SetEventType(ModMain.FrmSetupUI.HintCustom, CustomEvent.EventType.打开网页);
					CustomEventService.SetEventData(ModMain.FrmSetupUI.HintCustom, "https://github.com/Meloong-Git/PCL/discussions/2528");
					break;
				case 3:
					ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Visible;
					ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupUI.HintCustomWarn.Visibility = Visibility.Collapsed;
					break;
				}
				ModMain.FrmSetupUI.CardCustom.TriggerForceResize();
			}
		}
		private void LabLauncherTheme5Unlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			RadioLauncherTheme5Gray.Opacity -= 0.7;
			RadioLauncherTheme5.Opacity += 0.7;
			ModAnimation.AniStart(checked(new ModAnimation.AniData[2]
			{
				ModAnimation.AaOpacity(RadioLauncherTheme5Gray, 1.0, (int)Math.Round(1000.0 * ModAnimation.AniSpeed), (int)Math.Round(500.0 * ModAnimation.AniSpeed), new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(RadioLauncherTheme5, -1.0, (int)Math.Round(1000.0 * ModAnimation.AniSpeed), (int)Math.Round(500.0 * ModAnimation.AniSpeed), new ModAnimation.AniEaseInFluent())
			}), "ThemeUnlock");
			if (RadioLauncherTheme5Gray.Opacity < 0.02)
			{
				ModSecret.ThemeUnlock(5, ShowDoubleHint: true, "隐藏主题 玄素黑 已解锁！");
				ModAnimation.AniStop("ThemeUnlock");
				RadioLauncherTheme5.Checked = true;
			}
		}
		private void LabLauncherTheme11Click_MouseLeftButtonUp()
		{
			if ((LabLauncherTheme11Click.Visibility == Visibility.Collapsed || (LabLauncherTheme11Click.ToolTip ?? "").ToString().Contains("点击")) && ModMain.MyMsgBox("1. 不爬取或攻击相关服务或网站，不盗取相关账号，没有谜题可以或需要以此来解决。\r\n2. 不得篡改或损毁相关公开信息，请尽量让它们保持原状。\r\n3. 在你感到迷茫的时候，看看回声洞可能会给你带来惊喜。\r\n\r\n若违规，可能会被从任意相关群中踢出！", "解密游戏的基本规则", "我知道了", "恕我拒绝") == 1)
			{
				ModMain.MyMsgBox("你需要用自己的智慧来找到下一步的线索……\r\n初始线索：gnp.dorC61\\60\\20\\0202\\moc.x1xa.2s\\\\:sp" + "T".Lower() + "th", "解密游戏");
			}
		}
		private void LabLauncherTheme8Copy_MouseRightButtonUp()
		{
			ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat");
		}
		private void LabLauncherTheme9Copy_MouseRightButtonUp()
		{
			PageOtherLeft.TryFeedback();
		}
		private void RadioLauncherTheme14_Change(object sender, ModBase.RouteEventArgs e)
		{
			if (RadioLauncherTheme14.Checked)
			{
				if (LabLauncherHue.Visibility == Visibility.Visible)
				{
					return;
				}
				LabLauncherHue.Visibility = Visibility.Visible;
				SliderLauncherHue.Visibility = Visibility.Visible;
				LabLauncherSat.Visibility = Visibility.Visible;
				SliderLauncherSat.Visibility = Visibility.Visible;
				LabLauncherDelta.Visibility = Visibility.Visible;
				SliderLauncherDelta.Visibility = Visibility.Visible;
				LabLauncherLight.Visibility = Visibility.Visible;
				SliderLauncherLight.Visibility = Visibility.Visible;
			}
			else
			{
				if (LabLauncherHue.Visibility == Visibility.Collapsed)
				{
					return;
				}
				LabLauncherHue.Visibility = Visibility.Collapsed;
				SliderLauncherHue.Visibility = Visibility.Collapsed;
				LabLauncherSat.Visibility = Visibility.Collapsed;
				SliderLauncherSat.Visibility = Visibility.Collapsed;
				LabLauncherDelta.Visibility = Visibility.Collapsed;
				SliderLauncherDelta.Visibility = Visibility.Collapsed;
				LabLauncherLight.Visibility = Visibility.Collapsed;
				SliderLauncherLight.Visibility = Visibility.Collapsed;
			}
			CardLauncher.TriggerForceResize();
		}
		private void HSL_Change()
		{
			if (ModAnimation.AniControlEnabled == 0 && SliderLauncherSat != null && SliderLauncherSat.IsLoaded)
			{
				ModSecret.ThemeRefresh();
			}
		}
		public static void HiddenRefresh()
		{
			if (ModMain.FrmMain.PanTitleSelect == null || !ModMain.FrmMain.PanTitleSelect.IsLoaded)
			{
				return;
			}
			checked
			{
				try
				{
					if (!HiddenForceShow && Settings.Get<bool>("UiHiddenPageDownload") && Settings.Get<bool>("UiHiddenPageLink") && Settings.Get<bool>("UiHiddenPageSetup") && Settings.Get<bool>("UiHiddenPageOther"))
					{
						ModMain.FrmMain.PanTitleSelect.Visibility = Visibility.Collapsed;
					}
					else
					{
						ModMain.FrmMain.PanTitleSelect.Visibility = Visibility.Visible;
						ModMain.FrmMain.BtnTitleSelect1.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageDownload")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmMain.BtnTitleSelect2.Visibility = Visibility.Collapsed;
						ModMain.FrmMain.BtnTitleSelect3.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageSetup")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmMain.BtnTitleSelect4.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageOther")) ? Visibility.Collapsed : Visibility.Visible);
					}
					ModMain.FrmLaunchLeft.RefreshButtonsUI();
					if (ModMain.FrmSetupUI != null)
					{
						ModMain.FrmSetupUI.CardSwitch.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenFunctionHidden")) ? Visibility.Collapsed : Visibility.Visible);
					}
					if (ModMain.FrmSetupLeft != null)
					{
						ModMain.FrmSetupLeft.ItemLaunch.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupLaunch")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmSetupLeft.ItemUI.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupUi")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmSetupLeft.ItemLink.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupLeft.ItemSystem.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupSystem")) ? Visibility.Collapsed : Visibility.Visible);
						int num = 0;
						if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
						{
							num++;
						}
						if (!Settings.Get<bool>("UiHiddenSetupUi"))
						{
							num++;
						}
						if (!Settings.Get<bool>("UiHiddenSetupSystem"))
						{
							num++;
						}
						ModMain.FrmSetupLeft.PanItem.Visibility = ((num < 2 && !HiddenForceShow) ? Visibility.Collapsed : Visibility.Visible);
					}
					int num2 = 0;
					if (!Settings.Get<bool>("UiHiddenOtherHelp"))
					{
						num2++;
					}
					if (!Settings.Get<bool>("UiHiddenOtherAbout"))
					{
						num2++;
					}
					if (!Settings.Get<bool>("UiHiddenOtherTest"))
					{
						num2++;
					}
					if (!Settings.Get<bool>("UiHiddenOtherFeedback"))
					{
						num2++;
					}
					if (!Settings.Get<bool>("UiHiddenOtherVote"))
					{
						num2++;
					}
					if (ModMain.FrmOtherLeft != null)
					{
						ModMain.FrmOtherLeft.ItemHelp.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherHelp")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmOtherLeft.ItemFeedback.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherFeedback")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmOtherLeft.ItemVote.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherVote")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmOtherLeft.ItemAbout.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherAbout")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmOtherLeft.ItemTest.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherTest")) ? Visibility.Collapsed : Visibility.Visible);
						ModMain.FrmOtherLeft.PanItem.Visibility = ((num2 < 2 && !HiddenForceShow) ? Visibility.Collapsed : Visibility.Visible);
					}
					if (num2 == 1 && !HiddenForceShow)
					{
						if (!Settings.Get<bool>("UiHiddenOtherHelp"))
						{
							ModMain.FrmMain.BtnTitleSelect4.Text = "帮助";
						}
						else if (!Settings.Get<bool>("UiHiddenOtherAbout"))
						{
							ModMain.FrmMain.BtnTitleSelect4.Text = "关于";
						}
						else
						{
							ModMain.FrmMain.BtnTitleSelect4.Text = "百宝箱";
						}
					}
					else
					{
						ModMain.FrmMain.BtnTitleSelect4.Text = "更多";
					}
					if (ModMain.FrmMain.PageCurrent == FormMain.PageType.InstanceSelect)
					{
						ModMain.FrmSelectRight.BtnEmptyDownload_Loaded();
					}
					if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Launch)
					{
						ModMain.FrmLaunchLeft.RefreshButtonsUI();
					}
					if (ModMain.FrmMain.PageCurrent == FormMain.PageType.InstanceSetup && ModMain.FrmInstanceModDisabled != null)
					{
						ModMain.FrmInstanceModDisabled.BtnDownload_Loaded();
					}
					if (ModMain.FrmSetupUI != null)
					{
						ModMain.FrmSetupUI.CardSwitch.Title = (HiddenForceShow ? "功能隐藏（已暂时关闭，按 F12 以重新启用）" : "功能隐藏");
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "刷新功能隐藏项目失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		private void HiddenSetupMain()
		{
			if (CheckHiddenPageSetup.Checked)
			{
				CheckHiddenSetupLaunch.Checked = true;
				CheckHiddenSetupSystem.Checked = true;
				CheckHiddenSetupLink.Checked = true;
				CheckHiddenSetupUI.Checked = true;
			}
			else if (Settings.Get<bool>("UiHiddenSetupLaunch") && Settings.Get<bool>("UiHiddenSetupUi") && Settings.Get<bool>("UiHiddenSetupSystem") && Settings.Get<bool>("UiHiddenSetupLink"))
			{
				CheckHiddenSetupLaunch.Checked = false;
				CheckHiddenSetupSystem.Checked = false;
				CheckHiddenSetupLink.Checked = false;
				CheckHiddenSetupUI.Checked = false;
			}
		}
		private void HiddenSetupSub()
		{
			if (Settings.Get<bool>("UiHiddenSetupLaunch") && Settings.Get<bool>("UiHiddenSetupUi") && Settings.Get<bool>("UiHiddenSetupSystem") && Settings.Get<bool>("UiHiddenSetupLink"))
			{
				CheckHiddenPageSetup.Checked = true;
			}
			else
			{
				CheckHiddenPageSetup.Checked = false;
			}
		}
		private void HiddenOtherMain()
		{
			if (CheckHiddenPageOther.Checked)
			{
				CheckHiddenOtherAbout.Checked = true;
				CheckHiddenOtherTest.Checked = true;
				CheckHiddenOtherFeedback.Checked = true;
				CheckHiddenOtherVote.Checked = true;
				CheckHiddenOtherHelp.Checked = true;
			}
			else if (Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest") && Settings.Get<bool>("UiHiddenOtherVote") && Settings.Get<bool>("UiHiddenOtherFeedback"))
			{
				CheckHiddenOtherAbout.Checked = false;
				CheckHiddenOtherTest.Checked = false;
				CheckHiddenOtherFeedback.Checked = false;
				CheckHiddenOtherVote.Checked = false;
				CheckHiddenOtherHelp.Checked = false;
			}
		}
		private void HiddenOtherSub(object sender, bool user)
		{
			if (Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest"))
			{
				CheckHiddenPageOther.Checked = true;
			}
			else
			{
				CheckHiddenPageOther.Checked = false;
			}
			if (user && Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest"))
			{
				CheckHiddenOtherFeedback.Checked = true;
				CheckHiddenOtherVote.Checked = true;
			}
		}
		private void HiddenOtherNet(object sender, bool user)
		{
			if (user && Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest") && (!Settings.Get<bool>("UiHiddenOtherFeedback") || !Settings.Get<bool>("UiHiddenOtherVote")))
			{
				CheckHiddenOtherAbout.Checked = false;
				CheckHiddenOtherTest.Checked = false;
				CheckHiddenOtherHelp.Checked = false;
			}
		}
		private void HiddenHint(object sender, bool user)
		{
			if (Conversions.ToBoolean(ModAnimation.AniControlEnabled == 0 && Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "Checked", new object[0], null, null, null))))
			{
				ModMain.Hint("按 F12 即可暂时关闭功能隐藏设置。千万别忘了，要不然设置就改不回来了……");
			}
		}
		private void SliderLoad()
		{
			SliderMusicVolume.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Ceiling", new object[1] { Operators.MultiplyObject(v, 0.1) }, null, null, null), "%"));
			SliderLauncherTransparent.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[1] { Operators.AddObject(40, Operators.MultiplyObject(v, 0.1)) }, null, null, null), "%"));
			SliderLauncherHue.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, "°"));
			SliderLauncherSat.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, "%"));
			checked
			{
				SliderLauncherDelta.GetHintText = (VB_AnonymousDelegate_26<int, string>)([SpecialName] (int Value) =>
				{
					if (Value > 90)
					{
						return "+" + Conversions.ToString(Value - 90);
					}
					return (Value == 90) ? Conversions.ToString(0) : Conversions.ToString(Value - 90);
				});
				SliderLauncherLight.GetHintText = (VB_AnonymousDelegate_26<int, string>)([SpecialName] (int Value) =>
				{
					if (Value > 20)
					{
						return "+" + Conversions.ToString(Value - 20);
					}
					return (Value == 20) ? Conversions.ToString(0) : Conversions.ToString(Value - 20);
				});
				SliderBackgroundOpacity.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[1] { Operators.MultiplyObject(v, 0.1) }, null, null, null), "%"));
				SliderBackgroundBlur.GetHintText = (VB_AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, " 像素"));
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HSL_Change();
						};
						MySlider mySlider = SliderLauncherHue;
						if (mySlider != null)
						{
							mySlider.Change -= obj;
						}
						mySlider = SliderLauncherHue;
						if (mySlider != null)
						{
							mySlider.Change += obj;
						}
			
			}
			{
						MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HSL_Change();
						};
						MySlider mySlider = SliderLauncherDelta;
						if (mySlider != null)
						{
							mySlider.Change -= obj;
						}
						mySlider = SliderLauncherDelta;
						if (mySlider != null)
						{
							mySlider.Change += obj;
						}
			
			}
			{
						MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HSL_Change();
						};
						MySlider mySlider = SliderLauncherSat;
						if (mySlider != null)
						{
							mySlider.Change -= obj;
						}
						mySlider = SliderLauncherSat;
						if (mySlider != null)
						{
							mySlider.Change += obj;
						}
			
			}
			{
						MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HSL_Change();
						};
						MySlider mySlider = SliderLauncherLight;
						if (mySlider != null)
						{
							mySlider.Change -= obj;
						}
						mySlider = SliderLauncherLight;
						if (mySlider != null)
						{
							mySlider.Change += obj;
						}
			
			}
			{
						MouseButtonEventHandler value2 = LabLauncherTheme5Unlock_MouseLeftButtonUp;
						TextBlock textBlock = LabLauncherTheme5Unlock;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonUp -= value2;
						}
						textBlock = LabLauncherTheme5Unlock;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme8Copy_MouseRightButtonUp();
						};
						MyRadioBox myRadioBox = RadioLauncherTheme8;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp -= value2;
						}
						myRadioBox = RadioLauncherTheme8;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme8Copy_MouseRightButtonUp();
						};
						TextBlock textBlock = LabLauncherTheme8Copy;
						if (textBlock != null)
						{
							textBlock.MouseRightButtonUp -= value2;
						}
						textBlock = LabLauncherTheme8Copy;
						if (textBlock != null)
						{
							textBlock.MouseRightButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme9Copy_MouseRightButtonUp();
						};
						MyRadioBox myRadioBox = RadioLauncherTheme9;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp -= value2;
						}
						myRadioBox = RadioLauncherTheme9;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme9Copy_MouseRightButtonUp();
						};
						TextBlock textBlock = LabLauncherTheme9Copy;
						if (textBlock != null)
						{
							textBlock.MouseRightButtonUp -= value2;
						}
						textBlock = LabLauncherTheme9Copy;
						if (textBlock != null)
						{
							textBlock.MouseRightButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme11Click_MouseLeftButtonUp();
						};
						MyRadioBox myRadioBox = RadioLauncherTheme11;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp -= value2;
						}
						myRadioBox = RadioLauncherTheme11;
						if (myRadioBox != null)
						{
							myRadioBox.MouseRightButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							LabLauncherTheme11Click_MouseLeftButtonUp();
						};
						TextBlock textBlock = LabLauncherTheme11Click;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonUp -= value2;
						}
						textBlock = LabLauncherTheme11Click;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonUp += value2;
						}
			
			}
			{
						IMyRadio.ChangedEventHandler obj = RadioLauncherTheme14_Change;
						MyRadioBox myRadioBox = RadioLauncherTheme14;
						if (myRadioBox != null)
						{
							myRadioBox.Changed -= obj;
						}
						myRadioBox = RadioLauncherTheme14;
						if (myRadioBox != null)
						{
							myRadioBox.Changed += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnUIBgOpen_Click;
						MyButton myButton = BtnBackgroundOpen;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnBackgroundOpen;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnBackgroundRefresh_Click;
						MyButton myButton = BtnBackgroundRefresh;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnBackgroundRefresh;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnBackgroundClear_Click;
						MyButton myButton = BtnBackgroundClear;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnBackgroundClear;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							CheckMusicStart_Change();
						};
						MyCheckBox myCheckBox = CheckMusicStart;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckMusicStart;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							CheckMusicStop_Change();
						};
						MyCheckBox myCheckBox = CheckMusicStop;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckMusicStop;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnMusicOpen_Click;
						MyButton myButton = BtnMusicOpen;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnMusicOpen;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnMusicRefresh_Click;
						MyButton myButton = BtnMusicRefresh;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnMusicRefresh;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnMusicClear_Click;
						MyButton myButton = BtnMusicClear;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnMusicClear;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyRadioBox.PreviewCheckEventHandler obj = RadioLogoType3_Check;
						MyRadioBox myRadioBox = RadioLogoType3;
						if (myRadioBox != null)
						{
							myRadioBox.PreviewCheck -= obj;
						}
						myRadioBox = RadioLogoType3;
						if (myRadioBox != null)
						{
							myRadioBox.PreviewCheck += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnLogoChange_Click;
						MyButton myButton = BtnLogoChange;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnLogoChange;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnLogoDelete_Click;
						MyButton myButton = BtnLogoDelete;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnLogoDelete;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnCustomRefresh_Click();
						};
						MyButton myButton = BtnCustomRefresh;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnCustomRefresh;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnCustomFile_Click;
						MyButton myButton = BtnCustomFile;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnCustomFile;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnCustomTutorial_Click;
						MyButton myButton = BtnCustomTutorial;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnCustomTutorial;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenSetupMain();
						};
						MyCheckBox.ChangeEventHandler obj2 = HiddenHint;
						MyCheckBox myCheckBox = CheckHiddenPageSetup;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
							myCheckBox.Change -= obj2;
						}
						myCheckBox = CheckHiddenPageSetup;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
							myCheckBox.Change += obj2;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenOtherMain();
						};
						MyCheckBox myCheckBox = CheckHiddenPageOther;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenPageOther;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenSetupSub();
						};
						MyCheckBox myCheckBox = CheckHiddenSetupLaunch;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenSetupLaunch;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenSetupSub();
						};
						MyCheckBox myCheckBox = CheckHiddenSetupLink;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenSetupLink;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenSetupSub();
						};
						MyCheckBox.ChangeEventHandler obj2 = HiddenHint;
						MyCheckBox myCheckBox = CheckHiddenSetupUI;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
							myCheckBox.Change -= obj2;
						}
						myCheckBox = CheckHiddenSetupUI;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
							myCheckBox.Change += obj2;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							HiddenSetupSub();
						};
						MyCheckBox myCheckBox = CheckHiddenSetupSystem;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenSetupSystem;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
						MyCheckBox myCheckBox = CheckHiddenOtherHelp;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenOtherHelp;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
						MyCheckBox myCheckBox = CheckHiddenOtherAbout;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenOtherAbout;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
						MyCheckBox myCheckBox = CheckHiddenOtherTest;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenOtherTest;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenOtherNet;
						MyCheckBox myCheckBox = CheckHiddenOtherFeedback;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenOtherFeedback;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenOtherNet;
						MyCheckBox myCheckBox = CheckHiddenOtherVote;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenOtherVote;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = HiddenHint;
						MyCheckBox myCheckBox = CheckHiddenFunctionHidden;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckHiddenFunctionHidden;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
		}

	}
}
