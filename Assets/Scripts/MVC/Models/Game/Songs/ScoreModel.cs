﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel : IScoreModel
{
    public event Action<int> OnComboChanged;
    public event Action OnPlayComboBreakSFX;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnAccuracyChanged;
    
    public Dictionary<HitScore, int> NoteScores => noteScores;
    
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
    
    readonly ISongModel songModel;
    readonly Dictionary<HitScore, int> noteScores = new()
    {
        {HitScore.Perfect, 0},
        {HitScore.Great, 0},
        {HitScore.Okay, 0},
        {HitScore.Miss, 0},
    };

    
    int combo;
    int score;
    float accuracy = 1f;
    bool holdingLongNote;

    public ScoreModel (ISongModel songModel)
    {
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        AddListeners();
        CoroutineRunner.Instance.StartRoutine(nameof(LongNoteHoldRoutine), LongNoteHoldRoutine());
    }

    void AddListeners ()
    {
        songModel.OnNoteHit += HandleNoteHit;
        songModel.OnLongNoteHit += HandleLongNoteHit;
        songModel.OnLongNoteReleased += HandleLongNoteReleased;
        songModel.OnNoteMissed += HandleNoteMissed;
    }

    void RemoveListeners ()
    {
        songModel.OnNoteHit -= HandleNoteHit;
        songModel.OnLongNoteHit -= HandleLongNoteHit;
        songModel.OnLongNoteReleased -= HandleLongNoteReleased;
        songModel.OnNoteMissed -= HandleNoteMissed;
    }

    IEnumerator LongNoteHoldRoutine ()
    {
        while (true)
        {
            if (!holdingLongNote)
            {
                yield return null;
                continue;
            }
            Combo++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void HandleNoteHit (Note _, HitScore hitScore)
    {
        noteScores[hitScore]++;
        Combo++;
        UpdateScore(hitScore);
        UpdateAccuracy();
    }

    void HandleLongNoteHit (Note note, HitScore hitScore)
    {
        holdingLongNote = true;
    }

    void HandleLongNoteReleased (Note note, HitScore hitScore)
    {
        if (hitScore != HitScore.Miss)
        {
            UpdateScore(hitScore);
            noteScores[hitScore]++;
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
        noteScores[HitScore.Miss]++;
        UpdateAccuracy();
    }

    void UpdateScore (HitScore hitScore) => Score += (int) ((float) hitScore * Mathf.Pow(combo, .4f));
    
    void UpdateAccuracy ()
    {
        Accuracy = (((float)HitScore.Perfect * noteScores[HitScore.Perfect]) + ((float)HitScore.Great * noteScores[HitScore.Great]) + ((float)HitScore.Okay * noteScores[HitScore.Okay])) /
                   ((float)HitScore.Perfect * (noteScores[HitScore.Perfect] + noteScores[HitScore.Great] + noteScores[HitScore.Okay] + noteScores[HitScore.Miss]));
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}