using System;

public class PauseModel : IPauseModel
{
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnRetry;
    public event Action OnQuit;

    readonly ISongModel songModel;
    
    bool paused;

    public PauseModel (ISongModel songModel)
    {
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        paused = false;
    }

    public PauseRequestResult HandleEscPressed ()
    {
        if (songModel.AllNotesRead)
            return PauseRequestResult.Ignored;
        if (paused)
            OnResume?.Invoke();
        else
            OnPause?.Invoke();
        paused = !paused;
        return paused ? PauseRequestResult.Paused : PauseRequestResult.Unpaused;
    }

    public void RaiseResume () => OnResume?.Invoke();
    public void RaiseRetry () => OnRetry?.Invoke();
    public void RaiseQuit () => OnQuit?.Invoke();

    public void Dispose ()
    {
    }
}