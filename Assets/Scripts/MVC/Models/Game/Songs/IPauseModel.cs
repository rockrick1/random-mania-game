using System;

public interface IPauseModel : IDisposable
{
    event Action OnPause;
    event Action OnResume;
    event Action OnRetry;
    event Action OnQuit;
    
    void Initialize ();
    PauseRequestResult HandleEscPressed ();
    void RaiseResume ();
    void RaiseRetry ();
    void RaiseQuit ();
}