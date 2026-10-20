// PCLCS

namespace PCLCS
{
	[Flags]
	public enum ModLoaders
	{
		None = 0,
		Forge = 1,
		LiteLoader = 2,
		Fabric = 4,
		NeoForge = 0x10,
		All = Forge | LiteLoader | Fabric | NeoForge
	}
}
