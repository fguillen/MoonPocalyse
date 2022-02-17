using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsBarItemController : MonoBehaviour
{
    Image image;
    GunController gunController;
    [SerializeField] Transform levelsTransform;
    [SerializeField] GameObject WeaponBarItemLevelCheckedPrefab;
    [SerializeField] GameObject WeaponBarItemLevelUncheckedPrefab;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetGunController(GunController gunController)
    {
        this.gunController = gunController;
        this.gunController.onChangeEvent.AddListener(ReDraw);
        ReDraw();
    }

    void ReDraw()
    {
        image.sprite = gunController.gunData.gunSprite;

        // Remove old levels
        foreach (Transform child in levelsTransform.transform)
            GameObject.Destroy(child.gameObject);

        // Recreate levels
        for (int i = 0; i < gunController.gunData.levels.Count; i++)
        {
            if(gunController.level >= i)
                Instantiate(WeaponBarItemLevelCheckedPrefab, levelsTransform);
            else
                Instantiate(WeaponBarItemLevelUncheckedPrefab, levelsTransform);
        }
    }
}
