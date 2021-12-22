using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        public UnitStats BaseStats;
        public UnitStats CurrentStats;
        public UnitStatFluctuation StatFluctuation;

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
            SetupStats();
        }

        private void SetupStats()
        {
            CurrentStats = BaseStats.ShallowCopy();
            ApplyFluctuation();
        }

        private void ApplyFluctuation()
        {
            CurrentStats.AttackCooldown =+ CurrentStats.AttackCooldown*Random.Range(-StatFluctuation.AttackSpeed, StatFluctuation.AttackSpeed);
            CurrentStats.MeleeDamage =+ CurrentStats.MeleeDamage*Random.Range(-StatFluctuation.MeeleeDamage, StatFluctuation.MeeleeDamage);
            CurrentStats.RangedDamage =+ CurrentStats.RangedDamage *Random.Range(-StatFluctuation.RangedDamage, StatFluctuation.RangedDamage);
            CurrentStats.HP =+ CurrentStats.HP*Random.Range(-StatFluctuation.HP, StatFluctuation.HP);
            CurrentStats.DamageResist =+ CurrentStats.DamageResist*Random.Range(-StatFluctuation.DamageResist, StatFluctuation.DamageResist);
            CurrentStats.MagicResist =+CurrentStats.MagicResist* Random.Range(-StatFluctuation.MagicResist, StatFluctuation.MagicResist);
            CurrentStats.MovementSpeed =+ CurrentStats.MovementSpeed*Random.Range(-StatFluctuation.Speed, StatFluctuation.Speed);
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
