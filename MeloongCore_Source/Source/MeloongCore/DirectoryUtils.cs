// MeloongCore

namespace MeloongCore
{
	public static class DirectoryUtils
	{
		private static readonly Lazy<HashSet<string>> criticalFolders = new Lazy<HashSet<string>>(() => new HashSet<string>(new string[6]
		{
			Paths.Base,
			Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
			Environment.GetFolderPath(Environment.SpecialFolder.Personal),
			Environment.GetFolderPath(Environment.SpecialFolder.Windows),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
		}.Where((string f) => Enumerable.Contains(f, Path.VolumeSeparatorChar))));
		public static void Create(string folder)
		{
			if (!Exists(folder))
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(6, 1, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("新建文件夹：");
					handler.AppendFormatted(folder);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				Directory.CreateDirectory(PathUtils.ForApi(folder));
			}
		}
		public static string CreateRandom()
		{
			string text = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Create(text);
			return text;
		}
		public static bool Exists(string folder)
		{
			return Directory.Exists(PathUtils.ForApi(folder));
		}
		public static DirectoryInfo GetInfo(string folder)
		{
			return new DirectoryInfo(PathUtils.ForApi(folder));
		}
		public static IEnumerable<string> EnumerateFiles(string folder, bool includeSubDirectories = false, string searchPattern = "*")
		{
			if (!Exists(folder))
			{
				return Array.Empty<string>();
			}
			return Directory.EnumerateFiles(PathUtils.ForApi(folder), searchPattern, includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Select(PathUtils.RemoveExtendedPrefix);
		}
		public static IEnumerable<string> EnumerateDirectories(string folder, bool includeSubDirectories = false, string searchPattern = "*")
		{
			if (!Exists(folder))
			{
				return Array.Empty<string>();
			}
			return Directory.EnumerateDirectories(PathUtils.ForApi(folder), searchPattern, includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Select(PathUtils.RemoveExtendedPrefix);
		}
		public static bool IsEmpty(string folder)
		{
			if (!Exists(folder))
			{
				return true;
			}
			return !Directory.EnumerateFileSystemEntries(PathUtils.ForApi(folder)).Any();
		}
		public static void Copy(string sourceFolder, string destFolder)
		{
			sourceFolder = PathUtils.ForCompare(sourceFolder);
			destFolder = PathUtils.ForCompare(destFolder);
			bool isEnabled;
			if (string.CompareOrdinal(sourceFolder, destFolder) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(18, 2, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("复制文件夹到自身，不执行操作：");
					handler.AppendFormatted(sourceFolder);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				return;
			}
			if (string.Compare(sourceFolder, destFolder, StringComparison.OrdinalIgnoreCase) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(29, 2, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("复制文件夹到自身，但大小写不同，等效于重命名文件夹：");
					handler2.AppendFormatted(sourceFolder);
					handler2.AppendLiteral(" → ");
					handler2.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				Move(sourceFolder, destFolder);
				return;
			}
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(9, 2, out isEnabled);
			if (isEnabled)
			{
				handler3.AppendLiteral("复制文件夹：");
				handler3.AppendFormatted(sourceFolder);
				handler3.AppendLiteral(" → ");
				handler3.AppendFormatted(destFolder);
			}
			Logger.Trace(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
			foreach (string item in EnumerateFiles(sourceFolder, includeSubDirectories: true).ToList())
			{
				FileUtils.Copy(item, item.Replace(sourceFolder, destFolder));
			}
		}
		public static void Move(string sourceFolder, string destFolder)
		{
			SafetyCheck(sourceFolder);
			sourceFolder = PathUtils.ForCompare(sourceFolder);
			destFolder = PathUtils.ForCompare(destFolder);
			bool isEnabled;
			if (string.CompareOrdinal(sourceFolder, destFolder) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(18, 2, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("剪切文件夹到自身，不执行操作：");
					handler.AppendFormatted(sourceFolder);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
			}
			else if (string.Compare(sourceFolder, destFolder, StringComparison.OrdinalIgnoreCase) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(19, 2, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("剪切文件夹到自身，但大小写不同：");
					handler2.AppendFormatted(sourceFolder);
					handler2.AppendLiteral(" → ");
					handler2.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				string text = Path.Combine(PathUtils.RemoveLastPart(sourceFolder), Path.GetRandomFileName());
				Move(sourceFolder, text);
				Move(text, destFolder);
			}
			else if (PathUtils.GetDiskName(sourceFolder) == PathUtils.GetDiskName(destFolder))
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(14, 2, out isEnabled);
				if (isEnabled)
				{
					handler3.AppendLiteral("剪切文件夹到同一磁盘：");
					handler3.AppendFormatted(sourceFolder);
					handler3.AppendLiteral(" → ");
					handler3.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				Delete(destFolder);
				Retrier.Attempt((int _) =>
				{
					FileUtils.SetReadOnly(sourceFolder, readOnly: false);
					Directory.Move(PathUtils.ForApi(sourceFolder), PathUtils.ForApi(destFolder));
				}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
			}
			else
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler4 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(14, 2, out isEnabled);
				if (isEnabled)
				{
					handler4.AppendLiteral("剪切文件夹到不同磁盘：");
					handler4.AppendFormatted(sourceFolder);
					handler4.AppendLiteral(" → ");
					handler4.AppendFormatted(destFolder);
				}
				Logger.Trace(ref handler4, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				Copy(sourceFolder, destFolder);
				Delete(sourceFolder);
			}
		}
		public static bool? Delete(string folder, bool toRecycleBin = false)
		{
			if (!Exists(folder))
			{
				return null;
			}
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(1, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendFormatted(toRecycleBin ? "将文件夹删除到回收站" : "删除文件夹");
				handler.AppendLiteral("：");
				handler.AppendFormatted(folder);
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
			SafetyCheck(folder);
			if (toRecycleBin)
			{
				try
				{
					FileUtils.DeleteToRecycleBin(folder);
					return true;
				}
				catch (Exception ex)
				{
					Logger.Warn(ex, "无法将文件夹删除到回收站，回退到永久删除：" + folder, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
				}
			}
			DeleteInternal(folder);
			return false;
			static void DeleteInternal(string text)
			{
				try
				{
					text = PathUtils.ForApi(text);
					foreach (string item in EnumerateFiles(text))
					{
						FileUtils.Delete(item);
					}
					foreach (string item2 in EnumerateDirectories(text))
					{
						DeleteInternal(item2);
					}
					Retrier.Attempt((int _) =>
					{
						FileUtils.SetReadOnly(text, readOnly: false);
						Directory.Delete(text, recursive: true);
					}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex3) => ex3 is IOException);
				}
				catch (DirectoryNotFoundException ex2)
				{
					if (Exists(text))
					{
						Logger.Warn(ex2, "该文件夹可能为孤立的符号链接，尝试直接删除（" + text + "）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\DirectoryUtils.cs");
						FileUtils.SetReadOnly(text, readOnly: false);
						Directory.Delete(text);
						return;
					}
					throw;
				}
			}
		}
		private static void SafetyCheck(string folder)
		{
			folder = PathUtils.ForCompare(folder);
			if (folder == PathUtils.ForCompare(Path.GetPathRoot(folder)))
			{
				throw new UnauthorizedAccessException("不应操作磁盘根目录：" + folder);
			}
			if (criticalFolders.Value.Any((string f) => PathUtils.IsParentOf(folder, f)))
			{
				throw new UnauthorizedAccessException("不应操作文件夹：" + folder);
			}
		}
	}
}
