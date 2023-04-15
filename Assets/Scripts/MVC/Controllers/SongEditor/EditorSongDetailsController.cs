using System;

public class EditorSongDetailsController : IDisposable
{
    public event Action<int> OnSignatureChanged;
    
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
        view.OnStartingTimeChanged += HandleStartingTimeChanged;
        view.OnSignatureChanged += HandleSignatureChanged;
    }

    void RemoveListeners ()
    {
        view.OnBPMChanged -= HandleBPMChanged;
        view.OnARChanged -= HandleARChanged;
        view.OnDiffChanged -= HandleDiffChanged;
        view.OnStartingTimeChanged -= HandleStartingTimeChanged;
        view.OnSignatureChanged -= HandleSignatureChanged;
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

    void HandleStartingTimeChanged (float val)
    {
        throw new NotImplementedException();
    }

    void HandleSignatureChanged (string signature)
    {
        OnSignatureChanged?.Invoke(int.Parse(signature.Substring(signature.IndexOf('/') + 1)));
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}