// PCL

namespace PCL
{
	public class ResourceSearcher
	{
		public class SearchRequest
		{
			public SearchResult Storage;
			public int TargetResultCount;
			public ResourceTypes Type;
			public string Tag;
			public ModLoaders ModLoaders;
			public string GameVersion;
			public string SearchText;
			public ResourcePlatforms Sources;
			public bool CanContinue
			{
				get
				{
					if (Tag.StartsWithF("/") || !Sources.HasFlagF(ResourcePlatforms.CurseForge))
					{
						Storage.CurseForgeTotal = 0;
					}
					if (Tag.EndsWithF("/") || !Sources.HasFlagF(ResourcePlatforms.Modrinth))
					{
						Storage.ModrinthTotal = 0;
					}
					if (Storage.CurseForgeTotal == -1 || Storage.ModrinthTotal == -1)
					{
						return true;
					}
					return Storage.CurseForgeOffset < Storage.CurseForgeTotal || Storage.ModrinthOffset < Storage.ModrinthTotal;
				}
			}
			public SearchRequest(ResourceTypes Type, SearchResult Storage, int TargetResultCount)
			{
				Tag = "";
				ModLoaders = ModLoaders.None;
				GameVersion = null;
				SearchText = null;
				Sources = ResourcePlatforms.Any;
				this.Type = Type;
				this.Storage = Storage;
				this.TargetResultCount = TargetResultCount;
			}
			public string GetCurseForgeAddress(string SearchText, bool IgnoreModLoaderFilter)
			{
				if (Tag.StartsWithF("/"))
				{
					Storage.CurseForgeTotal = 0;
				}
				string text = $"https://api.curseforge.com/v1/mods/search?gameId=432&sortField=2&sortOrder=desc&pageSize={40}";
				switch (Type)
				{
				case ResourceTypes.Mod:
					text += "&classId=6";
					break;
				case ResourceTypes.ModPack:
					text += "&classId=4471";
					break;
				case ResourceTypes.DataPack:
					text += "&classId=6945";
					break;
				case ResourceTypes.Shader:
					text += "&classId=6552";
					break;
				case ResourceTypes.ResourcePack:
					text += "&classId=12";
					break;
				}
				if (Operators.CompareString(Tag, "", TextCompare: false) != 0)
				{
					text = text + "&categoryId=" + Tag.BeforeFirst("/");
				}
				if (ModLoaders != ModLoaders.None && !IgnoreModLoaderFilter)
				{
					text = text + "&modLoaderType=" + Conversions.ToString(Resource.ToCurseForgeModLoaderType(ModLoaders.Flags().First()));
				}
				if (!string.IsNullOrEmpty(GameVersion))
				{
					text = text + "&gameVersion=" + GameVersion;
				}
				if (!string.IsNullOrEmpty(SearchText))
				{
					text = text + "&searchFilter=" + StringUtils.UrlEscape(SearchText);
				}
				if (Storage.CurseForgeOffset > 0)
				{
					text = text + "&index=" + Conversions.ToString(Storage.CurseForgeOffset);
				}
				return text;
			}
			public string GetModrinthAddress(string SearchText, bool IgnoreModLoaderFilter)
			{
				if (Tag.EndsWithF("/"))
				{
					Storage.ModrinthTotal = 0;
				}
				string text = $"https://api.modrinth.com/v2/search?limit={40}&index=relevance";
				if (!string.IsNullOrEmpty(SearchText))
				{
					text = text + "&query=" + StringUtils.UrlEscape(SearchText);
				}
				if (Storage.ModrinthOffset > 0)
				{
					text = text + "&offset=" + Conversions.ToString(Storage.ModrinthOffset);
				}
				List<string> list = new List<string>();
				list.Add($"[\"project_type:{Type.ToString().Lower()}\"]");
				if (!string.IsNullOrEmpty(Tag))
				{
					list.Add(string.Format("[\"categories:'{0}'\"]", Tag.AfterLast("/")));
				}
				if (ModLoaders != ModLoaders.None && !IgnoreModLoaderFilter)
				{
					list.Add(string.Format("[\"categories:'{0}'\"]", (from f in ModLoaders.Flags()
						select f.ToString().Lower()).Join("',categories:'")));
				}
				if (!string.IsNullOrEmpty(GameVersion))
				{
					list.Add($"[\"versions:'{GameVersion}'\"]");
				}
				return text + "&facets=[" + string.Join(",", list) + "]";
			}
			public override bool Equals(object obj)
			{
				if (obj is SearchRequest searchRequest && Type == searchRequest.Type && TargetResultCount == searchRequest.TargetResultCount && Operators.CompareString(Tag, searchRequest.Tag, TextCompare: false) == 0 && ModLoaders == searchRequest.ModLoaders && Sources == searchRequest.Sources && Operators.CompareString(GameVersion, searchRequest.GameVersion, TextCompare: false) == 0)
				{
					return Operators.CompareString(SearchText, searchRequest.SearchText, TextCompare: false) == 0;
				}
				return false;
			}
			public static bool operator ==(SearchRequest left, SearchRequest right)
			{
				return EqualityComparer<SearchRequest>.Default.Equals(left, right);
			}
			public static bool operator !=(SearchRequest left, SearchRequest right)
			{
				return !(left == right);
			}
			public override int GetHashCode()
			{
				return (Type, Tag, ModLoaders, GameVersion, SearchText, Sources).GetHashCode();
			}
		}
		public class SearchResult
		{
			public int CurseForgeOffset;
			public int CurseForgeTotal;
			public int ModrinthOffset;
			public int ModrinthTotal;
			public List<ResourceProject> Results;
			public string ErrorMessage;
			public SearchResult()
			{
				CurseForgeOffset = 0;
				CurseForgeTotal = -1;
				ModrinthOffset = 0;
				ModrinthTotal = -1;
				Results = new List<ResourceProject>();
				ErrorMessage = null;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_0
		{
			public List<string> _VB_Local_Candidates;
			public Func<string, bool> _VB_Local_CanForm;
			public _Closure___6_0(_Closure___6_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Candidates = arg0._VB_Local_Candidates;
					_VB_Local_CanForm = arg0._VB_Local_CanForm;
				}
			}
			[SpecialName]
			internal bool _Lambda___4(string s)
			{
				_Closure___6_1 arg = default;
				_Closure___6_1 CS_8__locals6 = new _Closure___6_1(arg);
				CS_8__locals6._VB_NonLocal__VB_Closure_2 = this;
				CS_8__locals6._VB_Local_s = s;
				if (!_VB_Local_Candidates.Contains(CS_8__locals6._VB_Local_s))
				{
					return _VB_Local_Candidates.Any([SpecialName] (string c) => CS_8__locals6._VB_Local_s.StartsWith(c) && CS_8__locals6._VB_NonLocal__VB_Closure_2._VB_Local_CanForm(CS_8__locals6._VB_Local_s.Substring(c.Length)));
				}
				return true;
			}
			[SpecialName]
			internal bool _Lambda___6(string w)
			{
				_Closure___6_2 arg = default;
				_Closure___6_2 CS_8__locals6 = new _Closure___6_2(arg);
				CS_8__locals6._VB_NonLocal__VB_Closure_3 = this;
				CS_8__locals6._VB_Local_w = w;
				return !_VB_Local_Candidates.Any([SpecialName] (string c) => c.Length < CS_8__locals6._VB_Local_w.Length && CS_8__locals6._VB_Local_w.StartsWith(c) && CS_8__locals6._VB_NonLocal__VB_Closure_3._VB_Local_CanForm(CS_8__locals6._VB_Local_w.Substring(c.Length)));
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_1
		{
			public string _VB_Local_s;
			public _Closure___6_0 _VB_NonLocal__VB_Closure_2;
			public _Closure___6_1(_Closure___6_1 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_s = arg0._VB_Local_s;
				}
			}
			[SpecialName]
			internal bool _Lambda___5(string c)
			{
				if (_VB_Local_s.StartsWith(c))
				{
					return _VB_NonLocal__VB_Closure_2._VB_Local_CanForm(_VB_Local_s.Substring(c.Length));
				}
				return false;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_2
		{
			public string _VB_Local_w;
			public _Closure___6_0 _VB_NonLocal__VB_Closure_3;
			public _Closure___6_2(_Closure___6_2 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_w = arg0._VB_Local_w;
				}
			}
			[SpecialName]
			internal bool _Lambda___7(string c)
			{
				if (c.Length < _VB_Local_w.Length && _VB_Local_w.StartsWith(c))
				{
					return _VB_NonLocal__VB_Closure_3._VB_Local_CanForm(_VB_Local_w.Substring(c.Length));
				}
				return false;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_3
		{
			public string _VB_Local_RawSearchText;
			public SearchRequest _VB_Local_Request;
			public string _VB_Local_CurseForgeAltSearchText;
			public bool _VB_Local_IgnoreModLoaderFilter;
			public ConcurrentBag<ResourceProject> _VB_Local_SearchResults;
			public SearchResult _VB_Local_Storage;
			public ConcurrentBag<(Exception Ex, ResourcePlatforms Source)> _VB_Local_Errors;
			public ModLoader.LoaderTask<SearchRequest, int> _VB_Local_Task;
			public string _VB_Local_ModrinthAltSearchText;
			public List<string> _VB_Local_ModrinthSlugs;
			public List<ResourceProject> _VB_Local_RealResults;
			public Func<ModBase.SearchSource, bool> _I12;
			public Action<ResourceProject> _I16;
			public Action<ResourceProject> _I18;
			public Func<JToken, bool> _I20;
			public Func<string, bool> _I21;
			public Action<ResourceProject> _I22;
			public _Closure___6_3(_Closure___6_3 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_RawSearchText = arg0._VB_Local_RawSearchText;
					_VB_Local_Request = arg0._VB_Local_Request;
					_VB_Local_CurseForgeAltSearchText = arg0._VB_Local_CurseForgeAltSearchText;
					_VB_Local_IgnoreModLoaderFilter = arg0._VB_Local_IgnoreModLoaderFilter;
					_VB_Local_SearchResults = arg0._VB_Local_SearchResults;
					_VB_Local_Storage = arg0._VB_Local_Storage;
					_VB_Local_Errors = arg0._VB_Local_Errors;
					_VB_Local_Task = arg0._VB_Local_Task;
					_VB_Local_ModrinthAltSearchText = arg0._VB_Local_ModrinthAltSearchText;
					_VB_Local_ModrinthSlugs = arg0._VB_Local_ModrinthSlugs;
					_VB_Local_RealResults = arg0._VB_Local_RealResults;
				}
			}
			[SpecialName]
			internal bool _Lambda___12(ModBase.SearchSource s)
			{
				return s.Aliases.Contains(_VB_Local_RawSearchText);
			}
			[SpecialName]
			internal void _Lambda___15()
			{
				checked
				{
					try
					{
						string curseForgeAddress = _VB_Local_Request.GetCurseForgeAddress(_VB_Local_CurseForgeAltSearchText ?? _VB_Local_RawSearchText, _VB_Local_IgnoreModLoaderFilter);
						Logger.Info($"开始 CurseForge 搜索：{curseForgeAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						JObject jObject = (JObject)ModDownload.DlModRequest(curseForgeAddress);
						List<ResourceProject> list = new List<ResourceProject>();
						foreach (JObject item in (IEnumerable<JToken>)jObject["data"])
						{
							ResourceProject resourceProject = new ResourceProject(item);
							if ((_VB_Local_Request.Type != ResourceTypes.ResourcePack || !resourceProject.Tags.Contains("数据包")) && (_VB_Local_Request.Type == ResourceTypes.Any || resourceProject.Types.HasFlagF(_VB_Local_Request.Type)))
							{
								list.Add(resourceProject);
							}
						}
						list.ForEach([SpecialName] (ResourceProject p) =>
						{
							_VB_Local_SearchResults.Add(p);
						});
						_VB_Local_Storage.CurseForgeOffset += jObject["data"].Count();
						_VB_Local_Storage.CurseForgeTotal = jObject["pagination"]["totalCount"].ToObject<int>();
						Logger.Info($"从 CurseForge 搜索到了 {list.Count} 个工程（总计已获取 {_VB_Local_Storage.CurseForgeOffset} 个，共 {_VB_Local_Storage.CurseForgeTotal} 个）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "CurseForge 搜索失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						_VB_Local_Storage.CurseForgeTotal = -1;
						_VB_Local_Errors.Add((ex2, ResourcePlatforms.CurseForge));
						ProjectData.ClearProjectError();
					}
					if (_VB_Local_Task.Progress < 0.75)
					{
						_VB_Local_Task.Progress += 0.25;
					}
				}
			}
			[SpecialName]
			internal void _Lambda___16(ResourceProject p)
			{
				_VB_Local_SearchResults.Add(p);
			}
			[SpecialName]
			internal void _Lambda___17()
			{
				checked
				{
					try
					{
						string modrinthAddress = _VB_Local_Request.GetModrinthAddress(_VB_Local_ModrinthAltSearchText ?? _VB_Local_RawSearchText, _VB_Local_IgnoreModLoaderFilter);
						Logger.Info($"开始 Modrinth 搜索：{modrinthAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						JObject jObject = (JObject)ModDownload.DlModRequest(modrinthAddress);
						List<ResourceProject> list = new List<ResourceProject>();
						foreach (JObject item in (IEnumerable<JToken>)jObject["hits"])
						{
							list.Add(new ResourceProject(item));
						}
						list.ForEach([SpecialName] (ResourceProject p) =>
						{
							_VB_Local_SearchResults.Add(p);
						});
						_VB_Local_Storage.ModrinthOffset += jObject["hits"].Count();
						_VB_Local_Storage.ModrinthTotal = jObject["total_hits"].ToObject<int>();
						Logger.Info($"从 Modrinth 搜索到了 {list.Count} 个工程（总计已获取 {_VB_Local_Storage.ModrinthOffset} 个，共 {_VB_Local_Storage.ModrinthTotal} 个）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "Modrinth 搜索失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						_VB_Local_Storage.ModrinthTotal = -1;
						_VB_Local_Errors.Add((ex2, ResourcePlatforms.Modrinth));
						ProjectData.ClearProjectError();
					}
					if (_VB_Local_Task.Progress < 0.75)
					{
						_VB_Local_Task.Progress += 0.25;
					}
				}
			}
			[SpecialName]
			internal void _Lambda___18(ResourceProject p)
			{
				_VB_Local_SearchResults.Add(p);
			}
			[SpecialName]
			internal void _Lambda___19()
			{
				try
				{
					string text = string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", _VB_Local_ModrinthSlugs.Join("\",\""));
					Logger.Info($"开始 Modrinth 直接获取：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					List<ResourceProject> list = new List<ResourceProject>();
					foreach (JObject item in (IEnumerable<JToken>)ModDownload.DlModRequest(text))
					{
						ResourceProject resourceProject = new ResourceProject(item);
						if ((_VB_Local_Request.Type == ResourceTypes.Any || resourceProject.Types.HasFlagF(_VB_Local_Request.Type)) && (string.IsNullOrEmpty(_VB_Local_Request.Tag) || item["categories"].Any([SpecialName] (JToken c) => Operators.CompareString(c.ToString(), _VB_Local_Request.Tag.AfterLast("/"), TextCompare: false) == 0)) && (_VB_Local_Request.ModLoaders == ModLoaders.None || _VB_Local_IgnoreModLoaderFilter || resourceProject.ModLoaders.Flags().Intersect(_VB_Local_Request.ModLoaders.Flags()).Any()) && (string.IsNullOrEmpty(_VB_Local_Request.GameVersion) || resourceProject.UnsafeGameVersions.Any([SpecialName] (string d) => Operators.CompareString(d, _VB_Local_Request.GameVersion, TextCompare: false) == 0)))
						{
							list.Add(resourceProject);
						}
					}
					list.ForEach([SpecialName] (ResourceProject p) =>
					{
						_VB_Local_SearchResults.Add(p);
					});
					Logger.Info($"从 Modrinth 直接获取到了 {list.Count} 个工程", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					_VB_Local_ModrinthSlugs.Clear();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "Modrinth 直接获取失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					_VB_Local_Errors.Add((ex2, ResourcePlatforms.Modrinth));
					ProjectData.ClearProjectError();
				}
				if (_VB_Local_Task.Progress < 0.75)
				{
					_VB_Local_Task.Progress += 0.25;
				}
			}
			[SpecialName]
			internal bool _Lambda___20(JToken c)
			{
				return Operators.CompareString(c.ToString(), _VB_Local_Request.Tag.AfterLast("/"), TextCompare: false) == 0;
			}
			[SpecialName]
			internal bool _Lambda___21(string d)
			{
				return Operators.CompareString(d, _VB_Local_Request.GameVersion, TextCompare: false) == 0;
			}
			[SpecialName]
			internal void _Lambda___22(ResourceProject p)
			{
				_VB_Local_SearchResults.Add(p);
			}
			[SpecialName]
			internal bool _Lambda___28(ResourceProject r)
			{
				_Closure___6_4 arg = default;
				_Closure___6_4 CS_8__locals3 = new _Closure___6_4(arg);
				CS_8__locals3._VB_Local_r = r;
				if (!_VB_Local_RealResults.Any([SpecialName] (ResourceProject b) => CS_8__locals3._VB_Local_r.IsLike(b)))
				{
					return !_VB_Local_Storage.Results.Any([SpecialName] (ResourceProject b) => CS_8__locals3._VB_Local_r.IsLike(b));
				}
				return false;
			}
			[SpecialName]
			internal double _Lambda___31(ResourceProject Project)
			{
				switch (_VB_Local_Request.Type)
				{
				case ResourceTypes.Mod:
				case ResourceTypes.ModPack:
					return (Project.Platform == ResourcePlatforms.CurseForge) ? 1 : 5;
				case ResourceTypes.DataPack:
					return (Project.Platform != ResourcePlatforms.CurseForge) ? 1 : 10;
				case ResourceTypes.ResourcePack:
				case ResourceTypes.Shader:
					return (Project.Platform == ResourcePlatforms.CurseForge) ? 1 : 4;
				default:
					return 1.0;
				}
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___6_4
		{
			public ResourceProject _VB_Local_r;
			public _Closure___6_4(_Closure___6_4 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_r = arg0._VB_Local_r;
				}
			}
			[SpecialName]
			internal bool _Lambda___29(ResourceProject b)
			{
				return _VB_Local_r.IsLike(b);
			}
			[SpecialName]
			internal bool _Lambda___30(ResourceProject b)
			{
				return _VB_Local_r.IsLike(b);
			}
		}
		private const int RESULT_PAGE_SIZE = 40;
		private const string MSG_NO_CHINESE_RESULT = "无搜索结果，请尝试搜索其英文名称";
		[SpecialName]
		private static List<ModBase.SearchEntry<WikiEntry>> _STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init;
		[SpecialName]
		private static List<ModBase.SearchEntry<WikiEntry>> _STATIC_Run_011151286AC21283B48_ModrinthSearchEntries;
		[SpecialName]
		private static StaticLocalInitFlag _STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init;
		public static ModLoader.LoaderTask<SearchRequest, int> GetLoader(Func<SearchRequest> InputDelegate)
		{
			return new ModLoader.LoaderTask<SearchRequest, int>("社区资源获取", Run, InputDelegate)
			{
				ReloadTimeout = 60000
			};
		}
		private static void Run(ModLoader.LoaderTask<SearchRequest, int> Task)
		{
			_Closure___6_3 arg = default;
			_Closure___6_3 CS_8__locals167 = new _Closure___6_3(arg);
			CS_8__locals167._VB_Local_Task = Task;
			CS_8__locals167._VB_Local_Request = CS_8__locals167._VB_Local_Task.Input;
			CS_8__locals167._VB_Local_Storage = CS_8__locals167._VB_Local_Request.Storage;
			if (CS_8__locals167._VB_Local_Storage.Results.Count >= CS_8__locals167._VB_Local_Request.TargetResultCount)
			{
				Logger.Info($"已有 {CS_8__locals167._VB_Local_Storage.Results.Count} 个结果，多于所需的 {CS_8__locals167._VB_Local_Request.TargetResultCount} 个结果，结束处理", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				return;
			}
			if (!CS_8__locals167._VB_Local_Request.CanContinue)
			{
				if (!CS_8__locals167._VB_Local_Storage.Results.Any())
				{
					throw new Exception("没有符合条件的结果");
				}
				Logger.Info($"已有 {CS_8__locals167._VB_Local_Storage.Results.Count} 个结果，少于所需的 {CS_8__locals167._VB_Local_Request.TargetResultCount} 个结果，但无法继续获取，结束处理", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				return;
			}
			CS_8__locals167._VB_Local_RawSearchText = (CS_8__locals167._VB_Local_Request.SearchText ?? "").Trim();
			CS_8__locals167._VB_Local_RawSearchText = CS_8__locals167._VB_Local_RawSearchText.Lower();
			Logger.Info($"工程列表搜索原始文本：{CS_8__locals167._VB_Local_RawSearchText}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
			CS_8__locals167._VB_Local_RawSearchText = Strings.StrConv(CS_8__locals167._VB_Local_RawSearchText, VbStrConv.SimplifiedChinese);
			bool flag = CS_8__locals167._VB_Local_RawSearchText.RegexCheck("[\\u4e00-\\u9fbb]") && !string.IsNullOrEmpty(CS_8__locals167._VB_Local_RawSearchText) && (CS_8__locals167._VB_Local_Request.Type == ResourceTypes.Mod || CS_8__locals167._VB_Local_Request.Type == ResourceTypes.DataPack);
			CS_8__locals167._VB_Local_CurseForgeAltSearchText = null;
			CS_8__locals167._VB_Local_ModrinthAltSearchText = null;
			CS_8__locals167._VB_Local_ModrinthSlugs = new List<string>();
			if (flag)
			{
				VB_AnonymousDelegate_1<ModBase.SearchEntry<WikiEntry>, ResourcePlatforms, IEnumerable<string>> obj = [SpecialName] (ModBase.SearchEntry<WikiEntry> Result, ResourcePlatforms Source) =>
				{
					_Closure___6_0 arg2 = default;
					_Closure___6_0 CS_8__locals162 = new _Closure___6_0(arg2);
					CS_8__locals162._VB_Local_Candidates = new List<string>();
					if (Result.Item.Slugs.ContainsKey(Source))
					{
						CS_8__locals162._VB_Local_Candidates.Add(Result.Item.Slugs[Source].Replace("-", " ").Replace("/", " "));
					}
					if (Result.Item.ChineseName != null)
					{
						CS_8__locals162._VB_Local_Candidates.Add(Result.Item.ChineseName.AfterLast(" (").TrimEnd(')').BeforeFirst(" - ")
							.Replace("-", " ")
							.Replace("/", " ")
							.Replace(":", " ")
							.Replace("(", " ")
							.Replace(")", ""));
					}
					CS_8__locals162._VB_Local_Candidates = (from w in CS_8__locals162._VB_Local_Candidates.SelectMany([SpecialName] (string c) => c.Split(" "))
						select w.TrimStart('{').TrimEnd('}').Lower()).Where([SpecialName] (string w) =>
					{
						if (w.Length <= 1)
						{
							return false;
						}
						if (new string[9] { "the", "of", "mod", "and", "forge", "fabric", "for", "quilt", "neoforge" }.Contains(w))
						{
							return false;
						}
						if (ModBase.Val(w) > 0.0)
						{
							return false;
						}
						return w.IsAsciiOnly() ? true : false;
					}).Distinct().ToList();
					CS_8__locals162._VB_Local_CanForm = null;
					CS_8__locals162._VB_Local_CanForm = [SpecialName] (string s) =>
					{
						_Closure___6_1 arg3 = default;
						_Closure___6_1 CS_8__locals172 = new _Closure___6_1(arg3);
						CS_8__locals172._VB_NonLocal__VB_Closure_2 = CS_8__locals162;
						CS_8__locals172._VB_Local_s = s;
						return CS_8__locals162._VB_Local_Candidates.Contains(CS_8__locals172._VB_Local_s) || CS_8__locals162._VB_Local_Candidates.Any([SpecialName] (string c) => CS_8__locals172._VB_Local_s.StartsWith(c) && CS_8__locals172._VB_NonLocal__VB_Closure_2._VB_Local_CanForm(CS_8__locals172._VB_Local_s.Substring(c.Length)));
					};
					CS_8__locals162._VB_Local_Candidates = CS_8__locals162._VB_Local_Candidates.Where([SpecialName] (string w) =>
					{
						_Closure___6_2 arg3 = default;
						_Closure___6_2 CS_8__locals176 = new _Closure___6_2(arg3);
						CS_8__locals176._VB_NonLocal__VB_Closure_3 = CS_8__locals162;
						CS_8__locals176._VB_Local_w = w;
						return !CS_8__locals162._VB_Local_Candidates.Any([SpecialName] (string c) => c.Length < CS_8__locals176._VB_Local_w.Length && CS_8__locals176._VB_Local_w.StartsWith(c) && CS_8__locals176._VB_NonLocal__VB_Closure_3._VB_Local_CanForm(CS_8__locals176._VB_Local_w.Substring(c.Length)));
					}).ToList();
					return CS_8__locals162._VB_Local_Candidates;
				};
				VB_AnonymousDelegate_2<ResourcePlatforms, IEnumerable<ModBase.SearchEntry<WikiEntry>>> obj2 = (ResourcePlatforms Source) => Iterator_6_8(Source);
				IEnumerable<ModBase.SearchEntry<WikiEntry>> Iterator_6_8(ResourcePlatforms Source)
				{
					foreach (WikiEntry item in WikiEntry.All.Value)
					{
						if (item.Slugs.ContainsKey(Source))
						{
							if (item.ChineseName != null)
							{
								yield return new ModBase.SearchEntry<WikiEntry>
								{
									Item = item,
									SearchSource = new List<ModBase.SearchSource>
									{
										new ModBase.SearchSource(Strings.StrConv(item.ChineseName.BeforeFirst(" ("), VbStrConv.SimplifiedChinese).Split('/', removeEmptyEntries: true)),
										new ModBase.SearchSource(Strings.StrConv(item.ChineseName.AfterFirst(" ("), VbStrConv.SimplifiedChinese) + item.Slugs[Source], 0.5)
									}
								};
							}
							else
							{
								yield return new ModBase.SearchEntry<WikiEntry>
								{
									Item = item,
									SearchSource = new List<ModBase.SearchSource>
									{
										new ModBase.SearchSource(item.Slugs[Source], 0.5)
									}
								};
							}
						}
					}
								}
				if (CS_8__locals167._VB_Local_Request.Sources.HasFlagF(ResourcePlatforms.CurseForge))
				{
					if (_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init == null)
					{
						Interlocked.CompareExchange(ref _STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken = false;
					try
					{
						Monitor.Enter(_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init, ref lockTaken);
						if (_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init.State == 0)
						{
							_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init.State = 2;
							_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries = obj2(ResourcePlatforms.CurseForge).ToList();
						}
						else if (_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init.State = 1;
						if (lockTaken)
						{
							Monitor.Exit(_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries_Init);
						}
					}
					List<ModBase.SearchEntry<WikiEntry>> source = ModBase.Search(_STATIC_Run_011151286AC21283B48_CurseForgeSearchEntries, CS_8__locals167._VB_Local_RawSearchText, 100, 0.25);
					if (source.Any())
					{
						ModBase.SearchEntry<WikiEntry> result = (source.First().AbsoluteRight ? source.Where([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.AbsoluteRight).ToList() : source.MaxByAll([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.Similarity)).MaxBy([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.Item.Popularity);
						CS_8__locals167._VB_Local_CurseForgeAltSearchText = obj(result, ResourcePlatforms.CurseForge).Join(" ");
						Logger.Warn($"中文搜索关键词（CurseForge）：{CS_8__locals167._VB_Local_CurseForgeAltSearchText}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					}
				}
				if (CS_8__locals167._VB_Local_Request.Sources.HasFlagF(ResourcePlatforms.Modrinth))
				{
					if (_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init == null)
					{
						Interlocked.CompareExchange(ref _STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken2 = false;
					try
					{
						Monitor.Enter(_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init, ref lockTaken2);
						if (_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init.State == 0)
						{
							_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init.State = 2;
							_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries = obj2(ResourcePlatforms.Modrinth).ToList();
						}
						else if (_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init.State = 1;
						if (lockTaken2)
						{
							Monitor.Exit(_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries_Init);
						}
					}
					List<ModBase.SearchEntry<WikiEntry>> list = ModBase.Search(_STATIC_Run_011151286AC21283B48_ModrinthSearchEntries, CS_8__locals167._VB_Local_RawSearchText, 100, 0.25);
					if (list.Any())
					{
						Dictionary<string, double> dictionary = new Dictionary<string, double>();
						foreach (ModBase.SearchEntry<WikiEntry> item2 in list)
						{
							foreach (string item3 in obj(item2, ResourcePlatforms.Modrinth))
							{
								if (!dictionary.ContainsKey(item3))
								{
									dictionary.Add(item3, 0.0);
								}
								double num = (item2.SearchSource.Any([SpecialName] (ModBase.SearchSource s) => s.Aliases.Contains(CS_8__locals167._VB_Local_RawSearchText)) ? 1000.0 : item2.Similarity);
								dictionary[item3] += num * (double)item2.Item.Popularity;
							}
						}
						CS_8__locals167._VB_Local_ModrinthAltSearchText = dictionary.MaxBy([SpecialName] (KeyValuePair<string, double> w) => w.Value).Key;
						Logger.Warn($"中文搜索关键词（Modrinth）：{CS_8__locals167._VB_Local_ModrinthAltSearchText}", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						CS_8__locals167._VB_Local_ModrinthSlugs = (from r in list.Take(100)
							select r.Item.Slugs[ResourcePlatforms.Modrinth]).ToList();
					}
				}
				if (string.IsNullOrEmpty(CS_8__locals167._VB_Local_CurseForgeAltSearchText) && string.IsNullOrEmpty(CS_8__locals167._VB_Local_ModrinthAltSearchText) && !CS_8__locals167._VB_Local_ModrinthSlugs.Any())
				{
					throw new Exception("无搜索结果，请尝试搜索其英文名称");
				}
			}
			CS_8__locals167._VB_Local_Task.Progress = 0.05;
			CS_8__locals167._VB_Local_RealResults = new List<ResourceProject>();
			checked
			{
				while (true)
				{
					List<ResourceProject> source2 = new List<ResourceProject>();
					List<Thread> list2 = new List<Thread>();
					CS_8__locals167._VB_Local_Errors = new ConcurrentBag<(Exception, ResourcePlatforms)>();
					CS_8__locals167._VB_Local_IgnoreModLoaderFilter = CS_8__locals167._VB_Local_Request.ModLoaders == ModLoaders.Forge && ModMinecraft.McVersion.VersionToDrop(CS_8__locals167._VB_Local_Request.GameVersion) < 140;
					CS_8__locals167._VB_Local_SearchResults = new ConcurrentBag<ResourceProject>();
					try
					{
						if (CS_8__locals167._VB_Local_Request.Sources.HasFlagF(ResourcePlatforms.CurseForge) && (CS_8__locals167._VB_Local_Storage.CurseForgeTotal <= -1 || CS_8__locals167._VB_Local_Storage.CurseForgeTotal > CS_8__locals167._VB_Local_Storage.CurseForgeOffset) && (!flag || (flag && !string.IsNullOrEmpty(CS_8__locals167._VB_Local_CurseForgeAltSearchText))))
						{
							list2.Add(ModBase.RunInNewThread([SpecialName] () =>
							{
								try
								{
									string curseForgeAddress = CS_8__locals167._VB_Local_Request.GetCurseForgeAddress(CS_8__locals167._VB_Local_CurseForgeAltSearchText ?? CS_8__locals167._VB_Local_RawSearchText, CS_8__locals167._VB_Local_IgnoreModLoaderFilter);
									Logger.Info($"开始 CurseForge 搜索：{curseForgeAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									JObject jObject = (JObject)ModDownload.DlModRequest(curseForgeAddress);
									List<ResourceProject> list5 = new List<ResourceProject>();
									foreach (JObject item4 in (IEnumerable<JToken>)jObject["data"])
									{
										ResourceProject resourceProject = new ResourceProject(item4);
										if ((CS_8__locals167._VB_Local_Request.Type != ResourceTypes.ResourcePack || !resourceProject.Tags.Contains("数据包")) && (CS_8__locals167._VB_Local_Request.Type == ResourceTypes.Any || resourceProject.Types.HasFlagF(CS_8__locals167._VB_Local_Request.Type)))
										{
											list5.Add(resourceProject);
										}
									}
									list5.ForEach([SpecialName] (ResourceProject p) =>
									{
										CS_8__locals167._VB_Local_SearchResults.Add(p);
									});
									CS_8__locals167._VB_Local_Storage.CurseForgeOffset += jObject["data"].Count();
									CS_8__locals167._VB_Local_Storage.CurseForgeTotal = jObject["pagination"]["totalCount"].ToObject<int>();
									Logger.Info($"从 CurseForge 搜索到了 {list5.Count} 个工程（总计已获取 {CS_8__locals167._VB_Local_Storage.CurseForgeOffset} 个，共 {CS_8__locals167._VB_Local_Storage.CurseForgeTotal} 个）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								}
								catch (Exception ex)
								{
									ProjectData.SetProjectError(ex);
									Exception ex2 = ex;
									Logger.Warn(ex2, "CurseForge 搜索失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									CS_8__locals167._VB_Local_Storage.CurseForgeTotal = -1;
									CS_8__locals167._VB_Local_Errors.Add((ex2, ResourcePlatforms.CurseForge));
									ProjectData.ClearProjectError();
								}
								if (CS_8__locals167._VB_Local_Task.Progress < 0.75)
								{
									CS_8__locals167._VB_Local_Task.Progress += 0.25;
								}
							}, "CurseForge Search"));
						}
						if (CS_8__locals167._VB_Local_Request.Sources.HasFlagF(ResourcePlatforms.Modrinth) && (CS_8__locals167._VB_Local_Storage.ModrinthTotal <= -1 || CS_8__locals167._VB_Local_Storage.ModrinthTotal > CS_8__locals167._VB_Local_Storage.ModrinthOffset) && (!flag || (flag && !string.IsNullOrEmpty(CS_8__locals167._VB_Local_ModrinthAltSearchText))))
						{
							list2.Add(ModBase.RunInNewThread([SpecialName] () =>
							{
								try
								{
									string modrinthAddress = CS_8__locals167._VB_Local_Request.GetModrinthAddress(CS_8__locals167._VB_Local_ModrinthAltSearchText ?? CS_8__locals167._VB_Local_RawSearchText, CS_8__locals167._VB_Local_IgnoreModLoaderFilter);
									Logger.Info($"开始 Modrinth 搜索：{modrinthAddress}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									JObject jObject = (JObject)ModDownload.DlModRequest(modrinthAddress);
									List<ResourceProject> list5 = new List<ResourceProject>();
									foreach (JObject item5 in (IEnumerable<JToken>)jObject["hits"])
									{
										list5.Add(new ResourceProject(item5));
									}
									list5.ForEach([SpecialName] (ResourceProject p) =>
									{
										CS_8__locals167._VB_Local_SearchResults.Add(p);
									});
									CS_8__locals167._VB_Local_Storage.ModrinthOffset += jObject["hits"].Count();
									CS_8__locals167._VB_Local_Storage.ModrinthTotal = jObject["total_hits"].ToObject<int>();
									Logger.Info($"从 Modrinth 搜索到了 {list5.Count} 个工程（总计已获取 {CS_8__locals167._VB_Local_Storage.ModrinthOffset} 个，共 {CS_8__locals167._VB_Local_Storage.ModrinthTotal} 个）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								}
								catch (Exception ex)
								{
									ProjectData.SetProjectError(ex);
									Exception ex2 = ex;
									Logger.Warn(ex2, "Modrinth 搜索失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									CS_8__locals167._VB_Local_Storage.ModrinthTotal = -1;
									CS_8__locals167._VB_Local_Errors.Add((ex2, ResourcePlatforms.Modrinth));
									ProjectData.ClearProjectError();
								}
								if (CS_8__locals167._VB_Local_Task.Progress < 0.75)
								{
									CS_8__locals167._VB_Local_Task.Progress += 0.25;
								}
							}, "Modrinth Search"));
						}
						if (CS_8__locals167._VB_Local_Request.Sources.HasFlagF(ResourcePlatforms.Modrinth) && (CS_8__locals167._VB_Local_Storage.ModrinthTotal <= -1 || CS_8__locals167._VB_Local_Storage.ModrinthTotal > CS_8__locals167._VB_Local_Storage.ModrinthOffset) && CS_8__locals167._VB_Local_ModrinthSlugs.Any())
						{
							list2.Add(ModBase.RunInNewThread([SpecialName] () =>
							{
								try
								{
									string text = string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", CS_8__locals167._VB_Local_ModrinthSlugs.Join("\",\""));
									Logger.Info($"开始 Modrinth 直接获取：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									List<ResourceProject> list5 = new List<ResourceProject>();
									foreach (JObject item6 in (IEnumerable<JToken>)ModDownload.DlModRequest(text))
									{
										ResourceProject resourceProject = new ResourceProject(item6);
										if ((CS_8__locals167._VB_Local_Request.Type == ResourceTypes.Any || resourceProject.Types.HasFlagF(CS_8__locals167._VB_Local_Request.Type)) && (string.IsNullOrEmpty(CS_8__locals167._VB_Local_Request.Tag) || item6["categories"].Any([SpecialName] (JToken c) => Operators.CompareString(c.ToString(), CS_8__locals167._VB_Local_Request.Tag.AfterLast("/"), TextCompare: false) == 0)) && (CS_8__locals167._VB_Local_Request.ModLoaders == ModLoaders.None || CS_8__locals167._VB_Local_IgnoreModLoaderFilter || resourceProject.ModLoaders.Flags().Intersect(CS_8__locals167._VB_Local_Request.ModLoaders.Flags()).Any()) && (string.IsNullOrEmpty(CS_8__locals167._VB_Local_Request.GameVersion) || resourceProject.UnsafeGameVersions.Any([SpecialName] (string d) => Operators.CompareString(d, CS_8__locals167._VB_Local_Request.GameVersion, TextCompare: false) == 0)))
										{
											list5.Add(resourceProject);
										}
									}
									list5.ForEach([SpecialName] (ResourceProject p) =>
									{
										CS_8__locals167._VB_Local_SearchResults.Add(p);
									});
									Logger.Info($"从 Modrinth 直接获取到了 {list5.Count} 个工程", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									CS_8__locals167._VB_Local_ModrinthSlugs.Clear();
								}
								catch (Exception ex)
								{
									ProjectData.SetProjectError(ex);
									Exception ex2 = ex;
									Logger.Warn(ex2, "Modrinth 直接获取失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
									CS_8__locals167._VB_Local_Errors.Add((ex2, ResourcePlatforms.Modrinth));
									ProjectData.ClearProjectError();
								}
								if (CS_8__locals167._VB_Local_Task.Progress < 0.75)
								{
									CS_8__locals167._VB_Local_Task.Progress += 0.25;
								}
							}, "Modrinth Get"));
						}
						foreach (Thread item7 in list2)
						{
							item7.Join();
							if (CS_8__locals167._VB_Local_Task.IsCanceled)
							{
								return;
							}
						}
						source2 = new List<ResourceProject>(CS_8__locals167._VB_Local_SearchResults);
						if (CS_8__locals167._VB_Local_IgnoreModLoaderFilter)
						{
							source2.KeepIf([SpecialName] (ResourceProject p) => p.ModLoaders == ModLoaders.None || p.ModLoaders.HasFlagF(ModLoaders.Forge));
						}
						CS_8__locals167._VB_Local_Storage.ErrorMessage = null;
						if (!source2.Any())
						{
							if (CS_8__locals167._VB_Local_Errors.Any())
							{
								throw CS_8__locals167._VB_Local_Errors.First().Ex;
							}
							if (flag && CS_8__locals167._VB_Local_Request.Type != ResourceTypes.Mod && CS_8__locals167._VB_Local_Request.Type != ResourceTypes.DataPack)
							{
								throw new Exception("无搜索结果，请尝试搜索其英文名称");
							}
							if (CS_8__locals167._VB_Local_Request.Sources == ResourcePlatforms.CurseForge && CS_8__locals167._VB_Local_Request.Tag.StartsWithF("/"))
							{
								throw new Exception("CurseForge 不兼容所选的类型");
							}
							if (CS_8__locals167._VB_Local_Request.Sources == ResourcePlatforms.Modrinth && CS_8__locals167._VB_Local_Request.Tag.EndsWithF("/"))
							{
								throw new Exception("Modrinth 不兼容所选的类型");
							}
							throw new Exception("没有搜索结果");
						}
						if (CS_8__locals167._VB_Local_Errors.Any())
						{
							if (CS_8__locals167._VB_Local_Errors.Any([SpecialName] ((Exception Ex, ResourcePlatforms Source) e) => e.Source == ResourcePlatforms.CurseForge))
							{
								CS_8__locals167._VB_Local_Storage.ErrorMessage = string.Format("无法连接到 CurseForge，所以目前仅显示了来自 Modrinth 的内容，搜索结果可能不全。{0}请稍后再试，或使用 VPN 改善网络环境。", "\r\n");
							}
							else
							{
								CS_8__locals167._VB_Local_Storage.ErrorMessage = string.Format("无法连接到 Modrinth，所以目前仅显示了来自 CurseForge 的内容，搜索结果可能不全。{0}请稍后再试，或使用 VPN 改善网络环境。", "\r\n");
							}
						}
					}
					finally
					{
						foreach (Thread item8 in list2)
						{
							if (item8.IsAlive)
							{
								item8.Interrupt();
							}
						}
					}
					source2 = source2.Where([SpecialName] (ResourceProject x) => x.Platform == ResourcePlatforms.CurseForge).Concat(source2.Where([SpecialName] (ResourceProject x) => x.Platform != ResourcePlatforms.CurseForge)).ToList();
					source2 = source2.Distinct([SpecialName] (ResourceProject a, ResourceProject b) => a.IsLike(b)).ToList();
					source2 = source2.Where([SpecialName] (ResourceProject r) =>
					{
						_Closure___6_4 arg2 = default;
						_Closure___6_4 CS_8__locals169 = new _Closure___6_4(arg2);
						CS_8__locals169._VB_Local_r = r;
						return !CS_8__locals167._VB_Local_RealResults.Any([SpecialName] (ResourceProject b) => CS_8__locals169._VB_Local_r.IsLike(b)) && !CS_8__locals167._VB_Local_Storage.Results.Any([SpecialName] (ResourceProject b) => CS_8__locals169._VB_Local_r.IsLike(b));
					}).ToList();
					CS_8__locals167._VB_Local_RealResults.AddRange(source2);
					Logger.Info($"去重、筛选后累计新增结果 {CS_8__locals167._VB_Local_RealResults.Count} 个（目前已有结果 {CS_8__locals167._VB_Local_Storage.Results.Count} 个）", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					if (CS_8__locals167._VB_Local_RealResults.Count + CS_8__locals167._VB_Local_Storage.Results.Count >= CS_8__locals167._VB_Local_Request.TargetResultCount)
					{
						break;
					}
					Logger.Info($"总结果数需求最少 {CS_8__locals167._VB_Local_Request.TargetResultCount} 个，仅获得了 {CS_8__locals167._VB_Local_RealResults.Count + CS_8__locals167._VB_Local_Storage.Results.Count} 个", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					if (CS_8__locals167._VB_Local_Request.CanContinue && !CS_8__locals167._VB_Local_Errors.Any())
					{
						Logger.Info("将继续尝试加载下一页", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
						continue;
					}
					Logger.Info("无法继续加载，将强制结束", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					break;
				}
				Dictionary<ResourceProject, double> dictionary2 = new Dictionary<ResourceProject, double>();
				VB_AnonymousDelegate_3<ResourceProject, double> obj3 = [SpecialName] (ResourceProject Project) =>
				{
					switch (CS_8__locals167._VB_Local_Request.Type)
					{
					case ResourceTypes.Mod:
					case ResourceTypes.ModPack:
						return (Project.Platform == ResourcePlatforms.CurseForge) ? 1 : 5;
					case ResourceTypes.DataPack:
						return (Project.Platform != ResourcePlatforms.CurseForge) ? 1 : 10;
					case ResourceTypes.ResourcePack:
					case ResourceTypes.Shader:
						return (Project.Platform == ResourcePlatforms.CurseForge) ? 1 : 4;
					default:
						return 1.0;
					}
				};
				if (string.IsNullOrEmpty(CS_8__locals167._VB_Local_RawSearchText))
				{
					foreach (ResourceProject item9 in CS_8__locals167._VB_Local_RealResults)
					{
						dictionary2[item9] = (double)item9.DownloadCount * obj3(item9);
					}
				}
				else
				{
					List<ModBase.SearchEntry<ResourceProject>> list3 = new List<ModBase.SearchEntry<ResourceProject>>();
					foreach (ResourceProject item10 in CS_8__locals167._VB_Local_RealResults)
					{
						dictionary2[item10] = ((item10.WikiId > 0) ? 0.2 : 0.0) + Math.Log10((double)Math.Max(item10.DownloadCount, 1) * obj3(item10)) / 9.0;
						list3.Add(new ModBase.SearchEntry<ResourceProject>
						{
							Item = item10,
							SearchSource = new List<ModBase.SearchSource>
							{
								new ModBase.SearchSource((flag ? item10.TranslatedName : item10.RawName).Split('/', removeEmptyEntries: true)),
								new ModBase.SearchSource(item10.Description, 0.05)
							}
						});
					}
					List<ModBase.SearchEntry<ResourceProject>> list4 = ModBase.Search(list3, CS_8__locals167._VB_Local_RawSearchText, 10000, -1.0);
					foreach (ModBase.SearchEntry<ResourceProject> item11 in list4)
					{
						dictionary2[item11.Item] += (item11.AbsoluteRight ? 10.0 : item11.Similarity) / (list4.First().AbsoluteRight ? 10.0 : list4.First().Similarity);
					}
				}
				if (CS_8__locals167._VB_Local_Task.IsCanceled)
				{
					throw new OperationCanceledException();
				}
				CS_8__locals167._VB_Local_Storage.Results.AddRange(from r in dictionary2
					orderby r.Value descending
					select r.Key);
			}
		}
	}
}
