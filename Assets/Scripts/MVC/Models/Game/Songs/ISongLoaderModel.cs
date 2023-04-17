using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISongLoaderModel
{
    event Action OnSongLoaded;
    event Action OnSongSaved;
    event Action OnSongCreated;
    
    SongSettings Settings { get; }
    AudioClip Audio { get; }
    string SongsPath { get; }

    void Initialize ();
    void CreateSongFolder (string songId);
    void LoadSong (string songId);
    void SaveSong (ISongSettings settings);
    IReadOnlyList<string> GetAllSongDirs ();
    IReadOnlyList<ISongSettings> GetAllSongSettings();
    bool SongExists (string songId);
}