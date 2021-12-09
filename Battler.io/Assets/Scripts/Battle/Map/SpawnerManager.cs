using System;
using System.Collections.Generic;
using Battle.Map.Helper;
using Battle.Unit;
using Extensions;
using UnityEngine;

namespace Battle.Map
{
    public class SpawnerManager : MonoBehaviour
    {
        public List<Spawner> AllSpawners;
        public List<Spawner> HumanSpawners;
        public List<Spawner> UndeadSpawners;
        [Space(10)] 
        public SpawnerConfig SpawnerConfig;
        [Space(10)] 
        public GameObject HumanSpawnerContainer;
        public GameObject AISpawnerContainer;
        [Space(10)]
        public Spawner SpawnerPrefab;

        private void PlaceSpawner(UnitFaction faction)
        {
            var obj = Instantiate(SpawnerPrefab.gameObject);
            var objScript = obj.GetComponent<Spawner>();
            
            //Set Spawner Faction
            objScript.Faction = faction;
            //Add spawner to proper list + put it in proper containers + set spawner position
            switch (faction)
            {
                case UnitFaction.Human:
                    HumanSpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetHumanBounds());
                    objScript.transform.parent = HumanSpawnerContainer.transform;
                    break;
                case UnitFaction.Undead:
                    UndeadSpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetUndeadBounds());
                    objScript.transform.parent = AISpawnerContainer.transform;
                    break;
            }
            //Add Spawner to all Spawner list
            AllSpawners.Add(objScript);
        }

        public void SetupSpawners(SpawnerConfig config)
        {
            SetupSpawnerByFaction(config,UnitFaction.Human);
            SetupSpawnerByFaction(config,UnitFaction.Undead);
        }

        /// <summary>
        /// Sets up spawners by config and faction
        /// </summary>
        /// <param name="config"> Spawner config class to get info from</param>
        /// <param name="faction"> Faction you want to setup</param>
        private void SetupSpawnerByFaction(SpawnerConfig config, UnitFaction faction)
        {
            int spawnerAmount = faction switch
            {
                UnitFaction.Human => config.TotalHumanSpawners,
                UnitFaction.Undead => config.TotalUndeadSpawners,
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
            };
            
            for (var i = 0; i < spawnerAmount; i++)
            {
                PlaceSpawner(faction);
            }

            var tmpTroops = faction switch
            {
                UnitFaction.Human => config.HumanTroopsSpawnerAmount,
                UnitFaction.Undead => config.UndeadTroopsSpawnerAmount,
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
            };
            
            var tmpArchers = faction switch
            {
                UnitFaction.Human => config.HumanArcherSpawnerAmount,
                UnitFaction.Undead => config.UndeadTArcherSpawnerAmount,
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
            };
            var list = faction switch
            {  
                UnitFaction.Human => HumanSpawners,
                UnitFaction.Undead => UndeadSpawners,
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
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
        }
    }

    [Serializable]
    public class SpawnerConfig
    {
        [Header("Human")] 
        public int TotalHumanSpawners;
        public int HumanTroopsSpawnerAmount;
        public int HumanArcherSpawnerAmount;
        public int HumanFlyingSpawnerAmount;

        [Header("Undead")] 
        public int TotalUndeadSpawners;
        public int UndeadTroopsSpawnerAmount;
        public int UndeadTArcherSpawnerAmount;
        public int UndeadFlyingSpawnerAmount;
    }
}