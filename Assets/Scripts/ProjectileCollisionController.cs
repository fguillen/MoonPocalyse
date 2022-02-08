using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionController : MonoBehaviour
{
    ProjectileController projectileController;

    void Awake()
    {
        this.projectileController = GetComponent<ProjectileController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Zombie"))
        {
            Impact(other.GetComponent<ZombieHealthController>());
        }
    }

    void Impact(ZombieHealthController zombieHealthController)
    {
        zombieHealthController.Impact(this.projectileController);
        Destroy(gameObject);
    }
}
