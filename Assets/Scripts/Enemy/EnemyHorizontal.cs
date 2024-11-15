using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHorizontal : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    private int direction; // 1 for right, -1 for left
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        Debug.Log($"Current scene: {SceneManager.GetActiveScene().name}");
        
        // Check current scene name
        // if (SceneManager.GetActiveScene().name != "Main")
        // {
        //     Debug.Log("Not in Main scene, destroying enemy");
        //     Destroy(gameObject);
        //     return;
        // }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        InitializeSpawnPosition();
        Debug.Log($"Enemy spawned at position: {transform.position} with direction: {direction}");
    }

    void InitializeSpawnPosition()
    {
        // Convert viewport points to world coordinates
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        // Randomly choose left (-1) or right (1) side
        direction = Random.Range(0, 2) * 2 - 1; // Results in either -1 or 1
        
        // Set initial position
        float spawnX = direction == -1 ? screenBounds.x : -screenBounds.x;
        float spawnY = Random.Range(-screenBounds.y, screenBounds.y);
        
        transform.position = new Vector2(spawnX, spawnY);
    }

    // Update is called once per frame
    void Update()
    {
        // Move horizontally based on direction
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        
        // Get current position in world coordinates
        Vector2 worldPosition = transform.position;
        
        // Check if enemy hits screen bounds and reverse direction
        if (worldPosition.x >= screenBounds.x || worldPosition.x <= -screenBounds.x)
        {
            direction *= -1; // Reverse direction
        }
    }
}
