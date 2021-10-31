using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Map;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleMap : MonoBehaviour
{
    public Transform Transform;
    public MapBiome Biome;
    public Material CurrentMaterial;
    public Material[] Materials;
    public MeshRenderer MeshRenderer;
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
}
