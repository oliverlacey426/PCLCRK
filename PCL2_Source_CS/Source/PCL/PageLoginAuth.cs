// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLoginAuth : Grid
	{
		private bool IsFirstLoad;




		public PageLoginAuth()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ReloadRegisterButton();
			};
			IsFirstLoad = true;
			InitializeComponent();
			InitConnectEvents();
		}
		public void Reload(bool KeepInput)
		{
			CheckRemember.Checked = Settings.Get<bool>("LoginRemember");
			if (KeepInput && !IsFirstLoad)
			{
				string text = ComboName.Text;
				ComboName.ItemsSource = ((Operators.CompareString(Settings.Get<string>("LoginAuthEmail"), "", TextCompare: false) == 0) ? null : Settings.Get<string>("LoginAuthEmail").Split("\u00a8"));
				ComboName.Text = text;
			}
			else if (Operators.CompareString(Settings.Get<string>("LoginAuthEmail"), "", TextCompare: false) == 0)
			{
				ComboName.ItemsSource = null;
			}
			else
			{
				ComboName.ItemsSource = Settings.Get<string>("LoginAuthEmail").Split("\u00a8");
				ComboName.Text = Settings.Get<string>("LoginAuthEmail").BeforeFirst("\u00a8");
				if (Settings.Get<bool>("LoginRemember"))
				{
					TextPass.Password = Settings.Get<string>("LoginAuthPass").BeforeFirst("\u00a8").Trim();
				}
			}
			IsFirstLoad = false;
		}
		public static ModLaunch.McLoginServer GetLoginData()
		{
			string baseUrl = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheAuthServerServer") : Settings.Get<string>("VersionServerAuthServer", ModMinecraft.McInstanceSelected)) + "/authserver";
			if (ModMain.FrmLoginAuth == null)
			{
				return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
				{
					Token = "Auth",
					BaseUrl = baseUrl,
					UserName = "",
					Password = "",
					Description = "Authlib-Injector",
					Type = ModLaunch.McLoginType.Auth
				};
			}
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
			{
				Token = "Auth",
				BaseUrl = baseUrl,
				UserName = ModMain.FrmLoginAuth.ComboName.Text.Replace("\u00a8", "").Trim(),
				Password = ModMain.FrmLoginAuth.TextPass.Password.Replace("\u00a8", "").Trim(),
				Description = "Authlib-Injector",
				Type = ModLaunch.McLoginType.Auth
			};
		}
		public static string IsVaild(ModLaunch.McLoginServer LoginData)
		{
			if (Operators.CompareString(LoginData.UserName, "", TextCompare: false) == 0)
			{
				return "账号不能为空！";
			}
			if (Operators.CompareString(LoginData.Password, "", TextCompare: false) == 0)
			{
				return "密码不能为空！";
			}
			return "";
		}
		public string IsVaild()
		{
			return IsVaild(GetLoginData());
		}
		private void ComboName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "Text", new object[0], null, null, null), "", TextCompare: false))
			{
				TextPass.Password = "";
			}
			if (ModAnimation.AniControlEnabled == 0)
			{
				Settings.Set("CacheAuthAccess", "");
			}
		}
		private void TextPass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (ModAnimation.AniControlEnabled == 0)
			{
				Settings.Set("CacheAuthAccess", "");
			}
		}
		private void ComboName_SelectionChanged(MyComboBox sender, SelectionChangedEventArgs e)
		{
			if (sender.SelectedIndex == -1 || !Settings.Get<bool>("LoginRemember"))
			{
				TextPass.Password = "";
			}
			else
			{
				TextPass.Password = Settings.Get<string>("LoginAuthPass").ToString().Split("\u00a8")[sender.SelectedIndex].Trim();
			}
		}
		private void CheckBoxChange(MyCheckBox sender, object e)
		{
			if (ModAnimation.AniControlEnabled == 0)
			{
				Settings.Set(Conversions.ToString(sender.Tag), sender.Checked);
			}
		}
		private void ComboName_TextChanged()
		{
			BtnLink.Content = ((Operators.CompareString(ComboName.Text, "", TextCompare: false) == 0) ? "注册账号" : "找回密码");
		}
		private void Btn_Click(object sender, EventArgs e)
		{
			if (Operators.ConditionalCompareObjectEqual(BtnLink.Content, "注册账号", TextCompare: false))
			{
				ModBase.OpenWebsite(((object)ModMinecraft.McInstanceSelected != null) ? Settings.Get<string>("VersionServerAuthRegister", ModMinecraft.McInstanceSelected) : Settings.Get<string>("CacheAuthServerRegister"));
			}
			else
			{
				ModBase.OpenWebsite((((object)ModMinecraft.McInstanceSelected != null) ? Settings.Get<string>("VersionServerAuthRegister", ModMinecraft.McInstanceSelected) : Settings.Get<string>("CacheAuthServerRegister")).Replace("/auth/register", "/auth/forgot"));
			}
		}
		private void ReloadRegisterButton()
		{
			string str = (((object)ModMinecraft.McInstanceSelected != null) ? Settings.Get<string>("VersionServerAuthRegister", ModMinecraft.McInstanceSelected) : Settings.Get<string>("CacheAuthServerRegister"));
			BtnLink.Visibility = ((!string.IsNullOrEmpty(new ValidateHttp().Validate(str))) ? Visibility.Collapsed : Visibility.Visible);
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MyComboBox.TextChangedEventHandler obj = ComboName_TextChanged;
						SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
						{
							ComboName_SelectionChanged((MyComboBox)a0, a1);
						};
						MyComboBox.TextChangedEventHandler obj2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
						{
							ComboName_TextChanged();
						};
						MyComboBox myComboBox = ComboName;
						if (myComboBox != null)
						{
							myComboBox.TextChanged -= obj;
							myComboBox.SelectionChanged -= value2;
							myComboBox.TextChanged -= obj2;
						}
						myComboBox = ComboName;
						if (myComboBox != null)
						{
							myComboBox.TextChanged += obj;
							myComboBox.SelectionChanged += value2;
							myComboBox.TextChanged += obj2;
						}
			
			}
			{
						RoutedEventHandler value2 = TextPass_PasswordChanged;
						PasswordBox passwordBox = TextPass;
						if (passwordBox != null)
						{
							passwordBox.PasswordChanged -= value2;
						}
						passwordBox = TextPass;
						if (passwordBox != null)
						{
							passwordBox.PasswordChanged += value2;
						}
			
			}
			{
						MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
						{
							CheckBoxChange((MyCheckBox)a0, a1);
						};
						MyCheckBox myCheckBox = CheckRemember;
						if (myCheckBox != null)
						{
							myCheckBox.Change -= obj;
						}
						myCheckBox = CheckRemember;
						if (myCheckBox != null)
						{
							myCheckBox.Change += obj;
						}
			
			}
			{
						MyTextButton.ClickEventHandler obj = Btn_Click;
						MyTextButton myTextButton = BtnLink;
						if (myTextButton != null)
						{
							myTextButton.Click -= obj;
						}
						myTextButton = BtnLink;
						if (myTextButton != null)
						{
							myTextButton.Click += obj;
						}
			
			}
		}

	}
}
