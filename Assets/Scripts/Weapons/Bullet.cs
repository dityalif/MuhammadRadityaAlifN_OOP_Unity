using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    Rigidbody2D rb;
    public IObjectPool<Bullet> ObjectPool { get; set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        this.ObjectPool = pool;
    }

    void OnBecameInvisible()
    {
        if (ObjectPool != null)
        {
            ObjectPool.Release(this);
        }
    }
}