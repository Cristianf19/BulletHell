using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPattern : MonoBehaviour
{
    private string bulletTag = "Enemy";
    private Shooter shooter;

    public void ExplosionShoot(int bulletCount, Vector2 firePoint, Stats enemyStats)
    {
        //int bulletCount = 8;
        float angle = 360f / bulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            shooter.Shoot(firePoint, direction, enemyStats.speedShoot(), enemyStats.bulletDamage(), bulletTag);
            currentAngle += angle;
        }
        
    }
}
