using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemySpawnerScriptable enemySpawnerData;
    float nextEnemyAt;

    void Awake()
    {
        nextEnemyAt = Time.time;
    }

    void Update()
    {
        if(Time.time > nextEnemyAt)
            SpawnEnemy();
    }

    void SpawnEnemy()
    {
        EnemyController enemyController = Instantiate(enemyPrefab, this.transform).GetComponent<EnemyController>();
        enemyController.SetEnemyData(enemySpawnerData.enemyData);
        nextEnemyAt = Time.time + (1 / enemySpawnerData.enemiesPerSecond);
    }
}
