using System;

public class EditorSongController : IDisposable
{
    readonly EditorSongView view;
    readonly IEditorSongModel model;

    public EditorSongController (EditorSongView view, IEditorSongModel model)
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
        view.OnFieldButtonClicked += HandleFieldButtonClicked;
    }

    void RemoveListeners ()
    {
        view.OnFieldButtonClicked -= HandleFieldButtonClicked;
    }

    void HandleFieldButtonClicked (int pos)
    {
        model.ButtonClicked(pos);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}