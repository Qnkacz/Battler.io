using System;
using System.Collections.Generic;
using Battle.Unit;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float Range;
    public SphereCollider Collider;
    public List<CombatUnit> UnitsInRange;

    private void Awake()
    {
        Collider.radius = Range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CombatUnit otherUnit))
            UnitsInRange.Add(otherUnit);
    }

    private void OnTriggerExit(Collider other)
    {
        var otherUnit = other.gameObject.GetComponent<CombatUnit>();
        UnitsInRange.Remove(otherUnit);
    }

    public void LowerRangeBy(float amount)
    {
        Range = Math.Max(0f, Range - amount);
        Collider.radius = Range;
    }

    public void RaiseRangeBy(float amount)
    {
        Range = Math.Max(0f, Range - amount);
        Collider.radius = Range;
    }
}
