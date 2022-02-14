using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float mana;
    [SerializeField] Transform gunsCollection;
    [SerializeField] GameObject gunPrefab;
    [SerializeField] GunScriptable firstGun;

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
}
