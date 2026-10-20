// PCL

namespace PCL
{
	[StandardModule]
	public static class ModLoader
	{
		public abstract class LoaderBase : ILoadingTrigger
		{
			public delegate void OnStateChangedThreadEventHandler(LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState);
			public delegate void OnStateChangedUiEventHandler(LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState);
			public delegate void PreviewFinishEventHandler(LoaderBase Loader);
			public int Uuid;
			public string Name;
			public readonly object LockState;
			public LoaderBase Parent;
			public bool HasOnStateChangedThread;
			private ModBase.LoadState _State;
			private MyLoading.MyLoadingState _LoadingState;
			public bool Block;
			public bool Show;
			public bool IsForceRestarting;
			private double _Progress;
			private CancellationTokenSource CancellationTokenSource;
			public const string WaitForExitTimeoutMessage = "等待加载器执行超时。";
			public bool IsLoader { get; }
			public LoaderBase RealParent
			{
				get
				{
					LoaderBase loaderBase;
					try
					{
						loaderBase = Parent;
						while (loaderBase != null && loaderBase.Parent != null)
						{
							loaderBase = loaderBase.Parent;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, $"获取父加载器失败（{Name}）", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
						loaderBase = null;
						ProjectData.ClearProjectError();
					}
					return loaderBase;
				}
			}
			public Action<LoaderBase> OnStateChanged
			{
				set
				{
					OnStateChangedUi += [SpecialName] (LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState) =>
					{
						value(Loader);
					};
				}
			}
			public ModBase.LoadState State
			{
				get
				{
					return _State;
				}
				set
				{
					if (_State == value)
					{
						return;
					}
					ModBase.LoadState state = _State;
					if (value == ModBase.LoadState.Finished && Settings.Get<bool>("SystemDebugDelay"))
					{
						Thread.Sleep(ModBase.RandomInteger(100, 2000));
					}
					_State = value;
					if (value == ModBase.LoadState.Canceled)
					{
						CancellationTokenSource?.Cancel();
					}
					Logger.Log($"加载器 {GetType().Name} {Name} 状态改变：{value}", (value != ModBase.LoadState.Failed) ? LogLevel.Info : LogLevel.Error, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
					ModBase.RunInUi([SpecialName] () =>
					{
						switch (value)
						{
						case ModBase.LoadState.Loading:
							LoadingState = MyLoading.MyLoadingState.Run;
							break;
						case ModBase.LoadState.Failed:
							LoadingState = MyLoading.MyLoadingState.Error;
							break;
						default:
							LoadingState = MyLoading.MyLoadingState.Stop;
							break;
						}
						OnStateChangedUi?.Invoke(this, value, state);
					});
					if (HasOnStateChangedThread)
					{
						ModBase.RunInThread([SpecialName] () =>
						{
							OnStateChangedThread?.Invoke(this, value, state);
						});
					}
				}
			}
			public MyLoading.MyLoadingState LoadingState
			{
				get
				{
					return _LoadingState;
				}
				set
				{
					if (_LoadingState != value)
					{
						MyLoading.MyLoadingState loadingState = _LoadingState;
						_LoadingState = value;
						LoadingStateChanged?.Invoke(value, loadingState);
					}
				}
			}
			public Exception Error { get; set; }
			public virtual double Progress
			{
				get
				{
					return State switch
					{
						ModBase.LoadState.Waiting => 0.0, 
						ModBase.LoadState.Loading => (_Progress == -1.0) ? 0.0 : _Progress, 
						_ => 1.0, 
					};
				}
				set
				{
					if (_Progress != value)
					{
						double progress = _Progress;
						_Progress = value;
						ProgressChanged?.Invoke(value, progress);
					}
				}
			}
			public double ProgressWeight { get; set; }
			public event OnStateChangedThreadEventHandler OnStateChangedThread;
			public event OnStateChangedUiEventHandler OnStateChangedUi;
			public event PreviewFinishEventHandler PreviewFinish;
			public event ILoadingTrigger.LoadingStateChangedEventHandler LoadingStateChanged;
			public event ILoadingTrigger.ProgressChangedEventHandler ProgressChanged;
			protected LoaderBase()
			{
				IsLoader = true;
				Uuid = ModBase.GetUuid();
				Name = "未命名任务 " + Conversions.ToString(Uuid) + "#";
				LockState = RuntimeHelpers.GetObjectValue(new object());
				Parent = null;
				HasOnStateChangedThread = false;
				_State = ModBase.LoadState.Waiting;
				_LoadingState = MyLoading.MyLoadingState.Stop;
				Error = null;
				Block = true;
				Show = true;
				IsForceRestarting = false;
				_Progress = -1.0;
				ProgressWeight = 1.0;
			}
			public virtual void InitParent(LoaderBase Parent)
			{
				this.Parent = Parent;
			}
			protected void RaisePreviewFinish()
			{
				PreviewFinish?.Invoke(this);
			}
			public ProgressProvider CreateSyncProgressProvider(double FromPercentage = 0.0, double ToPercentage = 1.0)
			{
				ProgressProvider progressProvider = new ProgressProvider();
				progressProvider.ProgressChanged += [SpecialName] ((double actual, double skiped) ChangedProgress) =>
				{
					(double, double) raw = progressProvider.GetRaw();
					Progress = FromPercentage + (raw.Item1 + raw.Item2) * (ToPercentage - FromPercentage);
				};
				return progressProvider;
			}
			public abstract void Start(object Input = null, bool IsForceRestart = false);
			public abstract void Cancel();
			public abstract void Failed(Exception Ex);
			public CancellationToken CreateCancellationToken()
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (CancellationTokenSource == null || CancellationTokenSource.IsCancellationRequested)
					{
						CancellationTokenSource = new CancellationTokenSource();
						CancellationTokenSource.Token.Register(Cancel);
					}
					if (State == ModBase.LoadState.Canceled)
					{
						CancellationTokenSource.Cancel();
					}
					return CancellationTokenSource.Token;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
			}
			public void WaitForExit(object Input = null, LoaderBase LoaderToSyncProgress = null, bool IsForceRestart = false, CancellationToken c = default(CancellationToken), ProgressProvider p = null)
			{
				Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
				while (State == ModBase.LoadState.Loading)
				{
					if (LoaderToSyncProgress != null)
					{
						LoaderToSyncProgress.Progress = Progress;
					}
					p?.Set(Progress);
					c.ThrowIfCancellationRequested();
					Thread.Sleep(10);
				}
				if (State != ModBase.LoadState.Finished)
				{
					if (State == ModBase.LoadState.Canceled)
					{
						throw new OperationCanceledException("加载器执行已中断");
					}
					if (Information.IsNothing(Error))
					{
						throw new Exception("未知错误！");
					}
					throw new Exception(Error.Message, Error);
				}
			}
			public void WaitForExitTime(int Timeout, object Input = null, string TimeoutMessage = "等待加载器执行超时。", object LoaderToSyncProgress = null, bool IsForceRestart = false)
			{
				Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
				while (State == ModBase.LoadState.Loading)
				{
					if (LoaderToSyncProgress != null)
					{
						NewLateBinding.LateSet(LoaderToSyncProgress, null, "Progress", new object[1] { Progress }, null, null);
					}
					Thread.Sleep(10);
					Timeout = checked(Timeout - 10);
					if (Timeout < 0)
					{
						throw new TimeoutException(TimeoutMessage);
					}
				}
				if (State != ModBase.LoadState.Finished)
				{
					if (State == ModBase.LoadState.Canceled)
					{
						throw new OperationCanceledException("加载器执行已中断");
					}
					if (Information.IsNothing(Error))
					{
						throw new Exception("未知错误！");
					}
					throw Error;
				}
			}
			public override bool Equals(object obj)
			{
				if (obj is LoaderBase loaderBase)
				{
					return Uuid == loaderBase.Uuid;
				}
				return false;
			}
		}
		public class LoaderTask<TIn, TOut> : LoaderBase
		{
			protected internal ThreadPriority ThreadPriority;
			protected internal Action<LoaderTask<TIn, TOut>> LoadDelegate;
			protected internal Func<object> InputDelegate;
			public int ReloadTimeout;
			public long LastFinishedTime;
			public Thread LastRunningThread;
			public TIn Input;
			public TOut Output;
			public bool IsCanceled => IsCanceledWithThread(Thread.CurrentThread);
			public bool IsCanceledWithThread(Thread Thread)
			{
				if (LastRunningThread != null && object.ReferenceEquals(Thread, LastRunningThread))
				{
					return State == ModBase.LoadState.Canceled;
				}
				return true;
			}
			public TIn StartGetInput(TIn Input = default(TIn), Func<TIn> InputDelegate = null)
			{
				if (InputDelegate == null)
				{
					InputDelegate = ((this.InputDelegate == null) ? null : new Func<TIn>(() => (TIn)this.InputDelegate()));
				}
				TIn val = default;
				if ((Input == null || (val != null && Input.Equals(val))) && InputDelegate != null)
				{
					ModBase.RunInUiWait([SpecialName] () =>
					{
						Input = InputDelegate();
					});
				}
				return Input;
			}
			public bool ShouldStart(ref TIn Input, bool IsForceRestart = false, bool IgnoreReloadTimeout = false)
			{
				try
				{
					Input = StartGetInput(Input);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"加载输入获取失败（{Name}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
					Error = ex2;
					object lockState = LockState;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(lockState, ref lockTaken);
						State = ModBase.LoadState.Failed;
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(lockState);
						}
					}
					ProjectData.ClearProjectError();
				}
				if (IsForceRestart)
				{
					return true;
				}
				if (Input == null != (this.Input == null) || (Input != null && !Input.Equals(this.Input)))
				{
					return true;
				}
				if ((State == ModBase.LoadState.Loading || State == ModBase.LoadState.Finished) && (IgnoreReloadTimeout || ReloadTimeout == -1 || LastFinishedTime == 0L || checked(ModBase.GetTimeMs() - LastFinishedTime) < ReloadTimeout))
				{
					return false;
				}
				return true;
			}
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				TIn Input2 = Conversions.ToGenericParameter<TIn>(Input);
				bool flag = ShouldStart(ref Input2, IsForceRestart);
				Input = Input2;
				if (!flag)
				{
					return;
				}
				if (State == ModBase.LoadState.Loading)
				{
					TriggerThreadInterrupt();
				}
				this.Input = Conversions.ToGenericParameter<TIn>(Input);
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					State = ModBase.LoadState.Loading;
					Progress = -1.0;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				LastRunningThread = new Thread([SpecialName] () =>
				{
					try
					{
						IsForceRestarting = IsForceRestart;
						Logger.Trace([SpecialName] () => string.Format("加载线程 {0} 已{1}启动", Name, IsForceRestarting ? "强制" : ""), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
						LoadDelegate(this);
						if (IsCanceled)
						{
							Logger.Warn($"加载线程 {Name} 已中断但线程正常运行至结束，输出被弃用（最新线程：{((LastRunningThread == null) ? (-1) : LastRunningThread.ManagedThreadId)}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
						}
						else
						{
							Logger.Trace([SpecialName] () => $"加载线程 {Name} 已完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
							RaisePreviewFinish();
							State = ModBase.LoadState.Finished;
							LastFinishedTime = ModBase.GetTimeMs();
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						if (ex4.IsCanceled())
						{
							Logger.Trace(ex4, $"加载线程 {Name} 已取消，当前已完成 {Math.Round(Progress * 100.0)}%", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
							if (!IsCanceled)
							{
								State = ModBase.LoadState.Canceled;
							}
						}
						else
						{
							Failed(ex4);
						}
						ProjectData.ClearProjectError();
					}
				})
				{
					Name = "L/" + Name,
					Priority = ThreadPriority
				};
				try
				{
					LastRunningThread.Start();
				}
				catch (ThreadStateException ex)
				{
					ProjectData.SetProjectError(ex);
					ThreadStateException ex2 = ex;
					Thread.Sleep(500);
					LastRunningThread.Start();
					ProjectData.ClearProjectError();
				}
			}
			public override void Failed(Exception ex)
			{
				Error = ex;
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (IsCanceled || State >= ModBase.LoadState.Finished)
					{
						return;
					}
					State = ModBase.LoadState.Failed;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				Logger.Warn(ex, $"加载线程 {Name} 出错，已完成 {Math.Round(Progress * 100.0)}%", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				TriggerThreadInterrupt();
			}
			public override void Cancel()
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State != ModBase.LoadState.Loading)
					{
						return;
					}
					State = ModBase.LoadState.Canceled;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				TriggerThreadInterrupt();
			}
			private void TriggerThreadInterrupt()
			{
				if (LastRunningThread == null)
				{
					return;
				}
				if (LastRunningThread.IsAlive)
				{
					LastRunningThread.Interrupt();
					Logger.Trace([SpecialName] () => $"加载线程 {Name} 已中断", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				}
				LastRunningThread = null;
			}
			public LoaderTask()
			{
				ReloadTimeout = -1;
				LastFinishedTime = 0L;
				LastRunningThread = null;
				Input = default;
				Output = default;
			}
			public LoaderTask(string Name, Action<LoaderTask<TIn, TOut>> LoadDelegate, Func<TIn> InputDelegate = null, ThreadPriority Priority = ThreadPriority.Normal)
			{
				ReloadTimeout = -1;
				LastFinishedTime = 0L;
				LastRunningThread = null;
				Input = default;
				Output = default;
				base.Name = Name;
				this.LoadDelegate = LoadDelegate;
				this.InputDelegate = ((InputDelegate == null) ? null : new Func<object>(() => (object)InputDelegate()));
				ThreadPriority = Priority;
			}
		}
		public class LoaderWorker : LoaderBase
		{
			private IWorker _Worker;
			public IWorker Worker
			{
				get
				{
					return _Worker;
				}
				set
				{
					Action value2 = Worker_Succeeded;
					Action value3 = Cancel;
					Action<Exception> value4 = Failed;
					if (_Worker != null)
					{
						_Worker.Succeeded -= value2;
						_Worker.Canceled -= value3;
						_Worker.Failed -= value4;
					}
					_Worker = value;
					if (_Worker != null)
					{
						_Worker.Succeeded += value2;
						_Worker.Canceled += value3;
						_Worker.Failed += value4;
					}
				}
			}

			public LoaderWorker(string Name, IWorker Worker)
			{
				base.Name = Name;
				this.Worker = Worker;
				Worker.Progress.ProgressChanged += [SpecialName] ((double actual, double skiped) Raw) =>
				{
					Progress = Raw.actual + Raw.skiped;
				};
			}
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					State = ModBase.LoadState.Loading;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				Logger.Trace([SpecialName] () => $"Worker {Name} 已启动", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				IsForceRestarting = IsForceRestart;
				Worker.Start();
			}
			private void Worker_Succeeded()
			{
				Logger.Trace([SpecialName] () => $"Worker {Name} 已完成", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				RaisePreviewFinish();
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					State = ModBase.LoadState.Finished;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
			}
			public override void Cancel()
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State != ModBase.LoadState.Loading)
					{
						return;
					}
					State = ModBase.LoadState.Canceled;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				Logger.Trace([SpecialName] () => $"Worker {Name} 已取消，当前已完成 {Math.Round(Progress * 100.0)}%", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				Worker.Cancel();
			}
			public override void Failed(Exception Ex)
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State >= ModBase.LoadState.Finished)
					{
						return;
					}
					State = ModBase.LoadState.Failed;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				Logger.Warn(Ex, $"Worker {Name} 出错，已完成 {Math.Round(Progress * 100.0)}%", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				Error = Ex;
				Worker.Cancel();
			}
		}
		public class LoaderCombo<InputType> : LoaderBase
		{
			public List<LoaderBase> Loaders;
			public InputType Input;
			public override double Progress
			{
				get
				{
					switch (State)
					{
					case ModBase.LoadState.Waiting:
						return 0.0;
					case ModBase.LoadState.Loading:
					{
						double num = 0.0;
						double num2 = 0.0;
						foreach (LoaderBase loader in Loaders)
						{
							num += loader.ProgressWeight;
							num2 += loader.ProgressWeight * loader.Progress;
						}
						if (num == 0.0)
						{
							return 0.0;
						}
						return num2 / num;
					}
					default:
						return 1.0;
					}
				}
				set
				{
					throw new Exception("多重加载器不支持设置进度");
				}
			}
			public LoaderCombo(string Name, IEnumerable<LoaderBase> Loaders)
			{
				this.Loaders = new List<LoaderBase>();
				this.Loaders.Clear();
				foreach (LoaderBase Loader in Loaders)
				{
					if (Loader != null)
					{
						this.Loaders.Add(Loader);
						Loader.OnStateChangedThread += SubTaskStateChanged;
						Loader.HasOnStateChangedThread = true;
					}
				}
				InitParent(null);
				base.Name = Name;
			}
			public override void InitParent(LoaderBase Parent)
			{
				base.Parent = Parent;
				foreach (LoaderBase loader in Loaders)
				{
					loader.InitParent(this);
				}
			}
			public override void Start(object Input = null, bool IsForceRestart = false)
			{
				IsForceRestarting = IsForceRestart;
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State == ModBase.LoadState.Loading)
					{
						return;
					}
					State = ModBase.LoadState.Loading;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				this.Input = Conversions.ToGenericParameter<InputType>(Input);
				if (IsForceRestart)
				{
					foreach (LoaderBase loader in Loaders)
					{
						loader.State = ModBase.LoadState.Waiting;
					}
				}
				ModBase.RunInThread(Update);
			}
			public override void Cancel()
			{
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State != ModBase.LoadState.Loading && State != ModBase.LoadState.Waiting)
					{
						return;
					}
					State = ModBase.LoadState.Canceled;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				ModBase.RunInThread([SpecialName] () =>
				{
					foreach (LoaderBase loader in Loaders)
					{
						loader.Cancel();
					}
				});
			}
			public override void Failed(Exception Ex)
			{
				Error = Ex;
				object lockState = LockState;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockState, ref lockTaken);
					if (State >= ModBase.LoadState.Finished)
					{
						return;
					}
					State = ModBase.LoadState.Failed;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockState);
					}
				}
				foreach (LoaderBase loader in Loaders)
				{
					loader.Cancel();
				}
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			}
			private void SubTaskStateChanged(LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState)
			{
				switch (NewState)
				{
				case ModBase.LoadState.Finished:
					Update();
					break;
				case ModBase.LoadState.Canceled:
					Cancel();
					break;
				case ModBase.LoadState.Failed:
					Failed(new Exception(Loader.Name + "失败", Loader.Error));
					break;
				case ModBase.LoadState.Waiting:
				case ModBase.LoadState.Loading:
					break;
				}
			}
			private void Update()
			{
				if (State == ModBase.LoadState.Finished || State == ModBase.LoadState.Failed || State == ModBase.LoadState.Canceled)
				{
					return;
				}
				bool flag = true;
				bool flag2 = false;
				object obj = Input;
				foreach (LoaderBase loader in Loaders)
				{
					switch (loader.State)
					{
					case ModBase.LoadState.Finished:
						if (GeneralUtils.IsGenericInstanceOf(loader, typeof(LoaderTask<, >)))
						{
							object[] array2 = new object[2];
							array2[1] = ((obj != null && (object)loader.GetType().GenericTypeArguments.First() == obj.GetType()) ? obj : null);
							array2[0] = true;
							if (Conversions.ToBoolean(NewLateBinding.LateGet(loader, null, "ShouldStart", array2, new string[1] { "IgnoreReloadTimeout" }, null, null)))
							{
								Logger.Info($"由于输入条件变更，重启已完成的加载器 {loader.Name}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
								break;
							}
							obj = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(loader, null, "Output", new object[0], null, null, null));
						}
						if (loader.Block && !flag)
						{
							flag2 = true;
						}
						continue;
					case ModBase.LoadState.Loading:
						if (GeneralUtils.IsGenericInstanceOf(loader, typeof(LoaderTask<, >)))
						{
							object[] array = new object[2];
							array[1] = ((obj != null && (object)loader.GetType().GenericTypeArguments.First() == obj.GetType()) ? obj : null);
							array[0] = true;
							if (Conversions.ToBoolean(NewLateBinding.LateGet(loader, null, "ShouldStart", array, new string[1] { "IgnoreReloadTimeout" }, null, null)))
							{
								Logger.Warn($"由于输入条件变更，重启进行中的加载器 {loader.Name}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
								break;
							}
						}
						flag = false;
						flag2 = true;
						continue;
					}
					flag = false;
					if (flag2)
					{
						continue;
					}
					if (obj != null)
					{
						if (GeneralUtils.IsGenericInstanceOf(loader, typeof(LoaderTask<, >)) || GeneralUtils.IsGenericInstanceOf(loader, typeof(LoaderCombo<>)))
						{
							loader.Start(RuntimeHelpers.GetObjectValue(((object)loader.GetType().GenericTypeArguments.First() == obj.GetType()) ? obj : null), IsForceRestarting);
						}
						else if (loader is LoaderWorker)
						{
							loader.Start(null, IsForceRestarting);
						}
						else
						{
							if (!(loader is ModNet.LoaderDownload))
							{
								throw new Exception("未知的加载器类型（" + loader.GetType().Name + "）");
							}
							loader.Start(RuntimeHelpers.GetObjectValue((obj is List<ModNet.NetFile>) ? obj : null), IsForceRestarting);
						}
					}
					else
					{
						loader.Start(null, IsForceRestarting);
					}
					if (loader.Block)
					{
						flag2 = true;
					}
				}
				if (flag)
				{
					RaisePreviewFinish();
					State = ModBase.LoadState.Finished;
					ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				}
			}
			public static void GetLoaderList(object Loader, ref List<LoaderBase> List, bool RequireShow = true)
			{
				foreach (object item in (IEnumerable)NewLateBinding.LateGet(Loader, null, "Loaders", new object[0], null, null, null))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(item);
					if (Conversions.ToBoolean(Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Show", new object[0], null, null, null)) || !RequireShow))
					{
						List.Add((LoaderBase)objectValue);
					}
					if (GeneralUtils.IsGenericInstanceOf(RuntimeHelpers.GetObjectValue(objectValue), typeof(LoaderCombo<>)))
					{
						GetLoaderList(RuntimeHelpers.GetObjectValue(objectValue), ref List);
					}
				}
			}
			public void GetLoaderList(ref List<LoaderBase> List, bool RequireShow = true)
			{
				GetLoaderList(this, ref List, RequireShow);
			}
			public List<LoaderBase> GetLoaderList(bool RequireShow = true)
			{
				List<LoaderBase> List = new List<LoaderBase>();
				GetLoaderList(ref List, RequireShow);
				return List;
			}
		}
		private struct LoaderFolderDictionaryEntry
		{
			public DateTime? LastCheckTime;
			public string FolderPath;
			public override bool Equals(object obj)
			{
				if (!(obj is LoaderFolderDictionaryEntry loaderFolderDictionaryEntry))
				{
					return false;
				}
				return EqualityComparer<DateTime?>.Default.Equals(LastCheckTime, loaderFolderDictionaryEntry.LastCheckTime) && Operators.CompareString(FolderPath, loaderFolderDictionaryEntry.FolderPath, TextCompare: false) == 0;
			}
		}
		public enum LoaderFolderRunType
		{
			RunOnUpdated,
			ForceRun,
			UpdateOnly
		}
		public static ConcurrentList<LoaderBase> LoaderTaskbar = new ConcurrentList<LoaderBase>();
		public static double LoaderTaskbarProgress = 0.0;
		private static TaskbarItemProgressState LoaderTaskbarProgressLast = TaskbarItemProgressState.None;
		private static Dictionary<LoaderBase, LoaderFolderDictionaryEntry> LoaderFolderDictionary = new Dictionary<LoaderBase, LoaderFolderDictionaryEntry>();
		public static void LoaderTaskbarAdd<T>(LoaderCombo<T> Loader)
		{
			if (ModMain.FrmSpeedLeft != null)
			{
				ModMain.FrmSpeedLeft.TaskRemove(Loader);
			}
			LoaderTaskbar.Add(Loader);
			Logger.Info($"{Loader.Name} 已加入任务列表", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
		}
		public static void LoaderTaskbarProgressRefresh()
		{
			try
			{
				double num = LoaderTaskbarProgressGet();
				foreach (LoaderBase item in LoaderTaskbar)
				{
					if (LoaderTaskbar.All([SpecialName] (LoaderBase l) => l.State != ModBase.LoadState.Loading) || item.State == ModBase.LoadState.Waiting || item.State == ModBase.LoadState.Canceled)
					{
						ModMain.FrmSpeedLeft?.TaskRefresh(item);
						LoaderTaskbar.Remove(item);
						Logger.Info($"{item.Name} 已移出任务列表", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
					}
				}
				if (num <= 0.0 || num >= 1.0 || LoaderTaskbarProgress > num)
				{
					LoaderTaskbarProgress = num;
				}
				else
				{
					LoaderTaskbarProgress = LoaderTaskbarProgress * 0.9 + num * 0.1;
				}
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.BtnExtraDownload.Progress = LoaderTaskbarProgress;
				});
				TaskbarItemProgressState taskbarItemProgressState;
				if (!LoaderTaskbar.Any() || LoaderTaskbarProgress == 1.0)
				{
					taskbarItemProgressState = TaskbarItemProgressState.None;
				}
				else if (LoaderTaskbarProgress < 0.015)
				{
					taskbarItemProgressState = TaskbarItemProgressState.Indeterminate;
				}
				else
				{
					taskbarItemProgressState = TaskbarItemProgressState.Normal;
					ModMain.FrmMain.TaskbarItemInfo.ProgressValue = LoaderTaskbarProgress;
				}
				if (LoaderTaskbarProgressLast != taskbarItemProgressState)
				{
					LoaderTaskbarProgressLast = taskbarItemProgressState;
					ModMain.FrmMain.TaskbarItemInfo.ProgressState = taskbarItemProgressState;
					ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "刷新任务栏进度显示失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static double LoaderTaskbarProgressGet()
		{
			double result;
			try
			{
				result = (LoaderTaskbar.Any() ? LoaderTaskbar.Select([SpecialName] (LoaderBase l) => l.Progress).Average().Clamp(0.0, 1.0) : 1.0);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "获取任务栏进度出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				result = 0.5;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static bool LoaderFolderRun(LoaderBase Loader, string FolderPath, LoaderFolderRunType Type, int MaxDepth = 0, string ExtraPath = "", bool WaitForExit = false)
		{
			LoaderFolderDictionaryEntry value = new LoaderFolderDictionaryEntry
			{
				FolderPath = FolderPath + ExtraPath,
				LastCheckTime = null
			};
			try
			{
				DirectoryInfo info = DirectoryUtils.GetInfo(FolderPath + ExtraPath);
				value.LastCheckTime = (info.Exists ? new DateTime?(GetActualLastWriteTimeUtc(info, MaxDepth)) : ((DateTime?)(object)null));
				if (Type == LoaderFolderRunType.RunOnUpdated && LoaderFolderDictionary.ContainsKey(Loader))
				{
					if (info.Exists)
					{
						if (LoaderFolderDictionary[Loader].LastCheckTime.HasValue && value.Equals(LoaderFolderDictionary[Loader]))
						{
							return false;
						}
					}
					else if (!LoaderFolderDictionary[Loader].LastCheckTime.HasValue)
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "文件夹加载器启动检测出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModLoader.vb");
				ProjectData.ClearProjectError();
			}
			LoaderFolderDictionary[Loader] = value;
			if (Type == LoaderFolderRunType.UpdateOnly)
			{
				return false;
			}
			if (WaitForExit)
			{
				Loader.WaitForExit(FolderPath, null, IsForceRestart: true);
			}
			else
			{
				Loader.Start(FolderPath, IsForceRestart: true);
			}
			return true;
		}
		private static DateTime GetActualLastWriteTimeUtc(DirectoryInfo FolderInfo, int MaxDepth)
		{
			DateTime dateTime = FolderInfo.LastWriteTimeUtc;
			if (MaxDepth > 0)
			{
				foreach (DirectoryInfo item in FolderInfo.EnumerateDirectories())
				{
					DateTime actualLastWriteTimeUtc = GetActualLastWriteTimeUtc(item, checked(MaxDepth - 1));
					if (DateTime.Compare(actualLastWriteTimeUtc, dateTime) > 0)
					{
						dateTime = actualLastWriteTimeUtc;
					}
				}
			}
			return dateTime;
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
	}
}
