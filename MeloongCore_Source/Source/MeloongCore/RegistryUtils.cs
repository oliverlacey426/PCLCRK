// MeloongCore

namespace MeloongCore
{
	public static class RegistryUtils
	{
		public const string NULL_STRING = "__NULL__";
		public static (object? result, bool exists) Read(string folder, string entry)
		{
			using RegistryKey registryKey = GetRegistryKey(folder);
			object obj = registryKey?.GetValue(entry);
			if (obj == null)
			{
				return (result: null, exists: false);
			}
			return (result: (obj is string text && text == "__NULL__") ? null : obj, exists: true);
		}
		public static object? TryRead(string folder, string entry, object? defaultValue = null)
		{
			try
			{
				(object? result, bool exists) tuple = Read(folder, entry);
				var (obj, _) = tuple;
				return tuple.exists ? obj : defaultValue;
			}
			catch (Exception ex)
			{
				Logger.Warn(ex, "读取注册表值失败（" + folder + "，" + entry + "）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\RegistryUtils.cs");
				return defaultValue;
			}
		}
		public static void Write(string folder, string entry, object? value)
		{
			using RegistryKey registryKey = GetRegistryKey(folder, writable: false, create: true);
			registryKey.SetValue(entry, value ?? "__NULL__");
		}
		public static bool Has(string folder, string entry)
		{
			using RegistryKey registryKey = GetRegistryKey(folder);
			if (registryKey == null)
			{
				return false;
			}
			object obj = new object();
			return registryKey.GetValue(entry, obj) != obj;
		}
		public static void Delete(string folder, string entry)
		{
			using RegistryKey registryKey = GetRegistryKey(folder, writable: true);
			registryKey?.DeleteValue(entry, throwOnMissingValue: false);
		}
		private static RegistryKey? GetRegistryKey(string fullPath, bool writable = false, bool create = false)
		{
			using RegistryKey registryKey = fullPath.BeforeFirst("\\").ToUpperInvariant() switch
			{
				"HKEY_CLASSES_ROOT" => Registry.ClassesRoot, 
				"HKEY_CURRENT_USER" => Registry.CurrentUser, 
				"HKEY_LOCAL_MACHINE" => Registry.LocalMachine, 
				"HKEY_USERS" => Registry.Users, 
				"HKEY_CURRENT_CONFIG" => Registry.CurrentConfig, 
				_ => throw new ArgumentException("注册表路径有误（" + fullPath + "）", "fullPath"), 
			};
			string text = fullPath.AfterFirst("\\");
			return create ? registryKey.CreateSubKey(text) : registryKey.OpenSubKey(text, writable);
		}
	}
}
