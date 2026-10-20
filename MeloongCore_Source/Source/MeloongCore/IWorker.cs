// MeloongCore

namespace MeloongCore
{
	public interface IWorker
	{
		bool Running { get; }
		bool HasSucceeded { get; }
		bool LastSucceeded { get; }
		ProgressProvider Progress { get; }
		event Action? Started;
		event Action? Stopped;
		event Action? Succeeded;
		event Action<Exception>? Failed;
		event Action? Canceled;
		void Start(CancellationToken cancellationToken = default(CancellationToken), ProgressProvider? progressOverride = null);
		void Cancel();
		bool WaitIfRunning(int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> WaitIfRunningAsync(int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken));
	}
}
