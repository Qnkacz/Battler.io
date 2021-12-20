using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Map;
using Battle.Unit;
using Helper;
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
    public List<MapBounds> BoundsList;

    public float ObstacleSpawnBoundsMargin;
    public float SpawnerBoundsMargins;
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

    private void SetRenderMaterial(Material inputMaterial)
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

        var bounds = new MapBounds
        {
            Bounds = tmpBounds,
            Name = "ObstacleBounds",
            Side = MapSide.Whole,
            Owner = CombatAffiliation.Neutral,
            Controller = CombatAffiliation.Neutral
        };
        BoundsList.Add(bounds);
    }

    public void SetSpawnerPlacementBounds()
    {
        var humanBunds = gameObject.GetComponent<Renderer>().bounds;
        humanBunds.center = new Vector3(humanBunds.extents.x / 2, humanBunds.center.y, humanBunds.center.z);
        humanBunds.extents = new Vector3(humanBunds.extents.x, humanBunds.extents.y, humanBunds.extents.z*2);
        humanBunds.Expand(new Vector3(-SpawnerBoundsMargins,0,-SpawnerBoundsMargins));
        var playerBounds = new MapBounds
        {
            Name = "PlayerBounds",
            Bounds = humanBunds,
            Side = OptionsHelper.GetExportOptions().PlayerSide,
            Owner = CombatAffiliation.Player,
            Controller = CombatAffiliation.Player
        };
        BoundsList.Add(playerBounds);
        
        var aiTmpBounds = humanBunds;
        aiTmpBounds.center = new Vector3(humanBunds.center.x * -1, humanBunds.center.y, humanBunds.center.z);
        var aiBounds = new MapBounds
        {
            Name = "AIBounds",
            Bounds = aiTmpBounds,
            Side = OptionsHelper.GetExportOptions().AISide,
            Owner = CombatAffiliation.AI,
            Controller = CombatAffiliation.AI
        };
        BoundsList.Add(aiBounds);
    }

    public void ResizeBoundsAfterGeneration()
    {
        var playerBounds = BoundsList.First(bound => bound.Owner==CombatAffiliation.Player);
        var aiBounds = BoundsList.First(bound => bound.Owner==CombatAffiliation.AI);
        playerBounds.Bounds.Expand(new Vector3(-playerBounds.Bounds.extents.x,0,-playerBounds.Bounds.extents.z));
        aiBounds.Bounds.Expand(new Vector3(-aiBounds.Bounds.extents.x,0,-aiBounds.Bounds.extents.z));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(BoundsHelper.GetHumanBounds().center,BoundsHelper.GetHumanBounds().extents);
        
        Gizmos.color = Color.black;
        Gizmos.DrawCube(BoundsHelper.GetUndeadBounds().center,BoundsHelper.GetUndeadBounds().extents);

        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(ObstacleSpawnBounds.center,ObstacleSpawnBounds.extents);
    }
}
