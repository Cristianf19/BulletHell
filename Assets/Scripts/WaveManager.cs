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
                case EnemyType.Basic: return "BasicEnemy";
                case EnemyType.Zigzag: return "ZigzagEnemy";
                case EnemyType.Bomb: return "BombEnemy";
                case EnemyType.Boss: return "BossEnemy";
                default: return string.Empty;
            }
        }
        public static Enemy.EnemyShootPattern GetShootTypeString(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Basic: return Enemy.EnemyShootPattern.Burst;
                case EnemyType.Zigzag: return Enemy.EnemyShootPattern.Burst;
                case EnemyType.Bomb: return Enemy.EnemyShootPattern.Explosion;
                case EnemyType.Boss: return Enemy.EnemyShootPattern.Burst;
                default: return Enemy.EnemyShootPattern.Burst;
            }
        }
    }

    public List<SpawnEvent> spawnEvents = new List<SpawnEvent>();

    private float levelTimer = 0f;

    private HashSet<SpawnEvent> triggeredEvents = new HashSet<SpawnEvent>();


    private void Update()
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
            Enemy.EnemyShootPattern shootPattern = EnemyTypeHelper.GetShootTypeString(spawnEv.enemyType);
            spawnManager.SpawnEnemy(enemyName, spawnEv.spawnPointIndex, spawnManager.enemyBasicStats, shootPattern);

            if (spawnEv.delayBetween > 0f)
                yield return new WaitForSeconds(spawnEv.delayBetween);
        }
    }

}
