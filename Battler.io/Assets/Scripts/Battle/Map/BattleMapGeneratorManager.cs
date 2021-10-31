using Battle.Map;
using Extensions;
using UnityEngine;

public class BattleMapGeneratorManager : MonoBehaviour
{
    public BattleMap BattleMap;
    [Header("Biome Options")]
    public MapBiome Biome;
    public bool RandomizeBiome;
    [Header("Obstacle Manager")] 
    public ObstacleGeneratorManager ObstacleGeneratorManager;
    
    private void Awake()
    {
        SetMapSize();
        SetMapBiome();
        ObstacleGeneratorManager.CalculateAmountOfSmallObstacles();
        BattleMap.SetObstaclesBounds();
        ObstacleGeneratorManager.SpawnObstacles();
    }

    private Vector3 GenerateMapSizeFromScreen()
    {
        float height = Camera.main.orthographicSize;
        float width = height * Screen.width / Screen.height;
        return new Vector3(width,1,height)/5;
    }

    public void SetMapSize() => BattleMap.SetMapScale(GenerateMapSizeFromScreen());
    public void SetMapBiome()
    {
        if(RandomizeBiome) SetRandomBiome();
        
        BattleMap.SetBiome(Biome);
    }

    private void SetRandomBiome()
    {
        Biome = (MapBiome)typeof(MapBiome).GetRandomEnumValue();
    }

    
}
