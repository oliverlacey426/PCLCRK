// MeloongCore

namespace MeloongCore
{
	public interface IConfigProvider
	{
		void Set<T>(string key, T? value, bool encrypted);
		void Remove(string key);
		T? Read<T>(string key, T? defaultValue, bool encrypted);
		bool HasValue(string key);
		void DiscardCache();
	}
}
