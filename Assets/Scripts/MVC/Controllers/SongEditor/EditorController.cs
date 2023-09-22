using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorController : IDisposable
{
    readonly EditorSongPickerController songPickerController;
    readonly EditorSongDetailsController songDetailsController;
    readonly EditorTopBarController topBarController;
    readonly EditorSongController songController;
    readonly EditorNewSongController newSongController;
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
        AddListeners();
    }

    void AddListeners ()
    {
        view.BackButton.OnLeftClick.AddListener(HandleBack);
        songModel.OnSongRefreshed += HandleSongRefreshed;
    }

    void RemoveListeners ()
    {
        view.BackButton.OnLeftClick.RemoveListener(HandleBack);
        songModel.OnSongRefreshed -= HandleSongRefreshed;
    }

    void HandleBack () => SceneManager.LoadScene("MainMenu");

    void HandleSongRefreshed ()
    {
        AudioClip clip = songLoader.GetSelectedSongAudio();
        audioManager.SetMusicClip(clip);
        view.WaveForm2D.SetAudio(clip);
        view.WaveForm2D.ShowWave();
    }

    public void Dispose ()
    {
        RemoveListeners();
        songPickerController.Dispose();
        songDetailsController.Dispose();
        topBarController.Dispose();
        songController.Dispose();
        newSongController.Dispose();
    }
}