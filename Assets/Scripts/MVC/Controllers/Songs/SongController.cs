using System;
using System.Threading.Tasks;
using UnityEngine;

public class SongController
{
    const string HIT_SOUND = "soft-hitnormal";

    public event Action OnSongFinished;

    readonly SongView view;
    readonly PauseController pauseController;
    readonly ISongModel model;
    readonly IAudioManager audioManager;
    readonly SongLoader songLoader;
    
    public SongController (SongView view,
        PauseController pauseController,
        ISongModel model,
        IAudioManager audioManager,
        SongLoader songLoader
    )
    {
        this.view = view;
        this.pauseController = pauseController;
        this.model = model;
        this.audioManager = audioManager;
        this.songLoader = songLoader;
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
        audioManager.SetMusicClip(songLoader.GetSelectedSongAudio());
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

    void HandleStartSkipped (float skippedTime) => audioManager.SkipMusicTime(skippedTime);

    void HandleSongFinished ()
    {
        audioManager.PauseMusic();
        OnSongFinished?.Invoke();
    }

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