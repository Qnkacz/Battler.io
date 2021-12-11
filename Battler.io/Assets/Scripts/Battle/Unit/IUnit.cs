using Battle.Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public interface IUnit
{
    void Move();
    void Attack();
    void DealDamage(IUnit target);
    void TakeDamageFrom(IUnit enemy);
    void Die();
    void Despawn();
    void Spawn();
    void MoveTo(Vector3 postiotion);
    void Setup();
    void ChangeFactionTo(UnitFaction faction);
    void SetOwner(CombatAffiliation owner);
    void SetController(CombatAffiliation controller);
}
