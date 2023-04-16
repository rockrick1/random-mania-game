using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class EditorTopBarDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] ScrollRect scrollRect;
    
    public bool IsDragging { get; private set; }
    public float Progress => scrollRect.horizontalNormalizedPosition;

    public void SetProgress (float normalizedProgress)
    {
        scrollRect.horizontalNormalizedPosition = normalizedProgress;
    }
    
    public void OnBeginDrag (PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        IsDragging = false;
    }
}