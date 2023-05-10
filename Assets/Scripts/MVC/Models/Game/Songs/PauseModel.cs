using System;

public class PauseModel : IPauseModel
{
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnRetry;
    public event Action OnQuit;

    bool paused;
    
    public PauseModel ()
    {
    }

    public void Initialize ()
    {
        paused = false;
    }

    public bool HandleEscPressed ()
    {
        if (paused)
            OnResume?.Invoke();
        else
            OnPause?.Invoke();
        paused = !paused;
        return paused;
    }

    public void RaiseResume () => OnResume?.Invoke();
    public void RaiseRetry () => OnRetry?.Invoke();
    public void RaiseQuit () => OnQuit?.Invoke();

    public void Dispose ()
    {
    }
}