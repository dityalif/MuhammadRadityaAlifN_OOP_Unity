using UnityEngine;
using UnityEngine.Analytics;

public class EnemyForward : Enemy
{
    public float speed = 5f;
    private Vector3 direction;
    private Vector3 spawnPoint;
    public int health = 3; // Add health property

    void Start()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        spawnPoint = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 1, 0);
        direction = Vector3.down;

        transform.position = spawnPoint;
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the enemy is off the screen and reset to spawn point
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (transform.position.y < -screenBounds.y)
        {
            transform.position = spawnPoint;
            transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 1, 0);
        }
    }




}