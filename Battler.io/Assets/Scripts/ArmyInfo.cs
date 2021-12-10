using System;
using Battle.Unit;

namespace DefaultNamespace
{
    [Serializable]
    public struct ArmyInfo
    {
        public int TotalAmount;
        public int TroopAmount;
        public int ArcherAmount;
        public int FlyingAmount;
        public UnitFaction Faction;
    }
}