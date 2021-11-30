using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    // Cap of total number of units
    // Should be unsigned ?
    public int HumanUnitCap;
    public int AIUnitCap;

    // Human-type unit caps
    public int HuTrooperCap;
    public int HuArcherCap;
    public int HuFlyingCap;

    // AI-type unit caps
    public int AITrooperCap;
    public int AIArcherCap;
    public int AIFlyingCap;

    public void SetOptionsFromUI(int HTCap, int ATCap,
        int [] HumanUnitTotals, int [] AIUnitTotals)
    {
        // Unit totals for different races
        HumanUnitCap = HTCap;
        AIUnitCap = ATCap; 

        // Unit totals per race
        //      HUMANS
        HuTrooperCap = HumanUnitTotals[0];
        HuArcherCap = HumanUnitTotals[1];
        HuFlyingCap = HumanUnitTotals[2];
        //      AI
        AITrooperCap = AIUnitTotals[0];
        AIArcherCap = AIUnitTotals[1];
        AIFlyingCap = AIUnitTotals[2];
    }
}
