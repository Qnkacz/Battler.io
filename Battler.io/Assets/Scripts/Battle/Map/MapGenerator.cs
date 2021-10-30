using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public BattleMap BattleMap;
    private void Awake()
    {
        BattleMap.SetMapScale(GenerateMapSizeFromScreen());
    }

    private Vector3 GenerateMapSizeFromScreen()
    {
        float height = Camera.main.orthographicSize;
        float width = height * Screen.width / Screen.height;
        return new Vector3(width,1,height)/5;
    }
    
}
