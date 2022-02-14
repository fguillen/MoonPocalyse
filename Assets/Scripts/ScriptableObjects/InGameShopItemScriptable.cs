using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInGameShopItem", menuName = "InGameShopItem")]
public class InGameShopItemScriptable : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public GunScriptable gunData;
}
