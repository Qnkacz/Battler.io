using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleMap : MonoBehaviour
{
    public Transform MapTransform;
    public MapBiome MapBiome;
    
    public void SetMapScale(Vector3 scale)=>MapTransform.localScale = scale;
    public Vector3 GetMapScale =>MapTransform.localScale;

    public void SetRandomBiome()
    {
        MapBiome = (MapBiome)typeof(MapBiome).GetRandomEnumValue();
    }
}
