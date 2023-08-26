using System;
using UnityEngine;

public interface IGameInputManager
{
    event Action<int> OnHitterSelect;
    event Action OnHitPress;
    event Action OnEscPressed;
    event Action OnSpacePressed;

    bool GetPositionPressed (int pos);
    bool GetPositionReleased (int pos);
    bool GetPositionHeld (int pos);
    int GetCursorPosition ();
    bool GetKeyDown (KeyCode key);
}