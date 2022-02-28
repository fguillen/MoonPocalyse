using UnityEngine;
using System;
using System.Collections.Generic;

public enum TrajectoryKind
{
    directional = 1,
    strike = 10,
    closestTarget = 20,
    orbital = 30,
    fromSky = 40
}

[Serializable]
public class GunLevel
{
    [Range(0, 100)] public int coldDownDecrease;
    [Range(0, 100)] public int damageIncrease;
    [Range(0, 100)] public int speedIncrease;
    [Range(0, 5)] public int numProjectilesIncrease;
    [Range(0, 10)] public int numHitsIncrease;

    public String StatsDescription()
    {
        List<string> stats = new List<string>();

        if(coldDownDecrease != 0)
            stats.Add($"Cold down -{coldDownDecrease}%");

        if(damageIncrease != 0)
            stats.Add($"Damage +{damageIncrease}%");

        if(speedIncrease != 0)
            stats.Add($"Speed +{speedIncrease}%");

        if(numProjectilesIncrease != 0)
            stats.Add($"Num projectiles +{numProjectilesIncrease}");

        if(numHitsIncrease != 0)
            stats.Add($"Num hits +{numHitsIncrease}");

        return String.Join(", ", stats);
    }

    public void Apply(GunController gunController)
    {
        Debug.Log($"ApplyLevel: [{this}]");
        Debug.Log($"Level Stats: [{this.StatsDescription()}]");
        Debug.Log($"Before: [{gunController.StatsDescription()}]");
        gunController.coldDownSeconds = gunController.coldDownSeconds - (this.coldDownDecrease / 100f * gunController.coldDownSeconds);
        gunController.damage = gunController.damage + (this.damageIncrease / 100f * gunController.damage);
        gunController.numHits = gunController.numHits + this.numHitsIncrease;
        gunController.numProjectiles = gunController.numProjectiles + this.numProjectilesIncrease;
        gunController.speed = gunController.speed + (this.speedIncrease / 100f * gunController.speed);
        Debug.Log($"After: [{gunController.StatsDescription()}]");
    }
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScriptable : ScriptableObject
{
    public new string name;
    public string description;
    public GameObject bulletPrefab;
    public Sprite gunSprite;
    public TrajectoryKind trajectoryKind;

    public float coldDownSeconds;
    public float damage;
    public int numHits;
    public int numProjectiles;
    public float speed;
    public float angularSpeed;
    public float strikeRange;

    public List<GunLevel> levels;

    public string StatsDescription(int level)
    {
        if(level == 0)
        {
            return $"Cold down {coldDownSeconds}s, Damage: {damage}HP, Num projectiles: {numProjectiles}, Num hits: {numHits}, Speed: {speed}";
        }
        else
        {
            GunLevel gunLevel = levels[level - 1];
            return gunLevel.StatsDescription();
        }
    }
}
