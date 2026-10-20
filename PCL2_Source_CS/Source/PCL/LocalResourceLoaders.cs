// PCL

namespace PCL
{
	[StandardModule]
	public static class LocalResourceLoaders
	{
		[CompilerGenerated]
		internal sealed class _Closure___2_0
		{
			public LocalResourceFile _VB_Local_ModEntry;
			public _Closure___2_0(_Closure___2_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_ModEntry = arg0._VB_Local_ModEntry;
				}
			}
			[SpecialName]
			internal bool _Lambda___4(LocalResourceFile m)
			{
				return Operators.CompareString(m.EnabledName, _VB_Local_ModEntry.EnabledName, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_0
		{
			public List<LocalResourceFile> _VB_Local_Mods;
			public ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, ModMinecraft.McVersion Target), int> _VB_Local_Loader;
			public Thread _VB_Local_CurrentTaskThread;
			public string _VB_Local_VanillaVersion;
			public bool _VB_Local_IsFailed;
			public int _VB_Local_EndedThreadCount;
			public _Closure___4_0(_Closure___4_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Mods = arg0._VB_Local_Mods;
					_VB_Local_Loader = arg0._VB_Local_Loader;
					_VB_Local_CurrentTaskThread = arg0._VB_Local_CurrentTaskThread;
					_VB_Local_VanillaVersion = arg0._VB_Local_VanillaVersion;
					_VB_Local_IsFailed = arg0._VB_Local_IsFailed;
					_VB_Local_EndedThreadCount = arg0._VB_Local_EndedThreadCount;
				}
			}
			[SpecialName]
			internal void _Lambda___2()
			{
				checked
				{
					try
					{
						_Closure___4_1 arg = default;
						_Closure___4_1 CS_8__locals6 = new _Closure___4_1(arg);
						CS_8__locals6._VB_NonLocal__VB_Closure_2 = this;
						List<string> list = _VB_Local_Mods.Select([SpecialName] (LocalResourceFile m) => m.ModrinthHash).ToList();
						JObject jObject = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files", HttpMethod.Post, string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\"}}", list.Join("\",\"")), "application/json");
						Logger.Info($"从 Modrinth 获取到 {jObject.Count} 个本地 Mod 的对应信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (jObject.Count == 0)
						{
							return;
						}
						Dictionary<string, List<LocalResourceFile>> dictionary = new Dictionary<string, List<LocalResourceFile>>();
						CS_8__locals6._VB_Local_ModrinthLoaders = new Dictionary<LocalResourceFile, List<string>>();
						foreach (LocalResourceFile item in _VB_Local_Mods)
						{
							if (jObject.ContainsKey(item.ModrinthHash) && !((string?)jObject[item.ModrinthHash]["files"][0]["hashes"]["sha1"] != item.ModrinthHash))
							{
								string key = jObject[item.ModrinthHash]["project_id"].ToString();
								if (ResourceProject.Cache.ContainsKey(key) && (object)item.Project == null)
								{
									item.Project = ResourceProject.Cache[key];
								}
								if (!dictionary.ContainsKey(key))
								{
									dictionary[key] = new List<LocalResourceFile>();
								}
								dictionary[key].Add(item);
								ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)jObject[item.ModrinthHash], ResourceTypes.Mod);
								CS_8__locals6._VB_Local_ModrinthLoaders[item] = (from l in jObject[item.ModrinthHash]["loaders"].Select([SpecialName] (JToken l) => l.ToString().Lower()).Distinct()
									orderby l
									select l).ToList();
								if (item.ProjectVersion == null || DateTime.Compare(item.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
								{
									item.ProjectVersion = resourceVersion;
								}
								else
								{
									item.ProjectVersion.Version = resourceVersion.Version;
								}
							}
						}
						if (_VB_Local_Loader.IsCanceledWithThread(_VB_Local_CurrentTaskThread))
						{
							return;
						}
						Logger.Info($"需要从 Modrinth 获取 {dictionary.Count} 个本地 Mod 的工程信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (!dictionary.Any())
						{
							return;
						}
						JArray jArray = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", dictionary.Keys.Join("\",\"")));
						foreach (JToken item2 in jArray)
						{
							ResourceProject resourceProject = new ResourceProject((JObject)item2);
							foreach (LocalResourceFile item3 in dictionary[resourceProject.Id])
							{
								item3.Project = resourceProject;
							}
						}
						Logger.Info("已从 Modrinth 获取本地 Mod 信息，继续获取更新信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						List<LocalResourceFile> source = dictionary.SelectMany([SpecialName] (KeyValuePair<string, List<LocalResourceFile>> l) => l.Value).ToList();
						if (!source.Any())
						{
							return;
						}
						string arg2 = source.Select([SpecialName] (LocalResourceFile m) => string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, CS_8__locals6._VB_Local_ModrinthLoaders[m].Join("\",\""), CS_8__locals6._VB_NonLocal__VB_Closure_2._VB_Local_VanillaVersion)).Join(",");
						JObject jObject2 = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files/update_individual", HttpMethod.Post, $"{{\"algorithm\": \"sha1\", \"hashes\": [{arg2}]}}", "application/json");
						using (List<LocalResourceFile>.Enumerator enumerator4 = _VB_Local_Mods.GetEnumerator())
						{
							_Closure___4_2 obj = default;
							_Closure___4_3 obj2 = default;
							while (enumerator4.MoveNext())
							{
								obj = new _Closure___4_2(obj);
								obj._VB_Local_Entry = enumerator4.Current;
								obj2 = new _Closure___4_3(obj2);
								obj2._VB_NonLocal__VB_Closure_3 = obj;
								if (!jObject2.ContainsKey(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash) || obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion == null)
								{
									continue;
								}
								obj2._VB_Local_UpdateFile = ResourceVersion.FromPlatformJson((JObject)jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash], ResourceTypes.Mod);
								if (!obj2._VB_Local_UpdateFile.DownloadAvailable || !(from l in jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["loaders"].Select([SpecialName] (JToken l) => l.ToString().Lower()).Distinct()
									orderby l
									select l).ToList().SequenceEqual(CS_8__locals6._VB_Local_ModrinthLoaders[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry]))
								{
									continue;
								}
								Logger.Trace(obj2._Lambda___10, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								if (DateTime.Compare(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion.ReleaseDate, obj2._VB_Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion.Hash, obj2._VB_Local_UpdateFile.Hash, TextCompare: false) != 0)
								{
									if (obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile != null && Operators.CompareString(obj2._VB_Local_UpdateFile.Hash, obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
									{
										obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ChangelogUrls.Add(string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["project_id"], _VB_Local_VanillaVersion));
										obj2._VB_Local_UpdateFile.DownloadUrls.AddRange(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.DownloadUrls);
										obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile = obj2._VB_Local_UpdateFile;
									}
									else if (obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile == null || DateTime.Compare(obj2._VB_Local_UpdateFile.ReleaseDate, obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.ReleaseDate) >= 0)
									{
										obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ChangelogUrls = new List<string> { string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["project_id"], _VB_Local_VanillaVersion) };
										obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile = obj2._VB_Local_UpdateFile;
									}
								}
							}
						}
						Logger.Info("从 Modrinth 获取本地 Mod 信息结束", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "从 Modrinth 获取本地 Mod 信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						_VB_Local_IsFailed = true;
						ProjectData.ClearProjectError();
					}
					finally
					{
						_VB_Local_EndedThreadCount++;
					}
				}
			}
			[SpecialName]
			internal void _Lambda___11()
			{
				checked
				{
					try
					{
						_Closure___4_4 obj = new _Closure___4_4(default(_Closure___4_4));
						obj._VB_NonLocal__VB_Closure_4 = this;
						List<uint> list = new List<uint>();
						foreach (LocalResourceFile item in _VB_Local_Mods)
						{
							list.Add(item.CurseForgeHash);
							if (_VB_Local_Loader.IsCanceledWithThread(_VB_Local_CurrentTaskThread))
							{
								return;
							}
						}
						JContainer jContainer = (JContainer)ModDownload.DlModRequest("https://api.curseforge.com/v1/fingerprints/432", HttpMethod.Post, string.Format("{{\"fingerprints\": [{0}]}}", list.Join(",")), "application/json")["data"]["exactMatches"];
						Logger.Info($"从 CurseForge 获取到 {jContainer.Count} 个本地 Mod 的对应信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (!jContainer.Any())
						{
							return;
						}
						Dictionary<int, List<LocalResourceFile>> dictionary = new Dictionary<int, List<LocalResourceFile>>();
						obj._VB_Local_CurseForgeLoaders = new Dictionary<LocalResourceFile, ModLoaders>();
						foreach (JToken item2 in (IEnumerable<JToken>)jContainer)
						{
							string text = item2["id"].ToString();
							uint num = (uint)item2["file"]["fileFingerprint"];
							foreach (LocalResourceFile item3 in _VB_Local_Mods)
							{
								if (item3.CurseForgeHash == num)
								{
									if (ResourceProject.Cache.ContainsKey(text) && (object)item3.Project == null)
									{
										item3.Project = ResourceProject.Cache[text];
									}
									dictionary.AddIntoValueCollection(Conversions.ToInteger(text), item3);
									ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item2["file"], ResourceTypes.Mod);
									obj._VB_Local_CurseForgeLoaders[item3] = resourceVersion.ModLoaders;
									if (item3.ProjectVersion == null || DateTime.Compare(item3.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
									{
										item3.ProjectVersion = resourceVersion;
									}
								}
							}
						}
						if (_VB_Local_Loader.IsCanceledWithThread(_VB_Local_CurrentTaskThread))
						{
							return;
						}
						Logger.Info($"需要从 CurseForge 获取 {dictionary.Count} 个本地 Mod 的工程信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (!dictionary.Any())
						{
							return;
						}
						JToken jToken = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", HttpMethod.Post, string.Format("{{\"modIds\": [{0}]}}", dictionary.Keys.Join(",")), "application/json")["data"];
						Dictionary<int, List<LocalResourceFile>> dictionary2 = new Dictionary<int, List<LocalResourceFile>>();
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						_Closure___4_5 obj2 = default;
						foreach (JToken item4 in (IEnumerable<JToken>)jToken)
						{
							if (item4["isAvailable"] != null && !item4["isAvailable"].ToObject<bool>())
							{
								continue;
							}
							ResourceProject resourceProject = new ResourceProject((JObject)item4);
							using List<LocalResourceFile>.Enumerator enumerator5 = dictionary[Conversions.ToInteger(resourceProject.Id)].GetEnumerator();
							while (enumerator5.MoveNext())
							{
								obj2 = new _Closure___4_5(obj2);
								obj2._VB_NonLocal__VB_Closure_5 = obj;
								obj2._VB_Local_LocalFile = enumerator5.Current;
								ResourceProject project = obj2._VB_Local_LocalFile.Project;
								if ((object)project != null && project.Platform == ResourcePlatforms.Modrinth)
								{
									obj2._VB_Local_LocalFile.Project = obj2._VB_Local_LocalFile.Project;
								}
								else
								{
									obj2._VB_Local_LocalFile.Project = resourceProject;
								}
								List<JToken> list2 = item4["latestFilesIndexes"].Where(obj2._Lambda___12).MaxByAll([SpecialName] (JToken Entry) => Entry["gameVersion"].ToString(), new ModMinecraft.VersionComparer());
								foreach (JToken item5 in list2)
								{
									int key = item5["fileId"].ToObject<int>();
									if (!dictionary2.ContainsKey(key))
									{
										dictionary2[key] = new List<LocalResourceFile>();
									}
									dictionary2[key].Add(obj2._VB_Local_LocalFile);
									dictionary3[key] = resourceProject.Slug;
								}
							}
						}
						Logger.Info($"已从 CurseForge 获取本地 Mod 信息，需要获取 {dictionary2.Count} 个用于检查更新的文件信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (!dictionary2.Any())
						{
							return;
						}
						JToken jToken2 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, string.Format("{{\"fileIds\": [{0}]}}", dictionary2.Keys.Join(",")), "application/json")["data"];
						Dictionary<LocalResourceFile, ResourceVersion> dictionary4 = new Dictionary<LocalResourceFile, ResourceVersion>();
						foreach (JObject item6 in (IEnumerable<JToken>)jToken2)
						{
							ResourceVersion resourceVersion2 = ResourceVersion.FromPlatformJson(item6, ResourceTypes.Mod);
							if (!resourceVersion2.DownloadAvailable)
							{
								continue;
							}
							foreach (LocalResourceFile item7 in dictionary2[Conversions.ToInteger(resourceVersion2.Id)])
							{
								if (resourceVersion2.ModLoaders != obj._VB_Local_CurseForgeLoaders[item7])
								{
									Logger.Warn($"从 CurseForge 获取到的更新文件 {resourceVersion2.FileName} 加载器为 {resourceVersion2.ModLoaders}，与本地文件 {item7.File.Name} 的加载器 {obj._VB_Local_CurseForgeLoaders[item7]} 不一致，已忽略", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								}
								else if (!dictionary4.ContainsKey(item7) || DateTime.Compare(dictionary4[item7].ReleaseDate, resourceVersion2.ReleaseDate) < 0)
								{
									dictionary4[item7] = resourceVersion2;
								}
							}
						}
						_Closure___4_6 obj3 = default;
						foreach (KeyValuePair<LocalResourceFile, ResourceVersion> item8 in dictionary4)
						{
							obj3 = new _Closure___4_6(obj3);
							obj3._VB_Local_Entry = item8.Key;
							obj3._VB_Local_UpdateFile = item8.Value;
							Logger.Trace(obj3._Lambda___14, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
							if (DateTime.Compare(obj3._VB_Local_Entry.ProjectVersion.ReleaseDate, obj3._VB_Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(obj3._VB_Local_Entry.ProjectVersion.Hash, obj3._VB_Local_UpdateFile.Hash, TextCompare: false) != 0)
							{
								if (obj3._VB_Local_Entry.UpdateFile != null && Operators.CompareString(obj3._VB_Local_UpdateFile.Hash, obj3._VB_Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
								{
									obj3._VB_Local_Entry.ChangelogUrls.Add($"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(obj3._VB_Local_UpdateFile.Id)]}/files/{obj3._VB_Local_UpdateFile.Id}");
									obj3._VB_Local_Entry.UpdateFile.DownloadUrls.AddRange(obj3._VB_Local_UpdateFile.DownloadUrls);
								}
								else if (obj3._VB_Local_Entry.UpdateFile == null || DateTime.Compare(obj3._VB_Local_UpdateFile.ReleaseDate, obj3._VB_Local_Entry.UpdateFile.ReleaseDate) > 0)
								{
									obj3._VB_Local_Entry.ChangelogUrls = new List<string> { $"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(obj3._VB_Local_UpdateFile.Id)]}/files/{obj3._VB_Local_UpdateFile.Id}" };
									obj3._VB_Local_Entry.UpdateFile = obj3._VB_Local_UpdateFile;
								}
							}
						}
						Logger.Info("从 CurseForge 获取 Mod 更新信息结束", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "从 CurseForge 获取本地 Mod 信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						_VB_Local_IsFailed = true;
						ProjectData.ClearProjectError();
					}
					finally
					{
						_VB_Local_EndedThreadCount++;
					}
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_1
		{
			public Dictionary<LocalResourceFile, List<string>> _VB_Local_ModrinthLoaders;
			public _Closure___4_0 _VB_NonLocal__VB_Closure_2;
			public _Closure___4_1(_Closure___4_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_ModrinthLoaders = arg0._VB_Local_ModrinthLoaders;
				}
			}
			[SpecialName]
			internal string _Lambda___7(LocalResourceFile m)
			{
				return string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, _VB_Local_ModrinthLoaders[m].Join("\",\""), _VB_NonLocal__VB_Closure_2._VB_Local_VanillaVersion);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_2
		{
			public LocalResourceFile _VB_Local_Entry;
			public _Closure___4_2(_Closure___4_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Entry = arg0._VB_Local_Entry;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_3
		{
			public ResourceVersion _VB_Local_UpdateFile;
			public _Closure___4_2 _VB_NonLocal__VB_Closure_3;
			public _Closure___4_3(_Closure___4_3 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_UpdateFile = arg0._VB_Local_UpdateFile;
				}
			}
			[SpecialName]
			internal string _Lambda___10()
			{
				return $"本地文件 {_VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion.FileName} 在 Modrinth 上的最新版为 {_VB_Local_UpdateFile.FileName}";
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_4
		{
			public Dictionary<LocalResourceFile, ModLoaders> _VB_Local_CurseForgeLoaders;
			public _Closure___4_0 _VB_NonLocal__VB_Closure_4;
			public _Closure___4_4(_Closure___4_4 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_CurseForgeLoaders = arg0._VB_Local_CurseForgeLoaders;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_5
		{
			public LocalResourceFile _VB_Local_LocalFile;
			public _Closure___4_4 _VB_NonLocal__VB_Closure_5;
			public _Closure___4_5(_Closure___4_5 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_LocalFile = arg0._VB_Local_LocalFile;
				}
			}
			[SpecialName]
			internal bool _Lambda___12(JToken Entry)
			{
				ModLoaders modLoaders = ((Entry["modLoader"] != null) ? Resource.FromCurseForgeModLoaderType(Entry["modLoader"].ToObject<int>()) : ModLoaders.None);
				if (_VB_NonLocal__VB_Closure_5._VB_Local_CurseForgeLoaders[_VB_Local_LocalFile] == ModLoaders.None)
				{
					if (modLoaders != ModLoaders.None)
					{
						return false;
					}
				}
				else if (!_VB_NonLocal__VB_Closure_5._VB_Local_CurseForgeLoaders[_VB_Local_LocalFile].HasFlagF(modLoaders))
				{
					return false;
				}
				string text = (string?)Entry["gameVersion"];
				return text != null && Operators.CompareString(text, _VB_NonLocal__VB_Closure_5._VB_NonLocal__VB_Closure_4._VB_Local_VanillaVersion, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___4_6
		{
			public LocalResourceFile _VB_Local_Entry;
			public ResourceVersion _VB_Local_UpdateFile;
			public _Closure___4_6(_Closure___4_6 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Entry = arg0._VB_Local_Entry;
					_VB_Local_UpdateFile = arg0._VB_Local_UpdateFile;
				}
			}
			[SpecialName]
			internal string _Lambda___14()
			{
				return $"本地文件 {_VB_Local_Entry.ProjectVersion.FileName} 在 CurseForge 上的最新版为 {_VB_Local_UpdateFile.FileName}";
			}
		}
		public static ModLoader.LoaderTask<string, List<LocalResourceFile>> LocalResourceLoader = new ModLoader.LoaderTask<string, List<LocalResourceFile>>("LocalResourceLoader", LocalResourceFileLoad);
		public static ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, ModMinecraft.McVersion Target), int> LocalResourceOnlineLoader = new ModLoader.LoaderTask<(List<LocalResourceFile>, JObject, ModMinecraft.McVersion), int>("LocalResourceOnlineLoader", LocalResourceOnlineLoad);
		[SpecialName]
		private static Func<string, bool> _STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init;
		private static void LocalResourceFileLoad(ModLoader.LoaderTask<string, List<LocalResourceFile>> Loader)
		{
			try
			{
				ModBase.RunInUiWait([SpecialName] () =>
				{
					if (ModMain.FrmInstanceMod != null)
					{
						ModMain.FrmInstanceMod.Load.ShowProgress = false;
					}
				});
				if (PageInstanceMod.UpdatingInstanceModFolders.Contains(Loader.Input))
				{
					Logger.Info($"等待 Mod 更新完成后才能继续加载 Mod 列表：{Loader.Input}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					try
					{
						ModBase.RunInUiWait([SpecialName] () =>
						{
							if (ModMain.FrmInstanceMod != null)
							{
								ModMain.FrmInstanceMod.Load.Text = "正在更新 Mod";
							}
						});
						while (PageInstanceMod.UpdatingInstanceModFolders.Contains(Loader.Input))
						{
							if (Loader.IsCanceled)
							{
								return;
							}
							Thread.Sleep(100);
						}
					}
					finally
					{
						ModBase.RunInUiWait([SpecialName] () =>
						{
							if (ModMain.FrmInstanceMod != null)
							{
								ModMain.FrmInstanceMod.Load.Text = "正在加载 Mod 列表";
							}
						});
					}
					ModMain.FrmInstanceMod.LoaderRun(ModLoader.LoaderFolderRunType.UpdateOnly);
				}
				List<string> list = new List<string>();
				if (DirectoryUtils.Exists(Loader.Input))
				{
					string right = Loader.Input.Lower();
					foreach (string item in DirectoryUtils.EnumerateFiles(Loader.Input, includeSubDirectories: true))
					{
						if (Operators.CompareString(item.BeforeLast("\\").Lower() + "\\", right, TextCompare: false) != 0 && ((object)PageInstanceLeft.Instance == null || !PageInstanceLeft.Instance.Version.HasForge || PageInstanceLeft.Instance.Version.Vanilla.Major >= 13 || Operators.CompareString(PathUtils.GetLastPart(PathUtils.RemoveLastPart(item)), $"1.{PageInstanceLeft.Instance.Version.Vanilla.Major}.{PageInstanceLeft.Instance.Version.Vanilla.Build}", TextCompare: false) != 0))
						{
							continue;
						}
						if (_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init == null)
						{
							Interlocked.CompareExchange(ref _STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init, new StaticLocalInitFlag(), null);
						}
						bool lockTaken = false;
						try
						{
							Monitor.Enter(_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init, ref lockTaken);
							if (_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init.State == 0)
							{
								_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init.State = 2;
								_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile = [SpecialName] (string FullName) =>
								{
									if (FullName == null || !FullName.Contains("."))
									{
										return false;
									}
									FullName = FullName.Lower();
									return (FullName.EndsWithF(".jar", ignoreCase: true) || FullName.EndsWithF(".zip", ignoreCase: true) || FullName.EndsWithF(".litemod", ignoreCase: true) || FullName.EndsWithF(".jar.disabled", ignoreCase: true) || FullName.EndsWithF(".zip.disabled", ignoreCase: true) || FullName.EndsWithF(".litemod.disabled", ignoreCase: true) || FullName.EndsWithF(".jar.old", ignoreCase: true) || FullName.EndsWithF(".zip.old", ignoreCase: true) || FullName.EndsWithF(".litemod.old", ignoreCase: true)) ? true : false;
								};
							}
							else if (_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init.State == 2)
							{
								throw new IncompleteInitialization();
							}
						}
						finally
						{
							_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init.State = 1;
							if (lockTaken)
							{
								Monitor.Exit(_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile_Init);
							}
						}
						if (_STATIC_LocalResourceFileLoad_011151286AC2E1512815D11280C8_IsModFile(item))
						{
							list.Add(item);
						}
					}
				}
				string filePath = ModBase.PathTemp + "Cache\\LocalMod.json";
				JObject jObject = new JObject();
				try
				{
					string text = FileUtils.TryReadAsString(filePath);
					if (!string.IsNullOrWhiteSpace(text))
					{
						jObject = (JObject)text.DeserializeJson();
						if (!jObject.ContainsKey("version") || jObject["version"].ToObject<int>() != 19)
						{
							Logger.Warn("本地 Mod 信息缓存版本已过期，将弃用这些缓存信息", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
							jObject = new JObject();
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "读取本地 Mod 信息缓存失败，已重置", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					jObject = new JObject();
					ProjectData.ClearProjectError();
				}
				jObject["version"] = 19;
				List<LocalResourceFile> list2 = new List<LocalResourceFile>();
				_Closure___2_0 obj = default;
				foreach (string item2 in list)
				{
					obj = new _Closure___2_0(obj);
					if (Loader.IsCanceled)
					{
						return;
					}
					obj._VB_Local_ModEntry = new LocalResourceFile(item2);
					LocalResourceFile localResourceFile = list2.FirstOrDefault(obj._Lambda___4);
					if (localResourceFile != null)
					{
						LocalResourceFile localResourceFile2 = (localResourceFile.IsEnabled ? obj._VB_Local_ModEntry : localResourceFile);
						Logger.Warn($"重复的 Mod 文件：{localResourceFile.File.Name} 与 {obj._VB_Local_ModEntry.File.Name}，已忽略 {localResourceFile2.File.Name}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (localResourceFile2 == obj._VB_Local_ModEntry)
						{
							continue;
						}
						list2.Remove(localResourceFile2);
					}
					list2.Add(obj._VB_Local_ModEntry);
				}
				Logger.Info($"共发现 {list2.Count} 个 Mod", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				list2 = list2.OrderBy([SpecialName] (LocalResourceFile m) => m.File.Name).ToList();
				if (!Loader.IsCanceled)
				{
					Loader.Output = list2;
					LocalResourceOnlineLoader.Start((list2, jObject, PageInstanceLeft.Instance.Version), IsForceRestart: true);
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "Mod 列表加载失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				throw;
			}
		}
		private static void LocalResourceOnlineLoad(ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, ModMinecraft.McVersion Target), int> Loader)
		{
			_Closure___4_0 arg = default;
			_Closure___4_0 CS_8__locals52 = new _Closure___4_0(arg);
			CS_8__locals52._VB_Local_Loader = Loader;
			CS_8__locals52._VB_Local_Mods = new List<LocalResourceFile>();
			JObject item = CS_8__locals52._VB_Local_Loader.Input.Cache;
			ModLoaders modLoaders = CS_8__locals52._VB_Local_Loader.Input.Target.ModLoaders;
			foreach (LocalResourceFile item3 in CS_8__locals52._VB_Local_Loader.Input.ModList)
			{
				if (CS_8__locals52._VB_Local_Loader.IsCanceled)
				{
					return;
				}
				string propertyName = item3.ModrinthHash + CS_8__locals52._VB_Local_Loader.Input.Target.VanillaName + Conversions.ToString((int)modLoaders);
				if (item.ContainsKey(propertyName))
				{
					item3.FromJson((JObject)item[propertyName]);
					if (item3.OnlineDataLoaded && DateTime.Now - item[propertyName]["Project"]["CacheTime"].ToObject<DateTime>() < new TimeSpan(6, 0, 0))
					{
						continue;
					}
				}
				CS_8__locals52._VB_Local_Mods.Add(item3);
			}
			Logger.Info($"有 {CS_8__locals52._VB_Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project == null).Count()} 个 Mod 需要联网获取信息，{CS_8__locals52._VB_Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project != null).Count()} 个 Mod 需要更新信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			if (!CS_8__locals52._VB_Local_Mods.Any())
			{
				return;
			}
			ModMinecraft.McVersion item2 = CS_8__locals52._VB_Local_Loader.Input.Target;
			CS_8__locals52._VB_Local_VanillaVersion = item2.VanillaName;
			Logger.Info($"正在检查 Mod 信息的版本：{CS_8__locals52._VB_Local_VanillaVersion}，加载器 {modLoaders}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			CS_8__locals52._VB_Local_EndedThreadCount = 0;
			CS_8__locals52._VB_Local_IsFailed = false;
			CS_8__locals52._VB_Local_CurrentTaskThread = Thread.CurrentThread;
			checked
			{
				ModBase.RunInNewThread([SpecialName] () =>
				{
					try
					{
						_Closure___4_1 arg2 = default;
						_Closure___4_1 CS_8__locals54 = new _Closure___4_1(arg2);
						CS_8__locals54._VB_NonLocal__VB_Closure_2 = CS_8__locals52;
						List<string> list = CS_8__locals52._VB_Local_Mods.Select([SpecialName] (LocalResourceFile m) => m.ModrinthHash).ToList();
						JObject jObject = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files", HttpMethod.Post, string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\"}}", list.Join("\",\"")), "application/json");
						Logger.Info($"从 Modrinth 获取到 {jObject.Count} 个本地 Mod 的对应信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (jObject.Count != 0)
						{
							Dictionary<string, List<LocalResourceFile>> dictionary = new Dictionary<string, List<LocalResourceFile>>();
							CS_8__locals54._VB_Local_ModrinthLoaders = new Dictionary<LocalResourceFile, List<string>>();
							foreach (LocalResourceFile item4 in CS_8__locals52._VB_Local_Mods)
							{
								if (jObject.ContainsKey(item4.ModrinthHash) && !((string?)jObject[item4.ModrinthHash]["files"][0]["hashes"]["sha1"] != item4.ModrinthHash))
								{
									string key = jObject[item4.ModrinthHash]["project_id"].ToString();
									if (ResourceProject.Cache.ContainsKey(key) && (object)item4.Project == null)
									{
										item4.Project = ResourceProject.Cache[key];
									}
									if (!dictionary.ContainsKey(key))
									{
										dictionary[key] = new List<LocalResourceFile>();
									}
									dictionary[key].Add(item4);
									ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)jObject[item4.ModrinthHash], ResourceTypes.Mod);
									CS_8__locals54._VB_Local_ModrinthLoaders[item4] = (from l in jObject[item4.ModrinthHash]["loaders"].Select([SpecialName] (JToken l) => l.ToString().Lower()).Distinct()
										orderby l
										select l).ToList();
									if (item4.ProjectVersion == null || DateTime.Compare(item4.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
									{
										item4.ProjectVersion = resourceVersion;
									}
									else
									{
										item4.ProjectVersion.Version = resourceVersion.Version;
									}
								}
							}
							if (!CS_8__locals52._VB_Local_Loader.IsCanceledWithThread(CS_8__locals52._VB_Local_CurrentTaskThread))
							{
								Logger.Info($"需要从 Modrinth 获取 {dictionary.Count} 个本地 Mod 的工程信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								if (dictionary.Any())
								{
									JArray jArray = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", dictionary.Keys.Join("\",\"")));
									foreach (JToken item5 in jArray)
									{
										ResourceProject resourceProject = new ResourceProject((JObject)item5);
										foreach (LocalResourceFile item6 in dictionary[resourceProject.Id])
										{
											item6.Project = resourceProject;
										}
									}
									Logger.Info("已从 Modrinth 获取本地 Mod 信息，继续获取更新信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
									List<LocalResourceFile> source = dictionary.SelectMany([SpecialName] (KeyValuePair<string, List<LocalResourceFile>> l) => l.Value).ToList();
									if (source.Any())
									{
										string arg3 = source.Select([SpecialName] (LocalResourceFile m) => string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, CS_8__locals54._VB_Local_ModrinthLoaders[m].Join("\",\""), CS_8__locals54._VB_NonLocal__VB_Closure_2._VB_Local_VanillaVersion)).Join(",");
										JObject jObject2 = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files/update_individual", HttpMethod.Post, $"{{\"algorithm\": \"sha1\", \"hashes\": [{arg3}]}}", "application/json");
										using (List<LocalResourceFile>.Enumerator enumerator6 = CS_8__locals52._VB_Local_Mods.GetEnumerator())
										{
											_Closure___4_2 obj = default;
											_Closure___4_3 obj2 = default;
											while (enumerator6.MoveNext())
											{
												obj = new _Closure___4_2(obj);
												obj._VB_Local_Entry = enumerator6.Current;
												obj2 = new _Closure___4_3(obj2);
												obj2._VB_NonLocal__VB_Closure_3 = obj;
												if (jObject2.ContainsKey(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash) && obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion != null)
												{
													obj2._VB_Local_UpdateFile = ResourceVersion.FromPlatformJson((JObject)jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash], ResourceTypes.Mod);
													if (obj2._VB_Local_UpdateFile.DownloadAvailable && (from l in jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["loaders"].Select([SpecialName] (JToken l) => l.ToString().Lower()).Distinct()
														orderby l
														select l).ToList().SequenceEqual(CS_8__locals54._VB_Local_ModrinthLoaders[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry]))
													{
														Logger.Trace(obj2._Lambda___10, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
														if (DateTime.Compare(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion.ReleaseDate, obj2._VB_Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ProjectVersion.Hash, obj2._VB_Local_UpdateFile.Hash, TextCompare: false) != 0)
														{
															if (obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile != null && Operators.CompareString(obj2._VB_Local_UpdateFile.Hash, obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
															{
																obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ChangelogUrls.Add(string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["project_id"], CS_8__locals52._VB_Local_VanillaVersion));
																obj2._VB_Local_UpdateFile.DownloadUrls.AddRange(obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.DownloadUrls);
																obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile = obj2._VB_Local_UpdateFile;
															}
															else if (obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile == null || DateTime.Compare(obj2._VB_Local_UpdateFile.ReleaseDate, obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile.ReleaseDate) >= 0)
															{
																obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ChangelogUrls = new List<string> { string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", jObject2[obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.ModrinthHash]["project_id"], CS_8__locals52._VB_Local_VanillaVersion) };
																obj2._VB_NonLocal__VB_Closure_3._VB_Local_Entry.UpdateFile = obj2._VB_Local_UpdateFile;
															}
														}
													}
												}
											}
										}
										Logger.Info("从 Modrinth 获取本地 Mod 信息结束", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "从 Modrinth 获取本地 Mod 信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						CS_8__locals52._VB_Local_IsFailed = true;
						ProjectData.ClearProjectError();
					}
					finally
					{
						CS_8__locals52._VB_Local_EndedThreadCount++;
					}
				}, "Mod List Detail Loader Modrinth");
				ModBase.RunInNewThread([SpecialName] () =>
				{
					try
					{
						_Closure___4_4 obj = new _Closure___4_4(default(_Closure___4_4));
						obj._VB_NonLocal__VB_Closure_4 = CS_8__locals52;
						List<uint> list = new List<uint>();
						foreach (LocalResourceFile item7 in CS_8__locals52._VB_Local_Mods)
						{
							list.Add(item7.CurseForgeHash);
							if (CS_8__locals52._VB_Local_Loader.IsCanceledWithThread(CS_8__locals52._VB_Local_CurrentTaskThread))
							{
								return;
							}
						}
						JContainer jContainer = (JContainer)ModDownload.DlModRequest("https://api.curseforge.com/v1/fingerprints/432", HttpMethod.Post, string.Format("{{\"fingerprints\": [{0}]}}", list.Join(",")), "application/json")["data"]["exactMatches"];
						Logger.Info($"从 CurseForge 获取到 {jContainer.Count} 个本地 Mod 的对应信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						if (jContainer.Any())
						{
							Dictionary<int, List<LocalResourceFile>> dictionary = new Dictionary<int, List<LocalResourceFile>>();
							obj._VB_Local_CurseForgeLoaders = new Dictionary<LocalResourceFile, ModLoaders>();
							foreach (JToken item8 in (IEnumerable<JToken>)jContainer)
							{
								string text = item8["id"].ToString();
								uint num = (uint)item8["file"]["fileFingerprint"];
								foreach (LocalResourceFile item9 in CS_8__locals52._VB_Local_Mods)
								{
									if (item9.CurseForgeHash == num)
									{
										if (ResourceProject.Cache.ContainsKey(text) && (object)item9.Project == null)
										{
											item9.Project = ResourceProject.Cache[text];
										}
										dictionary.AddIntoValueCollection(Conversions.ToInteger(text), item9);
										ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item8["file"], ResourceTypes.Mod);
										obj._VB_Local_CurseForgeLoaders[item9] = resourceVersion.ModLoaders;
										if (item9.ProjectVersion == null || DateTime.Compare(item9.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
										{
											item9.ProjectVersion = resourceVersion;
										}
									}
								}
							}
							if (!CS_8__locals52._VB_Local_Loader.IsCanceledWithThread(CS_8__locals52._VB_Local_CurrentTaskThread))
							{
								Logger.Info($"需要从 CurseForge 获取 {dictionary.Count} 个本地 Mod 的工程信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								if (dictionary.Any())
								{
									JToken jToken = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", HttpMethod.Post, string.Format("{{\"modIds\": [{0}]}}", dictionary.Keys.Join(",")), "application/json")["data"];
									Dictionary<int, List<LocalResourceFile>> dictionary2 = new Dictionary<int, List<LocalResourceFile>>();
									Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
									_Closure___4_5 obj2 = default;
									foreach (JToken item10 in (IEnumerable<JToken>)jToken)
									{
										if (item10["isAvailable"] == null || item10["isAvailable"].ToObject<bool>())
										{
											ResourceProject resourceProject = new ResourceProject((JObject)item10);
											using List<LocalResourceFile>.Enumerator enumerator7 = dictionary[Conversions.ToInteger(resourceProject.Id)].GetEnumerator();
											while (enumerator7.MoveNext())
											{
												obj2 = new _Closure___4_5(obj2);
												obj2._VB_NonLocal__VB_Closure_5 = obj;
												obj2._VB_Local_LocalFile = enumerator7.Current;
												ResourceProject project = obj2._VB_Local_LocalFile.Project;
												if ((object)project != null && project.Platform == ResourcePlatforms.Modrinth)
												{
													obj2._VB_Local_LocalFile.Project = obj2._VB_Local_LocalFile.Project;
												}
												else
												{
													obj2._VB_Local_LocalFile.Project = resourceProject;
												}
												List<JToken> list2 = item10["latestFilesIndexes"].Where(obj2._Lambda___12).MaxByAll([SpecialName] (JToken Entry) => Entry["gameVersion"].ToString(), new ModMinecraft.VersionComparer());
												foreach (JToken item11 in list2)
												{
													int key = item11["fileId"].ToObject<int>();
													if (!dictionary2.ContainsKey(key))
													{
														dictionary2[key] = new List<LocalResourceFile>();
													}
													dictionary2[key].Add(obj2._VB_Local_LocalFile);
													dictionary3[key] = resourceProject.Slug;
												}
											}
										}
									}
									Logger.Info($"已从 CurseForge 获取本地 Mod 信息，需要获取 {dictionary2.Count} 个用于检查更新的文件信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
									if (dictionary2.Any())
									{
										JToken jToken2 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, string.Format("{{\"fileIds\": [{0}]}}", dictionary2.Keys.Join(",")), "application/json")["data"];
										Dictionary<LocalResourceFile, ResourceVersion> dictionary4 = new Dictionary<LocalResourceFile, ResourceVersion>();
										foreach (JObject item12 in (IEnumerable<JToken>)jToken2)
										{
											ResourceVersion resourceVersion2 = ResourceVersion.FromPlatformJson(item12, ResourceTypes.Mod);
											if (resourceVersion2.DownloadAvailable)
											{
												foreach (LocalResourceFile item13 in dictionary2[Conversions.ToInteger(resourceVersion2.Id)])
												{
													if (resourceVersion2.ModLoaders != obj._VB_Local_CurseForgeLoaders[item13])
													{
														Logger.Warn($"从 CurseForge 获取到的更新文件 {resourceVersion2.FileName} 加载器为 {resourceVersion2.ModLoaders}，与本地文件 {item13.File.Name} 的加载器 {obj._VB_Local_CurseForgeLoaders[item13]} 不一致，已忽略", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
													}
													else if (!dictionary4.ContainsKey(item13) || DateTime.Compare(dictionary4[item13].ReleaseDate, resourceVersion2.ReleaseDate) < 0)
													{
														dictionary4[item13] = resourceVersion2;
													}
												}
											}
										}
										_Closure___4_6 obj3 = default;
										foreach (KeyValuePair<LocalResourceFile, ResourceVersion> item14 in dictionary4)
										{
											obj3 = new _Closure___4_6(obj3);
											obj3._VB_Local_Entry = item14.Key;
											obj3._VB_Local_UpdateFile = item14.Value;
											Logger.Trace(obj3._Lambda___14, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
											if (DateTime.Compare(obj3._VB_Local_Entry.ProjectVersion.ReleaseDate, obj3._VB_Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(obj3._VB_Local_Entry.ProjectVersion.Hash, obj3._VB_Local_UpdateFile.Hash, TextCompare: false) != 0)
											{
												if (obj3._VB_Local_Entry.UpdateFile != null && Operators.CompareString(obj3._VB_Local_UpdateFile.Hash, obj3._VB_Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
												{
													obj3._VB_Local_Entry.ChangelogUrls.Add($"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(obj3._VB_Local_UpdateFile.Id)]}/files/{obj3._VB_Local_UpdateFile.Id}");
													obj3._VB_Local_Entry.UpdateFile.DownloadUrls.AddRange(obj3._VB_Local_UpdateFile.DownloadUrls);
												}
												else if (obj3._VB_Local_Entry.UpdateFile == null || DateTime.Compare(obj3._VB_Local_UpdateFile.ReleaseDate, obj3._VB_Local_Entry.UpdateFile.ReleaseDate) > 0)
												{
													obj3._VB_Local_Entry.ChangelogUrls = new List<string> { $"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(obj3._VB_Local_UpdateFile.Id)]}/files/{obj3._VB_Local_UpdateFile.Id}" };
													obj3._VB_Local_Entry.UpdateFile = obj3._VB_Local_UpdateFile;
												}
											}
										}
										Logger.Info("从 CurseForge 获取 Mod 更新信息结束", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "从 CurseForge 获取本地 Mod 信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						CS_8__locals52._VB_Local_IsFailed = true;
						ProjectData.ClearProjectError();
					}
					finally
					{
						CS_8__locals52._VB_Local_EndedThreadCount++;
					}
				}, "Mod List Detail Loader CurseForge");
			}
			do
			{
				if (CS_8__locals52._VB_Local_EndedThreadCount != 2)
				{
					Thread.Sleep(10);
					continue;
				}
				CS_8__locals52._VB_Local_Mods = CS_8__locals52._VB_Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project != null).ToList();
				Logger.Info($"联网获取本地 Mod 信息完成，为 {CS_8__locals52._VB_Local_Mods.Count} 个 Mod 更新缓存", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				if (!CS_8__locals52._VB_Local_Mods.Any() || CS_8__locals52._VB_Local_Loader.IsCanceled)
				{
					break;
				}
				foreach (LocalResourceFile item15 in CS_8__locals52._VB_Local_Mods)
				{
					item15.OnlineDataLoaded = !CS_8__locals52._VB_Local_IsFailed;
					item[item15.ModrinthHash + CS_8__locals52._VB_Local_VanillaVersion + Conversions.ToString((int)modLoaders)] = item15.ToJson();
				}
				FileUtils.Write(ModBase.PathTemp + "Cache\\LocalMod.json", item.ToString(ModBase.ModeDebug ? Formatting.Indented : Formatting.None));
				if (CS_8__locals52._VB_Local_Loader.IsCanceled)
				{
					break;
				}
				PageInstanceMod frmInstanceMod = ModMain.FrmInstanceMod;
				if (frmInstanceMod != null && frmInstanceMod.Filter == PageInstanceMod.FilterType.CanUpdate)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmInstanceMod?.RefreshUI();
					});
				}
				else
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmInstanceMod?.RefreshBars();
					});
				}
				break;
			}
			while (!CS_8__locals52._VB_Local_Loader.IsCanceled);
		}
	}
}
