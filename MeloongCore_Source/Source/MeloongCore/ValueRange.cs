// MeloongCore

namespace MeloongCore
{
	public sealed class ValueRange<T> : IEquatable<ValueRange<T>?> where T : IComparable<T>
	{
		private bool _hasLower_P;
		private bool _hasUpper_P;
		private bool _isLowerInclusive_P;
		private bool _isUpperInclusive_P;
		private T? _lower_P;
		private T? _upper_P;
		public bool IsLowerInclusive;
		public bool IsUpperInclusive;
		public T? Lower { get; private set; }
		public T? Upper { get; private set; }
		public bool HasLower { get; private set; }
		public bool HasUpper { get; private set; }
		public ValueRange(T? lower, T? upper, bool isLowerInclusive, bool isUpperInclusive, bool hasLower, bool hasUpper)
		{
			_lower_P = lower;
			_upper_P = upper;
			_isLowerInclusive_P = isLowerInclusive;
			_isUpperInclusive_P = isUpperInclusive;
			_hasLower_P = hasLower;
			_hasUpper_P = hasUpper;
			if (_hasLower_P && _lower_P == null)
			{
				throw new ArgumentNullException("lower");
			}
			Lower = _lower_P;
			if (_hasUpper_P && _upper_P == null)
			{
				throw new ArgumentNullException("upper");
			}
			Upper = _upper_P;
			HasLower = _hasLower_P;
			HasUpper = _hasUpper_P;
			IsLowerInclusive = _isLowerInclusive_P;
			IsUpperInclusive = _isUpperInclusive_P;
		}
		public void ClearLower()
		{
			Lower = default;
			HasLower = false;
		}
		public void ClearUpper()
		{
			Upper = default;
			HasUpper = false;
		}
		public void SetLower(T lower, bool isInclusive)
		{
			Lower = lower;
			HasLower = true;
			IsLowerInclusive = isInclusive;
		}
		public void SetUpper(T upper, bool isInclusive)
		{
			Upper = upper;
			HasUpper = true;
			IsUpperInclusive = isInclusive;
		}
		public static ValueRange<T> Closed(T lower, T upper)
		{
			return new ValueRange<T>(lower, upper, isLowerInclusive: true, isUpperInclusive: true, hasLower: true, hasUpper: true);
		}
		public static ValueRange<T> ClosedOpen(T lower, T upper)
		{
			return new ValueRange<T>(lower, upper, isLowerInclusive: true, isUpperInclusive: false, hasLower: true, hasUpper: true);
		}
		public static ValueRange<T> OpenClosed(T lower, T upper)
		{
			return new ValueRange<T>(lower, upper, isLowerInclusive: false, isUpperInclusive: true, hasLower: true, hasUpper: true);
		}
		public static ValueRange<T> Open(T lower, T upper)
		{
			return new ValueRange<T>(lower, upper, isLowerInclusive: false, isUpperInclusive: false, hasLower: true, hasUpper: true);
		}
		public static ValueRange<T> Exactly(T value)
		{
			return new ValueRange<T>(value, value, isLowerInclusive: true, isUpperInclusive: true, hasLower: true, hasUpper: true);
		}
		public static ValueRange<T> AtLeast(T lower)
		{
			return new ValueRange<T>(lower, default, isLowerInclusive: true, isUpperInclusive: true, hasLower: true, hasUpper: false);
		}
		public static ValueRange<T> GreaterThan(T lower)
		{
			return new ValueRange<T>(lower, default, isLowerInclusive: false, isUpperInclusive: true, hasLower: true, hasUpper: false);
		}
		public static ValueRange<T> AtMost(T upper)
		{
			return new ValueRange<T>(default, upper, isLowerInclusive: true, isUpperInclusive: true, hasLower: false, hasUpper: true);
		}
		public static ValueRange<T> LessThan(T upper)
		{
			return new ValueRange<T>(default, upper, isLowerInclusive: true, isUpperInclusive: false, hasLower: false, hasUpper: true);
		}
		public static ValueRange<T> All()
		{
			return new ValueRange<T>(default, default, isLowerInclusive: true, isUpperInclusive: true, hasLower: false, hasUpper: false);
		}
		public static ValueRange<T> FromString(string str, Func<string, T>? parseFunc = null)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("不能为空");
			}
			bool flag = str[0] == '[';
			bool flag2 = str[str.Length - 1] == ']';
			if (!flag && str[0] != '(')
			{
				throw new ArgumentException("开头必须是 [ 或 (");
			}
			if (!flag2)
			{
				if (str[str.Length - 1] != ')')
				{
					throw new ArgumentException("结尾必须是 ] 或 )");
				}
			}
			int num = str.IndexOfAny(new char[2] { ',', '，' });
			if (num == -1)
			{
				throw new ArgumentException("需要用逗号分隔两个值");
			}
			string text = str.Substring(1, num - 1).Trim();
			int num2 = num + 1;
			string text2 = str.Substring(num2, str.Length - 1 - num2).Trim();
			T val = default;
			T val2 = default;
			bool flag3 = false;
			bool flag4 = false;
			if (((text == null || text.Length != 0) && !(text == "-∞")) || 1 == 0)
			{
				val = ((parseFunc != null) ? parseFunc(text) : ((T)Convert.ChangeType(text, typeof(T))));
				flag3 = true;
			}
			if (((text2 == null || text2.Length != 0) && !(text2 == "+∞") && !(text2 == "∞")) || 1 == 0)
			{
				val2 = ((parseFunc != null) ? parseFunc(text2) : ((T)Convert.ChangeType(text2, typeof(T))));
				flag4 = true;
			}
			return new ValueRange<T>(val, val2, flag, flag2, flag3, flag4);
		}
		public override string ToString()
		{
			return ((HasLower && IsLowerInclusive) ? "[" : "(") + (HasLower ? Lower.ToString() : "-∞") + ", " + (HasUpper ? Upper.ToString() : "+∞") + ((HasUpper && IsUpperInclusive) ? "]" : ")");
		}
		public bool Contains(T value)
		{
			T val;
			if (HasLower)
			{
				ref T reference = ref value;
				val = default;
				if (val == null)
				{
					val = reference;
					reference = ref val;
				}
				T? other = Lower;
				int num = reference.CompareTo(other);
				if (num < 0)
				{
					return false;
				}
				if (num == 0 && !IsLowerInclusive)
				{
					return false;
				}
			}
			if (HasUpper)
			{
				ref T reference2 = ref value;
				val = default;
				if (val == null)
				{
					val = reference2;
					reference2 = ref val;
				}
				T? other2 = Upper;
				int num2 = reference2.CompareTo(other2);
				if (num2 > 0)
				{
					return false;
				}
				if (num2 == 0 && !IsUpperInclusive)
				{
					return false;
				}
			}
			return true;
		}
		public bool IsEmpty()
		{
			if (!HasLower || !HasUpper)
			{
				return false;
			}
			int num = Lower.CompareTo(Upper);
			if (num <= 0)
			{
				if (num == 0)
				{
					if (IsLowerInclusive)
					{
						return !IsUpperInclusive;
					}
					return true;
				}
				return false;
			}
			return true;
		}
		public ValueRange<T> Intersect(ValueRange<T> other)
		{
			T val = default;
			T val2 = default;
			bool flag = true;
			bool flag2 = true;
			if (HasLower && other.HasLower)
			{
				int num = Lower.CompareTo(other.Lower);
				if (num <= 0)
				{
					if (num < 0)
					{
						val = other.Lower;
						flag = other.IsLowerInclusive;
					}
					else
					{
						val = Lower;
						flag = IsLowerInclusive && other.IsLowerInclusive;
					}
				}
				else
				{
					val = Lower;
					flag = IsLowerInclusive;
				}
			}
			else if (HasLower)
			{
				val = Lower;
				flag = IsLowerInclusive;
			}
			else if (other.HasLower)
			{
				val = other.Lower;
				flag = other.IsLowerInclusive;
			}
			if (HasUpper && other.HasUpper)
			{
				int num = Upper.CompareTo(other.Upper);
				if (num >= 0)
				{
					if (num > 0)
					{
						val2 = other.Upper;
						flag2 = other.IsUpperInclusive;
					}
					else
					{
						val2 = Upper;
						flag2 = IsUpperInclusive && other.IsUpperInclusive;
					}
				}
				else
				{
					val2 = Upper;
					flag2 = IsUpperInclusive;
				}
			}
			else if (HasUpper)
			{
				val2 = Upper;
				flag2 = IsUpperInclusive;
			}
			else if (other.HasUpper)
			{
				val2 = other.Upper;
				flag2 = other.IsUpperInclusive;
			}
			bool flag3 = HasLower || other.HasLower;
			bool flag4 = HasUpper || other.HasUpper;
			return new ValueRange<T>(val, val2, flag, flag2, flag3, flag4);
		}
		public override bool Equals(object? obj)
		{
			return Equals(obj as ValueRange<T>);
		}
		public bool Equals(ValueRange<T>? other)
		{
			if ((object)other != null && EqualityComparer<T>.Default.Equals(Lower, other.Lower) && EqualityComparer<T>.Default.Equals(Upper, other.Upper) && HasLower == other.HasLower && HasUpper == other.HasUpper && IsLowerInclusive == other.IsLowerInclusive)
			{
				return IsUpperInclusive == other.IsUpperInclusive;
			}
			return false;
		}
		public override int GetHashCode()
		{
			return (((((unchecked(169504101 * -1521134295) + EqualityComparer<T>.Default.GetHashCode(_lower_P)) * -1521134295 + EqualityComparer<T>.Default.GetHashCode(_upper_P)) * -1521134295 + _hasLower_P.GetHashCode()) * -1521134295 + _hasUpper_P.GetHashCode()) * -1521134295 + _isLowerInclusive_P.GetHashCode()) * -1521134295 + _isUpperInclusive_P.GetHashCode();
		}
		public static bool operator ==(ValueRange<T>? left, ValueRange<T>? right)
		{
			return EqualityComparer<ValueRange<T>>.Default.Equals(left, right);
		}
		public static bool operator !=(ValueRange<T>? left, ValueRange<T>? right)
		{
			return !(left == right);
		}
	}
}
