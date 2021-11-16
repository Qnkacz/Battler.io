using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map;
using UnityEngine;
using UnityEngine.UI;

public class Options_generation : MonoBehaviour
{
    [Header("Human UI Elements")] 
    public Slider HumanTroop;
    public Slider HumanShooter;
    public Slider HumanFlyer;
    
    public MapBiome ChosenBiome;
    public bool IsObstacleAmountRandomized;
    public bool IsMaxUnitOnScreenRandomized;
    public int AmountOfObstacles;
    public int MaxUnitsOnScreen;
    [Header("Plyaer Unit Options")]
    public float PlayerUnitPercentage;

    public int ActualPlayerTotalAvaibleUnits;
    public int PlayerTroopPercentage;
    public int PlayerTroopMaxPercentage;
    public int PlayerArcherPercentage;
    public int PlayerArcherMaxPercentage;
    public int PlayerFlyerPercentage;
    public int PlayerFlyerMaxPercentage;
    
    [Header("AI Unit Options")]
    public int ActualAITotalAvaibleUnits;
    public int AITroopAmount;
    public int AIArcherAmount;
    public int AIFlyerAmount;


    public void ChooseBiome(Int32 value)=>ChosenBiome = (MapBiome)value;

    public void ChangeAmountOfObstacles(float value)=>AmountOfObstacles = (int)value;

    public void ChangeAmountOfMaxUnitsOnScreen(float value)=>MaxUnitsOnScreen = (int)value;

    public void IsAmountOfObstaclesRandomized(bool value) => IsObstacleAmountRandomized = value;

    public void IsAmountOfMaxUnitsOnScreenRandomized(bool value) => IsMaxUnitOnScreenRandomized = value;
    public void ChangePlayerUnitPercentage(float value)
    { 
        PlayerUnitPercentage = value;
        ActualPlayerTotalAvaibleUnits =(int)(MaxUnitsOnScreen * PlayerUnitPercentage);
        ActualAITotalAvaibleUnits = MaxUnitsOnScreen - ActualPlayerTotalAvaibleUnits;
    }
}
