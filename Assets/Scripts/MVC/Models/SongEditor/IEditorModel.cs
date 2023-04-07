using System;
using UnityEngine;

public interface IEditorModel : IDisposable
{
    event Action<AudioClip, ISongSettings> OnSongLoaded;
    
    void Initialize ();
    void ProcessSong (AudioClip clip);
}