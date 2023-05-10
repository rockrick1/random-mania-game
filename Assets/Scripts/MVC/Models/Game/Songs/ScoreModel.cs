using System;
using System.Collections;
using UnityEngine;

public class ScoreModel : IScoreModel
{
    public event Action<int> OnComboChanged;
    
    readonly ISongModel songModel;

    public int Combo
    {
        get => combo;
        set
        {
            if (combo != value)
                OnComboChanged?.Invoke(value);
            combo = value;
        }
    }
    
    int combo;
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

    void HandleNoteHit (Note _, HitScore __)
    {
        //TODO calculate score here
        Combo++;
    }

    void HandleLongNoteHit (Note note, HitScore score)
    {
        holdingLongNote = true;
    }

    void HandleLongNoteReleased (Note note, HitScore score)
    {
        holdingLongNote = false;
    }

    void HandleNoteMissed (Note _) => Combo = 0;

    public void Dispose ()
    {
        RemoveListeners();
    }
}