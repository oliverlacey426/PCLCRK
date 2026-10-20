// PCL

namespace PCL
{
	[ContentProperty("SearchTags")]
	[DesignerGenerated]
	public partial class PageResource : MyScrollViewer
	{
		private string _TypeName;
		private string _TypeNameSpaced;
		private ResourceTypes _Type;
		public static ModMinecraft.McInstance TargetInstance = null;
		public static string TargetName = null;
		public ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> Loader;
		public ResourceSearcher.SearchResult Storage;
		public const int PAGE_SIZE = 40;
		public int Page;
		[SpecialName]
		private bool _STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init;
		public ItemCollection SearchTags => ComboSearchTag.Items;
		public string TypeName
		{
			get
			{
				return _TypeName;
			}
			set
			{
				if (Operators.CompareString(_TypeName, value, TextCompare: false) != 0)
				{
					_TypeName = value;
					Loader.Name = $"社区资源获取：{value}";
				}
			}
		}
		public string TypeNameSpaced
		{
			get
			{
				return _TypeNameSpaced;
			}
			set
			{
				if (Operators.CompareString(_TypeNameSpaced, value, TextCompare: false) != 0)
				{
					_TypeNameSpaced = value;
					PanAlways.Title = $"搜索{value}";
					Load.Text = $"正在获取{value}列表";
				}
			}
		}
		public ResourceTypes PageType
		{
			get
			{
				return _Type;
			}
			set
			{
				if (_Type != value)
				{
					_Type = value;
					BtnSearchInstallModPack.Visibility = ((value != ResourceTypes.ModPack) ? Visibility.Collapsed : Visibility.Visible);
				}
			}
		}




















		public PageResource()
		{
			Loaded += PageResource_Loaded;
			_TypeName = "";
			_TypeNameSpaced = "";
			_Type = (ResourceTypes)(-1);
			Loader = ResourceSearcher.GetLoader(LoaderInput);
			Storage = new ResourceSearcher.SearchResult();
			Page = 0;
			InitializeComponent();
			InitConnectEvents();
		}
		private void PageResource_Loaded(object sender, EventArgs e)
		{
			if (_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init, ref lockTaken);
				if (_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init.State == 0)
				{
					_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init.State = 2;
					_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded = false;
				}
				else if (_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded_Init);
				}
			}
			if ((object)TargetInstance != null)
			{
				ResetFilter();
				TextSearchVersion.Text = TargetInstance.Version.VanillaName;
				VB_AnonymousDelegate_12<string, MyComboBoxItem> obj = [SpecialName] (string Name) =>
				{
					foreach (MyComboBoxItem item in (IEnumerable)ComboSearchLoader.Items)
					{
						if (Operators.ConditionalCompareObjectEqual(item.Content, Name, TextCompare: false))
						{
							return item;
						}
					}
					return (MyComboBoxItem)ComboSearchLoader.Items[0];
				};
				if (TargetInstance.Version.HasForge)
				{
					ComboSearchLoader.SelectedItem = obj("Forge");
				}
				else if (TargetInstance.Version.HasFabric)
				{
					ComboSearchLoader.SelectedItem = obj("Fabric");
				}
				else if (TargetInstance.Version.HasNeoForge)
				{
					ComboSearchLoader.SelectedItem = obj("NeoForge");
				}
				TargetInstance = null;
				if (TargetName != null)
				{
					TextSearchName.Text = TargetName;
					TargetName = null;
				}
				if (_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded)
				{
					StartNewSearch();
				}
				ScrollToHome();
			}
			if (_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded)
			{
				return;
			}
			_STATIC_PageResource_Loaded_20211C1281D_IsFirstLoaded = true;
			((MyPageRight)Parent).PageLoaderInit(Load, PanLoad, PanContent, PanAlways, Loader, [SpecialName] (ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> a0) =>
			{
				Load_OnFinish();
			}, LoaderInput);
			if (ModDownload.AllDrops != null && ModDownload.AllDrops.First() > 250)
			{
				string text = ModMinecraft.McVersion.DropToVersion(ModDownload.AllDrops.First());
				if (Operators.CompareString(((MyComboBoxItem)TextSearchVersion.Items[1]).Content.ToString(), text, TextCompare: false) != 0)
				{
					TextSearchVersion.Items.Insert(1, new MyComboBoxItem
					{
						Content = text
					});
				}
			}
		}
		private ResourceSearcher.SearchRequest LoaderInput()
		{
			ResourceSearcher.SearchRequest searchRequest = new ResourceSearcher.SearchRequest(PageType, Storage, checked((Page + 1) * 40));
			string gameVersion;
			if (Operators.CompareString(TextSearchVersion.Text, "全部 (也可自行输入)", TextCompare: false) != 0)
			{
				gameVersion = ((TextSearchVersion.Text.Contains(".") || TextSearchVersion.Text.Contains("w")) ? TextSearchVersion.Text : null);
			}
			else
			{
				gameVersion = null;
			}
			searchRequest.SearchText = TextSearchName.Text;
			searchRequest.GameVersion = gameVersion;
			searchRequest.Tag = Conversions.ToString(NewLateBinding.LateGet(ComboSearchTag.SelectedItem, null, "Tag", new object[0], null, null, null));
			searchRequest.ModLoaders = (ModLoaders)checked((int)Math.Round((PageType == ResourceTypes.Mod) ? ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ComboSearchLoader.SelectedItem, null, "Tag", new object[0], null, null, null))) : 0.0));
			searchRequest.Sources = (ResourcePlatforms)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ComboSearchSource.SelectedItem, null, "Tag", new object[0], null, null, null)))));
			_ = (object)null;
			return searchRequest;
		}
		private void Load_OnFinish()
		{
			checked
			{
				try
				{
					Logger.Info($"开始可视化{TypeNameSpaced}列表，已储藏 {Storage.Results.Count} 个结果，当前在第 {Page + 1} 页", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
					PanProjects.Children.Clear();
					int num = Math.Min(Page * 40, Storage.Results.Count - 1);
					foreach (ResourceProject item in Storage.Results.GetRange(num, Math.Min(Storage.Results.Count - num, 40)))
					{
						PanProjects.Children.Add(item.ToResourceItem(Loader.Input.GameVersion == null, Loader.Input.ModLoaders == ModLoaders.None && (PageType == ResourceTypes.Mod || PageType == ResourceTypes.ModPack)));
					}
					CardPages.Visibility = ((Storage.Results.Count <= 40 && Storage.CurseForgeOffset >= Storage.CurseForgeTotal && Storage.ModrinthOffset >= Storage.ModrinthTotal) ? Visibility.Collapsed : Visibility.Visible);
					LabPage.Text = Conversions.ToString(Page + 1);
					BtnPageFirst.IsEnabled = Page > 1;
					BtnPageFirst.Opacity = ((Page > 1) ? 1.0 : 0.2);
					BtnPageLeft.IsEnabled = Page > 0;
					BtnPageLeft.Opacity = ((Page > 0) ? 1.0 : 0.2);
					bool flag = Storage.Results.Count > 40 * (Page + 1) || Storage.CurseForgeOffset < Storage.CurseForgeTotal || Storage.ModrinthOffset < Storage.ModrinthTotal;
					BtnPageRight.IsEnabled = flag;
					BtnPageRight.Opacity = (flag ? 1.0 : 0.2);
					if (Storage.ErrorMessage == null)
					{
						HintError.Visibility = Visibility.Collapsed;
					}
					else
					{
						HintError.Visibility = Visibility.Visible;
						HintError.Text = Storage.ErrorMessage;
					}
					ScrollToTop();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"可视化{TypeNameSpaced}列表出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
		{
			ModBase.LoadState state2 = Loader.State;
			if (state2 == ModBase.LoadState.Failed)
			{
				string text = "";
				if (Loader.Error != null)
				{
					text = Loader.Error.Message;
				}
				if (text.Contains("不是有效的 json 文件"))
				{
					Logger.Warn($"下载的{TypeNameSpaced}列表 json 文件损坏，已自动重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
					((MyPageRight)Parent).PageLoaderRestart();
				}
			}
		}
		private void BtnPageFirst_Click(object sender, RoutedEventArgs e)
		{
			ChangePage(0);
		}
		private void BtnPageLeft_Click(object sender, RoutedEventArgs e)
		{
			ChangePage(checked(Page - 1));
		}
		private void BtnPageRight_Click(object sender, RoutedEventArgs e)
		{
			ChangePage(checked(Page + 1));
		}
		private void ChangePage(int NewPage)
		{
			CardPages.IsEnabled = false;
			Page = NewPage;
			ModMain.FrmMain.BackToTop();
			Logger.Info($"{TypeName}：切换到第 {checked(Page + 1)} 页", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
			ModBase.RunInThread([SpecialName] () =>
			{
				Thread.Sleep(100);
				ModBase.RunInUi([SpecialName] () =>
				{
					CardPages.IsEnabled = true;
				});
				Loader.Start();
			});
		}
		private void StartNewSearch()
		{
			Page = 0;
			ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> loader = Loader;
			ResourceSearcher.SearchRequest Input = LoaderInput();
			if (loader.ShouldStart(ref Input))
			{
				Storage = new ResourceSearcher.SearchResult();
			}
			Loader.Start();
		}
		private void EnterTrigger(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				StartNewSearch();
			}
		}
		private void ResetFilter()
		{
			TextSearchName.Text = "";
			TextSearchVersion.Text = "全部 (也可自行输入)";
			TextSearchVersion.SelectedIndex = 0;
			ComboSearchSource.SelectedIndex = 0;
			ComboSearchTag.SelectedIndex = 0;
			ComboSearchLoader.SelectedIndex = 0;
			Loader.LastFinishedTime = 0L;
		}
		private void TextSearchVersion_TextChanged()
		{
			if (!TextSearchVersion.IsDropDownOpen)
			{
				UpdateSearchLoaderVisibility();
			}
		}
		private void UpdateSearchLoaderVisibility()
		{
			if (PageType == ResourceTypes.Mod && (TextSearchVersion.Text.Contains(".") || TextSearchVersion.Text.Contains("w")))
			{
				ComboSearchLoader.Visibility = Visibility.Visible;
				Grid.SetColumnSpan(TextSearchVersion, 1);
			}
			else
			{
				ComboSearchLoader.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(TextSearchVersion, 2);
				ComboSearchLoader.SelectedIndex = 0;
			}
		}
		private void BtnSearchInstallModPack_Click(object sender, EventArgs e)
		{
			ModModpack.ModpackInstall();
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.KeyEventHandler value2 = EnterTrigger;
						MyTextBox myTextBox = TextSearchName;
						if (myTextBox != null)
						{
							myTextBox.KeyDown -= value2;
						}
						myTextBox = TextSearchName;
						if (myTextBox != null)
						{
							myTextBox.KeyDown += value2;
						}
			
			}
			{
						System.Windows.Input.KeyEventHandler value2 = EnterTrigger;
						MyComboBox.TextChangedEventHandler obj = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							TextSearchVersion_TextChanged();
						};
						EventHandler value3 = [SpecialName] (object a0, EventArgs a1) =>
						{
							UpdateSearchLoaderVisibility();
						};
						MyComboBox myComboBox = TextSearchVersion;
						if (myComboBox != null)
						{
							myComboBox.KeyDown -= value2;
							myComboBox.TextChanged -= obj;
							myComboBox.DropDownClosed -= value3;
						}
						myComboBox = TextSearchVersion;
						if (myComboBox != null)
						{
							myComboBox.KeyDown += value2;
							myComboBox.TextChanged += obj;
							myComboBox.DropDownClosed += value3;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							StartNewSearch();
						};
						MyButton myButton = BtnSearchRun;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnSearchRun;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							ResetFilter();
						};
						MyButton myButton = BtnSearchReset;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnSearchReset;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnSearchInstallModPack_Click;
						MyButton myButton = BtnSearchInstallModPack;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnSearchInstallModPack;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnPageFirst_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
						};
						MyIconButton myIconButton = BtnPageFirst;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnPageFirst;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnPageLeft_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
						};
						MyIconButton myIconButton = BtnPageLeft;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnPageLeft;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnPageRight_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
						};
						MyIconButton myIconButton = BtnPageRight;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnPageRight;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
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
