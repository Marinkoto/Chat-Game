using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ListUtils
{
    private static readonly System.Random rng = new();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
    public static T GetRandomItem<T>(this IList<T> list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }
    public static T GetItemByIndex<T>(this IList<T> list,int index)
    {
        return list[index];
    }
}
