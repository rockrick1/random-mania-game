using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EditorController : IDisposable
{
    readonly EditorSongPickerController songPickerController;
    readonly EditorSongDetailsController songDetailsController;
    readonly EditorTopBarController topBarController;
    readonly EditorSongController songController;
    readonly EditorView view;
    readonly IEditorModel model;
    readonly IEditorSongModel songModel;
    readonly ISongLoaderModel songLoaderModel;
    readonly IEditorInputManager inputManager;

    public EditorController (
        EditorSongPickerController songPickerController,
        EditorSongDetailsController songDetailsController,
        EditorTopBarController topBarController,
        EditorSongController songController,
        EditorView view,
        IEditorModel model,
        IEditorSongModel songModel,
        ISongLoaderModel songLoaderModel,
        IEditorInputManager inputManager
    )
    {
        this.songPickerController = songPickerController;
        this.songDetailsController = songDetailsController;
        this.topBarController = topBarController;
        this.songController = songController;
        this.view = view;
        this.model = model;
        this.songModel = songModel;
        this.songLoaderModel = songLoaderModel;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        songPickerController.Initialize();
        songDetailsController.Initialize();
        topBarController.Initialize();
        songController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        view.BackButton.OnLeftClick.AddListener(HandleBack);
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSongPlayPause += HandlePlayPause;
        inputManager.OnSongScroll += HandleSongScroll;
    }

    void RemoveListeners ()
    {
        view.BackButton.OnLeftClick.RemoveListener(HandleBack);
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
    }

    void HandleBack ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void HandleSongLoaded ()
    {
        view.SetSong(songLoaderModel.Audio);
        view.WaveForm2D.SetAudio(songLoaderModel.Audio);
        view.WaveForm2D.ShowWave();
        model.ProcessSong(songLoaderModel.Audio);
        view.ClearSeparators();
    }

    void HandlePlayPause () => view.PlayPauseSong();

    void HandleSongScroll (float amount)
    {
        view.SetSongTime(songModel.GetNextBeat(view.SongPlayer.time, Mathf.RoundToInt(-amount)));
    }

    public void Dispose ()
    {
        RemoveListeners();
        songPickerController.Dispose();
        songDetailsController.Dispose();
        topBarController.Dispose();
        songController.Dispose();
    }
}