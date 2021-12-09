using System;
using System.Linq;
using Battle.Map;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleMap : MonoBehaviour
{
    public Transform Transform;
    public MapBiome Biome;
    public Material CurrentMaterial;
    public Material[] Materials;
    public MeshRenderer MeshRenderer;
    [Header("Bounds")] 
    public Bounds ObstacleSpawnBounds;

    public float ObstacleSpawnBoundsMargin;
    public float SpawnerBoundsMargins;
    public Bounds PlayerUnitPlacementBounds;
    public Bounds AIUnitPlacementBounds;
    public void SetMapScale(Vector3 scale)=>Transform.localScale = scale;
    public Vector3 GetMapScale =>Transform.localScale;
    public void SetMaterialByBiome(MapBiome biome)
    {
        CurrentMaterial = biome switch
        {
            MapBiome.Dessert => Materials.First(material => material.name == "Dessert"),
            MapBiome.Plain => Materials.First(material => material.name == "Plain"),
            MapBiome.Forest => Materials.First(material => material.name == "Forest"),
            MapBiome.Shore => Materials.First(material => material.name == "Shore"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetBiome(MapBiome inputBiome)
    {
        this.Biome = inputBiome;
        SetMaterialByBiome(inputBiome);
        SetRenderMaterial(CurrentMaterial);
    }

    public void SetRenderMaterial(Material inputMaterial)
    {
        MeshRenderer.material = inputMaterial;
    }

    public void SetObstaclesBounds()
    {
        //because of some unknown reason i had to expand the bounds because it was oryginally too small
        var tmpBounds = gameObject.GetComponent<Renderer>().bounds;
        tmpBounds.Expand(tmpBounds.size);
        
        //shink the bounds by the margin
        tmpBounds.Expand(new Vector3(-ObstacleSpawnBoundsMargin,0,-ObstacleSpawnBoundsMargin));
        ObstacleSpawnBounds = tmpBounds;
    }

    public void SetSpawnerPlacementBounds()
    {
        var humanBunds = gameObject.GetComponent<Renderer>().bounds;
        humanBunds.center = new Vector3(humanBunds.extents.x / 2, humanBunds.center.y, humanBunds.center.z);
        humanBunds.extents = new Vector3(humanBunds.extents.x, humanBunds.extents.y, humanBunds.extents.z*2);
        humanBunds.Expand(new Vector3(-SpawnerBoundsMargins,0,-SpawnerBoundsMargins));
        PlayerUnitPlacementBounds = humanBunds;

        var aiBounds = humanBunds;

        aiBounds.center = new Vector3(humanBunds.center.x * -1, humanBunds.center.y, humanBunds.center.z);
        AIUnitPlacementBounds = aiBounds;
    }

    public Vector3 GetRandomPositionInsideObstacleBound(Bounds inputBound)
    {
        return new Vector3(
            Random.Range(inputBound.min.x,inputBound.max.x),
            Transform.position.y,
            Random.Range(inputBound.min.z,inputBound.max.z)
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(PlayerUnitPlacementBounds.center,PlayerUnitPlacementBounds.extents);
        
        Gizmos.color = Color.black;
        Gizmos.DrawCube(AIUnitPlacementBounds.center,AIUnitPlacementBounds.extents);

        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(ObstacleSpawnBounds.center,ObstacleSpawnBounds.extents);
    }
}
