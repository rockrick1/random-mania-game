using UnityEngine;

public static class SettingsProvider
{
    const float DEFAULT_MAIN_VOLUME = .33f;
    const float DEFAULT_MUSIC_VOLUME = .33f;
    const float DEFAULT_SFX_VOLUME = .33f;
    
    const string MAIN_VOLUME_KEY = "mainVolume";
    const string MUSIC_VOLUME_KEY = "musicVolume";
    const string SFX_VOLUME_KEY = "sfxVolume";

    static AudioManager audioManager => AudioManager.GetOrCreate();

    public static float MainVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(MAIN_VOLUME_KEY))
                MainVolume = DEFAULT_MAIN_VOLUME;
            return PlayerPrefs.GetFloat(MAIN_VOLUME_KEY);
        }
        set
        {
            audioManager.SetMusicVolume(value * MusicVolume);
            audioManager.SetSFXVolume(value * SFXVolume);
            PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, value);
        }
    }

    public static float MusicVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
                MusicVolume = DEFAULT_MUSIC_VOLUME;
            return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        }
        set
        {
            audioManager.SetMusicVolume(MainVolume * value);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        }
    }

    public static float SFXVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY))
                SFXVolume = DEFAULT_SFX_VOLUME;
            return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        }
        set
        {
            audioManager.SetSFXVolume(MainVolume * value);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        }
    }
}