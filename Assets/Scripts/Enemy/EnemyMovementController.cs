using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MoveableController
{
    GameManagerController gameManagerController;
    PlayerController playerController;

    void Start()
    {
        this.gameManagerController = GameManagerController.Instance;
        this.playerController = this.gameManagerController.playerController;
    }

    void Update()
    {
        CalculateDirection();
    }

    void CalculateDirection()
    {
        this.direction = this.playerController.transform.position - this.transform.position;
        this.direction = this.direction.normalized;
    }
}
