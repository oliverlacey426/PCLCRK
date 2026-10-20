// PCL

namespace PCL
{
	[StandardModule]
	public static class ModMinecraft
	{
		public class McFolder
		{
			public enum Types
			{
				Vanilla,
				RenamedVanilla,
				Custom
			}
			public string Name;
			public string Location;
			public Types Type;
			public override bool Equals(object obj)
			{
				if (!(obj is McFolder))
				{
					return false;
				}
				McFolder mcFolder = (McFolder)obj;
				return Operators.CompareString(Name, mcFolder.Name, TextCompare: false) == 0 && Operators.CompareString(Location, mcFolder.Location, TextCompare: false) == 0 && Type == mcFolder.Type;
			}
			public override string ToString()
			{
				return Location;
			}
		}
		public class McInstance
		{
			private string _Name;
			public string Info;
			public McInstanceState State;
			public string Logo;
			public bool IsStar;
			public McInstanceCardType DisplayType;
			private McVersion _Version;
			public DateTime ReleaseTime;
			private string _JsonText;
			private JObject _JsonObject;
			private JObject _JsonVersion;
			private string _InheritName;
			public JsonConfigProvider Config;
			public bool IsLoaded;
			[SpecialName]
			private object _STATIC_get_Version_2001287A0_Pattern;
			[SpecialName]
			private StaticLocalInitFlag _STATIC_get_Version_2001287A0_Pattern_Init;
			[SpecialName]
			private bool _STATIC_get_JsonVersion_2001281E9_JsonVersionInited;
			[SpecialName]
			private StaticLocalInitFlag _STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init;
			public string PathVersion { get; }
			public string PathIndie
			{
				get
				{
					if (!Settings.HasSaved("VersionArgumentIndieV2", this))
					{
						if (!IsLoaded)
						{
							Load();
						}
						VB_AnonymousDelegate_5<bool> obj = [SpecialName] () =>
						{
							if (Settings.HasSaved("VersionArgumentIndie", this) && Settings.Get<int>("VersionArgumentIndie", this) > 0)
							{
								Logger.Info($"版本隔离初始化（{Name}）：从老的版本独立设置中迁移", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								return Settings.Get<int>("VersionArgumentIndie", this) == 1;
							}
							DirectoryInfo info = DirectoryUtils.GetInfo(PathVersion + "mods\\");
							DirectoryInfo info2 = DirectoryUtils.GetInfo(PathVersion + "saves\\");
							if ((info.Exists && info.EnumerateFiles().Any()) || (info2.Exists && info2.EnumerateDirectories().Any()))
							{
								Logger.Info($"版本隔离初始化（{Name}）：版本文件夹下存在 mods 或 saves 文件夹，自动开启", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								return true;
							}
							bool flag = State != McInstanceState.Fool && State != McInstanceState.Old && State != McInstanceState.Snapshot;
							Logger.Info(string.Format("版本隔离初始化（{0}）：从全局默认设置中（{1}）判断，State {2}，IsRelease {3}，Modable {4}", Name, Settings.Get<int>("LaunchArgumentIndieV2"), State, flag, Modable), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							return Settings.Get<int>("LaunchArgumentIndieV2") switch
							{
								0 => false, 
								1 => Modable, 
								2 => !flag, 
								3 => !flag || Modable, 
								_ => true, 
							};
						};
						Settings.Set("VersionArgumentIndieV2", obj(), this);
					}
					if (!Settings.Get<bool>("VersionArgumentIndieV2", this))
					{
						return McFolderSelected;
					}
					return PathVersion;
				}
			}
			public string Name
			{
				get
				{
					if (_Name == null && Operators.CompareString(PathVersion, "", TextCompare: false) != 0)
					{
						_Name = PathUtils.GetLastPart(PathVersion);
					}
					return _Name;
				}
			}
			public bool Modable
			{
				get
				{
					if (!IsLoaded)
					{
						Load();
					}
					if (!Version.HasFabric && !Version.HasForgelike && !Version.HasLiteLoader)
					{
						return DisplayType == McInstanceCardType.API;
					}
					return true;
				}
			}
			public McVersion Version
			{
				get
				{
					if (_Version != null)
					{
						return _Version;
					}
					_Version = new McVersion();
					try
					{
						try
						{
							if (JsonObject["releaseTime"] == null)
							{
								ReleaseTime = new DateTime(1970, 1, 1, 15, 0, 0);
							}
							else
							{
								ReleaseTime = JsonObject["releaseTime"].ToObject<DateTime>();
							}
							if (ReleaseTime.Year <= 2000 || ReleaseTime.Year >= 2013)
							{
								goto end_IL_001f;
							}
							_Version.VanillaName = "Old";
							goto end_IL_001f_2;
							end_IL_001f:;
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ReleaseTime = new DateTime(1970, 1, 1, 15, 0, 0);
							ProjectData.ClearProjectError();
						}
						string text;
						if ((string?)(JsonObject["type"] ?? ((JToken)"")) == "pending")
						{
							_Version.VanillaName = "pending";
						}
						else if (JsonObject["clientVersion"] != null)
						{
							_Version.VanillaName = (string?)JsonObject["clientVersion"];
						}
						else
						{
							if (JsonObject["patches"] != null)
							{
								foreach (JObject item in (IEnumerable<JToken>)JsonObject["patches"])
								{
									if (Operators.CompareString((item["id"] ?? ((JToken)"")).ToString(), "game", TextCompare: false) != 0 || item["version"] == null)
									{
										continue;
									}
									_Version.VanillaName = item["version"].ToString();
									goto end_IL_001f_2;
								}
							}
							if (JsonObject["arguments"] != null && JsonObject["arguments"]["game"] != null)
							{
								bool flag = false;
								foreach (JToken item2 in (IEnumerable<JToken>)JsonObject["arguments"]["game"])
								{
									if (!flag)
									{
										if (Operators.CompareString(item2.ToString(), "--fml.mcVersion", TextCompare: false) == 0)
										{
											flag = true;
										}
										continue;
									}
									_Version.VanillaName = item2.ToString();
									goto end_IL_001f_2;
								}
							}
							if (Operators.CompareString(InheritName, "", TextCompare: false) != 0)
							{
								_Version.VanillaName = (JsonObject["jar"] ?? ((JToken)"")).ToString();
								if (Operators.CompareString(_Version.VanillaName, "", TextCompare: false) == 0)
								{
									_Version.VanillaName = InheritName;
								}
							}
							else
							{
								text = (JsonObject["downloads"] ?? ((JToken)"")).ToString().RegexSeek("(?<=launcher.mojang.com/mc/game/)[^/]*");
								if (text != null)
								{
									_Version.VanillaName = text;
								}
								else
								{
									string str = JsonObject["libraries"].ToString();
									text = str.RegexSeek("(?<=net.minecraftforge:forge:)[0-9]{1,2}.[0-9+.]+") ?? str.RegexSeek("(?<=net.minecraftforge:fmlloader:)[0-9]{1,2}.[0-9+.]+");
									if (text != null)
									{
										_Version.VanillaName = text;
									}
									else
									{
										text = str.RegexSeek("(?<=optifine:OptiFine:)[0-9]{1,2}.[0-9+.]+");
										if (text != null)
										{
											_Version.VanillaName = text;
										}
										else
										{
											text = str.RegexSeek("(?<=((fabricmc)|(quiltmc)):intermediary:)[^\"]*");
											if (text != null)
											{
												_Version.VanillaName = text;
											}
											else if (JsonObject["jar"] != null)
											{
												_Version.VanillaName = JsonObject["jar"].ToString();
											}
											else
											{
												if (JsonVersion?["name"] == null)
												{
													goto IL_0464;
												}
												string text2 = JsonVersion["name"].ToString();
												if (text2.Length >= 32)
												{
													goto IL_0464;
												}
												_Version.VanillaName = text2;
												Logger.Info($"从版本 jar 中的 version.json 获取到版本号：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
											}
										}
									}
								}
							}
						}
						goto end_IL_001f_2;
						IL_0464:
						if (_STATIC_get_Version_2001287A0_Pattern_Init == null)
						{
							Interlocked.CompareExchange(ref _STATIC_get_Version_2001287A0_Pattern_Init, new StaticLocalInitFlag(), null);
						}
						bool lockTaken = false;
						try
						{
							Monitor.Enter(_STATIC_get_Version_2001287A0_Pattern_Init, ref lockTaken);
							if (_STATIC_get_Version_2001287A0_Pattern_Init.State == 0)
							{
								_STATIC_get_Version_2001287A0_Pattern_Init.State = 2;
								_STATIC_get_Version_2001287A0_Pattern = "(([1-9][0-9]w[0-9]{2}[a-g])|((1|[2-9][0-9])\\.[0-9]+(\\.[0-9]+)?(-(pre|rc|snapshot-?)[1-9]*| Pre-Release( [1-9])?)?))(_unobfuscated)?";
							}
							else if (_STATIC_get_Version_2001287A0_Pattern_Init.State == 2)
							{
								throw new IncompleteInitialization();
							}
						}
						finally
						{
							_STATIC_get_Version_2001287A0_Pattern_Init.State = 1;
							if (lockTaken)
							{
								Monitor.Exit(_STATIC_get_Version_2001287A0_Pattern_Init);
							}
						}
						text = JsonObject["id"].ToString().RegexSeek(Conversions.ToString(_STATIC_get_Version_2001287A0_Pattern), RegexOptions.IgnoreCase);
						if (text != null)
						{
							_Version.VanillaName = text;
						}
						else
						{
							Logger.Info($"无法完全确认 MC 版本号的版本：{Name}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							_Version.Reliable = false;
							text = Name.RegexSeek(Conversions.ToString(_STATIC_get_Version_2001287A0_Pattern), RegexOptions.IgnoreCase);
							if (text != null)
							{
								_Version.VanillaName = text;
							}
							else
							{
								JObject jObject2 = (JObject)JsonObject.DeepClone();
								jObject2.Remove("libraries");
								text = jObject2.ToString().RegexSeek(Conversions.ToString(_STATIC_get_Version_2001287A0_Pattern), RegexOptions.IgnoreCase);
								if (text != null)
								{
									_Version.VanillaName = text;
								}
								else
								{
									_Version.VanillaName = "Unknown";
									Info = "PCL 无法识别该版本的 MC 版本号";
								}
							}
						}
						end_IL_001f_2:;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "识别 Minecraft 版本时出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						_Version.VanillaName = "Unknown";
						Info = "无法识别：" + ex2.Message;
						ProjectData.ClearProjectError();
					}
					_Version.VanillaName = _Version.VanillaName.Replace("_unobfuscated", "").Replace(" Unobfuscated", "");
					return _Version;
				}
				set
				{
					_Version = value;
				}
			}
			public string JsonText
			{
				get
				{
					VB_AnonymousDelegate_19<string, bool> obj = [SpecialName] (string Json) =>
					{
						string value = Json.Trim();
						return value.StartsWithF("{") && value.EndsWithF("}");
					};
					if (_JsonText == null)
					{
						string jsonPath = GetJsonPath();
						_JsonText = FileUtils.TryReadAsString(jsonPath) ?? "";
						if (!obj(_JsonText))
						{
							if (ModBase.RunInUi())
							{
								Logger.Warn("版本 JSON 文件为空或有误，由于代码在主线程运行，将不再进行重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								_JsonText.DeserializeJson();
							}
							else
							{
								Logger.Warn($"版本 JSON 文件为空或有误，将在 2s 后重试读取（{jsonPath}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								Thread.Sleep(2000);
								_JsonText = FileUtils.ReadAsString(jsonPath);
								if (!obj(_JsonText))
								{
									_JsonText.DeserializeJson();
								}
							}
						}
					}
					return _JsonText;
				}
				set
				{
					_JsonText = value;
				}
			}
			public JObject JsonObject
			{
				get
				{
					if (_JsonObject == null)
					{
						string jsonText = JsonText;
						try
						{
							_JsonObject = (JObject)jsonText.DeserializeJson();
							if (_JsonObject.ContainsKey("patches") && !_JsonObject.ContainsKey("time"))
							{
								IsHmclFormatJson = true;
								JObject jObject = null;
								List<JObject> list = new List<JObject>();
								foreach (JObject item in (IEnumerable<JToken>)_JsonObject["patches"])
								{
									list.Add(item);
								}
								list = ModBase.SortByComparison(list, [SpecialName] (JObject Left, JObject Right) => ModBase.Val((Left["priority"] ?? ((JToken)"0")).ToString()) < ModBase.Val((Right["priority"] ?? ((JToken)"0")).ToString()));
								foreach (JObject item2 in list)
								{
									string text = (string?)item2["id"];
									if (text != null)
									{
										Logger.Info($"合并 HMCL 分支项：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
										if (jObject != null)
										{
											jObject.Merge(item2);
										}
										else
										{
											jObject = item2;
										}
									}
									else
									{
										Logger.Info("存在为空的 HMCL 分支项", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
									}
								}
								_JsonObject = jObject;
								_JsonObject["id"] = Name;
								if (_JsonObject.ContainsKey("inheritsFrom"))
								{
									_JsonObject.Remove("inheritsFrom");
								}
							}
							object obj = null;
							try
							{
								obj = ((_JsonObject["inheritsFrom"] == null) ? "" : _JsonObject["inheritsFrom"].ToString());
								if (Operators.ConditionalCompareObjectEqual(obj, Name, TextCompare: false))
								{
									Logger.Warn($"自引用的继承版本：{Name}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
									obj = "";
								}
								else
								{
									while (Operators.ConditionalCompareObjectNotEqual(obj, "", TextCompare: false))
									{
										McInstance mcInstance = new McInstance(Conversions.ToString(obj));
										if (Operators.ConditionalCompareObjectEqual(mcInstance.InheritName, obj, TextCompare: false))
										{
											throw new Exception(Conversions.ToString(Operators.ConcatenateObject("版本依赖项出现嵌套：", obj)));
										}
										obj = mcInstance.InheritName;
										JArray jArray = (JArray)_JsonObject["libraries"].DeepClone();
										foreach (JToken item3 in (IEnumerable<JToken>)mcInstance.JsonObject["libraries"])
										{
											jArray.Add(item3);
										}
										mcInstance.JsonObject.Merge(_JsonObject);
										_JsonObject = mcInstance.JsonObject;
										_JsonObject["libraries"] = jArray;
									}
								}
							}
							catch (Exception ex)
							{
								ProjectData.SetProjectError(ex);
								Exception ex2 = ex;
								Logger.Warn(ex2, string.Format("合并版本依赖项 JSON 失败（{0}）", RuntimeHelpers.GetObjectValue(obj ?? "null")), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								ProjectData.ClearProjectError();
							}
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception innerException = ex3;
							throw new Exception("初始化版本 JSON 时失败（" + (Name ?? "null") + "）", innerException);
						}
					}
					return _JsonObject;
				}
				set
				{
					_JsonObject = value;
				}
			}
			public bool IsOldJson
			{
				get
				{
					if (JsonObject["minecraftArguments"] != null)
					{
						return (string?)JsonObject["minecraftArguments"] != "";
					}
					return false;
				}
			}
			public bool IsHmclFormatJson { get; set; }
			public JObject JsonVersion
			{
				get
				{
					if (_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init == null)
					{
						Interlocked.CompareExchange(ref _STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken = false;
					try
					{
						Monitor.Enter(_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init, ref lockTaken);
						if (_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init.State == 0)
						{
							_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init.State = 2;
							_STATIC_get_JsonVersion_2001281E9_JsonVersionInited = false;
						}
						else if (_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init.State = 1;
						if (lockTaken)
						{
							Monitor.Exit(_STATIC_get_JsonVersion_2001281E9_JsonVersionInited_Init);
						}
					}
					if (!_STATIC_get_JsonVersion_2001281E9_JsonVersionInited)
					{
						_STATIC_get_JsonVersion_2001281E9_JsonVersionInited = true;
						try
						{
							if (FileUtils.Exists($"{PathVersion}{Name}.jar"))
							{
								using ZipArchive zipArchive = FileUtils.OpenZip(PathVersion + Name + ".jar");
								ZipArchiveEntry entry = zipArchive.GetEntry("version.json");
								if (entry != null)
								{
									using StreamReader streamReader = new StreamReader(entry.Open());
									_JsonVersion = (JObject)streamReader.ReadToEnd().DeserializeJson();
								}
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, $"从版本 jar 中读取 version.json 失败（{PathVersion}{Name}.jar）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							ProjectData.ClearProjectError();
						}
					}
					return _JsonVersion;
				}
			}
			public string InheritName
			{
				get
				{
					if (_InheritName == null)
					{
						_InheritName = (JsonObject["inheritsFrom"] ?? ((JToken)"")).ToString();
						if (JsonText.Contains("liteloader") && Operators.CompareString(Version.VanillaName, Name, TextCompare: false) != 0 && !JsonText.Contains("logging") && Operators.CompareString((JsonObject["jar"] ?? ((JToken)Version.VanillaName)).ToString(), Version.VanillaName, TextCompare: false) == 0)
						{
							_InheritName = Version.VanillaName;
						}
						if (IsHmclFormatJson)
						{
							_InheritName = "";
						}
					}
					return _InheritName;
				}
			}
			public string GetJsonPath()
			{
				string text = PathVersion + Name + ".json";
				if (!FileUtils.Exists(text))
				{
					text = null;
					foreach (string item in DirectoryUtils.EnumerateFiles(PathVersion, includeSubDirectories: false, "*.json"))
					{
						try
						{
							JObject jObject = (JObject)FileUtils.ReadAsJson(item);
							if (!jObject.ContainsKey("mainClass") || !jObject.ContainsKey("type") || !jObject.ContainsKey("id"))
							{
								continue;
							}
							goto IL_008d;
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
						continue;
						IL_008d:
						text = item;
						break;
					}
					if (text == null)
					{
						throw new Exception($"未找到版本 JSON 文件：{PathVersion}{Name}.json");
					}
					Logger.Warn($"未找到同名版本 JSON，自动换用 {text}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				return text;
			}
			public void ResetSettingsCache()
			{
				ModBase.IniClearCache(PathIndie + "options.txt");
				ModBase.IniClearCache(PathVersion + "PCL\\Setup.ini");
				Config.DiscardCache();
				foreach (Settings.Setting value in Settings.Entries.Values)
				{
					if (value.Source == Settings.Sources.Instance)
					{
						value.ValueCache.Remove(PathVersion);
					}
				}
			}
			public McInstance(string NameOrFullPath)
			{
				_Name = null;
				Info = "该版本未被加载，请向作者反馈此问题";
				State = McInstanceState.Error;
				IsStar = false;
				DisplayType = McInstanceCardType.Auto;
				_Version = null;
				ReleaseTime = new DateTime(1970, 1, 1, 15, 0, 0);
				_JsonText = null;
				_JsonObject = null;
				IsHmclFormatJson = false;
				_JsonVersion = null;
				_InheritName = null;
				IsLoaded = false;
				NameOrFullPath = PathUtils.RemoveExtendedPrefix(NameOrFullPath);
				PathVersion = PathUtils.AddSlashSuffix((NameOrFullPath.Contains(":") ? "" : (McFolderSelected + "versions\\")) + NameOrFullPath);
				Config = new JsonConfigProvider(System.IO.Path.Combine(PathVersion, "PCL\\config.json"));
			}
			public bool Check()
			{
				bool result;
				if (!DirectoryUtils.Exists(PathVersion))
				{
					State = McInstanceState.Error;
					Info = "未找到版本 " + Name;
					result = false;
				}
				else
				{
					try
					{
						if (JsonObject["mainClass"] == null)
						{
							throw new Exception("版本 JSON 中没有 mainClass 项！");
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"版本 JSON 可用性检查失败（{PathVersion}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						JsonText = "";
						JsonObject = null;
						Info = ex2.Message;
						State = McInstanceState.Error;
						result = false;
						ProjectData.ClearProjectError();
						goto IL_01c9;
					}
					try
					{
						if (string.IsNullOrEmpty(Version.VanillaName))
						{
							throw new Exception("无法获取版本号，结果为空");
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Warn(ex4, $"版本号获取失败（{Name}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						State = McInstanceState.Error;
						Info = "版本号获取失败：" + ex4.GetDisplay(multiline: false);
						result = false;
						ProjectData.ClearProjectError();
						goto IL_01c9;
					}
					try
					{
						if (Operators.CompareString(InheritName, "", TextCompare: false) != 0 && !FileUtils.Exists($"{PathUtils.RemoveLastPart(PathVersion)}\\{InheritName}\\{InheritName}.json"))
						{
							State = McInstanceState.Error;
							Info = "需要安装 " + InheritName + " 作为前置版本";
							result = false;
							goto IL_01c9;
						}
					}
					catch (Exception ex5)
					{
						ProjectData.SetProjectError(ex5);
						Exception ex6 = ex5;
						Logger.Warn(ex6, $"依赖版本检查出错（{Name}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						State = McInstanceState.Error;
						Info = "未知错误：" + ex6.GetDisplay(multiline: false);
						result = false;
						ProjectData.ClearProjectError();
						goto IL_01c9;
					}
					State = McInstanceState.Original;
					result = true;
				}
				goto IL_01c9;
				IL_01c9:
				return result;
			}
			public McInstance Load()
			{
				try
				{
					if (Check())
					{
						string vanillaName = Version.VanillaName;
						if (Operators.CompareString(vanillaName, "Unknown", TextCompare: false) != 0)
						{
							if (Operators.CompareString(vanillaName, "Old", TextCompare: false) == 0)
							{
								State = McInstanceState.Old;
							}
							else
							{
								string text = (((object)JsonObject) ?? ((object)JsonText)).ToString();
								DateTime dateTime = ReleaseTime.ToUniversalTime().AddHours(2.0);
								string left = (JsonObject["type"] ?? ((JToken)"")).ToString();
								if (Operators.CompareString(left, "fool", TextCompare: false) == 0 || (dateTime.Month == 4 && dateTime.Day == 1 && Operators.CompareString(left, "snapshot", TextCompare: false) == 0) || Operators.CompareString(GetMcFoolName(Version.VanillaName), "", TextCompare: false) != 0)
								{
									State = McInstanceState.Fool;
								}
								else if (IsSnapshot())
								{
									State = McInstanceState.Snapshot;
								}
								if (text.Contains("optifine"))
								{
									State = McInstanceState.OptiFine;
									Version.OptiFine = text.RegexSeek("(?<=HD_U_)[^\":/]+") ?? "未知版本";
								}
								if (text.Contains("liteloader"))
								{
									State = McInstanceState.LiteLoader;
									Version.HasLiteLoader = true;
								}
								if (text.Contains("net.fabricmc:fabric-loader") || text.Contains("org.quiltmc:quilt-loader"))
								{
									State = McInstanceState.Fabric;
									Version.Fabric = (text.RegexSeek("(?<=(net.fabricmc:fabric-loader:)|(org.quiltmc:quilt-loader:))[0-9\\.]+(\\+build.[0-9]+)?") ?? "未知版本").Replace("+build", "");
								}
								else if (text.Contains("minecraftforge") && !text.Contains("net.neoforge"))
								{
									State = McInstanceState.Forge;
									Version.Forge = text.RegexSeek("(?<=forge:[0-9\\.]+(_pre[0-9]*)?\\-)[0-9\\.]+");
									if (Version.Forge == null)
									{
										Version.Forge = text.RegexSeek("(?<=net\\.minecraftforge:minecraftforge:)[0-9\\.]+");
									}
									if (Version.Forge == null)
									{
										Version.Forge = text.RegexSeek("(?<=net\\.minecraftforge:fmlloader:[0-9\\.]+-)[0-9\\.]+") ?? "未知版本";
									}
								}
								else if (text.Contains("net.neoforge"))
								{
									State = McInstanceState.NeoForge;
									Version.NeoForge = text.RegexSeek("(?<=orgeVersion\",[^\"]*?\")[^\"]+(?=\",)") ?? "未知版本";
								}
							}
						}
						else
						{
							State = McInstanceState.Error;
						}
					}
					Logo = ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "Logo");
					if (Operators.CompareString(Logo, "", TextCompare: false) == 0 || !Conversions.ToBoolean(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Value: false))))
					{
						switch (State)
						{
						case McInstanceState.Original:
							Logo = ModBase.PathImage + "Blocks/Grass.png";
							break;
						case McInstanceState.Snapshot:
							Logo = ModBase.PathImage + "Blocks/CommandBlock.png";
							break;
						case McInstanceState.Old:
							Logo = ModBase.PathImage + "Blocks/CobbleStone.png";
							break;
						case McInstanceState.Forge:
							Logo = ModBase.PathImage + "Blocks/Anvil.png";
							break;
						case McInstanceState.NeoForge:
							Logo = ModBase.PathImage + "Blocks/NeoForge.png";
							break;
						case McInstanceState.Fabric:
							Logo = ModBase.PathImage + "Blocks/Fabric.png";
							break;
						case McInstanceState.OptiFine:
							Logo = ModBase.PathImage + "Blocks/GrassPath.png";
							break;
						case McInstanceState.LiteLoader:
							Logo = ModBase.PathImage + "Blocks/Egg.png";
							break;
						case McInstanceState.Fool:
							Logo = ModBase.PathImage + "Blocks/GoldBlock.png";
							break;
						default:
							Logo = ModBase.PathImage + "Blocks/RedstoneBlock.png";
							break;
						}
					}
					string text2 = ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "CustomInfo");
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						Info = text2;
					}
					else
					{
						Info = VersionDisplayName();
						if (State == McInstanceState.Fool)
						{
							Info = GetMcFoolName(Version.VanillaName);
						}
						else if (State != McInstanceState.Error)
						{
							if (Settings.Get<int>("VersionServerLogin", this) == 3)
							{
								Info += ", 统一通行证验证";
							}
							if (Settings.Get<int>("VersionServerLogin", this) == 4)
							{
								Info += ", Authlib 验证";
							}
						}
					}
					IsStar = Conversions.ToBoolean(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(Value: false)));
					DisplayType = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
					if (DirectoryUtils.Exists(PathVersion))
					{
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "State", Conversions.ToString((int)State));
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "Info", Info);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "Logo", Logo);
					}
					if (State != McInstanceState.Error && Version.Reliable)
					{
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "ReleaseTime", ReleaseTime.ToString("yyyy'-'MM'-'dd HH':'mm"));
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionFabric", Version.Fabric);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionOptiFine", Version.OptiFine);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(Version.HasLiteLoader));
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionForge", Version.Forge);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionNeoForge", Version.NeoForge);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionVanillaName", Version.VanillaName);
						ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionVanilla", Version.Vanilla.ToString());
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Info = ex2.GetDisplay(multiline: false);
					Logo = ModBase.PathImage + "Blocks/RedstoneBlock.png";
					State = McInstanceState.Error;
					Logger.Warn(ex2, $"加载版本失败（{Name}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
				finally
				{
					IsLoaded = true;
				}
				return this;
			}
			private bool IsSnapshot()
			{
				string left = (JsonObject["type"] ?? ((JToken)"")).ToString();
				if (!new string[6] { "w", "snapshot", "rc", "pre", "experimental", "-" }.Any([SpecialName] (string s) => Version.VanillaName.ContainsIgnoreCase(s)) && !Name.ContainsIgnoreCase("combat") && Operators.CompareString(left, "snapshot", TextCompare: false) != 0 && Operators.CompareString(left, "pending", TextCompare: false) != 0)
				{
					return Operators.CompareString(left, "fool", TextCompare: false) == 0;
				}
				return true;
			}
			public string VersionDisplayName()
			{
				string text = "";
				if (Version.HasForge)
				{
					text = text + ", Forge" + ((Operators.CompareString(Version.Forge, "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.Forge));
				}
				if (Version.HasNeoForge)
				{
					text = text + ", NeoForge" + ((Operators.CompareString(Version.NeoForge, "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.NeoForge));
				}
				if (Version.HasFabric)
				{
					text = text + ", Fabric" + ((Operators.CompareString(Version.Fabric, "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.Fabric));
				}
				if (Version.HasOptiFine)
				{
					text = text + ", OptiFine" + ((Operators.CompareString(Version.OptiFine, "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.OptiFine.Replace("-", " ").Replace("_", " ")));
				}
				if (Version.HasLiteLoader)
				{
					text += ", LiteLoader";
				}
				string text2;
				switch (State)
				{
				case McInstanceState.Original:
				case McInstanceState.Snapshot:
				case McInstanceState.OptiFine:
				case McInstanceState.Forge:
				case McInstanceState.NeoForge:
				case McInstanceState.LiteLoader:
				case McInstanceState.Fabric:
					if (Version.VanillaName.ContainsIgnoreCase("pre"))
					{
						text2 = "预发布版 " + Version.VanillaName;
					}
					else if (Version.VanillaName.ContainsIgnoreCase("rc"))
					{
						text2 = "发布候选 " + Version.VanillaName;
					}
					else if (Version.VanillaName.ContainsIgnoreCase("experimental"))
					{
						text2 = "实验性快照" + Version.VanillaName;
					}
					else if (Operators.CompareString(Version.VanillaName, "pending", TextCompare: false) != 0)
					{
						if (IsSnapshot())
						{
							text2 = (Version.Reliable ? ("快照版 " + Version.VanillaName.Replace("-snapshot", "")) : "快照版");
						}
						else
						{
							text2 = (Version.Reliable ? ("正式版 " + Version.VanillaName) : "正式版");
						}
					}
					else
					{
						text2 = "实验性快照";
					}
					break;
				case McInstanceState.Old:
					text2 = "远古版本";
					break;
				case McInstanceState.Fool:
					text2 = "愚人节版本 " + Version.VanillaName;
					break;
				case McInstanceState.Error:
					return Info;
				default:
					return "发生了未知错误，请向作者反馈此问题";
				}
				return (text2 + text).Replace("_", "-");
			}
			public MyVirtualizingElement<MyListItem> ToListItem(Action<MyListItem, EventArgs> ContentHandler = null)
			{
				return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
				{
					MyListItem myListItem = new MyListItem
					{
						Info = Info,
						Height = 42.0,
						Tag = this,
						SnapsToDevicePixels = true,
						Type = MyListItem.CheckType.Clickable,
						ContentHandler = ContentHandler
					};
					myListItem.Inlines.Clear();
					myListItem.Inlines.Add(new Run(Name));
					if (Operators.CompareString(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "CustomInfo"), "", TextCompare: false) != 0)
					{
						myListItem.Inlines.Add(new Run("  |  " + VersionDisplayName())
						{
							Foreground = new ModBase.MyColor(215.0, 215.0, 215.0),
							FontSize = 12.0
						});
					}
					try
					{
						if (Logo.EndsWithF("PCL\\Logo.png"))
						{
							myListItem.Logo = PathVersion + "PCL\\Logo.png";
						}
						else
						{
							myListItem.Logo = Logo;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "加载版本图标失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						myListItem.Logo = "pack://application:,,,/images/Blocks/RedstoneBlock.png";
						ProjectData.ClearProjectError();
					}
					return myListItem;
				})
				{
					Height = 42.0
				};
			}
			public override bool Equals(object obj)
			{
				if (obj is McInstance mcInstance)
				{
					return Operators.CompareString(PathVersion, mcInstance.PathVersion, TextCompare: false) == 0;
				}
				return false;
			}
			public static bool operator ==(McInstance a, McInstance b)
			{
				if ((object)a == null && (object)b == null)
				{
					return true;
				}
				if ((object)a == null || (object)b == null)
				{
					return false;
				}
				return Operators.CompareString(a.PathVersion, b.PathVersion, TextCompare: false) == 0;
			}
			public static bool operator !=(McInstance a, McInstance b)
			{
				return !(a == b);
			}
		}
		public enum McInstanceState
		{
			Error,
			Original,
			Snapshot,
			Fool,
			OptiFine,
			Old,
			Forge,
			NeoForge,
			LiteLoader,
			Fabric
		}
		public class McVersion
		{
			private string _VanillaName;
			public Version Vanilla;
			public bool Reliable;
			public string OptiFine;
			public string Forge;
			public string NeoForge;
			public string Fabric;
			public bool HasLiteLoader;
			public string VanillaName
			{
				get
				{
					return _VanillaName;
				}
				set
				{
					if (Operators.CompareString(_VanillaName, value, TextCompare: false) != 0)
					{
						_VanillaName = value;
						Vanilla = NameToVersion(_VanillaName);
					}
				}
			}
			public bool Vaild => Vanilla.Major < 1000;
			public int Drop
			{
				get
				{
					if (!Vaild)
					{
						return 209;
					}
					return checked(Vanilla.Major * 10 + Vanilla.Minor);
				}
			}
			public bool HasOptiFine => OptiFine != null;
			public int OptiFineCode
			{
				get
				{
					if (OptiFine == null || Operators.CompareString(OptiFine, "未知版本", TextCompare: false) == 0)
					{
						return 0;
					}
					checked
					{
						int num = Strings.Asc(OptiFine.Upper().First()) - 65 + 1;
						num *= 100;
						num = (int)Math.Round((double)num + ModBase.Val(Strings.Right(OptiFine, OptiFine.Length - 1).RegexSeek("[0-9]+")));
						num *= 100;
						if (OptiFine.ContainsIgnoreCase("pre"))
						{
							num += 50;
						}
						int result;
						if (!OptiFine.ContainsIgnoreCase("pre") && !OptiFine.ContainsIgnoreCase("beta"))
						{
							result = num + 99;
						}
						else
						{
							result = ((ModBase.Val(Strings.Right(OptiFine, 1)) != 0.0 || Operators.CompareString(Strings.Right(OptiFine, 1), "0", TextCompare: false) == 0) ? ((int)Math.Round((double)num + ModBase.Val(OptiFine.Lower().RegexSeek("(?<=((pre)|(beta)))[0-9]+")))) : (num + 1));
						}
						return result;
					}
				}
			}
			public bool HasForge => Forge != null;
			public bool HasNeoForge => NeoForge != null;
			public bool HasForgelike
			{
				get
				{
					if (!HasForge)
					{
						return HasNeoForge;
					}
					return true;
				}
			}
			public int ForgelikeCode
			{
				get
				{
					if (!HasForgelike)
					{
						return 0;
					}
					if ((Forge == null || Operators.CompareString(Forge, "未知版本", TextCompare: false) == 0) && (NeoForge == null || Operators.CompareString(NeoForge, "未知版本", TextCompare: false) == 0))
					{
						return 0;
					}
					List<string> list = (HasForge ? Forge : NeoForge).RegexSearch("\\d+").ToList();
					int count = list.Count;
					checked
					{
						if (count > 4)
						{
							return (int)Math.Round(ModBase.Val(list[0]) * 1000000.0 + ModBase.Val(list[1]) * 10000.0 + ModBase.Val(list[3]));
						}
						return count switch
						{
							3 => (int)Math.Round(ModBase.Val(list[0]) * 1000000.0 + ModBase.Val(list[1]) * 10000.0 + ModBase.Val(list[2])), 
							2 => (int)Math.Round(ModBase.Val(list[0]) * 1000000.0 + ModBase.Val(list[1]) * 10000.0), 
							_ => (int)Math.Round(ModBase.Val(list[0]) * 1000000.0), 
						};
					}
				}
			}
			public bool HasFabric => Fabric != null;
			public ModLoaders ModLoaders
			{
				get
				{
					ModLoaders modLoaders = ModLoaders.None;
					if (HasForge)
					{
						modLoaders |= ModLoaders.Forge;
					}
					if (HasNeoForge)
					{
						modLoaders |= ModLoaders.NeoForge;
					}
					if (HasFabric)
					{
						modLoaders |= ModLoaders.Fabric;
					}
					if (HasLiteLoader)
					{
						modLoaders |= ModLoaders.LiteLoader;
					}
					return modLoaders;
				}
			}
			public McVersion()
			{
				Reliable = true;
				OptiFine = null;
				Forge = null;
				NeoForge = null;
				Fabric = null;
				HasLiteLoader = false;
			}
			public static bool IsFormatFit(string Version)
			{
				if (Version == null)
				{
					return false;
				}
				if (Version.RegexCheck("^1\\.\\d"))
				{
					return true;
				}
				if (ModBase.Val(Version.RegexSeek("^[2-9]\\d\\.\\d+")) >= 26.0)
				{
					return true;
				}
				return false;
			}
			public static int VersionToDrop(string VanillaName)
			{
				if (VanillaName == null)
				{
					return 209;
				}
				Version version = NameToVersion(VanillaName);
				if (version.Major >= 1000)
				{
					return 209;
				}
				return checked(version.Major * 10 + version.Minor);
			}
			public static string DropToVersion(int Drop)
			{
				if (Drop >= 250)
				{
					return $"{Drop / 10}.{Drop % 10}";
				}
				if (Drop == 209)
				{
					Logger.Warn(new Exception("尝试将旧快照版 Drop 序数转换为版本字符串，这不应该发生！"), null, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					return "1.20";
				}
				return $"1.{Drop / 10}";
			}
			public static Version NameToVersion(string Name)
			{
				Name = Name.Lower().Replace("_unobfuscated", "").Replace(" Unobfuscated", "");
				if (Name.StartsWithF("2.0"))
				{
					return NameToVersion("1.5.1");
				}
				if (Operators.CompareString(Name, "15w14a", TextCompare: false) == 0)
				{
					return NameToVersion("1.8.3");
				}
				if (Name.Contains(".rv-pre"))
				{
					return NameToVersion("1.9.2");
				}
				if (Name.Contains("shareware"))
				{
					return NameToVersion("1.13.2");
				}
				if (Name.StartsWithF("20w14") && Operators.CompareString(Name, "20w14a", TextCompare: false) != 0)
				{
					return NameToVersion("1.15.2");
				}
				if (Name.Contains("oneblockatatime"))
				{
					return NameToVersion("1.18.2");
				}
				if (Name.Contains("23w13a") && Operators.CompareString(Name, "20w13a", TextCompare: false) != 0)
				{
					return NameToVersion("1.19.4");
				}
				checked
				{
					switch (Name)
					{
					case "24w14potato":
						return NameToVersion("1.20.4");
					case "25w14craftmine":
						return NameToVersion("1.21.4");
					case "26w14a":
						return NameToVersion("26.1.1");
					default:
						if (Name.StartsWithF("1."))
						{
							string[] array = Name.Split(" _-.".ToCharArray());
							return new Version((int)Math.Round(ModBase.Val((array.Count() >= 2) ? array[1] : "0")), 0, (int)Math.Round(ModBase.Val((array.Count() >= 3) ? array[2] : "0")));
						}
						if (Name.RegexCheck("^[2-9][0-9]\\."))
						{
							string[] array2 = Name.Split(" _-.".ToCharArray());
							return new Version((int)Math.Round(ModBase.Val(array2[0])), (int)Math.Round(ModBase.Val((array2.Count() >= 2) ? array2[1] : "0")), (int)Math.Round(ModBase.Val((array2.Count() >= 3) ? array2[2] : "0")));
						}
						return new Version(9999, 0, 0);
					}
				}
			}
		}
		public enum McInstanceCardType
		{
			Star = -1,
			Auto,
			Hidden,
			API,
			OriginalLike,
			Rubbish,
			Fool,
			Error
		}
		public struct McSkinInfo
		{
			public bool IsSlim;
			public string LocalFile;
			public bool IsVaild;
		}
		public class McLibEntry
		{
			public string LocalPath;
			public long Size;
			public bool IsNatives;
			public string SHA1;
			private string _Url;
			public string OriginalName;
			public string Url
			{
				get
				{
					return _Url;
				}
				set
				{
					_Url = (string.IsNullOrWhiteSpace(value) ? null : value);
				}
			}
			public string Name
			{
				get
				{
					if (OriginalName == null)
					{
						return null;
					}
					List<string> list = new List<string>(OriginalName.Split(":"));
					list.RemoveAt(2);
					return list.Join(':');
				}
			}
			public McLibEntry()
			{
				Size = 0L;
				IsNatives = false;
				SHA1 = null;
			}
			public override string ToString()
			{
				return (IsNatives ? "[Native] " : "") + StringUtils.FormatByteSize(Size) + " | " + LocalPath;
			}
		}
		private struct McAssetsToken
		{
			public string LocalPath;
			public string SourcePath;
			public long Size;
			public string Hash;
			public override string ToString()
			{
				return StringUtils.FormatByteSize(Size) + " | " + LocalPath;
			}
		}
		public class VersionComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				return CompareVersion(x, y);
			}
			int IComparer<string>.Compare(string x, string y)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Compare
				return this.Compare(x, y);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___24_0
		{
			public McInstance _VB_Local_Instance;
			public _Closure___24_0(_Closure___24_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Instance = arg0._VB_Local_Instance;
				}
			}
			[SpecialName]
			internal string _Lambda___0(string Key)
			{
				string text = ModBase.ReadIni(_VB_Local_Instance.PathVersion + "PCL\\Setup.ini", Key, null);
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					return text;
				}
				return null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___25_0
		{
			public VB_AnonymousDelegate_21<McInstance, int> _VB_Local_GetComponentCode;
			public _Closure___25_0(_Closure___25_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_GetComponentCode = arg0._VB_Local_GetComponentCode;
				}
			}
			[SpecialName]
			internal bool _Lambda___3(McInstance Left, McInstance Right)
			{
				if ((Left.ReleaseTime.Year >= 2000 || Right.ReleaseTime.Year >= 2000) && DateTime.Compare(Left.ReleaseTime, Right.ReleaseTime) != 0)
				{
					return DateTime.Compare(Left.ReleaseTime, Right.ReleaseTime) > 0;
				}
				if (Left.Version.HasFabric != Right.Version.HasFabric)
				{
					return Left.Version.HasFabric;
				}
				if (Left.Version.HasNeoForge != Right.Version.HasNeoForge)
				{
					return Left.Version.HasNeoForge;
				}
				if (Left.Version.HasForge != Right.Version.HasForge)
				{
					return Left.Version.HasForge;
				}
				if (Left.Version.HasOptiFine != Right.Version.HasOptiFine)
				{
					return Left.Version.HasOptiFine;
				}
				if (Left.Version.HasLiteLoader != Right.Version.HasLiteLoader)
				{
					return Left.Version.HasLiteLoader;
				}
				if (_VB_Local_GetComponentCode(Left) != _VB_Local_GetComponentCode(Right))
				{
					return _VB_Local_GetComponentCode(Left) > _VB_Local_GetComponentCode(Right);
				}
				return Operators.CompareString(Left.Name, Right.Name, TextCompare: false) > 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___49_0
		{
			public JToken _VB_Local_Version;
			public _Closure___49_0(_Closure___49_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Version = arg0._VB_Local_Version;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				ModDownloadLib.McUpdateLogShow(_VB_Local_Version);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___49_1
		{
			public string _VB_Local_VersionName;
			public _Closure___49_1(_Closure___49_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_VersionName = arg0._VB_Local_VersionName;
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				PageDownloadInstall.VersionWaitingSelect = _VB_Local_VersionName;
				ModMain.FrmMain.PageChange(FormMain.PageType.Download);
			}
		}
		private static string _McFolderSelected;
		public static List<McFolder> McFolderList = new List<McFolder>();
		public static ModLoader.LoaderTask<int, int> McFolderListLoader = new ModLoader.LoaderTask<int, int>("Minecraft Folder List", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
		{
			McFolderListLoadSub();
		}, null, ThreadPriority.AboveNormal);
		private static McInstance _McInstanceSelected;
		public static Dictionary<McInstanceCardType, List<McInstance>> McInstanceList = new Dictionary<McInstanceCardType, List<McInstance>>();
		public static bool McInstanceListForceRefresh = false;
		private static bool IsFirstMcInstanceListLoad = true;
		public static ModLoader.LoaderTask<string, int> McInstanceListLoader = new ModLoader.LoaderTask<string, int>("Minecraft Instance List", InitMcInstanceList)
		{
			ReloadTimeout = 1
		};
		private static readonly object McSkinDownloadLock = RuntimeHelpers.GetObjectValue(new object());
		private static string OSVersion = MyWpfExtension.Computer.Info.OSVersion;
		[SpecialName]
		private static object _STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init;
		public static string McFolderSelected
		{
			get
			{
				if (_McFolderSelected == null)
				{
					SetMcFolder(Settings.Get<string>("LaunchFolderSelect"));
				}
				return _McFolderSelected;
			}
			set
			{
				SetMcFolder(value);
			}
		}
		public static McInstance McInstanceSelected
		{
			get
			{
				return _McInstanceSelected;
			}
			set
			{
				if (_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init == null)
				{
					Interlocked.CompareExchange(ref _STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init, ref lockTaken);
					if (_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init.State == 0)
					{
						_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init.State = 2;
						_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast = 0;
					}
					else if (_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast_Init);
					}
				}
				if (object.ReferenceEquals(RuntimeHelpers.GetObjectValue(_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast), value))
				{
					return;
				}
				_McInstanceSelected = value;
				_STATIC_set_McInstanceSelected_011128798__McInstanceSelectedLast = value;
				Logger.Info(string.Format("当前选择的 Minecraft 版本：{0}", value?.PathVersion ?? "无"), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ModBase.WriteIni(McFolderSelected + "PCL.ini", "Version", value?.Name ?? "");
				if ((object)value != null)
				{
					PageDownloadResourceDetail.CachedFolder.Clear();
					if (ModAnimation.AniControlEnabled == 0 && Operators.CompareString(Settings.Get<string>("VersionServerNide", value), Settings.Get<string>("CacheNideServer"), TextCompare: false) != 0 && Settings.Get<int>("VersionServerLogin", value) == 3)
					{
						Settings.Set("CacheNideAccess", "");
						Logger.Info("服务器改变，要求重新登录统一通行证", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					}
					if (Settings.Get<int>("VersionServerLogin", value) == 3)
					{
						Settings.Set("CacheNideServer", Settings.Get<string>("VersionServerNide", value));
					}
					if (ModAnimation.AniControlEnabled == 0 && Operators.CompareString(Settings.Get<string>("VersionServerAuthServer", value), Settings.Get<string>("CacheAuthServerServer"), TextCompare: false) != 0 && Settings.Get<int>("VersionServerLogin", value) == 4)
					{
						Settings.Set("CacheAuthAccess", "");
						Logger.Info("服务器改变，要求重新登录 Authlib-Injector", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					}
					if (Settings.Get<int>("VersionServerLogin", value) == 4)
					{
						Settings.Set("CacheAuthServerServer", Settings.Get<string>("VersionServerAuthServer", value));
						Settings.Set("CacheAuthServerName", Settings.Get<string>("VersionServerAuthName", value));
						Settings.Set("CacheAuthServerRegister", Settings.Get<string>("VersionServerAuthRegister", value));
					}
				}
			}
		}
		private static void SetMcFolder(string Value)
		{
			if (Operators.CompareString(_McFolderSelected, Value, TextCompare: false) != 0)
			{
				_McFolderSelected = Value.Replace("$", Paths.Base);
				_McFolderSelected = PathUtils.AddSlashSuffix(PathUtils.ForCompare(_McFolderSelected));
				Settings.Set("LaunchFolderSelect", Value.Replace(Paths.Base, "$"));
				Logger.Info($"当前选择的 Minecraft 文件夹：{_McFolderSelected}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
		}
		private static void McFolderListLoadSub()
		{
			try
			{
				List<McFolder> list = new List<McFolder>();
				try
				{
					if (DirectoryUtils.Exists(Paths.Base + "versions\\"))
					{
						list.Add(new McFolder
						{
							Name = "当前文件夹",
							Location = Paths.Base,
							Type = McFolder.Types.Vanilla
						});
					}
					DirectoryInfo[] directories = DirectoryUtils.GetInfo(Paths.Base).GetDirectories();
					foreach (DirectoryInfo directoryInfo in directories)
					{
						if (DirectoryUtils.Exists(directoryInfo.FullName + "versions\\") || Operators.CompareString(directoryInfo.Name, ".minecraft", TextCompare: false) == 0)
						{
							list.Add(new McFolder
							{
								Name = "当前文件夹",
								Location = PathUtils.AddSlashSuffix(PathUtils.ForCompare(directoryInfo.FullName)),
								Type = McFolder.Types.Vanilla
							});
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "扫描 PCL 所在文件夹中是否有 MC 文件夹失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
				string text = Paths.AppData + ".minecraft\\";
				if ((!list.Any() || Operators.CompareString(text, list[0].Location, TextCompare: false) != 0) && DirectoryUtils.Exists(text + "versions\\"))
				{
					list.Add(new McFolder
					{
						Name = "官方启动器文件夹",
						Location = text,
						Type = McFolder.Types.Vanilla
					});
				}
				string[] array = Settings.Get<string>("LaunchFolders").ToString().Split('|', removeEmptyEntries: true);
				foreach (string text2 in array)
				{
					if (Operators.CompareString(text2, "", TextCompare: false) == 0)
					{
						continue;
					}
					if (!text2.Contains(">") || !text2.EndsWithF("\\"))
					{
						ModMain.Hint("无效的 Minecraft 文件夹：" + text2, ModMain.HintType.Red);
						continue;
					}
					string name = text2.BeforeFirst(">");
					string text3 = text2.AfterFirst(">");
					try
					{
						ModBase.CheckPermissionWithException(text3);
						bool flag = false;
						foreach (McFolder item in list)
						{
							if (Operators.CompareString(item.Location, text3, TextCompare: false) == 0)
							{
								item.Name = name;
								item.Type = McFolder.Types.RenamedVanilla;
								flag = true;
							}
						}
						if (!flag)
						{
							list.Add(new McFolder
							{
								Name = name,
								Location = PathUtils.AddSlashSuffix(PathUtils.ForCompare(text3)),
								Type = McFolder.Types.Custom
							});
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						ModMain.MyMsgBox("失效的 Minecraft 文件夹：\r\n" + text3 + "\r\n\r\n" + ex4.GetDisplay(multiline: false), "Minecraft 文件夹失效", "确定", "", "", IsWarn: true);
						Logger.Warn(ex4, $"无法访问 Minecraft 文件夹 {text3}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						ProjectData.ClearProjectError();
					}
				}
				List<string> list2 = new List<string>();
				foreach (McFolder item2 in list)
				{
					if (item2.Type != McFolder.Types.Vanilla)
					{
						list2.Add(item2.Name + ">" + item2.Location);
					}
				}
				if (!list2.Any())
				{
					list2.Add("");
				}
				Settings.Set("LaunchFolders", list2.Join('|'));
				if (!list.Any())
				{
					DirectoryUtils.Create(Paths.Base + ".minecraft\\versions\\");
					list.Add(new McFolder
					{
						Name = "当前文件夹",
						Location = Paths.Base + ".minecraft\\",
						Type = McFolder.Types.Vanilla
					});
				}
				foreach (McFolder item3 in list)
				{
					McFolderLauncherProfilesJsonCreate(item3.Location);
				}
				if (Settings.Get<bool>("SystemDebugDelay"))
				{
					Thread.Sleep(ModBase.RandomInteger(200, 2000));
				}
				McFolderList = list;
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "加载 Minecraft 文件夹列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static void McFolderLauncherProfilesJsonCreate(string Folder)
		{
			try
			{
				if (!FileUtils.Exists(Folder + "launcher_profiles.json"))
				{
					string text = "{\r\n    \"profiles\":  {\r\n        \"PCL\": {\r\n            \"icon\": \"Grass\",\r\n            \"name\": \"PCL\",\r\n            \"lastVersionId\": \"latest-release\",\r\n            \"type\": \"latest-release\",\r\n            \"lastUsed\": \"" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + "T" + DateTime.Now.ToString("HH':'mm':'ss") + ".0000Z\"\r\n        }\r\n    },\r\n    \"selectedProfile\": \"PCL\",\r\n    \"clientToken\": \"23323323323323323323323323323333\"\r\n}";
					FileUtils.Write(Folder + "launcher_profiles.json", text, Encoding.GetEncoding("GB18030"));
					Logger.Info($"已创建 launcher_profiles.json：{Folder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"创建 launcher_profiles.json 失败（{Folder}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static string GetMcFoolName(string Name)
		{
			Name = Name.Lower();
			if (Name.StartsWithF("2.0"))
			{
				return "2013 | 这个秘密计划了两年的更新将游戏推向了一个新高度！";
			}
			switch (Name)
			{
			case "15w14a":
				return "2015 | 作为一款全年龄向的游戏，我们需要和平，需要爱与拥抱。";
			case "1.rv-pre1":
				return "2016 | 是时候将现代科技带入 Minecraft 了！";
			case "3d shareware v1.34":
				return "2019 | 我们从地下室的废墟里找到了这个开发于 1994 年的杰作！";
			default:
				if (!Name.StartsWithF("20w14inf"))
				{
					switch (Name)
					{
					case "20w14∞":
						break;
					case "22w13oneblockatatime":
						return "2022 | 一次一个方块更新！迎接全新的挖掘、合成与骑乘玩法吧！";
					case "23w13a_or_b":
						return "2023 | 研究表明：玩家喜欢作出选择——越多越好！";
					case "24w14potato":
						return "2024 | 毒马铃薯一直都被大家忽视和低估，于是我们超级加强了它！";
					case "25w14craftmine":
						return "2025 | 你可以合成任何东西——包括合成你的世界！";
					case "26w14a":
						return "2026 | 为什么需要物品栏？让方块们跟着你走吧！";
					default:
						return "";
					}
				}
				return "2020 | 我们加入了 20 亿个新的维度，让无限的想象变成了现实！";
			}
		}
		private static void InitMcInstanceList(ModLoader.LoaderTask<string, int> Loader)
		{
			string input = Loader.Input;
			try
			{
				McInstanceList = new Dictionary<McInstanceCardType, List<McInstance>>();
				List<string> list = new List<string>();
				if (DirectoryUtils.Exists(input + "versions"))
				{
					try
					{
						DirectoryInfo[] directories = DirectoryUtils.GetInfo(input + "versions").GetDirectories();
						foreach (DirectoryInfo directoryInfo in directories)
						{
							list.Add(directoryInfo.Name);
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception innerException = ex;
						throw new Exception("无法读取版本文件夹，可能是由于没有权限（" + input + "versions）", innerException);
					}
				}
				if (!list.Any())
				{
					ModBase.WriteIni(input + "PCL.ini", "InstanceCache", "");
					goto IL_0167;
				}
				int num = Convert.ToInt32(decimal.Remainder(new decimal((Conversions.ToString(38) + "#" + list.Join('#')).GetStableHashCode()), 2147483646m));
				if (!McInstanceListForceRefresh && ModBase.Val(ModBase.ReadIni(input + "PCL.ini", "InstanceCache")) == (double)num)
				{
					Dictionary<McInstanceCardType, List<McInstance>> dictionary = InitMcInstanceListWithCache(input);
					if (dictionary != null)
					{
						McInstanceList = dictionary;
						goto IL_0161;
					}
				}
				McInstanceListForceRefresh = false;
				Logger.Info("文件夹列表变更，重载所有版本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ModBase.WriteIni(input + "PCL.ini", "InstanceCache", Conversions.ToString(num));
				McInstanceList = InitMcInstanceListWithoutCache(input);
				goto IL_0161;
				IL_0161:
				IsFirstMcInstanceListLoad = false;
				goto IL_0167;
				IL_0167:
				if (Loader.IsCanceled)
				{
					return;
				}
				if (McInstanceList.Any([SpecialName] (KeyValuePair<McInstanceCardType, List<McInstance>> v) => v.Key != McInstanceCardType.Error))
				{
					string text = ModBase.ReadIni(input + "PCL.ini", "Version");
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						foreach (KeyValuePair<McInstanceCardType, List<McInstance>> mcInstance in McInstanceList)
						{
							foreach (McInstance item in mcInstance.Value)
							{
								if (Operators.CompareString(item.Name, text, TextCompare: false) == 0 && item.State != McInstanceState.Error)
								{
									McInstanceSelected = item;
									Logger.Info($"选择该文件夹储存的 Minecraft 版本：{McInstanceSelected.PathVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
									return;
								}
							}
						}
					}
					if (McInstanceList.First().Value[0].State != McInstanceState.Error)
					{
						McInstanceSelected = McInstanceList.First().Value[0];
						Logger.Info($"自动选择 Minecraft 版本：{McInstanceSelected.PathVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					}
				}
				else
				{
					McInstanceSelected = null;
					Logger.Info("未找到可用 Minecraft 版本", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				if (Settings.Get<bool>("SystemDebugDelay"))
				{
					Thread.Sleep(ModBase.RandomInteger(200, 3000));
				}
			}
			catch (Exception ex2)
			{
				ProjectData.SetProjectError(ex2);
				Exception ex3 = ex2;
				if (Loader.IsCanceled || ex3.IsCanceled())
				{
					ProjectData.ClearProjectError();
					return;
				}
				ModBase.WriteIni(input + "PCL.ini", "InstanceCache", "");
				Logger.Error(ex3, "加载 .minecraft 版本列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static Dictionary<McInstanceCardType, List<McInstance>> InitMcInstanceListWithCache(string Folder)
		{
			Dictionary<McInstanceCardType, List<McInstance>> dictionary = new Dictionary<McInstanceCardType, List<McInstance>>();
			Dictionary<McInstanceCardType, List<McInstance>> result;
			try
			{
				int num = Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL.ini", "CardCount", Conversions.ToString(-1)));
				if (num == -1)
				{
					result = null;
				}
				else
				{
					int num2 = checked(num - 1);
					int num3 = 0;
					_Closure___24_0 obj = default;
					while (true)
					{
						if (num3 <= num2)
						{
							McInstanceCardType key = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL.ini", "CardKey" + Conversions.ToString(checked(num3 + 1)), ":"));
							List<McInstance> list = new List<McInstance>();
							checked
							{
								string[] array = ModBase.ReadIni(Folder + "PCL.ini", "CardValue" + Conversions.ToString(num3 + 1), ":").Split(":");
								int num4 = 0;
								while (true)
								{
									unchecked
									{
										if (num4 < array.Length)
										{
											string text = array[num4];
											if (Operators.CompareString(text, "", TextCompare: false) != 0)
											{
												string text2 = $"{Folder}versions\\{text}\\";
												if (FileUtils.Exists(text2 + ".pclignore"))
												{
													if (!IsFirstMcInstanceListLoad)
													{
														Logger.Info($"跳过要求忽略的项目：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
														goto IL_0554;
													}
													Logger.Info($"清理残留的忽略项目：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
													FileUtils.Delete(text2 + ".pclignore");
												}
												try
												{
													obj = new _Closure___24_0(obj);
													obj._VB_Local_Instance = new McInstance(text2);
													list.Add(obj._VB_Local_Instance);
													obj._VB_Local_Instance.Info = ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
													if (Operators.CompareString(obj._VB_Local_Instance.Info, "", TextCompare: false) == 0)
													{
														obj._VB_Local_Instance.Info = ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "Info", obj._VB_Local_Instance.Info);
													}
													obj._VB_Local_Instance.Logo = ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "Logo", obj._VB_Local_Instance.Logo);
													obj._VB_Local_Instance.ReleaseTime = Conversions.ToDate(ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "ReleaseTime", Conversions.ToString(obj._VB_Local_Instance.ReleaseTime)));
													obj._VB_Local_Instance.State = (McInstanceState)Conversions.ToInteger(ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "State", Conversions.ToString((int)obj._VB_Local_Instance.State)));
													obj._VB_Local_Instance.IsStar = Conversions.ToBoolean(ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(Value: false)));
													obj._VB_Local_Instance.DisplayType = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
													if (obj._VB_Local_Instance.State != McInstanceState.Error && Operators.CompareString(ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "VersionVanillaName", "Unknown"), "Unknown", TextCompare: false) != 0)
													{
														VB_AnonymousDelegate_20<string, string> obj2 = obj._Lambda___0;
														McVersion version = new McVersion
														{
															Fabric = obj2("VersionFabric"),
															Forge = obj2("VersionForge"),
															NeoForge = obj2("VersionNeoForge"),
															OptiFine = obj2("VersionOptiFine"),
															HasLiteLoader = Conversions.ToBoolean(ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(Value: false))),
															VanillaName = ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "VersionVanillaName", "Unknown")
														};
														obj._VB_Local_Instance.Version = version;
													}
													else
													{
														obj._VB_Local_Instance.Load();
													}
													if (obj._VB_Local_Instance.State == McInstanceState.Error)
													{
														string info = obj._VB_Local_Instance.Info;
														obj._VB_Local_Instance.State = McInstanceState.Original;
														obj._VB_Local_Instance.Check();
														string left = ModBase.ReadIni(obj._VB_Local_Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
														if (obj._VB_Local_Instance.State == McInstanceState.Original || (Operators.CompareString(left, "", TextCompare: false) == 0 && Operators.CompareString(info, obj._VB_Local_Instance.Info, TextCompare: false) != 0))
														{
															Logger.Info($"版本 {obj._VB_Local_Instance.Name} 的错误状态已变更，新的状态为：{obj._VB_Local_Instance.Info}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
															result = null;
															goto end_IL_057e;
														}
													}
													if (Operators.CompareString(obj._VB_Local_Instance.Logo, "", TextCompare: false) == 0)
													{
														Logger.Info($"版本 {obj._VB_Local_Instance.Name} 未被加载", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
														result = null;
														goto end_IL_057e;
													}
												}
												catch (Exception ex)
												{
													ProjectData.SetProjectError(ex);
													Exception ex2 = ex;
													Logger.Warn(ex2, $"读取版本加载缓存失败（{text}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
													result = null;
													ProjectData.ClearProjectError();
													goto end_IL_057e;
												}
											}
											goto IL_0554;
										}
										if (list.Any())
										{
											dictionary.Add(key, list);
										}
									}
									num3++;
									break;
									IL_0554:
									num4++;
								}
								continue;
							}
						}
						result = dictionary;
						break;
						continue;
						end_IL_057e:
						break;
					}
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "读取版本缓存失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				result = null;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private static Dictionary<McInstanceCardType, List<McInstance>> InitMcInstanceListWithoutCache(string Folder)
		{
			List<McInstance> InstanceList = new List<McInstance>();
			DirectoryInfo[] directories = DirectoryUtils.GetInfo(Folder + "versions").GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				if (!directoryInfo.Exists || !directoryInfo.EnumerateFiles().Any())
				{
					Logger.Info($"跳过空文件夹：{directoryInfo.FullName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					continue;
				}
				if ((Operators.CompareString(directoryInfo.Name, "cache", TextCompare: false) == 0 || Operators.CompareString(directoryInfo.Name, "BLClient", TextCompare: false) == 0 || Operators.CompareString(directoryInfo.Name, "PCL", TextCompare: false) == 0) && !FileUtils.Exists(directoryInfo.FullName + "\\" + directoryInfo.Name + ".json"))
				{
					Logger.Info($"跳过可能不是版本文件夹的项目：{directoryInfo.FullName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					continue;
				}
				string text = directoryInfo.FullName + "\\";
				if (FileUtils.Exists(text + ".pclignore"))
				{
					if (!IsFirstMcInstanceListLoad)
					{
						Logger.Info($"跳过要求忽略的项目：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						continue;
					}
					Logger.Info($"清理残留的忽略项目：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					FileUtils.Delete(text + ".pclignore");
				}
				McInstance mcInstance = new McInstance(text);
				InstanceList.Add(mcInstance);
				mcInstance.Load();
			}
			Dictionary<McInstanceCardType, List<McInstance>> dictionary = new Dictionary<McInstanceCardType, List<McInstance>>();
			try
			{
				Dictionary<McInstanceCardType, List<McInstance>> Target = new Dictionary<McInstanceCardType, List<McInstance>>();
				List<McInstance> list = new List<McInstance>();
				foreach (McInstance item in InstanceList.ToList())
				{
					if (item.IsStar && item.DisplayType != McInstanceCardType.Hidden)
					{
						list.Add(item);
						InstanceList.Remove(item);
					}
				}
				if (list.Any())
				{
					Target.Add(McInstanceCardType.Star, list);
				}
				McInstanceFilter(ref InstanceList, ref Target, new McInstanceState[1], McInstanceCardType.Error);
				McInstanceFilter(ref InstanceList, ref Target, new McInstanceState[1] { McInstanceState.Fool }, McInstanceCardType.Fool);
				McInstanceFilter(ref InstanceList, ref Target, new McInstanceState[4]
				{
					McInstanceState.Forge,
					McInstanceState.NeoForge,
					McInstanceState.LiteLoader,
					McInstanceState.Fabric
				}, McInstanceCardType.API);
				List<McInstance> list2 = new List<McInstance>();
				List<McInstance> KeepList = new List<McInstance>();
				McInstanceFilter(ref InstanceList, new McInstanceState[1] { McInstanceState.Old }, ref KeepList);
				McInstance mcInstance2 = InstanceList.Where([SpecialName] (McInstance v) => v.State == McInstanceState.Original || v.State == McInstanceState.Snapshot).MaxBy([SpecialName] (McInstance v) => v.ReleaseTime);
				if ((object)mcInstance2 != null && mcInstance2.State == McInstanceState.Snapshot)
				{
					list2.Add(mcInstance2);
					InstanceList.Remove(mcInstance2);
				}
				McInstanceFilter(ref InstanceList, new McInstanceState[1] { McInstanceState.Snapshot }, ref KeepList);
				Dictionary<string, McInstance> dictionary2 = new Dictionary<string, McInstance>();
				List<int> list3 = new List<int>();
				foreach (McInstance item2 in InstanceList)
				{
					if (!item2.Version.Vaild)
					{
						continue;
					}
					if (!list3.Contains(item2.Version.Drop))
					{
						list3.Add(item2.Version.Drop);
					}
					string key = Conversions.ToString(item2.Version.Drop) + "-" + Conversions.ToString((int)item2.State);
					if (!dictionary2.ContainsKey(key))
					{
						dictionary2.Add(key, item2);
					}
					else if (item2.Version.HasOptiFine)
					{
						if (item2.Version.OptiFineCode > dictionary2[key].Version.OptiFineCode)
						{
							dictionary2[key] = item2;
						}
					}
					else if (DateTime.Compare(item2.ReleaseTime, dictionary2[key].ReleaseTime) > 0)
					{
						dictionary2[key] = item2;
					}
				}
				foreach (int item3 in list3)
				{
					if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(4)) && dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(1)))
					{
						McInstance mcInstance3 = dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)];
						McInstance mcInstance4 = dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)];
						if (mcInstance3.Version.Drop > mcInstance4.Version.Drop)
						{
							list2.Add(mcInstance3);
							InstanceList.Remove(mcInstance3);
						}
						list2.Add(mcInstance4);
						InstanceList.Remove(mcInstance4);
					}
					else if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(4)))
					{
						list2.Add(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)]);
						InstanceList.Remove(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)]);
					}
					else if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(1)))
					{
						list2.Add(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)]);
						InstanceList.Remove(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)]);
					}
				}
				KeepList.AddRange(InstanceList);
				if (list2.Any())
				{
					Target.Add(McInstanceCardType.OriginalLike, list2);
				}
				if (KeepList.Any())
				{
					Target.Add(McInstanceCardType.Rubbish, KeepList);
				}
				foreach (KeyValuePair<McInstanceCardType, List<McInstance>> item4 in Target)
				{
					foreach (McInstance item5 in item4.Value)
					{
						McInstanceCardType key2 = ((item5.DisplayType == McInstanceCardType.Auto || item4.Key == McInstanceCardType.Star) ? item4.Key : item5.DisplayType);
						if (!dictionary.ContainsKey(key2))
						{
							dictionary.Add(key2, new List<McInstance>());
						}
						dictionary[key2].Add(item5);
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				dictionary.Clear();
				Logger.Error(ex2, "分类版本列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			Dictionary<McInstanceCardType, List<McInstance>> dictionary3 = new Dictionary<McInstanceCardType, List<McInstance>>();
			McInstanceCardType[] array = new McInstanceCardType[7]
			{
				McInstanceCardType.Star,
				McInstanceCardType.API,
				McInstanceCardType.OriginalLike,
				McInstanceCardType.Rubbish,
				McInstanceCardType.Fool,
				McInstanceCardType.Error,
				McInstanceCardType.Hidden
			};
			for (int num = 0; num < array.Length; num = checked(num + 1))
			{
				string value = Conversions.ToString((int)array[num]);
				if (dictionary.ContainsKey((McInstanceCardType)Conversions.ToInteger(value)))
				{
					dictionary3.Add((McInstanceCardType)Conversions.ToInteger(value), dictionary[(McInstanceCardType)Conversions.ToInteger(value)]);
				}
			}
			dictionary = dictionary3;
			McInstanceCardType[] array2 = new McInstanceCardType[5]
			{
				McInstanceCardType.Star,
				McInstanceCardType.API,
				McInstanceCardType.OriginalLike,
				McInstanceCardType.Rubbish,
				McInstanceCardType.Fool
			};
			_Closure___25_0 obj = default;
			foreach (McInstanceCardType key3 in array2)
			{
				obj = new _Closure___25_0(obj);
				if (!dictionary.ContainsKey(key3))
				{
					continue;
				}
				obj._VB_Local_GetComponentCode = [SpecialName] (McInstance Instance) =>
				{
					if (Instance.Version.ForgelikeCode > 0)
					{
						return Instance.Version.ForgelikeCode;
					}
					return Instance.Version.HasOptiFine ? Instance.Version.OptiFineCode : 0;
				};
				dictionary[key3] = ModBase.SortByComparison(dictionary[key3], obj._Lambda___3);
			}
			ModBase.WriteIni(Folder + "PCL.ini", "CardCount", Conversions.ToString(dictionary.Count));
			checked
			{
				int num3 = dictionary.Count - 1;
				for (int num4 = 0; num4 <= num3; num4++)
				{
					ModBase.WriteIni(Folder + "PCL.ini", "CardKey" + Conversions.ToString(num4 + 1), Conversions.ToString(unchecked((int)dictionary.Keys.ElementAtOrDefault(num4))));
					string text2 = "";
					foreach (McInstance item6 in dictionary.Values.ElementAtOrDefault(num4))
					{
						text2 = text2 + item6.Name + ":";
					}
					ModBase.WriteIni(Folder + "PCL.ini", "CardValue" + Conversions.ToString(num4 + 1), text2);
				}
				return dictionary;
			}
		}
		private static void McInstanceFilter(ref List<McInstance> InstanceList, ref Dictionary<McInstanceCardType, List<McInstance>> Target, McInstanceState[] Formula, McInstanceCardType CardType)
		{
			List<McInstance> list = InstanceList.Where([SpecialName] (McInstance v) => Formula.Contains(v.State)).ToList();
			if (list.Any())
			{
				Target.Add(CardType, list);
				InstanceList = InstanceList.Except(list).ToList();
			}
		}
		private static void McInstanceFilter(ref List<McInstance> InstanceList, McInstanceState[] Formula, ref List<McInstance> KeepList)
		{
			KeepList.AddRange(InstanceList.Where([SpecialName] (McInstance v) => Formula.Contains(v.State)));
			if (KeepList.Any())
			{
				InstanceList = InstanceList.Except(KeepList).ToList();
			}
		}
		public static McSkinInfo McSkinSelect()
		{
			string text = Dialogs.SelectFile("选择皮肤文件", multiselect: false, null, new (string[], string)[1] { (new string[4] { "png", "jpg", "jpeg", "webp" }, "皮肤文件") }).FirstOrDefault();
			McSkinInfo result;
			if (string.IsNullOrEmpty(text))
			{
				result = new McSkinInfo
				{
					IsVaild = false
				};
			}
			else
			{
				try
				{
					MyBitmap myBitmap = new MyBitmap(text);
					if (myBitmap.Pic.Width != 64 || (myBitmap.Pic.Height != 32 && myBitmap.Pic.Height != 64))
					{
						ModMain.Hint("皮肤图片大小应为 64x32 像素或 64x64 像素！", ModMain.HintType.Red);
						result = new McSkinInfo
						{
							IsVaild = false
						};
						goto IL_01d8;
					}
					FileInfo info = FileUtils.GetInfo(text);
					if (info.Length > 24576)
					{
						ModMain.Hint("皮肤文件大小需小于 24 KB，而所选文件大小为 " + Conversions.ToString(Math.Round((double)info.Length / 1024.0, 2)) + " KB", ModMain.HintType.Red);
						result = new McSkinInfo
						{
							IsVaild = false
						};
						goto IL_01d8;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "皮肤文件存在错误", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					result = new McSkinInfo
					{
						IsVaild = false
					};
					ProjectData.ClearProjectError();
					goto IL_01d8;
				}
				int num = ModMain.MyMsgBox("此皮肤为 Steve 模型（粗手臂）还是 Alex 模型（细手臂）？", "选择皮肤种类", "Steve 模型", "Alex 模型", "我不知道", IsWarn: false, HighLight: false);
				if (num == 3)
				{
					ModMain.Hint("请在皮肤下载页面确认皮肤种类后再使用此皮肤！");
					result = new McSkinInfo
					{
						IsVaild = false
					};
				}
				else
				{
					result = new McSkinInfo
					{
						IsVaild = true,
						IsSlim = (num == 2),
						LocalFile = text
					};
				}
			}
			goto IL_01d8;
			IL_01d8:
			return result;
		}
		public static string McSkinGetAddress(string UUID, string Type)
		{
			if (Operators.CompareString(UUID, "", TextCompare: false) == 0)
			{
				throw new Exception("UUID 为空。");
			}
			if (UUID.StartsWithF("00000") && Operators.CompareString(Type, "Auth", TextCompare: false) != 0)
			{
				throw new Exception("离线 UUID 无正版皮肤文件：" + UUID);
			}
			string text = ModBase.ReadIni(ModBase.PathTemp + "Cache\\Skin\\Index" + Type + ".ini", UUID);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				return text;
			}
			string text2;
			switch (Type)
			{
			case "Mojang":
			case "Ms":
				text2 = "https://sessionserver.mojang.com/session/minecraft/profile/";
				break;
			case "Nide":
				text2 = "https://auth.mc-user.com:233/" + (((object)McInstanceSelected == null) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", McInstanceSelected)) + "/sessionserver/session/minecraft/profile/";
				break;
			case "Auth":
				text2 = (((object)McInstanceSelected == null) ? Settings.Get<string>("CacheAuthServerServer") : Settings.Get<string>("VersionServerAuthServer", McInstanceSelected)) + "/sessionserver/session/minecraft/profile/";
				break;
			default:
				throw new ArgumentException("皮肤地址种类无效：" + (Type ?? "null"));
			}
			string text3 = ModNet.NetRequestByClientRetry(text2 + UUID, null, null, null, null, "*/*", null, RequireJson: true);
			if (Operators.CompareString(text3, "", TextCompare: false) == 0)
			{
				throw new Exception("皮肤返回值为空，可能是未设置自定义皮肤的用户");
			}
			string s;
			try
			{
				foreach (JToken item in (IEnumerable<JToken>)text3.DeserializeJson()["properties"])
				{
					if (!((string?)item["name"] == "textures"))
					{
						continue;
					}
					s = (string?)item["value"];
					goto end_IL_0178;
				}
				throw new Exception("未从皮肤返回值中找到符合条件的 Property");
				end_IL_0178:;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"无法完成解析的皮肤返回值，可能是未设置自定义皮肤的用户：{text3}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				throw new Exception("皮肤返回值中不包含皮肤数据项，可能是未设置自定义皮肤的用户", ex2);
			}
			text3 = Encoding.UTF8.GetString(Convert.FromBase64String(s));
			JObject jObject = (JObject)text3.Lower().DeserializeJson();
			if (jObject["textures"] == null || jObject["textures"]["skin"] == null || jObject["textures"]["skin"]["url"] == null)
			{
				throw new Exception("用户未设置自定义皮肤");
			}
			string text4 = jObject["textures"]["skin"]["url"].ToString();
			s = (text4.Contains("minecraft.net/") ? text4.Replace("http://", "https://") : text4);
			ModBase.WriteIni(ModBase.PathTemp + "Cache\\Skin\\Index" + Type + ".ini", UUID, s);
			Logger.Info($"UUID {UUID} 对应的皮肤文件为 {s}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			return s;
		}
		public static string McSkinDownload(string Address)
		{
			PathUtils.GetLastPart(Address);
			string text = ModBase.PathTemp + "Cache\\Skin\\" + Conversions.ToString(Address.GetStableHashCode()) + ".png";
			object mcSkinDownloadLock = McSkinDownloadLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(mcSkinDownloadLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(mcSkinDownloadLock, ref lockTaken);
				if (!FileUtils.Exists(text))
				{
					ModNet.NetDownloadByClient(Address, text + ".PCLDownloading");
					FileUtils.Move(text + ".PCLDownloading", text);
					Logger.Info($"皮肤下载成功：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				return text;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(mcSkinDownloadLock);
				}
			}
		}
		public static string McSkinSex(string Uuid)
		{
			if (Uuid.Length != 32)
			{
				return "Steve";
			}
			int num = int.Parse(Conversions.ToString(Uuid[7]), NumberStyles.AllowHexSpecifier);
			int num2 = int.Parse(Conversions.ToString(Uuid[15]), NumberStyles.AllowHexSpecifier);
			int num3 = int.Parse(Conversions.ToString(Uuid[23]), NumberStyles.AllowHexSpecifier);
			int num4 = int.Parse(Conversions.ToString(Uuid[31]), NumberStyles.AllowHexSpecifier);
			return ((num ^ num2 ^ num3 ^ num4) % 2 != 0) ? "Alex" : "Steve";
		}
		public static bool McJsonRuleCheck(JToken RuleToken)
		{
			if (RuleToken == null)
			{
				return true;
			}
			bool result = false;
			foreach (JToken item in (IEnumerable<JToken>)RuleToken)
			{
				bool flag = true;
				if (item["os"] != null)
				{
					if (item["os"]["name"] != null)
					{
						string left = item["os"]["name"].ToString();
						if (Operators.CompareString(left, "unknown", TextCompare: false) != 0)
						{
							if (Operators.CompareString(left, "windows", TextCompare: false) == 0)
							{
								if (item["os"]["version"] != null)
								{
									flag = flag && OSVersion.RegexCheck(item["os"]["version"].ToString());
								}
							}
							else
							{
								flag = false;
							}
						}
					}
					if (item["os"]["arch"] != null)
					{
						flag = flag && Operators.CompareString(item["os"]["arch"].ToString(), "x86", TextCompare: false) != 0 && !item["os"]["arch"].Contains("arm");
					}
				}
				if (!Information.IsNothing(item["features"]))
				{
					flag = flag && Information.IsNothing(item["features"]["is_demo_user"]);
					if (((JObject)item["features"]).Children().Any([SpecialName] (JToken a0) => ((VB_AnonymousDelegate_22<JProperty, bool>)([SpecialName] (JProperty j) => j.Name.Contains("quick_play")))((JProperty)a0)))
					{
						flag = false;
					}
				}
				if (Operators.CompareString(item["action"].ToString(), "allow", TextCompare: false) == 0)
				{
					if (flag)
					{
						result = true;
					}
				}
				else if (flag)
				{
					result = false;
				}
			}
			return result;
		}
		public static List<McLibEntry> McLibListGet(McInstance Instance, bool IncludeMainJar)
		{
			Logger.Info($"获取支持库列表：{Instance.Name}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			List<McLibEntry> list = McLibListGetWithJson(Instance.JsonObject);
			if (IncludeMainJar)
			{
				string text = Instance.JsonObject["jar"]?.ToString();
				McInstance mcInstance2;
				if (Instance.IsHmclFormatJson || text == null)
				{
					McInstance mcInstance = Instance;
					if (!Instance.Version.HasForgelike || Instance.Version.Vanilla.Major < 17)
					{
						while (Operators.CompareString(mcInstance.InheritName, "", TextCompare: false) != 0 && Operators.CompareString(mcInstance.InheritName, mcInstance.Name, TextCompare: false) != 0)
						{
							mcInstance = new McInstance(McFolderSelected + "versions\\" + mcInstance.InheritName + "\\");
						}
					}
					mcInstance2 = new McInstance(mcInstance.PathVersion);
				}
				else
				{
					mcInstance2 = new McInstance(text);
				}
				if (!FileUtils.Exists(mcInstance2.PathVersion + mcInstance2.Name + ".json"))
				{
					mcInstance2 = Instance;
					Logger.Warn($"可能缺少前置版本 {mcInstance2.Name}，找不到对应的 json 文件", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				string url;
				string sHA;
				if (mcInstance2.JsonObject["downloads"] != null && mcInstance2.JsonObject["downloads"]["client"] != null)
				{
					url = (string?)mcInstance2.JsonObject["downloads"]["client"]["url"];
					sHA = (string?)mcInstance2.JsonObject["downloads"]["client"]["sha1"];
				}
				else
				{
					url = null;
					sHA = null;
				}
				list.Add(new McLibEntry
				{
					LocalPath = mcInstance2.PathVersion + mcInstance2.Name + ".jar",
					Size = 0L,
					IsNatives = false,
					Url = url,
					SHA1 = sHA
				});
			}
			return list;
		}
		public static List<McLibEntry> McLibListGetWithJson(JObject JsonObject, bool KeepSameNameDifferentVersionResult = false, string CustomMcFolder = null)
		{
			CustomMcFolder = CustomMcFolder ?? McFolderSelected;
			List<McLibEntry> list = new List<McLibEntry>();
			checked
			{
				foreach (JObject item in ((JArray)JsonObject["libraries"]).Children())
				{
					for (int i = item.Properties().Count() - 1; i >= 0; i += -1)
					{
						if (item.Properties().ElementAtOrDefault(i).Value.Type == JTokenType.Null)
						{
							item.Remove(item.Properties().ElementAtOrDefault(i).Name);
						}
					}
					if (!McJsonRuleCheck(item["rules"]))
					{
						continue;
					}
					string text = (string?)item["url"];
					if (text != null)
					{
						text += McLibGet((string?)item["name"], WithHead: false, IgnoreLiteLoader: true, CustomMcFolder).Replace("\\", "/");
					}
					if (item["natives"] == null)
					{
						string localPath = McLibGet((string?)item["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder);
						try
						{
							if (item["downloads"] != null && item["downloads"]["artifact"] != null)
							{
								list.Add(new McLibEntry
								{
									OriginalName = (string?)item["name"],
									Url = (string?)(item["downloads"]["artifact"]["url"] ?? ((JToken)text)),
									LocalPath = ((item["downloads"]["artifact"]["path"] == null) ? McLibGet((string?)item["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder) : (CustomMcFolder + "libraries\\" + item["downloads"]["artifact"]["path"].ToString().Replace("/", "\\"))),
									Size = (long)Math.Round(ModBase.Val(item["downloads"]["artifact"]["size"].ToString())),
									IsNatives = false,
									SHA1 = item["downloads"]["artifact"]["sha1"]?.ToString()
								});
							}
							else
							{
								list.Add(new McLibEntry
								{
									OriginalName = (string?)item["name"],
									Url = text,
									LocalPath = localPath,
									Size = 0L,
									IsNatives = false,
									SHA1 = null
								});
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, string.Format("处理实际支持库列表失败（无 Natives，{0}）", item["name"] ?? ((JToken)"Nothing")), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							list.Add(new McLibEntry
							{
								OriginalName = (string?)item["name"],
								Url = text,
								LocalPath = localPath,
								Size = 0L,
								IsNatives = false,
								SHA1 = null
							});
							ProjectData.ClearProjectError();
						}
					}
					else
					{
						if (item["natives"]["windows"] == null)
						{
							continue;
						}
						string text2 = (string?)item["natives"]["windows"];
						try
						{
							if (item["downloads"] != null && item["downloads"]["classifiers"] != null && item["downloads"]["classifiers"][text2] != null)
							{
								JToken jToken = item["downloads"]["classifiers"][text2];
								list.Add(new McLibEntry
								{
									OriginalName = (string?)item["name"],
									Url = (string?)(jToken["url"] ?? ((JToken)text)),
									LocalPath = ((jToken["path"] == null) ? McLibGet((string?)item["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", "-" + text2 + ".jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32") : string.Format("{0}libraries\\{1}", CustomMcFolder, jToken["path"].ToString().Replace("/", "\\"))),
									Size = (long)Math.Round(ModBase.Val(jToken["size"].ToString())),
									IsNatives = true,
									SHA1 = jToken["sha1"].ToString()
								});
							}
							else
							{
								list.Add(new McLibEntry
								{
									OriginalName = (string?)item["name"],
									Url = text,
									Size = 0L,
									IsNatives = true,
									SHA1 = null,
									LocalPath = McLibGet((string?)item["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", $"-{text2}.jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32")
								});
							}
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Warn(ex4, string.Format("处理实际支持库列表失败（有 Natives，{0}）", item["name"] ?? ((JToken)"Nothing")), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							list.Add(new McLibEntry
							{
								OriginalName = (string?)item["name"],
								Url = text,
								Size = 0L,
								IsNatives = true,
								SHA1 = null,
								LocalPath = McLibGet((string?)item["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", $"-{text2}.jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32")
							});
							ProjectData.ClearProjectError();
						}
					}
				}
				Dictionary<string, McLibEntry> dictionary = new Dictionary<string, McLibEntry>();
				VB_AnonymousDelegate_23<McLibEntry, string> obj = [SpecialName] (McLibEntry Token) => PathUtils.GetLastPart(PathUtils.RemoveLastPart(Token.LocalPath));
				int num = list.Count - 1;
				for (int num2 = 0; num2 <= num; num2++)
				{
					string text3 = list[num2].Name + list[num2].IsNatives;
					if (dictionary.ContainsKey(text3))
					{
						string text4 = obj(list[num2]);
						string text5 = obj(dictionary[text3]);
						if (Operators.CompareString(text4, text5, TextCompare: false) != 0 && KeepSameNameDifferentVersionResult)
						{
							Logger.Info($"发现疑似重复的支持库：{list[num2]} ({text4}) 与 {dictionary[text3]} ({text5})", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							dictionary.Add(text3 + Conversions.ToString(ModBase.GetUuid()), list[num2]);
							continue;
						}
						Logger.Info($"发现重复的支持库：{list[num2]} ({text4}) 与 {dictionary[text3]} ({text5})，已忽略其中之一", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						if (CompareVersionGE(text4, text5))
						{
							dictionary[text3] = list[num2];
						}
					}
					else
					{
						dictionary.Add(text3, list[num2]);
					}
				}
				return dictionary.Values.ToList();
			}
		}
		public static List<ModNet.NetFile> McLibNetFilesFromInstance(McInstance Instance)
		{
			if (!Instance.IsLoaded)
			{
				Instance.Load();
			}
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			try
			{
				ModNet.NetFile netFile = ModDownload.DlClientJarGet(Instance, ReturnNothingOnFileUseable: true);
				if (netFile != null)
				{
					list.Add(netFile);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "版本缺失主 jar 文件所必须的信息", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			list.AddRange(McLibNetFilesFromTokens(McLibListGet(Instance, IncludeMainJar: false)));
			if (Settings.Get<int>("VersionServerLogin", Instance) == 3)
			{
				string text = ModBase.PathPure.Value + "nide8auth.jar";
				try
				{
					Logger.Info("开始获取统一通行证下载信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					JObject jObject = (JObject)ModNet.NetRequestByClientRetry("https://auth.mc-user.com:233/" + Settings.Get<string>("VersionServerNide", Instance), null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
					list.Add(new ModNet.NetFile(new string[1] { "https://login.mc-user.com:233/index/jar" }, text, new FileChecker
					{
						Hash = jObject["jarHash"].ToString()
					}));
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					if (!FileUtils.Exists(text))
					{
						throw new Exception("获取统一通行证下载信息失败", ex4);
					}
					Logger.Warn(ex4, "获取统一通行证下载信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
			}
			if (Settings.Get<int>("VersionServerLogin", Instance) == 4)
			{
				string text2 = ModBase.PathPure.Value + "authlib-injector.jar";
				try
				{
					Logger.Info("开始获取 Authlib-Injector 下载信息", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					JObject jObject2 = (JObject)ModNet.NetRequestByClientRetry("https://authlib-injector.yushi.moe/artifact/latest.json", null, "https://bmclapi2.bangbang93.com/mirrors/authlib-injector/artifact/latest.json", null, null, "*/*", null, RequireJson: true).DeserializeJson();
					string text3 = jObject2["download_url"].ToString().Replace("bmclapi2.bangbang93.com/mirrors/authlib-injector", "authlib-injector.yushi.moe");
					list.Add(new ModNet.NetFile(new string[2]
					{
						text3,
						text3.Replace("authlib-injector.yushi.moe", "bmclapi2.bangbang93.com/mirrors/authlib-injector")
					}, text2, new FileChecker
					{
						Hash = jObject2["checksums"]["sha256"].ToString()
					}));
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					if (!FileUtils.Exists(text2))
					{
						throw new Exception("获取 Authlib-Injector 下载信息失败", ex6);
					}
					Logger.Warn(ex6, "获取 Authlib-Injector 下载信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
			}
			if (Conversions.ToBoolean(ShouldIgnoreFileCheck(Instance)))
			{
				Logger.Warn("设置要求尽量忽略文件检查，这可能会保留有误的文件", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				list = list.Where([SpecialName] (ModNet.NetFile f) =>
				{
					if (FileUtils.Exists(f.LocalPath))
					{
						Logger.Warn($"跳过下载的支持库文件：{f.LocalPath}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						return false;
					}
					return true;
				}).ToList();
			}
			return list;
		}
		public static List<ModNet.NetFile> McLibNetFilesFromTokens(List<McLibEntry> Libs, string CustomMcFolder = null)
		{
			CustomMcFolder = CustomMcFolder ?? McFolderSelected;
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			foreach (McLibEntry Lib in Libs)
			{
				FileChecker checker = ((!Lib.Name.ContainsIgnoreCase("labymod")) ? new FileChecker
				{
					ActualSize = ((Lib.Size == 0L) ? (-1) : Lib.Size),
					Hash = Lib.SHA1
				} : new FileChecker());
				List<string> list2 = new List<string>();
				if (Lib.Url == null && Operators.CompareString(Lib.Name, "net.minecraftforge:forge:universal", TextCompare: false) == 0)
				{
					Lib.Url = "https://maven.minecraftforge.net" + Lib.LocalPath.Replace(CustomMcFolder + "libraries", "").Replace("\\", "/");
				}
				if (Lib.Url != null)
				{
					list2.Add(Lib.Url);
					if (Lib.Url.Contains("launcher.mojang.com/v1/objects") || Lib.Url.Contains("client.txt") || Lib.Url.Contains(".tsrg"))
					{
						list2.AddRange(ModDownload.DlSourceLauncherOrMetaGet(Lib.Url));
					}
					if (Lib.Url.Contains("maven"))
					{
						string item = Lib.Url.Replace(Lib.Url.Substring(0, Lib.Url.IndexOfF("maven")), "https://bmclapi2.bangbang93.com/").Replace("maven.fabricmc.net", "maven").Replace("maven.minecraftforge.net", "maven")
							.Replace("maven.neoforged.net/releases", "maven");
						if (ModDownload.DlSourcePreferMojang)
						{
							list2.Add(item);
						}
						else
						{
							list2.Insert(0, item);
						}
					}
				}
				if (Lib.LocalPath.Contains("transformer-discovery-service"))
				{
					if (!FileUtils.Exists(Lib.LocalPath))
					{
						ModBase.ExtractResources(Lib.LocalPath, "Transformer");
					}
					Logger.Warn("已自动释放 Transformer Discovery Service", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					continue;
				}
				if (Lib.LocalPath.Contains("optifine\\OptiFine"))
				{
					string text = Lib.LocalPath.Replace(CustomMcFolder + "libraries\\optifine\\OptiFine\\", "").Split("_")[0] + "/" + PathUtils.GetLastPart(Lib.LocalPath).Replace("-", "_");
					text = "/maven/com/optifine/" + text;
					if (text.Contains("_pre"))
					{
						text = text.Replace("com/optifine/", "com/optifine/preview_");
					}
					list2.Add("https://bmclapi2.bangbang93.com" + text);
				}
				else if (list2.Count <= 2)
				{
					list2.AddRange(ModDownload.DlSourceLibraryGet("https://libraries.minecraft.net" + Lib.LocalPath.Replace(CustomMcFolder + "libraries", "").Replace("\\", "/")));
				}
				list.Add(new ModNet.NetFile(list2.Distinct(), Lib.LocalPath, checker));
			}
			return list.DistinctBy([SpecialName] (ModNet.NetFile a) => a.LocalPath).ToList();
		}
		public static string McLibGet(string Original, bool WithHead = true, bool IgnoreLiteLoader = false, string CustomMcFolder = null)
		{
			CustomMcFolder = CustomMcFolder ?? McFolderSelected;
			string[] array = Original.Split(":");
			return (WithHead ? (CustomMcFolder + "libraries\\") : "") + string.Format("{0}\\{1}\\{2}\\{3}.jar", array[0].Replace(".", "\\"), array[1], array[2], array.Skip(1).Join("-"));
		}
		public static object ShouldIgnoreFileCheck(McInstance Instance)
		{
			return Settings.Get<bool>("VersionAdvanceAssetsV2", Instance) || Settings.Get<int>("VersionAdvanceAssets", Instance) == 2;
		}
		public static JToken McAssetsGetIndex(McInstance Instance, bool ReturnLegacyOnError = false, bool CheckURLEmpty = false)
		{
			try
			{
				while (true)
				{
					JToken jToken = Instance.JsonObject["assetIndex"];
					if (jToken != null && jToken["id"] != null)
					{
						return jToken;
					}
					if (Instance.JsonObject["assets"] != null)
					{
						Instance.JsonObject["assets"].ToString();
					}
					if (CheckURLEmpty && jToken["url"] != null)
					{
						return jToken;
					}
					if (Operators.CompareString(Instance.InheritName, "", TextCompare: false) != 0)
					{
						Instance = new McInstance(McFolderSelected + "versions\\" + Instance.InheritName);
						continue;
					}
					break;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			if (ReturnLegacyOnError)
			{
				Logger.Info("无法获取资源文件索引下载地址，使用默认的 legacy 下载地址", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				return "{\r\n                \"id\": \"legacy\",\r\n                \"sha1\": \"c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729\",\r\n                \"size\": 134284,\r\n                \"url\": \"https://launchermeta.mojang.com/mc-staging/assets/legacy/c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729/legacy.json\",\r\n                \"totalSize\": 111220701\r\n            }".DeserializeJson();
			}
			throw new Exception("该版本不存在资源文件索引信息");
		}
		public static string McAssetsGetIndexName(McInstance Instance)
		{
			try
			{
				while (true)
				{
					if (Instance.JsonObject["assetIndex"] != null && Instance.JsonObject["assetIndex"]["id"] != null)
					{
						return Instance.JsonObject["assetIndex"]["id"].ToString();
					}
					if (Instance.JsonObject["assets"] != null)
					{
						return Instance.JsonObject["assets"].ToString();
					}
					if (Operators.CompareString(Instance.InheritName, "", TextCompare: false) != 0)
					{
						Instance = new McInstance(McFolderSelected + "versions\\" + Instance.InheritName);
						continue;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "获取资源文件索引名失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			return "legacy";
		}
		private static List<McAssetsToken> McAssetsListGet(McInstance Instance)
		{
			string text = McAssetsGetIndexName(Instance);
			try
			{
				if (!FileUtils.Exists($"{McFolderSelected}assets\\indexes\\{text}.json"))
				{
					throw new FileNotFoundException("未找到 Asset Index", McFolderSelected + "assets\\indexes\\" + text + ".json");
				}
				List<McAssetsToken> list = new List<McAssetsToken>();
				JObject jObject = (JObject)FileUtils.ReadAsJson($"{McFolderSelected}assets\\indexes\\{text}.json");
				foreach (JProperty item in jObject["objects"].Children())
				{
					string text2;
					if (jObject["map_to_resources"] != null && jObject["map_to_resources"].ToObject<bool>())
					{
						text2 = $"{Instance.PathIndie}resources\\{item.Name}";
					}
					else
					{
						text2 = ((jObject["virtual"] == null || !jObject["virtual"].ToObject<bool>()) ? string.Format("{0}assets\\objects\\{1}\\{2}", McFolderSelected, Strings.Left(item.Value["hash"].ToString(), 2), item.Value["hash"]) : $"{McFolderSelected}assets\\virtual\\legacy\\{item.Name}");
					}
					list.Add(new McAssetsToken
					{
						LocalPath = text2.Replace("/", "\\"),
						SourcePath = item.Name,
						Hash = item.Value["hash"].ToString(),
						Size = Conversions.ToLong(item.Value["size"].ToString())
					});
				}
				return list;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"获取资源文件列表失败：{text}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				throw;
			}
		}
		public static List<ModNet.NetFile> McAssetsFixList(McInstance Instance, bool CheckHash, [Optional][DefaultParameterValue(null)] ref ModLoader.LoaderBase ProgressFeed)
		{
			if (CheckHash)
			{
				return (from Token in McAssetsListGet(Instance)
					select new ModNet.NetFile(ModDownload.DlSourceAssetsGet($"https://resources.download.minecraft.net/{Strings.Left(Token.Hash, 2)}/{Token.Hash}"), Token.LocalPath, new FileChecker
					{
						ActualSize = ((Token.Size == 0L) ? (-1) : Token.Size),
						Hash = Token.Hash
					})).ToList();
			}
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			checked
			{
				try
				{
					List<McAssetsToken> list2 = McAssetsListGet(Instance);
					if (ProgressFeed != null)
					{
						ProgressFeed.Progress = 0.04;
					}
					int num = list2.Count - 1;
					for (int num2 = 0; num2 <= num; num2++)
					{
						McAssetsToken mcAssetsToken = list2[num2];
						if (ProgressFeed != null)
						{
							ProgressFeed.Progress = 0.05 + 0.94 * (double)num2 / (double)list2.Count;
						}
						FileInfo info = FileUtils.GetInfo(mcAssetsToken.LocalPath);
						if (!info.Exists || (mcAssetsToken.Size != 0L && mcAssetsToken.Size != info.Length))
						{
							list.Add(new ModNet.NetFile(ModDownload.DlSourceAssetsGet($"https://resources.download.minecraft.net/{Strings.Left(mcAssetsToken.Hash, 2)}/{mcAssetsToken.Hash}"), mcAssetsToken.LocalPath, new FileChecker
							{
								ActualSize = ((mcAssetsToken.Size == 0L) ? (-1) : mcAssetsToken.Size),
								Hash = mcAssetsToken.Hash
							}));
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "获取版本缺失的资源文件下载列表失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
				if (ProgressFeed != null)
				{
					ProgressFeed.Progress = 0.99;
				}
				return list;
			}
		}
		public static void McDownloadClientUpdateHint(string VersionName, JObject Json)
		{
			_Closure___49_1 arg = default;
			_Closure___49_1 CS_8__locals11 = new _Closure___49_1(arg);
			CS_8__locals11._VB_Local_VersionName = VersionName;
			try
			{
				_Closure___49_0 arg2 = default;
				_Closure___49_0 CS_8__locals10 = new _Closure___49_0(arg2);
				CS_8__locals10._VB_Local_Version = null;
				foreach (JToken item in (IEnumerable<JToken>)Json["versions"])
				{
					if (item["id"] != null && Operators.CompareString(item["id"].ToString(), CS_8__locals11._VB_Local_VersionName, TextCompare: false) == 0)
					{
						CS_8__locals10._VB_Local_Version = item;
						break;
					}
				}
				if (CS_8__locals10._VB_Local_Version == null)
				{
					return;
				}
				DateTime dateTime = (DateTime)CS_8__locals10._VB_Local_Version["releaseTime"];
				if (ModMain.MyMsgBox(string.Format("新版本：{0}{1}", CS_8__locals11._VB_Local_VersionName, "\r\n") + (((DateTime.Now - dateTime).TotalDays > 1.0) ? ("更新时间：" + dateTime.ToString()) : ("更新于：" + StringUtils.FormatTimeSpan(dateTime - DateTime.Now, isShort: false))), "Minecraft 更新提示", "确定", "下载", ((DateTime.Now - dateTime).TotalHours > 3.0) ? "更新日志" : "", IsWarn: false, HighLight: true, ForceWait: false, null, null, [SpecialName] () =>
				{
					ModDownloadLib.McUpdateLogShow(CS_8__locals10._VB_Local_Version);
				}) == 2)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						PageDownloadInstall.VersionWaitingSelect = CS_8__locals11._VB_Local_VersionName;
						ModMain.FrmMain.PageChange(FormMain.PageType.Download);
					});
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, string.Format("Minecraft 更新提示发送失败（{0}）", CS_8__locals11._VB_Local_VersionName ?? "Nothing"), LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static bool CompareVersionGE(string Left, string Right)
		{
			return CompareVersion(Left, Right) >= 0;
		}
		public static int CompareVersion(string Left, string Right)
		{
			if (Operators.CompareString(Left, "未知版本", TextCompare: false) == 0 || Operators.CompareString(Right, "未知版本", TextCompare: false) == 0)
			{
				if (Operators.CompareString(Left, "未知版本", TextCompare: false) == 0 && Operators.CompareString(Right, "未知版本", TextCompare: false) != 0)
				{
					return 1;
				}
				if (Operators.CompareString(Left, "未知版本", TextCompare: false) == 0 && Operators.CompareString(Right, "未知版本", TextCompare: false) == 0)
				{
					return 0;
				}
				if (Operators.CompareString(Left, "未知版本", TextCompare: false) != 0 && Operators.CompareString(Right, "未知版本", TextCompare: false) == 0)
				{
					return -1;
				}
			}
			Left = Left.Lower();
			Right = Right.Lower();
			List<string> list = Left.Replace("快照", "snapshot").Replace("预览版", "pre").RegexSearch("[a-z]+|[0-9]+")
				.ToList();
			List<string> list2 = Right.Replace("快照", "snapshot").Replace("预览版", "pre").RegexSearch("[a-z]+|[0-9]+")
				.ToList();
			int num = 0;
			checked
			{
				while (true)
				{
					if (list.Count - 1 < num && list2.Count - 1 < num)
					{
						if (Operators.CompareString(Left, Right, TextCompare: false) > 0)
						{
							return 1;
						}
						if (Operators.CompareString(Left, Right, TextCompare: false) < 0)
						{
							return -1;
						}
						return 0;
					}
					string text = Conversions.ToString((list.Count - 1 < num) ? ((object)0) : list[num]);
					string text2 = Conversions.ToString((list2.Count - 1 < num) ? ((object)0) : list2[num]);
					if (Operators.CompareString(text, text2, TextCompare: false) != 0)
					{
						if (Operators.CompareString(text, "rc", TextCompare: false) == 0)
						{
							text = Conversions.ToString(-1);
						}
						if (Operators.CompareString(text, "pre", TextCompare: false) == 0)
						{
							text = Conversions.ToString(-2);
						}
						if (Operators.CompareString(text, "snapshot", TextCompare: false) == 0)
						{
							text = Conversions.ToString(-3);
						}
						if (Operators.CompareString(text, "experimental", TextCompare: false) == 0)
						{
							text = Conversions.ToString(-4);
						}
						double num2 = ModBase.Val(text);
						if (Operators.CompareString(text2, "rc", TextCompare: false) == 0)
						{
							text2 = Conversions.ToString(-1);
						}
						if (Operators.CompareString(text2, "pre", TextCompare: false) == 0)
						{
							text2 = Conversions.ToString(-2);
						}
						if (Operators.CompareString(text2, "snapshot", TextCompare: false) == 0)
						{
							text2 = Conversions.ToString(-3);
						}
						if (Operators.CompareString(text2, "experimental", TextCompare: false) == 0)
						{
							text2 = Conversions.ToString(-4);
						}
						double num3 = ModBase.Val(text2);
						if (num2 == 0.0 && num3 == 0.0)
						{
							if (Operators.CompareString(text, text2, TextCompare: false) > 0)
							{
								return 1;
							}
							if (Operators.CompareString(text, text2, TextCompare: false) < 0)
							{
								return -1;
							}
						}
						else
						{
							if (num2 > num3)
							{
								return 1;
							}
							if (num2 < num3)
							{
								break;
							}
						}
					}
					num++;
				}
				return -1;
			}
		}
		public static string FilterAccessToken(string Raw, char FilterChar)
		{
			if (Raw.Contains("accessToken "))
			{
				foreach (string item in Raw.RegexSearch("(?<=accessToken ([^ ]{5}))[^ ]+(?=[^ ]{5})"))
				{
					Raw = Raw.Replace(item, new string(FilterChar, item.Count()));
				}
			}
			string accessToken = ModLaunch.McLoginLoader.Output.AccessToken;
			if (accessToken != null && accessToken.Length >= 10 && Raw.ContainsIgnoreCase(accessToken) && Operators.CompareString(ModLaunch.McLoginLoader.Output.Uuid, ModLaunch.McLoginLoader.Output.AccessToken, TextCompare: false) != 0)
			{
				Raw = Raw.Replace(accessToken, Strings.Left(accessToken, 5) + new string(FilterChar, checked(accessToken.Length - 10)) + Strings.Right(accessToken, 5));
			}
			return Raw;
		}
		public static string FilterUserName(string Raw, char FilterChar)
		{
			if (Raw.Contains(":\\Users\\"))
			{
				foreach (string item in Raw.RegexSearch("(?<=:\\\\Users\\\\)[^\\\\]+"))
				{
					Raw = Raw.Replace("\\Users\\" + item, "\\Users\\" + new string(FilterChar, item.Count()));
				}
			}
			if (Raw.Contains(":/Users/"))
			{
				foreach (string item2 in Raw.RegexSearch("(?<=:/Users/)[^/]+"))
				{
					Raw = Raw.Replace("/Users/" + item2, "/Users/" + new string(FilterChar, item2.Count()));
				}
			}
			return Raw;
		}
	}
}
