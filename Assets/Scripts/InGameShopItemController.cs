using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameShopItemController : MonoBehaviour
{
    InGameShopItemScriptable data;

    [SerializeField] Image image;
    [SerializeField] TMP_Text textField;

    public void SetData(InGameShopItemScriptable data)
    {
        this.data = data;

        image.sprite = data.sprite;
        textField.text = data.description;
    }

    public void Acquire()
    {
        GameManagerController.instance.AcquireInGameItem(data);
    }
}
