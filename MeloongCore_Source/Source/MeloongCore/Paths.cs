// MeloongCore

namespace MeloongCore
{
	public static class Paths
	{
		public static string Base => PathUtils.AddSlashSuffix(AppContext.BaseDirectory);
		public static string BaseThenName => PathUtils.AddSlashSuffix(Base + Main.AppName);
		public static string AppData => PathUtils.AddSlashSuffix(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		public static string AppDataThenName => PathUtils.AddSlashSuffix(AppData + Main.AppName);
	}
}
