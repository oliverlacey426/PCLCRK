// MeloongCore

namespace MeloongCore
{
	public class JsonConfigProvider : IConfigProvider
	{
		public static readonly ConcurrentBag<WeakReference<JsonConfigProvider>> allProviders = new ConcurrentBag<WeakReference<JsonConfigProvider>>();
		private readonly string filePath;
		private Lazy<JObject> json;
		private readonly ConcurrentDictionary<string, object?> cache = new ConcurrentDictionary<string, object>();
		private readonly RateLimitedWorker saveWorker;
		private bool isDirty;
		public JsonConfigProvider(string filePath)
		{
			this.filePath = filePath;
			allProviders.Add(new WeakReference<JsonConfigProvider>(this));
			saveWorker = new RateLimitedWorker(Save, 500.0, RateLimitMode.WaitUnillNoNewInvoke);
			InitJson();
		}
		private void InitJson()
		{
			json = new Lazy<JObject>(() =>
			{
				try
				{
					return FileUtils.Exists(filePath) ? (((JObject)FileUtils.ReadAsJson(filePath)) ?? new JObject()) : new JObject();
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "读取配置文件失败（" + filePath + "）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Config.cs");
					return new JObject();
				}
			});
		}
		public void Set<T>(string key, T? value, bool encrypted)
		{
			cache[key] = value;
			lock (this)
			{
				bool isEnabled;
				if (value == null)
				{
					json.Value[key] = JValue.CreateNull();
					Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(15, 2, out isEnabled);
					if (isEnabled)
					{
						handler.AppendLiteral("配置已修改：");
						handler.AppendFormatted(key);
						handler.AppendLiteral(" = null（");
						handler.AppendFormatted(filePath);
						handler.AppendLiteral("）");
					}
					Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Config.cs");
				}
				else
				{
					if (encrypted)
					{
						throw new NotImplementedException("加密需要改为使用识别码，现在尚未实现。");
					}
					JToken jToken = JToken.FromObject(value);
					json.Value[key] = jToken;
					Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(11, 3, out isEnabled);
					if (isEnabled)
					{
						handler2.AppendLiteral("配置已修改：");
						handler2.AppendFormatted(key);
						handler2.AppendLiteral(" = ");
						handler2.AppendFormatted(jToken.ToString(Formatting.None));
						handler2.AppendLiteral("（");
						handler2.AppendFormatted(filePath);
						handler2.AppendLiteral("）");
					}
					Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Config.cs");
				}
			}
			MakeDirty();
		}
		public T? Read<T>(string key, T? defaultValue, bool encrypted)
		{
			if (cache.TryGetValue(key, out object value))
			{
				return (T)value;
			}
			JToken value2;
			lock (this)
			{
				if (!json.Value.TryGetValue(key, out value2))
				{
					return defaultValue;
				}
				value2 = value2.DeepClone();
			}
			try
			{
				JTokenType type = value2.Type;
				T val;
				if ((uint)(type - 10) <= 1u)
				{
					val = default;
				}
				else if (encrypted)
				{
					string text = CryptographyUtils.AesDecrypt(value2.Value<string>());
					val = ((typeof(T) == typeof(string)) ? ((T)(object)text) : JsonConvert.DeserializeObject<T>(text));
				}
				else
				{
					val = value2.ToObject<T>();
				}
				cache[key] = val;
				return val;
			}
			catch (CryptographicException ex)
			{
				Logger.Error(ex, "解密配置失败，该配置将被重置（" + key + "）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\Config.cs");
				Remove(key);
				return defaultValue;
			}
		}
		public bool HasValue(string key)
		{
			lock (this)
			{
				return json.Value.ContainsKey(key);
			}
		}
		public void Remove(string key)
		{
			lock (this)
			{
				json.Value.Remove(key);
			}
			cache.TryRemove(key, out object _);
			MakeDirty();
		}
		public void DiscardCache()
		{
			lock (this)
			{
				InitJson();
			}
			cache.Clear();
		}
		public void MakeDirty()
		{
			isDirty = true;
			saveWorker.Start();
		}
		public void Save()
		{
			if (!isDirty)
			{
				return;
			}
			isDirty = false;
			lock (this)
			{
				FileUtils.Write(filePath, json.Value.ToString(Formatting.Indented));
			}
		}
	}
}
