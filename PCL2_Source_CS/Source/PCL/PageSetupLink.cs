// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageSetupLink : MyPageRight
	{
		[SpecialName]
		private bool _STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init;





		public PageSetupLink()
		{
			Loaded += PageSetupLink_Loaded;
			InitializeComponent();
		}
		private void PageSetupLink_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToHome();
			if (_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init, ref lockTaken);
				if (_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init.State == 0)
				{
					_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init.State = 2;
					_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded = false;
				}
				else if (_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded_Init);
				}
			}
			checked
			{
				if (!_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded)
				{
					_STATIC_PageSetupLink_Loaded_20211C128135_IsLoaded = true;
					ModAnimation.AniControlEnabled++;
					Reload();
					ModAnimation.AniControlEnabled--;
				}
			}
		}
		public void Reload()
		{
			SettingService.RefreshSettings(this);
		}
		public void Reset()
		{
			try
			{
				SettingService.ResetSettings(this);
				Logger.Info("已初始化联机页设置", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLink.xaml.vb");
				ModMain.Hint("已初始化联机页设置！", ModMain.HintType.Green, Log: false);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "初始化联机页设置失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLink.xaml.vb");
				ProjectData.ClearProjectError();
			}
			Reload();
		}
	}
}
