using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] AudioSource sfxPlayer;

    readonly Dictionary<string, AudioClip> sfxDict = new();
    
    public AudioManager ()
    {
    }

    public void Initialize ()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Hitsounds");
        foreach (AudioClip clip in clips)
            sfxDict.Add(clip.name, clip);
    }

    public void PlaySfx (string sfx)
    {
        sfxPlayer.clip = sfxDict[sfx];
        sfxPlayer.time = 0;
        sfxPlayer.Play();
    }
}