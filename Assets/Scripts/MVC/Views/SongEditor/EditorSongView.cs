using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongView : MonoBehaviour
{
    public event Action<int> OnFieldButtonClicked;
    
    [SerializeField] Button fieldButtonLeft;
    [SerializeField] Button fieldButtonCenter;
    [SerializeField] Button fieldButtonRight;

    [SerializeField] Transform notesContainer;
    [SerializeField] NoteView noteViewPrefab;

    readonly List<NoteView> noteInstances = new();
    
    void Start ()
    {
        fieldButtonLeft.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(0));
        fieldButtonCenter.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(1));
        fieldButtonRight.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(2));
    }

    public void SpawnNote (Note note)
    {
        NoteView instance = Instantiate(noteViewPrefab, notesContainer);
        instance.Note = note;
        noteInstances.Add(instance);
    }

    public void ClearNotes ()
    {
        foreach (NoteView noteView in noteInstances)
            Destroy(noteView.gameObject);
        noteInstances.Clear();
    }
}