using UnityEngine;

public class BulletMovementDirectionalController : BulletMovementBase
{
    Vector2 direction;
    float speed;
    float angularSpeed;
    float range;

    void Update()
    {
        CheckIfItMustBeDestroyed();
    }

    void CheckIfItMustBeDestroyed()
    {
        if(Vector3.Distance(GameManagerController.Instance.playerController.transform.position, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }

    public override void StartMovement(GunScriptable gunData)
    {
        direction = GameManagerController.Instance.playerController.playerMovementController.lastHorizontalDirection;
        speed = gunData.speed;
        angularSpeed = gunData.angularSpeed;
        range = gunData.range;

        if(direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public override void Move() {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }


}
