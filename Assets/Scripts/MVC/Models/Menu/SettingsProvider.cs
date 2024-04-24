using UnityEngine;

public static class SettingsProvider
{
    const float DEFAULT_MAIN_VOLUME = .33f;
    const float DEFAULT_MUSIC_VOLUME = .33f;
    const float DEFAULT_SFX_VOLUME = .33f;
    
    const float DEFAULT_APPROACH_RATE = 1.5f;
    
    const string MAIN_VOLUME_KEY = "mainVolume";
    const string MUSIC_VOLUME_KEY = "musicVolume";
    const string SFX_VOLUME_KEY = "sfxVolume";

    const string APPROACH_RATE_KEY = "approachRate";
    
    static AudioManager audioManager => AudioManager.GetOrCreate();

    public static void Initialize ()
    {
        if (!PlayerPrefs.HasKey(MAIN_VOLUME_KEY))
            PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, DEFAULT_MAIN_VOLUME);
        if (!PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, DEFAULT_MUSIC_VOLUME);
        if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY))
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, DEFAULT_SFX_VOLUME);
        
        if (!PlayerPrefs.HasKey(APPROACH_RATE_KEY))
            PlayerPrefs.SetFloat(APPROACH_RATE_KEY, DEFAULT_APPROACH_RATE);
    }

    public static float MainVolume
    {
        get => PlayerPrefs.GetFloat(MAIN_VOLUME_KEY);
        set
        {
            audioManager.SetMusicVolume(value * MusicVolume);
            audioManager.SetSFXVolume(value * SFXVolume);
            PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, value);
        }
    }

    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        set
        {
            audioManager.SetMusicVolume(MainVolume * value);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        }
    }

    public static float SFXVolume
    {
        get => PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        set
        {
            audioManager.SetSFXVolume(MainVolume * value);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        }
    }

    public static float ApproachRate
    {
        get => PlayerPrefs.GetFloat(APPROACH_RATE_KEY);
        set => PlayerPrefs.SetFloat(APPROACH_RATE_KEY, value);
    }
}