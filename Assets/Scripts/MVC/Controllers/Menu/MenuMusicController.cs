using System;
using UnityEngine;

public class MenuMusicController : IDisposable
{
    readonly IAudioManager audioManager;

    AudioClip mainMenuMusic;
    
    public MenuMusicController (
        IAudioManager audioManager
    )
    {
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        LoadMenuMusics();
        PlayMainMenuMusic();
    }

    void LoadMenuMusics ()
    {
        mainMenuMusic = Resources.Load<AudioClip>("Music/mainMenu");
    }

    void PlayMainMenuMusic ()
    {
        audioManager.SetMusicClip(mainMenuMusic);
        audioManager.PlayMusic(true, true);
    }

    void AddListeners ()
    {
        // view.OnOpenSongMenu += HandleOpenSongMenu;
        // view.OnOpenEditor += HandleOpenEditor;
        // view.OnOpenSettings += HandleOpenSettings;
        // view.OnQuit += HandleQuit;
    }

    void RemoveListeners ()
    {
        // view.OnOpenSongMenu -= HandleOpenSongMenu;
        // view.OnOpenEditor -= HandleOpenEditor;
        // view.OnOpenSettings -= HandleOpenSettings;
        // view.OnQuit -= HandleQuit;
    }

    // void HandleOpenSongMenu () => OnOpenSongMenu?.Invoke();
    //
    // void HandleOpenEditor () => OnOpenEditor?.Invoke();
    //
    // void HandleOpenSettings () => OnOpenSettings?.Invoke();
    //
    // void HandleQuit () => OnQuit?.Invoke();

    public void Dispose ()
    {
        try
        {
            audioManager.PauseMusic();
        }
        catch { }
        RemoveListeners();
    }
}