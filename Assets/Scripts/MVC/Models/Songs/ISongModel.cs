using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    event Action<Note> OnNoteHit;
    event Action<Note> OnNoteMissed;
    event Action OnSongFinished;
    
    ISongSettings CurrentSongSettings { get; }
    AudioClip CurrentSongAudio { get; }
    
    void Initialize();
    void LoadSong(string songId);
    void Play();
}