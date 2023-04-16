using System;

public class SongMenuController : IDisposable
{
    readonly SongMenuView view;
    readonly ISongMenuModel model;
    
    public SongMenuController (SongMenuView view, ISongMenuModel model)
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
    }

    void RemoveListeners ()
    {
    }

    public void Open () => view.Open();
    
    public void Close () => view.Close();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}