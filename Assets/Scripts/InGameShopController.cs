using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class InGameShopController : MonoBehaviour
{
    [SerializeField] List<InGameShopItemScriptable> inGameShopItems;
    [SerializeField] GameObject inGameItemPrefab;
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] RectTransform components;

    void Start()
    {
        components.localPosition = new Vector2(0, 1100);
    }

    [ContextMenu("Show()")]
    public void Show()
    {
        PopulateButtons();
        components.DOLocalMove(Vector3.zero, 0.5f);
    }

    public void Hide()
    {
        components.DOLocalMove(new Vector2(0, 1100), 0.5f);
    }

    public void PopulateButtons()
    {
        System.Random rnd = new System.Random();

        List<InGameShopItemScriptable> randomItems = inGameShopItems.OrderBy(x => rnd.Next()).Take(3).ToList();
        foreach (var itemData in randomItems)
        {
            InGameShopItemController item = Instantiate(inGameItemPrefab, buttonsPanel.transform).GetComponent<InGameShopItemController>();
            item.SetData(itemData);
        }
    }

}
