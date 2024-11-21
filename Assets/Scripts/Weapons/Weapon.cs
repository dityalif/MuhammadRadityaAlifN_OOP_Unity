using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    private float shootIntervalInSeconds = 0.1f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private Coroutine shootCoroutine;

    public bool isPickedUp = false; 

    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet, 
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck, 
            defaultCapacity, 
            maxSize
        );
    }

    private void Update() {
        if (!isPickedUp) return; 

        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds) {
            timer = 0;
            Shoot();
        }
    }
    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.SetPool(objectPool);
        return bulletInstance;
    }

    private void OnGetBullet(Bullet bullet) {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation; 
    }

    private void OnReleaseBullet(Bullet bullet) {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet) {
        Destroy(bullet.gameObject);
    }

    private void Shoot()
    {
        if (objectPool == null) return;

        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;

        bulletObject.transform.position = bulletSpawnPoint.position;
        bulletObject.transform.rotation = bulletSpawnPoint.rotation;
    }
}
