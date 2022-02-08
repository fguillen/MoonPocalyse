using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] float zombiesPerSecond;
    float nextZombieAt;

    void Awake()
    {
        nextZombieAt = Time.time;
    }

    void Update()
    {
        if(Time.time > nextZombieAt)
            SpawnZombie();
    }

    void SpawnZombie()
    {
        Instantiate(zombiePrefab, this.transform);
        nextZombieAt = Time.time + (1 / zombiesPerSecond);
    }
}
