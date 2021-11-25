using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class UIUnitsOptions : MonoBehaviour
    {
        [Header("Plyaer Unit Options")]
        public float PlayerUnitPercentage;

        public int ActualPlayerTotalAvaibleUnits;
        public int PlayerTroopPercentage;
        public int PlayerTroopMaxPercentage;
        public int PlayerArcherPercentage;
        public int PlayerArcherMaxPercentage;
        public int PlayerFlyerPercentage;
        public int PlayerFlyerMaxPercentage;
    
        [Header("AI Unit Options")]
        public int ActualAITotalAvaibleUnits;
        public int AITroopAmount;
        public int AIArcherAmount;
        public int AIFlyerAmount;
        
        
    }
}