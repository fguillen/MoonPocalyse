using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemyData;
    EnemyHealthController zombieHealthController;
    EnemyMovementController enemyMovementController;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        this.zombieHealthController = GetComponent<EnemyHealthController>();
        this.zombieHealthController.life = enemyData.life;
        this.zombieHealthController.defense = enemyData.defense;

        this.enemyMovementController = GetComponent<EnemyMovementController>();
        this.enemyMovementController.speed = enemyData.speed;

        // Image
        spriteRenderer.sprite = enemyData.sprite;
    }

}
