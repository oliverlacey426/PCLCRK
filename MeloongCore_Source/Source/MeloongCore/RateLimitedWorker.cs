// MeloongCore

namespace MeloongCore
{
	public class RateLimitedWorker(Action workload, double minimalIntervalMs, RateLimitMode mode) : IDisposable
	{
		private readonly RateLimitedAction action = mode switch
		{
			RateLimitMode.ImmediateThenMerge => Throttler.Throttle(workload, TimeSpan.FromMilliseconds(minimalIntervalMs)), 
			RateLimitMode.WaitUnillNoNewInvoke => Debouncer.Debounce(workload, TimeSpan.FromMilliseconds(minimalIntervalMs)), 
			_ => Debouncer.Debounce(workload, TimeSpan.FromMilliseconds(minimalIntervalMs), leading: true, trailing: false), 
		};
		private bool disposed;
		public void Start()
		{
			if (!disposed)
			{
				action.Invoke();
			}
		}
		public virtual void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				action.Dispose();
			}
		}
	}
}
