using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Transform> spawnerTransforms;
    [SerializeField] List<EnemyScriptable> enemiesData;

    Dictionary<EnemyScriptable, float> spawnClock = new Dictionary<EnemyScriptable, float>();

    void Awake()
    {
        foreach (var enemyData in enemiesData)
            spawnClock.Add(enemyData, 0);
    }

    void Update()
    {
        FollowPlayer();
        CheckSpawns();
    }

    void CheckSpawns()
    {
        foreach (var enemyData in enemiesData)
        {
            CheckSpawnForEnemy(enemyData);
        }
    }

    void CheckSpawnForEnemy(EnemyScriptable enemyData)
    {
        if(enemyData.minPlayerLevel > GameManagerController.Instance.playerController.level) return;
        if(enemyData.SpawnsPerSecond(GameManagerController.Instance.gameTime) == 0) return;

        if(Time.time > spawnClock[enemyData])
            SpawnEnemy(enemyData);
    }

    void FollowPlayer()
    {
        transform.position = GameManagerController.Instance.playerController.transform.position;
    }

    void SpawnEnemy(EnemyScriptable enemyData)
    {
        Transform spawnerTransform = spawnerTransforms[Random.Range(0, spawnerTransforms.Count)];

        EnemyController enemyController = Instantiate(enemyPrefab, spawnerTransform.position, Quaternion.identity, enemiesContainer.transform).GetComponent<EnemyController>();
        enemyController.SetEnemyData(enemyData);

        spawnClock[enemyData] = Time.time + (1 / enemyData.SpawnsPerSecond(GameManagerController.Instance.gameTime));
    }
}
