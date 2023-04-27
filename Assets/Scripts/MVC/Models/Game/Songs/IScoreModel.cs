using System;

public interface IScoreModel : IDisposable
{
    event Action<int> OnComboChanged;

    void Initialize ();
}