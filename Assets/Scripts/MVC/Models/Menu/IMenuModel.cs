using System;

public interface IMenuModel : IDisposable
{
    event Action OnBackPressed;
    
    void Initialize ();
}