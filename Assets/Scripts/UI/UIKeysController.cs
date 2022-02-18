using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeysController : MonoBehaviour
{
    [SerializeField] StatsDisplayController statsDisplayController;
    bool statsDisplayShown = false;

    public void OnStatsDisplayToggle()
    {
        Debug.Log("OnStatsDisplayToggle()");

        if(statsDisplayShown)
        {
            statsDisplayController.Hide();
            statsDisplayShown = false;
        }
        else
        {
            statsDisplayController.Show();
            statsDisplayShown = true;
        }
    }
}
