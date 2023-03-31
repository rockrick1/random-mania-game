using System;
using UnityEngine;

public interface ISongEditorModel : IDisposable
{
    event Action<AudioClip, ISongSettings> OnSongLoaded;
    
    void Initialize ();
    void ProcessSong (AudioClip clip);
}