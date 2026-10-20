// MeloongCore

namespace MeloongCore
{
	public class RedoableWorker<TOut> : RedoableWorkerBase<TOut>
	{
		public RedoableWorker(Func<CancellationToken?, ProgressProvider?, TOut> workload, [CallerMemberName] string creatorMemberName = "")
			: base(workload, (ProgressProvider?)null, creatorMemberName)
		{
		}
		public RedoableWorker(Func<CancellationToken?, TOut> workload, [CallerMemberName] string creatorMemberName = "")
			: base((Func<CancellationToken?, ProgressProvider?, TOut>)((CancellationToken? c, ProgressProvider? _) => workload(c)), (ProgressProvider?)null, creatorMemberName)
		{
		}
		public RedoableWorker(Func<TOut> workload, [CallerMemberName] string creatorMemberName = "")
			: base((Func<CancellationToken?, ProgressProvider?, TOut>)((CancellationToken? _, ProgressProvider? _) => workload()), (ProgressProvider?)null, creatorMemberName)
		{
		}
	}
}
