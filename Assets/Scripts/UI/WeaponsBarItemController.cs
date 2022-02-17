using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsBarItemController : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetGunData(GunScriptable gunData)
    {
        image.sprite = gunData.gunSprite;
    }
}
