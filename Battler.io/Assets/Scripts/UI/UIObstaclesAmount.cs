using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class UIObstaclesAmount : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public int MaxAmountOfObstacles;
        public int ChosenAmountOfObstacles;
        public bool AmountIsRandomized;

        public void ChangeAmountOfObstacles(float value)
        {
            ChosenAmountOfObstacles = (int)value;
            Text.SetText(value.ToString());
        }
        public void IsAmountOfObstaclesRandomized(bool value) => AmountIsRandomized = value;
    }
}