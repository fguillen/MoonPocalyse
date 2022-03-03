using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SpawnChance
{
    public float gameSeconds;
    public float spawnsPerSecond;
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public float life;
    public float defense;
    public Sprite sprite;
    public float speed;
    public float mass;
    public float damage;
    public int minPlayerLevel;

    public List<SpawnChance> spawnChances;

    public float SpawnsPerSecond(float atSecond)
    {
        if(spawnChances.Count == 0) return 0;

        SpawnChance spawnChance =
            spawnChances.
                Where( e => e.gameSeconds < atSecond ).
                OrderBy( e => e.gameSeconds ).
                LastOrDefault();

        return spawnChance == null ? 0 : spawnChance.spawnsPerSecond;
    }
}
