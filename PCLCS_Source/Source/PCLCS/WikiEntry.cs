// PCLCS

namespace PCLCS
{
	public class WikiEntry
	{
		public int Id;
		public string? ChineseName;
		public Dictionary<ResourcePlatforms, string> Slugs = new Dictionary<ResourcePlatforms, string>();
		public int Popularity;
		public static readonly Lazy<List<WikiEntry>> All = new Lazy<List<WikiEntry>>(() =>
		{
			List<string> list = FileUtils.ReadAsLines("Resource\\WikiEntries.txt", skipEmptyLines: false, null, typeof(WikiEntry)).ToList();
			Queue<int> queue = new Queue<int>();
			for (int i = 0; i < list[list.Count - 1].Length; i += 3)
			{
				queue.Enqueue(int.Parse(list[list.Count - 1].Substring(i, 3).ConvertRadix(86, 10)));
			}
			list.RemoveAt(list.Count - 1);
			List<WikiEntry> list2 = new List<WikiEntry>();
			int num = 0;
			foreach (string item in list)
			{
				num++;
				if (!(item == ""))
				{
					int popularity = queue.Dequeue();
					string[] array = item.Split('\u00a8');
					foreach (string obj in array)
					{
						WikiEntry wikiEntry = new WikiEntry();
						string[] array2 = obj.Split('|');
						string text = array2[0];
						if (text.StartsWithF("@"))
						{
							wikiEntry.Slugs.Add(ResourcePlatforms.Modrinth, text.Replace("@", ""));
						}
						else if (text.EndsWithF("@"))
						{
							wikiEntry.Slugs.Add(ResourcePlatforms.CurseForge, text.TrimEnd('@'));
							wikiEntry.Slugs.Add(ResourcePlatforms.Modrinth, wikiEntry.Slugs[ResourcePlatforms.CurseForge]);
						}
						else if (text.Contains("@"))
						{
							string[] array3 = text.Split('@');
							wikiEntry.Slugs.Add(ResourcePlatforms.CurseForge, array3[0]);
							wikiEntry.Slugs.Add(ResourcePlatforms.Modrinth, array3[1]);
						}
						else
						{
							wikiEntry.Slugs.Add(ResourcePlatforms.CurseForge, text);
						}
						wikiEntry.Id = num;
						wikiEntry.Popularity = popularity;
						if (array2.Length >= 2)
						{
							wikiEntry.ChineseName = array2[^1];
							if (wikiEntry.ChineseName.Contains("*"))
							{
								string text2 = wikiEntry.Slugs.Values.First().Replace("-", " ").Capitalize();
								wikiEntry.ChineseName = wikiEntry.ChineseName.Replace("*", " (" + text2 + ")");
							}
						}
						list2.Add(wikiEntry);
					}
				}
			}
			return list2;
		});
		public override string ToString()
		{
			return $"{Slugs.GetOrDefault(ResourcePlatforms.CurseForge)}&{Slugs.GetOrDefault(ResourcePlatforms.Modrinth)}|{Id}|{ChineseName}，浏览量 {Popularity}";
		}
	}
}
