using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Battle.Unit
{
    public class CombatUnit : MonoBehaviour, IUnit
    {
        [Header("Unit Info")]
        public string Name;
        public CombatAffiliation Owner;
        public UnitType UnitType;
        public UnitAttackType AttackType;
        public UnitFaction Faction;
        public UnitMovementType MovementType;
        public Image Portrait;
        [Header("Combat Information")]
        public CombatAffiliation Allies;
        public CombatAffiliation Enemies;
        public bool CanAttackMelee;
        public bool CanAttackRanged;
        public float AttackCooldown;
        public float MeleeDamage;
        public float RangedDamage;
        public AttackRange MeleeRange;
        public AttackRange RangedRange;
        [Header("Movement")]
        public float MovementSpeed;
        public NavMeshAgent NavMeshAgent;
        [Header("Vitality")]
        public float HP;
        public float DamageResist;
        public float MagicResist;
        [Header("Visibility")] 
        public Material Material;
        public Renderer Renderer;
        public bool IsVisible;

        private void Awake()
        {
            Setup();
        }

        private void SetUnitColor()
        {
            var unitColor = Faction switch
            {
                UnitFaction.Human=>Color.blue,
                UnitFaction.Undead=>Color.red,
                _ => throw new ArgumentOutOfRangeException()
            };
            print(unitColor);
            Renderer.material.color=unitColor;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void DealDamage(IUnit target)
        {
            throw new NotImplementedException();
        }

        public void Despawn()
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public void TakeDamageFrom(IUnit enemy)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(Vector3 postiotion)
        {
            throw new NotImplementedException();
        }

        public void Setup()
        {
            SetUnitColor();
        }

        public void ChangeFactionTo(UnitFaction faction)
        {
            throw new NotImplementedException();
        }
    }
}
