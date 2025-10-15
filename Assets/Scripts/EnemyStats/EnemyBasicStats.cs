using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicStats : Stats
{
    public int enemyMaxHealth = 3;
    public float enemyCadencyShoot = 0.5f;
    public float enemySpeed = 2f;
    public float enemySpeedShoot = 4f;
    public int enemyBulletDamage = 1;
    public Vector2 enemyBulletDirection = new Vector2(0, -1);//Ya lo define el Enemy

    public override int bulletDamage()
    {
        return enemyBulletDamage;
    }

    public override Vector2 bulletDirection()
    {
        return enemyBulletDirection;
    }

    public override float cadencyShoot()
    {
        return enemyCadencyShoot;
    }

    public override float speed()
    {
        return enemySpeed;
    }

    public override float speedShoot()
    {
        return enemySpeedShoot;
    }

    public override int maxHealth()
    {
        return enemyMaxHealth;
    }
}
