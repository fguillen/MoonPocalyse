using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    void Update()
    {
        transform.position = playerTransform.position;
    }
}
