using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;
    
    public bool HasMusicClip => musicPlayer.clip != null;
    public float MusicLength => HasMusicClip ? musicPlayer.clip.length : 0f;
    public float MusicTime => musicPlayer.time;

    static AudioManager _instance;

    readonly Dictionary<string, AudioClip> sfxDict = new();
    
    public static AudioManager GetOrCreate ()
    {
        if (_instance != null)
            return _instance;
        AudioManager instance = Instantiate(Resources.Load<AudioManager>("AudioManager"));
        _instance = instance;
        DontDestroyOnLoad(_instance);
        _instance.Initialize();
        return _instance;
    }

    void Initialize ()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Hitsounds");
        foreach (AudioClip clip in clips)
            sfxDict.Add(clip.name, clip);
    }

    public void SetMusicClip (AudioClip clip)
    {
        musicPlayer.time = 0;
        musicPlayer.clip = clip;
    }

    public void PlayMusic () => musicPlayer.Play();

    public void PauseMusic () => musicPlayer.Pause();
    
    public void PlayPauseMusic ()
    {
        if (musicPlayer.isPlaying)
            musicPlayer.Pause();
        else
            musicPlayer.Play();
    }

    public void PlaySfx (string sfx)
    {
        sfxPlayer.clip = sfxDict[sfx];
        sfxPlayer.time = 0;
        sfxPlayer.Play();
    }
    
    public void SetMusicTime (float time)
    {
        if (!HasMusicClip)
            return;
        musicPlayer.time = Mathf.Clamp(time, 0, musicPlayer.clip.length - .1f);
    }
    
    public void SkipMusicTime (float time)
    {
        if (!HasMusicClip)
            return;
        musicPlayer.time = Mathf.Clamp(musicPlayer.time + time, 0, musicPlayer.clip.length - .1f);
    }

    public void SetMusicPlaybackSpeed (float speed)
    {
        musicPlayer.outputAudioMixerGroup.audioMixer.SetFloat("pitch", 1f / speed);
        musicPlayer.pitch = speed;
    }

    public void SetMusicVolume (float value) => musicPlayer.volume = value;

    public void SetSFXVolume (float value) => sfxPlayer.volume = value;
}