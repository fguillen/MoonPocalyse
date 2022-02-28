using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventController : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider2D> onCollistion;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"ColliderEventController.OnTriggerEnter2D({other.tag})");
        onCollistion.Invoke(other);
    }
}
