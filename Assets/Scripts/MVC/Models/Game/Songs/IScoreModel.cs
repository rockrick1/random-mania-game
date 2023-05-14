using System;
using System.Collections.Generic;

public interface IScoreModel : IDisposable
{
    event Action<int> OnComboChanged;
    event Action<int> OnScoreChanged;
    event Action<float> OnAccuracyChanged;
    
    int Score { get; }
    int Combo { get; }
    float Accuracy { get; }
    Dictionary<HitScore, int> NoteScores { get; }

    void Initialize ();
}