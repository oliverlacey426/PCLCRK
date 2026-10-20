// PCLCS

namespace PCLCS
{
	public static class JavaUtils
	{
		internal static readonly Lazy<List<string>> CandidateFolders = new Lazy<List<string>>(() =>
		{
			List<string> list = new List<string>();
			list.Add(Paths.AppData + ".minecraft\\runtime\\");
			list.Add(Paths.AppData + ".hmcl\\java\\");
			list.Add(Paths.AppData + "ATLauncher\\runtimes\\minecraft\\");
			list.Add(Paths.AppData + "ModrinthApp\\meta\\java_versions\\");
			list.Add(Paths.AppData + "PrismLauncher\\java\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\curseforge\\minecraft\\Install\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.jdks\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.sdkman\\candidates\\java\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\.ftba\\bin\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Packages\\Microsoft.4297127D64EC6_8wekyb3d8bbwe\\LocalCache\\Local\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft Launcher\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Curse\\Minecraft\\Install\\runtime\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Java\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Eclipse Adoptium\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Amazon Corretto\\");
			list.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Zulu\\");
			list.AddRange(DirectoryUtils.EnumerateDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Microsoft\\", includeSubDirectories: false, "jdk-*"));
			list.AddRange(from path in (Environment.GetEnvironmentVariable("JDK_HOME") + ";" + Environment.GetEnvironmentVariable("JAVA_HOME")).Split(";", removeEmptyEntries: true)
				select path.Trim(' ', '"'));
			return (from f in list.Select((string f) =>
				{
					try
					{
						return PathUtils.ForCompare(f);
					}
					catch (Exception ex)
					{
						Logger.Warn(ex, "Java 候选文件夹路径无效（" + f + "）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
						return (string)null;
					}
				})
				where f != null
				select f).Cast<string>().ToList();
		});
		public static async Task<List<Java>> CheckAllAsync(IEnumerable<Java> javas, CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progress = null)
		{
			// C# 不允许在查询表达式的 select 里 await，改成先投影成 Task 列表再 WhenAll
			List<Task> checkTasks = javas.Where((Java java) => !java.available.HasValue).Select((Java java) => (Task)java.CheckAsync(cancellationToken)).ToList();
			await TaskUtils.WhenAll(checkTasks, progress).NoCapture();
			return javas.Where((Java j) => j.available == true).ToList();
		}
		public static async Task<List<Java>> SortAsync(List<Java> javas, CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progress = null)
		{
			javas = await CheckAllAsync(javas, cancellationToken, progress).NoCapture();
			javas.Sort((Java left, Java right) =>
			{
				bool flag = CandidateFolders.Value.Any((string folder) => PathUtils.IsParentOf(folder, left.Folder));
				bool flag2 = CandidateFolders.Value.Any((string folder) => PathUtils.IsParentOf(folder, right.Folder));
				if (flag != flag2)
				{
					if (!flag)
					{
						return 1;
					}
					return -1;
				}
				int num = (((object)left.Version == null) ? int.MaxValue : Math.Abs(left.Version.Major - 21));
				int value = (((object)right.Version == null) ? int.MaxValue : Math.Abs(right.Version.Major - 21));
				return num.CompareTo(value);
			});
			return javas;
		}
		public static async Task RefreshListAsync(CancellationToken c = default(CancellationToken), ProgressProvider? p = null)
		{
			List<Java> javaList = await SearchFoldersAsync(includeSubDirectories: true, CandidateFolders.Value, c, p?.SplitTo(0.5)).NoCapture();
			ConcurrentList<Java> oldJavaList = Configs.JavaList.Get();
			oldJavaList = await CheckAllAsync(oldJavaList, c, p?.SplitTo(0.9)).NoCapture();
			javaList.RemoveAll(oldJavaList);
			javaList = await SortAsync(javaList, c).NoCapture();
			javaList.AddRange(oldJavaList);
			javaList = javaList.Distinct().ToList();
			javaList.RemoveIf((Java j) => Configs.JavaRemovedList.Get().Contains(j.Folder, StringComparer.OrdinalIgnoreCase));
			ConcurrentList<Java> concurrentList = new ConcurrentList<Java>();
			foreach (Java item in javaList)
			{
				concurrentList.Add(item);
			}
			Configs.JavaList.Set(concurrentList);
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(20, 1, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("Java 搜索完成，发现 ");
				handler.AppendFormatted(javaList.Count);
				handler.AppendLiteral(" 个 Java");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
		}
		public static async Task<List<Java>> SearchFoldersAsync(bool includeSubDirectories, IEnumerable<string> folders, CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progress = null)
		{
			ConcurrentBag<(string Folder, bool HasReparsePoint, bool IsSpecialPath)> results = new ConcurrentBag<(string, bool, bool)>();
			await TaskUtils.ForEachAsync(folders.Distinct(StringComparer.OrdinalIgnoreCase), 4, async (string folder, ProgressProvider? cp) =>
			{
				await Task.Run(() =>
				{
					try
					{
						foreach (string item3 in DirectoryUtils.EnumerateFiles(folder, includeSubDirectories, "java.exe"))
						{
							cancellationToken.ThrowIfCancellationRequested();
							string text = PathUtils.AddSlashSuffix(PathUtils.ForCompare(PathUtils.RemoveLastPart(item3)));
							bool item = HasReparsePoint(FileUtils.GetInfo(Path.Combine(text, "java.exe")));
							bool item2 = text.Contains("java8path_target_") || text.Contains("javapath_target_") || text.Contains("javatmp") || text.Contains("system32");
							results.Add((text, item, item2));
						}
					}
					catch (UnauthorizedAccessException)
					{
						Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler4 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(17, 1, out var isEnabled2);
						if (isEnabled2)
						{
							handler4.AppendLiteral("快速查找 Java 时没有权限（");
							handler4.AppendFormatted(folder);
							handler4.AppendLiteral("）");
						}
						Logger.Info(ref handler4, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
					}
					catch (Exception ex2)
					{
						ex2.ThrowIfCanceled();
						Logger.Warn(ex2, "快速查找 Java 时出错（" + folder + "）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
					}
				}, cancellationToken).NoCapture();
			}, cancellationToken, progress?.SplitTo(0.2)).NoCapture();
			if (!results.Any())
			{
				return new List<Java>();
			}
			bool isEnabled;
			if (results.All(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => r.HasReparsePoint))
			{
				Logger.Warn("找到的所有 Java 都包含重解析点，保留全部结果", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
			}
			else if (results.Any(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => r.HasReparsePoint))
			{
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(28, 1, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("找到的以下 Java 包含重解析点，将它们从列表中排除：");
					handler.AppendFormatted((from r in results
						where r.HasReparsePoint
						select r.Folder).Join('、'));
				}
				Logger.Warn(ref handler, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
				ConcurrentBag<(string, bool, bool)> concurrentBag = new ConcurrentBag<(string, bool, bool)>();
				foreach (var item4 in results.Where(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => !r.HasReparsePoint))
				{
					concurrentBag.Add(item4);
				}
				results = concurrentBag;
			}
			if (results.All(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => r.IsSpecialPath))
			{
				Logger.Warn("找到的所有 Java 都包含特殊路径，保留全部结果", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
			}
			else if (results.Any(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => r.IsSpecialPath))
			{
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(28, 1, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("找到的以下 Java 包含特殊路径，将它们从列表中排除：");
					handler2.AppendFormatted((from r in results
						where r.IsSpecialPath
						select r.Folder).Join('、'));
				}
				Logger.Warn(ref handler2, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
				ConcurrentBag<(string, bool, bool)> concurrentBag = new ConcurrentBag<(string, bool, bool)>();
				foreach (var item5 in results.Where(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => !r.IsSpecialPath))
				{
					concurrentBag.Add(item5);
				}
				results = concurrentBag;
			}
			List<Java> javas = (from folder in results.Select(((string Folder, bool HasReparsePoint, bool IsSpecialPath) r) => r.Folder).Distinct(StringComparer.OrdinalIgnoreCase)
				select new Java(folder)).ToList();
			javas = (await CheckAllAsync(javas, cancellationToken, progress?.SplitTo(1.0)).NoCapture()).ToList();
			javas = await SortAsync(javas, cancellationToken).NoCapture();
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(21, 2, out isEnabled);
			if (isEnabled)
			{
				handler3.AppendLiteral("快速查找 Java 完成，共有 ");
				handler3.AppendFormatted(javas.Count);
				handler3.AppendLiteral(" 个候选：");
				handler3.AppendFormatted(javas.Join('、'));
			}
			Logger.Info(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
			return javas;
			static bool HasReparsePoint(FileSystemInfo info)
			{
				if (PathUtils.IsParentOf(Paths.AppData + ".minecraft\\runtime\\", info.FullName))
				{
					return false;
				}
				do
				{
					if (info.Attributes.HasFlagF(FileAttributes.ReparsePoint))
					{
						return true;
					}
					info = ((info is FileInfo fileInfo) ? fileInfo.Directory : ((DirectoryInfo)info).Parent);
				}
				while (info != null);
				return false;
			}
		}
	}
}
