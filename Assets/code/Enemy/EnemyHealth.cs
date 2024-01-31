using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // The maximum health of the enemy
    public int currentHealth; // The current health of the enemy
    public GameObject floatingTextPrefab; // Prefab for displaying floating damage numbers.
    public GameObject coinPrefab; // Prefab for the coin dropped upon enemy's death.

    private void Start()
    {
        currentHealth = maxHealth; // Initialize the enemy's health to the maximum health.
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce the enemy's health by the damage amount.
        ShowFloatingText(damage.ToString(), Color.red); // Show floating text to indicate damage taken.

        if (currentHealth <= 0)
        {
            DropCoinRandomly(); // Drop a coin upon the enemy's death.
            Destroy(gameObject); // Destroy the enemy game object when health reaches zero.
        }
    }

    private void ShowFloatingText(string text, Color textColor)
    {
        // Instantiate floating text to show damage numbers at the enemy's position.
        GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        FloatingText floatingText = floatingTextObject.GetComponent<FloatingText>();
        floatingText.SetText(text, textColor);
    }

    private void DropCoinRandomly()
    {
        // Generate a random number between 0 and 1.
        float randomValue = Random.Range(0f, 1f);

        // If the random value is less than or equal to 0.5 (50% chance), drop a coin.
        if (randomValue <= 0.5f)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
