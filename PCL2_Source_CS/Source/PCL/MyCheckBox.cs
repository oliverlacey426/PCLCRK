// PCL

namespace PCL
{
	[ContentProperty("Inlines")]
	[DesignerGenerated]
	public partial class MyCheckBox : Grid, ISettingControl
	{
		public delegate void ChangeEventHandler(object sender, bool user);
		public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);
		public int Uuid;
		public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(MyCheckBox), new PropertyMetadata(false, [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
		{
			((VB_AnonymousDelegate_6<MyCheckBox, DependencyPropertyChangedEventArgs>)([SpecialName] (MyCheckBox d, DependencyPropertyChangedEventArgs e) =>
			{
				if (!d.IsLoaded)
				{
					d.SyncUI();
				}
			}))((MyCheckBox)a0, a1);
		}));
		private const int AnimationTimeOfCheck = 150;
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyCheckBox), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (!Information.IsNothing(sender))
			{
				((MyCheckBox)sender).LabText.Text = Conversions.ToString(e.NewValue);
			}
		}));
		private bool MouseDowned;
		private bool AllowMouseDown;
		private const int AnimationTimeOfMouseIn = 100;
		private const int AnimationTimeOfMouseOut = 200;
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




		public event ChangeEventHandler Change;
		public event PreviewChangeEventHandler PreviewChange;
		public MyCheckBox()
		{
			MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Checkbox_MouseUp();
			};
			MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Checkbox_MouseDown();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Checkbox_MouseLeave();
			};
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				Checkbox_IsEnabledChanged();
			};
			MouseEnter += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Checkbox_MouseEnterAnimation();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				Checkbox_MouseLeaveAnimation();
			};
			Change += [SpecialName] (object a0, bool a1) =>
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
				if (value == Checked)
				{
					return;
				}
				if (value && user)
				{
					ModBase.RouteEventArgs e = new ModBase.RouteEventArgs(user);
					PreviewChange?.Invoke(this, e);
					if (e.Handled)
					{
						MouseDowned = true;
						Checkbox_MouseLeave();
						MouseDowned = false;
						return;
					}
				}
				SetValue(CheckedProperty, value);
				if (IsLoaded)
				{
					Change?.Invoke(this, user);
				}
				SyncUI();
				ModMain.RaiseCustomEvent(this);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "设置 Checked 失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyCheckBox.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void SyncUI()
		{
			if (ModAnimation.AniControlEnabled == 0 && IsLoaded)
			{
				AllowMouseDown = false;
				if (Checked)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaScale(ShapeBorder, 12.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(), After: false, Absolute: true),
						ModAnimation.AaScaleTransform(ShapeCheck, 1.0 - ((ScaleTransform)ShapeCheck.RenderTransform).ScaleX, 300, 105, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaScale(ShapeBorder, 6.0, 300, 105, new ModAnimation.AniEaseOutBack(), After: false, Absolute: true)
					}, "MyCheckBox Scale " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Border.BorderBrushProperty, (!IsEnabled) ? "ColorBrushGray5" : (IsMouseOver ? "ColorBrush3" : "ColorBrush2"), 150) }, "MyCheckBox BorderColor " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaCode([SpecialName] () =>
					{
						AllowMouseDown = true;
					}, 300) }, "MyCheckBox AllowMouseDown " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaScale(ShapeBorder, 12.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(), After: false, Absolute: true),
						ModAnimation.AaScaleTransform(ShapeCheck, 0.0 - ((ScaleTransform)ShapeCheck.RenderTransform).ScaleX, 135, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaScale(ShapeBorder, 6.0, 300, 105, new ModAnimation.AniEaseOutBack(), After: false, Absolute: true)
					}, "MyCheckBox Scale " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Border.BorderBrushProperty, (!IsEnabled) ? "ColorBrushGray5" : (IsMouseOver ? "ColorBrush3" : "ColorBrush1"), 150) }, "MyCheckBox BorderColor " + Conversions.ToString(Uuid));
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaCode([SpecialName] () =>
					{
						AllowMouseDown = true;
					}, 300) }, "MyCheckBox AllowMouseDown " + Conversions.ToString(Uuid));
				}
			}
			else
			{
				ModAnimation.AniStop("MyCheckBox Scale " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyCheckBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyCheckBox AllowMouseDown " + Conversions.ToString(Uuid));
				if (Checked)
				{
					((ScaleTransform)ShapeCheck.RenderTransform).ScaleX = 1.0;
					((ScaleTransform)ShapeCheck.RenderTransform).ScaleY = 1.0;
					ShapeBorder.SetResourceReference(Border.BorderBrushProperty, IsEnabled ? "ColorBrush2" : "ColorBrushGray5");
				}
				else
				{
					((ScaleTransform)ShapeCheck.RenderTransform).ScaleX = 0.0;
					((ScaleTransform)ShapeCheck.RenderTransform).ScaleY = 0.0;
					ShapeBorder.SetResourceReference(Border.BorderBrushProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
				}
			}
		}
		private void Checkbox_MouseUp()
		{
			if (MouseDowned)
			{
				Logger.Info($"按下复选框（{!Checked}）：{Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyCheckBox.xaml.vb");
				MouseDowned = false;
				SetChecked(!Checked, user: true);
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Border.BackgroundProperty, "ColorBrushHalfWhite", 100), "MyCheckBox Background " + Conversions.ToString(Uuid));
			}
		}
		private void Checkbox_MouseDown()
		{
			if (AllowMouseDown)
			{
				MouseDowned = true;
				Focus();
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Border.BackgroundProperty, "ColorBrushBg1", 100), "MyCheckBox Background " + Conversions.ToString(Uuid));
				if (Checked)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScale(ShapeBorder, 16.5 - ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true),
						ModAnimation.AaScaleTransform(ShapeCheck, 0.9 - ((ScaleTransform)ShapeCheck.RenderTransform).ScaleX, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
					}, "MyCheckBox Scale " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 16.5 - ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyCheckBox Scale " + Conversions.ToString(Uuid));
				}
			}
		}
		private void Checkbox_MouseLeave()
		{
			if (MouseDowned)
			{
				MouseDowned = false;
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Border.BackgroundProperty, "ColorBrushHalfWhite", 100), "MyCheckBox Background " + Conversions.ToString(Uuid));
				if (Checked)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true),
						ModAnimation.AaScaleTransform(ShapeCheck, 1.0 - ((ScaleTransform)ShapeCheck.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
					}, "MyCheckBox Scale " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyCheckBox Scale " + Conversions.ToString(Uuid));
				}
			}
		}
		private void Checkbox_IsEnabledChanged()
		{
			if (IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				if (IsEnabled)
				{
					Checkbox_MouseLeaveAnimation();
					return;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Border.BorderBrushProperty, ModSecret.ColorGray5 - ShapeBorder.BorderBrush, 200) }, "MyCheckBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, ModSecret.ColorGray5 - LabText.Foreground, 200) }, "MyCheckBox TextColor " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyCheckBox TextColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStop("MyCheckBox BorderColor " + Conversions.ToString(Uuid));
				LabText.SetResourceReference(TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
				Border shapeBorder = ShapeBorder;
				DependencyProperty borderBrushProperty = Border.BorderBrushProperty;
				string name;
				if (IsEnabled)
				{
					name = (Checked ? "ColorBrush2" : "ColorBrush1");
				}
				else
				{
					name = "ColorBrushGray5";
				}
				shapeBorder.SetResourceReference(borderBrushProperty, name);
			}
		}
		private void Checkbox_MouseEnterAnimation()
		{
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100) }, "MyCheckBox TextColor " + Conversions.ToString(Uuid));
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Border.BorderBrushProperty, "ColorBrush3", 100) }, "MyCheckBox BorderColor " + Conversions.ToString(Uuid));
		}
		private void Checkbox_MouseLeaveAnimation()
		{
			if (IsEnabled)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, IsEnabled ? "ColorBrush1" : "ColorBrushGray5", 200) }, "MyCheckBox TextColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Border.BorderBrushProperty, (!IsEnabled) ? "ColorBrushGray5" : (Checked ? "ColorBrush2" : "ColorBrush1"), 200) }, "MyCheckBox BorderColor " + Conversions.ToString(Uuid));
			}
		}
		private void RefreshSetting(string NewValue)
		{
			Checked = bool.Parse(NewValue);
		}
		void ISettingControl.RefreshSetting(string NewValue)
		{
			//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
			this.RefreshSetting(NewValue);
		}
		private string GetCurrentSetting()
		{
			return Checked.ToString();
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
