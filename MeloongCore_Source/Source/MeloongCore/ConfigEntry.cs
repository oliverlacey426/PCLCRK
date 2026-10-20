// MeloongCore

namespace MeloongCore
{
	public class ConfigEntry<T>(string key, T? defaultValue, bool encrypted = false, IConfigProvider? provider = null)
	{
		public delegate void RefAction(ref T? value);
		public IConfigProvider Provider { get; } = provider ?? ConfigProviders.Default;
		public event Action<T?>? Changed;
		public event Action<PreviewGetEventArgs<T>, IConfigProvider>? PreviewGet;
		public void Edit(Func<T?, T?> editFunc)
		{
			Set(editFunc(Get()));
		}
		public void Edit(RefAction editAction)
		{
			T value = Get();
			editAction(ref value);
			Set(value);
		}
		public void Set(T? value)
		{
			Provider.Set(key, value, encrypted);
			Changed?.Invoke(value);
		}
		public void Reset()
		{
			if (Provider.HasValue(key))
			{
				Provider.Remove(key);
				Changed?.Invoke(defaultValue);
			}
		}
		public T? Get()
		{
			PreviewGetEventArgs<T> e = new PreviewGetEventArgs<T>(Provider.Read(key, defaultValue, encrypted));
			PreviewGet?.Invoke(e, Provider);
			return e.Value;
		}
		public bool HasValue()
		{
			return Provider.HasValue(key);
		}
	}
}
