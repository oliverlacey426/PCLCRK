// PCL

namespace PCL
{
	public class MyResizer
	{
		private delegate void RefreshDelegate();
		private struct POINT
		{
			public int x;
			public int y;
			public POINT(int x, int y)
			{
				this = default;
				this.x = x;
				this.y = y;
			}
		}
		private struct MINMAXINFO
		{
			public POINT ptReserved;
			public POINT ptMaxSize;
			public POINT ptMaxPosition;
			public POINT ptMinTrackSize;
			public POINT ptMaxTrackSize;
		}
		private struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
			public static RECT Empty = default;
			public int Width => Math.Abs(checked(right - left));
			public int Height => checked(bottom - top);
			public RECT(int left, int top, int right, int bottom)
			{
				this = default;
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
			public RECT(RECT rcSrc)
			{
				this = default;
				left = rcSrc.left;
				top = rcSrc.top;
				right = rcSrc.right;
				bottom = rcSrc.bottom;
			}
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private class MONITORINFO
		{
			public int cbSize;
			public RECT rcMonitor;
			public RECT rcWork;
			public int dwFlags;
			public MONITORINFO()
			{
				cbSize = Marshal.SizeOf(typeof(MONITORINFO));
				rcMonitor = default;
				rcWork = default;
				dwFlags = 0;
			}
		}
		private struct PointAPI
		{
			public int X;
			public int Y;
		}
		private readonly Window target;
		private bool resizeRight;
		private bool resizeLeft;
		private bool resizeUp;
		private bool resizeDown;
		private readonly Dictionary<UIElement, short> leftElements;
		private readonly Dictionary<UIElement, short> rightElements;
		private readonly Dictionary<UIElement, short> upElements;
		private readonly Dictionary<UIElement, short> downElements;
		private PointAPI startMousePoint;
		private System.Windows.Size startWindowSize;
		private POINT startWindowLeftUpPoint;
		private static int workAreaMaxHeight = -1;
		private HwndSource hs;
		public MyResizer(Window target)
		{
			this.target = null;
			resizeRight = false;
			resizeLeft = false;
			resizeUp = false;
			resizeDown = false;
			leftElements = new Dictionary<UIElement, short>();
			rightElements = new Dictionary<UIElement, short>();
			upElements = new Dictionary<UIElement, short>();
			downElements = new Dictionary<UIElement, short>();
			startMousePoint = default;
			startWindowSize = default;
			startWindowLeftUpPoint = default;
			this.target = target;
			if (Information.IsNothing(target))
			{
				throw new Exception("Invalid Window handle");
			}
			target.SourceInitialized += MyMacClass_SourceInitialized;
		}
		private void MyMacClass_SourceInitialized(object sender, EventArgs e)
		{
			hs = PresentationSource.FromVisual((Visual)sender) as HwndSource;
			hs.AddHook(WndProc);
		}
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 36)
			{
				WmGetMinMaxInfo(hwnd, lParam);
				handled = true;
			}
			return (IntPtr)0;
		}
		private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
		{
			object obj = Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
			MINMAXINFO structure = ((obj != null) ? ((MINMAXINFO)obj) : default(MINMAXINFO));
			int flags = 2;
			IntPtr intPtr = MonitorFromWindow(hwnd, flags);
			checked
			{
				if (intPtr != IntPtr.Zero)
				{
					MONITORINFO mONITORINFO = new MONITORINFO();
					GetMonitorInfo(intPtr, mONITORINFO);
					RECT rcWork = mONITORINFO.rcWork;
					RECT rcMonitor = mONITORINFO.rcMonitor;
					structure.ptMaxPosition.x = Math.Abs(rcWork.left - rcMonitor.left);
					structure.ptMaxPosition.y = Math.Abs(rcWork.top - rcMonitor.top);
					structure.ptMaxSize.y = Math.Abs(rcWork.bottom - rcWork.top);
					workAreaMaxHeight = structure.ptMaxSize.y;
					if (rcWork.Height == rcMonitor.Height)
					{
						structure.ptMaxSize.y -= 2;
					}
				}
				Marshal.StructureToPtr(structure, lParam, fDeleteOld: true);
			}
		}
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
		[DllImport("User32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
		private void connectMouseHandlers(UIElement element)
		{
			element.MouseLeftButtonDown += element_MouseLeftButtonDown;
		}
		public void addResizerRight(UIElement element)
		{
			connectMouseHandlers(element);
			rightElements.Add(element, 0);
		}
		public void addResizerLeft(UIElement element)
		{
			connectMouseHandlers(element);
			leftElements.Add(element, 0);
		}
		public void addResizerUp(UIElement element)
		{
			connectMouseHandlers(element);
			upElements.Add(element, 0);
		}
		public void addResizerDown(UIElement element)
		{
			connectMouseHandlers(element);
			downElements.Add(element, 0);
		}
		public void addResizerRightDown(UIElement element)
		{
			connectMouseHandlers(element);
			rightElements.Add(element, 0);
			downElements.Add(element, 0);
		}
		public void addResizerLeftDown(UIElement element)
		{
			connectMouseHandlers(element);
			leftElements.Add(element, 0);
			downElements.Add(element, 0);
		}
		public void addResizerRightUp(UIElement element)
		{
			connectMouseHandlers(element);
			rightElements.Add(element, 0);
			upElements.Add(element, 0);
		}
		public void addResizerLeftUp(UIElement element)
		{
			connectMouseHandlers(element);
			leftElements.Add(element, 0);
			upElements.Add(element, 0);
		}
		private void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			GetCursorPos(out startMousePoint);
			checked
			{
				startMousePoint.X = (int)Math.Round(ModBase.GetWPFSize(startMousePoint.X));
				startMousePoint.Y = (int)Math.Round(ModBase.GetWPFSize(startMousePoint.Y));
				startWindowSize = new System.Windows.Size(target.Width, target.Height);
				startWindowLeftUpPoint = new POINT((int)Math.Round(target.Left), (int)Math.Round(target.Top));
				UIElement key = (UIElement)sender;
				if (leftElements.ContainsKey(key))
				{
					resizeLeft = true;
				}
				if (rightElements.ContainsKey(key))
				{
					resizeRight = true;
				}
				if (upElements.ContainsKey(key))
				{
					resizeUp = true;
				}
				if (downElements.ContainsKey(key))
				{
					resizeDown = true;
				}
				ModBase.RunInNewThread(updateSizeLoop, "窗口大小调整检测");
			}
		}
		private void updateSizeLoop()
		{
			try
			{
				while (resizeDown || resizeLeft || resizeRight || resizeUp)
				{
					target.Dispatcher.Invoke(updateSize, DispatcherPriority.Render);
					target.Dispatcher.Invoke(updateMouseDown, DispatcherPriority.Render);
					Thread.Sleep(0);
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
		private void updateSize()
		{
			PointAPI lpPoint = default;
			GetCursorPos(out lpPoint);
			checked
			{
				lpPoint.X = (int)Math.Round(ModBase.GetWPFSize(lpPoint.X));
				lpPoint.Y = (int)Math.Round(ModBase.GetWPFSize(lpPoint.Y));
				try
				{
					double num = -1.0;
					double num2 = -1.0;
					double num3 = -10000.0;
					double num4 = -10000.0;
					if (resizeRight)
					{
						if (target.Width != target.MinWidth)
						{
							num = ((!(startWindowSize.Width - (double)(startMousePoint.X - lpPoint.X) >= target.MinWidth)) ? target.MinWidth : (startWindowSize.Width - (double)(startMousePoint.X - lpPoint.X)));
						}
						else if (startMousePoint.X < lpPoint.X)
						{
							num = startWindowSize.Width - (double)(startMousePoint.X - lpPoint.X);
						}
					}
					if (resizeDown)
					{
						if (target.Height != target.MinHeight)
						{
							if (startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y) >= target.MinHeight)
							{
								if (workAreaMaxHeight <= 0)
								{
									num2 = startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y);
								}
								else
								{
									num2 = ((startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y) + target.Top <= (double)workAreaMaxHeight) ? (startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y)) : ((double)workAreaMaxHeight - target.Top));
								}
							}
							else
							{
								num2 = target.MinHeight;
							}
						}
						else if (startMousePoint.Y < lpPoint.Y)
						{
							if (workAreaMaxHeight <= 0)
							{
								num2 = startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y);
							}
							else
							{
								num2 = ((startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y) + target.Top <= (double)workAreaMaxHeight) ? (startWindowSize.Height - (double)(startMousePoint.Y - lpPoint.Y)) : ((double)workAreaMaxHeight - target.Top));
							}
						}
					}
					if (resizeLeft)
					{
						if (target.Width == target.MinWidth)
						{
							if (startMousePoint.X > lpPoint.X)
							{
								num = startWindowSize.Width + (double)startMousePoint.X - (double)lpPoint.X;
								num3 = startWindowLeftUpPoint.x - (startMousePoint.X - lpPoint.X);
							}
							else
							{
								num = target.MinWidth;
								num3 = (double)startWindowLeftUpPoint.x + startWindowSize.Width - target.Width;
							}
						}
						else if (startWindowSize.Width + (double)(startMousePoint.X - lpPoint.X) >= target.MinWidth)
						{
							num = startWindowSize.Width + (double)(startMousePoint.X - lpPoint.X);
							num3 = startWindowLeftUpPoint.x - (startMousePoint.X - lpPoint.X);
						}
						else
						{
							num = target.MinWidth;
							num3 = (double)startWindowLeftUpPoint.x + startWindowSize.Width - target.Width;
						}
					}
					if (resizeUp)
					{
						if (target.Height == target.MinHeight)
						{
							if (startMousePoint.Y > lpPoint.Y)
							{
								num2 = startWindowSize.Height + (double)startMousePoint.Y - (double)lpPoint.Y;
								num4 = startWindowLeftUpPoint.y - (startMousePoint.Y - lpPoint.Y);
							}
							else
							{
								num2 = target.MinHeight;
								num4 = (double)startWindowLeftUpPoint.y + startWindowSize.Height - target.Height;
							}
						}
						else if (startWindowSize.Height + (double)(startMousePoint.Y - lpPoint.Y) >= target.MinHeight)
						{
							num2 = startWindowSize.Height + (double)startMousePoint.Y - (double)lpPoint.Y;
							num4 = startWindowLeftUpPoint.y - (startMousePoint.Y - lpPoint.Y);
						}
						else
						{
							num2 = target.MinHeight;
							num4 = (double)startWindowLeftUpPoint.y + startWindowSize.Height - target.Height;
						}
					}
					if (num > 10.0 && Math.Abs(num - target.Width) > 0.7)
					{
						target.Width = num;
					}
					if (num2 > 10.0 && Math.Abs(num2 - target.Height) > 0.7)
					{
						target.Height = num2;
					}
					if (num3 > -9999.0 && Math.Abs(num3 - target.Left) > 0.7)
					{
						target.Left = num3;
					}
					if (num4 > -9999.0 && Math.Abs(num4 - target.Top) > 0.7)
					{
						target.Top = num4;
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
			}
		}
		private void updateMouseDown()
		{
			if ((GetAsyncKeyState(1) & 0x8000) == 0)
			{
				resizeRight = false;
				resizeLeft = false;
				resizeUp = false;
				resizeDown = false;
			}
		}
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool GetCursorPos(out PointAPI lpPoint);
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern short GetAsyncKeyState(int vKey);
	}
}
