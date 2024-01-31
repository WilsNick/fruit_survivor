using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    public int damageAmount = 2; // Amount of damage dealt to the player
    private float canHitTimer = 0f; // Timer to control how often the enemy can hit the player
    private float hitCooldown = 0.2f; // Time delay between hits

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player when first colliding with them
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damageAmount);
            canHitTimer = hitCooldown; // Set the timer to the cooldown duration
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(canHitTimer <= 0)
            {
                // Deal damage to the player as long as the hit cooldown has elapsed
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damageAmount);
                canHitTimer = hitCooldown; // Reset the timer after hitting the player
            }
            else
            {
                canHitTimer -= Time.deltaTime; // Reduce the timer while still on cooldown
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            canHitTimer = 0f; // Reduce the timer while still on cooldown
        }
    }
}
