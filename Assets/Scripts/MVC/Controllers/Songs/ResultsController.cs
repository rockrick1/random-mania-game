using System;
using UnityEngine.SceneManagement;

public class ResultsController : IDisposable
{
    public event Action OnRetry;
    public event Action OnQuit;

    readonly ResultsView view;
    readonly SongController songController;
    readonly IGameInputManager inputManager;
    readonly IScoreModel scoreModel;

    public ResultsController (ResultsView view, SongController songController, IScoreModel scoreModel)
    {
        this.view = view;
        this.songController = songController;
        this.scoreModel = scoreModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void SyncView ()
    {
        view.SetCounts(
            scoreModel.NoteScores[HitScore.Perfect].ToString(),
            scoreModel.NoteScores[HitScore.Great].ToString(),
            scoreModel.NoteScores[HitScore.Okay].ToString(),
            scoreModel.NoteScores[HitScore.Miss].ToString()
        );
        view.SetScore(scoreModel.Score.FormatScore(), scoreModel.Combo.FormatCombo(), scoreModel.Accuracy.FormatAccuracy());
    }

    void AddListeners ()
    {
        view.RetryButton.OnLeftClick.AddListener(HandleRetry);
        view.QuitButton.OnLeftClick.AddListener(HandleQuit);
        songController.OnSongFinished += HandleSongFinished;
    }

    void RemoveListeners ()
    {
        view.RetryButton.OnLeftClick.RemoveAllListeners();
        view.QuitButton.OnLeftClick.RemoveAllListeners();
        songController.OnSongFinished -= HandleSongFinished;
    }

    void HandleEscPressed ()
    {
        view.Close(() => OnQuit?.Invoke());
    }

    void HandleRetry ()
    {
        view.Close(() =>
        {
            OnRetry?.Invoke();
            SceneManager.LoadScene("Game");
        });
    }

    void HandleQuit ()
    {
        view.Close(() =>
        {
            OnQuit?.Invoke();
            SceneManager.LoadScene("MainMenu");
        });
    }

    void HandleSongFinished ()
    {
        SyncView();
        view.Open();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}