using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem", menuName = "Gem")]
public class GemScriptable : ScriptableObject
{
    public float mana;
    public string description;
    public Sprite sprite;
}
