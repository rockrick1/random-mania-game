using System;
using UnityEngine;

public class SongEditorController : IDisposable
{
    readonly EditorSongPickerController songPickerController;
    readonly EditorSongDetailsController songDetailsController;
    readonly EditorTopBarController topBarController;
    readonly SongEditorView view;
    readonly ISongEditorModel model;
    readonly IEditorInputManager inputManager;

    public SongEditorController (
        EditorSongPickerController songPickerController,
        EditorSongDetailsController songDetailsController,
        EditorTopBarController topBarController,
        SongEditorView view,
        ISongEditorModel model,
        IEditorInputManager inputManager
    )
    {
        this.songPickerController = songPickerController;
        this.songDetailsController = songDetailsController;
        this.topBarController = topBarController;
        this.view = view;
        this.model = model;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        songPickerController.Initialize();
        songDetailsController.Initialize();
        topBarController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSongPlayPause += HandlePlayPause;
        inputManager.OnSongScroll += HandleSongScroll;
    }

    void RemoveListeners ()
    {
        model.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
    }

    void HandleSongLoaded (AudioClip clip, ISongSettings settings)
    {
        view.SetSong(clip);
        view.WaveForm2D.SetAudio(clip);
        view.WaveForm2D.ShowWave();
        model.ProcessSong(clip);
        view.ClearSeparators();
    }

    void HandlePlayPause () => view.PlayPauseSong();

    void HandleSongScroll (float amount)
    {
        //TODO implement some sort of snap here
        view.ChangeSongTime(amount);
    }

    public void Dispose ()
    {
        RemoveListeners();
        songPickerController.Dispose();
        songDetailsController.Dispose();
        topBarController.Dispose();
    }
}