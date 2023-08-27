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
    readonly ISongLoaderModel songLoaderModel;
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
        ISongLoaderModel songLoaderModel,
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
        this.songLoaderModel = songLoaderModel;
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
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        view.BackButton.OnLeftClick.RemoveListener(HandleBack);
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
    }

    void HandleBack () => SceneManager.LoadScene("MainMenu");

    void HandleSongLoaded ()
    {
        Task.Run(() => songLoaderModel.GetSelectedSongAudio(clip =>
        {
            audioManager.SetMusicClip(clip);
            view.WaveForm2D.SetAudio(clip);
            view.WaveForm2D.ShowWave();
        }));
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