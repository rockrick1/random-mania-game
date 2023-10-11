using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    event Action<Note> OnNoteSpawned;
    event Action<Note, HitScore> OnNoteHit;
    event Action<Note, HitScore> OnLongNoteHit;
    event Action<Note, HitScore> OnLongNoteReleased;
    event Action<Note> OnNoteMissed;
    event Action OnAudioStartTimeReached;
    event Action<float> OnSongStartSkipped;
    event Action OnSongStarted;
    event Action OnSongFinished;
    event Action<bool> OnSkippableChanged;
    
    SongLoader SongLoader { get; }
    ISongSettings CurrentSongSettings { get; }
    bool AllNotesRead { get; }

    void Initialize ();
    void LoadSong (string songId, string songDifficultyName);
    void Play ();
}