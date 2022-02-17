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
    }

    void Shoot()
    {
        BulletController bullet = Instantiate(bulletPrefab, GameManagerController.Instance.playerController.transform.position, Quaternion.identity).GetComponent<BulletController>();

        bullet.SetGunData(gunData);
        lastShootAt = Time.time;
    }
}
