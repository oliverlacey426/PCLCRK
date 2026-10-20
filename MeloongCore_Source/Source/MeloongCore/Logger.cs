// MeloongCore

namespace MeloongCore
{
	public static class Logger
	{
		[InterpolatedStringHandler]
		public ref struct LogInterpolatedStringHandler
		{
			private StringBuilder? _sb;
			public readonly bool isEnabled;
			public LogInterpolatedStringHandler(int literalLength, LogLevel level, out bool isEnabled)
			{
				this.isEnabled = (isEnabled = Instance.MinLevel <= level);
				_sb = (this.isEnabled ? new StringBuilder(literalLength) : null);
			}
			public void AppendLiteral(string value)
			{
				_sb?.Append(value);
			}
			public void AppendFormatted<T>(T value)
			{
				_sb?.Append(value);
			}
			public void AppendFormatted<T>(T value, string? format)
			{
				_sb?.Append((value is IFormattable formattable) ? formattable.ToString(format, null) : value?.ToString());
			}
			public void AppendFormatted<T>(T value, int alignment)
			{
				if (_sb != null)
				{
					string text = value?.ToString();
					if (text == null)
					{
						text = string.Empty;
					}
					string text2 = text;
					StringBuilder? sb = _sb;
					string? value2;
					if (alignment > 0)
					{
						value2 = text2.PadLeft(alignment);
					}
					else
					{
						value2 = ((alignment < 0) ? text2.PadRight(-alignment) : text2);
					}
					sb.Append(value2);
				}
			}
			public void AppendFormatted<T>(T value, int alignment, string? format)
			{
				if (_sb == null)
				{
					return;
				}
				string text;
				if (!(value is IFormattable formattable))
				{
					text = value?.ToString();
					if (text == null)
					{
						text = string.Empty;
					}
				}
				else
				{
					text = formattable.ToString(format, null);
				}
				string text2 = text;
				StringBuilder? sb = _sb;
				string? value2;
				if (alignment > 0)
				{
					value2 = text2.PadLeft(alignment);
				}
				else
				{
					value2 = ((alignment < 0) ? text2.PadRight(-alignment) : text2);
				}
				sb.Append(value2);
			}
			public void AppendFormatted(string? value)
			{
				_sb?.Append(value);
			}
			public void AppendFormatted(object? value, int alignment = 0, string? format = null)
			{
				if (_sb != null)
				{
					string text = ((value is IFormattable formattable) ? formattable.ToString(format, null) : (value?.ToString() ?? string.Empty));
					StringBuilder? sb = _sb;
					string? value2;
					if (alignment > 0)
					{
						value2 = text.PadLeft(alignment);
					}
					else
					{
						value2 = ((alignment < 0) ? text.PadRight(-alignment) : text);
					}
					sb.Append(value2);
				}
			}
			internal string ToStringAndClear()
			{
				string result = _sb?.ToString() ?? string.Empty;
				_sb = null;
				return result;
			}
		}
		[InterpolatedStringHandler]
		public ref struct LogInterpolatedStringHandler<TLevel> where TLevel : struct, ILogLevelProvider
		{
			private LogInterpolatedStringHandler _inner;
			public bool IsEnabled => _inner.isEnabled;
			public LogInterpolatedStringHandler(int literalLength, int formattedCount, out bool isEnabled)
			{
				_inner = new LogInterpolatedStringHandler(literalLength, default(TLevel).Level, out isEnabled);
			}
			public void AppendLiteral(string value)
			{
				_inner.AppendLiteral(value);
			}
			public void AppendFormatted<T>(T value)
			{
				_inner.AppendFormatted(value);
			}
			public void AppendFormatted<T>(T value, string? format)
			{
				_inner.AppendFormatted(value, format);
			}
			public void AppendFormatted<T>(T value, int alignment)
			{
				_inner.AppendFormatted(value, alignment);
			}
			public void AppendFormatted<T>(T value, int alignment, string? format)
			{
				_inner.AppendFormatted(value, alignment, format);
			}
			public void AppendFormatted(string? value)
			{
				_inner.AppendFormatted(value);
			}
			public void AppendFormatted(object? value, int alignment = 0, string? format = null)
			{
				_inner.AppendFormatted(value, alignment, format);
			}
			internal string ToStringAndClear()
			{
				return _inner.ToStringAndClear();
			}
		}
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public readonly struct TraceLogLevel : ILogLevelProvider
		{
			public LogLevel Level => LogLevel.Trace;
		}
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public readonly struct InfoLogLevel : ILogLevelProvider
		{
			public LogLevel Level => LogLevel.Info;
		}
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public readonly struct WarnLogLevel : ILogLevelProvider
		{
			public LogLevel Level => LogLevel.Warn;
		}
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public readonly struct ErrorLogLevel : ILogLevelProvider
		{
			public LogLevel Level => LogLevel.Error;
		}
		public interface ILogLevelProvider
		{
			LogLevel Level { get; }
		}
		public static BaseLogger Instance { get; set; } = new BaseLogger();
		public static void Trace(string message, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(null, message, LogLevel.Trace, behavior, filePath);
		}
		public static void Info(string message, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(null, message, LogLevel.Info, behavior, filePath);
		}
		public static void Warn(string message, LogBehavior behavior = LogBehavior.ToastIfDebug, [CallerFilePath] string filePath = "")
		{
			Log(null, message, LogLevel.Warn, behavior, filePath);
		}
		public static void Error(string message, LogBehavior behavior = LogBehavior.AlertThenFeedback, [CallerFilePath] string filePath = "")
		{
			Log(null, message, LogLevel.Error, behavior, filePath);
		}
		public static void Log(string message, LogLevel level = LogLevel.Info, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(null, message, level, behavior, filePath);
		}
		public static void Trace(Exception ex, string? message = null, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(ex, message, LogLevel.Trace, behavior, filePath);
		}
		public static void Info(Exception ex, string? message = null, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(ex, message, LogLevel.Info, behavior, filePath);
		}
		public static void Warn(Exception ex, string? message = null, LogBehavior behavior = LogBehavior.ToastIfDebug, [CallerFilePath] string filePath = "")
		{
			Log(ex, message, LogLevel.Warn, behavior, filePath);
		}
		public static void Error(Exception ex, string? message = null, LogBehavior behavior = LogBehavior.AlertThenFeedback, [CallerFilePath] string filePath = "")
		{
			Log(ex, message, LogLevel.Error, behavior, filePath);
		}
		public static void Log(Exception? ex, string? message = null, LogLevel level = LogLevel.Warn, LogBehavior behavior = LogBehavior.ToastIfDebug, [CallerFilePath] string filePath = "")
		{
			Instance.Log(ex, message, level, behavior, filePath);
		}
		[Conditional("DEBUG")]
		public static void SendToDebug(string message, LogLevel level = LogLevel.Error, [CallerFilePath] string filePath = "")
		{
		}
		[Conditional("DEBUG")]
		public static void SendToDebug(Exception ex, string? message = null, LogLevel level = LogLevel.Error, [CallerFilePath] string filePath = "")
		{
		}
		public static void StackTrace(LogLevel level = LogLevel.Trace, [CallerFilePath] string filePath = "")
		{
			Instance.Log(null, new StackTrace(1, fNeedFileInfo: true).ToString(), level, LogBehavior.None, filePath);
		}
		public static void Log(Func<string?> messageFactory, LogLevel level = LogLevel.Info, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			if (Instance.MinLevel <= level)
			{
				Instance.Log(null, messageFactory() ?? string.Empty, level, behavior, filePath);
			}
		}
		public static void Trace(Func<string?> messageFactory, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(messageFactory, LogLevel.Trace, behavior, filePath);
		}
		public static void Info(Func<string?> messageFactory, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			Log(messageFactory, LogLevel.Info, behavior, filePath);
		}
		public static void Warn(Func<string?> messageFactory, LogBehavior behavior = LogBehavior.ToastIfDebug, [CallerFilePath] string filePath = "")
		{
			Log(messageFactory, LogLevel.Warn, behavior, filePath);
		}
		public static void Error(Func<string?> messageFactory, LogBehavior behavior = LogBehavior.AlertThenFeedback, [CallerFilePath] string filePath = "")
		{
			Log(messageFactory, LogLevel.Error, behavior, filePath);
		}
		public static void Log(LogLevel level, [InterpolatedStringHandlerArgument("level")] ref LogInterpolatedStringHandler handler, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			if (handler.isEnabled)
			{
				Instance.Log(null, handler.ToStringAndClear(), level, behavior, filePath);
			}
		}
		public static void Trace(ref LogInterpolatedStringHandler<TraceLogLevel> handler, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			if (handler.IsEnabled)
			{
				Instance.Log(null, handler.ToStringAndClear(), LogLevel.Trace, behavior, filePath);
			}
		}
		public static void Info(ref LogInterpolatedStringHandler<InfoLogLevel> handler, LogBehavior behavior = LogBehavior.None, [CallerFilePath] string filePath = "")
		{
			if (handler.IsEnabled)
			{
				Instance.Log(null, handler.ToStringAndClear(), LogLevel.Info, behavior, filePath);
			}
		}
		public static void Warn(ref LogInterpolatedStringHandler<WarnLogLevel> handler, LogBehavior behavior = LogBehavior.ToastIfDebug, [CallerFilePath] string filePath = "")
		{
			if (handler.IsEnabled)
			{
				Instance.Log(null, handler.ToStringAndClear(), LogLevel.Warn, behavior, filePath);
			}
		}
		public static void Error(ref LogInterpolatedStringHandler<ErrorLogLevel> handler, LogBehavior behavior = LogBehavior.AlertThenFeedback, [CallerFilePath] string filePath = "")
		{
			if (handler.IsEnabled)
			{
				Instance.Log(null, handler.ToStringAndClear(), LogLevel.Error, behavior, filePath);
			}
		}
	}
}
