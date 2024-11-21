using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public event Action<Enemy> OnEnemyDeath;

    public void Die()
    {
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
