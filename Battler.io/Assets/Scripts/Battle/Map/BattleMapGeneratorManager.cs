using System;
using Battle.Map;
using Battle.Unit;
using DefaultNamespace;
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
    [Space(10)] public PlacementController PlacementController;
    [Space(10)] public UnitGenerator UnitGenerator;


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
        SpawnerManager.SetupSpawners();
        BattleMap.ResizeBoundsAfterGeneration();
        NavigationManager.BakeAll();
    }

    public void SetOptionsFromUI( MapBiome biome)
    {
        Biome = biome;
    }
}
