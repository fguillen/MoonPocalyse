using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController instance;
    public PlayerController playerController;

    void Awake()
    {
        GameManagerController.instance = this;
    }
}
