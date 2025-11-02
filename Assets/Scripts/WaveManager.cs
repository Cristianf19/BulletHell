using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public SpawnEnemyManager spawnManager;

    private GameObject player;

    [System.Serializable]
    public class SpawnEvent 
    {
        public EnemyType enemyType;
        public int spawnPointIndex;
        public int enemyCount;
        public float timeToSpawn;
        public float delayBetween;
    }

    public enum EnemyType
    {
        Basic,
        Zigzag,
        Bomb,
        Boss
    }

    public static class EnemyTypeHelper
    {
        public static string GetEnemyTypeString(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Basic: return "basicEnemy";
                case EnemyType.Zigzag: return "ZigzagEnemy";
                case EnemyType.Bomb: return "bombEnemy";
                case EnemyType.Boss: return "bossEnemy";
                default: return string.Empty;
            }
        }
    }

    public List<SpawnEvent> spawnEvents = new List<SpawnEvent>();

    private float levelTimer = 0f;

    private HashSet<SpawnEvent> triggeredEvents = new HashSet<SpawnEvent>();


    void Update()
    {
        levelTimer += Time.deltaTime;

        foreach (var spawn in spawnEvents)
        {
            if (triggeredEvents.Contains(spawn))
            {
                continue;
            }

            if (levelTimer >= spawn.timeToSpawn)
            {
                StartCoroutine(SpawnWave(spawn));
                triggeredEvents.Add(spawn);
            }
        }
    } 

    private System.Collections.IEnumerator SpawnWave(SpawnEvent spawnEv)
    {
        for (int i = 0; i < spawnEv.enemyCount; i++)
        {
            string enemyName = EnemyTypeHelper.GetEnemyTypeString(spawnEv.enemyType);
            spawnManager.SpawnEnemy(enemyName, spawnEv.spawnPointIndex, spawnManager.enemyBasicStats);

            if (spawnEv.delayBetween > 0f)
                yield return new WaitForSeconds(spawnEv.delayBetween);
        }
    }

}
