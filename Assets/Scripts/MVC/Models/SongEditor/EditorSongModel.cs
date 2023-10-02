using System;
using System.Collections.Generic;
using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    const int DEFAULT_SIGNATURE = 4;

    public event Action OnSongRefreshed;
    public event Action OnSongSaved;
    
    public int SelectedSignature { get; private set; }
    public float SignedBeatInterval { get; private set; }
    public bool HasUnsavedChanges { get; private set; }

    readonly IEditorInputManager inputManager;
    readonly SongLoader songLoader;

    readonly List<int> colors1_1 = new() {1};
    readonly List<int> colors1_2 = new() {1, 2};
    readonly List<int> colors1_3 = new() {1, 3, 3};
    readonly List<int> colors1_4 = new() {1, 4, 2, 4};
    readonly List<int> colors1_6 = new() {1, 4, 3, 2, 3, 4};

    SongSettings currentSongSettings;

    float currentNoteCreationTime;
    float currentNoteCreationTimeEnd;

    float beatInterval;
    
    public EditorSongModel (IEditorInputManager inputManager, SongLoader songLoader)
    {
        this.inputManager = inputManager;
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void Refresh (string songId, string songDifficultyName)
    {
        songLoader.SelectedSongId = songId;
        songLoader.SelectedSongDifficulty = songDifficultyName;
        if (songLoader.GetSongSettings(songId, songDifficultyName) is not SongSettings songSettings)
            throw new Exception("Cant't convert loaded song settings");
        SelectedSignature = DEFAULT_SIGNATURE;
        currentSongSettings = songSettings;
        SetBeatInterval(currentSongSettings.Bpm);
        OnSongRefreshed?.Invoke();
    }

    public void StartCreatingNote (int pos, float songProgress, float height)
    {
        float time = GetTimeClicked(songProgress, height);
        
        if (time < 0)
            return;
        
        currentNoteCreationTime = SnapToBeat(time);
    }

    public NoteCreationResult? CreateNote (int pos, float songProgress, float height)
    {
        float time = GetTimeClicked(songProgress, height);
        
        if (time < 0)
            return null;
        
        currentNoteCreationTimeEnd = SnapToBeat(time);

        if (TryFindNote(pos, currentNoteCreationTime, currentNoteCreationTimeEnd,
                out List<int> substituted))
            return null;

        Note note = Mathf.Approximately(currentNoteCreationTime, currentNoteCreationTimeEnd)
            ? new Note(currentNoteCreationTime, pos)
            : new Note(currentNoteCreationTime, currentNoteCreationTimeEnd, pos);
        
        substituted.Reverse();
        foreach (int i in substituted)
            currentSongSettings.Notes.RemoveAt(i);
        
        int insertAtIndex = 0;
        for (int i = 0; i < currentSongSettings.Notes.Count; i++)
        {
            Note n = currentSongSettings.Notes[i];
            if (n.Time > currentNoteCreationTimeEnd)
            {
                insertAtIndex = i;
                break;
            }

            if (i == currentSongSettings.Notes.Count - 1)
                insertAtIndex = currentSongSettings.Notes.Count;
        }
        currentSongSettings.Notes.Insert(insertAtIndex, note);

        HasUnsavedChanges = true;
        return new NoteCreationResult
        {
            Substituted = substituted,
            Index = insertAtIndex,
            Note = note
        };
    }

    public void RemoveNoteAt (int index)
    {
        HasUnsavedChanges = true;
        currentSongSettings.Notes.RemoveAt(index);
    }

    public int GetSeparatorColorByIndex (int i)
    {
        return SelectedSignature switch
        {
            1 => colors1_1[i % SelectedSignature],
            2 => colors1_2[i % SelectedSignature],
            3 => colors1_3[i % SelectedSignature],
            4 => colors1_4[i % SelectedSignature],
            6 => colors1_6[i % SelectedSignature],
            _ => 1
        };
    }

    public void ChangeBpm (float val)
    {
        SetBeatInterval(val);
        currentSongSettings.Bpm = val;
    }

    public void ChangeAr (float val) => currentSongSettings.ApproachRate = val;

    public void ChangeDiff (float val) => currentSongSettings.Difficulty = val;

    public void ChangeStartingTime (float val) => currentSongSettings.StartingTime = val;

    public void ChangeSignature (int signature)
    {
        SelectedSignature = signature;
        SignedBeatInterval = beatInterval / signature;
    }
    
    public float GetNextBeat (float time, int direction) => SnapToBeat(time) + (direction * SignedBeatInterval);

    public void SaveSong ()
    {
        if (currentSongSettings == null)
            return;
        HasUnsavedChanges = false;
        songLoader.SaveSong(currentSongSettings);
        OnSongSaved?.Invoke();
    }
    
    float GetTimeClicked (float songProgress, float height)
    {
        return songProgress - currentSongSettings.StartingTime +
               (inputManager.GetMousePos().y / height * currentSongSettings.ApproachRate);
    }

    float SnapToBeat (float time) => Mathf.RoundToInt(time / SignedBeatInterval) * SignedBeatInterval;

    bool TryFindNote (int pos, float time, float endTime, out List<int> substituted)
    {
        substituted = new List<int>();
        for (int i = 0; i <= currentSongSettings.Notes.Count; i++)
        {
            if (i == currentSongSettings.Notes.Count)
                return false;
            
            Note note = currentSongSettings.Notes[i];
            if (Intersects(time, endTime, note))
            {
                if (note.Position == pos)
                    return true;
                substituted.Add(i);
            }
            if (note.Time > endTime)
                break;
        }
        return false;
    }

    bool Intersects (float time, float endTime, Note note)
    {
        float noteEndTime = note.IsLong ? note.EndTime : note.Time;
        float iStart = time >= note.Time ? time : note.Time;
        float iEnd = endTime < noteEndTime ? endTime : noteEndTime;
        return iStart < iEnd || Mathf.Approximately(iStart, iEnd);
    }

    void SetBeatInterval (float bpm)
    {
        beatInterval = 60f / bpm;
        SignedBeatInterval = beatInterval / SelectedSignature;
    }

    void AddListeners ()
    {
        inputManager.OnSavePressed += HandleSavePressed;
    }

    void RemoveListeners ()
    {
        inputManager.OnSavePressed -= HandleSavePressed;
    }
    
    void HandleSavePressed () => SaveSong();

    public void Dispose ()
    {
        RemoveListeners();
    }
}