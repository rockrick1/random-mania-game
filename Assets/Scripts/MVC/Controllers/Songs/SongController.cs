using System;
using System.Threading.Tasks;

public class SongController
{
    const string HIT_SOUND = "soft-hitnormal";

    public event Action OnSongFinished;

    readonly SongView view;
    readonly PauseController pauseController;
    readonly ISongModel model;
    readonly IAudioManager audioManager;
    
    public SongController (SongView view,
        PauseController pauseController,
        ISongModel model,
        IAudioManager audioManager
    )
    {
        this.view = view;
        this.pauseController = pauseController;
        this.model = model;
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        AddListeners();
        try
        {
            StartSong().Start();
        }
        catch
        {
            //TODO find a way to do this better
        }
    }

    async Task StartSong ()
    {
        //TODO find a way to do this better
        await Task.Delay(500);
        audioManager.SetMusicClip(model.CurrentSongAudio);
        model.Play();
    }

    void AddListeners ()
    {
        model.OnAudioStartTimeReached += HandleAudioStartTimeReached;
        model.OnNoteHit += HandleNoteHit;
        model.OnLongNoteHit += HandleNoteHit;
        model.OnLongNoteReleased += HandleNoteHit;
        model.OnSongStartSkipped += HandleStartSkipped;
        model.OnSongFinished += HandleSongFinished;
        pauseController.OnPause += HandlePause;
        pauseController.OnResume += HandleResume;
        pauseController.OnRetry += HandleRetry;
        pauseController.OnQuit += HandleQuit;
    }

    void RemoveListeners ()
    {
        model.OnAudioStartTimeReached -= HandleAudioStartTimeReached;
        model.OnNoteHit -= HandleNoteHit;
        model.OnLongNoteHit -= HandleNoteHit;
        model.OnLongNoteReleased -= HandleNoteHit;
        model.OnSongStartSkipped -= HandleStartSkipped;
        model.OnSongFinished -= HandleSongFinished;
        pauseController.OnPause -= HandlePause;
        pauseController.OnResume -= HandleResume;
        pauseController.OnRetry -= HandleRetry;
        pauseController.OnQuit -= HandleQuit;
    }


    void HandleAudioStartTimeReached () => audioManager.PlayMusic();

    void HandleNoteHit (Note _, HitScore __)
    {
        //TODO add dynamic hit sound check based on note settings
        audioManager.PlaySfx(HIT_SOUND);
    }

    void HandleStartSkipped (float startingTime) => audioManager.SetMusicTime(startingTime);

    void HandleSongFinished () => OnSongFinished?.Invoke();

    void HandlePause ()
    {
        GameManager.IsPaused = true;
        audioManager.PauseMusic();
    }

    void HandleResume ()
    {
        GameManager.IsPaused = false;
        audioManager.PlayMusic();
    }
    void HandleRetry ()
    {
        GameManager.IsPaused = false;
    }

    void HandleQuit ()
    {
        GameManager.IsPaused = false;
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}