using UnityEngine;

public class BulletMovementClosestEnemyController : BulletMovementBase
{
    Vector2 direction;
    float speed;
    float range;

    [SerializeField] EnemiesSetScriptable enemiesSetData;

    public override void StartMovement(GunScriptable gunData)
    {
        speed = gunData.speed;
        range = gunData.range;

        EnemyController closestEnemyController = ClosestEnemy();

        if(closestEnemyController == null)
        {
            Destroy(gameObject);
            return;
        }

        direction = (closestEnemyController.transform.position - GameManagerController.Instance.playerController.transform.position).normalized;
        transform.right = closestEnemyController.transform.position - transform.position;
    }

    public override void Move() {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    EnemyController ClosestEnemy()
    {
        if(enemiesSetData.All().Count == 0)
            return null;

        Vector2 playerPosition = GameManagerController.Instance.playerController.transform.position;

        EnemyController closestEnemyController = null;
        float closestDistance = range; // Also return null if not close Enemy;

        foreach (var enemyController in enemiesSetData.All())
        {
            float distance = Vector2.Distance(playerPosition, enemyController.transform.position);
            if(closestDistance > distance)
            {
                closestEnemyController = enemyController;
                closestDistance = distance;
            }
        }

        return closestEnemyController;
    }
}
