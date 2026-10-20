// MeloongCore.Extensions

namespace MeloongCore.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Distinct<T>(this IList<T> list, Func<T, T, bool> comparer)
		{
			List<T> seen = new List<T>();
			foreach (T item in list)
			{
				if (!seen.Any((T s) => comparer(s, item)))
				{
					seen.Add(item);
					yield return item;
				}
			}
		}
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			HashSet<TKey> seen = new HashSet<TKey>();
			foreach (TSource item in source)
			{
				if (seen.Add(selector(item)))
				{
					yield return item;
				}
			}
		}
		public static ParallelQuery<TSource> DistinctBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> selector)
		{
			ConcurrentDictionary<TKey, bool> seen = new ConcurrentDictionary<TKey, bool>();
			return source.Where((TSource element) => seen.TryAdd(selector(element), value: true));
		}
		public static List<TSource> MaxByAll<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey>
		{
			List<TSource> list = new List<TSource>();
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return list;
			}
			TSource current = enumerator.Current;
			TKey other = selector(current);
			list.Add(current);
			while (enumerator.MoveNext())
			{
				TSource current2 = enumerator.Current;
				TKey val = selector(current2);
				int num = val.CompareTo(other);
				if (num > 0)
				{
					other = val;
					list.Clear();
					list.Add(current2);
				}
				else if (num == 0)
				{
					list.Add(current2);
				}
			}
			return list;
		}
		public static List<TSource> MaxByAll<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			List<TSource> list = new List<TSource>();
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return list;
			}
			TSource current = enumerator.Current;
			TKey y = selector(current);
			list.Add(current);
			while (enumerator.MoveNext())
			{
				TSource current2 = enumerator.Current;
				TKey val = selector(current2);
				int num = comparer.Compare(val, y);
				if (num > 0)
				{
					y = val;
					list.Clear();
					list.Add(current2);
				}
				else if (num == 0)
				{
					list.Add(current2);
				}
			}
			return list;
		}
		public static List<TSource> MinByAll<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey>
		{
			List<TSource> list = new List<TSource>();
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return list;
			}
			TSource current = enumerator.Current;
			TKey other = selector(current);
			list.Add(current);
			while (enumerator.MoveNext())
			{
				TSource current2 = enumerator.Current;
				TKey val = selector(current2);
				int num = val.CompareTo(other);
				if (num < 0)
				{
					other = val;
					list.Clear();
					list.Add(current2);
				}
				else if (num == 0)
				{
					list.Add(current2);
				}
			}
			return list;
		}
		public static List<TSource> MinByAll<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			List<TSource> list = new List<TSource>();
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return list;
			}
			TSource current = enumerator.Current;
			TKey y = selector(current);
			list.Add(current);
			while (enumerator.MoveNext())
			{
				TSource current2 = enumerator.Current;
				TKey val = selector(current2);
				int num = comparer.Compare(val, y);
				if (num < 0)
				{
					y = val;
					list.Clear();
					list.Add(current2);
				}
				else if (num == 0)
				{
					list.Add(current2);
				}
			}
			return list;
		}
		public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey>
		{
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return default;
			}
			TSource current = enumerator.Current;
			TKey other = selector(current);
			while (enumerator.MoveNext())
			{
				TKey val = selector(enumerator.Current);
				if (val.CompareTo(other) > 0)
				{
					current = enumerator.Current;
					other = val;
				}
			}
			return current;
		}
		public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return default;
			}
			TSource current = enumerator.Current;
			TKey y = selector(current);
			while (enumerator.MoveNext())
			{
				TKey val = selector(enumerator.Current);
				if (comparer.Compare(val, y) > 0)
				{
					current = enumerator.Current;
					y = val;
				}
			}
			return current;
		}
		public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey>
		{
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return default;
			}
			TSource current = enumerator.Current;
			TKey other = selector(current);
			while (enumerator.MoveNext())
			{
				TKey val = selector(enumerator.Current);
				if (val.CompareTo(other) < 0)
				{
					current = enumerator.Current;
					other = val;
				}
			}
			return current;
		}
		public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			using IEnumerator<TSource> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return default;
			}
			TSource current = enumerator.Current;
			TKey y = selector(current);
			while (enumerator.MoveNext())
			{
				TKey val = selector(enumerator.Current);
				if (comparer.Compare(val, y) < 0)
				{
					current = enumerator.Current;
					y = val;
				}
			}
			return current;
		}
		public static IEnumerable<T> Ordered<T>(this IEnumerable<T> source) where T : IComparable<T>
		{
			return source.OrderBy((T x) => x);
		}
		public static IEnumerable<T> OrderedDescending<T>(this IEnumerable<T> source) where T : IComparable<T>
		{
			return source.OrderByDescending((T x) => x);
		}
		public static string Join(this IEnumerable list, char split)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object item in list)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(split);
				}
				if (item != null)
				{
					stringBuilder.Append(item.ToString());
				}
			}
			return stringBuilder.ToString();
		}
		public static string Join(this IEnumerable list, string split)
		{
			if (split.IsSingle())
			{
				return list.Join(split[0]);
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object item in list)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(split);
				}
				if (item != null)
				{
					stringBuilder.Append(item);
				}
			}
			return stringBuilder.ToString();
		}
		public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key)
		{
			TValue value;
			return dict.TryRemove(key, out value);
		}
		public static Dictionary<T, T> ToDictionary<T>(this T[,] arr) where T : notnull
		{
			Dictionary<T, T> dictionary = new Dictionary<T, T>();
			if (arr.Length == 0)
			{
				return dictionary;
			}
			if (arr.GetLength(1) != 2)
			{
				throw new ArgumentException("数组必须为两列，第一列为 Key，第二列为 Value。");
			}
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				dictionary[arr[i, 0]] = arr[i, 1];
			}
			return dictionary;
		}
		public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue? defaultValue = default(TValue?))
		{
			if (!dict.TryGetValue(key, out TValue value))
			{
				return defaultValue;
			}
			return value;
		}
		public static void AddIntoValueCollection<TKey, TValue>(this IDictionary<TKey, List<TValue>> dict, TKey key, TValue value)
		{
			if (dict.TryGetValue(key, out List<TValue> value2))
			{
				value2.Add(value);
				return;
			}
			dict.Add(key, new List<TValue>(1) { value });
		}
		public static IEnumerable<T> Flags<T>(this T value) where T : struct, Enum
		{
			return from element in EnumUtils.GetAllFlags<T>()
				where value.HasFlag(element)
				select element;
		}
		public static void RemoveIf<T>(this ICollection<T> source, Func<T, bool> predicate)
		{
			foreach (T item in source.Where(predicate).ToList())
			{
				source.Remove(item);
			}
		}
		public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> itemsToRemove)
		{
			foreach (T item in itemsToRemove)
			{
				source.Remove(item);
			}
		}
		public static void KeepIf<T>(this ICollection<T> source, Func<T, bool> predicate)
		{
			foreach (T item in source.Where((T item) => !predicate(item)).ToList())
			{
				source.Remove(item);
			}
		}
		public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T element)
		{
			return source.Except(new _z__ReadOnlySingleElementList<T>(element));
		}
		public static bool IsSingle<T>(this IEnumerable<T>? source)
		{
			if (source == null)
			{
				return false;
			}
			if (source is IList<T> list)
			{
				return list.Count == 1;
			}
			using IEnumerator<T> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			if (enumerator.MoveNext())
			{
				return false;
			}
			return true;
		}
		public static IEnumerable<T> ForAll<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
			{
				action(item);
			}
			return source;
		}
	}
}
