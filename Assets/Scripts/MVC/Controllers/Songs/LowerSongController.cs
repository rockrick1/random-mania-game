using System;

public class LowerSongController : IDisposable
{
    readonly IInputManager inputManager;
    readonly LowerSongView view;

    public float HitterYPos => view.HitterYPos;

    public LowerSongController (LowerSongView view, IInputManager inputManager)
    {
        this.view = view;
        this.inputManager = inputManager;
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

    void HandleHitterSelect (int index)
    {
        view.SetActiveHitter(index);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}