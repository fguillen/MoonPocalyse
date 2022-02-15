using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyHealthController : MonoBehaviour
{
    [HideInInspector] public float life;
    [HideInInspector] public float defense;

    float maxLife;

    public UnityEvent<float, float, float> healthModificatedEvent;

    [SerializeField] GameObject gemGreenPrefabs;
    [SerializeField] GameObject gemPurplePrefabs;
    [SerializeField] [Range(0,1)] float probabilityOfPurpleGem;

    void Awake()
    {
        if(healthModificatedEvent == null)
            healthModificatedEvent = new UnityEvent<float, float, float>();
    }

    public void Impact(float damage)
    {
        float totalDamage = damage - defense; // TODO: limit to >= 0
        life -= totalDamage;

        if(life <= 0)
            Death();

        healthModificatedEvent.Invoke(totalDamage, life, maxLife);
    }

    public void SetData(EnemyScriptable enemyData)
    {
        life = enemyData.life;
        defense = enemyData.defense;
        maxLife = life;
    }

    void Death() {
        GameObject gem;

        if(Random.value < probabilityOfPurpleGem)
        {
            gem = gemPurplePrefabs;
        } else {
            gem = gemGreenPrefabs;
        }

        Instantiate(gem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
