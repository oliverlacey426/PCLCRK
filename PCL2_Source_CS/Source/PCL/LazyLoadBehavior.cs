// PCL

namespace PCL
{
	public class LazyLoadBehavior
	{
		[CompilerGenerated]
		internal sealed class _Closure___4_0
		{
			public bool _VB_Local_Handled;
			public FrameworkElement _VB_Local_Element;
			public DependencyPropertyChangedEventArgs _VB_Local_e;
			public EventHandler _VB_Local_Handler;
			[SpecialName]
			internal void _Lambda___R1(object a0, EventArgs a1)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				if (!_VB_Local_Handled && !(_VB_Local_Element.RenderSize.Width < double.Epsilon) && _VB_Local_Element.IsVisible)
				{
					ScrollViewer scrollViewer = FindParentScrollViewer(_VB_Local_Element);
					if (scrollViewer != null && new Rect(0.0, 0.0, scrollViewer.ViewportWidth, scrollViewer.ViewportHeight).IntersectsWith(_VB_Local_Element.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new System.Windows.Point(0.0, 0.0), _VB_Local_Element.RenderSize))))
					{
						_VB_Local_Handled = true;
						((Action)_VB_Local_e.NewValue)();
						_VB_Local_Element.LayoutUpdated -= _VB_Local_Handler;
					}
				}
			}
		}
		private static readonly DependencyProperty IsInViewportProperty = DependencyProperty.RegisterAttached("IsInViewport", typeof(Action), typeof(LazyLoadBehavior), new PropertyMetadata(null, OnIsInViewportChanged));
		public static void OnFirstEnterScrollViewerViewport(DependencyObject obj, Action value)
		{
			obj.SetValue(IsInViewportProperty, value);
		}
		private static void OnIsInViewportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			_Closure___4_0 CS_8__locals9 = new _Closure___4_0();
			CS_8__locals9._VB_Local_e = e;
			CS_8__locals9._VB_Local_Element = d as FrameworkElement;
			if (CS_8__locals9._VB_Local_Element != null && CS_8__locals9._VB_Local_e.NewValue != null)
			{
				CS_8__locals9._VB_Local_Handled = false;
				CS_8__locals9._VB_Local_Handler = [SpecialName] (object a0, EventArgs a1) =>
				{
					CS_8__locals9._Lambda___0();
				};
				CS_8__locals9._VB_Local_Element.LayoutUpdated += CS_8__locals9._VB_Local_Handler;
			}
		}
		private static ScrollViewer FindParentScrollViewer(DependencyObject d)
		{
			while (d != null)
			{
				if (d is ScrollViewer)
				{
					return (ScrollViewer)d;
				}
				d = VisualTreeHelper.GetParent(d);
			}
			return null;
		}
	}
}
