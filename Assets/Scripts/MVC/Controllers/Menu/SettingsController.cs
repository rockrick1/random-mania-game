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
        view.OnMainVolumeChanged += HandleMainVolumeChanged;
        view.OnMusicVolumeChanged += HandleMusicVolumeChanged;
        view.OnSFXVolumeChanged += HandleSFXVolumeChanged;
    }

    void RemoveListeners ()
    {
        view.OnMainVolumeChanged -= HandleMainVolumeChanged;
        view.OnMusicVolumeChanged -= HandleMusicVolumeChanged;
        view.OnSFXVolumeChanged -= HandleSFXVolumeChanged;
    }

    void HandleMainVolumeChanged (float value) => model.SetMainVolume(value);

    void HandleMusicVolumeChanged (float value) => model.SetMusicVolume(value);

    void HandleSFXVolumeChanged (float value) => model.SetSFXVolume(value);

    void SyncView ()
    {
        view.MainVolumeSlider.value = model.MainVolume;
        view.MusicVolumeSlider.value = model.MusicVolume;
        view.SFXVolumeSlider.value = model.SFXVolume;
    }

    public void Open ()
    {
        SyncView();
        view.Open();
    }

    public void Close () => view.Close();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}