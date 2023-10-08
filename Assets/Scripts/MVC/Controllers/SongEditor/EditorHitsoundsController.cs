using System;

public class EditorHitsoundsController : IDisposable
{
    readonly IAudioManager audioManager;
    readonly IEditorHitsoundsModel model;

    public EditorHitsoundsController (
        IAudioManager audioManager,
        IEditorHitsoundsModel model
    )
    {
        this.audioManager = audioManager;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnPlayHitsound += HandlePlayHitsound;
    }

    void RemoveListeners ()
    {
        model.OnPlayHitsound -= HandlePlayHitsound;
    }

    void HandlePlayHitsound() => audioManager.PlaySFX("soft-hitnormal");

    public void Dispose ()
    {
        RemoveListeners();
    }
}