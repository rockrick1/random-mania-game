using UnityEngine;

public class EditorModel : IEditorModel
{
    public ISongLoaderModel SongLoaderModel { get; }
    public IEditorSongPickerModel SongPickerModel { get; }
    public IEditorInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }
    public IEditorSongModel SongModel { get; }
    public IEditorNewSongModel NewSongModel { get; set; }

    public EditorModel (
        ISongLoaderModel songLoaderModel,
        IEditorSongPickerModel songPickerModel,
        IEditorSongModel songModel,
        IEditorNewSongModel newSongModel,
        IEditorInputManager inputManager,
        IAudioManager audioManager
    )
    {
        SongLoaderModel = songLoaderModel;
        SongPickerModel = songPickerModel;
        SongModel = songModel;
        NewSongModel = newSongModel;
        InputManager = inputManager;
        AudioManager = audioManager;
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

    void HandleSongPicked (string songId) => SongLoaderModel.LoadSong(songId);

    public void Dispose ()
    {
        RemoveListeners();
        
        SongModel.Dispose();
    }
}