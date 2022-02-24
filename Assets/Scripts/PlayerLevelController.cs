using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelDisplayController : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    int actualLevel = 0;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        if(actualLevel != GameManagerController.Instance.playerController.level)
            UpdateText();
    }

    void UpdateText()
    {
        actualLevel = GameManagerController.Instance.playerController.level;
        text.text = $"L{actualLevel.ToString("D2")}";
    }
}
