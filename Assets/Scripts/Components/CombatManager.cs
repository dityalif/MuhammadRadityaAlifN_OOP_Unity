using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    void Start()
    {
        InitializeSpawners();
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTotalEnemies();
        
        if (timer >= waveInterval)
        {
            StartNewWave();
        }
    }

    private void InitializeSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.combatManager = this;
            }
        }
    }

    private void UpdateTotalEnemies()
    {
        totalEnemies = 0;
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                totalEnemies += spawner.spawnCount;
            }
        }
    }

    private void StartNewWave()
    {
        waveNumber++;
        timer = 0;
        
        // Optionally increase difficulty per wave
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.spawnCountMultiplier++;
            }
        }
    }

    public void EnemyDefeated()
    {
        UpdateTotalEnemies();
    }
}
