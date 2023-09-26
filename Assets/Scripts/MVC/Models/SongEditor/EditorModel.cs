using UnityEngine;

public class EditorModel : IEditorModel
{
    public SongLoader SongLoader { get; }
    public IEditorSongPickerModel SongPickerModel { get; }
    public IEditorInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }
    public IEditorSongModel SongModel { get; }
    public IEditorNewSongModel NewSongModel { get; set; }

    public EditorModel (
        SongLoader songLoader,
        IEditorSongPickerModel songPickerModel,
        IEditorSongModel songModel,
        IEditorNewSongModel newSongModel,
        IEditorInputManager inputManager,
        IAudioManager audioManager
    )
    {
        SongLoader = songLoader;
        SongPickerModel = songPickerModel;
        SongModel = songModel;
        NewSongModel = newSongModel;
        InputManager = inputManager;
        AudioManager = audioManager;
    }

    public void Initialize ()
    {
        AddListeners();
        SongModel.Initialize();
    }

    void AddListeners ()
    {
        SongPickerModel.OnSongPicked += HandleSongPicked;
    }

    void RemoveListeners ()
    {
        SongPickerModel.OnSongPicked -= HandleSongPicked;
    }

    void HandleSongPicked (string songId, string songDifficultyName) => SongModel.Refresh(songId, songDifficultyName);

    public void Dispose ()
    {
        RemoveListeners();
        SongModel.Dispose();
    }
}