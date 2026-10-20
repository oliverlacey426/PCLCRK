// PCL

namespace PCL
{
	public class MyVirtualizingElement : FrameworkElement
	{
		private Func<FrameworkElement> Initializer;
		public MyVirtualizingElement(Func<FrameworkElement> Initializer)
		{
			this.Initializer = Initializer;
			LazyLoadBehavior.OnFirstEnterScrollViewerViewport(this, [SpecialName] () =>
			{
				Init();
			});
		}
		public FrameworkElement Init()
		{
			FrameworkElement frameworkElement = Initializer();
			if (Parent != null)
			{
				if (!(Parent is System.Windows.Controls.Panel))
				{
					throw new Exception("MyVirtualizingElement 的父级必须是一个 Panel");
				}
				System.Windows.Controls.Panel panel = (System.Windows.Controls.Panel)Parent;
				int index = panel.Children.IndexOf(this);
				panel.Children.RemoveAt(index);
				panel.Children.Insert(index, frameworkElement);
			}
			return frameworkElement;
		}
		public static FrameworkElement TryInit(FrameworkElement Element)
		{
			if (GeneralUtils.IsGenericInstanceOf(Element, typeof(MyVirtualizingElement<>)))
			{
				return (FrameworkElement)NewLateBinding.LateGet(Element, null, "Init", new object[0], null, null, null);
			}
			if (Element is MyVirtualizingElement)
			{
				return ((MyVirtualizingElement)Element).Init();
			}
			return Element;
		}
	}
}
