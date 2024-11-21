using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 10;

    void Start()
    {
        // Ensure collider is set to trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == gameObject.tag)
            return;

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();

        if (hitbox != null)
        {
            if (invincibility != null)
            {
                invincibility.StartInvincibility();
            }

            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else
            {
                hitbox.Damage(damage);
            }
        }
    }
}
