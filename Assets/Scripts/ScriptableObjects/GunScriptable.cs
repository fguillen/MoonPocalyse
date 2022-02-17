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
}
