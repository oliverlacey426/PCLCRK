// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageDownloadResourceDetail : MyPageRight
	{
		private class CardSorter : IComparer<string>
		{
			public string Topmost;
			public int Compare(string x, string y)
			{
				if (Operators.CompareString(x, y, TextCompare: false) == 0)
				{
					return 0;
				}
				if (Operators.CompareString(x, Topmost, TextCompare: false) == 0)
				{
					return -1;
				}
				if (Operators.CompareString(y, Topmost, TextCompare: false) == 0)
				{
					return 1;
				}
				bool flag = !x.Contains(".");
				bool flag2 = !y.Contains(".");
				if (flag && flag2)
				{
					return x.CompareTo(y);
				}
				if (flag)
				{
					return 1;
				}
				if (flag2)
				{
					return -1;
				}
				checked
				{
					int num = -ModMinecraft.CompareVersion(x.Replace(x.BeforeFirst(" ") + " ", ""), y.Replace(y.BeforeFirst(" ") + " ", ""));
					if (num != 0)
					{
						return num;
					}
					return -ModMinecraft.CompareVersion(x, y);
				}
			}
			int IComparer<string>.Compare(string x, string y)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Compare
				return this.Compare(x, y);
			}
			public CardSorter(string Topmost = "")
			{
				this.Topmost = "";
				this.Topmost = Topmost ?? "";
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___16_0
		{
			public List<string> _VB_Local_Filters;
			public List<ResourceVersion> _VB_Local_Results;
			public PageDownloadResourceDetail _VB_Me;
			public _Closure___16_0(_Closure___16_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Filters = arg0._VB_Local_Filters;
					_VB_Local_Results = arg0._VB_Local_Results;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				_VB_Local_Filters = (from v in _VB_Local_Results.SelectMany([SpecialName] (ResourceVersion v) => v.GameVersions)
					select _VB_Me.GetGroupedVersionName(v, _VB_Me.GroupedDrop, _VB_Me.GroupedOld)).Distinct().OrderByDescending([SpecialName] (string s) => s, new ModMinecraft.VersionComparer()).ToList();
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___21_0
		{
			public string _VB_Local_Target;
			public string _VB_Local_InstanceName;
			public string _VB_Local_LogoFileAddress;
			[SpecialName]
			internal void _Lambda___R4(ModLoader.LoaderTask<int, int> a0)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				ModModpack.ModpackInstall(_VB_Local_Target, _VB_Local_InstanceName, FileUtils.Exists(_VB_Local_LogoFileAddress) ? _VB_Local_LogoFileAddress : null);
			}
		}
		private MyResourceItem ResourceItem;
		private ResourceTypes TargetResourceType;
		private ResourceProject Project;
		private string TargetVersion;
		private ModLoaders TargetLoaders;
		private ModLoader.LoaderTask<int, List<ResourceVersion>> ResourceFileLoader;
		private string VersionFilter;
		private bool GroupedDrop;
		private bool GroupedOld;
		private bool IsFirstInit;
		public static Dictionary<ResourceTypes, string> CachedFolder = new Dictionary<ResourceTypes, string>();













		public PageDownloadResourceDetail()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				LoadPageArguments();
			};
			Initialized += PageDownloadResourceDetail_Inited;
			PageEnter += Init;
			ResourceItem = null;
			ResourceFileLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("Resource File", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
			{
				LoadPageArguments();
				List<ResourceVersion> output = ResourceVersion.FromProjectId(Project.Id, Project.Platform);
				if (!Task.IsCanceled)
				{
					Task.Output = output;
				}
			});
			IsFirstInit = true;
			InitializeComponent();
			InitConnectEvents();
		}
		public void LoadPageArguments()
		{
			Project = (ResourceProject)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 0 }, null);
			TargetVersion = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 2 }, null));
			TargetLoaders = (ModLoaders)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 3 }, null));
			if (TargetLoaders != ModLoaders.None && !TargetLoaders.Flags().IsSingle())
			{
				TargetLoaders = TargetLoaders.Flags().First();
			}
			TargetResourceType = (ResourceTypes)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 4 }, null));
		}
		private void PageDownloadResourceDetail_Inited(object sender, EventArgs e)
		{
			LoadPageArguments();
			PageLoaderInit(Load, PanLoad, PanMain, CardIntro, ResourceFileLoader, [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> a0) =>
			{
				Load_OnFinish();
			});
		}
		private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
		{
			ModBase.LoadState state2 = ResourceFileLoader.State;
			if (state2 == ModBase.LoadState.Failed)
			{
				string text = "";
				if (ResourceFileLoader.Error != null)
				{
					text = ResourceFileLoader.Error.Message;
				}
				if (text.Contains("不是有效的 json 文件"))
				{
					Logger.Warn("下载的文件 json 列表损坏，已自动重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
					PageLoaderRestart();
				}
			}
		}
		private List<ResourceVersion> GetResults()
		{
			List<ResourceVersion> list = ResourceFileLoader.Output;
			switch (TargetResourceType)
			{
			case ResourceTypes.Any:
				list = list.Where([SpecialName] (ResourceVersion r) => r.ResourceType != ResourceTypes.Plugin).ToList();
				break;
			default:
				list = list.Where([SpecialName] (ResourceVersion r) => r.ResourceType == TargetResourceType).ToList();
				break;
			case ResourceTypes.ResourcePack:
			case ResourceTypes.Shader:
				break;
			}
			return list;
		}
		private void Load_OnFinish()
		{
			_Closure___16_0 arg = default;
			_Closure___16_0 CS_8__locals15 = new _Closure___16_0(arg);
			CS_8__locals15._VB_Me = this;
			CS_8__locals15._VB_Local_Results = GetResults();
			CS_8__locals15._VB_Local_Filters = null;
			VB_AnonymousDelegate_10 obj = [SpecialName] () =>
			{
				CS_8__locals15._VB_Local_Filters = (from v in CS_8__locals15._VB_Local_Results.SelectMany([SpecialName] (ResourceVersion v) => v.GameVersions)
					select CS_8__locals15._VB_Me.GetGroupedVersionName(v, CS_8__locals15._VB_Me.GroupedDrop, CS_8__locals15._VB_Me.GroupedOld)).Distinct().OrderByDescending([SpecialName] (string s) => s, new ModMinecraft.VersionComparer()).ToList();
			};
			GroupedDrop = false;
			GroupedOld = false;
			obj();
			if (CS_8__locals15._VB_Local_Filters.Count >= 9)
			{
				GroupedDrop = true;
				GroupedOld = false;
				obj();
				if (CS_8__locals15._VB_Local_Filters.Count >= 9)
				{
					GroupedDrop = false;
					GroupedOld = true;
					obj();
					if (CS_8__locals15._VB_Local_Filters.Count >= 9)
					{
						GroupedDrop = true;
						GroupedOld = true;
						obj();
					}
				}
			}
			PanFilter.Children.Clear();
			if (CS_8__locals15._VB_Local_Filters.Count < 2)
			{
				CardFilter.Visibility = Visibility.Collapsed;
				VersionFilter = null;
			}
			else
			{
				CardFilter.Visibility = Visibility.Visible;
				CS_8__locals15._VB_Local_Filters.Insert(0, "全部");
				foreach (string item in CS_8__locals15._VB_Local_Filters)
				{
					MyRadioButton myRadioButton = new MyRadioButton
					{
						Text = item,
						Margin = new Thickness(2.0, 0.0, 2.0, 0.0),
						ColorType = MyRadioButton.ColorState.Highlight
					};
					myRadioButton.LabText.Margin = new Thickness(-2.0, 0.0, 10.0, 0.0);
					myRadioButton.Check += [SpecialName] (object a0, bool a1) =>
					{
						_Lambda___16_4((MyRadioButton)a0, a1);
					};
					PanFilter.Children.Add(myRadioButton);
				}
				MyRadioButton myRadioButton2 = null;
				if (Operators.CompareString(TargetVersion, "", TextCompare: false) != 0 && CS_8__locals15._VB_Local_Results.FirstOrDefault([SpecialName] (ResourceVersion v) => v.GameVersions.Contains(TargetVersion)) != null)
				{
					string groupedVersionName = GetGroupedVersionName(TargetVersion, GroupedDrop, GroupedOld);
					foreach (MyRadioButton child in PanFilter.Children)
					{
						if (Operators.CompareString(child.Text, groupedVersionName, TextCompare: false) == 0)
						{
							myRadioButton2 = child;
							break;
						}
					}
				}
				if (myRadioButton2 == null)
				{
					myRadioButton2 = (MyRadioButton)PanFilter.Children[0];
				}
				myRadioButton2.Checked = true;
			}
			UpdateFilterResult();
		}
		private void UpdateFilterResult()
		{
			List<ResourceVersion> results = GetResults();
			string text = ((Operators.CompareString(TargetVersion, "", TextCompare: false) != 0 || TargetLoaders != ModLoaders.None) ? string.Format("{0}{1}（所选版本）", (TargetLoaders != ModLoaders.None) ? (TargetLoaders.ToString() + " ") : "", TargetVersion) : "");
			SortedDictionary<string, List<ResourceVersion>> sortedDictionary = new SortedDictionary<string, List<ResourceVersion>>(new CardSorter(text));
			sortedDictionary.Add("其他", new List<ResourceVersion>());
			foreach (ResourceVersion item in results)
			{
				foreach (string gameVersion in item.GameVersions)
				{
					if (VersionFilter != null && Operators.CompareString(GetGroupedVersionName(gameVersion, GroupedDrop, GroupedOld), VersionFilter, TextCompare: false) != 0)
					{
						continue;
					}
					string groupedVersionName = GetGroupedVersionName(gameVersion, GroupedByDrop: false, FoldOld: false);
					List<string> list = new List<string>();
					if (Project.ModLoaders.Flags().Count() > 1 && item.ResourceType == ResourceTypes.Mod && ModMinecraft.McVersion.IsFormatFit(groupedVersionName))
					{
						list.AddRange(from f in item.ModLoaders.Flags()
							select f.ToString() + " ");
					}
					if (!list.Any())
					{
						list.Add("");
					}
					foreach (string item2 in list)
					{
						string key = item2 + groupedVersionName;
						if (!sortedDictionary.ContainsKey(key))
						{
							sortedDictionary.Add(key, new List<ResourceVersion>());
						}
						if (!sortedDictionary[key].Contains(item))
						{
							sortedDictionary[key].Add(item);
						}
					}
				}
			}
			if (Operators.CompareString(text, "", TextCompare: false) != 0 && (VersionFilter == null || GetGroupedVersionName(TargetVersion, GroupedDrop, GroupedOld).StartsWithF(VersionFilter)))
			{
				sortedDictionary.Add(text, new List<ResourceVersion>());
				foreach (ResourceVersion item3 in results)
				{
					if (item3.GameVersions.Contains(TargetVersion) && (TargetLoaders == ModLoaders.None || TargetLoaders.Flags().Intersect(item3.ModLoaders.Flags()).Any()) && (VersionFilter == null || item3.GameVersions.Any([SpecialName] (string v) => Operators.CompareString(GetGroupedVersionName(v, GroupedDrop, GroupedOld), VersionFilter, TextCompare: false) == 0)) && !sortedDictionary[text].Contains(item3))
					{
						sortedDictionary[text].Add(item3);
					}
				}
			}
			try
			{
				PanResults.Children.Clear();
				foreach (KeyValuePair<string, List<ResourceVersion>> item4 in sortedDictionary)
				{
					if (item4.Value.Any() && Operators.CompareString(item4.Key, text.Replace("（所选版本）", ""), TextCompare: false) != 0)
					{
						MyCard myCard = new MyCard
						{
							Title = item4.Key,
							Margin = new Thickness(0.0, 0.0, 0.0, 15.0),
							SwapType = ((TargetResourceType == ResourceTypes.ModPack) ? 9 : 8)
						};
						StackPanel Stack = new StackPanel
						{
							Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
							VerticalAlignment = VerticalAlignment.Top,
							RenderTransform = new TranslateTransform(0.0, 0.0),
							Tag = item4.Value
						};
						myCard.Children.Add(Stack);
						myCard.SwapControl = Stack;
						PanResults.Children.Add(myCard);
						if (Operators.CompareString(item4.Key, text, TextCompare: false) == 0 || (ModMain.FrmMain.PageCurrent.Additional != null && ((List<string>)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 1 }, null)).Contains(myCard.Title)))
						{
							MyCard.StackInstall(ref Stack, (TargetResourceType == ResourceTypes.ModPack) ? 9 : 8, item4.Key);
						}
						else
						{
							myCard.IsSwapped = true;
						}
						if (Operators.CompareString(item4.Key, "其他", TextCompare: false) == 0)
						{
							Stack.Children.Add(new MyHint
							{
								Text = "由于版本信息更新缓慢，可能无法识别刚更新的 MC 版本。几天后即可正常识别。",
								Theme = MyHint.Themes.Yellow,
								Margin = new Thickness(5.0, 0.0, 0.0, 8.0)
							});
						}
					}
				}
				if (PanResults.Children.Count == 1)
				{
					((MyCard)PanResults.Children[0]).IsSwapped = false;
				}
				if (Project.Types == ResourceTypes.ModOrDataPack && (TargetResourceType == ResourceTypes.Mod || TargetResourceType == ResourceTypes.DataPack))
				{
					HintAlternative.Visibility = Visibility.Visible;
					HintAlternative.Text = ((TargetResourceType == ResourceTypes.Mod) ? "以下是该项目的 Mod 版本。点击这里查看其数据包版本。" : "以下是该项目的数据包版本。点击这里查看其 Mod 版本。");
				}
				else
				{
					HintAlternative.Visibility = Visibility.Collapsed;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "可视化工程下载列表出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private string GetGroupedVersionName(string Name, bool GroupedByDrop, bool FoldOld)
		{
			int num = ModMinecraft.McVersion.VersionToDrop(Name);
			if (Name == null)
			{
				return "其他";
			}
			if (Name.Contains("w") || num == 209)
			{
				return "快照版";
			}
			if (!ModMinecraft.McVersion.IsFormatFit(Name) || (FoldOld && num < 120))
			{
				return "远古版";
			}
			if (GroupedByDrop)
			{
				return ModMinecraft.McVersion.DropToVersion(num);
			}
			return Name;
		}
		public void Init()
		{
			checked
			{
				ModAnimation.AniControlEnabled++;
				Project = (ResourceProject)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 0 }, null);
				PanBack.ScrollToHome();
				if (IsFirstInit)
				{
					IsFirstInit = false;
				}
				else
				{
					PageLoaderRestart();
				}
				if (ResourceItem != null)
				{
					PanIntro.Children.Remove(ResourceItem);
				}
				ResourceItem = Project.ToResourceItem(ShowMcVersionDesc: true, ShowLoaderDesc: true).Init();
				ResourceItem.CanInteraction = false;
				ResourceItem.Margin = new Thickness(-7.0, -7.0, 0.0, 8.0);
				PanIntro.Children.Insert(0, ResourceItem);
				BtnIntroWeb.Text = $"转到 {Project.Platform}";
				BtnIntroWiki.Visibility = ((Project.WikiId == 0) ? Visibility.Collapsed : Visibility.Visible);
				ModAnimation.AniControlEnabled--;
			}
		}
		public void Install_Click(MyListItem sender, EventArgs e)
		{
			try
			{
				_Closure___21_0 CS_8__locals8 = new _Closure___21_0();
				ResourceVersion resourceVersion = (ResourceVersion)sender.Tag;
				string name = $"{Project.Platform} 整合包下载：{Project.TranslatedName} ";
				string text = Project.TranslatedName.Replace(".zip", "").Replace(".rar", "").Replace(".mrpack", "")
					.Replace("\\", "＼")
					.Replace("/", "／")
					.Replace("|", "｜")
					.Replace(":", "：")
					.Replace("<", "＜")
					.Replace(">", "＞")
					.Replace("*", "＊")
					.Replace("?", "？")
					.Replace("\"", "")
					.Replace("： ", "：");
				ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
				if (Operators.CompareString(validateFolderName.Validate(text), "", TextCompare: false) != 0)
				{
					text = "";
				}
				CS_8__locals8._VB_Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", text, new Collection<ValidateBase> { validateFolderName });
				if (string.IsNullOrEmpty(CS_8__locals8._VB_Local_InstanceName))
				{
					return;
				}
				List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
				CS_8__locals8._VB_Local_Target = string.Format("{0}versions\\{1}\\原始整合包.{2}", ModMinecraft.McFolderSelected, CS_8__locals8._VB_Local_InstanceName, (Project.Platform == ResourcePlatforms.CurseForge) ? "zip" : "mrpack");
				CS_8__locals8._VB_Local_LogoFileAddress = ResourceItem.PathLogo.ActualSource;
				list.Add(new ModNet.LoaderDownload("下载整合包文件", new ModNet.NetFile[1] { resourceVersion.ToNetFile(CS_8__locals8._VB_Local_Target, ResourceVersion.DownloadReason.Standalone, resourceVersion.GameVersions.FirstOrDefault(), resourceVersion.ModLoaders) })
				{
					ProgressWeight = 10.0,
					Block = true
				});
				list.Add(new ModLoader.LoaderTask<int, int>("准备安装整合包", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
				{
					CS_8__locals8._Lambda___0();
				})
				{
					ProgressWeight = 0.1
				});
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(name, list);
				loaderCombo.OnStateChanged = [SpecialName] (ModLoader.LoaderBase MyLoader) =>
				{
					switch (MyLoader.State)
					{
					case ModBase.LoadState.Failed:
						ModMain.Hint(MyLoader.Name + "失败：" + MyLoader.Error.GetDisplay(multiline: false), ModMain.HintType.Red);
						break;
					case ModBase.LoadState.Canceled:
						ModMain.Hint(MyLoader.Name + "已取消！");
						break;
					case ModBase.LoadState.Loading:
						return;
					}
					ModDownloadLib.McInstallFailedClearFolder(MyLoader);
				};
				loaderCombo.Start(ModMinecraft.McFolderSelected + "versions\\" + CS_8__locals8._VB_Local_InstanceName + "\\");
				ModLoader.LoaderTaskbarAdd(loaderCombo);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.PageChange(FormMain.PageType.TaskManager);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "下载资源整合包失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		public void Save_Click(object sender, EventArgs e)
		{
			ResourceVersion resourceVersion = (ResourceVersion)NewLateBinding.LateGet((sender is MyListItem) ? sender : NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
			ModBase.RunInNewThread([SpecialName] () =>
			{
				try
				{
					string text = null;
					switch (resourceVersion.ResourceType)
					{
					case ResourceTypes.ModPack:
						text = "整合包";
						break;
					case ResourceTypes.Mod:
						text = "Mod ";
						break;
					case ResourceTypes.ResourcePack:
						text = "资源包";
						break;
					case ResourceTypes.Shader:
						text = "光影包";
						break;
					case ResourceTypes.DataPack:
						text = "数据包";
						break;
					}
					string text2 = null;
					if (resourceVersion.ResourceType != ResourceTypes.ModPack)
					{
						string arg = null;
						switch (resourceVersion.ResourceType)
						{
						case ResourceTypes.Mod:
							arg = "mods\\";
							break;
						case ResourceTypes.ResourcePack:
							arg = "resourcepacks\\";
							break;
						case ResourceTypes.Shader:
							arg = "shaderpacks\\";
							break;
						case ResourceTypes.DataPack:
							arg = "";
							break;
						}
						Func<ModMinecraft.McInstance, bool> func = null;
						ModLoaders modLoaders = ModLoaders.None;
						if (resourceVersion.ModLoaders != ModLoaders.None)
						{
							modLoaders = resourceVersion.ModLoaders;
						}
						else if (Project.ModLoaders != ModLoaders.None)
						{
							modLoaders = Project.ModLoaders;
						}
						Logger.Info($"{text}要求的加载器种类：{modLoaders}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
						func = [SpecialName] (ModMinecraft.McInstance Instance) =>
						{
							if (!Instance.IsLoaded)
							{
								Instance.Load();
							}
							return ((resourceVersion.ResourceType != ResourceTypes.Mod && resourceVersion.ResourceType != ResourceTypes.DataPack) || resourceVersion.GameVersions.Any([SpecialName] (string FileVersion) =>
							{
								bool flag2 = FileVersion.Contains("预览版") || !FileVersion.Contains(".");
								bool flag3 = Instance.Version.VanillaName.Contains("snapshot") || !Instance.Version.VanillaName.Contains(".");
								if (flag2 != flag3)
								{
									return false;
								}
								if (!flag2 && !flag3)
								{
									return Operators.CompareString(FileVersion, Instance.Version.VanillaName, TextCompare: false) == 0;
								}
								bool flag4 = FileVersion.Contains(".") && ModBase.Val(FileVersion.BeforeFirst(".")) > 1.0;
								bool flag5 = Instance.Version.Drop > 250;
								if (flag4 != flag5)
								{
									return false;
								}
								if (flag4 && flag5)
								{
									return Operators.CompareString(FileVersion.BeforeFirst(" "), Instance.Version.VanillaName.BeforeFirst("-"), TextCompare: false) == 0;
								}
								return !FileVersion.Contains("w") || !Instance.Version.VanillaName.Contains("w") || Operators.CompareString(FileVersion, Instance.Version.VanillaName, TextCompare: false) == 0;
							})) && (modLoaders == ModLoaders.None || modLoaders.Flags().Intersect(Instance.Version.ModLoaders.Flags()).Any());
						};
						if (CachedFolder.ContainsKey(resourceVersion.ResourceType))
						{
							text2 = CachedFolder[resourceVersion.ResourceType];
							Logger.Info($"使用上次下载时的文件夹作为默认下载位置：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
						}
						else if ((object)ModMinecraft.McInstanceSelected != null && func(ModMinecraft.McInstanceSelected))
						{
							text2 = $"{ModMinecraft.McInstanceSelected.PathIndie}{arg}";
							DirectoryUtils.Create(text2);
							Logger.Info($"使用当前版本作为默认下载位置：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
						}
						else
						{
							bool flag = ModMinecraft.McInstanceListLoader.State != ModBase.LoadState.Finished;
							if (flag)
							{
								ModMain.Hint("正在查找适合的游戏版本……");
								ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", WaitForExit: true);
							}
							IEnumerable<DirectoryInfo> source = from v in ModMinecraft.McInstanceList.Values.SelectMany([SpecialName] (List<ModMinecraft.McInstance> l) => l)
								where func(v)
								select DirectoryUtils.GetInfo($"{v.PathIndie}{arg}");
							if (source.Any())
							{
								DirectoryInfo directoryInfo = (from Dir in source
									orderby (!Dir.Exists) ? DateTime.MinValue : Dir.LastWriteTimeUtc descending, (!Dir.Exists) ? (-1) : Dir.GetFiles().Length descending
									select Dir).First();
								text2 = PathUtils.RemoveExtendedPrefix(directoryInfo.FullName);
								DirectoryUtils.Create(text2);
								Logger.Info($"使用适合的游戏版本作为默认下载位置：{text2}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
							}
							else
							{
								text2 = ModMinecraft.McFolderSelected;
								if (flag)
								{
									ModMain.Hint("当前 MC 文件夹中没有找到合适的版本！");
								}
								else
								{
									Logger.Info("由于当前版本不兼容，使用当前的 MC 文件夹作为默认下载位置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
								}
							}
						}
					}
					string text3;
					if (Operators.CompareString(Project.TranslatedName, Project.RawName, TextCompare: false) == 0)
					{
						text3 = resourceVersion.FileName;
					}
					else
					{
						string text4 = Project.TranslatedName.BeforeFirst(" (").BeforeFirst(" - ").Replace("\\", "＼")
							.Replace("/", "／")
							.Replace("|", "｜")
							.Replace(":", "：")
							.Replace("<", "＜")
							.Replace(">", "＞")
							.Replace("*", "＊")
							.Replace("?", "？")
							.Replace("\"", "")
							.Replace("： ", "：");
						switch (Settings.Get<int>("ToolDownloadTranslateV2"))
						{
						case 0:
							text3 = $"【{text4}】{resourceVersion.FileName}";
							break;
						case 1:
							text3 = $"[{text4}] {resourceVersion.FileName}";
							break;
						case 2:
							text3 = $"{text4}-{resourceVersion.FileName}";
							break;
						case 3:
							text3 = $"{resourceVersion.FileName}-{text4}";
							break;
						default:
							text3 = resourceVersion.FileName;
							break;
						}
					}
					if (resourceVersion.ResourceType == ResourceTypes.Mod)
					{
						text3 = text3.Replace("~", "-");
					}
					ModBase.RunInUi([SpecialName] () =>
					{
						string item = ((resourceVersion.ResourceType != ResourceTypes.Mod) ? (resourceVersion.FileName.EndsWithF(".mrpack") ? "mrpack" : "zip") : (resourceVersion.FileName.EndsWithF(".litemod") ? "litemod" : "jar"));
						string text5 = Dialogs.SaveFile("选择保存位置", text3, text2, new (string, string)[1] { (item, text + "文件") });
						if (text5 != null)
						{
							string name = text + "下载：" + PathUtils.GetFileNameWithoutExtension(text5) + " ";
							if (Operators.CompareString(text5, text2, TextCompare: false) != 0)
							{
								CachedFolder[resourceVersion.ResourceType] = PathUtils.RemoveLastPart(text5);
							}
							ModLoader.LoaderCombo<int> loaderCombo = new ModLoader.LoaderCombo<int>(name, new List<ModLoader.LoaderBase>
							{
								new ModNet.LoaderDownload("下载文件", new ModNet.NetFile[1] { resourceVersion.ToNetFile(text5, (TargetResourceType == ResourceTypes.Any) ? ResourceVersion.DownloadReason.Dependency : ResourceVersion.DownloadReason.Standalone) })
								{
									ProgressWeight = 6.0,
									Block = true
								}
							});
							loaderCombo.OnStateChanged = ModDownloadLib.LoaderStateChangedHintOnly;
							loaderCombo.Start(1);
							ModLoader.LoaderTaskbarAdd(loaderCombo);
							ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
							ModMain.FrmMain.BtnExtraDownload.Ribble();
						}
					});
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "保存资源文件失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
					ProjectData.ClearProjectError();
				}
			}, "Download Resource Detail Save");
		}
		private void BtnIntroWeb_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite(Project.Website);
		}
		private void BtnIntroWiki_Click(object sender, EventArgs e)
		{
			ModBase.OpenWebsite("https://www.mcmod.cn/class/" + Conversions.ToString(Project.WikiId) + ".html");
		}
		private void BtnIntroCopy_Click(object sender, EventArgs e)
		{
			ModBase.ClipboardSet(ResourceItem.LabTitle.Text + ResourceItem.LabTitleRaw.Text);
		}
		private void HintAlternative_Click()
		{
			TargetResourceType = ((TargetResourceType != ResourceTypes.Mod) ? ResourceTypes.Mod : ResourceTypes.DataPack);
			NewLateBinding.LateIndexSet(ModMain.FrmMain.PageCurrent.Additional, new object[2] { 4, TargetResourceType }, null);
			PageLoaderRestart();
		}
		public static void ResourceFilesCardPreload(StackPanel Stack, List<ResourceVersion> Files)
		{
			List<string> list = Files.SelectMany([SpecialName] (ResourceVersion f) => f.Dependencies).Distinct().ToList();
			list.Sort();
			if (!list.Any())
			{
				return;
			}
			list = list.Where([SpecialName] (string dep) =>
			{
				if (!ResourceProject.Cache.ContainsKey(dep))
				{
					Logger.Warn($"未找到 ID {dep} 的前置信息", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				}
				return ResourceProject.Cache.ContainsKey(dep);
			}).ToList();
			Stack.Children.Add(new TextBlock
			{
				Text = "前置资源",
				FontSize = 14.0,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 2.0, 0.0, 5.0)
			});
			foreach (string item in list)
			{
				MyVirtualizingElement<MyResourceItem> element = ResourceProject.Cache[item].ToResourceItem(ShowMcVersionDesc: false, ShowLoaderDesc: false);
				Stack.Children.Add(element);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "版本列表",
				FontSize = 14.0,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 12.0, 0.0, 5.0)
			});
		}
		[SpecialName]
		[CompilerGenerated]
		private void _Lambda___16_4(MyRadioButton sender, bool raiseByMouse)
		{
			VersionFilter = ((Operators.CompareString(sender.Text, "全部", TextCompare: false) == 0) ? null : sender.Text);
			UpdateFilterResult();
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyButton.ClickEventHandler obj = BtnIntroWeb_Click;
						MyButton myButton = BtnIntroWeb;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnIntroWeb;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnIntroWiki_Click;
						MyButton myButton = BtnIntroWiki;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnIntroWiki;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnIntroCopy_Click;
						MyButton myButton = BtnIntroCopy;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnIntroCopy;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyHint.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							HintAlternative_Click();
						};
						MyHint myHint = HintAlternative;
						if (myHint != null)
						{
							myHint.Click -= obj;
						}
						myHint = HintAlternative;
						if (myHint != null)
						{
							myHint.Click += obj;
						}
			
			}
			{
						MyLoading.StateChangedEventHandler obj = Load_State;
						MyLoading myLoading = Load;
						if (myLoading != null)
						{
							myLoading.StateChanged -= obj;
						}
						myLoading = Load;
						if (myLoading != null)
						{
							myLoading.StateChanged += obj;
						}
			
			}
		}

	}
}
