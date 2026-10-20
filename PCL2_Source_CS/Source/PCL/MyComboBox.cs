// PCL

namespace PCL
{
	public class MyComboBox : System.Windows.Controls.ComboBox, ISettingControl
	{
		public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs e);
		public int Uuid;
		private MyTextBox TextBox;
		private string _Text;
		private bool IsMouseDown;
		private double RealWidth;
		private bool IsTextChanging;
		public string HintText { get; set; }
		public new string Text
		{
			get
			{
				if (IsEditable)
				{
					if (TextBox == null)
					{
						return _Text ?? "";
					}
					return TextBox.Text ?? "";
				}
				return (SelectedItem ?? "").ToString();
			}
			set
			{
				if (IsEditable)
				{
					if (Information.IsNothing(TextBox))
					{
						_Text = value;
					}
					else
					{
						TextBox.Text = value;
					}
					return;
				}
				throw new NotSupportedException("该 ComboBox 不支持修改文本。");
			}
		}
		public ContentPresenter ContentPresenter => (ContentPresenter)Template.FindName("PART_Content", this);
		public event TextChangedEventHandler TextChanged;
		public MyComboBox()
		{
			PreviewMouseLeftButtonDown += MyComboBox_PreviewMouseLeftButtonDown;
			PreviewMouseLeftButtonUp += MyComboBox_PreviewMouseLeftButtonUp;
			MouseLeave += MyComboBox_PreviewMouseLeftButtonUp;
			IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseEnter += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseLeave += [SpecialName] (object a0, System.Windows.Input.MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			PreviewMouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				RefreshColor();
			};
			PreviewMouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				RefreshColor();
			};
			GotKeyboardFocus += [SpecialName] (object a0, KeyboardFocusChangedEventArgs a1) =>
			{
				RefreshColor();
			};
			DropDownOpened += MyComboBox_DropDownOpened;
			DropDownClosed += MyComboBox_DropDownClosed;
			TextChanged += MyComboBox_TextChanged;
			SelectionChanged += MyComboBox_SelectionChanged;
			SelectionChanged += [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				SaveSetting();
			};
			Uuid = ModBase.GetUuid();
			HintText = "";
			_Text = Conversions.ToString(SelectedItem);
			IsMouseDown = false;
			IsTextChanging = false;
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (!IsEditable)
			{
				return;
			}
			try
			{
				TextBox = (MyTextBox)Template.FindName("PART_EditableTextBox", this);
				TextBox.AddHandler(UIElement.LostFocusEvent, (RoutedEventHandler)([SpecialName] (object a0, RoutedEventArgs a1) =>
				{
					RefreshColor();
				}));
				TextBox.ChangedEventList.Add([SpecialName] (object sender, RoutedEventArgs e) =>
				{
					TextChanged?.Invoke(RuntimeHelpers.GetObjectValue(sender), (TextChangedEventArgs)e);
				});
				TextBox.Tag = RuntimeHelpers.GetObjectValue(Tag);
				if (Operators.CompareString(Text, "", TextCompare: false) == 0)
				{
					TextBox.Text = _Text;
				}
				else
				{
					TextChanged?.Invoke(this, null);
				}
				if (HintText.Length > 0)
				{
					TextBox.HintText = HintText;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, string.Format("初始化可编辑文本框失败（{0}）", Name ?? ""), LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void MyComboBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsMouseDown = true;
		}
		private void MyComboBox_PreviewMouseLeftButtonUp(object sender, EventArgs e)
		{
			IsMouseDown = false;
		}
		public void RefreshColor()
		{
			string text;
			string text2;
			int time;
			if (IsEnabled)
			{
				if (Conversions.ToBoolean(IsMouseDown || IsDropDownOpen || (IsEditable && Conversions.ToBoolean(NewLateBinding.LateGet(Template.FindName("PART_EditableTextBox", this), null, "IsFocused", new object[0], null, null, null)))))
				{
					text = "ColorBrush3";
					text2 = "ColorBrush7";
					time = 10;
				}
				else if (IsMouseOver)
				{
					text = "ColorBrush4";
					text2 = "ColorBrush7";
					time = 100;
				}
				else
				{
					text = "ColorBrushBg0";
					text2 = "ColorBrushHalfWhite";
					time = 100;
				}
			}
			else
			{
				text = "ColorBrushGray5";
				text2 = "ColorBrushGray6";
				time = 200;
			}
			if (IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(this, System.Windows.Controls.Control.ForegroundProperty, text, time),
					ModAnimation.AaColor(this, System.Windows.Controls.Control.BackgroundProperty, text2, time)
				}, "MyComboBox Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyComboBox Color " + Conversions.ToString(Uuid));
				SetResourceReference(System.Windows.Controls.Control.ForegroundProperty, text);
				SetResourceReference(System.Windows.Controls.Control.BackgroundProperty, text2);
			}
		}
		private void MyComboBox_DropDownOpened(object sender, EventArgs e)
		{
			RealWidth = Width;
			Width = ActualWidth;
			try
			{
				((Grid)Template.FindName("PanPopup", this)).Opacity = ModMain.FrmMain.Opacity;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "设置下拉框透明度失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void MyComboBox_DropDownClosed(object sender, EventArgs e)
		{
			Width = RealWidth;
		}
		private void MyComboBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!IsTextChanging && IsEditable && SelectedItem != null && Operators.CompareString(Text, SelectedItem.ToString(), TextCompare: false) != 0)
			{
				string text = Text;
				int selectionStart = TextBox.SelectionStart;
				IsTextChanging = true;
				SelectedItem = null;
				Text = text;
				TextBox.SelectionStart = selectionStart;
				IsTextChanging = false;
			}
		}
		private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModMain.RaiseCustomEvent(this);
			}
		}
		private void RefreshSetting(string NewValue)
		{
			DependencyObject dependencyObject = Items.OfType<DependencyObject>().FirstOrDefault([SpecialName] (DependencyObject i) => ModBase.Val(RuntimeHelpers.GetObjectValue(SettingService.GetValue(i) ?? ((object)(-1)))) == Conversions.ToDouble(NewValue));
			if (dependencyObject != null)
			{
				SelectedItem = dependencyObject;
				return;
			}
			if ((double)Items.Count > Conversions.ToDouble(NewValue))
			{
				SelectedIndex = Conversions.ToInteger(NewValue);
				return;
			}
			Logger.Warn($"尝试显示设置 {SettingService.GetKey(this)}，但没有找到设置值 {NewValue} 的对应项", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
			SelectedIndex = -1;
			SelectedItem = null;
		}
		void ISettingControl.RefreshSetting(string NewValue)
		{
			//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
			this.RefreshSetting(NewValue);
		}
		private string GetCurrentSetting()
		{
			if (SelectedItem == null)
			{
				return null;
			}
			if (SelectedItem is DependencyObject)
			{
				return Conversions.ToString(SettingService.GetValue((DependencyObject)SelectedItem) ?? ((object)SelectedIndex));
			}
			return Conversions.ToString(SelectedIndex);
		}
		string ISettingControl.GetCurrentSetting()
		{
			//ILSpy generated this explicit interface implementation from .override directive in GetCurrentSetting
			return this.GetCurrentSetting();
		}
		private void SaveSetting()
		{
			if (SelectedItem != null)
			{
				SettingService.SaveSetting(this);
			}
		}
	}
}
