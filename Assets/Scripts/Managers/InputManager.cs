using System;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<int> OnHitterSelect;
    public event Action OnHitPress;
}