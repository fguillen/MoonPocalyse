using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ManaCollectedEvent : UnityEvent<int>
{
}


public class PlayerController : MonoBehaviour
{
    public PlayerMovementController playerMovementController;
    public int mana;
    [SerializeField] Transform gunsCollection;

    public ManaCollectedEvent manaCollectedEvent;

    void Awake()
    {
        playerMovementController = gameObject.GetComponent<PlayerMovementController>();

        if(manaCollectedEvent == null)
            manaCollectedEvent = new ManaCollectedEvent();
    }

    public void CollectGem(GemController gemController)
    {
        manaCollectedEvent.Invoke(gemController.mana);
        mana += gemController.mana;
        Debug.Log($"mana: {mana}");
    }

    public void AcquireGun(GunController gun)
    {
        gun.transform.SetParent(gunsCollection);
    }
}
