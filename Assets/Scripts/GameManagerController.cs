using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    private static GameManagerController instance;
    public static GameManagerController Instance { get { return instance; } }

    public PlayerController playerController;
    public ManaBarController manaBarController;
    public InGameShopController inGameShopController;
    public WeaponsBarController weaponsBarController;
    public bool isPaused = false;

    [SerializeField] GunScriptable firstGun;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    void Start()
    {
        AcquireGun(firstGun);
    }

    public void ManaBarFull()
    {
        inGameShopController.Show();
    }

    public void AcquireGun(GunScriptable gunData)
    {
        inGameShopController.Hide();
        manaBarController.Reset();
        GunController gunController = playerController.AcquireGun(gunData);
        weaponsBarController.AddWeapon(gunController);
    }

    public void CollectMana(float mana)
    {
        playerController.AddMana(mana);
        manaBarController.AddMana(mana);
    }

    public void SetPaused(bool value)
    {
        isPaused = value;
    }
}
