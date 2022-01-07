using System.Collections;
using System.Collections.Generic;
using Battle.Unit;
using DefaultNamespace;
using Helper;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    // Cap of total number of units
    // Should be unsigned ?
    public int HumanUnitCap;
    public int AIUnitCap;

    //List of army infos
    public List<ArmyInfo> StartArmyList;

    public void SetOptionsFromUI(int HTCap, int ATCap,
        int [] HumanUnitTotals, int [] AIUnitTotals)
    {
        // Unit totals for different races
        HumanUnitCap = HTCap;
        AIUnitCap = ATCap;
        
        // Unit totals per race
        //      HUMANS
         var humanArmy = new ArmyInfo
        {
            Affiliation = CombatAffiliation.Player,
            Faction = OptionsHelper.GetFactionOfPlayer(CombatAffiliation.Player),
            TotalAmount = HTCap,
            TroopAmount = HumanUnitTotals[0],
            ArcherAmount = HumanUnitTotals[1],
            FlyingAmount = HumanUnitTotals[2]
        };
        //      AI
        var undeadArmy = new ArmyInfo
        {
            Affiliation = CombatAffiliation.AI,
            Faction = OptionsHelper.GetFactionOfPlayer(CombatAffiliation.AI),
            TotalAmount = ATCap,
            TroopAmount = AIUnitTotals[0],
            ArcherAmount = AIUnitTotals[1],
            FlyingAmount = AIUnitTotals[2]
        };
        
        //put Infos to the list
        StartArmyList.Add(humanArmy);
        StartArmyList.Add(undeadArmy);
    }
}
