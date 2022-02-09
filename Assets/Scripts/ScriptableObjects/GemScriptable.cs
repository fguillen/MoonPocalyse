using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem", menuName = "Gem")]
public class GemScriptable : ScriptableObject
{
    public int mana;
    public Sprite sprite;
}
