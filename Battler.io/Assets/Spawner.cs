using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // On instantiation it should lock to only 1RACE && 1TYPE
    // Recieves 
    public int UnitCap;
    public GameObject Unit;
    public float SpawnCooldown;
    public int OffsetUpperLimit;

    private float Timer;

    public void Start()
    {
        // Initiate timer and make sure cap and cd are unsigned.
        Timer = 0;
        SpawnCooldown = Mathf.Abs(SpawnCooldown);
        UnitCap = Mathf.Abs(UnitCap);

        //
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
    }

    private void Spawn()
    {
        // If cooldown is down
        if (Timer >= SpawnCooldown)
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
}
