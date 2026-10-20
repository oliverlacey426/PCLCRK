// MeloongCore

namespace MeloongCore
{
	public class PreviewGetEventArgs<T>(T? value) : EventArgs
	{
		public T? Value { get; set; } = value;
	}
}
