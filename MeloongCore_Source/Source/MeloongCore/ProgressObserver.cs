// MeloongCore

namespace MeloongCore
{
	public class ProgressObserver : RateLimitedWorker
	{
		private readonly ProgressProvider provider;
		public ProgressObserver(ProgressProvider provider, Action<double> updateAction, double minimalIntervalMs = 70.0)
			: base(() =>
			{
				Observe(provider, updateAction);
			}, minimalIntervalMs, RateLimitMode.ImmediateThenMerge)
		{
			this.provider = provider;
			provider.ProgressChanged += StartObserver;
		}
		public override void Dispose()
		{
			provider.ProgressChanged -= StartObserver;
			base.Dispose();
		}
		private void StartObserver((double actual, double skiped) raw)
		{
			Start();
		}
		private static void Observe(ProgressProvider provider, Action<double> updateAction)
		{
			double increasing = provider.GetIncreasing();
			updateAction(increasing);
		}
	}
}
