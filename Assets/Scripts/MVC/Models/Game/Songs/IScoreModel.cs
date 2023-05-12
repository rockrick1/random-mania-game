using System;

public interface IScoreModel : IDisposable
{
    event Action<int> OnComboChanged;
    event Action<int> OnScoreChanged;
    event Action<float> OnAccuracyChanged;

    void Initialize ();
}