using System;

public class ComboController : IDisposable
{
    readonly ComboView view;
    readonly IComboModel model;

    public ComboController (ComboView view, IComboModel model)
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