using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerStatus playerStatus; // Reference to the PlayerStatus script
    public Slider healthSlider; // Reference to the UI slider for displaying health

    private void Start()
    {
        // Set the maximum value of the slider to the player's maximum health
        healthSlider.maxValue = playerStatus.maxHealth;

        // Set the initial value of the slider to the player's current health
        healthSlider.value = playerStatus.currentHealth;
    }

    private void Update()
    {
        // Update the maximum value of the slider in case it changes in real-time
        healthSlider.maxValue = playerStatus.maxHealth;

        // Update the value of the slider to reflect the player's current health
        healthSlider.value = playerStatus.currentHealth;
    }
}
