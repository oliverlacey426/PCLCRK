// PCL

namespace PCL
{
	public class DragHelper
	{
		public struct POINT
		{
			public int X;
			public int Y;
		}
		private struct CHANGEFILTERSTRUCT
		{
			public uint cbSize;
			public uint ExtStatus;
		}
		private string[] _DropFilePathsBackingField;
		private POINT _DropPointBackingField;
		private const uint WM_COPYGLOBALDATA = 73u;
		private const uint WM_COPYDATA = 74u;
		private const uint WM_DROPFILES = 563u;
		private const uint MSGFLT_ALLOW = 1u;
		private const uint MSGFLT_ADD = 1u;
		private const int MAX_PATH = 260;
		public string[] DropFilePaths
		{
			get
			{
				return _DropFilePathsBackingField;
			}
			private set
			{
				_DropFilePathsBackingField = value;
			}
		}
		public POINT DropPoint
		{
			get
			{
				return _DropPointBackingField;
			}
			private set
			{
				_DropPointBackingField = value;
			}
		}
		public HwndSource HwndIntPtrSource { get; set; }
		public event EventHandler DragDrop;
		public void AddHook()
		{
			RemoveDragHook();
			HwndIntPtrSource.AddHook(WndProc);
			IntPtr handle = HwndIntPtrSource.Handle;
			if (IsUserAnAdmin())
			{
				RevokeDragDrop(handle);
			}
			DragAcceptFiles(handle, fAccept: true);
			ChangeMessageFilter(handle);
		}
		public void RemoveDragHook()
		{
			HwndIntPtrSource.RemoveHook(WndProc);
			DragAcceptFiles(HwndIntPtrSource.Handle, fAccept: false);
		}
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			string[] dropFilePaths = null;
			POINT dropPoint = default;
			if (TryGetDropInfo(msg, wParam, ref dropFilePaths, ref dropPoint))
			{
				DropPoint = dropPoint;
				DropFilePaths = dropFilePaths;
				DragDrop?.Invoke(this, EventArgs.Empty);
				handled = true;
			}
			return IntPtr.Zero;
		}
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, uint msg, uint action, ref CHANGEFILTERSTRUCT pChangeFilterStruct);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool ChangeWindowMessageFilter(uint msg, uint flags);
		[DllImport("shell32.dll")]
		private static extern void DragAcceptFiles(IntPtr hWnd, bool fAccept);
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		private static extern uint DragQueryFile(IntPtr hWnd, uint iFile, StringBuilder lpszFile, int cch);
		[DllImport("shell32.dll")]
		private static extern bool DragQueryPoint(IntPtr hDrop, ref POINT lppt);
		[DllImport("shell32.dll")]
		private static extern void DragFinish(IntPtr hDrop);
		[DllImport("ole32.dll")]
		private static extern int RevokeDragDrop(IntPtr hWnd);
		[DllImport("shell32.dll")]
		private static extern bool IsUserAnAdmin();
		private static void ChangeMessageFilter(IntPtr handle)
		{
			Version version = Environment.OSVersion.Version;
			bool flag = version >= new Version(6, 0);
			bool flag2 = version >= new Version(6, 1);
			if (!flag)
			{
				return;
			}
			CHANGEFILTERSTRUCT pChangeFilterStruct = new CHANGEFILTERSTRUCT
			{
				cbSize = 8u
			};
			uint[] array = new uint[3] { 563u, 73u, 74u };
			foreach (uint msg in array)
			{
				bool flag3 = false;
				if ((!flag2) ? (!ChangeWindowMessageFilter(msg, 1u)) : (!ChangeWindowMessageFilterEx(handle, msg, 1u, ref pChangeFilterStruct)))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
		}
		private static bool TryGetDropInfo(int msg, IntPtr wParam, ref string[] dropFilePaths, ref POINT dropPoint)
		{
			dropFilePaths = null;
			dropPoint = default;
			if ((long)msg != 563)
			{
				return false;
			}
			uint num = DragQueryFile(wParam, uint.MaxValue, null, 0);
			checked
			{
				dropFilePaths = new string[(int)num - 1 + 1];
				uint num2 = (uint)((int)num - 1);
				for (uint num3 = 0u; num3 <= num2; num3++)
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					if (unchecked((long)DragQueryFile(wParam, num3, stringBuilder, stringBuilder.Capacity)) > 0L)
					{
						dropFilePaths[(int)num3] = stringBuilder.ToString();
					}
				}
				DragQueryPoint(wParam, ref dropPoint);
				DragFinish(wParam);
				return true;
			}
		}
	}
}
