using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameShopItemController : MonoBehaviour
{
    GunScriptable gunData;

    [SerializeField] Image image;
    [SerializeField] TMP_Text textField;

    public void SetData(GunScriptable gunData)
    {
        this.gunData = gunData;

        image.sprite = gunData.gunSprite;
        textField.text = gunData.description;
    }

    public void Acquire()
    {
        GameManagerController.Instance.AcquireGun(gunData);
    }
}
