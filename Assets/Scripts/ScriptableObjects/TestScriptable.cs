using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New TestScriptable", menuName = "TestScriptable")]
public class TestScriptable : ScriptableObject
{
    public new string name;

    void Reset()
    {
        Debug.Log("Reset");
    }

    void Awake()
    {
        Debug.Log("Awake");
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void OnValidate()
    {
        Debug.Log("OnValidate");
    }

}
