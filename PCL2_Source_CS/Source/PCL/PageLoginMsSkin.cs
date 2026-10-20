// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginMsSkin : Grid
	{
		private bool IsChanging;







		public PageLoginMsSkin()
		{
			Loaded += PageLoginLegacy_Loaded;
			IsChanging = false;
			InitializeComponent();
			InitConnectEvents();
			Skin.Loader = PageLaunchLeft.SkinMs;
		}
		private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
		{
			Skin.Loader.Start();
		}
		public void Reload(bool KeepInput)
		{
			TextName.Text = Settings.Get<string>("CacheMsV2Name");
		}
		public static ModLaunch.McLoginMs GetLoginData()
		{
			if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Finished)
			{
				return new ModLaunch.McLoginMs
				{
					OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
					UserName = Settings.Get<string>("CacheMsV2Name"),
					AccessToken = Settings.Get<string>("CacheMsV2Access"),
					Uuid = Settings.Get<string>("CacheMsV2Uuid"),
					ProfileJson = Settings.Get<string>("CacheMsV2ProfileJson")
				};
			}
			return new ModLaunch.McLoginMs
			{
				OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
				UserName = Settings.Get<string>("CacheMsV2Name")
			};
		}
		private void ShowPanel(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(PanButtons, 1.0 - PanButtons.Opacity, 120), "PageLoginMsSkin Button");
		}
		public void HidePanel(object sender, EventArgs e)
		{
			if (!BtnEdit.ContextMenu.IsOpen && !BtnSkin.ContextMenu.IsOpen && !PanData.IsMouseOver)
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(PanButtons, 0.0 - PanButtons.Opacity, 120), "PageLoginMsSkin Button");
			}
		}
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			BtnEdit.ContextMenu.IsOpen = true;
		}
		private void BtnExit_Click()
		{
			Settings.Set("CacheMsV2OAuthRefresh", "");
			Settings.Set("CacheMsV2Access", "");
			Settings.Set("CacheMsV2ProfileJson", "");
			Settings.Set("CacheMsV2Uuid", "");
			Settings.Set("CacheMsV2Name", "");
			Settings.Set("CacheMsV2Expires", 0L);
			ModLaunch.McLoginMsLoader.Cancel();
			ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
		}
		private void BtnSkin_Click(object sender, RoutedEventArgs e)
		{
			BtnSkin.ContextMenu.IsOpen = true;
		}
		public void BtnSkinEdit_Click(object sender, RoutedEventArgs e)
		{
			if (IsChanging)
			{
				ModMain.Hint("正在更改皮肤中，请稍候！");
				return;
			}
			if (ModLaunch.McLoginLoader.State == ModBase.LoadState.Failed)
			{
				ModMain.Hint("登录失败，无法更改皮肤！", ModMain.HintType.Red);
				return;
			}
			ModMinecraft.McSkinInfo skinInfo = ModMinecraft.McSkinSelect();
			if (skinInfo.IsVaild)
			{
				ModMain.Hint("正在更改皮肤……");
				IsChanging = true;
				ModBase.RunInNewThread([SpecialName] () =>
				{
					EditSkin(skinInfo);
				}, "Ms Skin Upload");
			}
		}
		private void EditSkin(ModMinecraft.McSkinInfo SkinInfo)
		{
			while (true)
			{
				try
				{
					while (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Loading)
					{
						Thread.Sleep(10);
					}
					if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Failed)
					{
						throw new Exception("登录失败", ModLaunch.McLoginMsLoader.Error);
					}
					string text = Settings.Get<string>("CacheMsV2Access");
					Settings.Get<string>("CacheMsV2Uuid");
					string text2 = ModNet.NetRequestByClientRetry("https://api.minecraftservices.com/minecraft/profile/skins", HttpMethod.Post, null, new MultipartFormDataContent
					{
						{
							new StringContent(SkinInfo.IsSlim ? "slim" : "classic"),
							"variant"
						},
						{
							new ByteArrayContent(FileUtils.ReadAsBytes(SkinInfo.LocalFile)),
							"file",
							PathUtils.GetLastPart(SkinInfo.LocalFile)
						}
					}, null, "*/*", new string[3, 2]
					{
						{
							"Authorization",
							"Bearer " + text
						},
						{ "Accept", "*/*" },
						{ "User-Agent", "MojangSharp/0.1" }
					});
					if (text2.Contains("request requires user authentication"))
					{
						ModMain.Hint("正在重新登录，将在登录后自动更改皮肤……");
						ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
						continue;
					}
					Logger.Info(string.Format("皮肤修改返回值：{0}{1}", "\r\n", text2), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
					JObject jObject = (JObject)text2.DeserializeJson();
					if (jObject.ContainsKey("errorMessage"))
					{
						throw new Exception(jObject["errorMessage"].ToString());
					}
					foreach (JObject item in (IEnumerable<JToken>)jObject["skins"])
					{
						if (Operators.CompareString(item["state"].ToString(), "ACTIVE", TextCompare: false) == 0)
						{
							MySkin.ReloadCache((string?)item["url"]);
							return;
						}
					}
					throw new Exception("未知错误（" + text2 + "）");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2 is HttpRequestCodeException)
					{
						HttpRequestCodeException ex3 = (HttpRequestCodeException)ex2;
						switch (ex3.StatusCode)
						{
						case HttpStatusCode.BadRequest:
						{
							Logger.Warn(ex2, "更改皮肤时遭遇 400 错误", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
							string? response = ex3.Response;
							if (response != null && response.Contains("\"error\""))
							{
								ModMain.Hint("更改皮肤失败：" + ex3.Response.DeserializeJson()["error"].ToString(), ModMain.HintType.Red);
								ProjectData.ClearProjectError();
								return;
							}
							string? response2 = ex3.Response;
							if (response2 != null && response2.Contains("\"errorMessage\""))
							{
								ModMain.Hint("更改皮肤失败：" + ex3.Response.DeserializeJson()["errorMessage"].ToString(), ModMain.HintType.Red);
								ProjectData.ClearProjectError();
								return;
							}
							break;
						}
						case HttpStatusCode.Unauthorized:
							Logger.Warn(ex2, "更改皮肤时遭遇 401 错误", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
							ModMain.Hint("正在重新登录，将在登录后自动更改皮肤……");
							ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
							ProjectData.ClearProjectError();
							goto end_IL_0212;
						}
					}
					else if (ex2.IsBadNetwork())
					{
						ModMain.Hint("更改皮肤失败：连接 Mojang 服务器超时，请稍后再试，或使用 VPN 改善网络环境", ModMain.HintType.Red);
					}
					else
					{
						Logger.Error(ex2, "更改皮肤失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
					}
					ProjectData.ClearProjectError();
					break;
					end_IL_0212:;
				}
				finally
				{
					IsChanging = false;
				}
			}
		}
		public void BtnSkinSave_Click(object sender, RoutedEventArgs e)
		{
			Skin.BtnSkinSave_Click();
		}
		public void BtnSkinRefresh_Click(object sender, RoutedEventArgs e)
		{
			Skin.RefreshClick();
		}
		public void BtnSkinCape_Click(object sender, RoutedEventArgs e)
		{
			Skin.BtnSkinCape_Click();
		}
		public void BtnSkinCapeRefresh_Click(object sender, RoutedEventArgs e)
		{
			ModBase.RunInThread([SpecialName] () =>
			{
				try
				{
					ModMain.Hint("正在刷新披风列表……");
					if (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
					{
						ModLaunch.McLoginMsLoader.WaitForExit();
					}
					else
					{
						ModLaunch.McLoginMsLoader.WaitForExit(GetLoginData(), null, IsForceRestart: true);
					}
					ModMain.Hint("已刷新披风列表！", ModMain.HintType.Green);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "刷新披风列表失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Input.MouseEventHandler value2 = ShowPanel;
						System.Windows.Input.MouseEventHandler value3 = HidePanel;
						Grid grid = PanData;
						if (grid != null)
						{
							grid.MouseEnter -= value2;
							grid.MouseLeave -= value3;
						}
						grid = PanData;
						if (grid != null)
						{
							grid.MouseEnter += value2;
							grid.MouseLeave += value3;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnSkin_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
						};
						MyIconButton myIconButton = BtnSkin;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnSkin;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = BtnEdit_Click;
						MyIconButton myIconButton = BtnEdit;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnEdit;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnExit_Click();
						};
						MyIconButton myIconButton = BtnExit;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnExit;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
		}

	}
}
