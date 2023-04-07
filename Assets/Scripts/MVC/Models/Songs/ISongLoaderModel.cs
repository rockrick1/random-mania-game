using System;
using UnityEngine;

public interface ISongLoaderModel
{
    event Action OnSongLoaded;
    event Action OnSongSaved;
    
    SongSettings Settings { get; }
    AudioClip Audio { get; }

    void Initialize ();
    void LoadSong (string songId);
    void SaveSong (ISongSettings settings);
}