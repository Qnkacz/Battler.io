using System;
using Battle.Map;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class UIBiomeChooser : MonoBehaviour
    {
        public MapBiome ChosenMapBiome;
        public bool IsBiomeRandomized;
        public void IsBiomeRadomized(bool value) => IsBiomeRandomized = value;
        public void ChooseBiome(Int32 value)=>ChosenMapBiome = (MapBiome)value;
    }
}