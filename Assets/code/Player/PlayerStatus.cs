using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float damageCooldown = 0f; // Time between damage instances

    public int exp = 0;
    public int maxExp = 5;

    public int baseDamage = 5;
    public int GetDamage() { return baseDamage; }

    public float fireRateReduction = 0.5f;
    public float GetFireRateReduction() { return fireRateReduction; }

    public int pierce = 0;
    public int GetPierce() { return pierce; }

    private bool canTakeDamage = true; // Flag to check if the player can take damage

    private void Start()
    {
        currentHealth = maxHealth; // Set the player's health to maximum at the start.
    }

    public void AddExperience(int experience)
    {
        exp += experience;
        if (exp >= maxExp)
        {
            LevelUp(); // Perform actions for leveling up.
            exp = exp - maxExp;
            maxExp += 1;
            if (exp >= maxExp)
            {
                AddExperience(0); // If experience exceeds the new maxExp, repeat leveling up process.
            }
        }
    }

    private void LevelUp()
    {
        // Perform actions when the player levels up (e.g., upgrade abilities, increase stats).
        GameManager.Instance.LevelUp();

    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die(); // Perform actions for player death.
            }
            // canTakeDamage = false;
            // Invoke("ResetDamageCooldown", damageCooldown); // Reset damage cooldown after taking damage.
        }
    }

    private void ResetDamageCooldown()
    {
        canTakeDamage = true; // Reset the damage cooldown flag.
    }

    private void Die()
    {
        // Perform actions when the player dies (e.g., game over screen, restart level, etc.)
        Debug.Log("Player died!");
        GameManager.Instance.Lossed();
    }

    // Methods to upgrade abilities

    public void AddDamage()
    {
        baseDamage += 5;
    }

    public void ReduceFireRate()
    {
        fireRateReduction -= 0.05f;
    }

    public void AddPierce()
    {
        pierce += 1;
    }
}
