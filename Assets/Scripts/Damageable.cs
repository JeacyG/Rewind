using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int maxHealth = 10;
    
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(enemy);
    }
}
