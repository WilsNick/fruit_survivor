using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isPauseLevelUp = false;
    private bool isGamePaused = false;

    public int maxExperience = 100; // Maximum experience required for leveling up
    public GameObject powerUpChoiceUI; // UI panel for power-up choices
    public GameObject powerUpChoicePanel; // Actual panel displaying power-up choices
    public int levelCount = 1; // Current level count
    public GameObject lossPanel; // UI panel for the loss screen
    public TextMeshProUGUI levelCounter; // Text displaying the current level count

    public static GameManager Instance { get; private set; }
    public List<TextMeshProUGUI> LevelUpText; // Text elements displaying power-up choices

    private void Awake()
    {
        Time.timeScale = 1f;

        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Check for pause input and call PauseGame method accordingly
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (!isGamePaused && !isPauseLevelUp)
        {
            // Pause the game if it is not already paused and not in the level up screen
            isGamePaused = true;
            Time.timeScale = 0f;
        }
        else if (isGamePaused && !isPauseLevelUp)
        {
            // Resume the game if it is paused and not in the level up screen
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    public void LevelUp()
    {
        // Enter the level up screen and show power-up choices
        isPauseLevelUp = true;
        Time.timeScale = 0f;
        levelCount += 1;
        levelCounter.text = levelCount.ToString();

        // Get power-up choices from the PowerUps script and display them on the UI
        List<string> functionNames = this.transform.GetComponent<PowerUps>().SelectLevelup();
        for (int i = 0; i < functionNames.Count; i++)
        {
            LevelUpText[i].text = functionNames[i];
        }
        powerUpChoiceUI.SetActive(true);
    }

    public void ResumeGame()
    {
        // Resume the game and exit the level up screen
        isGamePaused = false;
        isPauseLevelUp = false;

        Time.timeScale = 1f;
        powerUpChoiceUI.SetActive(false);
    }

    public void Lossed()
    {
        // Show the loss screen
        isPauseLevelUp = true;
        Time.timeScale = 0f;
        lossPanel.SetActive(true);
    }

    public void ResetGame()
    {
        // Reset the game by reloading the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
