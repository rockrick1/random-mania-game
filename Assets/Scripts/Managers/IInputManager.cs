using System;

public interface IInputManager
{
    event Action<int> OnHitterSelect;
    event Action OnHitPress;

    public bool GetPositionPressed (int pos);
    public int GetCursorPosition ();
}