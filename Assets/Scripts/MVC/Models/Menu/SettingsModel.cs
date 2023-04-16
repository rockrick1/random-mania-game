using System;

public class SettingsModel : ISettingsModel
{
    readonly IAudioManager audioManager;

    public SettingsModel (IAudioManager audioManager)
    {
        this.audioManager = audioManager;
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

    public void Dispose ()
    {
        RemoveListeners();
    }
}