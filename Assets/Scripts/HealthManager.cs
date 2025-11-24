using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    private Stats stats;

    [SerializeField] private HealthBar healthBar;
    private bool isPlayer; 

    void Awake()
    {
        isPlayer = CompareTag("Player");
    }

    public void Setup(Stats stat)
    {
        stats = stat;
        maxHealth = stats.maxHealth();
        currentHealth = maxHealth;
        if (isPlayer && healthBar != null)
        {
            healthBar.Setup(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (isPlayer && healthBar != null)
        {
            Debug.Log("Vida actual "+currentHealth);
            healthBar.UpdateHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Debug.Log(gameObject.name + " semurio");
        gameObject.SetActive(false);
    }
}
