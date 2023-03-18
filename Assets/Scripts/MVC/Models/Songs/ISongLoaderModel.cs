using UnityEngine;

public interface ISongLoaderModel
{
    ISongSettings Settings { get; }
    AudioClip Audio { get; }
    
    void Initialize();
    void LoadSong(string songJson);
}