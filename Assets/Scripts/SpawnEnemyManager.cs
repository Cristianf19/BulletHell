using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public GameObject spawner;
    List<Transform> spawnPoints = new List<Transform>();

    //public GameObject basicEnemyPrefab;
    //public GameObject zigzagEnemyPrefab;

    public Stats enemyBasicStats;


    void Start()
    {
        enemyBasicStats = GetComponent<Stats>();

        foreach (Transform spawnPointTransform in spawner.transform)
        {
            spawnPoints.Add(spawnPointTransform);
        }

    }

    public void SpawnEnemy(string poolTag, int spawnPoint, Stats enemyStats, Enemy.EnemyShootPattern shootPattern)
    {
        if (spawnPoint < 0 || spawnPoint >= spawnPoints.Count)
        {
            Debug.LogWarning("indice de spawn invalido");
            return;
        }

        GameObject enemy = PoolManager.Instance.SpawnFromPool(poolTag, spawnPoints[spawnPoint].position, Quaternion.identity);
        HealthManager hm = enemy.GetComponent<HealthManager>();
        hm.Setup(enemyStats);
        enemy.GetComponent<Enemy>().Setup(enemyStats, shootPattern);
    }
}
