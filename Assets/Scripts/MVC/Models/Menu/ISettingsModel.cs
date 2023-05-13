using System;

public interface ISettingsModel : IDisposable
{
    float MainVolume { get; }
    float MusicVolume { get; }
    float SFXVolume { get; }
    
    void Initialize ();
    void SetMainVolume (float value);
    void SetMusicVolume (float value);
    void SetSFXVolume (float value);
}