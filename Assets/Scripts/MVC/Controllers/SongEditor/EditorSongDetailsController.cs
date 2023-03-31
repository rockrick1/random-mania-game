using System;

public class EditorSongDetailsController : IDisposable
{
    readonly EditorSongDetailsView view;
    public EditorSongDetailsController (EditorSongDetailsView view)
    {
        this.view = view;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        throw new System.NotImplementedException();
    }

    void RemoveListeners ()
    {
        throw new System.NotImplementedException();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}