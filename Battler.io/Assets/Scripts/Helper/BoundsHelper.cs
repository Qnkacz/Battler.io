using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Helper To get info about in map bounds
    /// </summary>
    public static class BoundsHelper
    {
        /// <summary>
        /// Gets Human placement bounds
        /// </summary>
        /// <returns></returns>
        public static Bounds GetHumanBounds()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.PlayerUnitPlacementBounds;
        }
        /// <summary>
        /// Gets undead placement bounds
        /// </summary>
        /// <returns></returns>
        public static Bounds GetUndeadBounds()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.AIUnitPlacementBounds;
        }

        /// <summary>
        /// Gets obstacle bounds
        /// </summary>
        /// <returns></returns>
        public static Bounds GetObstacleBounds()
        {
            return BattleMapGeneratorManager.GameManager.BattleMap.ObstacleSpawnBounds;
        }
    }
}