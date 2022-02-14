using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float mana;
    [SerializeField] Transform gunsCollection;

    public void AddMana(float mana)
    {
        this.mana += mana;
        Debug.Log($"mana: {mana}");
    }

    public void AcquireGun(GunController gun)
    {
        gun.transform.SetParent(gunsCollection);
    }
}
