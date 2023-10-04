using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public UnityEvent OnClick;

    [HideInInspector] public UnityEvent OnLeftClick;
    [HideInInspector] public UnityEvent OnRightClick;
    [HideInInspector] public UnityEvent OnMiddleClick;
    
    [HideInInspector] public UnityEvent OnLeftRelease;
    [HideInInspector] public UnityEvent OnRightRelease;
    [HideInInspector] public UnityEvent OnMiddleRelease;
    
    [HideInInspector] public UnityEvent OnHover;
    [HideInInspector] public UnityEvent OnUnhover;

    bool isDragging;

    void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isDragging)
                return;
            OnClick?.Invoke();
        });
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (isDragging)
            return;
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
        if (isDragging)
            return;
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

    public void OnBeginDrag (PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnPointerEnter (PointerEventData eventData) => OnHover?.Invoke();

    public void OnPointerExit (PointerEventData eventData) => OnUnhover?.Invoke();
}