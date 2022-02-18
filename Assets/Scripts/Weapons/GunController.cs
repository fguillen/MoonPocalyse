using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public GunScriptable gunData;

    public float coldDownSeconds;
    public float damage;
    public int numHits;
    public float speed;

    public int level;

    float lastShootAt;

    [HideInInspector] public UnityEvent onChangeEvent;

    void Awake()
    {
        level = 0;

        if (onChangeEvent == null)
            onChangeEvent = new UnityEvent();
    }

    void Update()
    {
        if(
            (Time.time > lastShootAt + gunData.coldDownSeconds) &&
            !GameManagerController.Instance.isPaused
        )
            Shoot();
    }

    void Start()
    {
        Shoot();
    }

    public void SetGunData(GunScriptable gunData)
    {
        this.gunData = gunData;
        coldDownSeconds = gunData.coldDownSeconds;
        damage = gunData.damage;
        numHits = gunData.numHits;
        speed = gunData.speed;
    }

    void Shoot()
    {
        BulletController bullet = Instantiate(bulletPrefab, GameManagerController.Instance.playerController.transform.position, Quaternion.identity).GetComponent<BulletController>();

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
        coldDownSeconds = coldDownSeconds - (gunLevel.coldDownDecrease / 100 * coldDownSeconds);
        damage = damage + (gunLevel.damageIncrease / 100 * damage);
        numHits = numHits + gunLevel.numHitsIncrease;
        speed = speed + (gunLevel.speedIncrease / 100 * speed);
    }

    public string StatsDescription()
    {
        return $"[Level {level}] Cold down {coldDownSeconds}s, Damage: {damage}HP, Num hits: {numHits}, Speed: {speed}";
    }
}
