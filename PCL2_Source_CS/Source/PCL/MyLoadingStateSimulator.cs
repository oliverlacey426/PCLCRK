// PCL

namespace PCL
{
	public class MyLoadingStateSimulator : ILoadingTrigger
	{
		private MyLoading.MyLoadingState _LoadingState { get; set; }
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
		public bool IsLoader { get; }
		public event ILoadingTrigger.LoadingStateChangedEventHandler LoadingStateChanged;
		public event ILoadingTrigger.ProgressChangedEventHandler ProgressChanged;
		public MyLoadingStateSimulator()
		{
			_LoadingState = MyLoading.MyLoadingState.Unloaded;
			IsLoader = false;
		}
	}
}
