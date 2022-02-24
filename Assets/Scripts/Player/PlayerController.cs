using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float mana;
    public int level;
    [SerializeField] Transform gunsCollectionTransform;

    [SerializeField] GameObject gunPrefab;
    PlayerGunsList playerGunsList;
    [HideInInspector] public PlayerMovementController playerMovementController;

    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerGunsList = PlayerGunsList.Instance;
    }

    public void AddMana(float mana)
    {
        this.mana += mana;
    }

    public Vector2 GetLastDirection()
    {
        return playerMovementController.lastDirection;
    }

    public void AcquireGun(GunScriptable gunData)
    {
        GunController gunController = playerGunsList.ByName(gunData.name);
        if(gunController != null)
            playerGunsList.UpgradeGun(gunController);
        else
           AddGun(gunData);
    }

    void AddGun(GunScriptable gunData)
    {
        GunController gunController = Instantiate(gunPrefab, gunsCollectionTransform).GetComponent<GunController>();
        gunController.SetGunData(gunData);
        playerGunsList.Add(gunController);
    }

    public void NextLevel()
    {
        level ++;
    }
}
