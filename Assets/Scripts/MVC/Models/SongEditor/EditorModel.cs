using System;
using UnityEngine;

public class EditorModel : IEditorModel
{
    public event Action<AudioClip, ISongSettings> OnSongLoaded;
    
    public ISongLoaderModel SongLoaderModel { get; }
    public IEditorSongPickerModel SongPickerModel { get; }
    public IEditorInputManager InputManager { get; }
    public IEditorSongModel SongModel { get; }

    public EditorModel (
        ISongLoaderModel songLoaderModel,
        IEditorSongPickerModel songPickerModel,
        IEditorSongModel songModel,
        IEditorInputManager inputManager
    )
    {
        SongLoaderModel = songLoaderModel;
        SongPickerModel = songPickerModel;
        SongModel = songModel;
        InputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
        
        SongLoaderModel.Initialize();
        SongModel.Initialize();
    }

    public void ProcessSong (AudioClip clip)
    {
    }

    void AddListeners ()
    {
        SongPickerModel.OnSongPicked += HandleSongPicked;
    }

    void RemoveListeners ()
    {
        SongPickerModel.OnSongPicked -= HandleSongPicked;
    }

    void HandleSongPicked (string songId)
    {
        SongLoaderModel.LoadSong(songId);
        OnSongLoaded?.Invoke(SongLoaderModel.Audio, SongLoaderModel.Settings);
    }

    public void Dispose ()
    {
        RemoveListeners();
        
        SongModel.Dispose();
    }
}