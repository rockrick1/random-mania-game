using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    const int MAX_SFX_SOURCES = 10;
    
    [SerializeField] AudioSource musicPlayer;
    
    public bool HasMusicClip => musicPlayer.clip != null;
    public float MusicLength => HasMusicClip ? musicPlayer.clip.length : 0f;
    public float MusicTime => musicPlayer.time;
    public bool IsPlayingMusic => musicPlayer.isPlaying;

    static AudioManager _instance;

    readonly Dictionary<string, AudioClip> sfxDict = new();
    readonly List<AudioSource> sfxPlayers = new();

    int sfxSourceToUse;
    
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
        AudioClip[] clips = Resources.LoadAll<AudioClip>("SFX");
        foreach (AudioClip clip in clips)
            sfxDict.Add(clip.name, clip);
        for (int i = 0; i < MAX_SFX_SOURCES; i++)
            sfxPlayers.Add(gameObject.AddComponent<AudioSource>());
        SetVolumes();
    }

    public void SetMusicClip (AudioClip clip)
    {
        musicPlayer.time = 0;
        musicPlayer.clip = clip;
    }

    public void PlayMusic (bool loop = false, bool fadeIn = false)
    {
        musicPlayer.loop = loop;
        musicPlayer.Play();
        musicPlayer.DOFade(musicPlayer.volume, 1).From(0);
    }

    public void PauseMusic () => musicPlayer.Pause();
    
    public void PlayPauseMusic ()
    {
        if (musicPlayer.isPlaying)
            musicPlayer.Pause();
        else
            musicPlayer.Play();
    }

    public void PlaySFX (string sfx)
    {
        AudioSource sfxPlayer = sfxPlayers[sfxSourceToUse];
        sfxPlayer.clip = sfxDict[sfx];
        sfxPlayer.time = 0;
        sfxPlayer.Play();
        sfxSourceToUse = (sfxSourceToUse + 1) % MAX_SFX_SOURCES;
    }
    
    public void SetMusicTime (float time)
    {
        if (!HasMusicClip)
            return;
        musicPlayer.time = Mathf.Clamp(time, 0, musicPlayer.clip.length - .1f);
    }

    Tween musicTimeTween;
    
    public void AnimateMusicTime (float time)
    {
        if (!HasMusicClip)
            return;
        musicTimeTween?.Kill();
        float value = Mathf.Clamp(time, 0, musicPlayer.clip.length - .1f);
        musicTimeTween = DOTween.To(() => musicPlayer.time, x => musicPlayer.time = x, value, .2f)
            .SetEase(Ease.OutCubic);
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

    public void SetSFXVolume (float value)
    {
        foreach (AudioSource sfxPlayer in sfxPlayers)
            sfxPlayer.volume = value;
    }

    void SetVolumes ()
    {
        SetMusicVolume(SettingsProvider.MainVolume * SettingsProvider.MusicVolume);
        SetSFXVolume(SettingsProvider.MainVolume * SettingsProvider.SFXVolume);
    }
}