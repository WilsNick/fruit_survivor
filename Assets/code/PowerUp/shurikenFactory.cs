using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenFactory : MonoBehaviour
{
    public float bulletSpeed = 5f; // Adjust the shuriken's movement speed
    public int damage = 10;
    public int pierce = 0;

    public float shurikenSpawnInterval = 2f; // Time between shuriken spawns
    public float shurikenShotInterval = 1f; // Time between shots within a spawn
    public int shurikenCount = 1; // Number of shurikens to spawn in each interval

    private float shurikenSpawnTimer; // Timer for shuriken spawn interval
    private List<GameObject> shurikenPlaceholderList; // List of shuriken placeholders in the parent object
    public GameObject shurikenPrefab; // Prefab for the shuriken object to spawn
    private PlayerStatus playerStatus; // Reference to the player's status component

    public void LevelUp()
    {
        // Increase shuriken count when leveling up, or enable the factory if disabled
        if (!isActiveAndEnabled)
        {
            enabled = true;
        }
        else
        {
            shurikenCount += 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the player's status component from the parent object
        playerStatus = transform.parent.GetComponent<PlayerStatus>();

        // Adjust shuriken spawn interval and shot interval based on player's fire rate reduction
        shurikenSpawnTimer = shurikenSpawnInterval * playerStatus.GetFireRateReduction();
        shurikenShotInterval = shurikenShotInterval * playerStatus.GetFireRateReduction();

        // Initialize the list of shuriken placeholders by finding all child objects of this object
        shurikenPlaceholderList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            shurikenPlaceholderList.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Count down the shuriken spawn timer
        shurikenSpawnTimer -= Time.deltaTime;

        // Spawn shurikens when the spawn timer reaches 0
        if (shurikenSpawnTimer <= 0f)
        {
            SpawnShuriken();
            shurikenSpawnTimer = shurikenSpawnInterval * playerStatus.GetFireRateReduction();
        }
    }

    private void SpawnShuriken()
    {
        // Spawn multiple shurikens according to the shuriken count
        for (int i = 0; i < shurikenCount; i++)
        {
            // Instantiate a new shuriken object at the position of the current placeholder
            GameObject shuriken = Instantiate(shurikenPrefab, shurikenPlaceholderList[i].transform.position, Quaternion.identity);

            // Set the shuriken's parent to the same parent as the factory
            shuriken.transform.SetParent(this.transform.parent);

            // Set the shuriken's damage and pierce based on player's stats
            ShurikenPower shurikenPower = shuriken.GetComponent<ShurikenPower>();
            shurikenPower.SetDamage(damage + playerStatus.GetDamage());
            shurikenPower.SetPierce(pierce + playerStatus.GetPierce());
        }
    }
}
