using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel : IScoreModel
{
    public event Action<int> OnComboChanged;
    public event Action OnPlayComboBreakSFX;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnAccuracyChanged;

    public Dictionary<HitScore, int> NoteScores { get; } = new()
    {
        {HitScore.Perfect, 0},
        {HitScore.Great, 0},
        {HitScore.Okay, 0},
        {HitScore.Miss, 0},
    };

    public List<double> NoteHitTimes { get; } = new();
    
    public int Combo
    {
        get => combo;
        private set
        {
            if (combo != value)
                OnComboChanged?.Invoke(value);
            combo = value;
        }
    }

    public int Score
    {
        get => score;
        private set
        {
            if (score != value)
                OnScoreChanged?.Invoke(value);
            score = value;
        }
    }
    
    public float Accuracy
    {
        get => accuracy;
        private set
        {
            OnAccuracyChanged?.Invoke(value);
            accuracy = value;
        }
    }

    public float MaximumHitWindow => okayHitWindow;
    
    readonly ISongModel songModel;
    readonly SongLoader songLoader;

    int combo;
    int score;
    float accuracy = 1;
    bool holdingLongNote;
    
    float perfectHitWindow;
    float greatHitWindow;
    float okayHitWindow;

    public ScoreModel (
        ISongModel songModel,
        SongLoader songLoader
    )
    {
        this.songModel = songModel;
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
        CoroutineRunner.Instance.StartRoutine(nameof(LongNoteHoldRoutine), LongNoteHoldRoutine());
    }
    
    public HitScore GetHitScore (double timeToNoteHit)
    {
        double absValue = Math.Abs(timeToNoteHit);
        if (absValue <= perfectHitWindow)
            return HitScore.Perfect;
        if (absValue <= greatHitWindow)
            return HitScore.Great;
        if (absValue <= okayHitWindow)
            return HitScore.Okay;
        return HitScore.Miss;
    }

    public string GetResult ()
    {
        if (NoteScores[HitScore.Miss] == 0)
            return "FC";
        if (accuracy < .7f)
            return "D";
        if (accuracy < .85f)
            return "C";
        if (accuracy < .9f)
            return "B";
        if (accuracy < .95f)
            return "A";
        return "S";
    }

    void AddListeners ()
    {
        songModel.OnNoteHit += HandleNoteHit;
        songModel.OnLongNoteHit += HandleLongNoteHit;
        songModel.OnLongNoteReleased += HandleLongNoteReleased;
        songModel.OnNoteMissed += HandleNoteMissed;
        songModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        songModel.OnNoteHit -= HandleNoteHit;
        songModel.OnLongNoteHit -= HandleLongNoteHit;
        songModel.OnLongNoteReleased -= HandleLongNoteReleased;
        songModel.OnNoteMissed -= HandleNoteMissed;
        songModel.OnSongLoaded -= HandleSongLoaded;
    }

    IEnumerator LongNoteHoldRoutine ()
    {
        while (true)
        {
            if (!holdingLongNote || GameManager.IsPaused)
            {
                yield return null;
                continue;
            }
            Combo++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void HandleNoteHit (Note _, double timeToNote)
    {
        NoteHitTimes.Add(-timeToNote);
        HitScore hitScore = GetHitScore(timeToNote);
        NoteScores[hitScore]++;
        Combo++;
        UpdateScore(hitScore);
        UpdateAccuracy();
    }

    void HandleLongNoteHit (Note note, double timeToNote)
    {
        holdingLongNote = true;
    }

    void HandleLongNoteReleased (Note note, double timeToNote)
    {
        NoteHitTimes.Add(-timeToNote);
        HitScore hitScore = GetHitScore(timeToNote);
        if (hitScore != HitScore.Miss)
        {
            UpdateScore(hitScore);
            NoteScores[hitScore]++;
            UpdateAccuracy();
        }
        else
            HandleNoteMissed(note);
        holdingLongNote = false;
    }

    void HandleNoteMissed (Note _)
    {
        if (Combo >= 10)
            OnPlayComboBreakSFX?.Invoke();
        Combo = 0;
        NoteScores[HitScore.Miss]++;
        UpdateAccuracy();
    }

    void HandleSongLoaded ()
    {
        float difficulty = songLoader.SelectedSongSettings.Difficulty;
        perfectHitWindow = (80 - 6 * difficulty) / 1000f;
        greatHitWindow = (140 - 8 * difficulty) / 1000f;
        okayHitWindow = (200 - 10 * difficulty) / 1000f;
    }

    void UpdateScore (HitScore hitScore) => Score += (int) ((float) hitScore * Mathf.Pow(combo, .4f));
    
    void UpdateAccuracy ()
    {
        Accuracy = (((float)HitScore.Perfect * NoteScores[HitScore.Perfect]) + ((float)HitScore.Great * NoteScores[HitScore.Great]) + ((float)HitScore.Okay * NoteScores[HitScore.Okay])) /
                   ((float)HitScore.Perfect * (NoteScores[HitScore.Perfect] + NoteScores[HitScore.Great] + NoteScores[HitScore.Okay] + NoteScores[HitScore.Miss]));
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}