using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private Vector2 bulletDirection;
    private float bulletSpeed;
    private int bulletDamage;
    private string bulletOwner;


    public void Setup(Vector2 direction, float speed, int damage, string owner)
    {
        bulletDirection = direction;
        bulletSpeed = speed;
        bulletDamage = damage;
        bulletOwner = owner; 

    }

    void Update()
    {
        Move(bulletDirection, bulletSpeed);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary") || collision.CompareTag("BottomBorder"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletOwner) || collision.CompareTag("Bullet")) return;

        HealthManager health = collision.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(bulletDamage);
            gameObject.SetActive(false);
        }
        
    }
}
