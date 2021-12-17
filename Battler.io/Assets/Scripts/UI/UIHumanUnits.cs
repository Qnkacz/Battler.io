using System;
using Battle.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHumanUnits : MonoBehaviour
    {
        public TextMeshProUGUI PercentageText;
        public TextMeshProUGUI TroopPercentageText;
        public TextMeshProUGUI ArcherPercentageText;
        public TextMeshProUGUI FlyingPercentageText;
        public Slider TroopSlider;
        public Slider ArcherSlider;
        public Slider FlyingSlider;
        public UIFactionPicker FactionPicker;
        public UISidePicker SidePicker;
        
        public float UnitPercentage;
        public void SetUnitPercentage(float value)
        {
            UnitPercentage = value;
            PercentageText.SetText($"{value:P}");
            Options_generation.UIOptions.UnitsOptions.AIUnits.SetUnitPercentage(1f-value);
        }

        public void SetTroopValue(float value)
        {
            TroopPercentageText.SetText($"{TroopSlider.value:P}");
            var totalPercentage = TroopSlider.value + ArcherSlider.value + FlyingSlider.value;
            if (totalPercentage > 1)
            {
                var amountToRemove = totalPercentage - 1;
                ArcherSlider.SetValueWithoutNotify(ArcherSlider.value - amountToRemove / 2);
                ArcherPercentageText.SetText($"{ArcherSlider.value:P}");
                FlyingSlider.SetValueWithoutNotify(FlyingSlider.value - amountToRemove / 2);
                FlyingPercentageText.SetText($"{FlyingSlider.value:P}");
            }
        }

        public void SetArcherValue(float value)
        {
            ArcherPercentageText.SetText($"{ArcherSlider.value:P}");
            var totalPercentage = TroopSlider.value + ArcherSlider.value + FlyingSlider.value;
            if (totalPercentage > 1)
            {
                var amountToRemove = totalPercentage - 1;
                TroopSlider.SetValueWithoutNotify(TroopSlider.value - amountToRemove / 2);
                TroopPercentageText.SetText($"{TroopSlider.value:P}");
                FlyingSlider.SetValueWithoutNotify(FlyingSlider.value - amountToRemove / 2);
                FlyingPercentageText.SetText($"{FlyingSlider.value:P}");
            }
        }

        public void SetFlyingValue(float value)
        {
            FlyingPercentageText.SetText($"{FlyingSlider.value:P}");
            var totalPercentage = TroopSlider.value + ArcherSlider.value + FlyingSlider.value;
            if (totalPercentage > 1)
            {
                var amountToRemove = totalPercentage - 1;
                ArcherSlider.SetValueWithoutNotify(ArcherSlider.value - amountToRemove / 2);
                ArcherPercentageText.SetText($"{ArcherSlider.value:P}");
                TroopSlider.SetValueWithoutNotify(TroopSlider.value - amountToRemove / 2);
                TroopPercentageText.SetText($"{TroopSlider.value:P}");
            }
        }
    }
}