using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class HealthModificatedEvent : UnityEvent<float>
{
}


public class ZombieHealthController : MonoBehaviour
{
    [SerializeField] float life = 10;
    [SerializeField] float defense = 1;

    public HealthModificatedEvent healthModificatedEvent;

    void Awake()
    {
        if(this.healthModificatedEvent == null)
            this.healthModificatedEvent = new HealthModificatedEvent();
    }

    public void Impact(ProjectileController projectile)
    {
        float totalDamage = projectile.damage - this.defense; // TODO: limit to >= 0
        this.life -= totalDamage;
        // Debug.Log($"damage: {projectile.damage}, totalDamage: {totalDamage}, life: {life}");

        if(life <= 0)
            Death();

        healthModificatedEvent.Invoke(totalDamage);
    }

    void Death() {
        Debug.Log("Zombie death");
        Destroy(gameObject);
    }
}
