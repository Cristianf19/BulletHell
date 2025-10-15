using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public SpawnEnemyManager spawnManager;

    private GameObject player;
    //private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(EnemyLoop), 0f, 1.5f);
    }

    void EnemyLoop()
    {
        if(player == null) CancelInvoke("EnemyLoop");
        int position = Random.Range(0, 3);
        spawnManager.SpawnEnemy(spawnManager.zigzagEnemyPrefab, position, spawnManager.enemyBasicStats);
    }
}
