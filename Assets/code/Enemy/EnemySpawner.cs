using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public float spawnInterval = 5f; // Time between enemy spawns
    public int maxEnemies = 5; // Maximum number of enemies allowed on the screen

    private float timer;
    private Camera mainCamera;

    private void Start()
    {
        timer = spawnInterval; // Initialize the timer to the spawn interval.
        mainCamera = Camera.main; // Cache the main camera reference.
    }

    private void Update()
    {
        if (timer <= 0f && transform.childCount < maxEnemies)
        {
            SpawnEnemyOutsideView(); // Attempt to spawn an enemy.
            timer = spawnInterval; // Reset the timer after spawning.
        }

        timer -= Time.deltaTime; // Decrement the timer.
    }

    private void SpawnEnemyOutsideView()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to the EnemySpawner.");
            return; // No enemy prefabs assigned, so we cannot spawn an enemy.
        }

        // Randomly select an enemy prefab from the list
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Calculate a random position outside the camera's view
        Vector2 spawnPosition = Vector2.zero;

        // Generate a random spawn position outside the camera's view
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // Left side
                spawnPosition = new Vector2(-0.1f, Random.Range(0f, 1f));
                break;
            case 1: // Right side
                spawnPosition = new Vector2(1.1f, Random.Range(0f, 1f));
                break;
            case 2: // Top side
                spawnPosition = new Vector2(Random.Range(0f, 1f), 1.1f);
                break;
            case 3: // Bottom side
                spawnPosition = new Vector2(Random.Range(0f, 1f), -0.1f);
                break;
            default:
                break;
        }

        // Convert the spawn position from viewport space to world space
        spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, mainCamera.nearClipPlane));

        // Instantiate the enemy prefab at the calculated position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Set the parent of the enemy to this spawner (for organization in the scene hierarchy)
        enemy.transform.SetParent(transform);
    }
}
