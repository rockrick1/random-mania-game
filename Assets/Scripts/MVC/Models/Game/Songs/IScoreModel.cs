using System;
using System.Collections.Generic;

public interface IScoreModel : IDisposable
{
    event Action<int> OnComboChanged;
    event Action OnPlayComboBreakSFX;
    event Action<int> OnScoreChanged;
    event Action<float> OnAccuracyChanged;
    
    int Score { get; }
    int Combo { get; }
    float Accuracy { get; }
    Dictionary<HitScore, int> NoteScores { get; }
    List<double> NoteHitTimes { get; }
    float MaximumHitWindow { get; }

    void Initialize ();
    HitScore GetHitScore (double timeToNoteHit);
    string GetResult ();
}