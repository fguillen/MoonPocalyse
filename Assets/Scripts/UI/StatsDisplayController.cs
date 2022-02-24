using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StatsDisplayController : MonoBehaviour
{
    [SerializeField] GameObject statsDisplayItemPrefab;
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
        PopulateItems();
        components.transform.DOLocalMove(Vector3.zero, 0.5f).SetUpdate(true);
        GameManagerController.Instance.SetPaused(true);
    }

    public void Hide()
    {
        components.transform.DOLocalMove(new Vector2(0, 1100), 0.5f).SetUpdate(true);
        GameManagerController.Instance.SetPaused(false);
    }

    public void PopulateItems()
    {
        // Remove old Children
        foreach (Transform child in components.transform)
            GameObject.Destroy(child.gameObject);

        foreach (var gunController in playerGunsList.guns)
        {
            StatsDisplayItemController item = Instantiate(statsDisplayItemPrefab, components.transform).GetComponent<StatsDisplayItemController>();
            item.SetData(gunController);
        }
    }
}
