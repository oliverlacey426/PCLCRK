// PCL

namespace PCL
{
	public class ResourceVersion
	{
		public enum ReleaseTypes
		{
			Release = 1,
			Beta,
			Alpha
		}
		public enum DownloadReason
		{
			Standalone,
			Dependency,
			ModPack,
			Update
		}
		[CompilerGenerated]
		internal sealed class _Closure___23_0
		{
			public ResourceProject _VB_Local_TargetProject;
			public _Closure___23_0(_Closure___23_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_TargetProject = arg0._VB_Local_TargetProject;
				}
			}
			[SpecialName]
			internal ResourceVersion _Lambda___0(JToken a)
			{
				return FromPlatformJson((JObject)a, _VB_Local_TargetProject.Types);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___27_0
		{
			public bool _VB_Local_IsBadDisplay;
			public MyListItem.ClickEventHandler _VB_Local_ClickHandler;
			public MyIconButton.ClickEventHandler _VB_Local_SaveAsButtonHandler;
			public ResourceVersion _VB_Me;
			[SpecialName]
			internal MyListItem _Lambda___0()
			{
				MyListItem myListItem = new MyListItem();
				_Closure___27_1 obj = new _Closure___27_1();
				obj._VB_NonLocal__VB_Closure_3 = this;
				obj._W2 = myListItem;
				obj._W2.SnapsToDevicePixels = true;
				obj._W2.Height = 42.0;
				obj._W2.Type = MyListItem.CheckType.Clickable;
				obj._W2.Tag = _VB_Me;
				obj._W2.Title = (_VB_Local_IsBadDisplay ? _VB_Me.FileName : _VB_Me.Display).BeforeLastOfAny(new string[4] { ".zip", ".jar", ".mrpack", ".litemod" }, ignoreCase: true);
				obj._W2.Logo = $"{ModBase.PathImage}ReleaseTypes/{_VB_Me.ReleaseType}.png";
				obj._W2.Click += _VB_Local_ClickHandler;
				obj._W2.Info = obj._Lambda___1().Join('，');
				if (_VB_Local_SaveAsButtonHandler != null)
				{
					MyIconButton myIconButton = new MyIconButton
					{
						Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
						ToolTip = "另存为"
					};
					myIconButton.Click += _VB_Local_SaveAsButtonHandler;
					obj._W2.Buttons = new MyIconButton[1] { myIconButton };
				}
				return myListItem;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___27_1
		{
			public MyListItem _W2;
			public _Closure___27_0 _VB_NonLocal__VB_Closure_3;
			[SpecialName]
			internal IEnumerable<string> _Lambda___1()
			{
				if (_VB_NonLocal__VB_Closure_3._VB_Me.ModLoaders != ModLoaders.None)
				{
					yield return _VB_NonLocal__VB_Closure_3._VB_Me.ModLoaders.Flags().Join('、');
				}
				if (Operators.CompareString(_W2.Title, _VB_NonLocal__VB_Closure_3._VB_Me.FileName.BeforeLast("."), TextCompare: false) != 0)
				{
					yield return _VB_NonLocal__VB_Closure_3._VB_Me.FileName.BeforeLast(".");
				}
				if (_VB_NonLocal__VB_Closure_3._VB_Me.Dependencies.Any())
				{
					yield return $"{_VB_NonLocal__VB_Closure_3._VB_Me.Dependencies.Count} 项前置";
				}
				if (_VB_NonLocal__VB_Closure_3._VB_Me.GameVersions.All([SpecialName] (string v) => !v.Contains(".") || new string[6] { "w", "snapshot", "rc", "pre", "experimental", "-" }.Any([SpecialName] (string s) => v.ContainsIgnoreCase(s))))
				{
					yield return $"游戏版本 {_VB_NonLocal__VB_Closure_3._VB_Me.GameVersions.Join('、')}";
				}
				if (_VB_NonLocal__VB_Closure_3._VB_Me.DownloadCount > 0)
				{
					yield return string.Format("下载 {0}次", (_VB_NonLocal__VB_Closure_3._VB_Me.DownloadCount > 100000) ? (Conversions.ToString(Math.Round((double)_VB_NonLocal__VB_Closure_3._VB_Me.DownloadCount / 10000.0)) + " 万") : (_VB_NonLocal__VB_Closure_3._VB_Me.DownloadCount + " "));
				}
				yield return $"更新于 {StringUtils.FormatTimeSpan(_VB_NonLocal__VB_Closure_3._VB_Me.ReleaseDate - DateTime.Now, isShort: false)}";
				if (_VB_NonLocal__VB_Closure_3._VB_Me.ReleaseType != ReleaseTypes.Release)
				{
					yield return _VB_NonLocal__VB_Closure_3._VB_Me.ReleaseTypeDisplay;
				}
			}
		}
		public string Id;
		public ResourceTypes ResourceType;
		public string Display;
		public DateTime ReleaseDate;
		public int DownloadCount;
		public string Version;
		public ModLoaders ModLoaders;
		public List<string> GameVersions;
		public ReleaseTypes ReleaseType;
		public string FileName;
		public List<string> DownloadUrls;
		public string Hash;
		public int Size;
		private List<string> RawDependencies;
		public List<string> Dependencies;
		protected internal static Dictionary<string, List<ResourceVersion>> ProjectFilesCache = new Dictionary<string, List<ResourceVersion>>();
		public string ReleaseTypeDisplay => ReleaseType switch
		{
			ReleaseTypes.Release => "正式版", 
			ReleaseTypes.Beta => ModBase.ModeDebug ? "Beta 版" : "测试版", 
			_ => ModBase.ModeDebug ? "Alpha 版" : "早期测试版", 
		};
		public bool DownloadAvailable
		{
			get
			{
				if (FileName != null)
				{
					return DownloadUrls != null;
				}
				return false;
			}
		}
		public ResourceVersion()
		{
			ModLoaders = ModLoaders.None;
			FileName = null;
			Hash = null;
			Size = -1;
			RawDependencies = new List<string>();
			Dependencies = new List<string>();
		}
		public static ResourceVersion FromPlatformJson(JObject Data, ResourceTypes ExpectedResourceType)
		{
			ResourceVersion resourceVersion = new ResourceVersion();
			ResourceVersion resourceVersion2 = resourceVersion;
			resourceVersion2.ResourceType = ExpectedResourceType;
			if (Data.ContainsKey("gameId"))
			{
				resourceVersion2.Id = (string?)Data["id"];
				resourceVersion2.Display = Data["displayName"].ToString().Replace("\t", "").Trim(' ');
				resourceVersion2.Version = null;
				resourceVersion2.ReleaseDate = (DateTime)Data["fileDate"];
				resourceVersion2.ReleaseType = (ReleaseTypes)Data["releaseType"].ToObject<int>();
				resourceVersion2.DownloadCount = (int)Data["downloadCount"];
				resourceVersion2.FileName = (string?)Data["fileName"];
				resourceVersion2.Size = (int)Data["fileLength"];
				resourceVersion2.Hash = (string?)((JArray)Data["hashes"]).ToList().FirstOrDefault([SpecialName] (JToken s) => s["algo"].ToObject<int>() == 1)?["value"];
				if (resourceVersion2.Hash == null)
				{
					resourceVersion2.Hash = (string?)((JArray)Data["hashes"]).ToList().FirstOrDefault([SpecialName] (JToken s) => s["algo"].ToObject<int>() == 2)?["value"];
				}
				string text = Data["downloadUrl"].ToString();
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					text = $"https://edge.forgecdn.net/files/{Conversions.ToInteger(resourceVersion2.Id.ToString().Substring(0, 4))}/{Conversions.ToInteger(resourceVersion2.Id.ToString().Substring(4))}/{resourceVersion2.FileName}";
				}
				resourceVersion2.DownloadUrls = ParseCurseForgeDownloadUrls(text.Replace(resourceVersion2.FileName, StringUtils.UrlEscape(resourceVersion2.FileName)));
				resourceVersion2.DownloadUrls.Add(ModDownload.DlSourceModGet(text));
				resourceVersion2.DownloadUrls = resourceVersion2.DownloadUrls.Distinct().ToList();
				if (Data.ContainsKey("dependencies"))
				{
					resourceVersion2.RawDependencies = (from d in Data["dependencies"]
						where d["relationType"].ToObject<int>() == 3 && d["modId"].ToObject<int>() != 306612 && d["modId"].ToObject<int>() != 634179
						select d["modId"].ToString()).ToList();
				}
				List<string> list = Data["gameVersions"].Select([SpecialName] (JToken t) => t.ToString().Trim().Lower()).ToList();
				resourceVersion2.GameVersions = (from v in list
					where ModMinecraft.McVersion.IsFormatFit(v)
					select v.Replace("-snapshot", " 预览版")).Distinct().ToList();
				if (resourceVersion2.GameVersions.IsSingle())
				{
					resourceVersion2.GameVersions = resourceVersion2.GameVersions.ToList();
				}
				else if (resourceVersion2.GameVersions.Count > 1)
				{
					resourceVersion2.GameVersions = ModBase.SortByComparison(resourceVersion2.GameVersions, ModMinecraft.CompareVersionGE).ToList();
					if (resourceVersion2.ResourceType == ResourceTypes.ModPack)
					{
						resourceVersion2.GameVersions = new string[1] { resourceVersion2.GameVersions[0] }.ToList();
					}
				}
				else
				{
					resourceVersion2.GameVersions = new List<string> { "未知版本" };
				}
				resourceVersion2.ModLoaders = ModLoaders.None;
				foreach (ModLoaders allFlag in EnumUtils.GetAllFlags<ModLoaders>())
				{
					if (list.Contains(allFlag.ToString().Lower()))
					{
						resourceVersion2.ModLoaders |= allFlag;
					}
				}
			}
			else
			{
				resourceVersion2.Id = (string?)Data["id"];
				resourceVersion2.Display = Data["name"].ToString().Replace("\t", "").Trim(' ');
				resourceVersion2.Version = (string?)Data["version_number"];
				resourceVersion2.ReleaseDate = (DateTime)Data["date_published"];
				resourceVersion2.ReleaseType = ((Operators.CompareString(Data["version_type"].ToString(), "release", TextCompare: false) == 0) ? ReleaseTypes.Release : ((Operators.CompareString(Data["version_type"].ToString(), "beta", TextCompare: false) == 0) ? ReleaseTypes.Beta : ReleaseTypes.Alpha));
				resourceVersion2.DownloadCount = (int)Data["downloads"];
				if (((JArray)Data["files"]).Any())
				{
					JToken jToken = Data["files"][0];
					resourceVersion2.FileName = (string?)jToken["filename"];
					resourceVersion2.DownloadUrls = new List<string>
					{
						(string?)jToken["url"],
						ModDownload.DlSourceModGet((string?)jToken["url"])
					}.Distinct().ToList();
					resourceVersion2.Size = (int)jToken["size"];
					resourceVersion2.Hash = (string?)jToken["hashes"]["sha1"];
				}
				List<string> list2 = Data["loaders"].Select([SpecialName] (JToken v) => v.ToString()).ToList();
				resourceVersion2.ModLoaders = ModLoaders.None;
				foreach (ModLoaders allFlag2 in EnumUtils.GetAllFlags<ModLoaders>())
				{
					if (list2.Contains(allFlag2.ToString().Lower()))
					{
						resourceVersion2.ModLoaders |= allFlag2;
					}
				}
				if (resourceVersion2.ResourceType.HasFlagF(ResourceTypes.Mod) || resourceVersion2.ResourceType.HasFlagF(ResourceTypes.DataPack))
				{
					if (list2.Intersect(new string[5] { "bukkit", "folia", "paper", "purpur", "spigot" }).Any())
					{
						resourceVersion2.ResourceType = ResourceTypes.Plugin;
					}
					if (list2.Contains("datapack"))
					{
						resourceVersion2.ResourceType = ResourceTypes.DataPack;
					}
					if (resourceVersion2.ModLoaders.Flags().Any())
					{
						resourceVersion2.ResourceType = ResourceTypes.Mod;
					}
				}
				if (Data.ContainsKey("dependencies"))
				{
					resourceVersion2.RawDependencies = (from d in Data["dependencies"]
						where (string?)d["dependency_type"] == "required" && (string?)d["project_id"] != "P7dR8mSH" && (string?)d["project_id"] != "qvIfYCYJ" && d["project_id"].ToString().Length > 0
						select d["project_id"].ToString()).ToList();
				}
				List<string> source = Data["game_versions"].Select([SpecialName] (JToken t) => t.ToString().Trim().Lower()).ToList();
				resourceVersion2.GameVersions = source.Where([SpecialName] (string v) => v.Contains(".")).Select([SpecialName] (string v) =>
				{
					string result = ((!v.Contains("-")) ? ((!v.StartsWithF("b1.")) ? v : "远古版本") : (v.BeforeFirst("-") + " 预览版"));
					return result;
				}).Distinct()
					.ToList();
				if (!resourceVersion2.GameVersions.IsSingle())
				{
					if (resourceVersion2.GameVersions.Count > 1)
					{
						resourceVersion2.GameVersions = ModBase.SortByComparison(resourceVersion2.GameVersions, ModMinecraft.CompareVersionGE).ToList();
						if (resourceVersion2.ResourceType == ResourceTypes.ModPack)
						{
							resourceVersion2.GameVersions = new List<string> { resourceVersion2.GameVersions[0] };
						}
					}
					else if (source.Any([SpecialName] (string v) => v.RegexCheck("[0-9]{2}w[0-9]{2}[a-z]")))
					{
						resourceVersion2.GameVersions = source.Where([SpecialName] (string v) => v.RegexCheck("[0-9]{2}w[0-9]{2}[a-z]")).ToList();
					}
					else
					{
						resourceVersion2.GameVersions = new List<string> { "未知版本" };
					}
				}
			}
			resourceVersion2 = null;
			return resourceVersion;
		}
		public static List<ResourceVersion> FromProjectId(string ProjectId, ResourcePlatforms Platform, bool LoadDependencies = true)
		{
			_Closure___23_0 arg = default;
			_Closure___23_0 CS_8__locals3 = new _Closure___23_0(arg);
			if (!Platform.Flags().IsSingle())
			{
				throw new ArgumentException($"必须指定单一平台，当前值为 {Platform}");
			}
			CS_8__locals3._VB_Local_TargetProject = ResourceProject.FromProjectId(ProjectId, Platform);
			checked
			{
				if (!ProjectFilesCache.ContainsKey(ProjectId))
				{
					Logger.Info($"开始获取项目版本列表：{ProjectId}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
					JArray source = null;
					if (Platform == ResourcePlatforms.CurseForge)
					{
						int num = 0;
						do
						{
							JObject jObject = (JObject)ModDownload.DlModRequest($"https://api.curseforge.com/v1/mods/{ProjectId}/files?pageSize=" + Conversions.ToString(10000 + num));
							if (jObject["pagination"]["resultCount"].ToObject<int>() == jObject["pagination"]["totalCount"].ToObject<int>())
							{
								source = (JArray)jObject["data"];
								break;
							}
							if (num < 2)
							{
								Logger.Warn($"CurseForge 返回的资源版本列表存在缺失，即将进行第 {num + 1} 次重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
								Logger.Info(string.Format("返回的原始内容如下：{0}{1}", "\r\n", jObject), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
								num++;
								continue;
							}
							Logger.Info(string.Format("CurseForge 返回的资源版本列表存在缺失，返回的原始内容如下：{0}{1}", "\r\n", jObject), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
							throw new Exception("CurseForge 返回的资源版本列表存在缺失");
						}
						while (num <= 2);
					}
					else
					{
						source = (JArray)ModDownload.DlModRequest($"https://api.modrinth.com/v2/project/{ProjectId}/version?include_changelog=false");
					}
					ProjectFilesCache[ProjectId] = (from a in source
						select FromPlatformJson((JObject)a, CS_8__locals3._VB_Local_TargetProject.Types) into a
						where a.DownloadAvailable
						select a).DistinctBy([SpecialName] (ResourceVersion a) => a.Id).ToList();
				}
				List<ResourceVersion> list = ProjectFilesCache[ProjectId];
				if (!LoadDependencies)
				{
					return list;
				}
				List<string> list2 = list.SelectMany([SpecialName] (ResourceVersion f) => f.RawDependencies).Distinct().ToList();
				if (list2.Any())
				{
					Logger.Info(string.Format("项目 {0} 中需要加载的前置：{1}", ProjectId, list2.Join("，")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
					foreach (ResourceProject item in ResourceProject.FromProjectIds(list2, CS_8__locals3._VB_Local_TargetProject.Platform))
					{
						foreach (ResourceVersion item2 in list)
						{
							if (item2.RawDependencies.Contains(item.Id) && Operators.CompareString(item.Id, ProjectId, TextCompare: false) != 0 && !item2.Dependencies.Contains(item.Id))
							{
								item2.Dependencies.Add(item.Id);
							}
						}
					}
				}
				return list;
			}
		}
		public static ResourceVersion FromCacheJson(JObject Data)
		{
			ResourceVersion resourceVersion;
			ResourceVersion result = (resourceVersion = new ResourceVersion());
			resourceVersion.ResourceType = Data["ResourceType"].ToObject<ResourceTypes>();
			resourceVersion.Id = Data["Id"].ToString();
			resourceVersion.Display = Data["DisplayName"].ToString();
			if (Data.ContainsKey("Version"))
			{
				resourceVersion.Version = Data["Version"].ToString();
			}
			resourceVersion.ReleaseDate = Data["ReleaseDate"].ToObject<DateTime>();
			resourceVersion.DownloadCount = Data["DownloadCount"].ToObject<int>();
			resourceVersion.ReleaseType = Data["ReleaseType"].ToObject<ReleaseTypes>();
			if (Data.ContainsKey("FileName"))
			{
				resourceVersion.FileName = Data["FileName"].ToString();
			}
			if (Data.ContainsKey("DownloadUrls"))
			{
				resourceVersion.DownloadUrls = Data["DownloadUrls"].ToObject<List<string>>();
			}
			if (Data.ContainsKey("ModLoaders"))
			{
				resourceVersion.ModLoaders = Data["ModLoaders"].ToObject<ModLoaders>();
			}
			if (Data.ContainsKey("Size"))
			{
				resourceVersion.Size = Data["Size"].ToObject<int>();
			}
			if (Data.ContainsKey("Hash"))
			{
				resourceVersion.Hash = Data["Hash"].ToString();
			}
			if (Data.ContainsKey("GameVersions"))
			{
				resourceVersion.GameVersions = Data["GameVersions"].ToObject<List<string>>();
			}
			if (Data.ContainsKey("RawDependencies"))
			{
				resourceVersion.RawDependencies = Data["RawDependencies"].ToObject<List<string>>();
			}
			if (Data.ContainsKey("Dependencies"))
			{
				resourceVersion.Dependencies = Data["Dependencies"].ToObject<List<string>>();
			}
			resourceVersion = null;
			return result;
		}
		public JObject ToCacheJson()
		{
			JObject jObject;
			JObject result = (jObject = new JObject());
			jObject.Add("ResourceType", (int)ResourceType);
			jObject.Add("Id", Id);
			if (Version != null)
			{
				jObject.Add("Version", Version);
			}
			jObject.Add("DisplayName", Display);
			jObject.Add("ReleaseDate", ReleaseDate);
			jObject.Add("DownloadCount", DownloadCount);
			jObject.Add("ModLoaders", (int)ModLoaders);
			jObject.Add("GameVersions", new JArray(GameVersions));
			jObject.Add("ReleaseType", (int)ReleaseType);
			if (FileName != null)
			{
				jObject.Add("FileName", FileName);
			}
			if (DownloadUrls != null)
			{
				jObject.Add("DownloadUrls", new JArray(DownloadUrls));
			}
			if (Hash != null)
			{
				jObject.Add("Hash", Hash);
			}
			if (Size >= 0)
			{
				jObject.Add("Size", Size);
			}
			jObject.Add("RawDependencies", new JArray(RawDependencies));
			jObject.Add("Dependencies", new JArray(Dependencies));
			jObject = null;
			return result;
		}
		public MyVirtualizingElement<MyListItem> ToListItem(MyListItem.ClickEventHandler ClickHandler, MyIconButton.ClickEventHandler SaveAsButtonHandler = null, bool IsBadDisplay = false)
		{
			_Closure___27_0 CS_8__locals0 = new _Closure___27_0();
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem();
				_Closure___27_1 obj = new _Closure___27_1();
				obj._VB_NonLocal__VB_Closure_3 = CS_8__locals0;
				obj._W2 = myListItem;
				obj._W2.SnapsToDevicePixels = true;
				obj._W2.Height = 42.0;
				obj._W2.Type = MyListItem.CheckType.Clickable;
				obj._W2.Tag = this;
				obj._W2.Title = (IsBadDisplay ? FileName : Display).BeforeLastOfAny(new string[4] { ".zip", ".jar", ".mrpack", ".litemod" }, ignoreCase: true);
				obj._W2.Logo = $"{ModBase.PathImage}ReleaseTypes/{ReleaseType}.png";
				obj._W2.Click += ClickHandler;
				obj._W2.Info = obj._Lambda___1().Join('，');
				if (SaveAsButtonHandler != null)
				{
					MyIconButton myIconButton = new MyIconButton
					{
						Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
						ToolTip = "另存为"
					};
					myIconButton.Click += SaveAsButtonHandler;
					obj._W2.Buttons = new MyIconButton[1] { myIconButton };
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}
		public ModNet.NetFile ToNetFile(string LocalFileOrFolder, DownloadReason? TrackDownloadReason = null, string TrackGameVersion = null, ModLoaders TrackLoader = ModLoaders.None)
		{
			return new ModNet.NetFile(ParseModrinthTrackArguments(DownloadUrls, TrackDownloadReason, TrackGameVersion, TrackLoader), LocalFileOrFolder + (LocalFileOrFolder.EndsWithF("\\") ? FileName : ""), new FileChecker
			{
				Hash = Hash,
				ActualSize = Size
			}, SimulateBrowserHeaders: true);
		}
		public static IEnumerable<string> ParseModrinthTrackArguments(List<string> Urls, DownloadReason? TrackDownloadReason = null, string TrackGameVersion = null, ModLoaders TrackLoader = ModLoaders.None)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (TrackDownloadReason.HasValue)
			{
				dictionary.Add("mr_download_reason", TrackDownloadReason.ToString().Lower());
			}
			if (TrackGameVersion != null)
			{
				dictionary.Add("mr_game_version", TrackGameVersion);
			}
			if (TrackLoader.Flags().Any())
			{
				dictionary.Add("mr_loader", TrackLoader.Flags().First().ToString()
					.Lower());
			}
			string arg = dictionary.Select([SpecialName] (KeyValuePair<string, string> a) => $"{a.Key}={a.Value}").Join("&");
			foreach (string Url in Urls)
			{
				if (!Url.ContainsIgnoreCase("cdn.modrinth.com/"))
				{
					yield return Url;
				}
				yield return string.Format("{0}{1}{2}", Url, Url.Contains("?") ? "&" : "?", arg);
			}
		}
		public static List<string> ParseCurseForgeDownloadUrls(string Url)
		{
			return new string[5]
			{
				Url.Replace("-service.overwolf.wtf", ".forgecdn.net").Replace("://edge.", "://mediafilez.").Replace("://media.", "://mediafilez."),
				Url.Replace("://edge.", "://mediafilez.").Replace("://media.", "://mediafilez."),
				Url.Replace("-service.overwolf.wtf", ".forgecdn.net"),
				Url.Replace("://media.", "://edge."),
				Url
			}.Distinct().ToList();
		}
		public override string ToString()
		{
			return $"{Id}: {FileName}";
		}
	}
}
