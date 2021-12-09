using System;
using Battle.Map;
using Battle.Unit;
using Extensions;
using UnityEngine;

public class BattleMapGeneratorManager : MonoBehaviour
{
    public static BattleMapGeneratorManager GameManager;
    public BattleMap BattleMap;
    [Header("Biome Options")]
    public MapBiome Biome;
    public bool RandomizeBiome;
    [Space(10)] 
    public ObstacleGeneratorManager ObstacleGeneratorManager;

    [Space(10)] 
    public NavigationManager NavigationManager;

    [Space(10)] public SpawnerManager SpawnerManager;

    private void Awake()
    {
        GameManager = this;
    }

    private Vector3 GenerateMapSizeFromScreen()
    {
        float height = Camera.main.orthographicSize;
        float width = height * Screen.width / Screen.height;
        return new Vector3(width,1,height)/5;
    }

    public void SetMapSize() => BattleMap.SetMapScale(GenerateMapSizeFromScreen());
    public void GenerateMap()
    {
        SetMapSize();
        BattleMap.SetObstaclesBounds();
        BattleMap.SetSpawnerPlacementBounds();
        BattleMap.SetBiome(Biome);
        ObstacleGeneratorManager.SpawnObstacles();
        SetupSpawners();
        NavigationManager.BakeAll();
    }

    public void SetOptionsFromUI( MapBiome _Biome)
    {
        Biome = _Biome;
    }

    public void SetupSpawners()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnerManager.PlaceSpawner(UnitFaction.Human);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnerManager.PlaceSpawner(UnitFaction.Undead);
        }
    }
}
