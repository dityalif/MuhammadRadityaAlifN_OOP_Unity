using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    void Start()
    {
        // Ensure health component exists
        if (health == null)
        {
            health = GetComponent<HealthComponent>();
        }
    }

    // Damage method that takes a Bullet
    public void Damage(Bullet bullet)
    {
        if (health != null)
        {
            health.Subtract(bullet.damage);
        }
    }

    // Damage method that takes direct damage value
    public void Damage(int damage)
    {
        var invincibility = GetComponent<InvincibilityComponent>();
        if (invincibility == null || !invincibility.IsInvincible)
        {
            if (health != null)
            {
                health.Subtract(damage);
            }
        }
    }
}
