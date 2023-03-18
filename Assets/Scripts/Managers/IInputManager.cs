using System;

public interface IInputManager
{
    event Action<int> OnHitterSelect;
    event Action OnHitPress;
}