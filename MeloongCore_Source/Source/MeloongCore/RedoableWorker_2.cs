// MeloongCore

namespace MeloongCore
{
	public class RedoableWorker : RedoableWorkerBase<NoType?>
	{
		public RedoableWorker(Action<CancellationToken?, ProgressProvider?> workload, [CallerMemberName] string creatorMemberName = "")
			: base((Func<CancellationToken?, ProgressProvider?, NoType>)((CancellationToken? c, ProgressProvider? p) =>
			{
				workload(c, p);
				return (NoType?)null;
			}), (ProgressProvider?)null, creatorMemberName)
		{
		}
		public RedoableWorker(Action<CancellationToken?> workload, [CallerMemberName] string creatorMemberName = "")
			: base((Func<CancellationToken?, ProgressProvider?, NoType>)((CancellationToken? c, ProgressProvider? _) =>
			{
				workload(c);
				return (NoType?)null;
			}), (ProgressProvider?)null, creatorMemberName)
		{
		}
		public RedoableWorker(Action workload, [CallerMemberName] string creatorMemberName = "")
			: base((Func<CancellationToken?, ProgressProvider?, NoType>)((CancellationToken? _, ProgressProvider? _) =>
			{
				workload();
				return (NoType?)null;
			}), (ProgressProvider?)null, creatorMemberName)
		{
		}
	}
}
