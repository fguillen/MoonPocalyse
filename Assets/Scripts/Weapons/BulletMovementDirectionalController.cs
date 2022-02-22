using UnityEngine;

public class BulletMovementDirectionalController : BulletMovementBase
{
    public override void StartMovement(GunScriptable gunData)
    {
        Vector2 direction = GameManagerController.Instance.playerController.playerMovementController.lastHorizontalDirection;

        if(direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        rBody.velocity = direction * gunData.speed;
        rBody.angularVelocity = gunData.angularSpeed;
    }

    public override void Move() {}
}
