using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFactory : MonoBehaviour
{
    public float moveSpeed = 10f; // Adjust the seed's movement speed
    public int damage = 5;
    public int pierce = 0;

    public float seedInterval = 1f; // Time between shots
    private float seedTimer;
    public GameObject seedPrefab; // Assign the prefab for the seed object to spawn
    private PlayerStatus playerStatus;

    public void LevelUp()
    {
        // Enable the SeedFactory when it's leveled up
        if (!isActiveAndEnabled)
        {
            enabled = true;
        }
    }

    void Start()
    {
        // Get a reference to the PlayerStatus script
        playerStatus = transform.parent.GetComponent<PlayerStatus>();
        
        // Adjust the seed timer based on the player's fire rate reduction
        seedTimer = seedInterval * playerStatus.GetFireRateReduction();
    }

    void Update()
    {
        // Reduce the seed timer
        seedTimer -= Time.deltaTime;
        
        // Shoot a seed when the timer reaches zero
        if (seedTimer <= 0f)
        {
            shootSeed();
            seedTimer = seedInterval * playerStatus.GetFireRateReduction();
        }
    }

    public void shootSeed()
    {
        // Instantiate a seed object
        GameObject seed = Instantiate(seedPrefab, this.transform.position, Quaternion.identity);
        
        // Determine the direction of the seed based on the player's facing direction
        bool right = this.transform.parent.GetComponent<PlayerMovement>().IsRight();
        
        // Set the damage and pierce of the seed based on the player's stats
        seed.GetComponent<SeedPower>().SetDamage(damage + playerStatus.GetDamage());
        seed.GetComponent<SeedPower>().SetDirection(right);
        seed.GetComponent<SeedPower>().SetPierce(pierce + playerStatus.GetPierce());  
    }
}
