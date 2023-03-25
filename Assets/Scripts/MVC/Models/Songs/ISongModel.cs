using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    event Action<Note> OnNoteSpawned;
    event Action<Note, HitScore> OnNoteHit;
    event Action<Note> OnNoteMissed;
    event Action OnSongFinished;
    
    ISongSettings CurrentSongSettings { get; }
    AudioClip CurrentSongAudio { get; }

    void Initialize ();
    void LoadSong (string songId);
    void Play ();
}