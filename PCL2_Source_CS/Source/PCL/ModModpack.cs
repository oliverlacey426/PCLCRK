// PCL

namespace PCL
{
	[StandardModule]
	public static class ModModpack
	{
		[CompilerGenerated]
		internal sealed class _Closure___10_0
		{
			public string _VB_Local_TargetFolder;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public _Closure___10_0(_Closure___10_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_TargetFolder = arg0._VB_Local_TargetFolder;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_TargetFolder, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.9));
				ModBase.OpenExplorer(_VB_Local_TargetFolder);
				Thread.Sleep(400);
				string text = null;
				foreach (string item in DirectoryUtils.EnumerateFiles(_VB_Local_TargetFolder, includeSubDirectories: false, "*.exe"))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(item);
					Logger.Info($"文件 {item} 的产品名标识为 {versionInfo.ProductName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					if (Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher", TextCompare: false) == 0)
					{
						text = item;
						Logger.Info($"发现整合包附带的 PCL 启动器：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					else if ((versionInfo.ProductName.ContainsIgnoreCase("Launcher") || versionInfo.ProductName.Contains("启动")) && Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher Admin Manager", TextCompare: false) != 0 && text == null)
					{
						text = item;
						Logger.Info($"发现整合包附带的疑似第三方启动器：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
				}
				Task.Progress = 0.95;
				if (text != null)
				{
					Logger.Info($"找到压缩包中附带的启动器：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					if (ModMain.MyMsgBox(string.Format("整合包里似乎自带了启动器，是否换用它继续安装？{0}即将打开：{1}", "\r\n", text), "换用整合包启动器？", "换用", "不换用") == 1)
					{
						ModBase.StartProcess(text, "--wait");
						Logger.Info("为换用整合包中的启动器启动，强制结束程序", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						ModMain.FrmMain.EndProgram(SendWarning: false);
						return;
					}
				}
				else
				{
					Logger.Info("未找到压缩包中附带的启动器", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string lastPart = PathUtils.GetLastPart(_VB_Local_TargetFolder);
				DirectoryUtils.Create(_VB_Local_TargetFolder + ".minecraft\\");
				PageSelectLeft.AddFolder(_VB_Local_TargetFolder + ".minecraft\\" + _VB_Local_ArchiveBaseFolder.Replace("/", "\\").TrimStart('\\'), lastPart, ShowHint: false);
				string text2 = DirectoryUtils.EnumerateFiles(_VB_Local_TargetFolder, includeSubDirectories: true, "modpack.*").First();
				Logger.Info($"调用 modpack 文件继续安装：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ModpackInstall(text2);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___11_0
		{
			public string _VB_Local_TargetFolder;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public _Closure___11_0(_Closure___11_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_TargetFolder = arg0._VB_Local_TargetFolder;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_TargetFolder, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.95));
				PageSelectLeft.AddFolder(_VB_Local_TargetFolder + _VB_Local_ArchiveBaseFolder, PathUtils.GetLastPart(_VB_Local_TargetFolder), ShowHint: false);
				Thread.Sleep(400);
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
				});
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___5_0
		{
			public string _VB_Local_InstallTemp;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public string _VB_Local_OverrideHome;
			public string _VB_Local_InstanceName;
			public List<int> _VB_Local_ModList;
			public JObject _VB_Local_Json;
			public List<int> _VB_Local_ModOptionalList;
			public ModMinecraft.McVersion _VB_Local_TargetVersion;
			public string _VB_Local_Logo;
			public string _VB_Local_LoaderName;
			public Func<int, (int ProjectId, int FileId)> _I3;
			public _Closure___5_0(_Closure___5_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_InstallTemp = arg0._VB_Local_InstallTemp;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
					_VB_Local_OverrideHome = arg0._VB_Local_OverrideHome;
					_VB_Local_InstanceName = arg0._VB_Local_InstanceName;
					_VB_Local_ModList = arg0._VB_Local_ModList;
					_VB_Local_Json = arg0._VB_Local_Json;
					_VB_Local_ModOptionalList = arg0._VB_Local_ModOptionalList;
					_VB_Local_TargetVersion = arg0._VB_Local_TargetVersion;
					_VB_Local_Logo = arg0._VB_Local_Logo;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_InstallTemp, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
				CopyOverrideDirectory(_VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + ((Operators.CompareString(_VB_Local_OverrideHome, ".", TextCompare: false) == 0 || Operators.CompareString(_VB_Local_OverrideHome, "./", TextCompare: false) == 0) ? "" : _VB_Local_OverrideHome), $"{ModMinecraft.McFolderSelected}versions\\{_VB_Local_InstanceName}", Task);
			}
			[SpecialName]
			internal void _Lambda___1(ModLoader.LoaderTask<int, JArray> Task)
			{
				Task.Output = (JArray)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, "{\"fileIds\": [" + _VB_Local_ModList.Join(",") + "]}", "application/json")["data"];
				if (_VB_Local_ModList.Count == Task.Output.Count)
				{
					return;
				}
				List<int> list = _VB_Local_ModList.Except(Task.Output.Select([SpecialName] (JToken j) => j["id"].ToObject<int>())).ToList();
				Logger.Info(string.Format("缺失的 fileIds：{0}", list.Join("、")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				List<(int, int)> list2 = list.Select([SpecialName] (int id) =>
				{
					_Closure___5_1 arg = default;
					_Closure___5_1 CS_8__locals3 = new _Closure___5_1(arg);
					CS_8__locals3._VB_Local_id = id;
					return (((JObject)_VB_Local_Json["files"].AsEnumerable().First([SpecialName] (JToken f) => f["fileID"].ToObject<int>() == CS_8__locals3._VB_Local_id))["projectID"].ToObject<int>(), CS_8__locals3._VB_Local_id);
				}).ToList();
				Logger.Info(string.Format("缺失项：{0}", list2.Join("、")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				List<ResourceProject> source = new List<ResourceProject>();
				try
				{
					source = ResourceProject.FromProjectIds(list2.Select([SpecialName] ((int ProjectId, int FileId) e) => e.ProjectId.ToString()), ResourcePlatforms.CurseForge).ToList();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "获取缺失的工程信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ProjectData.ClearProjectError();
				}
				List<string> list3 = new List<string>();
				using (List<(int, int)>.Enumerator enumerator = list2.GetEnumerator())
				{
					_Closure___5_2 obj = default;
					while (enumerator.MoveNext())
					{
						obj = new _Closure___5_2(obj);
						obj._VB_Local_Missing = enumerator.Current;
						ResourceProject resourceProject = source.FirstOrDefault(obj._Lambda___6);
						if ((object)resourceProject == null)
						{
							list3.Add($"未知 CurseForge 项目 (项目 ID {obj._VB_Local_Missing.ProjectId}，文件 ID {obj._VB_Local_Missing.FileId})");
						}
						else
						{
							list3.Add($"{resourceProject.TranslatedName} (文件 ID {obj._VB_Local_Missing.FileId})");
						}
					}
				}
				if (ModMain.MyMsgBox(string.Format("未找到该整合包所需的以下内容，它们可能已被原作者删除：{0}- {1}{2}{3}是否继续安装？缺失这些内容可能导致整合包出现异常！", "\r\n", list3.Join("\r\n- "), "\r\n", "\r\n"), "整合包内容缺失", "继续", "取消", "", IsWarn: true) == 2)
				{
					Task.Cancel();
				}
			}
			[SpecialName]
			internal (int, int) _Lambda___3(int id)
			{
				_Closure___5_1 arg = default;
				_Closure___5_1 CS_8__locals3 = new _Closure___5_1(arg);
				CS_8__locals3._VB_Local_id = id;
				return (((JObject)_VB_Local_Json["files"].AsEnumerable().First([SpecialName] (JToken f) => f["fileID"].ToObject<int>() == CS_8__locals3._VB_Local_id))["projectID"].ToObject<int>(), CS_8__locals3._VB_Local_id);
			}
			[SpecialName]
			internal void _Lambda___7(ModLoader.LoaderTask<JArray, List<ModNet.NetFile>> Task)
			{
				Dictionary<int, ModNet.NetFile> dictionary = new Dictionary<int, ModNet.NetFile>();
				foreach (JToken item in Task.Input)
				{
					int num = item["id"].ToObject<int>();
					if (dictionary.ContainsKey(num) || (_VB_Local_ModOptionalList.Contains(num) && ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + item["displayName"].ToString() + "？", "下载可选文件", "是", "否") == 2))
					{
						continue;
					}
					string arg;
					ResourceTypes expectedResourceType;
					if (item["modules"].Any())
					{
						List<string> list = ((JArray)item["modules"]).Select([SpecialName] (JToken l) => l["name"].ToString()).ToList();
						if (list.Contains("META-INF") || list.Contains("mcmod.info") || list.Contains("fabric.mod.json"))
						{
							goto IL_0136;
						}
						if (item != null)
						{
							JToken? jToken = item["FileName"];
							if (jToken != null && jToken.ToString().EndsWithF(".jar", ignoreCase: true))
							{
								goto IL_0136;
							}
						}
						if (list.Contains("pack.mcmeta"))
						{
							arg = "resourcepacks";
							expectedResourceType = ResourceTypes.ResourcePack;
						}
						else if (list.Contains("shaders"))
						{
							arg = "shaderpacks";
							expectedResourceType = ResourceTypes.Shader;
						}
						else
						{
							arg = "saves";
							expectedResourceType = ResourceTypes.Map;
						}
					}
					else
					{
						arg = "mods";
						expectedResourceType = ResourceTypes.Mod;
					}
					goto IL_018d;
					IL_0136:
					arg = "mods";
					expectedResourceType = ResourceTypes.Mod;
					goto IL_018d;
					IL_018d:
					ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item, expectedResourceType);
					if (resourceVersion.DownloadAvailable)
					{
						dictionary.Add(num, resourceVersion.ToNetFile($"{ModMinecraft.McFolderSelected}versions\\{_VB_Local_InstanceName}\\{arg}\\", ResourceVersion.DownloadReason.ModPack, _VB_Local_TargetVersion.VanillaName, _VB_Local_TargetVersion.ModLoaders));
						Task.Progress += 1.0 / (double)checked(1 + _VB_Local_ModList.Count);
					}
				}
				Task.Output = dictionary.Values.ToList();
			}
			[SpecialName]
			internal void _Lambda___12(ModLoader.LoaderTask<string, string> Task)
			{
				string text = $"{ModMinecraft.McFolderSelected}versions\\{_VB_Local_InstanceName}\\";
				if (_VB_Local_Logo != null && FileUtils.Exists(_VB_Local_Logo))
				{
					FileUtils.Copy(_VB_Local_Logo, text + "PCL\\Logo.png");
					ModBase.WriteIni(text + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(text + "PCL\\Setup.ini", "LogoCustom", "True");
					Logger.Info($"已设置整合包 Logo：{_VB_Local_Logo}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string text2 = $"{text}saves\\";
				foreach (string item in DirectoryUtils.EnumerateFiles(text2, includeSubDirectories: false, "*.zip"))
				{
					FileUtils.ExtractToDirectory(item, text2, Task.CreateCancellationToken());
					FileUtils.Delete(item);
				}
				string[] array = new string[2]
				{
					text + "原始整合包.zip",
					text + "原始整合包.mrpack"
				};
				foreach (string text3 in array)
				{
					if (FileUtils.Exists(text3))
					{
						Logger.Info($"删除原始整合包文件：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						FileUtils.Delete(text3);
					}
				}
				if (FileUtils.Exists(_VB_Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(_VB_Local_FileAddress), "modpack", TextCompare: false) == 0)
				{
					Logger.Info($"删除安装整合包文件：{_VB_Local_FileAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(_VB_Local_FileAddress);
				}
			}
			[SpecialName]
			internal bool _Lambda___13(ModLoader.LoaderBase l)
			{
				return Operators.CompareString(l.Name, _VB_Local_LoaderName, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___5_1
		{
			public int _VB_Local_id;
			public _Closure___5_1(_Closure___5_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_id = arg0._VB_Local_id;
				}
			}
			[SpecialName]
			internal bool _Lambda___4(JToken f)
			{
				return f["fileID"].ToObject<int>() == _VB_Local_id;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___5_2
		{
			public (int ProjectId, int FileId) _VB_Local_Missing;
			public _Closure___5_2(_Closure___5_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Missing = arg0._VB_Local_Missing;
				}
			}
			[SpecialName]
			internal bool _Lambda___6(ResourceProject p)
			{
				return Conversions.ToDouble(p.Id) == (double)_VB_Local_Missing.ProjectId;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_0
		{
			public string _VB_Local_InstallTemp;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public string _VB_Local_InstanceName;
			public string _VB_Local_Logo;
			public string _VB_Local_LoaderName;
			public _Closure___6_0(_Closure___6_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_InstallTemp = arg0._VB_Local_InstallTemp;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
					_VB_Local_InstanceName = arg0._VB_Local_InstanceName;
					_VB_Local_Logo = arg0._VB_Local_Logo;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_InstallTemp, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.5));
				CopyOverrideDirectory(_VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + "overrides", ModMinecraft.McFolderSelected + "versions\\" + _VB_Local_InstanceName, Task);
				CopyOverrideDirectory(_VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + "client-overrides", ModMinecraft.McFolderSelected + "versions\\" + _VB_Local_InstanceName, Task);
			}
			[SpecialName]
			internal void _Lambda___5(ModLoader.LoaderTask<string, string> Task)
			{
				string text = $"{ModMinecraft.McFolderSelected}versions\\{_VB_Local_InstanceName}\\";
				if (_VB_Local_Logo != null && FileUtils.Exists(_VB_Local_Logo))
				{
					FileUtils.Copy(_VB_Local_Logo, text + "PCL\\Logo.png");
					ModBase.WriteIni(text + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(text + "PCL\\Setup.ini", "LogoCustom", "True");
					Logger.Info($"已设置整合包 Logo：{_VB_Local_Logo}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string[] array = new string[2]
				{
					text + "原始整合包.zip",
					text + "原始整合包.mrpack"
				};
				foreach (string text2 in array)
				{
					if (FileUtils.Exists(text2))
					{
						Logger.Info($"删除原始整合包文件：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						FileUtils.Delete(text2);
					}
				}
				if (FileUtils.Exists(_VB_Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(_VB_Local_FileAddress), "modpack", TextCompare: false) == 0)
				{
					Logger.Info($"删除安装整合包文件：{_VB_Local_FileAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(_VB_Local_FileAddress);
				}
			}
			[SpecialName]
			internal bool _Lambda___6(ModLoader.LoaderBase l)
			{
				return Operators.CompareString(l.Name, _VB_Local_LoaderName, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___8_0
		{
			public string _VB_Local_InstallTemp;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public string _VB_Local_InstanceName;
			public string _VB_Local_SetupFile;
			public string _VB_Local_LoaderName;
			public _Closure___8_0(_Closure___8_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_InstallTemp = arg0._VB_Local_InstallTemp;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
					_VB_Local_InstanceName = arg0._VB_Local_InstanceName;
					_VB_Local_SetupFile = arg0._VB_Local_SetupFile;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_InstallTemp, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.55));
				CopyOverrideDirectory(_VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + ".minecraft", ModMinecraft.McFolderSelected + "versions\\" + _VB_Local_InstanceName, Task);
				try
				{
					string text = _VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + "instance.cfg";
					if (!FileUtils.Exists(text))
					{
						return;
					}
					List<string> list = new List<string>();
					string[] array = FileUtils.ReadAsLines(text, skipEmptyLines: true);
					foreach (string text2 in array)
					{
						if (text2.Contains("="))
						{
							list.Add(text2.BeforeFirst("=") + ":" + text2.AfterFirst("="));
						}
					}
					FileUtils.Write(text, list.Join("\r\n"));
					if (Conversions.ToBoolean(ModBase.ReadIni(text, "OverrideCommands", Conversions.ToString(Value: false))))
					{
						string text3 = ModBase.ReadIni(text, "PreLaunchCommand");
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							text3 = text3.Replace("\\\"", "\"").Replace("$INST_JAVA", "{java}java.exe").Replace("$INST_MC_DIR\\", "{minecraft}")
								.Replace("$INST_MC_DIR", "{minecraft}")
								.Replace("$INST_DIR\\", "{verpath}")
								.Replace("$INST_DIR", "{verpath}")
								.Replace("$INST_ID", "{name}")
								.Replace("$INST_NAME", "{name}");
							ModBase.WriteIni(_VB_Local_SetupFile, "VersionAdvanceRun", text3);
							Logger.Info($"迁移 MultiMC 版本独立设置：启动前执行命令：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
					}
					if (Conversions.ToBoolean(ModBase.ReadIni(text, "JoinServerOnLaunch", Conversions.ToString(Value: false))))
					{
						string text4 = ModBase.ReadIni(text, "JoinServerOnLaunchAddress").Replace("\\\"", "\"");
						ModBase.WriteIni(_VB_Local_SetupFile, "VersionServerEnter", text4);
						Logger.Info($"迁移 MultiMC 版本独立设置：自动进入服务器：{text4}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					string text5 = ModBase.ReadIni(text, "iconKey");
					if (Operators.CompareString(text5, "", TextCompare: false) != 0 && FileUtils.Exists($"{_VB_Local_InstallTemp}{_VB_Local_ArchiveBaseFolder}{text5}.png"))
					{
						ModBase.WriteIni(_VB_Local_SetupFile, "LogoCustom", Conversions.ToString(Value: true));
						ModBase.WriteIni(_VB_Local_SetupFile, "Logo", "PCL\\Logo.png");
						FileUtils.Copy($"{_VB_Local_InstallTemp}{_VB_Local_ArchiveBaseFolder}{text5}.png", $"{ModMinecraft.McFolderSelected}versions\\{_VB_Local_InstanceName}\\PCL\\Logo.png");
						Logger.Info($"迁移 MultiMC 版本独立设置：版本图标（{text5}.png）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					string text6 = ModBase.ReadIni(text, "JvmArgs");
					if (Operators.CompareString(text6, "", TextCompare: false) != 0)
					{
						if (Conversions.ToBoolean(ModBase.ReadIni(text, "OverrideJavaArgs", Conversions.ToString(Value: false))))
						{
							ModBase.WriteIni(_VB_Local_SetupFile, "VersionAdvanceJvm", text6);
							Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（覆盖）：{text6}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
						else
						{
							text6 = text6 + " " + Settings.Get<string>("LaunchAdvanceJvm");
							ModBase.WriteIni(_VB_Local_SetupFile, "VersionAdvanceJvm", text6);
							Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（追加）：{text6}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"读取 MMC 配置文件失败（{_VB_Local_InstallTemp}{_VB_Local_ArchiveBaseFolder}instance.cfg）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ProjectData.ClearProjectError();
				}
			}
			[SpecialName]
			internal bool _Lambda___3(ModLoader.LoaderBase l)
			{
				return Operators.CompareString(l.Name, _VB_Local_LoaderName, TextCompare: false) == 0;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___9_0
		{
			public string _VB_Local_InstallTemp;
			public string _VB_Local_FileAddress;
			public string _VB_Local_ArchiveBaseFolder;
			public string _VB_Local_InstanceName;
			public JObject _VB_Local_Json;
			public string _VB_Local_SetupFile;
			public string _VB_Local_LoaderName;
			public _Closure___9_0(_Closure___9_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_InstallTemp = arg0._VB_Local_InstallTemp;
					_VB_Local_FileAddress = arg0._VB_Local_FileAddress;
					_VB_Local_ArchiveBaseFolder = arg0._VB_Local_ArchiveBaseFolder;
					_VB_Local_InstanceName = arg0._VB_Local_InstanceName;
					_VB_Local_Json = arg0._VB_Local_Json;
					_VB_Local_SetupFile = arg0._VB_Local_SetupFile;
					_VB_Local_LoaderName = arg0._VB_Local_LoaderName;
				}
			}
			[SpecialName]
			internal void _Lambda___0(ModLoader.LoaderTask<string, int> Task)
			{
				ExtractModpackFiles(_VB_Local_InstallTemp, _VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
				CopyOverrideDirectory(_VB_Local_InstallTemp + _VB_Local_ArchiveBaseFolder + "overrides", ModMinecraft.McFolderSelected + "versions\\" + _VB_Local_InstanceName, Task);
				if (_VB_Local_Json["launchInfo"] != null)
				{
					JObject jObject = (JObject)_VB_Local_Json["launchInfo"];
					if (jObject.ContainsKey("javaArgument"))
					{
						ModBase.WriteIni(_VB_Local_SetupFile, "VersionAdvanceJvm", string.Join(" ", jObject["javaArgument"]));
					}
					if (jObject.ContainsKey("launchArgument"))
					{
						ModBase.WriteIni(_VB_Local_SetupFile, "VersionAdvanceGame", string.Join(" ", jObject["launchArgument"]));
					}
				}
			}
			[SpecialName]
			internal bool _Lambda___3(ModLoader.LoaderBase l)
			{
				return Operators.CompareString(l.Name, _VB_Local_LoaderName, TextCompare: false) == 0;
			}
		}
		public static void ModpackInstall()
		{
			string text = Dialogs.SelectFile("选择整合包压缩文件", multiselect: false, null, new (string[], string)[1] { (new string[3] { "rar", "zip", "mrpack" }, "整合包文件") }).FirstOrDefault();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			ModBase.RunInThread([SpecialName] () =>
			{
				try
				{
					ModpackInstall(text);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ex2.ThrowIfCanceled();
					Logger.Error(ex2, "手动安装整合包失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		public static ModLoader.LoaderCombo<string> ModpackInstall(string File, string InstanceName = null, string Logo = null)
		{
			Logger.Info(string.Format("整合包安装请求：{0}", File ?? "null"), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			ZipArchive zipArchive = null;
			string archiveBaseFolder = "";
			try
			{
				string text = $"{ModMinecraft.McFolderSelected}versions\\{InstanceName}\\";
				if (text.Contains("!") || text.Contains(";"))
				{
					ModMain.Hint("游戏路径中不能含有感叹号或分号：" + text, ModMain.HintType.Red);
					throw new OperationCanceledException();
				}
				int num = -1;
				try
				{
					zipArchive = FileUtils.OpenZip(File);
					if (zipArchive.GetEntry("mcbbs.packmeta") != null)
					{
						num = 3;
					}
					else if (zipArchive.GetEntry("mmc-pack.json") != null)
					{
						num = 2;
					}
					else if (zipArchive.GetEntry("modrinth.index.json") != null)
					{
						num = 4;
					}
					else if (zipArchive.GetEntry("manifest.json") != null)
					{
						num = ((((JObject)zipArchive.GetEntry("manifest.json").Open().ReadString(Encoding.UTF8)
							.DeserializeJson())["addons"] != null) ? 3 : 0);
					}
					else if (zipArchive.GetEntry("modpack.json") != null)
					{
						num = 1;
					}
					else if (zipArchive.GetEntry("modpack.zip") != null || zipArchive.GetEntry("modpack.mrpack") != null)
					{
						num = 9;
					}
					else
					{
						foreach (ZipArchiveEntry entry in zipArchive.Entries)
						{
							string[] array = entry.FullName.Split("/");
							archiveBaseFolder = array[0] + "/";
							if (array.Count() != 2)
							{
								continue;
							}
							if (Operators.CompareString(array[1], "mcbbs.packmeta", TextCompare: false) == 0)
							{
								num = 3;
								break;
							}
							if (Operators.CompareString(array[1], "mmc-pack.json", TextCompare: false) == 0)
							{
								num = 2;
								break;
							}
							if (Operators.CompareString(array[1], "modrinth.index.json", TextCompare: false) == 0)
							{
								num = 4;
								break;
							}
							if (Operators.CompareString(array[1], "manifest.json", TextCompare: false) == 0)
							{
								if (((JObject)entry.Open().ReadString(Encoding.UTF8).DeserializeJson())["addons"] == null)
								{
									num = 0;
									break;
								}
								num = 3;
								archiveBaseFolder = "overrides/";
								break;
							}
							if (Operators.CompareString(array[1], "modpack.json", TextCompare: false) == 0)
							{
								num = 1;
								break;
							}
							if (Operators.CompareString(array[1], "modpack.zip", TextCompare: false) == 0 || Operators.CompareString(array[1], "modpack.mrpack", TextCompare: false) == 0)
							{
								num = 9;
								break;
							}
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.GetDisplay(multiline: true).Contains("Error.WinIOError"))
					{
						throw new Exception("打开整合包文件失败", ex2);
					}
					if (File.EndsWithF(".rar", ignoreCase: true))
					{
						throw new Exception("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试", ex2);
					}
					throw new Exception("打开整合包文件失败，文件可能损坏或为不支持的压缩包格式", ex2);
				}
				switch (num)
				{
				case 0:
					Logger.Info("整合包种类：CurseForge", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackCurseForge(File, zipArchive, archiveBaseFolder, InstanceName, Logo);
				case 1:
					Logger.Info("整合包种类：HMCL", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackHMCL(File, zipArchive, archiveBaseFolder);
				case 2:
					Logger.Info("整合包种类：MMC", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackMMC(File, zipArchive, archiveBaseFolder);
				case 3:
					Logger.Info("整合包种类：MCBBS", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackMCBBS(File, zipArchive, archiveBaseFolder, InstanceName);
				case 4:
					Logger.Info("整合包种类：Modrinth", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackModrinth(File, zipArchive, archiveBaseFolder, InstanceName, Logo);
				case 9:
					Logger.Info("整合包种类：带启动器的压缩包", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackLauncherPack(File, zipArchive, archiveBaseFolder);
				default:
					Logger.Info("整合包种类：未能识别，假定为压缩包", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					return InstallPackCompress(File, zipArchive);
				}
			}
			finally
			{
				zipArchive?.Dispose();
			}
		}
		private static void ExtractModpackFiles(string InstallTemp, string FileAddress, CancellationToken c = default(CancellationToken), ProgressProvider p = null)
		{
			DirectoryUtils.Delete(InstallTemp);
			c.ThrowIfCancellationRequested();
			FileUtils.ExtractToDirectory(FileAddress, InstallTemp, c, p);
		}
		private static void CopyOverrideDirectory(string OverridesFolder, string VersionFolder, ModLoader.LoaderBase Loader)
		{
			if (!OverridesFolder.EndsWithF("\\"))
			{
				OverridesFolder += "\\";
			}
			if (!VersionFolder.EndsWithF("\\"))
			{
				VersionFolder += "\\";
			}
			if (DirectoryUtils.Exists(OverridesFolder))
			{
				Logger.Info($"处理整合包覆写文件夹：{OverridesFolder} → {VersionFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				DirectoryUtils.Copy(OverridesFolder, VersionFolder);
			}
			else
			{
				Logger.Info($"整合包中没有覆写文件夹：{OverridesFolder}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string text = $"{OverridesFolder}PCL\\Setup.ini";
			string text2 = $"{VersionFolder}PCL\\Setup.ini";
			if (FileUtils.Exists(text))
			{
				ModBase.WriteIni(text, "VersionArgumentIndie", Conversions.ToString(1));
				ModBase.WriteIni(text, "VersionArgumentIndieV2", Conversions.ToString(Value: true));
				ModBase.WriteIni(text, "IsStar", Conversions.ToString(Value: false));
				FileUtils.Copy(text, text2);
			}
			else
			{
				ModBase.WriteIni(text2, "VersionArgumentIndie", Conversions.ToString(1));
				ModBase.WriteIni(text2, "VersionArgumentIndieV2", Conversions.ToString(Value: true));
			}
			ModBase.IniClearCache(text2);
		}
		private static void NotifyIncompatibleLoader(string LoaderName)
		{
			if (ModMain.MyMsgBox(string.Format("整合包中存在不兼容的加载器：{0}{1}如果你知道如何手动安装它，可以先选择跳过，然后在整合包下载结束后手动安装。", LoaderName, "\r\n"), "不兼容的加载器", "取消", $"不安装 {LoaderName} 并继续") == 1)
			{
				throw new OperationCanceledException();
			}
		}
		private static ModLoader.LoaderCombo<string> InstallPackCurseForge(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null, string Logo = null)
		{
			_Closure___5_0 arg = default;
			_Closure___5_0 CS_8__locals75 = new _Closure___5_0(arg);
			CS_8__locals75._VB_Local_FileAddress = FileAddress;
			CS_8__locals75._VB_Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS_8__locals75._VB_Local_InstanceName = InstanceName;
			CS_8__locals75._VB_Local_Logo = Logo;
			try
			{
				CS_8__locals75._VB_Local_Json = (JObject)Archive.GetEntry(CS_8__locals75._VB_Local_ArchiveBaseFolder + "manifest.json").Open().ReadString()
					.DeserializeJson();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("CurseForge 整合包安装信息存在问题", innerException);
			}
			if (CS_8__locals75._VB_Local_Json["minecraft"] == null || CS_8__locals75._VB_Local_Json["minecraft"]["version"] == null)
			{
				throw new Exception("CurseForge 整合包未提供 Minecraft 版本信息");
			}
			if (CS_8__locals75._VB_Local_InstanceName == null)
			{
				CS_8__locals75._VB_Local_InstanceName = (string?)(CS_8__locals75._VB_Local_Json["name"] ?? ((JToken)""));
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
				if (Operators.CompareString(validateFolderName.Validate(CS_8__locals75._VB_Local_InstanceName), "", TextCompare: false) != 0)
				{
					CS_8__locals75._VB_Local_InstanceName = "";
				}
				if (Operators.CompareString(CS_8__locals75._VB_Local_InstanceName, "", TextCompare: false) == 0)
				{
					CS_8__locals75._VB_Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<ValidateBase> { validateFolderName });
				}
				if (string.IsNullOrEmpty(CS_8__locals75._VB_Local_InstanceName))
				{
					throw new OperationCanceledException();
				}
			}
			CS_8__locals75._VB_Local_TargetVersion = new ModMinecraft.McVersion();
			CS_8__locals75._VB_Local_TargetVersion.VanillaName = CS_8__locals75._VB_Local_Json["minecraft"]["version"].ToString();
			foreach (JToken item in (IEnumerable<JToken>)(CS_8__locals75._VB_Local_Json["minecraft"]["modLoaders"] ?? ((JToken)new byte[0])))
			{
				string text = (item["id"] ?? ((JToken)"")).ToString().Lower();
				if (text.StartsWithF("forge-"))
				{
					if (text.Contains("recommended"))
					{
						throw new Exception("该整合包版本过老，已不支持进行安装！");
					}
					Logger.Info($"整合包 Forge 版本：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					CS_8__locals75._VB_Local_TargetVersion.Forge = text.Replace("forge-", "");
				}
				else if (text.StartsWithF("neoforge-"))
				{
					Logger.Info($"整合包 NeoForge 版本：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					CS_8__locals75._VB_Local_TargetVersion.NeoForge = text.Replace("neoforge-", "");
				}
				else if (text.StartsWithF("fabric-"))
				{
					Logger.Info($"整合包 Fabric 版本：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					CS_8__locals75._VB_Local_TargetVersion.Fabric = text.Replace("fabric-", "");
				}
				else
				{
					NotifyIncompatibleLoader(text);
				}
			}
			CS_8__locals75._VB_Local_InstallTemp = ModMain.RequestTaskTempFolder();
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			CS_8__locals75._VB_Local_OverrideHome = (string?)(CS_8__locals75._VB_Local_Json["overrides"] ?? ((JToken)""));
			if (Operators.CompareString(CS_8__locals75._VB_Local_OverrideHome, "", TextCompare: false) != 0)
			{
				list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
				{
					ExtractModpackFiles(CS_8__locals75._VB_Local_InstallTemp, CS_8__locals75._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
					CopyOverrideDirectory(CS_8__locals75._VB_Local_InstallTemp + CS_8__locals75._VB_Local_ArchiveBaseFolder + ((Operators.CompareString(CS_8__locals75._VB_Local_OverrideHome, ".", TextCompare: false) == 0 || Operators.CompareString(CS_8__locals75._VB_Local_OverrideHome, "./", TextCompare: false) == 0) ? "" : CS_8__locals75._VB_Local_OverrideHome), $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals75._VB_Local_InstanceName}", Task);
				})
				{
					ProgressWeight = (double)FileUtils.GetInfo(CS_8__locals75._VB_Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
					Block = false
				});
			}
			CS_8__locals75._VB_Local_ModList = new List<int>();
			CS_8__locals75._VB_Local_ModOptionalList = new List<int>();
			foreach (JToken item2 in (IEnumerable<JToken>)(CS_8__locals75._VB_Local_Json["files"] ?? ((JToken)new byte[0])))
			{
				if (item2["projectID"] == null || item2["fileID"] == null)
				{
					ModMain.Hint("某项 Mod 缺少必要信息，已跳过：" + item2.ToString());
					continue;
				}
				CS_8__locals75._VB_Local_ModList.Add((int)item2["fileID"]);
				if (item2["required"] != null && !item2["required"].ToObject<bool>())
				{
					CS_8__locals75._VB_Local_ModOptionalList.Add((int)item2["fileID"]);
				}
			}
			if (CS_8__locals75._VB_Local_ModList.Any())
			{
				List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
				list2.Add(new ModLoader.LoaderTask<int, JArray>("获取 Mod 下载信息", [SpecialName] (ModLoader.LoaderTask<int, JArray> Task) =>
				{
					Task.Output = (JArray)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, "{\"fileIds\": [" + CS_8__locals75._VB_Local_ModList.Join(",") + "]}", "application/json")["data"];
					if (CS_8__locals75._VB_Local_ModList.Count != Task.Output.Count)
					{
						List<int> list5 = CS_8__locals75._VB_Local_ModList.Except(Task.Output.Select([SpecialName] (JToken j) => j["id"].ToObject<int>())).ToList();
						Logger.Info(string.Format("缺失的 fileIds：{0}", list5.Join("、")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						List<(int, int)> list6 = list5.Select([SpecialName] (int id) =>
						{
							_Closure___5_1 arg2 = default;
							_Closure___5_1 CS_8__locals77 = new _Closure___5_1(arg2);
							CS_8__locals77._VB_Local_id = id;
							return (((JObject)CS_8__locals75._VB_Local_Json["files"].AsEnumerable().First([SpecialName] (JToken f) => f["fileID"].ToObject<int>() == CS_8__locals77._VB_Local_id))["projectID"].ToObject<int>(), CS_8__locals77._VB_Local_id);
						}).ToList();
						Logger.Info(string.Format("缺失项：{0}", list6.Join("、")), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						List<ResourceProject> source = new List<ResourceProject>();
						try
						{
							source = ResourceProject.FromProjectIds(list6.Select([SpecialName] ((int ProjectId, int FileId) e) => e.ProjectId.ToString()), ResourcePlatforms.CurseForge).ToList();
						}
						catch (Exception ex2)
						{
							ProjectData.SetProjectError(ex2);
							Exception ex3 = ex2;
							Logger.Warn(ex3, "获取缺失的工程信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
							ProjectData.ClearProjectError();
						}
						List<string> list7 = new List<string>();
						using (List<(int, int)>.Enumerator enumerator3 = list6.GetEnumerator())
						{
							_Closure___5_2 obj = default;
							while (enumerator3.MoveNext())
							{
								obj = new _Closure___5_2(obj);
								obj._VB_Local_Missing = enumerator3.Current;
								ResourceProject resourceProject = source.FirstOrDefault(obj._Lambda___6);
								if ((object)resourceProject == null)
								{
									list7.Add($"未知 CurseForge 项目 (项目 ID {obj._VB_Local_Missing.ProjectId}，文件 ID {obj._VB_Local_Missing.FileId})");
								}
								else
								{
									list7.Add($"{resourceProject.TranslatedName} (文件 ID {obj._VB_Local_Missing.FileId})");
								}
							}
						}
						if (ModMain.MyMsgBox(string.Format("未找到该整合包所需的以下内容，它们可能已被原作者删除：{0}- {1}{2}{3}是否继续安装？缺失这些内容可能导致整合包出现异常！", "\r\n", list7.Join("\r\n- "), "\r\n", "\r\n"), "整合包内容缺失", "继续", "取消", "", IsWarn: true) == 2)
						{
							Task.Cancel();
						}
					}
				})
				{
					ProgressWeight = (double)CS_8__locals75._VB_Local_ModList.Count / 10.0
				});
				list2.Add(new ModLoader.LoaderTask<JArray, List<ModNet.NetFile>>("构造 Mod 下载信息", [SpecialName] (ModLoader.LoaderTask<JArray, List<ModNet.NetFile>> Task) =>
				{
					Dictionary<int, ModNet.NetFile> dictionary = new Dictionary<int, ModNet.NetFile>();
					foreach (JToken item3 in Task.Input)
					{
						int num = item3["id"].ToObject<int>();
						if (dictionary.ContainsKey(num) || (CS_8__locals75._VB_Local_ModOptionalList.Contains(num) && ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + item3["displayName"].ToString() + "？", "下载可选文件", "是", "否") == 2))
						{
							continue;
						}
						string arg2;
						ResourceTypes expectedResourceType;
						if (item3["modules"].Any())
						{
							List<string> list5 = ((JArray)item3["modules"]).Select([SpecialName] (JToken l) => l["name"].ToString()).ToList();
							if (list5.Contains("META-INF") || list5.Contains("mcmod.info") || list5.Contains("fabric.mod.json"))
							{
								goto IL_0136;
							}
							if (item3 != null)
							{
								JToken? jToken = item3["FileName"];
								if (jToken != null && jToken.ToString().EndsWithF(".jar", ignoreCase: true))
								{
									goto IL_0136;
								}
							}
							if (list5.Contains("pack.mcmeta"))
							{
								arg2 = "resourcepacks";
								expectedResourceType = ResourceTypes.ResourcePack;
							}
							else if (list5.Contains("shaders"))
							{
								arg2 = "shaderpacks";
								expectedResourceType = ResourceTypes.Shader;
							}
							else
							{
								arg2 = "saves";
								expectedResourceType = ResourceTypes.Map;
							}
						}
						else
						{
							arg2 = "mods";
							expectedResourceType = ResourceTypes.Mod;
						}
						goto IL_018d;
						IL_0136:
						arg2 = "mods";
						expectedResourceType = ResourceTypes.Mod;
						goto IL_018d;
						IL_018d:
						ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item3, expectedResourceType);
						if (resourceVersion.DownloadAvailable)
						{
							dictionary.Add(num, resourceVersion.ToNetFile($"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals75._VB_Local_InstanceName}\\{arg2}\\", ResourceVersion.DownloadReason.ModPack, CS_8__locals75._VB_Local_TargetVersion.VanillaName, CS_8__locals75._VB_Local_TargetVersion.ModLoaders));
							Task.Progress += 1.0 / (double)checked(1 + CS_8__locals75._VB_Local_ModList.Count);
						}
					}
					Task.Output = dictionary.Values.ToList();
				})
				{
					ProgressWeight = (double)CS_8__locals75._VB_Local_ModList.Count / 200.0,
					Show = false
				});
				list2.Add(new ModNet.LoaderDownload("下载 Mod", new List<ModNet.NetFile>())
				{
					ProgressWeight = (double)CS_8__locals75._VB_Local_ModList.Count * 1.5
				});
				list.Add(new ModLoader.LoaderCombo<int>("下载 Mod（主加载器）", list2)
				{
					Show = false,
					ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
				});
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				NewInstanceName = CS_8__locals75._VB_Local_InstanceName,
				VersionFolder = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals75._VB_Local_InstanceName}\\",
				MinecraftName = CS_8__locals75._VB_Local_TargetVersion.VanillaName,
				ForgeVersion = CS_8__locals75._VB_Local_TargetVersion.Forge,
				NeoForgeVersion = CS_8__locals75._VB_Local_TargetVersion.NeoForge,
				FabricVersion = CS_8__locals75._VB_Local_TargetVersion.Fabric
			};
			List<ModLoader.LoaderBase> list3 = ModDownloadLib.McInstallLoader(mcInstallRequest);
			List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
			list4.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list4.Add(new ModLoader.LoaderCombo<string>("游戏安装", list3)
			{
				Show = false,
				ProgressWeight = list3.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list4.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				string text2 = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals75._VB_Local_InstanceName}\\";
				if (CS_8__locals75._VB_Local_Logo != null && FileUtils.Exists(CS_8__locals75._VB_Local_Logo))
				{
					FileUtils.Copy(CS_8__locals75._VB_Local_Logo, text2 + "PCL\\Logo.png");
					ModBase.WriteIni(text2 + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(text2 + "PCL\\Setup.ini", "LogoCustom", "True");
					Logger.Info($"已设置整合包 Logo：{CS_8__locals75._VB_Local_Logo}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string text3 = $"{text2}saves\\";
				foreach (string item4 in DirectoryUtils.EnumerateFiles(text3, includeSubDirectories: false, "*.zip"))
				{
					FileUtils.ExtractToDirectory(item4, text3, Task.CreateCancellationToken());
					FileUtils.Delete(item4);
				}
				string[] array = new string[2]
				{
					text2 + "原始整合包.zip",
					text2 + "原始整合包.mrpack"
				};
				foreach (string text4 in array)
				{
					if (FileUtils.Exists(text4))
					{
						Logger.Info($"删除原始整合包文件：{text4}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						FileUtils.Delete(text4);
					}
				}
				if (FileUtils.Exists(CS_8__locals75._VB_Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(CS_8__locals75._VB_Local_FileAddress), "modpack", TextCompare: false) == 0)
				{
					Logger.Info($"删除安装整合包文件：{CS_8__locals75._VB_Local_FileAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(CS_8__locals75._VB_Local_FileAddress);
				}
			})
			{
				ProgressWeight = 0.1,
				Show = false
			});
			CS_8__locals75._VB_Local_LoaderName = "CurseForge 整合包安装：" + CS_8__locals75._VB_Local_InstanceName + " ";
			if (ModLoader.LoaderTaskbar.Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_8__locals75._VB_Local_LoaderName, TextCompare: false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_8__locals75._VB_Local_LoaderName, list4);
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(mcInstallRequest.VersionFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackModrinth(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null, string Logo = null)
		{
			_Closure___6_0 arg = default;
			_Closure___6_0 CS_8__locals39 = new _Closure___6_0(arg);
			CS_8__locals39._VB_Local_FileAddress = FileAddress;
			CS_8__locals39._VB_Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS_8__locals39._VB_Local_InstanceName = InstanceName;
			CS_8__locals39._VB_Local_Logo = Logo;
			JObject jObject;
			try
			{
				jObject = (JObject)Archive.GetEntry(CS_8__locals39._VB_Local_ArchiveBaseFolder + "modrinth.index.json").Open().ReadString()
					.DeserializeJson();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("Modrinth 整合包安装信息存在问题", innerException);
			}
			if (jObject["dependencies"] == null || jObject["dependencies"]["minecraft"] == null)
			{
				throw new Exception("Modrinth 整合包未提供 Minecraft 版本信息");
			}
			ModMinecraft.McVersion mcVersion = new ModMinecraft.McVersion();
			foreach (JProperty item in (IEnumerable<JToken>)(jObject["dependencies"] ?? ((JToken)new byte[0])))
			{
				switch (item.Name.Lower())
				{
				case "minecraft":
					mcVersion.VanillaName = item.Value.ToString();
					break;
				case "forge":
					mcVersion.Forge = item.Value.ToString();
					Logger.Info($"整合包 Forge 版本：{mcVersion.Forge}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					break;
				case "neoforge":
				case "neo-forge":
					mcVersion.NeoForge = item.Value.ToString();
					Logger.Info($"整合包 NeoForge 版本：{mcVersion.NeoForge}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					break;
				case "fabric-loader":
					mcVersion.Fabric = item.Value.ToString();
					Logger.Info($"整合包 Fabric 版本：{mcVersion.Fabric}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					break;
				default:
					NotifyIncompatibleLoader(item.Name);
					break;
				}
			}
			if (CS_8__locals39._VB_Local_InstanceName == null)
			{
				CS_8__locals39._VB_Local_InstanceName = (string?)(jObject["name"] ?? ((JToken)""));
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
				if (Operators.CompareString(validateFolderName.Validate(CS_8__locals39._VB_Local_InstanceName), "", TextCompare: false) != 0)
				{
					CS_8__locals39._VB_Local_InstanceName = "";
				}
				if (Operators.CompareString(CS_8__locals39._VB_Local_InstanceName, "", TextCompare: false) == 0)
				{
					CS_8__locals39._VB_Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<ValidateBase> { validateFolderName });
				}
				if (string.IsNullOrEmpty(CS_8__locals39._VB_Local_InstanceName))
				{
					throw new OperationCanceledException();
				}
			}
			CS_8__locals39._VB_Local_InstallTemp = ModMain.RequestTaskTempFolder();
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_8__locals39._VB_Local_InstallTemp, CS_8__locals39._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.5));
				CopyOverrideDirectory(CS_8__locals39._VB_Local_InstallTemp + CS_8__locals39._VB_Local_ArchiveBaseFolder + "overrides", ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals39._VB_Local_InstanceName, Task);
				CopyOverrideDirectory(CS_8__locals39._VB_Local_InstallTemp + CS_8__locals39._VB_Local_ArchiveBaseFolder + "client-overrides", ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals39._VB_Local_InstanceName, Task);
			})
			{
				ProgressWeight = (double)FileUtils.GetInfo(CS_8__locals39._VB_Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
			foreach (JToken item2 in (IEnumerable<JToken>)(jObject["files"] ?? ((JToken)new byte[0])))
			{
				if (item2["env"] != null)
				{
					string left = item2["env"]["client"].ToString();
					if (Operators.CompareString(left, "optional", TextCompare: false) != 0)
					{
						if (Operators.CompareString(left, "unsupported", TextCompare: false) == 0)
						{
							continue;
						}
					}
					else if (ModMain.MyMsgBox("是否要下载可选文件 " + PathUtils.GetLastPart(item2["path"].ToString()) + "？", "下载可选文件", "是", "否") == 2)
					{
						continue;
					}
				}
				List<string> list3 = item2["downloads"].SelectMany([SpecialName] (JToken t) => ResourceVersion.ParseCurseForgeDownloadUrls(t.ToString())).ToList();
				list3.AddRange(list3.Select([SpecialName] (string u) => ModDownload.DlSourceModGet(u)).ToList());
				list3 = ResourceVersion.ParseModrinthTrackArguments(list3, ResourceVersion.DownloadReason.ModPack, mcVersion.VanillaName, mcVersion.ModLoaders).ToList();
				list3 = list3.Distinct().ToList();
				string text = System.IO.Path.Combine(ModMinecraft.McFolderSelected, "versions", CS_8__locals39._VB_Local_InstanceName, item2["path"].ToString().TrimStart(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
				if (!PathUtils.IsParentOf($"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals39._VB_Local_InstanceName}\\", text))
				{
					ModMain.MyMsgBox(string.Format("整合包的文件路径超出了版本文件夹，请向整合包作者反馈此问题！{0}目标：{1}{2}错误的文件：{3}", "\r\n", System.IO.Path.GetFullPath(text), "\r\n", text), "文件路径校验失败", "确定", "", "", IsWarn: true);
					throw new OperationCanceledException();
				}
				list2.Add(new ModNet.NetFile(list3, text, new FileChecker
				{
					Hash = item2["hashes"]["sha1"].ToString()
				}, SimulateBrowserHeaders: true));
			}
			if (list2.Any())
			{
				list.Add(new ModNet.LoaderDownload("下载额外文件", list2)
				{
					ProgressWeight = (double)list2.Count * 1.5
				});
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				NewInstanceName = CS_8__locals39._VB_Local_InstanceName,
				VersionFolder = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals39._VB_Local_InstanceName}\\",
				MinecraftName = mcVersion.VanillaName,
				ForgeVersion = mcVersion.Forge,
				NeoForgeVersion = mcVersion.NeoForge,
				FabricVersion = mcVersion.Fabric
			};
			List<ModLoader.LoaderBase> list4 = ModDownloadLib.McInstallLoader(mcInstallRequest);
			List<ModLoader.LoaderBase> list5 = new List<ModLoader.LoaderBase>();
			list5.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list5.Add(new ModLoader.LoaderCombo<string>("游戏安装", list4)
			{
				Show = false,
				ProgressWeight = list4.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list5.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				string text2 = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals39._VB_Local_InstanceName}\\";
				if (CS_8__locals39._VB_Local_Logo != null && FileUtils.Exists(CS_8__locals39._VB_Local_Logo))
				{
					FileUtils.Copy(CS_8__locals39._VB_Local_Logo, text2 + "PCL\\Logo.png");
					ModBase.WriteIni(text2 + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
					ModBase.WriteIni(text2 + "PCL\\Setup.ini", "LogoCustom", "True");
					Logger.Info($"已设置整合包 Logo：{CS_8__locals39._VB_Local_Logo}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string[] array = new string[2]
				{
					text2 + "原始整合包.zip",
					text2 + "原始整合包.mrpack"
				};
				foreach (string text3 in array)
				{
					if (FileUtils.Exists(text3))
					{
						Logger.Info($"删除原始整合包文件：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						FileUtils.Delete(text3);
					}
				}
				if (FileUtils.Exists(CS_8__locals39._VB_Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(CS_8__locals39._VB_Local_FileAddress), "modpack", TextCompare: false) == 0)
				{
					Logger.Info($"删除安装整合包文件：{CS_8__locals39._VB_Local_FileAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(CS_8__locals39._VB_Local_FileAddress);
				}
			})
			{
				ProgressWeight = 0.1,
				Show = false
			});
			CS_8__locals39._VB_Local_LoaderName = $"Modrinth 整合包安装：{CS_8__locals39._VB_Local_InstanceName} ";
			if (ModLoader.LoaderTaskbar.Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_8__locals39._VB_Local_LoaderName, TextCompare: false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_8__locals39._VB_Local_LoaderName, list5);
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(mcInstallRequest.VersionFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackHMCL(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			JObject jObject;
			try
			{
				jObject = (JObject)Archive.GetEntry(ArchiveBaseFolder + "modpack.json").Open().ReadString(Encoding.UTF8)
					.DeserializeJson();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("HMCL 整合包安装信息存在问题", innerException);
			}
			string text = (string?)(jObject["name"] ?? ((JToken)""));
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(text), "", TextCompare: false) != 0)
			{
				text = "";
			}
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<ValidateBase> { validateFolderName });
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new OperationCanceledException();
			}
			string text2 = ModMain.RequestTaskTempFolder();
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(text2, FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
				CopyOverrideDirectory(text2 + ArchiveBaseFolder + "minecraft", ModMinecraft.McFolderSelected + "versions\\" + text, Task);
			})
			{
				ProgressWeight = (double)FileUtils.GetInfo(FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (jObject["gameVersion"] == null)
			{
				throw new Exception("该 HMCL 整合包未提供游戏版本信息，无法安装！");
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				NewInstanceName = text,
				VersionFolder = $"{ModMinecraft.McFolderSelected}versions\\{text}\\",
				MinecraftName = jObject["gameVersion"].ToString()
			};
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest);
			List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
			{
				new ModLoader.LoaderCombo<string>("整合包安装", list)
				{
					Show = false,
					Block = false,
					ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
				},
				new ModLoader.LoaderCombo<string>("游戏安装", list2)
				{
					Show = false,
					ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
				}
			};
			string text3 = "HMCL 整合包安装：" + text + " ";
			if (ModLoader.LoaderTaskbar.Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, text3, TextCompare: false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(text3, loaders);
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(mcInstallRequest.VersionFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackMMC(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			_Closure___8_0 arg = default;
			_Closure___8_0 CS_8__locals40 = new _Closure___8_0(arg);
			CS_8__locals40._VB_Local_FileAddress = FileAddress;
			CS_8__locals40._VB_Local_ArchiveBaseFolder = ArchiveBaseFolder;
			JObject jObject;
			string str;
			try
			{
				jObject = (JObject)Archive.GetEntry(CS_8__locals40._VB_Local_ArchiveBaseFolder + "mmc-pack.json").Open().ReadString(Encoding.UTF8)
					.DeserializeJson();
				str = Archive.GetEntry(CS_8__locals40._VB_Local_ArchiveBaseFolder + "instance.cfg").Open().ReadString(Encoding.UTF8);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("MMC 整合包安装信息存在问题", innerException);
			}
			CS_8__locals40._VB_Local_InstanceName = str.RegexSeek("(?<=\\nname\\=)[^\\n]+") ?? "";
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(CS_8__locals40._VB_Local_InstanceName), "", TextCompare: false) != 0)
			{
				CS_8__locals40._VB_Local_InstanceName = "";
			}
			if (Operators.CompareString(CS_8__locals40._VB_Local_InstanceName, "", TextCompare: false) == 0)
			{
				CS_8__locals40._VB_Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<ValidateBase> { validateFolderName });
			}
			if (string.IsNullOrEmpty(CS_8__locals40._VB_Local_InstanceName))
			{
				throw new OperationCanceledException();
			}
			CS_8__locals40._VB_Local_InstallTemp = ModMain.RequestTaskTempFolder();
			CS_8__locals40._VB_Local_SetupFile = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals40._VB_Local_InstanceName}\\PCL\\Setup.ini";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_8__locals40._VB_Local_InstallTemp, CS_8__locals40._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.55));
				CopyOverrideDirectory(CS_8__locals40._VB_Local_InstallTemp + CS_8__locals40._VB_Local_ArchiveBaseFolder + ".minecraft", ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals40._VB_Local_InstanceName, Task);
				try
				{
					string text2 = CS_8__locals40._VB_Local_InstallTemp + CS_8__locals40._VB_Local_ArchiveBaseFolder + "instance.cfg";
					if (FileUtils.Exists(text2))
					{
						List<string> list4 = new List<string>();
						string[] array = FileUtils.ReadAsLines(text2, skipEmptyLines: true);
						foreach (string text3 in array)
						{
							if (text3.Contains("="))
							{
								list4.Add(text3.BeforeFirst("=") + ":" + text3.AfterFirst("="));
							}
						}
						FileUtils.Write(text2, list4.Join("\r\n"));
						if (Conversions.ToBoolean(ModBase.ReadIni(text2, "OverrideCommands", Conversions.ToString(Value: false))))
						{
							string text4 = ModBase.ReadIni(text2, "PreLaunchCommand");
							if (Operators.CompareString(text4, "", TextCompare: false) != 0)
							{
								text4 = text4.Replace("\\\"", "\"").Replace("$INST_JAVA", "{java}java.exe").Replace("$INST_MC_DIR\\", "{minecraft}")
									.Replace("$INST_MC_DIR", "{minecraft}")
									.Replace("$INST_DIR\\", "{verpath}")
									.Replace("$INST_DIR", "{verpath}")
									.Replace("$INST_ID", "{name}")
									.Replace("$INST_NAME", "{name}");
								ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "VersionAdvanceRun", text4);
								Logger.Info($"迁移 MultiMC 版本独立设置：启动前执行命令：{text4}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
							}
						}
						if (Conversions.ToBoolean(ModBase.ReadIni(text2, "JoinServerOnLaunch", Conversions.ToString(Value: false))))
						{
							string text5 = ModBase.ReadIni(text2, "JoinServerOnLaunchAddress").Replace("\\\"", "\"");
							ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "VersionServerEnter", text5);
							Logger.Info($"迁移 MultiMC 版本独立设置：自动进入服务器：{text5}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
						string text6 = ModBase.ReadIni(text2, "iconKey");
						if (Operators.CompareString(text6, "", TextCompare: false) != 0 && FileUtils.Exists($"{CS_8__locals40._VB_Local_InstallTemp}{CS_8__locals40._VB_Local_ArchiveBaseFolder}{text6}.png"))
						{
							ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "LogoCustom", Conversions.ToString(Value: true));
							ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "Logo", "PCL\\Logo.png");
							FileUtils.Copy($"{CS_8__locals40._VB_Local_InstallTemp}{CS_8__locals40._VB_Local_ArchiveBaseFolder}{text6}.png", $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals40._VB_Local_InstanceName}\\PCL\\Logo.png");
							Logger.Info($"迁移 MultiMC 版本独立设置：版本图标（{text6}.png）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
						string text7 = ModBase.ReadIni(text2, "JvmArgs");
						if (Operators.CompareString(text7, "", TextCompare: false) != 0)
						{
							if (Conversions.ToBoolean(ModBase.ReadIni(text2, "OverrideJavaArgs", Conversions.ToString(Value: false))))
							{
								ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "VersionAdvanceJvm", text7);
								Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（覆盖）：{text7}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
							}
							else
							{
								text7 = text7 + " " + Settings.Get<string>("LaunchAdvanceJvm");
								ModBase.WriteIni(CS_8__locals40._VB_Local_SetupFile, "VersionAdvanceJvm", text7);
								Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（追加）：{text7}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
							}
						}
					}
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception ex3 = ex2;
					Logger.Warn(ex3, $"读取 MMC 配置文件失败（{CS_8__locals40._VB_Local_InstallTemp}{CS_8__locals40._VB_Local_ArchiveBaseFolder}instance.cfg）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ProjectData.ClearProjectError();
				}
			})
			{
				ProgressWeight = (double)FileUtils.GetInfo(CS_8__locals40._VB_Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (jObject["components"] == null)
			{
				throw new Exception("该 MMC 整合包未提供游戏版本信息，无法安装！");
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				NewInstanceName = CS_8__locals40._VB_Local_InstanceName,
				VersionFolder = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals40._VB_Local_InstanceName}\\"
			};
			foreach (JObject item in (IEnumerable<JToken>)jObject["components"])
			{
				if (!item.ContainsKey("uid"))
				{
					continue;
				}
				string text = item["uid"].ToString();
				switch (text)
				{
				case "net.minecraft":
					mcInstallRequest.MinecraftName = (string?)item["version"];
					continue;
				case "net.minecraftforge":
					mcInstallRequest.ForgeVersion = (string?)item["version"];
					continue;
				case "net.neoforged":
					mcInstallRequest.NeoForgeVersion = (string?)item["version"];
					continue;
				case "net.fabricmc.fabric-loader":
					mcInstallRequest.FabricVersion = (string?)item["version"];
					continue;
				}
				if (text.StartsWithF("org.lwjgl"))
				{
					Logger.Info($"已跳过 LWJGL 项：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				else
				{
					NotifyIncompatibleLoader(text);
				}
			}
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest);
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list3.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
			{
				Show = false,
				ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			CS_8__locals40._VB_Local_LoaderName = "MMC 整合包安装：" + CS_8__locals40._VB_Local_InstanceName + " ";
			if (ModLoader.LoaderTaskbar.Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_8__locals40._VB_Local_LoaderName, TextCompare: false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_8__locals40._VB_Local_LoaderName, list3);
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(mcInstallRequest.VersionFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackMCBBS(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null)
		{
			_Closure___9_0 arg = default;
			_Closure___9_0 CS_8__locals35 = new _Closure___9_0(arg);
			CS_8__locals35._VB_Local_FileAddress = FileAddress;
			CS_8__locals35._VB_Local_ArchiveBaseFolder = ArchiveBaseFolder;
			CS_8__locals35._VB_Local_InstanceName = InstanceName;
			try
			{
				ZipArchiveEntry zipArchiveEntry = Archive.GetEntry(CS_8__locals35._VB_Local_ArchiveBaseFolder + "mcbbs.packmeta") ?? Archive.GetEntry(CS_8__locals35._VB_Local_ArchiveBaseFolder + "manifest.json");
				CS_8__locals35._VB_Local_Json = (JObject)zipArchiveEntry.Open().ReadString(Encoding.UTF8).DeserializeJson();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("MCBBS 整合包安装信息存在问题", innerException);
			}
			if (CS_8__locals35._VB_Local_InstanceName == null)
			{
				CS_8__locals35._VB_Local_InstanceName = (string?)(CS_8__locals35._VB_Local_Json["name"] ?? ((JToken)""));
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
				if (Operators.CompareString(validateFolderName.Validate(CS_8__locals35._VB_Local_InstanceName), "", TextCompare: false) != 0)
				{
					CS_8__locals35._VB_Local_InstanceName = "";
				}
				if (Operators.CompareString(CS_8__locals35._VB_Local_InstanceName, "", TextCompare: false) == 0)
				{
					CS_8__locals35._VB_Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<ValidateBase> { validateFolderName });
				}
				if (string.IsNullOrEmpty(CS_8__locals35._VB_Local_InstanceName))
				{
					throw new OperationCanceledException();
				}
			}
			CS_8__locals35._VB_Local_InstallTemp = ModMain.RequestTaskTempFolder();
			CS_8__locals35._VB_Local_SetupFile = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals35._VB_Local_InstanceName}\\PCL\\Setup.ini";
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_8__locals35._VB_Local_InstallTemp, CS_8__locals35._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
				CopyOverrideDirectory(CS_8__locals35._VB_Local_InstallTemp + CS_8__locals35._VB_Local_ArchiveBaseFolder + "overrides", ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals35._VB_Local_InstanceName, Task);
				if (CS_8__locals35._VB_Local_Json["launchInfo"] != null)
				{
					JObject jObject = (JObject)CS_8__locals35._VB_Local_Json["launchInfo"];
					if (jObject.ContainsKey("javaArgument"))
					{
						ModBase.WriteIni(CS_8__locals35._VB_Local_SetupFile, "VersionAdvanceJvm", string.Join(" ", jObject["javaArgument"]));
					}
					if (jObject.ContainsKey("launchArgument"))
					{
						ModBase.WriteIni(CS_8__locals35._VB_Local_SetupFile, "VersionAdvanceGame", string.Join(" ", jObject["launchArgument"]));
					}
				}
			})
			{
				ProgressWeight = (double)FileUtils.GetInfo(CS_8__locals35._VB_Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
			if (CS_8__locals35._VB_Local_Json["addons"] == null)
			{
				throw new Exception("该 MCBBS 整合包未提供游戏版本附加信息，无法安装！");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (JToken item in (IEnumerable<JToken>)CS_8__locals35._VB_Local_Json["addons"])
			{
				dictionary.Add((string?)item["id"], (string?)item["version"]);
			}
			if (!dictionary.ContainsKey("game"))
			{
				throw new Exception("该 MCBBS 整合包未提供游戏版本信息，无法安装！");
			}
			if (dictionary.ContainsKey("quilt"))
			{
				ModMain.Hint("PCL 不支持安装 Quilt 整合包！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModDownloadLib.McInstallRequest mcInstallRequest = new ModDownloadLib.McInstallRequest
			{
				NewInstanceName = CS_8__locals35._VB_Local_InstanceName,
				VersionFolder = $"{ModMinecraft.McFolderSelected}versions\\{CS_8__locals35._VB_Local_InstanceName}\\",
				MinecraftName = dictionary["game"],
				OptiFineVersion = (dictionary.ContainsKey("optifine") ? dictionary["optifine"] : null),
				ForgeVersion = (dictionary.ContainsKey("forge") ? dictionary["forge"] : null),
				NeoForgeVersion = (dictionary.ContainsKey("neoforge") ? dictionary["neoforge"] : null),
				FabricVersion = (dictionary.ContainsKey("fabric") ? dictionary["fabric"] : null)
			};
			List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoader(mcInstallRequest);
			List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
			list3.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			list3.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
			{
				Show = false,
				ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
			CS_8__locals35._VB_Local_LoaderName = "MCBBS 整合包安装：" + CS_8__locals35._VB_Local_InstanceName + " ";
			if (ModLoader.LoaderTaskbar.Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_8__locals35._VB_Local_LoaderName, TextCompare: false) == 0))
			{
				ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_8__locals35._VB_Local_LoaderName, list3);
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(mcInstallRequest.VersionFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackLauncherPack(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
		{
			_Closure___10_0 arg = default;
			_Closure___10_0 CS_8__locals15 = new _Closure___10_0(arg);
			CS_8__locals15._VB_Local_FileAddress = FileAddress;
			CS_8__locals15._VB_Local_ArchiveBaseFolder = ArchiveBaseFolder;
			ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", IsWarn: false, HighLight: true, ForceWait: true);
			CS_8__locals15._VB_Local_TargetFolder = Dialogs.SelectFolder("选择安装目标（必须是一个空文件夹）", multiselect: false).FirstOrDefault();
			if (CS_8__locals15._VB_Local_TargetFolder == null)
			{
				throw new OperationCanceledException();
			}
			if (!DirectoryUtils.IsEmpty(CS_8__locals15._VB_Local_TargetFolder))
			{
				ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[1]
			{
				new ModLoader.LoaderTask<string, int>("解压压缩包", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
				{
					ExtractModpackFiles(CS_8__locals15._VB_Local_TargetFolder, CS_8__locals15._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.9));
					ModBase.OpenExplorer(CS_8__locals15._VB_Local_TargetFolder);
					Thread.Sleep(400);
					string text = null;
					foreach (string item in DirectoryUtils.EnumerateFiles(CS_8__locals15._VB_Local_TargetFolder, includeSubDirectories: false, "*.exe"))
					{
						FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(item);
						Logger.Info($"文件 {item} 的产品名标识为 {versionInfo.ProductName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						if (Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher", TextCompare: false) == 0)
						{
							text = item;
							Logger.Info($"发现整合包附带的 PCL 启动器：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
						else if ((versionInfo.ProductName.ContainsIgnoreCase("Launcher") || versionInfo.ProductName.Contains("启动")) && Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher Admin Manager", TextCompare: false) != 0 && text == null)
						{
							text = item;
							Logger.Info($"发现整合包附带的疑似第三方启动器：{item}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
					}
					Task.Progress = 0.95;
					if (text != null)
					{
						Logger.Info($"找到压缩包中附带的启动器：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						if (ModMain.MyMsgBox(string.Format("整合包里似乎自带了启动器，是否换用它继续安装？{0}即将打开：{1}", "\r\n", text), "换用整合包启动器？", "换用", "不换用") == 1)
						{
							ModBase.StartProcess(text, "--wait");
							Logger.Info("为换用整合包中的启动器启动，强制结束程序", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
							ModMain.FrmMain.EndProgram(SendWarning: false);
							return;
						}
					}
					else
					{
						Logger.Info("未找到压缩包中附带的启动器", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					string lastPart = PathUtils.GetLastPart(CS_8__locals15._VB_Local_TargetFolder);
					DirectoryUtils.Create(CS_8__locals15._VB_Local_TargetFolder + ".minecraft\\");
					PageSelectLeft.AddFolder(CS_8__locals15._VB_Local_TargetFolder + ".minecraft\\" + CS_8__locals15._VB_Local_ArchiveBaseFolder.Replace("/", "\\").TrimStart('\\'), lastPart, ShowHint: false);
					string text2 = DirectoryUtils.EnumerateFiles(CS_8__locals15._VB_Local_TargetFolder, includeSubDirectories: true, "modpack.*").First();
					Logger.Info($"调用 modpack 文件继续安装：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ModpackInstall(text2);
				})
			});
			loaderCombo.Start(CS_8__locals15._VB_Local_TargetFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
			return loaderCombo;
		}
		private static ModLoader.LoaderCombo<string> InstallPackCompress(string FileAddress, ZipArchive Archive)
		{
			_Closure___11_0 arg = default;
			_Closure___11_0 CS_8__locals14 = new _Closure___11_0(arg);
			CS_8__locals14._VB_Local_FileAddress = FileAddress;
			Match match = null;
			Regex regex = new Regex("^.*\\/(?=versions\\/(?<ver>[^\\/]+)\\/(\\k<ver>)\\.json$)", RegexOptions.IgnoreCase);
			foreach (ZipArchiveEntry entry in Archive.Entries)
			{
				Match match2 = regex.Match("/" + entry.FullName);
				if (match2.Success)
				{
					match = match2;
					break;
				}
			}
			if (match == null)
			{
				throw new Exception("文件结构不匹配，这可能不是 Minecraft 客户端压缩包？");
			}
			CS_8__locals14._VB_Local_ArchiveBaseFolder = match.Value.Replace("/", "\\").TrimStart('\\');
			Logger.Info($"检测到压缩包的 .minecraft 根目录：{CS_8__locals14._VB_Local_ArchiveBaseFolder}，命中的版本名：{match.Groups[1].Value}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", IsWarn: false, HighLight: true, ForceWait: true);
			CS_8__locals14._VB_Local_TargetFolder = Dialogs.SelectFolder("选择安装目标（必须是一个空文件夹）", multiselect: false).FirstOrDefault();
			if (CS_8__locals14._VB_Local_TargetFolder == null)
			{
				throw new OperationCanceledException();
			}
			if (CS_8__locals14._VB_Local_TargetFolder.Contains("!") || CS_8__locals14._VB_Local_TargetFolder.Contains(";"))
			{
				ModMain.Hint("Minecraft 文件夹路径中不能含有感叹号或分号！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			if (!DirectoryUtils.IsEmpty(CS_8__locals14._VB_Local_TargetFolder))
			{
				ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[1]
			{
				new ModLoader.LoaderTask<string, int>("解压压缩包", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
				{
					ExtractModpackFiles(CS_8__locals14._VB_Local_TargetFolder, CS_8__locals14._VB_Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.95));
					PageSelectLeft.AddFolder(CS_8__locals14._VB_Local_TargetFolder + CS_8__locals14._VB_Local_ArchiveBaseFolder, PathUtils.GetLastPart(CS_8__locals14._VB_Local_TargetFolder), ShowHint: false);
					Thread.Sleep(400);
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
					});
				})
			});
			loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
			loaderCombo.Start(CS_8__locals14._VB_Local_TargetFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
			return loaderCombo;
		}
	}
}
