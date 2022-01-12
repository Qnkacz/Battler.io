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
        public float AmmoCapacity;
        public float CurrAmmoAmount;

        public UnitStats ShallowCopy()
        {
            return (UnitStats) MemberwiseClone();
        }

        public void Add(UnitStats variable)
        {
            var myFields = GetType().GetFields();
            foreach (var field in myFields)
            {
                float thisValue = (float) field.GetValue(this);
                float otherValue = (float) field.GetValue(variable);

                float newValue = thisValue + otherValue;

                field.SetValue(this, newValue);
            }
        }
    }
}