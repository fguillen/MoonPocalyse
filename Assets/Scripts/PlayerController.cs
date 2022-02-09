using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ManaCollectedEvent : UnityEvent<int>
{
}


public class PlayerController : MonoBehaviour
{
    public CharacterMovementController characterMovementController;
    public ShootingController shootingController;
    public ManaCollectedEvent manaCollectedEvent;
    public int mana;

    void Awake()
    {
        characterMovementController = gameObject.GetComponent<CharacterMovementController>();
        shootingController = gameObject.GetComponent<ShootingController>();

        if(manaCollectedEvent == null)
            manaCollectedEvent = new ManaCollectedEvent();
    }

    public void CollectGem(GemController gemController)
    {
        manaCollectedEvent.Invoke(gemController.mana);
        mana += gemController.mana;
        Debug.Log($"mana: {mana}");
    }
}
