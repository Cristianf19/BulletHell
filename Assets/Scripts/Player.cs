using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Stats playerStats;

    private Shooter shooter;
    private float nextShootTime = 0f;
    public Transform playerFirePoint;
    private Vector2 playerBulletDirection = new Vector2(0, 1);
    private string bulletTag = "Player";


    void Start()
    {
        playerStats = GetComponent<Stats>();
        shooter = GetComponent<Shooter>();
        HealthManager hm = GetComponent<HealthManager>();
        hm.Setup(playerStats);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Move(playerDirection, playerStats.speed());

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Vector2 firePoint = new Vector2(playerFirePoint.position.x, playerFirePoint.position.y);
            shooter.Shoot(firePoint, playerBulletDirection, playerStats.speedShoot(), playerStats.bulletDamage(), bulletTag);
            nextShootTime = Time.time + playerStats.cadencyShoot();
        }
    }

}
