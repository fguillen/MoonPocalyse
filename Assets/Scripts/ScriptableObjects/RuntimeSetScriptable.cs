using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public abstract class RuntimeSetScriptable<T> : ScriptableObject
{
    protected List<T> Items = new List<T>();
    public UnityEvent onListChanged = new UnityEvent();

    public void Add(T item)
    {
        if(!Items.Contains(item)) Items.Add(item);
        onListChanged.Invoke();
    }

    public void Remove(T item)
    {
        if(Items.Contains(item)) Items.Remove(item);
        onListChanged.Invoke();
    }

    public List<T> All()
    {
        return Items;
    }

    void OnEnable()
    {
        Items.Clear();
    }
}
