using UnityEngine;

public class SongView : MonoBehaviour
{
    [SerializeField] AudioSource Player;

    public void SetClip(AudioClip clip) => Player.clip = clip;

    public void Play() => Player.Play();
    
    public void Pause() => Player.Pause();
    
    public void Stop() => Player.Stop();
    
}