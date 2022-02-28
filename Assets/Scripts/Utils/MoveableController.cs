using System.Collections;
using UnityEngine;

public class MoveableController : MonoBehaviour
{
    [SerializeField] public float speed = 0f;
    protected Vector2 direction = Vector2.zero;
    Rigidbody2D rbody;
    bool knockedOut;
    IEnumerator knockOutCoroutine;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!knockedOut)
            Move();
    }

    void Move()
    {
        Vector2 adjustedMovement = direction * speed * Time.fixedDeltaTime;
        Vector2 newPos = rbody.position + adjustedMovement;
        rbody.MovePosition(newPos);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void KnockOut(float seconds)
    {
        if(knockOutCoroutine != null)
            StopCoroutine(knockOutCoroutine);

        knockOutCoroutine = KnockOutCoroutine(seconds);
        StartCoroutine(knockOutCoroutine);
    }

    IEnumerator KnockOutCoroutine(float seconds)
    {
        direction = Vector2.zero;
        knockedOut = true;
        yield return new WaitForSeconds(seconds);
        knockedOut = false;
    }
}
