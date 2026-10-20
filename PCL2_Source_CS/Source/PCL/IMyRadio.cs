// PCL

namespace PCL
{
	public interface IMyRadio
	{
		public delegate void CheckEventHandler(object sender, ModBase.RouteEventArgs e);
		public delegate void ChangedEventHandler(object sender, ModBase.RouteEventArgs e);
		event CheckEventHandler Check;
		event ChangedEventHandler Changed;
	}
}
