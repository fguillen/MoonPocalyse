using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrajectoryKind
{
    directional,
    closestTarget,
    circular,
    fromSky
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScriptable : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite gunSprite;
    public Sprite bulletSprite;
    public float coldDownSeconds;
    public float damage;
    public int numHits;
    public TrajectoryKind trajectoryKind;
    public float speed;
}
