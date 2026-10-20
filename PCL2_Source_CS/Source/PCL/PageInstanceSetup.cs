// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageInstanceSetup : MyPageRight
	{
		[Serializable]
		[CompilerGenerated]
		internal new sealed class _Closure___
		{
			public static readonly _Closure___ _I;
			public static Func<string, Version> _I2_1;
			public static Func<string, string> _I2_0;
			public static Func<FileInfo, bool> _I12_0;
			public static MouseButtonEventHandler _I23_0;
			public static MouseButtonEventHandler _I23_4;
			static _Closure___()
			{
				_I = new _Closure___();
			}
			[SpecialName]
			internal string _Lambda___2_0(string Str)
			{
				string result;
				try
				{
					if (string.IsNullOrWhiteSpace(Str))
					{
						result = "不能为空";
					}
					else
					{
						ValueRange<Version> valueRange = ValueRange<Version>.FromString(Str, [SpecialName] (string s) => StringUtils.ParseVersionWithDefaults(s));
						if (valueRange.IsEmpty())
						{
							result = "范围下限的版本号比上限更高，导致该范围无法匹配到任何版本";
						}
						else if (valueRange.HasLower && valueRange.Lower.Major <= 1 && (valueRange.Lower.Minor > 0 || valueRange.Lower.Build > 0 || valueRange.Lower.Revision > 0))
						{
							result = "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）";
						}
						else if (valueRange.HasUpper && valueRange.Upper.Major <= 1 && (valueRange.Upper.Minor > 0 || valueRange.Upper.Build > 0 || valueRange.Upper.Revision > 0))
						{
							result = "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）";
						}
						else if (valueRange.HasUpper && valueRange.Upper.Major <= 4)
						{
							result = "该范围所要求的 Java 版本过低";
						}
						else if (valueRange.HasUpper && valueRange.IsUpperInclusive && valueRange.Upper.Minor <= 0 && valueRange.Upper.Build <= 0)
						{
							result = $"右侧闭区间的意图并不明确。如果不想允许 Java {valueRange.Upper.Major}，请改为 {ModBase.vbLQ}{valueRange.Upper.Major}){ModBase.vbRQ}。如果想允许 Java {valueRange.Upper.Major}，请改为 {ModBase.vbLQ}{checked(valueRange.Upper.Major + 1)}){ModBase.vbRQ}。";
						}
						else
						{
							result = ((!valueRange.Intersect(ValueRange<Version>.OpenClosed(new Version(5, 0), new Version(99, 0))).IsEmpty()) ? "" : "该范围无法匹配到任何常见的 Java");
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					result = ex2.Message;
					ProjectData.ClearProjectError();
				}
				return result;
			}
			[SpecialName]
			internal Version _Lambda___2_1(string s)
			{
				return StringUtils.ParseVersionWithDefaults(s);
			}
			[SpecialName]
			internal bool _Lambda___12_0(FileInfo f)
			{
				return new string[3] { ".jar", ".zip", ".litemod" }.Contains(f.Extension.Lower());
			}
			[SpecialName]
			internal void _Lambda___23_0(object sender, MouseButtonEventArgs e)
			{
				e.Handled = true;
			}
			[SpecialName]
			internal void _Lambda___23_4(object sender, MouseButtonEventArgs e)
			{
				e.Handled = true;
				ModJava.ManuallyImportJava();
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___23_0
		{
			public Java _VB_Local_JavaEntry;
			public PageInstanceSetup _VB_Me;
			public _Closure___23_0(_Closure___23_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_JavaEntry = arg0._VB_Local_JavaEntry;
				}
			}
			[SpecialName]
			internal void _Lambda___R2(object a0, MouseButtonEventArgs a1)
			{
				_Lambda___1();
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				Configs.JavaForced.Set(_VB_Local_JavaEntry, PageInstanceLeft.Instance.Config);
				_VB_Me.UpdateJavaList();
				_VB_Me.ComboArgumentJavaSelect.IsDropDownOpen = false;
			}
			[SpecialName]
			internal void _Lambda___R3(object a0, EventArgs a1)
			{
				_Lambda___2();
			}
			[SpecialName]
			internal void _Lambda___2()
			{
				ModJava.ManuallyRemoveJava(_VB_Local_JavaEntry);
			}
			[SpecialName]
			internal void _Lambda___R4(object a0, EventArgs a1)
			{
				_Lambda___3();
			}
			[SpecialName]
			internal void _Lambda___3()
			{
				ModBase.OpenExplorer(_VB_Local_JavaEntry.JavaExePath);
			}
		}
		private bool LoadedFirst;
		private int RamTextLeft;
		private int RamTextRight;
		private int ComboServerLoginLast;
		[SpecialName]
		private bool _STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init;




















































		public PageInstanceSetup()
		{
			Loaded += PageSetupSystem_Loaded;
			LoadedFirst = false;
			RamTextLeft = 2;
			RamTextRight = 1;
			InitializeComponent();
			InitConnectEvents();
		}
		private void PageSetupSystem_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToHome();
			RefreshRam(ShowAnim: false);
			checked
			{
				ModAnimation.AniControlEnabled++;
				Reload();
				ModAnimation.AniControlEnabled--;
				if (LoadedFirst)
				{
					return;
				}
				LoadedFirst = true;
				PageInstanceLeft.CurrentJavaWorker.Started += UpdateJavaRelatedUi;
				PageInstanceLeft.CurrentJavaWorker.Stopped += UpdateJavaRelatedUi;
				TextArgumentJavaRange.ValidateRules = new Collection<ValidateBase>
				{
					new ValidateFunc([SpecialName] (string Str) =>
					{
						string result;
						try
						{
							if (string.IsNullOrWhiteSpace(Str))
							{
								result = "不能为空";
							}
							else
							{
								ValueRange<Version> valueRange = ValueRange<Version>.FromString(Str, [SpecialName] (string s) => StringUtils.ParseVersionWithDefaults(s));
								result = (valueRange.IsEmpty() ? "范围下限的版本号比上限更高，导致该范围无法匹配到任何版本" : ((valueRange.HasLower && valueRange.Lower.Major <= 1 && (valueRange.Lower.Minor > 0 || valueRange.Lower.Build > 0 || valueRange.Lower.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((valueRange.HasUpper && valueRange.Upper.Major <= 1 && (valueRange.Upper.Minor > 0 || valueRange.Upper.Build > 0 || valueRange.Upper.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((valueRange.HasUpper && valueRange.Upper.Major <= 4) ? "该范围所要求的 Java 版本过低" : ((valueRange.HasUpper && valueRange.IsUpperInclusive && valueRange.Upper.Minor <= 0 && valueRange.Upper.Build <= 0) ? $"右侧闭区间的意图并不明确。如果不想允许 Java {valueRange.Upper.Major}，请改为 {ModBase.vbLQ}{valueRange.Upper.Major}){ModBase.vbRQ}。如果想允许 Java {valueRange.Upper.Major}，请改为 {ModBase.vbLQ}{valueRange.Upper.Major + 1}){ModBase.vbRQ}。" : ((!valueRange.Intersect(ValueRange<Version>.OpenClosed(new Version(5, 0), new Version(99, 0))).IsEmpty()) ? "" : "该范围无法匹配到任何常见的 Java"))))));
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							result = ex2.Message;
							ProjectData.ClearProjectError();
						}
						return result;
					})
				};
				DispatcherTimer dispatcherTimer = new DispatcherTimer();
				dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
				dispatcherTimer.Tick += [SpecialName] (object a0, EventArgs a1) =>
				{
					RefreshRam();
				};
				dispatcherTimer.Start();
			}
		}
		public void Reload()
		{
			try
			{
				_ = PageInstanceLeft.Instance.PathIndie;
				ComboArgumentIndieV2.SelectedIndex = ((!Settings.Get<bool>("VersionArgumentIndieV2", PageInstanceLeft.Instance)) ? 1 : 0);
				UpdateJavaList();
				ComboServerLogin.SelectedIndex = Settings.Get<int>("VersionServerLogin", PageInstanceLeft.Instance);
				ComboServerLoginLast = ComboServerLogin.SelectedIndex;
				UpdateServerLoginUI();
				if (Settings.Get<int>("VersionAdvanceAssets", PageInstanceLeft.Instance) == 2)
				{
					Logger.Info("已迁移老版本的关闭文件校验设置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
					Settings.Reset("VersionAdvanceAssets", PageInstanceLeft.Instance);
					Settings.Set("VersionAdvanceAssetsV2", true, PageInstanceLeft.Instance);
				}
				SettingService.RefreshSettings(this);
				OnVersionRamTypeChanged(Settings.Get<int>("VersionRamType", PageInstanceLeft.Instance));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "重载版本独立设置时出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		public void Reset()
		{
			try
			{
				SettingService.ResetSettings(this);
				Settings.Reset("VersionServerLogin", PageInstanceLeft.Instance);
				Settings.Reset("VersionArgumentIndieV2", PageInstanceLeft.Instance);
				Settings.Reset("VersionAdvanceAssets", PageInstanceLeft.Instance);
				Configs.JavaForced.Reset(PageInstanceLeft.Instance.Config);
				ModJava.JavaListRefreshWorker.Start();
				Logger.Info("已初始化版本独立设置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
				ModMain.Hint("已初始化版本独立设置！", ModMain.HintType.Green, Log: false);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "初始化版本独立设置失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
				ProjectData.ClearProjectError();
			}
			Reload();
		}
		public static void OnVersionRamTypeChanged(int Type)
		{
			if (ModMain.FrmInstanceSetup != null)
			{
				ModMain.FrmInstanceSetup.RamType(Type);
			}
		}
		public void RamType(int Type)
		{
			if (SliderRamCustom != null)
			{
				SliderRamCustom.IsEnabled = Type == 1;
			}
		}
		public void RefreshRam(bool ShowAnim)
		{
			checked
			{
				if (LabRamGame != null && LabRamUsed != null && !(ModMain.FrmMain.PageCurrent != FormMain.PageType.InstanceSetup) && ModMain.FrmInstanceLeft.PageID == FormMain.PageSubType.SetupLink)
				{
					double num = Math.Round(GetRam(PageInstanceLeft.Instance), 5);
					double num2 = Math.Round((double)MyWpfExtension.Computer.Info.TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0, 1);
					double num3 = Math.Round((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0, 1);
					double num4 = Math.Round(Math.Min(num, num3), 5);
					double num5 = Math.Round(num2 - num3, 5);
					double num6 = Math.Round((num2 - num5 - num).Clamp(0.0, 1000.0), 1);
					if (num2 <= 1.5)
					{
						SliderRamCustom.MaxValue = (int)Math.Round(Math.Max(Math.Floor((num2 - 0.3) / 0.1), 1.0));
					}
					else if (num2 <= 8.0)
					{
						SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 1.5) / 0.5) + 12.0);
					}
					else if (num2 <= 16.0)
					{
						SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 8.0) / 1.0) + 25.0);
					}
					else
					{
						SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 16.0) / 2.0) + 33.0);
					}
					LabRamGame.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject((num == Math.Floor(num)) ? (Conversions.ToString(num) + ".0") : ((object)num), " GB"), (num != num4) ? Operators.ConcatenateObject(Operators.ConcatenateObject(" (可用 ", (num4 == Math.Floor(num4)) ? (Conversions.ToString(num4) + ".0") : ((object)num4)), " GB)") : ""));
					LabRamUsed.Text = Conversions.ToString(Operators.ConcatenateObject((num5 == Math.Floor(num5)) ? (Conversions.ToString(num5) + ".0") : ((object)num5), " GB"));
					LabRamTotal.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" / ", (num2 == Math.Floor(num2)) ? (Conversions.ToString(num2) + ".0") : ((object)num2)), " GB"));
					if (ShowAnim)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[3]
						{
							ModAnimation.AaGridLengthWidth(ColumnRamUsed, num5 - ColumnRamUsed.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
							ModAnimation.AaGridLengthWidth(ColumnRamGame, num4 - ColumnRamGame.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
							ModAnimation.AaGridLengthWidth(ColumnRamEmpty, num6 - ColumnRamEmpty.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
						}, "VersionSetup Ram Grid");
					}
					else
					{
						ColumnRamUsed.Width = new GridLength(num5, GridUnitType.Star);
						ColumnRamGame.Width = new GridLength(num4, GridUnitType.Star);
						ColumnRamEmpty.Width = new GridLength(num6, GridUnitType.Star);
					}
				}
			}
		}
		private void RefreshRam()
		{
			RefreshRam(ShowAnim: true);
		}
		private void RefreshRamText()
		{
			double actualWidth = RectRamUsed.ActualWidth;
			double actualWidth2 = PanRamDisplay.ActualWidth;
			double actualWidth3 = LabRamGame.ActualWidth;
			double actualWidth4 = LabRamUsed.ActualWidth;
			double actualWidth5 = LabRamTotal.ActualWidth;
			double actualWidth6 = LabRamGameTitle.ActualWidth;
			double actualWidth7 = LabRamUsedTitle.ActualWidth;
			int num = ((!(actualWidth - 30.0 < actualWidth4) && !(actualWidth - 30.0 < actualWidth7)) ? ((actualWidth - 25.0 < actualWidth4 + actualWidth5) ? 1 : 2) : 0);
			if (RamTextLeft != num)
			{
				RamTextLeft = num;
				switch (num)
				{
				case 0:
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabRamUsed, 0.0 - LabRamUsed.Opacity, 100),
						ModAnimation.AaOpacity(LabRamTotal, 0.0 - LabRamTotal.Opacity, 100),
						ModAnimation.AaOpacity(LabRamUsedTitle, 0.0 - LabRamUsedTitle.Opacity, 100)
					}, "VersionSetup Ram TextLeft");
					break;
				case 1:
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabRamUsed, 1.0 - LabRamUsed.Opacity, 100),
						ModAnimation.AaOpacity(LabRamTotal, 0.0 - LabRamTotal.Opacity, 100),
						ModAnimation.AaOpacity(LabRamUsedTitle, 0.7 - LabRamUsedTitle.Opacity, 100)
					}, "VersionSetup Ram TextLeft");
					break;
				case 2:
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabRamUsed, 1.0 - LabRamUsed.Opacity, 100),
						ModAnimation.AaOpacity(LabRamTotal, 1.0 - LabRamTotal.Opacity, 100),
						ModAnimation.AaOpacity(LabRamUsedTitle, 0.7 - LabRamUsedTitle.Opacity, 100)
					}, "VersionSetup Ram TextLeft");
					break;
				}
			}
			int num2 = ((!(actualWidth2 < actualWidth3 + 2.0 + actualWidth) && !(actualWidth2 < actualWidth6 + 2.0 + actualWidth)) ? 1 : 0);
			if (num2 == 0)
			{
				if (ModAnimation.AniControlEnabled == 0 && (RamTextRight != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaX(LabRamGame, actualWidth2 - actualWidth3 - LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaX(LabRamGameTitle, actualWidth2 - actualWidth6 - LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
					}, "VersionSetup Ram TextRight");
				}
				else
				{
					LabRamGame.Margin = new Thickness(actualWidth2 - actualWidth3, 3.0, 0.0, 0.0);
					LabRamGameTitle.Margin = new Thickness(actualWidth2 - actualWidth6, 0.0, 0.0, 5.0);
				}
			}
			else if (ModAnimation.AniControlEnabled == 0 && (RamTextRight != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaX(LabRamGame, 2.0 + actualWidth - LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaX(LabRamGameTitle, 2.0 + actualWidth - LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
				}, "VersionSetup Ram TextRight");
			}
			else
			{
				LabRamGame.Margin = new Thickness(2.0 + actualWidth, 3.0, 0.0, 0.0);
				LabRamGameTitle.Margin = new Thickness(2.0 + actualWidth, 0.0, 0.0, 5.0);
			}
			RamTextRight = num2;
		}
		public static double GetRam(ModMinecraft.McInstance Instance)
		{
			if (Settings.Get<int>("VersionRamType", Instance) == 2)
			{
				return PageSetupLaunch.GetRam(Instance, UseVersionJavaSetup: true);
			}
			checked
			{
				double num7 = default;
				if (Settings.Get<int>("VersionRamType", Instance) == 0)
				{
					double num = Math.Round((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0;
					if ((object)Instance != null && !Instance.IsLoaded)
					{
						Instance.Load();
					}
					double val;
					double num3;
					double num4;
					double num5;
					if ((object)Instance != null && Instance.Modable)
					{
						DirectoryInfo info = DirectoryUtils.GetInfo(Instance.PathIndie + "mods\\");
						int num2 = (info.Exists ? info.GetFiles().Count([SpecialName] (FileInfo f) => new string[3] { ".jar", ".zip", ".litemod" }.Contains(f.Extension.Lower())) : 0);
						val = 0.5 + (double)num2 / 150.0;
						num3 = 1.5 + (double)num2 / 90.0;
						num4 = 2.7 + (double)num2 / 50.0;
						num5 = 4.5 + (double)num2 / 25.0;
					}
					else if ((object)Instance != null && Instance.Version.HasOptiFine)
					{
						val = 0.5;
						num3 = 1.5;
						num4 = 3.0;
						num5 = 5.0;
					}
					else
					{
						val = 0.5;
						num3 = 1.5;
						num4 = 2.5;
						num5 = 4.0;
					}
					double num6 = num3;
					num7 += Math.Min(num, num6);
					num -= num6;
					if (!(num < 0.1))
					{
						num6 = num4 - num3;
						num7 += Math.Min(num * 0.7, num6);
						num -= num6 / 0.7;
						if (!(num < 0.1))
						{
							num6 = num5 - num4;
							num7 += Math.Min(num * 0.4, num6);
							num -= num6 / 0.4;
							if (!(num < 0.1))
							{
								num6 = num5;
								num7 += Math.Min(num * 0.15, num6);
								num -= num6 / 0.15;
								_ = 0.1;
							}
						}
					}
					num7 = Math.Round(Math.Max(num7, val), 1);
				}
				else
				{
					int num8 = Settings.Get<int>("VersionRamCustom", Instance);
					if (num8 <= 12)
					{
						num7 = (double)num8 * 0.1 + 0.3;
					}
					else if (num8 <= 25)
					{
						num7 = (double)(num8 - 12) * 0.5 + 1.5;
					}
					else
					{
						num7 = ((num8 > 33) ? ((double)((num8 - 33) * 2 + 16)) : ((double)((num8 - 25) * 1 + 8)));
					}
				}
				return num7;
			}
		}
		public static void OnVersionServerLoginChanged(int Type)
		{
			if (ModMain.FrmInstanceSetup != null)
			{
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				if ((object)PageInstanceLeft.Instance != null)
				{
					PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
					ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				}
			}
		}
		private void TextServerEnter_Change()
		{
			string text = TextServerEnter.Text.Replace("：", ":").Replace("。", ".");
			if (Operators.CompareString(text, TextServerEnter.Text, TextCompare: false) != 0)
			{
				int selectionStart = TextServerEnter.SelectionStart;
				TextServerEnter.Text = text;
				TextServerEnter.SelectionStart = selectionStart;
			}
		}
		private void ComboServerLogin_Changed()
		{
			if (ModAnimation.AniControlEnabled == 0)
			{
				UpdateServerLoginUI();
				if ((ComboServerLogin.SelectedIndex != 3 || TextServerNide.IsValidated) && (ComboServerLogin.SelectedIndex != 4 || TextServerAuthServer.IsValidated) && ComboServerLoginLast != ComboServerLogin.SelectedIndex)
				{
					ComboServerLoginLast = ComboServerLogin.SelectedIndex;
					Settings.Set("VersionServerLogin", ComboServerLogin.SelectedIndex, PageInstanceLeft.Instance);
				}
			}
		}
		private void UpdateServerLoginUI()
		{
			if (LabServerNide != null)
			{
				int selectedIndex = ComboServerLogin.SelectedIndex;
				LabServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
				TextServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
				PanServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
				LabServerAuthName.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				TextServerAuthName.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				LabServerAuthRegister.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				TextServerAuthRegister.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				LabServerAuthServer.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				TextServerAuthServer.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				BtnServerAuthLittle.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
				CardServer.TriggerForceResize();
			}
		}
		private void BtnServerAuthLittle_Click(object sender, EventArgs e)
		{
			if (Operators.CompareString(TextServerAuthServer.Text, "", TextCompare: false) == 0 || Operators.CompareString(TextServerAuthServer.Text, "https://littleskin.cn/api/yggdrasil", TextCompare: false) == 0 || ModMain.MyMsgBox("即将把第三方登录设置覆盖为 LittleSkin 登录。\r\n除非你是服主，或者服主要求你这样做，否则请不要继续。\r\n\r\n是否确实需要覆盖当前设置？", "设置覆盖确认", "继续", "取消") != 2)
			{
				TextServerAuthServer.Text = "https://littleskin.cn/api/yggdrasil";
				TextServerAuthRegister.Text = "https://littleskin.cn/auth/register";
				TextServerAuthName.Text = "LittleSkin 登录";
			}
		}
		private void ReloadCurrentJava()
		{
			PageInstanceLeft.ReloadCurrentJava();
		}
		private void UpdateJavaRelatedUi()
		{
			ModBase.RunInUi(_UpdateJavaRelatedUi);
		}
		private void _UpdateJavaRelatedUi()
		{
			RedoableWorker<Java> currentJavaWorker = PageInstanceLeft.CurrentJavaWorker;
			switch (ComboArgumentJava.SelectedIndex)
			{
			case 0:
			case 2:
				HintArgumentJava.Visibility = Visibility.Visible;
				TextArgumentJavaRange.Visibility = Visibility.Collapsed;
				PanArgumentJavaSelect.Visibility = Visibility.Collapsed;
				if (!currentJavaWorker.LastSucceeded || currentJavaWorker.Running)
				{
					if (!currentJavaWorker.Running)
					{
						currentJavaWorker.Start();
					}
					HintArgumentJava.Text = "正在查找 Java……";
					HintArgumentJava.Theme = MyHint.Themes.Blue;
				}
				else if (currentJavaWorker.LastResult == null)
				{
					if (ComboArgumentJava.SelectedIndex == 0)
					{
						HintArgumentJava.Text = "你的电脑上没有可供该版本使用的 Java，PCL 会在启动游戏时自动下载。";
						HintArgumentJava.Theme = MyHint.Themes.Yellow;
					}
					else
					{
						HintArgumentJava.Text = string.Format("该版本的版本文件夹中没有发现任何 Java！{0}请点击此处打开版本文件夹，然后将 Java 文件夹复制进去。", "\r\n");
						HintArgumentJava.Theme = MyHint.Themes.Red;
					}
				}
				else
				{
					HintArgumentJava.Text = $"将会使用：{currentJavaWorker.LastResult}";
					HintArgumentJava.Theme = MyHint.Themes.Blue;
				}
				break;
			case 1:
				HintArgumentJava.Visibility = Visibility.Visible;
				TextArgumentJavaRange.Visibility = Visibility.Visible;
				PanArgumentJavaSelect.Visibility = Visibility.Collapsed;
				if (!Settings.HasSaved("VersionArgumentJavaRange", PageInstanceLeft.Instance))
				{
					string text = ModJava.GetJavaRequirement(PageInstanceLeft.Instance).Range.ToString().Replace("-∞", "").Replace("+∞", "");
					Settings.Set("VersionArgumentJavaRange", text, PageInstanceLeft.Instance);
					TextArgumentJavaRange.Text = text;
				}
				if (!string.IsNullOrEmpty(TextArgumentJavaRange.ValidateResult))
				{
					HintArgumentJava.Text = $"Java 版本范围错误：{TextArgumentJavaRange.ValidateResult}";
					HintArgumentJava.Theme = MyHint.Themes.Red;
				}
				else if (!currentJavaWorker.LastSucceeded || currentJavaWorker.Running)
				{
					if (!currentJavaWorker.Running)
					{
						currentJavaWorker.Start();
					}
					HintArgumentJava.Text = "正在查找 Java……";
					HintArgumentJava.Theme = MyHint.Themes.Blue;
				}
				else if (currentJavaWorker.LastResult == null)
				{
					HintArgumentJava.Text = string.Format("你的电脑上没有任何 Java 符合该版本范围！{0}如果 Mojang 提供了对应版本的 Java，PCL 会在启动游戏时自动下载。", "\r\n");
					HintArgumentJava.Theme = MyHint.Themes.Yellow;
				}
				else
				{
					HintArgumentJava.Text = $"将会使用：{currentJavaWorker.LastResult}";
					HintArgumentJava.Theme = MyHint.Themes.Blue;
				}
				break;
			case 3:
				HintArgumentJava.Visibility = Visibility.Collapsed;
				TextArgumentJavaRange.Visibility = Visibility.Collapsed;
				PanArgumentJavaSelect.Visibility = Visibility.Visible;
				break;
			}
		}
		private void HintArgumentJava_Click(object sender, MouseButtonEventArgs e)
		{
			if (ComboArgumentJava.SelectedIndex == 2)
			{
				PageInstanceOverall.OpenInstanceFolder(PageInstanceLeft.Instance);
			}
		}
		public void UpdateJavaList()
		{
			if (ComboArgumentJavaSelect == null || LabArgumentJava == null || !ComboArgumentJavaSelect.IsLoaded)
			{
				return;
			}
			ComboArgumentJavaSelect.Items.Clear();
			if (ModJava.JavaListRefreshWorker.Running)
			{
				BtnArgumentJavaSearch.IsEnabled = false;
				ComboArgumentJavaSelect.IsEnabled = false;
				LabArgumentJava.Text = "搜索中\u200a…";
				return;
			}
			BtnArgumentJavaSearch.IsEnabled = true;
			ComboArgumentJavaSelect.IsEnabled = true;
			Java java = Configs.JavaForced.Get(PageInstanceLeft.Instance.Config);
			if (!Configs.JavaList.Get().Any())
			{
				LabArgumentJava.Text = "未找到 Java，点击以导入已有的 Java";
			}
			else if (java == null)
			{
				LabArgumentJava.Text = "点击选择 Java\u200a…";
			}
			else
			{
				LabArgumentJava.Text = java.ToString();
			}
			try
			{
				using (IEnumerator<Java> enumerator = Configs.JavaList.Get().GetEnumerator())
				{
					_Closure___23_0 obj = default;
					while (enumerator.MoveNext())
					{
						obj = new _Closure___23_0(obj);
						obj._VB_Me = this;
						obj._VB_Local_JavaEntry = enumerator.Current;
						MyListItem myListItem = new MyListItem
						{
							FontSize = 13.0,
							Height = 24.0,
							IsScaleAnimationEnabled = false,
							Type = MyListItem.CheckType.Clickable,
							Tag = obj._VB_Local_JavaEntry,
							Title = obj._VB_Local_JavaEntry.ToString()
						};
						myListItem.MouseLeftButtonUp += ((_Closure___._I23_0 == null) ? (_Closure___._I23_0 = [SpecialName] (object sender, MouseButtonEventArgs e) =>
						{
							e.Handled = true;
						}) : _Closure___._I23_0);
						myListItem.Click += obj._Lambda___R2;
						ComboArgumentJavaSelect.Items.Add(myListItem);
						List<MyIconButton> list = new List<MyIconButton>();
						bool flag = obj._VB_Local_JavaEntry.Folder.StartsWithF($"{Paths.AppData}.minecraft\\runtime\\");
						MyIconButton myIconButton = new MyIconButton
						{
							Logo = "M508 990.4c-261.6 0-474.4-212-474.4-474.4S246.4 41.6 508 41.6s474.4 212 474.4 474.4S769.6 990.4 508 990.4zM508 136.8c-209.6 0-379.2 169.6-379.2 379.2 0 209.6 169.6 379.2 379.2 379.2s379.2-169.6 379.2-379.2C887.2 306.4 717.6 136.8 508 136.8zM697.6 563.2 318.4 563.2c-26.4 0-47.2-21.6-47.2-47.2 0-26.4 21.6-47.2 47.2-47.2l379.2 0c26.4 0 47.2 21.6 47.2 47.2C744.8 542.4 724 563.2 697.6 563.2z",
							Height = 24.0,
							Width = 24.0
						};
						myIconButton.IsEnabled = !flag;
						myIconButton.ToolTip = (myIconButton.IsEnabled ? "从列表中移除" : "无法移除官方 Java");
						myIconButton.Click += obj._Lambda___R3;
						ToolTipService.SetShowOnDisabled(myIconButton, value: true);
						list.Add(myIconButton);
						MyIconButton myIconButton2 = new MyIconButton
						{
							Logo = "M889.018182 418.909091H884.363636V316.509091a93.090909 93.090909 0 0 0-99.607272-89.832727h-302.545455l-93.090909-76.334546A46.545455 46.545455 0 0 0 358.865455 139.636364H146.152727A93.090909 93.090909 0 0 0 46.545455 229.469091V837.818182a46.545455 46.545455 0 0 0 46.545454 46.545454 46.545455 46.545455 0 0 0 16.756364-3.258181 109.381818 109.381818 0 0 0 25.134545 3.258181h586.472727a85.178182 85.178182 0 0 0 87.04-63.301818l163.374546-302.545454a46.545455 46.545455 0 0 0 5.585454-21.876364A82.385455 82.385455 0 0 0 889.018182 418.909091z m-744.727273-186.181818h198.283636l93.09091 76.334545a46.545455 46.545455 0 0 0 29.323636 10.705455h319.301818a12.101818 12.101818 0 0 1 6.516364 0V418.909091H302.545455a85.178182 85.178182 0 0 0-87.04 63.301818L139.636364 622.778182V232.727273a19.549091 19.549091 0 0 1 6.516363 0z m578.094546 552.029091a27.461818 27.461818 0 0 0-2.792728 6.516363H154.530909l147.083636-272.290909a27.461818 27.461818 0 0 0 2.792728-6.981818h565.061818z",
							LogoScale = 1.1,
							Height = 24.0,
							Width = 24.0
						};
						myIconButton2.ToolTip = "打开文件夹";
						myIconButton2.Click += obj._Lambda___R4;
						list.Add(myIconButton2);
						myListItem.Buttons = list;
					}
				}
				MyListItem myListItem2 = new MyListItem
				{
					FontSize = 13.0,
					Height = 24.0,
					IsScaleAnimationEnabled = false,
					Title = "\u200a导入电脑中已有的 Java…",
					Type = MyListItem.CheckType.Clickable
				};
				myListItem2.MouseLeftButtonUp += [SpecialName] (object sender, MouseButtonEventArgs e) =>
				{
					e.Handled = true;
					ModJava.ManuallyImportJava();
				};
				ComboArgumentJavaSelect.Items.Add(myListItem2);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "更新 Java 下拉框失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void BtnAdvanceJavaSearch_Click()
		{
			ModJava.ManuallySearchJava();
		}
		private void ComboArgumentIndieV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.AniControlEnabled != 0)
			{
				return;
			}
			if (_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init, ref lockTaken);
				if (_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init.State == 0)
				{
					_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init.State = 2;
					_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting = false;
				}
				else if (_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting_Init);
				}
			}
			if (_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting)
			{
				return;
			}
			if (ModMain.MyMsgBox("调整版本隔离设置后，你需要游戏存档、Mod 等文件手动迁移到新的游戏文件夹中。\r\n如果发现存档消失，把这项设置改回来就能恢复。\r\n如果你不会迁移存档，不建议修改这项设置！", "警告", "我知道我在做什么", "取消", "", IsWarn: true) == 2)
			{
				_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting = true;
				if (e.RemovedItems.Count > 0)
				{
					ComboArgumentIndieV2.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
				}
				_STATIC_ComboArgumentIndieV2_SelectionChanged_20211C128129_IsReverting = false;
			}
			Settings.Set("VersionArgumentIndieV2", RuntimeHelpers.GetObjectValue(Operators.CompareObjectEqual(NewLateBinding.LateGet(sender, null, "SelectedIndex", new object[0], null, null, null), 0, TextCompare: false)), PageInstanceLeft.Instance);
		}
		private void TextAdvanceRun_TextChanged(object sender, TextChangedEventArgs e)
		{
			CheckAdvanceRunWait.Visibility = ModBase.ToVisibility(Operators.CompareString(TextAdvanceRun.Text, "", TextCompare: false) != 0);
		}
		private void BtnSwitch_Click(object sender, MouseButtonEventArgs e)
		{
			ModMain.FrmMain.PageChange(FormMain.PageType.Setup);
		}
		private void ReplaceEnter(MyTextBox sender, TextChangedEventArgs e)
		{
			string text = sender.Text.ReplaceLineEndings(" ", mergeMultiple: true);
			if (Operators.CompareString(text, sender.Text, TextCompare: false) != 0)
			{
				int caretIndex = sender.CaretIndex;
				sender.Text = text;
				sender.CaretIndex = Math.Max(0, checked(caretIndex - 1));
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						SelectionChangedEventHandler value2 = ComboArgumentIndieV2_SelectionChanged;
						MyComboBox myComboBox = ComboArgumentIndieV2;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
						}
						myComboBox = ComboArgumentIndieV2;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
						}
			
			}
			{
						SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
						{
							ReloadCurrentJava();
						};
						SelectionChangedEventHandler value3 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
						{
							UpdateJavaRelatedUi();
						};
						MyComboBox myComboBox = ComboArgumentJava;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
							myComboBox.SelectionChanged -= value3;
						}
						myComboBox = ComboArgumentJava;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
							myComboBox.SelectionChanged += value3;
						}
			
			}
			{
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							ReloadCurrentJava();
						};
						TextChangedEventHandler value3 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							UpdateJavaRelatedUi();
						};
						MyTextBox myTextBox = TextArgumentJavaRange;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged -= value2;
							myTextBox.TextChanged -= value3;
						}
						myTextBox = TextArgumentJavaRange;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged += value2;
							myTextBox.TextChanged += value3;
						}
			
			}
			{
						MyHint.ClickEventHandler obj = HintArgumentJava_Click;
						MyHint myHint = HintArgumentJava;
						if (myHint != null)
						{
							myHint.Click -= obj;
						}
						myHint = HintArgumentJava;
						if (myHint != null)
						{
							myHint.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnAdvanceJavaSearch_Click();
						};
						MyIconButton myIconButton = BtnArgumentJavaSearch;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnArgumentJavaSearch;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							RefreshRam();
						};
						MyRadioBox myRadioBox = RadioRamType2;
						if (myRadioBox != null)
						{
							myRadioBox.Check -= obj;
						}
						myRadioBox = RadioRamType2;
						if (myRadioBox != null)
						{
							myRadioBox.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							RefreshRam();
						};
						MyRadioBox myRadioBox = RadioRamType0;
						if (myRadioBox != null)
						{
							myRadioBox.Check -= obj;
						}
						myRadioBox = RadioRamType0;
						if (myRadioBox != null)
						{
							myRadioBox.Check += obj;
						}
			
			}
			{
						IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
						{
							RefreshRam();
						};
						MyRadioBox myRadioBox = RadioRamType1;
						if (myRadioBox != null)
						{
							myRadioBox.Check -= obj;
						}
						myRadioBox = RadioRamType1;
						if (myRadioBox != null)
						{
							myRadioBox.Check += obj;
						}
			
			}
			{
						MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							RefreshRam();
						};
						MySlider mySlider = SliderRamCustom;
						if (mySlider != null)
						{
							mySlider.Change -= obj;
						}
						mySlider = SliderRamCustom;
						if (mySlider != null)
						{
							mySlider.Change += obj;
						}
			
			}
			{
						SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
						{
							RefreshRamText();
						};
						System.Windows.Shapes.Rectangle rectangle = RectRamGame;
						if (rectangle != null)
						{
							rectangle.SizeChanged -= value2;
						}
						rectangle = RectRamGame;
						if (rectangle != null)
						{
							rectangle.SizeChanged += value2;
						}
			
			}
			{
						SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
						{
							RefreshRamText();
						};
						System.Windows.Shapes.Rectangle rectangle = RectRamEmpty;
						if (rectangle != null)
						{
							rectangle.SizeChanged -= value2;
						}
						rectangle = RectRamEmpty;
						if (rectangle != null)
						{
							rectangle.SizeChanged += value2;
						}
			
			}
			{
						SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
						{
							RefreshRamText();
						};
						TextBlock textBlock = LabRamGame;
						if (textBlock != null)
						{
							textBlock.SizeChanged -= value2;
						}
						textBlock = LabRamGame;
						if (textBlock != null)
						{
							textBlock.SizeChanged += value2;
						}
			
			}
			{
						SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
						{
							ComboServerLogin_Changed();
						};
						MyComboBox myComboBox = ComboServerLogin;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
						}
						myComboBox = ComboServerLogin;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
						}
			
			}
			{
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							ComboServerLogin_Changed();
						};
						MyTextBox myTextBox = TextServerNide;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged -= value2;
						}
						myTextBox = TextServerNide;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged += value2;
						}
			
			}
			{
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							ComboServerLogin_Changed();
						};
						MyTextBox myTextBox = TextServerAuthServer;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged -= value2;
						}
						myTextBox = TextServerAuthServer;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged += value2;
						}
			
			}
			{
						RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							ComboServerLogin_Changed();
						};
						MyTextBox myTextBox = TextServerAuthRegister;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged -= value2;
						}
						myTextBox = TextServerAuthRegister;
						if (myTextBox != null)
						{
							myTextBox.ValidatedTextChanged += value2;
						}
			
			}
			{
						TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							TextServerEnter_Change();
						};
						MyTextBox myTextBox = TextServerEnter;
						if (myTextBox != null)
						{
							myTextBox.TextChanged -= value2;
						}
						myTextBox = TextServerEnter;
						if (myTextBox != null)
						{
							myTextBox.TextChanged += value2;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnServerAuthLittle_Click;
						MyButton myButton = BtnServerAuthLittle;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnServerAuthLittle;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							ReplaceEnter((MyTextBox)a0, a1);
						};
						MyTextBox myTextBox = TextAdvanceJvm;
						if (myTextBox != null)
						{
							myTextBox.TextChanged -= value2;
						}
						myTextBox = TextAdvanceJvm;
						if (myTextBox != null)
						{
							myTextBox.TextChanged += value2;
						}
			
			}
			{
						TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							ReplaceEnter((MyTextBox)a0, a1);
						};
						MyTextBox myTextBox = TextAdvanceGame;
						if (myTextBox != null)
						{
							myTextBox.TextChanged -= value2;
						}
						myTextBox = TextAdvanceGame;
						if (myTextBox != null)
						{
							myTextBox.TextChanged += value2;
						}
			
			}
			{
						TextChangedEventHandler value2 = TextAdvanceRun_TextChanged;
						MyTextBox myTextBox = TextAdvanceRun;
						if (myTextBox != null)
						{
							myTextBox.TextChanged -= value2;
						}
						myTextBox = TextAdvanceRun;
						if (myTextBox != null)
						{
							myTextBox.TextChanged += value2;
						}
			
			}
			{
						MyExtraTextButton.ClickEventHandler obj = BtnSwitch_Click;
						MyExtraTextButton myExtraTextButton = BtnSwitch;
						if (myExtraTextButton != null)
						{
							myExtraTextButton.Click -= obj;
						}
						myExtraTextButton = BtnSwitch;
						if (myExtraTextButton != null)
						{
							myExtraTextButton.Click += obj;
						}
			
			}
		}

	}
}
