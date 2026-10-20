// MeloongCore

namespace MeloongCore
{
	public class FileLogger : BaseLogger, IDisposable
	{
		public string logFolder = Path.Combine(Paths.Base, "logs");
		public string fileNamePrefix = "Log";
		public string fileNameSuffix = ".txt";
		private StreamWriter? writer;
		public override void Init()
		{
			Logger.Info("开始轮转日志文件", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Logger.cs");
			try
			{
				for (int num = 4; num >= 1; num--)
				{
					string destFilePath = Path.Combine(logFolder, $"{fileNamePrefix}{num + 1}{fileNameSuffix}");
					string text = Path.Combine(logFolder, $"{fileNamePrefix}{num}{fileNameSuffix}");
					if (FileUtils.Exists(text))
					{
						FileUtils.Copy(text, destFilePath);
						FileUtils.Delete(text);
					}
				}
			}
			catch (IOException ex)
			{
				Logger.Warn(ex, "可能同时开启了多个程序，这或许会导致未知问题", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Logger.cs");
			}
			catch (Exception ex2)
			{
				Logger.Warn(ex2, "整理日志文件失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Logger.cs");
			}
			string path = PathUtils.ForApi(Path.Combine(logFolder, fileNamePrefix + "1" + fileNameSuffix));
			DirectoryUtils.Create(logFolder);
			writer = new StreamWriter(path, append: true)
			{
				AutoFlush = true
			};
		}
		public override void Output(LogEntry entry)
		{
			base.Output(entry);
			writer?.WriteLine(entry.message);
		}
		public void Dispose()
		{
			available = false;
			if (writer != null)
			{
				((IDisposable)writer).Dispose();
			}
			queuedEvent.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
