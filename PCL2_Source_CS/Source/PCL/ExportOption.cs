// PCL

namespace PCL
{
	public class ExportOption
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Rules { get; set; }
		public string ShowRules { get; set; }
		public bool DefaultChecked { get; set; }
		public bool RequireModLoader { get; set; }
		public bool RequireOptiFine { get; set; }
		public bool RequireModLoaderOrOptiFine { get; set; }
		public ExportOption()
		{
			RequireModLoader = false;
			RequireOptiFine = false;
			RequireModLoaderOrOptiFine = false;
		}
	}
}
