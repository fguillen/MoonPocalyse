using UnityEngine;

public abstract class BulletMovementBase : MonoBehaviour
{
    protected Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public abstract void StartMovement(GunScriptable gunData);
    public abstract void Move();
}
