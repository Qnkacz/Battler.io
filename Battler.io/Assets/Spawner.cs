using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Unit;
using Extensions;
using Helper;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // On instantiation it should lock to only 1RACE && 1TYPE
    // Recieves 
    public CombatAffiliation Owner;
    public CombatAffiliation Controller;
    public int UnitCap;
    public int UnitsSpawned;
    public float SpawnCooldown;
    public int OffsetUpperLimit;
    public int MaxHealth;
    public int CurrentHealth;

    public bool IsWorking;

    public UnitFaction Faction;
    public UnitAttackType Type;
    public bool IsDragged;
    public List<CombatUnit> Units;
    public Bounds PlacementBounds;
    public Transform UnitContainer;

    public GameObject Unit;
    public Healthbar Healthbar;

    private float Timer;

    public void Start()
    {
        //get health up and running
        CurrentHealth = MaxHealth;
        
        // Initiate timer and make sure cap and cd are unsigned.
        Timer = 0;
        SpawnCooldown = Mathf.Abs(SpawnCooldown);
        UnitCap = Mathf.Abs(UnitCap);

        // Set healthbar max value to spawners max healthbar
        Healthbar.SetMaxHealth(MaxHealth);

        //selects proper unit from units list
        SelectProperUnit();

        //sets the placements where the spawner can be moved around
        SetPlacementBounds();

        //finds and sets the unit container
        SetUnitContainer();

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
        StayInBounds();
    }

    private void SetUnitContainer() => UnitContainer = GameObject.Find("UnitsContainer").transform;


    private void SetPlacementBounds()
    {
        PlacementBounds = Controller switch
        {
            CombatAffiliation.Player => BoundsHelper.GetPlayerBounds(),
            CombatAffiliation.AI => BoundsHelper.GetAIBounds(),
            CombatAffiliation.Neutral => BoundsHelper.GetObstacleBounds(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void StayInBounds()
    {
        if (!PlacementBounds.Contains(transform.position) && IsDragged)
        {
            transform.position = PlacementBounds.ClosestPoint(transform.position);
        }
    }

    private void Spawn()
    {
        // If cooldown is down
        if (CanSpawn())
        {
            // Randomize position fluctuation
            float[] Offset =
            {
                UnityEngine.Random.Range(-OffsetUpperLimit, OffsetUpperLimit),
                UnityEngine.Random.Range(-OffsetUpperLimit, OffsetUpperLimit)
            };
            // Calculate Unit position with offset
            var NewPosition = transform.position + new Vector3(Offset[0], 0, Offset[1]);

            // Spawn the unit and move it
            GameObject NewUnit = Instantiate(Unit);
            var unitScript = NewUnit.GetComponent<CombatUnit>();
            
            //set unit controller
            SetUnitController(unitScript);
            
            //set unit owner
            SetUnitOwner(unitScript);
            
            NewUnit.transform.position = NewPosition;

            //Place the unit in the container
            if (UnitContainer != null)
                NewUnit.transform.parent = UnitContainer;

            //Adds one to the spawned count
            UnitsSpawned++;

            // Reset the timer so cooldown is up
            Timer = 0;
        }
    }

    private bool CanSpawn()
    {
        var timerCondition = Timer >= SpawnCooldown;
        var unitCondition = UnitCap > UnitsSpawned;
        return (IsWorking && timerCondition && unitCondition);
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

    //selects the probper unit from list to spawn according to settings
    private void SelectProperUnit()
    {
        var foundUnit = Units.First(unit => (unit.Faction == Faction && unit.AttackType == Type));

        if (foundUnit == null)
        {
            IsWorking = false;
            gameObject.name += "BORKED!";
            throw new Exception($"spawner: {gameObject.name} couldn't find proper unit in list");
        }

        Unit = foundUnit.gameObject;
    }

    //sets the chosen units owner
    private void SetUnitOwner(CombatUnit unit)
    {
        unit.Owner = this.Owner;
    }

    private void SetUnitController(CombatUnit unit)
    {
        unit.Controller = this.Controller;
    }
}