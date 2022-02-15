using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxMana(int value){
        slider.maxValue = value;
    }

    public void AddMana(float value) {
        slider.value += value;

        if(slider.value >= slider.maxValue)
            Full();
    }

    void Full()
    {
        GameManagerController.Instance.ManaBarFull();
    }

    public void Reset()
    {
        slider.value = 0;
    }

}
