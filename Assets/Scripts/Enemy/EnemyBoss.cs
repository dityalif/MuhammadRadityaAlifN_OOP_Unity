using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletForce = 20f;

    private int direction = 1; // 1 for right, -1 for left
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float shootTimer;
    private IObjectPool<Bullet> bulletPool;

    void Start()
    {
        // Initialize camera and screen bounds
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
            return;
        }

        // Create shoot point if missing
        if (shootPoint == null)
        {
            GameObject shootPointObj = new GameObject("ShootPoint");
            shootPoint = shootPointObj.transform;
            shootPoint.SetParent(transform);
            shootPoint.localPosition = new Vector3(0, -0.5f, 0);
        }

        // Validate bullet prefab
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!");
            enabled = false;
            return;
        }

        // Initialize bullet pool
        bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            defaultCapacity: 10,
            maxSize: 100
        );

        InitializeSpawnPosition();
        shootTimer = shootInterval;
    }

    void InitializeSpawnPosition()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        // Boss starts from top center
        float spawnX = 0f;
        float spawnY = screenBounds.y * 0.8f; // Spawn at 80% of screen height
        
        transform.position = new Vector2(spawnX, spawnY);
    }

    void Update()
    {
        // Horizontal movement
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        
        Vector2 worldPosition = transform.position;
        
        // Screen bounds check
        if (worldPosition.x >= screenBounds.x * 0.8f || worldPosition.x <= -screenBounds.x * 0.8f)
        {
            direction *= -1;
        }

        // Shooting logic
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    private Bullet CreateBullet()
    {
        GameObject bulletObj = Instantiate(bulletPrefab);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        return bullet;
    }
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Shoot()
    {
        if (bulletPool != null && shootPoint != null)
        {
            Bullet bullet = bulletPool.Get();
            if (bullet != null)
            {
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletForce;
            }
        }
    }
}
