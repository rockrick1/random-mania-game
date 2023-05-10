using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    readonly Dictionary<string, Coroutine> routines = new();
    public static CoroutineRunner Instance { get; private set; }

    void Awake ()
    {
        Instance ??= this;
        DontDestroyOnLoad(Instance);
    }

    public void StartRoutine (string id, IEnumerator routine)
    {
        if (routines.TryGetValue(id, out var existing) && existing != null)
            StopCoroutine(existing);
        routines[id] = StartCoroutine(routine);
    }

    public new void StopRoutine (string id)
    {
        if (routines.TryGetValue(id, out var existing) && existing != null)
            StopCoroutine(existing);
    }
}