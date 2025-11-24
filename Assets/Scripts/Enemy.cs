using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public enum EnemyShootPattern
    {
        Continuous,   
        Burst,        
        Spread,       
        Explosion     
    }

    private Stats enemyStats;
    private Shooter shooter;
    public EnemyMovementPattern movementPattern;
    public Transform enemyFirePoint;
    private Vector2 firePoint;
    public Transform player;
    private string bulletTag = "Enemy";

    private EnemyShootPattern patronDisparo;


    public void Setup(Stats stats, EnemyShootPattern patron)
    {
        enemyStats = stats;
        patronDisparo = patron;
        StartCoroutine(enemyShoot(patron));
    }

    private void Start()    
    {
        firePoint = new Vector2(enemyFirePoint.position.x, enemyFirePoint.position.y);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shooter = GetComponent<Shooter>();
        movementPattern = GetComponent<EnemyMovementPattern>();
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
                    yield return StartCoroutine(BurstShoot(3, 0.4f));
                    yield return new WaitForSeconds(2f);
                    break;

                case EnemyShootPattern.Spread:
                    yield return new WaitForSeconds(enemyStats.cadencyShoot());
                    SpreadShoot(5, 15f); 
                    break;

                case EnemyShootPattern.Explosion:
                    yield return new WaitForSeconds(2f);
                    ExplosionShoot(16); 
                    break;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomBorder"))
        {
            gameObject.SetActive(false);
        }
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
        yield return new WaitForSeconds(delay);
        ShootTowardsPlayer();
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
    gameObject.SetActive(false);
    
}

}
