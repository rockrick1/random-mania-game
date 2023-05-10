using System;

public class EditorTopBarController : IDisposable
{
    readonly EditorTopBarView view;
    readonly EditorSongDetailsController songDetailsController;
    readonly IEditorInputManager inputManager;

    public EditorTopBarController (
        EditorTopBarView view,
        EditorSongDetailsController songDetailsController,
        IEditorInputManager inputManager
    )
    {
        this.view = view;
        this.songDetailsController = songDetailsController;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        inputManager.OnSongPlayPause += HandlePlayPause;
        inputManager.OnSongScroll += HandleSongScroll;
        inputManager.OnZoomScroll += HandleZoomScroll;
        songDetailsController.OnShowWaveClicked += HandleShowWaveClicked;
    }

    void RemoveListeners ()
    {
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
        inputManager.OnZoomScroll -= HandleZoomScroll;
        songDetailsController.OnShowWaveClicked -= HandleShowWaveClicked;
    }

    void HandlePlayPause ()
    {
    }

    void HandleSongScroll (float amount)
    {
    }

    void HandleZoomScroll (float amount)
    {
        view.ChangeZoom(amount);
    }

    void HandleShowWaveClicked (bool active)
    {
        view.SetWaveActive(active);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}