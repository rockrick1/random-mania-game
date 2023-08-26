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
    event Action OnSongFinished;
    
    ISongSettings CurrentSongSettings { get; }
    AudioClip CurrentSongAudio { get; }
    bool AllNotesRead { get; }

    void Initialize ();
    void LoadSong (string songId);
    void Play ();
}