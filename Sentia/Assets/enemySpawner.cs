using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Enemy prefab to spawn
    public GameObject BossPrefab;
    public Transform[] spawnPoints; // Array of spawn positions

    public float initialSpawnRate = 3f; // Start spawning every 3 seconds
    public float minSpawnRate = 0.5f;   // Minimum spawn interval
    public float spawnAcceleration = 0.95f; // Rate of acceleration (reduces delay)

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // Infinite loop
        {
            yield return new WaitForSeconds(currentSpawnRate);
            
            SpawnEnemy(); // Spawn an enemy
            
            // Reduce spawn rate gradually, but never below minSpawnRate
            currentSpawnRate *= spawnAcceleration;
            currentSpawnRate = Mathf.Max(currentSpawnRate, minSpawnRate);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return; // Ensure spawn points exist

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void SpawnBoss()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(BossPrefab, spawnPoint.position, Quaternion.identity);
    }
}
