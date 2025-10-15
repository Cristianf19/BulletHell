using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public int playerMaxHealth = 10;
    public float playerCadencyShoot = 0.2f;
    public float playerSpeed = 5f;
    public float playerSpeedShoot = 7f;
    public int playerBulletDamage = 1;
    public Vector2 playerBulletDirection = new Vector2(0, 1);
    public override int bulletDamage()
    {
        return playerBulletDamage;
    }

    public override Vector2 bulletDirection()
    {
        return playerBulletDirection;
    }

    public override float cadencyShoot()
    {
        return playerCadencyShoot;
    }

    public override float speed()
    {
        return playerSpeed;
    }

    public override float speedShoot()
    {
        return playerSpeedShoot;
    }

    public override int maxHealth()
    {
        return playerMaxHealth;
    }
}
