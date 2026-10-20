// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class MyMsgInput : Grid
	{
		private readonly ModMain.MyMsgBoxConverter MyConverter;
		private readonly int Uuid;













		public MyMsgInput(ModMain.MyMsgBoxConverter Converter)
		{
			Loaded += Load;
			Uuid = ModBase.GetUuid();
			try
			{
				InitializeComponent();
			InitConnectEvents();
				Btn1.Name += Conversions.ToString(ModBase.GetUuid());
				Btn2.Name += Conversions.ToString(ModBase.GetUuid());
				MyConverter = Converter;
				LabTitle.Text = Converter.Title;
				LabText.Text = Converter.Text;
				PanText.Visibility = ((Operators.CompareString(Converter.Text, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				TextArea.Text = Conversions.ToString(Converter.Content);
				TextArea.HintText = Converter.HintText;
				TextArea.ValidateRules = Converter.ValidateRules;
				Btn1.Text = Converter.Button1;
				if (Converter.IsWarn)
				{
					Btn1.ColorType = MyButton.ColorState.Red;
					LabTitle.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrushRedLight");
				}
				Btn2.Text = Converter.Button2;
				Btn2.Visibility = ((Operators.CompareString(Converter.Button2, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
				ShapeLine.StrokeThickness = ModBase.GetWPFSize(1.0);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "输入弹窗初始化失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void Load(object sender, EventArgs e)
		{
			try
			{
				if (Btn2.IsVisible && Btn1.ColorType != MyButton.ColorState.Red)
				{
					Btn1.ColorType = MyButton.ColorState.Highlight;
				}
				TextArea.Focus();
				TextArea.SelectionStart = TextArea.Text.Length;
				Opacity = 0.0;
				ModAnimation.AniStart(ModAnimation.AaColor(ModMain.FrmMain.PanMsg, System.Windows.Controls.Panel.BackgroundProperty, (MyConverter.IsWarn ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain.FrmMain.PanMsg.Background, 200), "PanMsg Background");
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(this, 1.0, 120, 60),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						TranslateTransform transformPos;
						(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
					}, 0.0 - TransformPos.Y, 300, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						RotateTransform transformRotate;
						(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
					}, 0.0 - TransformRotate.Angle, 300, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
				}, "MyMsgBox " + Conversions.ToString(Uuid));
				Logger.Info($"输入弹窗：{LabTitle.Text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "输入弹窗加载失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void Close()
		{
			MyConverter.WaitFrame.Continue = false;
			ComponentDispatcher.PopModal();
			ModAnimation.AniStart(new ModAnimation.AniData[5]
			{
				ModAnimation.AaCode([SpecialName] () =>
				{
					if (!ModMain.WaitingMyMsgBox.Any())
					{
						ModAnimation.AniStart(ModAnimation.AaColor(ModMain.FrmMain.PanMsg, System.Windows.Controls.Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain.FrmMain.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
					}
				}, 30),
				ModAnimation.AaOpacity(this, 0.0 - Opacity, 80, 20),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					TranslateTransform transformPos;
					(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, 20.0 - TransformPos.Y, 150, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					RotateTransform transformRotate;
					(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, 6.0 - TransformRotate.Angle, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					((Grid)Parent).Children.Remove(this);
				}, 0, After: true)
			}, "MyMsgBox " + Conversions.ToString(Uuid));
		}
		public void Btn1_Click()
		{
			TextArea.Validate();
			if (!MyConverter.IsExited && TextArea.IsValidated)
			{
				MyConverter.IsExited = true;
				MyConverter.Result = TextArea.Text;
				Close();
			}
		}
		public void Btn2_Click()
		{
			if (!MyConverter.IsExited)
			{
				MyConverter.IsExited = true;
				MyConverter.Result = null;
				Close();
			}
		}
		private void TextCaption_ValidateChanged(object sender, EventArgs e)
		{
			Btn1.IsEnabled = TextArea.IsValidated;
		}
		private void Drag(object sender, MouseButtonEventArgs e)
		{
			int try0000_dispatch = -1;
			int num3 = default;
			int num = default;
			int num2 = default;
			while (true)
			{
				try
				{
					/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
					switch (try0000_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 1;
						goto IL_0007;
					case 86:
						{
							num = num2;
							switch (num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0000;
							}
							int num4 = num + 1;
							num = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_0007;
							case 3:
								goto end_IL_0000_2;
							default:
								goto end_IL_0000;
							case 4:
								goto end_IL_0000_3;
							}
							goto default;
						}
						IL_0007:
						num2 = 2;
						if (!(e.GetPosition(ShapeLine).Y <= 2.0))
						{
							goto end_IL_0000_3;
						}
						break;
						end_IL_0000_2:
						break;
					}
					num2 = 3;
					ModMain.FrmMain.DragMove();
					break;
					end_IL_0000:;
				}
				catch (Exception ex) when ((num3 != 0) & (num == 0))
				{
					ProjectData.SetProjectError(ex);
					try0000_dispatch = 86;
					continue;
				}
				throw ProjectData.CreateProjectError(-2146828237);
				continue;
				end_IL_0000_3:
				break;
			}
			if (num != 0)
			{
				ProjectData.ClearProjectError();
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MouseButtonEventHandler value2 = Drag;
						Border border = PanBorder;
						if (border != null)
						{
							border.MouseLeftButtonDown -= value2;
						}
						border = PanBorder;
						if (border != null)
						{
							border.MouseLeftButtonDown += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = Drag;
						TextBlock textBlock = LabTitle;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonDown -= value2;
						}
						textBlock = LabTitle;
						if (textBlock != null)
						{
							textBlock.MouseLeftButtonDown += value2;
						}
			
			}
			{
						MyTextBox.ValidateChangedEventHandler obj = TextCaption_ValidateChanged;
						if (TextArea != null)
						{
							MyTextBox.ValidateChanged -= obj;
						}
						if (TextArea != null)
						{
							MyTextBox.ValidateChanged += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Btn1_Click();
						};
						MyButton myButton = Btn1;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = Btn1;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Btn2_Click();
						};
						MyButton myButton = Btn2;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = Btn2;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
		}

	}
}
