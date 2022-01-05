using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;

namespace Battle.Unit
{
    [Serializable]
    public class UnitStats
    {
        public float AttackCooldown;
        public float MeleeDamage;
        public float RangedDamage;
        public float MovementSpeed;
        public float HP;
        public float MeleeDamageResist;
        public float RangedDamageResist;
        public float MagicResist;

        public UnitStats ShallowCopy()
        {
            return (UnitStats) MemberwiseClone();
        }

        public void Add(UnitStats variable)
        {
           //todo: zrobic to automatycznie
        }
    }
}