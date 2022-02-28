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

    public void Impact(float damage, Vector2 impactPosition)
    {
        enemyHealthController.Impact(damage);
        Vector2 impactDirection = (Vector2)transform.position - impactPosition;
        impactDirection = new Vector2(impactDirection.x, 0).normalized;
        // Debug.Log($"impactDirection: {impactDirection}, bulletImpactEffect: {enemyData.bulletImpactEffect}");
        rbody.velocity = Vector2.zero;
        rbody.AddForce(impactDirection * enemyData.bulletImpactEffect, ForceMode2D.Impulse);
        enemyMovementController.KnockOut(enemyData.knockOutTime);
    }
}
