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
        public List<Spawner> AiSpawners;

        [Space(10)] 
        public GameObject HumanSpawnerContainer;
        public GameObject AISpawnerContainer;


        public Spawner SpawnerPrefab;
        public void PlaceSpawner(UnitFaction faction)
        {
            var obj = Instantiate(SpawnerPrefab.gameObject);
            var objScript = obj.GetComponent<Spawner>();
            
            //Set Spawner Faction
            objScript.Faction = faction;
            
            //Set spawner initial position
            

            //Add spawner to proper list + put it in proper containers + set spawner position
            switch (faction)
            {
                
                case UnitFaction.Human:
                    HumanSpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetHumanBounds());
                    objScript.transform.parent = HumanSpawnerContainer.transform;
                    break;
                case UnitFaction.Undead:
                    AiSpawners.Add(objScript);
                    objScript.SetRandomPositionInsideBounds(BoundsHelper.GetUndeadBounds());
                    objScript.transform.parent = AISpawnerContainer.transform;
                    break;
            }

            //Add Spawner to all Spawner list
            AllSpawners.Add(objScript);
        }
    }
}