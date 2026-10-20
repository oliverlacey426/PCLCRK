// MeloongCore

namespace MeloongCore
{
	public abstract class RedoableWorkerBase<TOut>(Func<CancellationToken?, ProgressProvider?, TOut> workload, ProgressProvider? progress = null, [CallerMemberName] string creatorMemberName = "") : IWorker
	{
		private bool running;
		private bool hasSucceeded;
		private bool lastSucceeded;
		private object? lastResult;
		private bool pendingRedo;
		private CancellationTokenSource? realCts;
		private CancellationToken lastToken;
		private readonly ManualResetEventSlim idleEvent = new ManualResetEventSlim(initialState: true);
		public bool Running
		{
			get
			{
				lock (this)
				{
					return running;
				}
			}
		}
		public bool HasSucceeded
		{
			get
			{
				lock (this)
				{
					return hasSucceeded;
				}
			}
		}
		public bool LastSucceeded
		{
			get
			{
				lock (this)
				{
					return lastSucceeded;
				}
			}
		}
		public TOut LastResult
		{
			get
			{
				lock (this)
				{
					if (typeof(TOut) == typeof(NoType))
					{
						throw new InvalidOperationException("该 Worker 没有返回值。");
					}
					if (!hasSucceeded)
					{
						throw new InvalidOperationException("从未成功完成过。");
					}
					return (lastResult == null) ? default(TOut) : ((TOut)lastResult);
				}
			}
		}
		public ProgressProvider Progress { get; private set; } = progress ?? new ProgressProvider();
		public event Action? Started;
		public event Action? Stopped;
		public event Action? Succeeded;
		public event Action<Exception>? Failed;
		public event Action? Canceled;
		public void Start(CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progressOverride = null)
		{
			lock (this)
			{
				lastToken = cancellationToken;
				if (progressOverride != null)
				{
					Progress = progressOverride;
				}
				if (running)
				{
					Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(7, 1, out var isEnabled);
					if (isEnabled)
					{
						handler.AppendFormatted(creatorMemberName);
						handler.AppendLiteral("：接到重启请求");
					}
					Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
					pendingRedo = true;
					realCts?.Cancel();
					return;
				}
				running = true;
				idleEvent.Reset();
				realCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[1] { lastToken });
			}
			Thread thread = new Thread(_Start);
			thread.IsBackground = true;
			thread.Name = "W/" + creatorMemberName;
			thread.Start();
		}
		private void _Start()
		{
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(5, 1, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendFormatted(creatorMemberName);
				handler.AppendLiteral("：运行开始");
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
			Started?.Invoke();
			while (true)
			{
				try
				{
					realCts.Token.ThrowIfCancellationRequested();
					Progress?.Reset();
					TOut val = workload(realCts.Token, Progress);
					realCts.Token.ThrowIfCancellationRequested();
					lock (this)
					{
						realCts?.Dispose();
						if (pendingRedo)
						{
							pendingRedo = false;
							realCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[1] { lastToken });
							continue;
						}
						realCts = null;
						lastSucceeded = true;
						lastResult = val;
						hasSucceeded = true;
						running = false;
						idleEvent.Set();
						Progress?.Finish();
					}
					Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(5, 1, out isEnabled);
					if (isEnabled)
					{
						handler2.AppendFormatted(creatorMemberName);
						handler2.AppendLiteral("：运行成功");
					}
					Logger.Info(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
					Succeeded?.Invoke();
					break;
				}
				catch (Exception ex)
				{
					if (!ex.IsCanceled())
					{
						Logger.Log(ex, creatorMemberName + "：运行失败" + (pendingRedo ? "，但即将重启，或可忽略" : ""), pendingRedo ? LogLevel.Warn : LogLevel.Error, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
					}
					lock (this)
					{
						realCts?.Dispose();
						if (pendingRedo)
						{
							Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(5, 1, out var isEnabled2);
							if (isEnabled2)
							{
								handler3.AppendFormatted(creatorMemberName);
								handler3.AppendLiteral("：重启开始");
							}
							Logger.Info(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
							pendingRedo = false;
							realCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[1] { lastToken });
							continue;
						}
						realCts = null;
						lastSucceeded = false;
						running = false;
						idleEvent.Set();
						Progress?.Skip();
					}
					if (ex.IsCanceled())
					{
						Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler4 = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(6, 1, out isEnabled);
						if (isEnabled)
						{
							handler4.AppendFormatted(creatorMemberName);
							handler4.AppendLiteral("：运行已取消");
						}
						Logger.Info(ref handler4, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
						Canceled?.Invoke();
					}
					else
					{
						Failed?.Invoke(ex);
					}
					break;
				}
			}
			Stopped?.Invoke();
		}
		public void Cancel()
		{
			lock (this)
			{
				pendingRedo = false;
				Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(7, 1, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendFormatted(creatorMemberName);
					handler.AppendLiteral("：接到取消请求");
				}
				Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Worker\\RedoableWorker.cs");
				realCts?.Cancel();
			}
		}
		public bool WaitIfRunning(int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
		{
			return idleEvent.Wait(millisecondsTimeout, cancellationToken);
		}
		public async Task<bool> WaitIfRunningAsync(int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			RegisteredWaitHandle waitHandle = ThreadPool.RegisterWaitForSingleObject(idleEvent.WaitHandle, (object _, bool timedOut) =>
			{
				completionSource.TrySetResult(!timedOut);
			}, null, millisecondsTimeout, executeOnlyOnce: true);
			using (cancellationToken.Register(() =>
			{
				completionSource.TrySetCanceled(cancellationToken);
			}))
			{
				try
				{
					return await completionSource.Task.NoCapture();
				}
				finally
				{
					waitHandle.Unregister(null);
				}
			}
		}
	}
}
