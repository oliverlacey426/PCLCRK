// MeloongCore.Wpf

namespace MeloongCore.Wpf
{
	public static class Dialogs
	{
		public static string? SaveFile(string title, string defaultFileName = "", string? defaultDirectory = null, IEnumerable<(string Extension, string Display)>? filter = null)
		{
			List<(string, string)> list = filter?.ToList();
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				AddExtension = true,
				Title = title,
				FileName = defaultFileName
			};
			if (!string.IsNullOrEmpty(defaultDirectory) && DirectoryUtils.Exists(defaultDirectory))
			{
				saveFileDialog.InitialDirectory = PathUtils.ToShortPath(defaultDirectory);
			}
			if (list != null)
			{
				saveFileDialog.Filter = list.Select(((string Extension, string Display) f) => f.Display + "(*." + f.Extension + ")|*." + f.Extension).Join("|");
			}
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(44, 4, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("保存弹窗：");
				handler.AppendFormatted(saveFileDialog.Title);
				handler.AppendLiteral("（FileName=");
				handler.AppendFormatted(saveFileDialog.FileName);
				handler.AppendLiteral(", InitialDirectory=");
				handler.AppendFormatted(saveFileDialog.InitialDirectory);
				handler.AppendLiteral(", Filter=");
				handler.AppendFormatted(saveFileDialog.Filter);
				handler.AppendLiteral("）");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			if (saveFileDialog.ShowDialog() != true)
			{
				return null;
			}
			string result = saveFileDialog.FileName;
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(7, 1, out isEnabled);
			if (isEnabled)
			{
				handler2.AppendLiteral("保存弹窗返回：");
				handler2.AppendFormatted(result);
			}
			Logger.Info(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			if (!result.Contains(Path.DirectorySeparatorChar))
			{
				return null;
			}
			if (list != null && !string.IsNullOrEmpty(result) && !list.Any(((string Extension, string Display) f) => result.EndsWithF("." + f.Extension)))
			{
				result = result + "." + list[saveFileDialog.FilterIndex - 1].Item1;
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(24, 1, out isEnabled);
				if (isEnabled)
				{
					handler3.AppendLiteral("选择文件的返回无扩展名，将会手动添加，修改后为：");
					handler3.AppendFormatted(result);
				}
				Logger.Warn(ref handler3, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			}
			return result;
		}
		public static List<string> SelectFile(string title, bool multiselect, string? defaultDirectory = null, OneOf<IEnumerable<(string[] Extensions, string Display)>, string>? filter = null)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				AddExtension = true,
				CheckFileExists = true,
				Multiselect = multiselect,
				Title = title,
				ValidateNames = true
			};
			if (!string.IsNullOrEmpty(defaultDirectory) && DirectoryUtils.Exists(defaultDirectory))
			{
				openFileDialog.InitialDirectory = PathUtils.ToShortPath(defaultDirectory);
			}
			if (filter.HasValue)
			{
				if (filter.Value.Is<string>())
				{
					openFileDialog.Filter = filter.Value.As<string>();
				}
				else
				{
					openFileDialog.Filter = filter.Value.As<IEnumerable<(string[], string)>>().Select(((string[] Extensions, string Display) f) =>
					{
						string text = string.Join(";", f.Extensions.Select((string e) => "*." + e));
						return f.Display + "(" + text + ")|" + text;
					}).Join("|");
				}
			}
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(49, 4, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("选择文件弹窗：");
				handler.AppendFormatted(openFileDialog.Title);
				handler.AppendLiteral("（Multiselect=");
				handler.AppendFormatted(openFileDialog.Multiselect);
				handler.AppendLiteral(", InitialDirectory=");
				handler.AppendFormatted(openFileDialog.InitialDirectory);
				handler.AppendLiteral(", Filter=");
				handler.AppendFormatted(openFileDialog.Filter);
				handler.AppendLiteral("）");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			if (openFileDialog.ShowDialog() != true)
			{
				return new List<string>();
			}
			List<string> list = openFileDialog.FileNames.Where((string f) => f.Contains(Path.DirectorySeparatorChar)).ToList();
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(9, 1, out isEnabled);
			if (isEnabled)
			{
				handler2.AppendLiteral("选择文件弹窗返回：");
				handler2.AppendFormatted(list.Join(","));
			}
			Logger.Info(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			return list;
		}
		public static List<string> SelectFolder(string title, bool multiselect, string? defaultDirectory = null)
		{
			VistaFolderBrowserDialog vistaFolderBrowserDialog = new VistaFolderBrowserDialog
			{
				ShowNewFolderButton = true,
				Description = title,
				Multiselect = multiselect,
				UseDescriptionForTitle = true
			};
			if (!string.IsNullOrEmpty(defaultDirectory) && DirectoryUtils.Exists(defaultDirectory))
			{
				vistaFolderBrowserDialog.SelectedPath = defaultDirectory;
			}
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(37, 3, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("选择文件夹弹窗：");
				handler.AppendFormatted(vistaFolderBrowserDialog.Description);
				handler.AppendLiteral("（Multiselect=");
				handler.AppendFormatted(vistaFolderBrowserDialog.Multiselect);
				handler.AppendLiteral(", SelectedPath=");
				handler.AppendFormatted(vistaFolderBrowserDialog.SelectedPath);
				handler.AppendLiteral("）");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			if (vistaFolderBrowserDialog.ShowDialog() != true)
			{
				return new List<string>();
			}
			List<string> list = vistaFolderBrowserDialog.SelectedPaths.Where((string f) => f.Contains(Path.DirectorySeparatorChar)).Select(PathUtils.AddSlashSuffix).ToList();
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(10, 1, out isEnabled);
			if (isEnabled)
			{
				handler2.AppendLiteral("选择文件夹弹窗返回：");
				handler2.AppendFormatted(list.Join(","));
			}
			Logger.Info(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Wpf\\General\\Dialogs.cs");
			return list;
		}
	}
}
