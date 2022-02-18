using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsBarController : MonoBehaviour
{
    [SerializeField] GameObject weaponsBarItemPrefab;
    [SerializeField] PlayerGunsList playerGunsList;

    void Awake()
    {
        playerGunsList = PlayerGunsList.Instance;
        playerGunsList.onListChanged.AddListener(ReDraw);
    }

    void ReDraw()
    {
        // Remove old Children
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);

        // Recreate Children
        foreach (GunController gunController in playerGunsList.guns)
            AddWeapon(gunController);
    }

    void AddWeapon(GunController gunController)
    {
        WeaponsBarItemController weaponsBarItemController = Instantiate(weaponsBarItemPrefab, transform).GetComponent<WeaponsBarItemController>();
        weaponsBarItemController.SetGunController(gunController);
    }


}
