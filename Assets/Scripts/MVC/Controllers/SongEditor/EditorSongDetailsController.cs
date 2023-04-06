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
        view.OnBPMChanged += HandleBPMChanged;
        view.OnARChanged += HandleARChanged;
        view.OnDiffChanged += HandleDiffChanged;
        view.OnOffsetChanged += HandleOffsetChanged;
    }

    void RemoveListeners ()
    {
        view.OnBPMChanged -= HandleBPMChanged;
        view.OnARChanged -= HandleARChanged;
        view.OnDiffChanged -= HandleDiffChanged;
        view.OnOffsetChanged -= HandleOffsetChanged;
    }
    
    void HandleBPMChanged (float val)
    {
        throw new NotImplementedException();
    }

    void HandleARChanged (float val)
    {
        throw new NotImplementedException();
    }

    void HandleDiffChanged (float val)
    {
        throw new NotImplementedException();
    }

    void HandleOffsetChanged (double val)
    {
        throw new NotImplementedException();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}