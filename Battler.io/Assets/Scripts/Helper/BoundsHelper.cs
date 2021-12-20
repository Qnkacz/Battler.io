using System.Linq;
using Battle.Unit;
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
            var bounds =
                BattleMapGeneratorManager.GameManager.BattleMap.BoundsList.First(bound =>
                    bound.Owner == CombatAffiliation.Player);
            return bounds.Bounds;
        }
        /// <summary>
        /// Gets undead placement bounds
        /// </summary>
        /// <returns></returns>
        public static Bounds GetUndeadBounds()
        {
            var bounds =
                BattleMapGeneratorManager.GameManager.BattleMap.BoundsList.First(bound =>
                    bound.Owner == CombatAffiliation.AI);
            return bounds.Bounds;
        }

        /// <summary>
        /// Gets obstacle bounds
        /// </summary>
        /// <returns></returns>
        public static Bounds GetObstacleBounds()
        {
            var bounds =
                BattleMapGeneratorManager.GameManager.BattleMap.BoundsList.First(bound =>
                    bound.Owner == CombatAffiliation.Neutral);
            return bounds.Bounds;
        }
    }
}