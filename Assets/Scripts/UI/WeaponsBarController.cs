using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsBarController : MonoBehaviour
{
    [SerializeField] GameObject weaponsBarItemPrefab;

    public void AddWeapon(GunController gunController)
    {
        WeaponsBarItemController weaponsBarItemController = Instantiate(weaponsBarItemPrefab, this.transform).GetComponent<WeaponsBarItemController>();
        weaponsBarItemController.SetGunController(gunController);
    }
}
