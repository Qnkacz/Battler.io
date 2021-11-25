using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class UIObstaclesAmount : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public Slider Slider;
        public int MaxAmountOfObstacles;
        public int ChosenAmountOfObstacles;
        public bool AmountIsRandomized;

        public void ChangeAmountOfObstacles(float value)
        {
            ChosenAmountOfObstacles = (int)value;
            Text.SetText(value.ToString());
        }

        public void IsAmountOfObstaclesRandomized(bool value)
        {
            AmountIsRandomized = value;
            Slider.interactable = !value;
            Text.SetText(value ? "Random" : $"{ChosenAmountOfObstacles}");
        }
    }
}