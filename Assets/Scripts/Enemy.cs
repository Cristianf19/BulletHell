using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Stats enemyStats;
    private Shooter shooter;
    public EnemyMovementPattern movementPattern;
    public Transform enemyFirePoint;
    private Vector2 firePoint;
    public Transform player;
    private string bulletTag = "Enemy";


    public void Setup(Stats stats)
    {
        enemyStats = stats;
    }

    private void Start()    
    {
        firePoint = new Vector2(enemyFirePoint.position.x, enemyFirePoint.position.y);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shooter = GetComponent<Shooter>();
        movementPattern = GetComponent<EnemyMovementPattern>();
        StartCoroutine(enemyShoot(EnemyShootPattern.Continuous));
    }

    void Update()
    {
        if (movementPattern != null)
        {
            Vector2 dir = movementPattern.GetDirection();
            Move(dir, enemyStats.speed());
        }
        firePoint = new Vector2(enemyFirePoint.position.x, enemyFirePoint.position.y);
    }

    /* IEnumerator enemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyStats.cadencyShoot());
            Vector2 bulletDirection = ((Vector2)player.position - firePoint).normalized;
            shooter.Shoot(firePoint, bulletDirection, enemyStats.speedShoot(), enemyStats.bulletDamage(), bulletTag);
        }
    } */

    IEnumerator enemyShoot(EnemyShootPattern pattern)
    {
        while (true)
        {
            switch (pattern)
            {
                case EnemyShootPattern.Continuous:
                    yield return new WaitForSeconds(enemyStats.cadencyShoot());
                    ShootTowardsPlayer();
                    break;

                case EnemyShootPattern.Burst:
                    yield return StartCoroutine(BurstShoot(3, 0.2f));
                    yield return new WaitForSeconds(2f);
                    break;

                case EnemyShootPattern.Spread:
                    yield return new WaitForSeconds(enemyStats.cadencyShoot());
                    SpreadShoot(5, 15f); 
                    break;

                case EnemyShootPattern.Explosion:
                    yield return new WaitForSeconds(enemyStats.cadencyShoot());
                    ExplosionShoot(12); 
                    break;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomBorder"))
        {
            StopCoroutine(enemyShoot(EnemyShootPattern.Continuous));
            Destroy(gameObject);
        }
    }

    public enum EnemyShootPattern
    {
        Continuous,   
        Burst,        
        Spread,       
        Explosion     
    }


void ShootTowardsPlayer()
{
    Vector2 bulletDirection = ((Vector2)player.position - firePoint).normalized;
    shooter.Shoot(firePoint, bulletDirection, enemyStats.speedShoot(), enemyStats.bulletDamage(), bulletTag);
}

IEnumerator BurstShoot(int shots, float delay)
{
    for (int i = 0; i < shots; i++)
    {
        ShootTowardsPlayer();
        yield return new WaitForSeconds(delay);
    }
}

void SpreadShoot(int bulletCount, float angleStep)
{
    Vector2 bulletDirection = ((Vector2)player.position - firePoint).normalized;
    float baseAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
    
    int half = bulletCount / 2;
    for (int i = -half; i <= half; i++)
    {
        float angle = baseAngle + (i * angleStep);
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        shooter.Shoot(firePoint, dir, enemyStats.speedShoot(), enemyStats.bulletDamage(), bulletTag);
    }
}

void ExplosionShoot(int bulletCount)
{
    float angleStep = 360f / bulletCount;
    for (int i = 0; i < bulletCount; i++)
    {
        float angle = i * angleStep;
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        shooter.Shoot(firePoint, dir, enemyStats.speedShoot(), enemyStats.bulletDamage(), bulletTag);
    }
}

}
