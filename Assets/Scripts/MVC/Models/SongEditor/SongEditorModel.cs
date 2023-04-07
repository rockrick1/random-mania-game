using System;
using UnityEngine;

public class SongEditorModel : ISongEditorModel
{
    public event Action<AudioClip, ISongSettings> OnSongLoaded;
    
    public ISongLoaderModel SongLoaderModel { get; }
    public IEditorSongPickerModel EditorSongPickerModel { get; }
    public IEditorInputManager InputManager { get; }

    public SongEditorModel (
        ISongLoaderModel songLoaderModel,
        IEditorSongPickerModel editorSongPickerModel,
        IEditorInputManager inputManager
    )
    {
        SongLoaderModel = songLoaderModel;
        EditorSongPickerModel = editorSongPickerModel;
        InputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void ProcessSong (AudioClip clip)
    {
        // throw new NotImplementedException();
    }

    void AddListeners ()
    {
        EditorSongPickerModel.OnSongPicked += HandleSongPicked;
    }

    void RemoveListeners ()
    {
        EditorSongPickerModel.OnSongPicked -= HandleSongPicked;
    }

    void HandleSongPicked (string songId)
    {
        SongLoaderModel.LoadSong(songId);
        OnSongLoaded?.Invoke(SongLoaderModel.Audio, SongLoaderModel.Settings);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}