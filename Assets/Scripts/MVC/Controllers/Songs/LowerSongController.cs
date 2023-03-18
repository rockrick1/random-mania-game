using System;

public class LowerSongController : IDisposable
{
    readonly IInputManager inputManager;
    readonly LowerSongView view;

    public LowerSongController (LowerSongView view, IInputManager inputManager)
    {
        this.view = view;
        this.inputManager = inputManager;
    }

    public void Dispose ()
    {
        RemoveListeners();
    }

    public void Initialize ()
    {
        Addlisteners();
    }

    void Addlisteners ()
    {
        inputManager.OnHitterSelect += HandleHitterSelect;
    }

    void RemoveListeners ()
    {
        inputManager.OnHitterSelect -= HandleHitterSelect;
    }

    public void HandleHitterSelect (int index)
    {
        view.SetActiveHitter(index);
    }
}