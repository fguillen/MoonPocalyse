using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] BulletScriptable bulletData;
    [SerializeField] SpriteRenderer spriteRenderer;

    int numHitsMade = 0;
    Vector2 direction;
    Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetBulletData(BulletScriptable bulletData)
    {
        this.bulletData = bulletData;
        Initialize();
    }

    void Initialize()
    {
        spriteRenderer.sprite = bulletData.sprite;

        PlayerController playerController = GameManagerController.instance.playerController;
        transform.position = playerController.transform.position;
        direction = Vector2.right;
    }

    public void Move()
    {
        Vector2 adjustedMovement = direction * bulletData.speed * Time.fixedDeltaTime;
        Vector2 newPos = rBody.position + adjustedMovement;
        rBody.MovePosition(newPos);
    }

    void Impact(EnemyHealthController healthController)
    {
        Debug.Log("Impact()");
        healthController.Impact(bulletData.damage);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D()");

        if(other.CompareTag("Enemy"))
        {
            Impact(other.GetComponent<EnemyHealthController>());
        }
    }
}