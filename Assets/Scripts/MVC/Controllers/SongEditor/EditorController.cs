using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorController : IDisposable
{
    readonly EditorSongPickerController songPickerController;
    readonly EditorSongDetailsController songDetailsController;
    readonly EditorTopBarController topBarController;
    readonly EditorSongController songController;
    readonly EditorNewSongController newSongController;
    readonly EditorSongSavedController songSavedController;
    readonly EditorConfirmQuitController editorConfirmQuitController;
    readonly EditorHitsoundsController editorHitsoundsController;
    readonly EditorView view;
    readonly IEditorModel model;
    readonly IEditorSongModel songModel;
    readonly SongLoader songLoader;
    readonly IEditorInputManager inputManager;
    readonly IAudioManager audioManager;

    public EditorController (
        EditorSongPickerController songPickerController,
        EditorSongDetailsController songDetailsController,
        EditorTopBarController topBarController,
        EditorSongController songController,
        EditorNewSongController newSongController,
        EditorSongSavedController songSavedController,
        EditorConfirmQuitController editorConfirmQuitController,
        EditorHitsoundsController editorHitsoundsController,
        EditorView view,
        IEditorModel model,
        IEditorSongModel songModel,
        SongLoader songLoader,
        IEditorInputManager inputManager,
        IAudioManager audioManager
    )
    {
        this.songPickerController = songPickerController;
        this.songDetailsController = songDetailsController;
        this.topBarController = topBarController;
        this.songController = songController;
        this.newSongController = newSongController;
        this.songSavedController = songSavedController;
        this.editorConfirmQuitController = editorConfirmQuitController;
        this.editorHitsoundsController = editorHitsoundsController;
        this.view = view;
        this.model = model;
        this.songModel = songModel;
        this.songLoader = songLoader;
        this.inputManager = inputManager;
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        songPickerController.Initialize();
        songDetailsController.Initialize();
        topBarController.Initialize();
        songController.Initialize();
        newSongController.Initialize();
        songSavedController.Initialize();
        editorConfirmQuitController.Initialize();
        editorHitsoundsController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        view.BackButton.OnLeftClick.AddListener(HandleBackClicked);
        editorConfirmQuitController.OnQuit += Quit;
        songModel.OnSongRefreshed += HandleSongRefreshed;
    }

    void RemoveListeners ()
    {
        view.BackButton.OnLeftClick.RemoveListener(HandleBackClicked);
        editorConfirmQuitController.OnQuit -= Quit;
        songModel.OnSongRefreshed -= HandleSongRefreshed;
    }

    void HandleBackClicked ()
    {
        if (songModel.HasUnsavedChanges)
            editorConfirmQuitController.Show();
        else
            Quit();
    }

    void Quit () => SceneManager.LoadScene("MainMenu");

    void HandleSongRefreshed ()
    {
        songLoader.GetSelectedSongAudio(clip =>
        {
            audioManager.SetMusicClip(clip);
            view.WaveForm2D.SetAudio(clip);
            view.WaveForm2D.ShowWave();
        });
    }

    public void Dispose ()
    {
        RemoveListeners();
        songPickerController.Dispose();
        songDetailsController.Dispose();
        topBarController.Dispose();
        songController.Dispose();
        newSongController.Dispose();
        songSavedController.Dispose();
        editorConfirmQuitController.Dispose();
        editorHitsoundsController.Dispose();
    }
}