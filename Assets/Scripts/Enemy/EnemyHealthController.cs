using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class HealthModificatedEvent : UnityEvent<float>
{
}


public class EnemyHealthController : MonoBehaviour
{
    [HideInInspector] public float life;
    [HideInInspector] public float defense;

    public HealthModificatedEvent healthModificatedEvent;

    [SerializeField] GameObject gemGreenPrefabs;
    [SerializeField] GameObject gemPurplePrefabs;
    [SerializeField] [Range(0,1)] float probabilityOfPurpleGem;

    void Awake()
    {
        if(healthModificatedEvent == null)
            healthModificatedEvent = new HealthModificatedEvent();
    }

    public void Impact(float damage)
    {
        float totalDamage = damage - defense; // TODO: limit to >= 0
        life -= totalDamage;
        Debug.Log($"damage: {damage}, totalDamage: {totalDamage}, life: {life}");

        if(life <= 0)
            Death();

        healthModificatedEvent.Invoke(totalDamage);
    }

    void Death() {
        Debug.Log("Zombie death");
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
