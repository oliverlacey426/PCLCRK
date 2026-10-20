// PCL.My

namespace PCL.My
{
	[StandardModule]
	[HideModuleName]
	internal sealed class MyWpfExtension
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Windows.Window", "Create__Instance__", "Dispose__Instance__", "My.MyWpfExtenstionModule.Windows")]
		internal sealed class MyWindows
		{
			[ThreadStatic]
			private static Hashtable s_WindowBeingCreated;
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormMain m_FormMain;
			public FormMain FormMain
			{
				get
				{
					m_FormMain = Create__Instance__(m_FormMain);
					return m_FormMain;
				}
				set
				{
					if (value != m_FormMain)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						Dispose__Instance__(ref m_FormMain);
					}
				}
			}
			[DebuggerHidden]
			private static T Create__Instance__<T>(T Instance) where T : Window, new()
			{
				if (Instance == null)
				{
					if (s_WindowBeingCreated != null)
					{
						if (s_WindowBeingCreated.ContainsKey(typeof(T)))
						{
							throw new InvalidOperationException("The window cannot be accessed via My.Windows from the Window constructor.");
						}
					}
					else
					{
						s_WindowBeingCreated = new Hashtable();
					}
					s_WindowBeingCreated.Add(typeof(T), null);
					return new T();
				}
				return Instance;
			}
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance) where T : Window
			{
				instance = null;
			}
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyWindows()
			{
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			internal new Type GetType()
			{
				return typeof(MyWindows);
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}
		}
		private static MyProject.ThreadSafeObjectProvider<Computer> s_Computer = new MyProject.ThreadSafeObjectProvider<Computer>();
		private static MyProject.ThreadSafeObjectProvider<User> s_User = new MyProject.ThreadSafeObjectProvider<User>();
		private static MyProject.ThreadSafeObjectProvider<MyWindows> s_Windows = new MyProject.ThreadSafeObjectProvider<MyWindows>();
		private static MyProject.ThreadSafeObjectProvider<Log> s_Log = new MyProject.ThreadSafeObjectProvider<Log>();
		internal static Application Application => (Application)System.Windows.Application.Current;
		internal static Computer Computer => s_Computer.GetInstance;
		internal static User User => s_User.GetInstance;
		internal static Log Log => s_Log.GetInstance;
		internal static MyWindows Windows
		{
			[DebuggerHidden]
			get
			{
				return s_Windows.GetInstance;
			}
		}
	}
}
