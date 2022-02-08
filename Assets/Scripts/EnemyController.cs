using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemyData;
    ZombieHealthController zombieHealthController;
    ZombieMovementController zombieMovementController;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        this.zombieHealthController = GetComponent<ZombieHealthController>();
        this.zombieHealthController.life = enemyData.life;
        this.zombieHealthController.defense = enemyData.defense;

        this.zombieMovementController = GetComponent<ZombieMovementController>();
        this.zombieMovementController.speed = enemyData.speed;

        // Image
        spriteRenderer.sprite = enemyData.sprite;
    }

}
