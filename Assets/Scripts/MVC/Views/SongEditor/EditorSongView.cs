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
    float songSpeed;
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
        beatInterval = settings.Bpm / 60f;
        // zoomScale = songLength / settings.ApproachRate;
        songSpeed = height / approachRate;
        float totalHeight = (height * songLength) / approachRate;
        songObjectsParent.sizeDelta = new Vector2(songObjectsParent.sizeDelta.x, totalHeight);
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

    //OK
    public void SetStartingTime (float settingsStartingTime)
    {
        float startingPosition = settingsStartingTime * songSpeed;
        separatorsParent.transform.position = Vector3.up * startingPosition;
    }

    public void CreateSeparator ()
    {
        GameObject instance = Instantiate(separatorPrefab, separatorsParent.transform);
    }

    //OK
    public void ChangeSeparatorsDistance (int interval)
    {
        separatorsParent.spacing = (float) ((beatInterval * approachRate) * height) / interval;
    }
    
    void Update ()
    {
        progress = songSpeed * songPlayer.time;
        songObjects.anchoredPosition = new Vector2(0, -progress);
    }
}