// PCL

namespace PCL
{
	public class MyPageLeft : Grid
	{
		[CompilerGenerated]
		internal sealed class _Closure___7_0
		{
			public FrameworkElement _VB_Local_Element;
			public MyPageLeft _VB_Me;
			public _Closure___7_0(_Closure___7_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Element = arg0._VB_Local_Element;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				((MyListItem)_VB_Local_Element).IsMouseOverAnimationEnabled = true;
				((MyListItem)_VB_Local_Element).RefreshColor(_VB_Me, new EventArgs());
			}
		}
		private int Uuid;
		public static readonly DependencyProperty AnimatedControlProperty = DependencyProperty.Register("AnimatedControl", typeof(string), typeof(MyPageLeft), new PropertyMetadata(null));
		public string AnimatedControl
		{
			get
			{
				return Conversions.ToString(GetValue(AnimatedControlProperty));
			}
			set
			{
				SetValue(AnimatedControlProperty, value);
			}
		}
		public MyPageLeft()
		{
			Uuid = ModBase.GetUuid();
		}
		public void TriggerShowAnimation()
		{
			if (AnimatedControl == null)
			{
				if (!(RenderTransform is ScaleTransform))
				{
					RenderTransform = new ScaleTransform(0.96, 0.96);
					RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
				}
				Opacity = 0.0;
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaOpacity(this, 1.0, 100)
				}, "PageLeft PageChange " + Conversions.ToString(Uuid));
				return;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			int num2 = 0;
			checked
			{
				using (List<FrameworkElement>.Enumerator enumerator = GetAllAnimControls(IgnoreInvisibility: true).GetEnumerator())
				{
					_Closure___7_0 obj = default;
					while (enumerator.MoveNext())
					{
						obj = new _Closure___7_0(obj);
						obj._VB_Me = this;
						obj._VB_Local_Element = enumerator.Current;
						obj._VB_Local_Element = MyVirtualizingElement.TryInit(obj._VB_Local_Element);
						if (obj._VB_Local_Element.Visibility == Visibility.Collapsed)
						{
							obj._VB_Local_Element.Opacity = 1.0;
							obj._VB_Local_Element.RenderTransform = new TranslateTransform(0.0, 0.0);
							if (obj._VB_Local_Element is MyListItem)
							{
								((MyListItem)obj._VB_Local_Element).IsMouseOverAnimationEnabled = true;
							}
							continue;
						}
						obj._VB_Local_Element.Opacity = 0.0;
						obj._VB_Local_Element.RenderTransform = new TranslateTransform(-25.0, 0.0);
						if (obj._VB_Local_Element is MyListItem)
						{
							((MyListItem)obj._VB_Local_Element).IsMouseOverAnimationEnabled = false;
						}
						list.Add(ModAnimation.AaOpacity(obj._VB_Local_Element, (obj._VB_Local_Element is TextBlock) ? 0.6 : 1.0, 100, num2, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
						list.Add(ModAnimation.AaTranslateX(obj._VB_Local_Element, 5.0, 200, num2, new ModAnimation.AniEaseOutFluent()));
						list.Add(ModAnimation.AaTranslateX(obj._VB_Local_Element, 20.0, 300, num2, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)));
						if (obj._VB_Local_Element is MyListItem)
						{
							list.Add(ModAnimation.AaCode(obj._Lambda___0, num2 + 280));
						}
						num2 += Math.Max(15 - num, 7) * 2;
						num++;
					}
				}
				ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(Uuid));
			}
		}
		public void TriggerHideAnimation()
		{
			if (AnimatedControl == null)
			{
				if (!(RenderTransform is ScaleTransform))
				{
					RenderTransform = new ScaleTransform(1.0, 1.0);
					RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
				}
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(this, 0.95 - ((ScaleTransform)RenderTransform).ScaleX, 110, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaOpacity(this, 0.0 - Opacity, 80, 30)
				}, "PageLeft PageChange " + Conversions.ToString(Uuid));
				return;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			List<FrameworkElement> allAnimControls = GetAllAnimControls();
			checked
			{
				foreach (FrameworkElement item in allAnimControls)
				{
					list.Add(ModAnimation.AaOpacity(item, 0.0 - item.Opacity, 50, (int)Math.Round(70.0 / (double)allAnimControls.Count * (double)num)));
					list.Add(ModAnimation.AaTranslateX(item, -6.0, 50, (int)Math.Round(70.0 / (double)allAnimControls.Count * (double)num)));
					num++;
				}
				ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(Uuid));
			}
		}
		private List<FrameworkElement> GetAllAnimControls(bool IgnoreInvisibility = false)
		{
			List<FrameworkElement> AllControls = new List<FrameworkElement>();
			GetAllAnimControls((FrameworkElement)FindName(AnimatedControl), ref AllControls, IgnoreInvisibility);
			return AllControls;
		}
		private void GetAllAnimControls(FrameworkElement Element, ref List<FrameworkElement> AllControls, bool IgnoreInvisibility)
		{
			if (!IgnoreInvisibility && Element.Visibility == Visibility.Collapsed)
			{
				return;
			}
			if (Element is MyTextButton)
			{
				AllControls.Add(Element);
				return;
			}
			if (Element is MyListItem)
			{
				AllControls.Add(Element);
				return;
			}
			if (Element is ContentControl)
			{
				GetAllAnimControls((FrameworkElement)((ContentControl)Element).Content, ref AllControls, IgnoreInvisibility);
				return;
			}
			if (Element is System.Windows.Controls.Panel)
			{
				foreach (FrameworkElement child in ((System.Windows.Controls.Panel)Element).Children)
				{
					GetAllAnimControls(child, ref AllControls, IgnoreInvisibility);
				}
				return;
			}
			AllControls.Add(Element);
		}
	}
}
