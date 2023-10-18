using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    event Action<Note> OnNoteSpawned;
    event Action<Note, double> OnNoteHit;
    event Action<Note, double> OnLongNoteHit;
    event Action<Note, double> OnLongNoteReleased;
    event Action<Note> OnNoteMissed;
    event Action OnAudioStartTimeReached;
    event Action<float> OnSongStartSkipped;
    event Action OnSongStarted;
    event Action OnSongFinished;
    event Action<bool> OnSkippableChanged;
    event Action OnSongLoaded;
    
    SongLoader SongLoader { get; }
    ISongSettings CurrentSongSettings { get; }
    float ApproachRate { get; }
    bool AllNotesRead { get; }

    void Initialize ();
    void UpdateDependencies (IScoreModel scoreModel);
    void LoadSong (string songId, string songDifficultyName);
    void Play ();
}