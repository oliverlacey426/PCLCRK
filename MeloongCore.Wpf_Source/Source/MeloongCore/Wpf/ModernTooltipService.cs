// MeloongCore.Wpf

namespace MeloongCore.Wpf
{
	public static class ModernTooltipService
	{
		public static readonly DependencyProperty UseModernTooltipProperty;
		public static readonly DependencyProperty FollowMouseProperty;
		private static readonly DependencyProperty IsComboBoxDropDownHookedProperty;
		private const double closedScale = 0.97;
		private const int shadowRadius = 18;
		private const int contentMaxWidth = 676;
		private static readonly Thickness contentMargin;
		private static readonly Brush backgroundBrush;
		private static readonly Brush borderBrush;
		private static readonly Brush foregroundBrush;
		private static readonly DependencyPropertyDescriptor toolTipPropertyDescriptor;
		private static readonly DispatcherTimer openTimer;
		private static bool initialized;
		private static int animationToken;
		private static Point lastMousePoint;
		private static Popup? popup;
		private static Border? popupCard;
		private static ScaleTransform? popupScale;
		private static FrameworkElement? currentOwner;
		private static FrameworkElement? observedToolTipOwner;
		private static ToolTip? borrowedToolTip;
		private static object? borrowedContent;
		public static void SetUseModernTooltip(DependencyObject element, bool value)
		{
			element.SetValue(UseModernTooltipProperty, value);
		}
		public static bool GetUseModernTooltip(DependencyObject element)
		{
			return (bool)element.GetValue(UseModernTooltipProperty);
		}
		public static void SetFollowMouse(DependencyObject element, bool value)
		{
			element.SetValue(FollowMouseProperty, value);
		}
		public static bool GetFollowMouse(DependencyObject element)
		{
			return (bool)element.GetValue(FollowMouseProperty);
		}
		internal static void Init()
		{
			if (initialized)
			{
				return;
			}
			initialized = true;
			EventManager.RegisterClassHandler(typeof(FrameworkElement), UIElement.MouseEnterEvent, new MouseEventHandler(OnMouseEnter), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(FrameworkElement), UIElement.MouseMoveEvent, new MouseEventHandler(OnMouseMove), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(FrameworkElement), UIElement.MouseLeaveEvent, new MouseEventHandler(OnMouseLeave), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(FrameworkElement), UIElement.PreviewMouseUpEvent, new MouseButtonEventHandler(OnPreviewMouseUp), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(FrameworkElement), ToolTipService.ToolTipOpeningEvent, new ToolTipEventHandler(OnToolTipOpening), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(FrameworkElement), FrameworkElement.UnloadedEvent, (RoutedEventHandler)((object sender, RoutedEventArgs _) =>
			{
				if (sender is FrameworkElement frameworkElement && frameworkElement == currentOwner)
				{
					Close(animated: false);
				}
			}), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(ComboBox), FrameworkElement.LoadedEvent, (RoutedEventHandler)((object sender, RoutedEventArgs _) =>
			{
				HookComboBoxDropDown(sender as ComboBox);
			}), handledEventsToo: true);
			EventManager.RegisterClassHandler(typeof(ComboBox), UIElement.PreviewMouseDownEvent, (MouseButtonEventHandler)((object sender, MouseButtonEventArgs _) =>
			{
				HookComboBoxDropDown(sender as ComboBox);
			}), handledEventsToo: true);
		}
		static ModernTooltipService()
		{
			UseModernTooltipProperty = DependencyProperty.RegisterAttached("UseModernTooltip", typeof(bool), typeof(ModernTooltipService), new PropertyMetadata(true));
			FollowMouseProperty = DependencyProperty.RegisterAttached("FollowMouse", typeof(bool), typeof(ModernTooltipService), new PropertyMetadata(true));
			IsComboBoxDropDownHookedProperty = DependencyProperty.RegisterAttached("IsComboBoxDropDownHooked", typeof(bool), typeof(ModernTooltipService), new PropertyMetadata(false));
			contentMargin = new Thickness(12.0, 11.0, 12.0, 8.0);
			backgroundBrush = new SolidColorBrush(Colors.White);
			borderBrush = new SolidColorBrush(Color.FromRgb(214, 214, 214));
			foregroundBrush = new SolidColorBrush(Color.FromRgb(82, 82, 82));
			toolTipPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(FrameworkElement.ToolTipProperty, typeof(FrameworkElement));
			openTimer = new DispatcherTimer();
			backgroundBrush.Freeze();
			borderBrush.Freeze();
			foregroundBrush.Freeze();
			openTimer.Tick += (object _, EventArgs _) =>
			{
				openTimer.Stop();
				if (currentOwner != null)
				{
					Show(currentOwner, lastMousePoint);
				}
			};
		}
		private static void OnMouseEnter(object sender, MouseEventArgs e)
		{
			FrameworkElement reference = sender as FrameworkElement;
			if (reference != null)
			{
				reference.Dispatcher.BeginInvoke((Action)(() =>
				{
					RefreshCurrentOwner(reference);
				}), DispatcherPriority.Input);
			}
		}
		private static void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!(sender is FrameworkElement reference))
			{
				return;
			}
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				FrameworkElement frameworkElement = currentOwner;
				if (frameworkElement != null)
				{
					lastMousePoint = Mouse.GetPosition(frameworkElement);
					if (GetFollowMouse(frameworkElement))
					{
						Popup popup = ModernTooltipService.popup;
						if (popup != null && popup.IsOpen)
						{
							UpdatePosition(frameworkElement, lastMousePoint);
						}
					}
				}
				else
				{
					Popup popup = ModernTooltipService.popup;
					if (popup != null && popup.IsOpen && popup.PlacementTarget is FrameworkElement frameworkElement2 && GetFollowMouse(frameworkElement2))
					{
						lastMousePoint = Mouse.GetPosition(frameworkElement2);
						UpdatePosition(frameworkElement2, lastMousePoint);
					}
				}
				return;
			}
			RefreshCurrentOwner(reference);
			FrameworkElement frameworkElement3 = currentOwner;
			if (frameworkElement3 != null)
			{
				lastMousePoint = Mouse.GetPosition(frameworkElement3);
				if (GetFollowMouse(frameworkElement3))
				{
					Popup popup = ModernTooltipService.popup;
					if (popup != null && popup.IsOpen)
					{
						UpdatePosition(frameworkElement3, lastMousePoint);
					}
				}
			}
			else
			{
				Popup popup = ModernTooltipService.popup;
				if (popup != null && popup.IsOpen && popup.PlacementTarget is FrameworkElement frameworkElement4 && GetFollowMouse(frameworkElement4))
				{
					lastMousePoint = Mouse.GetPosition(frameworkElement4);
					UpdatePosition(frameworkElement4, lastMousePoint);
				}
			}
		}
		private static void OnMouseLeave(object sender, MouseEventArgs e)
		{
			if (sender is FrameworkElement frameworkElement && frameworkElement == currentOwner && (frameworkElement.IsEnabled || !ToolTipService.GetShowOnDisabled(frameworkElement) || !IsPointInside(frameworkElement, Mouse.GetPosition(frameworkElement))))
			{
				FrameworkElement frameworkElement2 = FindCurrentTooltipOwner(frameworkElement);
				if (frameworkElement2 != null && frameworkElement2 != frameworkElement)
				{
					BeginShow(frameworkElement2, Mouse.GetPosition(frameworkElement2));
				}
				else
				{
					Close(animated: true);
				}
			}
		}
		private static void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement reference = sender as FrameworkElement;
			if (reference != null)
			{
				reference.Dispatcher.BeginInvoke((Action)(() =>
				{
					RefreshCurrentOwner(reference);
				}), DispatcherPriority.Input);
			}
		}
		private static void OnToolTipOpening(object sender, ToolTipEventArgs e)
		{
			if (!(sender is FrameworkElement frameworkElement) || !CanShowModernTooltip(frameworkElement) || !TryGetToolTip(frameworkElement, out object _, out ToolTip _))
			{
				return;
			}
			e.Handled = true;
			if (ShouldSuppressTooltip(frameworkElement))
			{
				Close(animated: false);
			}
			else if (!frameworkElement.IsEnabled)
			{
				if (currentOwner != frameworkElement)
				{
					HideImmediately();
				}
				currentOwner = frameworkElement;
				openTimer.Stop();
				lastMousePoint = Mouse.GetPosition(frameworkElement);
				Show(frameworkElement, lastMousePoint);
			}
		}
		private static void RefreshCurrentOwner(FrameworkElement reference)
		{
			FrameworkElement frameworkElement = FindCurrentTooltipOwner(reference);
			FrameworkElement frameworkElement2 = currentOwner;
			if (frameworkElement2 != null && !frameworkElement2.IsEnabled && ToolTipService.GetShowOnDisabled(frameworkElement2) && IsPointInside(frameworkElement2, Mouse.GetPosition(frameworkElement2)))
			{
				return;
			}
			if (ShouldSuppressTooltip(frameworkElement))
			{
				if (currentOwner == null || !(Mouse.Captured is DependencyObject first) || !IsSameTreeBranch(first, currentOwner) || !IsPointInside(currentOwner, Mouse.GetPosition(currentOwner)))
				{
					Close(animated: false);
				}
			}
			else if (frameworkElement != null)
			{
				BeginShow(frameworkElement, Mouse.GetPosition(frameworkElement));
			}
			else if (currentOwner != null)
			{
				Close(animated: true);
			}
		}
		private static FrameworkElement? FindCurrentTooltipOwner(FrameworkElement reference)
		{
			FrameworkElement frameworkElement = FindOwnerInAncestors(Mouse.DirectlyOver as DependencyObject);
			if (frameworkElement != null)
			{
				return frameworkElement;
			}
			if (PresentationSource.FromVisual(reference)?.RootVisual is UIElement uIElement)
			{
				DependencyObject dependencyObject = uIElement.InputHitTest(Mouse.GetPosition(uIElement)) as DependencyObject;
				frameworkElement = FindOwnerInAncestors(dependencyObject);
				if (frameworkElement != null)
				{
					return frameworkElement;
				}
				if (dependencyObject != null && !IsSameTreeBranch(dependencyObject, reference))
				{
					return null;
				}
			}
			if (!(reference.InputHitTest(Mouse.GetPosition(reference)) is DependencyObject start))
			{
				return null;
			}
			return FindOwnerInAncestors(start);
			static FrameworkElement? FindOwnerInAncestors(DependencyObject? dependencyObject3)
			{
				for (DependencyObject dependencyObject2 = dependencyObject3; dependencyObject2 != null; dependencyObject2 = GetParent(dependencyObject2))
				{
					if (dependencyObject2 is FrameworkElement frameworkElement2 && CanShowModernTooltip(frameworkElement2) && TryGetToolTip(frameworkElement2, out object _, out ToolTip _))
					{
						return frameworkElement2;
					}
				}
				return null;
			}
		}
		private static bool CanShowModernTooltip(FrameworkElement owner)
		{
			if (GetUseModernTooltip(owner) && ToolTipService.GetIsEnabled(owner))
			{
				if (!owner.IsEnabled)
				{
					return ToolTipService.GetShowOnDisabled(owner);
				}
				return true;
			}
			return false;
		}
		private static bool TryGetToolTip(FrameworkElement owner, out object? content, out ToolTip? sourceToolTip)
		{
			object toolTip = owner.ToolTip;
			sourceToolTip = toolTip as ToolTip;
			content = ((sourceToolTip == null) ? toolTip : (sourceToolTip.Content ?? ((sourceToolTip == borrowedToolTip) ? borrowedContent : null)));
			if (content != null)
			{
				if (content is string text)
				{
					return text.Length > 0;
				}
				return true;
			}
			return false;
		}
		private static bool ShouldSuppressTooltip(FrameworkElement? owner)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.Captured == null)
			{
				return false;
			}
			if (!(Mouse.Captured is DependencyObject first))
			{
				return true;
			}
			if (owner == null)
			{
				return true;
			}
			return !IsSameTreeBranch(first, owner);
		}
		private static bool IsPointInside(FrameworkElement owner, Point point)
		{
			if (point.X >= 0.0 && point.Y >= 0.0 && point.X <= owner.ActualWidth)
			{
				return point.Y <= owner.ActualHeight;
			}
			return false;
		}
		private static bool IsSameTreeBranch(DependencyObject first, DependencyObject second)
		{
			if (first == second)
			{
				return true;
			}
			for (DependencyObject parent = GetParent(second); parent != null; parent = GetParent(parent))
			{
				if (parent == first)
				{
					return true;
				}
			}
			for (DependencyObject parent2 = GetParent(first); parent2 != null; parent2 = GetParent(parent2))
			{
				if (parent2 == second)
				{
					return true;
				}
			}
			return false;
		}
		private static DependencyObject? GetParent(DependencyObject current)
		{
			if ((current is Visual || current is Visual3D) ? true : false)
			{
				DependencyObject parent = VisualTreeHelper.GetParent(current);
				if (parent != null)
				{
					return parent;
				}
			}
			return LogicalTreeHelper.GetParent(current);
		}
		private static void BeginShow(FrameworkElement owner, Point point)
		{
			if (!CanShowModernTooltip(owner) || !TryGetToolTip(owner, out object _, out ToolTip _))
			{
				return;
			}
			if (ShouldSuppressTooltip(owner))
			{
				Close(animated: false);
				return;
			}
			if (owner is ComboBox comboBox)
			{
				HookComboBoxDropDown(comboBox);
			}
			if (currentOwner == owner)
			{
				lastMousePoint = point;
				Popup popup = ModernTooltipService.popup;
				if ((popup == null || !popup.IsOpen) && !openTimer.IsEnabled)
				{
					StartOpenTimer(owner);
				}
			}
			else
			{
				HideImmediately();
				currentOwner = owner;
				lastMousePoint = point;
				StartOpenTimer(owner);
			}
		}
		private static void StartOpenTimer(FrameworkElement owner)
		{
			openTimer.Stop();
			int num = Math.Max(0, ToolTipService.GetInitialShowDelay(owner));
			if (num == 0)
			{
				Show(owner, lastMousePoint);
				return;
			}
			openTimer.Interval = TimeSpan.FromMilliseconds(num);
			openTimer.Start();
		}
		private static void Show(FrameworkElement owner, Point point)
		{
			if (owner != currentOwner || !CanShowModernTooltip(owner) || !TryGetToolTip(owner, out object content, out ToolTip sourceToolTip))
			{
				return;
			}
			if (owner.IsEnabled && owner != FindCurrentTooltipOwner(owner))
			{
				Close(animated: false);
				return;
			}
			if (popup == null)
			{
				popupScale = new ScaleTransform(0.97, 0.97);
				popupCard = new Border
				{
					Background = backgroundBrush,
					BorderBrush = borderBrush,
					BorderThickness = new Thickness(1.0),
					CornerRadius = new CornerRadius(8.0),
					MaxWidth = 700.0,
					SnapsToDevicePixels = true,
					UseLayoutRounding = true,
					RenderTransform = popupScale,
					RenderTransformOrigin = new Point(0.0, 0.0),
					Effect = new DropShadowEffect
					{
						Opacity = 0.15,
						BlurRadius = 18.0,
						ShadowDepth = 0.0,
						Color = Colors.Black
					}
				};
				Grid grid = new Grid
				{
					Margin = new Thickness(19.0),
					SnapsToDevicePixels = true,
					UseLayoutRounding = true
				};
				grid.Children.Add(popupCard);
				popup = new Popup
				{
					AllowsTransparency = true,
					IsHitTestVisible = false,
					StaysOpen = true,
					PopupAnimation = PopupAnimation.None,
					Placement = PlacementMode.Relative,
					Child = grid
				};
			}
			popup.PlacementTarget = owner;
			UpdatePosition(owner, point);
			ObserveToolTip(owner);
			UpdateContent(owner, content, sourceToolTip);
			animationToken++;
			popupCard.Opacity = 0.0;
			popupScale.ScaleX = 0.97;
			popupScale.ScaleY = 0.97;
			popup.IsOpen = true;
			Animate(1.0, 1.0, null);
		}
		private static void UpdateContent(FrameworkElement owner, object content, ToolTip? sourceToolTip)
		{
			popupCard.DataContext = sourceToolTip?.DataContext ?? owner.DataContext;
			popupCard.FlowDirection = owner.FlowDirection;
			popupCard.Child = null;
			RestoreBorrowedContent();
			if (sourceToolTip != null && content is DependencyObject current && LogicalTreeHelper.GetParent(current) == sourceToolTip)
			{
				borrowedToolTip = sourceToolTip;
				borrowedContent = content;
				sourceToolTip.Content = null;
			}
			bool flag = sourceToolTip?.ContentTemplate != null || sourceToolTip?.ContentTemplateSelector != null;
			popupCard.Child = ((content is string text && !flag) ? ((FrameworkElement)new TextBlock
			{
				Text = text,
				TextWrapping = TextWrapping.Wrap,
				Foreground = foregroundBrush,
				Margin = contentMargin,
				FontSize = 12.5,
				LineHeight = 17.0,
				MaxWidth = 676.0
			}) : ((FrameworkElement)new ContentPresenter
			{
				Content = content,
				ContentTemplate = sourceToolTip?.ContentTemplate,
				ContentTemplateSelector = sourceToolTip?.ContentTemplateSelector,
				ContentStringFormat = sourceToolTip?.ContentStringFormat,
				Margin = contentMargin,
				MaxWidth = 676.0
			}));
		}
		private static void ObserveToolTip(FrameworkElement owner)
		{
			if (observedToolTipOwner != owner)
			{
				StopObservingToolTip();
				observedToolTipOwner = owner;
				toolTipPropertyDescriptor.AddValueChanged(owner, OnToolTipChanged);
			}
		}
		private static void StopObservingToolTip()
		{
			if (observedToolTipOwner != null)
			{
				toolTipPropertyDescriptor.RemoveValueChanged(observedToolTipOwner, OnToolTipChanged);
				observedToolTipOwner = null;
			}
		}
		private static void OnToolTipChanged(object? sender, EventArgs e)
		{
			if (!(sender is FrameworkElement frameworkElement) || frameworkElement != currentOwner)
			{
				return;
			}
			Popup popup = ModernTooltipService.popup;
			if (popup != null && popup.IsOpen)
			{
				if (!CanShowModernTooltip(frameworkElement) || !TryGetToolTip(frameworkElement, out object content, out ToolTip sourceToolTip))
				{
					Close(animated: true);
				}
				else
				{
					UpdateContent(frameworkElement, content, sourceToolTip);
				}
			}
		}
		private static void Close(bool animated)
		{
			openTimer.Stop();
			currentOwner = null;
			StopObservingToolTip();
			Popup popup = ModernTooltipService.popup;
			if (popup == null || !popup.IsOpen || popupCard == null || !animated)
			{
				HideImmediately();
				return;
			}
			int token = ++animationToken;
			Animate(0.0, 0.97, (object _, EventArgs _) =>
			{
				if (token == animationToken)
				{
					HideImmediately();
				}
			});
		}
		private static void HideImmediately()
		{
			openTimer.Stop();
			animationToken++;
			StopObservingToolTip();
			if (popup != null)
			{
				popup.IsOpen = false;
			}
			if (popupCard != null)
			{
				popupCard.BeginAnimation(UIElement.OpacityProperty, null);
				popupCard.Child = null;
			}
			if (popupScale != null)
			{
				popupScale.BeginAnimation(ScaleTransform.ScaleXProperty, null);
				popupScale.BeginAnimation(ScaleTransform.ScaleYProperty, null);
			}
			RestoreBorrowedContent();
		}
		private static void RestoreBorrowedContent()
		{
			if (borrowedToolTip != null)
			{
				borrowedToolTip.Content = borrowedContent;
				borrowedToolTip = null;
				borrowedContent = null;
			}
		}
		private static void UpdatePosition(FrameworkElement owner, Point point)
		{
			popup.PlacementTarget = owner;
			popup.HorizontalOffset = Math.Round(point.X + 15.0);
			popup.VerticalOffset = Math.Round(point.Y + 25.0);
		}
		private static void Animate(double opacity, double scale, EventHandler? completed)
		{
			Duration duration = new Duration(TimeSpan.FromMilliseconds(70.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(opacity, duration)
			{
				EasingFunction = new QuadraticEase
				{
					EasingMode = EasingMode.EaseOut
				}
			};
			if (completed != null)
			{
				doubleAnimation.Completed += completed;
			}
			DoubleAnimation animation = new DoubleAnimation(scale, duration)
			{
				EasingFunction = new QuadraticEase
				{
					EasingMode = EasingMode.EaseOut
				}
			};
			popupCard.BeginAnimation(UIElement.OpacityProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
			popupScale.BeginAnimation(ScaleTransform.ScaleXProperty, animation, HandoffBehavior.SnapshotAndReplace);
			popupScale.BeginAnimation(ScaleTransform.ScaleYProperty, animation, HandoffBehavior.SnapshotAndReplace);
		}
		private static void HookComboBoxDropDown(ComboBox? comboBox)
		{
			if (comboBox == null || (bool)comboBox.GetValue(IsComboBoxDropDownHookedProperty))
			{
				return;
			}
			comboBox.SetValue(IsComboBoxDropDownHookedProperty, true);
			comboBox.DropDownOpened += (object _, EventArgs _) =>
			{
				if (currentOwner != null)
				{
					Close(animated: true);
				}
			};
		}
	}
}
