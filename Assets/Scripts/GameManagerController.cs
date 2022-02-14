using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController instance;
    public PlayerController playerController;
    public ManaBarController manaBarController;
    public InGameShopController inGameShopController;

    void Awake()
    {
        GameManagerController.instance = this;
    }

    public void ManaBarFull()
    {
        inGameShopController.Show();
    }

    public void AcquireInGameItem(InGameShopItemScriptable itemData)
    {
        inGameShopController.Hide();
        manaBarController.Reset();
    }
}
