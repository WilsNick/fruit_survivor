using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBomFactory : MonoBehaviour
{
    public float bulletSpeed = 4f; // Adjust the bullet's movement speed
    public int damage = 5;
    public int pierce = 0;

    public float tomatoBomTimer;
    public GameObject tomatoBomPrefab; // Assign the prefab for the object to spawn
    public float tomatoBomInterval = 5f; // Time between shots
    private PlayerStatus playerStatus; // Reference to the player's status component

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = transform.parent.GetComponent<PlayerStatus>();
        tomatoBomTimer = tomatoBomInterval * playerStatus.GetFireRateReduction();
    }

    public void LevelUp()
    {
        // Enable the factory when leveling up
        if (!isActiveAndEnabled)
        {
            enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tomatoBomTimer -= Time.deltaTime;
        if (tomatoBomTimer <= 0f)
        {
            TomatoBomShot();
            tomatoBomTimer = tomatoBomInterval * playerStatus.GetFireRateReduction();
        }
    }

    public void TomatoBomShot()
    {
        // Instantiate a new tomato bomb at the factory's position
        GameObject tomatoBom = Instantiate(tomatoBomPrefab, this.transform.position, Quaternion.identity);

        // Set the tomato bomb's damage based on player's stats
        tomatoBom.GetComponent<TomatoBomPower>().SetDamage(damage + playerStatus.GetDamage());

        // Set the tomato bomb's direction to be determined by its own logic
        tomatoBom.GetComponent<TomatoBomPower>().SetDirection();
    }
}
