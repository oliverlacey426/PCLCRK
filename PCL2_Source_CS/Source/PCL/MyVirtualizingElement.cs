// PCL

namespace PCL
{
	public class MyVirtualizingElement<T> : FrameworkElement where T : FrameworkElement
	{
		private Func<T> Initializer;
		public MyVirtualizingElement(Func<T> Initializer)
		{
			this.Initializer = Initializer;
			LazyLoadBehavior.OnFirstEnterScrollViewerViewport(this, [SpecialName] () =>
			{
				Init();
			});
		}
		public T Init()
		{
			T val = Initializer();
			if (Parent != null)
			{
				if (!(Parent is System.Windows.Controls.Panel))
				{
					throw new Exception("MyVirtualizingElement 的父级必须是一个 Panel");
				}
				System.Windows.Controls.Panel panel = (System.Windows.Controls.Panel)Parent;
				int index = panel.Children.IndexOf(this);
				panel.Children.RemoveAt(index);
				panel.Children.Insert(index, val);
			}
			return val;
		}
		public static implicit operator T(MyVirtualizingElement<T> Virtualized)
		{
			return Virtualized.Init();
		}
	}
}
