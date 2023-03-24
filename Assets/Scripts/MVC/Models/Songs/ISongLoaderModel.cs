using UnityEngine;

public interface ISongLoaderModel
{
    SongSettings Settings { get; }
    AudioClip Audio { get; }

    void Initialize ();
    void LoadSong (string songJson);
}