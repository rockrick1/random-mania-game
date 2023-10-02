using System;

public class EditorSongSavedController : IDisposable
{
    public event Action<string, string> OnEditNewSong;
    
    readonly EditorSongSavedView view;
    readonly IEditorSongModel model;

    public EditorSongSavedController (
        EditorSongSavedView view,
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

    void AddListeners ()
    {
        model.OnSongSaved += HandleSongSaved;
    }

    void RemoveListeners ()
    {
        model.OnSongSaved -= HandleSongSaved;
    }

    void HandleSongSaved ()
    {
        view.Show();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}