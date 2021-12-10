using System;
using System.Linq;
using Battle.Unit;
using DefaultNamespace;

namespace Helper
{
    /// <summary>
    /// Helper to get info about available armies
    /// </summary>
    public static class ArmyHelper
    {
        /// <summary>
        /// Returns the max unit amount of chosen faction and type
        /// </summary>
        /// <param name="faction"> Chosen faction</param>
        /// <param name="type"> Chosen unit attack type</param>
        /// <returns>int</returns>
        public static int GetUnitMaxAmount(UnitFaction faction, UnitAttackType type)
        {
            var armyInfo = BattleMapGeneratorManager.GameManager.UnitGenerator.ArmyList.First(info => info.Faction==faction);
            var value = type switch
            {
                UnitAttackType.Melee => armyInfo.TroopAmount,
                UnitAttackType.Range => armyInfo.ArcherAmount,
                UnitAttackType.Flying => armyInfo.FlyingAmount,
                _ => throw new Exception("Wrong type")
            };
            
            return value;
        }

        /// <summary>
        /// Returns the factions unit cap
        /// </summary>
        /// <param name="faction">Chosen faction</param>
        /// <returns>int</returns>
        public static int GetFactionMaxUnits(UnitFaction faction)
        {
            var armyInfo = BattleMapGeneratorManager.GameManager.UnitGenerator.ArmyList.First(info => info.Faction==faction);
            return armyInfo.TotalAmount;
        }

        /// <summary>
        /// Returns army infro from the chosen faction
        /// </summary>
        /// <param name="faction">Chosen faction</param>
        /// <returns>ArmyInfo</returns>
        public static ArmyInfo GetArmyInfo(UnitFaction faction)
        {
            return BattleMapGeneratorManager.GameManager.UnitGenerator.ArmyList.First(info => info.Faction==faction);
        }
    }
}