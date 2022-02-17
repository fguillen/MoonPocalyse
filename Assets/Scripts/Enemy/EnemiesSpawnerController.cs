using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemySpawnerScriptable enemySpawnerData;
    float nextSpawnAt;
    GameObject enemiesContainer;

    void Awake()
    {
        nextSpawnAt = Time.time;

        enemiesContainer = GameObject.Find("EnemiesContainer");
        if(enemiesContainer == null)
        {
            Debug.Log("EnemiesContainer Object not found, fallback to Spawner");
            enemiesContainer = gameObject;
        }
    }

    void Update()
    {
        if(
            (Time.time > nextSpawnAt) &&
            !GameManagerController.Instance.isPaused
        )
            SpawnEnemy();
    }

    void SpawnEnemy()
    {
        int numEnemies = Random.Range(enemySpawnerData.numEnemiesPerSpawn.min, enemySpawnerData.numEnemiesPerSpawn.max);
        for (int i = 0; i < numEnemies; i++)
        {
            EnemyController enemyController = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemiesContainer.transform).GetComponent<EnemyController>();
            enemyController.SetEnemyData(enemySpawnerData.enemyData);
        }

        nextSpawnAt = Time.time + (1 / enemySpawnerData.spawnsPerSecond);
    }
}
