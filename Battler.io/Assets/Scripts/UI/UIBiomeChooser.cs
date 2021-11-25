using System;
using Battle.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [Serializable]
    public class UIBiomeChooser : MonoBehaviour
    {
        public TMP_Dropdown Dropdown;
        public MapBiome ChosenMapBiome;
        public bool IsBiomeRandomized;

        public void IsBiomeRadomized(bool value)
        {
            IsBiomeRandomized = value;
            Dropdown.interactable = !value;
        }
        public void ChooseBiome(Int32 value)=>ChosenMapBiome = (MapBiome)value;
    }
}