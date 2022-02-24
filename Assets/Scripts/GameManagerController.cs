using UnityEngine;
using System.Collections.Generic;

public class GameManagerController : MonoBehaviour
{
    private static GameManagerController instance;
    public static GameManagerController Instance { get { return instance; } }

    public PlayerController playerController;
    public ManaBarController manaBarController;
    public InGameShopController inGameShopController;
    public WeaponsBarController weaponsBarController;
    public bool isPaused = false;
    public float gameTime = 0;

    [SerializeField] GunScriptable firstGun;
    [SerializeField] public List<GunScriptable> allGuns;
    [SerializeField] TestScriptable testScriptable;
    [SerializeField] PlayerLevelProgressionScriptable playerLevelProgressionData;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        testScriptable.name = "New name";
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    void Start()
    {
        inGameShopController.Hide();
        manaBarController.Reset();
        manaBarController.SetMaxMana(playerLevelProgressionData.XpNeededForNextLevel(playerController.level));
        playerController.AcquireGun(firstGun);
    }

    public void ManaBarFull()
    {
        inGameShopController.Show();
    }

    public void AcquireGun(GunScriptable gunData)
    {
        inGameShopController.Hide();
        manaBarController.Reset();
        manaBarController.SetMaxMana(playerLevelProgressionData.XpNeededForNextLevel(playerController.level));
        playerController.AcquireGun(gunData);
        playerController.NextLevel();
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
