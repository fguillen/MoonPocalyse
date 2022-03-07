using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// From VampireSurvivors
// {
// 'minute': 0x5,
// 'minimum': 0xa,
// 'frequency': 0x3e8,
// 'enemies': [_0x5d86d9[_0x34fb19(0x681)]],
// 'bosses': [_0x5d86d9[_0x34fb19(0x365)]],
// 'events': [{
//     'eventType': _0x59e616[_0x34fb19(0x331)],
//     'delay': 0x4b0,
//     'chance': 0x46,
//     'repeat': 0x14
// }, {
//     'eventType': _0x59e616['BAT_SWARM'],
//     'delay': 0x8fc,
//     'chance': 0x46,
//     'repeat': 0x14
// }],
// 'treasure': {
//     'chances': [0x1, 0x5, 0x64],
//     'level': 0x1,
//     'prizeTypes': [_0xac3fec[_0x34fb19(0x1f7)], _0xac3fec[_0x34fb19(0x787)], _0xac3fec[_0x34fb19(0x787)], _0xac3fec[_0x34fb19(0x787)], _0xac3fec['EXISTING_ANY']]
// }

[Serializable]
public class EnemyChances : ItemChances<EnemyScriptable>
{
}

[Serializable]
public class LevelConfig
{
    public float gameSeconds;
    public float spawnsPerSecond;
    public int minEnemies;
    public List<EnemyChances> enemyChances = new List<EnemyChances>();

    public EnemyScriptable ChooseEnemyToSpawn()
    {
        List<ItemChances<EnemyScriptable>> listConverted = enemyChances.Cast<ItemChances<EnemyScriptable>>().ToList();
        return EnemyChances.ChooseItemByChances(listConverted);
    }

    List<EnemyScriptable> EligibleEnemies()
    {
        return enemyChances.Select( e => e.item ).Distinct().ToList();
    }

    public int MissingEnemiesCount(List<EnemyScriptable> enemiesPresent)
    {
        List<EnemyScriptable> eligibleEnemies = EligibleEnemies();

        return minEnemies - enemiesPresent.Where( e => eligibleEnemies.Contains(e) ).ToList().Count;
    }
}

[CreateAssetMenu]
public class LevelEnemiesConfigScriptable : ScriptableObject
{
    [SerializeField] List<LevelConfig> levelsConfig = new List<LevelConfig>();

    public LevelConfig ActualLevelConfing(float atSecond)
    {
        LevelConfig levelConfig =
            levelsConfig.
                Where( e => e.gameSeconds <= atSecond ).
                OrderBy( e => e.gameSeconds ).
                LastOrDefault();

        return levelConfig;
    }
}
