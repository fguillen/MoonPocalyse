using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyScriptable enemyData;
    EnemyHealthController enemyHealthController;
    EnemyMovementController enemyMovementController;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        enemyMovementController = GetComponent<EnemyMovementController>();
    }

    public void SetEnemyData(EnemyScriptable enemyData)
    {
        this.enemyData = enemyData;
        enemyHealthController.SetData(enemyData);
        enemyMovementController.speed = enemyData.speed;

        // Image
        spriteRenderer.sprite = enemyData.sprite;
    }
}
