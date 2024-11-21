using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTargeting : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform playerTransform;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private Collider2D enemyCollider;

    void Start()
    {
        // Get required components
        mainCamera = Camera.main;
        enemyCollider = GetComponent<Collider2D>();

        // Validate components
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
            return;
        }

        if (enemyCollider == null)
        {
            Debug.LogError("Collider2D component missing from EnemyTargeting!");
            enabled = false;
            return;
        }

        // Find player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
            enabled = false;
            return;
        }

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void InitializeSpawnPosition()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        // Random spawn position around screen edges
        float randomSide = Random.Range(0, 4); // 0:top, 1:right, 2:bottom, 3:left
        Vector2 spawnPos = Vector2.zero;
        
        switch (randomSide)
        {
            case 0: // top
                spawnPos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
                break;
            case 1: // right
                spawnPos = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 2: // bottom
                spawnPos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
                break;
            case 3: // left
                spawnPos = new Vector2(-screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
                break;
        }
        
        transform.position = spawnPos;
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Calculate direction to player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        
        // Move towards player
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
