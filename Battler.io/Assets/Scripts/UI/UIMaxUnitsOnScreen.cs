using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class UIMaxUnitsOnScreen : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public int ChosenAmontOfUnits;
        public bool AmountIsRandomized;
        
        public void ChangeAmountOfObstacles(float value)
        {
            ChosenAmontOfUnits = (int)value;
            Text.SetText(value.ToString());
        }
        public void IsAmountOfUnitsRandomized(bool value) => AmountIsRandomized = value;
    }
}