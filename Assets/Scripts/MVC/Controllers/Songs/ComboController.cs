using System;

public class ComboController : IDisposable
{
    readonly ComboView view;
    readonly IScoreModel model;
    readonly ISongModel songModel;
    readonly IAudioManager audioManager;

    public ComboController (
        ComboView view,
        IScoreModel model,
        ISongModel songModel,
        IAudioManager audioManager
    )
    {
        this.view = view;
        this.model = model;
        this.songModel = songModel;
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnComboChanged += HandleComboChanged;
        model.OnPlayComboBreakSFX += HandlePlayComboBreakSFX;
        songModel.OnSongStarted += HandleSongStarted;
        songModel.OnSongFinished += HandleSongFinished;
    }

    void RemoveListeners ()
    {
        model.OnComboChanged -= HandleComboChanged;
        model.OnPlayComboBreakSFX -= HandlePlayComboBreakSFX;
        songModel.OnSongStarted -= HandleSongStarted;
        songModel.OnSongFinished -= HandleSongFinished;
    }

    void HandleComboChanged (int combo) => view.SetCombo(combo);

    void HandlePlayComboBreakSFX () => audioManager.PlaySFX("combobreak");

    void HandleSongStarted () => view.PlayFadeInAnimation();

    void HandleSongFinished () => view.PlayFadeOutAnimation();

    public void Dispose ()
    {
        RemoveListeners();
    }
}