using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [SerializeField] Transform bulletSpawner;
    [SerializeField] GameObject bulletPrefab;

    void OnShoot(InputValue value)
    {
        Shoot();
    }

    void Shoot() {
        BulletController bulletController = Instantiate(bulletPrefab, bulletSpawner).GetComponent<BulletController>();
        bulletController.SetDirection(Vector2.right);
    }
}
