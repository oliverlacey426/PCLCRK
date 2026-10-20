// PCL

namespace PCL
{
	[StandardModule]
	public static class ModDownload
	{
		public struct DlClientListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public JObject Value;
		}
		public struct DlOptiFineListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public List<DlOptiFineListEntry> Value;
		}
		public class DlOptiFineListEntry
		{
			public string DisplayName;
			public string FileName;
			public string InstanceName;
			public bool IsPreview;
			private string _inherit;
			public string ReleaseTime;
			public string RequiredForgeVersion;
			public string Inherit
			{
				get
				{
					return _inherit;
				}
				set
				{
					if (value.EndsWithF(".0"))
					{
						value = Strings.Left(value, checked(value.Length - 2));
					}
					_inherit = value;
				}
			}
		}
		public struct DlForgeListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public List<string> Value;
		}
		public abstract class DlForgelikeEntry : IComparable<DlForgelikeEntry>
		{
			public bool IsNeoForge;
			public Version Version;
			public string VersionName;
			public string Inherit;
			public abstract string LoaderName { get; }
			public abstract string FileExtension { get; }
			public int CompareTo(DlForgelikeEntry other)
			{
				if (Version != other.Version)
				{
					return Version.CompareTo(other.Version);
				}
				return ModMinecraft.CompareVersion(VersionName, other.VersionName);
			}
			int IComparable<DlForgelikeEntry>.CompareTo(DlForgelikeEntry other)
			{
				//ILSpy generated this explicit interface implementation from .override directive in CompareTo
				return this.CompareTo(other);
			}
		}
		public class DlForgeVersionEntry : DlForgelikeEntry
		{
			public string ReleaseTime;
			public string Hash;
			public bool IsRecommended;
			public string Category;
			public string FileVersion;
			public override string LoaderName => "Forge";
			public override string FileExtension
			{
				get
				{
					if (Operators.CompareString(Category, "installer", TextCompare: false) != 0)
					{
						return "zip";
					}
					return "jar";
				}
			}
			public DlForgeVersionEntry(string Version, string Branch, string Inherit)
			{
				Hash = null;
				switch (Version)
				{
				case "11.15.1.2318":
				case "11.15.1.1902":
				case "11.15.1.1890":
					Branch = "1.8.9";
					break;
				}
				if (Branch == null && Operators.CompareString(Inherit, "1.7.10", TextCompare: false) == 0 && Conversions.ToDouble(Version.Split(".")[3]) >= 1300.0)
				{
					Branch = "1.7.10";
				}
				IsNeoForge = false;
				VersionName = Version;
				base.Version = new Version(Version);
				base.Inherit = Inherit;
				FileVersion = Version + ((Branch == null) ? "" : ("-" + Branch));
			}
		}
		public struct DlNeoForgeListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public List<DlNeoForgeListEntry> Value;
		}
		public class DlNeoForgeListEntry : DlForgelikeEntry
		{
			public bool IsBeta;
			public string ApiName;
			public string UrlBase
			{
				get
				{
					string text = ((Operators.CompareString(Inherit, "1.20.1", TextCompare: false) == 0) ? "forge" : "neoforge");
					return $"https://maven.neoforged.net/releases/net/neoforged/{text}/{ApiName}/{text}-{ApiName}";
				}
			}
			public override string LoaderName => "NeoForge";
			public override string FileExtension => "jar";
			public DlNeoForgeListEntry(string ApiName)
			{
				IsNeoForge = true;
				this.ApiName = ApiName;
				IsBeta = ApiName.ContainsIgnoreCase("beta") || ApiName.ContainsIgnoreCase("alpha");
				if (ApiName.Contains("1.20.1"))
				{
					VersionName = ApiName.Replace("1.20.1-", "");
					Version = new Version("19." + VersionName);
					Inherit = "1.20.1";
					return;
				}
				if (ApiName.StartsWithF("0."))
				{
					VersionName = ApiName;
					string[] array = ApiName.BeforeFirst("-").Split('.');
					Version = new Version(0, 0, Conversions.ToInteger(array.Last()));
					Inherit = array[1];
					return;
				}
				VersionName = ApiName;
				Version = new Version(ApiName.BeforeFirst("-"));
				if (Version.Major >= 24)
				{
					Inherit = $"{Version.Major}.{Version.Minor}.{Version.Build}";
				}
				else
				{
					Inherit = $"1.{Version.Major}.{Version.Minor}";
				}
				if (Inherit.EndsWithF(".0"))
				{
					Inherit = Strings.Left(Inherit, checked(Inherit.Length - 2));
				}
				if (VersionName.Contains("+"))
				{
					ref string inherit = ref Inherit;
					inherit = inherit + "-" + VersionName.AfterFirst("+");
				}
			}
		}
		public struct DlLiteLoaderListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public List<DlLiteLoaderListEntry> Value;
			public Exception OfficialError;
		}
		public class DlLiteLoaderListEntry
		{
			public string FileName;
			public bool IsPreview;
			public string Inherit;
			public bool IsLegacy;
			public string ReleaseTime;
			public string MD5;
			public JToken JsonToken;
		}
		public struct DlFabricListResult
		{
			public string SourceName;
			public bool IsOfficial;
			public JObject Value;
		}
		private static List<int> _AllDrops = null;
		public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Main", DlClientListMain);
		public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListMojangLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Mojang", DlClientListMojangMain);
		public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListBmclapiLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Bmclapi", DlClientListBmclapiMain);
		public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Main", DlOptiFineListMain);
		public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListOfficialLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Official", DlOptiFineListOfficialMain);
		public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListBmclapiLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Bmclapi", DlOptiFineListBmclapiMain);
		public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Main", DlForgeListMain);
		public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListOfficialLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Official", DlForgeListOfficialMain);
		public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListBmclapiLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Bmclapi", DlForgeListBmclapiMain);
		public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Main", DlNeoForgeListMain);
		public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListOfficialLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Official", DlNeoForgeListOfficialMain);
		public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListBmclapiLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Bmclapi", DlNeoForgeListBmclapiMain);
		public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Main", DlLiteLoaderListMain);
		public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListOfficialLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Official", DlLiteLoaderListOfficialMain);
		public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListBmclapiLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Bmclapi", DlLiteLoaderListBmclapiMain);
		public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Main", DlFabricListMain);
		public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListOfficialLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Official", DlFabricListOfficialMain);
		public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListBmclapiLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Bmclapi", DlFabricListBmclapiMain);
		public static ModLoader.LoaderTask<int, List<ResourceVersion>> DlFabricApiLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("Fabric API List Loader", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
		{
			Task.Output = ResourceVersion.FromProjectId("fabric-api", ResourcePlatforms.Modrinth, LoadDependencies: false);
		});
		public static ModLoader.LoaderTask<int, List<ResourceVersion>> DlOptiFabricLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("OptiFabric List Loader", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
		{
			Task.Output = ResourceVersion.FromProjectId("322385", ResourcePlatforms.CurseForge, LoadDependencies: false);
		});
		private static bool DlPreferMojang = false;
		[SpecialName]
		private static bool _STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init;
		public static List<int> AllDrops
		{
			get
			{
				if (_AllDrops == null)
				{
					_AllDrops = (from d in Settings.Get<string>("CacheDrops").ToString().Split(",", removeEmptyEntries: true)
						select checked((int)Math.Round(ModBase.Val(d)))).ToList();
				}
				if (!_AllDrops.Any())
				{
					return null;
				}
				return _AllDrops;
			}
			set
			{
				_AllDrops = value;
				Settings.Set("CacheDrops", value.Join(','));
			}
		}
		public static bool DlSourcePreferMojang
		{
			get
			{
				if (Settings.Get<int>("ToolDownloadSource") != 2)
				{
					if (Settings.Get<int>("ToolDownloadSource") == 1)
					{
						return DlPreferMojang;
					}
					return false;
				}
				return true;
			}
		}
		public static bool DlVersionListPreferMojang
		{
			get
			{
				if (Settings.Get<int>("ToolDownloadVersion") != 2)
				{
					if (Settings.Get<int>("ToolDownloadVersion") == 1)
					{
						return DlPreferMojang;
					}
					return false;
				}
				return true;
			}
		}
		public static ModNet.NetFile DlClientJarGet(ModMinecraft.McInstance Instance, bool ReturnNothingOnFileUseable)
		{
			try
			{
				while (!string.IsNullOrEmpty(Instance.InheritName))
				{
					Instance = new ModMinecraft.McInstance(Instance.InheritName);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "获取底层继承版本失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
				ProjectData.ClearProjectError();
			}
			if (Instance.JsonObject["downloads"] == null || Instance.JsonObject["downloads"]["client"] == null || Instance.JsonObject["downloads"]["client"]["url"] == null)
			{
				throw new Exception("底层版本 " + Instance.Name + " 中无 jar 文件下载信息");
			}
			FileChecker fileChecker = new FileChecker
			{
				MinSize = 1024L,
				ActualSize = (long)(Instance.JsonObject["downloads"]["client"]["size"] ?? ((JToken)(-1))),
				Hash = (string?)Instance.JsonObject["downloads"]["client"]["sha1"]
			};
			if (ReturnNothingOnFileUseable && fileChecker.Check(Instance.PathVersion + Instance.Name + ".jar") == null)
			{
				return null;
			}
			string original = (string?)Instance.JsonObject["downloads"]["client"]["url"];
			return new ModNet.NetFile(DlSourceLauncherOrMetaGet(original), Instance.PathVersion + Instance.Name + ".jar", fileChecker);
		}
		public static ModNet.NetFile DlClientAssetIndexGet(ModMinecraft.McInstance Instance)
		{
			while (!string.IsNullOrEmpty(Instance.InheritName))
			{
				Instance = new ModMinecraft.McInstance(Instance.InheritName);
			}
			JToken jToken = ModMinecraft.McAssetsGetIndex(Instance, ReturnLegacyOnError: true, CheckURLEmpty: true);
			string localPath = ModMinecraft.McFolderSelected + "assets\\indexes\\" + jToken["id"].ToString() + ".json";
			Logger.Info(string.Format("版本 {0} 对应的资源文件索引为 {1}", Instance.Name, jToken["id"]), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			string text = (string?)(jToken["url"] ?? ((JToken)""));
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				return null;
			}
			return new ModNet.NetFile(DlSourceLauncherOrMetaGet(text), localPath, new FileChecker
			{
				IsJson = true
			});
		}
		public static List<ModLoader.LoaderBase> DlClientFix(ModMinecraft.McInstance Instance, bool CheckAssetsHash, bool DownloadAssetIndexInBackground)
		{
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析缺失支持库文件", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					Task.Output = ModMinecraft.McLibNetFilesFromInstance(Instance);
				})
				{
					ProgressWeight = 1.0
				},
				new ModNet.LoaderDownload("下载支持库文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 15.0
				}
			};
			list.Add(new ModLoader.LoaderCombo<string>("下载支持库文件（主加载器）", loaders)
			{
				Block = false,
				Show = false,
				ProgressWeight = 16.0
			});
			if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(Instance)))
			{
				Logger.Warn("设置要求跳过所有 Assets 检查，文件可能不是最新的", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			}
			else
			{
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				string text = null;
				string destFilePath = null;
				list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					try
					{
						ModNet.NetFile netFile = DlClientAssetIndexGet(Instance);
						if (netFile == null)
						{
							Task.Output = new List<ModNet.NetFile>();
							Logger.Warn($"未找到版本 {Instance.Name} 的合适的资源索引下载地址，游戏 assets 可能缺失", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
						}
						else
						{
							string text3 = ModBase.PathTemp + "Cache\\" + netFile.LocalName + "_background";
							if (FileUtils.Exists(text3) && DateTime.Compare(FileUtils.GetInfo(text3).LastWriteTime.Date, FileUtils.GetInfo(netFile.LocalPath).LastWriteTime.Date) > 0)
							{
								Logger.Info($"发现后台更新的资源索引缓存文件：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
								FileUtils.Move(text3, netFile.LocalPath);
							}
							if (DownloadAssetIndexInBackground)
							{
								if (netFile.Check.Check(netFile.LocalPath) == null)
								{
									Task.Output = new List<ModNet.NetFile>();
								}
								else
								{
									Task.Output = new List<ModNet.NetFile> { netFile };
								}
							}
							else
							{
								destFilePath = netFile.LocalPath;
								text = ModBase.PathTemp + "Cache\\" + netFile.LocalName;
								netFile.LocalPath = text;
								Task.Output = new List<ModNet.NetFile> { netFile };
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception("分析资源文件索引地址失败", innerException);
					}
				})
				{
					ProgressWeight = 0.5,
					Show = false
				});
				list2.Add(new ModNet.LoaderDownload("下载资源文件索引", new List<ModNet.NetFile>())
				{
					ProgressWeight = 2.0
				});
				list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取资源文件列表", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					if (text != null)
					{
						FileUtils.Move(text, destFilePath);
					}
					ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
					ModMinecraft.McInstance instance = Instance;
					bool checkHash = CheckAssetsHash;
					ModLoader.LoaderBase ProgressFeed = Task;
					List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList(instance, checkHash, ref ProgressFeed);
					Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)ProgressFeed;
					loaderTask.Output = output;
				})
				{
					ProgressWeight = 0.01
				});
				list2.Add(new ModNet.LoaderDownload("下载资源文件", new List<ModNet.NetFile>())
				{
					ProgressWeight = 25.0
				});
				list.Add(new ModLoader.LoaderCombo<string>("下载资源文件（主加载器）", list2)
				{
					Block = false,
					Show = false,
					ProgressWeight = 27.0
				});
				if (DownloadAssetIndexInBackground)
				{
					string text2 = null;
					string localPath = null;
					List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
					list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("后台分析资源文件索引地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
					{
						ModNet.NetFile netFile = DlClientAssetIndexGet(Instance);
						if (netFile == null)
						{
							Logger.Warn($"未找到版本 {Instance.Name} 的合适的资源索引下载地址，游戏 assets 可能缺失", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
							Task.Cancel();
							throw new OperationCanceledException();
						}
						text2 = netFile.LocalPath;
						localPath = ModBase.PathTemp + "Cache\\" + netFile.LocalName + "_background";
						netFile.LocalPath = localPath;
						Task.Output = new List<ModNet.NetFile> { netFile };
						DateTime date = FileUtils.GetInfo(text2).LastWriteTime.Date;
						if (FileUtils.Exists(text2) && Math.Abs((date - DateAndTime.Now.Date).TotalDays) < 1.0)
						{
							Logger.Info($"无需更新资源文件索引，上次更新于 {date}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
							Task.Cancel();
						}
					}));
					list3.Add(new ModNet.LoaderDownload("后台下载资源文件索引", new List<ModNet.NetFile>()));
					ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("后台更新资源文件索引", list3);
					Logger.Info("开始后台检查资源文件索引", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
					loaderCombo.Start();
				}
			}
			return list;
		}
		private static void DlClientListMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
			List<int> list = new List<int>();
			foreach (JObject item in (IEnumerable<JToken>)Loader.Output.Value["versions"])
			{
				string text = item["id"].ToString();
				if (!text.Contains("-"))
				{
					int num = ModMinecraft.McVersion.VersionToDrop(text);
					if (num != 209)
					{
						list.Add(num);
					}
				}
			}
			AllDrops = (from d in list.Distinct()
				orderby d descending
				select d).ToList();
		}
		private static void DlClientListMojangMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
		{
			long timeMs = ModBase.GetTimeMs();
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://launchermeta.mojang.com/mc/game/version_manifest.json", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				JArray jArray = (JArray)jObject["versions"];
				if (jArray.Count < 200)
				{
					throw new Exception("获取到的版本列表长度不足（" + jObject.ToString() + "）");
				}
				if (!DlPreferMojang)
				{
					long num = checked(ModBase.GetTimeMs() - timeMs);
					DlPreferMojang = num < 4000;
					Logger.Info(string.Format("Mojang 官方源加载耗时：{0}ms，{1}", num, DlPreferMojang ? "可优先使用官方源" : "不优先使用官方源"), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
				}
				if (FileUtils.Exists(ModBase.PathTemp + "Cache\\download.json"))
				{
					jArray.Merge(FileUtils.ReadAsJson(ModBase.PathTemp + "Cache\\download.json"));
				}
				Loader.Output = new DlClientListResult
				{
					IsOfficial = true,
					SourceName = "Mojang 官方源",
					Value = jObject
				};
				if (_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init == null)
				{
					Interlocked.CompareExchange(ref _STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init, ref lockTaken);
					if (_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init.State == 0)
					{
						_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init.State = 2;
						_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted = false;
					}
					else if (_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted_Init);
					}
				}
				string text = (string?)jObject["latest"]["snapshot"];
				if (Settings.Get<bool>("ToolUpdateSnapshot") && Operators.CompareString(Settings.Get<string>("ToolUpdateSnapshotLast"), "", TextCompare: false) != 0 && Operators.CompareString(Settings.Get<string>("ToolUpdateSnapshotLast"), text, TextCompare: false) != 0 && !_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted)
				{
					_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted = true;
					ModMinecraft.McDownloadClientUpdateHint(text, jObject);
				}
				Settings.Set("ToolUpdateSnapshotLast", text ?? "Nothing");
				text = (string?)jObject["latest"]["release"];
				if (Settings.Get<bool>("ToolUpdateRelease") && Operators.CompareString(Settings.Get<string>("ToolUpdateReleaseLast"), "", TextCompare: false) != 0 && Operators.CompareString(Settings.Get<string>("ToolUpdateReleaseLast"), text, TextCompare: false) != 0 && !_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted)
				{
					_STATIC_DlClientListMojangMain_011151286AC2E11878_IsHinted = true;
					ModMinecraft.McDownloadClientUpdateHint(text, jObject);
				}
				Settings.Set("ToolUpdateReleaseLast", text);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Minecraft 官方源版本列表解析失败", innerException);
			}
		}
		private static void DlClientListBmclapiMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
		{
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/mc/game/version_manifest.json", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				JArray jArray = (JArray)jObject["versions"];
				if (jArray.Count < 200)
				{
					throw new Exception("获取到的版本列表长度不足（" + jObject.ToString() + "）");
				}
				if (FileUtils.Exists(ModBase.PathTemp + "Cache\\download.json"))
				{
					jArray.Merge(FileUtils.ReadAsJson(ModBase.PathTemp + "Cache\\download.json"));
				}
				if (!string.IsNullOrEmpty(Loader.Input))
				{
					string input = Loader.Input;
					if (DlClientListLoader.Output.Value != null && !DlClientListLoader.Output.Value["versions"].Any([SpecialName] (JToken v) => (string?)v["id"] == input))
					{
						throw new Exception("BMCLAPI 源未包含目标版本 " + input);
					}
				}
				Loader.Output = new DlClientListResult
				{
					IsOfficial = false,
					SourceName = "BMCLAPI",
					Value = jObject
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Minecraft BMCLAPI 版本列表解析失败（" + jObject.ToString() + "）", innerException);
			}
		}
		public static object DlClientListGet(string Id)
		{
			object result;
			try
			{
				Id = Id.Replace("_", "-");
				if (Operators.CompareString(Id, "1.0", TextCompare: false) != 0 && Id.EndsWithF(".0"))
				{
					Id = Strings.Left(Id, checked(Id.Length - 2));
				}
				switch (DlClientListLoader.State)
				{
				case ModBase.LoadState.Finished:
					foreach (JObject item in (IEnumerable<JToken>)DlClientListLoader.Output.Value["versions"])
					{
						if (!((string?)item["id"] == Id))
						{
							continue;
						}
						result = item["url"].ToString();
						goto end_IL_0000;
					}
					DlClientListLoader.WaitForExit(Id, null, IsForceRestart: true);
					break;
				case ModBase.LoadState.Loading:
					DlClientListLoader.WaitForExit(Id);
					break;
				case ModBase.LoadState.Waiting:
				case ModBase.LoadState.Failed:
				case ModBase.LoadState.Canceled:
					DlClientListLoader.WaitForExit(Id, null, IsForceRestart: true);
					break;
				}
				foreach (JObject item2 in (IEnumerable<JToken>)DlClientListLoader.Output.Value["versions"])
				{
					if (!((string?)item2["id"] == Id))
					{
						continue;
					}
					result = item2["url"].ToString();
					goto end_IL_0000;
				}
				Logger.Warn(string.Format("未发现版本 {0} 的 json 下载地址，版本列表返回为：{1}{2}", Id, "\r\n", DlClientListLoader.Output.Value), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
				result = null;
				end_IL_0000:;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"获取版本 {Id} 的 json 下载地址失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
				result = null;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private static void DlOptiFineListMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		private static void DlOptiFineListOfficialMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
		{
			string text = ModNet.NetRequestByClientRetry("https://optifine.net/downloads", null, null, null, null, "*/*", null, RequireJson: false, Encoding.Default);
			if (text.Length < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			checked
			{
				try
				{
					List<string> list = text.RegexSearch("(?<=colForge'>)[^<]*").ToList();
					List<string> list2 = text.RegexSearch("(?<=colDate'>)[^<]+").ToList();
					List<string> list3 = text.RegexSearch("(?<=OptiFine_)[0-9A-Za-z_.]+(?=.jar\")").ToList();
					if (list2.Count != list3.Count)
					{
						throw new Exception("版本与发布时间数据无法对应");
					}
					if (list.Count != list3.Count)
					{
						throw new Exception("版本与 Forge 兼容数据无法对应");
					}
					if (list2.Count < 10)
					{
						throw new Exception("获取到的版本数量不足（" + text + "）");
					}
					List<DlOptiFineListEntry> list4 = new List<DlOptiFineListEntry>();
					int num = list2.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						list3[i] = list3[i].Replace("_", " ");
						DlOptiFineListEntry dlOptiFineListEntry = new DlOptiFineListEntry();
						dlOptiFineListEntry.DisplayName = list3[i].Replace("HD U ", "").Replace(".0 ", " ");
						dlOptiFineListEntry.ReleaseTime = new string[3]
						{
							list2[i].Split(".")[2],
							list2[i].Split(".")[1],
							list2[i].Split(".")[0]
						}.Join('/');
						dlOptiFineListEntry.IsPreview = list3[i].ContainsIgnoreCase("pre");
						dlOptiFineListEntry.Inherit = list3[i].ToString().Split(" ")[0];
						dlOptiFineListEntry.FileName = (list3[i].ContainsIgnoreCase("pre") ? "preview_" : "") + "OptiFine_" + list3[i].Replace(" ", "_") + ".jar";
						dlOptiFineListEntry.RequiredForgeVersion = list[i].Replace("Forge ", "").Replace("#", "");
						DlOptiFineListEntry dlOptiFineListEntry2 = dlOptiFineListEntry;
						if (dlOptiFineListEntry2.RequiredForgeVersion.Contains("N/A"))
						{
							dlOptiFineListEntry2.RequiredForgeVersion = null;
						}
						dlOptiFineListEntry2.InstanceName = dlOptiFineListEntry2.Inherit + "-OptiFine_" + list3[i].ToString().Replace(" ", "_").Replace(dlOptiFineListEntry2.Inherit + "_", "");
						list4.Add(dlOptiFineListEntry2);
					}
					Loader.Output = new DlOptiFineListResult
					{
						IsOfficial = true,
						SourceName = "OptiFine 官方源",
						Value = list4
					};
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("OptiFine 官方源版本列表解析失败（" + text + "）", innerException);
				}
			}
		}
		private static void DlOptiFineListBmclapiMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
		{
			JArray jArray = (JArray)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/optifine/versionList", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				List<DlOptiFineListEntry> list = new List<DlOptiFineListEntry>();
				foreach (JObject item in jArray)
				{
					DlOptiFineListEntry dlOptiFineListEntry = new DlOptiFineListEntry
					{
						DisplayName = (item["mcversion"].ToString() + item["type"].ToString().Replace("HD_U", "").Replace("_", " ") + " " + item["patch"].ToString()).Replace(".0 ", " "),
						ReleaseTime = "",
						IsPreview = item["patch"].ToString().ContainsIgnoreCase("pre"),
						Inherit = item["mcversion"].ToString(),
						FileName = item["filename"].ToString(),
						RequiredForgeVersion = (item["forge"] ?? ((JToken)"")).ToString().Replace("Forge ", "").Replace("#", "")
					};
					if (dlOptiFineListEntry.RequiredForgeVersion.Contains("N/A"))
					{
						dlOptiFineListEntry.RequiredForgeVersion = null;
					}
					dlOptiFineListEntry.InstanceName = dlOptiFineListEntry.Inherit + "-OptiFine_" + (item["type"].ToString() + " " + item["patch"].ToString()).Replace(".0 ", " ").Replace(" ", "_").Replace(dlOptiFineListEntry.Inherit + "_", "");
					list.Add(dlOptiFineListEntry);
				}
				Loader.Output = new DlOptiFineListResult
				{
					IsOfficial = false,
					SourceName = "BMCLAPI",
					Value = list
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("OptiFine BMCLAPI 版本列表解析失败（" + jArray.ToString() + "）", innerException);
			}
		}
		private static void DlForgeListMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		private static void DlForgeListOfficialMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
		{
			string text = ModNet.NetRequestByClientRetry("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_1.2.4.html", null, null, null, null, "text/html", null, RequireJson: false, Encoding.Default, SimulateBrowserHeaders: true);
			if (text.Length < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			List<string> list = text.RegexSearch("(?<=a href=\"index_)[0-9.]+((_pre|[_-]snapshot[_-]?)[0-9]?)?(?=.html)").ToList();
			list.Add("1.2.4");
			if (list.Count < 10)
			{
				throw new Exception("获取到的版本数量不足（" + text + "）");
			}
			Loader.Output = new DlForgeListResult
			{
				IsOfficial = true,
				SourceName = "Forge 官方源",
				Value = list
			};
		}
		private static void DlForgeListBmclapiMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
		{
			List<string> list = ((JArray)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/minecraft", null, null, null, null, "*/*", null, RequireJson: true, Encoding.Default).DeserializeJson()).Select([SpecialName] (JToken v) => v.ToString()).ToList();
			if (list.Count < 10)
			{
				throw new Exception("获取到的版本数量不足");
			}
			Loader.Output = new DlForgeListResult
			{
				IsOfficial = false,
				SourceName = "BMCLAPI",
				Value = list
			};
		}
		public static void DlForgeVersionMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
		{
			ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> key = new ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>("DlForgeVersion Official", DlForgeVersionOfficialMain);
			ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> key2 = new ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>("DlForgeVersion Bmclapi", DlForgeVersionBmclapiMain);
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 30),
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 5),
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 60),
					new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		public static void DlForgeVersionOfficialMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
		{
			string text;
			try
			{
				text = ModNet.NetRequestByLoader("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + Loader.Input.Replace("-", "_") + ".html", 45000, IsJson: false, SimulateBrowserHeaders: true);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (ex2.GetDisplay(multiline: false).Contains("(404)"))
				{
					throw new Exception("无");
				}
				throw;
			}
			if (text.Length < 1000)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			List<DlForgeVersionEntry> list = new List<DlForgeVersionEntry>();
			checked
			{
				try
				{
					string[] array = text.Substring(0, text.LastIndexOfF("</table>")).Split("<td class=\"download-version");
					int num = array.Count() - 1;
					for (int i = 1; i <= num; i++)
					{
						string text2 = array[i];
						try
						{
							string text3 = text2.RegexSeek("(?<=[^(0-9)]+)[0-9\\.]+");
							bool isRecommended = text2.Contains("fa promo-recommended");
							string input = Loader.Input;
							string text4 = text2.RegexSeek($"(?<=-{text3}-)[^-\"]+(?=-[a-z]+.[a-z]{{3}})");
							if (string.IsNullOrWhiteSpace(text4))
							{
								text4 = null;
							}
							string[] array2 = text2.RegexSeek("(?<=\"download-time\" title=\")[^\"]+").Split(" -:".ToCharArray());
							string releaseTime = new DateTime(Conversions.ToInteger(array2[0]), Conversions.ToInteger(array2[1]), Conversions.ToInteger(array2[2]), Conversions.ToInteger(array2[3]), Conversions.ToInteger(array2[4]), Conversions.ToInteger(array2[5]), 0, DateTimeKind.Utc).ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
							string input2;
							string category;
							if (text2.Contains("classifier-installer\""))
							{
								text2 = text2.Substring(text2.IndexOfF("installer.jar"));
								input2 = text2.RegexSeek("(?<=MD5:</strong> )[^<]+");
								category = "installer";
								goto IL_022b;
							}
							if (text2.Contains("classifier-universal\""))
							{
								text2 = text2.Substring(text2.IndexOfF("universal.zip"));
								input2 = text2.RegexSeek("(?<=MD5:</strong> )[^<]+");
								category = "universal";
								goto IL_022b;
							}
							if (text2.Contains("client.zip"))
							{
								text2 = text2.Substring(text2.IndexOfF("client.zip"));
								input2 = text2.RegexSeek("(?<=MD5:</strong> )[^<]+");
								category = "client";
								goto IL_022b;
							}
							goto end_IL_00ba;
							IL_022b:
							list.Add(new DlForgeVersionEntry(text3, text4, input)
							{
								Category = category,
								IsRecommended = isRecommended,
								Hash = input2.ReplaceLineEndings(""),
								ReleaseTime = releaseTime
							});
							end_IL_00ba:;
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception innerException = ex3;
							throw new Exception("Forge 官方源版本信息提取失败（" + text2 + "）", innerException);
						}
					}
				}
				catch (Exception ex4)
				{
					ProjectData.SetProjectError(ex4);
					Exception innerException2 = ex4;
					throw new Exception("Forge 官方源版本列表解析失败（" + text + "）", innerException2);
				}
				if (!list.Any())
				{
					throw new Exception("无");
				}
				Loader.Output = list;
			}
		}
		public static void DlForgeVersionBmclapiMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
		{
			JArray jArray = (JArray)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/minecraft/" + Loader.Input.Replace("-", "_"), null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			List<DlForgeVersionEntry> list = new List<DlForgeVersionEntry>();
			try
			{
				string left = ModDownloadLib.McDownloadForgeRecommendedGet(Loader.Input);
				foreach (JObject item in jArray)
				{
					string hash = null;
					string category = "unknown";
					int num = -1;
					foreach (JObject item2 in (IEnumerable<JToken>)item["files"])
					{
						switch (item2["category"].ToString())
						{
						case "installer":
							if (Operators.CompareString(item2["format"].ToString(), "jar", TextCompare: false) == 0)
							{
								hash = (string?)item2["hash"];
								category = "installer";
								num = 2;
							}
							break;
						case "universal":
							if (num <= 1 && Operators.CompareString(item2["format"].ToString(), "zip", TextCompare: false) == 0)
							{
								hash = (string?)item2["hash"];
								category = "universal";
								num = 1;
							}
							break;
						case "client":
							if (num <= 0 && Operators.CompareString(item2["format"].ToString(), "zip", TextCompare: false) == 0)
							{
								hash = (string?)item2["hash"];
								category = "client";
								num = 0;
							}
							break;
						}
					}
					string branch = (string?)item["branch"];
					string text = (string?)item["version"];
					DlForgeVersionEntry dlForgeVersionEntry = new DlForgeVersionEntry(text, branch, Loader.Input)
					{
						Hash = hash,
						Category = category,
						IsRecommended = (Operators.CompareString(left, text, TextCompare: false) == 0)
					};
					item["modified"].ToString().Split('-', 'T', ':', '.', ' ', '/');
					dlForgeVersionEntry.ReleaseTime = item["modified"].ToObject<DateTime>().ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
					list.Add(dlForgeVersionEntry);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Forge BMCLAPI 版本列表解析失败（" + jArray.ToString() + "）", innerException);
			}
			if (!list.Any())
			{
				throw new Exception("无");
			}
			Loader.Output = list;
		}
		private static void DlNeoForgeListMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		private static void DlNeoForgeListOfficialMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
		{
			string text = ModNet.NetRequestByClientRetry("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/neoforge", null, null, null, null, "*/*", null, RequireJson: true);
			string text2 = ModNet.NetRequestByClientRetry("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/forge", null, null, null, null, "*/*", null, RequireJson: true);
			if (text.Length < 100 || text2.Length < 100)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			try
			{
				Loader.Output = new DlNeoForgeListResult
				{
					IsOfficial = true,
					SourceName = "NeoForge 官方源",
					Value = GetNeoForgeEntries(text, text2)
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("NeoForge 官方源版本列表解析失败（" + text + "\r\n\r\n" + text2 + "）", innerException);
			}
		}
		public static void DlNeoForgeListBmclapiMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
		{
			string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/neoforge", null, null, null, null, "*/*", null, RequireJson: true);
			string text2 = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/forge", null, null, null, null, "*/*", null, RequireJson: true);
			if (text.Length < 100 || text2.Length < 100)
			{
				throw new Exception("获取到的版本列表长度不足（" + text + "）");
			}
			try
			{
				Loader.Output = new DlNeoForgeListResult
				{
					IsOfficial = true,
					SourceName = "BMCLAPI",
					Value = GetNeoForgeEntries(text, text2)
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("NeoForge BMCLAPI 版本列表解析失败（" + text + "\r\n\r\n" + text2 + "）", innerException);
			}
		}
		private static List<DlNeoForgeListEntry> GetNeoForgeEntries(string LatestJson, string LatestLegacyJson)
		{
			List<DlNeoForgeListEntry> list = (from name in (LatestLegacyJson + LatestJson).RegexSearch("(?<=\")(1\\.20\\.1-)?\\d+\\.[^\\.]+\\.\\d+(\\.\\d+)?(-(beta|alpha)(\\.\\d+)?)?(\\+snapshot-\\d+)?(?=\")")
				where Operators.CompareString(name, "47.1.82", TextCompare: false) != 0
				select new DlNeoForgeListEntry(name) into a
				orderby a descending
				select a).ToList();
			if (!list.Any())
			{
				throw new Exception("无");
			}
			return list;
		}
		private static void DlLiteLoaderListMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		private static void DlLiteLoaderListOfficialMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
		{
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://dl.liteloader.com/versions/versions.json", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				JObject jObject2 = (JObject)jObject["versions"];
				List<DlLiteLoaderListEntry> list = new List<DlLiteLoaderListEntry>();
				foreach (KeyValuePair<string, JToken> item in jObject2)
				{
					if (!item.Key.StartsWithF("1.6") && !item.Key.StartsWithF("1.5"))
					{
						JToken jToken = (item.Value["artefacts"] ?? item.Value["snapshots"])["com.mumfrey:liteloader"]["latest"];
						list.Add(new DlLiteLoaderListEntry
						{
							Inherit = item.Key,
							IsLegacy = (Conversions.ToDouble(item.Key.Split(".")[1]) < 8.0),
							IsPreview = (Operators.CompareString(jToken["stream"].ToString().Lower(), "snapshot", TextCompare: false) == 0),
							FileName = "liteloader-installer-" + item.Key + ((Operators.CompareString(item.Key, "1.8", TextCompare: false) == 0 || Operators.CompareString(item.Key, "1.9", TextCompare: false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
							MD5 = (string?)jToken["md5"],
							ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)jToken["timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
							JsonToken = jToken
						});
					}
				}
				Loader.Output = new DlLiteLoaderListResult
				{
					IsOfficial = true,
					SourceName = "LiteLoader 官方源",
					Value = list
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("LiteLoader 官方源版本列表解析失败（" + jObject.ToString() + "）", innerException);
			}
		}
		private static void DlLiteLoaderListBmclapiMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
		{
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/maven/com/mumfrey/liteloader/versions.json", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				JObject jObject2 = (JObject)jObject["versions"];
				List<DlLiteLoaderListEntry> list = new List<DlLiteLoaderListEntry>();
				foreach (KeyValuePair<string, JToken> item in jObject2)
				{
					if (!item.Key.StartsWithF("1.6") && !item.Key.StartsWithF("1.5"))
					{
						JToken jToken = (item.Value["artefacts"] ?? item.Value["snapshots"])["com.mumfrey:liteloader"]["latest"];
						list.Add(new DlLiteLoaderListEntry
						{
							Inherit = item.Key,
							IsLegacy = (Conversions.ToDouble(item.Key.Split(".")[1]) < 8.0),
							IsPreview = (Operators.CompareString(jToken["stream"].ToString().Lower(), "snapshot", TextCompare: false) == 0),
							FileName = "liteloader-installer-" + item.Key + ((Operators.CompareString(item.Key, "1.8", TextCompare: false) == 0 || Operators.CompareString(item.Key, "1.9", TextCompare: false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
							MD5 = (string?)jToken["md5"],
							ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)jToken["timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
							JsonToken = jToken
						});
					}
				}
				Loader.Output = new DlLiteLoaderListResult
				{
					IsOfficial = false,
					SourceName = "BMCLAPI",
					Value = list
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("LiteLoader BMCLAPI 版本列表解析失败（" + jObject.ToString() + "）", innerException);
			}
		}
		private static void DlFabricListMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
		{
			switch (Settings.Get<int>("ToolDownloadVersion"))
			{
			case 0:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 30),
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 90)
				}, Loader.IsForceRestarting);
				break;
			case 1:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 5),
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 35)
				}, Loader.IsForceRestarting);
				break;
			default:
				DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
				{
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 60),
					new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 120)
				}, Loader.IsForceRestarting);
				break;
			}
		}
		private static void DlFabricListOfficialMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
		{
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://meta.fabricmc.net/v2/versions", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				DlFabricListResult output = new DlFabricListResult
				{
					IsOfficial = true,
					SourceName = "Fabric 官方源",
					Value = jObject
				};
				if (output.Value["game"] == null || output.Value["loader"] == null || output.Value["installer"] == null)
				{
					throw new Exception("获取到的列表缺乏必要项");
				}
				Loader.Output = output;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Fabric 官方源版本列表解析失败（" + jObject.ToString() + "）", innerException);
			}
		}
		private static void DlFabricListBmclapiMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
		{
			JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/fabric-meta/v2/versions", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
			try
			{
				DlFabricListResult output = new DlFabricListResult
				{
					IsOfficial = false,
					SourceName = "BMCLAPI",
					Value = jObject
				};
				if (output.Value["game"] == null || output.Value["loader"] == null || output.Value["installer"] == null)
				{
					throw new Exception("获取到的列表缺乏必要项");
				}
				Loader.Output = output;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Fabric BMCLAPI 版本列表解析失败（" + jObject.ToString() + "）", innerException);
			}
		}
		public static JToken DlModRequest(string Url, HttpMethod Method = null, string Content = null, string ContentType = null)
		{
			List<(string, int)> list = new List<(string, int)>();
			string text = DlSourceModGet(Url);
			if (Operators.CompareString(text, Url, TextCompare: false) != 0)
			{
				switch (Settings.Get<int>("ToolDownloadMod"))
				{
				case 0:
					list.Add((text, 10));
					list.Add((text, 10));
					list.Add((Url, 30));
					break;
				case 1:
					list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
					list.Add((text, 10));
					list.Add((Url, 30));
					list.Add((text, 30));
					break;
				default:
					list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
					list.Add((Url, 30));
					list.Add((text, 30));
					break;
				}
			}
			else
			{
				list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
				list.Add((Url, 30));
			}
			List<Exception> list2 = new List<Exception>();
			foreach (var item2 in list)
			{
				try
				{
					return ModNet.NetRequestByClient(item2.Item1, Method, Content, ContentType, "*/*", checked(item2.Item2 * 1000), null, RequireJson: true, Encoding.UTF8).DeserializeJson();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception item = ex;
					list2.Add(item);
					ProjectData.ClearProjectError();
				}
			}
			throw new AggregateException(list2);
		}
		public static IEnumerable<string> DlSourceOrder(IEnumerable<string> OfficialUrls, IEnumerable<string> MirrorUrls)
		{
			if (!DlSourcePreferMojang)
			{
				return MirrorUrls.Union(OfficialUrls);
			}
			return OfficialUrls.Union(MirrorUrls);
		}
		public static IEnumerable<string> DlVersionListOrder(IEnumerable<string> OfficialUrls, IEnumerable<string> MirrorUrls)
		{
			if (!DlVersionListPreferMojang)
			{
				return MirrorUrls.Union(OfficialUrls);
			}
			return OfficialUrls.Union(MirrorUrls);
		}
		public static IEnumerable<string> DlSourceAssetsGet(string Original)
		{
			Original = Original.Replace("http://resources.download.minecraft.net", "https://resources.download.minecraft.net");
			return DlSourceOrder(new string[1] { Original }, new string[1] { Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://resources.download.minecraft.net", "https://bmclapi2.bangbang93.com/assets") });
		}
		public static IEnumerable<string> DlSourceLibraryGet(string Original)
		{
			if (new string[3] { "minecraftforge", "fabricmc", "neoforged" }.Any([SpecialName] (string k) => Original.Contains(k)))
			{
				return new string[2]
				{
					Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/maven"),
					Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/libraries")
				};
			}
			return DlSourceOrder(new string[1] { Original }, new string[2]
			{
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/maven"),
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/libraries")
			});
		}
		public static IEnumerable<string> DlSourceLauncherOrMetaGet(string Original)
		{
			if (Original == null)
			{
				throw new Exception("无对应的 json 下载地址");
			}
			return DlSourceOrder(new string[1] { Original }, new string[1] { Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://launcher.mojang.com", "https://bmclapi2.bangbang93.com")
				.Replace("https://launchermeta.mojang.com", "https://bmclapi2.bangbang93.com") });
		}
		public static string DlSourceModGet(string Original)
		{
			return Original.Replace("api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("staging-api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("cdn.modrinth.com", "mod.mcimirror.top")
				.Replace("api.curseforge.com", "mod.mcimirror.top/curseforge")
				.Replace("edge.forgecdn.net", "mod.mcimirror.top")
				.Replace("mediafilez.forgecdn.net", "mod.mcimirror.top")
				.Replace("media.forgecdn.net", "mod.mcimirror.top");
		}
		private static void DlSourceLoader<InputType, OutputType>(ModLoader.LoaderTask<InputType, OutputType> MainLoader, List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList, bool IsForceRestart = false)
		{
			int num = 0;
			checked
			{
				do
				{
					bool flag = true;
					foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> Loader in LoaderList)
					{
						if (num != 0 || (!IsForceRestart && !((Loader.Key.Input == null) ^ (MainLoader.Input == null)) && (Loader.Key.Input == null || Loader.Key.Input.Equals(MainLoader.Input))))
						{
							if (Loader.Key.State != ModBase.LoadState.Failed)
							{
								flag = false;
							}
							if (Loader.Key.State == ModBase.LoadState.Finished)
							{
								MainLoader.Output = Loader.Key.Output;
								DlSourceLoaderCancel(LoaderList);
								return;
							}
							if (flag && num < Loader.Value * 100)
							{
								num = Loader.Value * 100;
							}
						}
					}
					if (num == 0)
					{
						LoaderList.First().Key.Start(MainLoader.Input, IsForceRestart);
						foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> item in LoaderList.Skip(1))
						{
							item.Key.State = ModBase.LoadState.Waiting;
						}
					}
					int num2 = LoaderList.Count - 1;
					for (int i = 0; i <= num2; i++)
					{
						if (num != LoaderList[i].Value * 100)
						{
							continue;
						}
						if (i < LoaderList.Count - 1 && !LoaderList.All([SpecialName] (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> l) => l.Key.State == ModBase.LoadState.Failed))
						{
							LoaderList[i + 1].Key.Start(MainLoader.Input, IsForceRestart);
							break;
						}
						Exception ex = null;
						int num3 = LoaderList.Count - 1;
						for (int num4 = 0; num4 <= num3; num4++)
						{
							LoaderList[num4].Key.Input = default;
							if (LoaderList[num4].Key.Error != null && (ex == null || Operators.CompareString(LoaderList[num4].Key.Error.Message, "无", TextCompare: false) == 0))
							{
								ex = LoaderList[num4].Key.Error;
							}
						}
						if (ex == null)
						{
							ex = new TimeoutException("下载源连接超时");
						}
						DlSourceLoaderCancel(LoaderList);
						throw ex;
					}
					Thread.Sleep(10);
					num++;
				}
				while (!MainLoader.IsCanceled);
				DlSourceLoaderCancel(LoaderList);
			}
		}
		private static void DlSourceLoaderCancel<InputType, OutputType>(List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList)
		{
			foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> Loader in LoaderList)
			{
				if (Loader.Key.State == ModBase.LoadState.Loading)
				{
					Loader.Key.Cancel();
				}
			}
		}
	}
}
