using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManagerController.Instance.playerController.transform.position;
    }
}
