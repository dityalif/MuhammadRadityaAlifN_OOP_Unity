using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    private CombatManager combatManager;

    public int Health
    {
        get { return health; }
    }

    void Start()
    {
        health = maxHealth;
        combatManager = FindObjectOfType<CombatManager>();
    }

    public void Subtract(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            // if (combatManager != null)
            // {
            //     combatManager.OnEnemyKilled();
            // }
        }
    }
}
