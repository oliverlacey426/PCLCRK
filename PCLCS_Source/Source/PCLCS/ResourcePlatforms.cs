// PCLCS

namespace PCLCS
{
	[Flags]
	public enum ResourcePlatforms
	{
		CurseForge = 1,
		Modrinth = 2,
		Any = CurseForge | Modrinth
	}
}
