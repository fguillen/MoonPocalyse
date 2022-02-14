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

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletScriptable : ScriptableObject
{
    public float damage;
    public Sprite sprite;
    public int numHits;
    public TrajectoryKind trajectoryKind;
    public float speed;
}
