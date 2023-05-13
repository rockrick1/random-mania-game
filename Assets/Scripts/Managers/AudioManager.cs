using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;

    public static AudioManager Instance;

    readonly Dictionary<string, AudioClip> sfxDict = new();
    
    public static AudioManager GetOrCreate ()
    {
        if (Instance != null)
            return Instance;
        AudioManager instance = Instantiate(Resources.Load<AudioManager>("AudioManager"));
        Instance = instance;
        DontDestroyOnLoad(Instance);
        Instance.Initialize();
        return Instance;
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

    public void PlaySfx (string sfx)
    {
        sfxPlayer.clip = sfxDict[sfx];
        sfxPlayer.time = 0;
        sfxPlayer.Play();
    }

    public void SetMusicVolume (float value) => musicPlayer.volume = value;

    public void SetSFXVolume (float value) => sfxPlayer.volume = value;
}