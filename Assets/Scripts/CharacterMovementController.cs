using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementController : MoveableController
{
    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
