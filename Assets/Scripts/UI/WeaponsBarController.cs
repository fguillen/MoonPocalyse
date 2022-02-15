using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsBarController : MonoBehaviour
{
    [SerializeField] GameObject weaponsBarItemPrefab;

    public void AddWeapon(GunScriptable gunData)
    {
        WeaponsBarItemController weaponsBarItemController = Instantiate(weaponsBarItemPrefab, this.transform).GetComponent<WeaponsBarItemController>();
        weaponsBarItemController.SetGunData(gunData);
    }
}
