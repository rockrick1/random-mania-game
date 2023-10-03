using System;

public class ComboController : IDisposable
{
    readonly ComboView view;
    readonly IScoreModel model;
    readonly IAudioManager audioManager;

    public ComboController (
        ComboView view,
        IScoreModel model,
        IAudioManager audioManager
    )
    {
        this.view = view;
        this.model = model;
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
    }

    void RemoveListeners ()
    {
        model.OnComboChanged -= HandleComboChanged;
        model.OnPlayComboBreakSFX -= HandlePlayComboBreakSFX;
    }

    void HandleComboChanged (int combo) => view.SetCombo(combo);

    void HandlePlayComboBreakSFX () => audioManager.PlaySFX("combobreak");

    public void Dispose ()
    {
        RemoveListeners();
    }
}