using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class ResultsController : IDisposable
{
    public event Action OnRetry;
    public event Action OnQuit;

    readonly ResultsView view;
    readonly SongController songController;
    readonly IScoreModel scoreModel;
    readonly ISongModel songModel;

    public ResultsController (
        ResultsView view,
        SongController songController,
        IScoreModel scoreModel,
        ISongModel songModel
    )
    {
        this.view = view;
        this.songController = songController;
        this.scoreModel = scoreModel;
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void SyncView ()
    {
        view.SetTitle(songModel.CurrentSongSettings.Id);
        view.SetCounts(
            scoreModel.NoteScores[HitScore.Perfect].ToString(),
            scoreModel.NoteScores[HitScore.Great].ToString(),
            scoreModel.NoteScores[HitScore.Okay].ToString(),
            scoreModel.NoteScores[HitScore.Miss].ToString()
        );
        view.SetScore(
            scoreModel.Score.FormatScore(),
            scoreModel.Combo.FormatCombo(),
            scoreModel.Accuracy.FormatAccuracy(),
            scoreModel.GetResult()
        );
        SyncOffsets();
    }

    void SyncOffsets ()
    {
        double earliestHit = scoreModel.NoteHitTimes.Min();
        double latestHit = scoreModel.NoteHitTimes.Max();
        double maxOffset = Math.Max(Math.Abs(earliestHit), Math.Abs(latestHit));
        view.SetOffsets($"{maxOffset * 1000:F0}");

        int sections = view.BarCount;
        double step = 2 * maxOffset / sections;
        Dictionary<int, int> hitsPerSection = new();
        for (int section = 0; section < sections; section++)
            hitsPerSection[section] = 0;
        foreach (double hitTime in scoreModel.NoteHitTimes)
        {
            double aux = -maxOffset + step;
            int section = 0;
            while (aux < hitTime)
            {
                aux += step;
                section++;
            }
            hitsPerSection[Math.Clamp(section, 0, sections - 1)]++;
        }
        int maxHitsInSection = hitsPerSection.Values.Max();
        for (int section = 0; section < sections; section++)
            view.SetOffsetsBarRatio(section, hitsPerSection[section] / (float)maxHitsInSection);
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