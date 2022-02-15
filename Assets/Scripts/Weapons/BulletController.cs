using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] BulletScriptable bulletData;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float distanceToDestroy = 14f;

    int numHitsMade = 0;
    Vector2 direction;
    Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if(!GameManagerController.Instance.isPaused)
        {
            Move();
            CheckIfItMustBeDestroyed();
        }
    }

    public void SetBulletData(BulletScriptable bulletData)
    {
        this.bulletData = bulletData;
        Initialize();
    }

    void Initialize()
    {
        spriteRenderer.sprite = bulletData.sprite;

        PlayerController playerController = GameManagerController.Instance.playerController;
        transform.position = playerController.transform.position;
        SetDirection();
        SetRotation();
    }

    void SetDirection()
    {
        direction = GameManagerController.Instance.playerController.playerMovementController.lastHorizontalDirection;
    }

    void SetRotation()
    {
        if(direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Move()
    {
        Vector2 adjustedMovement = direction * bulletData.speed * Time.fixedDeltaTime;
        Vector2 newPos = rBody.position + adjustedMovement;
        rBody.MovePosition(newPos);
    }

    void Impact(EnemyController enemyController)
    {
        enemyController.Impact(bulletData.damage, transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Impact(other.GetComponent<EnemyController>());
        }
    }

    void CheckIfItMustBeDestroyed()
    {
        if(Vector3.Distance(GameManagerController.Instance.playerController.transform.position, transform.position) > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
