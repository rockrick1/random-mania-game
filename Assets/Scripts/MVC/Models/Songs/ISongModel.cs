using System;
using UnityEngine;

public interface ISongModel : IDisposable
{
    ISongSettings CurrentSongSettings { get; }
    AudioClip CurrentSongAudio { get; }
    
    void Initialize();
    void InitializeSong(string songId);
}