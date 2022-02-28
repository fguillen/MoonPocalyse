using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRotationController : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
