using UnityEngine;

public class ZombieHealthController : MonoBehaviour
{
    [SerializeField] float life = 10;
    [SerializeField] float defense = 1;

    public void Impact(ProjectileController projectile)
    {
        this.life -= projectile.damage - this.defense; // TODO: limit to >= 0

        if(life <= 0)
            Death();
    }

    void Death() {
        Debug.Log("Zombie death");
    }
}
