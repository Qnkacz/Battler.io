using System;
using System.Collections;
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
using UnityEditor;
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
        public Spawner NearestEnemySpawner;

        private void Awake()
        {
            Setup();
        }

        private void Start()
        {
            SetUnitColor();
            Life();
        }

        private void Life()
        { 
            FirstMove();
            StartCoroutine(GetNearestEnemyInRange());
            StartCoroutine(GetNearestEnemySpawner());
            StartCoroutine(Move());
            StartCoroutine(Attack());
        }

        IEnumerator GetNearestEnemyInRange()
        {
            while (true)
            {
                //remove missing obj in list
                RangedRange.UnitsInRange = RangedRange.UnitsInRange.Where(item => item != null).ToList();
                
                //get list of enemies in range and calc their distance
                if (RangedRange.UnitsInRange.Count == 0)
                {
                    NearestEnemy = null;
                }

                var enemiesDistances = new List<Tuple<CombatUnit, float>>();
                foreach (var combatUnit in RangedRange.UnitsInRange)
                {
                    if (combatUnit.Controller != Controller)
                    {
                        var distance = Vector3.Distance(combatUnit.transform.position, transform.position);
                        enemiesDistances.Add(new Tuple<CombatUnit, float>(combatUnit, distance));
                    }
                }

                //pick the nearest enemy if there is one
                NearestEnemy = enemiesDistances.OrderBy(enemy => enemy.Item2).LastOrDefault()?.Item1;
                
                //wait for x seconds
                yield return new WaitForSeconds(.1f);
            }
        }
        IEnumerator GetNearestEnemySpawner()
        {
            while (true)
            {
                //remove missing obj in list
                RangedRange.SpawnersInRange = RangedRange.SpawnersInRange.Where(item => item != null).ToList();
                
                //get list of enemies in range and calc their distance
                if (RangedRange.SpawnersInRange.Count == 0)
                {
                    NearestEnemySpawner = null;
                }

                var spawnersInDistance = new List<Tuple<Spawner, float>>();
                foreach (var spawnerUnit in RangedRange.SpawnersInRange)
                {
                    if (spawnerUnit.Controller != Controller)
                    {
                        var distance = Vector3.Distance(spawnerUnit.transform.position, transform.position);
                        spawnersInDistance.Add(new Tuple<Spawner, float>(spawnerUnit, distance));
                    }
                }

                //pick the nearest enemy if there is one
                NearestEnemySpawner = spawnersInDistance.OrderBy(enemy => enemy.Item2).LastOrDefault()?.Item1;
                
                //wait for x seconds
                yield return new WaitForSeconds(.1f);
            }
        }

        private void SetUnitColor()
        {
            var unitColor = Controller switch
            {
                CombatAffiliation.Player => Color.blue,
                CombatAffiliation.AI => Color.red,
                CombatAffiliation.Neutral => Color.gray,
            };
            Renderer.material.color = unitColor;
        }

        private void FleeFrom(CombatUnit enemy)
        {
            
            Vector3 enemyDirection = transform.position - enemy.transform.position;
            Vector3 newPos = transform.position + enemyDirection + new Vector3(RangedRange.Range*1.5f,0,RangedRange.Range*1.5f);
            MoveTo(newPos);
        }

        public void FirstMove()
        {
            NavMeshAgent.isStopped = false;
            var firstMoveDirection = Controller switch
            {
                CombatAffiliation.Player => BoundsHelper.GetAIBounds(),
                CombatAffiliation.AI => BoundsHelper.GetPlayerBounds()
            };

            var direction = firstMoveDirection.center;
            MoveTo(direction);
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                if (NearestEnemy != null) 
                    DealDamage(NearestEnemy);
                if(NearestEnemySpawner!=null)
                    DealDamage(NearestEnemySpawner);
                yield return new WaitForSeconds(CurrentStats.AttackCooldown);
            }
            
        }

        private void DealDamage(Spawner nearestEnemySpawner)
        {
            if(MeleeRange.SpawnersInRange.Contains(nearestEnemySpawner) && CanAttackMelee) nearestEnemySpawner.TakeDamage((int)CurrentStats.MeleeDamage);
            if(CanAttackRanged) nearestEnemySpawner.TakeDamage((int)CurrentStats.RangedDamage);
        }

        private void DealDamage(CombatUnit target)
        {
            //if target is inside meele damage do only meele damage
            if (MeleeRange.UnitsInRange.Contains(target) && CanAttackMelee)
            {
                target.TakeDamageFrom(this, CurrentStats.MeleeDamage);
                return;
            }

            //if target is ranged attack range then attack
            if (RangedRange.UnitsInRange.Contains(target) && CanAttackRanged)
            {
                target.TakeDamageFrom(this, CurrentStats.RangedDamage);
            }
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }

        private void Die()
        {
            print($"{gameObject.name} has died");
            Despawn();
        }

        private IEnumerator Move()
        {
            while (true)
            {
                NavMeshAgent.isStopped = false;
                if (AttackType == UnitAttackType.Melee)
                {
                    if (NearestEnemy != null)
                    {
                        MoveTo(NearestEnemy.transform.position);
                    }
                }
                else
                {
                        NavMeshAgent.isStopped = false;
                        if(NearestEnemy!=null)
                            FleeFrom(NearestEnemy);
                }
                
                if(NearestEnemySpawner!=null) MoveTo(NearestEnemySpawner.transform.position);
                
                if(NearestEnemySpawner == null && NearestEnemy == null) FirstMove();

                yield return new WaitForSeconds(.1f);
            }
            
        }

        private void TakeDamageFrom(CombatUnit enemy, float amount)
        {
            float resist = enemy.AttackType switch
            {
                UnitAttackType.Melee => CurrentStats.MeleeDamageResist,
                UnitAttackType.Range => CurrentStats.RangedDamageResist,
                UnitAttackType.Flying => CurrentStats.MagicResist,
                _ => throw new ArgumentOutOfRangeException()
            };

            var dmgTaken = Mathf.Max((amount - resist), 0);
            CurrentStats.HP -= dmgTaken;
            print($"{gameObject.name} has taken: {dmgTaken} Damage!");
            if (CurrentStats.HP < 0) Die();
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
            CurrentStats.MeleeDamage += CurrentStats.MeleeDamage *
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
    }
}