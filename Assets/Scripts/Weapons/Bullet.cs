using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] public float bulletSpeed = 20f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;
    private Coroutine lifetimeCoroutine;
    public IObjectPool<Bullet> ObjectPool { get; set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Optimize physics
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void Initialize(Vector2 direction, float force)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D missing!");
            return;
        }
        
        // Stop any existing lifetime coroutine
        if (lifetimeCoroutine != null)
            StopCoroutine(lifetimeCoroutine);

        // Reset state
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        // Use direct velocity instead of AddForce
        rb.velocity = direction.normalized * force;
        Debug.Log($"Bullet velocity set to: {rb.velocity}");
        
        // Start lifetime countdown
        lifetimeCoroutine = StartCoroutine(DestroyAfterTime());
    }

    void OnEnable()
    {
        // Ensure proper initialization when enabled from pool
        if (rb != null && rb.velocity == Vector2.zero)
        {
            rb.velocity = transform.up * bulletSpeed;
            Debug.Log($"OnEnable velocity: {rb.velocity}");
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collision (e.g., damage enemy)
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (ObjectPool != null && gameObject.activeInHierarchy)
        {
            ObjectPool.Release(this);
        }
    }
}