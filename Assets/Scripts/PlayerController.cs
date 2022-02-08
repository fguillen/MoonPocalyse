using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterMovementController characterMovementController;
    public ShootingController shootingController;

    void Awake()
    {
        this.characterMovementController = gameObject.GetComponent<CharacterMovementController>();
        this.shootingController = gameObject.GetComponent<ShootingController>();
    }
}
