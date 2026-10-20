// MeloongCore

namespace MeloongCore
{
	public static class TaskUtils
	{
		public static Task WhenAll(IEnumerable<Func<ProgressProvider?, Task>> tasks, ProgressProvider? progress = null)
		{
			List<Func<ProgressProvider, Task>> list = tasks.ToList();
			if (!list.Any())
			{
				progress?.Skip();
				return Task.CompletedTask;
			}
			double progressEach = 1.0 / (double)list.Count;
			return Task.WhenAll(((IEnumerable<Func<ProgressProvider, Task>>)list).Select((Func<Func<ProgressProvider, Task>, int, Task>)(async (Func<ProgressProvider, Task> task, int _) =>
			{
				ProgressProvider cp = progress?.SplitBy(progressEach)[0];
				await task(cp).NoCapture();
				cp?.Finish();
			})));
		}
		public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Func<ProgressProvider?, Task<TResult>>> tasks, ProgressProvider? progress = null)
		{
			List<Func<ProgressProvider, Task<TResult>>> list = tasks.ToList();
			if (!list.Any())
			{
				progress?.Skip();
				return Task.FromResult(Array.Empty<TResult>());
			}
			double progressEach = 1.0 / (double)list.Count;
			return Task.WhenAll(list.Select(async (Func<ProgressProvider, Task<TResult>> task, int _) =>
			{
				ProgressProvider cp = progress?.SplitBy(progressEach)[0];
				TResult result = await task(cp).NoCapture();
				cp?.Finish();
				return result;
			}));
		}
		public static Task WhenAll(IEnumerable<Task> tasks, ProgressProvider? progress = null)
		{
			return WhenAll(tasks.Select((Func<Task, Func<ProgressProvider, Task>>)((Task task) => (ProgressProvider? _) => task)), progress);
		}
		public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks, ProgressProvider? progress = null)
		{
			return WhenAll(tasks.Select((Func<Task<TResult>, Func<ProgressProvider, Task<TResult>>>)((Task<TResult> task) => (ProgressProvider? _) => task)), progress);
		}
		public static void ForEach<T>(IEnumerable<T> source, Action<T> body)
		{
			Parallel.ForEach(source, body);
		}
		public static async Task ForEachAsync<T>(IEnumerable<T> source, int maxDegreeOfParallelism, Func<T, ProgressProvider?, Task> body, CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progress = null)
		{
			CancellationTokenSource linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[1] { cancellationToken });
			CancellationToken token;
			object enumeratorLock;
			IEnumerator<T> enumerator;
			try
			{
				token = linkedCancellationTokenSource.Token;
				enumeratorLock = new object();
				enumerator = source.GetEnumerator();
				try
				{
					await WhenAll(Enumerable.Repeat<Func<ProgressProvider, Task>>(WorkerAsync, maxDegreeOfParallelism), progress).NoCapture();
				}
				finally
				{
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			finally
			{
				if (linkedCancellationTokenSource != null)
				{
					((IDisposable)linkedCancellationTokenSource).Dispose();
				}
			}
			async Task WorkerAsync(ProgressProvider? cp)
			{
				try
				{
					while (true)
					{
						T current;
						lock (enumeratorLock)
						{
							token.ThrowIfCancellationRequested();
							if (!enumerator.MoveNext())
							{
								break;
							}
							current = enumerator.Current;
						}
						await body(current, cp).NoCapture();
					}
				}
				catch
				{
					try
					{
						linkedCancellationTokenSource.Cancel();
					}
					catch
					{
					}
					throw;
				}
			}
		}
		public static async Task<(string Output, int ExitCode)> RunProgramAsync(string file, string arguments = "", int? timeoutMs = null, Encoding? encoding = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				Arguments = arguments,
				FileName = PathUtils.ToShortPath(file),
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				StandardOutputEncoding = encoding,
				RedirectStandardError = true,
				StandardErrorEncoding = encoding
			};
			using Process program = new Process();
			program.StartInfo = startInfo;
			program.EnableRaisingEvents = true;
			if (!program.Start())
			{
				throw new InvalidOperationException("运行程序时出现意外错误：" + file + " " + arguments);
			}
			bool flag = timeoutMs.HasValue && timeoutMs.GetValueOrDefault() > 0;
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(13, 3, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("运行程序，并返回其输出：");
				handler.AppendFormatted(file);
				handler.AppendLiteral(" ");
				handler.AppendFormatted(arguments);
				handler.AppendFormatted(flag ? $"，最长可等待 {timeoutMs}ms" : "");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\TaskUtils.cs");
			Task<string> outputTask = program.StandardOutput.ReadToEndAsync();
			Task<string> errorTask = program.StandardError.ReadToEndAsync();
			TaskCompletionSource<object?> completionSource = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
			program.Exited += (object _, EventArgs _) =>
			{
				completionSource.TrySetResult(null);
			};
			if (program.HasExited)
			{
				completionSource.TrySetResult(null);
			}
			Task<object?> task = completionSource.Task;
			Task task2 = Task.Delay(flag ? timeoutMs.Value : (-1));
			Task cancelTask = Task.Delay(-1, cancellationToken);
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(8, 1, out isEnabled);
			if (isEnabled)
			{
				handler2.AppendLiteral("等待程序 ");
				handler2.AppendFormatted(program.Id);
				handler2.AppendLiteral(" 完成");
			}
			Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\TaskUtils.cs");
			Task task3 = await Task.WhenAny(task, task2, cancelTask).NoCapture();
			if (task3 != task)
			{
				try
				{
					if (!program.HasExited)
					{
						program.Kill();
					}
				}
				catch (InvalidOperationException)
				{
				}
				if (task3 == cancelTask)
				{
					throw new OperationCanceledException(cancellationToken);
				}
				throw new TimeoutException("运行程序超时：" + file + " " + arguments);
			}
			return (Output: await outputTask.NoCapture() + await errorTask.NoCapture(), ExitCode: program.ExitCode);
		}
	}
}
