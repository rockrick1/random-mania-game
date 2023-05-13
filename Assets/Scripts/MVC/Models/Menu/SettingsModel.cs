using UnityEngine;

public class SettingsModel : ISettingsModel
{
    const string MAIN_VOLUME_KEY = "mainVolume";
    const string MUSIC_VOLUME_KEY = "musicVolume";
    const string SFX_VOLUME_KEY = "sfxVolume";
    
    readonly IAudioManager audioManager;

    public float MainVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }

    public SettingsModel (IAudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        LoadOrCreatePlayerPrefs();
        AddListeners();
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    public void SetMainVolume (float value)
    {
        MainVolume = value;
        PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, value);
        audioManager.SetMusicVolume(value * MusicVolume);
        audioManager.SetSFXVolume(value * SFXVolume);
    }

    public void SetMusicVolume (float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        audioManager.SetSFXVolume(MainVolume * value);
    }

    public void SetSFXVolume (float value)
    {
        SFXVolume = value;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        audioManager.SetSFXVolume(SFXVolume * value);
    }

    void LoadOrCreatePlayerPrefs ()
    {
        if (!PlayerPrefs.HasKey(MAIN_VOLUME_KEY))
            PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, 1f);
        MainVolume = PlayerPrefs.GetFloat(MAIN_VOLUME_KEY);
        if (!PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, 1f);
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY))
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, 1f);
        SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}