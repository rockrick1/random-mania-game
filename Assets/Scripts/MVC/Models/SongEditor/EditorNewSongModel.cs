using System;
using System.IO;
using UnityEngine;

public class EditorNewSongModel : IEditorNewSongModel
{
    readonly SongLoader songLoader;
    public string LastCreatedSongId { get; private set; }
    public string LastCreatedSongDifficultyName { get; private set; }

    public EditorNewSongModel (SongLoader songLoader)
    {
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    public void CreateSong (string songName, string artistName, string songDifficultyName)
    {
        songLoader.CreateSong(songName, artistName, songDifficultyName);
        LastCreatedSongId = SongLoader.GetSongId(songName, artistName);
    }

    public void OpenSongFolder ()
    {
        if (string.IsNullOrEmpty(LastCreatedSongId))
            throw new Exception("LastCreatedSongId is null! Cannot open song folder!");
        
        string path = Path.Combine(songLoader.SongsPath, LastCreatedSongId);
        Application.OpenURL($"file://{path}");
    }

    public bool SongExists (string songName, string artistName) => songLoader.SongExists(songName, artistName);

    public void Dispose ()
    {
        RemoveListeners();
    }
}