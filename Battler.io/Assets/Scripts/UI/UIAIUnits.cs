using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIAIUnits : MonoBehaviour
    {
        public TextMeshProUGUI PercentageText;
        public TextMeshProUGUI TroopPercentageText;
        public TextMeshProUGUI ArcherPercentageText;
        public TextMeshProUGUI FlyingPercentageText;

        public Slider TroopSlider;
        public Slider ArcherSlider;
        public Slider FlyingSlider;
        
        public float UnitPercentage;

        public float TroopPercentage;
        public float ArcherPercentage;
        public float FlyerPercentage;

        public bool TroopPercentageIsRandomized;
        public bool ArcherPercentageIsRandomized;
        public bool FlyingPercentageIsRandomized;

        public void SetUnitPercentage(float value)
        {
            UnitPercentage = value;
            PercentageText.SetText($"{value:P}");
        }

        public void RandomizeTroops(bool value)
        {
            TroopPercentageIsRandomized = value;
            TroopSlider.interactable = !value;
            TroopPercentageText.SetText(value ? "Random" : $"{FlyerPercentage:P}");
        }
        public void RandomizeArchers(bool value)
        {
            ArcherPercentageIsRandomized = value;
            ArcherSlider.interactable = !value;
            ArcherPercentageText.SetText(value ? "Random" : $"{FlyerPercentage:P}");
        }

        public void RandomizeFlying(bool value)
        {
            FlyingPercentageIsRandomized = value;
            FlyingSlider.interactable = !value;
            FlyingPercentageText.SetText(value ? "Random" : $"{FlyerPercentage:P}");
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