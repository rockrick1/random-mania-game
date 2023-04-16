using System;
using UnityEngine;

public class MenuInputManager : MonoBehaviour, IMenuInputManager
{
    public event Action OnBackPressed;
    
    void Update ()
    {
        CheckBackPressed();
    }

    void CheckBackPressed ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnBackPressed?.Invoke();
    }
}