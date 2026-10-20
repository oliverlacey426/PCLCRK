// PCLCS

namespace PCLCS
{
	[Flags]
	public enum ResourceTypes
	{
		Mod = 1,
		ModPack = 2,
		ResourcePack = 4,
		Shader = 8,
		DataPack = 0x10,
		Plugin = 0x20,
		Map = 0x40,
		ModOrDataPack = Mod | DataPack,
		Any = ModOrDataPack | ModPack | ResourcePack | Shader | Plugin | Map
	}
}
