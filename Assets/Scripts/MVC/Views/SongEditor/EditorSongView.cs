using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongView : MonoBehaviour
{
    const float CURSOR_POSITION = 0.2f;
    public event Action<int> OnFieldButtonClicked;
    
    [SerializeField] Button fieldButtonLeft;
    [SerializeField] Button fieldButtonCenter;
    [SerializeField] Button fieldButtonRight;

    [SerializeField] RectTransform songObjectsParent;
    [SerializeField] RectTransform songObjects;
    [SerializeField] Transform notesParent;
    [SerializeField] NoteView noteViewPrefab;

    readonly List<NoteView> noteInstances = new();

    float height;
    
    void Start ()
    {
        fieldButtonLeft.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(0));
        fieldButtonCenter.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(1));
        fieldButtonRight.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(2));
        songObjectsParent.pivot = new Vector2(songObjectsParent.pivot.x, CURSOR_POSITION);
        height = ((RectTransform) transform).rect.height;
    }

    public void SetupSong (ISongSettings settings, float songLength)
    {
        float ratio = songLength / settings.ApproachRate;
        Vector3 localScale = songObjectsParent.localScale;
        localScale = new Vector3(localScale.x, localScale.y * ratio, 1);
        songObjectsParent.localScale = localScale;
    }

    public void SpawnNote (Note note)
    {
        NoteView instance = Instantiate(noteViewPrefab, notesParent);
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