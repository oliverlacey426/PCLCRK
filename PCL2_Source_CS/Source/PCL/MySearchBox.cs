// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class MySearchBox : MyCard
	{
		public delegate void TextChangedEventHandler(object sender, EventArgs e);
		public string HintText
		{
			get
			{
				return TextBox.HintText;
			}
			set
			{
				TextBox.HintText = value;
			}
		}
		public string Text
		{
			get
			{
				return TextBox.Text;
			}
			set
			{
				TextBox.Text = value;
			}
		}


		public event TextChangedEventHandler TextChanged;
		public MySearchBox()
		{
			Loaded += MySearchBox_Loaded;
			KeyUp += MySearchBox_KeyUp;
			InitializeComponent();
			InitConnectEvents();
		}
		private void MySearchBox_Loaded(object sender, RoutedEventArgs e)
		{
			TextBox.Focus();
		}
		private void Text_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(TextBox.Text))
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(BtnClear, 0.0 - BtnClear.Opacity, 90), "MySearchBox ClearBtn " + Conversions.ToString(Uuid));
				BtnClear.IsHitTestVisible = false;
			}
			else
			{
				ModAnimation.AniStart(ModAnimation.AaOpacity(BtnClear, 1.0 - BtnClear.Opacity, 90), "MySearchBox ClearBtn " + Conversions.ToString(Uuid));
				BtnClear.IsHitTestVisible = true;
			}
			TextChanged?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
		}
		private void BtnClear_Click(object sender, EventArgs e)
		{
			TextBox.Text = "";
			TextBox.Focus();
		}
		private void MySearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				ModMain.RaiseCustomEvent(this);
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						System.Windows.Controls.TextChangedEventHandler value2 = Text_TextChanged;
						MyTextBox myTextBox = TextBox;
						if (myTextBox != null)
						{
							myTextBox.TextChanged -= value2;
						}
						myTextBox = TextBox;
						if (myTextBox != null)
						{
							myTextBox.TextChanged += value2;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = BtnClear_Click;
						MyIconButton myIconButton = BtnClear;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnClear;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
		}

	}
}
