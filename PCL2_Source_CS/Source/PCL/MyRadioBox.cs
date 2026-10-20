// PCL

namespace PCL
{
	[ContentProperty("Inlines")]
	[DesignerGenerated]
	public partial class MyRadioBox : Grid, IMyRadio, ISettingControl
	{
		public delegate void PreviewCheckEventHandler(object sender, ModBase.RouteEventArgs e);
		public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);
		public int Uuid;
		public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(MyRadioBox), new PropertyMetadata(false, [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
		{
			((VB_AnonymousDelegate_6<MyRadioBox, DependencyPropertyChangedEventArgs>)([SpecialName] (MyRadioBox d, DependencyPropertyChangedEventArgs e) =>
			{
				if (!d.IsLoaded)
				{
					d.SyncUI();
				}
			}))((MyRadioBox)a0, a1);
		}));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyRadioBox), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (sender != null)
			{
				((MyRadioBox)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));
		private bool MouseDowned;
		private bool AllowMouseDown;
		private const int AnimationTimeOfMouseIn = 100;
		private const int AnimationTimeOfMouseOut = 200;
		private const int AnimationTimeOfCheck = 150;
		public bool Checked
		{
			get
			{
				return Conversions.ToBoolean(GetValue(CheckedProperty));
			}
			set
			{
				SetChecked(value, user: false);
			}
		}
		public InlineCollection Inlines => LabText.Inlines;
		public string Text
		{
			get
			{
				return Conversions.ToString(GetValue(TextProperty));
			}
			set
			{
				SetValue(TextProperty, value);
			}
		}




		public event PreviewCheckEventHandler PreviewCheck;
		public event PreviewChangeEventHandler PreviewChange;
		public event IMyRadio.CheckEventHandler Check;
		public event IMyRadio.ChangedEventHandler Changed;
		public MyRadioBox()
		{
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Radiobox_MouseUp();
			};
			MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Radiobox_MouseDown();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Radiobox_MouseLeave();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				Radiobox_IsEnabledChanged();
			};
			MouseEnter += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Radiobox_MouseEnterAnimation();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Radiobox_MouseLeaveAnimation();
			};
			Check += [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				SaveSetting();
			};
			Uuid = ModBase.GetUuid();
			MouseDowned = false;
			AllowMouseDown = true;
			InitializeComponent();
		}
		public void SetChecked(bool value, bool user)
		{
			try
			{
				if (value && user)
				{
					ModBase.RouteEventArgs e = new ModBase.RouteEventArgs(user);
					PreviewCheck?.Invoke(this, e);
					if (e.Handled)
					{
						Radiobox_MouseLeave();
						return;
					}
				}
				bool flag = false;
				if (IsLoaded && value != Checked)
				{
					PreviewChange?.Invoke(this, new ModBase.RouteEventArgs(user));
				}
				if (value != Checked)
				{
					SetValue(CheckedProperty, value);
					flag = true;
				}
				if (Parent == null)
				{
					return;
				}
				List<MyRadioBox> list = new List<MyRadioBox>();
				int num = 0;
				foreach (object item in (IEnumerable)NewLateBinding.LateGet(Parent, null, "Children", new object[0], null, null, null))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(item);
					if (objectValue is MyRadioBox)
					{
						list.Add((MyRadioBox)objectValue);
						if (Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Checked", new object[0], null, null, null)))
						{
							num = checked(num + 1);
						}
					}
				}
				if (num > 1)
				{
					if (Checked)
					{
						foreach (MyRadioBox item2 in list)
						{
							if (item2.Checked && !item2.Equals(this))
							{
								item2.Checked = false;
							}
						}
					}
					else
					{
						bool flag2 = false;
						foreach (MyRadioBox item3 in list)
						{
							if (item3.Checked)
							{
								if (flag2)
								{
									item3.Checked = false;
								}
								else
								{
									flag2 = true;
								}
							}
						}
					}
				}
				if (flag)
				{
					if (Checked)
					{
						Check?.Invoke(this, new ModBase.RouteEventArgs(user));
					}
					Changed?.Invoke(this, new ModBase.RouteEventArgs(user));
					ModMain.RaiseCustomEvent(this);
					SyncUI();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "单选框勾选改变错误", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioBox.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void SyncUI()
		{
			if (ModAnimation.AniControlEnabled == 0 && IsLoaded)
			{
				if (Checked)
				{
					if (ShapeDot.Opacity < 0.01)
					{
						ShapeDot.Opacity = 1.0;
					}
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScale(ShapeBorder, 10.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), After: false, Absolute: true),
						ModAnimation.AaScale(ShapeBorder, 8.0, 300, 90, new ModAnimation.AniEaseOutBack(), After: false, Absolute: true)
					}, "MyRadioBox Border " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScale(ShapeDot, 9.0 - ShapeDot.Width, 390, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), After: false, Absolute: true),
						ModAnimation.AaOpacity(ShapeDot, 1.0 - ShapeDot.Opacity, 75, 90)
					}, "MyRadioBox Dot " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, IsMouseOver ? "ColorBrush3" : (IsEnabled ? "ColorBrush2" : "ColorBrushGray5"), 150) }, "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(), After: false, Absolute: true) }, "MyRadioBox Border " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScale(ShapeDot, 0.0 - ShapeDot.Width, 150, 0, new ModAnimation.AniEaseInFluent(), After: false, Absolute: true),
						ModAnimation.AaOpacity(ShapeDot, 0.0 - ShapeDot.Opacity, 75, 30)
					}, "MyRadioBox Dot " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, IsMouseOver ? "ColorBrush3" : (IsEnabled ? "ColorBrush1" : "ColorBrushGray5"), 150) }, "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				}
			}
			else
			{
				ModAnimation.AniStop("MyRadioBox Border " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyRadioBox Dot " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				if (Checked)
				{
					ShapeDot.Width = 9.0;
					ShapeDot.Height = 9.0;
					ShapeDot.Opacity = 1.0;
					ShapeDot.Margin = new Thickness(5.5, 0.0, 0.0, 0.0);
					ShapeBorder.SetResourceReference(Shape.StrokeProperty, IsEnabled ? "ColorBrush2" : "ColorBrushGray5");
				}
				else
				{
					ShapeDot.Width = 0.0;
					ShapeDot.Height = 0.0;
					ShapeDot.Opacity = 0.0;
					ShapeDot.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
					ShapeBorder.SetResourceReference(Shape.StrokeProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
				}
			}
		}
		private void Radiobox_MouseUp()
		{
			if (MouseDowned)
			{
				Logger.Info($"按下单选框：{Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioBox.xaml.vb");
				SetChecked(value: true, user: true);
				MouseDowned = false;
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
			}
		}
		private void Radiobox_MouseDown()
		{
			MouseDowned = true;
			Focus();
			ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushBg1", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
			if (!Checked)
			{
				ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 16.5 - ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyRadioBox Border " + Conversions.ToString(Uuid));
			}
		}
		private void Radiobox_MouseLeave()
		{
			if (MouseDowned)
			{
				MouseDowned = false;
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
				if (!Checked)
				{
					ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyRadioBox Border " + Conversions.ToString(Uuid));
				}
			}
		}
		private void Radiobox_IsEnabledChanged()
		{
			if (IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				if (IsEnabled)
				{
					Radiobox_MouseLeaveAnimation();
					return;
				}
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, ModSecret.ColorGray5 - ShapeBorder.Stroke, 200), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, ModSecret.ColorGray5 - LabText.Foreground, 200), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(Uuid));
				LabText.SetResourceReference(TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
				Ellipse shapeBorder = ShapeBorder;
				DependencyProperty strokeProperty = Shape.StrokeProperty;
				string name;
				if (IsEnabled)
				{
					name = (Checked ? "ColorBrush2" : "ColorBrush1");
				}
				else
				{
					name = "ColorBrushGray5";
				}
				shapeBorder.SetResourceReference(strokeProperty, name);
			}
		}
		private void Radiobox_MouseEnterAnimation()
		{
			ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, "ColorBrush3", 100), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
		}
		private void Radiobox_MouseLeaveAnimation()
		{
			if (IsEnabled)
			{
				if (IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					Ellipse shapeBorder = ShapeBorder;
					DependencyProperty strokeProperty = Shape.StrokeProperty;
					string res;
					if (IsEnabled)
					{
						res = (Checked ? "ColorBrush2" : "ColorBrush1");
					}
					else
					{
						res = "ColorBrushGray5";
					}
					ModAnimation.AniStart(ModAnimation.AaColor(shapeBorder, strokeProperty, res, 200), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5", 200), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
					return;
				}
				ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(Uuid));
				Ellipse shapeBorder2 = ShapeBorder;
				DependencyProperty strokeProperty2 = Shape.StrokeProperty;
				string name;
				if (IsEnabled)
				{
					name = (Checked ? "ColorBrush2" : "ColorBrush1");
				}
				else
				{
					name = "ColorBrushGray5";
				}
				shapeBorder2.SetResourceReference(strokeProperty2, name);
				LabText.SetResourceReference(TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
			}
		}
		private void RefreshSetting(string NewValue)
		{
			Checked = Operators.CompareString(NewValue, SettingService.GetValue(this), TextCompare: false) == 0;
		}
		void ISettingControl.RefreshSetting(string NewValue)
		{
			//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
			this.RefreshSetting(NewValue);
		}
		private string GetCurrentSetting()
		{
			if (!Checked)
			{
				return null;
			}
			return SettingService.GetValue(this);
		}
		string ISettingControl.GetCurrentSetting()
		{
			//ILSpy generated this explicit interface implementation from .override directive in GetCurrentSetting
			return this.GetCurrentSetting();
		}
		private void SaveSetting()
		{
			SettingService.SaveSetting(this);
		}
	}
}
