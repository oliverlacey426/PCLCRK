// MeloongCore

namespace MeloongCore
{
	public static class ConfigProviders
	{
		public static JsonConfigProvider Default = new JsonConfigProvider(Path.Combine(Paths.AppDataThenName, "config.json"));
		public static readonly JsonConfigProvider Secret = new JsonConfigProvider(Path.Combine(Paths.AppData, "secret.json"));
	}
}
