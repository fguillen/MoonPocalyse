using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameShopController : MonoBehaviour
{
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
        GameManagerController.Instance.SetPaused(true);
    }

    public void Hide()
    {
        components.DOLocalMove(new Vector2(0, 1100), 0.5f);
        GameManagerController.Instance.SetPaused(false);
    }

    public void PopulateButtons()
    {
        // Remove old Children
        foreach (Transform child in buttonsPanel.transform)
            GameObject.Destroy(child.gameObject);

        System.Random rnd = new System.Random();
        List<GunController> randomGuns = AllUpgradableGuns().OrderBy(x => rnd.Next()).Take(3).ToList();
        foreach (var gunController in randomGuns)
        {
            InGameShopItemController item = Instantiate(inGameItemPrefab, buttonsPanel.transform).GetComponent<InGameShopItemController>();
            item.SetData(gunController.gunData);
        }
    }

    List<GunController> AllUpgradableGuns()
    {
        PlayerController playerController = GameManagerController.Instance.playerController;
        return playerController.gunsCollection.Where( gunController => gunController.level < gunController.gunData.levels.Count()).ToList();
    }

}
