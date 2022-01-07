using System;
using System.Collections.Generic;
using Battle.Unit;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float Range;
    public SphereCollider Collider;
    public List<CombatUnit> UnitsInRange;
    public List<Spawner> SpawnersInRange;

    private void Awake()
    {
        Collider.radius = Range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CombatUnit otherUnit))
            UnitsInRange.Add(otherUnit);
        if(other.gameObject.TryGetComponent(out Spawner spawner))
            SpawnersInRange.Add(spawner);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out CombatUnit unit))
            UnitsInRange.Remove(unit);
        
        if(other.gameObject.TryGetComponent(out Spawner spawner))
            SpawnersInRange.Remove(spawner);
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
