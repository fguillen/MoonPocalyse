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

    public void AcquireGun(GunScriptable gunData)
    {
        inGameShopController.Hide();
        manaBarController.Reset();
        playerController.AcquireGun(gunData);
    }

    public void CollectMana(float mana)
    {
        playerController.AddMana(mana);
        manaBarController.AddMana(mana);
    }
}
