using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float mana;
    [SerializeField] Transform gunsCollectionTransform;
    [SerializeField] GameObject gunPrefab;

    public List<GunController> gunsCollection;
    public PlayerMovementController playerMovementController;

    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        gunsCollection = new List<GunController>();
    }

    public void AddMana(float mana)
    {
        this.mana += mana;
    }

    public GunController AcquireGun(GunScriptable gunData)
    {
        GunController gunController = gunsCollection.Find( gunController => gunController.name == gunData.name );
        if(gunController != null)
            UpgradeGun(gunController);
        else
           gunController = AddGun(gunData);

        return gunController;
    }

    GunController AddGun(GunScriptable gunData)
    {
        GunController gunController = Instantiate(gunPrefab, gunsCollectionTransform).GetComponent<GunController>();
        gunController.SetGunData(gunData);
        gunsCollection.Add(gunController);

        return gunController;
    }

    void UpgradeGun(GunController gunController)
    {
        gunController.Upgrade();
    }

    public Vector2 GetLastDirection()
    {
        return playerMovementController.lastDirection;
    }
}
