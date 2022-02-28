using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GunScriptable gunData;
    [SerializeField] float distanceToDestroy = 14f;

    BulletMovementBase bulletMovement;

    int numHitsMade = 0;
    Vector2 direction;
    Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        bulletMovement = GetComponent<BulletMovementBase>();
    }

    void Start()
    {
        if(gunData != null)
            SetGunData(gunData);
    }

    void Update()
    {
        CheckIfItMustBeDestroyed();
    }

    void FixedUpdate()
    {
        bulletMovement.Move();
    }

    public void SetGunData(GunScriptable gunData)
    {
        this.gunData = gunData;
        Initialize();
    }

    void Initialize()
    {
        PlayerController playerController = GameManagerController.Instance.playerController;
        transform.position = playerController.transform.position;
        bulletMovement.StartMovement(gunData);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionDetected(other);
    }

    public void CollisionDetected(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            Impact(other.GetComponent<EnemyController>());
    }

    void Impact(EnemyController enemyController)
    {
        Debug.Log("BulletController.Impact()");
        // TODO: this is wrong: damage has to be taken from gunController
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
