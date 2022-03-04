using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [SerializeField] LevelEnemiesConfigScriptable levelConfigData;
    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemiesSetScriptable enemiesSetData;
    [SerializeField] List<Transform> spawnerTransforms;

    float lastSpawnAt = 0;

    void Update()
    {
        CheckSpawns();
    }

    void CheckSpawns()
    {
        LevelConfig levelConfig = levelConfigData.ActualLevelConfing(GameManagerController.Instance.gameTime);

        int enemiesToSpawn = levelConfig.MissingEnemiesCount(enemiesSetData.All().Select( e => e.enemyData ).ToList());
        for (int i = 0; i < enemiesToSpawn; i++)
            SpawnEnemy(levelConfig.ChooseEnemyToSpawn());

        if(Time.time > (lastSpawnAt + (1 / levelConfig.spawnsPerSecond)))
            SpawnEnemy(levelConfig.ChooseEnemyToSpawn());
    }

    void SpawnEnemy(EnemyScriptable enemyData)
    {
        Transform spawnerTransform = spawnerTransforms[Random.Range(0, spawnerTransforms.Count)];

        EnemyController enemyController = Instantiate(enemyPrefab, spawnerTransform.position, Quaternion.identity, enemiesContainer.transform).GetComponent<EnemyController>();
        enemyController.SetEnemyData(enemyData);

        lastSpawnAt = Time.time;
    }
}
