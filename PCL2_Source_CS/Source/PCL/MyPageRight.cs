// PCL

namespace PCL
{
	public class MyPageRight : AdornerDecorator
	{
		public enum PageStates
		{
			Empty,
			LoaderWait,
			LoaderEnter,
			LoaderStayForce,
			LoaderStay,
			LoaderExit,
			ContentEnter,
			ContentStay,
			ContentExit,
			PageExit
		}
		public delegate void PageEnterEventHandler();
		public delegate void PageExitEventHandler();
		[CompilerGenerated]
		internal sealed class _Closure___19_0<_CLS0, _CLS1>
		{
			public Action<ModLoader.LoaderTask<_CLS0, _CLS1>> _VB_Local_FinishedInvoke;
			public ModLoader.LoaderTask<_CLS0, _CLS1> _VB_Local_RealLoader;
			public MyPageRight _VB_Me;
			public Action _I1;
			public _Closure___19_0(_Closure___19_0<_CLS0, _CLS1> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_FinishedInvoke = arg0._VB_Local_FinishedInvoke;
					_VB_Local_RealLoader = arg0._VB_Local_RealLoader;
				}
			}
			[SpecialName]
			internal void _Lambda___R1(ModLoader.LoaderBase a0)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				while (_VB_Me.PageState == PageStates.PageExit || _VB_Me.PageState == PageStates.ContentExit)
				{
					Thread.Sleep(10);
				}
				ModBase.RunInUiWait([SpecialName] () =>
				{
					_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
				});
				Thread.Sleep(20);
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
			}
			[SpecialName]
			internal void _Lambda___4()
			{
				_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
			}
			[SpecialName]
			internal void _Lambda___R2(object a0, MouseButtonEventArgs a1)
			{
				_Lambda___5();
			}
			[SpecialName]
			internal void _Lambda___5()
			{
				if (_VB_Local_RealLoader.State == ModBase.LoadState.Failed)
				{
					_VB_Me.PageLoaderRestart();
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___19_1<_CLS0, _CLS1>
		{
			public ModLoader.LoaderBase _VB_Local_Loader;
			public ModBase.LoadState _VB_Local_NewState;
			public ModBase.LoadState _VB_Local_OldState;
			public MyPageRight _VB_Me;
			public _Closure___19_1(_Closure___19_1<_CLS0, _CLS1> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Loader = arg0._VB_Local_Loader;
					_VB_Local_NewState = arg0._VB_Local_NewState;
					_VB_Local_OldState = arg0._VB_Local_OldState;
				}
			}
			[SpecialName]
			internal void _Lambda___3()
			{
				_VB_Me.PageLoaderState(_VB_Local_Loader, _VB_Local_NewState, _VB_Local_OldState);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___20_0<_CLS0>
		{
			public Action<ModLoader.LoaderCombo<_CLS0>> _VB_Local_FinishedInvoke;
			public ModLoader.LoaderCombo<_CLS0> _VB_Local_RealLoader;
			public MyPageRight _VB_Me;
			public Action _I1;
			public _Closure___20_0(_Closure___20_0<_CLS0> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_FinishedInvoke = arg0._VB_Local_FinishedInvoke;
					_VB_Local_RealLoader = arg0._VB_Local_RealLoader;
				}
			}
			[SpecialName]
			internal void _Lambda___R3(ModLoader.LoaderBase a0)
			{
				_Lambda___0();
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				while (_VB_Me.PageState == PageStates.PageExit || _VB_Me.PageState == PageStates.ContentExit)
				{
					Thread.Sleep(10);
				}
				ModBase.RunInUiWait([SpecialName] () =>
				{
					_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
				});
				Thread.Sleep(20);
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
			}
			[SpecialName]
			internal void _Lambda___4()
			{
				_VB_Local_FinishedInvoke(_VB_Local_RealLoader);
			}
			[SpecialName]
			internal void _Lambda___R4(object a0, MouseButtonEventArgs a1)
			{
				_Lambda___5();
			}
			[SpecialName]
			internal void _Lambda___5()
			{
				if (_VB_Local_RealLoader.State == ModBase.LoadState.Failed)
				{
					_VB_Me.PageLoaderRestart();
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___20_1<_CLS0>
		{
			public ModLoader.LoaderBase _VB_Local_Loader;
			public ModBase.LoadState _VB_Local_NewState;
			public ModBase.LoadState _VB_Local_OldState;
			public MyPageRight _VB_Me;
			public _Closure___20_1(_Closure___20_1<_CLS0> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Loader = arg0._VB_Local_Loader;
					_VB_Local_NewState = arg0._VB_Local_NewState;
					_VB_Local_OldState = arg0._VB_Local_OldState;
				}
			}
			[SpecialName]
			internal void _Lambda___3()
			{
				_VB_Me.PageLoaderState(_VB_Local_Loader, _VB_Local_NewState, _VB_Local_OldState);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___42_0
		{
			public IEnumerable<FrameworkElement> _VB_Local_RealElements;
			public MyPageRight _VB_Me;
			public _Closure___42_0(_Closure___42_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_RealElements = arg0._VB_Local_RealElements;
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				foreach (FrameworkElement item in _VB_Local_RealElements)
				{
					item.Visibility = Visibility.Collapsed;
				}
				_VB_Me.PageOnExitAnimationFinished();
			}
		}
		public int PageUuid;
		private static readonly object PanScrollProperty = DependencyProperty.Register("PanScroll", typeof(string), typeof(MyPageRight), new PropertyMetadata(null));
		private PageStates _PageState;
		private ModLoader.LoaderBase PageLoader;
		private object PageLoaderInputInvoke;
		private MyLoading PageLoaderUi;
		private FrameworkElement PanLoader;
		private FrameworkElement PanContent;
		private FrameworkElement PanAlways;
		private bool PageLoaderAutoRun;
		public List<FrameworkElement> DisabledPageAnimControls;
		public string PanScroll
		{
			get
			{
				return Conversions.ToString(GetValue((DependencyProperty)PanScrollProperty));
			}
			set
			{
				object[] array = new object[2] { PanScrollProperty, value };
				object[] array2 = array;
				bool[] array3 = new bool[2] { false, true };
				bool[] array4 = array3;
				NewLateBinding.LateCall(this, null, "SetValue", array, null, null, array3, IgnoreReturn: true);
				if (array4[1])
				{
					value = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[1]), typeof(string));
				}
			}
		}
		public PageStates PageState
		{
			get
			{
				return _PageState;
			}
			set
			{
				if (_PageState != value)
				{
					_PageState = value;
					Logger.Trace($"页面状态切换为 {value}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
				}
			}
		}
		public event PageEnterEventHandler PageEnter;
		public event PageExitEventHandler PageExit;
		public MyPageRight()
		{
			PageUuid = ModBase.GetUuid();
			_PageState = PageStates.Empty;
			DisabledPageAnimControls = new List<FrameworkElement>();
		}
		public void PageLoaderInit<TIn, TOut>(MyLoading LoaderUi, FrameworkElement PanLoader, FrameworkElement PanContent, FrameworkElement PanAlways, ModLoader.LoaderTask<TIn, TOut> RealLoader, Action<ModLoader.LoaderTask<TIn, TOut>> FinishedInvoke = null, Func<TIn> InputInvoke = null, bool AutoRun = true)
		{
			_Closure___19_0<TIn, TOut> arg = default;
			_Closure___19_0<TIn, TOut> CS_8__locals19 = new _Closure___19_0<TIn, TOut>(arg);
			CS_8__locals19._VB_Me = this;
			CS_8__locals19._VB_Local_RealLoader = RealLoader;
			CS_8__locals19._VB_Local_FinishedInvoke = FinishedInvoke;
			this.PanLoader = PanLoader;
			this.PanContent = PanContent;
			this.PanAlways = PanAlways;
			PageLoader = CS_8__locals19._VB_Local_RealLoader;
			PageLoaderUi = LoaderUi;
			PageLoaderInputInvoke = InputInvoke;
			PageLoaderAutoRun = AutoRun;
			if (CS_8__locals19._VB_Local_FinishedInvoke != null)
			{
				CS_8__locals19._VB_Local_RealLoader.PreviewFinish += [SpecialName] (ModLoader.LoaderBase a0) =>
				{
					CS_8__locals19._Lambda___0();
				};
			}
			CS_8__locals19._VB_Local_RealLoader.OnStateChangedUi += [SpecialName] (ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState) =>
			{
				_Closure___19_1<TIn, TOut> arg2 = default;
				_Closure___19_1<TIn, TOut> CS_8__locals23 = new _Closure___19_1<TIn, TOut>(arg2);
				CS_8__locals23._VB_Me = this;
				CS_8__locals23._VB_Local_Loader = Loader;
				CS_8__locals23._VB_Local_NewState = NewState;
				CS_8__locals23._VB_Local_OldState = OldState;
				ModBase.RunInUi([SpecialName] () =>
				{
					CS_8__locals23._VB_Me.PageLoaderState(CS_8__locals23._VB_Local_Loader, CS_8__locals23._VB_Local_NewState, CS_8__locals23._VB_Local_OldState);
				});
			};
			PanLoader.Visibility = Visibility.Collapsed;
			PanContent.Visibility = Visibility.Collapsed;
			if (PanAlways != null)
			{
				PanAlways.Visibility = Visibility.Collapsed;
			}
			if (PageLoaderAutoRun)
			{
				PageLoader.Start(CS_8__locals19._VB_Local_RealLoader.StartGetInput(default, (Func<TIn>)PageLoaderInputInvoke));
			}
			if (PageLoader.State == ModBase.LoadState.Finished && CS_8__locals19._VB_Local_FinishedInvoke != null)
			{
				ModBase.RunInUiWait([SpecialName] () =>
				{
					CS_8__locals19._VB_Local_FinishedInvoke(CS_8__locals19._VB_Local_RealLoader);
				});
			}
			PageLoaderUi.State = CS_8__locals19._VB_Local_RealLoader;
			PageLoaderUi.Click += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				CS_8__locals19._Lambda___5();
			};
		}
		public void PageLoaderInit<TIn>(MyLoading LoaderUi, FrameworkElement PanLoader, FrameworkElement PanContent, FrameworkElement PanAlways, ModLoader.LoaderCombo<TIn> RealLoader, Action<ModLoader.LoaderCombo<TIn>> FinishedInvoke = null, Func<TIn> InputInvoke = null, bool AutoRun = true)
		{
			_Closure___20_0<TIn> arg = default;
			_Closure___20_0<TIn> CS_8__locals18 = new _Closure___20_0<TIn>(arg);
			CS_8__locals18._VB_Me = this;
			CS_8__locals18._VB_Local_RealLoader = RealLoader;
			CS_8__locals18._VB_Local_FinishedInvoke = FinishedInvoke;
			this.PanLoader = PanLoader;
			this.PanContent = PanContent;
			this.PanAlways = PanAlways;
			PageLoader = CS_8__locals18._VB_Local_RealLoader;
			PageLoaderUi = LoaderUi;
			PageLoaderInputInvoke = InputInvoke;
			PageLoaderAutoRun = AutoRun;
			if (CS_8__locals18._VB_Local_FinishedInvoke != null)
			{
				CS_8__locals18._VB_Local_RealLoader.PreviewFinish += [SpecialName] (ModLoader.LoaderBase a0) =>
				{
					CS_8__locals18._Lambda___0();
				};
			}
			CS_8__locals18._VB_Local_RealLoader.OnStateChangedUi += [SpecialName] (ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState) =>
			{
				_Closure___20_1<TIn> arg2 = default;
				_Closure___20_1<TIn> CS_8__locals22 = new _Closure___20_1<TIn>(arg2);
				CS_8__locals22._VB_Me = this;
				CS_8__locals22._VB_Local_Loader = Loader;
				CS_8__locals22._VB_Local_NewState = NewState;
				CS_8__locals22._VB_Local_OldState = OldState;
				ModBase.RunInUi([SpecialName] () =>
				{
					CS_8__locals22._VB_Me.PageLoaderState(CS_8__locals22._VB_Local_Loader, CS_8__locals22._VB_Local_NewState, CS_8__locals22._VB_Local_OldState);
				});
			};
			PanLoader.Visibility = Visibility.Collapsed;
			PanContent.Visibility = Visibility.Collapsed;
			if (PanAlways != null)
			{
				PanAlways.Visibility = Visibility.Collapsed;
			}
			if (PageLoaderAutoRun)
			{
				object obj = null;
				if (PageLoaderInputInvoke != null)
				{
					obj = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(PageLoaderInputInvoke, new object[0], null));
				}
				PageLoader.Start(RuntimeHelpers.GetObjectValue(obj));
			}
			if (PageLoader.State == ModBase.LoadState.Finished && CS_8__locals18._VB_Local_FinishedInvoke != null)
			{
				ModBase.RunInUiWait([SpecialName] () =>
				{
					CS_8__locals18._VB_Local_FinishedInvoke(CS_8__locals18._VB_Local_RealLoader);
				});
			}
			PageLoaderUi.State = CS_8__locals18._VB_Local_RealLoader;
			PageLoaderUi.Click += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				CS_8__locals18._Lambda___5();
			};
		}
		public void PageLoaderRestart(object Input = null, bool IsForceRestart = true)
		{
			if (!PageLoaderAutoRun)
			{
				return;
			}
			if (GeneralUtils.IsGenericInstanceOf(PageLoader, typeof(ModLoader.LoaderTask<, >)) || PageLoader is ModLoader.LoaderWorker)
			{
				ModLoader.LoaderBase pageLoader = PageLoader;
				ModLoader.LoaderBase pageLoader2 = PageLoader;
				object[] array = new object[2] { Input, null };
				ref object pageLoaderInputInvoke = ref PageLoaderInputInvoke;
				array[1] = pageLoaderInputInvoke;
				object[] array2 = array;
				bool[] array3 = new bool[2] { true, true };
				bool[] array4 = array3;
				object obj = NewLateBinding.LateGet(pageLoader2, null, "StartGetInput", array, null, null, array3);
				if (array4[0])
				{
					Input = RuntimeHelpers.GetObjectValue(array2[0]);
				}
				if (array4[1])
				{
					pageLoaderInputInvoke = RuntimeHelpers.GetObjectValue(array2[1]);
				}
				pageLoader.Start(RuntimeHelpers.GetObjectValue(obj), IsForceRestart);
			}
			else
			{
				if (Input == null && PageLoaderInputInvoke != null)
				{
					Input = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(PageLoaderInputInvoke, new object[0], null));
				}
				PageLoader.Start(RuntimeHelpers.GetObjectValue(Input), IsForceRestart);
			}
		}
		public void PageOnEnter()
		{
			Logger.Trace("已触发 PageOnEnter", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			PageEnter?.Invoke();
			switch (PageState)
			{
			case PageStates.Empty:
				if (PageLoader == null || PageLoader.State == ModBase.LoadState.Finished || PageLoader.State == ModBase.LoadState.Waiting || PageLoader.State == ModBase.LoadState.Canceled)
				{
					PageState = PageStates.ContentEnter;
					TriggerEnterAnimation(PanAlways, (FrameworkElement)(PanContent ?? Child));
				}
				else if (PageLoader.State == ModBase.LoadState.Loading)
				{
					PageState = PageStates.LoaderWait;
					ModAnimation.AniStart(ModAnimation.AaCode(PageOnLoaderWaitFinished, 400), "PageRight PageChange " + Conversions.ToString(PageUuid));
				}
				else
				{
					PageState = PageStates.LoaderEnter;
					TriggerEnterAnimation(PanAlways, PanLoader);
				}
				break;
			case PageStates.ContentExit:
				if (PageLoader == null || PageLoader.State == ModBase.LoadState.Finished || PageLoader.State == ModBase.LoadState.Waiting || PageLoader.State == ModBase.LoadState.Canceled)
				{
					PageState = PageStates.ContentEnter;
					TriggerEnterAnimation((FrameworkElement)(PanContent ?? Child));
				}
				else if (PageLoader.State == ModBase.LoadState.Loading)
				{
					PageState = PageStates.LoaderWait;
					ModAnimation.AniStart(ModAnimation.AaCode(PageOnLoaderWaitFinished, 400), "PageRight PageChange " + Conversions.ToString(PageUuid));
				}
				else
				{
					PageState = PageStates.LoaderEnter;
					TriggerEnterAnimation(PanLoader);
				}
				break;
			default:
				throw new Exception("在状态为 " + PageState.ToString() + " 时触发了 PageOnEnter 事件。");
			case PageStates.ContentEnter:
				break;
			}
		}
		public void PageOnExit()
		{
			Logger.Trace("已触发 PageOnExit", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			PageExit?.Invoke();
			switch (PageState)
			{
			case PageStates.ContentEnter:
			case PageStates.ContentStay:
				PageState = PageStates.PageExit;
				TriggerExitAnimation(PanAlways, (FrameworkElement)(PanContent ?? Child));
				break;
			case PageStates.LoaderEnter:
			case PageStates.LoaderStayForce:
			case PageStates.LoaderStay:
				PageState = PageStates.PageExit;
				TriggerExitAnimation(PanAlways, PanLoader);
				break;
			case PageStates.LoaderWait:
				PageState = PageStates.PageExit;
				TriggerExitAnimation(PanAlways);
				break;
			case PageStates.LoaderExit:
			case PageStates.ContentExit:
				PageState = PageStates.PageExit;
				if (PanAlways != null)
				{
					TriggerExitAnimation(PanAlways, (FrameworkElement)(PanContent ?? Child));
				}
				break;
			case PageStates.Empty:
			case PageStates.PageExit:
				break;
			}
		}
		public void PageOnForceExit()
		{
			if (PageState == PageStates.Empty)
			{
				return;
			}
			Logger.Trace("已触发 PageOnForceExit", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			PageState = PageStates.Empty;
			ModAnimation.AniStop("PageRight PageChange " + Conversions.ToString(PageUuid));
			if (PageLoader == null)
			{
				Child.Visibility = Visibility.Collapsed;
				return;
			}
			PanContent.Visibility = Visibility.Collapsed;
			PanLoader.Visibility = Visibility.Collapsed;
			if (PanAlways != null)
			{
				PanAlways.Visibility = Visibility.Collapsed;
			}
		}
		public void PageOnContentExit()
		{
			Logger.Trace("已触发 PageOnContentExit", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			if (PageLoader != null && PageLoader.State == ModBase.LoadState.Loading)
			{
				throw new Exception("在调用 PageOnContentExit 时，加载器不能为 Loading 状态");
			}
			switch (PageState)
			{
			case PageStates.ContentEnter:
			case PageStates.ContentStay:
				PageState = PageStates.ContentExit;
				TriggerExitAnimation(PanContent);
				break;
			case PageStates.LoaderExit:
				PageState = PageStates.ContentExit;
				break;
			case PageStates.LoaderEnter:
			case PageStates.LoaderStayForce:
			case PageStates.LoaderStay:
				PageState = PageStates.ContentExit;
				TriggerExitAnimation(PanLoader);
				break;
			case PageStates.LoaderWait:
				PageOnEnter();
				break;
			case PageStates.Empty:
				if (IsLoaded)
				{
					PageOnEnter();
				}
				break;
			}
		}
		private void PageOnEnterAnimationFinished()
		{
			Logger.Trace("已触发 PageOnEnterAnimationFinished", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			switch (PageState)
			{
			case PageStates.ContentEnter:
				PageState = PageStates.ContentStay;
				break;
			case PageStates.LoaderEnter:
				PageState = PageStates.LoaderStayForce;
				ModAnimation.AniStart(ModAnimation.AaCode(PageOnLoaderStayFinished, 400), "PageRight PageChange " + Conversions.ToString(PageUuid));
				break;
			default:
				throw new Exception("在状态为 " + PageState.ToString() + " 时触发了 PageOnEnterAnimationFinished 事件。");
			}
		}
		private void PageOnExitAnimationFinished()
		{
			Logger.Trace("已触发 PageOnExitAnimationFinished", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			switch (PageState)
			{
			case PageStates.PageExit:
				PageState = PageStates.Empty;
				break;
			case PageStates.ContentExit:
				PageOnEnter();
				break;
			case PageStates.LoaderExit:
				PageState = PageStates.ContentEnter;
				TriggerEnterAnimation(PanContent);
				break;
			default:
				throw new Exception("在状态为 " + PageState.ToString() + " 时触发了 PageOnExitAnimationFinished 事件。");
			}
		}
		private void PageOnLoaderWaitFinished()
		{
			Logger.Trace("已触发 PageOnLoaderWaitFinished", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			PageStates pageState = PageState;
			if (pageState == PageStates.LoaderWait)
			{
				PageState = PageStates.LoaderEnter;
				if (PanAlways != null && PanAlways.Visibility == Visibility.Collapsed)
				{
					TriggerEnterAnimation(PanAlways, PanLoader);
				}
				else
				{
					TriggerEnterAnimation(PanLoader);
				}
				return;
			}
			throw new Exception("在状态为 " + PageState.ToString() + " 时触发了 PageOnLoaderWaitFinished 事件。");
		}
		private void PageOnLoaderStayFinished()
		{
			Logger.Trace("已触发 PageOnLoaderStayFinished", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
			PageStates pageState = PageState;
			if (pageState == PageStates.LoaderStayForce)
			{
				if (PageLoader.State == ModBase.LoadState.Finished)
				{
					PageState = PageStates.LoaderExit;
					TriggerExitAnimation(PanLoader);
				}
				else
				{
					PageState = PageStates.LoaderStay;
				}
				return;
			}
			throw new Exception("在状态为 " + PageState.ToString() + " 时触发了 PageOnLoaderWaitFinished 事件。");
		}
		private void PageLoaderState(object sender, ModBase.LoadState NewState, ModBase.LoadState OldState)
		{
			switch (NewState)
			{
			case ModBase.LoadState.Loading:
			case ModBase.LoadState.Failed:
				if (OldState != ModBase.LoadState.Failed && OldState != ModBase.LoadState.Loading)
				{
					Logger.Trace("已触发 PageLoaderState (Start/Refresh)", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
					switch (PageState)
					{
					case PageStates.ContentEnter:
					case PageStates.ContentStay:
						PageState = PageStates.ContentExit;
						TriggerExitAnimation(PanContent);
						break;
					case PageStates.LoaderExit:
						PageState = PageStates.ContentExit;
						break;
					}
				}
				break;
			case ModBase.LoadState.Waiting:
			case ModBase.LoadState.Finished:
			case ModBase.LoadState.Canceled:
				if (OldState != ModBase.LoadState.Failed && OldState != ModBase.LoadState.Loading)
				{
					break;
				}
				Logger.Trace("已触发 PageLoaderState (Stop/Canceled)", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyPageRight.vb");
				switch (PageState)
				{
				case PageStates.LoaderWait:
					PageState = PageStates.ContentEnter;
					if (PanAlways != null && PanAlways.Visibility == Visibility.Collapsed)
					{
						TriggerEnterAnimation(PanAlways, PanContent);
					}
					else
					{
						TriggerEnterAnimation(PanContent);
					}
					break;
				case PageStates.LoaderStay:
					PageState = PageStates.LoaderExit;
					TriggerExitAnimation(PanLoader);
					break;
				}
				break;
			}
		}
		public void TriggerEnterAnimation(params FrameworkElement[] Elements)
		{
			IEnumerable<FrameworkElement> enumerable = Elements.Where([SpecialName] (FrameworkElement e) => e != null);
			foreach (FrameworkElement item in enumerable)
			{
				item.Visibility = Visibility.Visible;
			}
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			foreach (FrameworkElement item2 in enumerable)
			{
				foreach (FrameworkElement allAnimControl in GetAllAnimControls(item2, IgnoreInvisibility: true))
				{
					allAnimControl.IsHitTestVisible = true;
					if (allAnimControl.RenderTransform != null && allAnimControl.RenderTransform is TranslateTransform)
					{
						allAnimControl.RenderTransform = null;
					}
				}
				foreach (FrameworkElement allAnimControl2 in GetAllAnimControls(item2))
				{
					if (allAnimControl2 is MyExtraTextButton)
					{
						((MyExtraTextButton)allAnimControl2).Show = true;
						continue;
					}
					allAnimControl2.Opacity = 0.0;
					allAnimControl2.RenderTransform = new TranslateTransform(0.0, -16.0);
					list.Add(ModAnimation.AaOpacity(allAnimControl2, 1.0, 100, num, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
					list.Add(ModAnimation.AaTranslateY(allAnimControl2, 5.0, 250, num, new ModAnimation.AniEaseOutFluent()));
					list.Add(ModAnimation.AaTranslateY(allAnimControl2, 11.0, 350, num, new ModAnimation.AniEaseOutBack()));
					num = checked(num + 25);
				}
			}
			MyScrollBar firstScrollViewer = GetFirstScrollViewer(enumerable);
			if (firstScrollViewer != null)
			{
				if (!(firstScrollViewer.RenderTransform is TranslateTransform))
				{
					firstScrollViewer.RenderTransform = new TranslateTransform(10.0, 0.0);
				}
				list.Add(ModAnimation.AaTranslateX(firstScrollViewer, 0.0 - ((TranslateTransform)firstScrollViewer.RenderTransform).X, 350, 0, new ModAnimation.AniEaseOutFluent()));
			}
			list.Add(ModAnimation.AaCode([SpecialName] () =>
			{
				PageOnEnterAnimationFinished();
			}, 0, After: true));
			ModAnimation.AniStart(list, "PageRight PageChange " + Conversions.ToString(PageUuid), RefreshTime: true);
		}
		public void TriggerExitAnimation(params FrameworkElement[] Elements)
		{
			_Closure___42_0 arg = default;
			_Closure___42_0 CS_8__locals6 = new _Closure___42_0(arg);
			CS_8__locals6._VB_Me = this;
			CS_8__locals6._VB_Local_RealElements = Elements.Where([SpecialName] (FrameworkElement e) => e != null);
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			int num = 0;
			foreach (FrameworkElement item in CS_8__locals6._VB_Local_RealElements)
			{
				foreach (FrameworkElement allAnimControl in GetAllAnimControls(item))
				{
					if (allAnimControl is MyExtraTextButton)
					{
						((MyExtraTextButton)allAnimControl).Show = false;
						continue;
					}
					allAnimControl.IsHitTestVisible = false;
					list.Add(ModAnimation.AaOpacity(allAnimControl, -1.0, 70, num));
					list.Add(ModAnimation.AaTranslateY(allAnimControl, -6.0, 70, num));
					num = checked(num + 15);
				}
			}
			MyScrollBar firstScrollViewer = GetFirstScrollViewer(CS_8__locals6._VB_Local_RealElements);
			if (firstScrollViewer != null)
			{
				if (!(firstScrollViewer.RenderTransform is TranslateTransform))
				{
					firstScrollViewer.RenderTransform = new TranslateTransform();
				}
				list.Add(ModAnimation.AaTranslateX(firstScrollViewer, 10.0 - ((TranslateTransform)firstScrollViewer.RenderTransform).X, 90, 0, new ModAnimation.AniEaseInFluent()));
			}
			list.Add(ModAnimation.AaCode([SpecialName] () =>
			{
				foreach (FrameworkElement item2 in CS_8__locals6._VB_Local_RealElements)
				{
					item2.Visibility = Visibility.Collapsed;
				}
				CS_8__locals6._VB_Me.PageOnExitAnimationFinished();
			}, 0, After: true));
			ModAnimation.AniStart(list, "PageRight PageChange " + Conversions.ToString(PageUuid));
		}
		internal IEnumerable<FrameworkElement> GetAllAnimControls(FrameworkElement Element, bool IgnoreInvisibility = false)
		{
			List<FrameworkElement> AllControls = new List<FrameworkElement>();
			_GetAllAnimControls(Element, ref AllControls, IgnoreInvisibility);
			return AllControls.Except(DisabledPageAnimControls);
		}
		private void _GetAllAnimControls(FrameworkElement Element, ref List<FrameworkElement> AllControls, bool IgnoreInvisibility)
		{
			if (!IgnoreInvisibility && Element.Visibility == Visibility.Collapsed)
			{
				return;
			}
			if (Element is MyCard || Element is MyHint || Element is MyExtraTextButton || Element is TextBlock || Element is MyTextButton)
			{
				AllControls.Add(Element);
			}
			else if (Element is ContentControl)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(((ContentControl)Element).Content);
				if (objectValue != null && objectValue is FrameworkElement)
				{
					_GetAllAnimControls((FrameworkElement)objectValue, ref AllControls, IgnoreInvisibility);
				}
			}
			else
			{
				if (!(Element is System.Windows.Controls.Panel))
				{
					return;
				}
				foreach (object child in ((System.Windows.Controls.Panel)Element).Children)
				{
					object objectValue2 = RuntimeHelpers.GetObjectValue(child);
					if (objectValue2 is FrameworkElement)
					{
						_GetAllAnimControls((FrameworkElement)objectValue2, ref AllControls, IgnoreInvisibility);
					}
				}
			}
		}
		private MyScrollBar GetFirstScrollViewer(IEnumerable<FrameworkElement> Elements)
		{
			MyScrollViewer myScrollViewer = null;
			foreach (FrameworkElement Element in Elements)
			{
				if (Element is MyScrollViewer)
				{
					myScrollViewer = (MyScrollViewer)Element;
					goto IL_008d;
				}
				foreach (object child in LogicalTreeHelper.GetChildren(Element))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(child);
					if (!(objectValue is MyScrollViewer))
					{
						continue;
					}
					myScrollViewer = (MyScrollViewer)objectValue;
					goto IL_008d;
				}
			}
			return null;
			IL_008d:
			if (myScrollViewer.ComputedVerticalScrollBarVisibility != Visibility.Visible)
			{
				return null;
			}
			return myScrollViewer.ScrollBar;
		}
	}
}
