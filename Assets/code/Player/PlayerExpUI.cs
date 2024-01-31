using UnityEngine;
using UnityEngine.UI;

public class PlayerExpUI : MonoBehaviour
{
    public PlayerStatus playerStatus; // Reference to the PlayerStatus script
    public Slider expSlider; // Reference to the UI slider for displaying experience

    private void Start()
    {
        // Set the maximum value of the slider to the player's maximum experience
        expSlider.maxValue = playerStatus.maxExp;

        // Set the initial value of the slider to the player's current experience
        expSlider.value = playerStatus.exp;
    }

    private void Update()
    {
        // Update the maximum value of the slider in case it changes in real-time
        expSlider.maxValue = playerStatus.maxExp;

        // Update the value of the slider to reflect the player's current experience
        expSlider.value = playerStatus.exp;
    }
}
