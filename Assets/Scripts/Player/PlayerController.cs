using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float mana;
    [SerializeField] Transform gunsCollection;
    [SerializeField] GameObject gunPrefab;
    [SerializeField] GunScriptable firstGun;

    public PlayerMovementController playerMovementController;

    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        AcquireGun(firstGun);
    }

    public void AddMana(float mana)
    {
        this.mana += mana;
        Debug.Log($"mana: {mana}");
    }

    public void AcquireGun(GunScriptable gunData)
    {
        GunController gunController = Instantiate(gunPrefab, gunsCollection).GetComponent<GunController>();
        gunController.SetGunData(gunData);
    }

    public Vector2 GetLastDirection()
    {
        return playerMovementController.lastDirection;
    }
}
