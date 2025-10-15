using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    private Stats stats;

    public void Setup(Stats stat)
    {
        stats = stat;
        maxHealth = stats.maxHealth();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Debug.Log(gameObject.name + " semurio");
        Destroy(gameObject);
    }
}
