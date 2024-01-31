using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as per your preference

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Camera mainCamera; // Reference to the main camera
    public Animator animator; // Reference to the Animator component
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public bool right = false; // Flag to indicate if the player is facing right

    public bool IsRight() { return right; } // Getter method to check if the player is facing right

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component
        mainCamera = Camera.main; // Cache the main camera reference
    }

    private void FixedUpdate()
    {
        // Get input from the player
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Set the "Moving" parameter in the animator based on player input
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // Set the "Right" parameter in the animator based on player's facing direction
        if (moveHorizontal < 0)
        {
            right = false;
            animator.SetBool("Right", false);
        }
        else if (moveHorizontal > 0)
        {
            right = true;
            animator.SetBool("Right", true);
        }

        // Calculate movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize the movement vector to prevent faster diagonal movement
        movement = movement.normalized;

        // Move the player using the Rigidbody2D component
        rb.velocity = movement * moveSpeed;

        // Update the camera target position to follow the player
        Vector3 targetPosition = transform.position;
        targetPosition.z = mainCamera.transform.position.z; // Keep the camera's original z position
        mainCamera.GetComponent<CameraFollow>().target = transform;
        mainCamera.GetComponent<CameraFollow>().offset = new Vector3(0f, 0f, -10f); // Adjust the camera offset as needed
    }
}
