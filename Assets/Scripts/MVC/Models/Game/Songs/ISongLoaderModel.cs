using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISongLoaderModel
{
    event Action OnSongLoaded;
    event Action OnSongSaved;
    
    SongSettings Settings { get; }
    AudioClip Audio { get; }
    string SongsPath { get; }

    void Initialize ();
    void LoadSong (string songId);
    void SaveSong (ISongSettings settings);
    IReadOnlyList<string> GetAllSongDirs ();
}