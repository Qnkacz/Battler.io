using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float Range;
    public List<IUnit> UnitsInRange;
    public List<IUnit> EnemiesInRange;
    public List<IUnit> AlliesInRange;
}
