using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public float life;
    public float defense;
    public Sprite sprite;
    public float speed;
}
