using System;
using UnityEngine;

public interface IGameInputManager
{
    event Action<int> OnHitterSelect;
    event Action OnHitPress;

    public bool GetPositionPressed (int pos);
    public int GetCursorPosition ();
    bool GetKeyDown (KeyCode key);
}