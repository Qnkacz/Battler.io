using Battle.Unit;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIFactionPicker : MonoBehaviour
    {
        public TMP_Dropdown Dropdown;
        public UnitFaction Faction;
        public void PickFaction(int value) => Faction = (UnitFaction) value;
    }
}