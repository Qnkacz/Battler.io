using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map;
using Battle.Unit;
using Helper;
using UnityEngine;


public class TerrainBuff : MonoBehaviour
{
    public List<UnitStats> BuffList;

    public UnitStats ThisBiomeBuff;

    private void Start()
    {
        var currBiome = BiomeHelper.GetCurrentBiome();
        ThisBiomeBuff = BuffList[(int) currBiome];
    }
}