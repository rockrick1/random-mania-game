using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    ISongSettings CurrentSongSettings { get; }
    AudioClip CurrentSongAudio { get; }
    event Action<Note> OnNoteHit;
    event Action<Note> OnNoteMissed;
    event Action OnSongFinished;

    void Initialize ();
    void LoadSong (string songId);
    void Play ();
}