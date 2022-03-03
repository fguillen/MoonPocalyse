using System.Collections;
using UnityEngine;

public class MoveableSnappyController : MonoBehaviour
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
        rbody.velocity = direction * speed;
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
