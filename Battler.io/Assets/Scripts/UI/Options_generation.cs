using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Map;
using Battle.Unit;
using Extensions;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
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
        int[] HumanUnits = {exportOptions.TotalPlayerTroops,
                            exportOptions.TotalPlayerArchers,
                            exportOptions.TotalPlayerFlying};

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
            exportOptions.TotalPlayerUnits, exportOptions.TotalAIUnits,
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
        exportOptions.SetTotalPlayerUnits(exportValue);
    }

    private void SetExportAIUnits()
    {
        var maxUnits = exportOptions.MaxUnitsOnScreen;
        var exportValue = (int)(maxUnits * UnitsOptions.AIUnits.UnitPercentage);
        exportOptions.SetTotalAIUnits(exportValue);
    }

    private void SetExportHumanTroops()
    {
        var maxUnits = exportOptions.TotalPlayerUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.TroopSlider.value);
        exportOptions.SetPlayerTroops(exportValue);
    }

    private void SetExportHumanArchers()
    {
        var maxUnits = exportOptions.TotalPlayerUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.ArcherSlider.value);
        exportOptions.SetPlayerArchers(exportValue);
    }

    private void SetExportHumanFlying()
    {
        var maxUnits = exportOptions.TotalPlayerUnits;
        var exportValue =(int)( maxUnits * UnitsOptions.HumanUnits.FlyingSlider.value);
        exportOptions.SetPlayerFlying(exportValue);
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
        public int MaxUnitsOnScreen; [Space(5)]
        public int TotalPlayerUnits;
        public int TotalAIUnits;
        [Space(5)] 
        public int TotalPlayerTroops;
        public int TotalPlayerArchers;
        public int TotalPlayerFlying;
        [Space(5)] 
        public int TotalAITroops;
        public int TotalAIArchers;
        public int TotalAIFlying;

        [Space(5)] 
        public UnitFaction PlayerFaction;
        public UnitFaction AIFaction;
        [Space(5)] 
        public MapSide PlayerSide;
        public MapSide AISide;
        public void SetBiome(MapBiome value) => Biome = value;

        public void SetAmountOfObstacles(int value) => AmountOfObstacles = value;

        public void SetUnitsOnScreen(int value) => MaxUnitsOnScreen = value;
        public void SetTotalPlayerUnits(int value) => TotalPlayerUnits = value;
        public void SetTotalAIUnits(int value) => TotalAIUnits = value;
        public void SetPlayerTroops(int value) => TotalPlayerTroops = value;

        public void SetPlayerArchers(int value) => TotalPlayerArchers = value;

        public void SetPlayerFlying(int value) => TotalPlayerFlying = value;
        public void SetAITroops(int value) => TotalAITroops = value;
        public void SetAIArchers(int value) => TotalAIArchers = value;
        public void SetAIFlying(int value) => TotalAIFlying = value;

        public void SetPlayerFaction(UnitFaction faction) => PlayerFaction = faction;
        public void SetAiFaction(UnitFaction faction) => AIFaction = faction;

        public void SetPlayerSide(MapSide side) => PlayerSide = side;
        public void SetAISide(MapSide side) => AISide = side;
    }
}
