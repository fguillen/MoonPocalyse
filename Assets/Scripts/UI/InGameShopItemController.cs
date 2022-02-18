using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameShopItemController : MonoBehaviour
{
    InGameShopItemData data;

    [SerializeField] Image image;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;

    public void SetData(InGameShopItemData data)
    {
        this.data = data;

        image.sprite = data.gunData.gunSprite;
        titleText.text = BuildTitle(data);
        descriptionText.text = data.gunData.StatsDescription(data.level);
    }

    public void Acquire()
    {
        GameManagerController.Instance.AcquireGun(data.gunData);
    }

    string BuildTitle(InGameShopItemData data)
    {
        if(data.level == 0)
        {
            return $"new! {data.gunData.name}";
        }
        else
        {
            return $"{data.gunData.name} - Level {data.level}";
        }
    }
}
