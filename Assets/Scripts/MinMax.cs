using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using System;

[Serializable]
public class MinMax<T> : System.Object
{
    public T min;
    public T max;
}
