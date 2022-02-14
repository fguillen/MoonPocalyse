using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyScriptable enemyData;
    EnemyHealthController zombieHealthController;
    EnemyMovementController enemyMovementController;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        zombieHealthController = GetComponent<EnemyHealthController>();
        enemyMovementController = GetComponent<EnemyMovementController>();
    }

    public void SetEnemyData(EnemyScriptable enemyData)
    {
        this.enemyData = enemyData;
        zombieHealthController.life = enemyData.life;
        zombieHealthController.defense = enemyData.defense;
        enemyMovementController.speed = enemyData.speed;

        // Image
        spriteRenderer.sprite = enemyData.sprite;
    }
}
