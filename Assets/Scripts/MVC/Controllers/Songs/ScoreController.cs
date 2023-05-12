using System;
using System.Globalization;

public class ScoreController : IDisposable
{
    const string ACCURACY_FORMAT = "{0}%";
    
    readonly ScoreView view;
    readonly IScoreModel model;

    public ScoreController (ScoreView view, IScoreModel model)
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
        model.OnScoreChanged += HandleScoreChanged;
        model.OnAccuracyChanged += HandleAccuracyChanged;
    }

    void RemoveListeners ()
    {
        model.OnScoreChanged -= HandleScoreChanged;
        model.OnAccuracyChanged -= HandleAccuracyChanged;
    }

    void HandleScoreChanged (int combo) => view.SetScore(combo.ToString());

    void HandleAccuracyChanged (float accuracy) =>
        view.SetAccuracy(string.Format(ACCURACY_FORMAT, (accuracy * 100).ToString("F2", CultureInfo.InvariantCulture)));

    public void Dispose ()
    {
        RemoveListeners();
    }
}