using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.5f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    public Transform parentTransform;

    private Coroutine shootCoroutine;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, 
            collectionCheck, defaultCapacity, maxSize);
    }

    void OnEnable()
    {
        StartShooting();
    }

    void OnDisable()
    {
        StopShooting();
    }

    private void StartShooting()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        shootCoroutine = StartCoroutine(ShootRoutine());
    }

    private void StopShooting()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (enabled)
        {
            Shoot();
            yield return new WaitForSeconds(shootIntervalInSeconds);
        }
    }

    private void Shoot()
    {
        if (objectPool == null) return;

        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;

        bulletObject.transform.position = bulletSpawnPoint.position;
        bulletObject.transform.rotation = bulletSpawnPoint.rotation;
        bulletObject.Initialize(bulletSpawnPoint.up, bulletObject.bulletSpeed);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    void OnDrawGizmos()
    {
        if (bulletSpawnPoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 direction = bulletSpawnPoint.up * 2f;
            Gizmos.DrawRay(bulletSpawnPoint.position, direction);
        }
    }

    void OnGetFromPool(Bullet bullet) {
        if (bullet == null) return;
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        Debug.Log($"Bullet spawned at position: {bullet.transform.position}");
    }

    void OnReleaseToPool(Bullet bullet) {
        if (bullet == null) return;
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject(Bullet bullet) {
        if (bullet == null) return;
        Destroy(bullet.gameObject);
    }
}
