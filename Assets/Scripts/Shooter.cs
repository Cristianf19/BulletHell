using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot(Vector2 firePoint, Vector2 bulletDirection, float bulletSpeed, int bulletDamage, string ownerTag)
    {
        if (player == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(bulletDirection, bulletSpeed, bulletDamage, ownerTag);
    }
}
