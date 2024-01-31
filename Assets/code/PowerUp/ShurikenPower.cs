using UnityEngine;
using System.Collections.Generic;

public class ShurikenPower : MonoBehaviour
{
    public float shurikenShotInterval = 1f; // Time between shots
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    private float shurikenShotTimer;

    public float moveSpeed = 2f; // Adjust the shuriken's movement speed
    public int damage = 10;
    public int pierce = 0;
    private Vector2 direction;
    public bool isShot = false;
    public float rotationSpeed = 500f;

    private void Start()
    {
        shurikenShotTimer = shurikenShotInterval;
    }

    public bool ShootShuriken()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // Find the nearest enemy
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // Set the direction to the nearest enemy or random if no enemy found
        if (nearestEnemy == null)
        {
            var x = Random.Range(-1f, 1f);
            var y = Random.Range(-1f, 1f);
            direction = new Vector2(x, y);
            this.transform.SetParent(null);
            isShot = true;
        }
        else
        {
            direction = nearestEnemy.transform.position - transform.position;
            this.transform.SetParent(null);
            isShot = true;
        }

        return true;
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    public void SetPierce(int p)
    {
        pierce = p;
    }

    private void Update()
    {
        // Check if the shuriken is not yet shot and if it's time to shoot
        if (!isShot)
        {
            shurikenShotTimer -= Time.deltaTime;
            if (shurikenShotTimer <= 0f)
            {
                ShootShuriken();
            }
        }

        // Rotate the shuriken
        this.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Move the shuriken if it has been shot
        if (isShot)
        {
            direction.Normalize(); // Normalize the direction to have a magnitude of 1
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If the enemy hasn't been damaged yet, damage it and decrease pierce count
            if (!damagedEnemies.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                if (pierce > 0)
                {
                    pierce -= 1;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            damagedEnemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the enemy from the damagedEnemies HashSet when it exits the trigger
        if (damagedEnemies.Contains(collision.gameObject))
        {
            damagedEnemies.Remove(collision.gameObject);
        }
    }
}
