// PCL

namespace PCL
{
	public class LocalResourceFile
	{
		public delegate void OnUpdateEventHandler(LocalResourceFile sender);
		public readonly FileInfo File;
		private string _Display;
		private string _Description;
		public string _Version;
		private ResourceProject _Project;
		public ResourceVersion ProjectVersion;
		private ResourceVersion _UpdateFile;
		public List<string> ChangelogUrls;
		public bool OnlineDataLoaded;
		private uint? _CurseForgeHash;
		private string _ModrinthHash;
		public bool IsEnabled => new string[3] { ".jar", ".zip", ".litemod" }.Contains(File.Extension.Lower());
		public string EnabledName => File.Name.Replace(".disabled", "").Replace(".old", "");
		public string EnabledFullName => $"{File.DirectoryName}\\{EnabledName}";
		public string Display
		{
			get
			{
				if (_Display == null)
				{
					_Display = ProjectVersion?.Display;
				}
				if (_Display == null)
				{
					_Display = PathUtils.GetFileNameWithoutExtension(File.FullName);
				}
				return _Display;
			}
			set
			{
				if (_Display == null && value != null && !value.Contains("modname") && Operators.CompareString(value.Lower(), "name", TextCompare: false) != 0 && value.Count() > 1 && Operators.CompareString(ModBase.Val(value).ToString(), value, TextCompare: false) != 0)
				{
					_Display = value;
				}
			}
		}
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (_Description == null && value != null && value.Count() > 2)
				{
					_Description = value.ToString().Trim('\n');
					if (_Description.Lower().LastIndexOfAny(Conversions.ToCharArrayRankOne("qwertyuiopasdfghjklzxcvbnm0123456789")) == checked(_Description.Count() - 1))
					{
						_Description += ".";
					}
				}
			}
		}
		public string Version
		{
			get
			{
				if (_Version == null)
				{
					_Version = ProjectVersion?.Version;
				}
				return _Version;
			}
			set
			{
				if (_Version == null || !_Version.RegexCheck("[0-9.\\-]+"))
				{
					if (value != null && value.ContainsIgnoreCase("version"))
					{
						value = "version";
					}
					_Version = value;
				}
			}
		}
		public ResourceProject Project
		{
			get
			{
				return _Project;
			}
			set
			{
				_Project = value;
				OnUpdate?.Invoke(this);
			}
		}
		public ResourceVersion UpdateFile
		{
			get
			{
				return _UpdateFile;
			}
			set
			{
				_UpdateFile = value;
				OnUpdate?.Invoke(this);
			}
		}
		public bool HasUpdate
		{
			get
			{
				if (!Settings.Get<bool>("UiHiddenFunctionModUpdate") && !Settings.Get<bool>("VersionAdvanceDisableModUpdate", PageInstanceLeft.Instance))
				{
					return UpdateFile != null;
				}
				return false;
			}
		}
		public uint CurseForgeHash
		{
			get
			{
				checked
				{
					if (!_CurseForgeHash.HasValue)
					{
						string key = Conversions.ToString($"{EnabledFullName}-{File.LastWriteTime.ToLongTimeString()}-{File.Length}-C".GetStableHashCode());
						string text = ModBase.ReadIni(ModBase.PathTemp + "Cache\\ModHash.ini", key);
						if (Operators.CompareString(text, "", TextCompare: false) != 0 && text.RegexCheck("^\\d+$"))
						{
							_CurseForgeHash = Conversions.ToUInteger(text);
							return _CurseForgeHash.Value;
						}
						List<byte> list = new List<byte>();
						byte[] array = FileUtils.ReadAsBytes(File.FullName);
						foreach (byte b in array)
						{
							if (b != 9 && b != 10 && b != 13 && b != 32)
							{
								list.Add(b);
							}
						}
						int count = list.Count;
						uint num = (uint)(1 ^ count);
						int num2 = count - 4;
						int j;
						for (j = 0; j <= num2; j += 4)
						{
							uint num3;
							unchecked
							{
								num3 = (uint)checked(list[j] | (list[j + 1] << 8) | (list[j + 2] << 16) | (list[j + 3] << 24));
							}
							num3 = (uint)((unchecked((long)num3) * 1540483477L) & 0xFFFFFFFFu);
							num3 ^= num3 >> 24;
							num3 = (uint)((unchecked((long)num3) * 1540483477L) & 0xFFFFFFFFu);
							num = (uint)((unchecked((long)num) * 1540483477L) & 0xFFFFFFFFu);
							num ^= num3;
						}
						switch (count - j)
						{
						case 3:
							unchecked
							{
								num ^= (uint)(list[j] | (list[checked(j + 1)] << 8));
								num ^= (uint)(list[checked(j + 2)] << 16);
							}
							num = (uint)((unchecked((long)num) * 1540483477L) & 0xFFFFFFFFu);
							break;
						case 2:
							unchecked
							{
								num ^= (uint)(list[j] | (list[checked(j + 1)] << 8));
							}
							num = (uint)((unchecked((long)num) * 1540483477L) & 0xFFFFFFFFu);
							break;
						case 1:
							num ^= list[j];
							num = (uint)((unchecked((long)num) * 1540483477L) & 0xFFFFFFFFu);
							break;
						}
						num ^= num >> 13;
						num = (uint)((unchecked((long)num) * 1540483477L) & 0xFFFFFFFFu);
						num ^= num >> 15;
						_CurseForgeHash = num;
						ModBase.WriteIni(ModBase.PathTemp + "Cache\\ModHash.ini", key, num.ToString());
					}
					return _CurseForgeHash.Value;
				}
			}
		}
		public string ModrinthHash
		{
			get
			{
				if (_ModrinthHash == null)
				{
					string key = Conversions.ToString($"{EnabledFullName}-{File.LastWriteTime.ToLongTimeString()}-{File.Length}-M".GetStableHashCode());
					string text = ModBase.ReadIni(ModBase.PathTemp + "Cache\\ModHash.ini", key);
					if (Operators.CompareString(text, "", TextCompare: false) != 0)
					{
						_ModrinthHash = text;
						return _ModrinthHash;
					}
					_ModrinthHash = CryptographyUtils.ComputeFileHash(File.FullName, CryptographyUtils.HashMethod.Sha1);
					ModBase.WriteIni(ModBase.PathTemp + "Cache\\ModHash.ini", key, _ModrinthHash);
				}
				return _ModrinthHash;
			}
		}
		public event OnUpdateEventHandler OnUpdate;
		public LocalResourceFile(FileInfo FileInfo)
		{
			_Display = null;
			_Description = null;
			_Version = null;
			ChangelogUrls = new List<string>();
			OnlineDataLoaded = false;
			File = FileInfo;
		}
		public LocalResourceFile(string FullName)
			: this(FileUtils.GetInfo(FullName))
		{
		}
		public JObject ToJson()
		{
			JObject jObject = new JObject();
			if ((object)Project != null)
			{
				jObject.Add("Project", Project.ToJson());
			}
			jObject.Add("ChangelogUrls", new JArray(ChangelogUrls));
			jObject.Add("OnlineDataLoaded", OnlineDataLoaded);
			if (ProjectVersion != null)
			{
				jObject.Add("Version", ProjectVersion.ToCacheJson());
			}
			if (UpdateFile != null)
			{
				jObject.Add("UpdateFile", UpdateFile.ToCacheJson());
			}
			return jObject;
		}
		public void FromJson(JObject Json)
		{
			OnlineDataLoaded = (bool)Json["OnlineDataLoaded"];
			if (Json.ContainsKey("Project"))
			{
				Project = new ResourceProject((JObject)Json["Project"]);
			}
			if (Json.ContainsKey("ChangelogUrls"))
			{
				ChangelogUrls = Json["ChangelogUrls"].ToObject<List<string>>();
			}
			if (Json.ContainsKey("Version"))
			{
				ProjectVersion = ResourceVersion.FromCacheJson((JObject)Json["Version"]);
			}
			if (Json.ContainsKey("UpdateFile"))
			{
				UpdateFile = ResourceVersion.FromCacheJson((JObject)Json["UpdateFile"]);
			}
		}
		public override string ToString()
		{
			return File.FullName;
		}
		public override bool Equals(object obj)
		{
			if (obj is LocalResourceFile localResourceFile)
			{
				return Operators.CompareString(File.FullName, localResourceFile.File.FullName, TextCompare: false) == 0;
			}
			return false;
		}
	}
}
