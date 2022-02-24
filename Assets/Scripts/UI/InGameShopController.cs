using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameShopController : MonoBehaviour
{
    [SerializeField] GameObject inGameShopItemPrefab;
    [SerializeField] GameObject components;
    PlayerGunsList playerGunsList;

    void Awake()
    {
        playerGunsList = PlayerGunsList.Instance;
    }

    void Start()
    {
        components.transform.localPosition = new Vector2(0, 1100);
    }

    [ContextMenu("Show()")]
    public void Show()
    {
        PopulateButtons();
        components.transform.DOLocalMove(Vector3.zero, 0.5f).SetUpdate(true);
        GameManagerController.Instance.SetPaused(true);
    }

    public void Hide()
    {
        components.transform.DOLocalMove(new Vector2(0, 1100), 0.5f).SetUpdate(true);
        GameManagerController.Instance.SetPaused(false);
    }

    public void PopulateButtons()
    {
        // Remove old Children
        foreach (Transform child in components.transform)
            GameObject.Destroy(child.gameObject);

        System.Random rnd = new System.Random();
        List<InGameShopItemData> randomInGameShopItems = AllPossibleShopItems().OrderBy(x => rnd.Next()).Take(3).ToList();
        foreach (var inGameShopItem in randomInGameShopItems)
        {
            InGameShopItemController item = Instantiate(inGameShopItemPrefab, components.transform).GetComponent<InGameShopItemController>();
            item.SetData(inGameShopItem);
        }
    }

    // List<GunScriptable> AllUpgradableGuns()
    // {
    //     List<GunScriptable> allGuns = GameManagerController.Instance.allGuns;
    //     List<GunScriptable> noUpgradableGuns = playerGunsList.NoUpgradable().Select( gunController => gunController.gunData ).ToList();
    //     List<GunScriptable> upgradableGuns = allGuns.Except(noUpgradableGuns).ToList();

    //     Debug.Log($"allGuns: {allGuns.Count}, noUpgradableGuns: {noUpgradableGuns.Count}, upgradableGuns: {upgradableGuns.Count}");

    //     return upgradableGuns;
    // }

    List<InGameShopItemData> AllPossibleShopItems()
    {
        List<GunScriptable> allGuns = GameManagerController.Instance.allGuns;
        List<GunScriptable> upgradableGuns = playerGunsList.Upgradable().Select( gunController => gunController.gunData ).ToList();
        List<GunScriptable> noUpgradableGuns = playerGunsList.NoUpgradable().Select( gunController => gunController.gunData ).ToList();
        List<GunScriptable> newGuns = allGuns.Except(upgradableGuns).Except(noUpgradableGuns).ToList();

        List<InGameShopItemData> inGameShopItems = new List<InGameShopItemData>();

        foreach (var gunController in playerGunsList.Upgradable())
        {
            InGameShopItemData inGameShopItem = new InGameShopItemData(gunController.gunData, gunController.level + 1);
            inGameShopItems.Add(inGameShopItem);
        }

        foreach (var gunData in newGuns)
        {
            InGameShopItemData inGameShopItem = new InGameShopItemData(gunData, 0);
            inGameShopItems.Add(inGameShopItem);
        }

        return inGameShopItems;
    }

}
