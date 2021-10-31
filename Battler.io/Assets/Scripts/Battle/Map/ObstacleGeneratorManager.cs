using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map.SmallObstackes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGeneratorManager : MonoBehaviour
{
    public BattleMapGeneratorManager BattleMapGeneratorManager;
    public bool RandomizeSmallObjectAmount;
    public int ObstaclesBaseAmount;

    public int ObstaclesAmountFluctuation;
    public int ObstaclesDelta;

    [Header("Obstacle Arrays")] 
    public SmallObstaclePair[] PlaceholderObstacles;

    public GameObject PlaceholderRandomObstacle;
    [Header("Spawned Obstacles")]
    public List<GameObject> AllSpawnedObstacles = new List<GameObject>();
    public List<GameObject> SpawnedSmallObstacles = new List<GameObject>();
    public List<GameObject> SpawnedMediumObstacles = new List<GameObject>();
    public List<GameObject> SpawnedLargeObstacles = new List<GameObject>();
    // Start is called before the first frame update
    public void CalculateAmountOfSmallObstacles()
    {
        if(RandomizeSmallObjectAmount) SetRandomAMountOfObstacles();
        else SetPreciseAmountOfObstacles();
    }

    private void SetRandomAMountOfObstacles() => ObstaclesDelta = Random.Range(1, 5);

    private void SetPreciseAmountOfObstacles()
    {
        ObstaclesDelta = ObstaclesBaseAmount +
                              Random.Range(-ObstaclesAmountFluctuation, ObstaclesAmountFluctuation);
    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < ObstaclesDelta; i++)
        {
            SpawnRandomObstacle();
        }
    }

    public void SpawnRandomObstacle()
    {
        GameObject spawnedObstacle = Instantiate(PlaceholderRandomObstacle);
        AddObstacleToCategory(spawnedObstacle);
    }

    private void AddObstacleToCategory(GameObject obj)
    {
        var obstacle = obj.GetComponent<Obstacle>();
        AllSpawnedObstacles.Add(obj);
        switch (obstacle.obstacleSizeCategory)
        {
            case SmallObstacleSizeCategory.Small:
                SpawnedSmallObstacles.Add(obj);
                break;
            case SmallObstacleSizeCategory.Medium:
                SpawnedMediumObstacles.Add(obj);
                break;
            case SmallObstacleSizeCategory.Large:
                SpawnedLargeObstacles.Add(obj);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

[Serializable]
public struct SmallObstaclePair
{
    public SmallObstacleSizeCategory Category;
    public GameObject prefab;
}
