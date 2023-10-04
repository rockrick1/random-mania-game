using System;

public class SettingsController : IDisposable
{
    public event Action OnClose;
    
    readonly SettingsView view;
    readonly ISettingsModel model;

    readonly MenuAnimationsController menuAnimationsController;
    
    public SettingsController (SettingsView view, ISettingsModel model)
    {
        this.view = view;
        this.model = model;
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

    void HandleMainVolumeChanged (float value) => model.SetMainVolume(value);

    void HandleMusicVolumeChanged (float value) => model.SetMusicVolume(value);

    void HandleSFXVolumeChanged (float value) => model.SetSFXVolume(value);

    void HandleClose () => OnClose?.Invoke();

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
        menuAnimationsController.PlayOpen();
    }

    public void Close () => menuAnimationsController.PlayClose();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}