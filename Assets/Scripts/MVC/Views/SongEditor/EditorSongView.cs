using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongView : MonoBehaviour
{
    public event Action<int> OnFieldButtonLeftClicked;
    public event Action<int> OnFieldButtonRightClicked;
    
    [SerializeField] AudioSource songPlayer;
    
    [SerializeField] UIClickHandler fieldButtonLeft;
    [SerializeField] UIClickHandler fieldButtonCenter;
    [SerializeField] UIClickHandler fieldButtonRight;

    [SerializeField] RectTransform songObjects;

    [Header("Notes")]
    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] Transform notesParent;
    [SerializeField] Transform leftNotesPosition;
    [SerializeField] Transform centerNotesPosition;
    [SerializeField] Transform rightNotesPosition;

    [Header("Separators")]
    [SerializeField] GameObject separatorPrefab;
    [SerializeField] VerticalLayoutGroup separatorsParent;
    [SerializeField] Color beatColor;
    [SerializeField] Color halfBeatColor;
    [SerializeField] Color thirdBeatColor;
    [SerializeField] Color quarterBeatColor;

    public AudioSource SongPlayer => songPlayer;

    public float Height { get; private set; }

    readonly List<NoteView> noteInstances = new();
    readonly List<GameObject> separatorInstances = new();

    float progress;
    float zoomScale;
    float songLength;
    float totalHeight;
    float objectsSpeed;
    float approachRate;

    float beatInterval;
    
    void Start ()
    {
        fieldButtonLeft.OnLeftClick.AddListener(() => OnFieldButtonLeftClicked?.Invoke(0));
        fieldButtonCenter.OnLeftClick.AddListener(() => OnFieldButtonLeftClicked?.Invoke(1));
        fieldButtonRight.OnLeftClick.AddListener(() => OnFieldButtonLeftClicked?.Invoke(2));
        fieldButtonLeft.OnRightClick.AddListener(() => OnFieldButtonRightClicked?.Invoke(0));
        fieldButtonCenter.OnRightClick.AddListener(() => OnFieldButtonRightClicked?.Invoke(1));
        fieldButtonRight.OnRightClick.AddListener(() => OnFieldButtonRightClicked?.Invoke(2));
        
        Height = ((RectTransform) transform).rect.height;
    }

    public void SetupSong (ISongSettings settings, float songLength)
    {
        progress = 0;
        approachRate = settings.ApproachRate;
        this.songLength = songLength;
        
        beatInterval = 60f / settings.Bpm;
        objectsSpeed = Height / approachRate;
        totalHeight = (Height * songLength) / approachRate;
        songObjects.sizeDelta = new Vector2(songObjects.sizeDelta.x, totalHeight);
        
        float startingPosition = settings.StartingTime * objectsSpeed;
        ((RectTransform) separatorsParent.transform).localPosition = new Vector3(0, startingPosition, 0);
        ((RectTransform) notesParent.transform).localPosition = new Vector3(0, startingPosition, 0);
    }

    public void CreateNote (Note note, int index = -1)
    {
        NoteView instance = Instantiate(noteViewPrefab, notesParent);
        instance.transform.localPosition = new Vector3(GetNoteXPosition(note.Position), GetNoteYPosition(note.Timestamp));
        instance.Note = note;
        if (index == -1)
            noteInstances.Add(instance);
        else
            noteInstances.Insert(index, instance);
    }

    public void RemoveNote (int index)
    {
        Destroy(noteInstances[index].gameObject);
        noteInstances.RemoveAt(index);
    }

    public void ClearNotes ()
    {
        foreach (NoteView noteView in noteInstances)
            Destroy(noteView.gameObject);
        noteInstances.Clear();
    }

    public void ClearSeparators ()
    {
        foreach (GameObject separator in separatorInstances)
            Destroy(separator);
        separatorInstances.Clear();
    }

    public void CreateSeparator (int color)
    {
        GameObject instance = Instantiate(separatorPrefab, separatorsParent.transform);
        instance.GetComponentInChildren<Image>().color = color switch
        {
            1 => beatColor,
            2 => halfBeatColor,
            3 => thirdBeatColor,
            4 => quarterBeatColor,
            _ => instance.GetComponentInChildren<Image>().color
        };
        instance.transform.SetAsFirstSibling();
        separatorInstances.Add(instance);
    }

    public void ChangeSeparatorsDistance (int interval)
    {
        separatorsParent.spacing = (beatInterval * Height) / approachRate / interval;
    }

    float GetNoteXPosition (int pos)
    {
        return pos switch
        {
            0 => leftNotesPosition.transform.localPosition.x,
            1 => centerNotesPosition.transform.localPosition.x,
            2 => rightNotesPosition.transform.localPosition.x,
            _ => throw new ArgumentOutOfRangeException($"Note position out of range! {pos}")
        };
    }

    float GetNoteYPosition (float time)
    {
        return (totalHeight * time) / songLength;
    }
    
    void Update ()
    {
        progress = objectsSpeed * songPlayer.time;
        songObjects.anchoredPosition = new Vector2(0, -progress);
    }
}