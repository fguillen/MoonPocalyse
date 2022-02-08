using UnityEngine;

public class MoveableController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
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
        Vector2 adjustedMovement = direction * speed * Time.fixedDeltaTime;
        Vector2 newPos = rbody.position + adjustedMovement;
        rbody.MovePosition(newPos);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
