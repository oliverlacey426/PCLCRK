// MeloongCore

namespace MeloongCore
{
	public static class EnumUtils
	{
		private static readonly ConcurrentDictionary<Type, List<Enum>> allFlagsCache = new ConcurrentDictionary<Type, List<Enum>>();
		private static IEnumerable<T> GetAllValues<T>() where T : struct, Enum
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}
		public static T FromString<T>(string? value) where T : struct, Enum
		{
			if (string.IsNullOrEmpty(value))
			{
				return (T)Enum.ToObject(typeof(T), 0);
			}
			if (double.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out var result))
			{
				return (T)Enum.ToObject(typeof(T), Convert.ToInt64(result));
			}
			return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
		}
		public static bool HasFlagF<T>(this T value, T flag) where T : struct, Enum
		{
			if (value.HasFlag(flag))
			{
				return !EqualityComparer<T>.Default.Equals(flag, default);
			}
			return false;
		}
		public static List<T> GetAllFlags<T>() where T : struct, Enum
		{
			if (allFlagsCache.TryGetValue(typeof(T), out List<Enum> value))
			{
				return value.Cast<T>().ToList();
			}
			if (!typeof(T).IsDefined(typeof(FlagsAttribute), inherit: false))
			{
				throw new ArgumentException("枚举 " + typeof(T).FullName + " 没有定义 FlagsAttribute。");
			}
			List<T> list = new List<T>();
			foreach (T item in GetAllValues<T>().Distinct())
			{
				long num = Convert.ToInt64(item, CultureInfo.InvariantCulture);
				if (num != 0L && (num & (num - 1)) == 0L)
				{
					list.Add(item);
				}
			}
			allFlagsCache[typeof(T)] = list.Cast<Enum>().ToList();
			return list;
		}
	}
}
