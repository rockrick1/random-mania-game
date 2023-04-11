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
    float objectsSpeed;
    float approachRate;

    double beatInterval;
    
    void Start ()
    {
        fieldButtonLeft.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(0));
        fieldButtonCenter.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(1));
        fieldButtonRight.onClick.AddListener(() => OnFieldButtonClicked?.Invoke(2));
        height = ((RectTransform) transform).rect.height;
    }

    public void SetupSong (ISongSettings settings, float songLength)
    {
        approachRate = settings.ApproachRate;
        this.songLength = songLength;
        
        beatInterval = 60f / settings.Bpm;
        objectsSpeed = height / approachRate;
        float totalHeight = (height * songLength) / approachRate;
        songObjects.sizeDelta = new Vector2(songObjects.sizeDelta.x, totalHeight * 10);
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
        float startingPosition = settingsStartingTime * objectsSpeed;
        ((RectTransform) separatorsParent.transform).anchoredPosition = new Vector3(0, startingPosition, 0);
    }

    public void CreateSeparator ()
    {
        GameObject instance = Instantiate(separatorPrefab, separatorsParent.transform);
    }

    public void ChangeSeparatorsDistance (int interval)
    {
        separatorsParent.spacing = (float) ((beatInterval * height) / approachRate) / interval;
    }
    
    void Update ()
    {
        progress = objectsSpeed * songPlayer.time;
        songObjects.anchoredPosition = new Vector2(0, -progress);
    }
}