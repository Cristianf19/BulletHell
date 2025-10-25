using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject player;
    public string poolTag = "Bullet";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot(Vector2 firePoint, Vector2 bulletDirection, float bulletSpeed, int bulletDamage, string ownerTag)
    {
        if (player == null || !player.activeInHierarchy) return;

        GameObject bullet = PoolManager.Instance.SpawnFromPool(poolTag, firePoint, Quaternion.identity);
        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().Setup(bulletDirection, bulletSpeed, bulletDamage, ownerTag);
        }
    }
}
