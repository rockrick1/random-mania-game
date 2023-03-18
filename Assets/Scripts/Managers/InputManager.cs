using System;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<int> OnHitterSelect;
    public event Action OnHitPress;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnHitterSelect?.Invoke(0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnHitterSelect?.Invoke(2);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnHitterSelect?.Invoke(1);
            return;
        }
    }
}