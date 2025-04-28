using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class SpawnEstensione
{

     public static void SpawnEnemies(this EnemyManager enemyManager, Transform[] spawnPoints, GameObject normalEnemyPrefab, GameObject bossEnemyPrefab, Transform[] bossSpawnPoints)
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject enemy = Object.Instantiate(normalEnemyPrefab, spawnPoint.position, Quaternion.identity);
            Nemici nemicoComponente = enemy.GetComponent<Nemici>();
            if (nemicoComponente != null)
            {
                Debug.Log("Nemico spawnato: " + nemicoComponente.name);
                enemyManager.AddEnemy(nemicoComponente); // Aggiungi alla lista
            }
        }

        // Spawna il boss
        bool bossExists = enemyManager.enemyList.Exists(enemy => enemy != null && enemy.CompareTag("Boss"));
        if (!bossExists)
        {
            int lastBossSpawnIndex= enemyManager.GetLastIndex();

            lastBossSpawnIndex = (lastBossSpawnIndex + 1) % bossSpawnPoints.Length;

            enemyManager.SetLastIndex(lastBossSpawnIndex);

            GameObject boss = Object.Instantiate(bossEnemyPrefab, bossSpawnPoints[lastBossSpawnIndex].position, Quaternion.identity);
            Nemici bossComponente = boss.GetComponent<Nemici>();
            if (bossComponente != null)
            {
            enemyManager.AddEnemy(bossComponente); // Aggiungi alla lista
            }
        }
    }
}
