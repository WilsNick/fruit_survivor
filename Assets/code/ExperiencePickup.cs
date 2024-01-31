using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    public int experienceValue = 1; // Adjust the value of the experience gained when picked up

    // Called when a collider enters the trigger attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Add the experience value to the player's status
            collision.GetComponent<PlayerStatus>().AddExperience(experienceValue);

            // Destroy the experience pickup object
            Destroy(gameObject);
        }
    }
}
