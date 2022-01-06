﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Battle.Map;
using Helper;
using UI;
using Unity.VisualScripting;
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
        public UnitStats TerrainBuff;
        public UnitStatFluctuation StatFluctuation;

        public CombatUnit NearestEnemy;
        
        //Experimental multithread
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            Setup();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            SetUnitColor();
            Life();
        }

        private async Task Life()
        {
            await FirstMove();
            while (CurrentStats.HP>0)
            {
               await GetNearestEnemyInRange();
               await Attack();
            }
        }

        async Task GetNearestEnemyInRange()
        {
            //get list of enemies in range and calc their distance
            var enemiesDistances = new List<Tuple<CombatUnit, float>>();
            foreach (var combatUnit in RangedRange.UnitsInRange)
            {
                if (combatUnit.Controller != Controller)
                {
                    var distance = Vector3.Distance(combatUnit.transform.position, transform.position);
                    enemiesDistances.Add(new Tuple<CombatUnit, float>(combatUnit,distance));
                }
            }
            //pick the nearest enemy if there is one
            NearestEnemy = enemiesDistances.OrderBy(enemy => enemy.Item2).LastOrDefault()?.Item1;
            
            //if we are ranged then we have to run from the nearest enemy
            if (AttackType == UnitAttackType.Range && NearestEnemy!=null) FleeFrom(NearestEnemy);
            
            await Task.Delay(500);
        }
        private void SetUnitColor()
        {
            var unitColor = Controller switch
            {
                CombatAffiliation.Player => Color.blue,
                CombatAffiliation.AI => Color.red,
                CombatAffiliation.Neutral => Color.gray,
            };
            Renderer.material.color=unitColor;
        }

        public void FleeFrom(CombatUnit enemy)
        {
            Vector3 enemyDirection = transform.position - enemy.transform.position;
            Vector3 newPos = transform.position + enemyDirection;
            MoveTo(newPos);
        }

        public async Task FirstMove()
        {
            var firstMoveDirection = Controller switch
            {
                CombatAffiliation.Player => BoundsHelper.GetAIBounds(),
                CombatAffiliation.AI => BoundsHelper.GetPlayerBounds()
            };

            var direction = firstMoveDirection.center;
            await Task.Delay(1000);
            MoveTo(direction);
        }

        private async Task Attack()
        {
            if (NearestEnemy == null) return;
            MoveTo(NearestEnemy.transform.position);
            await DealDamage(NearestEnemy);
            await Task.Delay((int) CurrentStats.AttackCooldown * 1000);
        }

        private async Task DealDamage(CombatUnit target)
        {
            //if target is inside meele damage do only meele damage
            if (MeleeRange.UnitsInRange.Contains(target) && CanAttackMelee)
            {
                await target.TakeDamageFrom(this,CurrentStats.MeleeDamage);
                return;
            }

            //if target is ranged attack range then attack
            if (RangedRange.UnitsInRange.Contains(target) && CanAttackRanged)
            {
                await target.TakeDamageFrom(this,CurrentStats.RangedDamage);
            }
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }

        private async Task Die()
        {
            print($"{gameObject.name} has died");
            await Task.Delay(5);
            Despawn();
        }

        private async Task Move()
        {
            if (NearestEnemy != null)
            {
                await Attack();
            }
        }
        private async Task TakeDamageFrom(CombatUnit enemy,float amount)
        {
            float resist = enemy.AttackType switch
            {
                UnitAttackType.Melee => CurrentStats.MeleeDamageResist,
                UnitAttackType.Range => CurrentStats.RangedDamageResist,
                UnitAttackType.Flying => CurrentStats.MagicResist,
                _ => throw new ArgumentOutOfRangeException()
            };

            var dmgTaken = Mathf.Max((amount-resist),0);
            CurrentStats.HP -= dmgTaken;
            print($"{gameObject.name} has taken: {dmgTaken} Damage!");
            if(CurrentStats.HP<0) Die();
        }

        private void MoveTo(Vector3 position)
        {
            NavMeshAgent.destination = position;
        }

        public void ChangeFactionTo(UnitFaction faction)
        {
            this.Faction = faction;
        }

        public void SetOwner(CombatAffiliation owner)
        {
            this.Owner = owner;
        }

        public void SetController(CombatAffiliation controller)
        {
            this.Controller = controller;
        }

        private void Setup()
        {
            SetUnitColor();
            SetupStats();
        }

        private void SetupStats()
        {
            CurrentStats = BaseStats.ShallowCopy();
            ApplyTerrainBuff();
            ApplyFluctuation();
        }

        private void ApplyTerrainBuff()
        {
            if (TerrainBuff == null) return;
            CurrentStats.Add(TerrainBuff);
        }

        private void ApplyFluctuation()
        {
            CurrentStats.AttackCooldown += CurrentStats.AttackCooldown *
                                          Random.Range(-StatFluctuation.AttackSpeed, StatFluctuation.AttackSpeed);
            CurrentStats.MeleeDamage +=CurrentStats.MeleeDamage *
                                       Random.Range(-StatFluctuation.MeeleeDamage, StatFluctuation.MeeleeDamage);
            CurrentStats.RangedDamage += CurrentStats.RangedDamage *
                                        Random.Range(-StatFluctuation.RangedDamage, StatFluctuation.RangedDamage);
            CurrentStats.HP += CurrentStats.HP * Random.Range(-StatFluctuation.HP, StatFluctuation.HP);
            CurrentStats.MeleeDamageResist += CurrentStats.MeleeDamageResist *
                                             Random.Range(-StatFluctuation.MeleeDamageResist,
                                                 StatFluctuation.MeleeDamageResist);
            CurrentStats.RangedDamageResist += CurrentStats.RangedDamageResist *
                                               Random.Range(-StatFluctuation.RangedDamageResist,
                                                   StatFluctuation.RangedDamageResist);
            CurrentStats.MagicResist += CurrentStats.MagicResist *
                                       Random.Range(-StatFluctuation.MagicResist, StatFluctuation.MagicResist);
            CurrentStats.MovementSpeed += CurrentStats.MovementSpeed *
                                         Random.Range(-StatFluctuation.Speed, StatFluctuation.Speed);
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
