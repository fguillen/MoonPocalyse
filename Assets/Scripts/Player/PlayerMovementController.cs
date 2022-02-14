using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MoveableController
{
    public Vector2 lastDirection = Vector2.right;
    public Vector2 lastHorizontalDirection = Vector2.right;

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();

        if(direction != Vector2.zero)
        {
            lastDirection = direction;
        }

        if(direction.x != 0)
        {
            lastHorizontalDirection = new Vector2(direction.x, 0).normalized;
        }
    }
}
