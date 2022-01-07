using System;
using Battle.Unit;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    [Serializable]
    public class ArmyInfo
    {
        public int TotalAmount;
        public int TroopAmount;
        public int ArcherAmount;
        public int FlyingAmount;
        public UnitFaction Faction;
        public CombatAffiliation Affiliation;

        public void Clear()
        {
            TotalAmount = TroopAmount = ArcherAmount = FlyingAmount = 0;
        }
    }
}