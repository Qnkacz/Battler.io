using Battle.Map;
using Extensions;
using UnityEngine;

public class BattleMapGeneratorManager : MonoBehaviour
{
    public BattleMap BattleMap;
    [Header("Biome Options")]
    public MapBiome Biome;
    public bool RandomizeBiome;
    [Space(10)] 
    public ObstacleGeneratorManager ObstacleGeneratorManager;

    [Space(10)] 
    public NavigationManager NavigationManager;

    [Space(10)] public SpawnerManager SpawnerManager;
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
        NavigationManager.BakeAll();
    }

    public void SetOptionsFromUI( MapBiome _Biome)
    {
        Biome = _Biome;
    }
}
