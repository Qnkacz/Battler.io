using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map;
using Extensions;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Options_generation : MonoBehaviour
{
    public GameObject UICanvas;
    public UIBiomeChooser BiomeChooser;
    public UIMaxUnitsOnScreen MaxUnitsOnScreen;
    public UIObstaclesAmount ObstaclesAmount;
    public UIUnitsOptions UnitsOptions;
    public ExportOptions exportOptions;

    public static Options_generation UIOptions;
    private void Awake()
    {
        UIOptions = this;
    }

    public void PressButton()
    {
        ExportOptionsFromUI();

        // Prep arrays for trespass
        int[] HumanUnits = {exportOptions.TotalHumanTroops,
                            exportOptions.TotalHumanArchers,
                            exportOptions.TotalHumanFlying};

        int[] AIUnits = {exportOptions.TotalAITroops,
                         exportOptions.TotalAIArchers,
                         exportOptions.TotalAIFlying};

        // Pass set amount of obstacles from UI to GM
        FindObjectOfType<ObstacleGeneratorManager>().SetOptionsFromUI(exportOptions.AmountOfObstacles);
        // Pass set biome type from UI to GM
        var MapGenerationManager = FindObjectOfType<BattleMapGeneratorManager>();
        MapGenerationManager.SetOptionsFromUI(exportOptions.Biome);
        // Pass unit totals to UnitGenerator from UI
        FindObjectOfType<UnitGenerator>().SetOptionsFromUI(
            exportOptions.TotalHumanUnits, exportOptions.TotalAIUnits,
            HumanUnits, AIUnits);
        
        // Hide the UI
        UICanvas.SetActive(false);
        
        //Generate Map
        MapGenerationManager.GenerateMap();
    }

    public void ExportOptionsFromUI()
    {
        SetExportBiome();
        SetExportAmountOfObstacles();
        SetExportUnitsOnScreen();
        SetExportHumanUnits();
        SetExportAIUnits();
        SetExportHumanTroops();
        SetExportHumanArchers();
        SetExportHumanFlying();
        SetExportAITroops();
        SetExportAIArchers();
        SetExportAIFlying();
    }

    private void SetExportBiome()
    {
        var exportValue = BiomeChooser.IsBiomeRandomized
            ? (MapBiome)typeof(MapBiome).GetRandomEnumValue()
            : BiomeChooser.ChosenMapBiome;
        exportOptions.SetBiome(exportValue);
    }
        

    private void SetExportAmountOfObstacles()
    {
        var exportValue = ObstaclesAmount.AmountIsRandomized
            ? Random.Range(1, 8)
            : ObstaclesAmount.ChosenAmountOfObstacles;
        exportOptions.SetAmountOfObstacles(exportValue);
    }

    private void SetExportUnitsOnScreen()
    {
        var exportValue= MaxUnitsOnScreen.AmountIsRandomized
            ? Random.Range(2, 254)
            : MaxUnitsOnScreen.ChosenAmontOfUnits;
        exportOptions.SetUnitsOnScreen(exportValue);
    }

    private void SetExportHumanUnits()
    {
        var maxUnits = exportOptions.MaxUnitsOnScreen;
        var exportValue = (int)(maxUnits * UnitsOptions.HumanUnits.UnitPercentage);
        exportOptions.SetTotalHumanUnits(exportValue);
    }

    private void SetExportAIUnits()
    {
        var maxUnits = exportOptions.MaxUnitsOnScreen;
        var exportValue = (int)(maxUnits * UnitsOptions.AIUnits.UnitPercentage);
        exportOptions.SetTotalAIUnits(exportValue);
    }

    private void SetExportHumanTroops()
    {
        var maxUnits = exportOptions.TotalHumanUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.TroopSlider.value);
        exportOptions.SetHumanTroops(exportValue);
    }

    private void SetExportHumanArchers()
    {
        var maxUnits = exportOptions.TotalHumanUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.ArcherSlider.value);
        exportOptions.SetHumanArchers(exportValue);
    }

    private void SetExportHumanFlying()
    {
        var maxUnits = exportOptions.TotalHumanUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.FlyingSlider.value);
        exportOptions.SetHumanFlying(exportValue);
    }

    private void SetExportAITroops()
    {
        var maxUnits = exportOptions.TotalAIUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.AIUnits.TroopSlider.value);
        exportOptions.SetAITroops(exportValue);
    }
    private void SetExportAIArchers()
    {
        var maxUnits = exportOptions.TotalAIUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.AIUnits.ArcherSlider.value);
        exportOptions.SetAIArchers(exportValue);
    }

    private void SetExportAIFlying()
    {
        var maxUnits = exportOptions.TotalAIUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.AIUnits.FlyingSlider.value);
        exportOptions.SetAIFlying(exportValue);
    }

    [Serializable]
    public class ExportOptions
    {
        public MapBiome Biome;
        [Space(5)]
        public int AmountOfObstacles;
        public int MaxUnitsOnScreen;
        [Space(5)]
        public int TotalHumanUnits;
        public int TotalAIUnits;
        [Space(5)] 
        public int TotalHumanTroops;
        public int TotalHumanArchers;
        public int TotalHumanFlying;
        [Space(5)] 
        public int TotalAITroops;
        public int TotalAIArchers;
        public int TotalAIFlying;
        public void SetBiome(MapBiome value) => Biome = value;

        public void SetAmountOfObstacles(int value) => AmountOfObstacles = value;

        public void SetUnitsOnScreen(int value) => MaxUnitsOnScreen = value;
        public void SetTotalHumanUnits(int value) => TotalHumanUnits = value;
        public void SetTotalAIUnits(int value) => TotalAIUnits = value;
        public void SetHumanTroops(int value) => TotalHumanTroops = value;

        public void SetHumanArchers(int value) => TotalHumanArchers = value;

        public void SetHumanFlying(int value) => TotalHumanFlying = value;
        public void SetAITroops(int value) => TotalAITroops = value;
        public void SetAIArchers(int value) => TotalAIArchers = value;

        public void SetAIFlying(int value) => TotalAIFlying = value;
    }
}
