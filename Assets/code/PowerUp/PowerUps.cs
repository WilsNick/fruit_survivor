using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUps : MonoBehaviour
{
    public List<string> powerups; // List of available power-ups
    private Dictionary<string, UnityAction> nameToPower = new Dictionary<string, UnityAction>();

    // Power-up amount for each type
    private int DamageAmount = 5;
    private int FireRateAmount = 5;
    private int PierceAmount = 5;
    private int ShurikenAmount = 5;
    private int SeedAmount = 3;
    private int TomatoBomAmount = 3;

    public PlayerStatus playerStatus; // Reference to the PlayerStatus script
    public SeedFactory seedFactory; // Reference to the SeedFactory script
    public TomatoBomFactory tomatoBomFactory; // Reference to the TomatoBomFactory script
    public ShurikenFactory shurikensFactory; // Reference to the ShurikenFactory script

    private int amountOfChoices = 3; // Number of power-up choices presented to the player

    private void Start()
    {
        // Initialize the list of power-ups and corresponding actions
        powerups = new List<string> { "Damage", "FireRate", "Pierce", "Shuriken", "Seed", "TomatoBom" };
        nameToPower.Add("Damage", Damage);
        nameToPower.Add("FireRate", FireRate);
        nameToPower.Add("Pierce", Pierce);
        nameToPower.Add("Shuriken", Shuriken);
        nameToPower.Add("Seed", Seed);
        nameToPower.Add("TomatoBom", TomatoBom);
    }

    // Power-up method for increasing damage
    private void Damage()
    {
        Debug.Log("Damage");
        DamageAmount -= 1;
        if (DamageAmount <= 0)
        {
            powerups.Remove("Damage");
        }
        playerStatus.AddDamage();
    }

    // Power-up method for reducing fire rate
    private void FireRate()
    {
        Debug.Log("FireRate");
        FireRateAmount -= 1;
        if (FireRateAmount <= 0)
        {
            powerups.Remove("FireRate");
        }
        playerStatus.ReduceFireRate();
    }

    // Power-up method for adding pierce
    private void Pierce()
    {
        Debug.Log("Pierce");
        PierceAmount -= 1;
        if (PierceAmount <= 0)
        {
            powerups.Remove("Pierce");
        }
        playerStatus.AddPierce();
    }

    // Power-up method for upgrading shuriken
    private void Shuriken()
    {
        Debug.Log("Shuriken");
        ShurikenAmount -= 1;
        if (ShurikenAmount <= 0)
        {
            powerups.Remove("Shuriken");
        }
        shurikensFactory.LevelUp();
    }

    // Power-up method for upgrading seed
    private void Seed()
    {
        Debug.Log("Seed");
        SeedAmount -= 1;
        if (SeedAmount <= 0)
        {
            powerups.Remove("Seed");
        }
        seedFactory.LevelUp();
    }

    // Power-up method for upgrading tomato bomb
    private void TomatoBom()
    {
        Debug.Log("TomatoBom");
        TomatoBomAmount -= 1;
        if (TomatoBomAmount <= 0)
        {
            powerups.Remove("TomatoBom");
        }
        tomatoBomFactory.LevelUp();
    }

    // Method to invoke the selected power-up
    public void LevelupChoice(int choice)
    {
        nameToPower[powerups[powerups.Count - amountOfChoices + choice]].Invoke();
    }

    // Method to select power-ups to be presented to the player
    public List<string> SelectLevelup()
    {
        List<string> choices = new List<string>();
        for (var i = 0; i < amountOfChoices; i++)
        {
            int choice = Random.Range(0, powerups.Count);
            choices.Add(powerups[choice]);
            powerups.RemoveAt(choice);
        }
        for (var i = 0; i < amountOfChoices; i++)
        {
            powerups.Add(choices[i]);
        }
        return choices;
    }
}
