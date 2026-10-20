// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageOtherHelpDetail : MyPageRight, IRefreshable
	{
		public ModMain.HelpEntry Entry;


		public PageOtherHelpDetail()
		{
			Loaded += PageOtherHelpDetail_Loaded;
			InitializeComponent();
		}
		public void Refresh()
		{
			Init(new ModMain.HelpEntry(Entry.RawPath));
		}
		void IRefreshable.Refresh()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Refresh
			this.Refresh();
		}
		private void PageOtherHelpDetail_Loaded(object sender, RoutedEventArgs e)
		{
			PanBack.ScrollToTop();
		}
		public bool Init(ModMain.HelpEntry Entry)
		{
			string text = Entry.XamlContent ?? "";
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				throw new Exception("帮助 xaml 文件为空");
			}
			bool result;
			try
			{
				text = ModMain.ArgumentReplace(text, StringUtils.XmlEscape);
				while (text.Contains("xmlns"))
				{
					text = text.RegexReplace("xmlns[^\"']*(\"|')[^\"']*(\"|')", "").Replace("xmlns", "");
				}
				text = "<StackPanel xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:sys=\"clr-namespace:System;assembly=mscorlib\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" xmlns:core=\"clr-namespace:MeloongCore;assembly=MeloongCore\" xmlns:corewpf=\"clr-namespace:MeloongCore.Wpf;assembly=MeloongCore.Wpf\">" + text + "</StackPanel>";
				this.Entry = Entry;
				PanCustom.Children.Clear();
				PanCustom.Children.Add((UIElement)ModBase.GetObjectFromXML(text));
				result = true;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Info(string.Format("自定义信息内容：{0}{1}", "\r\n", text), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelpDetail.xaml.vb");
				Logger.Error(ex2, "加载帮助 XAML 文件失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelpDetail.xaml.vb");
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}
}
