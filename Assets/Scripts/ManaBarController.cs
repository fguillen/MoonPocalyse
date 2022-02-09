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

    public void AddMana(int value) {
        slider.value += value;
    }

}
