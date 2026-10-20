// MeloongCore.Extensions

namespace MeloongCore.Extensions
{
	public static class TaskExtensions
	{
		public static void Run(this Task task)
		{
			task.GetAwaiter().GetResult();
		}
		public static T Run<T>(this Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}
		public static ConfiguredTaskAwaitable NoCapture(this Task task)
		{
			return task.ConfigureAwait(continueOnCapturedContext: false);
		}
		public static ConfiguredTaskAwaitable<T> NoCapture<T>(this Task<T> task)
		{
			return task.ConfigureAwait(continueOnCapturedContext: false);
		}
	}
}
