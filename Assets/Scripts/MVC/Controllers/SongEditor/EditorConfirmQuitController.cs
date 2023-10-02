using System;

public class EditorConfirmQuitController : IDisposable
{
    public event Action OnQuit;
    
    readonly EditorConfirmQuitView view;
    readonly IEditorSongModel model;

    public EditorConfirmQuitController (
        EditorConfirmQuitView view,
        IEditorSongModel model
    )
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void Show () => view.Show();

    void AddListeners ()
    {
        view.OnSaveAndQuitClicked += HandleSaveAndQuitClicked;
        view.OnQuitClicked += HandleQuitClicked;
    }

    void RemoveListeners ()
    {
        view.OnSaveAndQuitClicked -= HandleSaveAndQuitClicked;
        view.OnQuitClicked -= HandleQuitClicked;

    }

    void HandleSaveAndQuitClicked ()
    {
        model.SaveSong();
        OnQuit?.Invoke();
    }

    void HandleQuitClicked () => OnQuit?.Invoke();

    public void Dispose ()
    {
        RemoveListeners();
    }
}