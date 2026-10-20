// MeloongCore

namespace MeloongCore
{
	public class DynamicConfigEntry<T>(string key, T? defaultValue, bool encrypted = false)
	{
		public delegate void RefAction(ref T? value);
		public event Action<T?>? Changed;
		public event Action<PreviewGetEventArgs<T>, IConfigProvider>? PreviewGet;
		public void Edit(Func<T?, T?> editFunc, IConfigProvider provider)
		{
			Set(editFunc(Get(provider)), provider);
		}
		public void Edit(RefAction editAction, IConfigProvider provider)
		{
			T value = Get(provider);
			editAction(ref value);
			Set(value, provider);
		}
		public void Set(T? value, IConfigProvider provider)
		{
			provider.Set(key, value, encrypted);
			Changed?.Invoke(value);
		}
		public void Reset(IConfigProvider provider)
		{
			if (provider.HasValue(key))
			{
				provider.Remove(key);
				Changed?.Invoke(defaultValue);
			}
		}
		public T? Get(IConfigProvider provider)
		{
			PreviewGetEventArgs<T> e = new PreviewGetEventArgs<T>(provider.Read(key, defaultValue, encrypted));
			PreviewGet?.Invoke(e, provider);
			return e.Value;
		}
		public bool HasValue(IConfigProvider provider)
		{
			return provider.HasValue(key);
		}
	}
}
