using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public UnityEvent OnLeftClick;
    [HideInInspector] public UnityEvent OnRightClick;
    [HideInInspector] public UnityEvent OnMiddleClick;
    
    [HideInInspector] public UnityEvent OnLeftRelease;
    [HideInInspector] public UnityEvent OnRightRelease;
    [HideInInspector] public UnityEvent OnMiddleRelease;

    public void OnPointerDown (PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnLeftClick.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                OnMiddleClick.Invoke();
                break;
        }
    }
    
    public void OnPointerUp (PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnLeftRelease.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightRelease.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                OnMiddleRelease.Invoke();
                break;
        }
    }
}