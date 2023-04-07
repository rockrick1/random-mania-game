using System;

public class EditorTopBarController : IDisposable
{
    readonly EditorTopBarView view;
    readonly IEditorInputManager inputManager;

    public EditorTopBarController (
        EditorTopBarView view,
        IEditorInputManager inputManager
    )
    {
        this.view = view;
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
    }

    void RemoveListeners ()
    {
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
        inputManager.OnZoomScroll -= HandleZoomScroll;
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

    public void Dispose ()
    {
        RemoveListeners();
    }
}