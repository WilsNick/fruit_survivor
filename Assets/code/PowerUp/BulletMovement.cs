using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Adjust the bullet's movement speed
    public int damage = 20; // Damage dealt by the bullet
    private Vector2 direction; // Direction in which the bullet moves
    public float shootInterval = 1f; // Time between shots
    public bool shot = false; // Flag to check if the bullet has been shot
    // private float shootTimer = 0f; // Timer for tracking the time between shots
    public float rotationSpeed = 500f; // Rotation speed of the bullet
    private Transform target; // Target enemy for the bullet to follow

    public void SetTarget(Transform targetTransform = null)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // Find the nearest enemy 
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy == null)
        {
            shot = false;
        }
        else
        {
            // Set the target to the nearest enemy and calculate the direction
            target = nearestEnemy.transform;
            direction = target.position - transform.position;
            this.transform.SetParent(null); // Detach the bullet from its parent (e.g., the turret)
        }
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    private void Update()
    {
        // Rotate the bullet
        this.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Reduce shoot interval timer
        shootInterval -= Time.deltaTime;

        if (!shot)
        {
            // If the shoot interval is over, set the target and shoot
            if (shootInterval <= 0f)
            {
                shot = true;
                SetTarget();
            }
        }

        if (shot)
        {
            direction.Normalize(); // Normalize the direction to have a magnitude of 1
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World); // Move the bullet in the specified direction
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy and destroy the bullet on collision
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Exp"))
        {
            // Destroy the bullet if it collides with anything other than an enemy, player, or experience object
            Destroy(gameObject);
        }
    }
}
