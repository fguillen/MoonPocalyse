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

        healthModificatedEvent.Invoke(totalDamage);

        if(life <= 0)
            Death();
    }

    void Death() {
        Debug.Log("Zombie death");
    }
}
