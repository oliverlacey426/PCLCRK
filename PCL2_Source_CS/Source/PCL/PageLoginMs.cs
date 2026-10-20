// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginMs : StackPanel
	{



		public PageLoginMs()
		{
			InitializeComponent();
			InitConnectEvents();
		}
		public void Reload(bool KeepInput)
		{
			int selectedIndex = ComboAccounts.SelectedIndex;
			ComboAccounts.Items.Clear();
			ComboAccounts.Items.Add(new MyComboBoxItem
			{
				Content = "添加新账号"
			});
			try
			{
				JObject jObject = (JObject)Settings.Get<string>("LoginMsJson").DeserializeJson();
				foreach (KeyValuePair<string, JToken> item in jObject)
				{
					MyListItem myListItem = (MyListItem)((DataTemplate)FindResource("ComboBoxItemTemplateWithDelete")).LoadContent();
					myListItem.Tag = item.Value.ToString();
					myListItem.Title = item.Key;
					myListItem.Buttons.ElementAtOrDefault(0).Tag = item.Key;
					ComboAccounts.Items.Add(myListItem);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "微软登录信息出错，登录信息已被重置", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
				Settings.Set("LoginMsJson", "{}");
				ProjectData.ClearProjectError();
			}
			ComboAccounts.SelectedIndex = (KeepInput ? Math.Max(0, selectedIndex) : 0);
		}
		private void ComboAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ModAnimation.AniControlEnabled == 0 && ComboAccounts.SelectedItem != null && ComboAccounts.ContentPresenter != null)
			{
				if (ComboAccounts.SelectedItem is MyListItem)
				{
					ComboAccounts.ContentPresenter.Content = ((MyListItem)ComboAccounts.SelectedItem).Title;
				}
				else if (ComboAccounts.SelectedItem is MyComboBoxItem)
				{
					ComboAccounts.ContentPresenter.Content = RuntimeHelpers.GetObjectValue(((MyComboBoxItem)ComboAccounts.SelectedItem).Content);
				}
			}
		}
		public static ModLaunch.McLoginMs GetLoginData()
		{
			if (ModMain.FrmLoginMs == null)
			{
				return new ModLaunch.McLoginMs
				{
					OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
					UserName = Settings.Get<string>("CacheMsV2Name")
				};
			}
			ModLaunch.McLoginMs result = null;
			ModBase.RunInUiWait([SpecialName] () =>
			{
				if (ModMain.FrmLoginMs.ComboAccounts.SelectedIndex == 0)
				{
					result = new ModLaunch.McLoginMs();
				}
				else
				{
					MyListItem myListItem = (MyListItem)ModMain.FrmLoginMs.ComboAccounts.SelectedItem;
					result = new ModLaunch.McLoginMs
					{
						OAuthRefreshToken = Conversions.ToString(myListItem.Tag),
						UserName = myListItem.Title
					};
				}
			});
			return result;
		}
		public static string IsVaild(ModLaunch.McLoginMs LoginData)
		{
			if (Operators.CompareString(LoginData.OAuthRefreshToken, "", TextCompare: false) == 0)
			{
				return "请在登录账号后再启动游戏！";
			}
			return "";
		}
		public string IsVaild()
		{
			return IsVaild(GetLoginData());
		}
		private void BtnLogin_Click(object sender, EventArgs e)
		{
			ComboAccounts.IsEnabled = false;
			BtnLogin.IsEnabled = false;
			BtnLogin.Text = "0%";
			ModBase.RunInNewThread([SpecialName] () =>
			{
				try
				{
					ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
					while (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Loading)
					{
						ModBase.RunInUi([SpecialName] () =>
						{
							BtnLogin.Text = Conversions.ToString(Math.Round(ModLaunch.McLoginMsLoader.Progress * 100.0)) + "%";
						});
						Thread.Sleep(50);
					}
					if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Finished)
					{
						ModBase.RunInUi([SpecialName] () =>
						{
							ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
						});
						return;
					}
					if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Canceled)
					{
						throw new OperationCanceledException();
					}
					if (ModLaunch.McLoginMsLoader.Error == null)
					{
						throw new Exception("未知错误！");
					}
					throw new Exception(ModLaunch.McLoginMsLoader.Error.Message, ModLaunch.McLoginMsLoader.Error);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (!ex2.IsCanceled())
					{
						if (ex2.Message.StartsWithF("$"))
						{
							ModMain.Hint(ex2.Message.TrimStart('$'), ModMain.HintType.Red);
						}
						else if (ex2 is AuthenticationException && ex2.Message.Contains("SSL/TLS"))
						{
							Logger.Error(ex2, string.Format("正版登录验证失败，请考虑在 [设置 → 其他] 中关闭 [在正版登录时验证 SSL 证书]，然后再试。{0}{1}原始错误信息：", "\r\n", "\r\n"), LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
						}
						else
						{
							Logger.Error(ex2, "正版登录尝试失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
						}
					}
					ProjectData.ClearProjectError();
				}
				finally
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ComboAccounts.IsEnabled = true;
						BtnLogin.IsEnabled = true;
						BtnLogin.Text = "登录";
					});
				}
			}, "Ms Login");
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						SelectionChangedEventHandler value2 = ComboAccounts_SelectionChanged;
						MyComboBox myComboBox = ComboAccounts;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged -= value2;
						}
						myComboBox = ComboAccounts;
						if (myComboBox != null)
						{
							myComboBox.SelectionChanged += value2;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = BtnLogin_Click;
						MyButton myButton = BtnLogin;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnLogin;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
