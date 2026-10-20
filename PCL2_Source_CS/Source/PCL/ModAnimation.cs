// PCL

namespace PCL
{
	[StandardModule]
	public static class ModAnimation
	{
		public class AniGroupEntry
		{
			public List<AniData> Data;
			public long StartTick;
			public int Uuid;
			public AniGroupEntry()
			{
				Uuid = ModBase.GetUuid();
			}
		}
		public struct AniData
		{
			public AniType TypeMain;
			public AniTypeSub TypeSub;
			public int TimeTotal;
			public double TimeFinished;
			public double TimePercent;
			public bool IsAfter;
			public AniEase Ease;
			public object Obj;
			public object Value;
			public object ValueLast;
			public override string ToString()
			{
				return TypeMain.ToString() + " | " + Conversions.ToString(TimeFinished) + "/" + Conversions.ToString(TimeTotal) + "(" + Conversions.ToString(Math.Round(TimePercent * 100.0)) + "%)" + ((Obj == null) ? "" : (" | " + Obj.ToString() + "(" + Obj.GetType().Name + ")"));
			}
		}
		public enum AniType
		{
			Number,
			Color,
			Scale,
			TextAppear,
			Code,
			ScaleTransform,
			RotateTransform
		}
		public enum AniTypeSub
		{
			X,
			Y,
			Width,
			Height,
			Opacity,
			Value,
			Radius,
			BorderThickness,
			StrokeThickness,
			TranslateX,
			TranslateY,
			Double,
			DoubleParam,
			GridLengthWidth
		}
		public enum AniEasePower
		{
			Weak = 2,
			Middle,
			Strong,
			ExtraStrong
		}
		public abstract class AniEase
		{
			public abstract double GetValue(double t);
			public virtual double GetDelta(double t1, double t0)
			{
				return GetValue(t1) - GetValue(t0);
			}
		}
		public class AniEaseInout : AniEase
		{
			private readonly AniEase EaseIn;
			private readonly AniEase EaseOut;
			private readonly double EaseInPercent;
			public AniEaseInout(AniEase EaseIn, AniEase EaseOut, double EaseInPercent = 0.5)
			{
				this.EaseIn = EaseIn;
				this.EaseOut = EaseOut;
				this.EaseInPercent = EaseInPercent;
			}
			public override double GetValue(double t)
			{
				if (t < EaseInPercent)
				{
					return EaseInPercent * EaseIn.GetValue(t / EaseInPercent);
				}
				return (1.0 - EaseInPercent) * EaseOut.GetValue((t - EaseInPercent) / (1.0 - EaseInPercent)) + EaseInPercent;
			}
		}
		public class AniEaseLinear : AniEase
		{
			public override double GetValue(double t)
			{
				return t.Clamp(0.0, 1.0);
			}
			public override double GetDelta(double t1, double t0)
			{
				return t1.Clamp(0.0, 1.0) - t0.Clamp(0.0, 1.0);
			}
		}
		public class AniEaseInFluent : AniEase
		{
			private readonly AniEasePower p;
			public AniEaseInFluent(AniEasePower Power = AniEasePower.Middle)
			{
				p = Power;
			}
			public override double GetValue(double t)
			{
				return Math.Pow(t.Clamp(0.0, 1.0), (double)p);
			}
		}
		public class AniEaseOutFluent : AniEase
		{
			private readonly AniEasePower p;
			public AniEaseOutFluent(AniEasePower Power = AniEasePower.Middle)
			{
				p = Power;
			}
			public override double GetValue(double t)
			{
				return 1.0 - Math.Pow((1.0 - t).Clamp(0.0, 1.0), (double)p);
			}
		}
		public class AniEaseInoutFluent : AniEase
		{
			private AniEaseInout Ease;
			public AniEaseInoutFluent(AniEasePower Power = AniEasePower.Middle, double Middle = 0.5)
			{
				Ease = new AniEaseInout(new AniEaseInFluent(Power), new AniEaseOutFluent(Power), Middle);
			}
			public override double GetValue(double t)
			{
				return Ease.GetValue(t);
			}
		}
		public class AniEaseOutFluentWithInitial : AniEase
		{
			private readonly double alpha;
			public AniEaseOutFluentWithInitial(double InitialPixelPerSecond, double TotalSecond, double TotalDistance)
			{
				double num = InitialPixelPerSecond * TotalSecond / TotalDistance;
				alpha = num - 1.0;
				if (alpha < 0.0)
				{
					alpha = 0.0;
				}
			}
			public override double GetValue(double percent)
			{
				double num = percent.Clamp(0.0, 1.0);
				if (alpha == 0.0)
				{
					return num;
				}
				return (alpha + 1.0) * num / (1.0 + alpha * num);
			}
		}
		public class AniEaseInBack : AniEase
		{
			private readonly double p;
			public AniEaseInBack(AniEasePower Power = AniEasePower.Middle)
			{
				p = 3.0 - (double)Power * 0.5;
			}
			public override double GetValue(double t)
			{
				t = t.Clamp(0.0, 1.0);
				return Math.Pow(t, p) * Math.Cos(4.71238898038469 * (1.0 - t));
			}
		}
		public class AniEaseOutBack : AniEase
		{
			private readonly double p;
			public AniEaseOutBack(AniEasePower Power = AniEasePower.Middle)
			{
				p = 3.0 - (double)Power * 0.5;
			}
			public override double GetValue(double t)
			{
				t = t.Clamp(0.0, 1.0);
				return 1.0 - Math.Pow(1.0 - t, p) * Math.Cos(4.71238898038469 * t);
			}
		}
		public class AniEaseInCar : AniEase
		{
			private AniEaseInout Ease;
			public AniEaseInCar(double Middle = 0.7, AniEasePower Power = AniEasePower.Middle)
			{
				Ease = new AniEaseInout(new AniEaseInBack(Power), new AniEaseOutFluent(Power), Middle);
			}
			public override double GetValue(double t)
			{
				return Ease.GetValue(t);
			}
		}
		public class AniEaseOutCar : AniEase
		{
			private AniEaseInout Ease;
			public AniEaseOutCar(double Middle = 0.3, AniEasePower Power = AniEasePower.Middle)
			{
				Ease = new AniEaseInout(new AniEaseInFluent(Power), new AniEaseOutBack(Power), Middle);
			}
			public override double GetValue(double t)
			{
				return Ease.GetValue(t);
			}
		}
		public class AniEaseInElastic : AniEase
		{
			private readonly int p;
			public AniEaseInElastic(AniEasePower Power = AniEasePower.Middle)
			{
				p = (int)checked(Power + 4);
			}
			public override double GetValue(double t)
			{
				t = t.Clamp(0.0, 1.0);
				return Math.Pow(t, (double)checked(p - 1) * 0.25) * Math.Cos(((double)p - 3.5) * Math.PI * Math.Pow(1.0 - t, 1.5));
			}
		}
		public class AniEaseOutElastic : AniEase
		{
			private readonly int p;
			public AniEaseOutElastic(AniEasePower Power = AniEasePower.Middle)
			{
				p = (int)checked(Power + 4);
			}
			public override double GetValue(double t)
			{
				t = 1.0 - t.Clamp(0.0, 1.0);
				return 1.0 - Math.Pow(t, (double)checked(p - 1) * 0.25) * Math.Cos(((double)p - 3.5) * Math.PI * Math.Pow(1.0 - t, 1.5));
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___62_0
		{
			public long _VB_Local_DeltaTime;
			public _Closure___62_0(_Closure___62_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_DeltaTime = arg0._VB_Local_DeltaTime;
				}
			}
			[SpecialName]
			internal void _Lambda___1()
			{
				AniCount = 0;
				AniTimer((double)_VB_Local_DeltaTime * AniSpeed);
				if (ModBase.RandomInteger(0, checked(64 * (ModBase.ModeDebug ? 5 : 30))) != 0 || ((AniFPS >= 62 || AniFPS <= 0) && AniCount <= 4 && ModNet.NetManager.FileRemain == 0))
				{
					return;
				}
				Logger.Info($"FPS {AniFPS}, 动画 {AniCount}, 下载中 {ModNet.NetManager.FileRemain}（{StringUtils.FormatByteSize(ModNet.NetManager.Speed)}/s）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
				if (ModNet.NetManager.FileRemain >= 5)
				{
					return;
				}
				foreach (ModNet.NetFile value in ModNet.NetManager.AllFiles.Values)
				{
					if (value.State < ModNet.NetState.Finished)
					{
						Logger.Trace($"- {value.State} {StringUtils.FormatByteSize(value.Speed)}/s {value.LocalPath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					}
				}
			}
		}
		public static Dictionary<string, AniGroupEntry> AniGroups = new Dictionary<string, AniGroupEntry>();
		private static long AniLastTick;
		public static bool AniRunning = false;
		private static int _AniControlEnabled = 0;
		private static readonly object AniControlEnabledLock = RuntimeHelpers.GetObjectValue(new object());
		private static int AniCount = 0;
		private static int AniFPSCounter = 0;
		private static long AniFPSTimer = 0L;
		public static int AniFPS = 0;
		public static double AniSpeed
		{
			get
			{
				int num = Settings.Get<int>("SystemDebugAnim");
				if (num < 30)
				{
					return ((double)num * 0.1 + 0.1).Clamp(0.1, 3.0);
				}
				return 200.0;
			}
		}
		public static int AniControlEnabled
		{
			get
			{
				return _AniControlEnabled;
			}
			set
			{
				object aniControlEnabledLock = AniControlEnabledLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(aniControlEnabledLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(aniControlEnabledLock, ref lockTaken);
					_AniControlEnabled = value;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(aniControlEnabledLock);
					}
				}
			}
		}
		public static void AniDispose(MyCard Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (Control.IsHitTestVisible)
			{
				Control.IsHitTestVisible = false;
				AniStart(new AniData[4]
				{
					AaScaleTransform(Control, -0.08, 200, 0, new AniEaseInFluent()),
					AaOpacity(Control, -1.0, 200, 0, new AniEaseOutFluent()),
					AaHeight(Control, 0.0 - Control.ActualHeight, 150, 100, new AniEaseOutFluent()),
					AaCode([SpecialName] () =>
					{
						if (RemoveFromChildren)
						{
							if (Control.Parent == null)
							{
								return;
							}
							object[] array3;
							bool[] array4;
							NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array3 = new object[1] { Control }, null, null, array4 = new bool[1] { true }, IgnoreReturn: true);
							if (array4[0])
							{
								Control = (MyCard)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(MyCard));
							}
						}
						else
						{
							Control.Visibility = Visibility.Collapsed;
						}
						if (CallBack != null)
						{
							CallBack(Control);
						}
					}, 0, After: true)
				}, "MyCard Dispose " + Conversions.ToString(Control.Uuid));
				return;
			}
			if (RemoveFromChildren)
			{
				if (Control.Parent == null)
				{
					return;
				}
				object[] array;
				bool[] array2;
				NewLateBinding.LateCall(NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null), null, "Remove", array = new object[1] { Control }, null, null, array2 = new bool[1] { true }, IgnoreReturn: true);
				if (array2[0])
				{
					Control = (MyCard)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(MyCard));
				}
			}
			else
			{
				Control.Visibility = Visibility.Collapsed;
			}
			if (CallBack != null)
			{
				CallBack(Control);
			}
		}
		public static void AniDispose(MyHint Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (!Control.IsHitTestVisible)
			{
				return;
			}
			Control.IsHitTestVisible = false;
			AniStart(new AniData[4]
			{
				AaScaleTransform(Control, -0.08, 200, 0, new AniEaseInFluent()),
				AaOpacity(Control, -1.0, 200, 0, new AniEaseOutFluent()),
				AaHeight(Control, 0.0 - Control.ActualHeight, 150, 100, new AniEaseOutFluent()),
				AaCode([SpecialName] () =>
				{
					if (RemoveFromChildren)
					{
						object instance = NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null);
						object[] array = new object[1] { Control };
						object[] array2 = array;
						bool[] array3 = new bool[1] { true };
						bool[] array4 = array3;
						NewLateBinding.LateCall(instance, null, "Remove", array, null, null, array3, IgnoreReturn: true);
						if (array4[0])
						{
							Control = (MyHint)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(MyHint));
						}
					}
					else
					{
						Control.Visibility = Visibility.Collapsed;
					}
					if (CallBack != null)
					{
						CallBack(Control);
					}
				}, 0, After: true)
			}, "MyCard Dispose " + Conversions.ToString(Control.Uuid));
		}
		public static void AniDispose(MyIconButton Control, bool RemoveFromChildren, ParameterizedThreadStart CallBack = null)
		{
			if (!Control.IsHitTestVisible)
			{
				return;
			}
			Control.IsHitTestVisible = false;
			AniStart(new AniData[2]
			{
				AaScaleTransform(Control, -1.5, 200, 0, new AniEaseInFluent()),
				AaCode([SpecialName] () =>
				{
					if (RemoveFromChildren)
					{
						object instance = NewLateBinding.LateGet(Control.Parent, null, "Children", new object[0], null, null, null);
						object[] array = new object[1] { Control };
						object[] array2 = array;
						bool[] array3 = new bool[1] { true };
						bool[] array4 = array3;
						NewLateBinding.LateCall(instance, null, "Remove", array, null, null, array3, IgnoreReturn: true);
						if (array4[0])
						{
							Control = (MyIconButton)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(MyIconButton));
						}
					}
					else
					{
						Control.Visibility = Visibility.Collapsed;
					}
					if (CallBack != null)
					{
						CallBack(Control);
					}
				}, 0, After: true)
			}, "MyIconButton Dispose " + Conversions.ToString(Control.Uuid));
		}
		public static AniData AaX(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.X,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaY(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Y,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaWidth(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Width,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaHeight(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Height,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaOpacity(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Opacity,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaValue(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Value,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaRadius(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Radius,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaBorderThickness(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.BorderThickness,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaStrokeThickness(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.StrokeThickness,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaGridLengthWidth(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.GridLengthWidth,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaDouble(object Obj, DependencyProperty Prop, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.Double,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = new object[3] { Obj, Prop, "" },
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaDouble(ParameterizedThreadStart Lambda, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.DoubleParam,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = Lambda,
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaColor(FrameworkElement Obj, DependencyProperty Prop, ModBase.MyColor Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Color,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = new object[3] { Obj, Prop, "" },
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay),
				ValueLast = new ModBase.MyColor(0.0, 0.0, 0.0, 0.0)
			};
		}
		public static AniData AaColor(FrameworkElement Obj, DependencyProperty Prop, string Res, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Color,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = new object[3] { Obj, Prop, Res },
				Value = new ModBase.MyColor(RuntimeHelpers.GetObjectValue(System.Windows.Application.Current.FindResource(Res))) - new ModBase.MyColor(RuntimeHelpers.GetObjectValue(Obj.GetValue(Prop))),
				IsAfter = After,
				TimeFinished = checked(-Delay),
				ValueLast = new ModBase.MyColor(0.0, 0.0, 0.0, 0.0)
			};
		}
		public static AniData AaScale(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false, bool Absolute = false)
		{
			ModBase.MyRect value = ((!Absolute) ? new ModBase.MyRect(Conversions.ToDouble(Operators.MultiplyObject(Operators.MultiplyObject(-0.5, NewLateBinding.LateGet(Obj, null, "ActualWidth", new object[0], null, null, null)), Value)), Conversions.ToDouble(Operators.MultiplyObject(Operators.MultiplyObject(-0.5, NewLateBinding.LateGet(Obj, null, "ActualHeight", new object[0], null, null, null)), Value)), Conversions.ToDouble(Operators.MultiplyObject(NewLateBinding.LateGet(Obj, null, "ActualWidth", new object[0], null, null, null), Value)), Conversions.ToDouble(Operators.MultiplyObject(NewLateBinding.LateGet(Obj, null, "ActualHeight", new object[0], null, null, null), Value))) : new ModBase.MyRect(-0.5 * Value, -0.5 * Value, Value, Value));
			return new AniData
			{
				TypeMain = AniType.Scale,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaTextAppear(object Obj, bool Hide = false, bool TimePerText = true, int Time = 70, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return checked(new AniData
			{
				TypeMain = AniType.TextAppear,
				Ease = (Ease ?? new AniEaseLinear()),
				TimeTotal = (TimePerText ? (Time * ((Obj is TextBlock) ? NewLateBinding.LateGet(Obj, null, "Text", new object[0], null, null, null) : NewLateBinding.LateGet(Obj, null, "Context", new object[0], null, null, null).ToString()).ToString().Length) : Time),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = new object[2]
				{
					(Obj is TextBlock) ? NewLateBinding.LateGet(Obj, null, "Text", new object[0], null, null, null) : NewLateBinding.LateGet(Obj, null, "Context", new object[0], null, null, null).ToString(),
					Hide
				},
				IsAfter = After,
				TimeFinished = -Delay
			});
		}
		public static AniData AaCode(ThreadStart Code, int Delay = 0, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Code,
				TimeTotal = 1,
				Value = Code,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaScaleTransform(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.ScaleTransform,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaRotateTransform(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.RotateTransform,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaTranslateX(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.TranslateX,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static AniData AaTranslateY(object Obj, double Value, int Time = 400, int Delay = 0, AniEase Ease = null, bool After = false)
		{
			return new AniData
			{
				TypeMain = AniType.Number,
				TypeSub = AniTypeSub.TranslateY,
				TimeTotal = Time,
				Ease = (Ease ?? new AniEaseLinear()),
				Obj = RuntimeHelpers.GetObjectValue(Obj),
				Value = Value,
				IsAfter = After,
				TimeFinished = checked(-Delay)
			};
		}
		public static List<AniData> AaStack(StackPanel Stack, int Time = 100, int Delay = 25)
		{
			List<AniData> list = new List<AniData>();
			int num = 0;
			foreach (object child in Stack.Children)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(child);
				NewLateBinding.LateSet(objectValue, null, "Opacity", new object[1] { 0 }, null, null);
				list.Add(AaOpacity(RuntimeHelpers.GetObjectValue(objectValue), 1.0, Time, num));
				num = checked(num + Delay);
			}
			return list;
		}
		public static void AniStart(IList AniGroup, string Name = "", bool RefreshTime = false)
		{
			if (RefreshTime)
			{
				AniLastTick = ModBase.GetTimeMs();
			}
			List<AniData> list = new List<AniData>();
			foreach (object item in AniGroup)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				if (objectValue is ICollection)
				{
					list.AddRange((IEnumerable<AniData>)objectValue);
				}
				else
				{
					list.Add((objectValue != null) ? ((AniData)objectValue) : default(AniData));
				}
			}
			AniGroupEntry aniGroupEntry = new AniGroupEntry
			{
				Data = list,
				StartTick = ModBase.GetTimeMs()
			};
			if (Operators.CompareString(Name, "", TextCompare: false) == 0)
			{
				Name = Conversions.ToString(aniGroupEntry.Uuid);
			}
			else
			{
				AniStop(Name);
			}
			AniGroups.Add(Name, aniGroupEntry);
		}
		public static void AniStart(AniData AniGroup, string Name = "", bool RefreshTime = false)
		{
			AniStart(new List<AniData> { AniGroup }, Name, RefreshTime);
		}
		public static void AniStop(string Name)
		{
			AniGroups.Remove(Name);
		}
		public static bool AniIsRun(string Name)
		{
			return AniGroups.ContainsKey(Name);
		}
		public static void AniStart()
		{
			AniLastTick = ModBase.GetTimeMs();
			AniFPSTimer = AniLastTick;
			AniRunning = true;
			ModBase.RunInNewThread(checked([SpecialName] () =>
			{
				try
				{
					Logger.Info("动画线程开始", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					_Closure___62_0 obj = default;
					while (true)
					{
						obj = new _Closure___62_0(obj);
						obj._VB_Local_DeltaTime = (ModBase.GetTimeMs() - AniLastTick).Clamp(0L, 100000L);
						if (obj._VB_Local_DeltaTime >= 3)
						{
							AniLastTick = ModBase.GetTimeMs();
							if (ModBase.ModeDebug)
							{
								if ((AniLastTick - AniFPSTimer).Clamp(0L, 100000L) >= 500)
								{
									AniFPS = AniFPSCounter;
									AniFPSCounter = 0;
									AniFPSTimer = AniLastTick;
								}
								AniFPSCounter += 2;
							}
							ModBase.RunInUiWait(obj._Lambda___1);
						}
						Thread.Sleep(1);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "动画帧执行失败", LogBehavior.AlertThenCrash, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					ProjectData.ClearProjectError();
				}
			}), "Animation", ThreadPriority.AboveNormal);
		}
		public static void AniTimer(double DeltaTick)
		{
			checked
			{
				try
				{
					if (DeltaTick / AniSpeed > 100.0)
					{
						Logger.Info($"两个动画帧间隔 {DeltaTick} ms", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					}
					int num = -1;
					while (num + 1 < AniGroups.Count)
					{
						num++;
						AniGroupEntry aniGroupEntry = AniGroups.Values.ElementAtOrDefault(num);
						if (aniGroupEntry.StartTick > AniLastTick)
						{
							continue;
						}
						bool flag = true;
						int num2 = 0;
						while (num2 < aniGroupEntry.Data.Count)
						{
							AniData aniData = aniGroupEntry.Data[num2];
							if (!aniData.IsAfter)
							{
								flag = false;
								aniData.TimeFinished += DeltaTick;
								if (aniData.TimeFinished > 0.0)
								{
									aniData = AniRun(aniData);
									AniCount++;
								}
								if (aniData.TimeFinished >= (double)aniData.TimeTotal)
								{
									if (Conversions.ToBoolean(aniData.TypeMain == AniType.Color && Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateIndexGet(aniData.Obj, new object[1] { 2 }, null), "", TextCompare: false)))))
									{
										object instance = NewLateBinding.LateIndexGet(aniData.Obj, new object[1] { 0 }, null);
										object[] array = new object[2];
										object obj = aniData.Obj;
										object[] array2 = new object[1];
										object obj2 = (array2[0] = 1);
										array[0] = NewLateBinding.LateIndexGet(obj, array2, null);
										object obj3 = aniData.Obj;
										object[] array3 = new object[1];
										object obj4 = (array3[0] = 2);
										array[1] = NewLateBinding.LateIndexGet(obj3, array3, null);
										object[] array4 = array;
										bool[] array5 = new bool[2] { true, true };
										bool[] array6 = array5;
										NewLateBinding.LateCall(instance, null, "SetResourceReference", array, null, null, array5, IgnoreReturn: true);
										if (array6[0])
										{
											NewLateBinding.LateIndexSetComplex(obj, new object[2]
											{
												obj2,
												array4[0]
											}, null, OptimisticSet: true, RValueBase: false);
										}
										if (array6[1])
										{
											NewLateBinding.LateIndexSetComplex(obj3, new object[2]
											{
												obj4,
												array4[1]
											}, null, OptimisticSet: true, RValueBase: false);
										}
									}
									aniGroupEntry.Data.RemoveAt(num2);
								}
								else
								{
									aniGroupEntry.Data[num2] = aniData;
									num2++;
								}
							}
							else
							{
								if (!flag)
								{
									break;
								}
								flag = false;
								aniData.IsAfter = false;
								aniGroupEntry.Data[num2] = aniData;
							}
						}
						if (aniGroupEntry.Data.Any())
						{
							continue;
						}
						int num3 = AniGroups.Count - 1;
						for (int i = 0; i <= num3; i++)
						{
							if (AniGroups.ElementAt(i).Value.Uuid == aniGroupEntry.Uuid)
							{
								AniGroups.Remove(AniGroups.ElementAt(i).Key);
								break;
							}
						}
						num--;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "动画刻执行失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		private static AniData AniRun(AniData Ani)
		{
			checked
			{
				try
				{
					switch (Ani.TypeMain)
					{
					case AniType.Number:
					{
						double num3 = MathUtils.Lerp(0.0, Conversions.ToDouble(Ani.Value), Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, Ani.TimePercent));
						if (num3 == 0.0)
						{
							break;
						}
						switch (Ani.TypeSub)
						{
						case AniTypeSub.X:
						{
							if (Ani.Obj is Window)
							{
								((Window)Ani.Obj).Left += num3;
								break;
							}
							object left2 = NewLateBinding.LateGet(Ani.Obj, null, "HorizontalAlignment", new object[0], null, null, null);
							if (Conversions.ToBoolean(Conversions.ToBoolean(Operators.CompareObjectEqual(left2, System.Windows.HorizontalAlignment.Left, TextCompare: false)) || Conversions.ToBoolean(Operators.CompareObjectEqual(left2, System.Windows.HorizontalAlignment.Stretch, TextCompare: false))))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "Margin", new object[1]
								{
									new Thickness(Conversions.ToDouble(Operators.AddObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Left", new object[0], null, null, null), num3)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Top", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Right", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Bottom", new object[0], null, null, null)))
								}, null, null);
							}
							else if (Operators.ConditionalCompareObjectEqual(left2, System.Windows.HorizontalAlignment.Right, TextCompare: false))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "Margin", new object[1]
								{
									new Thickness(Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Left", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Top", new object[0], null, null, null)), Conversions.ToDouble(Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Right", new object[0], null, null, null), num3)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Bottom", new object[0], null, null, null)))
								}, null, null);
							}
							break;
						}
						case AniTypeSub.Y:
						{
							if (Ani.Obj is Window)
							{
								((Window)Ani.Obj).Top += num3;
								break;
							}
							object left3 = NewLateBinding.LateGet(Ani.Obj, null, "VerticalAlignment", new object[0], null, null, null);
							if (Operators.ConditionalCompareObjectEqual(left3, VerticalAlignment.Top, TextCompare: false))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "Margin", new object[1]
								{
									new Thickness(Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Left", new object[0], null, null, null)), Conversions.ToDouble(Operators.AddObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Top", new object[0], null, null, null), num3)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Right", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Bottom", new object[0], null, null, null)))
								}, null, null);
							}
							else if (Operators.ConditionalCompareObjectEqual(left3, VerticalAlignment.Bottom, TextCompare: false))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "Margin", new object[1]
								{
									new Thickness(Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Left", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Top", new object[0], null, null, null)), Conversions.ToDouble(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Right", new object[0], null, null, null)), Conversions.ToDouble(Operators.SubtractObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Margin", new object[0], null, null, null), null, "Bottom", new object[0], null, null, null), num3)))
								}, null, null);
							}
							break;
						}
						case AniTypeSub.Opacity:
							if (Ani.Obj is UIElement)
							{
								UIElement uIElement = (UIElement)Ani.Obj;
								uIElement.Opacity = (uIElement.Opacity + num3).Clamp(0.0, 1.0);
							}
							else if (Ani.Obj is DropShadowEffect)
							{
								DropShadowEffect dropShadowEffect = (DropShadowEffect)Ani.Obj;
								dropShadowEffect.Opacity = (dropShadowEffect.Opacity + num3).Clamp(0.0, 1.0);
							}
							break;
						case AniTypeSub.Width:
						{
							FrameworkElement frameworkElement3 = (FrameworkElement)Ani.Obj;
							frameworkElement3.Width = Math.Max((double.IsNaN(frameworkElement3.Width) ? frameworkElement3.ActualWidth : frameworkElement3.Width) + num3, 0.0);
							break;
						}
						case AniTypeSub.Height:
						{
							FrameworkElement frameworkElement4 = (FrameworkElement)Ani.Obj;
							frameworkElement4.Height = Math.Max((double.IsNaN(frameworkElement4.Height) ? frameworkElement4.ActualHeight : frameworkElement4.Height) + num3, 0.0);
							break;
						}
						case AniTypeSub.Value:
						{
							object obj = Ani.Obj;
							NewLateBinding.LateSet(obj, null, "Value", new object[1] { Operators.AddObject(NewLateBinding.LateGet(obj, null, "Value", new object[0], null, null, null), num3) }, null, null);
							break;
						}
						case AniTypeSub.Radius:
						{
							object obj = Ani.Obj;
							NewLateBinding.LateSet(obj, null, "Radius", new object[1] { Operators.AddObject(NewLateBinding.LateGet(obj, null, "Radius", new object[0], null, null, null), num3) }, null, null);
							break;
						}
						case AniTypeSub.StrokeThickness:
							NewLateBinding.LateSet(Ani.Obj, null, "StrokeThickness", new object[1] { NewLateBinding.LateGet(null, typeof(Math), "Max", new object[2]
							{
								Operators.AddObject(NewLateBinding.LateGet(Ani.Obj, null, "StrokeThickness", new object[0], null, null, null), num3),
								0
							}, null, null, null) }, null, null);
							break;
						case AniTypeSub.BorderThickness:
						{
							object obj5 = Ani.Obj;
							object[] array10 = new object[1];
							object obj6 = NewLateBinding.LateGet(Ani.Obj, null, "BorderThickness", new object[0], null, null, null);
							array10[0] = new Thickness(((obj6 != null) ? ((Thickness)obj6) : default(Thickness)).Bottom + num3);
							NewLateBinding.LateSet(obj5, null, "BorderThickness", array10, null, null);
							break;
						}
						case AniTypeSub.TranslateX:
							if (Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null))) || !(NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null) is TranslateTransform))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "RenderTransform", new object[1]
								{
									new TranslateTransform(0.0, 0.0)
								}, null, null);
							}
							((TranslateTransform)NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null)).X += num3;
							break;
						case AniTypeSub.TranslateY:
							if (Information.IsNothing(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null))) || !(NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null) is TranslateTransform))
							{
								NewLateBinding.LateSet(Ani.Obj, null, "RenderTransform", new object[1]
								{
									new TranslateTransform(0.0, 0.0)
								}, null, null);
							}
							((TranslateTransform)NewLateBinding.LateGet(Ani.Obj, null, "RenderTransform", new object[0], null, null, null)).Y += num3;
							break;
						case AniTypeSub.Double:
						{
							object instance = NewLateBinding.LateIndexGet(Ani.Obj, new object[1] { 0 }, null);
							object[] array = new object[2];
							object obj = Ani.Obj;
							object instance2 = obj;
							object[] array2 = new object[1];
							object obj2 = (array2[0] = 1);
							array[0] = NewLateBinding.LateIndexGet(instance2, array2, null);
							object instance3 = NewLateBinding.LateIndexGet(Ani.Obj, new object[1] { 0 }, null);
							object[] array3 = new object[1];
							object obj3 = Ani.Obj;
							object[] array4 = new object[1];
							object obj4 = (array4[0] = 1);
							array3[0] = NewLateBinding.LateIndexGet(obj3, array4, null);
							object[] array5 = array3;
							bool[] array6 = new bool[1] { true };
							bool[] array7 = array6;
							object left = NewLateBinding.LateGet(instance3, null, "GetValue", array3, null, null, array6);
							if (array7[0])
							{
								NewLateBinding.LateIndexSetComplex(obj3, new object[2]
								{
									obj4,
									array5[0]
								}, null, OptimisticSet: true, RValueBase: false);
							}
							array[1] = Operators.AddObject(left, num3);
							object[] array8 = array;
							bool[] array9;
							NewLateBinding.LateCall(instance, null, "SetValue", array, null, null, array9 = new bool[2] { true, false }, IgnoreReturn: true);
							if (array9[0])
							{
								NewLateBinding.LateIndexSetComplex(obj, new object[2]
								{
									obj2,
									array8[0]
								}, null, OptimisticSet: true, RValueBase: false);
							}
							break;
						}
						case AniTypeSub.DoubleParam:
							((ParameterizedThreadStart)Ani.Obj)(num3);
							break;
						case AniTypeSub.GridLengthWidth:
							NewLateBinding.LateSet(Ani.Obj, null, "Width", new object[1]
							{
								new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[2]
								{
									Operators.AddObject(NewLateBinding.LateGet(NewLateBinding.LateGet(Ani.Obj, null, "Width", new object[0], null, null, null), null, "Value", new object[0], null, null, null), num3),
									0
								}, null, null, null)), GridUnitType.Star)
							}, null, null);
							break;
						}
						break;
					}
					case AniType.Color:
					{
						ModBase.MyColor myColor = ModBase.MyColor.Lerp(new ModBase.MyColor(0.0, 0.0, 0.0, 0.0), (ModBase.MyColor)Ani.Value, Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, Ani.TimePercent)) + (ModBase.MyColor)Ani.ValueLast;
						FrameworkElement frameworkElement2 = (FrameworkElement)NewLateBinding.LateIndexGet(Ani.Obj, new object[1] { 0 }, null);
						DependencyProperty dependencyProperty = (DependencyProperty)NewLateBinding.LateIndexGet(Ani.Obj, new object[1] { 1 }, null);
						ModBase.MyColor myColor2 = new ModBase.MyColor(RuntimeHelpers.GetObjectValue(frameworkElement2.GetValue(dependencyProperty))) + myColor;
						frameworkElement2.SetValue(dependencyProperty, RuntimeHelpers.GetObjectValue((Operators.CompareString(dependencyProperty.PropertyType.Name, "Color", TextCompare: false) == 0) ? ((object)(System.Windows.Media.Color)myColor2) : ((SolidColorBrush)myColor2)));
						Ani.ValueLast = myColor2 - new ModBase.MyColor(RuntimeHelpers.GetObjectValue(frameworkElement2.GetValue(dependencyProperty)));
						break;
					}
					case AniType.Scale:
					{
						FrameworkElement frameworkElement6 = (FrameworkElement)Ani.Obj;
						double delta = Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, Ani.TimePercent);
						frameworkElement6.Margin = new Thickness(frameworkElement6.Margin.Left + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Left", new object[0], null, null, null)), delta), frameworkElement6.Margin.Top + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Top", new object[0], null, null, null)), delta), frameworkElement6.Margin.Right + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Left", new object[0], null, null, null)), delta), frameworkElement6.Margin.Bottom + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Top", new object[0], null, null, null)), delta));
						frameworkElement6.Width = Math.Max(frameworkElement6.Width + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Width", new object[0], null, null, null)), delta), 0.0);
						frameworkElement6.Height = Math.Max(frameworkElement6.Height + MathUtils.Lerp(0.0, Conversions.ToDouble(NewLateBinding.LateGet(Ani.Value, null, "Height", new object[0], null, null, null)), delta), 0.0);
						break;
					}
					case AniType.TextAppear:
					{
						string text = Conversions.ToString(NewLateBinding.LateIndexGet(Ani.Value, new object[1] { 0 }, null));
						int num2 = (int)Math.Round((double)(Conversions.ToBoolean(NewLateBinding.LateIndexGet(Ani.Value, new object[1] { 1 }, null)) ? text.Length : 0) + Math.Round((double)(text.Length * ((!Conversions.ToBoolean(NewLateBinding.LateIndexGet(Ani.Value, new object[1] { 1 }, null))) ? 1 : (-1))) * Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, 0.0)));
						string text2 = text.Substring(0, num2);
						if (num2 < text.Length)
						{
							text2 = ((Convert.ToInt32(Convert.ToChar(text.Substring(num2, 1))) < Convert.ToInt32(Convert.ToChar(128))) ? (text2 + ModBase.RandomInteger(0, 85).ConvertRadix(86)) : (text2 + Encoding.GetEncoding("GB18030").GetString(new byte[2]
							{
								(byte)ModBase.RandomInteger(176, 247),
								(byte)ModBase.RandomInteger(161, 249)
							})));
						}
						if (Ani.Obj is TextBlock)
						{
							NewLateBinding.LateSet(Ani.Obj, null, "Text", new object[1] { text2 }, null, null);
						}
						else
						{
							NewLateBinding.LateSet(Ani.Obj, null, "Context", new object[1] { text2 }, null, null);
						}
						break;
					}
					case AniType.Code:
						Ani.TimeFinished = Ani.TimeTotal;
						((ThreadStart)Ani.Value)();
						break;
					case AniType.ScaleTransform:
					{
						FrameworkElement frameworkElement5 = (FrameworkElement)Ani.Obj;
						if (!(frameworkElement5.RenderTransform is ScaleTransform))
						{
							if (frameworkElement5.RenderTransformOrigin == new System.Windows.Point(0.0, 0.0))
							{
								frameworkElement5.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
							}
							frameworkElement5.RenderTransform = new ScaleTransform(1.0, 1.0);
						}
						double num4 = MathUtils.Lerp(0.0, Conversions.ToDouble(Ani.Value), Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, Ani.TimePercent));
						((ScaleTransform)frameworkElement5.RenderTransform).ScaleX = Math.Max(((ScaleTransform)frameworkElement5.RenderTransform).ScaleX + num4, 0.0);
						((ScaleTransform)frameworkElement5.RenderTransform).ScaleY = Math.Max(((ScaleTransform)frameworkElement5.RenderTransform).ScaleY + num4, 0.0);
						break;
					}
					case AniType.RotateTransform:
					{
						FrameworkElement frameworkElement = (FrameworkElement)Ani.Obj;
						if (!(frameworkElement.RenderTransform is RotateTransform))
						{
							if (frameworkElement.RenderTransformOrigin == new System.Windows.Point(0.0, 0.0))
							{
								frameworkElement.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
							}
							frameworkElement.RenderTransform = new RotateTransform(0.0);
						}
						double num = MathUtils.Lerp(0.0, Conversions.ToDouble(Ani.Value), Ani.Ease.GetDelta(Ani.TimeFinished / (double)Ani.TimeTotal, Ani.TimePercent));
						((RotateTransform)frameworkElement.RenderTransform).Angle = ((RotateTransform)frameworkElement.RenderTransform).Angle + num;
						break;
					}
					}
					Ani.TimePercent = Ani.TimeFinished / (double)Ani.TimeTotal;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"执行动画失败：{Ani}", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModAnimation.vb");
					ProjectData.ClearProjectError();
				}
				return Ani;
			}
		}
	}
}
