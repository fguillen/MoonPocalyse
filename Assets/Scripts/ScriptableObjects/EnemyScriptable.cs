using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

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
}
