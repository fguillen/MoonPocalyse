using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class ItemChances<T>
{
    public T item;
    [Range(0, 100)] public int chances;

    public static T ChooseItemByChances(List<ItemChances<T>> itemsChances)
    {
        List<T> chancesList = new List<T>();
        foreach (var itemChances in itemsChances)
            for (int i = 0; i < itemChances.chances; i++)
                chancesList.Add(itemChances.item);

        return chancesList[Random.Range(0, chancesList.Count)];
    }
}
