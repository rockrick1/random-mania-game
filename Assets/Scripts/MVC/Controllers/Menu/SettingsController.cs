using System;

public class SettingsController : IDisposable
{
    public event Action OnClose;
    
    readonly SettingsView view;

    readonly MenuAnimationsController menuAnimationsController;
    
    public SettingsController (SettingsView view)
    {
        this.view = view;
        menuAnimationsController = new MenuAnimationsController(view.transform);
    }

    public void Initialize ()
    {
        AddListeners();
        menuAnimationsController.Initialize();
    }

    void AddListeners ()
    {
        view.OnMainVolumeChanged += HandleMainVolumeChanged;
        view.OnMusicVolumeChanged += HandleMusicVolumeChanged;
        view.OnSFXVolumeChanged += HandleSFXVolumeChanged;
        view.OnClose += HandleClose;
    }

    void RemoveListeners ()
    {
        view.OnMainVolumeChanged -= HandleMainVolumeChanged;
        view.OnMusicVolumeChanged -= HandleMusicVolumeChanged;
        view.OnSFXVolumeChanged -= HandleSFXVolumeChanged;
        view.OnClose -= HandleClose;
    }

    void HandleMainVolumeChanged (float value) => SettingsProvider.MainVolume = value;

    void HandleMusicVolumeChanged (float value) => SettingsProvider.MusicVolume = value;

    void HandleSFXVolumeChanged (float value) => SettingsProvider.SFXVolume = value;

    void HandleClose () => OnClose?.Invoke();

    void SyncView ()
    {
        view.MainVolumeSlider.value = SettingsProvider.MainVolume;
        view.MusicVolumeSlider.value = SettingsProvider.MusicVolume;
        view.SFXVolumeSlider.value = SettingsProvider.SFXVolume;
    }

    public void Open ()
    {
        SyncView();
        view.Open();
        menuAnimationsController.PlayOpen();
    }

    public void Close () => menuAnimationsController.PlayClose();

    public void Dispose ()
    {
        RemoveListeners();
    }
}