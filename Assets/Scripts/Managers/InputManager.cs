using System;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<int> OnHitterSelect;
    public event Action OnHitPress;

    int selectedPosition = 1;

    public bool GetPositionPressed (int pos) =>
        (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && selectedPosition == pos;

    public int GetCursorPosition () => selectedPosition;

    void Start ()
    {
        OnHitterSelect?.Invoke(1);
    }

    void Update ()
    {
        CheckHitterChange();
    }

    void CheckHitterChange ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedPosition = 0;
            OnHitterSelect?.Invoke(0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedPosition = 2;
            OnHitterSelect?.Invoke(2);
            return;
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            selectedPosition = 1;
            OnHitterSelect?.Invoke(1);
            return;
        }
    }
}