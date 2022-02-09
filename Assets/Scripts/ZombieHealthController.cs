using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class HealthModificatedEvent : UnityEvent<float>
{
}


public class ZombieHealthController : MonoBehaviour
{
    [HideInInspector] public float life;
    [HideInInspector] public float defense;

    public HealthModificatedEvent healthModificatedEvent;

    [SerializeField] GameObject gemPrefab;

    void Awake()
    {
        if(healthModificatedEvent == null)
            healthModificatedEvent = new HealthModificatedEvent();
    }

    public void Impact(ProjectileController projectile)
    {
        float totalDamage = projectile.damage - defense; // TODO: limit to >= 0
        life -= totalDamage;
        // Debug.Log($"damage: {projectile.damage}, totalDamage: {totalDamage}, life: {life}");

        if(life <= 0)
            Death();

        healthModificatedEvent.Invoke(totalDamage);
    }

    void Death() {
        Debug.Log("Zombie death");
        Instantiate(gemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
