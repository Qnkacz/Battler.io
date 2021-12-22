using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Unit;
using Extensions;
using Helper;
using UnityEngine;

namespace Battle.Map
{
    public class SpawnerManager : MonoBehaviour
    {
        public List<Spawner> AllSpawners;
        public List<Spawner> PlayerSpawners;
        public List<Spawner> AISpawners;
        [Space(10)] 
        public SpawnerConfig SpawnerConfig;
        [Space(10)] 
        public GameObject HumanSpawnerContainer;
        public GameObject AISpawnerContainer;
        [Space(10)]
        public Spawner SpawnerPrefab;

        private void PlaceSpawner(CombatAffiliation owner)
        {
            var obj = Instantiate(SpawnerPrefab.gameObject);
            var objScript = obj.GetComponent<Spawner>();
            
            //Set Spawner Faction
            objScript.Faction = OptionsHelper.GetFactionOfPlayer(owner);
            objScript.Owner = owner;
            objScript.Controller = owner;
            //Add spawner to proper list + put it in proper containers + set spawner position
            switch (objScript.Owner)
            {
                case CombatAffiliation.Player:
                    PlayerSpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetPlayerBounds());
                    objScript.transform.parent = HumanSpawnerContainer.transform;
                    break;
                case CombatAffiliation.AI:
                    AISpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetAIBounds());
                    objScript.transform.parent = AISpawnerContainer.transform;
                    break;
            }
            //Add Spawner to all Spawner list
            AllSpawners.Add(objScript);
        }

        public void SetupSpawnersForPlayers(SpawnerConfig config, params CombatAffiliation[] players)
        {
            foreach (var combatAffiliation in players)
            {
                SetupSpawnerByOwner(config,combatAffiliation);
                SetupSpawnerByOwner(config,combatAffiliation);
            }
        }

        public void SetupSpawners()
        {
            SetupSpawnerByOwner(SpawnerConfig,CombatAffiliation.Player);
            SetupSpawnerByOwner(SpawnerConfig,CombatAffiliation.AI);
        }

        /// <summary>
        /// Sets up spawners by config and faction
        /// </summary>
        /// <param name="config"> Spawner config class to get info from</param>
        /// <param name="owner"> Owner of the spawners</param>
        private void SetupSpawnerByOwner(SpawnerConfig config, CombatAffiliation owner)
        {
            int spawnerAmount = owner switch
            {
                CombatAffiliation.Player => config.TotalPlayerSpawners,
                CombatAffiliation.AI => config.TotalAISpawners,
                _ => throw new Exception("Wrong owner")
            };
            
            for (var i = 0; i < spawnerAmount; i++)
            {
                PlaceSpawner(owner);
            }

            var tmpTroops = owner switch
            {
                CombatAffiliation.Player => config.PlayerTroopsSpawnerAmount,
                CombatAffiliation.AI => config.AITroopsSpawnerAmount
            };
            
            var tmpArchers = owner switch
            {
                CombatAffiliation.Player=> config.PlayerArcherSpawnerAmount,
                CombatAffiliation.AI=> config.AITArcherSpawnerAmount
            };
            var list = owner switch
            {  
                CombatAffiliation.Player => PlayerSpawners,
                CombatAffiliation.AI=> AISpawners
            };
            foreach (var spawner in list)
            {
                if (tmpTroops > 0)
                {
                    spawner.Type = UnitAttackType.Melee;
                    tmpTroops--;
                }
                else if (tmpArchers > 0)
                {
                    spawner.Type = UnitAttackType.Range;
                    tmpArchers--;
                }
                else
                {
                    spawner.Type = UnitAttackType.Flying;
                }
            }
            DistributeUnitsInArmy(owner);
        }

        private void DistributeUnitsInArmy(CombatAffiliation owner)
        {
            //get the owners faction

            var faction = OptionsHelper.GetFactionOfPlayer(owner);
            
            //get the factions spawner list
            var spawnerList = owner switch
            {
                CombatAffiliation.Player => PlayerSpawners,
                CombatAffiliation.AI => AISpawners,
                _ => throw new Exception("Wrong ownership")
            };

            //get the spawners with the good type
            var troopSpawners = spawnerList.Where(spawner => spawner.Type == UnitAttackType.Melee).ToList();
            var archerSpawners = spawnerList.Where(spawner => spawner.Type == UnitAttackType.Range).ToList();
            var flyingSpawners = spawnerList.Where(spawner => spawner.Type == UnitAttackType.Flying).ToList();
            
            //troops unit cap
            var troopUnitsCount = ArmyHelper.GetUnitMaxAmount(owner, UnitAttackType.Melee);
            //archer unit cap
            var archerUnitCount = ArmyHelper.GetUnitMaxAmount(owner, UnitAttackType.Range);
            //flying unit cap
            var flyingUnitCount = ArmyHelper.GetUnitMaxAmount(owner, UnitAttackType.Flying);
            
            //Distributing troop units
            troopSpawners.ForEach(spawner =>
            {
                spawner.UnitCap = troopUnitsCount / troopSpawners.Count;
            });
            
            //Distributing archer units
            archerSpawners.ForEach(spawner =>
            {
                spawner.UnitCap = archerUnitCount / archerSpawners.Count;
            });
            
            //Distributing Flying units
            flyingSpawners.ForEach(spawner =>
            {
                spawner.UnitCap = flyingUnitCount / flyingSpawners.Count;
            });
        }
    }

    [Serializable]
    public class SpawnerConfig
    {
        [Header("Player")] 
        public int TotalPlayerSpawners;
        public int PlayerTroopsSpawnerAmount;
        public int PlayerArcherSpawnerAmount;
        public int PlayerFlyingSpawnerAmount;

        [Header("AI")] 
        public int TotalAISpawners;
        public int AITroopsSpawnerAmount;
        public int AITArcherSpawnerAmount;
        public int AIFlyingSpawnerAmount;
    }
}