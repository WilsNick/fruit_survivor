using UnityEngine;
using System.Collections.Generic;

public class SeedPower : MonoBehaviour
{
    // Store the enemies that have already been damaged by this seed
    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    public float moveSpeed = 10f; // Adjust the seed's movement speed
    public int damage = 5;
    public int pierce = 0;
    private bool right = false;

    // Set the direction in which the seed will move
    public void SetDirection(bool isRight)
    {
        right = isRight;
    }

    // Set the damage value for the seed
    public void SetDamage(int value)
    {
        damage = value;
    }

    // Set the pierce value for the seed
    public void SetPierce(int value)
    {
        pierce = value;
    }

    private void Update()
    {
        // Move the seed in the appropriate direction
        Vector2 moveDirection = right ? Vector2.right : Vector2.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        Destroy(gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if the enemy has not been damaged by this seed yet
            if (!damagedEnemies.Contains(collision.gameObject))
            {
                // Apply damage to the enemy and handle pierce
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

            // Add the enemy to the damaged set to prevent double-damage
            damagedEnemies.Add(collision.gameObject);
        }
        else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Exp"))
        {
            // Destroy the seed when it collides with non-enemy objects
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the enemy from the damaged set when it exits the trigger
        if (damagedEnemies.Contains(collision.gameObject))
        {
            damagedEnemies.Remove(collision.gameObject);
        }
    }
}
