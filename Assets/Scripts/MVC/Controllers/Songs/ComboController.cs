using System;

public class ComboController : IDisposable
{
    readonly ComboView view;
    readonly IScoreModel model;

    public ComboController (ComboView view, IScoreModel model)
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
        model.OnComboChanged += HandleComboChanged;
    }
    
    void RemoveListeners ()
    {
        model.OnComboChanged -= HandleComboChanged;
    }

    void HandleComboChanged (int combo)
    {
        view.SetCombo(combo);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}