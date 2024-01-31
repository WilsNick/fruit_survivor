using UnityEngine;
using System.Collections.Generic;

public class TomatoBomPower : MonoBehaviour
{
    public float moveSpeed = 3f; // Adjust the tomato's movement speed
    public int damage = 5;
    private float tomatoTimer = 2f;
    public Vector2 direction = Vector2.left;
    public Animator animator; // Reference to the Animator component
    public float countdownTimer = 0.2f;
    public float radius = 2f;
    public float force = 1000f;
    private HashSet<GameObject> pushedEnemies = new HashSet<GameObject>();
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    public GameObject explosionParticles;
    private bool exploded = false;

    public void SetDirection()
    {
        // Set a random direction for the tomato bomb to move in
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        direction = new Vector2(x, y);
        direction.Normalize();

        // Set the Animator parameters based on the direction of the movement
        if (x > 0)
        {
            animator.SetBool("TomatoRight", true);
        }
        animator.SetBool("TomatoMoving", true);
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    private void Update()
    {
        tomatoTimer -= Time.deltaTime;
        if (tomatoTimer > 0)
        {
            // Move the tomato bomb in the set direction during the initial timer
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool("TomatoMoving", false);

            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0 && !exploded)
            {
                exploded = true;
                Explode();
            }
        }
    }

    private void Explode()
    {
        // Instantiate explosion particles and destroy them after a delay
        GameObject spawnedParticle = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(spawnedParticle, 2);

        // Enable the collider to detect collisions for the explosion
        this.GetComponent<Collider2D>().enabled = true;

        // Perform explosion logic
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        // foreach (Collider2D nearbyObject in colliders)
        // {
        //     // Apply damage and force to nearby enemies
        //     // ...
        // }
        Destroy(gameObject, 0.1f); // Destroy the tomato bomb after a delay
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with enemies and apply damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!damagedEnemies.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            damagedEnemies.Add(collision.gameObject);
        }
    }
}
