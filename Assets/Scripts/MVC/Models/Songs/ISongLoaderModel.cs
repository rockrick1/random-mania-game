using UnityEngine;

public interface ISongLoaderModel
{
    ISongSettings Settings { get; }
    AudioClip Audio { get; }
    
    void Initialize();
    void InitializeSong(string songJson);
}