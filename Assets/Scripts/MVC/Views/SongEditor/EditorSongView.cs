using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongView : MonoBehaviour
{
    public event Action<int> OnFieldButtonClicked;
    
    [SerializeField] AudioSource songPlayer;
    
    [SerializeField] Button fieldButtonLeft;
    [SerializeField] Button fieldButtonCenter;
    [SerializeField] Button fieldButtonRight;

    [SerializeField] RectTransform songObjectsParent;
    [SerializeField] RectTransform songObjects;
    [SerializeField] Transform notesParent;
    [SerializeField] VerticalLayoutGroup separatorsParent;

    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] GameObject separatorPrefab;

    public AudioSource SongPlayer => songPlayer;

    readonly List<NoteView> noteInstances = new();

    float height;
    float progress;
    float zoomScale;
    float songLength;
    
    void Start ()
    {
        fieldButtonLeft.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(0));
        fieldButtonCenter.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(1));
        fieldButtonRight.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(2));
        height = ((RectTransform) transform).rect.height;
    }

    public void SetupSong (ISongSettings settings, float songLength)
    {
        this.songLength = songLength;
        zoomScale = songLength / settings.ApproachRate;
        songObjectsParent.localScale = new Vector3(1, zoomScale, 1);
        ChangeSeparatorsDistance(2);
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

    public void SetStartingTime (float settingsStartingTime)
    {
        separatorsParent.transform.position += Vector3.up * zoomScale * settingsStartingTime / zoomScale;
    }

    public void CreateSeparator ()
    {
        GameObject instance = Instantiate(separatorPrefab, separatorsParent.transform);
        RectTransform rect = (RectTransform) instance.transform;
        rect.localScale = new Vector3(1, 1 / zoomScale, 1);
    }

    public void ChangeSeparatorsDistance (int interval)
    {
        separatorsParent.spacing = (height / songLength) / interval;
    }
    
    void Update ()
    {
        progress = songPlayer.time / songLength;
        songObjects.anchoredPosition = new Vector2(0, -progress * height);
    }
}