using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject spawnedEnemy;

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

    private void Start()
    {
        // Spawn enemies immediately at the start
        SpawnEnemy();
        // Set the spawn interval to 6 seconds for subsequent spawns
        spawnInterval = 3f;
    }

    private void Update()
    {
        if (isSpawning)
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0f)
            {
                SpawnEnemy();
                spawnInterval = 6f; // Reset spawn interval
            }
        }

        // Check if total kills in the wave exceed the minimum required to increase spawn count
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCount += multiplierIncreaseCount;
            totalKillWave = 0; // Reset total kills for the wave
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
        }
        totalKillWave++; // Increment total kills in the wave
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }
}