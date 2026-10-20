// PCLCS

namespace PCLCS
{
	[Serializable]
	public class Java(string folder, Version? version = null) : IEquatable<Java>
	{
		public readonly string Folder = PathUtils.AddSlashSuffix(PathUtils.ForCompare(folder));
		public Version? Version = version;
		[JsonIgnore]
		public string? FileEncodingName;
		[JsonIgnore]
		public string? NativeEncodingName;
		[JsonIgnore]
		private int isChecked;
		[JsonIgnore]
		internal bool? available;
		[JsonIgnore]
		public string JavaExePath => Path.Combine(Folder, "java.exe");
		public async Task<bool> CheckAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (Interlocked.Exchange(ref isChecked, 1) == 1 && available.HasValue)
			{
				return available.Value;
			}
			string output = null;
			bool isEnabled;
			try
			{
				if (!FileUtils.Exists(JavaExePath))
				{
					throw new FileNotFoundException("未找到 java.exe 文件", JavaExePath);
				}
				output = (await TaskUtils.RunProgramAsync(JavaExePath, "-XshowSettings:properties -version", 15000, null, cancellationToken).NoCapture()).Item1.Lower();
				if (output == "")
				{
					throw new InvalidOperationException("尝试运行该 Java 失败");
				}
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(10, 3, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("Java 检查输出：");
					handler.AppendFormatted(JavaExePath);
					handler.AppendFormatted(Environment.NewLine);
					handler.AppendFormatted(output);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
				if (output.Contains("/lib/ext exists"))
				{
					throw new InvalidOperationException("无法运行该 Java，请在删除 Java 文件夹中的 /lib/ext 文件夹后再试");
				}
				if (output.Contains("a fatal error") || output.Contains("error: "))
				{
					throw new InvalidOperationException("无法运行该 Java，该 Java 或系统存在问题");
				}
				string text = (output.RegexSeek("(?<=version \")[^\"]+") ?? output.RegexSeek("(?<=openjdk )[0-9]+"))?.Replace("_", ".").Replace("+", ".").BeforeFirst("-");
				if (string.IsNullOrEmpty(text))
				{
					throw new InvalidOperationException("未找到该 Java 的版本号" + ((output.Length < 500) ? ("\n原始输出为：\n" + output) : ""));
				}
				if (text.StartsWithF("1."))
				{
					text = text.AfterFirst(".");
				}
				List<string> list = text.Split(".", removeEmptyEntries: true).ToList();
				while (list.Count < 4)
				{
					list.Add("0");
				}
				Version = new Version(list.Take(4).Join("."));
				int major = Version.Major;
				isEnabled = ((major <= 4 || major >= 100) ? true : false);
				if (isEnabled)
				{
					throw new InvalidOperationException(string.Format("分析详细信息失败，获取的版本为 {0}{1}", Version, (output.Length < 500) ? ("\n原始输出为：\n" + output) : ""));
				}
				if (!output.Contains("64-bit"))
				{
					throw new InvalidOperationException("该 Java 为 32 位版本，请安装 64 位的 Java");
				}
				FileEncodingName = GetProperty("file.encoding");
				NativeEncodingName = GetProperty("native.encoding") ?? FileEncodingName;
				Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(11, 1, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("检查 Java 成功：");
					handler2.AppendFormatted(this);
				}
				Logger.Info(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
				available = true;
			}
			catch (Exception ex)
			{
				if (ex.IsCanceled())
				{
					isChecked = 0;
					throw;
				}
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(19, 2, out isEnabled);
				if (isEnabled)
				{
					handler3.AppendLiteral("检查 Java 失败（");
					handler3.AppendFormatted(JavaExePath);
					handler3.AppendLiteral("），其输出为：\n");
					handler3.AppendFormatted(output ?? "无程序输出");
				}
				Logger.Warn(ref handler3, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Java.cs");
				available = false;
			}
			return available.Value;
			string? GetProperty(string name)
			{
				string prefix = name + " = ";
				return (from l in output.Split('\n')
					select l.Trim()).FirstOrDefault((string l) => l.StartsWithF(prefix))?.Substring(prefix.Length).Trim();
			}
		}
		public void InvalidateChecked()
		{
			isChecked = 0;
			available = null;
		}
		public bool Equals(Java? other)
		{
			if (other != null)
			{
				return string.Equals(Folder, other.Folder, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
		public override bool Equals(object? obj)
		{
			if (obj is Java other)
			{
				return Equals(other);
			}
			return false;
		}
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(Folder);
		}
		public override string ToString()
		{
			return string.Format("Java {0} ({1})：{2}", Version?.Major.ToString() ?? "尚未检查", Version, Folder);
		}
	}
}
