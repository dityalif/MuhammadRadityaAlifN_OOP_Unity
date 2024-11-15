using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyForward : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private int direction = -1; // -1 for down only

    void Start()
    {
        // if (SceneManager.GetActiveScene().name != "Main")
        // {
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
    }

    void InitializeSpawnPosition()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float spawnX = Random.Range(-screenBounds.x, screenBounds.x);
        float spawnY = screenBounds.y; // Start at top
        transform.position = new Vector2(spawnX, spawnY);
    }

    void Update()
    {
        // Move downward only
        transform.Translate(Vector2.up * direction * moveSpeed * Time.deltaTime);
        
        // Destroy when below screen
        if (transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
