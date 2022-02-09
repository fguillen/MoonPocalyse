using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController instance;
    public PlayerController playerController;
    public ManaBarController manaBarController;

    void Awake()
    {
        GameManagerController.instance = this;
    }


}
