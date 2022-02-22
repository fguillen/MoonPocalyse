using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GunScriptable gunData;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float distanceToDestroy = 14f;

    int numHitsMade = 0;
    Vector2 direction;
    Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!GameManagerController.Instance.isPaused)
            CheckIfItMustBeDestroyed();
    }

    public void SetGunData(GunScriptable gunData)
    {
        this.gunData = gunData;
        Initialize();
    }

    void Initialize()
    {
        spriteRenderer.sprite = gunData.bulletSprite;

        PlayerController playerController = GameManagerController.Instance.playerController;
        transform.position = playerController.transform.position;
        SetDirection();
        SetRotation();
        Move();
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
        rBody.velocity = direction * gunData.speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            Impact(other.GetComponent<EnemyController>());
    }

    void Impact(EnemyController enemyController)
    {
        enemyController.Impact(gunData.damage, transform.position);
        numHitsMade ++;

        if(numHitsMade >= gunData.numHits)
            Destroy(gameObject);
    }

    void CheckIfItMustBeDestroyed()
    {
        if(Vector3.Distance(GameManagerController.Instance.playerController.transform.position, transform.position) > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
