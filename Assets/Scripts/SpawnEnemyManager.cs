using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public GameObject spawner;
    List<Transform> spawnPoints = new List<Transform>();

    public GameObject basicEnemyPrefab;
    public GameObject zigzagEnemyPrefab;

    public Stats enemyBasicStats;


    void Start()
    {
        enemyBasicStats = GetComponent<Stats>();

        foreach (Transform spawnPointTransform in spawner.transform)
        {
            spawnPoints.Add(spawnPointTransform);
        }

    }

    public void SpawnEnemy(GameObject enemyPrefab, int spawnPoint, Stats enemyStats)
    {
        if (spawnPoint < 0 || spawnPoint >= spawnPoints.Count)
        {
            Debug.LogWarning("Índice de spawn inválido");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
        HealthManager hm = enemy.GetComponent<HealthManager>();
        hm.Setup(enemyStats);
        enemy.GetComponent<Enemy>().Setup(enemyStats);
    }
}
