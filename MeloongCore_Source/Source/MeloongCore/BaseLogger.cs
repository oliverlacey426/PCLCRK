// MeloongCore

namespace MeloongCore
{
	public class BaseLogger
	{
		public record struct LogEntry(LogLevel Level, string message);
		internal volatile bool available;
		internal readonly ConcurrentQueue<LogEntry> queue = new ConcurrentQueue<LogEntry>();
		internal readonly AutoResetEvent queuedEvent = new AutoResetEvent(initialState: false);
		private readonly object flushLock = new object();
		public LogLevel MinLevel { get; set; }
		public virtual void Log(Exception? ex, string? message, LogLevel level, LogBehavior behavior, string filePath)
		{
			if (ex != null && ex.IsCanceled())
			{
				Log(null, "操作已取消（" + message + "）", LogLevel.Info, LogBehavior.None, filePath);
			}
			else
			{
				if (MinLevel > level)
				{
					return;
				}
				try
				{
					string text = ((ex == null) ? (message ?? "") : (((message == null) ? "" : (message + "：")) + ex.GetDisplay(multiline: true)));
					text = Format(text, level, filePath, ex);
					if (available || queue.Count < 100)
					{
						queue.Enqueue(new LogEntry(level, text));
						queuedEvent.Set();
					}
					HandleBehavior(message, text, behavior, ex);
				}
				catch (Exception)
				{
				}
			}
		}
		public virtual string Format(string text, LogLevel level, string filePath, Exception? ex)
		{
			string prefix = GetLogPrefix(level, filePath);
			if (string.IsNullOrEmpty(text))
			{
				return prefix;
			}
			return (from t in text.ReplaceLineEndings("\n", mergeMultiple: true).Split('\n', removeEmptyEntries: true)
				select prefix + t).Join("\r\n");
		}
		public static string GetLogPrefix(LogLevel level, string filePath)
		{
			return string.Format("{0:HH':'mm':'ss'.'fff} {1} {2}[{3}] ", DateTime.Now, level.ToString().First(), (Thread.CurrentThread.Name != null) ? $"<{Thread.CurrentThread.ManagedThreadId} · {Thread.CurrentThread.Name}> " : (Thread.CurrentThread.IsThreadPoolThread ? "<Async> " : ""), PathUtils.GetLastPart(filePath).BeforeFirst("."));
		}
		public virtual void HandleBehavior(string? rawMessage, string formattedMessage, LogBehavior behavior, Exception? ex)
		{
		}
		public virtual void Output(LogEntry entry)
		{
		}
		public virtual void Init()
		{
		}
		public BaseLogger()
		{
			Thread thread = new Thread(() =>
			{
				try
				{
					Init();
					available = true;
					Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(8, 1, out var isEnabled);
					if (isEnabled)
					{
						handler.AppendFormatted(GetType().Name);
						handler.AppendLiteral(" 日志初始化结束");
					}
					Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Logger.cs");
					while (available)
					{
						queuedEvent.WaitOne();
						Flush();
					}
				}
				catch (Exception)
				{
					available = false;
				}
			});
			thread.Name = "Logger";
			thread.Priority = ThreadPriority.Lowest;
			thread.IsBackground = true;
			thread.Start();
		}
		public void Flush()
		{
			try
			{
				if (!available)
				{
					return;
				}
				lock (flushLock)
				{
					LogEntry result;
					while (queue.TryDequeue(out result))
					{
						Output(result);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
