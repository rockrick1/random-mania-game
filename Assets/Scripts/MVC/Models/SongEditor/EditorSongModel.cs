using System.Collections.Generic;
using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    const int DEFAULT_SIGNATURE = 4;
    
    public int SelectedSignature { get; private set; }
    public float SignedBeatInterval { get; private set; }
    
    readonly IEditorInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;

    readonly List<int> colors1_1 = new() {1};
    readonly List<int> colors1_2 = new() {1, 2};
    readonly List<int> colors1_3 = new() {1, 3, 3};
    readonly List<int> colors1_4 = new() {1, 4, 2, 4};
    readonly List<int> colors1_6 = new() {1, 4, 3, 2, 3, 4};

    SongSettings currentSongSettings;

    float beatInterval;
    
    public EditorSongModel (IEditorInputManager inputManager, ISongLoaderModel songLoaderModel)
    {
        this.inputManager = inputManager;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }
    
    void AddListeners ()
    {
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSavePressed += HandleSavePressed;
    }

    void RemoveListeners ()
    {
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSavePressed -= HandleSavePressed;
    }

    void HandleSongLoaded ()
    {
        currentSongSettings = songLoaderModel.Settings;
        SelectedSignature = DEFAULT_SIGNATURE;
        SetBeatInterval(songLoaderModel.Settings.Bpm);
    }
    
    void HandleSavePressed ()
    {
        songLoaderModel.SaveSong(currentSongSettings);
    }
    
    public NoteCreationResult? ButtonLeftClicked (int pos, float songProgress, float height)
    {
        float time = GetTimeClicked(songProgress, height);
        
        if (time < 0)
            return null;
        
        time = SnapToBeat(time);
        
        if (TryFindNote(pos, time, out int index, out bool substituted))
            return null;

        Note note = new Note(time, pos);
        if (substituted)
            currentSongSettings.Notes.RemoveAt(index);
        currentSongSettings.Notes.Insert(index, note);
        
        return new NoteCreationResult
        {
            Substituted = substituted,
            Index = index,
            Note = note
        };
    }
    
    public int ButtonRightClicked (int pos, float songProgress, float height)
    {
        float time = GetTimeClicked(songProgress, height);
        
        if (time < 0)
            return -1;
        
        time = SnapToBeat(time);
        
        if (!TryFindNote(pos, time, out int index, out bool _))
            return -1;

        currentSongSettings.Notes.RemoveAt(index);

        return index;
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
    
    float GetTimeClicked (float songProgress, float height)
    {
        return songProgress - currentSongSettings.StartingTime +
               (inputManager.GetMousePos().y / height * currentSongSettings.ApproachRate);
    }

    float SnapToBeat (float time) => Mathf.RoundToInt(time / SignedBeatInterval) * SignedBeatInterval;

    bool TryFindNote (int pos, float time, out int noteIndex, out bool substituted)
    {
        substituted = false;
        noteIndex = -1;
        for (int i = 0; i <= currentSongSettings.Notes.Count; i++)
        {
            noteIndex = i;
            if (i == currentSongSettings.Notes.Count)
                return false;
            
            Note note = currentSongSettings.Notes[i];
            if (Mathf.Approximately(note.Time, time))
            {
                if (note.Position == pos)
                    return true;
                substituted = true;
                return false;
            }
            if (note.Time > time)
                break;
        }
        return false;
    }

    void SetBeatInterval (float bpm)
    {
        beatInterval = 60f / bpm;
        SignedBeatInterval = beatInterval / SelectedSignature;
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}