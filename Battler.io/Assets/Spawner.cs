using System;
using System.Collections.Generic;
using Battle.Map.Helper;
using Battle.Unit;
using Extensions;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // On instantiation it should lock to only 1RACE && 1TYPE
    // Recieves 
    public int UnitCap;
    public float SpawnCooldown;
    public int OffsetUpperLimit;
    public int MaxHealth;
    public int CurrentHealth;

    public bool IsWorking;
    
    public UnitFaction Faction;
    public UnitAttackType Type;
    public bool IsDragged;
    public List<IUnit> Units;

    public GameObject Unit;
    public Healthbar Healthbar;

    private float Timer;

    public void Start()
    {
        // Initiate timer and make sure cap and cd are unsigned.
        Timer = 0;
        SpawnCooldown = Mathf.Abs(SpawnCooldown);
        UnitCap = Mathf.Abs(UnitCap);

        // Set healthbar max value to spawners max healthbar
        Healthbar.SetMaxHealth(MaxHealth);

        // Check whether we set any unit to spawn
        if (Unit == null)
        {
            gameObject.SetActive(false);
            throw new Exception("Unit not set");
        }
    }

    public void Update()
    {
        Timer += Time.deltaTime;
        Spawn();

        // DELETE THIS WHEN WE WILL ACTUALLY USE THE TAKEDAMAGE() FUNC
        // ------------------------------------------
        Healthbar.SetHealth(CurrentHealth);
        // ------------------------------------------
        
        //If is dragged make sure that the object is in the right bounds
        StayInBounds(Faction);
    }

    private void StayInBounds(UnitFaction faction)
    {
        var bounds = faction switch
        {
            UnitFaction.Human => BoundsHelper.GetHumanBounds(),
            UnitFaction.Undead => BoundsHelper.GetUndeadBounds(),
            _ => throw new Exception("Bad faction")
        };

        if (!bounds.Contains(transform.position) && IsDragged)
        {
            transform.position = bounds.ClosestPoint(transform.position);
        }
    }

    private void Spawn()
    {
        // If cooldown is down
        if (Timer >= SpawnCooldown && IsWorking)
        {
            // Randomize position fluctuation
            float[] Offset = { UnityEngine.Random.Range(-OffsetUpperLimit, OffsetUpperLimit), UnityEngine.Random.Range(-OffsetUpperLimit, OffsetUpperLimit) };
            // Calculate Unit position with offset
            var NewPosition = transform.position + new Vector3(Offset[0], 0, Offset[1]);

            // Spawn the unit and move it
            GameObject NewUnit = Instantiate(Unit);
            NewUnit.transform.position = NewPosition;

            // Reset the timer so cooldown is up
            Timer = 0;
        }
    }

    public void TakeDamage(int DamageTaken)
    {
        if (CurrentHealth <= 0)
        {
            // Destroy object? Disable? Disable'n'move?
            Debug.Log("Spawner got rekt.");
        }
        Healthbar.SetHealth(CurrentHealth);
    }

    public void SetRandomPositionInsideBounds(Bounds bound)
    {
        transform.position = bound.GetRandomPositionInsideBound();
    }
}
