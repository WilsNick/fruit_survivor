using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Adjust the enemy's movement speed
    private Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer component

    private void Start()
    {
        // Find the player's transform using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the sprite renderer component attached to the enemy
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check if the player is still available (not destroyed)
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();

            // Move the enemy in the direction of the player
            MoveEnemy(directionToPlayer);
        }
    }

    private void MoveEnemy(Vector3 moveDirection)
    {
        // Move the enemy in the specified direction
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Flip the sprite horizontally if moving to the right
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
