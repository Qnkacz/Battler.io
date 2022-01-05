using System.Collections.Generic;
using Battle.Map;

namespace Helper
{
    public static class BiomeHelper
    {
        public static MapBiome GetCurrentBiome()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.Biome;
        } 
    }
}