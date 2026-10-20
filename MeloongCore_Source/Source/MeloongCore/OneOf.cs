// MeloongCore

namespace MeloongCore
{
	public readonly struct OneOf<T1, T2>
	{
		private readonly int _index;
		private readonly T1? _value1;
		private readonly T2? _value2;
		public static implicit operator OneOf<T1, T2>(T1 value)
		{
			return new OneOf<T1, T2>(1, value, default);
		}
		public static implicit operator OneOf<T1, T2>(T2 value)
		{
			return new OneOf<T1, T2>(2, default, value);
		}
		public static implicit operator T1(OneOf<T1, T2> value)
		{
			return value.As<T1>();
		}
		public static implicit operator T2(OneOf<T1, T2> value)
		{
			return value.As<T2>();
		}
		private OneOf(int index, T1? value1, T2? value2)
		{
			_index = -1;
			_index = index;
			_value1 = value1;
			_value2 = value2;
		}
		public TResult Switch<TResult>(Func<T1, TResult> f1, Func<T2, TResult> f2)
		{
			return _index switch
			{
				1 => f1(_value1), 
				2 => f2(_value2), 
				_ => throw new InvalidOperationException(), 
			};
		}
		public void Switch(Action<T1> f1, Action<T2> f2)
		{
			if (_index == 1)
			{
				f1(_value1);
				return;
			}
			if (_index == 2)
			{
				f2(_value2);
				return;
			}
			throw new InvalidOperationException();
		}
		public bool Is<T>()
		{
			switch (_index)
			{
			case 1:
				if (typeof(T) == typeof(T1))
				{
					return true;
				}
				break;
			case 2:
				if (typeof(T) == typeof(T2))
				{
					return true;
				}
				break;
			}
			return false;
		}
		public bool IsT1()
		{
			return _index == 1;
		}
		public bool IsT2()
		{
			return _index == 2;
		}
		public T As<T>()
		{
			switch (_index)
			{
			case 1:
				if (typeof(T) == typeof(T1))
				{
					return (T)(object)_value1;
				}
				break;
			case 2:
				if (typeof(T) == typeof(T2))
				{
					return (T)(object)_value2;
				}
				break;
			}
			throw new InvalidOperationException();
		}
		public T1 AsT1()
		{
			if (_index == 1)
			{
				return _value1;
			}
			throw new InvalidOperationException();
		}
		public T2 AsT2()
		{
			if (_index == 2)
			{
				return _value2;
			}
			throw new InvalidOperationException();
		}
	}
}
