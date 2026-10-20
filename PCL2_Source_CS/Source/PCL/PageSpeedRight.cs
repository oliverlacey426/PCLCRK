// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageSpeedRight : MyPageRight
	{


		public PageSpeedRight()
		{
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				Init();
			};
			InitializeComponent();
		}
		private void Init()
		{
			PanBack.ScrollToHome();
		}
	}
}
