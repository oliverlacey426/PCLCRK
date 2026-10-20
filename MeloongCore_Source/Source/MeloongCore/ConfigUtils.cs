// MeloongCore

namespace MeloongCore
{
	public static class ConfigUtils
	{
		public static void SaveAll()
		{
			foreach (WeakReference<JsonConfigProvider> allProvider in JsonConfigProvider.allProviders)
			{
				if (allProvider.TryGetTarget(out var target))
				{
					target.Save();
				}
			}
		}
		public static string GetSecret(string key)
		{
			return ConfigProviders.Secret.Read<string>(key, null, encrypted: false) ?? throw new KeyNotFoundException("未找到密钥：" + key);
		}
	}
}
