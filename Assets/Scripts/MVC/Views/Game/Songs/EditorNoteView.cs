using System;
using UnityEngine;

public class EditorNoteView : NoteView
{
    public event Action<EditorNoteView> OnRightClick;
    
    [SerializeField] UIClickHandler button;

    void Start ()
    {
        button.OnRightClick.AddListener(() => OnRightClick?.Invoke(this));
    }
}