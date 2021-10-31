using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Map;
using Extensions;
using Unity.VisualScripting;
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
    public Bounds PlayerUnitPlacementBounds;
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
        var verticleList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        var firstVerticle = verticleList.First();
        var lastVerticle = verticleList.Last();
        Vector3 center = Vector3.Lerp(firstVerticle, lastVerticle, .5f);
        var tmpBounds = new Bounds(center, lastVerticle);
        tmpBounds.Expand(-ObstacleSpawnBoundsMargin);
        Debug.Log($"First verticle params: {firstVerticle}");
        Debug.Log($"Last verticle params: {lastVerticle}");
        ObstacleSpawnBounds = tmpBounds;
    }

    void OnDrawGizmos()
    {
        var verticleList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        var firstVerticle = verticleList.First();
        var lastVerticle = verticleList.Last();
    }
}
