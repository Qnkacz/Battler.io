using System;

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
        public float DamageResist;
        public float MagicResist;
    }
}