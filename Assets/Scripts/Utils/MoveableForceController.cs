using System.Collections;
using UnityEngine;

public class MoveableForceController : MonoBehaviour
{
    [SerializeField] public float speed = 0f;
    protected Vector2 direction = Vector2.zero;
    Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rbody.AddForce(direction * speed * rbody.mass, ForceMode2D.Force);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
