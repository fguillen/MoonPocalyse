using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsDisplayItemController : MonoBehaviour
{
    GunController gunController;

    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    public void SetData(GunController gunController)
    {
        this.gunController = gunController;

        image.sprite = gunController.gunData.gunSprite;
        text.text = gunController.StatsDescription();
    }
}
