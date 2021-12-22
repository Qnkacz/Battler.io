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
    public class CombatUnit : MonoBehaviour
    {
        public string Name;
        public CombatAffiliation Owner;
        public CombatAffiliation Controller;
        public UnitType UnitType;
        public UnitAttackType AttackType;
        public UnitFaction Faction;
        public UnitMovementType MovementType;
        public Image Portrait;
        public CombatAffiliation Allies;
        public CombatAffiliation Enemies;
        public bool CanAttackMelee;
        public bool CanAttackRanged;
        public AttackRange MeleeRange;
        public AttackRange RangedRange;
        public NavMeshAgent NavMeshAgent;
        public Material Material;
        public Renderer Renderer;
        public bool IsVisible;
        public UnitStats Stats;

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
            Renderer.material.color=unitColor;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void DealDamage(CombatUnit target)
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

        public void TakeDamageFrom(CombatUnit enemy)
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

        public void SetOwner(CombatAffiliation owner)
        {
            throw new NotImplementedException();
        }

        public void SetController(CombatAffiliation controller)
        {
            throw new NotImplementedException();
        }
    }
}
