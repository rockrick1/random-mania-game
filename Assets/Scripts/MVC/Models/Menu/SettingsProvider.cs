using UnityEngine;

public static class SettingsProvider
{
    const string MAIN_VOLUME_KEY = "mainVolume";
    const string MUSIC_VOLUME_KEY = "musicVolume";
    const string SFX_VOLUME_KEY = "sfxVolume";

    static AudioManager audioManager => AudioManager.GetOrCreate();

    public static float MainVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(MAIN_VOLUME_KEY))
                MainVolume = 1;
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
                MusicVolume = 1;
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
                SFXVolume = 1;
            return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        }
        set
        {
            audioManager.SetSFXVolume(MainVolume * value);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        }
    }
}