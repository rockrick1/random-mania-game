using UnityEngine;

public interface IAudioManager
{
    void SetMusicClip (AudioClip clip);
    void PlayMusic ();
    void PauseMusic ();
    void PlaySfx (string sfx);
    void SetMusicVolume (float value);
    void SetSFXVolume (float value);
}