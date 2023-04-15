using System.Collections.Generic;
using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    const int DEFAULT_SIGNATURE = 4;
    
    public int SelectedSignature { get; private set; }
    
    readonly IEditorInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;

    readonly List<int> colors1_1 = new() {1};
    readonly List<int> colors1_2 = new() {1, 2};
    readonly List<int> colors1_3 = new() {1, 3, 3};
    readonly List<int> colors1_4 = new() {1, 4, 2, 4};
    readonly List<int> colors1_6 = new() {1, 4, 3, 2, 3, 4};

    SongSettings currentSongSettings;

    float beatInterval;
    float signedBeatInterval;
    
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
        beatInterval = 60f / currentSongSettings.Bpm;
        ChangeSignature(DEFAULT_SIGNATURE);
    }
    
    void HandleSavePressed ()
    {
        songLoaderModel.SaveSong(currentSongSettings);
    }
    
    public NoteCreationResult? ButtonClicked (int pos, float songProgress, float height)
    {
        float mouseY = inputManager.GetMousePos().y;
        float time = songProgress - currentSongSettings.StartingTime +
                     (mouseY / height * currentSongSettings.ApproachRate);
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

    public int GetSeparatorColorByIndex (int i, int signature)
    {
        return signature switch
        {
            1 => colors1_1[i % signature],
            2 => colors1_2[i % signature],
            3 => colors1_3[i % signature],
            4 => colors1_4[i % signature],
            6 => colors1_6[i % signature],
            _ => 1
        };
    }

    public void ChangeSignature (int signature)
    {
        SelectedSignature = signature;
        signedBeatInterval = beatInterval / signature;
    }

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
            if (Mathf.Approximately(note.Timestamp, time))
            {
                if (note.Position == pos)
                    return true;
                substituted = true;
                return false;
            }
            if (note.Timestamp > time)
                break;
        }
        return false;
    }

    float SnapToBeat (float time) => Mathf.RoundToInt(time / signedBeatInterval) * signedBeatInterval;

    public void Dispose ()
    {
        RemoveListeners();
    }
}