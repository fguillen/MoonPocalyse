using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GunScriptable gunData;

    float lastShootAt;

    void Update()
    {
        if(Time.time > lastShootAt + gunData.coldDownSeconds)
            Shoot();
    }

    void Start()
    {
        Shoot();
    }

    public void SetGunData(GunScriptable gunData)
    {
        this.gunData = gunData;
    }

    void Shoot()
    {
        Debug.Log($"Gun [{gunData.name}] Shoo()");
        Debug.Log("bulletPrefab: " + bulletPrefab);
        BulletController bullet = Instantiate(bulletPrefab, GameManagerController.instance.playerController.transform.position, Quaternion.identity).GetComponent<BulletController>();

        bullet.SetBulletData(gunData.bulletData);
        lastShootAt = Time.time;
    }
}
