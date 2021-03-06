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
        public static int GetUnitMaxAmount(CombatAffiliation affiliation, UnitAttackType type)
        {
            var armyInfo = BattleMapGeneratorManager.GameManager.UnitGenerator.StartArmyList.First(info => info.Affiliation==affiliation);
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
        public static int GetOwnerMaxUnits(CombatAffiliation owner)
        {
            var armyInfo = BattleMapGeneratorManager.GameManager.UnitGenerator.StartArmyList.First(info => info.Affiliation==owner);
            return armyInfo.TotalAmount;
        }

        /// <summary>
        /// Returns army infro from the chosen faction
        /// </summary>
        /// <param name="faction">Chosen faction</param>
        /// <returns>ArmyInfo</returns>
        public static ArmyInfo GetArmyInfo(CombatAffiliation affiliation)
        {
            return BattleMapGeneratorManager.GameManager.UnitGenerator.StartArmyList.First(info => info.Affiliation==affiliation);
        }

        public static void RemoveUnit(CombatAffiliation controller, UnitAttackType type, int amount)
        {
            
            var armyInfo = BattleMapGeneratorManager.GameManager.UnitGenerator.StartArmyList.First(info => info.Affiliation==controller);
            switch (type)
            {
               case UnitAttackType.Melee:
                   armyInfo.TroopAmount -= amount;
                   break;
               case UnitAttackType.Range:
                   armyInfo.ArcherAmount -= amount;
                   break;
               case UnitAttackType.Flying:
                   armyInfo.FlyingAmount -= amount;
                   break;
            }

            armyInfo.TotalAmount -= amount;
            if (armyInfo.TotalAmount == 0)
            {
                BattleMapGeneratorManager.GameManager.EndGame();
            }
        }
    }
}