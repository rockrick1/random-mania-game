using UnityEngine;

public class SongView : MonoBehaviour
{
    [SerializeField] AudioSource Player;
    [SerializeField] LowerSongView lowerSongView;

    public LowerSongView LowerSongView => lowerSongView;

    public void SetClip (AudioClip clip)
    {
        Player.clip = clip;
    }

    public void Play ()
    {
        Player.Play();
    }

    public void Pause ()
    {
        Player.Pause();
    }

    public void Stop ()
    {
        Player.Stop();
    }
}