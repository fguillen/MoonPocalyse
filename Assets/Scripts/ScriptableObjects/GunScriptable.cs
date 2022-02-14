using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScriptable : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public BulletScriptable bulletData;
    public float coldDownSeconds;
}
