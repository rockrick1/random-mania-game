using System;

public class EditorHitsoundsController : IDisposable
{
    readonly IAudioManager audioManager;
    readonly IEditorHitsoundsModel model;

    public EditorHitsoundsController (
        IAudioManager audioManager,
        IEditorHitsoundsModel model
    )
    {
        this.audioManager = audioManager;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnCreateSong += HandleCreateSong;
        view.OnOpenFolder += HandleOpenNewSongFolder;
        view.OnEdit += HandleEditNewSong;
    }

    void RemoveListeners ()
    {
        view.OnCreateSong -= HandleCreateSong;
        view.OnOpenFolder -= HandleOpenNewSongFolder;
        view.OnEdit -= HandleEditNewSong;
    }

    public void Open ()
    {
        view.SetCreationState(false);
        view.Open();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}