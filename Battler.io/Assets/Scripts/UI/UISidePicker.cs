using Battle.Map;
using Battle.Unit;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UISidePicker : MonoBehaviour
    {
        public TMP_Dropdown Dropdown;
        public MapSide Side;
        public void PickFaction(int value) => Side = (MapSide) value;
    }
}