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
        public void PlaceSpawner(UnitFaction faction)
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
            SetupHuman(config);
            SetupUndead(config);
        }

        private void SetupHuman(SpawnerConfig config)
        {
            for (var i = 0; i < config.TotalHumanSpawners; i++)
            {
                PlaceSpawner(UnitFaction.Human);
            }
        }

        private void SetupUndead(SpawnerConfig config)
        {
            for (int i = 0; i < config.TotalUndeadSpawners; i++)
            {
                PlaceSpawner(UnitFaction.Undead);
            }
        }
    }

    [Serializable]
    public class SpawnerConfig
    {
        public int TotalSpawnerAmount;
        
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