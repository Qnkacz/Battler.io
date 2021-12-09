using UnityEngine;

namespace Battle.Map.Helper
{
    public static class BoundsHelper
    {
        public static Bounds GetHumanBounds()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.PlayerUnitPlacementBounds;
        }
        public static Bounds GetUndeadBounds()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.AIUnitPlacementBounds;
        }
    }
}