using System;
using UnityEngine;

public class EditorLongNoteView : LongNoteView
{
    public event Action<EditorLongNoteView> OnUpperRightClick;
    public event Action<EditorLongNoteView> OnLowerRightClick;
    [SerializeField] UIClickHandler upperButton;
    [SerializeField] UIClickHandler lowerButton;

    void Start ()
    {
        upperButton.OnRightClick.AddListener(() => OnUpperRightClick?.Invoke(this));
        lowerButton.OnRightClick.AddListener(() => OnLowerRightClick?.Invoke(this));
    }
}