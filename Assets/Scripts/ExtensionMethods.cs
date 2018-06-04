using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    //private static Random rng = new Random();
    //You have changed rng.next to Random.Range here below... should be fine?
    
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static string RandomLetter()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return chars[Random.Range(0, chars.Length)].ToString();
    }

    public static bool randomBoolean()
    {
        return (Random.value > 0.5f);
    }

    public static List<T> GetComponentsInChildrenNoParent<T>(this GameObject parent) where T : Component
    {

        Transform tr = parent.transform;
        int count = tr.childCount;
        List<T> list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var child = tr.GetChild(i);
            list.AddRange(child.GetComponentsInChildren<T>());
        }

        return list;
    }
}

