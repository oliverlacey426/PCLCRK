// PCL.Controls.Behaviors

namespace PCL.Controls.Behaviors
{
	public sealed class ClipboardInterceptor
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _Closure___
		{
			public static readonly _Closure___ _I;
			public static Func<DataGridCellInfo, object> _I15_0;
			public static Func<DataGridCellInfo, string> _I15_1;
			static _Closure___()
			{
				_I = new _Closure___();
			}
			[SpecialName]
			internal object _Lambda___15_0(DataGridCellInfo c)
			{
				return c.Item;
			}
			[SpecialName]
			internal string _Lambda___15_1(DataGridCellInfo cell)
			{
				if (!(cell.Column.GetCellContent(RuntimeHelpers.GetObjectValue(cell.Item)) is TextBlock textBlock))
				{
					return "";
				}
				return textBlock.Text;
			}
		}
		public static readonly DependencyProperty EnableSafeClipboardProperty = DependencyProperty.RegisterAttached("EnableSafeClipboard", typeof(bool), typeof(ClipboardInterceptor), new PropertyMetadata(false, OnEnableSafeClipboardChanged));
		private ClipboardInterceptor()
		{
		}
		public static void SetEnableSafeClipboard(DependencyObject element, bool value)
		{
			element.SetValue(EnableSafeClipboardProperty, value);
		}
		public static bool GetEnableSafeClipboard(DependencyObject element)
		{
			return Conversions.ToBoolean(element.GetValue(EnableSafeClipboardProperty));
		}
		private static void OnEnableSafeClipboardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is System.Windows.Controls.TextBox && Conversions.ToBoolean(e.NewValue))
			{
				AddCommandBindingsToTextBox((System.Windows.Controls.TextBox)d);
			}
			else if (d is System.Windows.Controls.RichTextBox && Conversions.ToBoolean(e.NewValue))
			{
				AddCommandBindingsToRichTextBox((System.Windows.Controls.RichTextBox)d);
			}
			else if (d is System.Windows.Controls.DataGrid && Conversions.ToBoolean(e.NewValue))
			{
				AddCommandBindingsToDataGrid((System.Windows.Controls.DataGrid)d);
			}
		}
		private static void AddCommandBindingsToTextBox(System.Windows.Controls.TextBox tb)
		{
			tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyTextBox));
			tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCutTextBox));
			tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteTextBox));
		}
		private static void AddCommandBindingsToRichTextBox(System.Windows.Controls.RichTextBox rtb)
		{
			rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyRichTextBox));
			rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCutRichTextBox));
			rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteRichTextBox));
		}
		private static void AddCommandBindingsToDataGrid(System.Windows.Controls.DataGrid dg)
		{
			dg.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyDataGrid));
		}
		private static void OnCopyTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (sender is System.Windows.Controls.TextBox { SelectionLength: >0 } textBox)
			{
				try
				{
					System.Windows.Forms.Clipboard.Clear();
					System.Windows.Forms.Clipboard.SetDataObject(textBox.SelectedText, copy: true);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "复制失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
					ProjectData.ClearProjectError();
				}
				e.Handled = true;
			}
		}
		private static void OnCutTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (sender is System.Windows.Controls.TextBox { SelectionLength: >0 } textBox)
			{
				try
				{
					System.Windows.Forms.Clipboard.Clear();
					System.Windows.Forms.Clipboard.SetDataObject(textBox.SelectedText, copy: true);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "剪切失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
					ProjectData.ClearProjectError();
				}
				textBox.SelectedText = string.Empty;
				e.Handled = true;
			}
		}
		private static void OnPasteTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (!(sender is System.Windows.Controls.TextBox textBox))
			{
				return;
			}
			try
			{
				if (System.Windows.Forms.Clipboard.ContainsText())
				{
					string text = System.Windows.Forms.Clipboard.GetText();
					if (!textBox.AcceptsReturn)
					{
						text = text.ReplaceLineEndings(" ", mergeMultiple: true);
					}
					int selectionStart = textBox.SelectionStart;
					textBox.SelectedText = text;
					textBox.CaretIndex = checked(selectionStart + text.Length);
					textBox.SelectionLength = 0;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "粘贴失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
			}
			e.Handled = true;
		}
		private static void OnCopyRichTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
			{
				return;
			}
			TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
			if (!string.IsNullOrEmpty(textRange.Text))
			{
				try
				{
					System.Windows.Forms.Clipboard.Clear();
					System.Windows.Forms.Clipboard.SetDataObject(textRange.Text, copy: true);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "复制失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
					ProjectData.ClearProjectError();
				}
				e.Handled = true;
			}
		}
		private static void OnCutRichTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
			{
				return;
			}
			TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
			if (!string.IsNullOrEmpty(textRange.Text))
			{
				try
				{
					System.Windows.Forms.Clipboard.Clear();
					System.Windows.Forms.Clipboard.SetDataObject(textRange.Text, copy: true);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "剪切失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
					ProjectData.ClearProjectError();
				}
				textRange.Text = string.Empty;
				e.Handled = true;
			}
		}
		private static void OnPasteRichTextBox(object sender, ExecutedRoutedEventArgs e)
		{
			if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
			{
				return;
			}
			try
			{
				if (!System.Windows.Forms.Clipboard.ContainsText())
				{
					return;
				}
				string text = System.Windows.Forms.Clipboard.GetText();
				TextSelection selection = richTextBox.Selection;
				selection.Text = text;
				TextPointer textPointer = (richTextBox.CaretPosition = selection.End);
				richTextBox.Selection.Select(textPointer, textPointer);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "粘贴失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
			}
			e.Handled = true;
		}
		private static void OnCopyDataGrid(object sender, ExecutedRoutedEventArgs e)
		{
			if (!(sender is System.Windows.Controls.DataGrid { SelectedCells: not null } dataGrid) || dataGrid.SelectedCells.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerable<IGrouping<object, DataGridCellInfo>> enumerable = from c in dataGrid.SelectedCells
				group c by c.Item;
			foreach (IGrouping<object, DataGridCellInfo> item in enumerable)
			{
				string value = string.Join("\t", item.Select((_Closure___._I15_1 == null) ? (_Closure___._I15_1 = [SpecialName] (DataGridCellInfo cell) => (!(cell.Column.GetCellContent(RuntimeHelpers.GetObjectValue(cell.Item)) is TextBlock textBlock)) ? "" : textBlock.Text) : _Closure___._I15_1));
				stringBuilder.AppendLine(value);
			}
			string data = stringBuilder.ToString().TrimEnd('\r', '\n');
			try
			{
				System.Windows.Forms.Clipboard.Clear();
				System.Windows.Forms.Clipboard.SetDataObject(data, copy: true);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "复制失败，请稍后再试", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
			}
			e.Handled = true;
		}
	}
}
