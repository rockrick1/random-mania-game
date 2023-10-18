using System;

public class ScoreController : IDisposable
{
    const string ACCURACY_FORMAT = "{0}%";
    
    readonly ScoreView view;
    readonly IScoreModel model;
    readonly ISongModel songModel;

    public ScoreController (
        ScoreView view,
        IScoreModel model,
        ISongModel songModel
    )
    {
        this.view = view;
        this.model = model;
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnScoreChanged += HandleScoreChanged;
        model.OnAccuracyChanged += HandleAccuracyChanged;
        songModel.OnSongStarted += HandleSongStarted;
        songModel.OnSongFinished += HandleSongFinished;
    }

    void RemoveListeners ()
    {
        model.OnScoreChanged -= HandleScoreChanged;
        model.OnAccuracyChanged -= HandleAccuracyChanged;
        songModel.OnSongStarted -= HandleSongStarted;
        songModel.OnSongFinished -= HandleSongFinished;
    }

    void HandleScoreChanged (int combo) => view.SetScore(combo.ToString());

    void HandleAccuracyChanged (float accuracy) =>
        view.SetAccuracy(accuracy.FormatAccuracy());

    void HandleSongStarted () => view.PlayFadeInAnimation();

    void HandleSongFinished () => view.PlayFadeOutAnimation();

    public void Dispose ()
    {
        RemoveListeners();
    }
}