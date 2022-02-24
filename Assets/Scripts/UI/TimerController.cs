using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{

    [SerializeField] TMP_Text text;

    void Start()
    {
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }

    void UpdateTime()
    {
        float time = GameManagerController.Instance.gameTime;
        text.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }
}
