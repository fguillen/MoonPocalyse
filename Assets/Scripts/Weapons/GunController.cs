using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GunScriptable gunData;
    float lastShootAt;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] BulletScriptable bulletData;

    void Update()
    {
        if(Time.time > lastShootAt + gunData.coldDownSeconds)
            Shoot();
    }

    void Start()
    {
        Shoot();
    }

    void Shoot()
    {
        Debug.Log($"Gun [{gunData.name}] Shoo()");
        Debug.Log("bulletPrefab: " + bulletPrefab);
        BulletController bullet = Instantiate(bulletPrefab, GameManagerController.instance.playerController.transform.position, Quaternion.identity).GetComponent<BulletController>();

        bullet.SetBulletData(bulletData);
        lastShootAt = Time.time;
    }
}
