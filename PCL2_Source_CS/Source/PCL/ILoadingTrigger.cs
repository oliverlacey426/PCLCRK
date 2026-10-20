// PCL

namespace PCL
{
	public interface ILoadingTrigger
	{
		public delegate void LoadingStateChangedEventHandler(MyLoading.MyLoadingState NewState, MyLoading.MyLoadingState OldState);
		public delegate void ProgressChangedEventHandler(double NewProgress, double OldProgress);
		bool IsLoader { get; }
		MyLoading.MyLoadingState LoadingState { get; set; }
		event LoadingStateChangedEventHandler LoadingStateChanged;
		event ProgressChangedEventHandler ProgressChanged;
	}
}
