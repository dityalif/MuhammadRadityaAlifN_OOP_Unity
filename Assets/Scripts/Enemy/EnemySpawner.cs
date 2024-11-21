using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnWave()
    {
        int enemiesToSpawn = defaultSpawnCount * spawnCountMultiplier;
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 5f;
            randomPosition.y = transform.position.y;
            
            Enemy enemy = Instantiate(spawnedEnemy, randomPosition, Quaternion.identity);
            spawnCount++;
            
            if (enemy != null && combatManager != null)
            {
                enemy.OnEnemyDeath += HandleEnemyDeath;
                activeEnemies.Add(enemy);
            }
        }
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            enemy.OnEnemyDeath -= HandleEnemyDeath;
            activeEnemies.Remove(enemy);
            
            totalKill++;
            totalKillWave++;
            spawnCount--;

            if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
            {
                spawnCountMultiplier += multiplierIncreaseCount;
                totalKillWave = 0;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy != null)
            {
                enemy.OnEnemyDeath -= HandleEnemyDeath;
            }
        }
        activeEnemies.Clear();
        StopAllCoroutines();
    }
}
