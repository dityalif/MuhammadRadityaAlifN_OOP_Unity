using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject horizontalEnemyPrefab;
    [SerializeField] private GameObject forwardEnemyPrefab;
    [SerializeField] private GameObject targetingEnemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 10;
    
    private float nextSpawnTime;
    private int currentEnemyCount;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnRandomEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnRandomEnemy()
    {
        // Random value between 0 and 1
        float random = Random.value;
        GameObject enemyPrefab;

        // Split probability equally between 3 enemy types
        if (random < 0.33f)
        {
            enemyPrefab = horizontalEnemyPrefab;
        }
        else if (random < 0.66f)
        {
            enemyPrefab = forwardEnemyPrefab;
        }
        else
        {
            enemyPrefab = targetingEnemyPrefab;
        }
        
        GameObject enemy = Instantiate(enemyPrefab);
        currentEnemyCount++;
    }

    // Call this when an enemy is destroyed
    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
