using System;
using System.IO;
using UnityEngine;

public class EditorNewSongModel : IEditorNewSongModel
{
    readonly ISongLoaderModel songLoaderModel;
    public string LastCreatedSongId { get; private set; }
    
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

    public void CreateSongFolder (string songId)
    {
        songLoaderModel.CreateSongFolder(songId);
        LastCreatedSongId = songId;
    }

    public void OpenSongFolder ()
    {
        if (string.IsNullOrEmpty(LastCreatedSongId))
            throw new Exception("LastCreatedSongId is null! Cannot open song folder!");
        
        string path = Path.Combine(songLoaderModel.SongsPath, LastCreatedSongId);
        Application.OpenURL($"file://{path}");
    }

    public bool SongExists (string songId) => songLoaderModel.SongExists(songId);

    public void Dispose ()
    {
        RemoveListeners();
    }
}