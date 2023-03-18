using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance { get; private set; }
    
    readonly Dictionary<string, Coroutine> routines = new();

    void Awake()
    {
        Instance = this;
    }

    public void StartCoroutine(string id, IEnumerator routine)
    {
        if (routines.TryGetValue(id, out Coroutine existing))
            StopCoroutine(existing);
        routines[id] = StartCoroutine(routine);
    }
}