using System;
using UnityEngine;

namespace Battle.Unit
{
    [Serializable]
    public class UnitStatFluctuation
    {
        [Range(0, 1)] public float AttackSpeed;
        [Range(0, 1)] public float MeeleeDamage;
        [Range(0, 1)] public float RangedDamage;
        [Range(0, 1)] public float HP;
        [Range(0, 1)] public float DamageResist;
        [Range(0, 1)] public float MagicResist;
        [Range(0, 1)] public float Speed;
    }
}