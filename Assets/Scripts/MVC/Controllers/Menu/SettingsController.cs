using System;

public class SettingsController : IDisposable
{
    readonly SettingsView view;
    readonly ISettingsModel model;
    
    public SettingsController (SettingsView view, ISettingsModel model)
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
        throw new NotImplementedException();
    }

    void RemoveListeners ()
    {
        throw new NotImplementedException();
    }
    
    public void Open () => view.Open();
    
    public void Close () => view.Close();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}