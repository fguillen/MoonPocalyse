using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySpawner", menuName = "Enemy Spawner")]
public class EnemySpawnerScriptable : ScriptableObject
{
    public EnemyScriptable enemyData;
    public float enemiesPerSecond;
}
