using System;
using UnityEngine;

public class GameInputManager : MonoBehaviour, IGameInputManager
{
    public event Action<int> OnHitterSelect;
    public event Action OnHitPress;
    public event Action OnEscPressed;
    public event Action OnSpacePressed;

    int selectedPosition = 1;

    public bool GetPositionPressed (int pos) =>
        (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && selectedPosition == pos;
    
    public bool GetPositionReleased (int pos) =>
        (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X)) && selectedPosition == pos;
    
    public bool GetPositionHeld (int pos) =>
        (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) && selectedPosition == pos;

    public int GetCursorPosition () => selectedPosition;

    public bool GetKeyDown (KeyCode key) => Input.GetKeyDown(key);

    void Start ()
    {
        OnHitterSelect?.Invoke(1);
    }

    void Update ()
    {
        CheckHitterChange();
        CheckEscape();
        CheckSpacebar();
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

    void CheckEscape ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscPressed?.Invoke();
        }
    }

    void CheckSpacebar ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }
    }
}