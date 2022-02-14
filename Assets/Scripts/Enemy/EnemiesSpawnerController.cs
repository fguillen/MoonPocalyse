using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemiesPerSecond;
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
        Instantiate(enemyPrefab, this.transform);
        nextEnemyAt = Time.time + (1 / enemiesPerSecond);
    }
}
