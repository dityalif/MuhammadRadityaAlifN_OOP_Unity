using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;
    private Transform player;
    private Vector3 screenBounds;

    void RandomizeSpawnPoint()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure there is an object with the tag 'Player' in the scene.");
        }
        RandomizeSpawnPoint();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // Keep the enemy within screen bounds
        // Vector3 pos = transform.position;
        // pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        // pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);
        // transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            Destroy(collision.gameObject); // Destroy the bullet
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}