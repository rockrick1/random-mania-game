using UnityEngine;

public interface IAudioManager
{
    bool HasMusicClip { get; }
    float MusicLength { get; }
    float MusicTime { get; }
    bool IsPlayingMusic { get; }
    
    void SetMusicClip (AudioClip clip);
    void PlayMusic (bool loop = false, bool fadeIn = false);
    void PauseMusic ();
    
    void PlayPauseMusic ();
    void PlaySFX (string sfx);
    void SetMusicTime (float time);
    void AnimateMusicTime (float time);
    void SkipMusicTime (float time);
    void SetMusicPlaybackSpeed (float speed);
    void SetMusicVolume (float value);
    void SetSFXVolume (float value);
}