using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingGameController : MonoBehaviour
{
    // This method is called when "Start Game 1" button is clicked
    public void StartGame1()
    {
        // Load the scene "Nick" to start Game 1
        SceneManager.LoadScene("Nick");
    }

    // This method is called when "Start Game 2" button is clicked
    public void StartGame2()
    {
        // Load the scene "Senne" to start Game 2
        SceneManager.LoadScene("Senne");
    }

    // This method is called when "Start Game 3" button is clicked
    public void StartGame3()
    {
        // Load the scene "Stijn" to start Game 3
        SceneManager.LoadScene("Stijn");
    }
}
