using System;
using System.Collections.Generic;

public interface IEditorSongModel : IDisposable
{
    event Action OnSongRefreshed;
    event Action OnSongSaved;
    
    int SelectedSignature { get; }
    float SignedBeatInterval { get; }
    bool HasUnsavedChanges { get; }
    List<Note> Notes { get; }
    float SongStartingTime { get; }

    void Initialize ();
    void Refresh (string songId, string songDifficultyName);
    void StartCreatingNote (int pos, float songPlayerTime, float viewHeight);
    NoteCreationResult? CreateNote (int pos, float songProgress, float height);
    void RemoveNoteAt (int index);
    int GetSeparatorColorByIndex (int i);
    float GetNextBeat (float time, int direction);
    void ChangeBpm (float val);
    void ChangeAr (float val);
    void ChangeDiff (float val);
    void ChangeStartingTime (float val);
    void ChangeSignature (int signature);
    void SaveSong ();
}