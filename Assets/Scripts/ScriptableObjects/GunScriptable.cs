using UnityEngine;
using System;
using System.Collections.Generic;

public enum TrajectoryKind
{
    directional,
    closestTarget,
    circular,
    fromSky
}

[Serializable]
public class GunLevel
{
    [Range(0, 100)] public int coldDownDecrease;
    [Range(0, 100)] public int damageIncrease;
    [Range(0, 100)] public int speedIncrease;
    [Range(0, 5)] public int numProjectilesIncrease;
    [Range(0, 10)] public int numHitsIncrease;
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScriptable : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite gunSprite;
    public Sprite bulletSprite;
    public TrajectoryKind trajectoryKind;

    public float coldDownSeconds;
    public float damage;
    public int numHits;
    public float speed;

    public List<GunLevel> levels;

    public string StatsDescription(int level)
    {
        if(level == 0)
        {
            return $"Cold down {coldDownSeconds}s, Damage: {damage}HP, Num hits: {numHits}, Speed: {speed}";
        }
        else
        {
            List<string> stats = new List<string>();
            GunLevel gunLevel = levels[level - 1];

            if(gunLevel.coldDownDecrease != 0)
                stats.Add($"Cold down -{gunLevel.coldDownDecrease}%");

            if(gunLevel.damageIncrease != 0)
                stats.Add($"Damage +{gunLevel.damageIncrease}%");

            if(gunLevel.speedIncrease != 0)
                stats.Add($"Speed +{gunLevel.speedIncrease}%");

            if(gunLevel.numProjectilesIncrease != 0)
                stats.Add($"Num projectiles +{gunLevel.numProjectilesIncrease}");

            if(gunLevel.numHitsIncrease != 0)
                stats.Add($"Num hits +{gunLevel.numHitsIncrease}");

            return String.Join(", ", stats);
        }
    }
}
