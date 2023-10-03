using UnityEngine;

public interface IAudioManager
{
    bool HasMusicClip { get; }
    float MusicLength { get; }
    float MusicTime { get; }
    
    void SetMusicClip (AudioClip clip);
    void PlayMusic ();
    void PauseMusic ();
    
    void PlayPauseMusic ();
    void PlaySFX (string sfx);
    void SetMusicTime (float time);
    void SkipMusicTime (float time);
    void SetMusicPlaybackSpeed (float speed);
    void SetMusicVolume (float value);
    void SetSFXVolume (float value);
}