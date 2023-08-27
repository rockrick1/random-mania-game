using System;
using System.IO;
using UnityEngine;

public class EditorNewSongModel : IEditorNewSongModel
{
    readonly ISongLoaderModel songLoaderModel;
    public string LastCreatedSongId { get; private set; }
    public string LastCreatedSongDifficultyName { get; private set; }

    public EditorNewSongModel (ISongLoaderModel songLoaderModel)
    {
        this.songLoaderModel = songLoaderModel;
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
        songLoaderModel.CreateSong(songName, artistName, songDifficultyName);
        LastCreatedSongId = SongLoaderModel.GetSongId(songName, artistName);
    }

    public void OpenSongFolder ()
    {
        if (string.IsNullOrEmpty(LastCreatedSongId))
            throw new Exception("LastCreatedSongId is null! Cannot open song folder!");
        
        string path = Path.Combine(songLoaderModel.SongsPath, LastCreatedSongId);
        Application.OpenURL($"file://{path}");
    }

    public bool SongExists (string songName, string artistName) => songLoaderModel.SongExists(songName, artistName);

    public void Dispose ()
    {
        RemoveListeners();
    }
}