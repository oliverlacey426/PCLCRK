// MeloongCore

namespace MeloongCore
{
	public class ProgressProvider
	{
		public enum ChildrenAction
		{
			None,
			Finished,
			Skiped
		}
		private (double actual, double skiped, double splited) progressParts = (actual: 0.0, skiped: 0.0, splited: 0.0);
		private readonly List<(double percentage, ProgressProvider child)> childrens = new List<(double, ProgressProvider)>();
		private (double actual, double skiped) observedProgress = (actual: 0.0, skiped: 0.0);
		private double incrementProgress;
		private double progressSum => progressParts.actual + progressParts.skiped + progressParts.splited;
		public bool IsFinished => GetRaw().actual + GetRaw().skiped > 0.9999999;
		public event Action<(double actual, double skiped)>? ProgressChanged;
		private event Action? ChildProgressChanged;
		private void InvokeProgressChanged()
		{
			ProgressChanged?.Invoke(GetRaw());
			ChildProgressChanged?.Invoke();
		}
		private bool _Set(double value, bool skiped)
		{
			value = value.Clamp(0.0, 1.0);
			double num = value - progressSum;
			if (num < 0.0)
			{
				progressParts = (actual: progressParts.actual * value / progressSum, skiped: progressParts.skiped * value / progressSum, splited: progressParts.splited * value / progressSum);
			}
			else if (skiped)
			{
				progressParts.skiped += num;
			}
			else
			{
				progressParts.actual += num;
			}
			return num != 0.0;
		}
		public void Set(double value, bool skiped = false, ChildrenAction action = ChildrenAction.Finished)
		{
			bool flag;
			lock (this)
			{
				flag = _Set(value, skiped);
				flag |= AssumeChildrenAs(action);
			}
			if (flag)
			{
				InvokeProgressChanged();
			}
		}
		public void Add(double value, bool skiped = false, ChildrenAction action = ChildrenAction.Finished)
		{
			bool flag;
			lock (this)
			{
				flag = _Set(value + progressSum, skiped);
				flag |= AssumeChildrenAs(action);
			}
			if (flag)
			{
				InvokeProgressChanged();
			}
		}
		public void Finish(ChildrenAction action = ChildrenAction.Finished)
		{
			Set(1.0, skiped: false, action);
		}
		public void Skip(ChildrenAction action = ChildrenAction.Skiped)
		{
			Set(1.0, skiped: true, action);
		}
		public void Reset()
		{
			bool flag;
			lock (this)
			{
				flag = progressSum > 0.0;
				progressParts = (actual: 0.0, skiped: 0.0, splited: 0.0);
				childrens.ForEach(((double percentage, ProgressProvider child) c) =>
				{
					c.child.ChildProgressChanged -= InvokeProgressChanged;
				});
				childrens.Clear();
			}
			if (flag)
			{
				InvokeProgressChanged();
			}
		}
		public ProgressProvider SplitTo(double value)
		{
			lock (this)
			{
				return SplitBy(value - progressSum).First();
			}
		}
		public List<ProgressProvider> SplitBy(params double[] percentages)
		{
			lock (this)
			{
				if (percentages.Any((double p) => p <= 0.0))
				{
					throw new ArgumentException("子项进度必须为正数且");
				}
				progressParts.splited += (percentages.Sum() + progressSum).Clamp(0.0, 1.0) - progressSum;
				return percentages.Select((double percentage) =>
				{
					ProgressProvider progressProvider = new ProgressProvider();
					progressProvider.ChildProgressChanged += InvokeProgressChanged;
					childrens.Add((percentage, progressProvider));
					return progressProvider;
				}).ToList();
			}
		}
		private bool AssumeChildrenAs(ChildrenAction action)
		{
			if (action == ChildrenAction.None)
			{
				return false;
			}
			lock (this)
			{
				bool changed = false;
				if (childrens.Any(((double percentage, ProgressProvider child) c) => c.percentage > 0.0))
				{
					double mult = progressParts.splited / childrens.Sum(((double percentage, ProgressProvider child) c) => c.percentage);
					childrens.ForEach(((double percentage, ProgressProvider child) c) =>
					{
						var (num, num2) = c.child.GetRaw();
						progressParts.actual += mult * c.percentage * ((action == ChildrenAction.Finished) ? (1.0 - num2) : num);
						progressParts.skiped += mult * c.percentage * ((action == ChildrenAction.Skiped) ? (1.0 - num) : num2);
						c.child.ChildProgressChanged -= InvokeProgressChanged;
						if (num + num2 < 0.9999999)
						{
							changed = true;
						}
					});
				}
				progressParts.splited = 0.0;
				childrens.Clear();
				return changed;
			}
		}
		public (double actual, double skiped) GetRaw()
		{
			lock (this)
			{
				(double actual, double skiped) current = (actual: progressParts.actual, skiped: progressParts.skiped);
				if (childrens.Any(((double percentage, ProgressProvider child) c) => c.percentage > 0.0))
				{
					double mult = progressParts.splited / childrens.Sum(((double percentage, ProgressProvider child) c) => c.percentage);
					childrens.ForEach(((double percentage, ProgressProvider child) c) =>
					{
						var (num, num2) = c.child.GetRaw();
						current.actual += mult * c.percentage * num;
						current.skiped += mult * c.percentage * num2;
					});
				}
				return current;
			}
		}
		public double GetIncreasing()
		{
			lock (this)
			{
				(double, double) raw = GetRaw();
				(double, double) tuple = raw;
				(double, double) tuple2 = observedProgress;
				if (tuple.Item1 == tuple2.Item1 && tuple.Item2 == tuple2.Item2)
				{
					return incrementProgress;
				}
				if (IsFinished)
				{
					incrementProgress = 1.0;
				}
				else if (raw.Item1 >= observedProgress.actual)
				{
					incrementProgress += (1.0 - incrementProgress) * (raw.Item1 - observedProgress.actual) / (1.0 - observedProgress.actual - raw.Item2);
				}
				observedProgress = raw;
				return incrementProgress;
			}
		}
	}
}
