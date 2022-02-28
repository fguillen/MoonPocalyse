using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    [SerializeField] public GunScriptable gunData;

    [HideInInspector] public float coldDownSeconds;
    [HideInInspector] public float damage;
    [HideInInspector] public int numHits;
    [HideInInspector] public int numProjectiles;
    [HideInInspector] public float speed;

    [HideInInspector] public int level;

    float lastShootAt;

    [HideInInspector] public UnityEvent onChangeEvent;

    void Awake()
    {
        level = 0;

        if (onChangeEvent == null)
            onChangeEvent = new UnityEvent();
    }

    void Start()
    {
        if(gunData != null)
            SetGunData(gunData);

        Shoot();
    }

    void Update()
    {
        if(Time.time > lastShootAt + gunData.coldDownSeconds)
            Shoot();
    }

    public void SetGunData(GunScriptable gunData)
    {
        this.gunData = gunData;

        coldDownSeconds = gunData.coldDownSeconds;
        damage = gunData.damage;
        numHits = gunData.numHits;
        numProjectiles = gunData.numProjectiles;
        speed = gunData.speed;
    }

    void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            ShootBullet();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void ShootBullet()
    {
        BulletController bullet = Instantiate(gunData.bulletPrefab, GameManagerController.Instance.playerController.transform.position, Quaternion.identity).GetComponent<BulletController>();
        bullet.SetGunData(gunData);
        lastShootAt = Time.time;
    }

    public void Upgrade()
    {
        GunLevel nextLevel = gunData.levels[level];
        ApplyLevel(nextLevel);
        level += 1;
        onChangeEvent.Invoke();
    }

    void ApplyLevel(GunLevel gunLevel)
    {
        gunLevel.Apply(this);
    }

    public string StatsDescription()
    {
        return $"[Level {level}] Cold down {coldDownSeconds}s, Damage: {damage}HP, Num projectiles: {numProjectiles}, Num hits: {numHits}, Speed: {speed}";
    }
}
