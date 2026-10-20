// PCL

namespace PCL
{
	public class Settings
	{
		public enum Sources
		{
			Normal,
			Registry,
			Instance
		}
		public class Setting
		{
			public string Key;
			public bool Encrypted;
			public object DefaultValue;
			public Sources Source;
			public Action<object> OnChanged;
			public Type Type;
			public ConcurrentDictionary<string, object> ValueCache;
			public Setting(string Key, object Value, Sources Source = Sources.Normal, bool Encrypted = false, Action<object> OnChanged = null)
			{
				ValueCache = new ConcurrentDictionary<string, object>();
				try
				{
					this.Key = Key;
					DefaultValue = RuntimeHelpers.GetObjectValue(Value);
					this.Encrypted = Encrypted;
					this.Source = Source;
					Type = (Value ?? "").GetType();
					this.OnChanged = OnChanged;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "初始化设置项失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
					ProjectData.ClearProjectError();
				}
			}
			public object GetCache(ModMinecraft.McInstance Instance)
			{
				string key = ((Source == Sources.Instance) ? Instance.PathVersion : "");
				object value = null;
				if (!ValueCache.TryGetValue(key, out value))
				{
					return null;
				}
				return value;
			}
			public void Set(object Value, ModMinecraft.McInstance Instance)
			{
				string key = ((Source == Sources.Instance) ? Instance.PathVersion : "");
				ValueCache[key] = RuntimeHelpers.GetObjectValue(Value);
			}
			public void Save(ModMinecraft.McInstance Instance = null)
			{
				string text = Conversions.ToString(GetCache(Instance));
				Logger.Trace(string.Format("保存设置：{0} {1}{2}", Key, Encrypted ? "" : ("= " + text), ((object)Instance == null) ? "" : $"（实例：{Instance?.PathVersion}）"), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
				if (Encrypted)
				{
					try
					{
						if (text == null)
						{
							text = "";
						}
						text = DesEncrypt(text, "PCL" + ModBase.Identify);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"加密设置失败：{Key}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
						ProjectData.ClearProjectError();
					}
				}
				switch (Source)
				{
				case Sources.Normal:
					ModBase.WriteIni("Setup", Key, text);
					break;
				case Sources.Registry:
					try
					{
						RegistryUtils.Write(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), Key, text);
						break;
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Warn(ex4, $"写入注册表失败（{Key} → {text}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
						ProjectData.ClearProjectError();
						break;
					}
				case Sources.Instance:
					if ((object)Instance == null)
					{
						throw new Exception($"保存版本独立设置 {Key} 时未提供目标版本");
					}
					ModBase.WriteIni(Instance.PathVersion + "PCL\\Setup.ini", Key, text);
					break;
				}
			}
		}
		public static readonly Dictionary<string, Setting> Entries = new List<Setting>
		{
			new Setting("Identify", "", Sources.Registry),
			new Setting("WindowHeight", 550),
			new Setting("WindowWidth", 900),
			new Setting("AprilYear", 0, Sources.Registry, Encrypted: true),
			new Setting("Potatoes", "", Sources.Registry, Encrypted: true),
			new Setting("HintDownloadThread", false, Sources.Registry),
			new Setting("HintNotice", 0, Sources.Registry),
			new Setting("HintDownload", 0, Sources.Registry),
			new Setting("HintHide", false, Sources.Registry),
			new Setting("HintHandInstall", false, Sources.Registry),
			new Setting("HintBuy", false, Sources.Registry),
			new Setting("HintClearRubbish", 0, Sources.Registry),
			new Setting("HintUpdateMod", false, Sources.Registry),
			new Setting("HintCustomCommand", false, Sources.Registry),
			new Setting("HintCustomWarn", false, Sources.Registry),
			new Setting("HintMoreAdvancedSetup", false, Sources.Registry),
			new Setting("HintIndieSetup", false, Sources.Registry),
			new Setting("HintExportConfig", false, Sources.Registry),
			new Setting("HintSnapshot", false, Sources.Registry),
			new Setting("SystemEulaVersion", 0, Sources.Registry),
			new Setting("SystemCount", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemLaunchCount", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemLastVersionReg", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemHighestSavedBetaVersionReg", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemHighestBetaVersionReg", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemHighestAlphaVersionReg", 0, Sources.Registry, Encrypted: true),
			new Setting("SystemHelpVersion", 0, Sources.Registry),
			new Setting("SystemDebugMode", false, Sources.Registry, Encrypted: false, [SpecialName] (object a0) =>
			{
				((VB_AnonymousDelegate_10)([SpecialName] () =>
				{
					Logger.Instance.MinLevel = ((!ModBase.ModeDebug) ? LogLevel.Info : LogLevel.Trace);
				}))();
			}),
			new Setting("SystemDebugAnim", 9, Sources.Registry),
			new Setting("SystemDebugDelay", false, Sources.Registry),
			new Setting("SystemDebugSkipCopy", false, Sources.Registry),
			new Setting("SystemSystemCache", "", Sources.Registry),
			new Setting("SystemSystemUpdate", 0),
			new Setting("SystemSystemActivity", 0),
			new Setting("SystemSystemTelemetry", true, Sources.Registry),
			new Setting("CacheDrops", "", Sources.Registry),
			new Setting("CacheConfig", 0, Sources.Registry),
			new Setting("CacheExportConfig", "", Sources.Registry),
			new Setting("CacheSavedPageUrl", "", Sources.Registry),
			new Setting("CacheSavedPageVersion", "", Sources.Registry),
			new Setting("CacheMsOAuthRefresh", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsAccess", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsProfileJson", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsUuid", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsName", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2Migrated", false, Sources.Registry),
			new Setting("CacheMsV2OAuthRefresh", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2Access", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2ProfileJson", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2Uuid", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2Name", "", Sources.Registry, Encrypted: true),
			new Setting("CacheMsV2Expires", 0L, Sources.Registry, Encrypted: true),
			new Setting("CacheNideAccess", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNideClient", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNideUuid", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNideName", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNideUsername", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNidePass", "", Sources.Registry, Encrypted: true),
			new Setting("CacheNideServer", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthAccess", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthClient", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthUuid", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthName", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthUsername", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthPass", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthServerServer", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthServerName", "", Sources.Registry, Encrypted: true),
			new Setting("CacheAuthServerRegister", "", Sources.Registry, Encrypted: true),
			new Setting("CacheDownloadFolder", "", Sources.Registry),
			new Setting("CacheJavaListVersion", 0, Sources.Registry),
			new Setting("LoginRemember", true, Sources.Registry, Encrypted: true),
			new Setting("LoginLegacyName", "", Sources.Registry, Encrypted: true),
			new Setting("LoginMsJson", "{}", Sources.Registry, Encrypted: true),
			new Setting("LoginNideEmail", "", Sources.Registry, Encrypted: true),
			new Setting("LoginNidePass", "", Sources.Registry, Encrypted: true),
			new Setting("LoginAuthEmail", "", Sources.Registry, Encrypted: true),
			new Setting("LoginAuthPass", "", Sources.Registry, Encrypted: true),
			new Setting("LoginType", ModLaunch.McLoginType.Legacy, Sources.Registry),
			new Setting("LoginPageType", 0),
			new Setting("LaunchSkinID", "", Sources.Registry, Encrypted: false, [SpecialName] (object a0) =>
			{
				((VB_AnonymousDelegate_10)([SpecialName] () =>
				{
					PageLaunchLeft.SkinLegacy.Start();
				}))();
			}),
			new Setting("LaunchSkinType", 0, Sources.Registry, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupLaunch.UpdateSkinType();
			}),
			new Setting("LaunchSkinSlim", false, Sources.Registry),
			new Setting("LaunchFolderSelect", ""),
			new Setting("LaunchFolders", "", Sources.Registry),
			new Setting("LaunchArgumentTitle", ""),
			new Setting("LaunchArgumentInfo", "PCL"),
			new Setting("LaunchArgumentJavaSelect", "", Sources.Registry),
			new Setting("LaunchArgumentJavaAll", "[]", Sources.Registry),
			new Setting("LaunchArgumentIndie", 0),
			new Setting("LaunchArgumentIndieV2", 4),
			new Setting("LaunchArgumentVisible", 5, Sources.Registry),
			new Setting("LaunchArgumentPriority", 1, Sources.Registry),
			new Setting("LaunchArgumentWindowWidth", 854),
			new Setting("LaunchArgumentWindowHeight", 480),
			new Setting("LaunchArgumentWindowType", 1),
			new Setting("LaunchArgumentRam", false, Sources.Registry),
			new Setting("LaunchAdvanceJvm", "-XX:-OmitStackTraceInFastThrow -Djdk.lang.Process.allowAmbiguousCommands=True -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True"),
			new Setting("LaunchAdvanceGame", ""),
			new Setting("LaunchAdvanceRun", ""),
			new Setting("LaunchAdvanceRunWait", true),
			new Setting("LaunchAdvanceDisableJLW", false),
			new Setting("LaunchAdvanceDisableLUA", false),
			new Setting("LaunchAdvanceGraphicCard", true, Sources.Registry),
			new Setting("LaunchAdvanceGC", 4),
			new Setting("LaunchRamType", 0, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupLaunch.UpdateRamType();
			}),
			new Setting("LaunchRamCustom", 15),
			new Setting("LinkLastAutoJoinInviteCode", "", Sources.Registry),
			new Setting("LinkLatencyMode", 0, Sources.Registry),
			new Setting("LinkCustomPeer", ""),
			new Setting("LinkEasyTierVersion", -1, Sources.Registry),
			new Setting("ToolHelpChinese", true, Sources.Registry),
			new Setting("ToolDownloadThread", 63, Sources.Registry),
			new Setting("ToolDownloadSpeed", 42, Sources.Registry, Encrypted: false, [SpecialName] (object a0) =>
			{
				ModNet.UpdateNetTaskSpeedLimitHigh(Conversions.ToInteger(a0));
			}),
			new Setting("ToolDownloadSource", 1, Sources.Registry),
			new Setting("ToolDownloadVersion", 1, Sources.Registry),
			new Setting("ToolDownloadTranslate", 0, Sources.Registry),
			new Setting("ToolDownloadTranslateV2", 1, Sources.Registry),
			new Setting("ToolDownloadCert", false, Sources.Registry, Encrypted: false, [SpecialName] (object a0) =>
			{
				ModNet.ShouldValidateSslCertificateOnLogin(Conversions.ToBoolean(a0));
			}),
			new Setting("ToolDownloadMod", 2, Sources.Registry),
			new Setting("ToolModLocalNameStyle", 0, Sources.Registry),
			new Setting("ToolUpdateRelease", false, Sources.Registry),
			new Setting("ToolUpdateSnapshot", false, Sources.Registry),
			new Setting("ToolUpdateReleaseLast", "", Sources.Registry),
			new Setting("ToolUpdateSnapshotLast", "", Sources.Registry),
			new Setting("UiLauncherTransparent", 600, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				((VB_AnonymousDelegate_24<int>)([SpecialName] (int Value) =>
				{
					if (ModMain.FrmMain != null)
					{
						ModMain.FrmMain.Opacity = (double)Value / 1000.0 + 0.4;
					}
				}))(Conversions.ToInteger(a0));
			}),
			new Setting("UiLauncherHue", 180),
			new Setting("UiLauncherSat", 80),
			new Setting("UiLauncherDelta", 90),
			new Setting("UiLauncherLight", 20),
			new Setting("UiLauncherTheme", 0, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				ModSecret.ThemeRefresh(Conversions.ToInteger(a0));
			}),
			new Setting("UiLauncherThemeHide", "0|1|2|3|4", Sources.Registry, Encrypted: true),
			new Setting("UiLauncherThemeHide2", "0|1|2|3|4", Sources.Registry, Encrypted: true),
			new Setting("UiLauncherLogo", true),
			new Setting("UiLauncherEmail", false),
			new Setting("UiBackgroundColorful", true, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				((VB_AnonymousDelegate_10)([SpecialName] () =>
				{
					ModSecret.ThemeRefresh();
				}))();
			}),
			new Setting("UiBackgroundOpacity", 1000, Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiBackgroundBlur", 0, Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiBackgroundSuit", 0, Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiCustomType", 0, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.OnMainPageTypeChanged();
			}),
			new Setting("UiCustomPreset", 0),
			new Setting("UiCustomNet", ""),
			new Setting("UiLogoType", 1, Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiLogoText", "", Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiLogoLeft", false, Sources.Normal, Encrypted: false, FormMain.UpdateBackgroundAndTitleBar),
			new Setting("UiMusicVolume", 500),
			new Setting("UiMusicStop", false),
			new Setting("UiMusicStart", false),
			new Setting("UiMusicRandom", true),
			new Setting("UiMusicAuto", true),
			new Setting("UiHiddenPageDownload", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenPageLink", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenPageSetup", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenPageOther", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenFunctionSelect", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenFunctionModUpdate", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenFunctionHidden", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenSetupLaunch", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenSetupUi", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenSetupLink", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenSetupSystem", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenOtherHelp", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenOtherFeedback", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenOtherVote", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenOtherAbout", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("UiHiddenOtherTest", false, Sources.Normal, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageSetupUI.HiddenRefresh();
			}),
			new Setting("VersionAdvanceJvm", "", Sources.Instance),
			new Setting("VersionAdvanceGame", "", Sources.Instance),
			new Setting("VersionAdvanceAssets", 0, Sources.Instance),
			new Setting("VersionAdvanceAssetsV2", false, Sources.Instance),
			new Setting("VersionAdvanceRun", "", Sources.Instance),
			new Setting("VersionAdvanceRunWait", true, Sources.Instance),
			new Setting("VersionAdvanceDisableJLW", false, Sources.Instance),
			new Setting("VersionAdvanceDisableLUA", false, Sources.Instance),
			new Setting("VersionAdvanceDisableModUpdate", false, Sources.Instance),
			new Setting("VersionAdvanceGC", 0, Sources.Instance),
			new Setting("VersionRamType", 2, Sources.Instance, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageInstanceSetup.OnVersionRamTypeChanged(Conversions.ToInteger(a0));
			}),
			new Setting("VersionRamCustom", 15, Sources.Instance),
			new Setting("VersionRamOptimize", 0, Sources.Instance),
			new Setting("VersionArgumentTitle", "", Sources.Instance),
			new Setting("VersionArgumentInfo", "", Sources.Instance),
			new Setting("VersionArgumentIndie", -1, Sources.Instance),
			new Setting("VersionArgumentIndieV2", false, Sources.Instance),
			new Setting("VersionArgumentJavaSelect", "使用全局设置", Sources.Instance),
			new Setting("VersionArgumentJavaV2", 0, Sources.Instance),
			new Setting("VersionArgumentJavaRange", "", Sources.Instance),
			new Setting("VersionServerEnter", "", Sources.Instance),
			new Setting("VersionServerLogin", 0, Sources.Instance, Encrypted: false, [SpecialName] (object a0) =>
			{
				PageInstanceSetup.OnVersionServerLoginChanged(Conversions.ToInteger(a0));
			}),
			new Setting("VersionServerNide", "", Sources.Instance),
			new Setting("VersionServerAuthRegister", "", Sources.Instance),
			new Setting("VersionServerAuthName", "", Sources.Instance),
			new Setting("VersionServerAuthServer", "", Sources.Instance)
		}.ToDictionary([SpecialName] (Setting e) => e.Key);
		private static readonly byte[] desInitialVector = Encoding.UTF8.GetBytes("95168702");
		[SpecialName]
		private static Dictionary<string, object> _STATIC_Get_021CE128798_ForcedSettings;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_Get_021CE128798_ForcedSettings_Init;
		public static void Set(string Key, object Value, ModMinecraft.McInstance Instance = null)
		{
			Setting value = null;
			if (!Entries.TryGetValue(Key, out value))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key);
			}
			try
			{
				Value = RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(RuntimeHelpers.GetObjectValue(Value), value.Type));
				if (value.GetCache(Instance) == null)
				{
					Get(Key, Instance);
				}
				if (!Operators.ConditionalCompareObjectEqual(value.GetCache(Instance), Value, TextCompare: false))
				{
					value.Set(RuntimeHelpers.GetObjectValue(Value), Instance);
					value.Save(Instance);
					if (value.OnChanged != null)
					{
						value.OnChanged(RuntimeHelpers.GetObjectValue(Value));
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"设置设置项时出错（{Key}, {RuntimeHelpers.GetObjectValue(Value)}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static void SetSafe(string Key, object Value, ModMinecraft.McInstance Instance = null)
		{
			Setting value = null;
			if (!Entries.TryGetValue(Key, out value))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key);
			}
			if (value.Encrypted)
			{
				throw new InvalidOperationException("禁止写入加密设置项：" + Key);
			}
			Set(Key, RuntimeHelpers.GetObjectValue(Value), Instance);
		}
		public static object Get(string Key, ModMinecraft.McInstance Instance = null)
		{
			Setting value = null;
			if (!Entries.TryGetValue(Key, out value))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key);
			}
			object objectValue = RuntimeHelpers.GetObjectValue(value.GetCache(Instance));
			if (objectValue != null)
			{
				return objectValue;
			}
			if (_STATIC_Get_021CE128798_ForcedSettings_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_Get_021CE128798_ForcedSettings_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_Get_021CE128798_ForcedSettings_Init, ref lockTaken);
				if (_STATIC_Get_021CE128798_ForcedSettings_Init.State == 0)
				{
					_STATIC_Get_021CE128798_ForcedSettings_Init.State = 2;
					_STATIC_Get_021CE128798_ForcedSettings = new Dictionary<string, object>
					{
						{ "UiHiddenPageLink", true },
						{ "UiHiddenSetupLink", true }
					};
				}
				else if (_STATIC_Get_021CE128798_ForcedSettings_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_Get_021CE128798_ForcedSettings_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_Get_021CE128798_ForcedSettings_Init);
				}
			}
			object value2 = null;
			if (_STATIC_Get_021CE128798_ForcedSettings.TryGetValue(Key, out value2))
			{
				value.Set(RuntimeHelpers.GetObjectValue(value2), Instance);
				return value2;
			}
			try
			{
				string text = null;
				string text2 = Conversions.ToString(value.Encrypted ? DesEncrypt(Conversions.ToString(value.DefaultValue), "PCL" + ModBase.Identify) : value.DefaultValue);
				switch (value.Source)
				{
				case Sources.Normal:
					text = ModBase.ReadIni("Setup", Key, text2);
					break;
				case Sources.Registry:
					text = Conversions.ToString(RegistryUtils.TryRead(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), Key, text2));
					break;
				case Sources.Instance:
					if ((object)Instance == null)
					{
						throw new Exception($"读取版本设置 {Key} 时未提供目标版本");
					}
					text = ModBase.ReadIni(Instance.PathVersion + "PCL\\Setup.ini", Key, text2);
					break;
				}
				if (value.Encrypted)
				{
					if (Operators.CompareString(text, text2, TextCompare: false) == 0)
					{
						text = Conversions.ToString(value.DefaultValue);
					}
					else
					{
						try
						{
							text = DesDecrypt(text, "PCL" + ModBase.Identify);
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, $"解密设置失败：{Key}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
							text = Conversions.ToString(value.DefaultValue);
							value.Set(RuntimeHelpers.GetObjectValue(value.DefaultValue), Instance);
							value.Save(Instance);
							ProjectData.ClearProjectError();
						}
					}
				}
				value.Set(RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(text, value.Type)), Instance);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"读取设置失败：{Key}", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
				value.Set(RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(RuntimeHelpers.GetObjectValue(value.DefaultValue), value.Type)), Instance);
				ProjectData.ClearProjectError();
			}
			return value.GetCache(Instance);
		}
		public static T Get<T>(string Key, ModMinecraft.McInstance Instance = null)
		{
			return Conversions.ToGenericParameter<T>(Get(Key, Instance));
		}
		public static object GetSafe(string Key, ModMinecraft.McInstance Instance = null)
		{
			Setting value = null;
			if (!Entries.TryGetValue(Key, out value))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key);
			}
			if (value.Encrypted)
			{
				throw new InvalidOperationException("禁止读取加密设置项：" + Key);
			}
			return Get(Key, Instance);
		}
		public static void Reset(string Key, ModMinecraft.McInstance Instance = null)
		{
			Setting value = null;
			if (!Entries.TryGetValue(Key, out value))
			{
				throw new KeyNotFoundException("未找到设置项：" + Key);
			}
			try
			{
				if (value.GetCache(Instance) == null)
				{
					Get(Key, Instance);
				}
				if (Operators.ConditionalCompareObjectEqual(value.GetCache(Instance), value.DefaultValue, TextCompare: false))
				{
					return;
				}
				value.Set(RuntimeHelpers.GetObjectValue(value.DefaultValue), Instance);
				switch (value.Source)
				{
				case Sources.Normal:
					ModBase.DeleteIniKey("Setup", Key);
					break;
				case Sources.Registry:
					try
					{
						RegistryUtils.Delete(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), Key);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"删除注册表失败（{Key}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
						ProjectData.ClearProjectError();
					}
					break;
				case Sources.Instance:
					if ((object)Instance == null)
					{
						throw new Exception($"重置版本设置 {Key} 时未提供目标版本");
					}
					ModBase.DeleteIniKey(Instance.PathVersion + "PCL\\Setup.ini", Key);
					break;
				}
				if (value.OnChanged != null)
				{
					value.OnChanged(RuntimeHelpers.GetObjectValue(value.DefaultValue));
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"重置设置项时出错（{Key}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static string GetDefault(string Key)
		{
			return Conversions.ToString(Entries[Key].DefaultValue);
		}
		public static bool HasSaved(string Key, ModMinecraft.McInstance Instance = null)
		{
			bool result;
			switch (Entries[Key].Source)
			{
			case Sources.Normal:
				result = ModBase.HasIniKey("Setup", Key);
				break;
			case Sources.Registry:
				try
				{
					result = RegistryUtils.Has(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), Key);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"判断注册表 {Key} 是否存在时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\Settings.vb");
					result = false;
					ProjectData.ClearProjectError();
				}
				break;
			default:
				if ((object)Instance == null)
				{
					throw new Exception($"判断版本设置 {Key} 是否存在时未提供目标版本");
				}
				result = ModBase.HasIniKey(Instance.PathVersion + "PCL\\Setup.ini", Key);
				break;
			}
			return result;
		}
		private static string DesEncrypt(string sourceString, string key = null)
		{
			key = ((key == null) ? "@;$ Abv2" : key.GetStableHashCode().ToString().EnsureLength('X', 8)
				.Substring(0, 8));
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			using DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, desInitialVector), CryptoStreamMode.Write);
			byte[] bytes2 = Encoding.UTF8.GetBytes(sourceString);
			cryptoStream.Write(bytes2, 0, bytes2.Length);
			cryptoStream.FlushFinalBlock();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
		private static string DesDecrypt(string encryptedString, string key = null)
		{
			key = ((key == null) ? "@;$ Abv2" : key.GetStableHashCode().ToString().EnsureLength('X', 8)
				.Substring(0, 8));
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			using DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, desInitialVector), CryptoStreamMode.Write);
			byte[] array = Convert.FromBase64String(encryptedString);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}
	}
}
