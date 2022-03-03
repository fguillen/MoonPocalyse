using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyScriptable enemyData;
    EnemyHealthController enemyHealthController;
    EnemyMovementController enemyMovementController;
    Rigidbody2D rbody;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] EnemiesSetScriptable enemiesSetData;

    void Awake()
    {
        enemyHealthController = GetComponent<EnemyHealthController>();
        enemyMovementController = GetComponent<EnemyMovementController>();
        rbody = GetComponent<Rigidbody2D>();

        if(enemiesSetData != null)
            enemiesSetData.Add(this);
    }

    void OnDestroy()
    {
        if(enemiesSetData != null)
            enemiesSetData.Remove(this);
    }

    public void SetEnemyData(EnemyScriptable enemyData)
    {
        this.enemyData = enemyData;
        enemyHealthController.SetData(enemyData);
        enemyMovementController.speed = enemyData.speed;
        spriteRenderer.sprite = enemyData.sprite;
        rbody.mass = enemyData.mass;
    }

    public void Impact(float damage, float knockbackForce, Vector2 impactPosition)
    {
        enemyHealthController.Impact(damage);
        Vector2 impactDirection = ((Vector2)transform.position - impactPosition).normalized;
        rbody.AddForce(impactDirection * knockbackForce, ForceMode2D.Impulse);
    }
}
