using System;

public interface IComboModel : IDisposable
{
    event Action<int> OnComboChanged;

    void Initialize ();
}